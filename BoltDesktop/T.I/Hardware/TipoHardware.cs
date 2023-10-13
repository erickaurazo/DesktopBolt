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
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class TipoHardware : Form
    {
        private List<SAS_DispositivoTipoDispositivoListadoResult> listado;
        private SAS_DispositivoTipoDispositivoController Modelo;
        private SAS_DispositivoTipoDispositivoListadoResult registro;
        private SAS_DispositivoTipoDispositivo oTipoDispositivo;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private string fileName;
        private bool exportVisualSettings;

        public TipoHardware()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Actualizar();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvListado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();

            base.OnLoad(e);
            this.SetConditions();
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        public TipoHardware(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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
            Actualizar();
        }

        private void TipoDeDispositivos_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();

            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;
                progressBar1.Visible = false;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_DispositivoTipoDispositivoListadoResult>();
                Modelo =new SAS_DispositivoTipoDispositivoController();
                listado = Modelo.GetLIstadoTipoDispositivo("SAS");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.OrderBy(x=> x.descripcion).ToList().ToDataTable<SAS_DispositivoTipoDispositivoListadoResult>();
                dgvListado.Refresh();

                if (chkResaltarResultados.Checked == true)
                {
                    SetConditions();
                }
                else
                {
                    SetUnConditions();
                }

                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            registro = new SAS_DispositivoTipoDispositivoListadoResult();
            registro.id = string.Empty;

            Limpiar();


            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chid"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                        {
                            string codigo = dgvListado.CurrentRow.Cells["chid"].Value.ToString();
                            var resultQuery = listado.Where(x => x.id == codigo).ToList();

                            if (resultQuery != null)
                            {
                                if (resultQuery.ToList().Count == 1)
                                {
                                    registro = resultQuery.Single();

                                    this.txtCodigo.Text = registro.id.ToString();
                                    this.txtIdEstado.Text = registro.estado.ToString();
                                    this.txtEstado.Text = registro.estadoDescripcion.Trim();
                                    this.txtAbreviatura.Text = registro.nombreCorto.Trim();
                                    this.txtDescripcion.Text = registro.descripcion.Trim();
                                    this.txtObservacion.Text = registro.observaciones != null ? registro.observaciones.Trim() : string.Empty;
                                    this.chkGeneraParteDiario.Checked = !true;
                                    this.chkPresenteEnSolicitud.Checked = !true;
                                    if (registro.enFormatoSolicitud == 1)
                                    {
                                        this.chkPresenteEnSolicitud.Checked = true;
                                    }                                   

                                    if (registro.GenerarParteDiario == 1)
                                    {
                                        this.chkGeneraParteDiario.Checked = true;
                                    }                                    
                                }
                            }

                        }
                    }
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void Grabar()
        {
            try
            {

                oTipoDispositivo = new SAS_DispositivoTipoDispositivo();
                Modelo = new SAS_DispositivoTipoDispositivoController();

                oTipoDispositivo = ObtenerObjeto();

                if (oTipoDispositivo != null)
                {
                    #region Registrar() 
                    int resultado = Modelo.Register("SAS", oTipoDispositivo);

                    btnGrabar.Enabled = !false;
                    btnCancelar.Enabled = !false;

                    if (resultado == 0)
                    {
                        MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se registró satisfactoriamente", "Confirmación del sistema");
                    }
                    else if (resultado == 1)
                    {
                        MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se actualizó satisfactoriamente", "Confirmación del sistema");
                    }

                    Actualizar();
                    btnGrabar.Enabled = false;
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnEditar.Enabled = true;
                    btnCancelar.Enabled = true;
                    #endregion
                }                               
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Proceso de registro de documento");
                return;
            }
        }

        private SAS_DispositivoTipoDispositivo ObtenerObjeto()
        {
            SAS_DispositivoTipoDispositivo item = new SAS_DispositivoTipoDispositivo();
            try
            {
                item.id = this.txtCodigo.Text.Trim();
                item.descripcion = this.txtDescripcion.Text.Trim();
                item.nombreCorto = this.txtAbreviatura.Text.Trim();
                item.estado = this.txtIdEstado.Text != string.Empty ? Convert.ToByte(this.txtIdEstado.Text.Trim()) : Convert.ToByte("0");
                item.observaciones = this.txtObservacion.Text.Trim();
                item.enFormatoSolicitud = chkPresenteEnSolicitud.Checked == true ? 1 : 0;
                item.GenerarParteDiario = chkGeneraParteDiario.Checked == true ? 1 : 0;

                return item;
            }
            catch (Exception Ex)
            {
                
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA | Obtener tipo de dispositivo");
                return null;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            oTipoDispositivo = new SAS_DispositivoTipoDispositivo();
            // Asignar codigo y colocar el foco en la descripcion
            int nuevoCodigo = listado != null ? Convert.ToInt32(listado.Max(x => x.id)) + 1 : 1;
            txtCodigo.Text = nuevoCodigo.ToString().PadLeft(3, '0');
            txtIdEstado.Text = "1";
            txtEstado.Text = "ACTIVO";
            this.txtDescripcion.Focus();
            Cancelar();
        }

        private void Limpiar()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtIdEstado.Text = string.Empty;
            this.txtEstado.Text = "ANULADO";
            this.txtAbreviatura.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;

            chkGeneraParteDiario.Checked = false;
            chkPresenteEnSolicitud.Checked = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void CambiarEstado()
        {
            try
            {
                ObtenerObjeto();
                Modelo =new SAS_DispositivoTipoDispositivoController();
                int resultado = Modelo.ChangeState("SAS", oTipoDispositivo);
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cancelar();
            btnCancelar.Enabled = true;
        }


        private void Cancelar()
        {
            btnGrabar.Enabled = !false;
            gbEdit.Enabled = !false;
            gbList.Enabled = !true;
            btnEditar.Enabled = !true;
            btnCancelar.Enabled = !true;
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }

                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
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

        private void TipoDeDispositivos_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnElegirColummnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {

        }

        private void btnActivar_Click(object sender, EventArgs e)
        {

        }

        private void btnVisualizarColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }


        private void SetConditions()
        {             //add a couple of sample formatting objects             
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Red, applied to entire row", ConditionTypes.Equal, "0", "", true);
            c1.RowBackColor = Utiles.colorRojoClaro;
            c1.CellBackColor = Utiles.colorRojoClaro;
            c1.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvListado.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);

            //ConditionalFormattingObject c2 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "2", "", true);
            //c2.RowBackColor = Utiles.colorRojoClaro;
            //c2.CellBackColor = Utiles.colorRojoClaro;
            //dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c2);


            //ConditionalFormattingObject c3 = new ConditionalFormattingObject("White,  applied to entire row", ConditionTypes.Equal, "3", "", true);
            //c3.RowBackColor = Color.White;
            //c3.CellBackColor = Color.White;
            //dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c3);

            //update the grid view for the conditional formatting to take effect             
            //radGridView1.TableElement.Update(false);         
        }

        private void SetUnConditions()
        {             //add a couple of sample formatting objects             
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "1", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular); 
            dgvListado.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);

            //ConditionalFormattingObject c2 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "2", "", true);
            //c2.RowBackColor = Color.White;
            //c2.CellBackColor = Color.White;
            //dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c2);


            //ConditionalFormattingObject c3 = new ConditionalFormattingObject("White,  applied to entire row", ConditionTypes.Equal, "3", "", true);
            //c3.RowBackColor = Color.White;
            //c3.CellBackColor = Color.White;
            //dgvListado.Columns["chprioridadId"].ConditionalFormattingObjectList.Add(c3);

            //update the grid view for the conditional formatting to take effect             
            //radGridView1.TableElement.Update(false);         
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
    }
}
