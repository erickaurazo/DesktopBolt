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
using ComparativoHorasVisualSATNISIRA.T.I.ReportesEntregaDevolucion;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class SolicitudDeEquipamientoTecnologicoMantenimiento : Form
    {
        #region Variables()
        string nombreformulario = "EquipamientoTecnologico";
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes;
        private SAS_SolicitudDeEquipamientoTecnologico solicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> sedesEnSolicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> HardwareEnSolicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> SoftwareEnSolicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult> listadoHardwareEnBlanco;
        private List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult> listadoSedesEnBlanco;
        private List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult> listadoSoftwareEnBlanco;
        private List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> listadoLineaCelularEnBlanco;

        private List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult> listadoHardwareById;
        private SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult solicitydById;
        private List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult> listadoSedesById;
        private List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult> listadoSoftwareById;
        private List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> listadoLineaCelularById;

        private SAS_SolicitudDeEquipamientoTecnologicoController modelo;
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> sedesEnSolicitudRegistro;
        private int numeroCorrelativoDeCero;
        private int result;
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareARegistrar;
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareARegistrar;
        private List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
        private SAS_InfoPersonalController modelInfoPersonal;
        private SAS_InfoPersonal infoPerson;
        private int idSolicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular> listadoLineaCelularARegistrar;
        private List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular> listadoLineaCelularAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular>();
        private int codigoSelecionado;
        private int ultimoItemEnListaDetalleHardware, ultimoItemEnListaDetalleSofware, ultimoItemEnListaDetalleCelulares = 1;
        public List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> HardwareEnSolicitudRegistro;
        public List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> SoftwareEnSolicitudRegistro;
        public List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> HardwareEnSolicitudRegistroEliminar;
        public List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> SoftwareEnSolicitudRegistroEliminar;
        #endregion

        public SolicitudDeEquipamientoTecnologicoMantenimiento()
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
        }

        public SolicitudDeEquipamientoTecnologicoMantenimiento(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_SolicitudDeEquipamientoTecnologico _solicitud)
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            solicitud = _solicitud;
            //            CargarCombos();
            CargarObjeto();
            bgwHilo.RunWorkerAsync();
        }

        public SolicitudDeEquipamientoTecnologicoMantenimiento(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _idSolicitud)
        {
            InitializeComponent();
            Inicio();
            CargarCombos();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            //this.solicitud = solicitud;
            idSolicitud = _idSolicitud;
            solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
            solicitud = modelo.GetRequestsById("SAS", _idSolicitud);
            //            CargarCombos();
            CargarObjeto();
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
                documentos = comboHelper.GetDocumentTypeForForm("SAS", "Equipamiento tecnologico");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();
                series = comboHelper.GetDocumentSeriesForForm("SAS", "Equipamiento tecnologico");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();
                tipoSolicitudes = comboHelper.GetRequestTypes("SAS", "Equipamiento tecnologico");
                cboTipoSolicitud.DisplayMember = "Descripcion";
                cboTipoSolicitud.ValueMember = "Codigo";
                cboTipoSolicitud.DataSource = tipoSolicitudes.OrderBy(x => x.Descripcion).ToList();
                cboTipoSolicitud.SelectedValue = "1";
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

        private void SolicitudDeEquipamientoTecnologicoMantenimiento_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkTemporal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTemporal.Checked == true)
            {
                txtVencimiento.Enabled = true;
            }
            else
            {
                txtVencimiento.Enabled = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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

        private void Nuevo()
        {
            #region Nuevo
            btnNuevo.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnEditar.Enabled = false;
            btnRegistrar.Enabled = true;
            btnExportToExcel.Enabled = false;
            btnNuevo.Enabled = false;
            btnAtras.Enabled = false;
            //gbDatosPersonal.Enabled = false;
            //gbDetale.Enabled = false;
            //gbDocumento.Enabled = false;
            //gbJustificacion.Enabled = false;
            //gbUbicacion.Enabled = false;

            //1.- Limpiar objetos y listar

            solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            solicitud.id = 0;
            sedesEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
            HardwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            SoftwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
            listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
            listadoSedesEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
            listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
            listadoHardwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
            solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
            listadoSedesById = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
            listadoSoftwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();

            //2.- Limpiar formulario
            Limpiar(this, gbDatosPersonal);
            Limpiar(this, gbDetale);
            Limpiar(this, gbDocumento);
            Limpiar(this, gbJustificacion);
            Limpiar(this, gbUbicacion);

            //3.- Cargar objeto y listas en blanco

            //4.- Presentar objetos y listas en los formularios
            bgwHilo.RunWorkerAsync();
            #endregion
        }

        private void CargarObjeto()
        {
            #region Nuevo
            btnNuevo.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();


            //2.- Limpiar formulario
            Limpiar(this, gbDatosPersonal);
            Limpiar(this, gbDetale);
            Limpiar(this, gbDocumento);
            Limpiar(this, gbJustificacion);
            Limpiar(this, gbUbicacion);

            //3.- Cargar objeto y listas en blanco

            //4.- Presentar objetos y listas en los formularios

            #endregion
        }

        private void ActualizarSoftware()
        {
            #region Agregar listado de Software()
            btnNuevo.Enabled = false;
            btnSoftwareActualizarListado.Enabled = !true;
            btnHardwareActualizarLista.Enabled = !true;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            gbJustificacion.Enabled = false;
            gbUbicacion.Enabled = false;

            //1.- Limpiar objetos y listar

            //solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            //solicitud.id = 0;
            //sedesEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
            //HardwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            //SoftwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
            //listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
            //listadoSedesEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
            listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
            //listadoHardwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
            //solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
            //listadoSedesById = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
            //listadoSoftwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();

            //2.- Limpiar formulario
            //Limpiar(this, gbDatosPersonal);
            //Limpiar(this, gbDetale);
            //Limpiar(this, gbDocumento);
            //Limpiar(this, gbJustificacion);
            //Limpiar(this, gbUbicacion);

            //3.- Cargar objeto y listas en blanco

            //4.- Presentar objetos y listas en los formularios
            bgwSoftware.RunWorkerAsync();
            #endregion
        }

        private void ActualizarHardware()
        {
            #region Nuevo
            btnNuevo.Enabled = false;
            btnSoftwareActualizarListado.Enabled = !true;
            btnHardwareActualizarLista.Enabled = !true;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            gbDatosPersonal.Enabled = false;
            gbDetale.Enabled = false;
            gbDocumento.Enabled = false;
            gbJustificacion.Enabled = false;
            gbUbicacion.Enabled = false;

            //1.- Limpiar objetos y listar

            //solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            //solicitud.id = 0;
            //sedesEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
            //HardwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            //SoftwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
            listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
            //listadoSedesEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
            //listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
            //listadoHardwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
            //solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
            //listadoSedesById = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
            //listadoSoftwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();

            //2.- Limpiar formulario
            //Limpiar(this, gbDatosPersonal);
            //Limpiar(this, gbDetale);
            //Limpiar(this, gbDocumento);
            //Limpiar(this, gbJustificacion);
            //Limpiar(this, gbUbicacion);

            //3.- Cargar objeto y listas en blanco

            //4.- Presentar objetos y listas en los formularios
            bgwHardware.RunWorkerAsync();
            #endregion
        }

        private void txtPersonalCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                btnInicioContrato.P_TablaConsulta = "personal_empresa where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }

            //personal_empresa where idcodigogeneral = ''
        }

        private void txtPersonal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnInicioContrato.P_TablaConsulta = "personal_empresa where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }
        }

        private void txtPersonalCodigo_Leave(object sender, EventArgs e)
        {
            try
            {
                btnInicioContrato.P_TablaConsulta = "personal_empresa where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
                modelInfoPersonal = new SAS_InfoPersonalController();
                infoPerson = new SAS_InfoPersonal();
                infoPerson = modelInfoPersonal.GetInfoById("SAS", this.txtPersonalCodigo.Text.Trim());
                this.txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                this.txtCargo.Text = infoPerson.cargo.ToUpper();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void txtPersonal_Leave(object sender, EventArgs e)
        {
            try
            {
                btnInicioContrato.P_TablaConsulta = "personal_empresa where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }
        }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                btnInicioContrato.P_TablaConsulta = "personal_empresa where idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }
        }

        private void btnActualizarListado_Click(object sender, EventArgs e)
        {
            ActualizarSoftware();
        }

        private void bgwSoftware_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
                listadoSoftwareEnBlanco = modelo.GetSoftwareDetailBlanklistingForRequest("SAS", solicitud);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwSoftware_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvSoftware.CargarDatos(listadoSoftwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>());
                dgvSoftware.Refresh();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

            btnNuevo.Enabled = !false;
            BarraPrincipal.Enabled = !false;
            progressBar1.Visible = !true;
            gbDatosPersonal.Enabled = !false;
            gbDetale.Enabled = !false;
            gbDocumento.Enabled = !false;
            gbJustificacion.Enabled = !false;
            gbUbicacion.Enabled = !false;
            btnSoftwareActualizarListado.Enabled = true;
            btnHardwareActualizarLista.Enabled = true;
        }

        private void tabSoftware_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bgwHardware_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
                listadoHardwareEnBlanco = modelo.GetHardwareDetailBlanklistingForRequest("SAS", solicitud);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void bgwHardware_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region 
                dgvHardware.CargarDatos(listadoHardwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>());
                dgvHardware.Refresh();

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

            btnNuevo.Enabled = !false;
            BarraPrincipal.Enabled = !false;
            progressBar1.Visible = !true;
            gbDatosPersonal.Enabled = !false;
            gbDetale.Enabled = !false;
            gbDocumento.Enabled = !false;
            gbJustificacion.Enabled = !false;
            gbUbicacion.Enabled = !false;
            btnSoftwareActualizarListado.Enabled = true;
            btnHardwareActualizarLista.Enabled = true;

        }

        private void btnActualizarListadoHardware_Click(object sender, EventArgs e)
        {
            ActualizarHardware();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "PE")
            {
                #region Grabar()
                Registrar();
                // listadoHardwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                #endregion
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para grabar", "Advertencia del sistema");
                return;
            }
        }

        private void Registrar()
        {
            try
            {
                /*1.- Instanciar objetos y listas de cero */
                solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                //solicitud.id = 0;
                sedesEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
                HardwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                SoftwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                //listadoHardwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                //HardwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                //SoftwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();


                /*2.- Asignar objetos y listas desde controles */
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.fecha = Convert.ToDateTime(this.txtFecha.Text);
                solicitud.idCodigoGeneral = this.txtPersonalCodigo.Text.Trim();
                solicitud.nombresCompletos = this.txtPersonal.Text.Trim();
                solicitud.esExterno = (chkEsExterno.Checked == true ? 1 : 0);


                //solicitud.fechaDeVencimiento = Convert.ToDateTime(this.txtVencimiento.Text);

                string ASCD = this.txtValidar.Text.ToString().Trim();

                if (this.txtVencimiento.Text.ToString().Trim() != ASCD)
                {
                    if (this.txtVencimiento.Text != null)
                    {
                        if (this.txtVencimiento.Text.ToString().Trim() != string.Empty)
                        {
                            solicitud.fechaDeVencimiento = this.txtVencimiento.Text.ToString().Trim() != "" ? Convert.ToDateTime(this.txtVencimiento.Text.ToString().Trim()) : (DateTime?)null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar una fecha de finalización", "Notificación del sistema");
                    this.txtVencimiento.Focus();
                    return;

                }



                if (this.txtVencimientoContrato.Text.ToString().Trim() != ASCD)
                {
                    if (this.txtVencimientoContrato.Text != null)
                    {
                        if (this.txtVencimientoContrato.Text.ToString().Trim() != string.Empty)
                        {
                            solicitud.vencimientoContrato = this.txtVencimientoContrato.Text.ToString().Trim() != "" ? Convert.ToDateTime(this.txtVencimientoContrato.Text.ToString().Trim()) : (DateTime?)null;
                        }
                    }
                }





                solicitud.esTemporal = (chkTemporal.Checked == true ? 1 : 0);
                solicitud.justificacion = this.txtJustificacion.Text.Trim();
                solicitud.estadoCodigo = txtIdEstado.Text.Trim();
                solicitud.usuarioEnAtencion = user2.IdUsuario.Trim();
                solicitud.tipoSolicitud = this.cboTipoSolicitud.SelectedValue != null ? Convert.ToInt32(this.cboTipoSolicitud.SelectedValue) : 0;
                //solicitud.vencimientoContrato = Convert.ToDateTime(this.txtVencimientoContrato.Text.Trim());
                solicitud.itemInicioContrato = txtInicioContratoCodigo.Text.Trim(); ;
                solicitud.tipoContrato = (rbtPermanente.Checked == true ? 1 : 0);


                /*3.- Registrar Objetos y lista detalles */
                #region Agregar Sedes() 

                // casona item 005 | cod Sede: 007 | Casona
                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo sedeEnSolicitudRegistro = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                if (chkCasona.Checked == true)
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "005";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "007";
                    sedeEnSolicitudRegistro.estado = 1;
                }
                else
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "005";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "007";
                    sedeEnSolicitudRegistro.estado = 0;
                }
                sedesEnSolicitudRegistro.Add(sedeEnSolicitudRegistro);


                // casona item 004  | cod Sede: 006 | Lima
                sedeEnSolicitudRegistro = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                if (chkOficinasLima.Checked == true)
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "004";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "006";
                    sedeEnSolicitudRegistro.estado = 1;
                }
                else
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "004";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "006";
                    sedeEnSolicitudRegistro.estado = 0;
                }
                sedesEnSolicitudRegistro.Add(sedeEnSolicitudRegistro);


                // casona item 003  | cod Sede: 003 | Colca03
                sedeEnSolicitudRegistro = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                if (chkColca03.Checked == true)
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "003";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "003";
                    sedeEnSolicitudRegistro.estado = 1;
                }
                else
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "003";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "003";
                    sedeEnSolicitudRegistro.estado = 0;
                }
                sedesEnSolicitudRegistro.Add(sedeEnSolicitudRegistro);



                // casona item 002  | cod Sede: 002 | Packing
                sedeEnSolicitudRegistro = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                if (chkPacking.Checked == true)
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "002";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "002";
                    sedeEnSolicitudRegistro.estado = 1;

                }
                else
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "002";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "002";
                    sedeEnSolicitudRegistro.estado = 0;
                }
                sedesEnSolicitudRegistro.Add(sedeEnSolicitudRegistro);


                // casona item 001  | cod Sede: 001 | Colca 03
                sedeEnSolicitudRegistro = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                if (chkColca01.Checked == true)
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "001";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "001";
                    sedeEnSolicitudRegistro.estado = 1;

                }
                else
                {
                    sedeEnSolicitudRegistro.idSolicitudEquipamientoTecnologico = solicitud.id;
                    sedeEnSolicitudRegistro.item = "001";
                    sedeEnSolicitudRegistro.sedeDeTrabajoCodigo = "001";
                    sedeEnSolicitudRegistro.estado = 0;
                }
                sedesEnSolicitudRegistro.Add(sedeEnSolicitudRegistro);
                #endregion

                #region Agregar Hardware() 
                listadoHardwareARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                if (this.dgvHardware != null)
                {
                    #region Detalle de tipo de HardWare()                     
                    if (this.dgvHardware.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvHardware.Rows)
                        {
                            if (fila.Cells["chId"].Value.ToString().Trim() != string.Empty && fila.Cells["chItem"].Value.ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle para los tipos de hardware() 
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware item = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                    item.idSolicitudEquipamientoTecnologico = fila.Cells["chId"].Value != null ? Convert.ToInt32(fila.Cells["chId"].Value.ToString().Trim()) : 0;
                                    item.item = fila.Cells["chItem"].Value != null ? Convert.ToString(fila.Cells["chItem"].Value.ToString().Trim()) : string.Empty;
                                    item.idDispositivoTipoHardware = fila.Cells["chIdHardware"].Value != null ? Convert.ToString(fila.Cells["chIdHardware"].Value.ToString().Trim()) : string.Empty;
                                    //item.desde = fila.Cells["chDesde"].Value != null ? Convert.ToDateTime(fila.Cells["chDesde"].Value) : (DateTime?)null;                                   
                                    item.desde = fila.Cells["chDesde"].Value != null ? (fila.Cells["chDesde"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chDesde"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.hasta = fila.Cells["chHasta"].Value != null ? (fila.Cells["chHasta"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chHasta"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.estado = fila.Cells["chEstado"].Value != null ? Convert.ToDecimal(fila.Cells["chEstado"].Value.ToString().Trim()) : 0;
                                    item.valor = (fila.Cells["chValor"].Value != null && fila.Cells["chValor"].Value.ToString() != string.Empty) ? Convert.ToDecimal(fila.Cells["chValor"].Value.ToString().Trim()) : 0;
                                    item.glosa = fila.Cells["chGlosa"].Value != null ? Convert.ToString(fila.Cells["chGlosa"].Value.ToString().Trim()) : string.Empty;
                                    item.actualizado = fila.Cells["chActualizado"].Value != null ? Convert.ToDecimal(fila.Cells["chActualizado"].Value.ToString().Trim()) : 0;
                                    item.elegido = fila.Cells["chElegido"].Value != null ? (fila.Cells["chElegido"].Value.ToString() != string.Empty ? Convert.ToInt32(fila.Cells["chElegido"].Value) : 0) : 0;
                                    item.codigoERP = fila.Cells["chCodigoERP"].Value != null ? (Convert.ToInt32(fila.Cells["chCodigoERP"].Value.ToString() != string.Empty ? (fila.Cells["chCodigoERP"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chCodigoERP"].Value) : (int?)null) : (int?)null;

                                    item.GeneraSolicitud = fila.Cells["chGeneraSolicitud"].Value != null ? (Convert.ToInt32(fila.Cells["chGeneraSolicitud"].Value.ToString() != string.Empty ? (fila.Cells["chGeneraSolicitud"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chGeneraSolicitud"].Value) : (int?)null) : (int?)null;
                                    item.idReferenciaSoporteTecnico = fila.Cells["chidReferenciaSoporteTecnico"].Value != null ? (Convert.ToInt32(fila.Cells["chidReferenciaSoporteTecnico"].Value.ToString() != string.Empty ? (fila.Cells["chidReferenciaSoporteTecnico"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chidReferenciaSoporteTecnico"].Value) : (int?)null) : (int?)null;
                                    item.RequiereCapacitacion = fila.Cells["chRequiereCapacitacion"].Value != null ? (Convert.ToInt32(fila.Cells["chRequiereCapacitacion"].Value.ToString() != string.Empty ? (fila.Cells["chRequiereCapacitacion"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chRequiereCapacitacion"].Value) : (int?)null) : (int?)null;
                                    #endregion
                                    listadoHardwareARegistrar.Add(item);
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

                #endregion

                #region Agregar Software() 
                listadoSoftwareARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                if (this.dgvSoftware != null)
                {
                    #region Detalle de tipo de HardWare()                     
                    if (this.dgvSoftware.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvSoftware.Rows)
                        {
                            if (fila.Cells["chIdSolicitudSoftware"].Value.ToString().Trim() != string.Empty && fila.Cells["chItemSoftware"].Value.ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle para los tipos de hardware() 
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware item = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                    item.idSolicitudEquipamientoTecnologico = fila.Cells["chIdSolicitudSoftware"].Value != null ? Convert.ToInt32(fila.Cells["chIdSolicitudSoftware"].Value.ToString().Trim()) : 0;
                                    item.item = fila.Cells["chItemSoftware"].Value != null ? Convert.ToString(fila.Cells["chItemSoftware"].Value.ToString().Trim()) : string.Empty;
                                    item.idDispositivoTipoSoftware = fila.Cells["chidSoftware"].Value != null ? Convert.ToInt32(fila.Cells["chidSoftware"].Value.ToString().Trim()) : 0;
                                    item.desde = fila.Cells["chDesdeSoftware"].Value != null ? (fila.Cells["chDesdeSoftware"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chDesdeSoftware"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.hasta = fila.Cells["chHastaSoftware"].Value != null ? (fila.Cells["chHastaSoftware"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chHastaSoftware"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.estado = fila.Cells["chEstadoSoftware"].Value != null ? Convert.ToDecimal(fila.Cells["chEstadoSoftware"].Value.ToString().Trim()) : 0;
                                    item.valor = fila.Cells["chValorSoftware"].Value != null ? Convert.ToDecimal(fila.Cells["chValorSoftware"].Value.ToString().Trim()) : 0;
                                    item.glosa = fila.Cells["chGlosaSoftware"].Value != null ? Convert.ToString(fila.Cells["chGlosaSoftware"].Value.ToString().Trim()) : string.Empty;
                                    item.actualizado = fila.Cells["chActualizadoSoftware"].Value != null ? Convert.ToInt32(fila.Cells["chActualizadoSoftware"].Value.ToString().Trim()) : 0;

                                    //item.elegido = fila.Cells["chElegidoSoftware"].Value != null ? Convert.ToInt32(fila.Cells["chElegidoSoftware"].Value) : 0;
                                    item.elegido = fila.Cells["chElegidoSoftware"].Value != null ? (fila.Cells["chElegidoSoftware"].Value.ToString() != string.Empty ? Convert.ToInt32(fila.Cells["chElegidoSoftware"].Value) : 0) : 0;
                                    //item.perfilDeAcceso = fila.Cells["chPerfilDeAcceso"].Value != null ? Convert.ToInt32(fila.Cells["chPerfilDeAcceso"].Value.ToString().Trim()) : 0;
                                    item.perfilDeAcceso = fila.Cells["chPerfilDeAcceso"].Value != null ? (Convert.ToInt32(fila.Cells["chPerfilDeAcceso"].Value.ToString() != string.Empty ? (fila.Cells["chPerfilDeAcceso"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chPerfilDeAcceso"].Value) : (int?)null) : (int?)null;

                                    item.GeneraSolicitud = fila.Cells["chGeneraSolicitudSoft"].Value != null ? (Convert.ToInt32(fila.Cells["chGeneraSolicitudSoft"].Value.ToString() != string.Empty ? (fila.Cells["chGeneraSolicitudSoft"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chGeneraSolicitudSoft"].Value) : (int?)null) : (int?)null;
                                    item.idReferenciaSoporteFuncional = fila.Cells["chidReferenciaSoporteFuncionalSoft"].Value != null ? (Convert.ToInt32(fila.Cells["chidReferenciaSoporteFuncionalSoft"].Value.ToString() != string.Empty ? (fila.Cells["chidReferenciaSoporteFuncionalSoft"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chidReferenciaSoporteFuncionalSoft"].Value) : (int?)null) : (int?)null;
                                    item.RequiereCapacitacion = fila.Cells["chRequiereCapacitacionSoft"].Value != null ? (Convert.ToInt32(fila.Cells["chRequiereCapacitacionSoft"].Value.ToString() != string.Empty ? (fila.Cells["chRequiereCapacitacionSoft"].Value) : 0) > 0 ? Convert.ToInt32(fila.Cells["chRequiereCapacitacionSoft"].Value) : (int?)null) : (int?)null;


                                    #endregion
                                    listadoSoftwareARegistrar.Add(item);
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

                #endregion

                #region Agregar Línea()
                listadoLineaCelularARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular>();
                if (this.dgvDetalleLineaCelular != null)
                {
                    #region Detalle de tipo de HardWare()                     
                    if (this.dgvDetalleLineaCelular.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalleLineaCelular.Rows)
                        {
                            if (fila.Cells["chIdDetalleCelular"].Value.ToString().Trim() != string.Empty && fila.Cells["chitemDetalleCelular"].Value.ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle para los tipos de hardware() 
                                    SAS_SolicitudDeEquipamientoTecnologicoLineaCelular item = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                    item.idSolicitudEquipamientoTecnologico = solicitud.id != null ? Convert.ToInt32(solicitud.id) : 0;
                                    item.item = fila.Cells["chitemDetalleCelular"].Value != null ? Convert.ToString(fila.Cells["chitemDetalleCelular"].Value.ToString().Trim()) : string.Empty;
                                    item.idLinea = fila.Cells["chIdLineaDetalleCelular"].Value != null ? (fila.Cells["chIdLineaDetalleCelular"].Value.ToString().Trim() != string.Empty ? Convert.ToInt32(fila.Cells["chIdLineaDetalleCelular"].Value.ToString().Trim()) : (int?)null) : (int?)null;
                                    item.desde = fila.Cells["chDesdeDetalleCelular"].Value != null ? (fila.Cells["chDesdeDetalleCelular"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chDesdeDetalleCelular"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.hasta = fila.Cells["chHastaDetalleCelular"].Value != null ? (fila.Cells["chHastaDetalleCelular"].Value.ToString() != string.Empty ? Convert.ToDateTime(fila.Cells["chHastaDetalleCelular"].Value) : (DateTime?)null) : (DateTime?)null;
                                    item.estado = fila.Cells["chEstadoDetalleCelular"].Value != null ? Convert.ToDecimal(fila.Cells["chEstadoDetalleCelular"].Value.ToString().Trim()) : 0;
                                    item.valor = fila.Cells["chValorDetalleCelular"].Value != null ? (fila.Cells["chValorDetalleCelular"].Value.ToString().Trim() != string.Empty ? Convert.ToInt32(fila.Cells["chValorDetalleCelular"].Value.ToString().Trim()) : (int?)null) : (int?)null;
                                    item.glosa = fila.Cells["chGlosaDetalleCelular"].Value != null ? Convert.ToString(fila.Cells["chGlosaDetalleCelular"].Value.ToString().Trim()) : string.Empty;
                                    item.actualizado = fila.Cells["chActualizadoDetalleCelular"].Value != null ? Convert.ToInt32(fila.Cells["chActualizadoDetalleCelular"].Value.ToString().Trim()) : 0;
                                    item.elegido = fila.Cells["chElegidoDetalleCelular"].Value != null ? (fila.Cells["chElegidoDetalleCelular"].Value.ToString() != string.Empty ? Convert.ToInt32(fila.Cells["chElegidoDetalleCelular"].Value) : 0) : 0;
                                    #endregion
                                    listadoLineaCelularARegistrar.Add(item);
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
                #endregion


                btnNuevo.Enabled = false;
                BarraPrincipal.Enabled = false;
                progressBar1.Visible = true;
                gbDatosPersonal.Enabled = false;
                gbDetale.Enabled = false;
                gbDocumento.Enabled = false;
                gbJustificacion.Enabled = false;
                gbUbicacion.Enabled = false;
                bgwRegistrar.RunWorkerAsync();


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void dgvSoftware_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.dgvSoftware.CurrentRow.Cells["chElegidoSoftware"].Value.ToString() == "1")
            {
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                if (((DataGridView)sender).RowCount > 0)
                {
                    #region Tipo de Perfil de accesso de Software() 
                    if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chPerfilAcceso")
                    {
                        if (e.KeyCode == Keys.F3)
                        {
                            frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                            search.ListaGeneralResultado = modelo.GetListOfProfiles("SAS");
                            search.Text = "Buscar tipo de perfil de Acceso";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPerfilDeAcceso"].Value = search.ObjetoRetorno.Codigo;
                                this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPerfilAcceso"].Value = search.ObjetoRetorno.Descripcion;
                            }
                        }
                    }
                    #endregion


                    #region Tipo de Software() 
                    if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chSoftware")
                    {
                        if (e.KeyCode == Keys.F3)
                        {
                            modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                            frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                            search.ListaGeneralResultado = modelo.ObtenerTipoDeSoftware("SAS");
                            search.Text = "Buscar tipo de perfil de Acceso";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidSoftware"].Value = search.ObjetoRetorno.Codigo;
                                this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chSoftware"].Value = search.ObjetoRetorno.Descripcion;
                            }
                        }
                    }
                    #endregion

                }
            }
        }

        private void dgvSoftware_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgvSoftware_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 10)//set your checkbox column index instead of 2
                {   //When you check
                    if (Convert.ToBoolean(dgvSoftware.Rows[e.RowIndex].Cells["chElegidoSoftware"].EditedFormattedValue) == true)
                    {
                        //EXAMPLE OF OTHER CODE
                        // dgvSoftware.Rows[e.RowIndex].Cells[5].Value = DateTime.Now.ToShortDateString();

                        //SET BY CODE THE CHECK BOX
                        // dgvSoftware.Rows[e.RowIndex].Cells[2].Value = 1;
                    }
                    else //When you decheck
                    {
                        if (this.dgvSoftware.CurrentRow.Cells["chPerfilAcceso"].Value.ToString() != string.Empty)
                        {
                            this.dgvSoftware.CurrentRow.Cells["chPerfilDeAcceso"].Value = string.Empty;
                            this.dgvSoftware.CurrentRow.Cells["chPerfilAcceso"].Value = string.Empty;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "ERROR EN EL SISTEMA");
                return;
            }


        }

        private void dgvSoftware_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgvSoftware_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvSoftware_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvSoftware_Leave(object sender, EventArgs e)
        {
            //if (this.dgvSoftware.CurrentRow.Cells["chElegidoSoftware"].Value.ToString() == "0" || this.dgvSoftware.CurrentRow.Cells["chElegidoSoftware"].Value.ToString() == string.Empty)
            //{
            //    if (this.dgvSoftware.CurrentRow.Cells["chPerfilAcceso"].Value.ToString() != string.Empty)
            //    {
            //        this.dgvSoftware.CurrentRow.Cells["chPerfilDeAcceso"].Value = string.Empty;
            //        this.dgvSoftware.CurrentRow.Cells["chPerfilAcceso"].Value = string.Empty;
            //    }
            //}
        }

        private void dgvSoftware_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                result = modelo.ToRegister("SAS", solicitud, sedesEnSolicitudRegistro, listadoHardwareARegistrar, listadoSoftwareARegistrar, listadoLineaCelularARegistrar, listadoHardwareAEliminar, listadoSoftwareAEliminar);

                this.solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                solicitud = modelo.GetRequestsById("SAS", result);

                #region 
                //solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                sedesEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
                HardwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                SoftwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                modelInfoPersonal = new SAS_InfoPersonalController();
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
                solicitydById = modelo.ListRequestsById("SAS", solicitud);

                if (solicitud != null)
                {
                    if (solicitud.id == 0)
                    {
                        listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
                        listadoHardwareEnBlanco = modelo.GetHardwareDetailBlanklistingForRequest("SAS", solicitud);
                        listadoSedesEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
                        listadoSedesEnBlanco = modelo.ObtainABlankListOfTheDetailsOfTheVenues("SAS", solicitud);
                        listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
                        listadoSoftwareEnBlanco = modelo.GetSoftwareDetailBlanklistingForRequest("SAS", solicitud);
                        numeroCorrelativoDeCero = modelo.ObtenerNumeroCorrelativoDeCero("SAS", solicitud);
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());
                    }
                    else
                    {
                        listadoHardwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
                        listadoHardwareById = modelo.GetListOfHardwareDetailByRequestId("SAS", solicitud);
                        listadoSedesById = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
                        listadoSedesById = modelo.GetDetailedListOfVenuesByRequestId("SAS", solicitud);
                        listadoSoftwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();
                        listadoSoftwareById = modelo.GetListOfSoftwareDetailByRequestId("SAS", solicitud);
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());
                    }
                }

                #endregion


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
            MessageBox.Show("Registro realizo correctamente", "Confirmación del sistema");
        }

        private void MostrarResultados()
        {
            try
            {
                if (solicitud.id == 0)
                {
                    txtCodigo.Text = result.ToString();
                }
                if (solicitud != null)
                {
                    #region Registro nuevo ()                    
                    this.txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                    this.txtEmpresaCodigo.Text = "001";
                    this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO SA";
                    this.txtSucursalCodigo.Text = "001";
                    this.txtSucursal.Text = "SEDE LOGISTICA AGRICOLA";
                    this.txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "PE";
                    if (this.txtIdEstado.Text == "PE")
                    {
                        this.btnVB1.Enabled = true;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = true;
                        btnSoftwareActualizarListado.Enabled = true;
                    }
                    else if (this.txtIdEstado.Text == "AN")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        this.txtEstado.BackColor = System.Drawing.Color.Peru;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "CE")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        this.txtEstado.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "V1")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = true;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "V2")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = true;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "AP")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else
                    {
                        this.btnVB1.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }

                    this.txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim() : "PENDIENTE";
                    this.txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                    this.txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                    this.cboTipoSolicitud.SelectedValue = solicitydById.idTipoSolicitud.ToString();
                    this.txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                    this.txtPersonal.Text = solicitydById.nombresCompletos != null ? solicitydById.nombresCompletos.Trim() : string.Empty;
                    this.txtInicioContratoCodigo.Text = solicitydById.itemInicioContrato != null ? solicitydById.itemInicioContrato.Trim() : string.Empty;
                    this.txtInicioContrato.Text = solicitydById.fecha_Ingreso != null ? solicitydById.fecha_Ingreso.ToPresentationDate().Trim() : string.Empty;
                    this.txtVencimiento.Text = solicitydById.fechaDeVencimiento != (DateTime?)null ? solicitydById.fechaDeVencimiento.ToPresentationDate() : solicitydById.fecha.Value.AddYears(1).ToPresentationDate();
                    this.txtVencimiento.Enabled = true;

                    this.txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                    this.txtCargo.Text = infoPerson.cargo.ToUpper();

                    chkTemporal.Checked = false;
                    if (solicitydById.esTemporal == 1)
                    {
                        chkTemporal.Checked = true;
                    }

                    if (solicitydById.tipoContrato == 0)
                    {
                        rbtPermanente.Checked = false;
                        rbtTemporal.Checked = false;
                    }
                    else if (solicitydById.tipoContrato == 1)
                    {
                        rbtPermanente.Checked = true;
                        rbtTemporal.Checked = false;
                    }
                    else if (solicitydById.tipoContrato == 2)
                    {
                        rbtPermanente.Checked = false;
                        rbtTemporal.Checked = true;
                    }

                    this.txtVencimientoContrato.Text = solicitydById.vencimientoContrato.ToPresentationDate();
                    this.txtJustificacion.Text = solicitydById.justificacion != null ? solicitydById.justificacion.Trim() : "PE";

                    if (solicitud.id == 0)
                    {
                        #region Nuevo() 
                        dgvHardware.CargarDatos(listadoHardwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>());
                        dgvHardware.Refresh();
                        listadoSoftwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                        listadoHardwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                        listadoLineaCelularAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular>();

                        dgvSoftware.CargarDatos(listadoSoftwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>());
                        dgvSoftware.Refresh();

                        chkColca01.Checked = false;
                        chkPacking.Checked = false;
                        chkColca03.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkCasona.Checked = false;

                        if (listadoSedesEnBlanco != null)
                        {
                            if (listadoSedesEnBlanco.ToList().Count > 0)
                            {
                                foreach (var item in listadoSedesEnBlanco)
                                {
                                    if (item.sede.Trim().ToUpper() == "Colca 01".ToUpper() && item.estado == 1)
                                    {
                                        chkColca01.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Packing".ToUpper() && item.estado == 1)
                                    {
                                        chkPacking.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Colca 03".ToUpper() && item.estado == 1)
                                    {
                                        chkColca03.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Sede Lima".ToUpper() && item.estado == 1)
                                    {
                                        chkOficinasLima.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Casona".ToUpper() && item.estado == 1)
                                    {
                                        chkCasona.Checked = true;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Lectura de registro Existente() 
                        chkColca01.Checked = false;
                        chkPacking.Checked = false;
                        chkColca03.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkCasona.Checked = false;

                        if (listadoSedesById != null)
                        {
                            if (listadoSedesById.ToList().Count > 0)
                            {
                                foreach (var item in listadoSedesById)
                                {
                                    if (item.sede.Trim().ToUpper() == "Colca 01".ToUpper() && item.estado == 1)
                                    {
                                        chkColca01.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Packing".ToUpper() && item.estado == 1)
                                    {
                                        chkPacking.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Colca 03".ToUpper() && item.estado == 1)
                                    {
                                        chkColca03.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Sede Lima".ToUpper() && item.estado == 1)
                                    {
                                        chkOficinasLima.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Casona".ToUpper() && item.estado == 1)
                                    {
                                        chkCasona.Checked = true;
                                    }
                                }
                            }
                        }

                        listadoSoftwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                        listadoHardwareAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                        listadoLineaCelularAEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular>();

                        dgvHardware.CargarDatos(listadoHardwareById.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>());
                        dgvHardware.Refresh();

                        dgvSoftware.CargarDatos(listadoSoftwareById.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>());
                        dgvSoftware.Refresh();
                        #endregion
                    }


                    if (this.txtIdEstado.Text == "PE")
                    {
                        #region 
                        btnNuevo.Enabled = true;
                        BarraPrincipal.Enabled = true;
                        progressBar1.Visible = false;
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
                        btnAtras.Enabled = true;
                        btnFlujoAprobacion.Enabled = true;
                        #endregion
                    }
                    else
                    {
                        #region 
                        btnNuevo.Enabled = true;
                        BarraPrincipal.Enabled = true;
                        progressBar1.Visible = false;
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = true;
                        btnAnular.Enabled = false;
                        btnFlujoAprobacion.Enabled = true;
                        #endregion
                    }
                    #endregion
                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                btnRegistrar.Enabled = false;
                btnEditar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAtras.Enabled = false;
                btnEliminarRegistro.Enabled = true;
                btnAnular.Enabled = true;
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "PE")
            {
                Anular();
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para anular el registro", "Mensaje del sistema");
            }


        }

        private void Anular()
        {
            /*1.- Instanciar objetos y listas de cero */
            solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            //solicitud.id = 0;
            sedesEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
            HardwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            SoftwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
            HardwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            SoftwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();

            /*2.- Asignar objetos y listas desde controles */
            solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;

            if (this.txtCodigo.Text != "0")
            {
                try
                {
                    modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                    result = modelo.ChangeState("SAS", solicitud);
                    this.txtEstado.Text = "ANULADO";
                    this.txtIdEstado.Text = "AN";
                    this.txtEstado.BackColor = Color.Peru;
                    MessageBox.Show("Se actualizo el estado del documento correctamente", "Confirmación del sistema");



                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                    return;
                }
            }

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "PE")
            {
                Eliminar();
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para anular el registro", "Mensaje del sistema");
            }
        }

        private void Eliminar()
        {
            /*1.- Instanciar objetos y listas de cero */
            solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            //solicitud.id = 0;
            sedesEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
            HardwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            SoftwareEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
            HardwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
            SoftwareEnSolicitudRegistroEliminar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();

            /*2.- Asignar objetos y listas desde controles */
            solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;

            if (this.txtCodigo.Text != "0")
            {
                try
                {
                    modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                    result = modelo.DeleteRecord("SAS", solicitud);
                    MessageBox.Show("Se elimino el estado del documento correctamente", "Confirmación del sistema");
                    Nuevo();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                    return;
                }
            }
        }

        private void btnHardwareGenerarSolicitud_Click(object sender, EventArgs e)
        {

        }

        private void btnFlujoAprobacion_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "PE")
            {
                this.btnVB1.Enabled = true;
                this.btnBV3.Enabled = false;
                this.btnVB2.Enabled = false;
                this.btnRechazarSolicitud.Enabled = true;

            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region 
                //solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                sedesEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();
                HardwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                SoftwareEnSolicitud = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                modelInfoPersonal = new SAS_InfoPersonalController();

                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                solicitydById = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
                solicitydById = modelo.ListRequestsById("SAS", solicitud);

                if (solicitud != null)
                {
                    if (solicitud.id == 0)
                    {
                        listadoLineaCelularEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();
                        listadoLineaCelularEnBlanco = modelo.ListDetailRequestByCelLineByIdRequestBlank("SAS", 0);

                        listadoHardwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
                        listadoHardwareEnBlanco = modelo.GetHardwareDetailBlanklistingForRequest("SAS", solicitud);
                        ultimoItemEnListaDetalleHardware = 1;

                        listadoSedesEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
                        listadoSedesEnBlanco = modelo.ObtainABlankListOfTheDetailsOfTheVenues("SAS", solicitud);

                        listadoSoftwareEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
                        listadoSoftwareEnBlanco = modelo.GetSoftwareDetailBlanklistingForRequest("SAS", solicitud);
                        ultimoItemEnListaDetalleSofware = 1;

                        numeroCorrelativoDeCero = modelo.ObtenerNumeroCorrelativoDeCero("SAS", solicitud);
                        infoPerson = new SAS_InfoPersonal();
                        infoPerson = modelInfoPersonal.GetInfoById("SAS", solicitydById.idCodigoGeneral.Trim());

                        ultimoItemEnListaDetalleCelulares = 1;
                    }
                    else
                    {
                        listadoLineaCelularById = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();
                        listadoLineaCelularById = modelo.ListDetailRequestByCelLineByIdRequest("sas", solicitud.id);

                        if (listadoLineaCelularById != null && listadoLineaCelularById.ToList().Count > 0)
                        {
                            ultimoItemEnListaDetalleCelulares = Convert.ToInt32(listadoLineaCelularById.Max(x => x.item)) + 1;
                        }



                        listadoHardwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
                        listadoHardwareById = modelo.GetListOfHardwareDetailByRequestId("SAS", solicitud);

                        if (listadoHardwareById != null && listadoHardwareById.ToList().Count > 0)
                        {
                            ultimoItemEnListaDetalleHardware = Convert.ToInt32(listadoHardwareById.Max(x => x.item)) + 1;
                        }


                        listadoSedesById = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
                        listadoSedesById = modelo.GetDetailedListOfVenuesByRequestId("SAS", solicitud);



                        listadoSoftwareById = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();
                        listadoSoftwareById = modelo.GetListOfSoftwareDetailByRequestId("SAS", solicitud);


                        if (listadoSoftwareById != null && listadoSoftwareById.ToList().Count > 0)
                        {
                            ultimoItemEnListaDetalleSofware = Convert.ToInt32(listadoSoftwareById.Max(x => x.item)) + 1;
                        }


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
            MostrarResultados();
            #region 
            /*
            try
            {
                
                #region Asignar objetos a controles()
                if (solicitud != null)
                {
                    #region Registro nuevo ()                    
                    this.txtCodigo.Text = solicitydById.id != null ? solicitydById.id.ToString().Trim() : "0";
                    this.txtEmpresaCodigo.Text = "001";
                    this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO SA";
                    this.txtSucursalCodigo.Text = "001";
                    this.txtSucursal.Text = "SEDE LOGISTICA AGRICOLA";
                    this.txtIdEstado.Text = solicitydById.estadoCodigo != null ? solicitydById.estadoCodigo.Trim() : "PE";

                    if (this.txtIdEstado.Text == "PE")
                    {
                        this.btnVB1.Enabled = true;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = true;
                        btnSoftwareActualizarListado.Enabled = true;
                    }
                    else if (this.txtIdEstado.Text == "AN")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        this.txtEstado.BackColor = System.Drawing.Color.Peru;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "CE")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        this.txtEstado.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "V1")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = true;
                        this.btnBV3.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;

                    }
                    else if (this.txtIdEstado.Text == "V2")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnBV3.Enabled = true;
                        this.btnRechazarSolicitud.Enabled = true;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else if (this.txtIdEstado.Text == "AP")
                    {
                        this.btnVB1.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }
                    else
                    {
                        this.btnVB1.Enabled = false;
                        this.btnBV3.Enabled = false;
                        this.btnVB2.Enabled = false;
                        this.btnRechazarSolicitud.Enabled = false;
                        btnHardwareActualizarLista.Enabled = false;
                        btnSoftwareActualizarListado.Enabled = false;
                    }

                    this.txtEstado.Text = solicitydById.estado != null ? solicitydById.estado.Trim().ToUpper().ToUpper() : "PENDIENTE";
                    this.txtCorrelativo.Text = solicitydById.id.ToString().PadLeft(7, '0') == "0000000" ? numeroCorrelativoDeCero.ToString().PadLeft(7, '0') : solicitydById.id.ToString().PadLeft(7, '0');
                    this.txtFecha.Text = solicitydById.fecha.ToPresentationDate();
                    this.cboTipoSolicitud.SelectedValue = solicitydById.idTipoSolicitud.ToString();
                    this.txtPersonalCodigo.Text = solicitydById.idCodigoGeneral != null ? solicitydById.idCodigoGeneral.Trim() : string.Empty;
                    this.txtPersonal.Text = solicitydById.nombresCompletos != null ? solicitydById.nombresCompletos.Trim() : string.Empty;
                    this.txtInicioContratoCodigo.Text = solicitydById.itemInicioContrato != null ? solicitydById.itemInicioContrato.Trim() : string.Empty;
                    this.txtInicioContrato.Text = solicitydById.fecha_Ingreso != null ? solicitydById.fecha_Ingreso.ToPresentationDate().Trim() : string.Empty;
                    this.txtVencimiento.Text = solicitydById.fechaDeVencimiento != (DateTime?)null ? solicitydById.fechaDeVencimiento.ToPresentationDate() : solicitydById.fecha.Value.AddYears(1).ToPresentationDate();
                    this.txtVencimiento.Enabled = true;

                    this.txtDNI.Text = infoPerson.nrodocumento.ToUpper();
                    this.txtCargo.Text = infoPerson.cargo.ToUpper();

                    chkTemporal.Checked = false;
                    if (solicitydById.esTemporal == 1)
                    {
                        chkTemporal.Checked = true;
                    }

                    if (solicitydById.tipoContrato == 0)
                    {
                        rbtPermanente.Checked = false;
                        rbtTemporal.Checked = false;
                    }
                    else if (solicitydById.tipoContrato == 1)
                    {
                        rbtPermanente.Checked = true;
                        rbtTemporal.Checked = false;
                    }
                    else if (solicitydById.tipoContrato == 2)
                    {
                        rbtPermanente.Checked = false;
                        rbtTemporal.Checked = true;
                    }

                    this.txtVencimientoContrato.Text = solicitydById.vencimientoContrato.ToPresentationDate();
                    this.txtJustificacion.Text = solicitydById.justificacion != null ? solicitydById.justificacion.Trim() : "PE";

                    if (solicitud.id == 0)
                    {
                        #region Nuevo() 
                        dgvHardware.CargarDatos(listadoHardwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>());
                        dgvHardware.Refresh();

                        dgvSoftware.CargarDatos(listadoSoftwareEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>());
                        dgvSoftware.Refresh();

                        dgvDetalleLineaCelular.CargarDatos(listadoLineaCelularEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>());
                        dgvDetalleLineaCelular.Refresh();

                        chkColca01.Checked = false;
                        chkPacking.Checked = false;
                        chkColca03.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkCasona.Checked = false;

                        if (listadoSedesEnBlanco != null)
                        {
                            if (listadoSedesEnBlanco.ToList().Count > 0)
                            {
                                foreach (var item in listadoSedesEnBlanco)
                                {
                                    if (item.sede.Trim().ToUpper() == "Colca 01".ToUpper() && item.estado == 1)
                                    {
                                        chkColca01.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Packing".ToUpper() && item.estado == 1)
                                    {
                                        chkPacking.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Colca 03".ToUpper() && item.estado == 1)
                                    {
                                        chkColca03.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Sede Lima".ToUpper() && item.estado == 1)
                                    {
                                        chkOficinasLima.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Casona".ToUpper() && item.estado == 1)
                                    {
                                        chkCasona.Checked = true;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Lectura de registro Existente() 
                        chkColca01.Checked = false;
                        chkPacking.Checked = false;
                        chkColca03.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkOficinasLima.Checked = false;
                        chkCasona.Checked = false;

                        if (listadoSedesById != null)
                        {
                            if (listadoSedesById.ToList().Count > 0)
                            {
                                foreach (var item in listadoSedesById)
                                {
                                    if (item.sede.Trim().ToUpper() == "Colca 01".ToUpper() && item.estado == 1)
                                    {
                                        chkColca01.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Packing".ToUpper() && item.estado == 1)
                                    {
                                        chkPacking.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Colca 03".ToUpper() && item.estado == 1)
                                    {
                                        chkColca03.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Sede Lima".ToUpper() && item.estado == 1)
                                    {
                                        chkOficinasLima.Checked = true;
                                    }
                                    if (item.sede.Trim().ToUpper() == "Casona".ToUpper() && item.estado == 1)
                                    {
                                        chkCasona.Checked = true;
                                    }
                                }
                            }
                        }

                        dgvHardware.CargarDatos(listadoHardwareById.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>());
                        dgvHardware.Refresh();

                        dgvSoftware.CargarDatos(listadoSoftwareById.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>());
                        dgvSoftware.Refresh();

                        dgvDetalleLineaCelular.CargarDatos(listadoLineaCelularById.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>());
                        dgvDetalleLineaCelular.Refresh();

                        #endregion
                    }


                    if (this.txtIdEstado.Text == "PE")
                    {
                        btnNuevo.Enabled = true;
                        BarraPrincipal.Enabled = true;
                        progressBar1.Visible = false;
                        //btnRegistrar.Enabled = false;
                        //btnEditar.Enabled = true;
                        //btnAtras.Enabled = false;
                        //btnAnular.Enabled = true;
                        btnFlujoAprobacion.Enabled = true;
                        //gbDatosPersonal.Enabled = !false;
                        //gbDetale.Enabled = !false;
                        //gbDocumento.Enabled = !false;
                        //gbJustificacion.Enabled = !false;
                        //gbUbicacion.Enabled = !false;

                    }
                    else
                    {
                        btnNuevo.Enabled = true;
                        BarraPrincipal.Enabled = true;
                        progressBar1.Visible = false;
                        btnRegistrar.Enabled = false;
                        btnEditar.Enabled = false;
                        btnAtras.Enabled = false;
                        btnAnular.Enabled = false;
                        btnFlujoAprobacion.Enabled = true;
                    }
                    #endregion
                }

                #endregion

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

            */
            #endregion
        }

        private void bgwFlujoAprobacion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                modelo.ChangeStateDocument("SAS", solicitud);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "FLUJO DE APROBACION DEL DOCUMENTO");
            }
        }

        private void bgwFlujoAprobacion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Se ha cambiado el estado de la solicitud", "Confirmación del sistema");
            btnNuevo.Enabled = false;
            BarraPrincipal.Enabled = true;
            progressBar1.Visible = false;
            //gbDatosPersonal.Enabled = false;
            btnRegistrar.Enabled = false;
            //gbDetale.Enabled = false;
            //gbDocumento.Enabled = false;
            //gbJustificacion.Enabled = false;
            //gbUbicacion.Enabled = false;
            btnEditar.Enabled = false;

            if (solicitud.estadoCodigo == "V1")
            {

                this.btnVB1.Enabled = false;
                this.btnVB2.Enabled = true;
                this.btnBV3.Enabled = false;
                this.btnRechazarSolicitud.Enabled = true;
                btnHardwareActualizarLista.Enabled = false;
                btnSoftwareActualizarListado.Enabled = false;
            }
            else if (solicitud.estadoCodigo == "V2")
            {
                this.btnVB1.Enabled = false;
                this.btnVB2.Enabled = false;
                this.btnBV3.Enabled = true;
                this.btnRechazarSolicitud.Enabled = true;
                btnHardwareActualizarLista.Enabled = false;
                btnSoftwareActualizarListado.Enabled = false;
            }
            else if (solicitud.estadoCodigo == "AP")
            {
                this.btnVB1.Enabled = false;
                this.btnVB2.Enabled = false;
                this.btnBV3.Enabled = false;
                this.btnRechazarSolicitud.Enabled = false;
                btnHardwareActualizarLista.Enabled = false;
                btnSoftwareActualizarListado.Enabled = false;
            }
            else if (solicitud.estadoCodigo == "RE")
            {
                this.btnVB1.Enabled = false;
                this.btnVB2.Enabled = false;
                this.btnBV3.Enabled = false;
                this.btnRechazarSolicitud.Enabled = false;
                btnHardwareActualizarLista.Enabled = false;
                btnSoftwareActualizarListado.Enabled = false;
            }


            //this.txtIdEstado.Text = "V1";
            //this.txtEstado.Text = "Vº Bº 1";

            //this.btnVB1.Enabled = false;
            //this.btnVB2.Enabled = true;
            //this.btnBV3.Enabled = false;
            //this.btnRechazarSolicitud.Enabled = true;
        }

        private void btnVB1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Visto de Jefatura");
            try
            {
                solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.estadoCodigo = "V1";
                this.txtEstado.Text = "Vº Bº 1";
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        private void btnVB2_Click(object sender, EventArgs e)
        {
            try
            {
                solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.estadoCodigo = "V2";
                this.txtEstado.Text = "Vº Bº 2";
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtIdEstado.Text == "PE")
            {
                btnEditar.Enabled = false;
                btnRegistrar.Enabled = true;
                btnNuevo.Enabled = false;
                btnAtras.Enabled = true;
                gbDatosPersonal.Enabled = true;
                gbDetale.Enabled = true;
                gbDocumento.Enabled = true;
                gbJustificacion.Enabled = true;
                gbUbicacion.Enabled = true;

            }
            else
            {
                MessageBox.Show("EL documento no tiene el estado para realizar la edición del documento", "Mensaje ");
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

            if (this.txtIdEstado.Text == "PE")
            {
                btnEditar.Enabled = true;
                btnRegistrar.Enabled = false;
                btnNuevo.Enabled = true;
                btnAtras.Enabled = false;
            }
            else
            {
                MessageBox.Show("EL documento no tiene el estado para realizar la edición del documento", "Mensaje ");
            }
        }

        private void dgvHardware_KeyUp(object sender, KeyEventArgs e)
        {
            comboHelper = new ComboBoxHelper();

            if (((DataGridView)sender).RowCount > 0)
            {
                string estado = this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstado"].Value != null ? this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstado"].Value.ToString() : "0";
                string tipoHardware = this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIdHardware"].Value != null ? this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIdHardware"].Value.ToString() : string.Empty;
                string elegido = this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chElegido"].Value != null ? this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chElegido"].Value.ToString() : "0";

                if (elegido == "1")
                {
                    #region Opción de elegido esta en 01
                    if (estado == "1")// el estado tiene que ser 01, es decir activo 
                    {
                        #region Asingar código ERP()  
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chDispositivo")
                        {
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                search.ListaGeneralResultado = comboHelper.GetIdERPDevice("SAS", this.txtPersonalCodigo.Text, tipoHardware);
                                search.Text = "Obtener dispositivos del ERP";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chCodigoERP"].Value = search.ObjetoRetorno.Codigo;
                                    this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDispositivo"].Value = search.ObjetoRetorno.Descripcion;
                                }
                            }
                        }
                        #endregion


                        #region Asingar código ERP()  
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHardware")
                        {
                            if (e.KeyCode == Keys.F3)
                            {
                                comboHelper = new ComboBoxHelper();
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                search.ListaGeneralResultado = comboHelper.ObtenerListadoTipoDeDispositivos("SAS");
                                search.Text = "Obtener tipo de dispositivo ERP";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIdHardware"].Value = search.ObjetoRetorno.Codigo;
                                    this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHardware"].Value = search.ObjetoRetorno.Descripcion;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }


            }
        }

        private void dgvDetalleLineaCelular_KeyUp(object sender, KeyEventArgs e)
        {

            comboHelper = new ComboBoxHelper();

            if (((DataGridView)sender).RowCount > 0)
            {
                string estado = this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoDetalleCelular"].Value != null ? this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoDetalleCelular"].Value.ToString() : "0";
                string elegido = this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chElegidoDetalleCelular"].Value != null ? this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chElegidoDetalleCelular"].Value.ToString() : "0";

                if (elegido == "1")
                {
                    #region Opción de elegido esta en 01
                    if (estado == "1")// el estado tiene que ser 01, es decir activo 
                    {
                        #region Asingar código ERP()  
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chValorDetalleCelular")
                        {
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                search.ListaGeneralResultado = comboHelper.GetListCelNumberByIdPerson("SAS", this.txtPersonalCodigo.Text);
                                search.Text = "Obtener línea celulares asociadas al colaborador";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIdLineaDetalleCelular"].Value = search.ObjetoRetorno.Codigo;
                                    this.dgvDetalleLineaCelular.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chValorDetalleCelular"].Value = search.ObjetoRetorno.Descripcion;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }


            }

        }

        private void btnActualizarListaLineaCelular_Click(object sender, EventArgs e)
        {
            AddItemDetailCelNumber();
        }

        private void AddItemDetailCelNumber()
        {
            if (dgvDetalleLineaCelular.Rows.Count == 0)
            {
                listadoLineaCelularEnBlanco = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();
                listadoLineaCelularEnBlanco = modelo.ListDetailRequestByCelLineByIdRequestBlank("SAS", 0);

                dgvDetalleLineaCelular.CargarDatos(listadoLineaCelularEnBlanco.ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>());
                dgvDetalleLineaCelular.Refresh();

            }
            else
            {
                MessageBox.Show("ya existen datos en el detalle, no se pueden regenerar desde esta opción", "ADVERTENCIA DEL SISTEMA");
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
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                modelo.Notify(conection, "soporte@saturno.net.pe", "Solicitud de Equipamiento tecnológico", codigoSelecionado);

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

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void Preview()
        {
            if (txtCodigo.Text.Trim() != string.Empty)
            {
                if (txtCodigo.Text.Trim() != "0")
                {
                    if (cboTipoSolicitud.SelectedValue.ToString().Trim() == "1" || cboTipoSolicitud.SelectedValue.ToString().Trim() == "10" || cboTipoSolicitud.SelectedValue.ToString().Trim() == "8") //1   Alta
                    {
                        VistaPreviaSolicitudEquipamiento ofrm = new VistaPreviaSolicitudEquipamiento(Convert.ToInt32((txtCodigo.Text.Trim())), "ALTA");
                        ofrm.ShowDialog();
                    }
                    else if (cboTipoSolicitud.SelectedValue.ToString().Trim() == "3" || cboTipoSolicitud.SelectedValue.ToString().Trim() == "11" || cboTipoSolicitud.SelectedValue.ToString().Trim() == "6") //3   Baja
                    {
                        VistaPreviaSolicitudEquipamiento ofrm = new VistaPreviaSolicitudEquipamiento(Convert.ToInt32((txtCodigo.Text.Trim())), "BAJA");
                        ofrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se tiene configurado un formato para esta categoria de equipamiento", "MENSAJE DEL SISTEMA");
                    }

                    // id descripcion
                    //0-- Selecionar--
                    //1   Alta -- /////// ALTA /////// 
                    //2   Modificación
                    //3   Baja -- ****BAJA
                    //4   Renovación
                    //5   Línea Nueva 
                    //6   Suspención -- ***BAJA
                    //7   Activación
                    //8   Prestamo -- /////// ALTA /////// 
                    //9   Duplicado
                    //10  Prestamo equipo -- /////// ALTA /////// 
                    //11  Devolucion -- **** BAJA



                }


            }

        }

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {
            QuitarDetalleHardware();
        }

        private string ObtenerFormatoParaAgregarItemDetalle(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }

        private void AgregarDetalleHardware()
        {
            try
            {
                if (dgvHardware != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chId                  
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalleHardware))); // chItem
                    array.Add(string.Empty); // chIdHardware
                    array.Add(string.Empty); // chHardware                    
                    array.Add(0); // chCodigoERP
                    array.Add(string.Empty); //chDispositivo
                    array.Add(this.txtFecha.Text); // chDesde
                    array.Add(this.txtFecha.Text); //    chHasta                                     
                    array.Add(1); // chEstado
                    array.Add(0); // chValor
                    array.Add(string.Empty); // chGlosa
                    array.Add(0); // chActualizado
                    array.Add(0); // chElegido        
                    array.Add(0); // GeneraSolicitud
                    array.Add(0); // idReferenciaSoporteTecnico
                    array.Add(0); // RequiereCapacitacion
                    dgvHardware.AgregarFila(array);
                    ultimoItemEnListaDetalleHardware += 1;
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
        private void AgregarDetalleSoftware()
        {
            try
            {
                if (dgvSoftware != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); //       chIdSolicitudSoftware            
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalleSofware))); // chItemSoftware
                    array.Add(0); // chidSoftware
                    array.Add(string.Empty); //        chSoftware                                                    
                    array.Add(this.txtFecha.Text); // chDesdeSoftware
                    array.Add(this.txtFecha.Text); //      chHastaSoftware                                   
                    array.Add(1); // chEstadoSoftware
                    array.Add(0); // chValorSoftware
                    array.Add(string.Empty); // chGlosaSoftwar
                    array.Add(0); // chActualizadoSoftware
                    array.Add(0); // chElegidoSoftware        
                    array.Add(0); //chPerfilDeAcceso
                    array.Add(string.Empty); // chPerfilAcceso
                    array.Add(0); // chGeneraSolicitudSoft
                    array.Add(0); // chidReferenciaSoporteFuncionalSoft
                    array.Add(0); // chRequiereCapacitacionSoft
                    dgvSoftware.AgregarFila(array);
                    ultimoItemEnListaDetalleSofware += 1;
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
        private void AgregarDetalleLineaCelular()
        {

            try
            {
                if (dgvDetalleLineaCelular != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); //                 chIdDetalleCelular  
                    array.Add((ObtenerFormatoParaAgregarItemDetalle(ultimoItemEnListaDetalleCelulares))); // chitemDetalleCelular
                    array.Add(0); // chIdLineaDetalleCelular                                                           
                    array.Add(this.txtFecha.Text); // chDesdeDetalleCelular
                    array.Add(this.txtFecha.Text); //     chHastaDetalleCelular                                    
                    array.Add(1); // chEstadoDetalleCelular
                    array.Add(0); // chValorDetalleCelular
                    array.Add(string.Empty); // chGlosaDetalleCelular
                    array.Add(0); // chActualizadoDetalleCelular
                    array.Add(0); //         chElegidoDetalleCelular
                    array.Add(0); //chidreferenciaLineaCelular
                    array.Add(string.Empty); // chdocumentoReferenciaLineaCelular
                    dgvDetalleLineaCelular.AgregarFila(array);
                    ultimoItemEnListaDetalleCelulares += 1;
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

        private void QuitarDetalleHardware()
        {
            if (this.dgvHardware != null)
            {
                #region Quitar detalle Hardware() 
                if (dgvHardware.CurrentRow != null && dgvHardware.CurrentRow.Cells["chId"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvHardware.CurrentRow.Cells["chId"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvHardware.CurrentRow.Cells["chId"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvHardware.CurrentRow.Cells["chItem"].Value != null | dgvHardware.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvHardware.CurrentRow.Cells["chItem"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoHardwareAEliminar.Add(new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware
                                {
                                    idSolicitudEquipamientoTecnologico = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }

                        }

                        dgvHardware.Rows.Remove(dgvHardware.CurrentRow);
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
        private void QuitarDetalleSoftware()
        {
            if (this.dgvSoftware != null)
            {
                #region Quitar detalle Software() 
                if (dgvSoftware.CurrentRow != null && dgvSoftware.CurrentRow.Cells["chIdSolicitudSoftware"].Value != null)
                {
                    try
                    {
                        Int32 dispositivoCodigo = (dgvSoftware.CurrentRow.Cells["chIdSolicitudSoftware"].Value.ToString().Trim() != string.Empty ? Convert.ToInt32(dgvSoftware.CurrentRow.Cells["chIdSolicitudSoftware"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvSoftware.CurrentRow.Cells["chItemSoftware"].Value != null | dgvSoftware.CurrentRow.Cells["chItemSoftware"].Value.ToString().Trim() != string.Empty) ? (dgvSoftware.CurrentRow.Cells["chItemSoftware"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {
                                listadoSoftwareAEliminar.Add(new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware
                                {
                                    idSolicitudEquipamientoTecnologico = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }
                        dgvSoftware.Rows.Remove(dgvSoftware.CurrentRow);
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
        private void QuitarDetalleLineaCelular()
        {
            if (this.dgvDetalleLineaCelular != null)
            {
                #region Quitar detalle Linea Celular() 
                if (dgvDetalleLineaCelular.CurrentRow != null && dgvDetalleLineaCelular.CurrentRow.Cells["chIdDetalleCelular"].Value != null)
                {
                    try
                    {
                        Int32 dispositivoCodigo = (dgvDetalleLineaCelular.CurrentRow.Cells["chIdDetalleCelular"].Value.ToString().Trim() != string.Empty ? Convert.ToInt32(dgvDetalleLineaCelular.CurrentRow.Cells["chIdDetalleCelular"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvDetalleLineaCelular.CurrentRow.Cells["chitemDetalleCelular"].Value != null | dgvDetalleLineaCelular.CurrentRow.Cells["chitemDetalleCelular"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleLineaCelular.CurrentRow.Cells["chitemDetalleCelular"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {
                                listadoLineaCelularAEliminar.Add( new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular
                                {
                                    idSolicitudEquipamientoTecnologico = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }
                        dgvDetalleLineaCelular.Rows.Remove(dgvDetalleLineaCelular.CurrentRow);
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

        private void CambiarEstadoDetalleHardware()
        {

        }
        private void CambiarEstadoDetalleSoftware()
        {

        }
        private void CambiarEstadoDetalleLineaCelular()
        {

        }

        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {
            AgregarDetalleHardware();
        }

        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {
            CambiarEstadoDetalleHardware();
        }

        private void btnDetalleCambiarEstadoSoftware_Click(object sender, EventArgs e)
        {
            CambiarEstadoDetalleSoftware();
        }

        private void btnDetalleAgregarSoftware_Click(object sender, EventArgs e)
        {
            AgregarDetalleSoftware();
        }

        private void btnDetalleQuitarSoftware_Click(object sender, EventArgs e)
        {
            QuitarDetalleSoftware();
        }

        private void btnDetalleCambiarEstadoLineaCelular_Click(object sender, EventArgs e)
        {
            CambiarEstadoDetalleLineaCelular();
        }

        private void btnVerDispositivo_Click(object sender, EventArgs e)
        {

        }

        private void btnVerDocumentoDeReferencia_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleAgregarLineaCelular_Click(object sender, EventArgs e)
        {
            AgregarDetalleLineaCelular();
        }

        private void btnDetalleQuitarLineaCelular_Click(object sender, EventArgs e)
        {
            QuitarDetalleLineaCelular();
        }

        private void btnVB3_Click(object sender, EventArgs e)
        {
            try
            {
                solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.estadoCodigo = "AP";
                this.txtEstado.Text = "APROBADO";
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void btnRechazarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                solicitud.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                solicitud.estadoCodigo = "RE";
                this.txtEstado.Text = "RECHAZADO";
                bgwFlujoAprobacion.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

    }
}
