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

namespace ComparativoHorasVisualSATNISIRA.Evaluaciones_agricolas
{
    public partial class Lotes : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private CentroDeCostosController Modelo;
        private List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo> Listado;
        private SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo oDetalle;

        private string fileName;
        private bool exportVisualSettings;
        private string añoCampana;
        private string cultivoId;

        public Lotes()
        {
            InitializeComponent();
        }


        public Lotes(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            oDetalle = new SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo();
            oDetalle.idConsumidor = string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            Consultar();

        }

        private void Consultar()
        {
            
        }

        private void Lotes_Load(object sender, EventArgs e)
        {

        }
    }
}
