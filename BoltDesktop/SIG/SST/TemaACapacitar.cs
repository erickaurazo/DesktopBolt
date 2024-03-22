﻿using System;
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
using MyDataGridViewColumns;
using System.Drawing;
using Asistencia.Negocios.SIG.SST;
using System.Collections;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class TemaACapacitar : Form
    {
        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private Tema selectedItem;
        private List<Tema> resultList;
        private TemaACapacitarControllers model = new TemaACapacitarControllers();
        private Tema oTemaARegistrar;
        private List<TemaArea> ListadoDetalleAEliminar = new List<TemaArea>();
        private List<TemaArea> ListadoDetalleARegistrar = new List<TemaArea>();
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SSOMA";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private List<SAS_ListadoTemasParaCapacitacionByTemaIdResult> ListadoDetalleSelecionado;
        #endregion


        public TemaACapacitar()
        {
            InitializeComponent();
            connection = "SSOMA";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();
        }

        public TemaACapacitar(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            connection = _connection;
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();
        }



        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnActivarfiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro = ClickFiltro + 1;
            ActivateFilter();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registrar();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Historial();
        }

        private void btnFlujoAprobacion_Click(object sender, EventArgs e)
        {
            FlujoAprobacion();

        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        private void btnNotificar_Click(object sender, EventArgs e)
        {
            Notificar();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ClickResaltarResultados = ClickResaltarResultados + 1;
            ResaltarResultados();
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Registrar();
        }

        private void Tema_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecuarConsultaAsincrona();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        private void Tema_Load(object sender, EventArgs e)
        {

        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            #region Selection Changed() 
            selectedItem = new Tema();
            ListadoDetalleSelecionado = new List<SAS_ListadoTemasParaCapacitacionByTemaIdResult>();
            selectedItem.TemaID = string.Empty;
            selectedItem.FrecuenciaCapacitacion = 0;
            selectedItem.Descripcion = string.Empty;
            selectedItem.Estado = false;
            LimpiarFomularioEdicion();

            if (dgvRegistro.Rows.Count > 0)
            {
                if (dgvRegistro.CurrentRow != null && dgvRegistro.CurrentRow.Cells["chTemaID"].Value != null)
                {
                    string TemaID = dgvRegistro.CurrentRow.Cells["chTemaID"].Value != null ? Convert.ToString(dgvRegistro.CurrentRow.Cells["chTemaID"].Value.ToString().Trim()) : string.Empty;
                    var resultByFilterId = resultList.Where(x => x.TemaID.ToString().Trim() == TemaID).ToList();
                    if (resultByFilterId.ToList().Count == 1)
                    {
                        selectedItem = new Tema();
                        selectedItem = resultByFilterId.ElementAt(0);
                        txtTemaID.Text = selectedItem.TemaID.ToString();
                        txtIdEstado.Text = selectedItem.Estado == true ? "1" : "0";
                        txtEstado.Text = selectedItem.Estado == true ? "ACTIVO" : "ANULADO";
                        txtDescripcion.Text = selectedItem.Descripcion.ToString().Trim();
                        txtFrecuencia.Text = selectedItem.FrecuenciaCapacitacion.ToDecimalPresentation();
                        model = new TemaACapacitarControllers();
                        ListadoDetalleSelecionado = model.ObtenerListadoTemasParaCapacitacionByTemaId(connection, selectedItem.TemaID).ToList();
                        dgvDetalle.CargarDatos(ListadoDetalleSelecionado.ToDataTable<SAS_ListadoTemasParaCapacitacionByTemaIdResult>());
                        dgvDetalle.Refresh();
                    }
                }
            }
            #endregion
        }


        #region Funciones()
        private void LimpiarFomularioEdicion()
        {
            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, gbEdit);
            txtTemaID.Text = "000";
            txtIdEstado.Text = "1";
            txtEstado.Text = "ACTIVO";

            ListadoDetalleSelecionado = new List<SAS_ListadoTemasParaCapacitacionByTemaIdResult>();
            ListadoDetalleSelecionado = model.ObtenerListadoTemasParaCapacitacionByTemaId(connection, "").ToList();
            dgvDetalle.CargarDatos(ListadoDetalleSelecionado.ToDataTable<SAS_ListadoTemasParaCapacitacionByTemaIdResult>());
            dgvDetalle.Refresh();
            ListadoDetalleAEliminar = new List<TemaArea>();
            ListadoDetalleARegistrar = new List<TemaArea>();

            //txtDescripcion.Clear();
            //this.txtFormulario.Clear();
            //this.txtFormularioCodigo.Clear();
            //this.txtOrden.Clear();
            //this.chkVisibleEnReportes.Checked = true;
            //this.txtSeccion.Clear();

            //txtFormularioCodigo.Focus();
        }

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void Actualizar()
        {
            BarraPrincipal.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarR.Enabled = true;
            btnRegistrar.Enabled = false;
            btnAtras.Enabled = false;
            pgBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void ElegirColumna()
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void Registrar()
        {
            #region Registrar()  
            if (this.txtTemaID.Text.Trim() != string.Empty && this.txtDescripcion.Text.Trim() != string.Empty && this.txtIdEstado.Text.Trim() != "0")
            {
                if (ValidateForm() == true)
                {
                    oTemaARegistrar = new Tema();
                    oTemaARegistrar.TemaID = Convert.ToString(this.txtTemaID.Text);
                    oTemaARegistrar.Descripcion = this.txtDescripcion.Text.Trim();
                    oTemaARegistrar.Estado = this.txtIdEstado.Text.Trim() == "1" ? true : false;
                    oTemaARegistrar.FrecuenciaCapacitacion = Convert.ToDecimal(this.txtFrecuencia.Value);

                    #region Obtener listado Detalle()


                    ListadoDetalleARegistrar = new List<TemaArea>();
                    if (this.dgvDetalle != null)
                    {
                        if (this.dgvDetalle.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                            {
                                if (fila.Cells["chTemaAreaId"].Value.ToString().Trim() != String.Empty)
                                {
                                    if (fila.Cells["chTemaIDDetalle"].Value.ToString().Trim() != String.Empty)
                                    {
                                        if (fila.Cells["chAreaID"].Value.ToString().Trim() != String.Empty)
                                        {
                                            try
                                            {
                                                #region Obtener detalle por linea detalle() 
                                                TemaArea oItem = new TemaArea();
                                                oItem.TemaAreaId = fila.Cells["chTemaAreaId"].Value != null ? Convert.ToInt32(fila.Cells["chTemaAreaId"].Value.ToString().Trim()) : 0;
                                                oItem.TemaID = fila.Cells["chTemaIDDetalle"].Value != null ? fila.Cells["chTemaIDDetalle"].Value.ToString().Trim() : string.Empty;
                                                oItem.AreaID = fila.Cells["chAreaID"].Value != null ? Convert.ToString(fila.Cells["chAreaID"].Value) : string.Empty;
                                                oItem.Estado = fila.Cells["chEstadoDetalle"].Value != null ?  Convert.ToByte( fila.Cells["chEstadoDetalle"].Value.ToString().Trim()) : Convert.ToByte(0);
                                                #endregion
                                                ListadoDetalleARegistrar.Add(oItem);
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
                        }
                    }
                    #endregion

                  

                    model = new TemaACapacitarControllers();
                    if (model.Registrar(connection, oTemaARegistrar, ListadoDetalleAEliminar, ListadoDetalleARegistrar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        ListadoDetalleAEliminar = new List<TemaArea>();
                        ListadoDetalleARegistrar = new List<TemaArea>();
                        selectedItem = new Tema();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarR.Enabled = true;
                        btnGrabar.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
            #endregion
        }

        private bool ValidateForm()
        {
            bool status = true;
            if (this.txtTemaID.Text.Trim() == string.Empty)
            {
                status = false;
            }

            if (this.txtDescripcion.Text.Trim() == string.Empty)
            {
                status = false;
            }


            if (Convert.ToInt32(this.txtFrecuencia.Value) < 1)
            {
                status = false;
            }
            if (Convert.ToInt32(this.txtFrecuencia.Value) > 720)
            {
                status = false;
            }

            return status;
        }

        private void Eliminar()
        {
            #region Eliminar()  
            if (this.txtTemaID.Text.Trim() != string.Empty && this.txtDescripcion.Text.Trim() != string.Empty && this.txtIdEstado.Text.Trim() != "0")
            {
                if (ValidateForm() == true)
                {
                    oTemaARegistrar = new Tema();
                    oTemaARegistrar.TemaID = Convert.ToString(this.txtTemaID.Text);
                    model = new TemaACapacitarControllers();
                    if (model.Eliminar(connection, oTemaARegistrar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarR.Enabled = true;
                        btnGrabar.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
            #endregion
        }

        private void Exportar()
        {
            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.ExportarToExcel(dgvRegistro, saveFileDialog);
        }

        private void Anular()
        {
            #region Anular()  
            if (this.txtTemaID.Text.Trim() != string.Empty && this.txtDescripcion.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    oTemaARegistrar = new Tema();
                    oTemaARegistrar.TemaID = Convert.ToString(this.txtTemaID.Text);
                    model = new TemaACapacitarControllers();
                    if (model.Anular(connection, oTemaARegistrar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarR.Enabled = true;
                        btnGrabar.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;

                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
            #endregion
        }

        private void Atras()
        {
            LimpiarFomularioEdicion();
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarR.Enabled = true;
            btnRegistrar.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void EjecuarConsultaAsincrona()
        {

            try
            {
                #region Consultar()

                resultList = new List<Tema>();
                model = new TemaACapacitarControllers();
                resultList = model.ListadoDeTemasEnCapacitacion(connection).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
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
            items1.Add(new GridViewSummaryItem("chDescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void PresentarResultados()
        {
            try
            {
                #region PresentarResultados()
                dgvRegistro.DataSource = resultList;
                dgvRegistro.Refresh();

                PintarResultadosEnGrilla();
                BarraPrincipal.Enabled = true;
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = true;
                btnAnular.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminarR.Enabled = true;
                btnRegistrar.Enabled = false;
                btnAtras.Enabled = false;
                pgBar.Visible = true;


                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void ResaltarResultados()
        {
            ClickResaltarResultados += 1;
            PintarResultadosEnGrilla();
        }

        private void PintarResultadosEnGrilla()
        {

            if ((ClickResaltarResultados % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }
        }

        private void Editar()
        {
            if (this.txtEstado.Text != null)
            {
                if (this.txtEstado.Text != "ANULADO")
                {
                    gbEdit.Enabled = true;
                    gbList.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnActualizar.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarR.Enabled = false;
                    btnRegistrar.Enabled = true;
                    btnAtras.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El documento no tiene el estado para la edición", "Confirmación del sistema");
                    return;
                }
            }
        }

        private void Nuevo()
        {
            LimpiarFomularioEdicion();
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarR.Enabled = false;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = true;

        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                dgvRegistro.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistro.EnableFiltering = true;
                dgvRegistro.ShowHeaderCellButtons = true;
                #endregion
            }
        }

        private void Historial()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void FlujoAprobacion()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void Adjuntar()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void Notificar()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void CambiarEstadoDetalle()
        {
            try
            {

                if (dgvDetalle.CurrentRow.Cells["chEstadoDetalle"].Value.ToString() == "1")
                {
                    dgvDetalle.CurrentRow.Cells["chEstadoDetalle"].Value = false;
                    //dgvDetalle.CurrentRow.Cells["chEstadoSW"].Value = "INACTIVO";
                }
                else
                {
                    dgvDetalle.CurrentRow.Cells["chEstadoDetalle"].Value = true;
                    //dgvDetalle.CurrentRow.Cells["chEstadoSW"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void AddItemDetalle()
        {
            try
            {
                if (dgvDetalle != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(0)); // TemaAreaId                                     
                    array.Add(this.txtTemaID.Text); // TemaID
                    array.Add(string.Empty); // AreaID   
                    array.Add(string.Empty); // Area  
                    array.Add(string.Empty); // Tema
                    array.Add(Convert.ToInt32(1)); // Estado


                    dgvDetalle.AgregarFila(array);

                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void RemoveItemDetalle()
        {
            if (this.dgvDetalle != null)
            {
                #region
                if (dgvDetalle.CurrentRow != null && dgvDetalle.CurrentRow.Cells["chTemaAreaId"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {
                        Int32 dispositivoCodigo = (dgvDetalle.CurrentRow.Cells["chTemaAreaId"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalle.CurrentRow.Cells["chTemaAreaId"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {

                            if (dispositivoCodigo != 0)
                            {

                                ListadoDetalleAEliminar.Add(new TemaArea
                                {
                                    TemaAreaId = dispositivoCodigo,
                                });
                            }
                        }

                        dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
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


        #endregion

        private void btnSoftwareChangeStatus_Click(object sender, EventArgs e)
        {
            CambiarEstadoDetalle();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            AddItemDetalle();
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            RemoveItemDetalle();
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            model = new TemaACapacitarControllers();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chAreaID" || ((DataGridView)sender).CurrentCell.OwningColumn.Name == "chArea")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = model.ObtnerListadoDeAreas("SAS");
                        search.Text = "Buscar área administrativas de trabajo";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chAreaID"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chArea"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }
        }
    }
}
