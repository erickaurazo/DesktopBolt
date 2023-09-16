using System;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;

namespace Asistencia
{
    public partial class GoPlanillasReporteAsistenciasObservadasActualizarDNI : Form
    {                
        private string _conection;
        private string _companyId;
        private ASJ_USUARIOS _user;
        private string _controlIngresoSalidaId;
        private string _item;
        private string _period;
        private string _name;
        private string _dni;

        public ControlIngresoSalidaPersonalController AsistenciaModelo { get; private set; }

        public GoPlanillasReporteAsistenciasObservadasActualizarDNI()
        {
            InitializeComponent();
            Inicio();
        }


        public GoPlanillasReporteAsistenciasObservadasActualizarDNI(string conection, string companyId, ASJ_USUARIOS user)
        {
            InitializeComponent();
            _conection = conection;
            _companyId = companyId;
            _user = user;
            Inicio();
        }



        public GoPlanillasReporteAsistenciasObservadasActualizarDNI(string period, string controlIngresoSalidaId, string item, string name, string dni, string conection, string companyId, ASJ_USUARIOS user)
        {
            InitializeComponent();
            _controlIngresoSalidaId = controlIngresoSalidaId;
            _item = item;
            _period = period;
            _name = name;
            _dni = dni;
            _conection = conection;
            _companyId = companyId;
            _user = user;

            this.txtdniObservado.Text = _dni;
            this.txtNombresObservador.Text = _name;

            Inicio();
        }

        private void ActualizarDNIObservado_Load(object sender, EventArgs e)
        {

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controlIngresoSalidaId != string.Empty && _item != string.Empty)
                {
                    if (this.txtDNI.Text.Trim() != string.Empty && this.txtNombres.Text.Trim() != string.Empty)
                    {
                        AsistenciaModelo = new ControlIngresoSalidaPersonalController();
                        AsistenciaModelo.UpdateObservedListDNI(_conection, _controlIngresoSalidaId, _item, this.txtDNI.Text.Trim());
                        MessageBox.Show("Actualizado correctamente", "MENSAJE DEL SISTEMA");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;

            }

        }
    }
}
