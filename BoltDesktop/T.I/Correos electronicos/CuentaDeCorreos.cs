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

namespace ComparativoHorasVisualSATNISIRA.T.I
{

    public partial class CuentaDeCorreos : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS userLogin;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_CuentasCorreoListado> ListarCuentasDeCorreoAll;
        private SAS_CuentasCorreoController Modelo;
        private SAS_CuentasCorreo oCuentaDeCorreo;
        private SAS_CuentasCorreoListado oCuentaDeCorreoSeleccionado;
        private List<SAS_CuentasCorreoDetalleByIdResult> ListadoCuentasDeCorreoDetalleLog;
        private int lastItem;
        private string msgError;
        private List<SAS_CuentasCorreoDetalle> ListadoDetalleLogEliminar = new List<SAS_CuentasCorreoDetalle>();
        private List<SAS_CuentasCorreoDetalle> ListadoDetalleLogRegistrar = new List<SAS_CuentasCorreoDetalle>();

        private List<SAS_CuentasCorreoAsignacionPersonal> ListadoDetalleAsignacionDeCuentaEliminar = new List<SAS_CuentasCorreoAsignacionPersonal>();
        private List<SAS_CuentasCorreoAsignacionPersonal> ListadoDetalleAsignacionDeCuentaRegistrar = new List<SAS_CuentasCorreoAsignacionPersonal>();
        private List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult> ListadoDetalleAsignacionDeCuentas = new List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>();


        private List<SAS_CuentasCorreosHistoricoPlan> ListadoDetalleHistoricoPlanEliminar = new List<SAS_CuentasCorreosHistoricoPlan>();
        private List<SAS_CuentasCorreosHistoricoPlan> ListadoDetalleHistoricoPlanRegistrar = new List<SAS_CuentasCorreosHistoricoPlan>();
        private List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult> ListadoDetallHistoricoPlanByCuentaCorreo = new List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>();


        object result;
        private int EstadoCambio = 0;
        private int CodigoRegistro;
        private int ClickFiltro = 0;

        public CuentaDeCorreos()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            try
            {
                Inicio();
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
            //catch (TargetInvocationException ex)
            //{
            //    result = ex.InnerException.Message;
            //}
            //catch (Exception ex)
            //{
            //    result = ex.Message;
            //}
        }

