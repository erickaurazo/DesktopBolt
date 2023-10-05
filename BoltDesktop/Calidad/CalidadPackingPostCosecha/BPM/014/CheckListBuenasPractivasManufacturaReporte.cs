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
using ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._010;
using ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._013;
using ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._014;

namespace ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.BPM._014
{
    public partial class CheckListBuenasPractivasManufacturaReporte : Form
    {


        #region Variables() 

        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoCheckListManufacturaAllByDatesResult selectedItem;
        private List<SAS_ListadoCheckListManufacturaAllByDatesResult> resultList;
        SAS_FormatosDeInspeccionCalidadPacking model;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private string PeriodoDesde = string.Empty;
        private string PeriodoHasta = string.Empty;
        private MesController MesesNeg;
        private GlobalesHelper globalHelper;
        private int resumido;

        #endregion
        
        public CheckListBuenasPractivasManufacturaReporte()
        {
            InitializeComponent();
            connection = "SAS";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            Inicio();
            Consultar();
        }

        public CheckListBuenasPractivasManufacturaReporte(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();


            connection = _connection;
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();

            Inicio();
            Consultar();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaAsincrona();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Registar();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Historial();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {
            CambiarEstadoDispositivo();
        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            GenerarPDF();
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
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

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            #region Seleccion al cambiar cursor() 
            selectedItem = new SAS_ListadoCheckListManufacturaAllByDatesResult();
            selectedItem.CabeceraId = 0;
            selectedItem.periodo = string.Empty;
            selectedItem.Semana = "00";

            try
            {
                #region Selecionar registro()                                                                
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chCabeceraId"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chCabeceraId"].Value.ToString() != string.Empty)
                            {
                                string id = (dgvRegistro.CurrentRow.Cells["chCabeceraId"].Value != null ? dgvRegistro.CurrentRow.Cells["chCabeceraId"].Value.ToString() : string.Empty);
                                var resultado = resultList.Where(x => x.CabeceraId.ToString() == id).ToList();
                                if (resultado.ToList().Count > 0)
                                {
                                    selectedItem = resultado.ElementAt(0);

                                    if (selectedItem.EstadoId == '1')
                                    {
                                        btnEditar.Enabled = true;
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
            #endregion
        }

        private void CheckListBuenasPractivasManufacturaReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void vistaPreviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaPrevia(selectedItem);
        }

        #region Métodos() 

        private void VistaPrevia(SAS_ListadoCheckListManufacturaAllByDatesResult selectedItem)
        {            
            if (selectedItem != null)
            {
                if (selectedItem.CabeceraId != null && selectedItem.CabeceraId > 0)
                {

                    CheckListBuenasPractivasManufacturaPreview ofrm = new CheckListBuenasPractivasManufacturaPreview(connection, selectedItem);
                    ofrm.Show();
                }
            }
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
            items1.Add(new GridViewSummaryItem("chCriterio", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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


        private void Registar()
        {

        }

        private void Nuevo()
        {
            #region Limpiar controles y reiniciar elección de busqueda()

            #endregion
        }

        private void Editar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void GenerarPDF()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void CambiarEstadoDispositivo()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Anular()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Adjuntar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Exportar()
        {
            try
            {
                modelExportToExcel = new ExportToExcelHelper();
                modelExportToExcel.ExportarToExcel(dgvRegistro, saveFileDialog);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void Atras()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Eliminar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Historial()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void PresentarResultados()
        {
            try
            {
                dgvRegistro.DataSource = resultList;
                dgvRegistro.Refresh();

                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                BarraPrincipal.Enabled = true;
                progressBar1.Visible = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Consultar()
        {
            PeriodoDesde = this.txtFechaDesde.Text;
            PeriodoHasta = this.txtFechaHasta.Text;
            resumido = chkResumido.Checked == true ? 1 : 0;

            if (PeriodoDesde != string.Empty && PeriodoHasta != string.Empty)
            {
                gbCabecera.Enabled = false;
                gbList.Enabled = false;
                BarraPrincipal.Enabled = false;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();
            }


        }

        private void ElegirColumna()
        {
            this.dgvRegistro.ShowColumnChooser();
        }


        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }


        private void EjecutarConsultaAsincrona()
        {
            try
            {
                resultList = new List<SAS_ListadoCheckListManufacturaAllByDatesResult>();
                model = new SAS_FormatosDeInspeccionCalidadPacking();
                resultList = model.ListadoCheckListManufacturaAllByDates(connection, PeriodoDesde, PeriodoHasta, resumido);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        #endregion

        private void CheckListBuenasPractivasManufacturaReporte_Load(object sender, EventArgs e)
        {

        }
    }
}
