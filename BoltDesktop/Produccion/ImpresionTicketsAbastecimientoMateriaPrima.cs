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
    public partial class ImpresionTicketsAbastecimientoMateriaPrima : Form
    {
        private ComboBoxHelper comboBoxHelper;
        private string desde;
        private string hasta;
        private string desdeFormato112;
        private string hastaFormato112;
        private RegistroAbastecimientoController modelo;
        private List<ListadoAcopioByTiktesResult> listadoRegistroDeAcopio;
        private ListadoAcopioByTiktesResult oRegistro;
        private string fileName;
        private bool exportVisualSettings;

        public ImpresionTicketsAbastecimientoMateriaPrima()
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

        public void Inicio()
        {
            try
            {

               
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJAS"].ToString();
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


        public MesController MesesNeg { get; private set; }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
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


        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        protected override void OnLoad(EventArgs e)
        {


            this.dgvRegistros.TableElement.BeginUpdate();            
            this.LoadFreightSummary();            
            this.dgvRegistros.TableElement.EndUpdate();
            base.OnLoad(e);
            
        }

        private void LoadFreightSummary()
        {
            dgvRegistros.MasterTemplate.AutoExpandGroups = true;
            dgvRegistros.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            dgvRegistros.GroupDescriptors.Clear();
            this.dgvRegistros.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chDESCRIPCION", "Count : {0:N0}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chPESOBRUTO", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chPESONETO", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chNROJABAS", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            dgvRegistros.MasterTemplate.SummaryRowsTop.Add(items1);




        }


        private void ImpresionTicketsAbastecimientoMateriaPrima_Load(object sender, EventArgs e)
        {

        }

        private void txtPeriodo_ValueChanged_1(object sender, EventArgs e)
        {
            ObtenerFechasMes();

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new RegistroAbastecimientoController();
                listadoRegistroDeAcopio = new List<ListadoAcopioByTiktesResult>();
                listadoRegistroDeAcopio = modelo.ObtenerListadoRecepcionEntrePeriodos("NSFAJAS", desde, hasta).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistros.DataSource = listadoRegistroDeAcopio.ToDataTable<ListadoAcopioByTiktesResult>();
                dgvRegistros.Refresh();
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA" );
                return;
            }
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            try
            {
                /* Formato de busqueda '20190321', '20191221' */
                //desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd");
                //hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");
                //desdeFormato112 = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                //hastaFormato112 = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
                desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
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

        private void dgvRegistros_DoubleClick(object sender, EventArgs e)
        {

            Editar();
           
        }

        private void Editar()
        {
            if (oRegistro != null)
            {
                if (oRegistro.item != null && oRegistro.IDINGRESOSALIDAACOPIOCAMPO != null)
                {
                    if (oRegistro.item != string.Empty && oRegistro.IDINGRESOSALIDAACOPIOCAMPO != string.Empty)
                    {
                        ImpresionTicketsAbastecimientoMateriaPrimaDetalleImpresion ofrm = new ImpresionTicketsAbastecimientoMateriaPrimaDetalleImpresion(oRegistro);
                        ofrm.ShowDialog();
                    }
                }
            }
        }

        private void dgvRegistros_SelectionChanged(object sender, EventArgs e)
        {
            oRegistro = new ListadoAcopioByTiktesResult();
            if (dgvRegistros.Rows.Count > 0)
            {
                if (dgvRegistros.CurrentRow != null && dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value != null)
                {
                    try
                    {
                        string codigo = dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value != null ? dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value.ToString() : string.Empty;
                        string item = dgvRegistros.CurrentRow.Cells["chItem"].Value != null ? dgvRegistros.CurrentRow.Cells["chItem"].Value.ToString() : string.Empty;
                        var result = listadoRegistroDeAcopio.Where(x => x.IDINGRESOSALIDAACOPIOCAMPO == codigo && x.item == item).ToList();

                        if (result != null && result.ToList().Count == 1)
                        {
                            oRegistro = result.Single();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros != null && dgvRegistros.Rows.Count > 0)
            {
                Exportar(dgvRegistros);
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

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }
    }
}
