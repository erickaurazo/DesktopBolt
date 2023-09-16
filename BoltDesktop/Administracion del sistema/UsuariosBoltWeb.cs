using Asistencia.Datos;
using Asistencia.Helper;
using Asistencia.Negocios;
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    public partial class UsuariosBoltWeb : Form
    {

        private ExportToExcelHelper modelExportToExcel;

        private SAS_UsuariosBoltController Model;
        //private ASJ_USUARIOS userSelect;
        private List<Grupo> listAreaByComboBox;
        private List<Grupo> listStatudByComboBox;
        private ComboBoxHelper modelComboBox;
        private string connection = "SAS";
        private string companyId = "001";
        private List<SAS_ListadoCuentasBoltWeb> ListUserBoltWeb;
        private SAS_ListadoCuentasBoltWeb UserBoltWeb;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private usuarioMaquinaria userToRegister;
       
        private string direcionLogica = @"C:\SOLUTION\firmas";
        private string imagen = string.Empty;
        private string imagen2 = string.Empty;
        private List<SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos> ListadoUsuariosNISIRAYBolt;
        private SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos UsuariosNISIRAYBolt;
        private int ParImpar = 0;
        public int ParImparFiltro = 0;

        public UsuariosBoltWeb()
        {
            InitializeComponent();
            connection = "SAS";
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
            RefreshList();

            btnValidarDatos.Enabled = false;

        }


        public UsuariosBoltWeb(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            connection = (_connection == "NSFAJAS"? "SAS": _connection);
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            RefreshList();
        }

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[connection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

                Model = new SAS_UsuariosBoltController();
                listAreaByComboBox = new List<Grupo>();
                listAreaByComboBox = Model.GetComboBoxArea();

                listStatudByComboBox = new List<Grupo>();
                listStatudByComboBox = Model.GetComboBoxPerfil();


                cboArea.DataSource = listAreaByComboBox;
                cboArea.ValueMember = "Codigo";
                cboArea.DisplayMember = "Descripcion";
                //                cboArea.SelectedValue = "000";

                cboPefil.DataSource = listStatudByComboBox;
                cboPefil.ValueMember = "Codigo";
                cboPefil.DisplayMember = "Descripcion";
                //              cboPefil.SelectedValue = "1";




            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvList.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvList.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvList.MasterTemplate.AutoExpandGroups = true;
            this.dgvList.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvList.GroupDescriptors.Clear();
            this.dgvList.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chColaboradorNombresCompletos", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvList.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ParImparFiltro += 1;
            ActivateFilter();
        }
        private void ActivateFilter()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvList.EnableFiltering = true;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvList.EnableFiltering = !true;
                #endregion
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void NewRegister()
        {
            LimpiarFormularioDeEdicion();
            this.txtIdentificador.Text = "0";
            txtEstadoDescripcion.Text = "ACTIVO";
            cboArea.SelectedValue = "ITD";
            cboPefil.SelectedValue = "S";
            btnValidarDatos.Enabled = true;
            gbEdition.Enabled = true;
            gbList.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizarLista.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnSave.Enabled = true;
            btnAtras.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            if (this.txtEstadoDescripcion.Text != null)
            {
                if (this.txtEstadoDescripcion.Text != "ANULADO")
                {
                    gbEdition.Enabled = true;
                    gbList.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnActualizarLista.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
                    btnSave.Enabled = true;
                    btnAtras.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El documento no tiene el estado para la edición", "Confirmación del sistema");
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            #region Registrar()  
            if (this.txtIdentificador.Text.Trim() != string.Empty && this.txtCuentaUsuario.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    userToRegister = new usuarioMaquinaria();
                    userToRegister.area = cboArea.SelectedValue.ToString().Trim();
                    userToRegister.clave = this.txtPassword.Text.Trim();
                    userToRegister.codigoTrabajador = txtPassword.Text.Trim();
                    userToRegister.email = this.txtEmail.Text.Trim();
                    userToRegister.FIRMA = null;
                    userToRegister.estado = this.txtEstadoDescripcion.Text == "ACTIVO" ? '1' : '0';                                        
                    if (this.txtRuta.Text.Trim() != string.Empty)
                    {
                        MemoryStream obj = new MemoryStream();
                        pbFirma.Image.Save(obj, ImageFormat.Png);
                        byte[] archivo = obj.ToArray();
                        userToRegister.FIRMA = archivo;
                    }                                       
                    userToRegister.idUsuarioMaquinaria = Convert.ToInt32(this.txtIdentificador.Text);
                    userToRegister.nombres = this.txtNombreColaborador.Text;
                    userToRegister.nroDocumento = this.txtNumeroDocumento.Text.Trim();
                    userToRegister.perfil = cboPefil.SelectedValue.ToString().Trim();
                    userToRegister.RutaDeFirma = this.txtRuta.Text.Trim();
                    userToRegister.usuario = this.txtCuentaUsuario.Text.Trim();
                    if (Model.ToRegisterUserBoltWeb(connection, userToRegister) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        gbEdition.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizarLista.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
                        btnSave.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;
                        btnValidarDatos.Enabled = false;
                        RefreshList();
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
            if (cboArea.SelectedValue != null && cboArea.SelectedValue.ToString().Trim() == "0")
            {
                status = false;
            }


            if (cboPefil.SelectedValue != null && cboPefil.SelectedValue.ToString().Trim() == "0")
            {
                status = false;
            }

            return status;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Toback();
        }

        private void Toback()
        {
            LimpiarFormularioDeEdicion();
            gbEdition.Enabled = false;
            gbList.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            if (this.txtIdentificador.Text.Trim() != string.Empty && this.txtCuentaUsuario.Text.Trim() != string.Empty)
            {
                userToRegister = new usuarioMaquinaria();
                userToRegister.idUsuarioMaquinaria = Convert.ToInt32(this.txtIdentificador.Text.Trim());
                if (Model.ToChangeStatusUserBoltWeb(connection, userToRegister) > 0)
                {
                    MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                    gbEdition.Enabled = false;
                    gbList.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnActualizarLista.Enabled = true;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnSave.Enabled = false;
                    btnEditar.Enabled = true;
                    btnAtras.Enabled = false;
                    RefreshList();
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
        }


        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (this.txtIdentificador.Text.Trim() != string.Empty && this.txtCuentaUsuario.Text.Trim() != string.Empty)
            {
                Model = new SAS_UsuariosBoltController();
                userToRegister = new usuarioMaquinaria();
                userToRegister.idUsuarioMaquinaria = Convert.ToInt32(this.txtIdentificador.Text.Trim());
                if (Model.Eliminar(connection, userToRegister) > 0)
                {
                    MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                    gbEdition.Enabled = false;
                    gbList.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnActualizarLista.Enabled = true;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnSave.Enabled = false;
                    btnEditar.Enabled = true;
                    btnAtras.Enabled = false;
                    RefreshList();
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
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            ViewLog();
        }

        private void ViewLog()
        {
            MessageBox.Show("Opción no implementada", "MENSAJE DEL SISTEMA");
        }

        private void btnPrivileges_Click(object sender, EventArgs e)
        {
            ViewPrivileges();
        }

        private void ViewPrivileges()
        {
            MessageBox.Show("Opción no implementada", "MENSAJE DEL SISTEMA");
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.ExportarToExcel(dgvList, saveFileDialog);
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
        }

        private void ElegirColumna()
        {
            this.dgvList.ShowColumnChooser();
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ParImpar += 1;
            PintarResultadosEnGrilla();
        }

        private void PintarResultadosEnGrilla()
        {
            if ((ParImpar % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Condición, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                dgvList.Columns["chSituacionEnPlanilla"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Condición, applied to entire row", ConditionTypes.Equal, "DESCONOCIDO", "", true);
                c2.RowBackColor = Color.GreenYellow;
                c2.CellBackColor = Color.GreenYellow;
                dgvList.Columns["chSituacionEnPlanilla"].ConditionalFormattingObjectList.Add(c2);

                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("chEstado UI", 8, FontStyle.Strikeout);
                dgvList.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Condicion, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvList.Columns["chSituacionEnPlanilla"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Condición, applied to entire row", ConditionTypes.Equal, "DESCONOCIDO", "", true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                dgvList.Columns["chSituacionEnPlanilla"].ConditionalFormattingObjectList.Add(c2);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.White;
                c4.RowFont = new Font("chEstado UI", 8, FontStyle.Regular);
                dgvList.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
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

        private void UsuariosBoltWeb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UsuariosBoltWeb_Load(object sender, EventArgs e)
        {

        }

        private void RefreshList()
        {
            BarraPrincipal.Enabled = false;
            gbEdition.Enabled = false;
            gbList.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnAtras.Enabled = false;
            ProgressBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void bgwHiloInit_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwHiloInit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                #region Consultar()
                ListUserBoltWeb = new List<SAS_ListadoCuentasBoltWeb>();
                Model = new SAS_UsuariosBoltController();
                ListUserBoltWeb = Model.GetListBoltWebUsers(connection).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        private void PresentarResultados()
        {
            try
            {
                #region PresentarResultados()
                dgvList.DataSource = ListUserBoltWeb;
                dgvList.Refresh();

                PintarResultadosEnGrilla();
                BarraPrincipal.Enabled = !false;
                gbEdition.Enabled = false;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizarLista.Enabled = true;
                btnAnular.Enabled = true;
                btnEliminarRegistro.Enabled = true;
                btnSave.Enabled = false;
                btnEditar.Enabled = true;
                btnAtras.Enabled = false;
                ProgressBar.Visible = !true;


                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            #region Edicion() 
            UserBoltWeb = new SAS_ListadoCuentasBoltWeb();
            LimpiarFormularioDeEdicion();
            if (dgvList.Rows.Count > 0)
            {
                if (dgvList.CurrentRow != null && dgvList.CurrentRow.Cells["chCuentaUsuarioIdentificador"].Value != null)
                {
                    string idUserSelect = dgvList.CurrentRow.Cells["chCuentaUsuarioIdentificador"].Value != null ? Convert.ToString(dgvList.CurrentRow.Cells["chCuentaUsuarioIdentificador"].Value.ToString().Trim()) : string.Empty;

                    var userSelectByIdUser = ListUserBoltWeb.Where(x => x.CuentaUsuarioIdentificador.ToString().Trim() == idUserSelect).ToList();
                    if (userSelectByIdUser.ToList().Count == 1)
                    {
                        UserBoltWeb = userSelectByIdUser.Single();
                        this.txtIdentificador.Text = UserBoltWeb.CuentaUsuarioIdentificador != null ? UserBoltWeb.CuentaUsuarioIdentificador.ToString().Trim() : "0";
                        txtEmail.Text = UserBoltWeb.Correo != null ? UserBoltWeb.Correo.Trim() : string.Empty;
                        txtCuentaUsuario.Text = UserBoltWeb.CuentaUsuario != null ? UserBoltWeb.CuentaUsuario.Trim() : string.Empty;
                        txtPassword.Text = UserBoltWeb.ClaveAcceso != null ? UserBoltWeb.ClaveAcceso.Trim() : string.Empty;
                        txtCodigoColaborador.Text = UserBoltWeb.ColaboradorCodigo != null ? UserBoltWeb.ColaboradorCodigo.Trim() : string.Empty;
                        txtNombreColaborador.Text = UserBoltWeb.ColaboradorNombresCompletos != null ? UserBoltWeb.ColaboradorNombresCompletos.Trim() : string.Empty;
                        cboArea.SelectedValue = UserBoltWeb.AreaId != null ? UserBoltWeb.AreaId.ToString().Trim() : "ITD";
                        cboPefil.SelectedValue = UserBoltWeb.Perfil != null ? UserBoltWeb.Perfil.ToString().Trim() : "S";
                        this.txtNumeroDocumento.Text = UserBoltWeb.NumeroDocumento != null ? UserBoltWeb.NumeroDocumento.ToString().Trim() : string.Empty;
                        this.txtRuta.Text = UserBoltWeb.RutaDeFirma != null ? UserBoltWeb.RutaDeFirma.ToString().Trim() : string.Empty;
                        this.txtEstadoDescripcion.Text = UserBoltWeb.Estado == '1' ? "ACTIVO" : "ANULADO";

                        if (UserBoltWeb.RutaDeFirma != null && UserBoltWeb.RutaDeFirma.ToString().Length > 10)
                        {
                            VerImagen(UserBoltWeb.RutaDeFirma.ToString().Trim());
                        }

                    }
                }
            }
            #endregion
        }

        private void LimpiarFormularioDeEdicion()
        {
            this.txtEmail.Clear();
            this.txtNumeroDocumento.Clear();
            this.txtCuentaUsuario.Clear();
            this.txtPassword.Clear();
            this.txtCodigoColaborador.Clear();
            this.txtNombreColaborador.Clear();
            this.txtRuta.Clear();
            this.txtIdentificador.Clear();
            this.txtIdentificador.Text = "0";
            this.txtEstadoDescripcion.Clear();
            this.txtEstadoDescripcion.Text = "ANULADO";
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (this.txtIdentificador.Text != string.Empty)
            {
                if (this.txtIdentificador.Text.Trim() != "0")
                {
                    Model = new SAS_UsuariosBoltController();
                    usuarioMaquinaria usuario = new usuarioMaquinaria();
                    usuario.idUsuarioMaquinaria = Convert.ToInt32(this.txtIdentificador.Text);
                    Model.ResetearClave(connection, usuario);
                    MessageBox.Show("Se ha reseteado la clave del usuario", "Confirmación del sistema");
                }
            }
        }

        private void ResetearClave()
        {
            try
            {
                #region Resetear clave()
                usuarioMaquinaria CuentaAReset = new usuarioMaquinaria();
                CuentaAReset.idUsuarioMaquinaria = Convert.ToInt32(this.txtIdentificador.Text);
                Model = new SAS_UsuariosBoltController();
                Model.ResetearClave(connection, CuentaAReset);
                MessageBox.Show("La cuenta ha sido reseteada, el usuario tendra que iniciar sesión con su DNI", "Confirmación del sistema");

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {

        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            CargarFoto();
        }

        private void CargarFoto()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\SOLUTION\firmas\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "png",
                Filter = "png files (*.png)|*.png",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtRuta.Text = openFileDialog1.FileName;
                pbFirma.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void VerImagen(string ruta)
        {
            try
            {
                imagen = ruta;
                int largo = imagen.Length;
                if (largo > 0)
                {
                    imagen2 = string.Empty;
                    imagen2 = imagen.Substring(largo - 3, 3);
                    if (imagen2.ToUpper() == "png".ToUpper())
                    {
                        pbFirma.Image = Image.FromFile(@imagen);
                    }
                    else
                    {
                        MessageBox.Show("Formato no válido", "Mensaje del sistema");
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }


        }

        private void btnVerImagen_Click(object sender, EventArgs e)
        {
            VerImagen(this.txtRuta.Text.Trim());
        }

        private void btnValidarDatos_Click(object sender, EventArgs e)
        {
            if (this.txtIdentificador.Text.Trim() =="0")
            {
                if (this.txtCodigoColaborador.Text.Trim()!= string.Empty && this.txtNombreColaborador.Text.Trim() != string.Empty)
                {
                    string CodigoEmpleado = this.txtCodigoColaborador.Text.Trim();
                    AutoCompletarDatosDelUsuario(CodigoEmpleado);
                }
               
            }
            else
            {
                MessageBox.Show("No se puede validar el usuario", "Mensaje del sistema");
            }
        }

        private void AutoCompletarDatosDelUsuario(string codigoEmpleado)
        {
            try
            {
                Model = new SAS_UsuariosBoltController();
                UsuariosNISIRAYBolt = new SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos();
                UsuariosNISIRAYBolt = Model.ListarDatosParaAutoCompletarDatosDelUsuario(connection, codigoEmpleado);

                this.txtPassword.Text = UsuariosNISIRAYBolt.Clave.Trim();
                this.txtCuentaUsuario.Text = UsuariosNISIRAYBolt.UsuarioCodigo.Trim();
                this.txtNumeroDocumento.Text = UsuariosNISIRAYBolt.PersonalDocumento.Trim();
                this.txtEmail.Text = UsuariosNISIRAYBolt.Correo.Trim();


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }
    }
}