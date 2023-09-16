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


namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    public partial class PartesDiariosDeEquipamientoDetalle : Form
    {
        #region Variables()
        string nombreformulario = "ParteDeEquipamientoITD";
        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string companyId;
        private string conection;
        private string messageValidation;
        private string fileName = "DEFAULT";
        private string desde;
        private string hasta;
        private string CodigoProveedor = string.Empty;
        private string CodigoSedeDeTrabajo = string.Empty;
        private string CodigoTipoDipositivo = string.Empty;
        private string Fecha = string.Empty;
        private int ultimoItemEnListaDetalle = 0;
        private int codigoDispositivo;
        private int codigoSelecionado = 0;
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private SAS_ParteDiariosDeDispositivosController model;
        private List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult> listing; //Listado
        private List<SAS_ParteDiariosDeDispositivosDetalle> listadoDetalleEliminado = new List<SAS_ParteDiariosDeDispositivosDetalle>();
        private List<SAS_ParteDiariosDeDispositivosDetalle> listadoDetalle = new List<SAS_ParteDiariosDeDispositivosDetalle>();
        private SAS_ParteDiariosDeDispositivosDetalleByCodigoResult selectedItem; // Item Selecionado
        private SAS_ParteDiariosDeDispositivosDetalleByCodigoResult item;
        private SAS_ParteDiariosDeDispositivos itemParteDiarioDispositivo;
        private SAS_ParteDiariosDeDispositivos parteDiario;
        private List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult> listDetalleByCodigoMantenimiento;
        private SAS_DispositivoUsuariosController modeloDispositivo;
        private List<Grupo> tiempoProgramados, tiempoEjecutados;
        private List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeResult> ListadoPorTipoHardware;
        private List<SAS_ParteDiariosDeDispositivosDetalle> detail;
        private List<SAS_ParteDiariosDeDispositivosDetalle> detailDelete = new List<SAS_ParteDiariosDeDispositivosDetalle>();
        private int resultadoOperacion;
        #endregion
        public PartesDiariosDeEquipamientoDetalle()
        {
            InitializeComponent();
        }

        public PartesDiariosDeEquipamientoDetalle(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoSeleccionado)
        {
            InitializeComponent();
            nombreformulario = "ParteDeEquipamientoITD";
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSeleccionado;
            Inicio();
            CargarCombos();
            // btnGenerarReprogramacion.Enabled = false;
            AperturarFormulario();
        }



        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                EjecutarConsulta();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                gbCabecera02.Enabled = !false;
                gbDetalle.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }
        }

        private void EjecutarConsulta()
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                listing = new List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>();
                item = new SAS_ParteDiariosDeDispositivosDetalleByCodigoResult();
                listing = model.GetListDetailById("SAS", codigoSelecionado);
                if (listing != null && listing.ToList().Count > 0)
                {
                    item = listing.ElementAt(0);
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                gbCabecera02.Enabled = !false;
                gbDetalle.Enabled = !false;
                gbDocumento.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultadoDocumento();
        }

        private void MostrarResultadoDocumento()
        {
            #region Finalización del Hilo()           
            try
            {
                if (item != null)
                {
                    #region 
                    if (item.Codigo != (int?)null)
                    {
                        if (item.Codigo == 0)
                        {
                            #region Nuevo() 
                            LimpiarFormulario();
                            dgvDetalle.CargarDatos(listing.ToDataTable<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>());
                            dgvDetalle.Refresh();
                            btnProcesar.Enabled = true;
                            btnRegistrar.Enabled = true;
                            btnEditar.Enabled = false;
                            btnNuevo.Enabled = true;
                            btnDetalleCambiarEstado.Enabled = true;
                            btnDetalleAgregar.Enabled = true;
                            btnDetalleQuitar.Enabled = true;
                            #endregion
                        }
                        else if (item.Codigo > 0)
                        {
                            #region Asignar Objeto para edición() 
                            txtCodigo.Text = item.Codigo.ToString();
                            txtCorrelativo.Text = item.Codigo.ToString().PadLeft(7, '0');
                            txtObservaciones.Text = item.observacion01.Trim();
                            txtEstadoCodigo.Text = item.EstadoCodigo.ToString().Trim();
                            txtEstado.Text = item.Estado != (byte?)null ? ((item.Estado.ToString().Trim() == "1" ? "PENDIENTE" : "ANULADO")) : string.Empty;
                            txtEmpresaCodigo.Text = "001";
                            txtEmpresa.Text = ("Sociedad Agrícola Saturno S.A").ToUpper();
                            txtSucursalCodigo.Text = "001";
                            txtSucursal.Text = ("Sociedad Agrícola Saturno S.A").ToUpper();
                            txtProveedorCodigo.Text = item.idclieprov.Trim();
                            txtProveedor.Text = item.razonsocial != null ? item.razonsocial.Trim() : string.Empty;
                            txtTipoDispositivoCodigo.Text = item.tipoHardwareCodigo != null ? item.tipoHardwareCodigo.Trim() : string.Empty;
                            txtTipoDispositivoDescripcion.Text = item.tipoHardware != null ? item.tipoHardware.Trim() : string.Empty;
                            txtSedeCodigo.Text = item.sedeCodigo != null ? item.sedeCodigo.Trim() : string.Empty;
                            txtSede.Text = item.sede != null ? item.sede.Trim() : string.Empty;
                            #endregion

                            #region Listado detalle() 

                            // ultimoItemEnListaDetalle = 1;

                            if (listing != null)
                            {
                                if (listing.Count > 0)
                                {
                                    ultimoItemEnListaDetalle = Convert.ToInt32(listing.Max(X => X.Item)) + 1;
                                }
                            }
                            #endregion
                            dgvDetalle.CargarDatos(listing.ToDataTable<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>());
                            dgvDetalle.Refresh();

                            btnRegistrar.Enabled = true;
                            btnEditar.Enabled = false;
                            btnNuevo.Enabled = true;
                        }
                    }
                    else
                    {
                        #region Nuevo() 
                        LimpiarFormulario();
                        dgvDetalle.CargarDatos(listing.ToDataTable<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>());
                        dgvDetalle.Refresh();
                        btnProcesar.Enabled = true;
                        btnRegistrar.Enabled = true;
                        btnEditar.Enabled = false;
                        btnNuevo.Enabled = true;
                        btnDetalleCambiarEstado.Enabled = true;
                        btnDetalleAgregar.Enabled = true;
                        btnDetalleQuitar.Enabled = true;
                        #endregion
                    }
                    #endregion
                }

                BarraPrincipal.Enabled = true;
                gbCabecera02.Enabled = true;
                gbDetalle.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = true;
                gbCabecera02.Enabled = true;
                gbDetalle.Enabled = true;
                gbDocumento.Enabled = true;
                progressBar1.Visible = false;
                return;
            }
            #endregion
        }

        private void LimpiarFormulario()
        {
            model = new SAS_ParteDiariosDeDispositivosController();
            int ultimoRegistro = model.ObtenerUltimoOperacion("SAS");
            ultimoItemEnListaDetalle = 1;
            listadoDetalle = new List<SAS_ParteDiariosDeDispositivosDetalle>();
            listadoDetalleEliminado = new List<SAS_ParteDiariosDeDispositivosDetalle>();
            listing = new List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>();

            this.txtCodigo.Text = "0"; // cuando es 0 es nuevo
            this.txtCorrelativo.Text = ultimoRegistro.ToString().PadLeft(7, '0'); // traer el último registrado + 1, que solo se va a mostrar.
            this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO SA";
            this.txtEmpresaCodigo.Text = "001";
            this.txtEstado.Text = "PENDIENTE";
            this.txtFecha.Text = DateTime.Now.ToShortDateString();
            this.txtEstadoCodigo.Text = "PE";
            this.txtObservaciones.Text = string.Empty;
            this.txtProveedor.Text = string.Empty;
            this.txtProveedorCodigo.Text = string.Empty;
            this.txtSucursal.Text = "SEDE LOGISTICA AGRICOLA";
            this.txtSucursalCodigo.Text = "001";
            txtSedeCodigo.Text = string.Empty;
            txtSede.Text = string.Empty;
            txtProveedorCodigo.Text = string.Empty;
            txtProveedor.Text = string.Empty;
            txtTipoDispositivoCodigo.Text = string.Empty;
            txtTipoDispositivoDescripcion.Text = string.Empty;

        }



        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStatusDetail();
        }

        private void ChangeStatusDetail()
        {

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {

            CodigoProveedor = this.txtProveedorCodigo.Text.Trim();
            CodigoSedeDeTrabajo = this.txtSedeCodigo.Text.Trim();
            CodigoTipoDipositivo = this.txtTipoDispositivoCodigo.Text.Trim();
            Fecha = this.txtFecha.Text.Trim();

            if (ValidacionCabecera(CodigoProveedor, CodigoSedeDeTrabajo, CodigoTipoDipositivo, Fecha) == true)
            {
                gbDocumento.Enabled = false;
                gbCabecera02.Enabled = false;
                gbDetalle.Enabled = false;
                progressBar1.Visible = true;
                bgwProcesar.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show(messageValidation, "Procesar partes diarios");
            }
        }

        private void AgregarListadoDeDispositivos(string _codigoProveedor, string _codigoSedeDeTrabajo, string _codigoTipoDipositivo, string _fecha)
        {
            try
            {
                #region Agregar listado() 
                #region 


                model = new SAS_ParteDiariosDeDispositivosController();



                //var   listadoDetalleByItem = modelo.GetToListDetailByIdByTypeSoftware(conection, oDetalle, codigoTipoSoftwareElegido).ToList();
                //   if (listadoDetalleByItem != null)
                //   {
                //       if (listadoDetalleByItem.ToList().Count > 0)
                //       {
                //           foreach (var item in listadoDetalleByItem)
                //           {
                //               if (dgvDetalle != null)
                //               {
                //                   ArrayList array = new ArrayList();
                //                   array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chId                 
                //                   array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalle))); // chItemDetalle
                //                   array.Add(string.Empty); // chCodigoDispositivo
                //                   array.Add(string.Empty); // chDispositivo
                //                   array.Add(string.Empty); // chPersonal
                //                   array.Add(string.Empty); // chArea
                //                   array.Add(item.minutos.Value.ToDecimalPresentation()); // chHorasActivas                                                                                                                             
                //                   array.Add(item.minutos.Value.ToDecimalPresentation()); // chHorasInactivas           
                //                   array.Add(string.Empty); // chMotivoInactivoCodigo
                //                   array.Add(string.Empty); // chMotivoInactivo
                //                   array.Add(string.Empty); // chObservacion
                //                   array.Add(string.Empty); // chEstadoId                
                //                   dgvDetalle.AgregarFila(array);
                //                   ultimoItemEnListaDetalle += 1;
                //               }
                //               else
                //               {
                //                   Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                //               }
                //           }
                //       }
                //   }
                #endregion
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Agregar listado de dispostivos a grilla detalle");
            }
        }

        private bool ValidacionCabecera(string _codigoProveedor, string _codigoSedeDeTrabajo, string _codigoTipoDipositivo, string _fecha)
        {
            messageValidation = string.Empty;
            string paraValidacion = string.Empty;
            bool estadoCabecera = true;

            if (_codigoProveedor == paraValidacion)
            {
                messageValidation += "Debe ingresar un proveedor válido\n";
                return false;
            }

            if (_codigoSedeDeTrabajo == paraValidacion)
            {
                messageValidation += "Debe ingresar una sede de trabajo válido\n";
                return false;

            }

            if (_codigoTipoDipositivo == paraValidacion)
            {
                messageValidation += "Debe ingresar tipo de dispositivo válido\n";
                return false;
            }

            if (_fecha == paraValidacion)
            {
                var result = new List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeRegisterResult>();
                model = new SAS_ParteDiariosDeDispositivosController();
                result = model.GetListByTipoHardwareProveedorSedeToRegister(conection, _codigoProveedor, _codigoTipoDipositivo, _codigoSedeDeTrabajo, _fecha);
                if (result != null || result.ToList().Count > 0)
                {
                    messageValidation += "Ya se tiene registro de un parte para esta sede de trabajo, sede de trabajo, proveedor\n";
                    return false;
                }
            }


            return estadoCabecera;
        }

        private void bgwProcesar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                ListadoPorTipoHardware = new List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeResult>();
                ListadoPorTipoHardware = model.GetListByTipoHardwareProveedorSede(conection, CodigoProveedor, CodigoTipoDipositivo, CodigoSedeDeTrabajo);

                if (ListadoPorTipoHardware != null && ListadoPorTipoHardware.ToList().Count > 0)
                {
                    //
                    listing = new List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>();
                    listing = (from item in ListadoPorTipoHardware
                               group item by new { item.item } into j
                               select new SAS_ParteDiariosDeDispositivosDetalleByCodigoResult
                               {
                                   sedeCodigo = string.Empty,
                                   EstadoCodigo = string.Empty,
                                   observacion01 = string.Empty,
                                   sede = string.Empty,
                                   estadoDocumento = string.Empty,
                                   Codigo = Convert.ToInt32(this.txtCodigo.Text),
                                   DispositivoCodigo = j.FirstOrDefault().id,
                                   Item = j.FirstOrDefault().item,
                                   HorasActivas = j.FirstOrDefault().horasActivas,
                                   HorasInactivas = j.FirstOrDefault().horasInactivas,
                                   Observacion = j.FirstOrDefault().observacion,
                                   MotivoInactivoCodigo = 0,
                                   Disponibilidad = 100,
                                   Estado = 1,
                                   dispositivo = j.FirstOrDefault().dispositivo.Trim(),
                                   estadoItemDetalle = string.Empty,
                                   HorasActivasEnJornada = 24,
                                   horasProgramadas = 24,
                                   motivoInactividad = string.Empty,
                                   tipoHardwareCodigo = string.Empty,
                                   razonsocial = string.Empty,
                                   idclieprov = string.Empty,
                                   personalAsignado = j.FirstOrDefault().Personal,
                                   areaAsignada = j.FirstOrDefault().AreaDeTrabajo
                               }
                                    ).ToList();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Procesar Do Work");
            }
        }

        private void bgwProcesar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvDetalle.CargarDatos(listing.ToDataTable<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>());
                dgvDetalle.Refresh();
                gbDocumento.Enabled = !false;
                gbCabecera02.Enabled = !false;
                gbDetalle.Enabled = !false;
                progressBar1.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Procesar Run Complete");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registar();
        }

        private void Registar()
        {
            try
            {
                #region Validar Formulario()

                BarraPrincipal.Enabled = false;
                gbDocumento.Enabled = false;
                gbCabecera02.Enabled = false;
                gbDetalle.Enabled = false;
                progressBar1.Visible = true;

                CodigoProveedor = this.txtProveedorCodigo.Text.Trim();
                CodigoSedeDeTrabajo = this.txtSedeCodigo.Text.Trim();
                CodigoTipoDipositivo = this.txtTipoDispositivoCodigo.Text.Trim();
                Fecha = this.txtFecha.Text.Trim();

                if (ValidacionCabecera(CodigoProveedor, CodigoSedeDeTrabajo, CodigoTipoDipositivo, Fecha) == true)
                {
                    #region Obtener Objeto()
                    itemParteDiarioDispositivo = new SAS_ParteDiariosDeDispositivos();
                    itemParteDiarioDispositivo.Codigo = Convert.ToInt32(txtCodigo.Text.Trim());
                    itemParteDiarioDispositivo.TipoHardwareCodigo = txtTipoDispositivoCodigo.Text.Trim();
                    itemParteDiarioDispositivo.idClieprov = txtProveedorCodigo.Text.Trim();
                    itemParteDiarioDispositivo.Fecha = Convert.ToDateTime(txtFecha.Text);
                    itemParteDiarioDispositivo.SedeCodigo = txtSedeCodigo.Text.Trim();
                    itemParteDiarioDispositivo.EstadoCodigo = txtEstadoCodigo.Text.Trim();
                    itemParteDiarioDispositivo.Observacion = txtObservaciones.Text.Trim();
                    itemParteDiarioDispositivo.GeneradoPor = user2.IdUsuario;
                    ///itemParteDiarioDispositivo.Fecha = DateTime.Now;
                    itemParteDiarioDispositivo.Hostname = Environment.MachineName;

                    detail = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                    if (dgvDetalle != null)
                    {
                        if (dgvDetalle.RowCount > 0)
                        {
                            foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                            {
                                if (fila.Cells["chCodigoDispositivo"].Value.ToString().Trim() != String.Empty)
                                {
                                    SAS_ParteDiariosDeDispositivosDetalle oDetail = new SAS_ParteDiariosDeDispositivosDetalle();

                                    oDetail.Codigo = fila.Cells["chId"].Value != null ? Convert.ToInt32(fila.Cells["chId"].Value.ToString().Trim()) : 0;
                                    oDetail.DispositivoCodigo = fila.Cells["chCodigoDispositivo"].Value != null ? Convert.ToInt32(fila.Cells["chCodigoDispositivo"].Value.ToString().Trim()) : 0;
                                    oDetail.Item = fila.Cells["chItemDetalle"].Value != null ? fila.Cells["chItemDetalle"].Value.ToString().Trim() : string.Empty;
                                    oDetail.HorasActivas = fila.Cells["chHorasActivas"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasActivas"].Value.ToString().Trim()) : 0;
                                    oDetail.HorasInactivas = fila.Cells["chHorasInactivas"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasInactivas"].Value.ToString().Trim()) : 0;
                                    oDetail.Observacion = fila.Cells["chObservacion"].Value != null ? fila.Cells["chObservacion"].Value.ToString().Trim() : string.Empty;
                                    oDetail.MotivoInactivoCodigo = fila.Cells["chMotivoInactivoCodigo"].Value != null ? Convert.ToInt32(fila.Cells["chMotivoInactivoCodigo"].Value.ToString().Trim()) : 0;
                                    oDetail.Estado = fila.Cells["chEstadoId"].Value != null ? Convert.ToByte(fila.Cells["chEstadoId"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    detail.Add(oDetail);
                                }

                            }
                        }
                    }

                    // detailDelete = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                    #endregion

                    #region Registar Objeto()                   
                    bgwRegistrar.RunWorkerAsync();
                    #endregion

                    MessageBox.Show("Se ha registrado exitosamente el documento", "Registro del documento | Partde diario de equipamiento");
                }
                else
                {
                    MessageBox.Show(messageValidation, "Registro del documento");
                }

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                resultadoOperacion = model.RegistrarParteDiario(conection, itemParteDiarioDispositivo, detail, detailDelete);
                codigoSelecionado = resultadoOperacion;
                EjecutarConsulta();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }

        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                this.txtCodigo.Text = resultadoOperacion.ToString();
                MostrarResultadoDocumento();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }

        }

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void DeleteItem()
        {
            if (this.dgvDetalle != null)
            {
                #region
                if (dgvDetalle.CurrentRow != null && dgvDetalle.CurrentRow.Cells["chId"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 CodigoParte = (dgvDetalle.CurrentRow.Cells["chId"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalle.CurrentRow.Cells["chId"].Value) : 0);
                        Int32 CodigoDispositivo = (dgvDetalle.CurrentRow.Cells["chCodigoDispositivo"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalle.CurrentRow.Cells["chCodigoDispositivo"].Value) : 0);
                        if (CodigoParte != 0)
                        {
                            string itemIP = ((dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value != null | dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value.ToString().Trim() != string.Empty) ? (dgvDetalle.CurrentRow.Cells["chItemDetalle"].Value.ToString()) : string.Empty);
                            if (CodigoDispositivo != 0 && itemIP != string.Empty)
                            {

                                detailDelete.Add(new SAS_ParteDiariosDeDispositivosDetalle
                                {
                                    Codigo = CodigoParte,
                                    DispositivoCodigo = CodigoDispositivo,
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

        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {
            CodigoProveedor = this.txtProveedorCodigo.Text.Trim();
            CodigoSedeDeTrabajo = this.txtSedeCodigo.Text.Trim();
            CodigoTipoDipositivo = this.txtTipoDispositivoCodigo.Text.Trim();
            Fecha = this.txtFecha.Text.Trim();

            if (ValidacionCabecera(CodigoProveedor, CodigoSedeDeTrabajo, CodigoTipoDipositivo, Fecha) == true)
            {
                AddItem();
            }
            else
            {
                MessageBox.Show(messageValidation, "Agregar detalle");
            }

        }

        private string ObtenerFormatoParaAgregarItemDetalle(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }



        private void AddItem()
        {
            try
            {
                if (dgvDetalle != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chId                  
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalle))); // chItemDetalle
                    array.Add(Convert.ToInt32(0)); // chCodigoDispositivo
                    array.Add(string.Empty); // chDispositivo
                    array.Add(string.Empty); // chPersonal
                    array.Add(string.Empty); // chArea
                    array.Add(Convert.ToDecimal(24)); // chHorasActivas                                      
                    array.Add(Convert.ToDecimal(0)); // chHorasInactivas
                    array.Add(Convert.ToInt32(0)); // chMotivoInactivoCodigo          
                    array.Add(string.Empty); // chMotivoInactivo
                    array.Add(string.Empty); // chObservacion
                    array.Add(Convert.ToInt32(0));  // chEstadoId                    
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {

            try
            {

                txtSedeCodigo.Text = string.Empty;
                txtProveedorCodigo.Text = string.Empty;
                txtTipoDispositivoCodigo.Text = string.Empty;
                txtObservaciones.Text = string.Empty;
                txtEstadoCodigo.Text = string.Empty;
                txtCodigo.Text = string.Empty;
                txtFecha.Text = string.Empty;


                txtSede.Text = string.Empty;
                txtProveedor.Text = string.Empty;
                txtTipoDispositivoDescripcion.Text = string.Empty;
                txtCorrelativo.Text = "0".PadLeft(7, '0');


                txtCodigo.Text = "0";
                txtEstadoCodigo.Text = "PE";
                txtEstado.Text = "PENDIENTE";
                txtFecha.Text = DateTime.Now.ToShortDateString();


                itemParteDiarioDispositivo = new SAS_ParteDiariosDeDispositivos();
                detail = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                detailDelete = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                ultimoItemEnListaDetalle = 1;
                listing = new List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>();
                dgvDetalle.CargarDatos(listing.ToDataTable<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>());
                dgvDetalle.Refresh();

                BarraPrincipal.Enabled = true;
                gbDocumento.Enabled = true;
                gbCabecera02.Enabled = true;
                gbDetalle.Enabled = true;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = false;
                btnRegistrar.Enabled = true;
                btnAtras.Enabled = true;
                btnAnular.Enabled = false;
                btnEliminarRegistro.Enabled = false;
                btnExportToExcel.Enabled = false;
                btnAdjuntar.Enabled = false;
                btnNotificar.Enabled = false;
                btnCerrar.Enabled = true;


                btnSedeBuscar.Enabled = true;
                btnProveedorBuscar.Enabled = true;
                btnTipoDispositivoBuscar.Enabled = true;

                btnDetalleCambiarEstado.Enabled = true;
                btnDetalleAgregar.Enabled = true;
                btnDetalleQuitar.Enabled = true;

                txtSedeCodigo.Enabled = true;
                txtSede.Enabled = true;

                txtProveedorCodigo.Enabled = true;
                txtProveedor.Enabled = true;

                txtTipoDispositivoCodigo.Enabled = true;
                txtTipoDispositivoDescripcion.Enabled = true;


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Procesar Run Complete");
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            if (this.txtEstadoCodigo.Text != "PE")
            {
                MessageBox.Show("El documento no tiene el estado para realizar la edición", "Edición del documento");
            }
            else
            {
                BarraPrincipal.Enabled = true;
                gbDocumento.Enabled = true;
                gbCabecera02.Enabled = true;
                gbDetalle.Enabled = true;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = false;
                btnRegistrar.Enabled = true;
                btnAtras.Enabled = true;
                btnAnular.Enabled = false;
                btnEliminarRegistro.Enabled = false;
                btnExportToExcel.Enabled = false;
                btnAdjuntar.Enabled = false;
                btnNotificar.Enabled = false;
                btnCerrar.Enabled = true;

                btnSedeBuscar.Enabled = false;
                btnProveedorBuscar.Enabled = false;
                btnTipoDispositivoBuscar.Enabled = false;

                btnDetalleCambiarEstado.Enabled = false;
                btnDetalleAgregar.Enabled = false;
                btnDetalleQuitar.Enabled = false;

                if (this.txtEstadoCodigo.Text == "PE")
                {
                    btnDetalleCambiarEstado.Enabled = true;
                    btnDetalleAgregar.Enabled = true;
                    btnDetalleQuitar.Enabled = true;
                }


                txtSedeCodigo.Enabled = false;
                txtSede.Enabled = false;

                txtProveedorCodigo.Enabled = false;
                txtProveedor.Enabled = false;

                txtTipoDispositivoCodigo.Enabled = false;
                txtTipoDispositivoDescripcion.Enabled = false;
            }





        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            AnularDocumento();
        }

        private void AnularDocumento()
        {
            BarraPrincipal.Enabled = true;
            gbDocumento.Enabled = false;
            gbCabecera02.Enabled = false;
            gbDetalle.Enabled = false;

            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnAtras.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportToExcel.Enabled = false;
            btnAdjuntar.Enabled = false;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            btnSedeBuscar.Enabled = false;
            btnProveedorBuscar.Enabled = false;
            btnTipoDispositivoBuscar.Enabled = false;

            btnDetalleCambiarEstado.Enabled = false;
            btnDetalleAgregar.Enabled = false;
            btnDetalleQuitar.Enabled = false;

            txtSedeCodigo.Enabled = false;
            txtSede.Enabled = false;

            txtProveedorCodigo.Enabled = false;
            txtProveedor.Enabled = false;

            txtTipoDispositivoCodigo.Enabled = false;
            txtTipoDispositivoDescripcion.Enabled = false;
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            EliminarDocumento();
        }

        private void EliminarDocumento()
        {
            model = new SAS_ParteDiariosDeDispositivosController();
            SAS_ParteDiariosDeDispositivos itemDelete = new SAS_ParteDiariosDeDispositivos();
            itemDelete.Codigo = Convert.ToInt32(this.txtCodigo.Text);
            model.DeleteRecord(conection, itemDelete);
            MessageBox.Show("Eliminado correctamente", "Eliminación de documento");
            Nuevo();

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {

        }

        private void btnNotificar_Click(object sender, EventArgs e)
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

        private void PartesDiariosDeEquipamientoDetalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            comboHelper = new ComboBoxHelper();
            
            int totalHorasPorTurno = 24;
            if (((DataGridView)sender).RowCount > 0)
            {
                #region 
                string estado = this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoId"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoId"].Value.ToString() : "0";
                if (estado == "1")
                {
                    if (user2 != null)
                    {
                        if (user2.IdUsuario != null)
                        {
                            //if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                            //{
                            #region Cambiar de estado()  
                            if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chCodigoDispositivo")
                            {
                                if (e.KeyCode == Keys.F3)
                                {
                                    frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                    List<int> listadoCodigoEnGrilla = new List<int>();
                                    listadoCodigoEnGrilla = ObtenerListadoCodigoDispositivosEnGrilla();
                                    string idClieprov = this.txtProveedorCodigo.Text.Trim();
                                    string sedecodigo = this.txtSedeCodigo.Text.Trim();
                                    string tipoDispositivoCodigo = this.txtTipoDispositivoCodigo.Text.Trim();

                                    model = new SAS_ParteDiariosDeDispositivosController();
                                    search.ListaGeneralResultado = model.ObtenerListadoDispositivosDisponibles("SAS", this.txtFecha.Text.ToString(), listadoCodigoEnGrilla, idClieprov, sedecodigo, tipoDispositivoCodigo);
                                    search.Text = "Obtener dispositivo";
                                    search.txtTextoFiltro.Text = "";
                                    if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chCodigoDispositivo"].Value = search.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDispositivo"].Value = search.ObjetoRetorno.Descripcion;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPersonal"].Value = ObtenerNombrePersonal(search.ObjetoRetorno.Descripcion.ToString());
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chArea"].Value = ObtenerNombreArea(search.ObjetoRetorno.Descripcion.ToString());
                                    }
                                }
                            }
                            #endregion
                            //}
                        }

                        #region Minutos activos()
                        //if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHorasActivas")
                        //{

                        //    if (e.KeyCode == Keys.F3)
                        //    {
                        //        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        //        comboHelper = new ComboBoxHelper();
                        //        var resultado03 = comboHelper.MinutosActivosPartesEquipamiento("SAS");
                        //        List<DFormatoSimple> resultado04 = new List<DFormatoSimple>();

                        //        resultado04 = (from items in resultado03.ToList()
                        //                       group items by new { items.Code } into j
                        //                       select new DFormatoSimple
                        //                       {
                        //                            Codigo = j.Key.Code.ToString(),
                        //                            Descripcion = j.FirstOrDefault().Descripcion
                        //                       }).ToList();

                        //        search.ListaGeneralResultado = resultado04;
                        //        search.Text = "Minutos Activos";
                        //        search.txtTextoFiltro.Text = "";
                        //        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                        //            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasActivas"].Value = search.ObjetoRetorno.Codigo;
                        //            //this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chUsuarioDetalle"].Value = search.ObjetoRetorno.Codigo;
                        //        }
                        //    }
                        //}
                        #endregion

                        #region Minutos Inactivos()
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHorasInactivas")
                        {
                            int codigoMotivoInactividad = Convert.ToInt32(this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value : 0);
                            if (codigoMotivoInactividad > 0)
                            {
                                #region Solo los que marcaron motivo 0
                                if (e.KeyCode == Keys.F3)
                                {
                                    frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                    comboHelper = new ComboBoxHelper();
                                    //var resultado03 = comboHelper.HorasActivosPartesEquipamiento("SAS");
                                    //List<DFormatoSimple> resultado04 = new List<DFormatoSimple>();

                                    //resultado04 = (from items in resultado03.ToList()
                                      //             group items by new { items.Code } into j
                                        //           select new DFormatoSimple
                                          //         {
                                            //           Codigo = j.Key.Code.ToString(),
                                              //         Descripcion = j.FirstOrDefault().Descripcion
                                                //   }).ToList();

                                    search.ListaGeneralResultado = comboHelper.HorasActivosPartesEquipamiento("SAS");
                                    search.Text = "Minutos Inactivos";
                                    search.txtTextoFiltro.Text = "";
                                    if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasInactivas"].Value = search.ObjetoRetorno.Codigo;
                                        //int totalMinutosInactivos = Convert.ToInt32(search.ObjetoRetorno.Codigo);
                                        //int totalMinutosActivos = totalMinutosPorTurno - totalMinutosInactivos;
                                        Decimal totalHorasInactivas = Convert.ToDecimal(search.ObjetoRetorno.Codigo);
                                        Decimal totalHorasActivos = Convert.ToDecimal(totalHorasPorTurno - totalHorasInactivas);
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasActivas"].Value = totalHorasActivos.ToString();
                                    }
                                }
                                #endregion
                            }



                        }
                        #endregion

                        #region Motivo de Inactivo()
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chMotivoInactivo")
                        {
                            //int codigoMotivoInactividad = Convert.ToInt32(this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value : 0);
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                comboHelper = new ComboBoxHelper();
                                search.ListaGeneralResultado = comboHelper.ObtenerListadoDeMotivosDeInactividad("SAS");
                                search.Text = "Motivo de inactividad";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    int codigoMotivoInactivo = Convert.ToInt32(search.ObjetoRetorno.Codigo);
                                    if (codigoMotivoInactivo > 0)
                                    {
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value = search.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivo"].Value = search.ObjetoRetorno.Descripcion;
                                    }
                                    else
                                    {
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value = search.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivo"].Value = search.ObjetoRetorno.Descripcion;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasInactivas"].Value = 0.ToString();
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasActivas"].Value = totalHorasPorTurno.ToString();
                                    }

                                }
                            }
                        }
                        #endregion

                    }
                }
                #endregion
            }
        }

        private List<int> ObtenerListadoCodigoDispositivosEnGrilla()
        {
            List<int> result = new List<int>();

            if (dgvDetalle != null)
            {
                if (dgvDetalle.RowCount > 0)
                {
                    foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                    {
                        if (fila.Cells["chCodigoDispositivo"].Value.ToString().Trim() != String.Empty)
                        {
                            if (fila.Cells["chCodigoDispositivo"].Value.ToString().Trim() != "0")
                            {
                                result.Add(Convert.ToInt32(fila.Cells["chCodigoDispositivo"].Value));
                            }
                        }

                    }
                }
            }

            return result;
        }

        private string ObtenerNombrePersonal(string descripcion)
        {
            string[] lineas = descripcion.Split('|');

            return lineas[1];
        }


        private string ObtenerNombreArea(string descripcion)
        {
            string[] lineas = descripcion.Split('|');

            return lineas[2];
        }

        private void AperturarFormulario()
        {
            BarraPrincipal.Enabled = false;
            gbCabecera02.Enabled = false;
            gbDetalle.Enabled = false;
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
                tipoDePrioridades = new List<Grupo>();

                documentos = comboHelper.GetDocumentTypeForForm("SAS", "ParteDeEquipamientoITD");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                series = comboHelper.GetDocumentSeriesForForm("SAS", "ParteDeEquipamientoITD");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();


                tiempoProgramados = new List<Grupo>();
                tiempoEjecutados = new List<Grupo>();



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


        private void PartesDiariosDeEquipamientoDetalle_Load(object sender, EventArgs e)
        {

        }
    }
}
