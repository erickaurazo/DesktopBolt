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
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.Cosecha
{
    public partial class ReporteLecturasAAbastecimientoALineas : Form
    {
        private int esAgrupado;
        private List<SAS_RegistroAbastecientoALineasDeProceso> listado;
        private SAS_DispostivoController modelo;
        private string conection = "SAS";
        private SAS_USUARIOS user2;
        private string companyId = "001";
        private PrivilegesByUser privilege;
        private SAS_RegistroAbastecientoALineasDeProceso odetalleSelecionado;
        private int codigoDispotivo = 0;
        private string desde;
        private string hasta;
        private SAS_RegistroDeAbastecimientoController model;
        private List<SAS_RegistroAbastecientoALineasDeProceso> listing; //Listado
        private List<SAS_RegistroAbastecientoALineasDeProceso> selectedList; // ListaSelecionada
        private SAS_RegistroAbastecientoALineasDeProceso selectedItem; // Item Selecionado
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private int ticket;
        private int leido;

        public ReporteLecturasAAbastecimientoALineas()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "NSFAJA";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "eaurazo";
            user2.NombreCompleto = "Erick Aurazo";

            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.editar = 1;

            CargarMeses();
            ObtenerFechasIniciales();
            Consultar();
        }

        public ReporteLecturasAAbastecimientoALineas(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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

            if (chkDiaActual.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();

                base.OnLoad(e);
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

        private void LoadFreightSummary()
        {
            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chDocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
                items1.Add(new GridViewSummaryItem("chcantidadRegistrada", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
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

        private void Consultar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                gbList.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                gbCabecera.Enabled = false;
                btnConsultar.Enabled = false;
                progressBar1.Visible = true;
                btnActualizarLista.Enabled = false;
                //desde = this.txtFechaDesde.Text.Trim();
                //hasta = this.txtFechaHasta.Text.Trim();

                if (chkDiaActual.Checked == true)
                {
                    desde = DateTime.Now.ToString("dd/MM/yyyy");
                    hasta = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                    hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
                }


                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void ReporteLecturasAAbastecimientoALineas_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_RegistroDeAbastecimientoController();
                listing = new List<SAS_RegistroAbastecientoALineasDeProceso>();
                listing = model.GetListAll("NSFAJAS", desde, hasta).ToList();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
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
                                this.dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.colorRojoClaro;
                            }
                        }
                        else if (item.Cells["chprioridadId"].Value.ToString() == "2")
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                this.dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Utiles.amarillo3D;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvListado.Columns.Count; i++)
                            {
                                this.dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                                this.dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
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
                            this.dgvListado.Rows[item.Index].Cells[i].Style.CustomizeFill = true;
                            this.dgvListado.Rows[item.Index].Cells[i].Style.DrawFill = true;
                            this.dgvListado.Rows[item.Index].Cells[i].Style.BackColor = Color.White;
                        }

                    }
                }
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listing.ToDataTable<SAS_RegistroAbastecientoALineasDeProceso>();
                dgvListado.Refresh();

                //if (chkResaltarResultados.Checked == true)
                //{
                //    PintarResultados(dgvListado);
                //}

                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;

                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                btnConsultar.Enabled = true;
                btnActualizarLista.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }
            }

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnRegistrarTicket.Enabled = false;
            btnLiberarTicket.Enabled = false;
            selectedItem = new SAS_RegistroAbastecientoALineasDeProceso();

            ticket = 0;
            leido = 0;
            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chidDetalle"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidDetalle"].Value.ToString() != string.Empty)
                        {
                            ticket = (dgvListado.CurrentRow.Cells["chidDetalle"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chidDetalle"].Value) : 0);
                            leido = (dgvListado.CurrentRow.Cells["chlecturaTicket"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chlecturaTicket"].Value) : 0);
                            var coincidencia = listing.Where(x => x.idDetalle.Value == ticket).ToList();
                            if (coincidencia != null)
                            {
                                if (coincidencia.ToList().Count > 0)
                                {
                                    selectedItem = coincidencia.ElementAt(0);
                                }
                            }

                            if (ticket > 0)
                            {
                                if (leido == 0)
                                {
                                    btnRegistrarTicket.Enabled = true;
                                    btnLiberarTicket.Enabled = false;
                                }
                                else
                                {
                                    btnRegistrarTicket.Enabled = false;
                                    btnLiberarTicket.Enabled = true;
                                }
                            }

                        }
                    }
                }
            }
        }

        private void btnRegistrarTicket_Click(object sender, EventArgs e)
        {
            RegistrarTicketALineaDeAbastecimiento ofrm = new RegistrarTicketALineaDeAbastecimiento(conection, user2, companyId, privilege, ticket, selectedItem);
            if (ofrm.ShowDialog() == DialogResult.OK)
            {
                Consultar();
            }



        }

        private void btnLiberarTicket_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.itemDetalle != null)
                {
                    model = new SAS_RegistroDeAbastecimientoController();
                    int resultado = model.ToRemove(conection, selectedItem);
                    Consultar();
                }
            }
        }

        private void chkVisualizacionDelDia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiaActual.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                if (cboMes.SelectedIndex >= 0)
                {
                    globalHelper = new GlobalesHelper();
                    globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
                }
            }
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }
    }
}
