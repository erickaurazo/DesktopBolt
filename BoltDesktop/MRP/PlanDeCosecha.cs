using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using System.Reflection;
using Telerik.WinControls.UI.Export;


namespace ComparativoHorasVisualSATNISIRA.MRP
{
    public partial class PlanDeCosecha : Form
    {

        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_PlanDeCosechaController model;
        private List<SAS_PlanDeCosechaListadoFull> listing; //Listado
        private List<SAS_PlanDeCosechaListadoFull> selectedList; // ListaSelecionada
        private SAS_PlanDeCosechaListadoFull selectedItem; // Item Selecionado

        public PlanDeCosecha()
        {
            InitializeComponent();
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = "SAS";
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = "EAURAZO";
            _user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";

            _companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.imprimir = 1;
            privilege.editar = 1;
            privilege.consultar = 1;
            privilege.eliminar = 1;
            privilege.anular = 1;
            privilege.exportar = 1;

            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        public PlanDeCosecha(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }


        private void CargarMeses()
        {
            try
            {
                MesesNeg = new MesController();
                cboMes.DisplayMember = "descripcion";
                cboMes.ValueMember = "valor";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboMes.DataSource = MesesNeg.ListarMeses().ToList();
                cboMes.SelectedValue = DateTime.Now.ToString("MM");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return;
            }

        }

        private void ObtenerFechasIniciales()
        {
            try
            {
                this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return;
            }

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

        private void LoadFreightSummary()
        {
            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chdocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (FilterExpressionException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }



        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                listing = new List<SAS_PlanDeCosechaListadoFull>();
                model = new SAS_PlanDeCosechaController();
                listing = model.GetList("SAS", desde, hasta);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void btnAnularRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnDuplicarVersion_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (selectedItem != null)
            {
                if (selectedItem.idplan != null)
                {
                    PlanDeCosechaEdicion ofrm = new PlanDeCosechaEdicion("SAS", _user2, _companyId, privilege, selectedItem.idplan);
                    ofrm.ShowDialog();
                }
            }


        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvListado.Rows.Count > 0)
            {
                Exportar(dgvListado);
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

        private void PlanDeCosecha_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void PlanDeCosecha_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;                           
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnAnular.Enabled = false;
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                btnConsultar.Enabled = false;
                progressBar1.Visible = true;
                btnActualizarLista.Enabled = false;
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
                bgwHilo.RunWorkerAsync();

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
                dgvListado.DataSource = listing.ToDataTable<SAS_PlanDeCosechaListadoFull>();
                dgvListado.Refresh();
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;
                btnActualizarLista.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
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
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnGenerarNuevoPlanApartirDeEsteOrigen.Enabled = false;
            btnGenerarNuevaVersion.Enabled = false;
            selectedItem = new SAS_PlanDeCosechaListadoFull();
            selectedItem.idplan = 0;

            try
            {
                #region 

                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidplan"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chidplan"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvListado.CurrentRow.Cells["chidplan"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chidplan"].Value.ToString()) : 0);

                                if (id != 0)
                                {
                                    var resultado = listing.Where(x => x.idplan == id).ToList();
                                    if (resultado.ToList().Count >= 1)
                                    {
                                        selectedItem = resultado.ElementAt(0);
                                        if (selectedItem.idestado != "AN")
                                        {
                                            btnEditarRegistro.Enabled = true;
                                            btnAnularRegistro.Enabled = true;
                                            btnEliminarRegistro.Enabled = true;
                                            btnGenerarNuevoPlanApartirDeEsteOrigen.Enabled = true;
                                            btnGenerarNuevaVersion.Enabled = true;
                                        }


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

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }
    }
}
