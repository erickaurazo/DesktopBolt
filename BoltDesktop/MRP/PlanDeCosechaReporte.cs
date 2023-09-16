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
using Telerik.Pivot;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.ViewModels;
using Telerik.Pivot.Core.Aggregates;
using Asistencia.Datos.MRP;

namespace ComparativoHorasVisualSATNISIRA.MRP
{
    public partial class PlanDeCosechaReporte : Form
    {

        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_PlanDeCosechaController model;
        private List<SAS_PlanDeCosechaListadoFull> listing; //Listado
        private List<SAS_PlanDeCosechaListadoFull> selectedList; // ListaSelecionada
        private SAS_PlanDeCosechaListadoFull selectedItem; // Item Selecionado

        private List<SAS_PlanDeCosechaDetalleByJabasListadoFull> listingFull; //Listado
        private List<SAS_PlanDeCosechaDetalleByJabasListadoFull> selectedFullList; // ListaSelecionada
        private SAS_PlanDeCosechaDetalleByJabasListadoFull selectedItemFull; // Item Selecionado
        private PivotExportToExcelML exporter;
        private int idplan = 0;
        private List<PlanCosechaAgrupado> listadoPlanCosechaAgrupado;
        private List<Grupo> listaSemanasParaCabeceraDeGrilla;
        private List<PlanCosechaAgrupado> selectedListAgrupado;
        private List<Grupo> listWeelsQuery;
        private string tipoUnidadMedidadPresentacion = "Jabas".ToUpper();

        public PlanDeCosechaReporte()
        {
            InitializeComponent();
            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = "SAS";
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = "EAURAZO";
            _user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";

            _companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.imprimir = 1;
            privilege.editar = 1;
            privilege.consultar = 1;
            privilege.eliminar = 1;
            privilege.anular = 1;
            privilege.exportar = 1;

            Inicio();
            CargarCombos();
            Actualizar();
            //tabControl.DefaultPage= 
        }



        private void Actualizar()
        {
            if (this.txtPlan.Text.Trim() != string.Empty && this.txtPlanCodigo.Text.Trim() != string.Empty)
            {
                idplan = Convert.ToInt32(this.txtPlanCodigo.Text.Trim());
                gbCabeceraConsulta.Enabled = false;
                gbListResult.Enabled = false;
                btnMenu.Enabled = false;
                barraDeVistaDeProcesoTareaAsincrona.Enabled = true;
                bgwHilo.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Debe ingresar el plan de cosecha para realizar la consulta", "Notificación del sistema");
                return;
            }



        }

        private void CargarCombos()
        {

        }

