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
using Asistencia.Negocios.SIG.SST.Registro_de_Capacitaciones;
using System.Device.Location;

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class RegistroDeCapacitacionesEdicion : Form
    {

        #region Variables()
        string nombreformulario = "RegistroDeCapacitaciones";
        private ListadoRegistroCapacitacionesPorIDResult ItemAEdicion;
        private PrivilegesByUser privilegiosDeUsuarioEnSesion;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades;
        private string CompaniaID = "001";
        private string ConexionABaseDeDatos = "SSOMA";
        private SAS_USUARIOS UsuarioEnSesion;
        private GlobalesHelper globalHelper;
        private string result;
        private string ID = string.Empty;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private RegistroDeCapacitacionesController Model;
        private bool Validacion;
        private CapacitacionCabecera CapacitacionARegistrar;

        public string mensajeError { get; private set; }
        #endregion





        public RegistroDeCapacitacionesEdicion()
        {
            InitializeComponent();
            nombreformulario = "RegistroDeCapacitaciones";
            ConexionABaseDeDatos = "SSOMA";
            UsuarioEnSesion = new SAS_USUARIOS();
            UsuarioEnSesion.IdUsuario = "EAURAZO";
            UsuarioEnSesion.NombreCompleto = "Erick Aurazo";
            CompaniaID = "001";
            privilegiosDeUsuarioEnSesion = new PrivilegesByUser();
            privilegiosDeUsuarioEnSesion.nuevo = 1;
            privilegiosDeUsuarioEnSesion.editar = 1;
            privilegiosDeUsuarioEnSesion.eliminar = 1;
            ID = string.Empty;
            Inicio();
            CargarCombos();
            // btnGenerarReprogramacion.Enabled = false;
            AperturarFormulario();
        }

        public RegistroDeCapacitacionesEdicion(string _ConexionABaseDeDatos, SAS_USUARIOS _UsuarioEnSesion, string _CompaniaID, PrivilegesByUser _privilegiosDeUsuarioEnSesion, string _ID)
        {
            InitializeComponent();
            nombreformulario = "RegistroDeCapacitaciones";
            ConexionABaseDeDatos = _ConexionABaseDeDatos;
            UsuarioEnSesion = _UsuarioEnSesion;
            CompaniaID = _CompaniaID;
            privilegiosDeUsuarioEnSesion = _privilegiosDeUsuarioEnSesion;
            ID = _ID;
            Inicio();
            CargarCombos();
            AperturarFormulario();

        }

        private void RegistroDeCapacitacionesEdicion_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            RealizarConsultas();
        }

        private void RealizarConsultas()
        {
            try
            {
                Model = new RegistroDeCapacitacionesController();
                ItemAEdicion = new ListadoRegistroCapacitacionesPorIDResult();
                ItemAEdicion = Model.ObtenerRegistroDeCapacitacionesDesdeID(ConexionABaseDeDatos, ID);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void PresentarResultados()
        {

            try
            {
                if (ItemAEdicion != null)
                {
                    txtCodigo.Text = ItemAEdicion.CapacitacionID;
                    txtFolio.Text = ItemAEdicion.Folio;
                    //txtValidar.Text = string.Empty;
                    txtEstadoID.Text = ItemAEdicion.EstadoID;
                    txtEstado.Text = ItemAEdicion.Estado;
                    txtTipoDeCapacitacionID.Text = ItemAEdicion.CapacitacionTipoID;
                    txtTipoDeCapacitacion.Text = ItemAEdicion.Capacitacion;
                    //cboDocumento.Text = string.Empty;
                    //cboSerie.Text = string.Empty;
                    txtCorrelativo.Text = ItemAEdicion.Folio;
                    txtFecha.Text = ItemAEdicion.FechaCapacitacion.ToPresentationDate();
                    txtUbicacion.Text = ItemAEdicion.Ubicación;
                    txtLatitudLongitud.Text = ItemAEdicion.LatLong;
                    txtObservacion.Text = ItemAEdicion.Observacion;
                    txtInicio.Text = ItemAEdicion.HoraInicio.Value.ToPresentationDateTime();
                    txtFin.Text = ItemAEdicion.HoraFin.Value.ToPresentationDateTime();
                    txtDuracion.Text = ItemAEdicion.Duracion.Value.ToString();
                    txtReferencia.Text = ItemAEdicion.ReferenciaID != null ? ItemAEdicion.ReferenciaID.Trim() : string.Empty;
                    txtpdfRuta.Text = ItemAEdicion.PDFRuta != null ? ItemAEdicion.PDFRuta.Trim() : string.Empty;

                    BarraPrincipal.Enabled = !false;
                    gbCabecera.Enabled = !false;
                    gbDetalle.Enabled = !false;
                    progressBar1.Visible = !true;

                    if (ItemAEdicion.CapacitacionID == string.Empty)
                    {
                        ActivarControlesParaEdicion(true);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtEstadoID.Text.Trim() == "PE")
            {
                ActivarControlesParaEdicion(true);
            }
            else
            {
                MessageBox.Show("El documento no tiene el estado para su edición");
            }
        }

        private void ActivarControlesParaEdicion(bool Accion)
        {
            if (Accion == true)
            {
                #region Editable()
                txtCodigo.ReadOnly = Accion;
                txtFolio.ReadOnly = Accion;
                txtValidar.ReadOnly = Accion;
                txtEstadoID.ReadOnly = Accion;
                txtEstado.ReadOnly = Accion;
                txtTipoDeCapacitacionID.ReadOnly = !Accion;
                txtTipoDeCapacitacion.ReadOnly = Accion;
                txtTipoDeCapacitacionID.Enabled = Accion;
                txtTipoDeCapacitacion.Enabled = Accion;

                txtCorrelativo.ReadOnly = Accion;
                txtFecha.ReadOnly = !Accion;
                txtUbicacion.ReadOnly = !Accion;
                txtLatitudLongitud.ReadOnly = !Accion;
                txtObservacion.ReadOnly = !Accion;
                txtObservacion.Enabled = Accion;
                txtInicio.ReadOnly = !Accion;
                txtFin.ReadOnly = !Accion;
                txtDuracion.ReadOnly = Accion;
                btnBuscarTipoDeCapacitacion.Enabled = Accion;
                #endregion

            }
            else
            {
                #region No editable()
                txtCodigo.ReadOnly = !Accion;
                txtFolio.ReadOnly = !Accion;
                txtValidar.ReadOnly = !Accion;
                txtEstadoID.ReadOnly = !Accion;
                txtEstado.ReadOnly = !Accion;
                txtTipoDeCapacitacionID.ReadOnly = !Accion;
                txtTipoDeCapacitacion.ReadOnly = !Accion;
                txtCorrelativo.ReadOnly = !Accion;
                txtFecha.ReadOnly = !Accion;
                txtUbicacion.ReadOnly = !Accion;
                txtLatitudLongitud.ReadOnly = !Accion;
                txtObservacion.ReadOnly = !Accion;
                txtObservacion.Enabled = Accion;
                txtInicio.ReadOnly = !Accion;
                txtFin.ReadOnly = !Accion;
                txtDuracion.ReadOnly = !Accion;
                btnBuscarTipoDeCapacitacion.Enabled = !Accion;
                txtTipoDeCapacitacionID.Enabled = Accion;
                txtTipoDeCapacitacion.Enabled = Accion;

                #endregion
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivarControlesParaEdicion(true);
            Limpiar();

        }

        private void Limpiar()
        {
            ID = string.Empty;
            AperturarFormulario();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidacionRegistro() == true)
            {
                CapacitacionARegistrar = new CapacitacionCabecera();
                CapacitacionARegistrar = ObtenerObjetoCabecera();
                Model = new RegistroDeCapacitacionesController();
                ID = Model.Registrar(ConexionABaseDeDatos, CapacitacionARegistrar);
                AperturarFormulario();
                ActivarControlesParaEdicion(false);
            }
            else
            {
                MessageBox.Show(mensajeError, "MENSAJES DE VALIDACION DEL SISTEMA");
            }


        }

        private CapacitacionCabecera ObtenerObjetoCabecera()
        {
            CapacitacionCabecera Objeto = new CapacitacionCabecera();
            try
            {
                Objeto.CapacitacionID = txtCodigo.Text.Trim();
                Objeto.CapacitacionTipoID = txtTipoDeCapacitacionID.Text.Trim();
                Objeto.FechaCapacitacion = Convert.ToDateTime( txtFecha.Text.Trim());
                Objeto.Ubicación = txtUbicacion.Text.Trim();
                Objeto.LatLong = txtLatitudLongitud.Text.Trim();
                Objeto.HoraInicio = Convert.ToDateTime(txtInicio.Text.Trim());
                Objeto.HoraFin = Convert.ToDateTime(txtFin.Text.Trim());
                Objeto.Observacion = txtObservacion.Text.Trim();
                Objeto.FechaRegistro = DateTime.Now;
                Objeto.PDFRuta = this.txtpdfRuta.Text.Trim();
                Objeto.PDFPrint = 0;
                Objeto.EstadoID = txtEstadoID.Text.Trim();
                //Capacitacion.Correlativo = txtTipoDeCapacitacionID.Text.Trim();
                Objeto.IdReferencia = txtReferencia.Text.Trim();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

            }

            return Objeto;

        }

        private bool ValidacionRegistro()
        {
            Validacion = true;
            mensajeError = string.Empty;

            if (this.txtTipoDeCapacitacionID.Text.Trim() == string.Empty)
            {
                Validacion = false;
                mensajeError += "\n Debe ingresar un código de tipo de capacitacion válida";
            }

            if (this.txtTipoDeCapacitacion.Text.Trim() == string.Empty)
            {
                Validacion = false;
                mensajeError += "\n Debe ingresar un tipo de capacitación válida";
            }


            string ASCD = this.txtValidar.Text.ToString().Trim();
            if (this.txtFecha.Text.ToString().Trim() == ASCD)
            {
                mensajeError += "\n Debe ingresar una fecha en el formato validado dd/MM/yyyy";

                Validacion = false;
            }


            if (this.txtInicio.Text.ToString().Trim() == ASCD)
            {
                mensajeError += "\n Debe Inicio una fecha en el formato validado dd/MM/yyyy";

                Validacion = false;
            }

            if (this.txtFin.Text.ToString().Trim() == ASCD)
            {
                mensajeError += "\n Debe Inicio una fecha en el formato validado dd/MM/yyyy";

                Validacion = false;
            }


            return Validacion;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            ActivarControlesParaEdicion(false);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ActivarControlesParaEdicion(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ActivarControlesParaEdicion(false);
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

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Attach()
                if (this.txtCodigo.Text != string.Empty)
                {
                    if (this.txtCodigo.Text != "0")
                    {
                        string codigoSelecionado = Convert.ToString(this.txtCodigo.Text);
                        AdjuntarArchivos ofrm = new AdjuntarArchivos("SAS", UsuarioEnSesion, CompaniaID, privilegiosDeUsuarioEnSesion, codigoSelecionado.ToString(), nombreformulario);
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

        private void RegistroDeCapacitacionesEdicion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {

        }

        private void btnLatLong_Click(object sender, EventArgs e)
        {
            GetGeolocation();
        }

        private void GetGeolocation()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += (s, ev) =>
            {
                double latitude = ev.Position.Location.Latitude;
                double longitude = ev.Position.Location.Longitude;

                // Actualiza los labels
                txtLatitudLongitud.Text = "Latitude: " + latitude;
                txtObservacion.Text = "Longitude: " + longitude;

                // Deja de observar después de obtener la primera ubicación
                watcher.Stop();
            };

            watcher.Start();
        }

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }


        #region Métodos()
        private void AperturarFormulario()
        {
            BarraPrincipal.Enabled = false;
            gbCabecera.Enabled = false;
            gbDetalle.Enabled = false;
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


                documentos = comboHelper.GetDocumentTypeForForm("SAS", "RegistroDeCapacitaciones");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                series = comboHelper.GetDocumentSeriesForForm("SAS", "RegistroDeCapacitaciones");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.Where(x => x.Codigo == DateTime.Now.Year.ToString()).ToList();

                //this.txtCorrelativo.Text = "0".PadLeft(7, '0');



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
                Globales.BaseDatos = ConfigurationManager.AppSettings[ConexionABaseDeDatos].ToString();
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


        #endregion
    }
}
