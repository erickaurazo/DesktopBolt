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
using Asistencia.Negocios.PlaneamientoAgricola;

namespace ComparativoHorasVisualSATNISIRA.Planeamiento_Agricola
{
    public partial class ProgramaAgricolaEdicion : Form
    {

        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ProgramaSemanalListadoByIDResult selectedItem;
        private List<SAS_ProgramaSemanalListadoByIDResult> resultList;
        private ProgramaSemanaController model;
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
        public string ProgramaSemanalEstadoID;
        public string RequerimientoInternoEstadoID;
        public string ProgramaSemanalID;
        public string RequerimientoInternoID;
        private int modoEdicion = 0; // si es 0 no es editable, si es 1 es editable
        private List<DPROGRAMASEMANA> ListadoDetalleAnterior;
        #endregion

        public ProgramaAgricolaEdicion()
        {
            InitializeComponent();
            ProgramaSemanalID = string.Empty;

            dgvDetalle.Columns["chConsumidorArea"].ReadOnly = true;
            dgvDetalle.Columns["chLTKGSobreHA "].ReadOnly = true;

            if (modoEdicion == 1)
            {
                dgvDetalle.Columns["chConsumidorArea"].ReadOnly = false;
                dgvDetalle.Columns["chLTKGSobreHA "].ReadOnly = false;
            }

            Inicio();
            Consultar();
        }

        public ProgramaAgricolaEdicion(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege, string _ProgramaSemanalID, int _modoEdicion)
        {
            InitializeComponent();
            ProgramaSemanalID = _ProgramaSemanalID;
            modoEdicion = _modoEdicion;

            dgvDetalle.Columns["chConsumidorArea"].ReadOnly = true;
            dgvDetalle.Columns["chLTKGSobreHA"].ReadOnly = true;

            if (modoEdicion == 1)
            {
                dgvDetalle.Columns["chConsumidorArea"].ReadOnly = false;
                dgvDetalle.Columns["chLTKGSobreHA"].ReadOnly = false;
            }

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
            //CargarMeses();
            //ObtenerFechasIniciales();
            Inicio();
            Consultar();
        }

