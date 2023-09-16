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
    public partial class ActualizacionFechaAplicacionCianamida : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private CentroDeCostosController Modelo;
        private List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult> Listado;
        private SAS_ConsumidorFechaAplicacionCListadoPorAnioResult oDetalle;
        ConsumidorFechaAplicacionC oRegister = new ConsumidorFechaAplicacionC();

        private string fileName;
        private bool exportVisualSettings;
        private string añoCampana;

        public ActualizacionFechaAplicacionCianamida()
        {
            InitializeComponent();
            oDetalle = new SAS_ConsumidorFechaAplicacionCListadoPorAnioResult();
            oDetalle.ConsumidorId = string.Empty;
            oDetalle.IdEmpresa = string.Empty;
            oDetalle.SiembraId = string.Empty;
            oDetalle.CampanaAgricolaId = string.Empty;

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

        public ActualizacionFechaAplicacionCianamida(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            oDetalle = new SAS_ConsumidorFechaAplicacionCListadoPorAnioResult();
            oDetalle.ConsumidorId = string.Empty;
            oDetalle.IdEmpresa = string.Empty;
            oDetalle.SiembraId = string.Empty;
            oDetalle.CampanaAgricolaId = string.Empty;

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

        private void ActualizacionFechaAplicacionCianamida_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
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

            if (añoCampana == "0" || añoCampana.Trim() == "")
            {
                return false;
            }

            return resultado;
        }

        private void dgvDetalle_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                oDetalle = new SAS_ConsumidorFechaAplicacionCListadoPorAnioResult();
                oDetalle.ConsumidorId = string.Empty;
                oDetalle.IdEmpresa = string.Empty;
                oDetalle.SiembraId = string.Empty;
                oDetalle.CampanaAgricolaId = string.Empty;

                if (dgvDetalle != null && dgvDetalle.Rows.Count > 0)
                {
                    if (dgvDetalle.CurrentRow != null)
                    {
                        if (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value != null)
                        {
                            //if (dgvRegistro.CurrentRow.Cells["chLineaCelular"].Value.ToString() != string.Empty)
                            //{
                            string codigoConsumidor = (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value != null ? (dgvDetalle.CurrentRow.Cells["chidConsumidor"].Value.ToString()) : string.Empty);
                            string codigoEmpresa = (dgvDetalle.CurrentRow.Cells["chidempresa"].Value != null ? (dgvDetalle.CurrentRow.Cells["chidempresa"].Value.ToString()) : string.Empty);
                            string codigoAnioCampana = (dgvDetalle.CurrentRow.Cells["chidCampana"].Value != null ? (dgvDetalle.CurrentRow.Cells["chidCampana"].Value.ToString()) : string.Empty);
                            string codigoSiembra = (dgvDetalle.CurrentRow.Cells["chidSiembra"].Value != null ? (dgvDetalle.CurrentRow.Cells["chidSiembra"].Value.ToString()) : string.Empty);

                            var resultado = Listado.Where(x => x.ConsumidorId == codigoConsumidor && x.IdEmpresa == codigoEmpresa && x.CampanaAgricolaId == codigoAnioCampana && x.SiembraId == codigoSiembra).ToList();
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

        private void dgvDetalle_DoubleClick(object sender, EventArgs e)
        {
            ActualizarFechasAplicacionDeCinamida();
        }

        private void ActualizarFechasAplicacionDeCinamida()
        {
            if (oDetalle != null)
            {
                if (oDetalle.ConsumidorId != null)
                {
                    if (oDetalle.ConsumidorId.Trim() != string.Empty)
                    {
                        AbrirFormualrioDeEdicion();
                    }
                }

            }
        }

        private void AbrirFormualrioDeEdicion()
        {


            ActualizacionFechaAplicacionCianamidaEdicion oFron = new ActualizacionFechaAplicacionCianamidaEdicion(conection, user2, companyId, privilege, oDetalle);
            oFron.MdiParent = ActualizarFechaPodaPorCampañaListado.ActiveForm;
            oFron.WindowState = FormWindowState.Maximized;
            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            oFron.Show();

        }

        private void ActualizacionFechaAplicacionCianamida_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                Listado = new List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult>();
                Modelo = new CentroDeCostosController();
                Listado = Modelo.ListadoConsumidoresConFechaAplicacionCianamidaPorCampaña(conection, añoCampana).ToList();


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
                dgvDetalle.DataSource = Listado.ToDataTable<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult>();
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

        private void btnFechaAplicacionCianamida_Click(object sender, EventArgs e)
        {
            ActualizarFechasAplicacionDeCinamida();
        }
    }
}
