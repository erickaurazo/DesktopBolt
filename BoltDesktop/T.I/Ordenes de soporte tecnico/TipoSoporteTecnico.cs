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
using System.Reflection;
using Telerik.WinControls.Data;
using System.Collections;
using System.Configuration;
using ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class TipoSoporteTecnico : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private bool exportVisualSettings;
        private string fileName;
        private SAS_DispositivoTipoMantenimientoController modelo;
        private List<SAS_DispositivosTipoMantenimiento> listadoAll;
        private SAS_DispositivosTipoMantenimiento itemSelecionado, itemARegistrar;
        private List<SAS_DispositivosTipoMantenimientoDetalle> listadoDetalleByItemRegistrar, listadoDetalleByItemEliminar;
        private List<SAS_ListadoDispositivosTipoMantenimientoDetalleByIdResult> listadoDetalleByItem;
        private string result;
        private int lastItem = 0;
        private string msgError = string.Empty;


        public TipoSoporteTecnico()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.NombreCompleto = "Erick Aurazo";
            user2.IdUsuario = "eaurazo";

            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.consultar = 1;
            privilege.anular = 1;
            privilege.editar = 1;
            privilege.eliminar = 1;

            Consultar();
        }

        public TipoSoporteTecnico(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            Consultar();
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
                this.dgvRegistro.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvRegistro.TableElement.EndUpdate();

                base.OnLoad(e);
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
                this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
                this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvRegistro.GroupDescriptors.Clear();
                this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N0}; ", GridAggregateFunction.Count));
                this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }

        }



        private void Consultar()
        {
            try
            {
                gbEdit.Enabled = false;
                gbList.Enabled = false;
                progressBar1.Visible = true;
                btnMenu.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }


        private void CambiarEstado()
        {
            try
            {
                if (itemSelecionado != null)
                {
                    if (itemSelecionado.estado == 1)
                    {
                        gbEdit.Enabled = false;
                        gbList.Enabled = false;
                        btnMenu.Enabled = false;
                        modelo = new SAS_DispositivoTipoMantenimientoController();
                        modelo.ChangeState(conection, itemSelecionado);
                        gbEdit.Enabled = true;
                        txtAbreviatura.ReadOnly = true;
                        txtAbreviatura.Enabled = false;
                        txtDescripcion.ReadOnly = true;
                        txtDescripcion.Enabled = false;
                        gbList.Enabled = !false;
                        btnEditar.Enabled = !false;
                        btnGrabar.Enabled = !true;
                        btnCancelar.Enabled = !true;
                        btnNuevo.Enabled = !false;
                        btnActualizarLista.Enabled = !false;
                        btnEliminar.Enabled = !false;
                        btnAnular.Enabled = !false;
                        btnExportar.Enabled = !false;
                        Consultar();
                    }

                }
            }
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            try
            {
                if (user2.IdUsuario.ToUpper() == "EAURAZO" || user2.IdUsuario.ToUpper() == "ADMINISTRADOR" || user2.IdUsuario.ToUpper() == "FCERNA")
                {
                    if (itemSelecionado != null)
                    {
                        if (itemSelecionado.estado == 1)
                        {
                            gbEdit.Enabled = false;
                            gbList.Enabled = false;
                            btnMenu.Enabled = false;
                            modelo = new SAS_DispositivoTipoMantenimientoController();
                            modelo.Delete(conection, itemSelecionado);
                            gbEdit.Enabled = true;
                            txtAbreviatura.ReadOnly = true;
                            txtAbreviatura.Enabled = false;
                            txtDescripcion.ReadOnly = true;
                            txtDescripcion.Enabled = false;
                            gbList.Enabled = !false;
                            btnEditar.Enabled = !false;
                            btnGrabar.Enabled = !true;
                            btnCancelar.Enabled = !true;
                            btnNuevo.Enabled = !false;
                            btnActualizarLista.Enabled = !false;
                            btnEliminar.Enabled = !false;
                            btnAnular.Enabled = !false;
                            btnExportar.Enabled = !false;
                            Consultar();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Opción no disponible", "Mensaje del sistema");
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarToExcel();
        }


        private void ExportarToExcel()
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


        private void btnSalir_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void Salir()
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


        private void btnChangeStateDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (this.txtTipoDispositivoCodigo.Text.Trim() != string.Empty && this.txtTipoDispositivoDescripcion.Text.Trim() != string.Empty)
            {
                Additem();
            }
            else
            {
                MessageBox.Show("Debe ingresar un tipo de software", "Mensaje del sistema");
            }
        }


        private string ObtenerItemDetalle(int numeroRegistros)
        {
            #region Get item for grid detail() 
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }


        private void Additem()
        {
            try
            {
                #region add Item()
                if (dgvDetail != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(txtCodigo.Text.Trim()); // chcodigo                 
                    array.Add((ObtenerItemDetalle(lastItem))); // chitem
                    array.Add(this.txtTipoDispositivoCodigo.Text.Trim()); // chTipoSoftware                                                        
                    array.Add(this.txtTipoDispositivoDescripcion.Text.Trim()); // tipoSoftware                    
                    array.Add(string.Empty); // chdescripcion         
                    array.Add(Convert.ToDecimal(0)); // minutos              
                    array.Add(1); // chestado
                    array.Add(1); // chvisibleEnReportes
                    dgvDetail.AgregarFila(array);
                    lastItem += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }


        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            Deleteitem();
        }

        private void Deleteitem()
        {
            try
            {
                if (this.dgvDetail != null)
                {
                    #region delete item() 
                    if (dgvDetail.CurrentRow != null && dgvDetail.CurrentRow.Cells["chCodigo"].Value != null)
                    {
                        //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        try
                        {

                            string codigo = (dgvDetail.CurrentRow.Cells["chCodigo"].Value.ToString().Trim() != "" ? (dgvDetail.CurrentRow.Cells["chCodigo"].Value).ToString() : string.Empty);
                            if (codigo != null)
                            {
                                string itemIP = ((dgvDetail.CurrentRow.Cells["chItem"].Value != null | dgvDetail.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvDetail.CurrentRow.Cells["chItem"].Value.ToString()) : string.Empty);
                                if (codigo != null && itemIP != null)
                                {

                                    listadoDetalleByItemEliminar.Add(new SAS_DispositivosTipoMantenimientoDetalle
                                    {
                                        codigo = codigo,
                                        item = itemIP,
                                    });
                                }
                            }

                            dgvDetail.Rows.Remove(dgvDetail.CurrentRow);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                            return;
                        }
                        //}
                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
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
                try
                {
                    gbEdit.Enabled = false;
                    gbList.Enabled = false;
                    btnMenu.Enabled = false;

                    itemARegistrar = new SAS_DispositivosTipoMantenimiento();
                    itemARegistrar.id = (this.txtCodigo.Text).Trim();
                    itemARegistrar.descripcion = this.txtDescripcion.Text.Trim();
                    itemARegistrar.abreviatura = this.txtAbreviatura.Text.Trim();
                    itemARegistrar.estado = this.txtIdEstado.Text.ToString().Trim() == "1" ? Convert.ToByte(1) : Convert.ToByte(0);
                    itemARegistrar.fechaCreacion = DateTime.Now;
                    itemARegistrar.usuario = user2.IdUsuario != null ? user2.IdUsuario.Trim() : Environment.UserName.Trim();

                    #region Obtener detalle()
                    listadoDetalleByItemRegistrar = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                    if (this.dgvDetail != null)
                    {
                        if (this.dgvDetail.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow fila in this.dgvDetail.Rows)
                            {
                                if (fila.Cells["chcodigo"].Value.ToString().Trim() != String.Empty)
                                {
                                    try
                                    {
                                        #region Obtener detalle por linea detalle() 
                                        SAS_DispositivosTipoMantenimientoDetalle detailForItem = new SAS_DispositivosTipoMantenimientoDetalle();
                                        detailForItem.codigo = fila.Cells["chCodigo"].Value != null ? (fila.Cells["chCodigo"].Value.ToString().Trim()) : string.Empty;
                                        detailForItem.item = fila.Cells["chItem"].Value != null ? fila.Cells["chItem"].Value.ToString().Trim() : string.Empty;
                                        detailForItem.idtipoHardware = fila.Cells["chidtipoHardware"].Value != null ? (fila.Cells["chidtipoHardware"].Value).ToString() : string.Empty;
                                        detailForItem.descripcion = fila.Cells["chdescripcion"].Value != null ? fila.Cells["chdescripcion"].Value.ToString().Trim() : string.Empty;
                                        detailForItem.estado = fila.Cells["chestado"].Value != null ? Convert.ToByte(fila.Cells["chestado"].Value.ToString().Trim()) : Convert.ToByte(1);
                                        detailForItem.visibleEnReportes = fila.Cells["chvisibleEnReportes"].Value != null ? Convert.ToByte(fila.Cells["chvisibleEnReportes"].Value.ToString().Trim()) : Convert.ToByte(1);
                                        detailForItem.creadoPor = user2.IdUsuario != null ? user2.IdUsuario.Trim() : Environment.UserName.Trim();
                                        detailForItem.fechaCreacion = DateTime.Now;
                                        detailForItem.hostname = Environment.MachineName.Trim() + "/" + Environment.UserName.Trim();
                                        detailForItem.minutos = fila.Cells["chMinutos"].Value != null ? Convert.ToDecimal(fila.Cells["chMinutos"].Value.ToString().Trim()) : Convert.ToDecimal(0);

                                        listadoDetalleByItemRegistrar.Add(detailForItem);
                                        #endregion
                                    }
                                    catch (Exception Ex)
                                    {
                                        MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    return;
                }
                modelo = new SAS_DispositivoTipoMantenimientoController();
                int resultado = modelo.ToRegister(conection, itemARegistrar, listadoDetalleByItemRegistrar, listadoDetalleByItemEliminar);

                if (resultado == 0)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se registró satisfactoriamente", "Confirmación del sistema");
                }
                else if (resultado == 1)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se actualizó satisfactoriamente", "Confirmación del sistema");
                }

                listadoDetalleByItemRegistrar = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                listadoDetalleByItemEliminar = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                lastItem = 0;
                Consultar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbEdit.Enabled = true;
                btnGrabar.Enabled = true;
                btnCancelar.Enabled = true;
                return;
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnGrabar.Enabled = false;
            btnCancelar.Enabled = false;
            btnMenu.Enabled = true;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminar.Enabled = true;
            btnExportar.Enabled = true;
            btnSalir.Enabled = true;
            btnActualizarLista.Enabled = true;

        }


        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                Limpiar();
                itemSelecionado = new SAS_DispositivosTipoMantenimiento();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chCodigo"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chCodigo"].Value.ToString() != string.Empty)
                            {
                                string codigo = (dgvRegistro.CurrentRow.Cells["chCodigo"].Value != null ? (string)Convert.ChangeType(dgvRegistro.CurrentRow.Cells["chCodigo"].Value, typeof(string)) : string.Empty);
                                var resultado = listadoAll.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSelecionado = resultado.ElementAt(0);
                                    //itemSelecionado.id = codigo;
                                    AsingarObjeto(itemSelecionado);
                                }
                            }
                        }
                    }
                }
                #endregion
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

        private void AsingarObjeto(SAS_DispositivosTipoMantenimiento oDetalle)
        {
            try
            {
                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : string.Empty;
                this.txtIdEstado.Text = oDetalle.estado != null ? oDetalle.estado.ToString().Trim() : string.Empty;
                if (this.txtIdEstado.Text != string.Empty)
                {
                    if (this.txtIdEstado.Text == "1")
                    {
                        this.txtEstado.Text = ("ACTIVO");
                    }
                    else
                    {
                        this.txtEstado.Text = ("ANULADO");
                    }
                }
                this.txtDescripcion.Text = oDetalle.descripcion != null ? oDetalle.descripcion.ToString().Trim() : string.Empty;
                this.txtAbreviatura.Text = oDetalle.abreviatura != null ? oDetalle.abreviatura.ToString().Trim() : string.Empty;



                listadoDetalleByItem = new List<SAS_ListadoDispositivosTipoMantenimientoDetalleByIdResult>();
                modelo = new SAS_DispositivoTipoMantenimientoController();
                listadoDetalleByItem = modelo.GetToListDetailById(conection, oDetalle).ToList();


                lastItem = 0;

                if (listadoDetalleByItem != null)
                {
                    if (listadoDetalleByItem.Count > 0)
                    {
                        lastItem = Convert.ToInt32(listadoDetalleByItem.Max(X => X.item));
                    }
                }

                dgvDetail.CargarDatos(listadoDetalleByItem.ToDataTable<SAS_ListadoDispositivosTipoMantenimientoDetalleByIdResult>());
                dgvDetail.Refresh();



            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString() + msgError, "MENSAJE DEL SISTEMA");
                return;
            }
        }



        private void Limpiar()
        {
            #region Limpiar() 
            try
            {
                itemARegistrar = new SAS_DispositivosTipoMantenimiento();
                itemARegistrar.id = string.Empty;
                itemARegistrar.estado = 1;
                itemSelecionado = new SAS_DispositivosTipoMantenimiento();
                itemSelecionado.id = string.Empty;
                itemSelecionado.estado = 1;
                txtCodigo.Text = "0";
                txtEstado.Text = "ACTIVO";
                txtIdEstado.Text = "1";
                txtDescripcion.Clear();
                txtAbreviatura.Clear();
                txtDescripcion.Focus();
                txtTipoDispositivoCodigo.Clear();
                txtTipoDispositivoDescripcion.Clear();
                ClearGridDetail();
                listadoDetalleByItem = new List<SAS_ListadoDispositivosTipoMantenimientoDetalleByIdResult>();
                listadoDetalleByItemRegistrar = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                listadoDetalleByItemEliminar = new List<SAS_DispositivosTipoMantenimientoDetalle>();
                lastItem = 0;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
            #endregion

        }

        private void ClearGridDetail()
        {
            try
            {
                if (this.dgvDetail != null)
                {
                    if (this.dgvDetail.Rows.Count > 0)
                    {
                        int tope = dgvDetail.Rows.Count;
                        for (int i = 0; i < tope; i++)
                        {
                            dgvDetail.Rows.RemoveAt(0);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }


        private void Nuevo()
        {
            try
            {
                itemARegistrar = new SAS_DispositivosTipoMantenimiento();
                itemARegistrar.estado = 1;
                itemARegistrar.id = string.Empty;

                itemSelecionado = new SAS_DispositivosTipoMantenimiento();
                itemSelecionado.estado = 1;
                itemSelecionado.id = string.Empty;

                gbList.Enabled = false;
                gbEdit.Enabled = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = false;
                btnAnular.Enabled = false;
                btnEliminar.Enabled = false;
                btnActualizarLista.Enabled = false;
                btnCancelar.Enabled = true;
                btnGrabar.Enabled = true;
                btnExportar.Enabled = false;
                btnSalir.Enabled = true;

                Limpiar();
                Editar();
            }
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void Editar()
        {
            try
            {
                if (itemSelecionado != null)
                {
                    if (itemSelecionado.estado == 1)
                    {
                        gbEdit.Enabled = true;
                        txtAbreviatura.ReadOnly = false;
                        txtAbreviatura.Enabled = true;
                        txtDescripcion.ReadOnly = false;
                        txtDescripcion.Enabled = true;
                        gbList.Enabled = false;
                        btnEditar.Enabled = false;
                        btnGrabar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnNuevo.Enabled = false;
                        btnActualizarLista.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnAnular.Enabled = false;
                        btnExportar.Enabled = false;
                    }
                    else
                    {
                        RadMessageBox.Show(this, "El item seleccionado no tiene el estado para realizar una modificación", "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void TipoSoporteTecnico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TipoSoporteTecnico_Load(object sender, EventArgs e)
        {

        }

        private void dgvDetail_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispositivoTipoMantenimientoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Listado de minutos() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chMinutos")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.ObtenerListadoDeMinutos("SAS");
                        search.Text = "Buscar minutos";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDetail.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMinutos"].Value = search.ObjetoRetorno.Codigo;
                            //this.dgvDetail.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMinutos"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void btnVistaAgrupada_Click(object sender, EventArgs e)
        {
            string codigoTipoSoporte =(txtCodigo.Text != string.Empty ? this.txtCodigo.Text.ToString().Trim() : string.Empty);
            if (codigoTipoSoporte != string.Empty)
            {
                TipoSoporteTecnicoVistaAgrupada ofrm = new TipoSoporteTecnicoVistaAgrupada(conection, user2, companyId, privilege, codigoTipoSoporte);
                ofrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se tiene detalle de este tipo de mantenimineto", "MENSAJE DEL SISTEMAS");
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listadoAll = new List<SAS_DispositivosTipoMantenimiento>();
                modelo = new SAS_DispositivoTipoMantenimientoController();
                listadoAll = modelo.GetToList(conection).ToList();
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
                dgvRegistro.DataSource = listadoAll.OrderBy(x => x.descripcion).ToList().ToDataTable<SAS_DispositivosTipoMantenimiento>();
                dgvRegistro.Refresh();
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnAnular.Enabled = true;
                btnEliminar.Enabled = true;
                btnCancelar.Enabled = true;
                btnGrabar.Enabled = true;
                btnExportar.Enabled = true;
                btnSalir.Enabled = true;
                btnActualizarLista.Enabled = true;
                progressBar1.Visible = false;
                btnMenu.Enabled = true;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }
    }
}