        public CuentaDeCorreos(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            try
            {
                InitializeComponent();
                Inicio();
                RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
                RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
                RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
                RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

                conection = _conection;
                userLogin = _user2;
                companyId = _companyId;
                privilege = _privilege;
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        //public object Convert2(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    double parsed = 0;
        //    if (!double.TryParse(string.Empty, out parsed))
        //        return parsed;

        //    return (parsed) ;
        //}
        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    double parsed = 0;
        //    if (!double.TryParse(string.Empty, out parsed))
        //        return parsed;

        //    return System.Convert.ToInt32(parsed) ;
        //}


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
                this.dgvRegistro.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvRegistro.TableElement.EndUpdate();

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
                this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
                this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvRegistro.GroupDescriptors.Clear();
                this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chcuenta", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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
                FilterDescriptor wrongFilter = this.dgvRegistro.Columns["chcuenta"].FilterDescriptor;
                double correctValue = 12.5;
                FilterDescriptor filterDescriptor =
                    new FilterDescriptor(wrongFilter.PropertyName, wrongFilter.Operator, correctValue);
                filterDescriptor.IsFilterEditor = wrongFilter.IsFilterEditor;

                this.dgvRegistro.FilterDescriptors.Remove(wrongFilter);
                this.dgvRegistro.FilterDescriptors.Add(filterDescriptor);

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
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


        private void Actualizar()
        {
            try
            {
                BloquearControlesConsulta();
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void BloquearControlesConsulta()
        {
            btnMenu.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            progressBar1.Visible = true;
        }



        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnGrabar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }



        private void ObtenerObjeto()
        {

            try
            {
                oCuentaDeCorreo = new SAS_CuentasCorreo();
                oCuentaDeCorreo.id = Convert.ToInt32(this.txtCodigo.Text);
                oCuentaDeCorreo.cuenta = this.txtCuenta.Text.Trim();
                oCuentaDeCorreo.idcodigoGeneral = this.txtIdCodigoGeneral.Text.Trim();
                oCuentaDeCorreo.vienesDesdeSolicitud = chkActivoEnReporte.Checked == true ? 1 : 0;
                oCuentaDeCorreo.estado = this.txtIdEstado.Text.ToString().Trim() == "1" ? 1 : 0;
                oCuentaDeCorreo.codigoSolicitud = this.txtCodigoSolicitud.Text != string.Empty ? Convert.ToInt32(this.txtCodigoSolicitud.Text) : 0;
                oCuentaDeCorreo.observaciones = this.txtObservaciones.Text.Trim();
                oCuentaDeCorreo.fechaActivacion = DateTime.Now;
                oCuentaDeCorreo.fechaBaja = DateTime.Now;
                oCuentaDeCorreo.esCorportativo = chkEsCuentaCorporativa.Checked == true ? 1 : 0;
                oCuentaDeCorreo.clave = this.txtclave.Text.Trim();
                oCuentaDeCorreo.nombres = this.txtNombres.Text.Trim();
                oCuentaDeCorreo.idLicencia = this.txtLicenciaCodigo.Text.Trim() != null ? Convert.ToInt32(this.txtLicenciaCodigo.Text) : (int?)null;

                #region Obtener detalle Log()
                ListadoDetalleLogRegistrar = new List<SAS_CuentasCorreoDetalle>();
                if (this.dgvDetalleLog != null)
                {
                    if (this.dgvDetalleLog.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalleLog.Rows)
                        {
                            if (fila.Cells["chid"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_CuentasCorreoDetalle oAcountDetail = new SAS_CuentasCorreoDetalle();
                                    oAcountDetail.id = fila.Cells["chid"].Value != null ? Convert.ToInt32(fila.Cells["chId"].Value.ToString().Trim()) : 0;
                                    oAcountDetail.item = fila.Cells["chItem"].Value != null ? fila.Cells["chItem"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.idTipo = fila.Cells["chidTipo"].Value != null ? Convert.ToByte(fila.Cells["chidTipo"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oAcountDetail.link = fila.Cells["chlink"].Value != null ? fila.Cells["chlink"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.descripcion = fila.Cells["chdescripcion"].Value != null ? fila.Cells["chdescripcion"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.estado = fila.Cells["chestado"].Value != null ? Convert.ToInt32(fila.Cells["chestado"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oAcountDetail.FechaRegistro = DateTime.Now;
                                    oAcountDetail.UserID = userLogin.IdUsuario != null ? userLogin.IdUsuario : Environment.UserName;
                                    oAcountDetail.creadoPor = Environment.UserName;
                                    oAcountDetail.Hostname = Environment.MachineName;
                                    ListadoDetalleLogRegistrar.Add(oAcountDetail);
                                    #endregion
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


                #endregion

                #region Obtener detalle Asignacion a Colaborador()
                ListadoDetalleAsignacionDeCuentaRegistrar = new List<SAS_CuentasCorreoAsignacionPersonal>();
                if (this.dgvDetalleAsignacionesAPersonal != null)
                {
                    if (this.dgvDetalleAsignacionesAPersonal.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalleAsignacionesAPersonal.Rows)
                        {
                            if (fila.Cells["chCuentaCorreoAsignacionID"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_CuentasCorreoAsignacionPersonal oPersonalAsignado = new SAS_CuentasCorreoAsignacionPersonal();
                                    oPersonalAsignado.CuentaCorreoAsignacionId = fila.Cells["chCuentaCorreoAsignacionID"].Value != null ? Convert.ToInt32(fila.Cells["chCuentaCorreoAsignacionID"].Value.ToString().Trim()) : 0;
                                    oPersonalAsignado.CuentaCorreoID = fila.Cells["chCuentaCorreoID"].Value != null ? Convert.ToInt32(fila.Cells["chCuentaCorreoID"].Value.ToString().Trim()) : 0;
                                    oPersonalAsignado.PersonalID = fila.Cells["chPersonalID"].Value != null ? fila.Cells["chPersonalID"].Value.ToString().Trim() : string.Empty;
                                    oPersonalAsignado.Desde = (fila.Cells["chDesde"].Value != null && fila.Cells["chDesde"].Value.ToString().Trim() != "" && fila.Cells["chDesde"].Value.ToString().Trim() != string.Empty) ? Convert.ToDateTime(fila.Cells["chDesde"].Value.ToString().Trim()) : DateTime.Now;
                                    oPersonalAsignado.Hasta = (fila.Cells["chHasta"].Value != null && fila.Cells["chHasta"].Value.ToString().Trim() != "" && fila.Cells["chHasta"].Value.ToString().Trim() != string.Empty) ? Convert.ToDateTime(fila.Cells["chHasta"].Value.ToString().Trim()) : (DateTime?)null;                                    
                                    oPersonalAsignado.Nota = fila.Cells["chNota"].Value != null ? fila.Cells["chNota"].Value.ToString().Trim() : string.Empty;
                                    oPersonalAsignado.Estado = fila.Cells["chEstadoDetalleCuentaCorreo"].Value != null ? Convert.ToInt32(fila.Cells["chEstadoDetalleCuentaCorreo"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oPersonalAsignado.ReferenciaSolicitudID = fila.Cells["chReferenciaSolicitudID"].Value != null ? Convert.ToInt32(fila.Cells["chReferenciaSolicitudID"].Value.ToString().Trim()) : 0;
                                    oPersonalAsignado.ReferenciaID = fila.Cells["chReferenciaID"].Value != null ? Convert.ToInt32(fila.Cells["chReferenciaID"].Value.ToString().Trim()) : 0;
                                    oPersonalAsignado.TablaReferencia = string.Empty;
                                    oPersonalAsignado.TablaSolicitud = string.Empty;                                
                                    oPersonalAsignado.UserID = userLogin.IdUsuario != null ? userLogin.IdUsuario : Environment.UserName;
                                    oPersonalAsignado.HostName = Environment.MachineName.Trim();
                                    oPersonalAsignado.FechaRegistro = DateTime.Now;
                                    ListadoDetalleAsignacionDeCuentaRegistrar.Add(oPersonalAsignado);
                                    #endregion
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


                #endregion
                
                #region Obtener detalle HistoricoPlan()
                ListadoDetalleHistoricoPlanRegistrar = new List<SAS_CuentasCorreosHistoricoPlan>();
                if (this.dgvHistoricoPlanes != null)
                {
                    if (this.dgvHistoricoPlanes.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvHistoricoPlanes.Rows)
                        {
                            if (fila.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_CuentasCorreosHistoricoPlan oPlanHistorico = new SAS_CuentasCorreosHistoricoPlan();
                                    oPlanHistorico.CuentaCorreoTipoLicenciaId = fila.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value.ToString().Trim()) : 0;
                                    oPlanHistorico.CuentaCorreoID = fila.Cells["chCuentaCorreoIDHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chCuentaCorreoIDHistoricoPlan"].Value.ToString().Trim()) : 0;
                                    oPlanHistorico.LicenciaTipoId = fila.Cells["chLicenciaTipoIdHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chLicenciaTipoIdHistoricoPlan"].Value.ToString().Trim()) : 0;
                                    oPlanHistorico.Desde = (fila.Cells["chDesdeHistoricoPlan"].Value != null && fila.Cells["chDesdeHistoricoPlan"].Value.ToString().Trim() != "" && fila.Cells["chDesdeHistoricoPlan"].Value.ToString().Trim() != string.Empty) ? Convert.ToDateTime(fila.Cells["chDesdeHistoricoPlan"].Value.ToString().Trim()) : DateTime.Now;
                                    oPlanHistorico.Hasta = (fila.Cells["chHastaHistoricoPlan"].Value != null && fila.Cells["chHastaHistoricoPlan"].Value.ToString().Trim() != "" && fila.Cells["chHastaHistoricoPlan"].Value.ToString().Trim() != string.Empty) ? Convert.ToDateTime(fila.Cells["chHastaHistoricoPlan"].Value.ToString().Trim()) : (DateTime?)null;
                                    oPlanHistorico.Nota = fila.Cells["chNotaHistoricoPlan"].Value != null ? fila.Cells["chNotaHistoricoPlan"].Value.ToString().Trim() : string.Empty;
                                    oPlanHistorico.Estado = fila.Cells["chEstadoHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chEstadoHistoricoPlan"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oPlanHistorico.ReferenciaSolicitudID = fila.Cells["chReferenciaIDHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chReferenciaIDHistoricoPlan"].Value.ToString().Trim()) : 0;
                                    oPlanHistorico.ReferenciaID = fila.Cells["chReferenciaSolicitudIDHistoricoPlan"].Value != null ? Convert.ToInt32(fila.Cells["chReferenciaSolicitudIDHistoricoPlan"].Value.ToString().Trim()) : 0;
                                    oPlanHistorico.TablaReferencia = fila.Cells["chTablaReferenciaHistoricoPlan"].Value != null ? fila.Cells["chTablaReferenciaHistoricoPlan"].Value.ToString().Trim() : string.Empty;
                                    oPlanHistorico.TablaSolicitud = fila.Cells["chTablaSolicitudHistoricoPlan"].Value != null ? fila.Cells["chTablaSolicitudHistoricoPlan"].Value.ToString().Trim() : string.Empty;
                                    oPlanHistorico.UserID = userLogin.IdUsuario != null ? userLogin.IdUsuario : Environment.UserName;
                                    oPlanHistorico.HostName = Environment.MachineName;
                                    oPlanHistorico.FechaRegistro = DateTime.Now;
                                    ListadoDetalleHistoricoPlanRegistrar.Add(oPlanHistorico);
                                    #endregion
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


                #endregion

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void CambiarEstado()
        {
            try
            {

                ObtenerObjeto();
                #region Cambiar Estado
                if (oCuentaDeCorreo.cuenta != string.Empty)
                {
                    Modelo = new SAS_CuentasCorreoController();
                    int resultado = 0;
                    resultado = Modelo.ChangeState("SAS", oCuentaDeCorreo);
                    if (resultado == 2)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de anulación");
                        Actualizar();
                    }
                    else if (resultado == 3)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Activación");
                        Actualizar();
                    }
                    btnGrabar.Enabled = false;
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnEditar.Enabled = true;
                    btnCancelar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtCuenta.Focus();
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            if (userLogin != null)
            {
                if (userLogin.IdUsuario != null)
                {
                    if (userLogin.IdUsuario != string.Empty)
                    {
                        if (userLogin.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || userLogin.IdUsuario.Trim().ToUpper() == "EAURAZO")
                        {
                            int ResultadoProceso = Modelo.Eliminar(conection, CodigoRegistro);
                            Actualizar();
                        }
                    }
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvRegistro != null)
            {
                if (dgvRegistro.Rows.Count > 0)
                {
                    Exportar(dgvRegistro);
                }

                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void CuentaDeCorreos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AsingarObjeto(SAS_CuentasCorreoListado oDetalle)
        {
            try
            {
                Modelo = new SAS_CuentasCorreoController();
                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : string.Empty;
                this.txtIdCodigoGeneral.Text = oDetalle.idcodigoGeneral != null ? oDetalle.idcodigoGeneral.Trim() : string.Empty;
                this.txtNombres.Text = oDetalle.nombres != null ? oDetalle.nombres.Trim() : string.Empty;
                this.txtCuenta.Text = oDetalle.cuenta != null ? oDetalle.cuenta.Trim() : string.Empty;
                this.txtclave.Text = oDetalle.clave != null ? oDetalle.clave.Trim() : string.Empty;
                this.txtObservaciones.Text = oDetalle.observaciones != null ? oDetalle.observaciones.Trim() : string.Empty;
                //this.txtEstado.Text = (oDetalle.estado.Value != null ? Convert.ToInt32(oDetalle.estado.Value) : 1) == 1 ? "ACTIVO" : "ANULADO";
                this.txtEstado.Text = oDetalle.estadoDescripcion != null ? oDetalle.estadoDescripcion.ToString().Trim() : string.Empty;
                this.txtCodigoSolicitud.Text = oDetalle.codigoSolicitud != null ? oDetalle.codigoSolicitud.Value.ToString() : string.Empty;

                this.txtLicenciaCodigo.Text = oDetalle.idLicencia != null ? oDetalle.idLicencia.ToString() : string.Empty;
                this.txtLicenciaDescripcion.Text = oDetalle.descripcion != null ? oDetalle.descripcion.ToString() : string.Empty;

                if (oDetalle.esCorportativo == 1)
                {
                    chkEsCuentaCorporativa.Checked = true;
                }
                else
                {
                    chkEsCuentaCorporativa.Checked = !true;
                }

                if (oDetalle.vienesDesdeSolicitud == 1)
                {
                    chkActivoEnReporte.Checked = true;
                }
                else
                {
                    chkActivoEnReporte.Checked = !true;
                }

                this.txtFechaDeActivación.Text = oDetalle.fechaActivacion != (DateTime?)null ? oDetalle.fechaActivacion.ToPresentationDate() : string.Empty;
                this.txtFechaBaja.Text = oDetalle.fechaBaja != (DateTime?)null ? oDetalle.fechaBaja.ToPresentationDate() : string.Empty;




                ListadoCuentasDeCorreoDetalleLog = new List<SAS_CuentasCorreoDetalleByIdResult>();
                ListadoDetalleAsignacionDeCuentas = new List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>();
                ListadoDetallHistoricoPlanByCuentaCorreo = new List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>();

                SAS_CuentasCorreo account = new SAS_CuentasCorreo();
                account.id = oDetalle.id;
                ListadoCuentasDeCorreoDetalleLog = Modelo.GetEmailAccountsDetailById("SAS", account); // Obtener listado detalle
                ListadoDetalleAsignacionDeCuentas = Modelo.ObtenerListadoDePersonalAsignacionPorCodigoCorreoElectronico(conection, account.id);
                ListadoDetallHistoricoPlanByCuentaCorreo = Modelo.ObtenerListadoDePlanHistoricoPorCodigoCorreoElectronico(conection, account.id);

                lastItem = 0;

                if (ListadoCuentasDeCorreoDetalleLog != null)
                {
                    if (ListadoCuentasDeCorreoDetalleLog.Count > 0)
                    {
                        lastItem = Convert.ToInt32(ListadoCuentasDeCorreoDetalleLog.Max(X => X.item) + 1);
                    }
                }

                dgvDetalleLog.CargarDatos(ListadoCuentasDeCorreoDetalleLog.ToDataTable<SAS_CuentasCorreoDetalleByIdResult>());
                dgvDetalleLog.Refresh();
                msgError += "IP OK GRILLA LOG ";


                dgvDetalleAsignacionesAPersonal.CargarDatos(ListadoDetalleAsignacionDeCuentas.ToDataTable<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>());
                dgvDetalleAsignacionesAPersonal.Refresh();
                msgError += "IP OK GRILLA ASIGNACION PERSONAL";


                dgvHistoricoPlanes.CargarDatos(ListadoDetallHistoricoPlanByCuentaCorreo.ToDataTable<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>());
                dgvHistoricoPlanes.Refresh();
                msgError += "IP OK GRILLA Historico Planes";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString() + msgError, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        //[STAThread]
        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            CodigoRegistro = 0;
            //Thread t = new Thread((ThreadStart)(() =>
            //{
            try
            {
                #region 
                oCuentaDeCorreoSeleccionado = new SAS_CuentasCorreoListado();
                ListadoDetalleAsignacionDeCuentas = new List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>();
                ListadoDetallHistoricoPlanByCuentaCorreo = new List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>();


                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? (int)Convert.ChangeType(dgvRegistro.CurrentRow.Cells["chId"].Value, typeof(Int32)) : 0);
                                CodigoRegistro = codigo;
                                var resultado = ListarCuentasDeCorreoAll.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count >= 1)
                                {
                                    oCuentaDeCorreoSeleccionado = resultado.ElementAt(0);
                                    oCuentaDeCorreoSeleccionado.id = codigo;
                                    AsingarObjeto(oCuentaDeCorreoSeleccionado);
                                }
                                else
                                {
                                    Limpiar();
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
            //    throw;
            //}
            catch (TargetInvocationException ex)
            {
                result = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            //}));
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            //t.Join();

        }

        private void CuentaDeCorreos_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        //[STAThread]
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


        private void Nuevo()
        {
            try
            {
                oCuentaDeCorreo = new SAS_CuentasCorreo();
                oCuentaDeCorreo.id = 0;
                oCuentaDeCorreo.cuenta = string.Empty;
                oCuentaDeCorreo.idcodigoGeneral = string.Empty;
                oCuentaDeCorreo.vienesDesdeSolicitud = 0;
                oCuentaDeCorreo.estado = 1;
                oCuentaDeCorreo.codigoSolicitud = 0;
                oCuentaDeCorreo.observaciones = string.Empty;
                oCuentaDeCorreo.fechaActivacion = DateTime.Now;
                oCuentaDeCorreo.fechaBaja = DateTime.Now;
                oCuentaDeCorreo.esCorportativo = 0;
                oCuentaDeCorreo.clave = string.Empty;
                oCuentaDeCorreo.nombres = string.Empty;
                Limpiar();
                Editar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void Cancelar()
        {

            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnGrabar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            #region Limpiar() 
            try
            {
                oCuentaDeCorreo = new SAS_CuentasCorreo();
                oCuentaDeCorreo.id = 0;
                oCuentaDeCorreo.cuenta = string.Empty;
                oCuentaDeCorreo.idcodigoGeneral = string.Empty;
                oCuentaDeCorreo.vienesDesdeSolicitud = 0;
                oCuentaDeCorreo.estado = 1;
                oCuentaDeCorreo.codigoSolicitud = 0;
                oCuentaDeCorreo.observaciones = string.Empty;
                oCuentaDeCorreo.fechaActivacion = DateTime.Now;
                oCuentaDeCorreo.fechaBaja = DateTime.Now;
                oCuentaDeCorreo.esCorportativo = 0;
                oCuentaDeCorreo.clave = string.Empty;
                oCuentaDeCorreo.nombres = string.Empty;

                oCuentaDeCorreoSeleccionado = new SAS_CuentasCorreoListado();
                oCuentaDeCorreoSeleccionado.id = 0;
                oCuentaDeCorreoSeleccionado.cuenta = string.Empty;
                oCuentaDeCorreoSeleccionado.idcodigoGeneral = string.Empty;
                oCuentaDeCorreoSeleccionado.vienesDesdeSolicitud = 0;
                oCuentaDeCorreoSeleccionado.estado = 1;
                oCuentaDeCorreoSeleccionado.codigoSolicitud = 0;
                oCuentaDeCorreoSeleccionado.observaciones = string.Empty;
                oCuentaDeCorreoSeleccionado.fechaActivacion = DateTime.Now;
                oCuentaDeCorreoSeleccionado.fechaBaja = DateTime.Now;
                oCuentaDeCorreoSeleccionado.esCorportativo = 0;
                oCuentaDeCorreoSeleccionado.clave = string.Empty;
                oCuentaDeCorreoSeleccionado.nombres = string.Empty;
                oCuentaDeCorreoSeleccionado.idLicencia = 0;
                oCuentaDeCorreoSeleccionado.descripcion = string.Empty;

                this.txtCodigo.Text = "0";
                this.txtEstado.Text = "ACTIVO";
                this.txtIdEstado.Text = "1";
                this.txtCuenta.Text = string.Empty;
                this.txtIdCodigoGeneral.Text = string.Empty;
                this.txtNombres.Text = string.Empty;
                this.txtclave.Text = string.Empty;
                this.txtObservaciones.Text = string.Empty;
                this.chkActivoEnReporte.Checked = false;
                this.chkEsCuentaCorporativa.Checked = false;
                this.txtCodigoSolicitud.Text = string.Empty;
                this.txtLicenciaCodigo.Text = string.Empty;
                this.txtLicenciaDescripcion.Text = string.Empty;
                this.txtFechaDeActivación.Text = string.Empty;
                this.txtFechaBaja.Text = string.Empty;

                ClearGridDetail();
                ListadoDetalleLogRegistrar = new List<SAS_CuentasCorreoDetalle>();
                ListadoDetalleLogEliminar = new List<SAS_CuentasCorreoDetalle>();
                lastItem = 0;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
            #endregion

        }

        // [STAThread]
        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            //Thread t = new Thread((ThreadStart)(() =>
            //{
            try
            {
                ListarCuentasDeCorreoAll = new List<SAS_CuentasCorreoListado>();
                Modelo = new SAS_CuentasCorreoController();
                ListarCuentasDeCorreoAll = Modelo.GetEmailAccounts("SAS");




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

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultadoConsulta();
        }

        private void MostrarResultadoConsulta()
        {
            try
            {
                dgvRegistro.DataSource = ListarCuentasDeCorreoAll.OrderBy(x => x.cuenta).ToList().ToDataTable<SAS_CuentasCorreoListado>();
                dgvRegistro.Refresh();

                btnMenu.Enabled = true;
                gbEdit.Enabled = true;
                gbList.Enabled = true;
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }


        private void Grabar()
        {
            try
            {
                ObtenerObjeto();
                Modelo = new SAS_CuentasCorreoController();
                int resultado = Modelo.Register("SAS", oCuentaDeCorreo, ListadoDetalleLogEliminar, ListadoDetalleLogRegistrar, ListadoDetalleHistoricoPlanEliminar, ListadoDetalleHistoricoPlanRegistrar, ListadoDetalleAsignacionDeCuentaEliminar, ListadoDetalleAsignacionDeCuentaRegistrar);
                ListadoDetalleAsignacionDeCuentaEliminar = new List<SAS_CuentasCorreoAsignacionPersonal>();
                ListadoDetalleAsignacionDeCuentaRegistrar = new List<SAS_CuentasCorreoAsignacionPersonal>();
                ListadoDetalleLogEliminar = new List<SAS_CuentasCorreoDetalle>();
                ListadoDetalleLogRegistrar = new List<SAS_CuentasCorreoDetalle>();
                ListadoDetalleAsignacionDeCuentas = new List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>();
                oCuentaDeCorreo = new SAS_CuentasCorreo();

                ListadoDetalleHistoricoPlanRegistrar = new List<SAS_CuentasCorreosHistoricoPlan>();
                ListadoDetalleHistoricoPlanEliminar = new List<SAS_CuentasCorreosHistoricoPlan>();
                ListadoDetallHistoricoPlanByCuentaCorreo = new List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>();

                btnGrabar.Enabled = !false;
                btnCancelar.Enabled = !false;
                if (resultado == 0)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se registró satisfactoriamente", "Confirmación del sistema");
                }
                else if (resultado == 1)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se actualizó satisfactoriamente", "Confirmación del sistema");
                }
                Actualizar();
                btnGrabar.Enabled = false;
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = true;
                                
                lastItem = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnAgregarDetalleActivarIP_Click(object sender, EventArgs e)
        {
            ChangeStateDetailLog();
        }

        private void ChangeStateDetailLog()
        {
            try
            {

                if (dgvDetalleLog.CurrentRow.Cells["chestado"].Value.ToString() == "1")
                {
                    dgvDetalleLog.CurrentRow.Cells["chestado"].Value = "0";
                    dgvDetalleLog.CurrentRow.Cells["chestadoDescripcion"].Value = "ANULADO";
                }
                else
                {
                    dgvDetalleLog.CurrentRow.Cells["chestado"].Value = "1";
                    dgvDetalleLog.CurrentRow.Cells["chestadoDescripcion"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AdditemDetalle();
        }

        private string ObtenerItemDetalle(int numeroRegistros)
        {
            #region Get item for grid detail() 
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }


        private void AdditemDetalle()
        {
            try
            {
                #region add Item()
                if (dgvDetalleLog != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // id                 
                    array.Add((ObtenerItemDetalle(lastItem))); // item
                    array.Add(1); // idTipo
                    array.Add("BackUp"); // tipocuenta         
                    array.Add(string.Empty); // link                                                     
                    array.Add(string.Empty); // descripcion
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado                              
                    dgvDetalleLog.AgregarFila(array);
                    lastItem += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            Deleteitem();
        }

        private void Deleteitem()
        {
            try
            {
                if (this.dgvDetalleLog != null)
                {
                    #region delete item() 
                    if (dgvDetalleLog.CurrentRow != null && dgvDetalleLog.CurrentRow.Cells["chId"].Value != null)
                    {
                        //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        try
                        {

                            Int32 dispositivoCodigo = (dgvDetalleLog.CurrentRow.Cells["chId"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalleLog.CurrentRow.Cells["chId"].Value) : 0);
                            if (dispositivoCodigo != 0)
                            {
                                string itemIP = ((dgvDetalleLog.CurrentRow.Cells["chItem"].Value != null | dgvDetalleLog.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleLog.CurrentRow.Cells["chItem"].Value.ToString()) : string.Empty);
                                if (dispositivoCodigo != 0 && itemIP != string.Empty)
                                {

                                    ListadoDetalleLogEliminar.Add(new SAS_CuentasCorreoDetalle
                                    {
                                        id = dispositivoCodigo,
                                        item = itemIP,
                                    });
                                }
                            }

                            dgvDetalleLog.Rows.Remove(dgvDetalleLog.CurrentRow);
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void ClearGridDetail()
        {
            try
            {
                if (this.dgvDetalleLog != null)
                {
                    if (this.dgvDetalleLog.Rows.Count > 0)
                    {
                        int tope = dgvDetalleLog.Rows.Count;
                        for (int i = 0; i < tope; i++)
                        {
                            dgvDetalleLog.Rows.RemoveAt(0);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void dgvDetail_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo = new SAS_CuentasCorreoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de detalle() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chtipo")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = Modelo.GetType("SAS");
                        search.Text = "Buscar tipo de característica";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDetalleLog.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTipo"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetalleLog.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chtipo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {
            AsociarAreaDeTrabajo();
        }


        private void AsociarAreaDeTrabajo()
        {

            if (oCuentaDeCorreoSeleccionado.idcodigoGeneral != string.Empty)
            {
                ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(conection, userLogin, companyId, privilege, oCuentaDeCorreoSeleccionado);
                ofrm.Show();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();

            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnVerDatosDelColaborador_Click(object sender, EventArgs e)
        {
            VerDatosDelColaborador();
        }

        private void VerDatosDelColaborador()
        {

        }

        private void btnEnProcesoDeCopiaDeSeguridad_Click(object sender, EventArgs e)
        {
            EnProcesoDeCopiaDeSeguridad();
        }

        private void EnProcesoDeCopiaDeSeguridad()
        {
            try
            {
                EstadoCambio = 7;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }

        private void btnEnProcesosDeSuspencion_Click(object sender, EventArgs e)
        {
            EnProcesosDeSuspencion();
        }

        private void EnProcesosDeSuspencion()
        {
            try
            {
                EstadoCambio = 8;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnSuspendido_Click(object sender, EventArgs e)
        {
            SuspenderCuenta();
        }

        private void SuspenderCuenta()
        {
            try
            {
                EstadoCambio = 2;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnEnProcesoDeBaja_Click(object sender, EventArgs e)
        {
            GenerarProcesoDeBaja();
        }

        private void GenerarProcesoDeBaja()
        {
            try
            {
                EstadoCambio = 9;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnBajarCuenta_Click(object sender, EventArgs e)
        {
            BajarCuenta();
        }

        private void BajarCuenta()
        {
            try
            {
                EstadoCambio = 3;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnActivarCuenta_Click(object sender, EventArgs e)
        {
            ActivarCuenta();
        }

        private void ActivarCuenta()
        {
            try
            {
                EstadoCambio = 1;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnSinLicenciaCorporativa_Click(object sender, EventArgs e)
        {
            SinLicenciaCorporativa();
        }

        private void SinLicenciaCorporativa()
        {
            try
            {
                EstadoCambio = 5;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnAnularCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                EstadoCambio = 0;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwCambioEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                oCuentaDeCorreo = new SAS_CuentasCorreo();
                oCuentaDeCorreo.id = Convert.ToInt32(this.txtCodigo.Text);
                Modelo = new SAS_CuentasCorreoController();
                int resultado = 0;
                resultado = Modelo.ChangeState("SAS", oCuentaDeCorreo, EstadoCambio);
                ListarCuentasDeCorreoAll = new List<SAS_CuentasCorreoListado>();
                Modelo = new SAS_CuentasCorreoController();
                ListarCuentasDeCorreoAll = Modelo.GetEmailAccounts("SAS");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwCambioEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultadoConsulta();
        }

        private void btnReasginado_Click(object sender, EventArgs e)
        {
            Reasginado();
        }

        private void Reasginado()
        {
            try
            {
                EstadoCambio = 4;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnLiberacionDeCuentaM365_Click(object sender, EventArgs e)
        {
            LiberacionDeCuentaM365();
        }

        private void LiberacionDeCuentaM365()
        {
            try
            {
                EstadoCambio = 6;
                BloquearControlesConsulta();
                bgwCambioEstado.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();

        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                dgvRegistro.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistro.EnableFiltering = true;
                dgvRegistro.ShowHeaderCellButtons = true;
                #endregion
            }
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {

        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void btnExportarPersonalAsignado_Click(object sender, EventArgs e)
        {

        }

        private void AddItemDetalleAsignacion()
        {
            try
            {
                #region add Item()
                if (dgvDetalleAsignacionesAPersonal != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(0); // chCuentaCorreoAsignacionID                 
                    array.Add(Convert.ToInt32(this.txtCodigo.Text)); // chCuentaCorreoID
                    array.Add(string.Empty); // chPersonalID
                    array.Add(string.Empty); // chPersonal       
                    array.Add(DateTime.Now.ToShortDateString()); // chDesde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); //chHasta        
                    array.Add(string.Empty); // chNota                                                     
                    array.Add(0); // chReferenciaID
                    array.Add(0); // chReferenciaSolicitudID
                    array.Add(1); // chEstadoDetalleCuentaCorreo                    
                    dgvDetalleAsignacionesAPersonal.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }


        private void btnActivarEstado_Click(object sender, EventArgs e)
        {
            ChangeStateDetailAsignacionPersonal();
        }

        private void ChangeStateDetailAsignacionPersonal()
        {
            try
            {

                if (dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chestado"].Value.ToString() == "1")
                {
                    dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chestado"].Value = "0";
                }
                else
                {
                    dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chestado"].Value = "1";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            AddItemDetalleAsignacion();
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetalleAsignacionesAPersonal.Rows.Count > 0)
            {
                DeleteitemAsignacionPersonal();
            }
        }

        private void DeleteitemAsignacionPersonal()
        {
            try
            {
                if (this.dgvDetalleAsignacionesAPersonal != null)
                {
                    #region delete item() 
                    if (dgvDetalleAsignacionesAPersonal.CurrentRow != null && dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoAsignacionID"].Value != null)
                    {
                        //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        try
                        {

                            Int32 dispositivoCodigo = (dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoAsignacionID"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoAsignacionID"].Value) : 0);
                            if (dispositivoCodigo != 0)
                            {
                                //  string itemIP = ((dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoID"].Value != null | dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoID"].Value.ToString()) : string.Empty);
                                if (dispositivoCodigo != 0)
                                {

                                    ListadoDetalleAsignacionDeCuentaEliminar.Add(new SAS_CuentasCorreoAsignacionPersonal
                                    {
                                        CuentaCorreoAsignacionId = dispositivoCodigo,
                                    });
                                }
                            }

                            dgvDetalleAsignacionesAPersonal.Rows.Remove(dgvDetalleAsignacionesAPersonal.CurrentRow);
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void btnExportarHistoricoPlanes_Click(object sender, EventArgs e)
        {

        }

        private void btnCambiarEstadoHistoricoPlanes_Click(object sender, EventArgs e)
        {
            CambiarEstadoHistoricoPlan();
        }

        private void CambiarEstadoHistoricoPlan()
        {
            try
            {

                if (dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chEstadoHistoricoPlan"].Value.ToString() == "1")
                {
                    dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chEstadoHistoricoPlan"].Value = "0";
                    //dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chEstadoSW"].Value = "INACTIVO";
                }
                else
                {
                    dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chEstadoHistoricoPlan"].Value = "1";
                    //dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chEstadoSW"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void btnAgregarHistoricoPlanes_Click(object sender, EventArgs e)
        {
            AddItemHistoricoPlan();
        }

        private void btnQuitarHistoricoPlanes_Click(object sender, EventArgs e)
        {
            if (dgvHistoricoPlanes.Rows.Count > 0)
            {
                DeleteItemHistoricoPlan();
            }
        }

        private void AddItemHistoricoPlan()
        {
            try
            {
                #region add Item()
                if (dgvHistoricoPlanes != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chCuentaCorreoIDHistoricoPlan                 
                    array.Add(0); // chCuentaCorreoIDHistoricoPlan   
                    array.Add(string.Empty); // chLicenciaTipoIdHistoricoPlan
                    array.Add(string.Empty); // chLicenciaHistoricoPlan
                    array.Add(DateTime.Now.ToShortDateString()); // chDesdeHistoricoPlan
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); //chHastaHistoricoPlan      
                    array.Add(string.Empty); // chNotaHistoricoPlan
                    array.Add(1); // chEstadoHistoricoPlan                    
                    array.Add(0); // chReferenciaIDHistoricoPlan
                    array.Add(0); // chReferenciaSolicitudIDHistoricoPlan
                    array.Add(string.Empty); //chTablaReferenciaHistoricoPlan
                    array.Add(string.Empty); // chTablaSolicitudHistoricoPlan
                    array.Add(string.Empty); // chUserIDHistoricoPlan
                    array.Add(string.Empty); // chHostNameHistoricoPlan
                    array.Add(string.Empty); // chFechaRegistroHistoricoPlan
                    dgvHistoricoPlanes.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void DeleteItemHistoricoPlan()
        {
            try
            {
                if (this.dgvHistoricoPlanes != null)
                {
                    #region delete item() 
                    if (dgvHistoricoPlanes.CurrentRow != null && dgvHistoricoPlanes.CurrentRow.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value != null)
                    {
                        //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        try
                        {

                            Int32 dispositivoCodigo = (dgvHistoricoPlanes.CurrentRow.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvHistoricoPlanes.CurrentRow.Cells["chCuentaCorreoTipoLicenciaIdHistoricoPlan"].Value) : 0);
                            if (dispositivoCodigo != 0)
                            {
                                //  string itemIP = ((dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoID"].Value != null | dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleAsignacionesAPersonal.CurrentRow.Cells["chCuentaCorreoID"].Value.ToString()) : string.Empty);
                                if (dispositivoCodigo != 0)
                                {

                                    ListadoDetalleHistoricoPlanEliminar.Add(new SAS_CuentasCorreosHistoricoPlan
                                    {
                                        CuentaCorreoTipoLicenciaId = dispositivoCodigo,
                                    });
                                }
                            }

                            dgvHistoricoPlanes.Rows.Remove(dgvHistoricoPlanes.CurrentRow);
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void btnExportarDetalleLog_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetalleAsignacionesAPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo = new SAS_CuentasCorreoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de detalle() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chPersonalID" || ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chPersonal")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = Modelo.ObtenerListadoDePersonal("SAS");
                        search.Text = "Buscar personal para asignar";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDetalleAsignacionesAPersonal.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPersonalID"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetalleAsignacionesAPersonal.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPersonal"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }

        private void dgvRegistro_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void dgvHistoricoPlanes_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo = new SAS_CuentasCorreoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de detalle() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chLicenciaTipoIdHistoricoPlan" || ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chLicenciaHistoricoPlan")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = Modelo.ObtenerListadoDePlanesDeCorreo("SAS");
                        search.Text = "Buscar tipo de planes";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvHistoricoPlanes.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chLicenciaTipoIdHistoricoPlan"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvHistoricoPlanes.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chLicenciaHistoricoPlan"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
