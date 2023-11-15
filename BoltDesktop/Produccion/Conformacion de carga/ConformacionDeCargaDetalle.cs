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
using MyDataGridViewColumns;
using System.Drawing;
using MyControlsDataBinding.Controles;
using Asistencia.Negocios.Calidad;



namespace ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga
{
    public partial class ConformacionDeCargaDetalle : Form
    {

        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoTermoRegistroPorClienteCampanaAllResult selectedItem;
        private List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult> resultList;
        TermoRegistroPorClienteCampanaController model;
        SAS_TermoRegistroPorClienteCampana itemRegistar;
        SAS_TermoRegistroPorClienteCampana itemDelete;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private int resultadoOperacion;
        #endregion


        public ConformacionDeCargaDetalle()
        {
            InitializeComponent();
            RegistroSeleccionadoEnBlanco();
            connection = "SAS";
            user = new SAS_USUARIOS();
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            userLogin = user;
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();
        }

        public ConformacionDeCargaDetalle(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            connection = _connection;
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            //lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            //lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            //Inicio();
            //Actualizar();
        }



        private void btnAgregarDetalleIP_Click(object sender, EventArgs e)
        {
            AgregarDetalle();
        }

        private void btnQuitarDetalleIP_Click(object sender, EventArgs e)
        {
            QuitarDetalle();
        }

        private void btnAgregarDetalleActivarIP_Click(object sender, EventArgs e)
        {
            IrAConsolidado();
        }

       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
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

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnResaltar_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {

        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {

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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        private void ConformacionDeCargaDetalle_Load(object sender, EventArgs e)
        {

        }

        private void exonerarLiberarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExonerarPallet();
        }
        private void ConformacionDeCargaDetalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVerDetallePallet_Click(object sender, EventArgs e)
        {
            VerDetallePallet();
        }


        #region Funciones()

        private void AgregarDetalle() 
        {

        }

        private void QuitarDetalle()
        {

        }

        private void ExonerarPallet()
        {

        }

        private void VerDetallePallet()
        {

        }

        private void RegistroSeleccionadoEnBlanco()
        {
            selectedItem = new SAS_ListadoTermoRegistroPorClienteCampanaAllResult();
            selectedItem.Id = 0;
            selectedItem.TipoTermoRegistroId = 0;
            selectedItem.TipoTermoRegistro = string.Empty;
            selectedItem.desde = DateTime.Now;
            selectedItem.hasta = DateTime.Now;
            selectedItem.Empresa = string.Empty;
            selectedItem.EmpresaId = string.Empty;
            selectedItem.Campana = string.Empty;
            selectedItem.CampanaId = string.Empty;
            selectedItem.Glosa = string.Empty;
            selectedItem.Estado = Convert.ToByte("1");
        }

        private void EjecutarProcesoGuardado()
        {
            try
            {
                #region Consultar()

                model = new TermoRegistroPorClienteCampanaController();
                resultadoOperacion = model.ToRegister(connection, itemRegistar);

                if (resultadoOperacion > 0)
                {
                    resultList = new List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult>();
                    model = new TermoRegistroPorClienteCampanaController();
                    resultList = model.ListAll(connection).ToList();
                }

                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvRegistro.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;

            }
        }

        private void PresentarConsultaBusqueda()
        {
            try
            {
                #region PresentarResultados()

                if (resultadoOperacion > 0)
                {
                    RadMessageBox.Show(this, "Operacion realizada con éxito", "Confirmación del proceso", MessageBoxButtons.OK, RadMessageIcon.Info);
                }

                dgvRegistro.DataSource = resultList;
                dgvRegistro.Refresh();


                GbPeriodo.Enabled = !true;
                gbTipoTermoRegistro.Enabled = !true;
                PintarResultadosEnGrilla();
                BarraPrincipal.Enabled = true;
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = true;
                btnAnular.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminarRegistro.Enabled = true;
                btnRegistrar.Enabled = false;
                btnAtras.Enabled = false;
                pgBar.Visible = false;


                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvRegistro.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }
        private void EjecutarConsultaBusqueda()
        {
            try
            {
                #region Consultar()

                resultList = new List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult>();
                model = new TermoRegistroPorClienteCampanaController();
                resultList = model.ListAll(connection).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvRegistro.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;

            }
        }

        private void LimpiarFomularioEdicion()
        {

            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, gbEdit);
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, GbPeriodo);
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, gbTipoTermoRegistro);
            txtCodigo.Text = "0";
            txtIdEstado.Text = "1";
            txtEstado.Text = "ACTIVO";

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
            btnEliminarRegistro.Enabled = true;
            btnRegistrar.Enabled = false;
            btnAtras.Enabled = false;
            pgBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void ElegirColumna()
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void Grabar()
        {
            #region Registrar()  
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtEstado.Text.Trim() != string.Empty && this.txtIdEstado.Text.Trim() != "0")
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_TermoRegistroPorClienteCampana();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    itemRegistar.EmpresaId = this.txtEmpresaCodigoE.Text.Trim();
                    itemRegistar.CampanaId = this.txtCampañaCodigo.Text.Trim();
                    itemRegistar.ClienteId = (this.txtClienteId.Text.Trim());
                    itemRegistar.TipoTermoRegistro = Convert.ToInt32(this.txtTipoTermoRegistroId.Text.Trim());
                    itemRegistar.Desde = Convert.ToDateTime(this.txtDesde.Text.Trim());
                    itemRegistar.Hasta = Convert.ToDateTime(this.txtHasta.Text.Trim());
                    itemRegistar.Estado = Convert.ToByte(1);
                    itemRegistar.VisibleEnReportes = chkVisibleEnReportes.Checked == true ? 1 : 0;
                    itemRegistar.Usuario = user.IdUsuario;
                    itemRegistar.FechaCreacion = DateTime.Now;
                    itemRegistar.Glosa = this.txtGlosa.Text.Trim();
                    itemRegistar.HostName = Environment.MachineName.ToString();
                    gbEdit.Enabled = false;
                    gbList.Enabled = false;
                    GbPeriodo.Enabled = false;
                    pgBar.Visible = true;
                    bgwRegistrar.RunWorkerAsync();
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
            string ASCD = this.txtValidar.Text.ToString().Trim();

