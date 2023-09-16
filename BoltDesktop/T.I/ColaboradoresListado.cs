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

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ColaboradoresListado : Form
    {
        private int esAgrupado;
        private List<SAS_ListadoColaboradoresByDispositivo> listado;
        private SAS_DispositivoUsuariosController modelo;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoColaboradoresByDispositivo odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;

        public ColaboradoresListado()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consultar();
        }

        public ColaboradoresListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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
                items1.Add(new GridViewSummaryItem("chapenom", "Count : {0:N2}; ", GridAggregateFunction.Count));
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
                modelo = new SAS_DispositivoUsuariosController();
                listado = new List<SAS_ListadoColaboradoresByDispositivo>();
                listado = modelo.ListadoDeColaboradoresByDispositivo("SAS", esAgrupado);
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

        private void Consultar()
        {
            try
            {
                esAgrupado = 0;
                if (chkMostarAgrupado.Checked == true)
                {
                    esAgrupado = 1;
                }
                gbCabecera.Enabled = !true;
                gbListado.Enabled = !true;
                progressBar.Visible = true;
                bgwHilo.RunWorkerAsync();

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
                dgvListado.DataSource = listado.ToDataTable<SAS_ListadoColaboradoresByDispositivo>();
                dgvListado.Refresh();
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                progressBar.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void ColaboradoresListado_Load(object sender, EventArgs e)
        {

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }


        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                #region 
                odetalleSelecionado = new SAS_ListadoColaboradoresByDispositivo();
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value != null ? dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value.ToString() : string.Empty);
                                var resultado = listado.Where(x => x.idcodigogeneral == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    odetalleSelecionado.idcodigogeneral = codigo;

                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    odetalleSelecionado = resultado.ElementAt(0);
                                    odetalleSelecionado.idcodigogeneral = codigo;

                                }
                                else
                                {
                                    odetalleSelecionado.idcodigogeneral = string.Empty;
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

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            AsociarAreaDeTrabajo();
        }


        private void AsociarAreaDeTrabajo()
        {
            try
            {
                if (odetalleSelecionado.idcodigogeneral != string.Empty)
                {
                    ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(_conection, _user2, _companyId, privilege, odetalleSelecionado);
                    ofrm.Show();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }


        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {
            AsociarAreaDeTrabajo();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
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

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
