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

namespace ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha
{
    public partial class IncumplimientoBuenasPracticasHigieneReporte : Form
    {
        #region Variables()
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_ReporteIncumplimientoPracticasHigieneByDateResult selectedItem;
        private List<SAS_ReporteIncumplimientoPracticasHigieneByDateResult> result;
        IncumplimientoBuenasPracticasHigieneController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_ReporteIncumplimientoPracticasHigieneByDateResult selectItemById;
        public MesController MesesNeg;
        private ExportToExcelHelper modelExportToExcel;
        #endregion

        public IncumplimientoBuenasPracticasHigieneReporte()
        {
            InitializeComponent();
            selectedItem = new SAS_ReporteIncumplimientoPracticasHigieneByDateResult();
            selectedItem.idCabeceraRegistro = 0;

            CargarMeses();
            ObtenerFechasIniciales();
            conection = "SAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.editar = 1;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;
            //btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            //btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            //btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            gbCabecera.Enabled = false;
            gbList.Enabled = false;
            Consult();
        }

        public IncumplimientoBuenasPracticasHigieneReporte(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            selectedItem = new SAS_ReporteIncumplimientoPracticasHigieneByDateResult();
            selectedItem.idCabeceraRegistro = 0;
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
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;            
            btnAdjuntar.Enabled = true;            
            btnCerrar.Enabled = true;
            gbCabecera.Enabled = false;
            gbList.Enabled = false;
            Consult();
        }

        private void Consult()
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

            gbList.Enabled = false;
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void EjecutarConsulta()
        {
            result = new List<SAS_ReporteIncumplimientoPracticasHigieneByDateResult>();
            model = new IncumplimientoBuenasPracticasHigieneController();

            try
            {
                result = model.GetListByDate(conection, desde, hasta);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
        }

        private void MostrarResultados()
        {
            try
            {
                dgvRegistro.DataSource = result.ToDataTable<SAS_ReporteIncumplimientoPracticasHigieneByDateResult>();
                dgvRegistro.Refresh();
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                return;
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            CambiarFechasComboBox();
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            CambiarFechasComboBox();
            
        }

        private void CambiarFechasComboBox()
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void imprimirFormatoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aprobarEvaluacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {

        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.ExportarToExcel(dgvRegistro, saveFileDialog);
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            selectedItem = new SAS_ReporteIncumplimientoPracticasHigieneByDateResult();
            selectedItem.idCabeceraRegistro = 0;

            try
            {
                #region Selecionar registro()                                                                
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chidCabeceraRegistro"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chidCabeceraRegistro"].Value.ToString() != string.Empty)
                            {
                                string id = (dgvRegistro.CurrentRow.Cells["chidCabeceraRegistro"].Value != null ? dgvRegistro.CurrentRow.Cells["chidCabeceraRegistro"].Value.ToString() : string.Empty);
                                var resultado = result.Where(x => x.idCabeceraRegistro.ToString() == id).ToList();
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

        }

        private void btnEliminarSub(object sender, EventArgs e)
        {

        }

        private void btnAnularSub(object sender, EventArgs e)
        {

        }

        private void btnVistaPreviaSub(object sender, EventArgs e)
        {
            VistaPrevia(selectedItem);
        }

        private void VistaPrevia(SAS_ReporteIncumplimientoPracticasHigieneByDateResult selectedItem)
        {
            if (selectedItem != null)
            {
                if (selectedItem.idCabeceraRegistro != null && selectedItem.idCabeceraRegistro > 0)
                {

                    IncumplimientoBuenasPracticasHigieneView ofrm = new IncumplimientoBuenasPracticasHigieneView(conection, selectedItem.idCabeceraRegistro);
                    ofrm.Show();
                }
            }
        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void IncumplimientoBuenasPracticasHigieneReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
      
        private void IncumplimientoBuenasPracticasHigieneReporte_Load(object sender, EventArgs e)
        {

        }

        #region Métodos()
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
        #endregion
    }
}