        private void Consultar()
        {
            gbDatosCampana.Enabled = true;
            gbDatosProgramaAplicacin.Enabled = true;
            gbDatosRequerimiento.Enabled = true;
            gbDetalle.Enabled = true;
            gbDocumento.Enabled = true;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();
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


        private void lblFechaHasta_Click(object sender, EventArgs e)
        {

        }

        private void txtFechaHasta_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void ProgramaAgricolaEdicion_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            resultList = new List<SAS_ProgramaSemanalListadoByIDResult>();
            model = new ProgramaSemanaController();
            resultList = model.ObtenerDetallePorID(connection, ProgramaSemanalID);

            ListadoDetalleAnterior = new List<DPROGRAMASEMANA>();
            ListadoDetalleAnterior = model.ObtenerListadoDetalle(connection, ProgramaSemanalID);


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (resultList != null)
            {
                if (resultList.ToList().Count > 0)
                {
                    selectedItem = resultList.ElementAt(0);
                    cboDocumento.Text = selectedItem.DocumentoID != null ? selectedItem.DocumentoID.Trim() : string.Empty;
                    cboSerie.Text = selectedItem.DocumentoSerie != null ? selectedItem.DocumentoSerie.Trim() : string.Empty;
                    txtNumero.Text = selectedItem.DocumentoNumero != null ? selectedItem.DocumentoNumero.Trim() : string.Empty;
                    txtFecha.Text = selectedItem.Fecha != null ? selectedItem.Fecha.Value.ToShortDateString().Trim() : string.Empty;
                    txtID.Text = selectedItem.ProgramaSemanaID != null ? selectedItem.ProgramaSemanaID.Trim() : string.Empty;
                    txtEstado.Text = selectedItem.Estado != null ? selectedItem.Estado.Trim() : string.Empty;
                    cboAnioAgricola.Text = selectedItem.AnioCampana != null ? selectedItem.AnioCampana.Trim() : string.Empty;
                    cboTipoRecurso.Text = selectedItem.Recurso != null ? selectedItem.Recurso.Trim() : string.Empty;
                    txtObservaciones.Text = selectedItem.Observaciones != null ? selectedItem.Observaciones.Trim() : string.Empty;
                    txtSucursalID.Text = selectedItem.SucursalID != null ? selectedItem.SucursalID.Trim() : string.Empty;
                    txtSucursal.Text = selectedItem.Sucursal != null ? selectedItem.Sucursal.Trim() : string.Empty;
                    txtAlmacenID.Text = selectedItem.AlmacenID != null ? selectedItem.AlmacenID.Trim() : string.Empty;
                    txtAlmacen.Text = selectedItem.Almacen != null ? selectedItem.Almacen.Trim() : string.Empty;
                    txtMotivoID.Text = selectedItem.MotivoID != null ? selectedItem.MotivoID.Trim() : string.Empty;
                    txtMotivo.Text = selectedItem.Motivo != null ? selectedItem.Motivo.Trim() : string.Empty;
                    txtAreaID.Text = selectedItem.AreaID != null ? selectedItem.AreaID.Trim() : string.Empty;
                    txtArea.Text = selectedItem.Area != null ? selectedItem.Area.Trim() : string.Empty;
                    txtActividadID.Text = selectedItem.ActividadID != null ? selectedItem.ActividadID.Trim() : string.Empty;
                    txtActividad.Text = selectedItem.Actividad != null ? selectedItem.Actividad.Trim() : string.Empty;
                    txtLaborID.Text = selectedItem.LaborID != null ? selectedItem.LaborID.Trim() : string.Empty;
                    txtLabor.Text = selectedItem.Labor != null ? selectedItem.Labor.Trim() : string.Empty;
                    txtSupervisorID.Text = selectedItem.ResponsableID != null ? selectedItem.ResponsableID.Trim() : string.Empty;
                    txtSupervisor.Text = selectedItem.Responsable != null ? selectedItem.Responsable.Trim() : string.Empty;
                    txtRequerimientoID.Text = selectedItem.RequerimientoInterno != null ? selectedItem.RequerimientoInterno.Trim() : string.Empty;
                    txtRequerimientoEstadoSolicitud.Text = selectedItem.RequerimientoInternoEstado != null ? selectedItem.RequerimientoInternoEstado.Trim() : string.Empty;
                    BarraPrincipal.Enabled = true;

                    dgvDetalle.DataSource = resultList.ToDataTable<SAS_ProgramaSemanalListadoByIDResult>();
                    dgvDetalle.Refresh();

                }
                else
                {

                }
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (resultList != null)
            {
                if (resultList.ToList().Count > 0)
                {
                    selectedItem = resultList.ElementAt(0);
                    if (selectedItem.EstadoID == "PE" && selectedItem.RequerimientoInternoEstadoID == "PE")
                    {
                        #region Editar()

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("El documento no tiene el estado adecuado para realizar esta modificación", "MENSAJE DEL SISTEMA");
                        return;
                    }
                }

            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            gbDetalle.Enabled = false;
            bgwActualizarProgramaSemanal.RunWorkerAsync();

        }

        private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal Area = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chConsumidorArea"].Value);
            decimal Dosis = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chLTKGSobreHA"].Value);
            decimal TOTAL = Math.Round((Area * Dosis), 4);
            (dgvDetalle.CurrentRow.Cells["chProductoTotal"].Value) = TOTAL;
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            decimal Area = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chConsumidorArea"].Value);
            decimal Dosis = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chLTKGSobreHA"].Value);
            decimal TOTAL = Math.Round((Area * Dosis), 4);
            (dgvDetalle.CurrentRow.Cells["chProductoTotal"].Value) = TOTAL;
        }

        private void dgvDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            decimal Area = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chConsumidorArea"].Value);
            decimal Dosis = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells["chLTKGSobreHA"].Value);
            decimal TOTAL = Math.Round((Area * Dosis), 4);
            (dgvDetalle.CurrentRow.Cells["chProductoTotal"].Value) = TOTAL;
        }

