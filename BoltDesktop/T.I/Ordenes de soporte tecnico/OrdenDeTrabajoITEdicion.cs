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
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using Telerik.WinControls.Data;
using System.Threading;
using Telerik.WinControls.UI.Localization;
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;
using ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico;
using System.Text.RegularExpressions;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class OrdenDeTrabajoITEdicion : Form
    {
        #region Variables() 

        const string nombreformulario = "OrdenTrabajoITD";
        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades, canalesDeAtencion, tiempoProgramados, tiempoEjecutados;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private int codigoSelecionado = 0;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_DispositivoOrdenTrabajoController model;
        private List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult> listing; //Listado
        private List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalleEliminado = new List<SAS_DispositivoOrdenTrabajoDetalle>(); // 
        private List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>(); // 
        private SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem; // Item Selecionado
        private SAS_ListadoDeDispositivoOrdenTrabajoByIdResult item;
        private SAS_DispositivoOrdenTrabajo ordenTrabajo;
        List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult> listDetalleByCodigoMantenimiento;
        List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult> listDetalleHerramientasById;
        List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult> listDetalleSuministrosById;
        private int ultimoItemEnListaDetalle = 1;
        private int ultimoItemHerramientasEnListaDetalle = 1;
        private int ultimoItemSuministroEnListaDetalle = 1;
        private int codigoDispositivo = 0;
        private SAS_DispositivoUsuariosController modeloDispositivo = new SAS_DispositivoUsuariosController();
        private List<SAS_DispositivoOrdenTrabajoDetalleHerramientas> listadoHerramientas = new List<SAS_DispositivoOrdenTrabajoDetalleHerramientas>();
        private List<SAS_DispositivoOrdenTrabajoDetalleHerramientas> listadoHerramientasEliminados = new List<SAS_DispositivoOrdenTrabajoDetalleHerramientas>();
        private List<SAS_DispositivoOrdenTrabajoDetalleSuministroAlmacen> listadoSuministro = new List<SAS_DispositivoOrdenTrabajoDetalleSuministroAlmacen>();
        private List<SAS_DispositivoOrdenTrabajoDetalleSuministroAlmacen> listadoSuministroEliminados = new List<SAS_DispositivoOrdenTrabajoDetalleSuministroAlmacen>();
        private SAS_DispositivoTipoMantenimientoController modelTipoMantenimiento;
        private List<SAS_DispositivosTipoMantenimientoDetalle> listadoDetalleByItem;


        #endregion

        public OrdenDeTrabajoITEdicion()
        {
            InitializeComponent();
            Inicio();
        }

        public OrdenDeTrabajoITEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoSelecionado)
        {
            InitializeComponent();
            listadoDetalleEliminado = new List<SAS_DispositivoOrdenTrabajoDetalle>();
            listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>();
            listadoHerramientas = new List<SAS_DispositivoOrdenTrabajoDetalleHerramientas>();
            listadoSuministro = new List<SAS_DispositivoOrdenTrabajoDetalleSuministroAlmacen>();
            //btnGenerarReprogramacion.Enabled = false;
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSelecionado;
            Inicio();
            CargarCombos();

            BarraPrincipal.Enabled = false;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            progressBar1.Visible = true;

            bgwHilo.RunWorkerAsync();


        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                documentos = new List<Grupo>();
                series = new List<Grupo>();
                tipoSolicitudes = new List<Grupo>();

                canalesDeAtencion = new List<Grupo>();
                tiempoProgramados = new List<Grupo>();
                tiempoEjecutados = new List<Grupo>();


                documentos = comboHelper.GetDocumentTypeForForm("SAS", "MANTENIMIENTOS TI");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                series = comboHelper.GetDocumentSeriesForForm("SAS", "Equipamiento tecnologico");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();

                tipoSolicitudes = comboHelper.GetTypesOfMaintenance("SAS", "Equipamiento tecnologico");
                cboMantenimientoTipo.DisplayMember = "Descripcion";
                cboMantenimientoTipo.ValueMember = "Codigo";
                cboMantenimientoTipo.DataSource = tipoSolicitudes.OrderBy(x => x.Descripcion).ToList();
                cboMantenimientoTipo.SelectedValue = "001";


                tipoDePrioridades = comboHelper.GetPriorityList("SAS", "MANTENIMIENTOS TI");
                cboPrioridad.DisplayMember = "Descripcion";
                cboPrioridad.ValueMember = "Codigo";
                cboPrioridad.DataSource = tipoDePrioridades.OrderBy(x => x.Id).ToList();
                cboPrioridad.SelectedValue = "3";

                canalesDeAtencion = comboHelper.GetCanalesDeATencion("SAS");
                cboCanalDeAtencion.DisplayMember = "Descripcion";
                cboCanalDeAtencion.ValueMember = "Codigo";
                cboCanalDeAtencion.DataSource = canalesDeAtencion.OrderBy(x => x.Id).ToList();
                cboCanalDeAtencion.SelectedValue = "0";

                tiempoProgramados = comboHelper.TiempoProgramado("SAS");
                cboTiempoProgramado.DisplayMember = "Descripcion";
                cboTiempoProgramado.ValueMember = "Codigo";
                cboTiempoProgramado.DataSource = tiempoProgramados.ToList();
                cboTiempoProgramado.SelectedValue = "0";

                tiempoEjecutados = comboHelper.TiempoEjecutado("SAS");
                cboTiempoEjecutado.DisplayMember = "Descripcion";
                cboTiempoEjecutado.ValueMember = "Codigo";
                cboTiempoEjecutado.DataSource = tiempoEjecutados.ToList();
                cboTiempoEjecutado.SelectedValue = "0";




            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
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

        private void OrdenDeTrabajoITEdicion_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.codigoSelecionado = 0;
            bgwHilo.RunWorkerAsync();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RecordObject();
        }

        private void RecordObject()
        {
            btnRegistrar.Enabled = false;
            BarraPrincipal.Enabled = false;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            progressBar1.Visible = true;

            ordenTrabajo = new SAS_DispositivoOrdenTrabajo();
            if (ValidacionParaRegistrar() == true)
            {
                ordenTrabajo = ObtenerObjeto();

                // add 24.04.2022
                #region listado de acciones() 
                listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>();
                int totalDeActivades = 0;
                int totalDeActivadesCompletadas = 0;
                totalDeActivades = this.dgvDetalle.RowCount;

                if (this.dgvDetalle != null)
                {
                    #region 
                    if (this.dgvDetalle.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                        {
                            if (fila.Cells["chOTCodigoDetalle"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoOrdenTrabajoDetalle recordObject = new SAS_DispositivoOrdenTrabajoDetalle();
                                    recordObject.codigo = fila.Cells["chOTCodigoDetalle"].Value != null ? Convert.ToInt32(fila.Cells["chOTCodigoDetalle"].Value.ToString().Trim()) : 0;
                                    recordObject.item = fila.Cells["chItemDetalle"].Value != null ? fila.Cells["chItemDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.accion = fila.Cells["chAccionDetalle"].Value != null ? fila.Cells["chAccionDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.desde = fila.Cells["chDesdeDetalle"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeDetalle"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.hasta = fila.Cells["chHastaDetalle"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaDetalle"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.estado = fila.Cells["chEstadoCodigoDetalle"].Value != null ? Convert.ToByte(fila.Cells["chEstadoCodigoDetalle"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    recordObject.usuario = fila.Cells["chUsuarioDetalle"].Value != null ? fila.Cells["chUsuarioDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.costoUSD = fila.Cells["chValorUSDAccion"].Value != null ? Convert.ToDecimal(fila.Cells["chValorUSDAccion"].Value.ToString().Trim()) : 0;
                                    recordObject.HorasFormatoReloj = fila.Cells["chHorasFormatoReloj"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasFormatoReloj"].Value.ToString().Trim()) : 0;
                                    recordObject.HorasFormatoPlanilla = fila.Cells["chHorasFormatoPlanilla"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasFormatoPlanilla"].Value.ToString().Trim()) : 0;
                                    recordObject.glosa = fila.Cells["chGlosaDetalle"].Value != null ? fila.Cells["chGlosaDetalle"].Value.ToString().Trim() : string.Empty;

                                    if (recordObject.estado == 1)
                                    {
                                        totalDeActivadesCompletadas += 0;
                                    }
                                    else if (recordObject.estado == 2)
                                    {
                                        totalDeActivadesCompletadas += 1;
                                    }
                                    else if (recordObject.estado == 3)
                                    {
                                        totalDeActivadesCompletadas += 1;
                                    }
                                    else if (recordObject.estado == 0)
                                    {
                                        totalDeActivadesCompletadas += 1;
                                    }

                                    #endregion
                                    listadoDetalle.Add(recordObject);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                    #endregion
                }

                if (totalDeActivades == totalDeActivadesCompletadas)
                {
                    ordenTrabajo.idEstado = "AT";
                }
                else
                {
                    if (totalDeActivadesCompletadas == 0)
                    {
                        ordenTrabajo.idEstado = "PE";
                    }
                    else
                    {
                        ordenTrabajo.idEstado = "TP";
                    }
                }

                #endregion


                model = new SAS_DispositivoOrdenTrabajoController();
                //int resultadoRegistro = model.RegisterObject("SAS", ordenTrabajo);
                int resultadoRegistro = model.RegisterObject("SAS", ordenTrabajo, listadoDetalleEliminado, listadoDetalle);
                MessageBox.Show("Operación realizada con éxito", "Confirmación del sistema");
                this.codigoSelecionado = resultadoRegistro;
                listadoDetalleEliminado = new List<SAS_DispositivoOrdenTrabajoDetalle>();
                listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>();
                bgwRegistrar.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Deben considerarse los campos requeridos para el registro", "Notificación del sistema");
                btnRegistrar.Enabled = !false;
                BarraPrincipal.Enabled = !false;
                gbDatosPersonal.Enabled = !false;
                gbDetale.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }
        }

        private bool ValidacionParaRegistrar()
        {
            bool resultado = true;
            // ANULADO, ATENDIDO TOTAL, ARCHIVADO
            if (this.txtEstadoCodigo.Text == "AN" || this.txtEstadoCodigo.Text == "AT" || this.txtEstadoCodigo.Text == "CE")
            {
                resultado = false;
                MessageBox.Show("El documento no tiene el estado para edición", "Notificación del sistema");
                txtPersonalCodigo.Focus();
                return resultado;
            }

            if (this.txtPersonalCodigo.Text.Trim() == string.Empty)
            {
                resultado = false;
                MessageBox.Show("Debe ingresar el código del personal", "Notificación del sistema");
                txtPersonalCodigo.Focus();
                return resultado;
            }


            if (this.txtDispositivoCodigo.Text.Trim() == string.Empty)
            {
                resultado = false;
                MessageBox.Show("Debe ingresar el código del dispositivo", "Notificación del sistema");
                txtDispositivoCodigo.Focus();
                return resultado;
            }

            if (this.txtObservaciones.Text.Trim() == string.Empty)
            {
                resultado = false;
                MessageBox.Show("Debe ingresar una descripción", "Notificación del sistema");
                txtObservaciones.Focus();
                return resultado;
            }
            else
            {
                if (this.txtObservaciones.Text.Trim().Length <= 20)
                {
                    resultado = false;
                    MessageBox.Show("Debe ingresar una descripción valida superior a 20 caracteres", "Notificación del sistema");
                    txtObservaciones.Focus();
                    return resultado;
                }
            }




            string ASCD = this.txtValidar.Text.ToString().Trim();
            if (this.txtFechaFinalizacion.Text.ToString().Trim() == ASCD)
            {
                MessageBox.Show("Debe ingresar una fecha en el formato validado dd/MM/yyyy", "Notificación del sistema");
                this.txtFechaFinalizacion.Focus();
                resultado = false;
                return resultado;
            }

            if (this.txtFecha.Text.ToString().Trim() == ASCD)
            {
                MessageBox.Show("Debe ingresar una fecha de finalización en el formato validado dd/MM/yyyy", "Notificación del sistema");
                this.txtFecha.Focus();
                resultado = false;
                return resultado;
            }

            if (chkEjecutadoPorExterno.Checked == true)
            {
                if (this.txtProveedorCodigo.Text.Trim() == string.Empty)
                {
                    resultado = false;
                    MessageBox.Show("Al ser un tercero quien realiza este mantenimiento, registrar el RUC del proveedor", "Notificación del sistema");
                    this.txtProveedorCodigo.Focus();
                    return resultado;
                }

            }


            return resultado;
        }

        private SAS_DispositivoOrdenTrabajo ObtenerObjeto()
        {
            SAS_DispositivoOrdenTrabajo ot = new SAS_DispositivoOrdenTrabajo();
            try
            {
                #region Obtener Objeto

                ot.codigo = (this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0);
                ot.codigoPersonal = this.txtPersonalCodigo.Text.Trim();
                ot.idSerie = cboSerie.SelectedValue.ToString();
                ot.iddocumento = item.iddocumento = this.cboDocumento.SelectedValue.ToString();
                ot.fecha = Convert.ToDateTime(this.txtFecha.Text);
                //ot.periodo = item.periodo;
                ot.idTipoMantenimiento = cboMantenimientoTipo.SelectedValue.ToString();
                ot.Observación = this.txtObservaciones.Text.Trim();
                ot.idEstado = this.txtEstadoCodigo.Text;
                ot.idDispositivo = this.txtDispositivoCodigo.Text != string.Empty ? Convert.ToInt32(this.txtDispositivoCodigo.Text.Trim()) : 0;
                ot.usuario = user2.IdUsuario.Trim();
                ot.fechaCreacion = DateTime.Now;
                ot.idEmpresa = this.txtEmpresaCodigo.Text.Trim();
                ot.idSucursal = this.txtSucursalCodigo.Text.Trim();
                ot.costoUSD = this.txtCostoTotalDeMantenimiento.Text != string.Empty ? Convert.ToDecimal(this.txtCostoTotalDeMantenimiento.Text.Trim()) : 0;
                ot.horasEstimadas = Convert.ToDecimal(cboTiempoEjecutado.SelectedValue);
                ot.minutosProgramados = Convert.ToDecimal(cboTiempoProgramado.SelectedValue);
                ot.CanalDeAtencionCodigo = Convert.ToInt32(cboCanalDeAtencion.SelectedValue);

                ot.fechaEstimadaFinalizacion = Convert.ToDateTime(this.txtFechaFinalizacion.Text);
                ot.EsejecutadoPorPersonalExterno = chkEjecutadoPorExterno.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);
                ot.requiereSupervisionSST = chkConSupervisionSST.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);
                ot.esUnTrabajoProgramado = chkEsProgramado.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);
                ot.idclieprov = this.txtProveedorCodigo.Text.Trim();
                ot.numeroDeTicketEmpresaExterna = this.txtNroTicket.Text.Trim();
                ot.numeroDePedido = this.txtNroPedido.Text.Trim();
                ot.prioridad = Convert.ToByte(cboPrioridad.SelectedValue.ToString());
                ot.cerradoEnPrimeraAtencion = chkCerradoConPrimerContacto.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);
                ot.EsReprogracion = chkEsAtencionReprogramada.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);

                return ot;
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema | Obtener objeto para registro");
                return ot;
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void EditarRegistro()
        {
            if (this.txtEstadoCodigo.Text == "PE" || this.txtEstadoCodigo.Text == "TP" || this.txtEstadoCodigo.Text == "CE" || this.txtEstadoCodigo.Text == "RR")
            {
                btnEditar.Enabled = false;
                btnRegistrar.Enabled = true;
                btnNuevo.Enabled = false;
                btnAtras.Enabled = true;
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para registrar una edición", "MENSAJE DEL SISTEMA");
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
                item = model.GetListById("SAS", this.codigoSelecionado);

                listDetalleByCodigoMantenimiento = new List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>();
                listDetalleByCodigoMantenimiento = model.GetListDetalleByCodigoMantenimiento("SAS", this.codigoSelecionado).ToList();

                listDetalleHerramientasById = new List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult>();
                listDetalleHerramientasById = model.GetListDetalleHerramientasByID("SAS", this.codigoSelecionado).ToList();

                listDetalleSuministrosById = new List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult>();
                listDetalleSuministrosById = model.GetListDetalleSuministroByID("SAS", this.codigoSelecionado).ToList();



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                gbDatosPersonal.Enabled = !false;
                gbDetale.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }



        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                if (item != null)
                {
                    #region 
                    if (item.codigo != (int?)null)
                    {
                        if (item.codigo == 0)
                        {
                            #region Nuevo() 
                            LimpiarFormulario();
                            dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>());
                            dgvDetalle.Refresh();
                            btnGenerarReprogramacion.Enabled = true;
                            btnRegistrar.Enabled = true;
                            btnEditar.Enabled = false;
                            btnNuevo.Enabled = false;
                            #endregion
                        }
                        else if (item.codigo > 0)
                        {
                            #region Asignar Objeto para edición() 
                            txtCodigo.Text = item.codigo.ToString();
                            txtCorrelativo.Text = item.codigo.ToString().PadLeft(7, '0');
                            txtPersonalCodigo.Text = item.codigoPersonal != null ? item.codigoPersonal.ToString() : string.Empty;
                            txtPersonal.Text = item.colaborador != null ? item.colaborador.Trim() : string.Empty;
                            cboSerie.SelectedValue = item.idSerie.ToString();
                            cboPrioridad.SelectedValue = item.prioridadId.ToString();
                            cboDocumento.SelectedValue = item.iddocumento.ToString();
                            txtFecha.Text = item.fecha.ToShortDateString();
                            cboMantenimientoTipo.SelectedValue = item.idTipoMantenimiento.Trim();
                            txtObservaciones.Text = item.observacion.Trim();
                            txtEstadoCodigo.Text = item.idEstado.ToString().Trim();
                            txtEstado.Text = item.estado != null ? item.estado.Trim() : string.Empty;
                            txtDispositivoCodigo.Text = item.idDispositivo.ToString().Trim();
                            if (item.idDispositivo != (int?)null)
                            {
                                if (item.idDispositivo > 0)
                                {
                                    CompletarDatosDelTipoDeDispositivo(item.idDispositivo.ToString().Trim());
                                }

                            }

                            txtDispositivoDescripcion.Text = item.dispositivo != null ? item.dispositivo.Trim().ToUpper() : string.Empty;
                            txtUsuarioAsignado.Text = item.usuario.Trim();
                            txtEmpresaCodigo.Text = item.idEmpresa.Trim();
                            txtEmpresa.Text = item.empresa != null ? item.empresa.Trim() : string.Empty;
                            txtSucursalCodigo.Text = item.idSucursal.Trim();
                            txtSucursal.Text = item.sucursal != null ? item.sucursal.Trim() : string.Empty;
                            txtCostoTotalDeMantenimiento.Text = item.costoUSD.ToDecimalPresentation();

                            decimal tiempoEjecutadoNativo = Convert.ToDecimal((item.horasEstimadas != (decimal?)null ? item.horasEstimadas.Value : 0));
                            decimal tiempoEjecutadoParteEntera = Math.Truncate(tiempoEjecutadoNativo);
                            decimal tiempoEjecutadoDiferencia = tiempoEjecutadoNativo - tiempoEjecutadoParteEntera;
                            if (tiempoEjecutadoDiferencia == 0)
                            {
                                cboTiempoEjecutado.SelectedValue = Convert.ToInt32(tiempoEjecutadoNativo).ToString();
                            }
                            else
                            {
                                cboTiempoEjecutado.SelectedValue = Convert.ToDecimal(tiempoEjecutadoNativo).ToString();
                            }



                            decimal tiempoProgramadoNativo = Convert.ToDecimal((item.minutosProgramados != (decimal?)null ? item.minutosProgramados.Value : 0));
                            decimal tiempProgramadoParteEntera = Math.Truncate(tiempoProgramadoNativo);
                            decimal tiempoProgramadoDiferencia = tiempoProgramadoNativo - tiempProgramadoParteEntera;
                            if (tiempoEjecutadoDiferencia == 0)
                            {
                                cboTiempoProgramado.SelectedValue = Convert.ToInt32(tiempoProgramadoNativo).ToString();
                            }
                            else
                            {
                                cboTiempoProgramado.SelectedValue = Convert.ToDecimal(tiempoProgramadoNativo).ToString();
                            }



                            cboCanalDeAtencion.SelectedValue = item.CanalDeAtencionCodigo.ToString();

                            txtFechaFinalizacion.Text = item.fechaEstimadaFinalizacion.Value.ToShortDateString();
                            txtProveedorCodigo.Text = item.idclieprov.Trim();
                            txtProveedor.Text = item.proveedor != null ? item.proveedor.Trim() : string.Empty;
                            txtNroTicket.Text = item.numeroDeTicketEmpresaExterna.Trim();
                            txtNroPedido.Text = item.numeroDePedido.Trim();



                            chkEjecutadoPorExterno.Checked = false;
                            if (item.EsejecutadoPorPersonalExterno.Value == 1)
                            {
                                chkEjecutadoPorExterno.Checked = true;
                            }


                            chkConSupervisionSST.Checked = false;
                            if (item.requiereSupervisionSST.Value == 1)
                            {
                                chkConSupervisionSST.Checked = true;
                            }


                            chkEsProgramado.Checked = false;
                            if (item.esUnTrabajoProgramado.Value == 1)
                            {
                                chkEsProgramado.Checked = true;
                            }

                            #region Add 01.06.2023()
                            chkEsAtencionReprogramada.Checked = false;
                            if (item.EsReprogracion == 1)
                            {

                                chkEsAtencionReprogramada.Checked = true;
                            }

                            chkCerradoConPrimerContacto.Checked = false;
                            if (item.cerradoEnPrimeraAtencion == 1)
                            {
                                chkCerradoConPrimerContacto.Checked = true;
                            }
                            #endregion


                            btnPersonalBuscar.Enabled = false;
                            btnDispositivoBuscar.Enabled = false;
                            this.txtPersonal.ReadOnly = true;
                            this.txtPersonalCodigo.ReadOnly = true;
                            this.txtDispositivoCodigo.ReadOnly = true;
                            this.txtDispositivoDescripcion.ReadOnly = true;


                            btnGenerarReprogramacion.Enabled = true;
                            if (item.idEstado == "RR" || item.idEstado == "CE")
                            {
                                btnGenerarReprogramacion.Enabled = false;
                            }

                            #endregion


                            #region Listado detalle() 

                            ultimoItemEnListaDetalle = 1;
                            ultimoItemHerramientasEnListaDetalle = 1;
                            ultimoItemSuministroEnListaDetalle = 1;

                            ultimoItemEnListaDetalle = 1;

                            if (listDetalleByCodigoMantenimiento != null)
                            {
                                if (listDetalleByCodigoMantenimiento.Count > 0)
                                {
                                    ultimoItemEnListaDetalle = Convert.ToInt32(listDetalleByCodigoMantenimiento.Max(X => X.item));
                                }
                            }


                            dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>());
                            dgvDetalle.Refresh();
                            #endregion

                            #region listado Herramientas
                            if (listDetalleHerramientasById != null)
                            {
                                if (listDetalleHerramientasById.Count > 0)
                                {
                                    ultimoItemHerramientasEnListaDetalle = Convert.ToInt32(listDetalleHerramientasById.Max(X => X.item));
                                }
                            }
                            dgvHerramientas.CargarDatos(listDetalleHerramientasById.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult>());
                            dgvHerramientas.Refresh();
                            #endregion

                            #region listado Suministros()
                            if (listDetalleSuministrosById != null)
                            {
                                if (listDetalleSuministrosById.Count > 0)
                                {
                                    ultimoItemSuministroEnListaDetalle = Convert.ToInt32(listDetalleSuministrosById.Max(X => X.item));
                                }
                            }
                            dgvSuministrosAlmacen.CargarDatos(listDetalleSuministrosById.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult>());
                            dgvSuministrosAlmacen.Refresh();
                            #endregion

                            btnRegistrar.Enabled = true;
                            btnEditar.Enabled = false;
                            btnNuevo.Enabled = true;
                        }
                    }
                    else
                    {
                        #region Nuevo() 
                        LimpiarFormulario();
                        dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>());
                        dgvDetalle.Refresh();

                        dgvHerramientas.CargarDatos(listDetalleHerramientasById.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult>());
                        dgvHerramientas.Refresh();

                        dgvSuministrosAlmacen.CargarDatos(listDetalleSuministrosById.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult>());
                        dgvSuministrosAlmacen.Refresh();
                        btnGenerarReprogramacion.Enabled = true;
                        btnRegistrar.Enabled = true;
                        btnEditar.Enabled = false;
                        btnNuevo.Enabled = false;
                        #endregion
                    }
                    #endregion
                }

                BarraPrincipal.Enabled = true;
                gbDatosPersonal.Enabled = true;
                gbDetale.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = true;
                gbDatosPersonal.Enabled = true;
                gbDetale.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
                return;
            }


        }

        private void LimpiarFormulario()
        {
            model = new SAS_DispositivoOrdenTrabajoController();
            int ultimoRegistro = model.ObtenerUltimoOperacion("SAS");
            ultimoItemEnListaDetalle = 1;
            listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>();
            listadoDetalleEliminado = new List<SAS_DispositivoOrdenTrabajoDetalle>();

            cboPrioridad.SelectedValue = "3";

            this.txtCodigo.Text = "0"; // cuando es 0 es nuevo
            this.txtCorrelativo.Text = ultimoRegistro.ToString().PadLeft(7, '0'); // traer el último registrado + 1, que solo se va a mostrar.
            this.txtCostoTotalDeMantenimiento.Text = "0.00";
            this.txtDispositivoCodigo.Text = string.Empty;
            this.txtDispositivoDescripcion.Text = string.Empty;

            this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO SA";
            this.txtEmpresaCodigo.Text = "001";

            this.txtEstado.Text = "PENDIENTE";
            this.txtFecha.Text = DateTime.Now.ToShortDateString();
            this.cboTiempoEjecutado.SelectedValue = "0";
            this.cboTiempoEjecutado.SelectedValue = "0";
            this.cboCanalDeAtencion.SelectedValue = "0";
            this.txtEstadoCodigo.Text = "PE";
            this.txtNroPedido.Text = string.Empty;
            this.txtNroTicket.Text = string.Empty;
            this.txtPersonal.Text = string.Empty;
            this.txtPersonalCodigo.Text = string.Empty;

            this.txtFechaFinalizacion.Text = DateTime.Now.ToShortDateString();
            this.txtObservaciones.Text = string.Empty;

            this.txtProveedor.Text = string.Empty;
            this.txtProveedorCodigo.Text = string.Empty;
            this.txtSucursal.Text = "SEDE LOGISTICA AGRICOLA";
            this.txtSucursalCodigo.Text = "001";
            this.txtUsuarioAsignado.Text = user2.IdUsuario.Trim().ToUpper();


            txtCodigoTipoDispositivo.Clear();
            txTipoDispositivoDescripcion.Clear();

            btnPersonalBuscar.Enabled = !false;
            btnDispositivoBuscar.Enabled = !false;
            this.txtPersonal.ReadOnly = !true;
            this.txtPersonalCodigo.ReadOnly = !true;
            this.txtDispositivoCodigo.ReadOnly = !true;
            this.txtDispositivoDescripcion.ReadOnly = !true;

            chkEjecutadoPorExterno.Checked = false;
            chkConSupervisionSST.Checked = false;
            chkEsProgramado.Checked = false;
            item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
            item.codigo = 0;

            chkEsAtencionReprogramada.Checked = false;
            chkCerradoConPrimerContacto.Checked = false;

        }

        private void txtPersonal_TextChanged(object sender, EventArgs e)
        {
            ReformularConsultaEnFormularioDeBusquedaDeDispositivo();
        }

        private void ReformularConsultaEnFormularioDeBusquedaDeDispositivo()
        {
            try
            {
                btnDispositivoBuscar.P_TablaConsulta = "SAS_ListadoPersonalExternoInternoAsignadoBienITD where  dispositivoCodigo != 0 and funcionamiento = 1 and estadoitem = 1 and idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }
        }

        private void txtPersonalCodigo_TextChanged(object sender, EventArgs e)
        {
            ReformularConsultaEnFormularioDeBusquedaDeDispositivo();
        }

        private void txtPersonalCodigo_Leave(object sender, EventArgs e)
        {
            ReformularConsultaEnFormularioDeBusquedaDeDispositivo();
        }

        private void txtPersonalCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            ReformularConsultaEnFormularioDeBusquedaDeDispositivo();
        }

        private void chkEjecutadoPorExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEjecutadoPorExterno.Checked == false)
            {
                this.txtProveedor.Text = string.Empty;
                this.txtProveedorCodigo.Text = string.Empty;
            }
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
                item = model.GetListById("SAS", this.codigoSelecionado);
                listDetalleByCodigoMantenimiento = new List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>();
                listDetalleByCodigoMantenimiento = model.GetListDetalleByCodigoMantenimiento("SAS", this.codigoSelecionado).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Presentacion de objeto con detalle() 
                if (item != null)
                {
                    if (item.codigo != (int?)null)
                    {
                        if (item.codigo == 0)
                        {
                            #region Nuevo() 
                            LimpiarFormulario();
                            #region cierre()
                            btnRegistrar.Enabled = false;
                            btnEditar.Enabled = true;
                            btnNuevo.Enabled = true;
                            btnPersonalBuscar.Enabled = false;
                            btnDispositivoBuscar.Enabled = false;
                            this.txtPersonal.ReadOnly = true;
                            this.txtPersonalCodigo.ReadOnly = true;
                            this.txtDispositivoCodigo.ReadOnly = true;
                            this.txtDispositivoDescripcion.ReadOnly = true;
                            BarraPrincipal.Enabled = true;
                            gbDatosPersonal.Enabled = true;
                            gbDetale.Enabled = true;
                            gbDocumento.Enabled = true;
                            progressBar1.Visible = false;
                            #endregion

                            #endregion
                        }
                        else if (item.codigo > 0)
                        {
                            #region Asignar Objeto para edición() 
                            txtCodigo.Text = item.codigo.ToString();
                            this.txtCorrelativo.Text = item.codigo.ToString().PadLeft(7, '0');
                            txtPersonalCodigo.Text = item.codigoPersonal != null ? item.codigoPersonal.ToString() : string.Empty;
                            txtPersonal.Text = item.colaborador != null ? item.colaborador.Trim() : string.Empty;
                            cboSerie.SelectedValue = item.idSerie.ToString();
                            cboDocumento.SelectedValue = item.iddocumento.ToString();
                            txtFecha.Text = item.fecha.ToShortDateString();
                            cboMantenimientoTipo.SelectedValue = item.idTipoMantenimiento.Trim();
                            txtObservaciones.Text = item.observacion.Trim();
                            txtEstadoCodigo.Text = item.idEstado.ToString().Trim();
                            this.txtEstado.Text = item.estado != null ? item.estado.Trim() : string.Empty;
                            txtDispositivoCodigo.Text = item.idDispositivo.ToString().Trim();
                            this.txtDispositivoDescripcion.Text = item.dispositivo != null ? item.dispositivo.Trim().ToUpper() : string.Empty;
                            txtUsuarioAsignado.Text = item.usuario.Trim();
                            txtEmpresaCodigo.Text = item.idEmpresa.Trim();
                            txtEmpresa.Text = item.empresa != null ? item.empresa.Trim() : string.Empty;
                            txtSucursalCodigo.Text = item.idSucursal.Trim();
                            this.txtSucursal.Text = item.sucursal != null ? item.sucursal.Trim() : string.Empty;
                            txtCostoTotalDeMantenimiento.Text = item.costoUSD.ToDecimalPresentation();


                            decimal tiempoEjecutadoNativo = Convert.ToDecimal((item.horasEstimadas != (decimal?)null ? item.horasEstimadas.Value : 0));
                            decimal tiempoEjecutadoParteEntera = Math.Truncate(tiempoEjecutadoNativo);
                            decimal tiempoEjecutadoDiferencia = tiempoEjecutadoNativo - tiempoEjecutadoParteEntera;
                            if (tiempoEjecutadoDiferencia == 0)
                            {
                                cboTiempoEjecutado.SelectedValue = Convert.ToInt32(tiempoEjecutadoNativo).ToString();
                            }
                            else
                            {
                                cboTiempoEjecutado.SelectedValue = Convert.ToDecimal(tiempoEjecutadoNativo).ToString();
                            }



                            decimal tiempoProgramadoNativo = Convert.ToDecimal((item.minutosProgramados != (decimal?)null ? item.minutosProgramados.Value : 0));
                            decimal tiempProgramadoParteEntera = Math.Truncate(tiempoProgramadoNativo);
                            decimal tiempoProgramadoDiferencia = tiempoProgramadoNativo - tiempProgramadoParteEntera;
                            if (tiempoEjecutadoDiferencia == 0)
                            {
                                cboTiempoProgramado.SelectedValue = Convert.ToInt32(tiempoProgramadoNativo).ToString();
                            }
                            else
                            {
                                cboTiempoProgramado.SelectedValue = Convert.ToDecimal(tiempoProgramadoNativo).ToString();
                            }



                            cboCanalDeAtencion.SelectedValue = item.CanalDeAtencionCodigo.ToString();



                            txtFechaFinalizacion.Text = item.fechaEstimadaFinalizacion.Value.ToShortDateString();
                            txtProveedorCodigo.Text = item.idclieprov.Trim();
                            this.txtProveedor.Text = item.proveedor != null ? item.proveedor.Trim() : string.Empty;
                            txtNroTicket.Text = item.numeroDeTicketEmpresaExterna.Trim();
                            txtNroPedido.Text = item.numeroDePedido.Trim();

                            chkEjecutadoPorExterno.Checked = false;
                            if (item.EsejecutadoPorPersonalExterno.Value == 1)
                            {
                                chkEjecutadoPorExterno.Checked = true;
                            }


                            chkConSupervisionSST.Checked = false;
                            if (item.requiereSupervisionSST.Value == 1)
                            {
                                chkConSupervisionSST.Checked = true;
                            }


                            chkEsProgramado.Checked = false;
                            if (item.esUnTrabajoProgramado.Value == 1)
                            {
                                chkEsProgramado.Checked = true;
                            }
                            #endregion
                        }

                        #region Listado detalle() 

                        ultimoItemEnListaDetalle = 1;

                        if (listDetalleByCodigoMantenimiento != null)
                        {
                            if (listDetalleByCodigoMantenimiento.Count > 0)
                            {
                                ultimoItemEnListaDetalle = Convert.ToInt32(listDetalleByCodigoMantenimiento.Max(X => X.item));
                            }
                        }
                        #endregion
                        dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>());
                        dgvDetalle.Refresh();
                        #region cierre()
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnPersonalBuscar.Enabled = false;
                        btnDispositivoBuscar.Enabled = false;
                        this.txtPersonal.ReadOnly = true;
                        this.txtPersonalCodigo.ReadOnly = true;
                        this.txtDispositivoCodigo.ReadOnly = true;
                        this.txtDispositivoDescripcion.ReadOnly = true;
                        BarraPrincipal.Enabled = true;
                        gbDatosPersonal.Enabled = true;
                        gbDetale.Enabled = true;
                        gbDocumento.Enabled = true;
                        progressBar1.Visible = false;
                        #endregion
                    }
                    else
                    {
                        #region Nuevo() 
                        LimpiarFormulario();
                        #region cierre()
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnPersonalBuscar.Enabled = false;
                        btnDispositivoBuscar.Enabled = false;
                        this.txtPersonal.ReadOnly = true;
                        this.txtPersonalCodigo.ReadOnly = true;
                        this.txtDispositivoCodigo.ReadOnly = true;
                        this.txtDispositivoDescripcion.ReadOnly = true;
                        BarraPrincipal.Enabled = true;
                        gbDatosPersonal.Enabled = true;
                        gbDetale.Enabled = true;
                        gbDocumento.Enabled = true;
                        progressBar1.Visible = false;
                        #endregion
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                #region cierre()
                btnRegistrar.Enabled = false;
                btnEditar.Enabled = true;
                btnNuevo.Enabled = true;
                btnPersonalBuscar.Enabled = false;
                btnDispositivoBuscar.Enabled = false;
                this.txtPersonal.ReadOnly = true;
                this.txtPersonalCodigo.ReadOnly = true;
                this.txtDispositivoCodigo.ReadOnly = true;
                this.txtDispositivoDescripcion.ReadOnly = true;
                BarraPrincipal.Enabled = true;
                gbDatosPersonal.Enabled = true;
                gbDetale.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
                #endregion
                return;
            }

        }

        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {
            if (ValidacionParaRegistrar() == true)
            {
                AddItemAListaDetalle();
            }
            else
            {
                MessageBox.Show("Deben considerarse los campos requeridos para el registro", "Notificación del sistema");
                btnRegistrar.Enabled = !false;
                BarraPrincipal.Enabled = !false;
                gbDatosPersonal.Enabled = !false;
                gbDetale.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
            }
        }

        private string ObtenerFormatoParaAgregarItemDetalle(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }

        private void AddItemAListaDetalle()
        {
            try
            {
                if (dgvDetalle != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chOTCodigoDetalle                  
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalle))); // chItemDetalle
                    array.Add(string.Empty); // chAccionDetalle
                    array.Add(Convert.ToDecimal(0)); // chHorasReloj
                    array.Add(Convert.ToDecimal(0)); // chHorasPlanilla
                    array.Add(this.txtFecha.Text); // chDesdeDetalle
                    array.Add(this.txtFecha.Text); // chHastaDetalle                                        
                    array.Add(1); // chEstadoCodigoDetalle
                    array.Add("PENDIENTE"); // chEstadoDetalle          
                    array.Add(txtUsuarioAsignado.Text.Trim()); // chUsuarioDetalle
                    array.Add(string.Empty); // chGlosaDetalle
                    array.Add(0.0); // chValorUSDAccion                    
                    dgvDetalle.AgregarFila(array);
                    ultimoItemEnListaDetalle += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void AddItemAListaDetalleByListDetail(List<SAS_DispositivosTipoMantenimientoDetalle> listItem)
        {
            try
            {
                if (listItem != null)
                {
                    if (listItem.ToList().Count > 0)
                    {
                        foreach (var item in listItem)
                        {
                            #region Add()
                            if (dgvDetalle != null)
                            {
                                ArrayList array = new ArrayList();
                                array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chOTCodigoDetalle                  
                                array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalle))); // chItemDetalle
                                array.Add(item.descripcion.Trim()); // chAccionDetalle
                                array.Add(Convert.ToDecimal(0)); // chHorasReloj
                                array.Add(item.minutos.Value.ToDecimalPresentation()); // chHorasPlanilla
                                //array.Add(DateTime.Now.ToShortDateString()); // chDesdeDetalle
                                //array.Add(DateTime.Now.AddDays(Convert.ToInt32(Math.Round(Convert.ToDecimal(this.txtHorasEstimadas.Value / 8)))).ToShortDateString()); // chHastaDetalle              
                                array.Add(this.txtFecha.Text);
                                array.Add(this.txtFecha.Text);
                                array.Add(1); // chEstadoCodigoDetalle
                                array.Add("PENDIENTE"); // chEstadoDetalle          
                                array.Add(txtUsuarioAsignado.Text); // chUsuarioDetalle
                                array.Add(string.Empty); // chGlosaDetalle
                                array.Add(0.0); // chValorUSDAccion                    
                                dgvDetalle.AgregarFila(array);
                                ultimoItemEnListaDetalle += 1;
                            }
                            else
                            {
                                Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                            }

                            #endregion
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {

            if (ValidacionParaRegistrar() == true)
            {
                RemoveItemDetail();
            }
            else
            {
                MessageBox.Show("Deben considerarse los campos requeridos para el registro", "Notificación del sistema");
            }

        }

        private void RemoveItemDetail()
        {

            if (this.dgvDetalle != null)
            {
                #region
                if (dgvDetalle.CurrentRow != null && dgvDetalle.CurrentRow.Cells["chOTCodigoDetalle"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvDetalle.CurrentRow.Cells["chOTCodigoDetalle"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalle.CurrentRow.Cells["chOTCodigoDetalle"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value != null | dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value.ToString().Trim() != string.Empty) ? (dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoDetalleEliminado.Add(new SAS_DispositivoOrdenTrabajoDetalle
                                {
                                    codigo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }

                        }

                        dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                        return;
                    }
                    //}
                }
                #endregion
            }
        }

        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStateDetailItem();
        }

        private void ChangeStateDetailItem()
        {
            try
            {

                if (dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value.ToString() == "1")
                {
                    dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value = "0";
                    dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value = "ANULADO";
                }
                else
                {
                    dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value = "1";
                    dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value = "PENDIENTE";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnIrACatalogo_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text != string.Empty)
            {
                codigoDispositivo = Convert.ToInt32(txtDispositivoCodigo.Text);
            }

            if (codigoDispositivo > 0)
            {
                SAS_ListadoDeDispositivos oDispositivo = new SAS_ListadoDeDispositivos();
                modeloDispositivo = new SAS_DispositivoUsuariosController();
                oDispositivo = modeloDispositivo.ObtenerDispositivoById("SAS", codigoDispositivo);

                DispositivosEdicion oFron = new DispositivosEdicion("SAS", oDispositivo, user2, companyId, privilege);
                //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                oFron.MdiParent = OrdenDeTrabajoITEdicion.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();

            }
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            comboHelper = new ComboBoxHelper();

            if (((DataGridView)sender).RowCount > 0)
            {
                #region 
                string estado = this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoCodigoDetalle"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoCodigoDetalle"].Value.ToString() : "0";
                if (estado == "1")
                {
                    if (user2 != null)
                    {
                        if (user2.IdUsuario != null)
                        {
                            if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                            {
                                #region Cambiar de estado()  
                                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chEstadoDetalle")
                                {
                                    if (e.KeyCode == Keys.F3)
                                    {
                                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                        search.ListaGeneralResultado = comboHelper.GetListState("SAS");
                                        search.Text = "Estados de la tarea desarrollada";
                                        search.txtTextoFiltro.Text = "";
                                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                        {
                                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoCodigoDetalle"].Value = search.ObjetoRetorno.Codigo;
                                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoDetalle"].Value = search.ObjetoRetorno.Descripcion;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chUsuarioDetalle")
                    {
                        #region Usuario Asignado()
                        if (e.KeyCode == Keys.F3)
                        {
                            frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                            search.ListaGeneralResultado = comboHelper.GetListUser("SAS");
                            search.Text = "Asignar usuario";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chUsuarioDetalle"].Value = search.ObjetoRetorno.Codigo;
                                //this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chUsuarioDetalle"].Value = search.ObjetoRetorno.Codigo;
                            }
                        }
                        #endregion
                    }

                    #region Listado de minutos() 
                    if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHorasFormatoPlanilla")
                    {
                        if (e.KeyCode == Keys.F3)
                        {
                            model = new SAS_DispositivoOrdenTrabajoController();
                            frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                            search.ListaGeneralResultado = model.ObtenerListadoDeMinutos("SAS");
                            search.Text = "Buscar minutos para asignacion de actividades y labores";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                                this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasFormatoPlanilla"].Value = search.ObjetoRetorno.Codigo;
                                //this.dgvDetail.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasFormatoPlanilla"].Value = search.ObjetoRetorno.Descripcion;
                            }
                        }
                    }
                    #endregion


                }
                #endregion
            }




        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void btnHerramientasCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStateDetailHerramientaItem();
        }

        private void ChangeStateDetailHerramientaItem()
        {
            try
            {

                if (dgvHerramientas.CurrentRow.Cells["chestado"].Value.ToString() == "1")
                {
                    dgvHerramientas.CurrentRow.Cells["chestado"].Value = "0";
                    //dgvHerramientas.CurrentRow.Cells["chestado"].Value = "ANULADO";
                }
                else
                {
                    dgvHerramientas.CurrentRow.Cells["chestado"].Value = "1";
                    // dgvHerramientas.CurrentRow.Cells["chestado"].Value = "PENDIENTE";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void AddItemAListaHerramientaDetalle()
        {
            try
            {
                if (dgvHerramientas != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chOTCodigoHerramienta                  
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemHerramientasEnListaDetalle))); // chItemHerramienta
                    array.Add(string.Empty); // chHerramientaCodigo
                    array.Add(string.Empty); // chHerramienta
                    array.Add(string.Empty); // chidmedidaHerramienta
                    array.Add(string.Empty); // chunidadMedidaHerramienta
                    array.Add(Convert.ToDecimal(0)); // chCantidadHerramienta
                    array.Add(user2.IdUsuario); // chusuarioHerramienta
                    array.Add(string.Empty); // chglosaHerramienta                   
                    array.Add(1); // chestado
                    array.Add(0.0); // chValorUSDHerramienta                    
                    dgvHerramientas.AgregarFila(array);
                    ultimoItemHerramientasEnListaDetalle += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnHerramientasAgregar_Click(object sender, EventArgs e)
        {
            if (ValidacionParaRegistrar() == true)
            {
                AddItemAListaHerramientaDetalle();
            }
            else
            {
                MessageBox.Show("Deben considerarse los campos requeridos para el registro", "Notificación del sistema");
                //btnRegistrar.Enabled = !false;
                //BarraPrincipal.Enabled = !false;
                //gbDatosPersonal.Enabled = !false;
                //gbDetale.Enabled = !false;
                //gbDocumento.Enabled = !false;
                //progressBar1.Visible = !true;
            }
        }

        private void btnHerramientasQuitar_Click(object sender, EventArgs e)
        {
            if (ValidacionParaRegistrar() == true)
            {
                RemoveItemDetailHerramienta();
            }
            else
            {
                MessageBox.Show("Deben considerarse los campos requeridos para el registro", "Notificación del sistema");

            }
        }

        private void RemoveItemDetailHerramienta()
        {

            if (this.dgvHerramientas != null)
            {
                #region
                if (dgvHerramientas.CurrentRow != null && dgvHerramientas.CurrentRow.Cells["chOTCodigoHerramienta"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvHerramientas.CurrentRow.Cells["chOTCodigoHerramienta"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvHerramientas.CurrentRow.Cells["chOTCodigoHerramienta"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvHerramientas.CurrentRow.Cells["chItemHerramienta"].Value != null | dgvHerramientas.CurrentRow.Cells["chItemHerramienta"].Value.ToString().Trim() != string.Empty) ? (dgvHerramientas.CurrentRow.Cells["chItemHerramienta"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoHerramientasEliminados.Add(new SAS_DispositivoOrdenTrabajoDetalleHerramientas
                                {
                                    codigo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }

                        }

                        dgvHerramientas.Rows.Remove(dgvHerramientas.CurrentRow);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                        return;
                    }
                    //}
                }
                #endregion
            }
        }

        private void dgvHerramientas_KeyUp(object sender, KeyEventArgs e)
        {
            comboHelper = new ComboBoxHelper();

            if (((DataGridView)sender).RowCount > 0)
            {
                string estado = this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chestado"].Value != null ? this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chestado"].Value.ToString() : "0";
                if (estado == "1")
                {
                    #region Productos()  
                    if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHerramienta")
                    {
                        if (e.KeyCode == Keys.F3)
                        {
                            frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                            search.ListaGeneralResultado = comboHelper.GetListHerramientasIT("SAS");
                            search.Text = "Listado de productos";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHerramientaCodigo"].Value = search.ObjetoRetorno.Codigo;
                                string[] valores = search.ObjetoRetorno.Descripcion.Split('|');
                                this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHerramienta"].Value = valores[0];
                                this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidmedidaHerramienta"].Value = valores[1];
                                this.dgvHerramientas.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chunidadMedidaHerramienta"].Value = valores[2];
                            }
                        }
                    }
                    #endregion
                }

            }
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Attach()
                if (this.txtCodigo.Text != string.Empty)
                {
                    if (this.txtCodigo.Text != "0")
                    {
                        codigoSelecionado = Convert.ToInt32(this.txtCodigo.Text);
                        AdjuntarArchivos ofrm = new AdjuntarArchivos(conection, user2, companyId, privilege, codigoSelecionado.ToString(), nombreformulario);
                        ofrm.Show();

                    }
                    else
                    {
                        MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnNotificar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Notify() 
                if (this.txtCodigo.Text != string.Empty)
                {
                    if (this.txtCodigo.Text != "0")
                    {
                        codigoSelecionado = Convert.ToInt32(this.txtCodigo.Text);
                        gbDatosPersonal.Enabled = false;
                        gbDetale.Enabled = false;
                        gbDocumento.Enabled = false;
                        progressBar1.Visible = true;
                        BarraPrincipal.Enabled = false;
                        bgwNotify.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwNotify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                model.Notify(conection, "soporte@saturno.net.pe", "Solicitud de atención por soporte técnico", codigoSelecionado);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnCompletarConActividadesDefinidas_Click(object sender, EventArgs e)
        {
            string seleccionTipoManteniento = cboMantenimientoTipo.SelectedValue.ToString().Trim();
            string seleccionTipoHardware = txtCodigoTipoDispositivo.Text.Trim();

            if (this.txtObservaciones.Text.Trim().Length > 10)
            {
                #region Autocompletar() 
                if (seleccionTipoManteniento != string.Empty)
                {
                    if (seleccionTipoManteniento != "000")
                    {
                        if (seleccionTipoHardware != string.Empty)
                        {
                            if (Convert.ToInt32(seleccionTipoHardware) > 0)
                            {
                                #region AutoCompletar
                                modelTipoMantenimiento = new Asistencia.Negocios.SAS_DispositivoTipoMantenimientoController();
                                listadoDetalleByItem = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                                SAS_DispositivosTipoMantenimiento oDetalle = new SAS_DispositivosTipoMantenimiento();
                                oDetalle.id = cboMantenimientoTipo.SelectedValue.ToString().Trim();
                                listadoDetalleByItem = modelTipoMantenimiento.GetToListDetail(conection, oDetalle, seleccionTipoHardware).ToList();

                                if (listadoDetalleByItem != null)
                                {
                                    if (listadoDetalleByItem.ToList().Count > 0)
                                    {
                                        AddItemAListaDetalleByListDetail(listadoDetalleByItem);
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Debe ingresar una descripción válida", "Mensaje del sistema");
            }
        }

        private void txtDispositivoCodigo_Leave(object sender, EventArgs e)
        {
            if (txtDispositivoCodigo.Text.Trim() != string.Empty && txtDispositivoCodigo.Text.Trim() != string.Empty)
            {
                CompletarDatosDelTipoDeDispositivo(txtDispositivoCodigo.Text);
            }
        }

        private void CompletarDatosDelTipoDeDispositivo(string codigoDispositvo)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                int? codigoDispositivo = -10;
                codigoDispositivo = Convert.ToInt32(codigoDispositvo);
                SAS_ListadoDeDispositivosByIdDeviceResult claseDispositivo = new SAS_ListadoDeDispositivosByIdDeviceResult();
                claseDispositivo.tipoDispositivoCodigo = "000";
                claseDispositivo.tipoDispositivoCodigo = string.Empty;
                claseDispositivo = model.ObtenerClaseDispositivo(conection, codigoDispositivo).ElementAt(0);

                txtCodigoTipoDispositivo.Text = claseDispositivo.tipoDispositivoCodigo != null ? claseDispositivo.tipoDispositivoCodigo.ToString() : "000";
                txTipoDispositivoDescripcion.Text = claseDispositivo.tipoDispositivo != null ? claseDispositivo.tipoDispositivo.ToString() : string.Empty;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void txtEstado_DoubleClick(object sender, EventArgs e)
        {
            if (this.txtEstado.Text.ToUpper().Trim() == "ATENDIDO TOTAL")
            {
                ModifyStatusDevice();
            }
        }

        private void ModifyStatusDevice()
        {
            if (this.txtCodigo.Text != "0")
            {
                if (txtCodigo.Text != string.Empty)
                {

                    int codigoSelecionado = selectedItem.codigo;
                    OrdenDeTrabajoITEdicionActualizarEstadoADispositivo oFron = new OrdenDeTrabajoITEdicionActualizarEstadoADispositivo(conection, user2, companyId, privilege, codigoSelecionado);
                    oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                    oFron.WindowState = FormWindowState.Maximized;
                    oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    oFron.Show();

                }
            }
        }

        private void btnGenerarReprogramacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkEsProgramado.Checked == false)
                {
                    GenerarReprogramacion(codigoSelecionado);
                    MessageBox.Show("Operacion registrada correctamente", "MENSAJE DEL SISTEMA");
                    BarraPrincipal.Enabled = false;
                    gbDatosPersonal.Enabled = false;
                    gbDetale.Enabled = false;
                    gbDocumento.Enabled = false;
                    progressBar1.Visible = true;

                    bgwHilo.RunWorkerAsync();

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + " | Generar repogramación de actividad", "MENSAJE DEL SISTEMA");
            }


        }


        private void GenerarReprogramacion(int codigoSelecionado)
        {
            try
            {

                model = new SAS_DispositivoOrdenTrabajoController();
                int codigoDocumento = Convert.ToInt16(txtCodigo.Text != string.Empty ? this.txtCodigo.Text : "0");
                model.CambiarAEstadoReprogramado(conection, codigoDocumento);
            }

            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + " | Generar repogramación de actividad", "MENSAJE DEL SISTEMA");
            }
        }

        private void btnPersonalBuscar_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuarioAsignado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuarioAsignado_DoubleClick(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text != string.Empty)
            {
                if (this.txtCodigo.Text != "0")
                {
                    if (user2 != null)
                    {
                        if (user2.IdUsuario != null)
                        {
                            if (user2.IdUsuario.ToUpper().Trim() == "EAURAZO" || user2.IdUsuario.ToUpper().Trim() == "FCERNA" || user2.IdUsuario.ToUpper().Trim() == "ADMINISTRADOR")
                            {
                                ReasignarCaso();
                            }
                        }
                    }
                }

            }
        }



        private void ReasignarCaso()
        {
            if (this.txtCodigo.Text != null)
            {
                if (this.txtCodigo.Text != null)
                {
                    if (this.txtCodigo.Text != "0")
                    {
                        int codigoSelecionado = Convert.ToInt32(this.txtCodigo.Text);
                        OrdenDeTrabajoITEdicionAsignarSoporte oFron = new OrdenDeTrabajoITEdicionAsignarSoporte(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.ShowDialog();
                        if (oFron.DialogResult == DialogResult.OK)
                        {
                            AperturarFormulario();
                        }


                    }
                }
            }
        }


        private void AperturarFormulario()
        {
            BarraPrincipal.Enabled = false;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            progressBar1.Visible = true;

            bgwHilo.RunWorkerAsync();
        }

        private void bgwNotify_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Notificación enviada satisfactoriamente", "Confirmación del sistema");
                gbDatosPersonal.Enabled = !false;
                gbDetale.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbDatosPersonal.Enabled = !false;
                gbDetale.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
                return;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }
    }
}
