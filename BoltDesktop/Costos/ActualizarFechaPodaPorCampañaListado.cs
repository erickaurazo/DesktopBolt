using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asistencia.Datos;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using Asistencia.Negocios;
using MyControlsDataBinding.Extensions;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using Asistencia.Helper;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.Costos
{
    public partial class ActualizarFechaPodaPorCampañaListado : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private CentroDeCostosController Modelo;
        private List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo> Listado;        
        private SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo oDetalle;
        
        private string fileName;
        private bool exportVisualSettings;
        private string añoCampana;
        

        public ActualizarFechaPodaPorCampañaListado()
        {
            InitializeComponent();
            oDetalle = new SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo();
            oDetalle.idConsumidor = string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
           conection = "SAS";
           user2 = new SAS_USUARIOS();
           user2.IdUsuario = "EAURAZO";
           user2.NombreCompleto = "Erick Aurazo";

           companyId = "001";
            this.privilege = new PrivilegesByUser();
            privilege.nuevo = 1;

            Consultar();
        }

        public ActualizarFechaPodaPorCampañaListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            oDetalle = new SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo();
            oDetalle.idConsumidor = string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
           conection = _conection;
           user2 = _user2;
            companyId = _companyId;
           privilege = _privilege;
            Consultar();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            Consultar();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvDetalle.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvDetalle.TableElement.EndUpdate();

            base.OnLoad(e);
        }


        private void LoadFreightSummary()
        {
            this.dgvDetalle.MasterTemplate.AutoExpandGroups = true;
            this.dgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.GroupDescriptors.Clear();
            this.dgvDetalle.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chidConsumidor", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvDetalle.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void Consultar()
        {
            if (ValidacionConsulta() == true)
            {
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
        }

        private bool ValidacionConsulta()
        {
            bool resultado = true;

            añoCampana = this.txtPeriodo.Value.ToString();            

            if (añoCampana == "0" || añoCampana.Trim () == "")
            {
                return false;
            }           

            return resultado;
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Listado = new List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo>();
                Modelo = new CentroDeCostosController();
                Listado = Modelo.ListadoConsumidoresPorCampaña(conection, añoCampana).ToList();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvDetalle.DataSource = Listado.ToDataTable<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo>();
                dgvDetalle.Refresh();
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }

            
        }

        private void ActualizarFechaPodaPorCampañaListado_Load(object sender, EventArgs e)
        {

        }

        private void dgvPedidoServicio_DoubleClick(object sender, EventArgs e)
        {
            ActualizarFechasDePoda();
        }

        private void AbrirFormualrioDeEdicion()
        {
            

            ActualizarFechaPodaPorCampañaEdicion oFron = new ActualizarFechaPodaPorCampañaEdicion(conection, user2, companyId, privilege, oDetalle);
            oFron.MdiParent = ActualizarFechaPodaPorCampañaListado.ActiveForm;
            oFron.WindowState = FormWindowState.Maximized;
            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            oFron.Show();

        }

        private void dgvPedidoServicio_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                oDetalle = new SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo();
                oDetalle.idConsumidor = string.Empty;                

                if (dgvDetalle != null && dgvDetalle.Rows.Count > 0)
                {
                    if (dgvDetalle.CurrentRow != null)
                    {
                        if (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value != null)
                        {
                            //if (dgvRegistro.CurrentRow.Cells["chLineaCelular"].Value.ToString() != string.Empty)
                            //{
                            string codigo = (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value != null ? (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value.ToString()) : string.Empty);
                          
                            var resultado = Listado.Where(x => x.idConsumidor == codigo).ToList();
                            if (resultado.ToList().Count == 1)
                            {
                                oDetalle = resultado.Single();
                               
                            }
                            else if (resultado.ToList().Count > 1)
                            {
                                oDetalle = resultado.ElementAt(0);
                               
                            }
                            
                            //}
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnEditarFechaPoda_Click(object sender, EventArgs e)
        {
            ActualizarFechasDePoda();
        }

        private void ActualizarFechasDePoda()
        {
            if (oDetalle != null)
            {
                if (oDetalle.idConsumidor != null)
                {
                    if (oDetalle.idConsumidor.Trim() != string.Empty)
                    {
                        AbrirFormualrioDeEdicion();
                    }
                }

            }
        }

        private void btnFechaAplicacionCianamida_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar(dgvDetalle);
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
    }
}
