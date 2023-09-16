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


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ReportesListadoActividadesSoporteTecnico : Form
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


        public ReportesListadoActividadesSoporteTecnico()
        {
            InitializeComponent();
        }

        public ReportesListadoActividadesSoporteTecnico(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
                gbList.Enabled = false;
                gbListado.Enabled = false;
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
                items1.Add(new GridViewSummaryItem("chobservacion", "Count : {0:N2}; ", GridAggregateFunction.Count));
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

        

        private void ReportesListadoActividadesSoporteTecnico_Load(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            GetGisted();
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

       
        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listing.ToDataTable<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult>();
                dgvListado.Refresh();
               
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                progressBar1.Visible = false;
                gbList.Enabled = true;
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

        private void btnActualizar_Click(object sender, EventArgs e)
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


        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvListado.RowCount > 0)
            {
                Exportar(dgvListado);
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

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnActivarTarea.Enabled = false;
            btnAsignarTarea.Enabled = false;
            try
            {
                #region 
                selectedItem = new SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult();
                selectedItem.codigo = 0;
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
                                        if (user2.IdUsuario != null)
                                        {
                                            if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO")
                                            {
                                                btnActivarTarea.Enabled = true;
                                                btnAsignarTarea.Enabled = true;
                                            }
                                        }
                                    }


                                }
                            }
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

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Modify();
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
                                    GetGisted();
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

        private void ReportesListadoActividadesSoporteTecnico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

        private void btnAsignarLaPrioridad_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo > 0)
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
                            GetGisted();
                        }
                    }
                }
            }
        }

        private void btnAgregarLosComentarios_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo > 0)
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
                            GetGisted();
                        }
                    }
                }
            }
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo > 0)
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
                            GetGisted();
                        }
                    }
                }
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

    }
}
