using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asistencia.Datos;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using Asistencia.Negocios;
using MyControlsDataBinding.Extensions;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using Asistencia.Helper;
using System.Configuration;
using Telerik.WinControls.UI.Localization;
using Asistencia.Negocios.ITD.Cuentas;
using Telerik.WinControls.Data;

namespace ComparativoHorasVisualSATNISIRA.T.I.Cuentas_NISIRA
{
    public partial class CuentasERP : Form
    {

        string nombreformulario = "CuentaERPNISIRA";
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private string fileName;
        private bool exportVisualSettings;
        private List<USUARIO> listado;
        private NISIRAERPCuentasController Modelo;
        private USUARIO odetalle;
        private USUARIO odetalleSelecionado;
        private List<USUARIO> listDetails;
        private int lastItem;
        private string msgError;
        private List<USUARIO> detalleEliminados = new List<USUARIO>();
        private List<USUARIO> detalle = new List<USUARIO>();
        object result;
        int oParImpar = 0;
        private int ClickFiltro = 0;

        public CuentasERP()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
           
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = Environment.UserName;
            user2.NombreCompleto = Environment.MachineName;
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.consultar = 1;
            Inicio();
            Actualizar();

            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();

        }

        public CuentasERP(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            try
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

     
        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
      

        private void btnFiltro_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnEnivarCorreo_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        private void btnActivarDesactivarColumnas_Click(object sender, EventArgs e)
        {

        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {

        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void CuentasERP_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

       

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultado();
        }

        private void CuentasERP_Load(object sender, EventArgs e)
        {

        }



        #region Metodos()

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


        private void LoadFreightSummary()
        {

            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chUSR_NOMBRES", "Count : {0:N0}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            //catch (TargetInvocationException ex)
            //{
            //    result = ex.InnerException.Message;
            //}
            //catch (Exception ex)
            //{
            //    result = ex.Message;
            //}
            catch (FilterExpressionException ex)
            {
                FilterDescriptor wrongFilter = this.dgvListado.Columns["chcuenta"].FilterDescriptor;
                double correctValue = 12.5;
                FilterDescriptor filterDescriptor =
                    new FilterDescriptor(wrongFilter.PropertyName, wrongFilter.Operator, correctValue);
                filterDescriptor.IsFilterEditor = wrongFilter.IsFilterEditor;

                this.dgvListado.FilterDescriptors.Remove(wrongFilter);
                this.dgvListado.FilterDescriptors.Add(filterDescriptor);

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
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


        private void Actualizar()
        {
            try
            {                
                btnMenu.Enabled = false;
                progressBar1.Visible = true;
                gbList.Enabled = false;

                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void EjecutarConsulta()
        {
            //Thread t = new Thread((ThreadStart)(() =>
            //{
            try
            {
                listado = new List<USUARIO>();
                Modelo = new NISIRAERPCuentasController();
                listado = Modelo.ListarTodos(conection);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
            //}));

            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            //t.Join();
        }

        private void MostrarResultado()
        {
            try
            {
                dgvListado.DataSource = listado.OrderBy(x => x.IDUSUARIO).ToList().ToDataTable<USUARIO>();
                dgvListado.Refresh();
                progressBar1.Visible = false;

                if (dgvListado != null)
                {
                    if (dgvListado.RowCount > 0)
                    {
                        PintarResultados(oParImpar);
                    }
                }

                progressBar1.Visible = !true;
                btnMenu.Enabled = !false;
                gbList.Enabled = !false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void PintarResultados(int ParImpar)
        {             //add a couple of sample formatting objects            

            if ((ParImpar % 2) == 0)
            {
                #region Par() | Acción pintar()                
                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Cerrado applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.DarkSalmon;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()                
                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Cerrado applied to entire row", ConditionTypes.Equal, "1", "", true);
                c4.RowForeColor = Color.White;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvListado.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }




        }



        private void Nuevo()
        {
            NoImplementado();
        }

        private void NoImplementado()
        {
            MessageBox.Show("Acción no implementada", "MENSAJE DEL SISTEMA");
        }


        #endregion

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {

        }

        private void btnVerDatosDelColaborador_Click(object sender, EventArgs e)
        {

        }

        private void btnResetearClave_Click(object sender, EventArgs e)
        {

        }

        private void btnSuspenderTemporalmente_Click(object sender, EventArgs e)
        {

        }

        private void btnActivarCuenta_Click(object sender, EventArgs e)
        {

        }
    }
}
