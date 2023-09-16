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


namespace ComparativoHorasVisualSATNISIRA.Logistica
{
    public partial class LiberarOrdenesDeCompra : Form
    {
        private PedidoControllers modelo;
        private string desde;
        private string hasta;
        private List<SAS_SeguimientoOrdenCompraResult> ListadoOrdenesCompra;

        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user2;
        private string _companyId = string.Empty;
        private string _conection = string.Empty;
        private string _formulario = string.Empty;

        public MesController MesesNeg { get; private set; }

        public LiberarOrdenesDeCompra()
        {
            InitializeComponent();
            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = _conection;
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = Environment.UserName;
            _user2.NombreCompleto = Environment.MachineName;

            _companyId = "001";
            _privilege = new PrivilegesByUser();
            _privilege.nuevo = 1;
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        private void Actualizar()
        {
            desde = this.txtFechaDesde.Text;
            hasta = this.txtFechaHasta.Text;
            gbConsulta.Enabled = false;
            gbListado.Enabled = false;
            progressBar1.Visible = true;
        }

        private void CargarMeses()
        {
            try
            {
                MesesNeg = new MesController();
                cboMes.DisplayMember = "descripcion";
                cboMes.ValueMember = "valor";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboMes.DataSource = MesesNeg.ListarMeses().ToList();
                cboMes.SelectedValue = DateTime.Now.ToString("MM");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return;
            }

        }

        private void ObtenerFechasIniciales()
        {
            try
            {
                this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
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



        public LiberarOrdenesDeCompra(string conection, SAS_USUARIOS user2, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = conection;
            _user2 = user2;
            _companyId = companyId;
            _privilege = privilege;
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new PedidoControllers();
            ListadoOrdenesCompra = new List<SAS_SeguimientoOrdenCompraResult>();
            ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, string.Empty).ToList();
        }

        private void LiberarOrdenesDeCompra_Load(object sender, EventArgs e)
        {

        }
    }
}
