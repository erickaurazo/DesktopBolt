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
    public partial class AtencionesSoporteFuncional : Form
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
        private SAS_DispositivoSoporteFuncionalController model;
        private List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> listing; //Listado
        private List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> selectedList; // ListaSelecionada
        private SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult selectedItem; // Item Selecionado
        private int codigoSelecionado;

        public AtencionesSoporteFuncional()
        {
            InitializeComponent();
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";

            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.imprimir = 1;
            privilege.editar = 1;
            privilege.consultar = 1;
            privilege.eliminar = 1;
            privilege.anular = 1;
            privilege.exportar = 1;

            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        public AtencionesSoporteFuncional(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
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



        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                listing = new List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult>();
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
                dgvListado.DataSource = listing.ToDataTable<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult>();
                dgvListado.Refresh();

                if (chkResaltarResultados.Checked == true)
                {
                    SetConditions();
                }
                else
                {
                    SetUnConditions();
                }

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



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void NewRegister()
        {
            try
            {
                int codigoSelecionado = 0;
                AtencionesSoporteFuncionalEdicion oFron = new AtencionesSoporteFuncionalEdicion(conection, user2, companyId, privilege, codigoSelecionado);
                oFron.MdiParent = AtencionesSoporteFuncional.ActiveForm;
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


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
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
                        AtencionesSoporteFuncionalEdicion oFron = new AtencionesSoporteFuncionalEdicion(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.MdiParent = AtencionesSoporteFuncional.ActiveForm;
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
                    model = new SAS_DispositivoSoporteFuncionalController();
                    SAS_DispositivoSoporteFuncional itemAnular = new SAS_DispositivoSoporteFuncional();
                    itemAnular.codigo = selectedItem.codigo;
                    model.ChangeState("SAS", itemAnular);
                    Actualizar();
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void DeleteRecord()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    model = new SAS_DispositivoSoporteFuncionalController();
                    SAS_DispositivoSoporteFuncional itemAnular = new SAS_DispositivoSoporteFuncional();
                    itemAnular.codigo = selectedItem.codigo;
                    model.DeleteRecord("SAS", itemAnular);
                    Actualizar();
                }
            }
        }

        private void btnAnularRegistro_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private void btnVerHistorial_Click(object sender, EventArgs e)
        {
            VieHistory();
        }

        private void VieHistory()
        {

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

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            Modify();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnEditarRegistro.Enabled = false;
            btnAnularRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnActivarTarea.Enabled = false;
            btnAsociarAreaDeTrabajo.Enabled = false;
            btnReasignarCaso.Enabled = false;
            btnFinalizarCaso.Enabled = false;
            btnReAperturarCaso.Enabled = false;
            btnArchivarCaso.Enabled = false;
            btnCambiarEstadoDeDocumentoAReprogramado.Enabled = false;
            btnReprogramarCaso.Enabled = false;

            try
            {
                #region 
                selectedItem = new SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult();
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
                                    #region 
                                    var resultado = listing.Where(x => x.codigo == id).ToList();
                                    if (resultado.ToList().Count == 1)
                                    {
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnularRegistro.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                    }
                                    else if (resultado.ToList().Count > 1)
                                    {
                                        selectedItem = resultado.ElementAt(0);
                                        selectedItem = resultado.Single();
                                        btnEditarRegistro.Enabled = true;
                                        btnAnularRegistro.Enabled = true;
                                        btnEliminarRegistro.Enabled = true;
                                    }

                                    if (user2 != null)
                                    {
                                        #region 
                                        if (user2.IdUsuario != null)
                                        {
                                            if (user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                                            {
                                                btnActivarTarea.Enabled = true;
                                                btnAsociarAreaDeTrabajo.Enabled = true;
                                                btnReasignarCaso.Enabled = true;


                                                if (selectedItem.idEstado == "AN" || selectedItem.idEstado == "AT" || selectedItem.idEstado == "CE")
                                                {
                                                    btnReAperturarCaso.Enabled = true;
                                                    btnArchivarCaso.Enabled = false;
                                                    btnFinalizarCaso.Enabled = false;
                                                    btnReprogramarCaso.Enabled = false;
                                                    btnCambiarEstadoDeDocumentoAReprogramado.Enabled = false;
                                                }
                                                else
                                                {
                                                    btnFinalizarCaso.Enabled = true;
                                                    btnArchivarCaso.Enabled = true;
                                                    btnReAperturarCaso.Enabled = false;
                                                    btnReprogramarCaso.Enabled = true;
                                                    btnCambiarEstadoDeDocumentoAReprogramado.Enabled = true;
                                                }
                                            }
                                        }
                                        #endregion
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

        private void AtencionesSoporteFuncional_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnReasignarCaso_Click(object sender, EventArgs e)
        {
            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.ToUpper().Trim() == "EAURAZO")
                    {
                        ReasignarCaso();
                    }
                }
            }

        }

        private void ReasignarCaso()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo != 0)
                    {
                        int codigoSelecionado = selectedItem.codigo;
                        AtencionesSoporteFuncionalEdicionReasignarCaso oFron = new AtencionesSoporteFuncionalEdicionReasignarCaso(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.ShowDialog();
                        if (oFron.DialogResult == DialogResult.OK)
                        {
                            Actualizar();
                        }

                    }
                }
            }
        }

        private void AtencionesSoporteFuncional_Load(object sender, EventArgs e)
        {

        }

        private void btnActivarTarea_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo > 0)
                    {
                        model = new SAS_DispositivoSoporteFuncionalController();
                        int activarTarea = model.ActivarTarea(conection != null ? conection.ToUpper() : "SAS", selectedItem);
                        Actualizar();
                    }
                }
            }


        }

        private void btnVerDatosDelColaborador_Click(object sender, EventArgs e)
        {
            GoToWorkerCatalog();
        }



        private void GoToWorkerCatalog()
        {
            #region Ir a catalogo de colaboradores con el filtro del idcolaborador() 
            if (selectedItem != null)
            {
                if (selectedItem.codigoPersonal != null)
                {
                    if (selectedItem.codigoPersonal.ToString().Trim() != string.Empty)
                    {
                        string codigoColaboradorFiltrado = selectedItem.codigoPersonal.ToString().Trim();
                        ColaboradoresListado ofrm = new ColaboradoresListado(conection, user2, companyId, privilege, codigoColaboradorFiltrado);
                        ofrm.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();
                    }
                }
            }
            #endregion
        }

        private void btnNotificar_Click(object sender, EventArgs e)
        {
            Notify();
        }

        private void Notify()
        {
            try
            {
                #region Notify() 
                if (selectedItem != null)
                {
                    if (selectedItem.codigo != (int?)null)
                    {
                        if (selectedItem.codigo > 0)
                        {
                            codigoSelecionado = Convert.ToInt32(selectedItem.codigo);
                            gbCabecera.Enabled = false;
                            gbListado.Enabled = false;
                            progressBar1.Visible = true;
                            BarraPrincipal.Enabled = false;
                            bgwNotify.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwNotify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                model.Notify(conection, "soporte@saturno.net.pe", "Solicitud de atención por soporte funcional", codigoSelecionado);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwNotify_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Notificación enviada satisfactoriamente", "Confirmación del sistema");
                gbCabecera.Enabled = !false;
                gbListado.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbCabecera.Enabled = !false;
                gbListado.Enabled = !false;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
                return;
            }
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void Preview()
        {

        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            Duplicate();
        }


        private void Duplicate()
        {
            if (selectedItem != null)
            {
                if (selectedItem.codigo != null)
                {
                    if (selectedItem.codigo != 0)
                    {
                        int codigoSelecionado = selectedItem.codigo;
                        AtencionesSoporteFuncionalEdicionDuplicarRegistro oFron = new AtencionesSoporteFuncionalEdicionDuplicarRegistro(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.ShowDialog();
                        if (oFron.DialogResult == DialogResult.OK)
                        {
                            Actualizar();
                        }

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
                    if (selectedItem.codigo != 0)
                    {
                        int codigoSelecionado = selectedItem.codigo;
                        AtencionesSoporteFuncionalEdicionAsignarPrioridad oFron = new AtencionesSoporteFuncionalEdicionAsignarPrioridad(conection, user2, companyId, privilege, codigoSelecionado);
                        oFron.ShowDialog();
                        if (oFron.DialogResult == DialogResult.OK)
                        {
                            Actualizar();
                        }

                    }
                }
            }
        }

        private void chkResaltarResultados_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResaltarResultados.Checked == true)
            {
                if (dgvListado != null)
                {
                    if (dgvListado.Rows.Count > 0)
                    {
                        SetConditions();
                    }
                }
            }
            else
            {
                if (dgvListado != null)
                {
                    if (dgvListado.Rows.Count > 0)
                    {

                        SetUnConditions();
                    }
                }
            }
        }

        private void btnFinalizarCaso_Click(object sender, EventArgs e)
        {
            FinalizarCaso();
        }

        private void FinalizarCaso()
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                int confirmacionDeValidacionCaso = model.FinalizarCaso(conection, selectedItem);
                MessageBox.Show("Caso finalizado satisfactoriamente", "Confirmación del sistema");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnReAperturarCaso_Click(object sender, EventArgs e)
        {
            ReAperturarCaso();
        }

        private void ReAperturarCaso()
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                int confirmacionDeValidacionCaso = model.ReAperturarCaso(conection, selectedItem);
                MessageBox.Show("Caso re-aperturado satisfactoriamente", "Confirmación del sistema");
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
                model = new SAS_DispositivoSoporteFuncionalController();
                int confirmacionDeValidacionCaso = model.ArchivarCaso(conection, selectedItem);
                MessageBox.Show("Caso archivado satisfactoriamente", "Confirmación del sistema | Archivar caso");
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

        private void btnReprogramarCaso_Click(object sender, EventArgs e)
        {
            GenerarReprogramacion(selectedItem.codigo);
        }

        private void GenerarReprogramacion(int codigoSelecionado)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                int codigoDocumento = Convert.ToInt16(codigoSelecionado != null ? codigoSelecionado.ToString() : "0");
                model.CambiarAEstadoReprogramado(conection, codigoDocumento);
                MessageBox.Show("Caso archivado satisfactoriamente", "Confirmación del sistema | Reprogramación caso");
                Actualizar();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + " | Generar repogramación de actividad", "MENSAJE DEL SISTEMA");
            }
        }

        private void btnCambiarEstadoDeDocumentoAReprogramado_Click(object sender, EventArgs e)
        {
            CambiarEstadoDeDocumentoAReprogramado();
        }

        private void CambiarEstadoDeDocumentoAReprogramado()
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                int confirmacionDeValidacionCaso = model.CambiarEstadoDeDocumentoAReprogramado(conection, selectedItem);
                MessageBox.Show("Caso re-aperturado satisfactoriamente ", "Confirmación del sistema | Cambiar estado a reprogramación");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }



    }
}
