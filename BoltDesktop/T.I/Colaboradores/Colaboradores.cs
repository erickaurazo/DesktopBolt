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
using Telerik.WinControls.Data;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ColaboradoresListado : Form
    {
        private int filtrarPersonalActivo = 0;
        private List<SAS_ListadoColaboradoresByDispositivo> listado, listadoFiltro;
        private SAS_DispositivoUsuariosController modelo;
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId, codigoEmpleadoFiltrado = string.Empty;
        private PrivilegesByUser privilege;
        private SAS_ListadoColaboradoresByDispositivo odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;
        private int codigoDispotivo, numeroDeDispositivosPorColaborador = 0;
        private string correoCorporativo;
        private string lineaCorporativa;

        public ColaboradoresListado()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO";

            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;

            Consultar();
        }

        public ColaboradoresListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
            Consultar();
        }


        public ColaboradoresListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, string _codigoEmpleadoFiltrado)
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
            codigoEmpleadoFiltrado = _codigoEmpleadoFiltrado;
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
                listadoFiltro = new List<SAS_ListadoColaboradoresByDispositivo>();
                //if (_codigoEmpleadoFiltrado != string.Empty)
                //{
                //    listado = modelo.ListadoDeColaboradoresByDispositivo("SAS", esAgrupado, 0);
                //}
                //else
                //{
                //    listado = modelo.ListadoDeColaboradoresByDispositivo("SAS", esAgrupado);
                //}
                listado = modelo.ListadoDeColaboradoresByDispositivo("SAS",0);
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

                if (chkMostarActivos.Checked == true)
                {
                    filtrarPersonalActivo = 1;
                }
                else
                {
                    filtrarPersonalActivo = 0;
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
                if (filtrarPersonalActivo == 1)
                {
                    listadoFiltro = listado.Where(x=> x.estado.ToUpper().Trim() == "ACTIVO").ToList();                   
                }
                else
                {
                    listadoFiltro = listado;
                }

                dgvListado.DataSource = listadoFiltro.ToDataTable<SAS_ListadoColaboradoresByDispositivo>();
                dgvListado.Refresh();

                if (codigoEmpleadoFiltrado != string.Empty)
                {
                    FilterDescriptor filter = new FilterDescriptor();
                    filter.PropertyName = "chIdCodigoGeneral";
                    filter.Operator = FilterOperator.Contains;
                    filter.Value = codigoEmpleadoFiltrado;
                    filter.IsFilterEditor = true;
                    this.dgvListado.FilterDescriptors.Add(filter);
                    dgvListado.Refresh();
                }

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
            codigoDispotivo = 0;
            btnDetalleDispositivosPorColaborador.Enabled = false;
            btnLineaCorporativa.Enabled = false;
            btnCorreoCorporativo.Enabled = false;
            odetalleSelecionado = new SAS_ListadoColaboradoresByDispositivo();
            try
            {
                #region 
                
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value != null ? dgvListado.CurrentRow.Cells["chidcodigogeneral"].Value.ToString() : string.Empty);
                                codigoDispotivo = (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value) : 0);
                                lineaCorporativa = (dgvListado.CurrentRow.Cells["chlineaCorporativo"].Value != null ? dgvListado.CurrentRow.Cells["chlineaCorporativo"].Value.ToString() : string.Empty);
                                correoCorporativo = (dgvListado.CurrentRow.Cells["chcorrecorporativo"].Value != null ? dgvListado.CurrentRow.Cells["chcorrecorporativo"].Value.ToString() : string.Empty);
                                numeroDeDispositivosPorColaborador = (dgvListado.CurrentRow.Cells["chCantidadDispositivos"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chCantidadDispositivos"].Value) : 0);                                
                                var resultado = listado.Where(x => x.idcodigogeneral == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    //odetalleSelecionado.idcodigogeneral = codigo;
                                    //odetalleSelecionado.lineaCorporativo = lineaCorporativa;
                                    if (lineaCorporativa != string.Empty)
                                    {
                                        btnLineaCorporativa.Enabled = true;
                                    }
                                    if (correoCorporativo != string.Empty)
                                    {
                                        btnCorreoCorporativo.Enabled = true;
                                    }
                                    if (numeroDeDispositivosPorColaborador > 0)
                                    {
                                        btnDetalleDispositivosPorColaborador.Enabled = true;
                                    }
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    var resultado2 = resultado.Where(x => x.correcorporativo == correoCorporativo && x.lineaCorporativo == lineaCorporativa).ToList();

                                    if (resultado2.ToList().Count == 1)
                                    {
                                        odetalleSelecionado = resultado2.Single();
                                        //odetalleSelecionado.idcodigogeneral = codigo;
                                        //odetalleSelecionado.lineaCorporativo = lineaCorporativa;
                                        if (lineaCorporativa != string.Empty)
                                        {
                                            btnLineaCorporativa.Enabled = true;
                                        }
                                        if (correoCorporativo != string.Empty)
                                        {
                                            btnCorreoCorporativo.Enabled = true;
                                        }
                                        if (numeroDeDispositivosPorColaborador > 0)
                                        {
                                            btnDetalleDispositivosPorColaborador.Enabled = true;
                                        }
                                    }
                                }
                                else
                                {
                                    odetalleSelecionado.idcodigogeneral = string.Empty;
                                    odetalleSelecionado.dispositivoCodigo = 0;
                                    odetalleSelecionado.lineaCorporativo = string.Empty;
                                    btnLineaCorporativa.Enabled = false;
                                    btnDetalleDispositivosPorColaborador.Enabled = false;
                                    btnCorreoCorporativo.Enabled = false;
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
                    ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(conection, user2, companyId, privilege, odetalleSelecionado);
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

        private void btnIrACatalogo_Click(object sender, EventArgs e)
        {

            IrADispositivo();

        }

        private void IrADispositivo()
        {
            if (codigoDispotivo > 0)
            {
                SAS_ListadoDeDispositivos oDispositivo = new SAS_ListadoDeDispositivos();
                modelo = new SAS_DispositivoUsuariosController();
                oDispositivo = modelo.ObtenerDispositivoById("SAS", codigoDispotivo);

                DispositivosEdicion oFron = new DispositivosEdicion("SAS", oDispositivo, user2, companyId, privilege);
                //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                oFron.MdiParent = ColaboradoresListado.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();

            }
        }

        private void ColaboradoresListado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkMostarActivos_CheckedChanged(object sender, EventArgs e)
        {
            listadoFiltro = new List<SAS_ListadoColaboradoresByDispositivo>();
            if (chkMostarActivos.Checked == true)
            {
                listadoFiltro = listado.Where(x => x.estado.ToUpper().Trim() == "ACTIVO").ToList();
            }
            else
            {
                listadoFiltro = listado;
            }

            dgvListado.DataSource = listadoFiltro.ToDataTable<SAS_ListadoColaboradoresByDispositivo>();
            dgvListado.Refresh();
        }

        private void verCorreoCorporativoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleDispositivosPorColaborador_Click(object sender, EventArgs e)
        {
            if (odetalleSelecionado != null)
            {
                if (odetalleSelecionado.idcodigogeneral != null)
                {
                    if (odetalleSelecionado.idcodigogeneral.Trim() != string.Empty)
                    {
                        ColaboradorDetalleDeDispositivosAsignados ofrm = new ColaboradorDetalleDeDispositivosAsignados(conection, user2, companyId, privilege, odetalleSelecionado);
                        //ofrm.ShowDialog();
                        ofrm.MdiParent = ColaboradoresListado.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();

                    }
                }
            }
        }

        private void verLíneaCorporativaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
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
    }
}
