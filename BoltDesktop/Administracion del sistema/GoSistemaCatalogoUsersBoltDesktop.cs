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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Asistencia
{
    public partial class GoSistemaCatalogoUsers : Form
    {
        private ExportToExcelHelper modelExportToExcel;
        private List<ASJ_USUARIOS> users;
        private UsersController Modelo;
        //private ASJ_USUARIOS userSelect;
        private List<Grupo> listAreaByComboBox;
        private List<Grupo> listLocalByComboBox;
        private List<Grupo> listAccessNivelByComboBox;
        private List<Grupo> listBranchOfficeComboBox;
        private List<Grupo> listDoorAccessComboBox;
        private List<Grupo> listStatudByComboBox;
        private ComboBoxHelper modelComboBox;
        private string conection;
        private string companyId;
        private List<SAS_ListadoDeUsuariosDelSistema> usersSystem;
        private SAS_ListadoDeUsuariosDelSistema userSystemSelect;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS userToRegister;
        int ParImpar = 0;

        public GoSistemaCatalogoUsers()
        {
            InitializeComponent();
            conection = "SAS";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = Environment.UserName.Trim();
            userLogin.NombreCompleto = Environment.MachineName.Trim();
            companyId = "001";
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            RefreshList();
        }

        public GoSistemaCatalogoUsers(string _conection, SAS_USUARIOS _userLogin, string _companyId)
        {
            InitializeComponent();
            conection = _conection;
            userLogin = _userLogin;
            companyId = _companyId;

            lblUsuarioNombre.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario : Environment.UserName;
            lblNombreDescripcion.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto : Environment.MachineName;

            privilege = new PrivilegesByUser();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            RefreshList();
        }

        public GoSistemaCatalogoUsers(string _conection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            conection = "SAS";
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;

            lblUsuarioNombre.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario : Environment.UserName;
            lblNombreDescripcion.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto : Environment.MachineName;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            RefreshList();
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
            items1.Add(new GridViewSummaryItem("chNombreCompleto", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvList.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[conection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

                modelComboBox = new ComboBoxHelper();
                listAreaByComboBox = new List<Grupo>();
                listAreaByComboBox = modelComboBox.GetComboBoxAreaAccess();

                listLocalByComboBox = new List<Grupo>();
                listLocalByComboBox = modelComboBox.GetComboBoxLocal();

                listAccessNivelByComboBox = new List<Grupo>();
                listAccessNivelByComboBox = modelComboBox.GetComboBoxAccessLevel();

                listBranchOfficeComboBox = new List<Grupo>();
                listBranchOfficeComboBox = modelComboBox.GetComboBoxBranchOffice();

                listDoorAccessComboBox = new List<Grupo>();
                listDoorAccessComboBox = modelComboBox.GetComboBoxDoorAccess();

                listStatudByComboBox = new List<Grupo>();
                listStatudByComboBox = modelComboBox.GetComboBoxStatusUser();


                cboArea.DataSource = listAreaByComboBox;
                cboArea.ValueMember = "Codigo";
                cboArea.DisplayMember = "Descripcion";
                cboArea.SelectedValue = "000";

                cboNivelAcceso.DataSource = listAccessNivelByComboBox;
                cboNivelAcceso.ValueMember = "Codigo";
                cboNivelAcceso.DisplayMember = "Descripcion";
                cboNivelAcceso.SelectedValue = "1";

                cboPuerta.DataSource = listDoorAccessComboBox;
                cboPuerta.ValueMember = "Codigo";
                cboPuerta.DisplayMember = "Descripcion";
                cboPuerta.SelectedValue = "1";

                cboStatus.DataSource = listStatudByComboBox;
                cboStatus.ValueMember = "Codigo";
                cboStatus.DisplayMember = "Descripcion";
                cboStatus.SelectedValue = "1";

                cboSucursal.DataSource = listBranchOfficeComboBox;
                cboSucursal.ValueMember = "Codigo";
                cboSucursal.DisplayMember = "Descripcion";
                cboSucursal.SelectedValue = "001";

                cboLocal.DataSource = listLocalByComboBox;
                cboLocal.ValueMember = "Codigo";
                cboLocal.DisplayMember = "Descripcion";
                cboLocal.SelectedValue = "000";


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }


        private void Privilegio_Load(object sender, EventArgs e)
        {
            ImagePrimitive searchIcon = new ImagePrimitive();
            searchIcon.Image = imageList1.Images[4];
            searchIcon.Alignment = ContentAlignment.MiddleRight;
            //this.txtFormulario.TextBoxElement.Children.Add(searchIcon);
            //this.txtFormulario.TextBoxElement.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
            //this.txtFormulario.TextBoxElement.TextBoxItem.StretchHorizontally = false;
            //this.txtFormulario.TextBoxElement.TextBoxItem.HostedControl.MinimumSize = new Size(120, 0);
            //this.txtFormulario.TextBoxElement.TextBoxItem.PropertyChanged += new PropertyChangedEventHandler(TextBoxItem_PropertyChanged);
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {

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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void Add()
        {
            ClearForm();
            txtUserCode.ReadOnly = !true;
            txtPersonalCode.ReadOnly = !true;
            txtPersonalFullName.ReadOnly = !true;
            txtPassword.ReadOnly = !true;
            txtFullName.ReadOnly = !true;
            btnPersonalNisira.Enabled = true;
            gbEdition.Enabled = true;
            gbList.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizarLista.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnSave.Enabled = true;
            btnAtras.Enabled = true;
            this.txtUserCode.Focus();
        }

        private void ClearForm()
        {
            txtEmail.Clear();
            txtFullName.Clear();
            txtPassword.Clear();
            txtPersonalCode.Clear();
            txtPersonalFullName.Clear();
            txtUserCode.Clear();
            txtUserCode.Focus();
            cboArea.SelectedValue = "000";
            cboNivelAcceso.SelectedValue = "1";
            cboPuerta.SelectedValue = "1";
            cboStatus.SelectedValue = "1";
            cboSucursal.SelectedValue = "001";
            cboLocal.SelectedValue = "000";

            SAS_USUARIOS user = new SAS_USUARIOS();
            user.IdUsuario = string.Empty;
            user.IdCodigoGeneral = string.Empty;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            if (cboStatus.SelectedValue != null)
            {
                if (cboStatus.SelectedValue.ToString().Trim() != "0")
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
                    txtUserCode.ReadOnly = true;
                    txtPersonalCode.ReadOnly = true;
                    txtPersonalFullName.ReadOnly = true;
                    txtPassword.ReadOnly = true;
                    txtFullName.ReadOnly = false;
                    btnPersonalNisira.Enabled = false;

                    txtFullName.Focus();
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
            if (this.txtUserCode.Text.Trim() != string.Empty && this.txtFullName.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    userToRegister = new SAS_USUARIOS();
                    userToRegister.IdUsuario = txtUserCode.Text.Trim().ToUpper();
                    userToRegister.IdCodigoGeneral = txtPersonalCode.Text.Trim();
                    userToRegister.Password = txtPassword.Text.Trim();
                    userToRegister.NombreCompleto = txtFullName.Text.Trim();
                    userToRegister.AREA = cboArea.SelectedValue.ToString().Trim();
                    userToRegister.email = txtEmail.Text.Trim();
                    userToRegister.idestado = cboStatus.SelectedValue.ToString().Trim();
                    userToRegister.Local = cboLocal.SelectedValue.ToString().Trim();
                    userToRegister.nivel = cboNivelAcceso.SelectedValue.ToString().Trim();
                    userToRegister.IDSUCURSAL = cboSucursal.SelectedValue.ToString().Trim();
                    userToRegister.SUCURSAL = cboSucursal.SelectedText.ToString().Trim();
                    userToRegister.id_puerta = Convert.ToInt32(cboPuerta.SelectedValue.ToString().Trim());
                    userToRegister.SUCURSAL = cboPuerta.SelectedText.ToString().Trim();
                    if (Modelo.AddUser(conection, userToRegister, companyId) == true)
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

        private bool ValidateForm()
        {
            bool status = true;
            if (cboArea.SelectedValue != null && cboArea.SelectedValue.ToString().Trim() == "0")
            {
                status = false;
            }


            if (cboLocal.SelectedValue != null && cboLocal.SelectedValue.ToString().Trim() == "0")
            {
                status = false;
            }

            if (cboNivelAcceso.SelectedValue != null && cboNivelAcceso.SelectedValue.ToString().Trim() == "1")
            {
                status = false;
            }

            if (cboPuerta.SelectedValue != null && cboPuerta.SelectedValue.ToString().Trim() == "1")
            {
                status = false;
            }

            if (cboSucursal.SelectedValue != null && cboSucursal.SelectedValue.ToString().Trim() == "0")
            {
                status = false;
            }

            return status;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            GotoBack();
        }
        private void GotoBack()
        {
            ClearForm();
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
            if (this.txtUserCode.Text.Trim() != string.Empty && this.txtFullName.Text.Trim() != string.Empty)
            {
                userToRegister = new SAS_USUARIOS();
                userToRegister.IdUsuario = txtUserCode.Text.Trim();
                if (Modelo.ChangeStateUser(conection, userToRegister, companyId) == true)
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
            Remove();
        }

        private void Remove()
        {
            if (this.txtUserCode.Text.Trim() != string.Empty && this.txtFullName.Text.Trim() != string.Empty)
            {
                userToRegister = new SAS_USUARIOS();
                userToRegister.IdUsuario = txtUserCode.Text.Trim();
                if (Modelo.RemoveUser(conection, userToRegister, companyId) == true)
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
            ViewHistory();
        }

        private void ViewHistory()
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

        private void Privilegio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            #region Edicion() 
            userSystemSelect = new SAS_ListadoDeUsuariosDelSistema();
            ClearForm();
            if (dgvList.Rows.Count > 0)
            {
                if (dgvList.CurrentRow != null && dgvList.CurrentRow.Cells["chIdUsuario"].Value != null)
                {
                    string idUserSelect = dgvList.CurrentRow.Cells["chIdUsuario"].Value != null ? Convert.ToString(dgvList.CurrentRow.Cells["chIdUsuario"].Value.ToString().Trim()) : string.Empty;

                    var userSelectByIdUser = usersSystem.Where(x => x.IdUsuario.Trim() == idUserSelect).ToList();
                    if (userSelectByIdUser.ToList().Count == 1)
                    {
                        userSystemSelect = userSelectByIdUser.Single();
                        txtEmail.Text = userSystemSelect.email != null ? userSystemSelect.email.Trim() : string.Empty;
                        txtFullName.Text = userSystemSelect.NombreCompleto != null ? userSystemSelect.NombreCompleto.Trim() : string.Empty;
                        txtPassword.Text = userSystemSelect.Password != null ? userSystemSelect.Password.Trim() : string.Empty;
                        txtPersonalCode.Text = userSystemSelect.IdCodigoGeneral != null ? userSystemSelect.IdCodigoGeneral.Trim() : string.Empty;
                        txtPersonalFullName.Text = userSystemSelect.NombreCompleto != null ? userSystemSelect.NombreCompleto.Trim() : string.Empty;
                        txtUserCode.Text = userSystemSelect.IdUsuario != null ? userSystemSelect.IdUsuario.Trim() : string.Empty;

                        cboArea.SelectedValue = userSystemSelect.idArea != null ? userSystemSelect.idArea.ToString().Trim() : "010";
                        cboNivelAcceso.SelectedValue = userSystemSelect.nivelAccesoId != null ? userSystemSelect.nivelAccesoId.ToString().Trim() : "1";
                        cboPuerta.SelectedValue = userSystemSelect.puerdaId != null ? userSystemSelect.puerdaId.ToString().Trim() : "1";
                        cboStatus.SelectedValue = userSystemSelect.idestado != null ? userSystemSelect.idestado.ToString().Trim() : "1";
                        cboSucursal.SelectedValue = userSystemSelect.IDSUCURSAL != null ? userSystemSelect.IDSUCURSAL.ToString().Trim() : "0";
                        cboLocal.SelectedValue = userSystemSelect.sedeId != null ? userSystemSelect.sedeId.ToString().Trim() : "000";
                    }
                }
            }
            #endregion
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                users = new List<ASJ_USUARIOS>();
                usersSystem = new List<SAS_ListadoDeUsuariosDelSistema>();
                Modelo = new UsersController();
                usersSystem = Modelo.GetListAllUserSystem(conection, companyId).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvList.DataSource = usersSystem;
                dgvList.Refresh();



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

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dgvList.EnableFiltering == true)
            {
                dgvList.EnableFiltering = false;
                dgvList.ShowHeaderCellButtons = false;
            }
            else
            {
                dgvList.EnableFiltering = true;
                dgvList.ShowHeaderCellButtons = true;
            }
        }

        private void btnPrivileges_Click(object sender, EventArgs e)
        {

            if (this.txtUserCode.Text.Trim() != string.Empty && this.txtFullName.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    GoSistemaCatalogoUsersPrivileges ofrm = new GoSistemaCatalogoUsersPrivileges(this.txtUserCode.Text.Trim(), this.txtFullName.Text.Trim(), "SAS", userLogin, companyId);
                    ofrm.MdiParent = GoSistemaCatalogoUsers.ActiveForm;
                    ofrm.WindowState = FormWindowState.Maximized;
                    ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    ofrm.Show();

                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            ResetPassword();
        }

        private void ResetPassword()
        {
            try
            {
                if (this.txtUserCode.Text.Trim() != string.Empty && this.txtFullName.Text.Trim() != string.Empty)
                {
                    userToRegister = new SAS_USUARIOS();
                    userToRegister.IdUsuario = txtUserCode.Text.Trim();
                    if (Modelo.ResetPasswordByUser(conection, userToRegister, companyId) == true)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente. Vuelva a ingresar al sistema", "Confirmación del sistema");
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
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }
        }

        private void btnImportFromNISIRA_Click(object sender, EventArgs e)
        {
            try
            {
                Modelo = new UsersController();
                int resultadoQuery = Modelo.CopiarCuentasDeERPaBolt(conection);
                RefreshList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void txtUserCode_Leave(object sender, EventArgs e)
        {

        }

        private void txtUserCode_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo = new UsersController();
            List<SAS_USUARIOS> resultado = new List<SAS_USUARIOS>();
            resultado = Modelo.FindUserByIdUser(conection, this.txtUserCode.Text.Trim().ToUpper());
            if (resultado != null)
            {
                if (resultado.ToList().Count > 0)
                {
                    btnResetPassword.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    btnResetPassword.Enabled = !false;
                    btnSave.Enabled = !false;
                }
            }
        }

        private void btnAccessFromOtherUser_Click(object sender, EventArgs e)
        {
            try
            {
                SAS_USUARIOS userLoginToMofify = new SAS_USUARIOS();
                userLoginToMofify.IdUsuario = txtUserCode.Text.Trim();
                userLoginToMofify.NombreCompleto = txtFullName.Text.Trim();
                SAS_USUARIOS userLoginbase = new SAS_USUARIOS();
                GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios ofrm = new GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios(conection, userLogin, companyId, privilege, userLoginToMofify, userLoginbase);
                ofrm.Show();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }



        }

        private void btnPintar_Click(object sender, EventArgs e)
        {
            ParImpar += 1;
            PintarResultadosEnGrilla();
        }

        private void PintarResultadosEnGrilla()
        {
            if ((ParImpar % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c1.RowBackColor = Color.Peru;
                c1.CellBackColor = Color.Peru;
                dgvList.Columns["chSituacionPlanilla"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Cerrado applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("chEstado UI", 8, FontStyle.Strikeout);
                dgvList.Columns["chCerrado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvList.Columns["chsituacionEnPLanilla"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Cerrado applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.White;
                c4.RowFont = new Font("chEstado UI", 8, FontStyle.Regular);
                dgvList.Columns["chCerrado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvList.ShowColumnChooser();
        }
    }
}
