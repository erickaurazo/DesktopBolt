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

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class RegistroDeCapacitaciones : Form
    {

        #region Variables() 

        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private ListadoRegistroCapacitacionesPorPeriodoResult ItemSelecionado;
        public MesController MesesNeg;
        private List<ListadoRegistroCapacitacionesPorPeriodoResult> Listado;
        RegistroDeCapacitacionesController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private ListadoRegistroCapacitacionesPorPeriodoResult selectItemById;

        #endregion



        public RegistroDeCapacitaciones()
        {
            InitializeComponent();
            ItemSelecionado = new ListadoRegistroCapacitacionesPorPeriodoResult();
            ItemSelecionado.CapacitacionID = string.Empty;

            CargarMeses();
            ObtenerFechasIniciales();
            conection = "SSOMA";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "Erick Aurazo Carhuatanta";
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


        public RegistroDeCapacitaciones(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            ItemSelecionado = new ListadoRegistroCapacitacionesPorPeriodoResult();
            ItemSelecionado.CapacitacionID = string.Empty;

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
                desde = DateTime.Now.ToPresentationDate();
                hasta = DateTime.Now.ToPresentationDate();
            }
            else
            {
                desde = this.txtFechaDesde.Text;
                hasta = this.txtFechaHasta.Text;
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

        private void commandBarButton1_Click(object sender, EventArgs e)
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

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void RegistroDeCapacitaciones_FormClosing(object sender, FormClosingEventArgs e)
        {

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
                Listado = model.GetListByDate(conection, desde, hasta);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        #endregion

    }
}
