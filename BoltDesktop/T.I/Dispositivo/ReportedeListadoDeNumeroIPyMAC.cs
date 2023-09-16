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
using System.Reflection;
using ComparativoHorasVisualSATNISIRA.T.I;


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ReportedeListadoDeNumeroIPyMAC : Form
    {
        private int esAgrupado;
        private List<SAS_ListadoMacByIpByColaborador> listado;
        private SAS_DispostivoController modelo;
        SAS_DispositivoUsuariosController modeloUsuario;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoMacByIpByColaborador odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;
        private int codigoDispotivo = 0;

        public ReportedeListadoDeNumeroIPyMAC()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consultar();
        }

        public ReportedeListadoDeNumeroIPyMAC(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            Consultar();
        }


        private void Consultar()
        {
            try
            {

                btnMenu.Enabled = !true;
                gbList.Enabled = !true;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();

                base.OnLoad(e);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
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
                items1.Add(new GridViewSummaryItem("chnombres", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new SAS_DispostivoController();
                listado = new List<SAS_ListadoMacByIpByColaborador>();
                listado = modelo.GetListMACs("SAS");
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
                dgvListado.DataSource = listado.ToDataTable<SAS_ListadoMacByIpByColaborador>();
                dgvListado.Refresh();
                btnMenu.Enabled = true;
                gbList.Enabled = true;
                progressBar1.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {

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
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }

                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción ", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        private void ReportedeListadoDeNumeroIPyMAC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            excelExporter.SheetName = "Listado registros";
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

        private void ReportedeListadoDeNumeroIPyMAC_Load(object sender, EventArgs e)
        {

        }

        private void irADispositivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrADispositivo();
        }

        private void IrADispositivo()
        {
            if (codigoDispotivo > 0)
            {
                SAS_ListadoDeDispositivos oDispositivo = new SAS_ListadoDeDispositivos();
                modeloUsuario = new SAS_DispositivoUsuariosController();
                oDispositivo = modeloUsuario.ObtenerDispositivoById("SAS", codigoDispotivo);

                DispositivosEdicion oFron = new DispositivosEdicion("SAS", oDispositivo, _user2, _companyId, privilege);
                //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                oFron.MdiParent = ColaboradoresListado.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();

            }
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            codigoDispotivo = 0;
            btnIrACatalogo.Enabled = false;
            try
            {
                #region 
                odetalleSelecionado = new SAS_ListadoMacByIpByColaborador();
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null ? dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value.ToString() : string.Empty);
                                codigoDispotivo = (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value) : 0);

                                var resultado = listado.Where(x => x.dispositivoCodigo == codigoDispotivo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    odetalleSelecionado.dispositivoCodigo = codigoDispotivo;
                                    if (codigoDispotivo > 0)
                                    {
                                        btnIrACatalogo.Enabled = true;
                                    }

                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    odetalleSelecionado = resultado.ElementAt(0);
                                    odetalleSelecionado.dispositivoCodigo = codigoDispotivo;
                                    if (codigoDispotivo > 0)
                                    {
                                        btnIrACatalogo.Enabled = true;
                                    }

                                }
                                else
                                {
                                    odetalleSelecionado.dispositivoCodigo = 0;                                    
                                    btnIrACatalogo.Enabled = false;
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
    }
}
