using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using System.Reflection;
using Telerik.WinControls.UI.Export;
using Asistencia.Negocios.RRHH.Tareo;

namespace ComparativoHorasVisualSATNISIRA.RRHH.Tareo_Movil
{
    public partial class TareoMovil : Form
    {

        #region Variables() 
        private PrivilegesByUser PrivilegesUser;
        private string CompanyID;
        private string StringConection;
        private SAS_USUARIOS userSystem;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private TareoMovilController model;
        private List<SAS_TareoMovilListadoByFechasResult> listing; //Listado
        private List<SAS_TareoMovilListadoByFechasResult> selectedList; // ListaSelecionada
        private SAS_TareoMovilListadoByFechasResult selectedItem; // Item Selecionado

        #endregion




        public TareoMovil()
        {
            InitializeComponent();
            btnActualizar.Enabled = false;
            btnConsultar.Enabled = false;
            btnEditar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminar.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            StringConection = "SAS";
            userSystem = new SAS_USUARIOS();
            userSystem.IdUsuario = "EAURAZO";
            userSystem.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            CompanyID = "001";
            PrivilegesUser = new PrivilegesByUser();
            PrivilegesUser.nuevo = 1;
            PrivilegesUser.imprimir = 1;
            PrivilegesUser.editar = 1;
            PrivilegesUser.consultar = 1;
            PrivilegesUser.eliminar = 1;
            PrivilegesUser.anular = 1;
            PrivilegesUser.exportar = 1;

            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        public TareoMovil(string _StringConection, SAS_USUARIOS _userSystem, string _CompanyID, PrivilegesByUser _PrivilegesUser)
        {
            InitializeComponent();
            btnEditar.Enabled = false;
            btnAnular.Enabled = false;
            btnSEliminar.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            StringConection = _StringConection;
            userSystem = _userSystem;
            CompanyID = _CompanyID;
            PrivilegesUser = _PrivilegesUser;
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
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
                Globales.BaseDatos = ConfigurationManager.AppSettings[StringConection].ToString();
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




        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();

                base.OnLoad(e);
            }
            catch (TargetInvocationException ex)
            {
                result = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

        }

        private void LoadFreightSummary()
        {
            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chdocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (FilterExpressionException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }



        private void TareoMovil_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            RealizarConsulta();
        }

      



       



        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        #region Métodos()

        private void RealizarConsulta()
        {
            try
            {

                listing = new List<SAS_TareoMovilListadoByFechasResult>();
                model = new TareoMovilController();
                listing = model.GetTareoMovilListadoByFechas(StringConection, desde, hasta);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void PresentarResultados()
        {

            try
            {
                dgvListado.DataSource = listing.ToDataTable<SAS_TareoMovilListadoByFechasResult>();
                dgvListado.Refresh();

                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;               
                btnActualizar.Enabled = true;
               
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;                           
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnAnular.Enabled = false;
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                btnConsultar.Enabled = false;
                progressBar1.Visible = true;
                btnActualizar.Enabled = false;
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }


        #endregion

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
