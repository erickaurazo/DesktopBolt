using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using MyControlsDataBinding.Extensions;
using System.Globalization;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using Asistencia.Negocios;
using Asistencia.Datos;

namespace Asistencia
{
    public partial class GoPlanillasReporteAsistenciaPorPuerta : Form
    {
        
        private SemanaController modeloSemana;
        private SJ_Semanas oSemana;
        private SJ_Semanas oSemanaConsulta;
        private List<Garita> listadoPuertasDeGarita;
        private GaritaController garitaNegocio;
        private string desde;
        private string hasta;
        private string desdeP;
        private string hastaP;
        private string idPlanilla;
        private char? idGarita;
        private AsistenciaController negocio;
        private List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listado;
        private List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listadoAgrupado;
        private List<ASJ_ReporteAsistenciaByPuertaResult> listadoDetalleByMarcacionByDatos;
        private List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult> listadoDatosCompletos;



        private bool exportVisualSettings;
        private string fileName;
        private List<ASJ_ReporteAsistenciaByPuertaResult> listadoDetalleByMarcacion;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public GoPlanillasReporteAsistenciaPorPuerta()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            CargarCombos();
            ObtenerFechasIniciales();
        }

        public GoPlanillasReporteAsistenciaPorPuerta(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            CargarCombos();
            ObtenerFechasIniciales();
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

        private void CargarCombos()
        {
            MesController Meses = new MesController();
            cboMes.DisplayMember = "descripcion";
            cboMes.ValueMember = "valor";
            cboMes.DataSource = Meses.ListarMeses().ToList();
            cboMes.SelectedValue = DateTime.Now.ToString("13");

            listadoPuertasDeGarita = new List<Garita>();
            garitaNegocio = new GaritaController();
            cboGarita.ValueMember = "codigo";
            cboGarita.DisplayMember = "descripcion";
            cboGarita.DataSource = garitaNegocio.Listado().ToList();
            cboGarita.SelectedValue = string.Empty;

        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Text = DateTime.Now.Year.ToString();
            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture).Substring(3, 7);
            this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();


            modeloSemana = new SemanaController();
            oSemana = new SJ_Semanas();
            oSemanaConsulta = new SJ_Semanas();
            oSemana.año = Convert.ToInt32(this.txtPeriodo.Value);
            oSemanaConsulta = modeloSemana.GetWeekByNumberWeek(oSemana, _conection);
            int numeroSemana = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DateTime.Now.DayOfWeek) - 1;


            /*
            DayOfWeek referenceDayOfWeek = DateTime.Now.DayOfWeek;
            DateTime mondayOfTheWeek, sundayOfTheWeek = DateTime.Now;

            mondayOfTheWeek = DateTime.Now;
            while (mondayOfTheWeek.DayOfWeek != DayOfWeek.Monday)
            {
                mondayOfTheWeek = mondayOfTheWeek.AddDays(-1);
            }

            sundayOfTheWeek = DateTime.Now;
            while (sundayOfTheWeek.DayOfWeek != DayOfWeek.Sunday)
            {
                sundayOfTheWeek = sundayOfTheWeek.AddDays(1);
            }
            */


