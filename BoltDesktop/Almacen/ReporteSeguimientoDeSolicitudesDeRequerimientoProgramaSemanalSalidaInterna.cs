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
using Asistencia.Negocios.Almacen;

namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    public partial class ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna : Form
    {

        #region Variables() 
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string CompanyId, Desde, Hasta, AreaID = string.Empty;
        private string Connection;
        private SAS_RptSolicitudDeRequerimientoConProgramaSemanal selectedItem;
        private List<SAS_RptSolicitudDeRequerimientoConProgramaSemanal> listAll;
        private List<SAS_ListadoAreaEnProgramaSemanalResult> listAllAreas;
        private SolicitudesDeRequerimientoInternoControllers Model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;

        public MesController MesesNeg;
        private ExportToExcelHelper modelExportToExcel;
        private int IdEvaluacion = 0;
        private int Evaluado;
        private int Distribuido;
        private int Revisado;

        public int ParImparFiltro = 0;
        private int ClickResaltarResultados = 1;
        #endregion

        public ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna()
        {
            InitializeComponent();
            selectedItem = new SAS_RptSolicitudDeRequerimientoConProgramaSemanal();
            Connection = "SAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            user.IdCodigoGeneral = "100369";
            CompanyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.editar = 1;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;
            CargarComboBox();
            ObtenerFechasIniciales();
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


        public ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            selectedItem = new SAS_RptSolicitudDeRequerimientoConProgramaSemanal();
            Connection = _connection;
            user = _userLogin;
            CompanyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = user.IdUsuario != null ? user.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = user.NombreCompleto != null ? user.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            CargarComboBox();
            ObtenerFechasIniciales();
            MakeInquiry();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ParImparFiltro += 1;
            ActivarFiltro();
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
            CambiarEstadoDeDistribucion();
        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            VistaPrevia(0);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MakeInquiry();
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
        }

        private void ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna_Load(object sender, EventArgs e)
        {

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
                Desde = DateTime.Now.ToString("yyyyMMdd");
                Hasta = DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                Desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd");
                Hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");
            }
            AreaID = cboArea.SelectedValue.ToString().Trim();

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

        private void CargarComboBox()
        {
            try
            {

                #region Ejecutar consulta para cargar cbo() 
                MesesNeg = new MesController();
                cboMes.DisplayMember = "descripcion";
                cboMes.ValueMember = "valor";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboMes.DataSource = MesesNeg.ListarMeses().ToList();
                cboMes.SelectedValue = DateTime.Now.ToString("MM");

                Model = new SolicitudesDeRequerimientoInternoControllers();
                listAllAreas = new List<SAS_ListadoAreaEnProgramaSemanalResult>();
                listAllAreas = Model.GetListadoAreasConProgamasSemanal(Connection);
                cboArea.DisplayMember = "Area";
                cboArea.ValueMember = "AreaID";
                cboArea.DataSource = listAllAreas;
                cboArea.SelectedValue = "034";

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error al cargar la información en combos");
                return;
            }


        }

        private void EjecutarConsulta()
        {

            listAll = new List<SAS_RptSolicitudDeRequerimientoConProgramaSemanal>();
            Model = new SolicitudesDeRequerimientoInternoControllers();


            try
            {
                listAll = Model.GetListadoReporteRequerimientoConProgramaSemanalByArea(Connection, Desde, Hasta, AreaID);
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
                dgvRegistros.DataSource = listAll.ToDataTable<SAS_RptSolicitudDeRequerimientoConProgramaSemanal>();
                dgvRegistros.Refresh();
                ResaltarResultados();
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
            //if (Id != 0)
            //{
            //    TrazabilidadDeContenedorDespachosPreView ofrm = new TrazabilidadDeContenedorDespachosPreView(conection, Id);
            //    ofrm.Show();
            //}
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
            //if (IdEvaluacion > 0)
            //{
            //    int resultado = Model.CambiarEstadoDeEvaluacion(Connection, IdEvaluacion);
            //    if (resultado > 0)
            //    {
            //        MessageBox.Show("Se actualizo el estado de la Evaluacion del contenedor", "Confirmación del sistema");
            //        MakeInquiry();
            //    }
            //}
        }

        private void btnAprobacionDistribucionSub_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeDistribucion();
        }

        private void CambiarEstadoDeDistribucion()
        {
            //if (IdEvaluacion > 0)
            //{
            //    int resultado = model.CambiarEstadoDeDistribucion(conection, IdEvaluacion);
            //    if (resultado > 0)
            //    {
            //        MessageBox.Show("Se actualizo el estado de la distribución del contenedor", "Confirmación del sistema");
            //        MakeInquiry();
            //    }
            //}
        }

        private void btnAprobacionRevisionSub_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeRevision();
        }

        private void CambiarEstadoDeRevision()
        {
            //if (IdEvaluacion > 0)
            //{
            //    int resultado = model.CambiarEstadoDeRevision(conection, IdEvaluacion);
            //    if (resultado > 0)
            //    {
            //        MessageBox.Show("Se actualizo el estado de la revisión del contenedor", "Confirmación del sistema");
            //        MakeInquiry();
            //    }
            //}
        }

        private void elminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //private void btnActivarFiltro_Click(object sender, EventArgs e)
        //{
        //    ParImparFiltro += 1;
        //    ActivarFiltro();
        //}



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

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ClickResaltarResultados += 1;
            ResaltarResultados();
        }


        private void ResaltarResultados()
        {

            if ((ClickResaltarResultados % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ATENDIDO TOTAL", string.Empty, true);
                c1.RowBackColor = Color.MediumSeaGreen;
                c1.CellBackColor = Color.MediumSeaGreen;
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ATENDIDO PARCIAL", string.Empty, true);
                c2.RowBackColor = Color.WhiteSmoke;
                c2.CellBackColor = Color.WhiteSmoke;
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c2);


                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "APROBADO", string.Empty, true);
                c3.RowBackColor = Color.SkyBlue;
                c3.CellBackColor = Color.SkyBlue;
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c3);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ANULADO", string.Empty, true);
                c4.RowBackColor = Color.Indigo;
                c4.CellBackColor = Color.Indigo;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);

                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PENDIENTE", string.Empty, true);
                c5.RowBackColor = Color.Gainsboro;
                c5.CellBackColor = Color.Gainsboro;
                c5.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c5);


                ConditionalFormattingObject c6 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PROGRAMA SEMANAL", string.Empty, true);
                //c5.RowBackColor = Color.Gainsboro;
                //c5.CellBackColor = Color.Gainsboro;
                c6.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
                dgvRegistros.Columns["chProgramaSemanalOrigenDelRequerimiento"].ConditionalFormattingObjectList.Add(c6);

                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ATENDIDO TOTAL", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ATENDIDO PARCIAL", string.Empty, true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                c2.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c2);


                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "APROBADO", string.Empty, true);
                c3.RowBackColor = Color.White;
                c3.CellBackColor = Color.White;
                c3.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c3);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PENDIENTE", string.Empty, true);
                c4.RowBackColor = Color.Black;
                c4.CellBackColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);


                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ANULADO", string.Empty, true);
                c5.RowBackColor = Color.Black;
                c5.CellBackColor = Color.Black;
                c5.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstado"].ConditionalFormattingObjectList.Add(c5);

                ConditionalFormattingObject c6 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PROGRAMA SEMANAL", string.Empty, true);
                //c5.RowBackColor = Color.Gainsboro;
                //c5.CellBackColor = Color.Gainsboro;
                c6.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chProgramaSemanalOrigenDelRequerimiento"].ConditionalFormattingObjectList.Add(c6);

                #endregion
            }
        }


        private void CambiarEstadoDocument()
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
