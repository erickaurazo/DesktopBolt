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
using MyDataGridViewColumns;
using System.Drawing;


namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class MotivosDeExoneracionACamaraDeGasificados : Form
    {
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_ListadoDeRegistrosExoneradosByDatesResult selectedItem;
        public MesController MesesNeg;
        private List<SAS_ListadoDeRegistrosExoneradosByDatesResult> result;
        SAS_RegistroTicketCamaraGasificadoExoneradosController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_ListadoDeRegistrosExoneradosByDatesResult selectItemById;

        public MotivosDeExoneracionACamaraDeGasificados()
        {
            InitializeComponent();
        }

        public MotivosDeExoneracionACamaraDeGasificados(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            //selectedItem = new SAS_ListadoDeRegistrosExoneradosByDatesResult();
            //selectedItem.codigoExoneracion = 0;

            //CargarMeses();
            //ObtenerFechasIniciales();
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            //Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            //gbCabecera.Enabled = false;
            //gbList.Enabled = false;
            //Consult();
        }

        private void MotivosDeExoneracionACamaraDeGasificados_Load(object sender, EventArgs e)
        {

        }


    }
}
