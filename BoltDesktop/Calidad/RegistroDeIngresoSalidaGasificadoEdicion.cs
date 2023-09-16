﻿using System;
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

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class RegistroDeIngresoSalidaGasificadoEdicion : Form
    {

        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        SAS_ListadoDeRegistrosExoneradosByDatesResult document;
        List<SAS_RegistroGasificadoAll> documents;


        SAS_RegistroGasificadoAllByIDResult documentDate;        
        List<SAS_RegistroGasificadoAllByIDResult> documentsDate;        

        SAS_RegistroGasificadoController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_RegistroGasificado cabecera = new SAS_RegistroGasificado();
        //List<IngresoSalidaGasificado> listadoDetalle = new List<IngresoSalidaGasificado>();
        List<IngresoSalidaGasificado> listadoDetalleEliminado = new List<IngresoSalidaGasificado>();
        List<SAS_RegistroGasificadoAll> listadoDetalleFull = new List<SAS_RegistroGasificadoAll>();
        List<SAS_RegistroGasificadoAllByIDResult> listadoDetalleFullDate = new List<SAS_RegistroGasificadoAllByIDResult>();


        private string observacionesPorValidad = string.Empty;
        private string modo;

        public RegistroDeIngresoSalidaGasificadoEdicion()
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = "NSFAJA";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";
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
                        if (user2.IdUsuario.Trim() == "EAURAZO")
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

            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim() == "EAURAZO")
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

            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim() != string.Empty)
                    {
                        if (user2.IdUsuario.Trim() == "EAURAZO")
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
                        if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ABURGA" || user2.IdUsuario.Trim().ToUpper() == "JCHERO" || user2.IdUsuario.Trim().ToUpper() == "dvaldiviezo".ToUpper() || user2.IdUsuario.Trim().ToUpper() == "HVALENCIA")
                        {
                            btnGenerarSecuencia.Visible = true;
                        }
                    }
                }
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Acción() 
                model = new SAS_RegistroGasificadoController();
                documentsDate = model.GetListRegistroGasificadoByIdGasificado(conection, documentDate.idGasificado);
                listadoDetalleFullDate = new List<SAS_RegistroGasificadoAllByIDResult>();
                if (documentsDate != null)
                {
                    if (documentsDate.ToList().Count > 0)
                    {
                        documentsDate = model.ResumirListadoByIdGasificado(documentsDate);
                        if (documentsDate != null)
                        {
                            if (documentsDate.ToList().Count > 0)
                            {
                                documentDate = documentsDate.ElementAt(0);
                                //listadoDetalle = model.GetDetailList(conection, document.idGasificado);
                                listadoDetalleFullDate = model.GetListRegistroGasificadoByIdGasificado(conection, documentDate.idGasificado);
                            }
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
                            txtUsuarioAsignado.Text = user2.IdUsuario;
                            txtEstado.Text = "GASIFICANDO";
                            txtSucursalCodigo.Text = "002";
                            txtSucursal.Text = "PLANTA - SATURNO";
                            txtCodigo.Text = "0";
                            cboDocumento.SelectedValue = "GAS";
                            cboSerie.SelectedValue = "0001";
                            txtNumeroDocumento.Text = "0".ToString().Trim().PadLeft(7, '0');
                            txtFecha.Text = DateTime.Now.ToPresentationDate();
                            cboCamaraGasificado.SelectedValue = "001";
                            txtHoraInicioInyeccion.Text = DateTime.Now.AddMinutes(2).ToPresentationDate();
                            txtHoraInicioGasificado.Text = DateTime.Now.AddMinutes(6).ToPresentationDate();
                            txtHoraInicioVentilacion.Text = DateTime.Now.AddMinutes(10).ToPresentationDate();
                            txtHoraFinProceso.Text = DateTime.Now.AddMinutes(12).ToPresentationDate();
                            txtProductoAplicadoCodigo.Text = "250600100006";
                            txtProductoAplicado.Text = "SO2";
                            txtDosisSO2.Text = "0";
                            txtTemperaturaDelAgua.Text = "0";
                            txtLecturasEnPPM.Text = "0";
                            txtMinutoEnGasificado.Text = "0 Minutos";
                            #endregion
                        }
                        else
                        {
                            #region Edición de registro() 
                            txtEmpresaCodigo.Text = documentDate.empresaCodigo.Trim();
                            txtEmpresa.Text = documentDate.empresa.Trim();
                            txtUsuarioAsignado.Text = documentDate.registradoPorNombres.Trim();
                            txtEstado.Text = documentDate.estadoRegistroGasificado.Trim();
                            txtSucursalCodigo.Text = documentDate.sucursalCodigo.Trim();
                            txtSucursal.Text = documentDate.sucursal.Trim();
                            txtCodigo.Text = documentDate.idGasificado.ToString().Trim();
                            cboDocumento.SelectedValue = "GAS";
                            cboSerie.SelectedValue = "0001";
                            txtNumeroDocumento.Text = documentDate.idGasificado.ToString().Trim().PadLeft(7, '0');
                            txtFecha.Text = documentDate.FECHA.Value.ToString().Trim();
                            cboCamaraGasificado.SelectedValue = documentDate.idCamara.Trim();
                            txtHoraInicioInyeccion.Text = documentDate.horaInyeccion != null ? documentDate.horaInyeccion.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraInicioGasificado.Text = documentDate.horaGasificado != null ? documentDate.horaGasificado.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraInicioVentilacion.Text = documentDate.horaVentilacion != null ? documentDate.horaVentilacion.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtHoraFinProceso.Text = documentDate.fechaSalida != null ? documentDate.fechaSalida.Value.ToPresentationDateTime().Trim() : string.Empty;
                            txtProductoAplicadoCodigo.Text = documentDate.idProductoAplicado.Trim();
                            txtProductoAplicado.Text = documentDate.productoAplicado != null ? documentDate.productoAplicado.Trim() : string.Empty;
                            txtDosisSO2.Text = documentDate.dosisSO2 != (decimal?)null ? documentDate.dosisSO2.Value.ToDecimalPresentation().Trim() : "0";
                            txtTemperaturaDelAgua.Text = documentDate.tempAgua != (decimal?)null ? documentDate.tempAgua.Value.ToDecimalPresentation().Trim() : "0";
                            txtLecturasEnPPM.Text = documentDate.lecturaPpm != (decimal?)null ? documentDate.lecturaPpm.Value.ToDecimalPresentation().Trim() : "0";
                            txtMinutoEnGasificado.Text = (documentDate.minutos != null ? documentDate.minutos.Value.ToString().Trim() : "0") + " minutos";

                            txtPorcentajeOcupado.Text = "0";
                            txtCantidadJabas.Text = "0";
                            txtCapacidadAlmacenamiento.Text = "600";


                            if (listadoDetalleFullDate != null)
                            {
                                if (listadoDetalleFullDate.ToList().Count > 0)
                                {

                                    decimal capacidadTotal = 600;
                                    decimal cantidadJabas = 0;
                                    decimal porcentajeOcupado = 0;

                                    cantidadJabas = documentDate.cantidadEnTicket != null ? documentDate.cantidadEnTicket.Value : 0;
                                    if (cantidadJabas > 0)
                                    {
                                        porcentajeOcupado = Math.Round(((cantidadJabas / capacidadTotal) * 100), 2);
                                        txtPorcentajeOcupado.Text = porcentajeOcupado.ToDecimalPresentation();
                                        txtCantidadJabas.Text = cantidadJabas.ToDecimalPresentation();
                                    }

                                    //dgvDetalle.CargarDatos(listadoDetalleFull.ToDataTable<SAS_RegistroGasificadoAll>());
                                    dgvDetalle.CargarDatos(listadoDetalleFullDate.ToDataTable<SAS_RegistroGasificadoAllByIDResult>());
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

                                        //gbDatosDelProceso.Enabled = !false;
                                        //gbDetallePallet.Enabled = !false;
                                        //gbDocumento.Enabled = !false;
                                        //gbProcedimiento.Enabled = !false;
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
                                        //gbDatosDelProceso.Enabled = !false;
                                        //gbDetallePallet.Enabled = !false;
                                        //gbDocumento.Enabled = !false;
                                        //gbProcedimiento.Enabled = !false;
                                    }



                                }
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


            try
            {
                #region Acción() 
                LimpiarControles(gbDatosDelProceso);
                LimpiarControles(gbDetallePallet);
                LimpiarControles(gbDocumento);
                LimpiarControles(gbProcedimiento);
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
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
                    SAS_RegistroGasificado oRegistroGasificado = new SAS_RegistroGasificado();
                    oRegistroGasificado.idGasificado = Convert.ToInt32(txtCodigo.Text);
                    oRegistroGasificado.horaInyeccion = txtHoraInicioInyeccion.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioInyeccion.Text) : (DateTime?)null;
                    oRegistroGasificado.horaGasificado = txtHoraInicioGasificado.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioGasificado.Text) : (DateTime?)null;
                    oRegistroGasificado.horaVentilacion = txtHoraInicioVentilacion.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraInicioVentilacion.Text) : (DateTime?)null;
                    oRegistroGasificado.fechaSalida = txtHoraFinProceso.Text != txtCajaFechaValidad.Text ? Convert.ToDateTime(txtHoraFinProceso.Text) : (DateTime?)null;
                    oRegistroGasificado.idProductoAplicado = txtProductoAplicadoCodigo.Text != string.Empty ? Convert.ToString(txtProductoAplicadoCodigo.Text) : string.Empty;
                    oRegistroGasificado.dosisSO2 = txtDosisSO2.Text != string.Empty ? Convert.ToDecimal(txtDosisSO2.Text) : 0;
                    oRegistroGasificado.tempAgua = txtTemperaturaDelAgua.Text != string.Empty ? Convert.ToDecimal(txtTemperaturaDelAgua.Text) : 0;
                    oRegistroGasificado.lecturaPpm = txtLecturasEnPPM.Text != string.Empty ? Convert.ToDecimal(txtLecturasEnPPM.Text) : 0;

                    model = new SAS_RegistroGasificadoController();
                    int Registrar = model.ToRegister(conection, oRegistroGasificado);
                    documentDate = new SAS_RegistroGasificadoAllByIDResult();
                    documentDate.idGasificado = oRegistroGasificado.idGasificado;

                    if (Registrar > 1)
                    {
                        MessageBox.Show("Se ha actualizado correctamente el documento", "Confirmación del sistema");
                    }
                    else
                    {
                        MessageBox.Show("Se ha registrado correctamente el documento", "Confirmación del sistema");

                    }

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
    }
}
