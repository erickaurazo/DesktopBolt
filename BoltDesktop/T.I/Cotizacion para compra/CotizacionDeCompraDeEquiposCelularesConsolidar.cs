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
    public partial class CotizacionDeCompraDeEquiposCelularesConsolidar : Form
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
        private string fecha = string.Empty;
        private SAS_RegistroCompraEquipoCelularController modelo;
        private List<SAS_RegistroCompraEquipoCelularesDetallePendientesResult> listadoCelularesPendientes;

        public CotizacionDeCompraDeEquiposCelularesConsolidar()
        {
            InitializeComponent();
        }

        public CotizacionDeCompraDeEquiposCelularesConsolidar(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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


        protected override void OnLoad(EventArgs e)
        {
            this.dgvListado.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chnombresCompletos", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }



        private void ConsolidarCompraDeEquiposCelulares_Load(object sender, EventArgs e)
        {
            this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new SAS_RegistroCompraEquipoCelularController();
            listadoCelularesPendientes = new List<SAS_RegistroCompraEquipoCelularesDetallePendientesResult>();
            listadoCelularesPendientes = modelo.GetListDetailToDatePendiente("SAS", fecha).ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvListado.DataSource = listadoCelularesPendientes.ToDataTable<SAS_RegistroCompraEquipoCelularesDetallePendientesResult>();
            dgvListado.Refresh();
            
            btnProcesar.Enabled = !false;
            gbCabecera.Enabled = !false;
            gbListado.Enabled = !false;
            progressBar1.Visible = !true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            fecha = this.txtFechaHasta.Text;
            btnProcesar.Enabled = false;
            gbCabecera.Enabled = false;
            gbListado.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();
        }
    }
}
