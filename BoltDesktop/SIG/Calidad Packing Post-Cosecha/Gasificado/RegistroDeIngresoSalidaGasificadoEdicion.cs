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
using MyControlsDataBinding.Controles;
using System.Collections;
using MyControlsDataBinding.Busquedas;

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class RegistroDeIngresoSalidaGasificadoEdicion : Form
    {
        #region Variables()
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoDeRegistrosExoneradosByDatesResult document;
        private List<SAS_RegistroGasificadoAll> documents;
        private List<IngresoSalidaGasificado> listadoDetalle = new List<IngresoSalidaGasificado>();
        private List<IngresoSalidaGasificado> listadoDetalleEliminado = new List<IngresoSalidaGasificado>();
        private SAS_RegistroGasificadoAllByIDResult documentDate;
        private List<SAS_RegistroGasificadoAllByIDResult> documentsDate;
        private SAS_RegistroGasificadoController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_RegistroGasificado cabecera = new SAS_RegistroGasificado();        //List<IngresoSalidaGasificado> listadoDetalle = new List<IngresoSalidaGasificado>();
        private List<SAS_RegistroGasificadoAll> listadoDetalleFull = new List<SAS_RegistroGasificadoAll>();
        private List<SAS_RegistroGasificadoAllByIDResult> listadoDetalleFullDate = new List<SAS_RegistroGasificadoAllByIDResult>();
        private string observacionesPorValidad = string.Empty;
        private string modo;
        private SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult itemAdd;
        private int numeroDeTicket;
        private SAS_RegistroGasificado oRegistroGasificado;
        private IngresoSalidaGasificado itemDetalle;

        public int CodigoDelRegistro = 0;
        private decimal capacidadTotal = 600;
        private decimal cantidadJabas = 0;
        private decimal porcentajeOcupado = 0;
        #endregion

        public RegistroDeIngresoSalidaGasificadoEdicion()
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = "NSFAJA";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            user2.IdCodigoGeneral = "100369";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.editar = 1;
            documentDate = new SAS_RegistroGasificadoAllByIDResult();


            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim() == "EAURAZO" || user2.IdUsuario.Trim() == "JGARAY" || user2.IdUsuario.Trim() == "FCERNA" || user2.IdUsuario.Trim() == "ABURGA")
                        {
                            btnGenerarSecuencia.Visible = true;
                        }
                    }
                }
            }

            LimpiarControles(gbDatosDelProceso);
            LimpiarControles(gbDetallePallet);
            LimpiarControles(gbDocumento);
            LimpiarControles(gbProcedimiento);
            //            CargarCombos();            
            bgwHilo.RunWorkerAsync();
        }


        public RegistroDeIngresoSalidaGasificadoEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_ListadoDeRegistrosExoneradosByDatesResult _document)
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            document = _document;
            documentDate = new SAS_RegistroGasificadoAllByIDResult();
            documentDate.idGasificado = _document.idgasificado;
            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim() == "EAURAZO" || user2.IdUsuario.Trim() == "JGARAY" || user2.IdUsuario.Trim() == "FCERNA" || user2.IdUsuario.Trim() == "ABURGA")
                        {
                            btnGenerarSecuencia.Visible = true;
                        }
                    }
                }
            }

            LimpiarControles(gbDatosDelProceso);
            LimpiarControles(gbDetallePallet);
            LimpiarControles(gbDocumento);
            LimpiarControles(gbProcedimiento);
            //            CargarCombos();        
            gbDatosDelProceso.Enabled = false;
            gbDetallePallet.Enabled = false;
            gbDocumento.Enabled = false;
            gbProcedimiento.Enabled = false;

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportar.Enabled = false;
            btnAtras.Enabled = false;

            modo = "1"; // BLOQUEA CONTROLES Y DESBLOQUEA

            bgwHilo.RunWorkerAsync();
        }


        public RegistroDeIngresoSalidaGasificadoEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_RegistroGasificadoAllByIDResult _documentDate)
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            documentDate = _documentDate;
            documentDate.idGasificado = _documentDate.idGasificado;

            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim() == "EAURAZO" || user2.IdUsuario.Trim() == "JGARAY" || user2.IdUsuario.Trim() == "FCERNA" || user2.IdUsuario.Trim() == "ABURGA")
                        {
                            btnGenerarSecuencia.Visible = true;
                        }
                    }
                }
            }

            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportar.Enabled = false;
            btnAtras.Enabled = false;

            LimpiarControles(gbDatosDelProceso);
            LimpiarControles(gbDetallePallet);
            LimpiarControles(gbDocumento);
            LimpiarControles(gbProcedimiento);
            //            CargarCombos();        

            gbDatosDelProceso.Enabled = false;
            gbDetallePallet.Enabled = false;
            gbDocumento.Enabled = false;
            gbProcedimiento.Enabled = false;

            if (_documentDate.idGasificado == 0)
            {
                gbDatosDelProceso.Enabled = true;
                gbDetallePallet.Enabled = true;
                gbDocumento.Enabled = true;
                gbProcedimiento.Enabled = true;
                btnRegistrar.Enabled = true;
                btnNuevo.Enabled = true;
            }




            modo = "1"; // BLOQUEA CONTROLES Y DESBLOQUEA

            bgwHilo.RunWorkerAsync();
        }


        private void CargarCombos()
        {

            model = new SAS_RegistroGasificadoController();
            cboDocumento.DisplayMember = "descripcion";
            cboDocumento.ValueMember = "valor";
            // obtener listado de documentos | get list of documents
            cboDocumento.DataSource = model.GetListOfDocuments("RegistroDeIngresoSalidaGasificadoEdicion").ToList();
            cboDocumento.SelectedValue = "GAS";


            cboSerie.DisplayMember = "descripcion";
            cboSerie.ValueMember = "valor";
            cboSerie.DataSource = model.GetSerialNumberList("RegistroDeIngresoSalidaGasificadoEdicion").ToList();
            cboSerie.SelectedValue = "0001";

            cboCamaraGasificado.DisplayMember = "descripcion";
            cboCamaraGasificado.ValueMember = "valor";
            // obtain a list of gasification chambers
            cboCamaraGasificado.DataSource = model.ObtainAListOfGasificacionChambers("RegistroDeIngresoSalidaGasificadoEdicion").ToList();
            cboCamaraGasificado.SelectedValue = "000";


        }



        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJA"].ToString();
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


        private void LimpiarControles(Control cntr)
        {
            foreach (Control c in cntr.Controls)
            {
                //Cuando es un contenedor, vuelve a llamarse a si misma la funcion enviandose el conteneder como parametro
                if (c is TabControl)
                    LimpiarControles((TabControl)c);
                else if (c is TabPage)
                    LimpiarControles((TabPage)c);
                else if (c is Panel)
                    LimpiarControles((Panel)c);
                else if (c is GroupBox)
                    LimpiarControles((GroupBox)c);
                else if (c is TextBox)
                    c.Text = string.Empty;
                else if (c is MyTextBoxSearchSimple)
                    c.Text = string.Empty;
                else if (c is MyDataGridViewMaskedTextEditingControl)
                    c.Text = string.Empty;
                else if (c is MyTextBox)
                    c.Text = string.Empty;
                else if (c is ComboBox)
                {
                    ComboBox cm = new ComboBox();
                    cm = (ComboBox)c;
                    cm.SelectedIndex = -1;
                }
                else if (c is CheckBox)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)c;
                    chk.Checked = false;
                }
                else if (c is DateTimePicker)
                {
                    DateTimePicker dtp = new DateTimePicker();
                    dtp = (DateTimePicker)c;
                    dtp.Value = Convert.ToDateTime("01/01/1900");
                }
                // A los objetos no contenedores, creo una instancia de ellos y aplico la instruccion que considero es limpiar
                // Puedes agregar mas tipos de controles a los if anidadados segun necesites limpiarlos


            }

        }


        private void RegistroDeIngresoSalidaGasificadoEdicion_Load(object sender, EventArgs e)
        {
            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ABURGA" || user2.IdUsuario.Trim().ToUpper() == "JGARAY" || user2.IdUsuario.Trim().ToUpper() == "dvaldiviezo".ToUpper() || user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                        {
                            btnGenerarSecuencia.Visible = true;
                        }
                    }
                }
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void EjecutarConsulta()
        {
            try
            {
                #region Acción() 
                model = new SAS_RegistroGasificadoController();
                documentsDate = new List<SAS_RegistroGasificadoAllByIDResult>();
                //documentDate = new SAS_RegistroGasificadoAllByIDResult();
                documentsDate = model.GetListRegistroGasificadoByIdGasificado(conection, documentDate.idGasificado);
                cantidadJabas = model.ObtenerCantidadDeTicketGasificadosPorIdGasificado(conection, documentDate.idGasificado);

                listadoDetalleFullDate = new List<SAS_RegistroGasificadoAllByIDResult>();
                if (documentsDate != null)
                {
                    if (documentsDate.ToList().Count > 0)
                    {
                        documentDate = model.ResumirListadoByIdGasificado(documentsDate);
                        if (documentDate != null)
                        {
                            //if (documentDate.ToList().Count > 0)
                            //{
                            documentDate = documentsDate.ElementAt(0);
                            //listadoDetalle = model.GetDetailList(conection, document.idGasificado);
                            listadoDetalleFullDate = model.GetListRegistroGasificadoByIdGasificado(conection, documentDate.idGasificado);
                            //}
                        }
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

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Acción() 

                if (documentDate != null)
                {
                    if (documentDate.idGasificado != (int?)null)
                    {
                        if (documentDate.idGasificado == 0)
                        {
                            #region Registro Nuevo()
                            txtEmpresaCodigo.Text = "001";
                            txtEmpresa.Text = "SOCIEDAD AGRÍCOLA SATURNO S.A";
                            txtUsuarioAsignado.Text = user2.IdCodigoGeneral;
                            txtEstado.Text = "GASIFICANDO";
                            txtSucursalCodigo.Text = "002";
                            txtSucursal.Text = "PLANTA - SATURNO";
                            txtCodigo.Text = "0";
                            cboDocumento.SelectedValue = "GAS";
                            cboSerie.SelectedValue = "0001";
                            txtNumeroDocumento.Text = "0".ToString().Trim().PadLeft(7, '0');
                            txtFecha.Text = DateTime.Now.ToPresentationDate();
                            cboCamaraGasificado.SelectedValue = "001";
                            txtHoraInicioInyeccion.Text = DateTime.Now.AddMinutes(2).ToPresentationDateTime();
                            txtHoraInicioGasificado.Text = DateTime.Now.AddMinutes(6).ToPresentationDateTime();
                            txtHoraInicioVentilacion.Text = DateTime.Now.AddMinutes(10).ToPresentationDateTime();
                            txtHoraFinProceso.Text = DateTime.Now.AddMinutes(12).ToPresentationDateTime();
                            txtProductoAplicadoCodigo.Text = "250600100006";
                            txtProductoAplicado.Text = "SO2";
                            txtDosisSO2.Text = "0";
                            txtTemperaturaDelAgua.Text = "0";
                            txtLecturasEnPPM.Text = "0";
                            txtMinutoEnGasificado.Text = "0 Minutos";
                            txtCapacidadAlmacenamiento.Text = capacidadTotal.ToString("N0");
                            txtCantidadJabas.Text = cantidadJabas.ToString("N0");
                            txtPorcentajeOcupado.Text = porcentajeOcupado.ToString("N2");
                            #endregion
                        }
                        else
                        {
                            #region Edición de registro() 
                            txtEmpresaCodigo.Text = documentDate.empresaCodigo != null ? documentDate.empresaCodigo.Trim() : "001";
                            txtEmpresa.Text = documentDate.empresa != null ? documentDate.empresa.Trim() : "SOCIEDAD AGRÍCOLA SATURNO S.A";
                            txtUsuarioAsignado.Text = documentDate.registradoPorNombres != null ? documentDate.registradoPorNombres.Trim() : user2.NombreCompleto.Trim();
                            txtEstado.Text = documentDate.estadoRegistroGasificado != null ? documentDate.estadoRegistroGasificado.Trim() : "GASIFICANDO";
                            txtSucursalCodigo.Text = documentDate.sucursalCodigo != null ? documentDate.sucursalCodigo.Trim() : "001";
                            txtSucursal.Text = documentDate.sucursalRegistroGasificado != null ? documentDate.sucursalRegistroGasificado.Trim() : "PLANTA - SATURNO";
                            txtCodigo.Text = documentDate.idGasificado != null ? documentDate.idGasificado.ToString().Trim() : string.Empty;
                            cboDocumento.SelectedValue = "GAS";
                            cboSerie.SelectedValue = "0001";
                            txtNumeroDocumento.Text = documentDate.idGasificado != null ? documentDate.idGasificado.ToString().Trim().PadLeft(7, '0') : string.Empty;
                            txtFecha.Text = documentDate.FECHA != null ? documentDate.FECHA.Value.ToString().Trim() : DateTime.Now.ToPresentationDate().Trim();
                            cboCamaraGasificado.SelectedValue = documentDate.idCamara != null ? documentDate.idCamara.Trim() : string.Empty;
                            txtHoraInicioInyeccion.Text = documentDate.horaInyeccion != null ? documentDate.horaInyeccion.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraInicioGasificado.Text = documentDate.horaGasificado != null ? documentDate.horaGasificado.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraInicioVentilacion.Text = documentDate.horaVentilacion != null ? documentDate.horaVentilacion.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraFinProceso.Text = documentDate.fechaSalida != null ? documentDate.fechaSalida.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtProductoAplicadoCodigo.Text = documentDate.idProductoAplicado != null ? documentDate.idProductoAplicado.Trim() : string.Empty;
                            txtProductoAplicado.Text = documentDate.productoAplicado != null ? documentDate.productoAplicado.Trim() : string.Empty;
                            txtDosisSO2.Text = documentDate.dosisSO2 != (decimal?)null ? documentDate.dosisSO2.Value.ToDecimalPresentation().Trim() : "0";
                            txtTemperaturaDelAgua.Text = documentDate.tempAgua != (decimal?)null ? documentDate.tempAgua.Value.ToDecimalPresentation().Trim() : "0";
                            txtLecturasEnPPM.Text = documentDate.lecturaPpm != (decimal?)null ? documentDate.lecturaPpm.Value.ToDecimalPresentation().Trim() : "0";
                            txtMinutoEnGasificado.Text = (documentDate.minutos != null ? documentDate.minutos.Value.ToString().Trim() : "0") + " minutos";

                            porcentajeOcupado = Math.Round( ((cantidadJabas / capacidadTotal) * 100),2);
                            txtPorcentajeOcupado.Text = porcentajeOcupado.ToDecimalPresentation().Trim();
                            txtCantidadJabas.Text = cantidadJabas.ToDecimalPresentation().Trim();
                            txtCapacidadAlmacenamiento.Text = capacidadTotal.ToString("N0");


                            if (listadoDetalleFullDate != null)
                            {
                                if (listadoDetalleFullDate.ToList().Count > 0)
                                {                                    
                                    dgvDetalle.CargarDatos(listadoDetalleFullDate.Where(x => x.itemDetalleEnRegistroGasificado > 0).ToList().ToDataTable<SAS_RegistroGasificadoAllByIDResult>());
                                    dgvDetalle.Refresh();
                                    if (modo == "1")
                                    {
                                        btnEditar.Enabled = true;
                                        gbDatosDelProceso.Enabled = false;
                                        gbDetallePallet.Enabled = false;
                                        gbDocumento.Enabled = false;
                                        gbProcedimiento.Enabled = false;
                                        btnRegistrar.Enabled = false;
                                        btnAnular.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                        btnNuevo.Enabled = true;
                                        btnAtras.Enabled = false;
                                    }
                                    else
                                    {
                                        btnEditar.Enabled = true;
                                        gbDatosDelProceso.Enabled = false;
                                        gbDetallePallet.Enabled = false;
                                        gbDocumento.Enabled = false;
                                        gbProcedimiento.Enabled = false;
                                        btnRegistrar.Enabled = false;
                                        btnAnular.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                        btnNuevo.Enabled = true;
                                        btnAtras.Enabled = false;
                                    }
                                }
                            }
                            btnEditar.Enabled = false;
                            if (this.txtNumeroDocumento.Text != "0000000")
                            {
                                btnEditar.Enabled = true;
                            }
                            #endregion
                        }
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            try
            {
                #region Nuevo() 
                CodigoDelRegistro = 0;
                LimpiarControles(gbDatosDelProceso);
                LimpiarControles(gbDetallePallet);
                LimpiarControles(gbDocumento);
                LimpiarControles(gbProcedimiento);
                LimpiarGrilla(dgvDetalle);
                document = new SAS_ListadoDeRegistrosExoneradosByDatesResult();
                documents = new List<SAS_RegistroGasificadoAll>();
                listadoDetalle = new List<IngresoSalidaGasificado>();
                listadoDetalleEliminado = new List<IngresoSalidaGasificado>();
                itemDetalle = new IngresoSalidaGasificado();
                oRegistroGasificado = new SAS_RegistroGasificado();
                oRegistroGasificado.idGasificado = 0;
                documentDate = new SAS_RegistroGasificadoAllByIDResult();
                documentDate.idGasificado = 0;
                listadoDetalleFullDate = new List<SAS_RegistroGasificadoAllByIDResult>();
                dgvDetalle.CargarDatos(listadoDetalleFullDate.ToDataTable<SAS_RegistroGasificadoAllByIDResult>());
                dgvDetalle.Refresh();

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void LimpiarGrilla(MyDataGridViewDetails dgvDetalle)
        {



        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Actualizar Registro() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Editar() 
                if (this.txtEstado.Text != "ANULADO")
                {
                    btnEditar.Enabled = !true;
                    gbDatosDelProceso.Enabled = !false;
                    gbDetallePallet.Enabled = !false;
                    gbDocumento.Enabled = !false;
                    gbProcedimiento.Enabled = !false;
                    btnRegistrar.Enabled = !false;
                    btnAnular.Enabled = !true;
                    btnEliminarRegistro.Enabled = !true;
                    btnNuevo.Enabled = !true;
                    btnAtras.Enabled = !false;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Registrar() 

                if (ValidarRegistro() == true)
                {
                    oRegistroGasificado = new SAS_RegistroGasificado();
                    oRegistroGasificado.idGasificado = Convert.ToInt32(txtCodigo.Text);
                    oRegistroGasificado.horaInyeccion = txtHoraInicioInyeccion.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioInyeccion.Text) : (DateTime?)null;
                    oRegistroGasificado.horaGasificado = txtHoraInicioGasificado.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioGasificado.Text) : (DateTime?)null;
                    oRegistroGasificado.horaVentilacion = txtHoraInicioVentilacion.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioVentilacion.Text) : (DateTime?)null;
                    oRegistroGasificado.fechaSalida = txtHoraFinProceso.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraFinProceso.Text) : (DateTime?)null;
                    oRegistroGasificado.idProductoAplicado = txtProductoAplicadoCodigo.Text != string.Empty ? Convert.ToString(txtProductoAplicadoCodigo.Text) : string.Empty;
                    oRegistroGasificado.dosisSO2 = txtDosisSO2.Text != string.Empty ? Convert.ToDecimal(txtDosisSO2.Text) : 0;
                    oRegistroGasificado.tempAgua = txtTemperaturaDelAgua.Text != string.Empty ? Convert.ToDecimal(txtTemperaturaDelAgua.Text) : 0;
                    oRegistroGasificado.lecturaPpm = txtLecturasEnPPM.Text != string.Empty ? Convert.ToDecimal(txtLecturasEnPPM.Text) : 0;
                    oRegistroGasificado.idCamara = cboCamaraGasificado.SelectedValue.ToString().Trim();
                    oRegistroGasificado.fechaIngreso = txtFecha.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtFecha.Text.Trim()) : (DateTime?)null;
                    // oRegistroGasificado.cantidadJabas = txtCantidadJabas.Text.Trim() != string.Empty ? Convert.ToInt32(txtCantidadJabas.Text.ToString().Trim()) : 0;
                    oRegistroGasificado.estado = this.txtEstado.Text == "ANULADO" ? Convert.ToByte("0") : (this.txtEstado.Text == "Gasificando".ToUpper() ? Convert.ToByte("1") : Convert.ToByte("2"));
                    oRegistroGasificado.productoAplicado = txtProductoAplicado.Text != string.Empty ? Convert.ToString(txtProductoAplicado.Text) : string.Empty;
                    oRegistroGasificado.registradoPor = user2.IdUsuario;

                    #region Detalle()

                    listadoDetalle = new List<IngresoSalidaGasificado>();
                    if (this.dgvDetalle != null)
                    {
                        if (this.dgvDetalle.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                            {
                                if (fila.Cells["chidIngresoSalidaGasificado"].Value.ToString().Trim() != String.Empty)
                                {
                                    try
                                    {
                                        #region Obtener detalle por linea detalle() 
                                        itemDetalle = new IngresoSalidaGasificado();
                                        itemDetalle.idIngresoSalidaGasificado = fila.Cells["chidIngresoSalidaGasificado"].Value != null ? Convert.ToInt32(fila.Cells["chidIngresoSalidaGasificado"].Value.ToString().Trim()) : 0;
                                        itemDetalle.idCamara = cboCamaraGasificado.SelectedValue.ToString();
                                        itemDetalle.itemDetalle = fila.Cells["chitemDetalleEnRegistroGasificado"].Value != null ? Convert.ToInt32(fila.Cells["chitemDetalleEnRegistroGasificado"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                        itemDetalle.tipoRegistro = 'M';
                                        itemDetalle.estado = Convert.ToByte("1");
                                        itemDetalle.tipo = Convert.ToChar("l");
                                        itemDetalle.idGasificado = Convert.ToInt32(txtCodigo.Text.Trim());
                                        itemDetalle.fecha = Convert.ToDateTime(this.txtFecha.Text);
                                        #endregion
                                        listadoDetalle.Add(itemDetalle);
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

                    model = new SAS_RegistroGasificadoController();
                    CodigoDelRegistro = model.ToRegister(conection, oRegistroGasificado, listadoDetalle, listadoDetalleEliminado);
                    documentDate = new SAS_RegistroGasificadoAllByIDResult();
                    documentDate.idGasificado = CodigoDelRegistro;
                    this.txtNumeroDocumento.Text = CodigoDelRegistro.ToString().PadLeft(7, '0');
                    gbDatosDelProceso.Enabled = false;
                    gbDetallePallet.Enabled = false;
                    gbDocumento.Enabled = false;
                    gbProcedimiento.Enabled = false;

                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnRegistrar.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
                    btnExportar.Enabled = false;
                    btnAtras.Enabled = false;

                    modo = "2"; // BLOQUEA CONTROLES Y DESBLOQUEA
                    documentDate = new SAS_RegistroGasificadoAllByIDResult();
                    documentDate.idGasificado = CodigoDelRegistro;

                    MessageBox.Show("Acción realizada correctamente", "Notificación del sistema");

                    bgwHilo.RunWorkerAsync();


                }
                else
                {
                    MessageBox.Show(observacionesPorValidad, "MENSAJE DEL SISTEMA");

                }

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private bool ValidarRegistro()
        {
            observacionesPorValidad = string.Empty;
            bool validacion = true;
            if (cboCamaraGasificado.SelectedValue.ToString() == "000")
            {
                validacion = false;
                observacionesPorValidad += "\n Debe seleccionar una cámara de gasificado ";
                return validacion;
            }
            if (txtHoraInicioInyeccion.Text == txtCajaFechaValidad.Text)
            {
                validacion = false;
                observacionesPorValidad += "\n Debe ingresar fecha de inyeccion en un formato válido ";
                return validacion;
            }
            if (txtHoraInicioGasificado.Text == txtCajaFechaValidad.Text)
            {
                validacion = false;
                observacionesPorValidad += "\n Debe ingresar fecha de gasificado en un formato válido ";
                return validacion;
            }
            if (txtHoraInicioVentilacion.Text == txtCajaFechaValidad.Text)
            {
                validacion = false;
                observacionesPorValidad += "\n Debe ingresar fecha de ventilación en un formato válido ";
                return validacion;
            }
            if (txtHoraFinProceso.Text == txtCajaFechaValidad.Text)
            {
                validacion = false;
                observacionesPorValidad += "\n Debe ingresar fecha de finalización de proceso en un formato válido ";
                return validacion;
            }





            return validacion;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            try
            {
                #region Atras() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                #region Anular Registro() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                #region Eliminar Registro() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                #region Ver Log() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Exportar registro() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnFlujoAprobacion_Click(object sender, EventArgs e)
        {
            try
            {
                #region Flujo de aprobacion() 

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
                #region Adjuntar documentos() 

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
                #region Notificar Registro() 

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
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

        private void RegistroDeIngresoSalidaGasificadoEdicion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGenerarSecuencia_Click(object sender, EventArgs e)
        {

            var asas = this.txtCajaFechaValidad.Text;
            DateTime fecha;
            if (txtHoraInicioInyeccion.Text == asas)
            {
                MessageBox.Show("Debe ingresa una fecha de inyección valida", "MENSAJE DEL SISTEMA");
                return;
            }
            else
            {
                string formatoCorrectoDeFecha = txtHoraInicioInyeccion.Text;
                if (DateTime.TryParseExact(formatoCorrectoDeFecha, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    this.txtHoraInicioGasificado.Text = fecha.AddMinutes(2).ToPresentationDateTime();
                    this.txtHoraInicioVentilacion.Text = fecha.AddMinutes(6).ToPresentationDateTime();
                    this.txtHoraFinProceso.Text = fecha.AddMinutes(10).ToPresentationDateTime();
                    MessageBox.Show("Proceso generado satisfactoriaMente", "MENSAJE DEL SISTEMA");
                    return;
                    // txtMinutoEnGasificado
                }
                else
                {
                    MessageBox.Show("Debe ingresa una fecha de inyección valida", "MENSAJE DEL SISTEMA");
                    return;
                }
            }
        }

        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void AddItem()
        {
            try
            {
                if (dgvDetalle != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chidGasificado                                      
                    array.Add(this.txtFecha.Text.Trim()); // chfechaIngreso
                    array.Add("0"); // chidIngresoSalidaGasificado
                    array.Add(string.Empty); // chitemDetalleEnRegistroGasificado
                    array.Add(string.Empty); // chDOCUMENTO
                    array.Add(string.Empty); // chvariedad
                    array.Add(string.Empty); // chIDCONSUMIDOR
                    array.Add(string.Empty); // chconsumidor
                    array.Add(string.Empty); // chDESCRIPCION
                    array.Add(string.Empty); // chcantidadEnTicket
                    array.Add(string.Empty); // chNROENVIO
                    array.Add(string.Empty); // chguiaDeRemision
                    array.Add(string.Empty); // chfechaRegistroDetalle
                    array.Add(1); // chestadoItemDetalleGasificado                    
                    dgvDetalle.AgregarFila(array);
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

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void DeleteItem()
        {
            if (this.dgvDetalle != null)
            {
                #region
                if (dgvDetalle.CurrentRow != null && dgvDetalle.CurrentRow.Cells["chidIngresoSalidaGasificado"].Value != null)
                {
                    try
                    {
                        Int32 dispositivoCodigo = (dgvDetalle.CurrentRow.Cells["chidIngresoSalidaGasificado"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalle.CurrentRow.Cells["chidIngresoSalidaGasificado"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            if (dispositivoCodigo != 0)
                            {
                                listadoDetalleEliminado.Add(new IngresoSalidaGasificado
                                {
                                    idIngresoSalidaGasificado = dispositivoCodigo
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
                }
                #endregion
            }
        }

        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStateDetail();
        }

        private void ChangeStateDetail()
        {
            try
            {

                if (dgvDetalle.CurrentRow.Cells["chestadoItemDetalleGasificado"].Value.ToString() == "1")
                {
                    dgvDetalle.CurrentRow.Cells["chestadoItemDetalleGasificado"].Value = "0";
                    //dgvDetalle.CurrentRow.Cells["chEstadoIP"].Value = "INACTIVO";
                }
                else
                {
                    dgvDetalle.CurrentRow.Cells["chestadoItemDetalleGasificado"].Value = "1";
                    //dgvDetalle.CurrentRow.Cells["chEstadoIP"].Value = "ACTIVO";
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            numeroDeTicket = 0;
            model = new SAS_RegistroGasificadoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de interface() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chidGasificado" ||
                    ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chDOCUMENTO" ||
                    ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chguiaDeRemision" ||
                    ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chitemDetalleEnRegistroGasificado" ||
                    ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chconsumidor"
                    )
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        // get List of records pending reading
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        DateTime dateQuery = Convert.ToDateTime(this.txtFecha.Text);
                        DateTime dateQueryFinal = Convert.ToDateTime(this.txtHoraFinProceso.Text);

                        search.ListaGeneralResultado = model.GetListOfRecordPendingReading(conection, dateQuery, dateQueryFinal);
                        search.Text = "Buscar registros pendientes de lectura";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;


                            numeroDeTicket = Convert.ToInt32(search.ObjetoRetorno.Codigo);
                            EjecutarConsultaAgregarItemDetalle();
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chitemDetalleEnRegistroGasificado"].Value = search.ObjetoRetorno.Codigo;
                            //AgregarItemDetalleDesdeNumeroTicket(conection, search.ObjetoRetorno.Codigo);
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidGasificado"].Value = (Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0"));
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chfechaIngreso"].Value = itemAdd.fechaRegistro.Value;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidIngresoSalidaGasificado"].Value = 0;
                            //this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chitemDetalleEnRegistroGasificado"].Value = itemAdd.itemDetalle;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDOCUMENTO"].Value = itemAdd.documento;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chvariedad"].Value = itemAdd.variedad;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIDCONSUMIDOR"].Value = itemAdd.idconsumidor;

                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chconsumidor"].Value = itemAdd.consumidor;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDESCRIPCION"].Value = itemAdd.PRODUCTO;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcantidadEnTicket"].Value = itemAdd.cantidadRegistrada.Value;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chNROENVIO"].Value = itemAdd.NROENVIO.Trim();
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chguiaDeRemision"].Value = itemAdd.guiaDeRemision;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chfechaRegistroDetalle"].Value = itemAdd.fechaRegistro.Value;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chestadoItemDetalleGasificado"].Value = 1;

                            numeroDeTicket = 0;
                            itemAdd = new SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult();

                            //gbDatosDelProceso.Enabled = false;
                            //gbDetallePallet.Enabled = false;
                            //gbDocumento.Enabled = false;
                            //gbProcedimiento.Enabled = false;
                            //BarraPrincipal.Enabled = false;
                            //progressBar1.Visible = true;
                            //bgwAgregarItemDetalle.RunWorkerAsync();
                        }
                    }
                }
                #endregion



            }
        }

        private void AgregarItemDetalleDesdeNumeroTicket(string conection, SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult itemAddDetail)
        {
            ArrayList array = new ArrayList();
            array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chidGasificado                                      
            array.Add(itemAddDetail.fechaRegistro.Value.ToShortDateString()); // chfechaIngreso
            array.Add("0"); // chidIngresoSalidaGasificado
            array.Add(itemAddDetail.itemDetalle); // chitemDetalleEnRegistroGasificado
            array.Add(itemAddDetail.documento); // chDOCUMENTO
            array.Add(itemAddDetail.variedad); // chvariedad
            array.Add(itemAddDetail.idconsumidor); // chIDCONSUMIDOR
            array.Add(itemAddDetail.consumidor); // chconsumidor
            array.Add(itemAddDetail.PRODUCTO); // chDESCRIPCION
            array.Add(itemAddDetail.cantidadRegistrada); // chcantidadEnTicket
            array.Add(itemAddDetail.NROENVIO); // chNROENVIO
            array.Add(itemAddDetail.guiaDeRemision); // chguiaDeRemision
            array.Add(itemAddDetail.fechaRegistro.Value.ToShortDateString()); // chfechaRegistroDetalle
            array.Add(1); // chestadoItemDetalleGasificado                    
            dgvDetalle.AgregarFila(array);
        }

        private void bgwAgregarItemDetalle_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaAgregarItemDetalle();
        }

        private void bgwAgregarItemDetalle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            AgregarItemDetalleDesdeNumeroTicket(conection, itemAdd);
            gbDatosDelProceso.Enabled = !false;
            gbDetallePallet.Enabled = !false;
            gbDocumento.Enabled = !false;
            gbProcedimiento.Enabled = !false;
            BarraPrincipal.Enabled = !false;
            progressBar1.Visible = !true;
            numeroDeTicket = 0;
            itemAdd = new SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult();
        }

        private void EjecutarConsultaAgregarItemDetalle()
        {
            try
            {
                #region Acción() 
                model = new SAS_RegistroGasificadoController();
                itemAdd = new SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult();

                itemAdd = model.ObtenerListadoTicketsPendientesDeRegistroByTicket(conection, numeroDeTicket);
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
