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

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class RegistroDeCompraEquiposCelularesEdicion : Form
    {

        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado;
        private SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult itemSeleccionado = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult();
        private SAS_SolicitudDeRenovacionTelefoniaCelularController Modelo;
        private SAS_SolicitudDeRenovacionTelefoniaCelular solicitud;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado1;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado2;

        public MesController MesesNeg;
        public string FechaDesdeConsulta;
        public string FechaHastaConsulta;

        public RegistroDeCompraEquiposCelularesEdicion()
        {
            InitializeComponent();
        }


        public RegistroDeCompraEquiposCelularesEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            //Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            //Actualizar();

        }

        private void RegistroDeCompraEquiposCelularesEdicion_Load(object sender, EventArgs e)
        {

        }
    }
}
