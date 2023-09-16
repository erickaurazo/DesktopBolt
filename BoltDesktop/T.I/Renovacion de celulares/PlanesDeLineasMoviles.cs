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
    public partial class PlanesDeLineasMoviles : Form
    {
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_PlanesDeTelefoniaMovilListado> listado;
        private SAS_PlanesDeTelefoniaMovil item;
        private SAS_PlanesDeTelefoniaMovilListado itemSelecionado;
        private SAS_PlanesDeTelefoniaMovilController modelo;

        public PlanesDeLineasMoviles()
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


        public PlanesDeLineasMoviles(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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


        private void PlanesDeLineasMoviles_Load(object sender, EventArgs e)
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
                listado = new List<SAS_PlanesDeTelefoniaMovilListado>();
                modelo = new SAS_PlanesDeTelefoniaMovilController();
                listado = modelo.ListOfMobilePhonePlans("SAS");
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
                dgvRegistro.DataSource = listado.OrderBy(x => x.descripcion).ToList().ToDataTable<SAS_PlanesDeTelefoniaMovilListado>();
                dgvRegistro.Refresh();
                progressBar1.Visible = false;
                btnActualizarLista.Enabled = true;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            try
            {
                item = new SAS_PlanesDeTelefoniaMovil();
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
                item = new SAS_PlanesDeTelefoniaMovil();
                item.id = 0;
                item.descripcion = string.Empty;
                item.estado = 0;

                itemSelecionado = new SAS_PlanesDeTelefoniaMovilListado();
                item.id = 0;
                item.descripcion = string.Empty;
                item.estado = 0;

                txtCodigo.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtEstado.Text = "ACTIVO";
                txtIdEstado.Text = "1";
                txtDescripcion.Focus();
                txtClieProvCodigo.Text = string.Empty;
                txtClieProv.Text = string.Empty;
                txtDatos.Text = string.Empty;
                txtClieProvCodigo.Text = string.Empty;
                txtCargoFijo.Text = string.Empty;
                txtClieProv.Text = string.Empty;
                txtGbInternacional.Text = string.Empty;
                txtZonaNavegacion.Text = string.Empty;
                txtWhasappInternacional.Text = string.Empty;
                txtbpIlimitado.Text = string.Empty;
                txtR1.Text = string.Empty;
                txtR2.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtClieProv.Text = string.Empty;




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
            btnActualizarLista.Enabled = false;
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
                item = new SAS_PlanesDeTelefoniaMovil();
                item.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : Convert.ToInt32("0");
                item.descripcion = this.txtDescripcion.Text;
                item.datos = this.txtDatos.Text != string.Empty ? Convert.ToDecimal(this.txtDatos.Text.Trim()) : 0;
                item.CF = this.txtCargoFijo.Text != string.Empty ? Convert.ToDecimal(this.txtCargoFijo.Text.Trim()) : 0;
                item.GbInternacional = this.txtGbInternacional.Text != string.Empty ? Convert.ToDecimal(this.txtGbInternacional.Text.Trim()) : 0;
                item.zonaNavegacion = this.txtZonaNavegacion.Text != string.Empty ? Convert.ToString(this.txtZonaNavegacion.Text.Trim()) : string.Empty;
                item.whastappInternacional = this.txtWhasappInternacional.Text != string.Empty ? Convert.ToString(this.txtWhasappInternacional.Text.Trim()) : string.Empty;
                item.bpIliminatado = this.txtbpIlimitado.Text != string.Empty ? Convert.ToString(this.txtbpIlimitado.Text.Trim()) : string.Empty;
                item.minutosVozR1 = this.txtR1.Text != string.Empty ? Convert.ToString(this.txtR1.Text.Trim()) : string.Empty;
                item.minutosVozR2 = this.txtR2.Text != string.Empty ? Convert.ToString(this.txtR2.Text.Trim()) : string.Empty;
                item.tipo = this.txtTipo.Text != string.Empty ? Convert.ToString(this.txtTipo.Text.Trim()) : string.Empty;
                item.estado = this.txtIdEstado.Text != string.Empty ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : Convert.ToInt32("0");
                item.idProveedor = this.txtClieProvCodigo.Text != string.Empty ? Convert.ToInt32(this.txtClieProvCodigo.Text.Trim()) : (int?)null;
                item.fechaCreacion = DateTime.Now;
                item.creadoPor = _user2.IdUsuario;
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
                    modelo = new SAS_PlanesDeTelefoniaMovilController();
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
                    if (_user2.IdUsuario.Trim().ToUpper() == "Admin".ToUpper() || _user2.IdUsuario.Trim() == "Administrador".ToUpper() || _user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || _user2.IdUsuario.Trim().ToUpper() == "ERICKAURAZO")
                    {
                        try
                        {
                            ObtenerObjeto();
                            if (item.descripcion != string.Empty)
                            {
                                modelo = new SAS_PlanesDeTelefoniaMovilController();
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

        private void PlanesDeLineasMoviles_FormClosing(object sender, FormClosingEventArgs e)
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
            Registrar();
        }
        private void Registrar()
        {
            try
            {
                ObtenerObjeto();
                modelo = new SAS_PlanesDeTelefoniaMovilController();
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

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Limpiar();
                #region 
                itemSelecionado = new SAS_PlanesDeTelefoniaMovilListado();
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

        private void AsignarObjetoEnFormularioDeEdicion(SAS_PlanesDeTelefoniaMovilListado itemSelecionado)
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


                txtDatos.Text = itemSelecionado.datos != null ? itemSelecionado.datos.ToDecimalPresentation().Trim() : string.Empty;
                txtClieProvCodigo.Text = itemSelecionado.idProveedor != null ? itemSelecionado.idProveedor.Value.ToString().Trim() : string.Empty;
                txtCargoFijo.Text = itemSelecionado.CF != null ? itemSelecionado.CF.ToDecimalPresentation().Trim() : string.Empty;
                txtClieProv.Text = itemSelecionado.proveedor != null ? itemSelecionado.proveedor.ToString().Trim() : string.Empty;
                txtGbInternacional.Text = itemSelecionado.GbInternacional != null ? itemSelecionado.GbInternacional.ToDecimalPresentation().Trim() : string.Empty;
                txtZonaNavegacion.Text = itemSelecionado.zonaNavegacion != null ? itemSelecionado.zonaNavegacion.Trim() : string.Empty;
                txtWhasappInternacional.Text = itemSelecionado.whastappInternacional != null ? itemSelecionado.whastappInternacional.Trim() : string.Empty;
                txtbpIlimitado.Text = itemSelecionado.bpIliminatado != null ? itemSelecionado.bpIliminatado.Trim() : string.Empty;
                txtR1.Text = itemSelecionado.minutosVozR1 != null ? itemSelecionado.minutosVozR1.Trim() : string.Empty;
                txtR2.Text = itemSelecionado.minutosVozR2 != null ? itemSelecionado.minutosVozR2.Trim() : string.Empty;
                txtTipo.Text = itemSelecionado.tipo != null ? itemSelecionado.tipo.Trim() : string.Empty;



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

    }
}
