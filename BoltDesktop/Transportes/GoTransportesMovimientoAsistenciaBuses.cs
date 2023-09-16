using Asistencia.Datos;
using Asistencia.Helper;
using Asistencia.Negocios;
using MyControlsDataBinding.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Asistencia
{
    public partial class GoTransportesMovimientoAsistenciaBuses : Form
    {
        private string dateFrom;
        private ComboBoxHelper comboBoxHelper;
        public string dateUp;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult> asistancesByBuss;
        private RegistroTransferenciaTransportesController Modelo;
        private ExportToExcelHelper exportHelper;
        private GlobalesHelper globalHelper;
        private SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult registroResumen;

        public GoTransportesMovimientoAsistenciaBuses()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            GetDateInicial();
        }

        public GoTransportesMovimientoAsistenciaBuses(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            GetDateInicial();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvAsistenciaPorFundo.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvAsistenciaPorFundo.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvAsistenciaPorFundo.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaPorFundo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaPorFundo.GroupDescriptors.Clear();
            this.dgvAsistenciaPorFundo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chempresaTransporte", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaPorFundo.MasterTemplate.SummaryRowsTop.Add(items2);
        }



        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["bdSaturno"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void CargarMeses()
        {
            try
            {
                comboBoxHelper = new ComboBoxHelper();
                cboMonths.DisplayMember = "Descripcion";
                cboMonths.ValueMember = "Valor";
                cboMonths.DataSource = comboBoxHelper.GetComboMonth().ToList();
                cboMonths.SelectedValue = DateTime.Now.ToString("MM");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;

            }

        }

        private void GetDateInicial()
        {
            this.txtPeriod.Value = Convert.ToDecimal(DateTime.Now.Year);
            this.txtDateFrom.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtDateUp.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtPeriod.Value = Convert.ToDecimal(DateTime.Now.Year);
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                asistancesByBuss = new List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult>();
                Modelo =new RegistroTransferenciaTransportesController();
                asistancesByBuss = Modelo.ListarAsistenciaSalidaUnidadesTransportePersonalByPeriod(_conection, dateFrom, dateUp).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;

            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            try
            {
                if (asistancesByBuss != null)
                {
                    dgvAsistenciaPorFundo.DataSource = asistancesByBuss.ToDataTable<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult>();
                    dgvAsistenciaPorFundo.Refresh();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
                return;

            }




            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            try
            {
                exportHelper = new ExportToExcelHelper();
                exportHelper.ExportarToExcel(dgvAsistenciaPorFundo, saveFileDialog);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewConsult();
        }

        private void NewConsult()
        {
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void Consult()
        {
            try
            {
                /* Formato de busqueda '20190321', '20191221' */
                dateFrom = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd/MM/yyyy");
                dateUp = Convert.ToDateTime(this.txtDateUp.Text).ToString("dd/MM/yyyy");
                gbConsulta.Enabled = !true;
                gbDetalle.Enabled = !true;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
                return;
            }
        }

        private void GoTransportesMovimientoAsistenciaTransferenciaBuses_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            globalHelper = new GlobalesHelper();
            globalHelper.ObtenerFechasMes(cboMonths, txtDateFrom, txtDateUp, txtPeriod);
            
        }

        private void gbConsulta_Click(object sender, EventArgs e)
        {

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMonths.SelectedIndex > -1)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMonths, txtDateFrom, txtDateUp, txtPeriod);                
            }
        }

        private void modificarPlacaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registroResumen.fecha != string.Empty && registroResumen.placa != string.Empty)
            {
                GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta ofrm = new GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta(registroResumen, _conection, _companyId, _user);
                ofrm.ShowDialog();
            }
        }

        private void modificarRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgvAsistenciaPorFundo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                registroResumen = new SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult();
                registroResumen.fecha = DateTime.Now.ToShortTimeString();
                registroResumen.TipoMovilidad = string.Empty;
                registroResumen.categoriaMovilidad = string.Empty;
                registroResumen.empresaTransporte = string.Empty;
                registroResumen.placa = string.Empty;
                registroResumen.capacidad = 0;
                registroResumen.idCodigoGeneral = string.Empty;
                registroResumen.codigoPuerta = 0;
                registroResumen.IDRUTAORIGEN = 0;
                registroResumen.puerta = string.Empty;
                registroResumen.activo = 0;
                registroResumen.codigoTransportista = 0;
                registroResumen.precio = 0;
                registroResumen.ruta = string.Empty;
                if (dgvAsistenciaPorFundo != null)
                {
                    #region 
                    if (dgvAsistenciaPorFundo.CurrentRow != null)
                    {
                        if (dgvAsistenciaPorFundo.CurrentRow.Cells["chfecha"].Value != null)
                        {
                            if (dgvAsistenciaPorFundo.CurrentRow.Cells["chfecha"].Value.ToString() != string.Empty)
                            {
                                if (dgvAsistenciaPorFundo.CurrentRow.Cells["chplaca"].Value != null)
                                {
                                    if (dgvAsistenciaPorFundo.CurrentRow.Cells["chplaca"].Value.ToString() != string.Empty)
                                    {
                                        if (dgvAsistenciaPorFundo.CurrentRow.Cells["chfecha"].Value.ToString() != string.Empty)
                                        {
                                            if (dgvAsistenciaPorFundo.CurrentRow.Cells["chplaca"].Value.ToString() != string.Empty)
                                            {
                                                var resultado = asistancesByBuss.Where(x => x.fecha == (dgvAsistenciaPorFundo.CurrentRow.Cells["chfecha"].Value).ToString() && x.placa == dgvAsistenciaPorFundo.CurrentRow.Cells["chplaca"].Value.ToString()).ToList();

                                                if (resultado != null && resultado.ToList().Count > 0)
                                                {
                                                    registroResumen = resultado.FirstOrDefault();
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
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTransportesMovimientoAsistenciaBuses_Load(object sender, EventArgs e)
        {

        }
    }
}