        private void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["SAS"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";


                try
                {
                    model = new SAS_PlanDeCosechaController();
                    cboTipoDatoParaPresentacion.DisplayMember = "descripcion";
                    cboTipoDatoParaPresentacion.ValueMember = "Codigo";
                    //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                    cboTipoDatoParaPresentacion.DataSource = model.ObtenerTipoUnidadMedidaParaPresentacionDePlanDeCosecha().ToList();
                    cboTipoDatoParaPresentacion.SelectedValue = "JABAS";
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                    return;
                }

                model = new SAS_PlanDeCosechaController();
                var resultado = model.GetListVersionPlanCosecha("SAS");

                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        this.txtPlan.Text = resultado.ElementAt(0).descripcion.Trim();
                        this.txtPlanCodigo.Text = resultado.ElementAt(0).codigo.ToString().Trim();

                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        public PlanDeCosechaReporte(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            Inicio();
            CargarCombos();
            Actualizar();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvDetalle.TableElement.BeginUpdate();
            this.dgvAgrupado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvDetalle.TableElement.EndUpdate();
            this.dgvAgrupado.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {

            this.dgvAgrupado.MasterTemplate.AutoExpandGroups = true;
            this.dgvAgrupado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAgrupado.GroupDescriptors.Clear();

            this.dgvDetalle.MasterTemplate.AutoExpandGroups = true;
            this.dgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.GroupDescriptors.Clear();
            this.dgvDetalle.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chcantidad", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("charea", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            items1.Add(new GridViewSummaryItem("chpesoPromedioJabaPorVariedad", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chnumeroJabas", "Sum : {0:N0}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chcontenedores", "Sum : {0:N1}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chconsumidor", "Count : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvDetalle.MasterTemplate.SummaryRowsTop.Add(items1);

        }



        private void cboVersión_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            tipoUnidadMedidadPresentacion = cboTipoDatoParaPresentacion.SelectedValue.ToString();
            Actualizar();

        }

        private void PlanDeCosechaReporte_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                model = new SAS_PlanDeCosechaController();
                listingFull = new List<SAS_PlanDeCosechaDetalleByJabasListadoFull>();
                listadoPlanCosechaAgrupado = new List<PlanCosechaAgrupado>();
                listingFull = model.GetListFull("SAS", idplan).ToList();
                listadoPlanCosechaAgrupado = model.ConvertGetListFull(listingFull);

                if (listingFull != null)
                {
                    if (listingFull.ToList().Count > 0)
                    {
                        listaSemanasParaCabeceraDeGrilla = new List<Grupo>();
                        listaSemanasParaCabeceraDeGrilla = model.ObtenerListadoSemana(listingFull);

                    }
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return;
            }


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Construir y presentar plan de cosecha()             
            dgvPivotHistorialAsistencia.DataSource = listingFull.ToDataTable<SAS_PlanDeCosechaDetalleByJabasListadoFull>();
            dgvPivotHistorialAsistencia.Refresh();
            this.dgvPivotHistorialAsistencia.ColumnGrandTotalsPosition = TotalsPos.Last;
            this.dgvPivotHistorialAsistencia.ColumnsSubTotalsPosition = TotalsPos.None;
            this.dgvPivotHistorialAsistencia.RowGrandTotalsPosition = TotalsPos.Last;
            this.dgvPivotHistorialAsistencia.RowsSubTotalsPosition = TotalsPos.First;
            #endregion

            #region  Construir y presentar lista detallada del plan de cosecha()
            dgvDetalle.DataSource = listingFull.ToDataTable<SAS_PlanDeCosechaDetalleByJabasListadoFull>();
            dgvDetalle.Refresh();
            #endregion

            #region Construir y presentar lista agrupada del plan de cosecha
            // Construir las cabeceras de la tabla agrupada


            try
            {
                #region Generar Grilla desde query();
                this.dgvAgrupado.DataSource = null;
                this.dgvAgrupado.Rows.Clear();
                this.dgvAgrupado.Columns.Clear();

                selectedListAgrupado = new List<PlanCosechaAgrupado>();

                if (listadoPlanCosechaAgrupado != null)
                {
                    #region                      
                    if (listadoPlanCosechaAgrupado.ToList().Count > 0)
                    {
                        selectedListAgrupado.Add(listadoPlanCosechaAgrupado.ElementAt(0));
                        var ListConvertToDatatable = selectedListAgrupado.ToDataTable<PlanCosechaAgrupado>();
                        GridViewSummaryRowItem item1 = new GridViewSummaryRowItem();
                        foreach (DataColumn item in ListConvertToDatatable.Columns)
                        {
                            #region Agregar cabeceras de lista Agrupada()                             
                            Telerik.WinControls.UI.GridViewTextBoxColumn chcolumna = new Telerik.WinControls.UI.GridViewTextBoxColumn();
                            chcolumna.Name = item.ColumnName; // Comoviene del procedure
                            //chcolumna.IsPinned = true; // si quiere estar congelado
                            chcolumna.HeaderText = item.ColumnName; // Nombre para mostrar
                            chcolumna.Name = "ch" + item.ColumnName; // ch + nombre de columna
                            //chcolumna.AutoSizeMode = BestFitColumnMode.SystemCells;
                            chcolumna.FieldName = item.ColumnName;
                            chcolumna.IsVisible = false;
                            chcolumna.ReadOnly = true;
                            if (item.ColumnName.ToUpper().Trim() == "Variedad".ToUpper().Trim() ||
                                item.ColumnName.ToUpper().Trim() == "consumidor".ToUpper().Trim() ||
                                item.ColumnName.ToUpper().Trim() == "tipoCultivo".ToUpper().Trim())
                            {
                                chcolumna.IsVisible = true;
                            }
                            if (item.ColumnName.ToUpper().Trim() == "consumidor".ToUpper().Trim())
                            {
                                item1.Add(new GridViewSummaryItem("ch" + "consumidor", "Count: {0:N0}", GridAggregateFunction.Count));
                            }

                            dgvAgrupado.MasterTemplate.Columns.Add(chcolumna);
                            #endregion
                        }

                        if (dgvAgrupado.MasterTemplate.SummaryRowsTop.Count >= 1)
                        {
                            for (int i = 0; i < dgvAgrupado.MasterTemplate.SummaryRowsTop.Count; i++)
                            {
                                dgvAgrupado.MasterTemplate.SummaryRowsTop.RemoveAt(i);
                            }
                        }

                        
                        #region Agregar ColumnaSubTotales()
                        Telerik.WinControls.UI.GridViewDecimalColumn chcolumnaSubTotalVertical = new Telerik.WinControls.UI.GridViewDecimalColumn();
                        chcolumnaSubTotalVertical.Name = "SubTotalH"; // Comoviene del procedure
                        //chcolumnaSubTotalVertical.IsPinned = true; // si quiere estar congelado
                        chcolumnaSubTotalVertical.HeaderText = "SubTotalH"; // Nombre para mostrar
                        chcolumnaSubTotalVertical.Name = "ch" + "SubTotalH"; // ch + nombre de columna  
                        chcolumnaSubTotalVertical.FormatString = "{0:N2}";
                        chcolumnaSubTotalVertical.FieldName = "SubTotalH";
                        chcolumnaSubTotalVertical.DecimalPlaces = 2;
                        chcolumnaSubTotalVertical.AllowGroup = false;
                        chcolumnaSubTotalVertical.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Standard;
                        //chcolumnaSubTotalVertical.AutoSizeMode = BestFitColumnMode.SystemCells;
                        chcolumnaSubTotalVertical.ReadOnly = true;
                        chcolumnaSubTotalVertical.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        dgvAgrupado.MasterTemplate.Columns.Add(chcolumnaSubTotalVertical);

                        item1.Add(new GridViewSummaryItem("ch" + "SubTotalH", "Sum: {0:N2}", GridAggregateFunction.Sum));


                        listWeelsQuery = listaSemanasParaCabeceraDeGrilla.ToList();
                        foreach (Grupo itemPeriodo in listWeelsQuery.OrderBy(x => x.Codigo))
                        {
                            #region 
                            Telerik.WinControls.UI.GridViewDecimalColumn chcolumnaSemanas = new Telerik.WinControls.UI.GridViewDecimalColumn();
                            chcolumnaSemanas.Name = itemPeriodo.Codigo.ToString().Trim(); // Comoviene del procedure
                            //chcolumnaSemanas.IsPinned = false; // si quiere estar congelado
                            chcolumnaSemanas.HeaderText = itemPeriodo.Codigo.ToString().Trim(); // Nombre para mostrar
                            chcolumnaSemanas.FieldName = itemPeriodo.Codigo.ToString().Trim();
                            chcolumnaSemanas.Name = "ch" + itemPeriodo.Codigo.ToString().Trim(); // ch + nombre de columna
                            //chcolumnaSemanas.AutoSizeMode = BestFitColumnMode.SystemCells;
                            chcolumnaSemanas.FormatString = "{0:N2}";
                            chcolumnaSemanas.IsVisible = true;
                            chcolumnaSemanas.AllowGroup = false;
                            chcolumnaSemanas.DecimalPlaces = 2;
                            chcolumnaSemanas.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Standard;
                            chcolumnaSemanas.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            chcolumnaSemanas.ReadOnly = true;
                            dgvAgrupado.MasterTemplate.Columns.Add(chcolumnaSemanas);

                            try
                            {
                                item1.Add(new GridViewSummaryItem("ch" + itemPeriodo.Codigo.ToString().Trim(), "Sum: {0:N2}", GridAggregateFunction.Sum));
                            }
                            catch (FilterExpressionException ex)
                            {
                                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                                return;
                            }

                            #endregion
                        }
                        this.dgvAgrupado.MasterTemplate.SummaryRowsTop.Add(item1);
                    }
                    #endregion
                    #endregion
                }

                CargarDatosAgrilla(listadoPlanCosechaAgrupado, listWeelsQuery);

                //PintarResultadosGrilla();

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

            // Construir a las cabeceras los periodos
            // Construir a las cabeceras un totalizador vertical al Final de las columas (Aquí tener en consideración si son Kg, Jabas o contenedores()
            // Agregar Datos a la grilla Elaborada
            // Agregar en el constructor que realice las sumas de todas las semanas.
            #endregion


            gbCabeceraConsulta.Enabled = !false;
            gbListResult.Enabled = !false;
            btnMenu.Enabled = !false;
            barraDeVistaDeProcesoTareaAsincrona.Enabled = !true;
        }

        private void CargarDatosAgrilla(List<PlanCosechaAgrupado> listBuildQuery, List<Grupo> listWeelsQuery)
        {
            try
            {
                #region Llenar Grilla();
                int contador = 0;
                // se tienen que considerar cuatro puntos de variedad, campo y luego con las semanas segun corresponde

                var listadoVariedades = (from itemVariedad in listBuildQuery
                                         group itemVariedad by new { itemVariedad.variedad }
                                         into j
                                         select new { variedad = j.Key.variedad }
                                         ).ToList();

                if (listadoVariedades != null && listadoVariedades.ToList().Count > 0)
                {
                    #region Filtro por variedad
                    foreach (var oVariedad in listadoVariedades)
                    {
                        var listadolotesPorVariedadSelecionada = listBuildQuery.Where(x => x.variedad.Trim() == oVariedad.variedad.Trim()).ToList();

                        if (listadolotesPorVariedadSelecionada != null && listadolotesPorVariedadSelecionada.ToList().Count > 0)
                        {

                            var listadoDelotes = (from itemLote in listadolotesPorVariedadSelecionada
                                                  group itemLote by new { itemLote.idconsumidor }
                                       into j
                                                  select new { loteCodigo = j.Key.idconsumidor, lote = j.FirstOrDefault().consumidor }
                                       ).ToList();

                            if (listadoDelotes != null)
                            {
                                if (listadoDelotes.ToList().Count > 0)
                                {

                                    foreach (var itemLote in listadoDelotes)
                                    {
                                        var listado = listadolotesPorVariedadSelecionada.Where(x => x.idconsumidor.Trim() == itemLote.loteCodigo.Trim()).ToList();
                                        var TotalesPorLote = listBuildQuery.Where(x => x.idconsumidor.Trim() == itemLote.loteCodigo.Trim()).ToList();
                                        decimal? totalKgEnPlan = TotalesPorLote.Sum(x => x.cantidad);
                                        decimal? TotalJabasEnPlan = TotalesPorLote.Sum(x => x.numeroJabas);
                                        decimal? TotalContenedoresEnPlan = TotalesPorLote.Sum(x => x.contenedores);

                                        if (listado != null)
                                        {
                                            if (listado.ToList().Count > 0)
                                            {
                                                #region Agregar Aquí resultados();
                                                var oSelecionado = listado.ElementAt(0);
                                                ArrayList array = new ArrayList();
                                                GridViewDataRowInfo rowInfo = new GridViewDataRowInfo(this.dgvAgrupado.MasterView);

                                                foreach (GridViewDataColumn oItem in dgvAgrupado.Columns)
                                                {
                                                    #region MyRegion
                                                    string nombreColumna = dgvAgrupado.Columns[oItem.Index].Name.ToString();
                                                    nombreColumna = nombreColumna.Substring(2);
                                                    rowInfo.Cells["ch" + nombreColumna].ColumnInfo.AutoSizeMode = BestFitColumnMode.DisplayedCells;

                                                    if (nombreColumna == "idplan")
                                                    {
                                                        array.Add(oSelecionado.idplan.ToString());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.idplan.ToString();                                                        
                                                    }
                                                    else if (nombreColumna == "mes")
                                                    {
                                                        array.Add(oSelecionado.mes.ToString().Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.mes.ToString().Trim();
                                                    }
                                                    else if (nombreColumna == "tipocultivo")
                                                    {
                                                        array.Add(oSelecionado.tipocultivo.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.tipocultivo.ToString().Trim();
                                                    }
                                                    else if (nombreColumna == "variedad")
                                                    {
                                                        array.Add(oSelecionado.variedad.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.variedad.ToString().Trim();
                                                    }
                                                    else if (nombreColumna == "cultivo")
                                                    {
                                                        array.Add(oSelecionado.cultivo.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.cultivo.ToString().Trim();
                                                    }
                                                    else if (nombreColumna == "idconsumidor")
                                                    {
                                                        array.Add(oSelecionado.idconsumidor.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.idconsumidor.ToString().Trim();
                                                    }

                                                    else if (nombreColumna == "consumidor")
                                                    {
                                                        array.Add(oSelecionado.consumidor.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.consumidor.ToString().Trim();
                                                    }

                                                    else if (nombreColumna == "cantidad")
                                                    {
                                                        array.Add(oSelecionado.cantidad);
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.cantidad.Value.ToDecimalPresentation().Trim();

                                                    }
                                                    else if (nombreColumna == "sector")
                                                    {
                                                        array.Add(oSelecionado.sector.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.sector.ToString().Trim();
                                                    }

                                                    else if (nombreColumna == "periodoSemana")
                                                    {
                                                        array.Add(oSelecionado.periodoSemana.Trim());
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.periodoSemana.ToString().Trim();
                                                    }


                                                    else if (nombreColumna == "pesoPromedioJabaPorVariedad")
                                                    {
                                                        array.Add(oSelecionado.pesoPromedioJabaPorVariedad.Value);
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.cantidad.Value;
                                                    }

                                                    else if (nombreColumna == "numeroJabas")
                                                    {
                                                        array.Add(oSelecionado.numeroJabas.Value);
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.numeroJabas.Value;
                                                    }


                                                    else if (nombreColumna == "contenedores")
                                                    {
                                                        array.Add(oSelecionado.contenedores.Value);
                                                        rowInfo.Cells["ch" + nombreColumna].Value = oSelecionado.contenedores.Value;
                                                    }

                                                    else if (nombreColumna == "SubTotalH")
                                                    {
                                                        //array.Add(totalKgEnPlan.Value.ToDecimalPresentation());
                                                        //array.Add(totalKgEnPlan.Value);
                                                        if (tipoUnidadMedidadPresentacion == "Kilogramos".ToUpper())
                                                        {
                                                            array.Add(totalKgEnPlan.Value);
                                                            rowInfo.Cells["ch" + nombreColumna].Value = totalKgEnPlan.Value;
                                                        }
                                                        else if (tipoUnidadMedidadPresentacion == "JABAS".ToUpper())
                                                        {
                                                            array.Add(TotalJabasEnPlan.Value);
                                                            rowInfo.Cells["ch" + nombreColumna].Value = TotalJabasEnPlan.Value;
                                                        }
                                                        else if (tipoUnidadMedidadPresentacion == "CONTENEDORES EQUIVALENTES".ToUpper())
                                                        {
                                                            array.Add(TotalContenedoresEnPlan.Value);
                                                            rowInfo.Cells["ch" + nombreColumna].Value = TotalContenedoresEnPlan.Value;
                                                        }

                                                    }


                                                    else
                                                    {
                                                        var valorSemanal = listado.Where(x => x.periodoSemana == nombreColumna).ToList();
                                                        if (valorSemanal != null)
                                                        {
                                                            if (valorSemanal.ToList().Count > 0)
                                                            {
                                                                // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                                                if (tipoUnidadMedidadPresentacion == "Kilogramos".ToUpper())
                                                                {
                                                                    array.Add(valorSemanal.Sum(x => x.cantidad).Value);
                                                                    rowInfo.Cells["ch" + nombreColumna].Value = valorSemanal.Sum(x => x.cantidad).Value;
                                                                }
                                                                else if (tipoUnidadMedidadPresentacion == "JABAS".ToUpper())
                                                                {
                                                                    array.Add(valorSemanal.Sum(x => x.numeroJabas).Value);
                                                                    rowInfo.Cells["ch" + nombreColumna].Value = valorSemanal.Sum(x => x.numeroJabas).Value;
                                                                }
                                                                else if (tipoUnidadMedidadPresentacion == "CONTENEDORES EQUIVALENTES".ToUpper())
                                                                {
                                                                    array.Add(valorSemanal.Sum(x => x.contenedores).Value);
                                                                    rowInfo.Cells["ch" + nombreColumna].Value = valorSemanal.Sum(x => x.contenedores).Value;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                                                array.Add(Convert.ToDecimal(0));
                                                                rowInfo.Cells["ch" + nombreColumna].Value = (Convert.ToDecimal(0));
                                                            }
                                                        }


                                                    }

                                                    #endregion

                                                }


                                                //rowInfo.Cells[0].Value = "1";                                               
                                                dgvAgrupado.Rows.Add(rowInfo);
                                                //dgvAgrupado.Rows.Add(array);




                                                #endregion
                                            }
                                        }


                                    }

                                }
                            }

                        }


                    }
                    #endregion
                }

                this.dgvAgrupado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

            if (tabControl.SelectedPage == tabPlanDeCosecha)
            {
                exporter = new PivotExportToExcelML(dgvPivotHistorialAsistencia);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel ML|*.xls";
                saveFileDialog1.Title = "Export to File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    exporter.RunExport(saveFileDialog1.FileName);
                    MessageBox.Show("Exportado satisfactoriamente " + saveFileDialog1.FileName, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.barraDeVistaDeProcesoTareaAsincrona.Value = 0;
                    try
                    {
                        System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                    }
                    finally
                    {
                    }
                }
            }
            else if (tabControl.SelectedPage == tabDetalle)
            {
                Exportar(dgvDetalle);
            }
            else if (tabControl.SelectedPage == tabAgrupado)
            {
                Exportar(dgvAgrupado);
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

        private void PlanDeCosechaReporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void chkMostrarSubTotalesVerticales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarSubTotalesVerticales.Checked == true)
            {
                this.dgvPivotHistorialAsistencia.ColumnsSubTotalsPosition = TotalsPos.First;
            }
            else
            {
                this.dgvPivotHistorialAsistencia.ColumnsSubTotalsPosition = TotalsPos.None;
            }
        }

        private void chkMostrarSubTotalesHorizontales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarSubTotalesHorizontales.Checked == true)
            {
                this.dgvPivotHistorialAsistencia.RowsSubTotalsPosition = TotalsPos.First;
            }
            else
            {
                this.dgvPivotHistorialAsistencia.RowsSubTotalsPosition = TotalsPos.None;
            }
        }

        private void chkMostrarTotalesVerticales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarTotalesVerticales.Checked == true)
            {
                this.dgvPivotHistorialAsistencia.ColumnGrandTotalsPosition = TotalsPos.First;

            }
            else
            {
                this.dgvPivotHistorialAsistencia.ColumnGrandTotalsPosition = TotalsPos.None;
            }
        }

        private void chkMostrarTotalesHorizontales_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarTotalesHorizontales.Checked == true)
            {

                this.dgvPivotHistorialAsistencia.RowGrandTotalsPosition = TotalsPos.Last;

            }
            else
            {
                this.dgvPivotHistorialAsistencia.RowGrandTotalsPosition = TotalsPos.None;
            }
        }

        private void lblPresentacionEn_Click(object sender, EventArgs e)
        {

        }

        private void cboTipoDatoParaPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedPage == tabPlanDeCosecha)
            {

            }
            else if (tabControl.SelectedPage == tabDetalle)
            {
                this.dgvDetalle.ShowColumnChooser();
            }
            else if (tabControl.SelectedPage == tabAgrupado)
            {
                this.dgvAgrupado.ShowColumnChooser();
            }
        }
    }
}
