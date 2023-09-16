using System;
using System.ComponentModel;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.Configuration;
using Asistencia.Datos;
using Asistencia.Negocios;


namespace Asistencia
{
    public partial class GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta : Form
    {
        
        private RegistroTransferenciaTransportesController negocio;
        private string PlacaNew;
        private string routerIdNew;
        private string _conection;
        private SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult _asistance;
        private string _companyId;
        private ASJ_USUARIOS _user;

        public GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta()
        {
            InitializeComponent();
            Inicio();
        }

        public GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta(SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult asistance, string conection, string companyId, ASJ_USUARIOS user)
        {
            InitializeComponent();
            _conection = conection;
            _asistance = asistance;
            _companyId = companyId;
            _user = user;
            Inicio();
            txtFecha.Text = _asistance.fecha;
            txtEmpresaTransporteRUC.Text = _asistance.categoriaMovilidad != null ? _asistance.categoriaMovilidad : string.Empty;
            txtEmpresaTransporte.Text = _asistance.empresaTransporte != null ? _asistance.empresaTransporte : string.Empty;
            txtPlaca.Text = _asistance.placa != null ? _asistance.placa : string.Empty;
            txtRuta.Text = _asistance.ruta != null ? _asistance.ruta : string.Empty;
            txtRouteId.Text = _asistance.IDRUTAORIGEN != null ? _asistance.IDRUTAORIGEN.ToString() : "0";

        }

        public void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ActualizarPlaca_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            PlacaNew = this.txtPlacaCodigoNuevo.Text.Trim() != string.Empty ? this.txtPlacaCodigoNuevo.Text.Trim() : txtPlaca.Text.ToString().Trim();
            routerIdNew = this.txtRutaCodigo.Text.Trim() != string.Empty ? this.txtRutaCodigo.Text.Trim() : txtRouteId.Text.ToString().Trim();
            gbDestino.Enabled = false;
            gbRegistro.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                negocio = new RegistroTransferenciaTransportesController();
                negocio.UpdatePlaca(_conection, _asistance, PlacaNew, routerIdNew);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");                
                return;
                gbDestino.Enabled = !false;
                gbRegistro.Enabled = !false;
            }

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("ACTUALIZACION SATISFACTORIA", "MENSAJE DEL SISTEMA");
                gbDestino.Enabled = !false;
                gbRegistro.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                gbDestino.Enabled = !false;
                gbRegistro.Enabled = !false;
                return;
            }

        }
    }
}
