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
using System.Globalization;
using ComparativoHorasVisualSATNISIRA.Produccion;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ReasignacionDeClientesEnPaletasLibres : Form
    {

        private ComboBoxHelper comboBoxHelper;
        private string desde;
        private string hasta;
        private string desdeFormato112;
        private string hastaFormato112;
        private string fileName;
        private bool exportVisualSettings;
        string codigoParaConsulta = string.Empty;
        private string idCultivo;
        private string idCliente;
        private PaletasPendientesParaPackingListControllers modelo;
        private string resultadoQuery1;
        private List<PaletasPendientesParaPackingList> listadoConsulta;
        public PaletasPendientesParaPackingList oRegistro;
        private string nombreClienteActual;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public ReasignacionDeClientesEnPaletasLibres()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            GetDateInicial();
        }


        public ReasignacionDeClientesEnPaletasLibres(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
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
            CargarMeses();
            GetDateInicial();
        }


        public void Inicio()
        {
            try
            {


                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["bdSaturno"].ToString();
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
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

        private void CargarMeses()
        {
            try
            {
                comboBoxHelper = new ComboBoxHelper();
                cboMes.DisplayMember = "Descripcion";
                cboMes.ValueMember = "Valor";
                cboMes.DataSource = comboBoxHelper.GetComboMonth().ToList();
                cboMes.SelectedValue = DateTime.Now.ToString("MM");


                comboBoxHelper = new ComboBoxHelper();
                cboCultivo.DisplayMember = "Descripcion";
                cboCultivo.ValueMember = "Valor";
                cboCultivo.DataSource = comboBoxHelper.GetComboCultivosActivos("SAS").ToList();
                cboCultivo.SelectedValue = DateTime.Now.ToString("0005");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;

            }

        }

        private void GetDateInicial()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }

        private void ObtenerFechasMes()
        {
            DateTime fecha1;
            DateTime fecha2;

            if (cboMes.SelectedValue.ToString() != "00")
            {
                #region
                this.txtFechaDesde.Enabled = false;
                this.txtFechaHasta.Enabled = false;
                if (cboMes.SelectedValue.ToString() == "12")
                {
                    #region Si es mes diciembre
                    fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString() + "/" + this.txtPeriodo.Text.ToString()));// 
                    fecha2 = Convert.ToDateTime("31/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Text.ToString());// 
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();
                    #endregion
                }
                else
                {
                    #region Si es mes 13 habilitar controles de fecha, caso contrario es un mes de enero a noviembre.
                    if (cboMes.SelectedValue.ToString() == "13")
                    {
                        this.txtFechaDesde.Enabled = true;
                        this.txtFechaHasta.Enabled = true;
                    }
                    else
                    {
                        fecha2 = Convert.ToDateTime("01/" + (Convert.ToInt32(cboMes.SelectedValue) + 1) + "/" + this.txtPeriodo.Text.ToString()).AddDays(-1);// 
                        fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Text.ToString());// 
                        this.txtFechaDesde.Text = fecha1.ToShortDateString();
                        this.txtFechaHasta.Text = fecha2.ToShortDateString();
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (cboMes.SelectedValue.ToString() == "00")
                {
                    fecha2 = Convert.ToDateTime("31/12/" + this.txtPeriodo.Text.ToString());// 
                    fecha1 = Convert.ToDateTime("01/01/" + this.txtPeriodo.Text.ToString());//
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {


            this.dgvlistado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvlistado.TableElement.EndUpdate();
            base.OnLoad(e);

        }


        private void LoadFreightSummary()
        {
            dgvlistado.MasterTemplate.AutoExpandGroups = true;
            dgvlistado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            dgvlistado.GroupDescriptors.Clear();
            this.dgvlistado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chDESC_PRODUCTO", "Count : {0:N0}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chCANTIDAD", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            dgvlistado.MasterTemplate.SummaryRowsTop.Add(items1);
            

        }

        public MesController MesesNeg { get; private set; }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");
            idCultivo = this.cboCultivo.SelectedValue.ToString().Trim();
            idCliente = this.txtIDPROVLOTE.Text.Trim();
            nombreClienteActual = this.txtRAZON_SOCIAL.Text.Trim();
         



            gbConsulta.Enabled = false;
            gbDetalle.Enabled = false;
            bgwHilo.RunWorkerAsync();
            //20211119
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new PaletasPendientesParaPackingListControllers();
                codigoParaConsulta = modelo.ObtenerCodigoUnico("SAS");

                resultadoQuery1 = modelo.GenerarListadoDePaletasPendientesParaPackingList("SAS", codigoParaConsulta, idCliente, idCultivo, hasta);

                listadoConsulta = new List<PaletasPendientesParaPackingList>();
                listadoConsulta = modelo.ObtenerListadoDePaletasPendientesParaPackingList("SAS", codigoParaConsulta);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvlistado.DataSource = listadoConsulta.ToDataTable<PaletasPendientesParaPackingList>();
                dgvlistado.Refresh();
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void commandBarButton3_Click(object sender, EventArgs e)
        {

        }

        private void commandBarButton2_Click(object sender, EventArgs e)
        {

        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {

        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        private void ReasignacionDeClientesEnPaletasLibres_Load(object sender, EventArgs e)
        {

        }

        private void bgwInicio_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwInicio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void reasigarClienteAPaletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrAEditarDocumento();
        }

        private void IrAEditarDocumento()
        {
            if (oRegistro != null)
            {
                if (oRegistro.IDREFERENCIA != null)
                {
                    if (oRegistro.IDREFERENCIA != string.Empty)
                    {
                        ReasignacionDeClientesEnPaletasLibresEdicion ofrm = new ReasignacionDeClientesEnPaletasLibresEdicion(oRegistro, idCliente, nombreClienteActual);
                        ofrm.ShowDialog();
                    }
                }
            }
        }

        private void dgvlistado_SelectionChanged(object sender, EventArgs e)
        {
            oRegistro = new PaletasPendientesParaPackingList();
            oRegistro.IDREFERENCIA = string.Empty;
            if (dgvlistado.Rows.Count > 0)
            {
                if (dgvlistado.CurrentRow != null && dgvlistado.CurrentRow.Cells["chIDREFERENCIA"].Value != null)
                {
                    try
                    {
                        string codigo = dgvlistado.CurrentRow.Cells["chIDREFERENCIA"].Value != null ? dgvlistado.CurrentRow.Cells["chIDREFERENCIA"].Value.ToString() : string.Empty;
                        var result = listadoConsulta.Where(x => x.IDREFERENCIA == codigo).ToList();

                        if (result != null && result.ToList().Count >= 1)
                        {
                            oRegistro = result.ElementAt(0);
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "", "MENSAJE DEL SISTEMA");
                        return;
                    }
                }

            }
        }
    }
}
