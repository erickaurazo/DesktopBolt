using Asistencia.Datos;
using Asistencia.Helper;
using Asistencia.Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Asistencia
{
    public partial class GoSistemaCatalogoModulos : Form
    {
        private ModuloSistemaController modelo;
        private List<ModuleSystem> modulos;        
        private ExportToExcelHelper modelExcel;
        private ModuleSystem module;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public GoSistemaCatalogoModulos()
        {
            InitializeComponent();
        }


        public GoSistemaCatalogoModulos(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtAbreviatura.Focus();
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnSave.Enabled = true;
            btnAtras.Enabled = true;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportToExcel.Enabled = false;
            btnHistorial.Enabled = true;
            gbList.Enabled = false;
            gbEdit.Enabled = true;
        }

        private void Limpiar()
        {
            this.txtCodigo.Clear();
            this.txtDescripcion.Clear();
            this.txtEstado.Text = "ACTIVO";
            this.txtAbreviatura.Clear();
            this.txtAbreviatura.Clear();

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {            
            BarraPrincipal.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            ProgressBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            CancelAction();
        }

        private void CancelAction()
        {
            btnNuevo.Enabled = !false;
            btnEditar.Enabled = !false;
            btnSave.Enabled = !true;
            btnAtras.Enabled = !true;
            btnAnular.Enabled = !false;
            btnEliminarRegistro.Enabled = !false;
            btnExportToExcel.Enabled = !false;
            btnHistorial.Enabled = !true;
            gbList.Enabled = !false;
            gbEdit.Enabled = !true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnSave.Enabled = true;
            btnAtras.Enabled = true;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnExportToExcel.Enabled = false;
            btnHistorial.Enabled = true;
            gbList.Enabled = false;
            gbEdit.Enabled = true;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void ChangeState()
        {
            modelo = new ModuloSistemaController();
            ModuloSistema modulo = new ModuloSistema();
            modulo.abreviatura = this.txtAbreviatura.Text;
            modulo.descripcion = this.txtDescripcion.Text;
            modulo.estado = 1;
            modulo.moduloCodigo = this.txtCodigo.Text;

            if (modelo.ChangeState(modulo, _conection) == true)
            {
                Limpiar();
                RefreshList();
                btnNuevo.Enabled = !false;
                btnEditar.Enabled = !false;
                btnSave.Enabled = !true;
                btnAtras.Enabled = !true;
                btnAnular.Enabled = !false;
                btnEliminarRegistro.Enabled = !false;
                btnExportToExcel.Enabled = !false;
                btnHistorial.Enabled = !true;
                gbList.Enabled = !false;
                gbEdit.Enabled = !true;
            }
            
            
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            btnNuevo.Enabled = !false;
            btnEditar.Enabled = !false;
            btnSave.Enabled = !true;
            btnAtras.Enabled = !true;
            btnAnular.Enabled = !false;
            btnEliminarRegistro.Enabled = !false;
            btnExportToExcel.Enabled = !false;
            btnHistorial.Enabled = !true;
            gbList.Enabled = !false;
            gbEdit.Enabled = !true;
        }

        private void commandBarButton4_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            modelExcel = new ExportToExcelHelper();
            modelExcel.ExportarToExcel(dgvList, saveFileDialog);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (this.bgwHilo.IsBusy != true)
            {
                this.Close();
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new ModuloSistemaController();
            modulos = new List<ModuleSystem>();
            modulos = modelo.GetListAll(_conection);
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dgvList.DataSource = modulos;
            dgvList.Refresh();
            BarraPrincipal.Enabled = !false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            ProgressBar.Visible = !true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            Save();

        }

        private void Save()
        {
            try
            {
                ModuloSistema modulo = new ModuloSistema();
                modulo.abreviatura = this.txtAbreviatura.Text;
                modulo.descripcion = this.txtDescripcion.Text;
                modulo.estado = 1;
                modulo.moduloCodigo = this.txtCodigo.Text;

                modelo = new ModuloSistemaController();
                if (modelo.ToRegister(modulo, _conection) == true)
                {
                    MessageBox.Show("Registro satisfactorio", "Mensaje del sistema");
                    Limpiar();
                    RefreshList();
                }


                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnSave.Enabled = false;
                btnAtras.Enabled = false;
                btnAnular.Enabled = true;
                btnEliminarRegistro.Enabled = true;
                btnExportToExcel.Enabled = true;
                btnHistorial.Enabled = true;
                gbList.Enabled = true;
                gbEdit.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

           
        }

        private void Modulo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Modulo_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            module = new ModuleSystem();
            Limpiar();
            if (dgvList.Rows.Count > 0)
            {
                if (dgvList.CurrentRow != null && dgvList.CurrentRow.Cells["chModuloCodigo"].Value != null)
                {
                    string idModulo = dgvList.CurrentRow.Cells["chModuloCodigo"].Value != null ? Convert.ToString(dgvList.CurrentRow.Cells["chModuloCodigo"].Value.ToString()) : "";

                    var modulById = modulos.Where(x => x.moduloCodigo == idModulo);
                    if (modulById.ToList().Count == 1)
                    {
                        module = modulById.Single();
                        this.txtCodigo.Text = module.moduloCodigo != null ? module.moduloCodigo.Trim() : string.Empty;
                        this.txtAbreviatura.Text = module.abreviatura != null ? module.abreviatura.Trim() : string.Empty;
                        this.txtDescripcion.Text = module.descripcion != null ? module.descripcion.Trim() : string.Empty;
                        this.txtEstado.Text = module.estadoDescripcion != null ? module.estadoDescripcion.Trim() : string.Empty;
                    }
                }
            }
        }
    }
}
