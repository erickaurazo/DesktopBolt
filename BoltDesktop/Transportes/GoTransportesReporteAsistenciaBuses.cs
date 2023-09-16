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

namespace Asistencia
{
    public partial class GoTransportesReporteAsistenciaBuses : Form
    {
        private RegistroTransferenciaTransportesController negocio;
        private List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listado;
        private List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult> listadoByCostos;
        private string desde;
        private string fileName;
        private bool exportVisualSettings;
        private string desdeC;
        private SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult registroResumen;
        private List<AsistenciaBusByDia> listadoResumenAsistenciaByBusByDia;
        private List<ASJ_ReporteAsistenciaObservadosResult> listadoAsistenciaObservados;
        private int incluirAsistenciaObervada;
        private int incluirRecorridosInternos;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private GlobalesHelper globalHelper;

        public string Hasta { get; private set; }
        public MesController MesesNeg { get; private set; }
        public string HastaC { get; private set; }

        public GoTransportesReporteAsistenciaBuses()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();

            // ObtenerValoresConsultaBuscar();

        }

        public GoTransportesReporteAsistenciaBuses(string conection, ASJ_USUARIOS user, string companyId)
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
            ObtenerFechasIniciales();
        }

        protected override void OnLoad(EventArgs e)
        {

            this.dgvAsistenciaTransferida.TableElement.BeginUpdate();
            this.dgvAsistenciaPorFundo.TableElement.BeginUpdate();
            this.dgvResumenByDia.TableElement.BeginUpdate();
            this.dgvAsistenciaObservados.TableElement.BeginUpdate();

            this.LoadFreightSummary();
            this.dgvAsistenciaTransferida.TableElement.EndUpdate();
            this.dgvAsistenciaPorFundo.TableElement.EndUpdate();
            this.dgvResumenByDia.TableElement.EndUpdate();
            this.dgvAsistenciaObservados.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvAsistenciaTransferida.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaTransferida.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaTransferida.GroupDescriptors.Clear();
            this.dgvAsistenciaTransferida.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chNombres", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaTransferida.MasterTemplate.SummaryRowsTop.Add(items1);

            this.dgvAsistenciaPorFundo.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaPorFundo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaPorFundo.GroupDescriptors.Clear();
            this.dgvAsistenciaPorFundo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chempresaTransporte", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaPorFundo.MasterTemplate.SummaryRowsTop.Add(items2);

            this.dgvResumenByDia.MasterTemplate.AutoExpandGroups = true;
            this.dgvResumenByDia.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvResumenByDia.GroupDescriptors.Clear();
            this.dgvResumenByDia.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items3 = new GridViewSummaryRowItem();
            items3.Add(new GridViewSummaryItem("chempresaTransporteR", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            items3.Add(new GridViewSummaryItem("chBota", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chTablazo", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chBalsa", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chSantaMaria", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chIMP", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chtotalAsistencia", "Registros : {0:N0}; ", GridAggregateFunction.Sum));
            this.dgvResumenByDia.MasterTemplate.SummaryRowsTop.Add(items3);

            this.dgvAsistenciaObservados.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaObservados.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaObservados.GroupDescriptors.Clear();
            this.dgvAsistenciaObservados.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items4 = new GridViewSummaryRowItem();
            items4.Add(new GridViewSummaryItem("chnombresObs", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaObservados.MasterTemplate.SummaryRowsTop.Add(items4);

        }


        private void ObtenerValoresConsultaBuscar()
        {
            try
            {
                /* Formato de busqueda '20190321', '20191221' */
                desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd");
                Hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");

                desdeC = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                HastaC = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");

                incluirAsistenciaObervada = (chkIncluirAsistenciasObservadas.Checked == true ? 1 : 0);
                incluirRecorridosInternos = (chkIncluirRecorridoInternos.Checked == true ? 1 : 0);

                bgwHilo.RunWorkerAsync();
                gbConsulta.Enabled = !true;
                gbDetalle.Enabled = !true;
                ProgressBar.Visible = true;
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            ObtenerValoresConsultaBuscar();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            negocio = new RegistroTransferenciaTransportesController();
            listado = new List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult>();
            listadoByCostos = new List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult>();
            listado = negocio.ObtenerListadoRegistroMarcacionPersonalEnBuses(_conection, desde, Hasta).ToList();
            listadoByCostos = negocio.ListarAsistenciaSalidaUnidadesTransportePersonalByPeriod(_conection, desdeC, HastaC).ToList();


            listadoAsistenciaObservados = new List<ASJ_ReporteAsistenciaObservadosResult>();
            listadoAsistenciaObservados = negocio.GetListAssistanceObservedByBuss(_conection, desde, Hasta).ToList();

            //Funcion anterior
            listadoResumenAsistenciaByBusByDia = new List<AsistenciaBusByDia>();
            listadoResumenAsistenciaByBusByDia = negocio.GenerarAsistenciaResumida(listado, listadoAsistenciaObservados, incluirAsistenciaObervada, incluirRecorridosInternos).ToList();

            if (incluirRecorridosInternos == 0)
            {
                listadoResumenAsistenciaByBusByDia = new List<AsistenciaBusByDia>();
                listadoResumenAsistenciaByBusByDia = negocio.GenerarAsistenciaResumidaSinRecorridosInternos(listado, listadoAsistenciaObservados, incluirAsistenciaObervada, incluirRecorridosInternos).ToList();

            }


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (listado != null)
            {
                dgvAsistenciaTransferida.DataSource = listado.ToDataTable<ASJ_ListarRegistroMarcacionPersonalEnBusesResult>();
                dgvAsistenciaTransferida.Refresh();
            }

            if (listadoByCostos != null)
            {
                dgvAsistenciaPorFundo.DataSource = listadoByCostos.ToDataTable<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult>();
                dgvAsistenciaPorFundo.Refresh();
            }

            if (listadoAsistenciaObservados != null)
            {
                dgvAsistenciaObservados.DataSource = listadoAsistenciaObservados.ToDataTable<ASJ_ReporteAsistenciaObservadosResult>();
                dgvAsistenciaObservados.Refresh();
            }

            if (listadoResumenAsistenciaByBusByDia != null)
            {
                dgvResumenByDia.DataSource = listadoResumenAsistenciaByBusByDia.ToDataTable<AsistenciaBusByDia>();
                dgvResumenByDia.Refresh();
            }

            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedPage == tabAsistenciaTransferida)
                {
                    Exportar(dgvAsistenciaTransferida);
                }
                else if (tabControl.SelectedPage == tabAsistenciaByFundo)
                {
                    Exportar(dgvAsistenciaPorFundo);
                }
                else if (tabControl.SelectedPage == tabResumenByBus)
                {
                    Exportar(dgvResumenByDia);
                }


            }
            catch (Exception ex)
            {
                string message = String.Format("Error en el archivo.\nError message: {0}", ex.Message);
                RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }

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

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
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


        private void ListadoAsistenciaTransferenciaBuses_Load(object sender, EventArgs e)
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
                                                var resultado = listadoByCostos.Where(x => x.fecha == (dgvAsistenciaPorFundo.CurrentRow.Cells["chfecha"].Value).ToString() && x.placa == dgvAsistenciaPorFundo.CurrentRow.Cells["chplaca"].Value.ToString()).ToList();

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

        private void GoTransportesReporteAsistenciaTransferenciaBuses_FormClosing(object sender, FormClosingEventArgs e)
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
            globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }
    }
}
