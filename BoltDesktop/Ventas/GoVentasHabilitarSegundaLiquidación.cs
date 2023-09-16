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
    public partial class GoVentasHabilitarSegundaLiquidación : Form
    {
        private MesController MesesNeg;
        private string fileName;
        private bool exportVisualSettings;
        private GlobalesHelper globalHelper;
        private string desde;
        private string hasta;
        private string idDocumento = string.Empty;
        private LiquidacionVentasExteriorController modelo;
        private List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult> listadoinvoice;
        private string resultado;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public GoVentasHabilitarSegundaLiquidación()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
        }

        public GoVentasHabilitarSegundaLiquidación(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
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
            ObtenerFechasIniciales();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
        }

        private void GoVentasHabilitarSegundaLiquidación_Load(object sender, EventArgs e)
        {

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

        protected override void OnLoad(EventArgs e)
        {
            this.dgvLiquidacionExterior.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvLiquidacionExterior.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvLiquidacionExterior.MasterTemplate.AutoExpandGroups = true;
            this.dgvLiquidacionExterior.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvLiquidacionExterior.GroupDescriptors.Clear();
            this.dgvLiquidacionExterior.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chrazonsocial", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvLiquidacionExterior.MasterTemplate.SummaryRowsTop.Add(items1);
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
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new LiquidacionVentasExteriorController();
                listadoinvoice = new List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult>();
                listadoinvoice = modelo.ObtenerListadoinviocePorLiquidacionVentasExterior("SAS", desde, hasta).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvLiquidacionExterior.DataSource = listadoinvoice.ToDataTable<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult>();
                dgvLiquidacionExterior.Refresh();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                desde = this.txtFechaDesde.Text.ToString();
                hasta = this.txtFechaHasta.Text.ToString();
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

        private void dgvLiquidacionExterior_SelectionChanged(object sender, EventArgs e)
        {
            idDocumento = string.Empty;

            if (dgvLiquidacionExterior.Rows.Count > 0)
            {
                if (dgvLiquidacionExterior.CurrentRow != null)
                {
                    if (dgvLiquidacionExterior.CurrentRow.Cells["chidLIQVENTASEXTERIOR"].Value != null)
                    {
                        idDocumento = dgvLiquidacionExterior.CurrentRow.Cells["chidLIQVENTASEXTERIOR"].Value != null ? Convert.ToString(dgvLiquidacionExterior.CurrentRow.Cells["chidLIQVENTASEXTERIOR"].Value.ToString().Trim()) : string.Empty;
                    }
                }
            }
        }

        private void btnCambiarUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                if (idDocumento != null && idDocumento != string.Empty)
                {
                    desde = this.txtFechaDesde.Text.ToString();
                    hasta = this.txtFechaHasta.Text.ToString();
                    gbConsulta.Enabled = false;
                    gbDetalle.Enabled = false;
                    menuPrincipal.Enabled = false;
                    ProgressBar.Visible = true;
                    bgwCambiarUltimo.RunWorkerAsync();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bgwCambiarUltimo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new LiquidacionVentasExteriorController();
                resultado = string.Empty;
                resultado = modelo.ActualizarEsatadoDeUltimaLiquidacion("SAS", idDocumento);
                listadoinvoice = new List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult>();
                listadoinvoice = modelo.ObtenerListadoinviocePorLiquidacionVentasExterior("SAS", desde, hasta).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bgwCambiarUltimo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show(resultado, "MENSAJE DEL SISTEMA");
                dgvLiquidacionExterior.DataSource = listadoinvoice.ToDataTable<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult>();
                dgvLiquidacionExterior.Refresh();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }
    }
}
