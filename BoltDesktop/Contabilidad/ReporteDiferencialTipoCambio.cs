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

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ReporteDiferencialTipoCambio : Form
    {
        private MesController MesesController;
        private string desde;
        private string hasta;
        private List<SAS_ListarCuentasDiferenciaTCResumen01Result> listadoResumen01;
        private ReporteDiferenciaTCResumenController controller;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_ListarCuentasDiferenciaTCResumen02Result> listadoResumen02;

        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public ReporteDiferencialTipoCambio()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
        }


        public ReporteDiferencialTipoCambio(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
        }



        private void CargarMeses()
        {
            try
            {
                MesesController = new MesController();
                cboMesDesde.DisplayMember = "descripcion";
                cboMesDesde.ValueMember = "valor";
                cboMesDesde.DataSource = MesesController.ListarMeses().ToList().Where(x => x.Valor != "00" && x.Valor != "13").ToList();
                cboMesDesde.SelectedValue = DateTime.Now.ToString("MM");

                MesesController = new MesController();
                cboMesHasta.DisplayMember = "descripcion";
                cboMesHasta.ValueMember = "valor";
                cboMesHasta.DataSource = MesesController.ListarMeses().ToList().Where(x => x.Valor != "00" && x.Valor != "13").ToList(); ;
                cboMesHasta.SelectedValue = DateTime.Now.ToString("MM");

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistemas");
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvResumen01.TableElement.BeginUpdate();
            this.dgvResumen02.TableElement.BeginUpdate();

            this.LoadFreightSummary();
            this.dgvResumen01.TableElement.EndUpdate();
            this.dgvResumen02.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvResumen01.MasterTemplate.AutoExpandGroups = true;
            this.dgvResumen01.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvResumen01.GroupDescriptors.Clear();
            this.dgvResumen01.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chcuenta", "Count : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chsoles", "Count : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chdolares", "Count : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvResumen01.MasterTemplate.SummaryRowsTop.Add(items1);


            this.dgvResumen02.MasterTemplate.AutoExpandGroups = true;
            this.dgvResumen02.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvResumen02.GroupDescriptors.Clear();
            this.dgvResumen02.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chcuenta", "Count : {0:N2}; ", GridAggregateFunction.Count));
            items2.Add(new GridViewSummaryItem("chsoles", "Count : {0:N2}; ", GridAggregateFunction.Sum));
            items2.Add(new GridViewSummaryItem("chdolares", "Count : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvResumen02.MasterTemplate.SummaryRowsTop.Add(items2);

        }


        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
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

        private void ReporteDiferencialTipoCambio_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            // Ejecutar lógica

            listadoResumen01 = new List<SAS_ListarCuentasDiferenciaTCResumen01Result>();
            controller = new ReporteDiferenciaTCResumenController();
            listadoResumen01 = controller.ObtenerListadoResumenItem0001_008("SAS", "001", desde, hasta);


            listadoResumen02 = new List<SAS_ListarCuentasDiferenciaTCResumen02Result>();
            controller = new ReporteDiferenciaTCResumenController();
            listadoResumen02 = controller.ObtenerListadoResumenItem0009("SAS", "001", desde, hasta);



        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Mostrar resultado de forma asincrona
            dgvResumen01.DataSource = listadoResumen01.ToDataTable<SAS_ListarCuentasDiferenciaTCResumen01Result>();
            dgvResumen01.Refresh();

            dgvResumen02.DataSource = listadoResumen02.ToDataTable<SAS_ListarCuentasDiferenciaTCResumen02Result>();
            dgvResumen01.Refresh();


            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                desde = txtPeriodoDesde.Value.ToString() + cboMesDesde.SelectedValue.ToString();
                hasta = txtPeriodoHasta.Value.ToString() + cboMesHasta.SelectedValue.ToString();          
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

            if (tabControl.SelectedPage == tabResumenItem009)
            {
                if (dgvResumen02 != null && dgvResumen02.Rows.Count > 0)
                {
                    Exportar(dgvResumen02);
                }
            }
             else if (tabControl.SelectedPage == tabResumenItems)
            {
                if (dgvResumen01 != null && dgvResumen01.Rows.Count > 0)
                {
                    Exportar(dgvResumen01);
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

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void ReporteDiferencialTipoCambio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

        }

        private void gbConsulta_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No tiene acceso a esta opcion", "CONFIRMACION DEL SISTEMA");
        }
    }
}
