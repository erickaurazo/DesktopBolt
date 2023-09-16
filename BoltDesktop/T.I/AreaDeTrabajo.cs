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


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class AreaDeTrabajo : Form
    {
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName;
        private bool exportVisualSettings;
        private List<AREAS> listado;
        private AREAS item;
        private AREAS itemSelecionado;
        private AreasController modelo;


        public AreaDeTrabajo()
        {
            InitializeComponent();
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = Environment.UserName.Trim();
            Actualizar();
        }

        public AreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            Actualizar();
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
            items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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



        private void AreaDeTrabajo_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            try
            {
                item = new AREAS();
                item.IDEMPRESA = string.Empty;
                item.IDAREA = string.Empty;                
                item.ESTADO = 0;


                Limpiar();
                Cancelar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }


        private void Cancelar()
        {
            btnGrabar.Enabled = !false;
            gbEdit.Enabled = !false;
            gbList.Enabled = !true;
            btnEditar.Enabled = !true;
            btnCancelar.Enabled = !true;
        }

        private void Limpiar()
        {
            try
            {
                item = new AREAS();
                item.IDEMPRESA = string.Empty;
                item.IDAREA = string.Empty;
                item.ESTADO = 0;

                itemSelecionado = new AREAS();
                itemSelecionado.IDEMPRESA = string.Empty;
                itemSelecionado.IDAREA = string.Empty;
                itemSelecionado.ESTADO = 0;

                this.txtCodigo.Text = string.Empty;
                this.txtDescripcion.Text = string.Empty;                
                this.txtEstado.Text = "ACTIVO";
                this.txtIdEstado.Text = "1";                
                this.txtDescripcion.Focus();

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnGrabar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void ObtenerObjeto()
        {

            try
            {
                item = new AREAS();
                item.IDEMPRESA = "001";
                item.IDAREA = this.txtCodigo.Text;
                item.DESCRIPCION = this.txtDescripcion.Text;                
                item.ESTADO = this.txtIdEstado.Text != string.Empty ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : Convert.ToInt32("0");
                item.SINCRONIZA = ('N');
                item.FECHACREACION = DateTime.Now;
                item.exige_docvinculadoliqgasto = 0;           
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void CambiarEstado()
        {
            try
            {
                ObtenerObjeto();
                if (item.DESCRIPCION != string.Empty)
                {
                    modelo = new AreasController();
                    int resultado = 0;
                    resultado = modelo.ChangeState("SAS", item);
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
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            #region Eliminar() 
            if (_user2 != null)
            {
                if (_user2.IdUsuario != null)
                {
                    if (_user2.IdUsuario.Trim() == "Admin" || _user2.IdUsuario.Trim() == "Administrador" || _user2.IdUsuario.Trim() == "EAURAZO")
                    {

                    }
                    else
                    {
                        MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                return;
            }
            #endregion
        }

        private void btnExportar_Click(object sender, EventArgs e)
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<AREAS>();
                modelo = new AreasController();
                listado = modelo.ListOfWorkAreas("SAS");
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
                dgvRegistro.DataSource = listado.OrderBy(x => x.DESCRIPCION).ToList().ToDataTable<AREAS>();
                dgvRegistro.Refresh();
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {

            Registrar();
        }

        private void Registrar()
        {
            try
            {
                ObtenerObjeto();
                modelo = new AreasController();
                int resultado = modelo.ToRegister("SAS", item);
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

        private void AreaDeTrabajo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvRegistro_SelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
        {
            try
            {
                #region 
                itemSelecionado = new AREAS();
                itemSelecionado.DESCRIPCION = string.Empty;                
                itemSelecionado.ESTADO = 0;
                


                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chDescripcion"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chDescripcion"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvRegistro.CurrentRow.Cells["chIDAREA"].Value != null ? (dgvRegistro.CurrentRow.Cells["chIDAREA"].Value.ToString()) : string.Empty);
                                var resultado = listado.Where(x => x.IDAREA == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSelecionado = resultado.Single();
                                    itemSelecionado.IDAREA = codigo;
                                    AsignarObjetoEnFormularioDeEdicion(itemSelecionado);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    itemSelecionado = resultado.ElementAt(0);
                                    itemSelecionado.IDAREA = codigo;
                                    AsignarObjetoEnFormularioDeEdicion(itemSelecionado);
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


        private void AsignarObjetoEnFormularioDeEdicion(AREAS itemSelecionado)
        {
            try
            {
                this.txtCodigo.Text = itemSelecionado.IDAREA != null ? itemSelecionado.IDAREA.ToString().Trim() : string.Empty;
                this.txtDescripcion.Text = itemSelecionado.DESCRIPCION != null ? itemSelecionado.DESCRIPCION.Trim() : string.Empty;
                if (itemSelecionado.ESTADO == 1)
                {
                    this.txtEstado.Text = "ACTIVO";
                    this.txtIdEstado.Text = "1";
                }
                else
                {
                    this.txtEstado.Text = "ANULADO";
                    this.txtIdEstado.Text = "0";
                }

                
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

    }
}
