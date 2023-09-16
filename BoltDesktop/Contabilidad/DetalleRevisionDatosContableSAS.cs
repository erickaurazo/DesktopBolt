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
    public partial class DetalleRevisionDatosContableSAS : Form
    {
        private string idCuenta = string.Empty;
        private string periodoHasta = string.Empty;
        private string periodoDesde = string.Empty;
        private RevisionDatosContablesControllers modelo;
        private List<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult> listadoByModulo;
        private List<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult> listadoByBalance;
        private string fileName;
        private bool exportVisualSettings;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public MesController MesesNeg { get; private set; }

        public DetalleRevisionDatosContableSAS()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarPeriodos();

            //gbConsulta.Enabled = !true;
            //gbDetalle.Enabled = !true;
            //ProgressBar.Visible = true;
            //menuPrincipal.Enabled = false;
        }

        public DetalleRevisionDatosContableSAS(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
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
            CargarPeriodos();

            //gbConsulta.Enabled = !true;
            //gbDetalle.Enabled = !true;
            //ProgressBar.Visible = true;
            //menuPrincipal.Enabled = false;
        }



        protected override void OnLoad(EventArgs e)
        {

            this.dgvModulo.TableElement.BeginUpdate();
            this.dgvContabilidad.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvModulo.TableElement.EndUpdate();
            this.dgvContabilidad.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvModulo.MasterTemplate.AutoExpandGroups = true;
            this.dgvModulo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvModulo.GroupDescriptors.Clear();
            this.dgvModulo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chcuenta", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chIMPORTEMOF", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chIMPORTEMEX", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvModulo.MasterTemplate.SummaryRowsTop.Add(items1);

            this.dgvContabilidad.MasterTemplate.AutoExpandGroups = true;
            this.dgvContabilidad.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvContabilidad.GroupDescriptors.Clear();
            this.dgvContabilidad.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chDESCRIPCION", "COUNT : {0:N0}; ", GridAggregateFunction.Count));
            items2.Add(new GridViewSummaryItem("chSALDO", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items2.Add(new GridViewSummaryItem("chSALDO_MEX", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvContabilidad.MasterTemplate.SummaryRowsTop.Add(items2);                            

        }


        private void CargarPeriodos()
        {
            try
            {
                MesesNeg = new MesController();
                cboPeriodoHasta.DisplayMember = "descripcion";
                cboPeriodoHasta.ValueMember = "valor";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboPeriodoHasta.DataSource = MesesNeg.ObtenerListadoPeriodos().ToList();
                cboPeriodoHasta.SelectedValue = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["bdSaturno"].ToString();
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

        private void dgvComparativoVSvsNisira_Click(object sender, EventArgs e)
        {

        }

        private void DetalleRevisionDatosContableSAS_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                idCuenta = this.txtCuentaCodigo.Text.Trim();
                periodoHasta = this.cboPeriodoHasta.SelectedValue.ToString().Trim();
                periodoDesde = this.cboPeriodoHasta.SelectedValue.ToString().Trim();

                gbConsulta.Enabled = !true;
                gbDetalle.Enabled = !true;
                ProgressBar.Visible = true;
                menuPrincipal.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                //gbConsulta.Enabled = true;
                //gbDetalle.Enabled = true;
                //ProgressBar.Visible = !true;
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                modelo = new RevisionDatosContablesControllers();
                listadoByModulo = new List<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult>();
                listadoByBalance = new List<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult>();
                listadoByModulo = modelo.ObtenerListadoDetalldoMovimientoCuentaPorModuloEntrePeriodo("SAS", idCuenta, periodoHasta);
                listadoByBalance = modelo.ObtenerListadoDetalldoMovimientoCuentaPorBalanceEntrePeriodo("SAS", idCuenta, periodoHasta);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                //gbConsulta.Enabled = true;
                //gbDetalle.Enabled = true;
                //ProgressBar.Visible = !true;
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvContabilidad.DataSource = listadoByBalance.ToDataTable<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult>();
                dgvContabilidad.Refresh();

                dgvModulo.DataSource = listadoByModulo.ToDataTable<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult>();
                dgvModulo.Refresh();
                menuPrincipal.Enabled = true;
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;                
                ProgressBar.Visible = !true;
                return;
            }

        }

        private void DetalleRevisionDatosContableSAS_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.bgwSincronizar.IsBusy == true)
            {

                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (this.bgwSincronizar.IsBusy == true)
                {


                    MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                    "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Close();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedPage == tabContabilidad)
                {
                    Exportar(dgvContabilidad);
                }
                else if (tabControl.SelectedPage == tabModulo)
                {
                    //                    Exportar(dgvBDParteMaquinaria);
                    Exportar(dgvModulo);
                }     
                


            }
            catch (Exception ex)
            {
                string message = String.Format("Error en el archivo.\nError message: {0}", ex.Message);
                RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No tiene acceso a esta opcion", "CONFIRMACION DEL SISTEMA");
        }
    }
}
