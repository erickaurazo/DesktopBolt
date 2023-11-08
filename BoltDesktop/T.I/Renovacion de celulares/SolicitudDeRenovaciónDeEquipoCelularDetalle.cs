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
    public partial class SolicitudDeRenovaciónDeEquipoCelularDetalle : Form
    {

        #region Variables()
        const string nombreformulario = "RenovacionDeEquiposCelulares";
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoRenovacion;
        private SAS_SolicitudDeRenovacionTelefoniaCelular _solicitud;
        private SAS_SolicitudDeRenovacionTelefoniaCelularController modelo;
        private SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult solicitudByID;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> solicitudesByDate;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoAll> solicitudesAll;
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private int numeroCorrelativoDeCero;
        private int codigoSolicitud = 0;
        private int result;
        private SAS_InfoPersonalController modelInfoPersonal;
        private SAS_InfoPersonal infoPerson;
        private SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult solicitydById;
        private SAS_SolicitudDeRenovacionTelefoniaCelular solicitud;
        private SAS_SolicitudDeRenovacionTelefoniaCelular oSolicitud;
        private int codigoSelecionado;
        private string nombreColaborador = string.Empty;

        public int CodigoARegistrar { get; private set; }


        #endregion

        public SolicitudDeRenovaciónDeEquipoCelularDetalle()
        {
            InitializeComponent();
        }

        public SolicitudDeRenovaciónDeEquipoCelularDetalle(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_SolicitudDeRenovacionTelefoniaCelular _solicitud)
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            solicitud = _solicitud;
            codigoSolicitud = _solicitud.id;

            gbGlosa.Enabled = false;
            CargarObjeto();
            bgwHilo.RunWorkerAsync();
        }

        private void CargarObjeto()
        {
            #region Nuevo
            btnNuevo.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;


            //2.- Limpiar formulario
            Limpiar(this, gbDatosDeLaSolicitud);
            Limpiar(this, gbReferencia);
            Limpiar(this, gbDocumento);
            //Limpiar(this, gbDetalleDispositivo);

            //3.- Cargar objeto y listas en blanco

            //4.- Presentar objetos y listas en los formularios

            #endregion
        }

        public static void Limpiar(Control control, GroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                documentos = new List<Grupo>();
                series = new List<Grupo>();
                tipoSolicitudes = new List<Grupo>();
                tipoRenovacion = new List<Grupo>();


                documentos = comboHelper.GetDocumentTypeForForm("SAS", "Tipo de solicitudes de Renovacion de celulares");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                series = comboHelper.GetDocumentSeriesForForm("SAS", "Equipamiento tecnologico");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();

                tipoSolicitudes = comboHelper.GetRequestTypes("SAS", "Tipo de solicitudes de Renovacion de celulares");
                cboTipoSolicitud.DisplayMember = "Descripcion";
                cboTipoSolicitud.ValueMember = "Codigo";
                cboTipoSolicitud.DataSource = tipoSolicitudes.OrderBy(x => x.Id).ToList();
                cboTipoSolicitud.SelectedValue = "0";

                tipoRenovacion = comboHelper.GetRequGetRequestTypesRenovations("SAS", "Tipo de solicitudes de Renovacion de celulares");
                cboTipoRenovacion.DisplayMember = "Descripcion";
                cboTipoRenovacion.ValueMember = "Codigo";
                cboTipoRenovacion.DataSource = tipoRenovacion.OrderBy(x => x.Id).ToList();
                cboTipoSolicitud.SelectedValue = "00";



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar Consulta()
                modelInfoPersonal = new SAS_InfoPersonalController();
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                solicitydById = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult();

                solicitydById = modelo.GetRequestsById("SAS", codigoSolicitud);

                if (solicitydById != null)
                {
                    if (solicitydById.id == 0)
                    {

                        numeroCorrelativoDeCero = modelo.ObtenerNumeroCorrelativoDeCero("SAS", "SAS_SolicitudDeRenovacionTelefoniaCelular");
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty);
                    }
                    else
                    {

                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());
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
                #region Asignar objetos a controles()
                if (solicitydById != null)
                {
                    if (solicitydById.id > 0)
                    {
                        #region Llenar controles desde el objeto   
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? (solicitydById.idDispositivoBaja > 0 ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty) : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoAlta.Text = solicitydById.dispositivoAlta != null ? solicitydById.dispositivoAlta.ToString().Trim() : string.Empty;
                        txtDispositivoAltaCodigo.Text = solicitydById.idDispositivoAlta != null ? (solicitydById.idDispositivoAlta > 0 ? solicitydById.idDispositivoAlta.ToString().Trim() : string.Empty) : string.Empty;
                        //txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        //txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        txtReferenciaBajaCodigo.Text = solicitydById.idReferenciaBaja != null ? (solicitydById.idReferenciaBaja > 0 ? solicitydById.idReferenciaBaja.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaBajaDescripcion.Text = solicitydById.idReferenciaBaja != null ? (solicitydById.idReferenciaBaja > 0 ? "SOL-0001-" + solicitydById.documentoDeReferenciaBaja.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaAltaCodigo.Text = solicitydById.idReferenciaAlta != null ? (solicitydById.idReferenciaAlta > 0 ? solicitydById.idReferenciaAlta.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaAltaDescripcion.Text = solicitydById.idReferenciaAlta != null ? (solicitydById.idReferenciaAlta > 0 ? "SOL-0001-" + solicitydById.idReferenciaAlta.ToString().Trim().PadLeft(7,'0') : string.Empty) : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? (solicitydById.idReferencia > 0 ? solicitydById.idReferencia.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferencia.Text = solicitydById.idReferencia != null ? (solicitydById.idReferencia > 0 ? "SOL-0001-" + solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty) : string.Empty;

                        cboTipoRenovacion.SelectedValue = solicitydById.justificacionDeReposicionCodigo != null ? solicitydById.justificacionDeReposicionCodigo.Trim() : string.Empty;

                        this.txtGlosa.Text  = solicitydById.glosa != null ? solicitydById.glosa.Trim() : string.Empty;
                        this.txtNota.Text = solicitydById.nota != null ? solicitydById.nota.Trim() : string.Empty;
                        this.txtJustificacion.Text = solicitydById.justificacion != null ? solicitydById.justificacion.Trim() : string.Empty;
                        //cboTipoRenovacion.SelectedValue = solicitydById.justificacionDeReposicionCodigo != null ? solicitydById.justificacionDeReposicionCodigo.Trim() : string.Empty;

                        //txtLineaCelular.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelularCodigo.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelular.Text = string.Empty;

                        if (txtIdEstado.Text == "SO")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = true;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = true;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            gbDocumento.Enabled = false;

                            gbGlosa.Enabled = true;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = true;
                            btnRechazar.Enabled = true;
                            btnFlujoAprobacion.Enabled = true;

                        }
                        else if (this.txtIdEstado.Text == "AN")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;

                            gbDatosDeLaSolicitud.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                            btnAprobar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        else if (this.txtIdEstado.Text == "PE" || this.txtIdEstado.Text == "AP" || this.txtIdEstado.Text == "CT" || this.txtIdEstado.Text == "GR" || this.txtIdEstado.Text == "AT" || this.txtIdEstado.Text == "RE")
                        {

                            btnAprobar.Enabled = false;
                            btnLineaTelefonicaBuscar.Enabled = false;
                            txtLineaCelular.ReadOnly = true;
                            txtLineaCelularCodigo.ReadOnly = true;

                            btnReferenciaEquipamientoBuscar.Enabled = false;
                            txtReferencia.ReadOnly = true;
                            txtReferenciaEquipamientoCodigo.ReadOnly = true;

                            txtSucursal.ReadOnly = true;
                            txtSucursalCodigo.ReadOnly = true;
                            btnSucursal.Enabled = false;

                            btnEmpresa.Enabled = false;
                            txtEmpresa.ReadOnly = true;
                            txtEmpresaCodigo.ReadOnly = true;

                            cboTipoSolicitud.Enabled = false;


                            btnNuevo.Enabled = true;
                            txtPersonal.ReadOnly = true;
                            txtPersonalCodigo.ReadOnly = true;
                            txtDispositivoBaja.ReadOnly = true;
                            txtDispositivoBajaCodigo.ReadOnly = true;
                            btnPersonalBuscar.Enabled = false;
                            btnDispositivoBajaBuscar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;

                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = true;
                            //gbDetalleDispositivo.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                        }
                        else
                        {
                            btnNuevo.Enabled = false;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            //gbDetalleDispositivo.Enabled = false;
                            gbDocumento.Enabled = false;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = false;
                            btnRechazar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Llenar controles desde el objeto   
                        cboTipoRenovacion.SelectedValue = solicitydById.justificacionDeReposicionCodigo != null ? solicitydById.justificacionDeReposicionCodigo.Trim() : string.Empty;
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        btnLineaTelefonicaBuscar.Enabled = true;
                        txtLineaCelular.ReadOnly = false;
                        txtLineaCelularCodigo.ReadOnly = false;

                        btnReferenciaEquipamientoBuscar.Enabled = false;
                        txtReferencia.ReadOnly = true;
                        txtReferenciaEquipamientoCodigo.ReadOnly = true;
                        btnSucursal.Enabled = true;
                        txtSucursal.ReadOnly = false;
                        txtSucursalCodigo.ReadOnly = false;
                        btnEmpresa.Enabled = true;
                        txtEmpresa.ReadOnly = false;
                        txtEmpresaCodigo.ReadOnly = false;
                        cboTipoSolicitud.Enabled = true;
                        txtFecha.Text = DateTime.Now.ToPresentationDate();
                        btnRegistrar.Enabled = true;
                        btnPersonalBuscar.Enabled = true;
                        txtPersonal.ReadOnly = false;
                        txtPersonalCodigo.ReadOnly = false;
                        btnDispositivoBajaBuscar.Enabled = true;
                        txtDispositivoBaja.ReadOnly = false;
                        txtDispositivoBajaCodigo.ReadOnly = false;

                        gbGlosa.Enabled = true;

                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = false;
                        btnAtras.Enabled = true;
                        btnAnular.Enabled = false;
                        btnRegistrar.Enabled = true;
                        gbDatosDeLaSolicitud.Enabled = true;
                        //gbDetalleDispositivo.Enabled = false;
                        gbDocumento.Enabled = true;
                        gbDatosDeLaSolicitud.Enabled = true;
                        gbReferencia.Enabled = false;
                        btnAprobar.Enabled = true;
                        btnRechazar.Enabled = true;
                        btnFlujoAprobacion.Enabled = true;

                        txtEstado.Focus();

                        #endregion                        
                    }

                    BarraPrincipal.Enabled = true;
                    progressBar1.Visible = false;
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
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnAtras.Enabled = true;
            btnAnular.Enabled = false; ;
            btnRegistrar.Enabled = true;
            gbDatosDeLaSolicitud.Enabled = true;
            gbGlosa.Enabled = true;
            //gbDetalleDispositivo.Enabled = true;
            gbDocumento.Enabled = true;
            gbReferencia.Enabled = true;

            Limpiar(this, gbDatosDeLaSolicitud);
            Limpiar(this, gbReferencia);
            Limpiar(this, gbDocumento);
            //Limpiar(this, gbDetalleDispositivo);
            cboTipoSolicitud.SelectedValue = 0;
            txtEstado.Text = "EN SOLICITUD";
            txtIdEstado.Text = "SO";
            codigoSolicitud = 0;
            bgwHilo.RunWorkerAsync();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void Atras()
        {
            CargarObjeto();
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true; ;
            btnRegistrar.Enabled = false;
            gbDatosDeLaSolicitud.Enabled = false;
            //gbDetalleDispositivo.Enabled = false;
            gbDocumento.Enabled = false;
            gbReferencia.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            Editar();

        }

        private void Editar()
        {
            if (this.txtEstado.Text == "EN SOLICITUD")
            {
                txtEstado.Focus();
                btnNuevo.Enabled = true;
                btnEditar.Enabled = false;
                btnAtras.Enabled = true;
                btnAnular.Enabled = false; ;
                btnRegistrar.Enabled = true;
                gbDatosDeLaSolicitud.Enabled = true;
                //gbDetalleDispositivo.Enabled = true;
                gbDocumento.Enabled = true;
                gbGlosa.Enabled = true;
                gbReferencia.Enabled = true;
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para edicion", "MENSAJE DEL SISTEMA");
                return;
            }


        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registar();
        }

        private void Registar()
        {
            nombreColaborador = this.txtPersonal.Text;
            bool validacionOkey = true;
            string mensajeParaMostrar = string.Empty;

            if (cboTipoSolicitud.Text.ToUpper() == ("Devolución".ToUpper()) ||
                cboTipoSolicitud.Text.ToUpper() == "Suspención de equipo y línea".ToUpper() ||
                cboTipoSolicitud.Text.ToUpper() == "Avería".ToUpper() ||
                cboTipoSolicitud.Text.ToUpper() == "Baja".ToUpper() ||
                cboTipoSolicitud.Text.ToUpper() == "Perdida".ToUpper() ||
                cboTipoSolicitud.Text.ToUpper() == "Robo".ToUpper())
            {
                #region Suspención y baja()               
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                if (this.txtLineaCelularCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }

            else if (cboTipoSolicitud.Text.ToUpper() == ("Renovación".ToUpper()))
            {
                #region Renovación()
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                if (this.txtLineaCelularCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                if (this.txtDispositivoBajaCodigo.Text == string.Empty)
                {
                    if (chkCompletarCreacionDeDispositivos.Checked != true)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar el dispositivo de baja";
                    }

                }
                if (this.txtDispositivoBaja.Text == string.Empty)
                {
                    if (chkCompletarCreacionDeDispositivos.Checked != true)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar el dispositivo de baja";
                    }
                }
                #endregion

            }


            else if (cboTipoSolicitud.Text.ToUpper() == ("Suspención de línea".ToUpper()) || cboTipoSolicitud.Text.ToUpper() == (" Linea | Baja".ToUpper()))
            {
                #region Suspencion de línea | "Linea | Baja"
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                if (this.txtLineaCelularCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }
            else if (cboTipoSolicitud.Text.ToUpper().Contains("Suspención de Equipo".ToUpper()))
            {
                #region Suspencion de equipo()
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                //if (this.txtLineaCelularCodigo.Text == string.Empty)
                //{
                //    validacionOkey = false;
                //    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                //}
                if (this.txtDispositivoBajaCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de baja";
                }
                if (this.txtDispositivoBaja.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de baja";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }
            else if (cboTipoSolicitud.Text.ToUpper().Contains("Linea | Alta".ToUpper()))
            {
                #region "Linea | Alta"
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                if (this.txtLineaCelularCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                }
                if (this.txtDispositivoAltaCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                }
                if (this.txtDispositivoAlta.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }
            else if (cboTipoSolicitud.Text.ToUpper().Contains("Prestamo | Equipo".ToUpper()))
            {
                #region "Prestamo equipo"
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                //if (this.txtLineaCelularCodigo.Text == string.Empty)
                //{
                //    validacionOkey = false;
                //    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                //}
                if (this.txtDispositivoAltaCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                }
                if (this.txtDispositivoAlta.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                }
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }
            else if (cboTipoSolicitud.Text.ToUpper().Contains("Duplicado de Chip".ToUpper()))
            {
                #region "Devolucion"
                if (this.txtPersonalCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                }
                if (this.txtLineaCelularCodigo.Text == string.Empty)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                }
                //if (this.txtDispositivoAltaCodigo.Text == string.Empty)
                //{
                //    validacionOkey = false;
                //    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                //}
                //if (this.txtDispositivoAlta.Text == string.Empty)
                //{
                //    validacionOkey = false;
                //    mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                //}
                if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                {
                    validacionOkey = false;
                    mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                }
                #endregion
            }


            else
            {
                #region Cuando no es baja o suspención()
                if (txtEstado.Text.ToUpper() == "En solicitud".ToUpper())
                {
                    #region Cuando  esta estado de solicitud el documento
                    if (this.txtPersonalCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                    }
                    if (this.txtLineaCelularCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                    }
                    if (this.txtDispositivoAltaCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                    }
                    if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                    }

                    #endregion
                }
                else
                {
                    #region Cuando no esta estado de solicitud el documento
                    if (this.txtPersonalCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar código de colaborador";
                    }
                    if (this.txtLineaCelularCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar la línea a renovar";
                    }
                    if (this.txtDispositivoBajaCodigo.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar el dispositivo de baja";
                    }
                    if (Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString()) == 0)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nDebe seleccionar motivo para la renovación";
                    }
                    if (this.txtDispositivoAlta.Text == string.Empty)
                    {
                        validacionOkey = false;
                        mensajeParaMostrar += " \nFalta ingresar el dispositivo de alta";
                    }
                    #endregion
                }
                #endregion
            }


            if (validacionOkey == true)
            {
                progressBar1.Visible = true;
                btnRegistrar.Enabled = false;

                solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.idCodigoGeneral = this.txtPersonalCodigo.Text != string.Empty ? Convert.ToString(this.txtPersonalCodigo.Text) : string.Empty;
                solicitud.idEmpresa = this.txtEmpresaCodigo.Text != string.Empty ? Convert.ToString(this.txtEmpresaCodigo.Text) : string.Empty;
                solicitud.idSucursal = this.txtSucursalCodigo.Text != string.Empty ? Convert.ToString(this.txtSucursalCodigo.Text) : string.Empty;
                solicitud.serie = cboSerie.SelectedValue.ToString();
                solicitud.iddocumento = cboDocumento.SelectedValue.ToString();
                solicitud.fecha = this.txtFecha.Text != string.Empty ? Convert.ToDateTime(this.txtFecha.Text) : DateTime.Now;
                solicitud.estadoCodigo = this.txtIdEstado.Text != string.Empty ? Convert.ToString(this.txtIdEstado.Text) : "AN";
                solicitud.numeroCelular = this.txtLineaCelularCodigo.Text != string.Empty ? Convert.ToString(this.txtLineaCelularCodigo.Text) : string.Empty;
                solicitud.idDispositivoBaja = this.txtDispositivoBajaCodigo.Text != string.Empty ? Convert.ToInt32(this.txtDispositivoBajaCodigo.Text) : (int?)null;
                solicitud.idDispositivoAlta = this.txtDispositivoAltaCodigo.Text != string.Empty ? Convert.ToInt32(this.txtDispositivoAltaCodigo.Text) : (int?)null;
                solicitud.idReferencia = this.txtReferenciaEquipamientoCodigo.Text != string.Empty ? Convert.ToInt32(this.txtReferenciaEquipamientoCodigo.Text) : (int?)null;
                solicitud.idReferenciaAlta = this.txtReferenciaAltaCodigo.Text != string.Empty ? Convert.ToInt32(this.txtReferenciaAltaCodigo.Text) : (int?)null;
                solicitud.idReferenciaBaja = this.txtReferenciaBajaCodigo.Text != string.Empty ? Convert.ToInt32(this.txtReferenciaBajaCodigo.Text) : (int?)null;
                solicitud.usuarioEnAtencion = user2.IdUsuario;
                solicitud.motivoCodigo = Convert.ToInt32(cboTipoSolicitud.SelectedValue.ToString());
                solicitud.fechaCreacion = DateTime.Now;
                solicitud.estacionDeTrabajo = Environment.UserName.Trim();
                solicitud.glosa = this.txtGlosa.Text.Trim();
                solicitud.nota = this.txtNota.Text.Trim();
                solicitud.justificacion = this.txtJustificacion.Text.Trim();
                solicitud.justificacionDeReposicion = cboTipoRenovacion.SelectedValue.ToString().Trim();


                //CargarObjeto();
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnAtras.Enabled = false;
                btnAnular.Enabled = true; ;
                btnRegistrar.Enabled = false;
                gbDatosDeLaSolicitud.Enabled = false;
                gbGlosa.Enabled = false;
                //gbDetalleDispositivo.Enabled = false;
                gbDocumento.Enabled = false;
                gbReferencia.Enabled = false;
                bgwRegistrar.RunWorkerAsync();

            }
            else
            {
                MessageBox.Show(mensajeParaMostrar, "ADVERTENCIA DEL SISTEMA");
            }



        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void Anular()
        {
            CargarObjeto();
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true; ;
            btnRegistrar.Enabled = false;
            gbDatosDeLaSolicitud.Enabled = false;
            //gbDetalleDispositivo.Enabled = false;
            gbDocumento.Enabled = false;
            gbGlosa.Enabled = false;
            gbReferencia.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            CargarObjeto();
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true; ;
            btnRegistrar.Enabled = false;
            gbDatosDeLaSolicitud.Enabled = false;
            //gbDetalleDispositivo.Enabled = false;
            gbDocumento.Enabled = false;
            gbGlosa.Enabled = false;
            gbReferencia.Enabled = false;
            bgwHilo.RunWorkerAsync();
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void SolicitudDeRenovaciónDeEquipoCelularDetalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void txtPersonalCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                modelInfoPersonal = new SAS_InfoPersonalController();
                infoPerson = new SAS_InfoPersonal();
                infoPerson = modelInfoPersonal.GetInfoById("SAS", this.txtPersonalCodigo.Text.Trim());
                this.txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                this.txtCargo.Text = infoPerson.cargo.ToUpper();
                btnLineaTelefonicaBuscar.P_TablaConsulta = "SAS_LineasCelularesCoporativas  where estado = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                btnDispositivoBajaBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivosAsociadosAColaboradores where tipoDispositivoCodigo in ('006', '042', '005', '020','041') and estadoItem = 1 and funcionamiento = 1 and estadoDedispositivo = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                btnDispositivoAltaBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivosAsociadosAColaboradores where tipoDispositivoCodigo in ('006', '042', '005', '020','041') and estadoItem = 1 and funcionamiento = 1 and estadoDedispositivo = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";

                //btnDispositivoBajaBuscar.P_TablaConsulta = "SAS_ListadoColaboradoresByDispositivo where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                //btnDispositivoAltaBuscar.P_TablaConsulta = "SAS_ListadoColaboradoresByDispositivo where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnFlujoAprobación_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "SO")
            {
                this.btnRechazar.Enabled = true;
                this.btnAprobar.Enabled = true;
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Visto de Jefatura");
            try
            {
                solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                //solicitud.estadoCodigo = "PE";
                //this.txtEstado.Text = "PENDIENTE";
                progressBar1.Visible = true;
                btnRegistrar.Enabled = false;
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultado = modelo.ApproveRequest("SAS", solicitud, user2);
                MessageBox.Show(resultado, "CONFIRMACION DEL SISTEMA");
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "RESPUESTA DEL SISTEMA");
                return;
            }

        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Visto de Jefatura");
            try
            {
                solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                //solicitud.estadoCodigo = "PE";
                //this.txtEstado.Text = "PENDIENTE";
                progressBar1.Visible = true;
                btnRegistrar.Enabled = false;
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                int resultado = modelo.RejectRequest("SAS", solicitud);
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        private void bgwFlujoAprobacion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar Consulta()
                // actualizar el estado


                // volver a obtener el objerto
                modelInfoPersonal = new SAS_InfoPersonalController();
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                solicitydById = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult();

                solicitydById = modelo.GetRequestsById("SAS", codigoSolicitud);

                if (solicitydById != null)
                {
                    if (solicitydById.id == 0)
                    {

                        numeroCorrelativoDeCero = modelo.ObtenerNumeroCorrelativoDeCero("SAS", "SAS_SolicitudDeRenovacionTelefoniaCelular");
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty);
                    }
                    else
                    {

                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());
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

        private void bgwFlujoAprobacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Asignar objetos a controles()
                if (solicitydById != null)
                {
                    if (solicitydById.id > 0)
                    {
                        #region Llenar controles desde el objeto   
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        //txtLineaCelular.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelularCodigo.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelular.Text = string.Empty;

                        if (txtIdEstado.Text == "SO")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = true;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = true;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            //gbDetalleDispositivo.Enabled = false;
                            gbDocumento.Enabled = false;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = true;
                            btnRechazar.Enabled = true;
                            btnFlujoAprobacion.Enabled = true;

                        }
                        else if (this.txtIdEstado.Text == "AN")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = true;
                            //gbDetalleDispositivo.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                            btnAprobar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        else if (this.txtIdEstado.Text == "PE" || this.txtIdEstado.Text == "AP" || this.txtIdEstado.Text == "CT" || this.txtIdEstado.Text == "GR" || this.txtIdEstado.Text == "AT" || this.txtIdEstado.Text == "RE")
                        {

                            btnAprobar.Enabled = false;
                            btnLineaTelefonicaBuscar.Enabled = false;
                            txtLineaCelular.ReadOnly = true;
                            txtLineaCelularCodigo.ReadOnly = true;

                            btnReferenciaEquipamientoBuscar.Enabled = false;
                            txtReferencia.ReadOnly = true;
                            txtReferenciaEquipamientoCodigo.ReadOnly = true;

                            txtSucursal.ReadOnly = true;
                            txtSucursalCodigo.ReadOnly = true;
                            btnSucursal.Enabled = false;

                            btnEmpresa.Enabled = false;
                            txtEmpresa.ReadOnly = true;
                            txtEmpresaCodigo.ReadOnly = true;

                            cboTipoSolicitud.Enabled = false;


                            btnNuevo.Enabled = true;
                            txtPersonal.ReadOnly = true;
                            txtPersonalCodigo.ReadOnly = true;
                            txtDispositivoBaja.ReadOnly = true;
                            txtDispositivoBajaCodigo.ReadOnly = true;
                            btnPersonalBuscar.Enabled = false;
                            btnDispositivoBajaBuscar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;

                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = true;
                            //gbDetalleDispositivo.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                        }
                        else
                        {
                            btnNuevo.Enabled = false;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            //gbDetalleDispositivo.Enabled = false;
                            gbDocumento.Enabled = false;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = false;
                            btnRechazar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Llenar controles desde el objeto   
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        btnLineaTelefonicaBuscar.Enabled = true;
                        txtLineaCelular.ReadOnly = false;
                        txtLineaCelularCodigo.ReadOnly = false;

                        btnReferenciaEquipamientoBuscar.Enabled = false;
                        txtReferencia.ReadOnly = true;
                        txtReferenciaEquipamientoCodigo.ReadOnly = true;
                        btnSucursal.Enabled = true;
                        txtSucursal.ReadOnly = false;
                        txtSucursalCodigo.ReadOnly = false;
                        btnEmpresa.Enabled = true;
                        txtEmpresa.ReadOnly = false;
                        txtEmpresaCodigo.ReadOnly = false;
                        cboTipoSolicitud.Enabled = true;
                        txtFecha.Text = DateTime.Now.ToPresentationDate();
                        btnRegistrar.Enabled = true;
                        btnPersonalBuscar.Enabled = true;
                        txtPersonal.ReadOnly = false;
                        txtPersonalCodigo.ReadOnly = false;
                        btnDispositivoBajaBuscar.Enabled = true;
                        txtDispositivoBaja.ReadOnly = false;
                        txtDispositivoBajaCodigo.ReadOnly = false;


                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;
                        btnAnular.Enabled = true;
                        btnRegistrar.Enabled = false;
                        gbDatosDeLaSolicitud.Enabled = false;
                        //gbDetalleDispositivo.Enabled = false;
                        gbDocumento.Enabled = false;
                        gbReferencia.Enabled = false;
                        btnAprobar.Enabled = true;
                        btnRechazar.Enabled = true;
                        btnFlujoAprobacion.Enabled = true;

                        txtEstado.Focus();

                        #endregion                        
                    }

                    BarraPrincipal.Enabled = true;
                    progressBar1.Visible = false;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar registro de la consulta()

                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                CodigoARegistrar = modelo.ToRegister("SAS", solicitud, user2, nombreColaborador);
                codigoSolicitud = CodigoARegistrar;
                modelInfoPersonal = new SAS_InfoPersonalController();
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                solicitydById = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult();

                solicitydById = modelo.GetRequestsById("SAS", codigoSolicitud);

                if (solicitydById != null)
                {
                    if (solicitydById.id == 0)
                    {

                        numeroCorrelativoDeCero = modelo.ObtenerNumeroCorrelativoDeCero("SAS", "SAS_SolicitudDeRenovacionTelefoniaCelular");
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty);
                    }
                    else
                    {

                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());
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

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Asignar objetos a controles()
                if (solicitydById != null)
                {
                    if (solicitydById.id > 0)
                    {
                        #region Llenar controles desde el objeto   
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoAlta.Text = solicitydById.dispositivoAlta != null ? solicitydById.dispositivoAlta.ToString().Trim() : string.Empty;
                        txtDispositivoAltaCodigo.Text = solicitydById.idDispositivoAlta != null ? (solicitydById.idDispositivoAlta > 0 ? solicitydById.idDispositivoAlta.ToString().Trim() : string.Empty) : string.Empty;
                        //txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        //txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        txtReferenciaBajaCodigo.Text = solicitydById.idReferenciaBaja != null ? (solicitydById.idReferenciaBaja > 0 ? solicitydById.idReferenciaBaja.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaBajaDescripcion.Text = solicitydById.idReferenciaBaja != null ? (solicitydById.idReferenciaBaja > 0 ? "SOL-0001-" + solicitydById.documentoDeReferenciaBaja.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaAltaCodigo.Text = solicitydById.idReferenciaAlta != null ? (solicitydById.idReferenciaAlta > 0 ? solicitydById.idReferenciaAlta.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaAltaDescripcion.Text = solicitydById.idReferenciaAlta != null ? (solicitydById.idReferenciaAlta > 0 ? "SOL-0001-" + solicitydById.idReferenciaAlta.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? (solicitydById.idReferencia > 0 ? solicitydById.idReferencia.ToString().Trim() : string.Empty) : string.Empty;
                        txtReferencia.Text = solicitydById.idReferencia != null ? (solicitydById.idReferencia > 0 ? "SOL-0001-" + solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty) : string.Empty;


                        //txtLineaCelular.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelularCodigo.Text = solicitydById.numeroCelular != null ? solicitydById.numeroCelular.ToString().Trim() : string.Empty;
                        txtLineaCelular.Text = string.Empty;

                        if (txtIdEstado.Text == "SO")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = true;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = true;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            //gbDetalleDispositivo.Enabled = false;
                            gbDocumento.Enabled = false;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = true;
                            btnRechazar.Enabled = true;
                            btnFlujoAprobacion.Enabled = true;

                        }
                        else if (this.txtIdEstado.Text == "AN")
                        {
                            btnNuevo.Enabled = true;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = true;
                            //gbDetalleDispositivo.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                            btnAprobar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        else if (this.txtIdEstado.Text == "PE" || this.txtIdEstado.Text == "AP" || this.txtIdEstado.Text == "CT" || this.txtIdEstado.Text == "GR" || this.txtIdEstado.Text == "AT" || this.txtIdEstado.Text == "RE")
                        {

                            btnAprobar.Enabled = false;
                            btnLineaTelefonicaBuscar.Enabled = false;
                            txtLineaCelular.ReadOnly = true;
                            txtLineaCelularCodigo.ReadOnly = true;

                            btnReferenciaEquipamientoBuscar.Enabled = false;
                            txtReferencia.ReadOnly = true;
                            txtReferenciaEquipamientoCodigo.ReadOnly = true;

                            txtSucursal.ReadOnly = true;
                            txtSucursalCodigo.ReadOnly = true;
                            btnSucursal.Enabled = false;

                            btnEmpresa.Enabled = false;
                            txtEmpresa.ReadOnly = true;
                            txtEmpresaCodigo.ReadOnly = true;

                            cboTipoSolicitud.Enabled = false;


                            btnNuevo.Enabled = true;
                            txtPersonal.ReadOnly = true;
                            txtPersonalCodigo.ReadOnly = true;
                            txtDispositivoBaja.ReadOnly = true;
                            txtDispositivoBajaCodigo.ReadOnly = true;
                            btnPersonalBuscar.Enabled = false;
                            btnDispositivoBajaBuscar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;

                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = true;
                            //gbDetalleDispositivo.Enabled = true;
                            gbDocumento.Enabled = true;
                            gbReferencia.Enabled = true;
                        }
                        else
                        {
                            btnNuevo.Enabled = false;
                            btnEditar.Enabled = false;
                            btnAtras.Enabled = false;
                            btnAnular.Enabled = false;
                            btnRegistrar.Enabled = false;
                            gbDatosDeLaSolicitud.Enabled = false;
                            //gbDetalleDispositivo.Enabled = false;
                            gbDocumento.Enabled = false;
                            gbReferencia.Enabled = false;
                            btnAprobar.Enabled = false;
                            btnRechazar.Enabled = false;
                            btnFlujoAprobacion.Enabled = false;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Llenar controles desde el objeto   
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEmpresaCodigo.Text = solicitydById.idEmpresa != null ? solicitydById.idEmpresa.ToString().Trim() : "001";
                        txtEmpresa.Text = solicitydById.empresa != null ? solicitydById.empresa.ToString().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        txtSucursalCodigo.Text = solicitydById.idSucursal != null ? solicitydById.idSucursal.ToString().Trim() : "001";
                        txtSucursal.Text = solicitydById.sucursal != null ? solicitydById.sucursal.ToString().Trim() : "SEDE LOGISTICA AGRICOLA";
                        txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "SO";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        cboDocumento.SelectedValue = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        cboSerie.SelectedValue = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        cboTipoSolicitud.SelectedValue = solicitydById.motivoCodigo != null ? solicitydById.motivoCodigo.Value.ToString().Trim() : "0";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        txtDispositivoBajaCodigo.Text = solicitydById.idDispositivoBaja != null ? solicitydById.idDispositivoBaja.ToString().Trim() : string.Empty;
                        txtDispositivoBaja.Text = solicitydById.dispositivoBaja != null ? solicitydById.dispositivoBaja.ToString().Trim() : string.Empty;
                        txtReferenciaEquipamientoCodigo.Text = solicitydById.idReferencia != null ? solicitydById.idReferencia.ToString().Trim() : string.Empty;
                        txtReferencia.Text = solicitydById.documentoDeReferencia != null ? solicitydById.documentoDeReferencia.ToString().Trim() : string.Empty;

                        btnLineaTelefonicaBuscar.Enabled = true;
                        txtLineaCelular.ReadOnly = false;
                        txtLineaCelularCodigo.ReadOnly = false;

                        btnReferenciaEquipamientoBuscar.Enabled = false;
                        txtReferencia.ReadOnly = true;
                        txtReferenciaEquipamientoCodigo.ReadOnly = true;
                        btnSucursal.Enabled = true;
                        txtSucursal.ReadOnly = false;
                        txtSucursalCodigo.ReadOnly = false;
                        btnEmpresa.Enabled = true;
                        txtEmpresa.ReadOnly = false;
                        txtEmpresaCodigo.ReadOnly = false;
                        cboTipoSolicitud.Enabled = true;
                        txtFecha.Text = DateTime.Now.ToPresentationDate();
                        btnRegistrar.Enabled = true;
                        btnPersonalBuscar.Enabled = true;
                        txtPersonal.ReadOnly = false;
                        txtPersonalCodigo.ReadOnly = false;
                        btnDispositivoBajaBuscar.Enabled = true;
                        txtDispositivoBaja.ReadOnly = false;
                        txtDispositivoBajaCodigo.ReadOnly = false;


                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;
                        btnAnular.Enabled = true;
                        btnRegistrar.Enabled = false;
                        gbDatosDeLaSolicitud.Enabled = false;
                        //gbDetalleDispositivo.Enabled = false;
                        gbDocumento.Enabled = false;
                        gbReferencia.Enabled = false;
                        btnAprobar.Enabled = true;
                        btnRechazar.Enabled = true;
                        btnFlujoAprobacion.Enabled = true;

                        txtEstado.Focus();

                        #endregion                        
                    }

                    MessageBox.Show("Se ha registrado correctamente el registro " + CodigoARegistrar.ToString(), "MENSAJE DEL SISTEMA");
                    BarraPrincipal.Enabled = true;
                    progressBar1.Visible = false;
                    nombreColaborador = string.Empty;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void txtPersonalCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // filtrar sólo las líneas asociadas al colaborador
                btnLineaTelefonicaBuscar.P_TablaConsulta = "SAS_LineasCelularesCoporativas  where estado = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                btnDispositivoBajaBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivosAsociadosAColaboradores where tipoDispositivoCodigo in ('006', '042', '005', '020','041') and estadoItem = 1 and funcionamiento = 1 and estadoDedispositivo = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                btnDispositivoAltaBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivosAsociadosAColaboradores where tipoDispositivoCodigo in ('006', '042', '005', '020','041') and estadoItem = 1 and funcionamiento = 1 and estadoDedispositivo = 1 and  idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";

                //btnDispositivoBajaBuscar.P_TablaConsulta = "SAS_ListadoColaboradoresByDispositivo where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                //btnDispositivoAltaBuscar.P_TablaConsulta = "SAS_ListadoColaboradoresByDispositivo where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                //SAS_ListadoDeDispositivosAsociadosAColaboradores col where tipoDispositivoCodigo  in ('006', '042', '005', '020') and estadoItem = 1 and funcionamiento = 1 and estadoDedispositivo = 1
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }
        }

        private void cboTipoSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTipoRenovacion.Enabled = false;
            cboTipoRenovacion.SelectedValue = "00";

            if (cboTipoSolicitud.SelectedIndex >= 0)
            {
                if (cboTipoSolicitud.Text.ToUpper() == "Renovación".ToUpper())
                {

                    LimpiarControlesPorMotivoDeAsignacíonRenovacion();

                    cboTipoRenovacion.Enabled = true;

                }


                else if (cboTipoSolicitud.Text.ToUpper() == "Avería".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Perdida".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }


                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Robo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }


                }

                else if (cboTipoSolicitud.Text.ToUpper() == " Suspención de Equipo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == " Baja".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }


                }



                else if (cboTipoSolicitud.Text.ToUpper() == "Alta".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeAsignacíon();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Préstamo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeAsignacíon();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Prestamo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeAsignacíon();
                    }
                }


                else if (cboTipoSolicitud.Text.ToUpper() == " Suspención de línea".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }


                }

                else if (cboTipoSolicitud.Text.ToUpper() == " Suspencion de línea".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }


                }


                else if (cboTipoSolicitud.Text.ToUpper() == " Suspención de equipo y línea".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == " Suspención de equipo y linea".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }


                else if (cboTipoSolicitud.Text.ToUpper() == "Nueva asignación".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeAsignacíon();
                    }
                }


                else if (cboTipoSolicitud.Text.ToUpper() == "Nueva asignacion".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeAsignacíon();
                    }
                }


                else if (cboTipoSolicitud.Text.ToUpper() == "Linea | Alta".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorDuplicado();

                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Linea | Baja".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }
                else if (cboTipoSolicitud.Text.ToUpper() == "Devolución".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Devolucion".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBajaLinea();
                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Prestamo | Equipo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorPrestamoEquipo();

                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Préstamo | Equipo".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorPrestamoEquipo();

                    }
                }

                else if (cboTipoSolicitud.Text.ToUpper() == "Duplicado de Chip".ToUpper())
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorDuplicado();

                    }
                }

                else
                {
                    if (txtIdEstado.Text == "SO")
                    {
                        LimpiarControlesPorMotivoDeBaja();
                    }
                }
            }
        }

        private void LimpiarControlesPorMotivoDeAsignacíon()
        {
            btnDispositivoBajaBuscar.Enabled = false;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBaja.ReadOnly = true;
            txtDispositivoBajaCodigo.Clear();
            txtDispositivoBaja.Clear();

            btnDispositivoAltaBuscar.Enabled = true;
            txtDispositivoAltaCodigo.ReadOnly = false;
            txtDispositivoAlta.ReadOnly = false;

        }

        private void LimpiarControlesPorMotivoDeAsignacíonLinea()
        {
            btnDispositivoBajaBuscar.Enabled = false;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBajaCodigo.Clear();
            txtDispositivoBaja.ReadOnly = true;
            txtDispositivoBaja.Clear();

            btnDispositivoAltaBuscar.Enabled = true;
            txtDispositivoAltaCodigo.ReadOnly = true;
            txtDispositivoAltaCodigo.Clear();
            txtDispositivoAlta.ReadOnly = true;
            txtDispositivoAlta.Clear();

        }


        private void LimpiarControlesPorPrestamoEquipo()
        {
            btnDispositivoBajaBuscar.Enabled = false;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBajaCodigo.Clear();
            txtDispositivoBaja.ReadOnly = true;
            txtDispositivoBaja.Clear();


            btnLineaTelefonicaBuscar.Enabled = false;

            txtLineaCelularCodigo.ReadOnly = true;
            txtLineaCelularCodigo.Clear();

            txtLineaCelular.ReadOnly = true;
            txtLineaCelular.Clear();

            btnDispositivoAltaBuscar.Enabled = true;
            txtDispositivoAltaCodigo.ReadOnly = false;
            txtDispositivoAltaCodigo.Clear();
            txtDispositivoAlta.ReadOnly = true;
            txtDispositivoAlta.Clear();

        }

        private void LimpiarControlesPorDuplicado()
        {
            btnDispositivoBajaBuscar.Enabled = false;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBajaCodigo.Clear();
            txtDispositivoBaja.ReadOnly = true;
            txtDispositivoBaja.Clear();

            btnDispositivoAltaBuscar.Enabled = false;
            txtDispositivoAltaCodigo.ReadOnly = true;
            txtDispositivoAltaCodigo.Clear();
            txtDispositivoAlta.ReadOnly = true;
            txtDispositivoAlta.Clear();

        }

        private void LimpiarControlesPorMotivoDeAsignacíonRenovacion()
        {

            if (this.txtCodigo.Text == "0")
            {
                txtDispositivoBajaCodigo.Clear();
                txtDispositivoBaja.Clear();
            }

            btnDispositivoBajaBuscar.Enabled = true;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBaja.ReadOnly = false;

            btnDispositivoAltaBuscar.Enabled = true;
            txtDispositivoAltaCodigo.ReadOnly = true;
            txtDispositivoAlta.ReadOnly = false;

        }



        private void LimpiarControlesPorMotivoDeBaja()
        {
            btnDispositivoBajaBuscar.Enabled = false;
            txtDispositivoBajaCodigo.ReadOnly = true;
            txtDispositivoBajaCodigo.Clear();
            txtDispositivoBaja.ReadOnly = true;
            txtDispositivoBaja.Clear();

            btnDispositivoAltaBuscar.Enabled = true;
            txtDispositivoAltaCodigo.ReadOnly = true;
            txtDispositivoAltaCodigo.Clear();
            txtDispositivoAlta.ReadOnly = true;
            txtDispositivoAlta.Clear();

        }

        private void LimpiarControlesPorMotivoDeBajaLinea()
        {
            btnDispositivoBajaBuscar.Enabled = true;
            txtDispositivoBajaCodigo.ReadOnly = false;
            txtDispositivoBaja.ReadOnly = false;

            btnDispositivoAltaBuscar.Enabled = false;
            txtDispositivoAltaCodigo.ReadOnly = true;
            txtDispositivoAlta.ReadOnly = true;
            txtDispositivoAltaCodigo.Clear();
            txtDispositivoAlta.Clear();

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
                        gbDocumento.Enabled = false;
                        gbReferencia.Enabled = false;
                        gbDatosDeLaSolicitud.Enabled = false;
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
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                modelo.Notify(conection, "soporte@saturno.net.pe", "Solicitud de renovación de celulares", codigoSelecionado);

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
                gbDocumento.Enabled = !false;
                gbReferencia.Enabled = !false;
                gbDatosDeLaSolicitud.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbDocumento.Enabled = !false;
                gbReferencia.Enabled = !false;
                gbDatosDeLaSolicitud.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
                return;
            }
        }

        private void btnDispositivoBajaBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnFlujoAprobacion_Click(object sender, EventArgs e)
        {

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

        private void SolicitudDeRenovaciónDeEquipoCelularDetalle_Load(object sender, EventArgs e)
        {

        }
    }
}