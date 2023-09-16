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
using System.Globalization;
using System.Threading;

namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    public partial class PartesDiariosDeEquipamientoGeneracionMasiva : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_ParteDiariosDeDispositivosController model;
        private SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor selectedItem; //
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> ListFilterByProvider;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAll;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN01;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN02;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN03;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN04;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN05;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultAllAgrupadoN06;
        int NumeroDePartesDiariosRegistrados = 0;
        int Bandera = 0;
        private string CodigoProveedorSelecionado;
        private string CodigoSedeSelecionado;
        private string CodigoTipoDispositivoSelecionado;
        private string result;
        private int NumeroSemana;
        private int periodoSelecionado;
        private int semanaSelecionada;
        private List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> resultAllDocumentosCreados;

        public int codigoRegistroSolicitado;

        public PartesDiariosDeEquipamientoGeneracionMasiva()
        {
            try
            {
                InitializeComponent();

                RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
                RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
                RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
                RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
                conection = "SAS";
                user2 = new SAS_USUARIOS();
                user2.IdUsuario = "EAURAZO";
                user2.NombreCompleto = "AURAZO CARHUATANTA ERICK";

                companyId = "001";
                privilege = new PrivilegesByUser();
                privilege.nuevo = 1;
                CargarSemanas();
                ObtenerFechasIniciales();
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void PartesDiariosDeEquipamientoGeneracionMasiva_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();
                base.OnLoad(e);
                ResaltarResultado();
            }
            catch (TargetInvocationException ex)
            {
                result = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

        }

        private void ResaltarResultado()
        {
            if (Bandera % 2 == 0)
            {
                #region Pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.NotEqual, "0", "", true);
                c1.RowBackColor = Color.LightGreen;
                c1.CellBackColor = Color.LightGreen;
                c1.RowForeColor = Color.Black;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
                dgvListado.Columns["chcodigo"].ConditionalFormattingObjectList.Add(c1);
                #endregion

            }
            else
            {
                #region Despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.NotEqual, "0", "", true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                c1.RowForeColor = Color.Black;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvListado.Columns["chcodigo"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }


        }

        private void LoadFreightSummary()
        {
            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chRazonSocial", "Count : {0:N0}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);

                GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
                items2.Add(new GridViewSummaryItem("chCantidadEquipo", "Sum : {0:N0}; ", GridAggregateFunction.Sum));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items2);
            }
            //catch (TargetInvocationException ex)
            //{
            //    result = ex.InnerException.Message;
            //}
            //catch (Exception ex)
            //{
            //    result = ex.Message;
            //}
            catch (FilterExpressionException ex)
            {
                //FilterDescriptor wrongFilter = this.dgvListado.Columns["chcuenta"].FilterDescriptor;

                //FilterDescriptor filterDescriptor =
                //    new FilterDescriptor(wrongFilter.PropertyName, wrongFilter.Operator, correctValue);
                //filterDescriptor.IsFilterEditor = wrongFilter.IsFilterEditor;

                //this.dgvListado.FilterDescriptors.Remove(wrongFilter);
                //this.dgvListado.FilterDescriptors.Add(filterDescriptor);

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }



        public PartesDiariosDeEquipamientoGeneracionMasiva(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            try
            {
                InitializeComponent();

                RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
                RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
                RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
                RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
                conection = _conection;
                user2 = _user2;
                companyId = _companyId;
                privilege = _privilege;
                CargarSemanas();
                ObtenerFechasIniciales();
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void Actualizar()
        {
            try
            {
                btnNuevo.Enabled = false;
                gbListado.Enabled = false;
                btnConsultar.Enabled = false;
                pbar.Visible = true;
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }


        private void CargarSemanas()
        {

            MesesNeg = new MesController();
            cboSemana.DisplayMember = "descripcion";
            cboSemana.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboSemana.DataSource = MesesNeg.ListadoSemanasPorAnio(conection, Convert.ToInt32(this.txtPeriodo.Value)).ToList();


            var d = DateTime.Now;
            CultureInfo cul = CultureInfo.CurrentCulture;

            // Usa la fecha formateada y calcula el número de la semana
            NumeroSemana = cul.Calendar.GetWeekOfYear(
                 d,
                 CalendarWeekRule.FirstDay,
                 DayOfWeek.Sunday);



            cboSemana.SelectedValue = NumeroSemana.ToString();
        }

        private void ObtenerFechasIniciales()
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                SAS_PeriodoMaquinaria result01 = new SAS_PeriodoMaquinaria();
                result01 = model.ObtenerSemana(conection, NumeroSemana, DateTime.Now.Year);
                //this.txtPeriodo.Value = Convert.ToDecimal(result01.anio);
                this.txtFechaDesde.Text = result01.fechaInicio.Value.ToPresentationDate();
                this.txtFechaHasta.Text = result01.fechaFinal.Value.ToPresentationDate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void ObtenerFechas(int numeroSemana, int anio)
        {
            try
            {

                model = new SAS_ParteDiariosDeDispositivosController();
                SAS_PeriodoMaquinaria result01 = new SAS_PeriodoMaquinaria();
                result01 = model.ObtenerSemana(conection, numeroSemana, anio);
                this.txtFechaDesde.Text = result01.fechaInicio.Value.ToPresentationDate();
                this.txtFechaHasta.Text = result01.fechaFinal.Value.ToPresentationDate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        public void Inicio()
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
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }



        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnGenerarPartesDiarios_Click(object sender, EventArgs e)
        {
            try
            {
                NumeroDePartesDiariosRegistrados = 0;
                BarraPrincipal.Enabled = false;
                gbDocumento.Enabled = false;
                gbListado.Enabled = false;
                pbar.Visible = true;
                bgwGenerarPartesDiarios.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                EjecutarConsulta();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void EjecutarConsulta()
        {
            try
            {
                #region Ejecutar consulta()
                model = new SAS_ParteDiariosDeDispositivosController();
                resultAll = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>(); // Todos los dispositivos activos, en matenimiento y por devolver propios y terceros
                resultAllAgrupadoN01 = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>(); // Lista transformada de los dispositivos disponibles para generar parte diario
                resultAllDocumentosCreados = new List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult>(); // Listado de dispositivos entre los rangos de fechas, con la intensión de filtrar y con considerarlo en los partes.
                resultAllAgrupadoN02 = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>(); // Crear un Listado por dia, de los dispositivos que no tienen parte diarios restando los que ya están en algun parte de máquinaria
                resultAllAgrupadoN03 = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>(); //
                resultAllAgrupadoN04 = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
                resultAllAgrupadoN05 = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();

                // esta es la lista de todos los dispositivos
                resultAll = model.ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor(conection).ToList();

                // obtengo listado de partes creados en esa fecha, es decir los partes diarios que estan en al rando de fechas
                resultAllDocumentosCreados = model.ListarPorPeriodo(conection, desde, hasta).ToList();

                //Obtener listado de dispositivos con cantidad de partes 
                resultAllAgrupadoN01 = model.ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedorAgrupado(resultAll);

                // Crear una lista posible lista de los dispositivos pendientes a crear un parte diario
                resultAllAgrupadoN02 = model.CrearListadoDeDispositivosParaPartesDeEquipamientoPorPeriodos(desde, hasta, resultAllAgrupadoN01).ToList();


                // Aqui tendre la lista de solo la lista de esta pendiente de creación
                resultAllAgrupadoN03 = model.DepurarListaPendienteDeCreacionConListaCreadaPorPeriodo(resultAllAgrupadoN02, resultAllDocumentosCreados).ToList();


                // Aquí tendre la lista con el codigo de parte diario de equipamiento, sobre la base de la lista generica
                resultAllAgrupadoN04 = model.GenerarListadoGenericoDeDispositivosQueYaTienenParteDiario(resultAllDocumentosCreados);

                // Aquí junto las dos listas, que son las que voy a mostrar en el reporte.

                resultAllAgrupadoN05.AddRange(resultAllAgrupadoN03);
                resultAllAgrupadoN05.AddRange(resultAllAgrupadoN04);


                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MostrarResultadoProcesoAsincrono();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void MostrarResultadoProcesoAsincrono()
        {
            try
            {
                #region MostrarResultadoProcesoAsincrono()
                dgvListado.DataSource = resultAllAgrupadoN05.OrderBy(x => x.fecha).ToList().ToDataTable<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
                dgvListado.Refresh();
                BarraPrincipal.Enabled = !false;
                btnNuevo.Enabled = !false;
                gbListado.Enabled = !false;
                gbDocumento.Enabled = !false;
                btnConsultar.Enabled = !false;
                pbar.Visible = !true;
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
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

        private void PartesDiariosDeEquipamientoGeneracionMasiva_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResaltarResultado_Click(object sender, EventArgs e)
        {
            Bandera += 1;
            ResaltarResultado();
        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnVerDetalleDeDispositivos_Click(object sender, EventArgs e)
        {
            ViewDetailDevice();
        }

        private void ViewDetailDevice()
        {
            try
            {
                ListFilterByProvider = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
                ListFilterByProvider = resultAll.Where(x => x.idClieprov.Trim().ToUpper() == CodigoProveedorSelecionado.Trim() && x.sedecodigo.Trim().ToUpper() == CodigoSedeSelecionado.Trim() && x.tipoDispositivoCodigo.Trim() == CodigoTipoDispositivoSelecionado.Trim()).ToList();

                if (selectedItem != null && ListFilterByProvider != null)
                {
                    if (selectedItem.ID != null)
                    {
                        if (selectedItem.ID == 0 && ListFilterByProvider.ToList().Count > 0)
                        {
                            int codigoSelecionado = selectedItem.ID;
                            PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo oFron = new PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo(conection, user2, companyId, privilege, ListFilterByProvider);
                            oFron.MdiParent = PartesDiariosDeEquipamientoGeneracionMasiva.ActiveForm;
                            oFron.WindowState = FormWindowState.Maximized;
                            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                            oFron.Show();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            //gbDocumento.Enabled = false;
            //gbListado.Enabled = false;
            CodigoProveedorSelecionado = string.Empty;
            CodigoSedeSelecionado = string.Empty;
            CodigoTipoDispositivoSelecionado = string.Empty;
            codigoRegistroSolicitado = 0;
            btnVerDetalleDeDispositivos.Enabled = false;
            btnEditarRegistro.Enabled = false;

            try
            {
                #region 
                selectedItem = new SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor();
                selectedItem.ID = 0;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString() != string.Empty)
                            {
                                codigoRegistroSolicitado = (dgvListado.CurrentRow.Cells["chcodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString()) : 0);
                                CodigoProveedorSelecionado = (dgvListado.CurrentRow.Cells["chidClieprov"].Value != null ? Convert.ToString(dgvListado.CurrentRow.Cells["chidClieprov"].Value.ToString()) : string.Empty);
                                CodigoSedeSelecionado = (dgvListado.CurrentRow.Cells["chSedeCodigo"].Value != null ? Convert.ToString(dgvListado.CurrentRow.Cells["chSedeCodigo"].Value.ToString()) : string.Empty);
                                CodigoTipoDispositivoSelecionado = (dgvListado.CurrentRow.Cells["chTipoHardwareCodigo"].Value != null ? Convert.ToString(dgvListado.CurrentRow.Cells["chTipoHardwareCodigo"].Value.ToString()) : string.Empty);

                                if (codigoRegistroSolicitado == 0)
                                {
                                    btnVerDetalleDeDispositivos.Enabled = true;
                                    btnEditarRegistro.Enabled = false;
                                }
                                else
                                {
                                    btnEditarRegistro.Enabled = true;
                                    selectedItem.ID = codigoRegistroSolicitado;
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

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Exportar(dgvListado);
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

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {


            try
            {
                periodoSelecionado = Convert.ToInt32(this.txtPeriodo.Value);
                semanaSelecionada = Convert.ToInt32(cboSemana.SelectedValue);
                ObtenerFechas(semanaSelecionada, periodoSelecionado);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void cboSemana_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                periodoSelecionado = Convert.ToInt32(this.txtPeriodo.Value);
                semanaSelecionada = Convert.ToInt32(cboSemana.SelectedValue);
                ObtenerFechas(semanaSelecionada, periodoSelecionado);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void bgwGenerarPartesDiarios_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                NumeroDePartesDiariosRegistrados = model.GenerarPartesDiariosMasivosDesdeProcesoMasivo(conection, user2, resultAllAgrupadoN03, resultAll);
                EjecutarConsulta();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwGenerarPartesDiarios_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                MessageBox.Show("Operacion realizada correctamente \n Se han registrado " + NumeroDePartesDiariosRegistrados.ToString("N") + "  partes diarios de equipamiento", "Partes diarios de equipamiento tecnológico");
                MostrarResultadoProcesoAsincrono();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwNotify_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void Modify()
        {
            try
            {
                if (selectedItem != null)
                {
                    if (selectedItem.ID != null)
                    {
                        if (selectedItem.ID != 0)
                        {
                            int codigoSelecionado = selectedItem.ID;
                            PartesDiariosDeEquipamientoDetalle oFron = new PartesDiariosDeEquipamientoDetalle(conection, user2, companyId, privilege, codigoSelecionado);
                            oFron.MdiParent = PartesDiariosDeEquipamientoGeneracionMasiva.ActiveForm;
                            oFron.WindowState = FormWindowState.Maximized;
                            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                            oFron.Show();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void subMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void bgwGenerarPartesDiarios_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgwHilo_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbar.Value = e.ProgressPercentage;
        }
    }
}


