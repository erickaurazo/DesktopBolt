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

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class TipoDeMantenimientos : Form
    {

        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private bool exportVisualSettings;
        private string fileName;
        private SAS_DispositivoTipoSoftwareController Modelo;
        private List<SAS_DispositivoTipoSoftwareListado> listado;
        private SAS_DispositivoTipoSoftware otipo;
        private ComboBoxHelper comboHelper;
        private List<Grupo> clasificacionesSoftware;
        private SAS_DispositivoTipoSoftwareListado oDetalle;
        private SAS_DispositivoTipoSoftware oDetalleTipoSoftware;

        public TipoDeMantenimientos()
        {
            InitializeComponent();
        }

        public TipoDeMantenimientos(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            //CargarCombos();
            //Actualizar();
        }

        private void TipoDeMantenimientos_Load(object sender, EventArgs e)
        {

        }
    }
}
