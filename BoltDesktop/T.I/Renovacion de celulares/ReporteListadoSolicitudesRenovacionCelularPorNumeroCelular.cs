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


namespace ComparativoHorasVisualSATNISIRA.T.I.Renovacion_de_celulares
{
    public partial class ReporteListadoSolicitudesRenovacionCelularPorNumeroCelular : Form
    {

        string nombreformulario = "LINEAS CELULARES";
        private PrivilegesByUser privilege;
        private string companyId, conection, fileName, PersonalID, NumeroCelular = string.Empty;
        private SAS_USUARIOS user2;
        private bool exportVisualSettings;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByNumeroCelularResult> listado;
        private SAS_LineasCelularesCoporativa iLineaCelular;
        private SAS_SolicitudDeRenovacionTelefoniaCelularListadoByNumeroCelularResult itemSelecionado;
        private SAS_SolicitudDeRenovacionTelefoniaCelularController Modelo;
        public List<int> valores_permitidos = new List<int>() { 8, 13, 37, 38, 39, 40, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 46 };
        private string lineaCelular = string.Empty;
        private string mensajeDeValidacion;
        private string result;
        private int ClickFiltro, MotivoSolicitudID = 0;
        private int ClickResaltarResultados;
        public int IdLineaCelular = 0;

 
        public ReporteListadoSolicitudesRenovacionCelularPorNumeroCelular()
        {
            InitializeComponent();

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = Environment.UserName.Trim();
            user2.NombreCompleto = Environment.UserName.Trim();
            conection = "SAS";
            companyId = "001";
            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();

            Actualizar();
        }

        public ReporteListadoSolicitudesRenovacionCelularPorNumeroCelular(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;

            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();

            Actualizar();

        }

        public ReporteListadoSolicitudesRenovacionCelularPorNumeroCelular(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, string _NumeroCelular)
        {
            InitializeComponent();

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;            
            NumeroCelular = _NumeroCelular;            
            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();
            Actualizar();

        }


        private void ReporteListadoSolicitudesRenovacionCelularPorNumeroCelular_Load(object sender, EventArgs e)
        {

        }


        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;
                btnMenu.Enabled = false;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                listado = new List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByNumeroCelularResult>();
                listado = Modelo.ListRequestsRenovacionByNumeroCelular(conection, NumeroCelular).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.ToDataTable<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByNumeroCelularResult>();
                dgvListado.Refresh();
                btnMenu.Enabled = !false;
                progressBar1.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        public void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["SAS"].ToString();
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
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();
                //this.SetConditions();
                base.OnLoad(e);
            }
            catch (TargetInvocationException ex)
            {
                result = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }

                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chnumeroCelular", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
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

            fileName = this.saveFileDialog.FileName;
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla);
            excelExporter.SheetName = "Listado registros";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }



        private void Consultar()
        {
            try
            {
                btnMenu.Enabled = !true;
                gbList.Enabled = !true;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void ActivateFilter()
        {
            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | DesActivar Filtro()                
                dgvListado.EnableFiltering = !true;
                dgvListado.ShowHeaderCellButtons = false;

                #endregion
            }
            else
            {

                #region Par() | Activar Filtro()
                dgvListado.EnableFiltering = true;
                dgvListado.ShowHeaderCellButtons = true;
                #endregion
            }
        }



    }
}
