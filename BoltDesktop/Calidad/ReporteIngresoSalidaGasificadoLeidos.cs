using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Configuration;
using Telerik.WinControls.UI.Localization;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using System.Reflection;
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.Data;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class ReporteIngresoSalidaGasificadoLeidos : Form
    {
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult selectedItem;
        private List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult> result;
        SAS_RegistroGasificadoController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private int incluirTicketsLeidos;
        private SAS_RegistroGasificadoAllByIDResult selectItemById;
        public MesController MesesNeg;
        private int ticket = 0;
        private int CodigoExoneracion = 0;
        public int CodigoGasificado = 0;
        private string lecturaDeTicket;
        private DateTime fechaRegistroTicket;

        //chCantidad

        public ReporteIngresoSalidaGasificadoLeidos()
        {
            InitializeComponent();
            CargarMeses();
            ObtenerFechasIniciales();
            conection = "NSFAJA";
            user = new SAS_USUARIOS();
            user.IdUsuario = "eaurazo";
            user.NombreCompleto = "Erick Aurazo";
            user.IdCodigoGeneral = "100369";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

            Consult();
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            //btnAnular.Enabled = true;
            //btnEliminarRegistro.Enabled = true;
            //btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
        }

        public ReporteIngresoSalidaGasificadoLeidos(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            CargarMeses();
            ObtenerFechasIniciales();
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

            btnBarraPrincipal.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            //btnAnular.Enabled = true;
            //btnEliminarRegistro.Enabled = true;
            //btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
            Consult();

        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvRegistro.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvRegistro.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistro.GroupDescriptors.Clear();
            this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chconsumidor", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chCantidad", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);

        }


        private void Consult()
        {
            incluirTicketsLeidos = 0;
            if (chkIncluirLeidos.Checked == true)
            {
                incluirTicketsLeidos = 1;
            }

            if (chkVisualizacionPorDia.Checked == true)
            {
                desde = DateTime.Now.ToString("dd/MM/yyyy");
                hasta = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
            }



            //desde = this.txtFechaDesde.Text;
            //hasta = this.txtFechaHasta.Text;
            gbList.Enabled = false;
            progressBar1.Visible = true;
            btnBarraPrincipal.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void CargarMeses()
        {

            MesesNeg = new MesController();
            cboMes.DisplayMember = "descripcion";
            cboMes.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboMes.DataSource = MesesNeg.ListarMeses().ToList();
            cboMes.SelectedValue = DateTime.Now.ToString("MM");
        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);


            if (chkVisualizacionPorDia.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }


        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJA"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void PintarResultados(RadGridView dgvListado)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    foreach (var item in dgvListado.Rows)
                    {
                        if (item.Cells["chprioridadId"].Value.ToString() == "1")
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.colorRojoClaro;
                            }
                        }
                        else if (item.Cells["chprioridadId"].Value.ToString() == "2")
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.amarillo3D;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
                            }
                        }

                    }
                }
            }
        }

        private void DespintarResultados(RadGridView dgvListado)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    foreach (var item in dgvListado.Rows)
                    {
                        for (int i = 0; i < dgvListado.Columns.Count; i++)
                        {
                            dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                            dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                            dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
                        }

                    }
                }
            }
        }

        private void Exportar(RadGridView radGridView)
        {
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                RadMessageBox.Show("Ingrese nombre al archivo.");
                return;
            }

            fileName = this.saveFileDialog.FileName.Trim();
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(@fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(@fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaAsincrona();
        }

        private void EjecutarConsultaAsincrona()
        {
            try
            {
                model = new SAS_RegistroGasificadoController();
                result = new List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult>();
                result = model.GetListRegistroGasificadoNoLeidoByDates("NSFAJAS", desde, hasta).ToList();

                if (incluirTicketsLeidos == 0)
                {
                    result = result.Where(x => x.lecturaTicket == 0).ToList();
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = result.ToDataTable<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult>();
                dgvRegistro.Refresh();

                this.txtTotalJabas.Text = string.Empty;
                this.txtJbasLecturadas.Text = string.Empty;
                this.txtJbasLecturadasPorcentaje.Text = string.Empty;

                this.txtJabasPorLeer.Text = string.Empty;
                this.txtJabasPorLeerPorcentaje.Text = string.Empty;

                this.txtJabasExoneradas.Text = string.Empty;
                this.txtJabasExoneradasPorcentaje.Text = string.Empty;



                this.txtTotalDeGRE.Text = string.Empty;
                this.txtTotalDeLotes.Text = string.Empty;
                this.txtTotalViajes.Text = string.Empty;


                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        decimal totalJabas = result.Sum(x => x.cantidadRegistrada).Value;

                        decimal totalJabasLecturadas = result.Where(x => x.lecturaDeTicket.Trim().ToUpper() == "Leido".ToUpper()).Sum(x => x.cantidadRegistrada).Value;
                        decimal PorcentajeJabasLecturadas = totalJabas > 0 ? (Math.Round((totalJabasLecturadas / totalJabas), 4) * 100) : 0;

                        decimal totalJabasPorLeer = result.Where(x => x.lecturaDeTicket.Trim().ToUpper() == "No Leido".ToUpper()).Sum(x => x.cantidadRegistrada).Value;
                        decimal PorcentajeJabasPorLeer = totalJabas > 0 ? (Math.Round((totalJabasPorLeer / totalJabas), 4) * 100) : 0;

                        decimal totalJabasExoneradas = result.Where(x => x.lecturaDeTicket.Trim() == "Exonerado").Sum(x => x.cantidadRegistrada).Value;
                        decimal PorcentajetotalJabasExoneradas = totalJabas > 0 ? Math.Round((totalJabasExoneradas / totalJabas), 4) * 100 : 0;

                        txtTotalJabas.Text = totalJabas.ToDecimalPresentation();
                        txtJbasLecturadas.Text = totalJabasLecturadas.ToDecimalPresentation();
                        txtJbasLecturadasPorcentaje.Text = PorcentajeJabasLecturadas.ToDecimalPresentation();
                        txtJabasPorLeer.Text = totalJabasPorLeer.ToDecimalPresentation();
                        txtJabasPorLeerPorcentaje.Text = PorcentajeJabasPorLeer.ToDecimalPresentation();
                        txtJabasExoneradas.Text = totalJabasExoneradas.ToDecimalPresentation();
                        txtJabasExoneradasPorcentaje.Text = PorcentajetotalJabasExoneradas.ToDecimalPresentation();

                        decimal SumaGuiasRemision = result.GroupBy(x => x.guiaDeRemision).ToList().Count;
                        decimal SumaTotalViajes = result.GroupBy(x => x.NROENVIO).ToList().Count;
                        decimal SumaTotalLotes = result.GroupBy(x => x.idconsumidor).ToList().Count;

                        this.txtTotalDeGRE.Text = SumaGuiasRemision.ToDecimalPresentation();
                        this.txtTotalDeLotes.Text = SumaTotalLotes.ToDecimalPresentation();
                        this.txtTotalViajes.Text = SumaTotalViajes.ToDecimalPresentation();

                    }
                }


                //if (chkResaltarResultados.Checked == true)
                //{
                //    PintarResultados(dgvListado);
                //}

                btnEditar.Enabled = true;
                btnNuevo.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                btnConsultar.Enabled = true;
                btnBarraPrincipal.Enabled = true;
            }
            catch (Exception Ex)
            {
                btnEditar.Enabled = true;
                btnNuevo.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                btnConsultar.Enabled = true;
                btnBarraPrincipal.Enabled = true;

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            if (dgvRegistro != null)
            {
                if (dgvRegistro.Rows.Count > 0)
                {
                    Exportar(dgvRegistro);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void ReporteIngresoSalidaGasificadoLeidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void chkVisualizacionPorDia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVisualizacionPorDia.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                if (cboMes.SelectedIndex >= 0)
                {
                    globalHelper = new GlobalesHelper();
                    globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
                }
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void btnIrARegistroDeGasificado_Click(object sender, EventArgs e)
        {
            Editar();
        }


        private void Editar()
        {
            if (selectedItem != null)
            {
                if (selectedItem.idgasificado > 0)
                {
                    //RegistroDeIngresoSalidaGasificadoEdicion ofrm = new RegistroDeIngresoSalidaGasificadoEdicion(conection, user, companyId, privilege, selectedItem);
                    RegistroDeIngresoSalidaGasificadoEdicion ofrm = new RegistroDeIngresoSalidaGasificadoEdicion(conection, user, companyId, privilege, selectItemById);
                    //ofrm.Show();
                    // ofrm.MdiParent = RegistroDeIngresoSalidaGasificado.ActiveForm;
                    ofrm.WindowState = FormWindowState.Maximized;
                    ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    ofrm.Show();
                }
            }
        }

        private void btnIrARegistroDeGasificadoExonerado_Click(object sender, EventArgs e)
        {
            if (selectItemById != null && selectItemById.idGasificado != null && selectItemById.idIngresoSalidaGasificado != null)
            {                
                ExonerarTicketACamaraDeGasificadoEdicion ofrm = new ExonerarTicketACamaraDeGasificadoEdicion(conection, user, companyId, privilege, ticket, CodigoExoneracion, fechaRegistroTicket);
                ofrm.ShowDialog();
                

            }
        }

        private void btnIrARegistroDeAcopioCampo_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            try
            {

                if (desde != string.Empty && hasta != string.Empty)
                {
                    RegistroDeIngresoSalidaGasificadoVistaPrevia ofrm = new RegistroDeIngresoSalidaGasificadoVistaPrevia(conection, desde, hasta);
                    ofrm.Show();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            btnIrARegistroDeGasificado.Enabled = false;
            btnAgregarTicketAExonerar.Enabled = false;
            btnEliminarRegistroDeGasificado.Enabled = false;
            btnEliminarTicket.Enabled = false;
            btnIrARegistroDeGasificadoExonerado.Enabled = false;
            btnIrARegistroDeAcopioCampo.Enabled = false;
            btnAgregarAUnRegistroDeGasificado.Enabled = false;

            ticket = 0;
            CodigoExoneracion = 0;
            CodigoGasificado = 0;
            lecturaDeTicket = string.Empty;
            fechaRegistroTicket = DateTime.Now;

            try
            {
                #region Selecionar registro()
                selectedItem = new SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult();
                selectItemById = new SAS_RegistroGasificadoAllByIDResult();
                selectedItem.idgasificado = 0;
                selectItemById.idGasificado = 0;
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chidgasificado"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chidgasificado"].Value.ToString() != string.Empty)
                            {
                                CodigoGasificado = (dgvRegistro.CurrentRow.Cells["chidgasificado"].Value != null ?  Convert.ToInt32( dgvRegistro.CurrentRow.Cells["chidgasificado"].Value) : 0);
                                ticket = (dgvRegistro.CurrentRow.Cells["chidDetalle"].Value != null ? Convert.ToInt32( dgvRegistro.CurrentRow.Cells["chidDetalle"].Value) : 0);
                                CodigoExoneracion = (dgvRegistro.CurrentRow.Cells["chcodigoExoneracion"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chcodigoExoneracion"].Value) : 0);
                                lecturaDeTicket = (dgvRegistro.CurrentRow.Cells["chlecturaDeTicket"].Value != null ? (dgvRegistro.CurrentRow.Cells["chlecturaDeTicket"].Value).ToString().Trim().ToUpper() : string.Empty);
                                fechaRegistroTicket = (dgvRegistro.CurrentRow.Cells["chfechaRegistro"].Value != null ? Convert.ToDateTime (dgvRegistro.CurrentRow.Cells["chfechaRegistro"].Value) : DateTime.Now);
                                

                                var resultado = result.Where(x => x.idgasificado == CodigoGasificado).ToList();
                                if (resultado.ToList().Count > 0)
                                {
                                    selectedItem = resultado.ElementAt(0);
                                    selectedItem.codigoExoneracion = CodigoExoneracion;
                                    selectedItem.itemDetalle = ticket;
                                    selectedItem.idgasificado = CodigoGasificado;
                                    selectItemById.idGasificado = CodigoGasificado;
                                    selectItemById.itemDetalle = ticket;                                    

                                    if (lecturaDeTicket == "No Leido".ToUpper())
                                    {
                                        btnAgregarTicketAExonerar.Enabled = true;
                                        btnAgregarAUnRegistroDeGasificado.Enabled = true;                                        
                                    }
                                    else if (lecturaDeTicket == "Exonerado".ToUpper())
                                    {
                                        btnEliminarRegistroDeGasificado.Enabled = true;
                                        btnIrARegistroDeGasificadoExonerado.Enabled = true;
                                    }
                                    else if (lecturaDeTicket == "Leido".ToUpper())
                                    {
                                        btnIrARegistroDeGasificado.Enabled = true;
                                        btnEliminarRegistroDeGasificado.Enabled = true;
                                    }
                                    if (selectedItem.documento.Trim() == "ACA ELIMINADO")
                                    {
                                        btnEliminarTicket.Enabled = true;
                                    }
                                    if (selectedItem.IDINGRESOSALIDAACOPIOCAMPO != null)
                                    {
                                        btnIrARegistroDeAcopioCampo.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString() + "\n Error al cargar los datos en el contenedor del formulario", "Mensaje del sistems");
                return;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void btnEliminarRegistroDeGasificado_Click(object sender, EventArgs e)
        {
            LiberarTicketDeGasificado();
        }

        private void LiberarTicketDeGasificado()
        {
            if (selectItemById != null && selectItemById.idGasificado != null && selectItemById.idIngresoSalidaGasificado != null)
            {
                model = new SAS_RegistroGasificadoController();
                IngresoSalidaGasificado ItemALiberar = new IngresoSalidaGasificado();
                ItemALiberar.itemDetalle = selectItemById.itemDetalle;
                int ResultadoAccion = model.LiberarTicketGasificado(conection, ItemALiberar);
                MessageBox.Show("Se libero correctamente este ticket", "Confirmación de Operación");
                Consult();

            }
        }

        private void btnEliminarExoneración_Click(object sender, EventArgs e)
        {
            LiberarTicketExoneracion();
        }

        private void LiberarTicketExoneracion()
        {
            if (selectItemById != null && selectItemById.idGasificado != null && selectItemById.idIngresoSalidaGasificado != null)
            {
                model = new SAS_RegistroGasificadoController();
                SAS_RegistroTicketCamaraGasificadoExonerados ItemALiberar = new SAS_RegistroTicketCamaraGasificadoExonerados();
                ItemALiberar.itemDetalle = selectItemById.itemDetalle;
                int ResultadoAccion = model.LiberarTicketExonerado(conection, ItemALiberar);
                MessageBox.Show("Se libero correctamente este ticket", "Confirmación de Operación");
                Consult();

            }
        }

        private void btnAgregarTicketAExonerar_Click(object sender, EventArgs e)
        {
            if (selectItemById != null && selectItemById.idGasificado != null && selectItemById.idIngresoSalidaGasificado != null)
            {
                SAS_ListadoDeRegistrosExoneradosByDatesResult itemExonerado = new SAS_ListadoDeRegistrosExoneradosByDatesResult();
                itemExonerado.itemDetalle = selectItemById.itemDetalle;
                ExonerarTicketACamaraDeGasificadoEdicion ofrm = new ExonerarTicketACamaraDeGasificadoEdicion(conection,user, companyId, privilege, ticket, CodigoExoneracion, fechaRegistroTicket);
                ofrm.ShowDialog();
                Consult();

            }
        }

        private void subMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla1)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla1);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void ReporteIngresoSalidaGasificadoLeidos_Load(object sender, EventArgs e)
        {

        }
    }
}