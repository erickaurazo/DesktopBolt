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


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class AtencionesSoporteFuncionalEdicion : Form
    {
        #region Variables()
        string nombreformulario = "AtencionesSoporteFuncional";
        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades;
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
        private SAS_DispositivoSoporteFuncionalController model;
        private List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> listing; //Listado
        private List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalleEliminado = new List<SAS_DispositivoSoporteFuncionalDetalle>();
        private List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
        private SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult selectedItem; // Item Selecionado
        private SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult item;
        private SAS_DispositivoSoporteFuncional ordenTrabajo;
        List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult> listDetalleByCodigoMantenimiento;
        private int ultimoItemEnListaDetalle = 0;
        private int codigoDispositivo;
        private SAS_DispositivoUsuariosController modeloDispositivo;
        private List<SAS_DispositivoTipoSoporteFuncionalDetalle> listadoDetalleByItem;
        private SAS_DispositivoTipoSoporteFuncionalController modelo;
        private List<Grupo> canalesDeAtencion;
        private List<Grupo> tiempoProgramados, tiempoEjecutados;
        #endregion

        public AtencionesSoporteFuncionalEdicion()
        {
            InitializeComponent();
            nombreformulario = "AtencionesSoporteFuncional";
            Inicio();
        }

        public AtencionesSoporteFuncionalEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoSelecionado)
        {
            InitializeComponent();
            nombreformulario = "AtencionesSoporteFuncional";
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSelecionado;
            Inicio();
            CargarCombos();
            // btnGenerarReprogramacion.Enabled = false;
            AperturarFormulario();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            BarraPrincipal.Enabled = false;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            progressBar1.Visible = true;
            this.codigoSelecionado = 0;
            bgwHilo.RunWorkerAsync();
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
                MessageBox.Show("El documento no tiene el estado para registrar una edición", "MENSAJE DEL SISTEMA | Edición de registro");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RecordObject();
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


            if (this.txtSoftwareCodigo.Text.Trim() == string.Empty)
            {
                resultado = false;
                MessageBox.Show("Debe ingresar el código del software para registrar la incidencia de soporte", "Notificación del sistema");
                txtSoftwareCodigo.Focus();
                return resultado;
            }

            if (this.txtObservaciones.Text.Trim() == string.Empty)
            {
                resultado = false;
                MessageBox.Show("Debe ingresar una descripción que describa el un resumen del soporte que se require", "Notificación del sistema");
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

        private SAS_DispositivoSoporteFuncional ObtenerObjeto()
        {
            SAS_DispositivoSoporteFuncional ot = new SAS_DispositivoSoporteFuncional();
            ot.codigo = (this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0);
            ot.codigoPersonal = this.txtPersonalCodigo.Text.Trim();
            ot.idSerie = cboSerie.SelectedValue.ToString();
            ot.iddocumento = item.iddocumento = this.cboDocumento.SelectedValue.ToString();
            ot.fecha = Convert.ToDateTime(this.txtFecha.Text);
            //ot.periodo = item.periodo;
            //ot.idTipoSoporteFuncional = cboMantenimientoTipo.SelectedValue.ToString();
            ot.idTipoSoporteFuncional = this.txtMantenientoCodigo.Text.Trim();

            ot.Observación = this.txtObservaciones.Text.Trim();
            ot.idEstado = this.txtEstadoCodigo.Text;
            ot.idTipoSoftware = this.txtSoftwareCodigo.Text != string.Empty ? Convert.ToInt32(this.txtSoftwareCodigo.Text.Trim()) : 0;
            ot.usuario = user2.IdUsuario.Trim();
            ot.fechaCreacion = DateTime.Now;
            ot.idEmpresa = this.txtEmpresaCodigo.Text.Trim();
            ot.idSucursal = this.txtSucursalCodigo.Text.Trim();
            ot.costoUSD = this.txtCostoTotalDeMantenimiento.Text != string.Empty ? Convert.ToDecimal(this.txtCostoTotalDeMantenimiento.Text.Trim()) : 0;

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

            ot.horasEstimadas = Convert.ToDecimal(cboTiempoEjecutado.SelectedValue);
            ot.minutosProgramados = Convert.ToDecimal(cboTiempoProgramado.SelectedValue);
            ot.CanalDeAtencionCodigo = Convert.ToByte(cboCanalDeAtencion.SelectedValue);


            return ot;
        }

        private void RecordObject()
        {
            btnRegistrar.Enabled = false;
            BarraPrincipal.Enabled = false;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            progressBar1.Visible = true;

            ordenTrabajo = new SAS_DispositivoSoporteFuncional();
            if (ValidacionParaRegistrar() == true)
            {
                ordenTrabajo = ObtenerObjeto();

                // add 30.04.2022
                #region listado de acciones() 
                listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
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
                                    SAS_DispositivoSoporteFuncionalDetalle recordObject = new SAS_DispositivoSoporteFuncionalDetalle();
                                    recordObject.codigo = fila.Cells["chOTCodigoDetalle"].Value != null ? Convert.ToInt32(fila.Cells["chOTCodigoDetalle"].Value.ToString().Trim()) : 0;
                                    recordObject.item = fila.Cells["chItemDetalle"].Value != null ? fila.Cells["chItemDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.accion = fila.Cells["chAccionDetalle"].Value != null ? fila.Cells["chAccionDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.desde = fila.Cells["chDesdeDetalle"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeDetalle"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.hasta = fila.Cells["chHastaDetalle"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaDetalle"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.estado = fila.Cells["chEstadoCodigoDetalle"].Value != null ? Convert.ToByte(fila.Cells["chEstadoCodigoDetalle"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    recordObject.usuario = fila.Cells["chUsuarioDetalle"].Value != null ? fila.Cells["chUsuarioDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.costoUSD = fila.Cells["chValorUSDAccion"].Value != null ? Convert.ToDecimal(fila.Cells["chValorUSDAccion"].Value.ToString().Trim()) : 0;
                                    recordObject.glosa = fila.Cells["chGlosaDetalle"].Value != null ? fila.Cells["chGlosaDetalle"].Value.ToString().Trim() : string.Empty;
                                    recordObject.HorasFormatoReloj = fila.Cells["chHorasFormatoReloj"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasFormatoReloj"].Value.ToString().Trim()) : 0;
                                    recordObject.HorasFormatoPlanilla = fila.Cells["chHorasFormatoPlanilla"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasFormatoPlanilla"].Value.ToString().Trim()) : 0;
                                    #endregion

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

                if (totalDeActivades > 0)
                {
                    #region Actualizacion listado detalle de actividades
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
                }
                else
                {
                    ordenTrabajo.idEstado = "PE";
                }

                #endregion


                model = new SAS_DispositivoSoporteFuncionalController();
                //int resultadoRegistro = model.RegisterObject("SAS", ordenTrabajo);
                int resultadoRegistro = model.RegisterObject("SAS", ordenTrabajo, listadoDetalleEliminado, listadoDetalle);
                MessageBox.Show("Operación realizada con éxito", "Confirmación del sistema");
                this.codigoSelecionado = resultadoRegistro;
                listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
                listadoDetalleEliminado = new List<SAS_DispositivoSoporteFuncionalDetalle>();

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

        private void btnAtras_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

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
                    dgvDetalle.CurrentRow.Cells["chEstadoDetalle"].Value = "ANULADO";
                }
                else
                {
                    dgvDetalle.CurrentRow.Cells["chEstadoCodigoDetalle"].Value = "1";
                    dgvDetalle.CurrentRow.Cells["chEstadoDetalle"].Value = "PENDIENTE";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                item = new SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult();
                item = model.GetListById("SAS", codigoSelecionado);

                listDetalleByCodigoMantenimiento = new List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>();
                listDetalleByCodigoMantenimiento = model.GetListDetalleByCodigoMantenimiento("SAS", this.codigoSelecionado).ToList();
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

        private void LimpiarFormulario()
        {
            model = new SAS_DispositivoSoporteFuncionalController();
            int ultimoRegistro = model.ObtenerUltimoOperacion("SAS");
            ultimoItemEnListaDetalle = 1;
            listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
            listadoDetalleEliminado = new List<SAS_DispositivoSoporteFuncionalDetalle>();

            this.txtCodigo.Text = "0"; // cuando es 0 es nuevo
            this.txtCorrelativo.Text = ultimoRegistro.ToString().PadLeft(7, '0'); // traer el último registrado + 1, que solo se va a mostrar.
            this.txtCostoTotalDeMantenimiento.Text = "0.00";
            this.txtSoftwareCodigo.Text = string.Empty;
            this.txtSoftwareDescripcion.Text = string.Empty;

            this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO SA";
            this.txtEmpresaCodigo.Text = "001";

            this.txtEstado.Text = "PENDIENTE";
            this.txtFecha.Text = DateTime.Now.ToShortDateString();

            this.txtEstadoCodigo.Text = "PE";
            this.txtNroPedido.Text = string.Empty;
            this.txtNroTicket.Text = string.Empty;
            this.txtPersonal.Text = string.Empty;
            this.txtPersonalCodigo.Text = string.Empty;

            this.txtFechaFinalizacion.Text = DateTime.Now.AddDays(1).ToShortDateString();
            this.txtObservaciones.Text = string.Empty;

            this.txtProveedor.Text = string.Empty;
            this.txtProveedorCodigo.Text = string.Empty;
            this.txtSucursal.Text = "SEDE LOGISTICA AGRICOLA";
            this.txtSucursalCodigo.Text = "001";
            this.txtUsuarioAsignado.Text = user2.IdUsuario.Trim().ToUpper();

            //cboMantenimientoTipo.SelectedValue = "000";
            txtMantenientoCodigo.Text = string.Empty;
            txtMantenimientoDescripcion.Text = string.Empty;
            cboPrioridad.SelectedValue = "3";


            this.cboTiempoEjecutado.SelectedValue = "0";
            this.cboTiempoEjecutado.SelectedValue = "0";
            this.cboCanalDeAtencion.SelectedValue = "0";

            btnPersonalBuscar.Enabled = !false;
            btnSoftwareBuscar.Enabled = !false;
            this.txtPersonal.ReadOnly = !true;
            this.txtPersonalCodigo.ReadOnly = !true;
            this.txtSoftwareCodigo.ReadOnly = !true;
            this.txtSoftwareDescripcion.ReadOnly = !true;

            chkEjecutadoPorExterno.Checked = false;
            chkConSupervisionSST.Checked = false;
            chkEsProgramado.Checked = false;
            item = new SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult();
            item.codigo = 0;

            chkCerradoConPrimerContacto.Checked = false;
            chkEsAtencionReprogramada.Checked = false;


        }


        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Finalización del Hilo()           
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
                            dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>());
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
                            this.txtCorrelativo.Text = item.codigo.ToString().PadLeft(7, '0');
                            txtPersonalCodigo.Text = item.codigoPersonal != null ? item.codigoPersonal.ToString() : string.Empty;
                            txtPersonal.Text = item.nombresCompletos != null ? item.nombresCompletos.Trim() : string.Empty;
                            cboSerie.SelectedValue = item.idSerie.ToString();

                            cboDocumento.SelectedValue = item.iddocumento.ToString();
                            txtFecha.Text = item.fecha.ToShortDateString();
                            //cboMantenimientoTipo.SelectedValue = item.idTipoSoporteFuncional.Trim();

                            txtMantenientoCodigo.Text = item.idTipoSoporteFuncional.Trim();
                            txtMantenimientoDescripcion.Text = item.tipoSoporteFuncional != null ? item.tipoSoporteFuncional.Trim() : string.Empty;


                            txtObservaciones.Text = item.Observación.Trim();
                            txtEstadoCodigo.Text = item.idEstado.ToString().Trim();
                            this.txtEstado.Text = item.estado != null ? item.estado.Trim() : string.Empty;
                            txtSoftwareCodigo.Text = item.idTipoSoftware.ToString().Trim();
                            this.txtSoftwareDescripcion.Text = item.tipoSoftware != null ? item.tipoSoftware.Trim().ToUpper() : string.Empty;
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



                            decimal tiempoProgramadoNativo = Convert.ToDecimal((item.minutosProgramados != (decimal?)null ? item.minutosProgramados : 0));
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
                            cboPrioridad.SelectedValue = item.prioridadId.ToString();

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

                            btnPersonalBuscar.Enabled = false;
                            btnSoftwareBuscar.Enabled = false;
                            this.txtPersonal.ReadOnly = true;
                            this.txtPersonalCodigo.ReadOnly = true;
                            this.txtSoftwareCodigo.ReadOnly = true;
                            this.txtSoftwareDescripcion.ReadOnly = true;

                            #endregion

                            chkEsAtencionReprogramada.Checked = false;
                            if (item.EsReprogracion == 1)
                            {

                                chkEsAtencionReprogramada.Checked = true;
                            }

                            chkCerradoConPrimerContacto.Checked = false;
                            if (item.cerradoEnPrimeraAtencion == 1)
                            {
                                chkCerradoConPrimerContacto.Checked = false;
                            }


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

                            btnGenerarReprogramacion.Enabled = true;
                            if (item.idEstado == "RR" || item.idEstado == "CE")
                            {
                                btnGenerarReprogramacion.Enabled = false;
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
                            dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>());
                            dgvDetalle.Refresh();

                            btnRegistrar.Enabled = true;
                            btnEditar.Enabled = false;
                            btnNuevo.Enabled = true;
                        }
                    }
                    else
                    {
                        #region Nuevo() 
                        btnGenerarReprogramacion.Enabled = true;
                        LimpiarFormulario();
                        dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>());
                        dgvDetalle.Refresh();
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
            #endregion
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                item = new SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult();
                item = model.GetListById("SAS", this.codigoSelecionado);

                listDetalleByCodigoMantenimiento = new List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>();
                listDetalleByCodigoMantenimiento = model.GetListDetalleByCodigoMantenimiento("SAS", this.codigoSelecionado).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
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
                            btnSoftwareBuscar.Enabled = false;
                            this.txtPersonal.ReadOnly = true;
                            this.txtPersonalCodigo.ReadOnly = true;
                            this.txtSoftwareCodigo.ReadOnly = true;
                            this.txtSoftwareDescripcion.ReadOnly = true;
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
                            txtPersonal.Text = item.nombresCompletos != null ? item.nombresCompletos.Trim() : string.Empty;
                            cboSerie.SelectedValue = item.idSerie.ToString();
                            cboDocumento.SelectedValue = item.iddocumento.ToString();
                            txtFecha.Text = item.fecha.ToShortDateString();
                            // cboMantenimientoTipo.SelectedValue = item.idTipoSoporteFuncional.Trim();
                            this.txtMantenientoCodigo.Text = item.idTipoSoporteFuncional.Trim();
                            txtObservaciones.Text = item.Observación.Trim();
                            txtEstadoCodigo.Text = item.idEstado.ToString().Trim();
                            this.txtEstado.Text = item.estado != null ? item.estado.Trim() : string.Empty;
                            txtSoftwareCodigo.Text = item.idTipoSoftware.ToString().Trim();
                            this.txtSoftwareDescripcion.Text = item.tipoSoftware != null ? item.tipoSoftware.Trim().ToUpper() : string.Empty;
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



                            decimal tiempoProgramadoNativo = Convert.ToDecimal((item.minutosProgramados != (decimal?)null ? item.minutosProgramados : 0));
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
                        dgvDetalle.CargarDatos(listDetalleByCodigoMantenimiento.ToDataTable<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>());
                        dgvDetalle.Refresh();
                        #region cierre()
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnPersonalBuscar.Enabled = false;
                        btnSoftwareBuscar.Enabled = false;
                        this.txtPersonal.ReadOnly = true;
                        this.txtPersonalCodigo.ReadOnly = true;
                        this.txtSoftwareCodigo.ReadOnly = true;
                        this.txtSoftwareDescripcion.ReadOnly = true;
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
                        btnSoftwareBuscar.Enabled = false;
                        this.txtPersonal.ReadOnly = true;
                        this.txtPersonalCodigo.ReadOnly = true;
                        this.txtSoftwareCodigo.ReadOnly = true;
                        this.txtSoftwareDescripcion.ReadOnly = true;
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
                btnSoftwareBuscar.Enabled = false;
                this.txtPersonal.ReadOnly = true;
                this.txtPersonalCodigo.ReadOnly = true;
                this.txtSoftwareCodigo.ReadOnly = true;
                this.txtSoftwareDescripcion.ReadOnly = true;
                BarraPrincipal.Enabled = true;
                gbDatosPersonal.Enabled = true;
                gbDetale.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
                #endregion
                return;
            }
        }


        private string ObtenerFormatoParaAgregarItemDetalle(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }


        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {
            AddItemAListaDetalle();
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
                    array.Add(Convert.ToDecimal(0)); // 
                    array.Add(this.txtFecha.Text);
                    array.Add(this.txtFecha.Text);
                    //array.Add(DateTime.Now.ToShortDateString()); // chDesdeDetalle
                    //array.Add(DateTime.Now.AddDays(Convert.ToInt32(Math.Round(Convert.ToDecimal(this.txtHorasEstimadas.Value / 8)))).ToShortDateString()); // chHastaDetalle                                        
                    array.Add(1); // chEstadoCodigoDetalle
                    array.Add("PENDIENTE"); // chEstadoDetalle          
                    array.Add((txtUsuarioAsignado.Text.Trim())); // chUsuarioDetalle
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

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                #region 
                comboHelper = new ComboBoxHelper();
                if (((DataGridView)sender).RowCount > 0)
                {
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
                                            search.Text = "Estados del item";
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

                        #region Re-Asignación de usuario ()  
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chUsuarioDetalle")
                        {
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                search.ListaGeneralResultado = comboHelper.GetListUser("SAS");
                                search.Text = "Re-asginar a usuario";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chUsuarioDetalle"].Value = search.ObjetoRetorno.Codigo;
                                    //this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chUsuarioDetalle"].Value = search.ObjetoRetorno.Codigo;
                                }
                            }
                        }
                        #endregion


                        #region Listado de minutos() 
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHorasFormatoPlanilla")
                        {
                            if (e.KeyCode == Keys.F3)
                            {
                                model = new SAS_DispositivoSoporteFuncionalController();
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
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
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
                model = new SAS_DispositivoSoporteFuncionalController();

                model.Notify(conection, "soporte@saturno.net.pe", "Solicitud de atención por soporte funcional", codigoSelecionado);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

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

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {
            RemoveItemDetail();
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

        private void btnCompletarConActividadesDefinidas_Click(object sender, EventArgs e)
        {
            int codigoTipoSoftwareElegido = 0;
            if (txtSoftwareCodigo.Text.Trim() != string.Empty && txtSoftwareDescripcion.Text.Trim() != string.Empty)
            {
                codigoTipoSoftwareElegido = Convert.ToInt32(txtSoftwareCodigo.Text.Trim());
                //if (cboMantenimientoTipo.SelectedValue.ToString() != "000")

                if (this.txtMantenientoCodigo.Text.Trim() != string.Empty)
                {
                    if (this.txtMantenientoCodigo.Text != "000")
                    {
                        try
                        {
                            #region 
                            model = new SAS_DispositivoSoporteFuncionalController();
                            listadoDetalleByItem = new List<SAS_DispositivoTipoSoporteFuncionalDetalle>();
                            modelo = new SAS_DispositivoTipoSoporteFuncionalController();
                            SAS_DispositivoTipoSoporteFuncional oDetalle = new SAS_DispositivoTipoSoporteFuncional();
                            // oDetalle.codigo = cboMantenimientoTipo.SelectedValue.ToString().Trim();
                            oDetalle.codigo = this.txtMantenientoCodigo.Text.ToString().Trim();


                            listadoDetalleByItem = modelo.GetToListDetailByIdByTypeSoftware(conection, oDetalle, codigoTipoSoftwareElegido).ToList();
                            if (listadoDetalleByItem != null)
                            {
                                if (listadoDetalleByItem.ToList().Count > 0)
                                {
                                    foreach (var item in listadoDetalleByItem)
                                    {
                                        if (dgvDetalle != null)
                                        {
                                            ArrayList array = new ArrayList();
                                            array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chOTCodigoDetalle                  
                                            array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalle))); // chItemDetalle
                                            array.Add(item.descripcion); // chAccionDetalle
                                            array.Add(Convert.ToDecimal(0)); // chHorasReloj
                                            array.Add(item.minutos.Value.ToDecimalPresentation()); // chHorasPlanilla
                                                                                                   //array.Add(DateTime.Now.ToShortDateString()); // chDesdeDetalle
                                                                                                   //array.Add(DateTime.Now.AddDays(Convert.ToInt32(Math.Round(Convert.ToDecimal(this.txtHorasEstimadas.Value / 8)))).ToShortDateString()); // chHastaDetalle                                        
                                            array.Add(this.txtFecha.Text);
                                            array.Add(this.txtFecha.Text);
                                            array.Add(1); // chEstadoCodigoDetalle
                                            array.Add("PENDIENTE"); // chEstadoDetalle          
                                            array.Add((txtUsuarioAsignado.Text.Trim())); // chUsuarioDetalle
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
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Formateador.ControlExcepcion(this, this.Name, ex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Dele elegir una opcion de tipo de soporte válido", "Mensaje del sistema");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe selecionar un tipo de software o aplicación para poder autocompletar las acciones", "Mensaje del sistema");
            }


        }

        private void btnGenerarReprogramacion_Click(object sender, EventArgs e)
        {
            //if (chkEsProgramado.Checked == true)
            //{
            //    GenerarReprogramacion(codigoSelecionado);
            //}
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
                model = new SAS_DispositivoSoporteFuncionalController();
                int codigoDocumento = Convert.ToInt16(txtCodigo.Text != string.Empty ? this.txtCodigo.Text : "0");
                model.CambiarAEstadoReprogramado(conection, codigoDocumento);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + " | Generar repogramación de actividad", "MENSAJE DEL SISTEMA");
            }
        }

        private void tabDetalle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsuarioAsignado_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUSD_Click(object sender, EventArgs e)
        {

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
                        AtencionesSoporteFuncionalEdicionReasignarCaso oFron = new AtencionesSoporteFuncionalEdicionReasignarCaso(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.ShowDialog();
                        if (oFron.DialogResult == DialogResult.OK)
                        {
                            AperturarFormulario();
                        }


                    }
                }
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

                                listadoDetalleEliminado.Add(new SAS_DispositivoSoporteFuncionalDetalle
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

        private void AtencionesSoporteFuncionalEdicion_Load(object sender, EventArgs e)
        {

        }
    }
}
