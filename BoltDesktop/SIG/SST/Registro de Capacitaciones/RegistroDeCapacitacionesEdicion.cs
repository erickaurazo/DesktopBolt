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

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class RegistroDeCapacitacionesEdicion : Form
    {

        #region Variables()
        string nombreformulario = "AtencionesSoporteFuncional";
        private PrivilegesByUser privilegiosDeUsuarioEnSesion;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades;
        private string CompaniaID;
        private string ConexionABaseDeDatos;
        private SAS_USUARIOS UsuarioEnSesion;
        private GlobalesHelper globalHelper;
        private string result;
        private string CodigoSelecionado = string.Empty;
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
            CodigoSelecionado = string.Empty ;
            Inicio();
            CargarCombos();
            // btnGenerarReprogramacion.Enabled = false;
            AperturarFormulario();
        }

        public RegistroDeCapacitacionesEdicion(string _ConexionABaseDeDatos, SAS_USUARIOS _UsuarioEnSesion, string _CompaniaID, PrivilegesByUser _privilegiosDeUsuarioEnSesion, string _CodigoSelecionado)
        {
            InitializeComponent();
            nombreformulario = "RegistroDeCapacitaciones";
            ConexionABaseDeDatos = _ConexionABaseDeDatos;
            UsuarioEnSesion = _UsuarioEnSesion;
            CompaniaID = _CompaniaID;
            privilegiosDeUsuarioEnSesion = _privilegiosDeUsuarioEnSesion;
            CodigoSelecionado = _CodigoSelecionado;
            Inicio();
            CargarCombos();
            // btnGenerarReprogramacion.Enabled = false;
            AperturarFormulario();

        }

        private void RegistroDeCapacitacionesEdicion_Load(object sender, EventArgs e)
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
                cboSerie.DataSource = series.ToList();

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
