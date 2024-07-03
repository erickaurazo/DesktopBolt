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
    public partial class RegistroDeCapacitacionPorColaborador : Form
    {

        #region Variables() 
        string nombreformulario = "RegistroDeCapacitaciones";
        private int periodo;
        private PrivilegesByUser PrivilegiosDeUsuarioEnSesion;
        private SAS_USUARIOS UsuarioEnSesion;
        private string CompaniaID = "001";
        private string Desde = string.Empty;
        private string Hasta = string.Empty;
        private string PersonalID = string.Empty;
        
        private string ConexionABaseDeDatos;
        private RegistroDeCapacitacionesPorTrabajadorIDResult ItemSelecionado;

        public MesController MesesNeg;
        private List<RegistroDeCapacitacionesPorTrabajadorIDResult> Listado;
        RegistroDeCapacitacionesController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private RegistroDeCapacitacionesPorTrabajadorIDResult selectItemById;
        string ID = string.Empty;
        private string TemaID;
        private string EstadoId;

        #endregion


        public RegistroDeCapacitacionPorColaborador()
        {
            InitializeComponent();
            ItemSelecionado = new RegistroDeCapacitacionesPorTrabajadorIDResult();

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
            btnHistorial.Enabled = true;
            btnEliminar.Enabled = true;
            btnAdjuntar.Enabled = true;
            btnCerrar.Enabled = true;
            gbCabecera.Enabled = true;
            gbListado.Enabled = false;
        }

        private RegistroDeCapacitacionesPorTrabajadorIDResult GenerarObjetoenBlanco(RegistroDeCapacitacionesPorTrabajadorIDResult itemSelecionado)
        {
            DateTime FechaActual = DateTime.Now;
            RegistroDeCapacitacionesPorTrabajadorIDResult item = new RegistroDeCapacitacionesPorTrabajadorIDResult();
            item.CapacitacionID = string.Empty;
            item.Area = string.Empty;
            item.Area = string.Empty;
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
            item.TemaID = string.Empty;
            item.Temas = string.Empty;
            item.TemaID = string.Empty;
            item.Ubicación = string.Empty;
            item.PersonalID = string.Empty;
            return item;
        }

        public RegistroDeCapacitacionPorColaborador(string _ConexionABaseDeDatos, SAS_USUARIOS _UsuarioEnSesion, string _CompaniaID, PrivilegesByUser _PrivilegiosDeUsuarioEnSesion)
        {
            InitializeComponent();
            ItemSelecionado = new RegistroDeCapacitacionesPorTrabajadorIDResult();
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
            gbCabecera.Enabled = true;
            gbListado.Enabled = false;

        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (this.txtPersonal.Text.Trim() != string.Empty)
            {
                if (this.txtPersonalID.Text.Trim() != string.Empty)
                {
                    

                    RealizarConsulta();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

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

        }

        private void btnConvertirImagenes_Click(object sender, EventArgs e)
        {

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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarConsultar();
        }

        private void RegistroDeCapacitacionPorColaborador_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void RegistroDeCapacitacionPorColaborador_Load(object sender, EventArgs e)
        {

        }


        #region Métodos()

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
            items1.Add(new GridViewSummaryItem("chCapacitacion", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);

        }

        private void RealizarConsulta()
        {

            Desde = this.txtFechaDesde.Text;
            Hasta = this.txtFechaHasta.Text;
            PersonalID = this.txtPersonalID.Text.Trim();

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
            cboMes.DataSource = MesesNeg.ListarMeses().ToList();
            cboMes.SelectedValue = DateTime.Now.ToString("MM");
        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");

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


        public void Inicio()
        {
            #region Inicio()             
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["SSOMA"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString().Trim(), "MENSAJE DEL SISTEMA");
                return;
            }            
            #endregion
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


        private void PresentarConsultar()
        {
            try
            {
                dgvListado.DataSource = Listado.ToDataTable<RegistroDeCapacitacionesPorTrabajadorIDResult>();
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
                Listado = new List<RegistroDeCapacitacionesPorTrabajadorIDResult>();
                Listado = model.ObtenerListadoDeCapacitacionesPorPersonalID(ConexionABaseDeDatos, Desde, Hasta, PersonalID);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }




        #endregion

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

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            EstadoId = string.Empty;
            ID = string.Empty;
            ItemSelecionado = new RegistroDeCapacitacionesPorTrabajadorIDResult();
            ItemSelecionado = GenerarObjetoenBlanco(ItemSelecionado);
            TemaID = string.Empty;


            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chCapacitacionID"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chCapacitacionID"].Value.ToString() != string.Empty)
                        {
                            ID = dgvListado.CurrentRow.Cells["chCapacitacionID"].Value != null ? dgvListado.CurrentRow.Cells["chCapacitacionID"].Value.ToString().Trim() : string.Empty;
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

        private void btnSVistaPreviaAgrupado_Click(object sender, EventArgs e)
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

        private void btnSVistaPreviaIndividual_Click(object sender, EventArgs e)
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
    }
}