        private void bgwActualizarProgramaSemanal_DoWork(object sender, DoWorkEventArgs e)
        {
            PROGRAMASEMANA programa = new PROGRAMASEMANA();
            programa.IDPROGRAMASEMANA = ProgramaSemanalID;
            List<DPROGRAMASEMANA> detallePrograma = new List<DPROGRAMASEMANA>();

            if (this.dgvDetalle != null)
            {
                if (this.dgvDetalle.Rows.Count > 0)
                {
                    foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                    {
                        if (fila.Cells["chItem"].Value.ToString().Trim() != String.Empty)
                        {
                            try
                            {
                                #region Obtener detalle por linea detalle() 
                                DPROGRAMASEMANA detalleDelPrograma = new DPROGRAMASEMANA();
                                detalleDelPrograma.IDPROGRAMASEMANA = ProgramaSemanalID;
                                detalleDelPrograma.IDPRODUCTO = fila.Cells["chProductoID"].Value != null ? Convert.ToString(fila.Cells["chProductoID"].Value.ToString().Trim()) : string.Empty;
                                detalleDelPrograma.IDCONSUMIDOR = fila.Cells["chConsumidorID"].Value != null ? Convert.ToString(fila.Cells["chConsumidorID"].Value.ToString().Trim()) : string.Empty;
                                detalleDelPrograma.ITEM = fila.Cells["chItem"].Value != null ? Convert.ToString(fila.Cells["chItem"].Value.ToString().Trim()) : string.Empty;
                                detalleDelPrograma.cantidad_hectarea = fila.Cells["chLTKGSobreHA"].Value != null ? Convert.ToDecimal(fila.Cells["chLTKGSobreHA"].Value.ToString().Trim()) : Convert.ToInt32('0');
                                detalleDelPrograma.total = fila.Cells["chProductoTotal"].Value != null ? Convert.ToDecimal(fila.Cells["chProductoTotal"].Value.ToString().Trim()) : Convert.ToInt32('0');
                                detalleDelPrograma.AREA = fila.Cells["chConsumidorArea"].Value != null ? Convert.ToDecimal(fila.Cells["chConsumidorArea"].Value.ToString().Trim()) : Convert.ToInt32('0');

                                //programa.idcodigoGeneral = fila.Cells["chidCodigoGeneral"].Value != null ? fila.Cells["chidCodigoGeneral"].Value.ToString().Trim() : string.Empty;
                                //programa.desde = fila.Cells["chDesdeColaborador"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeColaborador"].Value.ToString().Trim()) : (DateTime?)null;
                                //programa.hasta = fila.Cells["chHastaColaborador"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaColaborador"].Value.ToString().Trim()) : (DateTime?)null;
                                //programa.observacion = fila.Cells["chObservacionColaborador"].Value != null ? fila.Cells["chObservacionColaborador"].Value.ToString().Trim() : "";
                                //programa.fechaCreacion = DateTime.Now;
                                //programa.registradoPor = Environment.UserName.ToString();
                                //programa.tipo = fila.Cells["chTipoColaborador"].Value != null ? Convert.ToChar(fila.Cells["chTipoColaborador"].Value.ToString().Trim()) : Convert.ToChar('P');
                                //programa.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesColaborador"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesColaborador"].Value.ToString().Trim()) : Convert.ToInt32('1');
                                #endregion
                                detallePrograma.Add(detalleDelPrograma);
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                return;
                            }

                        }
                    }

                }
            }


            model.ActualizarDosis(connection, programa, detallePrograma, "notifyProgramaSemanalBolt@saturno.net.pe", "Modificación de programa semanal agrícola", ListadoDetalleAnterior);
           
        }

        private void bgwActualizarProgramaSemanal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BarraPrincipal.Enabled = !false;
            progressBar1.Visible = !true;
            gbDetalle.Enabled = !false;
            MessageBox.Show("PROCESO FINALIZADO CON ÉXITO", ",MENSAJE DEL SISTEMA");
        }
    }
}

