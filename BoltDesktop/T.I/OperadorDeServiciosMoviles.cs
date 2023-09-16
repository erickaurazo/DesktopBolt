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
using System.Configuration;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class OperadorDeServiciosMoviles : Form
    {
        #region Definición de variables()         
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_OperadorTelefoniaMovilListado> listado;
        private SAS_OperadorTelefoniaMovil item;
        private SAS_OperadorTelefoniaMovilListado itemSelecionado;
        private SAS_OperadorTelefoniaMovilController modelo;
        #endregion

        public OperadorDeServiciosMoviles()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = Environment.UserName.Trim();
            Actualizar();
        }

        public OperadorDeServiciosMoviles(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
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

        private void OperadorDeServiciosMoviles_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_OperadorTelefoniaMovilListado>();
                modelo = new SAS_OperadorTelefoniaMovilController();
                listado = modelo.MobilePhoneOperators("SAS");
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
                dgvRegistro.DataSource = listado.OrderBy(x => x.descripcion).ToList().ToDataTable<SAS_OperadorTelefoniaMovilListado>();
                dgvRegistro.Refresh();
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            try
            {
                item = new SAS_OperadorTelefoniaMovil();
                item.id = 0;
                item.descripcion = string.Empty;
                item.estado = 0;


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
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnGrabar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Limpiar()
        {
            try
            {
                item = new SAS_OperadorTelefoniaMovil();
                item.id = 0;
                item.descripcion = string.Empty;
                item.estado = 0;

                itemSelecionado = new SAS_OperadorTelefoniaMovilListado();
                item.id = 0;
                item.descripcion = string.Empty;
                item.estado = 0;

                this.txtCodigo.Text = string.Empty;
                this.txtDescripcion.Text = string.Empty;
                this.txtEstado.Text = "ACTIVO";
                this.txtIdEstado.Text = "1";
                this.txtDescripcion.Focus();
                this.txtObservaciones.Text = string.Empty;
                this.txtClieProvCodigo.Text = string.Empty;
                this.txtClieProv.Text = string.Empty;
                this.txtAbreviatura.Text = string.Empty;
                this.chkesServicioMovil.Checked = false;
                this.chkesCableSatelital.Checked = false;
                this.chkCableFijo.Checked = false;



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
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
                item = new SAS_OperadorTelefoniaMovil();
                item.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : Convert.ToInt32("0");
                item.descripcion = this.txtDescripcion.Text;
                item.estado = this.txtIdEstado.Text != string.Empty ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : Convert.ToInt32("0");
                item.nota = this.txtObservaciones.Text != string.Empty ? Convert.ToString(this.txtObservaciones.Text.Trim()) : string.Empty;
                item.idclieprov = this.txtClieProvCodigo.Text != string.Empty ? Convert.ToString(this.txtClieProvCodigo.Text.Trim()) : string.Empty;
                item.abreviatura = this.txtAbreviatura.Text != string.Empty ? Convert.ToString(this.txtAbreviatura.Text.Trim()) : string.Empty;
                item.esServicioMovil = this.chkesServicioMovil.Checked == true ? 1 : 0;
                item.esCableSatelital = this.chkesCableSatelital.Checked == true ? 1 : 0;
                item.esCableFijo = this.chkCableFijo.Checked == true ? 1 : 0;
                item.esProveedorDeInternet = this.chkEsProveedorDeServiciosDeInternet.Checked == true ? 1 : 0;

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
                if (item.descripcion != string.Empty)
                {
                    modelo = new SAS_OperadorTelefoniaMovilController();
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
                    if (_user2.IdUsuario.Trim().ToUpper() == "Admin".ToUpper() || _user2.IdUsuario.Trim() == "Administrador".ToUpper() || _user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || _user2.IdUsuario.Trim().ToUpper() == "erickaurazo".ToUpper())
                    {
                        try
                        {
                            ObtenerObjeto();
                            if (item.descripcion != string.Empty)
                            {
                                modelo = new SAS_OperadorTelefoniaMovilController();
                                int resultado = 0;
                                resultado = modelo.Remove("SAS", item);
                                if (resultado == 4)
                                {
                                    MessageBox.Show("Se cambio ha eliminado correctamente", "Confirmación de Eliminación del registro");
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

        private void OperadorDeServiciosMoviles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
                #region 
                itemSelecionado = new SAS_OperadorTelefoniaMovilListado();
                itemSelecionado.descripcion = string.Empty;
                itemSelecionado.estado = 0;
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chDescripcion"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chDescripcion"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString()) : "0");
                                int codigoNumerico = Convert.ToInt32(codigo);
                                var resultado = listado.Where(x => x.id == codigoNumerico).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSelecionado = resultado.Single();
                                    itemSelecionado.id = codigoNumerico;
                                    AsignarObjetoEnFormularioDeEdicion(itemSelecionado);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    itemSelecionado = resultado.ElementAt(0);
                                    itemSelecionado.id = codigoNumerico;
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

        private void AsignarObjetoEnFormularioDeEdicion(SAS_OperadorTelefoniaMovilListado itemSelecionado)
        {
            try
            {
                this.txtCodigo.Text = itemSelecionado.id != null ? itemSelecionado.id.ToString().Trim() : string.Empty;
                this.txtDescripcion.Text = itemSelecionado.descripcion != null ? itemSelecionado.descripcion.Trim() : string.Empty;
                if (itemSelecionado.estado == 1)
                {
                    this.txtEstado.Text = "ACTIVO";
                    this.txtIdEstado.Text = "1";
                }
                else
                {
                    this.txtEstado.Text = "ANULADO";
                    this.txtIdEstado.Text = "0";
                }


                this.txtObservaciones.Text = itemSelecionado.nota != null ? itemSelecionado.nota.ToString().Trim() : string.Empty;
                this.txtClieProvCodigo.Text = itemSelecionado.idclieprov != null ? itemSelecionado.idclieprov.ToString().Trim() : string.Empty;
                this.txtAbreviatura.Text = itemSelecionado.abreviatura != null ? itemSelecionado.abreviatura.ToString().Trim() : string.Empty;
                this.txtClieProv.Text = itemSelecionado.razon_social != null ? itemSelecionado.razon_social.Trim() : string.Empty;


                if (itemSelecionado.esServicioMovil == 1)
                {
                    this.chkesServicioMovil.Checked = true;
                }
                else
                {
                    this.chkesServicioMovil.Checked = false;
                }

                if (itemSelecionado.esCableSatelital == 1)
                {
                    this.chkesCableSatelital.Checked = true;
                }
                else
                {
                    this.chkesCableSatelital.Checked = false;
                }

                if (itemSelecionado.esCableFijo == 1)
                {
                    this.chkCableFijo.Checked = true;
                }
                else
                {
                    this.chkCableFijo.Checked = false;
                }

                if (itemSelecionado.esProveedorDeInternet == 1)
                {
                    this.chkEsProveedorDeServiciosDeInternet.Checked = true;
                }
                else
                {
                    this.chkEsProveedorDeServiciosDeInternet.Checked = false;
                }
                


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
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
                modelo = new SAS_OperadorTelefoniaMovilController();
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

    }
}
