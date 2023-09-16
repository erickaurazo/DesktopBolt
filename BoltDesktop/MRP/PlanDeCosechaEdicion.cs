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



namespace ComparativoHorasVisualSATNISIRA.MRP
{
    public partial class PlanDeCosechaEdicion : Form
    {

        private PrivilegesByUser _privilege;
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
        private List<SAS_PlanDeCosechaDetalleListadoFull> listing; //Listado
        private List<SAS_PlanDeCosechaDetalleListadoFull> selectedList; // ListaSelecionada
        private SAS_PlanDeCosechaDetalleListadoFull selectedItem; // Item Selecionado
        private int _idplan;
        private List<Grupo> listaSemanasParaCabeceraDeGrilla;

        public PlanDeCosechaEdicion()
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
            _privilege = new PrivilegesByUser();
            _privilege.nuevo = 1;
            _privilege.imprimir = 1;
            _privilege.editar = 1;
            _privilege.consultar = 1;
            _privilege.eliminar = 1;
            _privilege.anular = 1;
            _privilege.exportar = 1;

            //CargarMeses();
            //ObtenerFechasIniciales();
            //Actualizar();
        }

        public PlanDeCosechaEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, int idplan)
        {
            InitializeComponent();
            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = _conection;
            _user2 = _user2;
            _companyId = _companyId;
            _privilege = privilege;
            _idplan = idplan;

            gbDatosPersonal.Enabled = false;
            gbDetalle.Enabled = false;
            gbDocumento.Enabled = false;
            dgvDetalle.Enabled = false;
            BarraPrincipal.Enabled = false;

            bgwHilo.RunWorkerAsync();
            //CargarMeses();
            //ObtenerFechasIniciales();
            //Actualizar();
        }


        private void PlanDeCosechaEdicion_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            if (_idplan != null)
            {
                if (_idplan > 0)
                {
                    try
                    {

                        listing = new List<SAS_PlanDeCosechaDetalleListadoFull>();
                        model = new SAS_PlanDeCosechaController();
                        listing = model.GetListDetailByID("SAS", _idplan);

                        if (listing != null)
                        {
                            if (listing.ToList().Count > 0)
                            {
                                listaSemanasParaCabeceraDeGrilla = new List<Grupo>();
                                listaSemanasParaCabeceraDeGrilla = model.ObtenerListadoSemana(listing);

                            }
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

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                GenerarGrillaFromQuery(listing, listaSemanasParaCabeceraDeGrilla);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void GenerarGrillaFromQuery(List<SAS_PlanDeCosechaDetalleListadoFull> listQuery, List<Grupo> listWeelsQuery)
        {
            try
            {
                #region Generar Grilla desde query();
                this.dgvDetalle.DataSource = null;
                this.dgvDetalle.Rows.Clear();

                selectedList = new List<SAS_PlanDeCosechaDetalleListadoFull>();
                if (listQuery != null)
                {
                    if (listQuery.ToList().Count > 0)
                    {
                        selectedList.Add(listQuery.ElementAt(0));
                        var ListConvertToDatatable = selectedList.ToDataTable<SAS_PlanDeCosechaDetalleListadoFull>();

                        foreach (DataColumn item in ListConvertToDatatable.Columns)
                        {
                            System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                            chcolumna.DataPropertyName = item.ColumnName; // Comoviene del procedure
                            chcolumna.Frozen = true; // si quiere estar congelado
                            chcolumna.HeaderText = item.ColumnName; // Nombre para mostrar
                            chcolumna.Name = "ch" + item.ColumnName; // ch + nombre de columna
                            chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            dgvDetalle.Columns.Add(chcolumna);
                            chcolumna.Visible = false;
                            chcolumna.ReadOnly = true;
                            if (item.ColumnName.ToUpper().Trim() == "Variedad".ToUpper().Trim() ||
                                item.ColumnName.ToUpper().Trim() == "consumidor".ToUpper().Trim() ||
                                item.ColumnName.ToUpper().Trim() == "tipoCultivo".ToUpper().Trim())
                            {
                                chcolumna.Visible = true;
                            }
                        }

                        foreach (var item in listWeelsQuery.OrderBy(x => x.Codigo))
                        {
                            System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                            chcolumna.DataPropertyName = item.Codigo; // Comoviene del procedure
                            chcolumna.Frozen = false; // si quiere estar congelado
                            chcolumna.HeaderText = item.Codigo; // Nombre para mostrar
                            chcolumna.Name = "ch" + item.Codigo; // ch + nombre de columna
                            chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                            chcolumna.Visible = true;
                            chcolumna.ValueType = typeof(decimal?);
                            chcolumna.DefaultCellStyle.Format = "N2";
                            dgvDetalle.Columns.Add(chcolumna);
                            chcolumna.ReadOnly = true;
                        }

                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumnaSubTotal = new DataGridViewTextBoxColumn();
                        chcolumnaSubTotal.DataPropertyName = "SubTotalH"; // Comoviene del procedure
                        chcolumnaSubTotal.Frozen = false; // si quiere estar congelado
                        chcolumnaSubTotal.HeaderText = "SubTotalH"; // Nombre para mostrar
                        chcolumnaSubTotal.Name = "ch" + "SubTotalH"; // ch + nombre de columna                        
                        chcolumnaSubTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        chcolumnaSubTotal.ValueType = typeof(decimal?);
                        chcolumnaSubTotal.DefaultCellStyle.Format = "N2";
                        chcolumnaSubTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        chcolumnaSubTotal.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        chcolumnaSubTotal.ReadOnly = true;
                        dgvDetalle.Columns.Add(chcolumnaSubTotal);

                    }
                }

                CargarDatosAgrilla(listQuery, listWeelsQuery);


                PintarResultadosGrilla();

                gbDatosPersonal.Enabled = true;
                gbDetalle.Enabled = true;
                gbDocumento.Enabled = true;
                dgvDetalle.Enabled = true;
                BarraPrincipal.Enabled = true;

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void PintarResultadosGrilla()
        {
           if (dgvDetalle != null && dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                {
                    if (fila.Cells["chConsumidor"].Value.ToString().Trim().ToUpper() == "TOTALIZADOS".ToUpper())
                    {
                        for (int i = 0; i < dgvDetalle.Columns.Count; i++)
                        {
                            dgvDetalle.Rows[fila.Index].Cells[i].Style.BackColor = Color.Aquamarine;
                            dgvDetalle.Rows[fila.Index].Cells[i].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                        }
                    }
                    
                }
            }
        }


        private void CargarDatosAgrilla(List<SAS_PlanDeCosechaDetalleListadoFull> listBuildQuery, List<Grupo> listWeelsQuery)
        {
            try
            {
                #region Llenar Grilla();
                int contador = 0;
                // se tienen que considerar cuatro puntos de variedad, campo y luego con las semanas segun corresponde

                var listadoVariedades = (from itemVariedad in listBuildQuery
                                         group itemVariedad by new { itemVariedad.variedad }
                                         into j
                                         select new { variedadCodigo = j.FirstOrDefault().idvariedad, nombre = j.Key.variedad }
                                         ).ToList();

                if (listadoVariedades != null && listadoVariedades.ToList().Count > 0)
                {
                    #region Filtro por variedad
                    foreach (var oVariedad in listadoVariedades)
                    {
                        var listadolotesPorVariedadSelecionada = listBuildQuery.Where(x => x.idvariedad.Trim() == oVariedad.variedadCodigo.Trim()).ToList();

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

                                        if (listado != null)
                                        {
                                            if (listado.ToList().Count > 0)
                                            {
                                                #region Agregar Aquí resultados();
                                                var oSelecionado = listado.ElementAt(0);
                                                ArrayList array = new ArrayList();

                                                foreach (System.Windows.Forms.DataGridViewTextBoxColumn oItem in dgvDetalle.Columns)
                                                {
                                                    #region MyRegion
                                                    string nombreColumna = dgvDetalle.Columns[oItem.Index].Name.ToString();
                                                    nombreColumna = nombreColumna.Substring(2);


                                                    if (nombreColumna == "versionDetallePlan")
                                                    {
                                                        array.Add(oSelecionado.versionDetallePlan.ToString());
                                                    }
                                                    else if (nombreColumna == "item")
                                                    {
                                                        array.Add(oSelecionado.item.ToString().Trim());
                                                    }
                                                    else if (nombreColumna == "idcultivo")
                                                    {
                                                        array.Add(oSelecionado.idcultivo.Trim());
                                                    }
                                                    else if (nombreColumna == "idvariedad")
                                                    {
                                                        array.Add(oSelecionado.idvariedad.Trim());
                                                    }
                                                    else if (nombreColumna == "idconsumidor")
                                                    {
                                                        array.Add(oSelecionado.idconsumidor.Trim());
                                                    }
                                                    else if (nombreColumna == "idcalidad")
                                                    {
                                                        array.Add(oSelecionado.idcalidad.Trim());
                                                    }

                                                    else if (nombreColumna == "anio")
                                                    {
                                                        array.Add(oSelecionado.anio.Trim());
                                                    }

                                                    else if (nombreColumna == "semana")
                                                    {
                                                        array.Add(oSelecionado.semana);
                                                    }
                                                    else if (nombreColumna == "cantidad")
                                                    {
                                                        array.Add(totalKgEnPlan.Value.ToDecimalPresentation().Trim());
                                                    }

                                                    else if (nombreColumna == "idempresa")
                                                    {
                                                        array.Add(oSelecionado.idempresa.Trim());
                                                    }


                                                    else if (nombreColumna == "idplan")
                                                    {
                                                        array.Add(oSelecionado.idplan.ToString().Trim());
                                                    }



                                                    else if (nombreColumna == "consumidor")
                                                    {
                                                        array.Add(oSelecionado.consumidor.Trim());
                                                    }



                                                    else if (nombreColumna == "cultivo")
                                                    {
                                                        array.Add(oSelecionado.cultivo.Trim());
                                                    }


                                                    else if (nombreColumna == "variedad")
                                                    {
                                                        array.Add(oSelecionado.variedad.Trim());
                                                    }


                                                    else if (nombreColumna == "tipoCultivo")
                                                    {
                                                        array.Add(oSelecionado.tipoCultivo.Trim());
                                                    }

                                                    else if (nombreColumna == "calidad")
                                                    {
                                                        array.Add(oSelecionado.calidad.Trim());
                                                    }

                                                    else if (nombreColumna == "periodoSemana")
                                                    {
                                                        array.Add(oSelecionado.periodoSemana.Trim());
                                                    }

                                                    else if (nombreColumna == "serie")
                                                    {
                                                        array.Add(oSelecionado.serie.Trim());
                                                    }

                                                    else if (nombreColumna == "numero")
                                                    {
                                                        array.Add(oSelecionado.numero.ToString().Trim());
                                                    }


                                                    else if (nombreColumna == "fecha")
                                                    {
                                                        array.Add(oSelecionado.fecha.ToPresentationDate());
                                                    }

                                                    else if (nombreColumna == "documento")
                                                    {
                                                        array.Add(oSelecionado.documento);
                                                    }


                                                    else if (nombreColumna == "idresponsable")
                                                    {
                                                        array.Add(oSelecionado.idresponsable.ToString());
                                                    }

                                                    else if (nombreColumna == "responsable")
                                                    {
                                                        array.Add(oSelecionado.responsable);
                                                    }



                                                    else if (nombreColumna == "idsucursal")
                                                    {
                                                        array.Add(oSelecionado.idsucursal.ToString());
                                                    }

                                                    else if (nombreColumna == "numversion")
                                                    {
                                                        array.Add(oSelecionado.numversion.ToString());
                                                    }


                                                    else if (nombreColumna == "idcampana")
                                                    {
                                                        array.Add(oSelecionado.idsucursal.ToString());
                                                    }

                                                    else if (nombreColumna == "campanaAnual")
                                                    {
                                                        array.Add(oSelecionado.campanaAnual.ToString());
                                                    }




                                                    else if (nombreColumna == "idestado")
                                                    {
                                                        array.Add(oSelecionado.idestado.ToString());
                                                    }

                                                    else if (nombreColumna == "estadoDescripcion")
                                                    {
                                                        array.Add(oSelecionado.estadoDescripcion.ToString());
                                                    }

                                                    else if (nombreColumna == "estado")
                                                    {
                                                        array.Add(oSelecionado.estado.ToString());
                                                    }

                                                    else if (nombreColumna == "observaciones")
                                                    {
                                                        array.Add(oSelecionado.observaciones.ToString());
                                                    }



                                                    else if (nombreColumna == "estado")
                                                    {
                                                        array.Add(oSelecionado.estado.ToString());
                                                    }

                                                    else if (nombreColumna == "observaciones")
                                                    {
                                                        array.Add(oSelecionado.observaciones.ToString());
                                                    }




                                                    else if (nombreColumna == "sector")
                                                    {
                                                        array.Add(oSelecionado.sector.ToString().Trim());
                                                    }

                                                    else if (nombreColumna == "loteEstado")
                                                    {
                                                        array.Add(oSelecionado.loteEstado.ToString().Trim());
                                                    }


                                                    else if (nombreColumna == "area")
                                                    {
                                                        array.Add(oSelecionado.area.Value.ToDecimalPresentation());
                                                    }

                                                    else if (nombreColumna == "campanaCodigo")
                                                    {
                                                        array.Add(oSelecionado.campanaCodigo.ToString().Trim());
                                                    }


                                                    else if (nombreColumna == "tipoCultivoCodigo")
                                                    {
                                                        array.Add(oSelecionado.tipoCultivoCodigo.ToString().Trim());
                                                    }

                                                    else if (nombreColumna == "SubTotalH")
                                                    {
                                                        //array.Add(totalKgEnPlan.Value.ToDecimalPresentation());
                                                        array.Add(totalKgEnPlan.Value);
                                                    }


                                                    else
                                                    {

                                                        var valorSemanal = listado.Where(x => x.periodoSemana == nombreColumna).ToList();
                                                        if (valorSemanal != null)
                                                        {
                                                            if (valorSemanal.ToList().Count > 0)
                                                            {
                                                                // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                                                array.Add(valorSemanal.Sum(x => x.cantidad).Value);
                                                            }
                                                            else
                                                            {
                                                                //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                                                array.Add(Convert.ToDecimal(0));
                                                            }
                                                        }


                                                    }

                                                    #endregion

                                                    contador += 1;
                                                }
                                                dgvDetalle.AgregarFila(array);

                                                // agregar un totalizador inicial


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

                // se tiene que agregar una columna más totalizador vertical y horizontal.
                ArrayList arrayFinal = new ArrayList();
                foreach (System.Windows.Forms.DataGridViewTextBoxColumn oItem in dgvDetalle.Columns)
                {
                    #region MyRegion
                    string nombreColumna = dgvDetalle.Columns[oItem.Index].Name.ToString();
                    nombreColumna = nombreColumna.Substring(2);
                    SAS_PlanDeCosechaDetalleListadoFull oSelecionado = listBuildQuery.ElementAt(0);

                    if (nombreColumna == "versionDetallePlan")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "item")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idcultivo")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idvariedad")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idconsumidor")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idcalidad")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "anio")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "semana")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "cantidad")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idempresa")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "idplan")
                    {
                        arrayFinal.Add(oSelecionado.idplan.ToString().Trim());
                    }

                    else if (nombreColumna == "consumidor")
                    {
                        arrayFinal.Add("TOTALIZADOS");
                    }
                    else if (nombreColumna == "cultivo")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "variedad")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "tipoCultivo")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "calidad")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "periodoSemana")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "serie")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "numero")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "fecha")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "documento")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idresponsable")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "responsable")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idsucursal")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "numversion")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "idcampana")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "campanaAnual")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "idestado")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "estadoDescripcion")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "estado")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "observaciones")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "estado")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "observaciones")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "sector")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "loteEstado")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "area")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "campanaCodigo")
                    {
                        arrayFinal.Add(string.Empty);
                    }
                    else if (nombreColumna == "tipoCultivoCodigo")
                    {
                        arrayFinal.Add(string.Empty);
                    }

                    else if (nombreColumna == "SubTotalH")
                    {
                        //array.Add(totalKgEnPlan.Value.ToDecimalPresentation());
                        arrayFinal.Add(listBuildQuery.Sum(x => x.cantidad).Value);
                    }
                    else
                    {
                        var valorSemanal = listBuildQuery.Where(x => x.periodoSemana == nombreColumna).ToList();
                        if (valorSemanal != null)
                        {
                            if (valorSemanal.ToList().Count > 0)
                            {
                                // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                arrayFinal.Add(valorSemanal.Sum(x => x.cantidad).Value);
                            }
                            else
                            {
                                //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                arrayFinal.Add(Convert.ToDecimal(0));
                            }
                        }
                    }
                    #endregion

                }
                dgvDetalle.AgregarFila(arrayFinal);


                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            SumarElementosSeleccionadosGrilla(sender);
        }



        private void SumarElementosSeleccionadosGrilla(object senderGrilla)
        {
            try
            {
                if (((DataGridView)senderGrilla).CurrentRow != null && ((DataGridView)senderGrilla).CurrentCell != null)
                {
                    int fila = ((DataGridView)senderGrilla).CurrentRow.Index;
                    int columna = ((DataGridView)senderGrilla).CurrentCell.ColumnIndex;

                    decimal SumaSeleccionada = 0;
                    decimal promedioSeleccionado = 0;
                    int recuento = 0;

                    foreach (DataGridViewCell celda in ((DataGridView)senderGrilla).SelectedCells)
                    {
                        string tipoDato = celda.Value == null ? string.Empty : celda.Value.GetType().Name.ToString();
                        if (tipoDato != null && tipoDato != string.Empty)
                        {
                            if (tipoDato == "Double" || tipoDato == "Decimal")
                            {
                                SumaSeleccionada += Convert.ToDecimal(celda.Value);
                                recuento++;
                                promedioSeleccionado = (SumaSeleccionada / recuento);
                            }
                            else
                            {
                                //if (tipoDato == "String" && celda.ColumnIndex > 36)
                                //{
                                //    SumaSeleccionada += Convert.ToDecimal(celda.Value);
                                //    recuento++;
                                //    promedioSeleccionado = (SumaSeleccionada / recuento);
                                //}
                                //else
                                //{
                                SumaSeleccionada = 0;
                                recuento = 0;
                                promedioSeleccionado = 0;
                                //}

                                break;
                            }
                        }
                        else
                        {
                            SumaSeleccionada = 0;
                            recuento = 0;
                            promedioSeleccionado = 0;
                            break;
                        }
                    }

                    this.lblSumaSeleccionada.Text = SumaSeleccionada.ToString("N3");
                    this.lblRecuento.Text = recuento.ToString();
                    this.lblPromedio.Text = promedioSeleccionado.ToString("N3");

                    //this.lblSumaSeleccionada.Text = SumaSeleccionada.ToDecimalPresentation();
                    //this.lblRecuento.Text = recuento.ToString();
                    //this.lblPromedio.Text = promedioSeleccionado.ToDecimalPresentation();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}

