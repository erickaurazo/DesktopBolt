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
using Telerik.WinControls.UI.Localization;
using System.Configuration;

namespace ComparativoHorasVisualSATNISIRA.Costos
{
    public partial class ActualizacionFechaAplicacionCianamidaEdicion : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private CentroDeCostosController Modelo;
        private List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult> listado;
        private SAS_ConsumidorFechaAplicacionCListadoPorAnioResult otipo;
        private SAS_ConsumidorFechaAplicacionCListadoPorAnioResult oDetalle;
        private string fileName = string.Empty;
        private bool exportVisualSettings = false;
        private ConsumidorFechaAplicacionC oCampanaCultivo;
        private int resultadoDelRegistro;

        public ActualizacionFechaAplicacionCianamidaEdicion()
        {
            InitializeComponent();
            Inicio();
        }

        public ActualizacionFechaAplicacionCianamidaEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_ConsumidorFechaAplicacionCListadoPorAnioResult _detalle)
        {
            InitializeComponent();
            
            Limpiar(this, gbCentroDeCostos);
            Limpiar(this, gbFechaPodas);
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            //Inicio();
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            oDetalle = _detalle;
            AsignarInformacionAControler(oDetalle);
        }

        private void AsignarInformacionAControler(SAS_ConsumidorFechaAplicacionCListadoPorAnioResult oDetalle)
        {
            this.txtEmpresaCodigo.Text = oDetalle.IdEmpresa != null ? oDetalle.IdEmpresa.ToString().Trim() : string.Empty;
            this.txtEmpresa.Text = "SOCIEDAD AGRICOLA SATURNO";
            this.txtConsumidorCodigo.Text = oDetalle.ConsumidorId != null ? oDetalle.ConsumidorId.ToString().Trim() : string.Empty;
            this.txtConsumidorDescripcion.Text = oDetalle.ConsumidorId != null ? "Lote " + oDetalle.ConsumidorId.ToString().Trim() : string.Empty;
            this.txtSiembraCodigo.Text = oDetalle.SiembraId != null ? oDetalle.SiembraId.ToString().Trim() : string.Empty;
            this.txtSiembraDescripcion.Text = oDetalle.SiembraId != null ? "Siembra Nro " + oDetalle.SiembraId.ToString().Trim() : string.Empty;
            this.txtCampañaCodigo.Text = oDetalle.CampanaAgricolaId != null ? oDetalle.CampanaAgricolaId.ToString().Trim() : string.Empty;
            this.txtCampañaDescripcion.Text = oDetalle.AnioCampaña != null ? "Año de la campaña : " + oDetalle.AnioCampaña.ToString().Trim() : string.Empty;
            this.txtFechaInicioCampaña.Text = oDetalle.inicioCampana != (DateTime?)null ? oDetalle.inicioCampana.ToPresentationDate().ToString().Trim() : string.Empty;
            this.txtFechaFinalCampaña.Text = oDetalle.finalCampana != (DateTime?)null ? oDetalle.finalCampana.ToPresentationDate().ToString().Trim() : string.Empty;
            this.txtFechaPodaFormacion.Text = oDetalle.FechaAplicacionEnFormacion != (DateTime?)null ? oDetalle.FechaAplicacionEnFormacion.ToPresentationDate().ToString().Trim() : string.Empty;
            this.txtFechaPodaProduccion.Text = oDetalle.FechaAplicacionEnProduccion != (DateTime?)null ? oDetalle.FechaAplicacionEnProduccion.ToPresentationDate().ToString().Trim() : string.Empty;
        }

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[conection != null ? conection : "SAS"].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
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


        private void ActualizacionFechaAplicacionCianamidaEdicion_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos() == true)
            {
                oCampanaCultivo = new ConsumidorFechaAplicacionC();
                oCampanaCultivo.IdEmpresa = oDetalle.IdEmpresa != null ? oDetalle.IdEmpresa.ToString().Trim() : string.Empty;
                oCampanaCultivo.ConsumidorId = oDetalle.ConsumidorId != null ? oDetalle.ConsumidorId.ToString().Trim() : string.Empty;
                oCampanaCultivo.SiembraId = oDetalle.SiembraId != null ? oDetalle.SiembraId.ToString().Trim() : string.Empty;
                oCampanaCultivo.CampanaAgricolaId = oDetalle.CampanaAgricolaId != null ? oDetalle.CampanaAgricolaId.ToString().Trim() : string.Empty;
                oCampanaCultivo.FechaAplicacionEnFormacion = this.txtFechaPodaFormacion.Text != string.Empty ? Convert.ToDateTime(this.txtFechaPodaFormacion.Text) : (DateTime?)null;
                oCampanaCultivo.FechaAplicacionEnProduccion = this.txtFechaPodaProduccion.Text != string.Empty ? Convert.ToDateTime(this.txtFechaPodaProduccion.Text) : (DateTime?)null;
                gbCentroDeCostos.Enabled = false;
                gbFechaPodas.Enabled = false;
                btnCancelar.Enabled = false;
                btnGrabar.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
        }

        private bool ValidarDatos()
        {
            bool estado = true;

            string ASCD = this.txtValidar.Text.ToString().Trim();
            if (this.txtFechaPodaFormacion.Text.ToString().Trim() == ASCD)
            {
                MessageBox.Show("Debe ingresar una fecha en el formato validado dd/MM/yyyy", "Notificación del sistema");
                this.txtFechaPodaFormacion.Focus();
                estado = false;
                return estado;
            }

            if (this.txtFechaPodaProduccion.Text.ToString().Trim() == ASCD)
            {
                MessageBox.Show("Debe ingresar una fecha en el formato validado dd/MM/yyyy", "Notificación del sistema");
                this.txtFechaPodaProduccion.Focus();
                estado = false;
                return estado;
            }

            return estado;
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo = new CentroDeCostosController();
                resultadoDelRegistro = Modelo.ActualizarFechaAplicacionCianamida(conection, oCampanaCultivo);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Source.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (resultadoDelRegistro == 1)
                {
                    MessageBox.Show("Registro actualizado correctamente", "MENSAJE DEL SISTEMA");
                    gbCentroDeCostos.Enabled = false;
                    gbFechaPodas.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnGrabar.Enabled = false;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Source.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void ActualizacionFechaAplicacionCianamidaEdicion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