            // txtSemana.Value = DiaDeLaSemana(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);


        }

        public void Inicio()
        {
            try
            {
                
                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZOC";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {

            this.dgvAsistenciaResumen.TableElement.BeginUpdate();
            //this.dgvListado.Columns["chDocumento"].FormatString = "{0:N0}";
            this.LoadFreightSummary();
            this.dgvAsistenciaResumen.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvAsistenciaResumen.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaResumen.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaResumen.GroupDescriptors.Clear();
            this.dgvAsistenciaResumen.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chNOMBRES", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaResumen.MasterTemplate.SummaryRowsTop.Add(items1);

            

            this.dgvDetalle.MasterTemplate.AutoExpandGroups = true;
            this.dgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.GroupDescriptors.Clear();
            this.dgvDetalle.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items3 = new GridViewSummaryRowItem();
            items3.Add(new GridViewSummaryItem("chNOMBRESDetalle", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvDetalle.MasterTemplate.SummaryRowsTop.Add(items3);


            this.dgvDetalleByDatosCompletos.MasterTemplate.AutoExpandGroups = true;
            this.dgvDetalleByDatosCompletos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleByDatosCompletos.GroupDescriptors.Clear();
            this.dgvDetalleByDatosCompletos.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chNOMBRESDetalleDC", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvDetalleByDatosCompletos.MasterTemplate.SummaryRowsTop.Add(items2);





            //


        }



        private void ReporteAsistenciaDiaByPuertaIngreso_Load(object sender, EventArgs e)
        {

        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            ObtenerFechasMes();
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        private void gbConsulta_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedPage == tabAsistenciaDetalle)
                {
                    Exportar(this.dgvDetalle);
                }
                else if (tabControl.SelectedPage == tabAsistenciaResumen)
                {
                    Exportar(this.dgvAsistenciaResumen);
                }
                else if (tabControl.SelectedPage == tabAsistenciaByDatosCompleto)
                {
                    Exportar(this.dgvDetalleByDatosCompletos);
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
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

            fileName = this.saveFileDialog.FileName;
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(fileName);
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
            excelExporter.SheetName = "Asistencia Pers.";
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void ReporteAsistenciaDiaByPuertaIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                negocio = new AsistenciaController();
                listado = new List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();
                listado = negocio.GetListAsistanceByDoor(_conection, desde, hasta, idPlanilla, idGarita).ToList();

                listadoDetalleByMarcacion = new List<ASJ_ReporteAsistenciaByPuertaResult>();
                listadoDetalleByMarcacion = negocio.ObtenerReporteAsistenciaByPuerta(_conection, desde, hasta).ToList();

                listadoDatosCompletos = new List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult>();
                listadoDatosCompletos = negocio.ObtenerReporteAsistenciaByPuertaDatosCompletos(_conection, desdeP, hastaP).ToList();

                listadoAgrupado = new List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();

                if (listado != null && listado.ToList().Count > 0)
                {
                    listadoAgrupado = negocio.AgruparListadoByFechaByPersona(listado).ToList();
                }


                listadoDetalleByMarcacionByDatos = new List<ASJ_ReporteAsistenciaByPuertaResult>();
                listadoDetalleByMarcacionByDatos = negocio.ObtenerReporteAsistenciaByPuertaOnlyRegistro(listadoDatosCompletos, listadoDetalleByMarcacion).ToList();




            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvAsistenciaResumen.DataSource = listadoAgrupado.ToDataTable<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();
                dgvAsistenciaResumen.Refresh();

                dgvDetalle.DataSource = listadoDetalleByMarcacion.ToDataTable<ASJ_ReporteAsistenciaByPuertaResult>();
                dgvDetalle.Refresh();

                dgvDetalleByDatosCompletos.DataSource = listadoDetalleByMarcacionByDatos.ToDataTable<ASJ_ReporteAsistenciaByPuertaResult>();
                dgvDetalleByDatosCompletos.Refresh();
            }

            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

            gbConsulta.Enabled = !false;
            gbRegistros.Enabled = !false;
            menuPrincipal.Enabled = !false;


        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            try
            {
                
                desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                desdeP = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                hastaP = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                idPlanilla = this.txtPlanillaCodigo.Text.Trim();
                idGarita = this.cboGarita.SelectedValue.ToString() != string.Empty ? Convert.ToChar(this.cboGarita.SelectedValue.ToString()) : (char?)null;
                if (desde != string.Empty && hasta != string.Empty)
                {
                    gbConsulta.Enabled = false;
                    gbRegistros.Enabled = false;
                    menuPrincipal.Enabled = false;
                    bgwHilo.RunWorkerAsync();
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }
    }
}
