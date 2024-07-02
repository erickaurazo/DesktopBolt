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
using MyDataGridViewColumns;
using System.Drawing;
using Asistencia.Negocios.SIG.SST.Registro_de_Capacitaciones;
using ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones;

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class RegistroDeCapacitaciones : Form
    {

        #region Variables() 
        string nombreformulario = "RegistroDeCapacitaciones";
        private int periodo;
        private PrivilegesByUser PrivilegiosDeUsuarioEnSesion;
        private SAS_USUARIOS UsuarioEnSesion;
        private string CompaniaID = "001";
        private string Desde = string.Empty;
        private string Hasta = string.Empty;
        private string ConexionABaseDeDatos;
        private ListadoRegistroCapacitacionesPorPeriodoResult ItemSelecionado;

        public MesController MesesNeg;
        private List<ListadoRegistroCapacitacionesPorPeriodoResult> Listado;
        RegistroDeCapacitacionesController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private ListadoRegistroCapacitacionesPorPeriodoResult selectItemById;
        string ID = string.Empty;
        private string TemaID;
        private string EstadoId;

        #endregion



        public RegistroDeCapacitaciones()
        {
            InitializeComponent();
            ItemSelecionado = new ListadoRegistroCapacitacionesPorPeriodoResult();

            ItemSelecionado = GenerarObjetoenBlanco(ItemSelecionado);


            CargarMeses();
            ObtenerFechasIniciales();
            ConexionABaseDeDatos = "SSOMA";
            UsuarioEnSesion = new SAS_USUARIOS();
            UsuarioEnSesion.IdUsuario = "EAURAZO";
            UsuarioEnSesion.NombreCompleto = "Erick Aurazo Carhuatanta";
            UsuarioEnSesion.IdCodigoGeneral = "100369";
            CompaniaID = "001";
            PrivilegiosDeUsuarioEnSesion = new PrivilegesByUser();
            PrivilegiosDeUsuarioEnSesion.nuevo = 1;
            Inicio();
            lblCodeUser.Text = UsuarioEnSesion.IdUsuario;
            lblFullName.Text = UsuarioEnSesion.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;

            btnEditar.Enabled = true;
            btnGrabar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarPrograma.Enabled = true;
            btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnCerrar.Enabled = true;
            gbCabecera.Enabled = false;
            RealizarConsulta();
        }

        private ListadoRegistroCapacitacionesPorPeriodoResult GenerarObjetoenBlanco(ListadoRegistroCapacitacionesPorPeriodoResult itemSelecionado)
        {
            DateTime FechaActual = DateTime.Now;
            ListadoRegistroCapacitacionesPorPeriodoResult item = new ListadoRegistroCapacitacionesPorPeriodoResult();
            item.CapacitacionID = string.Empty;
            item.Area = string.Empty;
            item.AreaId = string.Empty;
            item.Asistentes = 0;
            item.Capacitacion = string.Empty;
            
            item.CapacitacionTipoID = string.Empty;
            item.Capacitadores = 0;
            item.Duracion = 0;
            item.Estado = "PENDIENTE";
            item.EstadoID = "PE";
            item.FechaCapacitacion = FechaActual;
            item.FechaRegistro = FechaActual;
            item.Folio = "0".PadLeft(7, '0');
            item.HoraFin = FechaActual;
            item.HoraInicio = FechaActual;
            item.LatLong = string.Empty;
            item.Observacion = string.Empty;
            item.PDFPrint = 0;
            item.PDFRuta = string.Empty;
            item.TemaEstado = 0;
            item.Tema = string.Empty;
            item.TemaID = string.Empty;
            item.Ubicación = string.Empty;
            return item;
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


        public RegistroDeCapacitaciones(string _ConexionABaseDeDatos, SAS_USUARIOS _UsuarioEnSesion, string _CompaniaID, PrivilegesByUser _PrivilegiosDeUsuarioEnSesion)
        {
            InitializeComponent();
            ItemSelecionado = new ListadoRegistroCapacitacionesPorPeriodoResult();
            ItemSelecionado.CapacitacionID = string.Empty;
            CargarMeses();
            ObtenerFechasIniciales();
            ConexionABaseDeDatos = _ConexionABaseDeDatos;
            UsuarioEnSesion = _UsuarioEnSesion;
            CompaniaID = _CompaniaID;
            PrivilegiosDeUsuarioEnSesion = _PrivilegiosDeUsuarioEnSesion;
            Inicio();
            lblCodeUser.Text = UsuarioEnSesion.IdUsuario;
            lblFullName.Text = UsuarioEnSesion.NombreCompleto;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminar.Enabled = true;
            btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnCerrar.Enabled = true;
            gbCabecera.Enabled = false;
            RealizarConsulta();
        }


        protected override void OnLoad(EventArgs e)
        {
            this.dgvListado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chCapacitacionID", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);

        }

        private void RealizarConsulta()
        {
            if (chkVisualizacionPorDia.Checked == true)
            {
                Desde = DateTime.Now.ToPresentationDate();
                Hasta = DateTime.Now.ToPresentationDate();
            }
            else
            {
                Desde = this.txtFechaDesde.Text;
                Hasta = this.txtFechaHasta.Text;
            }

            btnConsultar.Enabled = false;
            gbListado.Enabled = false;
            gbCabecera.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;

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


            //this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
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



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ID = string.Empty;
            RegistroDeCapacitacionesEdicion ofrm = new RegistroDeCapacitacionesEdicion(ConexionABaseDeDatos, UsuarioEnSesion, CompaniaID, PrivilegiosDeUsuarioEnSesion, ID);
            ofrm.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();

            
        }

        private void Editar()
        {
            if (EstadoId == "PE")
            {
                RegistroDeCapacitacionesEdicion ofrm = new RegistroDeCapacitacionesEdicion(ConexionABaseDeDatos, UsuarioEnSesion, CompaniaID, PrivilegiosDeUsuarioEnSesion, ID);
                ofrm.Show();
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para Edición", "Edición del documento");
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (UsuarioEnSesion != null)
            {
                if (UsuarioEnSesion.IdUsuario == "EAURAZO" || UsuarioEnSesion.IdUsuario == "FCERNA" || UsuarioEnSesion.IdUsuario == "YCORDOVA" || UsuarioEnSesion.IdUsuario == "KGRANDA")
                {
                    if (EstadoId == "AN" || EstadoId == "PE")
                    {
                        model = new RegistroDeCapacitacionesController();
                        model.CambiarDeEStado(ConexionABaseDeDatos, ID);
                        MessageBox.Show("Acción realizada correctamente", "Confirmación de Cambio de estado");
                        RealizarConsulta();
                    }
                    else
                    {
                        MessageBox.Show("El documento no tiene el estado para cambiar estado", "Negación de Cambio de estado");
                    }

                }
            }
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            if (UsuarioEnSesion != null)
            {
                if (UsuarioEnSesion.IdUsuario == "EAURAZO" || UsuarioEnSesion.IdUsuario == "FCERNA")
                {
                    model = new RegistroDeCapacitacionesController();
                    model.Eliminar(ConexionABaseDeDatos, ID);
                    MessageBox.Show("Acción realizada correctamente", "Confirmación de eliminación");
                    RealizarConsulta();

                }
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }
            }
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {

        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {
            AprobacionPorGerencia();
        }




        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
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

        private void RegistroDeCapacitaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }



        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarConsultar();
        }


        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            RealizarConsulta();
        }

        private void btnAnularPrograma_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarPrograma_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarPrograma_Click(object sender, EventArgs e)
        {

        }

        private void btnVerPrograma_Click(object sender, EventArgs e)
        {

        }

        private void vistaPreviaFormato001ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void imprimirFormato001ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void duplicarRegistroDeCapacitaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void asociarRegistroDeCapacitaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void desvincularRegistroDeCapacitaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aprobaciónDeJefaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void arobaciónDeGerenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AprobacionPorGerencia();
        }

        private void AprobacionPorGerencia()
        {
            if (UsuarioEnSesion != null)
            {
                if (UsuarioEnSesion.IdUsuario == "EAURAZO" || UsuarioEnSesion.IdUsuario == "FCERNA" || UsuarioEnSesion.IdUsuario == "YCORDOVA")
                {
                    if (EstadoId == "AN" || EstadoId == "PE")
                    {
                        model = new RegistroDeCapacitacionesController();
                        model.Aprobar(ConexionABaseDeDatos, ID);
                        MessageBox.Show("Acción realizada correctamente", "Confirmación de Cambio de estado");
                        RealizarConsulta();
                    }
                    else
                    {
                        MessageBox.Show("El documento no tiene el estado para cambiar estado", "Negación de Cambio de estado");
                    }

                }
            }
        }

        private void RegistroDeCapacitaciones_Load(object sender, EventArgs e)
        {

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


        #region Metodos()

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


        private void PresentarConsultar()
        {
            try
            {
                dgvListado.DataSource = Listado.ToDataTable<ListadoRegistroCapacitacionesPorPeriodoResult>();
                dgvListado.Refresh();
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;
                return;
            }
        }

        private void EjecutarConsulta()
        {



            try
            {
                model = new RegistroDeCapacitacionesController();
                Listado = new List<ListadoRegistroCapacitacionesPorPeriodoResult>();
                Listado = model.ObtenerListaDeCapacitacionesDesdePeriodos(ConexionABaseDeDatos, Desde, Hasta);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        #endregion

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            EstadoId = string.Empty;
            ID = string.Empty;
            ItemSelecionado = new ListadoRegistroCapacitacionesPorPeriodoResult();
            ItemSelecionado = GenerarObjetoenBlanco(ItemSelecionado);
            TemaID = string.Empty;

            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chID"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chID"].Value.ToString() != string.Empty)
                        {
                            ID = dgvListado.CurrentRow.Cells["chID"].Value != null ? dgvListado.CurrentRow.Cells["chID"].Value.ToString().Trim() : string.Empty;
                            TemaID = dgvListado.CurrentRow.Cells["chTemaID"].Value != null ? dgvListado.CurrentRow.Cells["chTemaID"].Value.ToString().Trim() : string.Empty;
                            EstadoId = dgvListado.CurrentRow.Cells["chEstadoID"].Value != null ? dgvListado.CurrentRow.Cells["chEstadoID"].Value.ToString().Trim() : string.Empty;
                            var result01 = Listado.Where(x => x.CapacitacionID.Trim() == ID).ToList();
                            if (result01 != null && result01.ToList().Count > 0)
                            {
                                ItemSelecionado = Listado.Where(x => x.CapacitacionID.Trim() == ID).ToList().ElementAt(0);
                            }


                        }
                    }
                }
            }

        }

        private void agrupadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                if (ID.Trim() != string.Empty)
                {
                    RegistroDeCapacitacionesVistaPrevia ofrm = new RegistroDeCapacitacionesVistaPrevia(ConexionABaseDeDatos, ID);
                    ofrm.Show();
                }
            }

        }

        private void individualToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ID != null || TemaID != null)
            {
                if (ID.Trim() != string.Empty)
                {
                    if (TemaID.Trim() != string.Empty)
                    {
                        RegistroDeCapacitacionesVistaPreviaIndividual ofrm = new RegistroDeCapacitacionesVistaPreviaIndividual(ConexionABaseDeDatos, ID, TemaID);
                        ofrm.Show();
                    }
                    
                }
            }
        }

        private void btnConvertirImagenes_Click(object sender, EventArgs e)
        {
            ConvertirImagenesPGNtoJPG ofrm = new ConvertirImagenesPGNtoJPG();
            ofrm.Show();
        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {

            if (ItemSelecionado != null)
            {
                Editar();
            }

           
        }

        private void btnSLiberarAprobacion_Click(object sender, EventArgs e)
        {
            LiberarAprobacion();
        }


        private void LiberarAprobacion()
        {
            if (UsuarioEnSesion != null)
            {
                if (UsuarioEnSesion.IdUsuario == "EAURAZO" || UsuarioEnSesion.IdUsuario == "FCERNA" || UsuarioEnSesion.IdUsuario == "YCORDOVA")
                {
                    if (EstadoId == "AP")
                    {
                        model = new RegistroDeCapacitacionesController();
                        model.LiberarAprobacion(ConexionABaseDeDatos, ID);
                        MessageBox.Show("Acción realizada correctamente", "Confirmación de Cambio de estado");
                        RealizarConsulta();
                    }
                    else
                    {
                        MessageBox.Show("El documento no tiene el estado para cambiar estado", "Negación de Cambio de estado");
                    }

                }
            }
        }

    }
}
