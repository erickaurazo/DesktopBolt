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

namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    public partial class PartesDiariosDeEquipamiento : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_ParteDiariosDeDispositivosController model;
        private List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> listing; //Listado
        private List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> selectedList; // ListaSelecionada
        private SAS_ParteDiariosDeDispositivosAllByPeriodoResult selectedItem; // Item Selecionado
        private int codigoSelecionado;

        public PartesDiariosDeEquipamiento()
        {
            InitializeComponent();
        }

        public PartesDiariosDeEquipamiento(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            btnEditarRegistro.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
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
            Actualizar();
        }


        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();
                base.OnLoad(e);
                this.SetConditions();
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
                items1.Add(new GridViewSummaryItem("chdocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
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


        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;                           
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnAnular.Enabled = false;
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                btnConsultar.Enabled = false;
                progressBar1.Visible = true;
                btnActualizarLista.Enabled = false;
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


        private void btnConsultar_Click(object sender, EventArgs e)
        {

            RealizarConsulta();

           
        }

        private void RealizarConsulta()
        {
            try
            {

                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();

                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
                BarraPrincipal.Enabled = false;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                listing = new List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult>();
                listing = model.ListarPorPeriodo("SAS", desde, hasta).ToList();

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
                dgvListado.DataSource = listing.ToDataTable<SAS_ParteDiariosDeDispositivosAllByPeriodoResult>();
                dgvListado.Refresh();

                if (chkResaltarResultados.Checked == true)
                {
                    SetConditions();
                }
                else
                {
                    SetUnConditions();
                }


                BarraPrincipal.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;
                btnActualizarLista.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void SetConditions()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "AN", "", true);
            c1.RowBackColor = Utiles.colorRojoClaro;
            c1.CellBackColor = Utiles.colorRojoClaro;
            c1.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvListado.Columns["chEstadoCodigo"].ConditionalFormattingObjectList.Add(c1);



        }

        private void SetUnConditions()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "PE", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
            dgvListado.Columns["chEstadoCodigo"].ConditionalFormattingObjectList.Add(c1);
        }


        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnEditarRegistro.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            try
            {
                #region 
                selectedItem = new SAS_ParteDiariosDeDispositivosAllByPeriodoResult();
                selectedItem.Codigo = 0;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvListado.CurrentRow.Cells["chcodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString()) : 0);

                                if (id != 0)
                                {
                                    #region 
                                    var resultado = listing.Where(x => x.Codigo == id).ToList();
                                    if (resultado.ToList().Count == 1)
                                    {
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnular.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                    }
                                    else if (resultado.ToList().Count > 1)
                                    {
                                        selectedItem = resultado.ElementAt(0);
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnular.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                    }
                                    #endregion
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

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }


        private void ExportToExcel()
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void DeleteRecord()
        {
            if (selectedItem != null)
            {
                if (selectedItem.Codigo != null)
                {
                    model = new SAS_ParteDiariosDeDispositivosController();
                    SAS_ParteDiariosDeDispositivos itemAnular = new SAS_ParteDiariosDeDispositivos();
                    itemAnular.Codigo = selectedItem.Codigo;
                    model.DeleteRecord("SAS", itemAnular);
                    Actualizar();
                }
            }
        }


        private void ChangeState()
        {
            if (selectedItem != null)
            {
                if (selectedItem.Codigo != null)
                {
                    model = new SAS_ParteDiariosDeDispositivosController();
                    SAS_ParteDiariosDeDispositivos itemAnular = new SAS_ParteDiariosDeDispositivos();
                    itemAnular.Codigo = selectedItem.Codigo;
                    model.ChangeState("SAS", itemAnular);
                    Actualizar();
                }
            }

        }


        private void Modify()
        {
            try
            {
                if (selectedItem != null)
                {
                    if (selectedItem.Codigo != null)
                    {
                        if (selectedItem.Codigo != 0)
                        {
                            int codigoSelecionado = selectedItem.Codigo;
                            PartesDiariosDeEquipamientoDetalle oFron = new PartesDiariosDeEquipamientoDetalle(conection, user2, companyId, privilege, codigoSelecionado);
                            oFron.MdiParent = PartesDiariosDeEquipamiento.ActiveForm;
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

        private void NewRegister()
        {
            try
            {
                int codigoSelecionado = 0;
                PartesDiariosDeEquipamientoDetalle oFron = new PartesDiariosDeEquipamientoDetalle(conection, user2, companyId, privilege, codigoSelecionado);
                oFron.MdiParent = PartesDiariosDeEquipamiento.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            EliminarDocumento();
        }

        private void EliminarDocumento()
        {
            try
            {
                int codigoSelecionado = selectedItem.Codigo;
                SAS_ParteDiariosDeDispositivos oParteDiario = new SAS_ParteDiariosDeDispositivos();
                oParteDiario.Codigo = codigoSelecionado;
                model = new SAS_ParteDiariosDeDispositivosController();

                model.DeleteRecord(conection, oParteDiario);

                RealizarConsulta();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            Modify();
        }
    }
}
