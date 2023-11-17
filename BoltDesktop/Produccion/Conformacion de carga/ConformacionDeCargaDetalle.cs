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
using Asistencia.Negocios.ProduccionPacking;

namespace ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga
{
    public partial class ConformacionDeCargaDetalle : Form
    {

        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoConformacionDeCargaPBIByIdResult selectedItemDetail;
        private List<SAS_ListadoConformacionDeCargaPBIByIdResult> resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
        SAS_CondormidadDeCargaController model;
        SAS_ConformacionDeCarga itemRegistar;
        SAS_ConformacionDeCarga itemDelete;
        List<SAS_ConformacionDeCargaDetalle> itemsDetalleEliminado;
        List<SAS_ConformacionDeCargaDetalle> detalleRegistro;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private int resultadoOperacion = 0;
        private int Id = 0;

        private int ParImparFiltro;

        #endregion

        public ConformacionDeCargaDetalle()
        {
            InitializeComponent();
            resultadoOperacion = 0;
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

        public ConformacionDeCargaDetalle(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege, int _Id)
        {
            InitializeComponent();
            resultadoOperacion = 0;
            Id = _Id;
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
            EjecutarConsultaBusquedaPorId(Id);
        }

        private void EjecutarConsultaBusquedaPorId(int id)
        {
            gbCabecera.Enabled = false;
            gbDetalle.Enabled = false;
            BarraPrincipal.Enabled = false;

            bgwHilo.RunWorkerAsync();
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
            ParImparFiltro += 1;
            ActivarFiltro();
        }

        private void ActivarFiltro()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvResultados.EnableFiltering = !true;
                dgvResultados.ShowHeaderCellButtons = !true;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvResultados.EnableFiltering = true;
                dgvResultados.ShowHeaderCellButtons = true;
                #endregion
            }
        }


        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            NoImplementado();
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaBusquedaAsincrona();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarConsultaBusquedaAsincrona();
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarProcesoGuardado();
        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarConsultaBusquedaAsincrona();
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            VistaPrevia();
        }

        private void VistaPrevia()
        {
            NoImplementado();
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
            NoImplementado();
        }

        private void NoImplementado()
        {
            RadMessageBox.SetThemeName(dgvResultados.ThemeName);
            RadMessageBox.Show(this, "No tiene privilegios para esta acción", "Respuesta del sistema", MessageBoxButtons.OK, RadMessageIcon.Info);
        }


        private void QuitarDetalle()
        {
            GridViewRowInfo[] selectedRows = new GridViewRowInfo[this.dgvResultados.SelectedRows.Count];
            this.dgvResultados.SelectedRows.CopyTo(selectedRows, 0);
            this.dgvResultados.TableElement.BeginUpdate();
            for (int i = 0; i < selectedRows.Length; i++)
            {
                this.dgvResultados.Rows.Remove(selectedRows[i] as GridViewDataRowInfo);
                itemsDetalleEliminado.Add(new SAS_ConformacionDeCargaDetalle { Id = (selectedRows[i].Cells["chid"].Value != null ? Convert.ToInt32(selectedRows[i].Cells["chid"].Value) : 0) });
            }

            this.dgvResultados.TableElement.EndUpdate();

        }

        private void ExonerarPallet()
        {

        }

        private void VerDetallePallet()
        {

        }

        private void RegistroSeleccionadoEnBlanco()
        {
            #region  RegistroSeleccionadoEnBlanco() 
            selectedItemDetail = new SAS_ListadoConformacionDeCargaPBIByIdResult();
            selectedItemDetail.Id = 0;
            selectedItemDetail.Semana = 0;
            selectedItemDetail.ConformacionCargaId = 0;
            selectedItemDetail.ConformacionCarga = string.Empty;
            selectedItemDetail.ConformacionCargaFecha = DateTime.Now;
            selectedItemDetail.NumeroContenedor = string.Empty;
            selectedItemDetail.Booking = string.Empty;
            selectedItemDetail.NumeroPackingList = string.Empty;
            selectedItemDetail.CampaniaId = string.Empty;
            selectedItemDetail.EstadoId = string.Empty;
            selectedItemDetail.IdRegistroPaleta = string.Empty;
            selectedItemDetail.NumeroPaleta = string.Empty;
            selectedItemDetail.TipoDePaletaId = string.Empty;
            selectedItemDetail.TipoDePaleta = string.Empty;
            selectedItemDetail.ClienteId = string.Empty;
            selectedItemDetail.Cliente = string.Empty;
            selectedItemDetail.GrowerCode = string.Empty;
            selectedItemDetail.EstadoDetalle = Convert.ToByte("1");
            selectedItemDetail.CultivoId = string.Empty;
            selectedItemDetail.Cultivo = string.Empty;
            selectedItemDetail.VariedadId = string.Empty;
            selectedItemDetail.Variedad = string.Empty;
            selectedItemDetail.EnvaseId = string.Empty;
            selectedItemDetail.Envase = string.Empty;
            selectedItemDetail.CategoriaId = string.Empty;
            selectedItemDetail.Categoria = string.Empty;
            selectedItemDetail.CalibreId = string.Empty;
            selectedItemDetail.Calibre = string.Empty;
            selectedItemDetail.ColorId = string.Empty;
            selectedItemDetail.Color = string.Empty;
            selectedItemDetail.PahiruelaId = string.Empty;
            selectedItemDetail.Pahiruela = string.Empty;
            selectedItemDetail.Pahiruela = string.Empty;
            selectedItemDetail.FechaProduccion = string.Empty;
            selectedItemDetail.CantidadCajas = 0;
            selectedItemDetail.PaletaOrigen = string.Empty;
            selectedItemDetail.traza = string.Empty;
            selectedItemDetail.pesoReferencial = 0;
            selectedItemDetail.EmbalajeId = string.Empty;
            selectedItemDetail.Embalaje = string.Empty;
            #endregion
        }

        private void EjecutarProcesoGuardado()
        {
            try
            {
                #region Consultar()
                model = new SAS_CondormidadDeCargaController();
                resultadoOperacion = model.ToRegister(connection, itemRegistar, detalleRegistro, itemsDetalleEliminado);

                if (resultadoOperacion > 0)
                {
                    resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                    model = new SAS_CondormidadDeCargaController();
                    resultListDetail = model.ListAllDetailById(connection, Id).ToList();
                }
                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void EjecutarConsultaBusquedaAsincrona()
        {
            try
            {
                #region Consultar()

                resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                model = new SAS_CondormidadDeCargaController();
                resultListDetail = model.ListAllDetailById(connection, Id).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;

            }
        }


        private void PresentarConsultaBusquedaAsincrona()
        {
            try
            {
                #region PresentarResultados()


                if (resultListDetail != null)
                {
                    txtFecha.Text = resultListDetail.ElementAt(0).ConformacionCargaFecha != null ? resultListDetail.ElementAt(0).ConformacionCargaFecha.ToShortDateString() : string.Empty;
                    txtEmpresaCodigoE.Text = resultListDetail.ElementAt(0).EmpresaID != null ? resultListDetail.ElementAt(0).EmpresaID.Trim() : string.Empty;
                    txtCampañaCodigo.Text = resultListDetail.ElementAt(0).CampaniaId != null ? resultListDetail.ElementAt(0).CampaniaId.Trim() : string.Empty;
                    txtProveedorCodigo.Text = resultListDetail.ElementAt(0).ClienteId != null ? resultListDetail.ElementAt(0).ClienteId.Trim() : string.Empty;
                    txtContenedor.Text = resultListDetail.ElementAt(0).NumeroContenedor != null ? resultListDetail.ElementAt(0).NumeroContenedor.Trim() : string.Empty;
                    txtBooking.Text = resultListDetail.ElementAt(0).Booking != null ? resultListDetail.ElementAt(0).Booking.Trim() : string.Empty;
                    txtIdEstado.Text = resultListDetail.ElementAt(0).EstadoId != null ? resultListDetail.ElementAt(0).EstadoId.Trim() : string.Empty;
                    txtDescripcion.Text = resultListDetail.ElementAt(0).ConformacionCarga != null ? resultListDetail.ElementAt(0).ConformacionCarga.Trim() : string.Empty;
                    txtObservaciones.Text = resultListDetail.ElementAt(0).Observacion != null ? resultListDetail.ElementAt(0).Observacion.Trim() : string.Empty;


                    txtCodigo.Text = resultListDetail.ElementAt(0).ConformacionCargaId != null ? resultListDetail.ElementAt(0).ConformacionCargaId.ToString().Trim() : "0";
                    txtEmpresaDescripcionE.Text = resultListDetail.ElementAt(0).Empresa != null ? resultListDetail.ElementAt(0).Empresa.Trim() : string.Empty;
                    txtCampañaDescripcion.Text = resultListDetail.ElementAt(0).Campania != null ? resultListDetail.ElementAt(0).Campania.Trim() : string.Empty;
                    txtProveedorDescripcion.Text = resultListDetail.ElementAt(0).Cliente != null ? resultListDetail.ElementAt(0).Cliente.Trim() : string.Empty;
                    txtEstado.Text = resultListDetail.ElementAt(0).EstadoConformidadCarga != null ? resultListDetail.ElementAt(0).EstadoConformidadCarga.Trim() : string.Empty;

                }

                #region Llenar Grilla detalle()                
                dgvResultados.DataSource = resultListDetail.ToDataTable<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                dgvResultados.Refresh();

                PintarResultadosEnGrilla();
                #endregion

                #region Limpiar Variables()                
                selectedItemDetail = new SAS_ListadoConformacionDeCargaPBIByIdResult();
                resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                model = new SAS_CondormidadDeCargaController();
                itemRegistar = new SAS_ConformacionDeCarga();
                itemDelete = new SAS_ConformacionDeCarga();
                detalleRegistro = new List<SAS_ConformacionDeCargaDetalle>();
                itemsDetalleEliminado = new List<SAS_ConformacionDeCargaDetalle>();
                #endregion

                if (resultadoOperacion > 0)
                {
                    RadMessageBox.Show(this, "Operacion realizada con éxito", "Confirmación del proceso", MessageBoxButtons.OK, RadMessageIcon.Info);
                }

                gbDetalle.Enabled = !true;
                BarraPrincipal.Enabled = true;
                gbCabecera.Enabled = false;
                gbDetalle.Enabled = true;
                btnNuevo.Enabled = true;
                btnAgregarDetalle.Enabled = true;
                btnFiltro.Enabled = true;
                btnResaltar.Enabled = true;
                btnAnular.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminarRegistro.Enabled = true;
                btnGrabar.Enabled = false;
                btnAtras.Enabled = false;
                pbResultado.Visible = false;
                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void LimpiarFomularioEdicion()
        {

            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, gbCabecera);
            modelExportToExcel.LimpiarControlesEnGrupoBox(this, gbDetalle);
            txtCodigo.Text = "0";
            txtIdEstado.Text = "PE";
            txtEstado.Text = "PENDIENTE";

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
            gbCabecera.Enabled = false;
            gbDetalle.Enabled = false;
            btnNuevo.Enabled = true;
            btnFiltro.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnGrabar.Enabled = false;
            btnAtras.Enabled = false;
            pbResultado.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void ElegirColumna()
        {
            this.dgvResultados.ShowColumnChooser();
        }

        private void Grabar()
        {
            #region Registrar()  
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtEstado.Text.Trim() != string.Empty && (this.txtIdEstado.Text.Trim() == "PE"))
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_ConformacionDeCarga();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    itemRegistar.Fecha = Convert.ToDateTime(this.txtFecha.Text.Trim());
                    itemRegistar.Descripcion = this.txtDescripcion.Text.Trim();
                    itemRegistar.NumeroContenedor = (this.txtContenedor.Text.Trim());
                    itemRegistar.Booking = (this.txtBooking.Text.Trim());
                    itemRegistar.Observacion = (this.txtContenedor.Text.Trim());
                    itemRegistar.NumeroContenedor = (this.txtObservaciones.Text.Trim());
                    itemRegistar.FechaRegistro = DateTime.Now;
                    itemRegistar.UserId = user.IdUsuario != null ? user.IdUsuario : Environment.UserName;
                    itemRegistar.Hostname = Environment.MachineName.ToString();
                    itemRegistar.EstadoId = (this.txtIdEstado.Text.Trim());
                    itemRegistar.idCampania = this.txtCampañaCodigo.Text.Trim();
                    itemRegistar.IdClieprov = this.txtProveedorCodigo.Text.Trim();


                    detalleRegistro = new List<SAS_ConformacionDeCargaDetalle>();
                    foreach (GridViewRowInfo rowInfo in dgvResultados.Rows)
                    {
                        SAS_ConformacionDeCargaDetalle detail = new SAS_ConformacionDeCargaDetalle();
                        detail.IdConformacionCarga = itemRegistar.Id;
                        detail.Id = rowInfo.Cells["chId"].Value != null ? Convert.ToInt32(rowInfo.Cells["chId"].Value.ToString().Trim()) : 0;
                        detail.IdRegistroPaleta = rowInfo.Cells["chIdRegistroPaleta"].Value != null ? rowInfo.Cells["chIdRegistroPaleta"].Value.ToString().Trim() : string.Empty;
                        detail.Estado = rowInfo.Cells["chEstadoDetalle"].Value != null ? Convert.ToByte(rowInfo.Cells["chEstadoDetalle"].Value.ToString().Trim()) : Convert.ToByte(1);
                        detalleRegistro.Add(detail);
                    }



                    gbCabecera.Enabled = false;
                    gbDetalle.Enabled = false;
                    pbResultado.Visible = true;
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
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, "El documento no tiene el estado para edición", "Advertencia del sistema", MessageBoxButtons.OK, RadMessageIcon.Error);
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


            //if (this.txtCampañaDescripcion.Text.Trim() == string.Empty)
            //{
            //    status = false;
            //}

            //if (this.txtClienteId.Text.Trim() == string.Empty)
            //{
            //    status = false;
            //}


            //if (this.txtCliente.Text.Trim() == string.Empty)
            //{
            //    status = false;
            //}

            //if (this.txtDesde.Text.ToString().Trim() == ASCD)
            //{
            //    status = false;
            //}


            //if (this.txtHasta.Text.ToString().Trim() == ASCD)
            //{
            //    status = false;
            //}


            //if (this.txtTipoTermoRegistro.Text.ToString().Trim() == string.Empty)
            //{
            //    status = false;
            //}


            //if (this.txtTipoTermoRegistroId.Text.ToString().Trim() == string.Empty)
            //{
            //    status = false;
            //}


            return status;
        }

        private void Eliminar()
        {
            #region Eliminar()  
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtEstado.Text.Trim() != string.Empty && this.txtIdEstado.Text.Trim() != "0")
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_ConformacionDeCarga();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    model = new SAS_CondormidadDeCargaController();
                    if (model.ToDelete(connection, itemRegistar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbDetalle.Enabled = false;
                        gbDetalle.Enabled = true;
                        btnNuevo.Enabled = true;
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
            modelExportToExcel.ExportarToExcel(dgvResultados, saveFileDialog);
        }

        private void Anular()
        {
            #region Anular()  
            if (this.txtCodigo.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_ConformacionDeCarga();
                    itemRegistar.Id = Convert.ToInt32(this.txtCodigo.Text);
                    model = new SAS_CondormidadDeCargaController();
                    if (model.ToChangeStatus(connection, itemRegistar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbDetalle.Enabled = false;
                        gbDetalle.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnFiltro.Enabled = true;
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
            gbDetalle.Enabled = false;
            gbDetalle.Enabled = true;
            btnNuevo.Enabled = true;
            btnFiltro.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnGrabar.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvResultados.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvResultados.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvResultados.MasterTemplate.AutoExpandGroups = true;
            this.dgvResultados.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.GroupDescriptors.Clear();
            this.dgvResultados.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chNumeroPaleta", "# Reg. : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chpesoReferencial", "Sum. : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chCantidadCajas", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvResultados.MasterTemplate.SummaryRowsTop.Add(items1);
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
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PALLET COMPLETO", string.Empty, true);
                c1.RowBackColor = Color.MintCream;
                c1.CellBackColor = Color.MintCream;
                //c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvResultados.Columns["chTipoDePaleta"].ConditionalFormattingObjectList.Add(c1);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PALLET MIXTO REMONTE", string.Empty, true);
                c2.RowBackColor = Color.OldLace;
                c2.CellBackColor = Color.OldLace;
                //c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvResultados.Columns["chTipoDePaleta"].ConditionalFormattingObjectList.Add(c2);

                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PALLET COMPLETO", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                // c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvResultados.Columns["chTipoDePaleta"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PALLET MIXTO REMONTE", string.Empty, true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                //c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvResultados.Columns["chTipoDePaleta"].ConditionalFormattingObjectList.Add(c2);

                #endregion
            }
        }

        private void Editar()
        {
            if (this.txtEstado.Text != null)
            {
                if ((this.txtIdEstado.Text != "AN") || (this.txtIdEstado.Text != "FN") || (this.txtIdEstado.Text != "C0") || (this.txtIdEstado.Text != "RE"))
                {
                    btnAgregarDetalle.Enabled = true;
                    btnQuitarDetalle.Enabled = true;
                    btnIrAConsolidado.Enabled = true;
                    gbCabecera.Enabled = true;
                    gbDetalle.Enabled = true;
                    btnEditar.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnFiltro.Enabled = true;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
                    btnGrabar.Enabled = true;
                    btnAtras.Enabled = true;
                }
                else
                {
                    RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                    RadMessageBox.Show(this, "El documento no tiene el estado para edición", "Advertencia del sistema", MessageBoxButtons.OK, RadMessageIcon.Error);
                    return;
                }
            }
        }

        private void Nuevo()
        {
            LimpiarFomularioEdicion();
            //gbEdit.Enabled = true;
            //gbDetalle.Enabled = false;
            //GbPeriodo.Enabled = true;
            //gbTipoTermoRegistro.Enabled = true;
            //btnNuevo.Enabled = false;
            //btnEditar.Enabled = false;
            //btnActualizar.Enabled = false;
            //btnAnular.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            //btnRegistrar.Enabled = true;
            //btnAtras.Enabled = true;

        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvResultados.EnableFiltering = !true;
                dgvResultados.ShowHeaderCellButtons = !true;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvResultados.EnableFiltering = true;
                dgvResultados.ShowHeaderCellButtons = true;
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
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtEstado.Text.Trim() != string.Empty && (this.txtIdEstado.Text.Trim() == "PE"))
            {

            }
            else
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, "El documento no tiene el estado para agregar paletas al registro", "Advertencia del sistema", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {
            NoImplementado();
        }

        private void dgvResultados_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
