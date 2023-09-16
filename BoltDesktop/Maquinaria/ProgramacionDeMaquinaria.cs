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

namespace ComparativoHorasVisualSATNISIRA.Maquinaria
{
    public partial class ProgramacionDeMaquinaria : Form
    {
        private List<SAS_ProgramacionMaquinariaListAllByTurn> listado;
        private SAS_ProgramacionDetalleMaquinariaController model;
        private int periodo;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;
        private SAS_ProgramacionMaquinariaListAllByTurn selectedItem;

        public ProgramacionDeMaquinaria()
        {
            InitializeComponent();
            _conection = "SAS";
            _user = new SAS_USUARIOS();
            _companyId = "001";
            _privilege = new PrivilegesByUser();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consult();
        }

        public ProgramacionDeMaquinaria(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consult();
        }


        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
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
            items1.Add(new GridViewSummaryItem("chdocumento", "COUNT : {0:N0}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chdiurno", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chtarde", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chnoche", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chtotalTurno", "SUM : {0:N2}; ", GridAggregateFunction.Sum));                        
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void Consult()
        {
            try
            {
                periodo = Convert.ToInt32(this.txtPeriodo.Value);
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                BarraDeConsulta.Visible = true;
                BarraPrincipal.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.idProgramacionMaquinaria != 0)
                {
                    ProgramacionDeMaquinariaEdicion ofrm = new ProgramacionDeMaquinariaEdicion(_conection != null ? _conection : "SAS",_user,_companyId,_privilege, selectedItem);
                    ofrm.Show();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void NewRegister()
        {
            selectedItem = new SAS_ProgramacionMaquinariaListAllByTurn();
            selectedItem.idProgramacionMaquinaria = 0;
            ProgramacionDeMaquinariaEdicion ofrm = new ProgramacionDeMaquinariaEdicion(_conection,_user,_companyId,_privilege, selectedItem);
            ofrm.Show();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void ProgramacionDeMaquinaria_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            listado = new List<SAS_ProgramacionMaquinariaListAllByTurn>();
            model = new SAS_ProgramacionDetalleMaquinariaController();

            listado = model.GetListAll("SAS", periodo);


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvListado.DataSource = listado.ToDataTable<SAS_ProgramacionMaquinariaListAllByTurn>();
            dgvListado.Refresh();


            gbCabecera.Enabled = !false;
            gbListado.Enabled = !false;
            BarraDeConsulta.Visible = !true;
            BarraPrincipal.Enabled = !false;

        }

        private void ProgramacionDeMaquinaria_Load(object sender, EventArgs e)
        {

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            selectedItem = new SAS_ProgramacionMaquinariaListAllByTurn();
            selectedItem.idProgramacionMaquinaria = 0;
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;

            try
            {
                #region 
                
                
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidProgramacionMaquinaria"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chidProgramacionMaquinaria"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvListado.CurrentRow.Cells["chidProgramacionMaquinaria"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chidProgramacionMaquinaria"].Value.ToString()) : 0);

                                if (id != 0)
                                {
                                    var resultado = listado.Where(x => x.idProgramacionMaquinaria == id).ToList();
                                    if (resultado.ToList().Count == 1)
                                    {
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnularRegistro.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;

                                    }
                                    else if (resultado.ToList().Count > 1)
                                    {
                                        selectedItem = resultado.ElementAt(0);
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnularRegistro.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;

                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }
    }
}
