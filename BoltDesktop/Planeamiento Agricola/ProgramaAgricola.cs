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
using Asistencia.Negocios.PlaneamientoAgricola;

namespace ComparativoHorasVisualSATNISIRA.Planeamiento_Agricola
{

    public partial class ProgramaAgricola : Form
    {
        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ProgramaSemanalListadoByPeriodoResult selectedItem;
        private List<SAS_ProgramaSemanalListadoByPeriodoResult> resultList;
        private ProgramaSemanaController model;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private string PeriodoDesde = string.Empty;
        private string PeriodoHasta = string.Empty;
        private MesController MesesNeg;
        private GlobalesHelper globalHelper;
        private int resumido;
        public string ProgramaSemanalEstadoID;
        public string RequerimientoInternoEstadoID;
        public string ProgramaSemanalID;
        public string RequerimientoInternoID;
        #endregion

        public ProgramaAgricola()
        {
            InitializeComponent();
            connection = "SAS";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            Inicio();
            Consultar();
        }


        private void Consultar()
        {
            PeriodoDesde = this.txtFechaDesde.Text;
            PeriodoHasta = this.txtFechaHasta.Text;

            if (PeriodoDesde != string.Empty && PeriodoHasta != string.Empty)
            {
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                BarraPrincipal.Enabled = false;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();
            }


        }

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
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
            items1.Add(new GridViewSummaryItem("chDocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        public ProgramaAgricola(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();


            connection = _connection;
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();

            Inicio();
            Consultar();
        }


        private void ProgramaAgricola_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaAsincrona();
        }

        private void EjecutarConsultaAsincrona()
        {
            try
            {
                resultList = new List<SAS_ProgramaSemanalListadoByPeriodoResult>();
                model = new ProgramaSemanaController();
                resultList = model.ListadoProgramasPorPeriodos(connection, PeriodoDesde, PeriodoHasta);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        private void PresentarResultados()
        {
            try
            {
                dgvListado.DataSource = resultList;
                dgvListado.Refresh();

                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                BarraPrincipal.Enabled = true;
                progressBar1.Visible = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }


        private void CargarMeses()
        {

            MesesNeg = new MesController();
            cboMes.DisplayMember = "descripcion";
            cboMes.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboMes.DataSource = MesesNeg.ListarMeses().ToList();
            cboMes.SelectedValue = DateTime.Now.ToString("MM");
        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);

            //if (chkVisualizacionPorDia.Checked == true)
            //{
            //    this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
            //else
            //{
            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}


            //this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                modelExportToExcel = new ExportToExcelHelper();
                modelExportToExcel.ExportarToExcel(dgvListado, saveFileDialog);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {

            btnEditarProgramaConcentracionPorDosis.Enabled = false;
            btnEditarProgramaHectareaPorDosis.Enabled = false;
            btnEditarPrograma.Enabled = false;
            btnEliminarPrograma.Enabled = false;



            try
            {
                #region
                selectedItem = new SAS_ProgramaSemanalListadoByPeriodoResult();
                selectedItem.EstadoID = string.Empty;
                selectedItem.ProgramaSemanaID = string.Empty;
                selectedItem.RequerimientoInternoId = string.Empty;
                selectedItem.RequerimientoInternoEstadoID = string.Empty;
                ProgramaSemanalID = string.Empty;
                RequerimientoInternoID = string.Empty;
                ProgramaSemanalEstadoID = string.Empty;
                RequerimientoInternoEstadoID = string.Empty;

                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chProgramaSemanaID"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chProgramaSemanaID"].Value.ToString() != string.Empty)
                            {
                                ProgramaSemanalID = dgvListado.CurrentRow.Cells["chProgramaSemanaID"].Value.ToString() != null ? dgvListado.CurrentRow.Cells["chProgramaSemanaID"].Value.ToString() : string.Empty;
                                RequerimientoInternoID = dgvListado.CurrentRow.Cells["chRequerimientoInternoId"].Value.ToString() != null ? dgvListado.CurrentRow.Cells["chRequerimientoInternoId"].Value.ToString() : string.Empty;
                                ProgramaSemanalEstadoID = dgvListado.CurrentRow.Cells["chEstadoID"].Value.ToString() != null ? dgvListado.CurrentRow.Cells["chEstadoID"].Value.ToString() : string.Empty;
                                RequerimientoInternoEstadoID = dgvListado.CurrentRow.Cells["chRequerimientoInternoEstadoID"].Value.ToString() != null ? dgvListado.CurrentRow.Cells["chRequerimientoInternoEstadoID"].Value.ToString() : string.Empty;
                                var result = resultList.Where(x => x.ProgramaSemanaID.Trim() == ProgramaSemanalID.Trim()).ToList();

                                if (result != null)
                                {

                                    if (result.ToList().Count > 0)
                                    {
                                        selectedItem = result.ElementAt(0);
                                        selectedItem.ProgramaSemanaID = ProgramaSemanalID;
                                        if (ProgramaSemanalEstadoID.Trim() == "PE")
                                        {
                                            if (RequerimientoInternoEstadoID.Trim() == string.Empty || RequerimientoInternoEstadoID.Trim() == "PE")
                                            {
                                                if (userLogin.IdUsuario != null)
                                                {
                                                    if (userLogin.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || userLogin.IdUsuario.Trim().ToUpper() == "EAURAZO" || userLogin.IdUsuario.Trim().ToUpper() == "SHUACCHILLO" || userLogin.IdUsuario.Trim().ToUpper() == "FCERNA")
                                                    {
                                                        btnEditarProgramaConcentracionPorDosis.Enabled = true;
                                                        btnEditarProgramaHectareaPorDosis.Enabled = true;
                                                        btnEditarPrograma.Enabled = true;
                                                    }
                                                }

                                                
                                            }
                                        }
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

        private void btnEditarProgramaHectareaPorDosis_Click(object sender, EventArgs e)
        {
            if (ProgramaSemanalID != null && ProgramaSemanalID != string.Empty)
            {
                ProgramaAgricolaEdicion oFron = new ProgramaAgricolaEdicion(connection, userLogin, companyId, privilege, ProgramaSemanalID, 1);
               // OFRM.Show();
                oFron.MdiParent = ProgramaAgricola.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();

            }
        }

        private void btnEditarProgramaConcentracionPorDosis_Click(object sender, EventArgs e)
        {
            if (ProgramaSemanalID != null && ProgramaSemanalID != string.Empty)
            {
                ProgramaAgricolaEdicion oFron = new ProgramaAgricolaEdicion(connection, userLogin, companyId, privilege, selectedItem.ProgramaSemanaID.Trim(), 1);
                // OFRM.Show();
                oFron.MdiParent = ProgramaAgricola.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();
            }
        }

        private void btnVerPrograma_Click(object sender, EventArgs e)
        {
            if (ProgramaSemanalID != null && ProgramaSemanalID != string.Empty)
            {
                ProgramaAgricolaEdicion oFron = new ProgramaAgricolaEdicion(connection, userLogin, companyId, privilege, selectedItem.ProgramaSemanaID.Trim(), 0);
                // OFRM.Show();
                oFron.MdiParent = ProgramaAgricola.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();
            }

        }
    }
}
