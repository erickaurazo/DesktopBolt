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
using System.Reflection;
using ComparativoHorasVisualSATNISIRA.T.I;


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ReportesListadoSolicitudesRenovacionCelular : Form
    {
        private int esAgrupado;
        private List<SAS_ListadoMacByIpByColaborador> listado;
        private SAS_DispostivoController modelo;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoMacByIpByColaborador odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;
        private int codigoDispotivo = 0;

        public ReportesListadoSolicitudesRenovacionCelular()
        {
            InitializeComponent();
        }


        public ReportesListadoSolicitudesRenovacionCelular(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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
            Consultar();
        }


        private void Consultar()
        {
            try
            {
                btnMenu.Enabled = !true;
                gbList.Enabled = !true;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void ReportesListadoSolicitudesRenovacionCelular_Load(object sender, EventArgs e)
        {

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }
    }
}
