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
using MyDataGridViewColumns;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class ReporteIngresoSalidaGasificado : Form
    {
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_RegistroGasificadoByDatesResult selectedItem;
        private List<SAS_RegistroGasificadoByDatesResult> result;
        SAS_RegistroGasificadoController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private int resumirListaDelReporte = 1;

        public MesController MesesNeg { get; private set; }

        public ReporteIngresoSalidaGasificado()
        {
            InitializeComponent();
            CargarMeses();
            ObtenerFechasIniciales();
            conection = "NSFAJAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "eaurazo";
            user.NombreCompleto = "Erick Aurazo Carhuatanta";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.consultar = 1;
            privilege.nuevo = 1;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

            progressBar1.Visible = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
            Consult();
        }

        public ReporteIngresoSalidaGasificado(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            CargarMeses();
            ObtenerFechasIniciales();
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            chkDiaActual.Checked = true;
            Consult();
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;


        }

        private void Consult()
        {

            try
            {
                resumirListaDelReporte = 1;
                if (chkAgruparResultados.Checked == false)
                {
                    resumirListaDelReporte = 0;
                }


                if (chkDiaActual.Checked == true)
                {
                    desde = DateTime.Now.ToShortDateString();
                    hasta = DateTime.Now.ToShortDateString();
                }
                else
                {
                    desde = this.txtFechaDesde.Text;
                    hasta = this.txtFechaHasta.Text;
                }

                btnConsultar.Enabled = false;
                gbList.Enabled = false;
                btnBarraPrincipal.Enabled = false;
                progressBar1.Visible = false;
                btnBarraPrincipal.Enabled = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
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


        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJA"].ToString();
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


        protected override void OnLoad(EventArgs e)
        {
            this.dgvRegistro.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvRegistro.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistro.GroupDescriptors.Clear();
            this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chconsumidor", "COUNT : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chNroJabasAGasificar", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chNumeroJabasPorViaje", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);

        }

        private void PintarResultados(RadGridView dgvListado)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    foreach (var item in dgvListado.Rows)
                    {
                        if (item.Cells["chprioridadId"].Value.ToString() == "1")
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.colorRojoClaro;
                            }
                        }
                        else if (item.Cells["chprioridadId"].Value.ToString() == "2")
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.amarillo3D;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
                            }
                        }

                    }
                }
            }
        }

        private void DespintarResultados(RadGridView dgvListado)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    foreach (var item in dgvListado.Rows)
                    {
                        for (int i = 0; i < dgvListado.Columns.Count; i++)
                        {
                            dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                            dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                            dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
                        }

                    }
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


        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = result.ToDataTable<SAS_RegistroGasificadoByDatesResult>();
                dgvRegistro.Refresh();
                btnConsultar.Enabled = true;
                gbList.Enabled = !false;                
                progressBar1.Visible = !true;
                btnBarraPrincipal.Enabled = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                btnConsultar.Enabled = true;
                gbList.Enabled = !false;
                btnBarraPrincipal.Enabled = true;
                progressBar1.Visible = !true;

                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            result = new List<SAS_RegistroGasificadoByDatesResult>();
            model = new SAS_RegistroGasificadoController();

            try
            {
                result = model.GetListRegistroGasificadoByDates(conection, desde, hasta);
                if (resumirListaDelReporte == 1)
                {
                    result = model.SetListRegistroGasificadoAll(result);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ReporteIngresoSalidaGasificado_Load(object sender, EventArgs e)
        {
            
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consult();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvRegistro != null)
            {
                if (dgvRegistro.Rows.Count > 0)
                {
                    Exportar(dgvRegistro);
                }
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

        private void ReporteIngresoSalidaGasificado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void chkDiaActual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiaActual.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

            try
            {

                if (desde != string.Empty && hasta != string.Empty)
                {
                    RegistroDeIngresoSalidaGasificadoVistaPrevia ofrm = new RegistroDeIngresoSalidaGasificadoVistaPrevia(conection, desde, hasta);
                    ofrm.Show();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

            
            

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                chkDiaActual.Checked = false;
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                chkDiaActual.Checked = false;
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }



    }
}
