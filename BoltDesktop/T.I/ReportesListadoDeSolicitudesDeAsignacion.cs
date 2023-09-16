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
    public partial class ReportesListadoDeSolicitudesDeAsignacion : Form
    {
        private int esAgrupado;
        private GlobalesHelper globalHelper;
        private List<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult> listado;
        private SAS_SolicitudDeEquipamientoTecnologicoController modelo;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;
        private int codigoSolicitud = 0;
        public MesController MesesNeg;
        public string FechaDesdeConsulta;
        public string FechaHastaConsulta;
        private SAS_SolicitudDeEquipamientoTecnologico solicitud;

        public ReportesListadoDeSolicitudesDeAsignacion()
        {
            InitializeComponent();
            CargarMeses();
            ObtenerFechasIniciales();
        }

        public ReportesListadoDeSolicitudesDeAsignacion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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
            CargarMeses();
            ObtenerFechasIniciales();

            Consultar();
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
                items1.Add(new GridViewSummaryItem("chjustificacion", "Count : {0:N2}; ", GridAggregateFunction.Count));
                // chcodigoDispositivo
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void Consultar()
        {
            try
            {

                btnMenu.Enabled = !true;
                gbList.Enabled = !true;
                progressBar1.Visible = true;
                FechaDesdeConsulta = this.txtFechaDesde.Text;
                FechaHastaConsulta = this.txtFechaHasta.Text; 
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void ReportesListadoDeSolicitudesDeAsignacion_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
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
                if (dgvListado.RowCount > 0)
                {
                    Exportar(dgvListado);
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

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            codigoSolicitud = 0;
            btnIrASolicitudEquipamiento.Enabled = false;
            btnAsociarAreaDeTrabajo.Enabled = false;
            try
            {
                #region 
                odetalleSelecionado = new SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult();
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvListado.CurrentRow.Cells["chid"].Value != null ? dgvListado.CurrentRow.Cells["chid"].Value.ToString() : string.Empty);
                                codigoSolicitud = (dgvListado.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chid"].Value) : 0);

                                var resultado = listado.Where(x => x.id == codigoSolicitud).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    odetalleSelecionado.id = codigoSolicitud;
                                    if (codigoSolicitud > 0)
                                    {
                                        btnIrASolicitudEquipamiento.Enabled = true;
                                    }

                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    odetalleSelecionado = resultado.ElementAt(0);
                                    odetalleSelecionado.id = codigoSolicitud;
                                    if (codigoSolicitud > 0)
                                    {
                                        btnIrASolicitudEquipamiento.Enabled = true;
                                    }

                                }
                                else
                                {
                                    odetalleSelecionado.id = 0;
                                    btnIrASolicitudEquipamiento.Enabled = false;
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

        private void ReportesListadoDeSolicitudesDeAsignacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIrASolicitudEquipamiento_Click(object sender, EventArgs e)
        {

            
                if (codigoSolicitud != null)
                {
                    if (codigoSolicitud != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = codigoSolicitud;
                        SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(_conection, _user2, _companyId, privilege, solicitud);
                        ofrm.MdiParent = ReportesListadoDeSolicitudesDeAsignacion.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();

                    }
                }
           

        }

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {

        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                listado = new List<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult>();
                listado = modelo.ListRequestAllByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);
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
                dgvListado.DataSource = listado.ToDataTable<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult>();
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
    }
}
