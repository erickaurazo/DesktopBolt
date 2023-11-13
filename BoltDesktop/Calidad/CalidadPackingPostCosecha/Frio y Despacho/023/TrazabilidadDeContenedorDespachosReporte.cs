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
using Asistencia.Negocios.Calidad;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._045;

namespace ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._023
{
    public partial class TrazabilidadDeContenedorDespachosReporte : Form
    {
        #region Variables() 
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_EvaluacionTrazabilidadFCLDespachoResult selectedItem;
        private List<SAS_EvaluacionTrazabilidadFCLDespachoResult> listAll;
        private EvaluacionTrazabilidadFCLDespachoController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_ReporteIncumplimientoPracticasHigieneByDateResult selectItemById;
        public MesController MesesNeg;
        private ExportToExcelHelper modelExportToExcel;
        private int IdEvaluacion = 0;
        private int Evaluado;
        private int Distribuido;
        private int Revisado;

        public int ParImparFiltro = 0;
        #endregion

        public TrazabilidadDeContenedorDespachosReporte()
        {
            InitializeComponent();
            selectedItem = new SAS_EvaluacionTrazabilidadFCLDespachoResult();
            selectedItem.Id = 0;

            CargarMeses();
            ObtenerFechasIniciales();
            conection = "SAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            user.IdCodigoGeneral = "100369";
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
            MakeInquiry();
        }

        public TrazabilidadDeContenedorDespachosReporte(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            selectedItem = new SAS_EvaluacionTrazabilidadFCLDespachoResult();
            selectedItem.Id = 0;

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
            MakeInquiry();

        }



        private void TrazabilidadDeContenedorDespachosReporte_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MakeInquiry();
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
            Grabar();
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
            EliminarRegistro();
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
            CambiarEstadoDocumen();

        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            VistaPrevia(IdEvaluacion);
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
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

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            CambiarFechasComboBox();
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            CambiarFechasComboBox();
        }

        private void TrazabilidadDeContenedorDespachosReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dgvRegistros_SelectionChanged(object sender, EventArgs e)
        {
            #region Seleccion al cambiar cursor() 
            selectedItem = new SAS_EvaluacionTrazabilidadFCLDespachoResult();
            selectedItem.Id = 0;
            IdEvaluacion = 0;
            Evaluado = 0;
            Distribuido = 0;
            Revisado = 0;
            btnAprobacionDistribucionSub.Enabled = false;
            btnAprobacionEvaluacionSub.Enabled = false;
            btnAprobacionRevisionSub.Enabled = false;


            try
            {
                #region Selecionar registro()                                                                
                if (dgvRegistros != null && dgvRegistros.Rows.Count > 0)
                {
                    if (dgvRegistros.CurrentRow != null)
                    {
                        if (dgvRegistros.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistros.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                IdEvaluacion = (dgvRegistros.CurrentRow.Cells["chId"].Value != null ? Convert.ToInt32(dgvRegistros.CurrentRow.Cells["chId"].Value.ToString()) : 0);
                                var resultado = listAll.Where(x => x.Id == IdEvaluacion).ToList();
                                if (resultado.ToList().Count > 0)
                                {
                                    selectedItem = resultado.ElementAt(0);
                                    if (IdEvaluacion > 0)
                                    {
                                        btnAprobacionDistribucionSub.Enabled = true;
                                        btnAprobacionEvaluacionSub.Enabled = true;
                                        btnAprobacionRevisionSub.Enabled = true;
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


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
        }

        #region Metodos()

        private void Nuevo()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }
        private void MakeInquiry()
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

        private void Editar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }


        private void Grabar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }


        private void EliminarRegistro()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Atras()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void Anular()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }


        private void Historial()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void ElegirColumna()
        {
            this.dgvRegistros.ShowColumnChooser();
        }


        private void Exportar()
        {
            try
            {
                modelExportToExcel = new ExportToExcelHelper();
                modelExportToExcel.ExportarToExcel(dgvRegistros, saveFileDialog);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void CambiarFechasComboBox()
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
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

        private void EjecutarConsulta()
        {

            listAll = new List<SAS_EvaluacionTrazabilidadFCLDespachoResult>();
            model = new EvaluacionTrazabilidadFCLDespachoController();

            try
            {
                listAll = model.GetListByDate(conection, desde, hasta);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void MostrarResultados()
        {
            try
            {
                dgvRegistros.DataSource = listAll.ToDataTable<SAS_EvaluacionTrazabilidadFCLDespachoResult>();
                dgvRegistros.Refresh();
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

        private void dgvRegistros_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vistaPreviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaPrevia(IdEvaluacion);
        }

        private void VistaPrevia(int Id)
        {
            if (Id != 0)
            {
                TrazabilidadDeContenedorDespachosPreView ofrm = new TrazabilidadDeContenedorDespachosPreView(conection, Id);
                ofrm.Show();
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

        private void btnAprobacionEvaluacionSub_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeEvaluacion();
        }

        private void CambiarEstadoDeEvaluacion()
        {
            if (IdEvaluacion > 0)
            {
                int resultado = model.CambiarEstadoDeEvaluacion(conection, IdEvaluacion);
                if (resultado > 0)
                {
                    MessageBox.Show("Se actualizo el estado de la Evaluacion del contenedor", "Confirmación del sistema");
                    MakeInquiry();
                }
            }
        }

        private void btnAprobacionDistribucionSub_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeDistribucion();
        }

        private void CambiarEstadoDeDistribucion()
        {
            if (IdEvaluacion > 0)
            {
                int resultado = model.CambiarEstadoDeDistribucion(conection, IdEvaluacion);
                if (resultado > 0)
                {
                    MessageBox.Show("Se actualizo el estado de la distribución del contenedor", "Confirmación del sistema");
                    MakeInquiry();
                }
            }
        }

        private void btnAprobacionRevisionSub_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeRevision();
        }

        private void CambiarEstadoDeRevision()
        {
            if (IdEvaluacion > 0)
            {
                int resultado = model.CambiarEstadoDeRevision(conection, IdEvaluacion);
                if (resultado > 0)
                {
                    MessageBox.Show("Se actualizo el estado de la revisión del contenedor", "Confirmación del sistema");
                    MakeInquiry();
                }
            }
        }

        private void elminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ParImparFiltro += 1;
            ActivarFiltro();
        }



        private void ActivarFiltro()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistros.EnableFiltering = !true;
                dgvRegistros.ShowHeaderCellButtons = !true;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistros.EnableFiltering = true;
                dgvRegistros.ShowHeaderCellButtons = true;
                #endregion
            }
        }

        private void Adjuntar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void CambiarEstadoDocumen()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
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
