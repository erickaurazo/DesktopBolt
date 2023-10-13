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

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class Gerencias : Form
    {
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_ColaboradorGerencia> listado;
        private SAS_ColaboradorGerenciaController Modelo;
        private SAS_ColaboradorGerencia odetalle;
        private SAS_ColaboradorGerencia odetalleSelecionado;

        public Gerencias()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Actualizar();
        }


        public Gerencias(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();


        }


        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();

        }

        [STAThread]
        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar();

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

        private void Gerencias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                odetalleSelecionado = new SAS_ColaboradorGerencia();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvRegistro.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chid"].Value.ToString()) : 0);
                                var resultado = listado.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    odetalleSelecionado.id = codigo;
                                    AsingarObjeto(odetalleSelecionado);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    odetalleSelecionado = resultado.ElementAt(0);
                                    odetalleSelecionado.id = codigo;
                                    AsingarObjeto(odetalleSelecionado);
                                }
                                else
                                {
                                    Limpiar();
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

        private void AsingarObjeto(SAS_ColaboradorGerencia oDetalle)
        {
            try
            {

                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : "0";
                this.txtDescripcion.Text = oDetalle.descripcion != null ? oDetalle.descripcion.Trim() : string.Empty;
                this.txtEstado.Text = oDetalle.estado != null ? (oDetalle.estado == 1 ? "ACTIVO" : "ANULADO") : "ANULADO";
                this.txtIdEstado.Text = oDetalle.estado != null ? oDetalle.estado.ToString().Trim() : "0";
                //this.cboCategoria.SelectedValue = oDetalle.tipoSoftware != null ? oDetalle.tipoSoftware.ToString().Trim() : "000";
                this.txtAbreviatura.Text = oDetalle.abreviatura != null ? oDetalle.abreviatura.Trim() : string.Empty;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }


        #region Métodos() 

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
            items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void Grabar()
        {
            #region Registrar() 
            try
            {
                ObtenerObjeto();
                Modelo =new SAS_ColaboradorGerenciaController();
                int resultado = Modelo.Register("SAS", odetalle);
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
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion

        }

        private void Cancelar()
        {

            btnGrabar.Enabled = !false;
            gbEdit.Enabled = !false;
            gbList.Enabled = !true;
            btnEditar.Enabled = !true;
            btnCancelar.Enabled = !true;
        }

        private void Exportar()
        {
            if (dgvRegistro != null)
            {
                if (dgvRegistro.Rows.Count > 0)
                {
                    Exportar(dgvRegistro);
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


        private void Eliminar()
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void Editar()
        {
            Cancelar();
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

        private void Nuevo()
        {
            try
            {
                odetalle = new SAS_ColaboradorGerencia();
                odetalle.id = 0;
                odetalle.descripcion = string.Empty;
                odetalle.abreviatura = string.Empty;
                odetalle.estado = 0;

                odetalleSelecionado = new SAS_ColaboradorGerencia();
                odetalleSelecionado.id = 0;
                odetalleSelecionado.descripcion = string.Empty;
                odetalleSelecionado.abreviatura = string.Empty;
                odetalleSelecionado.estado = 0;

                Limpiar();
                Cancelar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void Limpiar()
        {
            this.txtCodigo.Text = "0";
            this.txtDescripcion.Text = string.Empty;
            this.txtAbreviatura.Text = string.Empty;
            this.txtEstado.Text = "ACTIVO";
            this.txtIdEstado.Text = "1";
            this.txtDescripcion.Focus();

        }

        private void ObtenerObjeto()
        {
            try
            {
                odetalle = new SAS_ColaboradorGerencia();
                odetalle.id = this.txtCodigo.Text.Trim() != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0;
                odetalle.descripcion = this.txtDescripcion.Text.Trim();
                odetalle.abreviatura = this.txtAbreviatura.Text.Trim();
                odetalle.estado = this.txtIdEstado.Text != string.Empty ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : Convert.ToInt32("0");

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void CambiarEstado()
        {
            #region Cambiar estado() 
            try
            {
                ObtenerObjeto();
                if (odetalle.descripcion != string.Empty)
                {
                    Modelo =new SAS_ColaboradorGerenciaController();
                    int resultado = 0;
                    resultado = Modelo.ChangeState("SAS", odetalle);
                    if (resultado == 2)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de anulación");
                        Actualizar();
                    }
                    else if (resultado == 3)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Activación");
                        Actualizar();
                    }
                    btnGrabar.Enabled = false;
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnEditar.Enabled = true;
                    btnCancelar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtDescripcion.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
            #endregion
        }


        #endregion

        private void Gerencias_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        [STAThread]
        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_ColaboradorGerencia>();
                Modelo =new SAS_ColaboradorGerenciaController();
                listado = Modelo.GetList("SAS");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        [STAThread]
        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = listado.OrderBy(x => x.descripcion).ToList().ToDataTable<SAS_ColaboradorGerencia>();
                dgvRegistro.Refresh();

                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }
    }
}
