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
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using Telerik.WinControls.Data;
using System.Threading;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class PersonalExterno : Form
    {
        #region declaración de variables() 
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName;
        private bool exportVisualSettings;
        private SAS_PersonalExternoController Modelo;
        private SAS_PersonalExterno item;
        private SAS_PersonalExternolistado itemSelecionadoDeGrilla;
        private int lastItem = 0;
        private List<SAS_PersonalExternoDetalle> detalleEliminados;
        private List<SAS_PersonalExternoDetalle> listadetalle;
        private List<SAS_PersonalExternolistado> listado;
        private List<SAS_PersonalExternoDetalleByIdResult> listadoDetallePorCodigoPersonalExterno;
        #endregion

        public PersonalExterno()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = Environment.UserName;
        }

        public PersonalExterno(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
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
            Inicio();
            Actualizar();
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
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
                items1.Add(new GridViewSummaryItem("chcuenta", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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
                FilterDescriptor wrongFilter = this.dgvRegistro.Columns["chcuenta"].FilterDescriptor;
                double correctValue = 12.5;
                FilterDescriptor filterDescriptor =
                    new FilterDescriptor(wrongFilter.PropertyName, wrongFilter.Operator, correctValue);
                filterDescriptor.IsFilterEditor = wrongFilter.IsFilterEditor;

                this.dgvRegistro.FilterDescriptors.Remove(wrongFilter);
                this.dgvRegistro.FilterDescriptors.Add(filterDescriptor);

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
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

        private void PersonalExterno_Load(object sender, EventArgs e)
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
            txtNumeroDocumento.Focus();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarDeEstado();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnEnivarCorreo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
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

        private void btnChangeStateDetail_Click(object sender, EventArgs e)
        {
            CambiarEstadoItemDetalle();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AgregarItemAGrillaDetalle();
        }

        private void AgregarItemAGrillaDetalle()
        {
            try
            {
                #region add Item()
                if (dgvDetail != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // id                 
                    array.Add((ObtenerItemDetalle(lastItem))); // item
                    array.Add(1); // idTipo
                    array.Add("Solicitud de autorización"); // tipocuenta         
                    array.Add(string.Empty); // link                                                     
                    array.Add(string.Empty); // descripcion
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado                              
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


        private string ObtenerItemDetalle(int numeroRegistros)
        {
            #region Get item for grid detail() 
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }


        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            EliminarItemDeGrillaDetalle();
        }

        private void EliminarItemDeGrillaDetalle()
        {
            try
            {
                if (this.dgvDetail != null)
                {
                    #region delete item() 
                    if (dgvDetail.CurrentRow != null && dgvDetail.CurrentRow.Cells["chId"].Value != null)
                    {
                        //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        try
                        {

                            Int32 dispositivoCodigo = (dgvDetail.CurrentRow.Cells["chId"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetail.CurrentRow.Cells["chId"].Value) : 0);
                            if (dispositivoCodigo != 0)
                            {
                                string itemIP = ((dgvDetail.CurrentRow.Cells["chItem"].Value != null | dgvDetail.CurrentRow.Cells["chItem"].Value.ToString().Trim() != string.Empty) ? (dgvDetail.CurrentRow.Cells["chItem"].Value.ToString()) : string.Empty);
                                if (dispositivoCodigo != 0 && itemIP != string.Empty)
                                {

                                    detalleEliminados.Add(new SAS_PersonalExternoDetalle
                                    {
                                        id = dispositivoCodigo,
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnGrabar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
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
                Modelo =new SAS_PersonalExternoController();
                int resultado = Modelo.ToRegister("SAS", item, detalleEliminados, listadetalle);
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
                listadetalle = new List<SAS_PersonalExternoDetalle>();
                detalleEliminados = new List<SAS_PersonalExternoDetalle>();
                lastItem = 0;
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
                #region 
                itemSelecionadoDeGrilla = new SAS_PersonalExternolistado();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? (int)Convert.ChangeType(dgvRegistro.CurrentRow.Cells["chId"].Value, typeof(Int32)) : 0);
                                var resultado = listado.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSelecionadoDeGrilla = resultado.Single();
                                    itemSelecionadoDeGrilla.id = codigo;
                                    AsignarControlesAObjetoParaRegistro(itemSelecionadoDeGrilla);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    itemSelecionadoDeGrilla = resultado.ElementAt(0);
                                    itemSelecionadoDeGrilla.id = codigo;
                                    AsignarControlesAObjetoParaRegistro(itemSelecionadoDeGrilla);
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
                throw;
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_PersonalExternolistado>();
                Modelo =new SAS_PersonalExternoController();
                listado = Modelo.GetListOfExternalStaff("SAS");
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
                dgvRegistro.DataSource = listado.OrderBy(x => x.NombresCompletos).ToList().ToDataTable<SAS_PersonalExternolistado>();
                dgvRegistro.Refresh();
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            Actualizar();

            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void AsignarControlesAObjetoParaRegistro(SAS_PersonalExternolistado oDetalle)
        {
            try
            {
                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : string.Empty;
                this.txtCodigoInterno.Text = oDetalle.codigoPersonalExterno != null ? oDetalle.codigoPersonalExterno.ToString().Trim() : string.Empty;
                this.txtNumeroDocumento.Text = oDetalle.codigoDeIdentifacion != null ? oDetalle.codigoDeIdentifacion.ToString().Trim() : string.Empty;
                this.txtNombresCompletos.Text = oDetalle.NombresCompletos != null ? oDetalle.NombresCompletos.ToString().Trim() : string.Empty;
                this.txtAreaCodigo.Text = oDetalle.codigoDeAreaSolicitante != null ? oDetalle.codigoDeAreaSolicitante.ToString().Trim() : string.Empty;
                this.txtAreaDescripcion.Text = oDetalle.nombreDeAreaSolicitante != null ? oDetalle.nombreDeAreaSolicitante.ToString().Trim() : string.Empty;
                this.txtCodigoProveedor.Text = oDetalle.codigoDeEmpresa != null ? oDetalle.codigoDeEmpresa.ToString().Trim() : string.Empty;
                this.txtProveedor.Text = oDetalle.nombreDeEmpresa != null ? oDetalle.nombreDeEmpresa.ToString().Trim() : string.Empty;
                this.txtEmpresa.Text = oDetalle.nombreEmpresa != null ? oDetalle.nombreEmpresa.ToString().Trim() : string.Empty;
                this.txtCargo.Text = oDetalle.cargo != null ? oDetalle.cargo.ToString().Trim() : string.Empty;
                this.txtFechaDesde.Text = oDetalle.vigenciaDesde != (DateTime?)null ? oDetalle.vigenciaDesde.ToPresentationDate() : string.Empty;
                this.txtFechaHasta.Text = oDetalle.vigenciaHasta != (DateTime?)null ? oDetalle.vigenciaHasta.ToPresentationDate() : string.Empty;
                this.txtIdEstado.Text = oDetalle.estado != null ? oDetalle.estado.ToDecimalPresentation() : "1";
                this.txtEstado.Text = oDetalle.estadoDescripcion != null ? oDetalle.estadoDescripcion : string.Empty;
                this.txtObservaciones.Text = oDetalle.glosa != null ? oDetalle.glosa.ToString().Trim() : string.Empty;

                listadoDetallePorCodigoPersonalExterno = new List<SAS_PersonalExternoDetalleByIdResult>();
                Modelo =new SAS_PersonalExternoController();
                listadoDetallePorCodigoPersonalExterno = Modelo.ListadoColaboradoresByDispositivoByCodigo("SAS", oDetalle.id); // Obtener listado detalle

                lastItem = 0;

                if (listadoDetallePorCodigoPersonalExterno != null)
                {
                    if (listadoDetallePorCodigoPersonalExterno.Count > 0)
                    {
                        lastItem = Convert.ToInt32(listadoDetallePorCodigoPersonalExterno.Max(X => X.item) + 1);
                    }
                }

                dgvDetail.CargarDatos(listadoDetallePorCodigoPersonalExterno.ToDataTable<SAS_PersonalExternoDetalleByIdResult>());
                dgvDetail.Refresh();



            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void CambiarEstadoItemDetalle()
        {
            try
            {

                if (dgvDetail.CurrentRow.Cells["chestado"].Value.ToString() == "1")
                {
                    dgvDetail.CurrentRow.Cells["chestado"].Value = "0";
                    dgvDetail.CurrentRow.Cells["chestadoDescripcion"].Value = "ANULADO";
                }
                else
                {
                    dgvDetail.CurrentRow.Cells["chestado"].Value = "1";
                    dgvDetail.CurrentRow.Cells["chestadoDescripcion"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
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

        private void Nuevo()
        {
            try
            {
                Limpiar();
                Editar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void Limpiar()
        {
            #region Limpiar() 
            try
            {
                #region variables en blanco()               
                item = new SAS_PersonalExterno();
                item.id = 0;
                item.idCodigoGeneral = string.Empty;
                item.dni = string.Empty;
                item.nombres = string.Empty;
                item.idAreaSolicita = string.Empty;
                item.idclieprov = string.Empty;
                item.empresa = string.Empty;
                item.fechaRegistro = (DateTime?)null;
                item.registradoPor = string.Empty;
                item.cargo = string.Empty;
                item.vigenciaDesde = (DateTime?)null;
                item.vigenciaHasta = (DateTime?)null;
                item.codigoFuncionarioAprueba = string.Empty;
                item.estado = 0;
                item.glosa = string.Empty;

                itemSelecionadoDeGrilla = new SAS_PersonalExternolistado();
                itemSelecionadoDeGrilla.id = 0;


                txtCodigo.Text = "0";
                txtEstado.Text = "ACTIVO";
                txtIdEstado.Text = "1";
                txtCodigoInterno.Text = string.Empty;
                txtNumeroDocumento.Text = string.Empty;
                txtNombresCompletos.Text = string.Empty;
                txtAreaCodigo.Text = string.Empty;
                txtAreaDescripcion.Text = string.Empty;
                txtCodigoProveedor.Text = string.Empty;
                txtProveedor.Text = string.Empty;
                txtEmpresa.Text = string.Empty;
                txtCargo.Text = string.Empty;
                txtFechaDesde.Text = string.Empty;
                txtFechaHasta.Text = string.Empty;
                txtUsuarioQueAprueba.Text = string.Empty;
                txtObservaciones.Text = string.Empty;
                #endregion
                ClearGridDetail();
                listadetalle = new List<SAS_PersonalExternoDetalle>();
                detalleEliminados = new List<SAS_PersonalExternoDetalle>();
                lastItem = 0;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
            #endregion
        }

        private void Editar()
        {
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnGrabar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = true;
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

        private void ObtenerObjeto()
        {

            try
            {
                item = new SAS_PersonalExterno();
                item.id = Convert.ToInt32(this.txtCodigo.Text);
                item.idCodigoGeneral = this.txtCodigoInterno.Text != string.Empty ? (this.txtCodigoInterno.Text).Trim() : string.Empty;
                item.dni = this.txtNumeroDocumento.Text != string.Empty ? (this.txtNumeroDocumento.Text).Trim() : string.Empty;
                item.nombres = this.txtNombresCompletos.Text != string.Empty ? (this.txtNombresCompletos.Text).Trim() : string.Empty;
                item.idAreaSolicita = this.txtAreaCodigo.Text != string.Empty ? (this.txtAreaCodigo.Text).Trim() : "010";
                item.idclieprov = this.txtCodigoProveedor.Text != string.Empty ? (this.txtCodigoProveedor.Text).Trim() : string.Empty;
                item.empresa = this.txtEmpresa.Text != string.Empty ? (this.txtEmpresa.Text).Trim() : string.Empty;
                item.fechaRegistro = DateTime.Now;
                item.registradoPor = _user2.IdUsuario;
                item.cargo = this.txtCargo.Text != string.Empty ? (this.txtCargo.Text).Trim() : string.Empty;

                item.vigenciaDesde =  (DateTime?)null;
                if (this.txtFechaDesde.Text != "  /  /")
                {
                    if (this.txtFechaDesde.Text != string.Empty)
                    {
                        item.vigenciaDesde = this.txtFechaDesde.Text != string.Empty ? Convert.ToDateTime((this.txtFechaDesde.Text).Trim()) : (DateTime?)null;
                    }
                }
                item.vigenciaHasta = (DateTime?)null;
                if (this.txtFechaHasta.Text != "  /  /")
                {
                    if (this.txtFechaHasta.Text != string.Empty)
                    {
                        item.vigenciaHasta = this.txtFechaHasta.Text != string.Empty ? Convert.ToDateTime((this.txtFechaHasta.Text).Trim()) : (DateTime?)null;
                    }
                }
                item.codigoFuncionarioAprueba = string.Empty;
                item.estado = 1;
                item.glosa = txtObservaciones.Text.Trim();

                #region Obtener detalle()
                listadetalle = new List<SAS_PersonalExternoDetalle>();
                if (this.dgvDetail != null)
                {
                    if (this.dgvDetail.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetail.Rows)
                        {
                            if (fila.Cells["chId"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_PersonalExternoDetalle oAcountDetail = new SAS_PersonalExternoDetalle();
                                    oAcountDetail.id = fila.Cells["chId"].Value != null ? Convert.ToInt32(fila.Cells["chId"].Value.ToString().Trim()) : 0;
                                    oAcountDetail.item = fila.Cells["chItem"].Value != null ? fila.Cells["chItem"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.idTipo = fila.Cells["chidTipo"].Value != null ? Convert.ToByte(fila.Cells["chidTipo"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oAcountDetail.link = fila.Cells["chlink"].Value != null ? fila.Cells["chlink"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.descripcion = fila.Cells["chdescripcion"].Value != null ? fila.Cells["chdescripcion"].Value.ToString().Trim() : string.Empty;
                                    oAcountDetail.estado = fila.Cells["chestado"].Value != null ? Convert.ToInt32(fila.Cells["chestado"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    oAcountDetail.creadoPor = _user2.IdUsuario;
                                    listadetalle.Add(oAcountDetail);
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
        }

        private void CambiarDeEstado()
        {
            try
            {

                ObtenerObjeto();
                #region Cambiar Estado
                if (item.idCodigoGeneral != string.Empty)
                {
                    Modelo =new SAS_PersonalExternoController();
                    int resultado = 0;
                    resultado = Modelo.ChangeStatus("SAS", item);
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
                    this.txtNumeroDocumento.Focus();
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void Eliminar()
        {
            try
            {
                if (_user2.IdUsuario.ToUpper().Trim() == "ADMINISTRADOR" || _user2.IdUsuario.ToUpper().Trim() == "EAURAZO")
                {
                    ObtenerObjeto();
                    #region Cambiar Estado
                    if (item.idCodigoGeneral != string.Empty)
                    {
                        Modelo =new SAS_PersonalExternoController();
                        int resultado = 0;
                        resultado = Modelo.Eliminate("SAS", item);
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
                        this.txtNumeroDocumento.Focus();
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }

        }

        private void radLabel7_Click(object sender, EventArgs e)
        {

        }

        private void PersonalExterno_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDetail_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo =new SAS_PersonalExternoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de detalle() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chtipo")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = Modelo.FeatureType("SAS");
                        search.Text = "Buscar tipo de característica";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDetail.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTipo"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetail.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chtipo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
