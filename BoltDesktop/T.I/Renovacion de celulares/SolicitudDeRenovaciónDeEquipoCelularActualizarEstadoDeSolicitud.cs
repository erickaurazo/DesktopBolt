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
    public partial class SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud : Form
    {
        #region Variables()
        const string nombreformulario = "RenovacionDeEquiposCelulares";
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes;
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

        public SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud()
        {
            InitializeComponent();
        }

        public SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_SolicitudDeRenovacionTelefoniaCelular _solicitud)
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
            CargarObjeto();
            bgwHilo.RunWorkerAsync();
        }

        private void CargarObjeto()
        {
            #region             
            Limpiar(this, gbCabecera);
            Limpiar(this, gbEstados);
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
                   // ((ComboBox)combo).SelectedIndex = 0;
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
                        #region Llenar controles desde el objeto   () 
                        txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                        txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "EN SOLICITUD";
                        txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                        txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                        txtCodigoDocumento.Text = solicitydById.iddocumento != null ? solicitydById.iddocumento.Trim() : "REN";
                        txtSerie.Text = solicitydById.serie != null ? solicitydById.serie.Trim() : "0001";
                        txtMotivo.Text = solicitydById.motivoSolicitud != null ? solicitydById.motivoSolicitud.ToString().Trim() : "RENOVACIÓN";
                        txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                        txtPersonal.Text = solicitydById.nombres != null ? solicitydById.nombres.Trim() : string.Empty;
                        txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                        txtCargo.Text = infoPerson.cargo.ToUpper();
                        cboEstado.SelectedValue = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim().ToUpper().ToUpper() : "SO"; 
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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.estadoCodigo = cboEstado.SelectedValue.ToString();
                modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                CodigoARegistrar = modelo.ChangeStateRequest("SAS", solicitud);
                MessageBox.Show("Se reasigno al colaborador", "MENSAJE DEL SISTEMA");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                documentos = new List<Grupo>();

                documentos = comboHelper.GetListStatesForRequest("SAS");
                cboEstado.DisplayMember = "Descripcion";
                cboEstado.ValueMember = "Codigo";
                cboEstado.DataSource = documentos.ToList();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }




        private void SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud_Load(object sender, EventArgs e)
        {

        }
    }
}
