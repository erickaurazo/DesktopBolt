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
using ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class OrdenDeTrabajoIT : Form
    {
        private PrivilegesByUser privilege;
        private string companyId = "001";
        private string conection = "SAS";
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result = string.Empty;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_DispositivoOrdenTrabajoController model;
        private List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult> listing; //Listado
        private List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult> selectedList; // ListaSelecionada
        private SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem; // Item Selecionado

        public OrdenDeTrabajoIT()
        {
            InitializeComponent();
        }

        public OrdenDeTrabajoIT(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
            Actualizar();
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
                btnActualizar.Enabled = false;
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


        private void OrdenDeTrabajoIT_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void NewRegister()
        {
            int codigoSelecionado = 0;
            OrdenDeTrabajoITEdicion oFron = new OrdenDeTrabajoITEdicion(conection, user2, companyId, privilege, codigoSelecionado);
            oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
            oFron.WindowState = FormWindowState.Maximized;
            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            oFron.Show();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            GetGisted();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void Modify()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo != 0)
                    {
                        int codigoSelecionado = selectedItem.codigo;
                        OrdenDeTrabajoITEdicion oFron = new OrdenDeTrabajoITEdicion(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                        oFron.WindowState = FormWindowState.Maximized;
                        oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        oFron.Show();
                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void ChangeState()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    model = new SAS_DispositivoOrdenTrabajoController();
                    SAS_DispositivoOrdenTrabajo itemAnular = new SAS_DispositivoOrdenTrabajo();
                    itemAnular.codigo = selectedItem.codigo;
                    model.ChangeState("SAS", itemAnular);
                    Actualizar();
                }
            }

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void DeleteRecord()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    model = new SAS_DispositivoOrdenTrabajoController();
                    SAS_DispositivoOrdenTrabajo itemAnular = new SAS_DispositivoOrdenTrabajo();
                    itemAnular.codigo = selectedItem.codigo;
                    model.DeleteRecord("SAS", itemAnular);
                    Actualizar();
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Exportar(dgvListado);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            GetGisted();
        }

        private void GetGisted()
        {
            try
            {
                btnActualizar.Enabled = false;
                btnConsultar.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = false;
                btnAnular.Enabled = false;
                progressBar1.Visible = true;
                desde = this.txtFechaDesde.Text;
                hasta = this.txtFechaHasta.Text;
                gbCabecera.Enabled = false;
                gbListado.Enabled = false;
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

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnActivarCaso.Enabled = false;
            btnAsignarTarea.Enabled = false;
            btnCerrarCaso.Enabled = false;
            btnArchivarCaso.Enabled = false;
            btnReaperturarCaso.Enabled = false;
            btnAsignarEstadoADispositivo.Enabled = false;
            btnCambiarAEstadoReprogramado.Enabled = false;
            btnReprogramarCaso.Enabled = false;


            try
            {
                #region 
                selectedItem = new SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult();
                selectedItem.codigo = 0;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    #region 
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvListado.CurrentRow.Cells["chcodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString()) : 0);
                                int cerrado = ((dgvListado.CurrentRow.Cells["chCerrado"].Value != null || dgvListado.CurrentRow.Cells["chCerrado"].Value.ToString() != string.Empty) ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chCerrado"].Value.ToString().Trim()) : 0);

                                if (id != 0)
                                {
                                    #region 
                                    var resultado = listing.Where(x => x.codigo == id).ToList();
                                    if (resultado.ToList().Count == 1)
                                    {
                                        selectedItem = resultado.Single();
                                    }
                                    else if (resultado.ToList().Count > 1)
                                    {
                                        selectedItem = resultado.ElementAt(0);
                                        selectedItem = resultado.Single();
                                    }

                                    if (user2 != null)
                                    {
                                        #region Si el usuario es super admin
                                        if (user2.IdUsuario != null)
                                        {
                                            if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                                            {
                                                btnActivarCaso.Enabled = true;
                                                btnAsignarTarea.Enabled = true;
                                                if (selectedItem.idEstado == "AN" || selectedItem.idEstado == "AT" || selectedItem.idEstado == "CE")
                                                {
                                                    btnReaperturarCaso.Enabled = true;
                                                    btnCerrarCaso.Enabled = false;
                                                    btnArchivarCaso.Enabled = false;
                                                    btnCambiarAEstadoReprogramado.Enabled = false;
                                                    btnReprogramarCaso.Enabled = false;
                                                }
                                                else
                                                {
                                                    btnReaperturarCaso.Enabled = false;
                                                    btnCerrarCaso.Enabled = true;
                                                    btnArchivarCaso.Enabled = true;
                                                    btnCambiarAEstadoReprogramado.Enabled = true;
                                                    btnReprogramarCaso.Enabled = true;

                                                }
                                            }
                                        }
                                        #endregion
                                    }

                                    if (cerrado == 0)
                                    {
                                        if ((user2.IdUsuario.Trim() == "EAURAZO") || (user2.IdUsuario.Trim() == "FCERNA") || (user2.IdUsuario.Trim() == "FCERNA"))
                                        {
                                            btnAsignarEstadoADispositivo.Enabled = true;
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                listing = new List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult>();
                listing = model.GetListByDate("SAS", desde, hasta).ToList();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        private void SetConditions()
        {             //add a couple of sample formatting objects             
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "1", "", true);
            c1.RowBackColor = Utiles.colorVerdeNivel01;
            c1.CellBackColor = Utiles.colorVerdeNivel01;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "2", "", true);
            c2.RowBackColor = Utiles.colorRojoClaro;
            c2.CellBackColor = Utiles.colorRojoClaro;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c2);


            ConditionalFormattingObject c3 = new ConditionalFormattingObject("White,  applied to entire row", ConditionTypes.Equal, "3", "", true);
            c3.RowBackColor = Color.White;
            c3.CellBackColor = Color.White;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c3);

            //update the grid view for the conditional formatting to take effect             
            //radGridView1.TableElement.Update(false);         
        }

        private void SetUnConditions()
        {             //add a couple of sample formatting objects             
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "1", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "2", "", true);
            c2.RowBackColor = Color.White;
            c2.CellBackColor = Color.White;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c2);


            ConditionalFormattingObject c3 = new ConditionalFormattingObject("White,  applied to entire row", ConditionTypes.Equal, "3", "", true);
            c3.RowBackColor = Color.White;
            c3.CellBackColor = Color.White;
            dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c3);

            //update the grid view for the conditional formatting to take effect             
            //radGridView1.TableElement.Update(false);         
        }


        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (dgvListado.Rows.Count > 0)
                {
                    // DespintarResultados(dgvListado);
                }

                dgvListado.DataSource = listing.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult>();
                dgvListado.Refresh();

                if (dgvListado != null)
                {
                    if (dgvListado.RowCount > 0)
                    {
                        if (chkResaltarResultados.Checked == true)
                        {
                            //PintarResultados(dgvListado);
                        }
                        else
                        {
                            //  DespintarResultados(dgvListado);
                        }
                    }
                }

                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                progressBar1.Visible = false;
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                btnConsultar.Enabled = true;
                btnActualizar.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void OrdenDeTrabajoIT_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void btnAnularRegistro_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void btnEliminarRegistro_Click_1(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void btnVerDispositivo_Click(object sender, EventArgs e)
        {
            GoToDevice();
        }

        private void GoToDevice()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.idDispositivo != null)
                    {
                        GoToDeviceCatalog(selectedItem.idDispositivo);
                    }
                }
            }
        }


        private void GoToDeviceCatalog(int codigoDeDispositivo)
        {

            #region Ir a catálogo de dispositivos            
            DispositivosEdicion oFron = new DispositivosEdicion("SAS", codigoDeDispositivo, user2, companyId, privilege);
            //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
            oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
            oFron.WindowState = FormWindowState.Maximized;
            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            oFron.Show();
            #endregion
        }

        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            GetHistory();
        }

        private void GetHistory()
        {

        }

        private void btnAsignarTarea_Click(object sender, EventArgs e)
        {
            AsignarTarea();
        }

        private void AsignarTarea()
        {
            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO")
                    {
                        if (selectedItem != null)
                        {
                            if (selectedItem.codigo != null)
                            {
                                int codigoSelecionado = selectedItem.codigo;
                                OrdenDeTrabajoITEdicionAsignarSoporte oForm = new OrdenDeTrabajoITEdicionAsignarSoporte(conection, user2, companyId, privilege, codigoSelecionado);
                                //oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                                //oFron.WindowState = FormWindowState.Normal;
                                //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                                //oFron.Show();
                                oForm.ShowDialog();
                                if (oForm.DialogResult == DialogResult.OK)
                                {
                                    Actualizar();
                                }
                            }
                        }
                    }
                }
            }

        }

        private void btnActivarTarea_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo > 0)
                    {
                        model = new SAS_DispositivoOrdenTrabajoController();
                        int activarTarea = model.ActivarTarea(conection != null ? conection.ToUpper() : "SAS", selectedItem);
                        GetGisted();
                    }
                }
            }



        }

        private void btnAsignarPrioridad_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    int codigoSelecionado = selectedItem.codigo;
                    OrdenDeTrabajoITEdicionAsignarPrioridad oForm = new OrdenDeTrabajoITEdicionAsignarPrioridad(conection, user2, companyId, privilege, codigoSelecionado);
                    //oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                    //oFron.WindowState = FormWindowState.Normal;
                    //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    //oFron.Show();
                    oForm.ShowDialog();
                    if (oForm.DialogResult == DialogResult.OK)
                    {
                        Actualizar();
                    }


                }
            }
        }

        private void btnDuplicarDocumento_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    int codigoSelecionado = selectedItem.codigo;
                    OrdenDeTrabajoITEdicionDuplicarDocumento oForm = new OrdenDeTrabajoITEdicionDuplicarDocumento(conection, user2, companyId, privilege, codigoSelecionado);
                    //oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                    //oFron.WindowState = FormWindowState.Normal;
                    //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    //oFron.Show();
                    oForm.ShowDialog();
                    if (oForm.DialogResult == DialogResult.OK)
                    {
                        Actualizar();
                    }
                }
            }
        }

        private void btnAgregarComentarios_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    int codigoSelecionado = selectedItem.codigo;
                    OrdenDeTrabajoITEdicionAgregarComentarios oForm = new OrdenDeTrabajoITEdicionAgregarComentarios(conection, user2, companyId, privilege, codigoSelecionado);
                    //oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                    //oFron.WindowState = FormWindowState.Normal;
                    //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    //oFron.Show();
                    oForm.ShowDialog();
                    if (oForm.DialogResult == DialogResult.OK)
                    {
                        Actualizar();
                    }
                }
            }
        }

        private void chkResaltarResultados_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.RowCount > 0)
                {
                    if (chkResaltarResultados.Checked == true)
                    {
                        SetConditions();
                    }
                    else
                    {
                        SetUnConditions();
                    }
                }
            }
        }

        private void btnCerrarCaso_Click(object sender, EventArgs e)
        {
            FinalizarCaso();
        }

        private void FinalizarCaso()
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                int confirmacionDeValidacionCaso = model.FinalizarCaso(conection != null ? conection.Trim() : "SAS", selectedItem);
                MessageBox.Show("Caso finalizado satisfactoriamente", "Confirmación del sistema");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnArchivarCaso_Click(object sender, EventArgs e)
        {
            ArchivarCaso();
        }

        private void ArchivarCaso()
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                int confirmacionDeValidacionCaso = model.ArchivarCaso(conection != null ? conection.Trim() : "SAS", selectedItem);
                MessageBox.Show("Caso archivado satisfactoriamente", "Confirmación del sistema");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnReaperturarCaso_Click(object sender, EventArgs e)
        {
            ReaperturarCaso();
        }

        private void ReaperturarCaso()
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                int confirmacionDeValidacionCaso = model.ReAperturarCaso(conection != null ? conection.Trim() : "SAS", selectedItem);
                MessageBox.Show("Caso re-aperturado satisfactoriamente", "Confirmación del sistema");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {
            if ((selectedItem.codigoPersonal != null ? selectedItem.codigoPersonal.Trim() : string.Empty) != string.Empty)
            {
                AssociateWorkerToWorkArea();
            }
        }

        private void AssociateWorkerToWorkArea()
        {
            #region Asociar Trabajador A Area de Trabajo() 
            ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(conection, user2, companyId, privilege, selectedItem.codigoPersonal != null ? selectedItem.codigoPersonal.Trim() : string.Empty);
            ofrm.Show();
            #endregion
        }

        private void btnAsignarEstadoADispositivo_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    int codigoSelecionado = selectedItem.codigo;
                    OrdenDeTrabajoITEdicionActualizarEstadoADispositivo oForm = new OrdenDeTrabajoITEdicionActualizarEstadoADispositivo(conection, user2, companyId, privilege, codigoSelecionado);
                    //oFron.MdiParent = OrdenDeTrabajoIT.ActiveForm;
                    //oFron.WindowState = FormWindowState.Normal;
                    //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    //oFron.Show();
                    oForm.ShowDialog();
                    if (oForm.DialogResult == DialogResult.OK)
                    {
                        //DespintarResultados(dgvListado);
                        Actualizar();
                    }
                }
            }
        }

        private void btnReprogramarCaso_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    GenerarReprogramacion(selectedItem.codigo != null ? selectedItem.codigo : 0);
                }
            }
        }


        private void GenerarReprogramacion(int codigo)
        {
            try
            {

                model = new SAS_DispositivoOrdenTrabajoController();
                int codigoDocumento = Convert.ToInt16(codigo.ToString() != string.Empty ? codigo.ToString() : "0");
                model.CambiarAEstadoReprogramado(conection, codigoDocumento);
                Actualizar();
            }

            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + " | Generar repogramación de actividad", "MENSAJE DEL SISTEMA");
            }
        }

        private void btnCambiarAEstadoReprogramado_Click(object sender, EventArgs e)
        {
            

            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    CambiarAEstadoReprogramado();
                }
            }
        }


        private void CambiarAEstadoReprogramado()
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                int confirmacionDeValidacionCaso = model.CambiarAEstadoReprogramado(conection != null ? conection.Trim() : "SAS", selectedItem);
                MessageBox.Show("Caso cambiado su estado del documento a Re-Programado satisfactoriamente", "Confirmación del sistema");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

    }
}
