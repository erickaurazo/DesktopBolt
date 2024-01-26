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

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class GestionDeSolicitudPedidoParaCompras : Form
    {
        #region Variable()         
        private string desde;
        private string hasta;
        private string fileName;
        private bool exportVisualSettings;
        private MesController MesesNeg;
        private GlobalesHelper globalHelper;
        private string idpedidoServicio, idpedido, itemPedido, tipoCambio, IdEstadoNuevo = string.Empty;
        private PedidoControllers modelo;
        private List<SAS_SeguimientoPedidosParaCompraResult> ListadoPedidos;
        private string resultado;
        private List<SAS_SeguimientoOrdenServicioResult> ListadoOrdenesServicio;
        private string idOrdenServicio;
        private List<SAS_SeguimientoOrdenCompraResult> ListadoOrdenesCompra;
        private List<SAS_SeguimientoPedidosCompraFullResult> ListadoPedidosSeguimiento;
        private string idOrdenCompra;

        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId = string.Empty;
        private string _conection = string.Empty;
        private string _formulario = string.Empty;

        #endregion

        public List<SAS_SeguimientoPedidosServicio2Result> ListadoPedidosServicios { get; private set; }

        public GestionDeSolicitudPedidoParaCompras()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
            _formulario = string.Empty;
        }


        public GestionDeSolicitudPedidoParaCompras(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            _formulario = string.Empty;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
        }

        public GestionDeSolicitudPedidoParaCompras(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege, string formulario)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            _formulario = formulario;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;
           // Consultar();
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
            this.dgvPedidoCompras.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvPedidoCompras.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvPedidoCompras.MasterTemplate.AutoExpandGroups = true;
            this.dgvPedidoCompras.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidoCompras.GroupDescriptors.Clear();
            this.dgvPedidoCompras.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chproducto", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvPedidoCompras.MasterTemplate.SummaryRowsTop.Add(items1);

            this.dgvPedidoServicio.MasterTemplate.AutoExpandGroups = true;
            this.dgvPedidoServicio.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidoServicio.GroupDescriptors.Clear();
            this.dgvPedidoServicio.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chRAZON_SOCIAL", "Count : {0:N2}; ", GridAggregateFunction.Count));
            items2.Add(new GridViewSummaryItem("chIMPORTE_MOF", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items2.Add(new GridViewSummaryItem("chIMPORTE_MEX", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvPedidoServicio.MasterTemplate.SummaryRowsTop.Add(items2);

            this.dgvOrdenesServicios.MasterTemplate.AutoExpandGroups = true;
            this.dgvOrdenesServicios.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenesServicios.GroupDescriptors.Clear();
            this.dgvOrdenesServicios.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items3 = new GridViewSummaryRowItem();
            items3.Add(new GridViewSummaryItem("chProveedor", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvOrdenesServicios.MasterTemplate.SummaryRowsTop.Add(items3);

            this.dgvPedidoComprasFull.MasterTemplate.AutoExpandGroups = true;
            this.dgvPedidoComprasFull.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidoComprasFull.GroupDescriptors.Clear();
            this.dgvPedidoComprasFull.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items4 = new GridViewSummaryRowItem();
            items4.Add(new GridViewSummaryItem("chPRODUCTO", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvPedidoComprasFull.MasterTemplate.SummaryRowsTop.Add(items4);

            this.dgvOrdenesCompra.MasterTemplate.AutoExpandGroups = true;
            this.dgvOrdenesCompra.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenesCompra.GroupDescriptors.Clear();
            this.dgvOrdenesCompra.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items5 = new GridViewSummaryRowItem();
            items5.Add(new GridViewSummaryItem("chProveedor", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvOrdenesCompra.MasterTemplate.SummaryRowsTop.Add(items5);
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

            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
        }

        private void Exportar(RadGridView radGridView)
        {
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                RadMessageBox.Show("Ingrese nombre al archivo.");
                return;
            }

            fileName = this.saveFileDialog.FileName.Trim();
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(@fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(@fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla1)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla1);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                desde = this.txtFechaDesde.Text.ToString();
                hasta = this.txtFechaHasta.Text.ToString();
                idpedidoServicio = string.Empty;
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void GestionDeSolicitudPedidoParaCompras_Load(object sender, EventArgs e)
        {

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new PedidoControllers();
                ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                ListadoPedidosSeguimiento = new List<SAS_SeguimientoPedidosCompraFullResult>();
                ListadoPedidosServicios = new List<SAS_SeguimientoPedidosServicio2Result>();
                ListadoOrdenesServicio = new List<SAS_SeguimientoOrdenServicioResult>();
                ListadoOrdenesCompra = new List<SAS_SeguimientoOrdenCompraResult>();

                if (_formulario == string.Empty)
                {
                    ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido, string.Empty).ToList();
                    ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoPedidosSeguimiento = modelo.ObternerListadoSeguimientoPorPeriodoCompleto("SAS", desde, hasta, idpedidoServicio).ToList();
                }
                else if (_formulario.ToUpper() == "PEDIDO COMPRAS".ToUpper())
                {
                    ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido, string.Empty).ToList();
                    //ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoPedidosSeguimiento = modelo.ObternerListadoSeguimientoPorPeriodoCompleto("SAS", desde, hasta, idpedidoServicio).ToList();
                }
                else if (_formulario.ToUpper() == "PEDIDO SERVICIOS".ToUpper())
                {
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();

                    ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido, string.Empty).ToList();
                    //ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoPedidosSeguimiento = modelo.ObternerListadoSeguimientoPorPeriodoCompleto("SAS", desde, hasta, idpedidoServicio).ToList();
                }
                else if (_formulario.ToUpper() == "ORDEN COMPRAS".ToUpper())
                {
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido).ToList();
                    //ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoPedidosSeguimiento = modelo.ObternerListadoSeguimientoPorPeriodoCompleto("SAS", desde, hasta, idpedidoServicio).ToList();
                }
                else if (_formulario.ToUpper() == "ORDEN SERVICIOS".ToUpper())
                {
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    // ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido).ToList();
                    ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                    //ListadoPedidosSeguimiento = modelo.ObternerListadoSeguimientoPorPeriodoCompleto("SAS", desde, hasta, idpedidoServicio).ToList();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                if (_formulario == string.Empty)
                {
                    tabControl.Enabled = true;
                    tabPedidoSeguimientoCompleto.Enabled = true;
                    tabPedidoCompras.Enabled = true;
                    tabPedidoServicio.Enabled = true;
                    TabOrdenCompras.Enabled = true;
                    tabOrdenServicio.Enabled = true;
                }
                else if (_formulario.ToUpper() == "PEDIDO COMPRAS".ToUpper())
                {
                    tabControl.Enabled = true;
                    tabPedidoCompras.Enabled = true;
                    tabPedidoSeguimientoCompleto.Enabled = true;
                    tabPedidoServicio.Enabled = false;
                    TabOrdenCompras.Enabled = false;
                    tabOrdenServicio.Enabled = false;
                }
                else if (_formulario.ToUpper() == "PEDIDO SERVICIOS".ToUpper())
                {
                    tabControl.Enabled = true;
                    tabPedidoSeguimientoCompleto.Enabled = false;
                    tabPedidoCompras.Enabled = false;
                    tabPedidoServicio.Enabled = true;
                    TabOrdenCompras.Enabled = false;
                    tabOrdenServicio.Enabled = false;

                }
                else if (_formulario.ToUpper() == "ORDEN COMPRAS".ToUpper())
                {

                    tabControl.Enabled = true;
                    tabPedidoSeguimientoCompleto.Enabled = false;
                    tabPedidoServicio.Enabled = false;
                    tabPedidoCompras.Enabled = false;
                    TabOrdenCompras.Enabled = true;
                    tabOrdenServicio.Enabled = false;
                }
                else if (_formulario.ToUpper() == "ORDEN SERVICIOS".ToUpper())
                {
                    tabControl.Enabled = true;
                    tabPedidoSeguimientoCompleto.Enabled = false;
                    tabPedidoServicio.Enabled = false;
                    tabPedidoCompras.Enabled = false;
                    TabOrdenCompras.Enabled = false;
                    tabOrdenServicio.Enabled = true;

                }
                else
                {
                    tabControl.Enabled = false;
                    tabPedidoSeguimientoCompleto.Enabled = false;
                    tabPedidoServicio.Enabled = false;
                    TabOrdenCompras.Enabled = false;
                    tabPedidoCompras.Enabled = false;
                    tabOrdenServicio.Enabled = false;
                }

                dgvPedidoCompras.DataSource = ListadoPedidos.ToDataTable<SAS_SeguimientoPedidosParaCompraResult>();
                dgvPedidoCompras.Refresh();
                dgvPedidoServicio.DataSource = ListadoPedidosServicios.ToDataTable<SAS_SeguimientoPedidosServicio2Result>();
                dgvPedidoServicio.Refresh();
                dgvOrdenesServicios.DataSource = ListadoOrdenesServicio.ToDataTable<SAS_SeguimientoOrdenServicioResult>();
                dgvOrdenesServicios.Refresh();
                dgvOrdenesCompra.DataSource = ListadoOrdenesCompra.ToDataTable<SAS_SeguimientoOrdenCompraResult>();
                dgvOrdenesCompra.Refresh();
                dgvPedidoComprasFull.DataSource = ListadoPedidosSeguimiento.ToDataTable<SAS_SeguimientoPedidosCompraFullResult>();
                dgvPedidoComprasFull.Refresh();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        #region Cambio de estado del documento()
        // metodo para cambiar estado | D = detalle | C = Cabecera
        private void CambiarEstado(string oTipoCambio, string oIdEstadoNuevo)
        {
            if (idpedido != string.Empty && itemPedido != string.Empty)
            {
                // Ejecutar Acción
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                tipoCambio = oTipoCambio;
                IdEstadoNuevo = oIdEstadoNuevo;
                bgwActualizadorPedidoCompraEstado.RunWorkerAsync();
            }
        }

        private void CambiarEstadoPedidoServicio(string oTipoCambio, string oIdEstadoNuevo)
        {
            if (idpedidoServicio != string.Empty && idpedidoServicio != string.Empty)
            {
                // Ejecutar Acción
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                tipoCambio = oTipoCambio;
                IdEstadoNuevo = oIdEstadoNuevo;
                bwgActualizarPedidoServicioEstado.RunWorkerAsync();
            }
        }

        private void CambiarEstadoOrdenServicio(string oTipoCambio, string oIdEstadoNuevo)
        {
            if (idOrdenServicio != string.Empty && idOrdenServicio != string.Empty)
            {
                // Ejecutar Acción
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                tipoCambio = oTipoCambio;
                IdEstadoNuevo = oIdEstadoNuevo;
                bwgActualizarOrdenServicioEstado.RunWorkerAsync();
            }
        }

        private void CambiarEstadoOrdenCompra(string oTipoCambio, string oIdEstadoNuevo)
        {
            if (idOrdenCompra != string.Empty && idOrdenCompra != string.Empty)
            {
                // Ejecutar Acción
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                menuPrincipal.Enabled = false;
                ProgressBar.Visible = true;
                tipoCambio = oTipoCambio;
                IdEstadoNuevo = oIdEstadoNuevo;
                bwgActualizarOrdenCompraEstado.RunWorkerAsync();
            }
        }

        private void cambiarAPendienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "PE");
        }



        private void cambiarAPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "PT");
        }

        private void cambiaAAprobadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "AP");
        }

        private void cambiarAConOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "CO");
        }

        private void cambiarAAtentidoParcialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "TP");
        }

        private void cambiarAAtentidoTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "AT");
        }

        private void cambiarAArchivadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("C", "CE");
        }
        #endregion

        #region Cambio de estado de item()
        private void cambiarItemDocumentoAPendienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "PE");
        }

        private void cambiarItemAPresupuestadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "PT");
        }

        private void cambiarItemAAprobadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "AP");
        }

        private void cambiarItemAConOrdenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "CO");
        }

        private void cambiarItemAAtendidoParcialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "TP");
        }

        private void cambiarItemAAtendidoTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "AT");
        }

        private void cambiarItemAArchivadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("D", "CE");
        }
        #endregion

        private void GestionDeSolicitudPedidoParaCompras_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.bgwActualizadorPedidoCompraEstado.IsBusy == true)
            {

                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwActualizadorEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPedidoCompras.DataSource = ListadoPedidos.ToDataTable<SAS_SeguimientoPedidosParaCompraResult>();
                dgvPedidoCompras.Refresh();
                MessageBox.Show(resultado, "CONFIRMACION DEL SISTEMA");

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar(dgvPedidoCompras);
        }

        private void dgvPedidoServicio_SelectionChanged(object sender, EventArgs e)
        {
            idpedidoServicio = string.Empty;
            //itemPedido = string.Empty;

            if (dgvPedidoServicio.Rows.Count > 0)
            {
                if (dgvPedidoServicio.CurrentRow != null)
                {
                    if (dgvPedidoServicio.CurrentRow.Cells["chIDPEDIDO"].Value != null && dgvPedidoCompras.CurrentRow.Cells["chItemPedido"].Value != null)
                    {
                        idpedidoServicio = dgvPedidoServicio.CurrentRow.Cells["chIDPEDIDO"].Value != null ? Convert.ToString(dgvPedidoServicio.CurrentRow.Cells["chIDPEDIDO"].Value.ToString().Trim()) : string.Empty;
                        //itemPedido = dgvPedidoServicio.CurrentRow.Cells["chIDPEDIDO"].Value != null ? Convert.ToString(dgvPedidoServicio.CurrentRow.Cells["chItemPedido"].Value.ToString().Trim()) : string.Empty;
                    }

                }
            }
        }

        private void bwgActualizarPedidoServicio_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                modelo = new PedidoControllers();

                if (tipoCambio == "C")
                {
                    modelo = new PedidoControllers();
                    resultado = modelo.ActualizarEstadoPedidoServicioDocumento("SAS", idpedidoServicio, IdEstadoNuevo);
                    ListadoPedidosServicios = new List<SAS_SeguimientoPedidosServicio2Result>();
                    ListadoPedidosServicios = modelo.ObternerSeguimientoPedidosServicio("SAS", Convert.ToDateTime(desde).ToString("yyyyMMdd"), Convert.ToDateTime(hasta).ToString("yyyyMMdd"), idpedido , string.Empty).ToList();
                }
                else if (tipoCambio == "D")
                {
                    //modelo = new PedidoControllers();
                    //resultado = modelo.ActualizarEstadoDetalleProductoEnDocumento("SAS", idpedidoServicio, itemPedido, IdEstadoNuevo);
                    //ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bwgActualizarPedidoServicio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvPedidoServicio.DataSource = ListadoPedidosServicios.ToDataTable<SAS_SeguimientoPedidosServicio2Result>();
                dgvPedidoServicio.Refresh();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;

        }

        private void btnPSRPresupuestado_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "PT");
        }

        private void btnPSRAprobado_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "AP");
        }

        private void btnPSROrdenServicio_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "OS");
        }

        private void btnPSRAtendidoParcial_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "TP");
        }

        private void btnPSRAtendidoTotal_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "AT");
        }

        private void btnPSRArchivado_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "CE");
        }

        private void dgvOrdenesServicios_SelectionChanged(object sender, EventArgs e)
        {
            idOrdenServicio = string.Empty;

            if (dgvOrdenesServicios.Rows.Count > 0)
            {
                if (dgvOrdenesServicios.CurrentRow != null)
                {
                    if (dgvOrdenesServicios.CurrentRow.Cells["chidservicio"].Value != null && dgvOrdenesServicios.CurrentRow.Cells["chidservicio"].Value != null)
                    {
                        idOrdenServicio = dgvOrdenesServicios.CurrentRow.Cells["chidservicio"].Value != null ? Convert.ToString(dgvOrdenesServicios.CurrentRow.Cells["chidservicio"].Value.ToString().Trim()) : string.Empty;
                    }
                }
            }
        }

        private void btnOSRPendiente_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "PE");
        }

        private void btnOSRPresupuestado_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "PT");
        }

        private void btnOSRAprobado_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "AP");
        }

        private void btnOSRAtendidoParcial_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "TP");

        }

        private void btnOSRAtendidoTotal_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "AT");
        }

        private void btnOSRArchivado_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenServicio("C", "CE");
        }

        private void bwgActualizarOrdenServicioEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new PedidoControllers();
                if (tipoCambio == "C")
                {
                    modelo = new PedidoControllers();
                    resultado = modelo.ActualizarEstadoOrdenServicioDocumento("SAS", idOrdenServicio, IdEstadoNuevo);
                    ListadoOrdenesServicio = new List<SAS_SeguimientoOrdenServicioResult>();
                    ListadoOrdenesServicio = modelo.ObternerListadoOrdenesServicioPorPeriodo("SAS", desde, hasta, idOrdenServicio).ToList();
                }
                else if (tipoCambio == "D")
                {
                    //modelo = new PedidoControllers();
                    //resultado = modelo.ActualizarEstadoDetalleProductoEnDocumento("SAS", idpedidoServicio, itemPedido, IdEstadoNuevo);
                    //ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bwgActualizarOrdenServicioEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvOrdenesServicios.DataSource = ListadoOrdenesServicio.ToDataTable<SAS_SeguimientoOrdenServicioResult>();
                dgvOrdenesServicios.Refresh();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        private void dgvPedidoCompras_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenCompra("C", "PE");
        }

        private void btnOCOPresupuesto_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenCompra("C", "PT");
        }

        private void btnOCOAprobado_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenCompra("C", "AP");
        }

        private void btnOCOAtencionParcial_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenCompra("C", "TP");
        }

        private void btnOCOAtencionTotal_Click(object sender, EventArgs e)
        {
            CambiarEstadoOrdenCompra("C", "AT");
        }

        private void btnOCOArchivado_Click(object sender, EventArgs e)
        {
            //
        }

        private void bwgActualizarOrdenCompraEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new PedidoControllers();
                if (tipoCambio == "C")
                {
                    modelo = new PedidoControllers();
                    resultado = modelo.ActualizarEstadoOrdenCompraDocumento("SAS", idOrdenCompra, IdEstadoNuevo);
                    ListadoOrdenesCompra = new List<SAS_SeguimientoOrdenCompraResult>();
                    ListadoOrdenesCompra = modelo.ObternerListadoOrdenesCompraPorPeriodo("SAS", desde, hasta, idOrdenCompra).ToList();
                }
                else if (tipoCambio == "D")
                {
                    //modelo = new PedidoControllers();
                    //resultado = modelo.ActualizarEstadoDetalleProductoEnDocumento("SAS", idpedidoServicio, itemPedido, IdEstadoNuevo);
                    //ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                    //ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedidoServicio).ToList();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void bwgActualizarOrdenCompraEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                dgvOrdenesCompra.DataSource = ListadoOrdenesCompra.ToDataTable<SAS_SeguimientoOrdenCompraResult>();
                dgvOrdenesCompra.Refresh();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        private void dgvOrdenesCompra_SelectionChanged(object sender, EventArgs e)
        {
            idOrdenCompra = string.Empty;

            if (dgvOrdenesCompra.Rows.Count > 0)
            {
                if (dgvOrdenesCompra.CurrentRow != null)
                {
                    if (dgvOrdenesCompra.CurrentRow.Cells["chidcompra"].Value != null && dgvOrdenesCompra.CurrentRow.Cells["chidcompra"].Value != null)
                    {
                        idOrdenCompra = dgvOrdenesCompra.CurrentRow.Cells["chidcompra"].Value != null ? Convert.ToString(dgvOrdenesCompra.CurrentRow.Cells["chidcompra"].Value.ToString().Trim()) : string.Empty;
                    }
                }
            }
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            CambiarEstadoPedidoServicio("C", "PE");
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
                if (this.bgwActualizadorPedidoCompraEstado.IsBusy == true)
                {


                    MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                    "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Close();
            }
        }

        private void bgwActualizadorEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                modelo = new PedidoControllers();

                if (tipoCambio == "C")
                {
                    modelo = new PedidoControllers();
                    resultado = modelo.ActualizarEstadoPedidoCompraDocumento("SAS", idpedido, IdEstadoNuevo);
                    ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                    ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedido).ToList();
                }
                else if (tipoCambio == "D")
                {
                    modelo = new PedidoControllers();
                    resultado = modelo.ActualizarEstadoPedidoCompraDetalleProductoEnDocumento("SAS", idpedido, itemPedido, IdEstadoNuevo);
                    ListadoPedidos = new List<SAS_SeguimientoPedidosParaCompraResult>();
                    ListadoPedidos = modelo.ObternerListadoSeguimientoPorPeriodo("SAS", desde, hasta, idpedido).ToList();
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void dgvSeguimientoPedidoResumen_SelectionChanged(object sender, EventArgs e)
        {
            idpedido = string.Empty;
            itemPedido = string.Empty;

            if (dgvPedidoCompras.Rows.Count > 0)
            {
                if (dgvPedidoCompras.CurrentRow != null)
                {
                    if (dgvPedidoCompras.CurrentRow.Cells["chidpedido"].Value != null && dgvPedidoCompras.CurrentRow.Cells["chItemPedido"].Value != null)
                    {
                        idpedido = dgvPedidoCompras.CurrentRow.Cells["chidpedido"].Value != null ? Convert.ToString(dgvPedidoCompras.CurrentRow.Cells["chidpedido"].Value.ToString().Trim()) : string.Empty;
                        itemPedido = dgvPedidoCompras.CurrentRow.Cells["chItemPedido"].Value != null ? Convert.ToString(dgvPedidoCompras.CurrentRow.Cells["chItemPedido"].Value.ToString().Trim()) : string.Empty;
                    }

                }
            }

        }
    }
}