            bool status = true;
            if (this.txtCodigo.Text.Trim() == string.Empty)
            {
                status = false;
            }

            if (this.txtEstado.Text.Trim() == string.Empty)
            {
                status = false;
            }



            if (this.txtEmpresaCodigoE.Text.Trim() == string.Empty)
            {
                status = false;
            }


            if (this.txtEmpresaDescripcionE.Text.Trim() == string.Empty)
            {
                status = false;
            }



            if (this.txtCampañaCodigo.Text.Trim() == string.Empty)
            {
                status = false;
            }


            if (this.txtCampañaDescripcion.Text.Trim() == string.Empty)
            {
                status = false;
            }

            if (this.txtClienteId.Text.Trim() == string.Empty)
            {
                status = false;
            }


            if (this.txtCliente.Text.Trim() == string.Empty)
            {
                status = false;
            }

            if (this.txtDesde.Text.ToString().Trim() == ASCD)
            {
                status = false;
            }


            if (this.txtHasta.Text.ToString().Trim() == ASCD)
            {
                status = false;
            }


            if (this.txtTipoTermoRegistro.Text.ToString().Trim() == string.Empty)
            {
                status = false;
            }


            if (this.txtTipoTermoRegistroId.Text.ToString().Trim() == string.Empty)
            {
                status = false;
            }


            return status;
        }

        private void Eliminar()
        {
            #region Eliminar()  
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtEstado.Text.Trim() != string.Empty && this.txtIdEstado.Text.Trim() != "0")
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_TermoRegistroPorClienteCampana();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    model = new TermoRegistroPorClienteCampanaController();
                    if (model.ToDelete(connection, itemRegistar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
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
            if (this.txtCodigo.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_TermoRegistroPorClienteCampana();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    model = new TermoRegistroPorClienteCampanaController();
                    if (model.ToChangeStatus(connection, itemRegistar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
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
            btnEliminarRegistro.Enabled = true;
            btnRegistrar.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void EjecuarConsultaAsincrona()
        {

            try
            {
                #region Consultar()

                resultList = new List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult>();
                model = new TermoRegistroPorClienteCampanaController();
                resultList = model.ListAll(connection).ToList();
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
            this.dgvRegistros.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistros.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistros.GroupDescriptors.Clear();
            this.dgvRegistros.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chTipoTermoRegistro", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistros.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void PresentarResultados()
        {
            try
            {
                #region PresentarResultados()
                dgvRegistros.DataSource = resultList;
                dgvRegistros.Refresh();

                PintarResultadosEnGrilla();
                BarraPrincipal.Enabled = true;
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = true;
                btnAnular.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminarRegistro.Enabled = true;
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
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
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
                    GbPeriodo.Enabled = true;
                    gbTipoTermoRegistro.Enabled = true;
                    gbEdit.Enabled = true;
                    gbList.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnActualizar.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
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
            GbPeriodo.Enabled = true;
            gbTipoTermoRegistro.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = true;

        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                dgvRegistro.ShowHeaderCellButtons = !true;
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

        private void IrAConsolidado()
        {

        }

       


        #endregion


    }
}
