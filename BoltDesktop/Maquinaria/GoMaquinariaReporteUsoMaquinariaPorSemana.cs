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
    public partial class GoMaquinariaReporteUsoMaquinariaPorSemana : Form
    {
        private string desde;
        private string hasta;
        private int incluirFeriados;
        private int incluirDomingos;
        private string periodo;
        private MesController MesesNeg;
        private ParteMaquinariaController modelo;
        private string nroSemana;
        private List<SAS_ListadoPartesDeMaquinariaPorSemanaResult> listadoPartesMaquinaria;
        private List<SAS_ListadoHorasDeTractorPorSemanaResult> listadoPorGPS;
        private List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult> listadoAbastecimientoCombustible;
        private List<TractorVS> listadoTractoresHomologados;
        private List<TablaComparativaHorasVSNISIRA> listadoComparativoEntreNISIRAVisualSAT;
        List<string> listadoUnidadesExcluidasParaReporte = new List<string>();
        List<string> listadoCombustibleIncluidoFiltro = new List<string>();
        List<string> listadoCategoriaDeMaquinariaEncluidoFiltro = new List<string>();

        List<string> listadoEstadosEncluidoFiltro = new List<string>();
        List<string> listadoLineaEncluidoFiltro = new List<string>();
        List<string> listadCultivosEncluidoFiltro = new List<string>();

        List<string> listadoPropioAlquiladoEncluidoFiltro = new List<string>();
        private int incluirTodasLasUnidadesAlReporte;
        private string fileName;
        private bool exportVisualSettings;
        private GlobalesHelper globalHelper;
        private List<SAS_ListadoFiltroReporteVisualSAT_NISIRAResult> listadoFiltros;
        private int UsarFiltrado = 0;
        private List<string> listadoUnidadesExcluidasParaReporteVSAT;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public string desdeC { get; private set; }

        public GoMaquinariaReporteUsoMaquinariaPorSemana()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = !true;
            gbDetalle.Enabled = !true;
            ProgressBar.Visible = true;
            menuPrincipal.Enabled = false;
            bgwFiltros.RunWorkerAsync();
        }


        public GoMaquinariaReporteUsoMaquinariaPorSemana(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
            gbConsulta.Enabled = !true;
            gbDetalle.Enabled = !true;
            ProgressBar.Visible = true;
            menuPrincipal.Enabled = false;
            bgwFiltros.RunWorkerAsync();
        }


        protected override void OnLoad(EventArgs e)
        {

            this.dgvBDParteMaquinaria.TableElement.BeginUpdate();
            this.dgvBDCombustible.TableElement.BeginUpdate();
            this.dgvComparativoVSvsNisira.TableElement.BeginUpdate();
            this.dgvDBVisualSAT.TableElement.BeginUpdate();

            this.LoadFreightSummary();
            this.dgvBDParteMaquinaria.TableElement.EndUpdate();
            this.dgvBDCombustible.TableElement.EndUpdate();
            this.dgvComparativoVSvsNisira.TableElement.EndUpdate();
            this.dgvDBVisualSAT.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvBDParteMaquinaria.MasterTemplate.AutoExpandGroups = true;
            this.dgvBDParteMaquinaria.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvBDParteMaquinaria.GroupDescriptors.Clear();
            this.dgvBDParteMaquinaria.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chidMaquinaria", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chHORAINICIO", "MIN : {0:N0}; ", GridAggregateFunction.Min));
            items1.Add(new GridViewSummaryItem("chHORAFINAL", "MAX : {0:N2}; ", GridAggregateFunction.Max));
            items1.Add(new GridViewSummaryItem("chdiferencia", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chHORAS_TRAB", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chHOROMETROINICIAL", "MIN : {0:N0}; ", GridAggregateFunction.Min));
            items1.Add(new GridViewSummaryItem("chHOROMETROFINAL", "MAX : {0:N2}; ", GridAggregateFunction.Max));


            this.dgvBDParteMaquinaria.MasterTemplate.SummaryRowsTop.Add(items1);

            this.dgvBDCombustible.MasterTemplate.AutoExpandGroups = true;
            this.dgvBDCombustible.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvBDCombustible.GroupDescriptors.Clear();
            this.dgvBDCombustible.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chproducto", "COUNT : {0:N0}; ", GridAggregateFunction.Count));
            items2.Add(new GridViewSummaryItem("chcantidad", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvBDCombustible.MasterTemplate.SummaryRowsTop.Add(items2);

            this.dgvComparativoVSvsNisira.MasterTemplate.AutoExpandGroups = true;
            this.dgvComparativoVSvsNisira.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvComparativoVSvsNisira.GroupDescriptors.Clear();
            this.dgvComparativoVSvsNisira.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items3 = new GridViewSummaryRowItem();
            items3.Add(new GridViewSummaryItem("chMaquinariaNombre", "COUNT : {0:N0}; ", GridAggregateFunction.Count));
            items3.Add(new GridViewSummaryItem("chHorasVisualSAT", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chHorasNisira", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chDiferencia", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chCantidadCombustible", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items3.Add(new GridViewSummaryItem("chgalonHoraParteMaquinaria", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            items3.Add(new GridViewSummaryItem("chgalonHoraRegistroVisualSAT", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            this.dgvComparativoVSvsNisira.MasterTemplate.SummaryRowsTop.Add(items3);



            this.dgvDBVisualSAT.MasterTemplate.AutoExpandGroups = true;
            this.dgvDBVisualSAT.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDBVisualSAT.GroupDescriptors.Clear();
            this.dgvDBVisualSAT.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items4 = new GridViewSummaryRowItem();
            items4.Add(new GridViewSummaryItem("chtracto", "COUNT : {0:N0}; ", GridAggregateFunction.Count));
            items4.Add(new GridViewSummaryItem("chtotalMinutos", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chminutosEnLote", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chprocentajeEnLote", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            items4.Add(new GridViewSummaryItem("chkilometrosRecorridosTotal", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chkilometrosRecorridosTotalEnLote", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chAvance_KMxTurno", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            items4.Add(new GridViewSummaryItem("chtotalParadas", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chhorasTrabajadas", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chhorasTrabajadasLote", "SUM : {0:N2}; ", GridAggregateFunction.Sum));
            items4.Add(new GridViewSummaryItem("chHorasRecorridosEnLote", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            this.dgvDBVisualSAT.MasterTemplate.SummaryRowsTop.Add(items4);




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


        private void GoMaquinariaReporteUsoMaquinariaPorSemana_Load(object sender, EventArgs e)
        {

        }


        private void ObtenerValoresConsultaBuscar()
        {
            try
            {
                /* Formato de busqueda '20190321', '20191221' */
                periodo = this.txtPeriodo.Value.ToString();
                nroSemana = this.txtSemana.Value.ToString();
                desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
                incluirFeriados = (chkIncluirFeriados.Checked == true ? 1 : 0);
                incluirDomingos = (chkIncluirDomingos.Checked == true ? 1 : 0);
                incluirTodasLasUnidadesAlReporte = (chkIncluirTodasLasUnidades.Checked == true ? 1 : 0);

                // agregar | Filtro 01 | lista de tipo de unidad al filtro
                listadoCategoriaDeMaquinariaEncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboTipoTractor.CheckedItems)
                {
                    listadoCategoriaDeMaquinariaEncluidoFiltro.Add(itemChecked.Value.ToString());
                }

                // agregar | Filtro 02 | lista  propio o alquilao al filtro 
                listadoPropioAlquiladoEncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboPropietario.CheckedItems)
                {
                    listadoPropioAlquiladoEncluidoFiltro.Add(itemChecked.Value.ToString());
                }

                // agregar | Filtro 03 | lista  estados al filtro 
                listadoEstadosEncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboEstado.CheckedItems)
                {
                    listadoEstadosEncluidoFiltro.Add(itemChecked.Value.ToString());
                }

                // agregar | Filtro 04 | lista linea al filtro 
                listadoLineaEncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboLinea.CheckedItems)
                {
                    listadoLineaEncluidoFiltro.Add(itemChecked.Value.ToString());
                }

                // agregar | Filtro 05 | lista Cultivo al filtro 
                listadCultivosEncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboCultivo.CheckedItems)
                {
                    listadCultivosEncluidoFiltro.Add(itemChecked.Value.ToString());
                }

                // agregar lista de combustible al filtro
                listadoCombustibleIncluidoFiltro = new List<string>();
                foreach (var itemChecked in cboCombustible.CheckedItems)
                {
                    listadoCombustibleIncluidoFiltro.Add(itemChecked.Value.ToString());
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new ParteMaquinariaController();


            // listado registros (visual SAT)
            listadoPorGPS = new List<SAS_ListadoHorasDeTractorPorSemanaResult>();
            listadoPorGPS = modelo.GetListadoRecorridoPorGPSDeMaquinariaPorPeriodo("VSAT", desde, hasta, periodo, nroSemana).ToList();


            // listado registros parte de maquinaria (NISIRA)
            listadoPartesMaquinaria = new List<SAS_ListadoPartesDeMaquinariaPorSemanaResult>();
            listadoPartesMaquinaria = modelo.GetListadoParteMaquinariaPorPeriodo("BDO", desde, hasta, periodo, nroSemana).ToList();

            // listado registros combustible (NISIRA)
            listadoAbastecimientoCombustible = new List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult>();
            listadoAbastecimientoCombustible = modelo.GetListadoConsumoCombustibleDeMaquinariaPorPeriodo("BDO", desde, hasta, periodo, nroSemana).ToList();





            // listado maquinaria con codigo homologado entre visual sat y NISIRA
            listadoTractoresHomologados = new List<TractorVS>();
            listadoTractoresHomologados = modelo.GetListadoTractoresHomologadosVS("BDO").ToList();

            // FILTRO 1 | Tipo de tractor
            if (listadoCategoriaDeMaquinariaEncluidoFiltro != null && listadoCategoriaDeMaquinariaEncluidoFiltro.ToList().Count > 0)
            {
                listadoTractoresHomologados = (from items in listadoTractoresHomologados.ToList()
                                               where (listadoCategoriaDeMaquinariaEncluidoFiltro.Contains(items.tractorImplemento.ToString().ToUpper().Trim()))
                                               select items).ToList();
            }



            // FILTRO 2 | Propio o alquilado
            if (listadoPropioAlquiladoEncluidoFiltro != null && listadoPropioAlquiladoEncluidoFiltro.ToList().Count > 0)
            {
                listadoTractoresHomologados = (from items in listadoTractoresHomologados.ToList()
                                               where (listadoPropioAlquiladoEncluidoFiltro.Contains(items.PropioAlquilado.ToString().ToUpper().Trim()))
                                               select items).ToList();
            }


            // FILTRO 3 | Estados
            if (listadoEstadosEncluidoFiltro != null && listadoEstadosEncluidoFiltro.ToList().Count > 0)
            {
                listadoTractoresHomologados = (from items in listadoTractoresHomologados.ToList()
                                               where (listadoEstadosEncluidoFiltro.Contains(items.estado.ToString().ToUpper().Trim()))
                                               select items).ToList();
            }


            // FILTRO 4 | Lineas
            if (listadoLineaEncluidoFiltro != null && listadoLineaEncluidoFiltro.ToList().Count > 0)
            {
                listadoTractoresHomologados = (from items in listadoTractoresHomologados.ToList()
                                               where (listadoLineaEncluidoFiltro.Contains(items.linea.ToString().ToUpper().Trim()))
                                               select items).ToList();
            }


            // FILTRO 5 | Cultivos
            if (listadCultivosEncluidoFiltro != null && listadCultivosEncluidoFiltro.ToList().Count > 0)
            {
                listadoTractoresHomologados = (from items in listadoTractoresHomologados.ToList()
                                               where (listadCultivosEncluidoFiltro.Contains(items.cultivo.ToString().ToUpper().Trim()))
                                               select items).ToList();
            }


            // FILTRO 6 | cOMBUSTIBLE
            if (listadoCombustibleIncluidoFiltro != null && listadoCombustibleIncluidoFiltro.ToList().Count > 0)
            {
                listadoAbastecimientoCombustible = (from items in listadoAbastecimientoCombustible.ToList()
                                                    where (listadoCombustibleIncluidoFiltro.Contains(items.idproducto.ToString().Trim()))
                                                    select items).ToList();
            }


            listadoUnidadesExcluidasParaReporte = new List<string>();
            listadoUnidadesExcluidasParaReporteVSAT = new List<string>();
            foreach (var item in listadoTractoresHomologados)
            {
                listadoUnidadesExcluidasParaReporte.Add(item.idConsumidor.Trim());
                listadoUnidadesExcluidasParaReporteVSAT.Add(item.TractorVisualSAT.Trim());
            }



            //LISTADO compartivo entre NISIRA y Visual SAT
            listadoComparativoEntreNISIRAVisualSAT = new List<TablaComparativaHorasVSNISIRA>();
            listadoComparativoEntreNISIRAVisualSAT = modelo.GetTablaComparativaPorSemana(listadoTractoresHomologados, listadoPorGPS, listadoPartesMaquinaria, listadoAbastecimientoCombustible).ToList();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // listado registros (visual SAT)

            if (incluirTodasLasUnidadesAlReporte == 0)
            {
                listadoPorGPS = (from items in listadoPorGPS.ToList()
                                           where (listadoUnidadesExcluidasParaReporteVSAT.Contains(items.tracto.ToString().Trim()))
                                           select items).ToList();
            }         

            dgvDBVisualSAT.DataSource = listadoPorGPS.ToDataTable<SAS_ListadoHorasDeTractorPorSemanaResult>();
            dgvDBVisualSAT.Refresh();

            // listado registros parte de maquinaria (NISIRA)
            if (incluirTodasLasUnidadesAlReporte == 0)
            {
                listadoPartesMaquinaria = (from items in listadoPartesMaquinaria.ToList()
                                           where (listadoUnidadesExcluidasParaReporte.Contains(items.idMaquinaria.ToString().Trim()))
                                           select items).ToList();
            }
            dgvBDParteMaquinaria.DataSource = listadoPartesMaquinaria.ToDataTable<SAS_ListadoPartesDeMaquinariaPorSemanaResult>();
            dgvBDParteMaquinaria.Refresh();



            // listado registros combustible (NISIRA)
            if (incluirTodasLasUnidadesAlReporte == 0)
            {
                listadoAbastecimientoCombustible = (from items in listadoAbastecimientoCombustible.Where(x=> x.idConsumidor != null).ToList()
                                                    where (listadoUnidadesExcluidasParaReporte.Contains(items.idConsumidor.ToString().Trim()))
                                                    select items).ToList();               

            }
            dgvBDCombustible.DataSource = listadoAbastecimientoCombustible.ToDataTable<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult>();
            dgvBDCombustible.Refresh();




            //LISTADO compartivo entre NISIRA y Visual SAT
            if (incluirTodasLasUnidadesAlReporte == 0)
            {
                listadoComparativoEntreNISIRAVisualSAT = (from items in listadoComparativoEntreNISIRAVisualSAT.ToList()
                                                          where (listadoUnidadesExcluidasParaReporte.Contains(items.idMaquinaria.ToString().Trim()))
                                                          select items).ToList();
            }
            dgvComparativoVSvsNisira.DataSource = listadoComparativoEntreNISIRAVisualSAT.ToDataTable<TablaComparativaHorasVSNISIRA>();
            dgvComparativoVSvsNisira.Refresh();


            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;

        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            gbConsulta.Enabled = false;
            gbDetalle.Enabled = false;
            menuPrincipal.Enabled = false;
            ProgressBar.Visible = true;
            bgwSincronizar.RunWorkerAsync();

        }

        private void btnUniformizarFechas_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtenerValoresConsultaBuscar();
                gbConsulta.Enabled = !true;
                gbDetalle.Enabled = !true;
                ProgressBar.Visible = true;
                menuPrincipal.Enabled = false;
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedPage == tabBDCombustible)
                {
                    Exportar(dgvBDCombustible);
                }
                else if (tabControl.SelectedPage == tabBDPartesMaquinaria)
                {
                    //                    Exportar(dgvBDParteMaquinaria);
                    Exportar(dgvBDParteMaquinaria);
                }
                else if (tabControl.SelectedPage == tabComparativoVisualSATvsNISIRA)
                {
                    Exportar(dgvComparativoVSvsNisira);
                }
                else if (tabControl.SelectedPage == tabDBVisualSAT)
                {
                    Exportar(dgvDBVisualSAT);
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

        private void bgwSincronizar_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new ParteMaquinariaController();
            modelo.TransferirInformacion("BDO");

        }

        private void bgwSincronizar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            MessageBox.Show("Sincronizacion correcta", "Confirmación del sistema");
            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            menuPrincipal.Enabled = !false;
            ProgressBar.Visible = !true;

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
                if (this.bgwSincronizar.IsBusy == true)
                {


                    MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                    "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Close();
            }
        }

        private void GoMaquinariaReporteUsoMaquinariaPorSemana_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.bgwSincronizar.IsBusy == true)
            {

                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwFiltros_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listadoFiltros = new List<SAS_ListadoFiltroReporteVisualSAT_NISIRAResult>();
                modelo = new ParteMaquinariaController();
                listadoFiltros = modelo.ListadoFiltroReporteVisualSAT_NISIRA("BDO").ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void bgwFiltros_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //cboTipoTractor.DisplayMember = "descripcion";
            //cboTipoTractor.ValueMember = "valor";
            ////cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            //cboTipoTractor.DataSource = listadoFiltros.Where(x => x.codigo == 1).ToList();

            foreach (var item in listadoFiltros.Where(x => x.codigo == 1).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = false;
                if (item.descripcion.Trim().ToUpper() == "TRACTOR")
                {
                    oItem.Checked = true;
                    cboTipoTractor.Text = "TRACTOR";
                }
                oItem.Text = item.descripcion;
                oItem.Value = item.valor;
                cboTipoTractor.Items.Add(oItem);
            }


            foreach (var item in listadoFiltros.Where(x => x.codigo == 2).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = false;
                if (item.descripcion.Trim().ToUpper() == "PROPIO" || item.descripcion.Trim().ToUpper() == "ALQUILADO")
                {
                    oItem.Checked = true;
                    cboPropietario.Text = item.descripcion.Trim().ToUpper();
                }
                oItem.Text = item.descripcion;
                oItem.Value = item.valor;
                cboPropietario.Items.Add(oItem);
            }


            foreach (var item in listadoFiltros.Where(x => x.codigo == 3).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = false;
                if (item.descripcion.Trim().ToUpper() == "VIGENTE")
                {
                    oItem.Checked = true;
                    cboEstado.Text = item.descripcion.Trim().ToUpper();
                }
                oItem.Text = item.descripcion;
                oItem.Value = item.valor;
                cboEstado.Items.Add(oItem);
            }


            foreach (var item in listadoFiltros.Where(x => x.codigo == 4).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = false;
                if (item.descripcion.Trim().ToUpper() == "LINEA VERDE")
                {
                    oItem.Checked = true;
                    cboLinea.Text = item.descripcion.Trim().ToUpper();
                }
                oItem.Text = item.descripcion;
                oItem.Value = item.valor;
                cboLinea.Items.Add(oItem);
            }

            foreach (var item in listadoFiltros.Where(x => x.codigo == 5).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = true;
                oItem.Text = item.descripcion;
                cboCultivo.Text = item.descripcion.Trim().ToUpper();
                oItem.Value = item.valor;
                cboCultivo.Items.Add(oItem);
            }


            foreach (var item in listadoFiltros.Where(x => x.codigo == 6).ToList())
            {
                RadCheckedListDataItem oItem = new Telerik.WinControls.UI.RadCheckedListDataItem();
                oItem.Checked = false;
                if (item.valor.Trim().ToUpper() == "250600300004" || item.valor.Trim().ToUpper() == "250600300005")
                {
                    oItem.Checked = true;
                    cboCombustible.Text = item.descripcion.Trim().ToUpper();
                }
                oItem.Text = item.descripcion;
                oItem.Value = item.valor;
                cboCombustible.Items.Add(oItem);
            }





            //cboTipoTractor.CheckedItems = [4];
            //cboTipoTractor.Text = "TRACTOR";

            gbConsulta.Enabled = true;
            gbDetalle.Enabled = true;
            ProgressBar.Visible = !true;
            menuPrincipal.Enabled = !false;

        }

        private void chkIncluirTodasLasUnidades_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncluirTodasLasUnidades.Checked == true)
            {
                UsarFiltrado = 1;
                cboCombustible.Enabled = false;
                cboCultivo.Enabled = false;
                cboEstado.Enabled = false;
                cboLinea.Enabled = false;
                cboPropietario.Enabled = false;
                cboTipoTractor.Enabled = false;
            }
            else
            {
                UsarFiltrado = 0;
                cboCombustible.Enabled = !false;
                cboCultivo.Enabled = !false;
                cboEstado.Enabled = !false;
                cboLinea.Enabled = !false;
                cboPropietario.Enabled = !false;
                cboTipoTractor.Enabled = !false;
            }
        }

        private void cboTipoTractor_ItemCheckedChanged(object sender, RadCheckedListDataItemEventArgs e)
        {

        }
    }
}
