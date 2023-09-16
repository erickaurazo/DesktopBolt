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
    public partial class UsuariosRendimientosBolt : Form
    {
        private string conection;
        private string companyId;
        private List<SAS_ListadoUsuarioBolt> users;
        private SAS_ListadoUsuarioBolt userSelect;
        private PrivilegesByUser privilege;
        private USUARIOSFAJA userToRegister;
        private SAS_USUARIOS userLogin;
        private List<Grupo> listTypeUser;
        private SAS_UsuariosBoltController model;
        private string mensajeResultado;
        private string imagen = string.Empty;
        private string imagen2 = string.Empty;

        public UsuariosRendimientosBolt()
        {
            InitializeComponent();
            conection = "NSFAJA";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "Erick Aurazo";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            RefreshList();
        }

        public UsuariosRendimientosBolt(string _conection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            conection = _conection;
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

                model = new SAS_UsuariosBoltController();
                listTypeUser = new List<Grupo>();
                listTypeUser = model.GetTypeUser(conection).ToList();
                cboTipo.DataSource = listTypeUser;
                cboTipo.ValueMember = "Codigo";
                cboTipo.DisplayMember = "Descripcion";
                cboTipo.SelectedValue = "O";


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }


        private void UsuariosBolt_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GotoBack();
        }

        private void GotoBack()
        {
            LimpiarControlesDeEdicion();
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }


        private void Edit()
        {
            if (txtEstado.Text.Trim().ToUpper() == "ACTIVO")
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
                btnVistaPrevia.Enabled = true;
                btnImprimir.Enabled = true;
                btnRegistrarUsuario.Enabled = true;
                btnCancelar.Enabled = true;
                cboTipo.Enabled = true;
            }
            else
            {
                btnVistaPrevia.Enabled = !true;
                btnImprimir.Enabled = !true;
                btnRegistrarUsuario.Enabled = !true;
                btnCancelar.Enabled = !true;
                MessageBox.Show("El documento no tiene el estado para la edición", "Confirmación del sistema");
                return;
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (this.txtUsuarioCodigo.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    userToRegister = new USUARIOSFAJA();
                    userToRegister.idusuariofaja = this.txtidusuariofaja.Text != string.Empty ? Convert.ToInt32(this.txtidusuariofaja.Text) : 0;
                    //userToRegister.nrodocumento = this.txt
                    userToRegister.nombres = this.txtNombresCompletos.Text.Trim();
                    userToRegister.usuario = this.txtUsuarioCodigo.Text.Trim();
                    userToRegister.clave = this.txtclave.Text.Trim();
                    userToRegister.estado = this.txtEstado.Text.Trim().ToUpper() == "ACTIVO" ? '1' : '0';
                    userToRegister.perfil = Convert.ToChar(cboTipo.SelectedValue.ToString().Trim());
                    userToRegister.firma = this.txtFirma.Text.Trim();

                    if (model.UpdateUser(conection, userToRegister) > 0)
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
            bool estadoValidacion = true;
            mensajeResultado = string.Empty;

            if (this.txtidusuariofaja.Text == string.Empty)
            {
                mensajeResultado += "\nLa cuenta no existe";
                return false;
            }

            if (this.txtUsuarioCodigo.Text.Trim() == string.Empty || this.txtNombresCompletos.Text.Trim() == string.Empty)
            {
                mensajeResultado += "\nLa cuenta del usuario no existe, ingrese una";
                return false;
            }


            return estadoValidacion;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            GotoBack();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                users = new List<SAS_ListadoUsuarioBolt>();
                model = new SAS_UsuariosBoltController();
                //users = Modelo.GetListAllUser(_conection, _companyId).ToList();
                users = model.GetListUsers(conection).ToList();
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
                dgvList.DataSource = users.ToList();
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

        private void dgvList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarControlesDeEdicion();
                #region Edicion() 
                userSelect = new SAS_ListadoUsuarioBolt();
                userSelect.idusuariofaja = 0;


                if (dgvList.Rows.Count > 0)
                {
                    if (dgvList.CurrentRow != null && dgvList.CurrentRow.Cells["chidusuariofaja"].Value != null)
                    {
                        int idUserSelect = dgvList.CurrentRow.Cells["chidusuariofaja"].Value != null ? Convert.ToInt32(dgvList.CurrentRow.Cells["chidusuariofaja"].Value.ToString().Trim()) : 0;

                        var userSelectByIdUser = users.Where(x => x.idusuariofaja == idUserSelect).ToList();
                        if (userSelectByIdUser.ToList().Count == 1)
                        {
                            userSelect = userSelectByIdUser.Single();
                            
                            txtEstado.Text = userSelect.estado != null ? userSelect.estado.Trim().ToUpper() : string.Empty;
                            txtidusuariofaja.Text = userSelect.idusuariofaja != null ? userSelect.idusuariofaja.ToString().Trim().ToUpper() : "0";
                            txtNombresCompletos.Text = userSelect.nombresCompletos != null ? userSelect.nombresCompletos.Trim().ToUpper() : string.Empty;
                            txtUsuarioCodigo.Text = userSelect.usuarioCodigo != null ? userSelect.usuarioCodigo.Trim().ToUpper() : string.Empty;
                            txtclave.Text = userSelect.clave != null ? userSelect.clave.Trim().ToUpper() : string.Empty;
                            cboTipo.SelectedValue = userSelect.perfilCodigo != null ? userSelect.perfilCodigo.ToString().Trim().ToUpper() : "0";
                            this.txtFirma.Text = userSelect.firma != null ? userSelect.firma.Trim() : string.Empty;

                            if ((userSelect.firma != null ? userSelect.firma.Trim() : string.Empty) != string.Empty)
                            {
                                //imagen = this.txtFirma.Text.Trim();
                                //pbFirma.Image = Image.FromFile(@imagen);
                                VerImagen(userSelect.firma.Trim());
                            }
                            
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void LimpiarControlesDeEdicion()
        {
            this.txtclave.Clear();
            this.txtEstado.Text = "ACTIVO";
            this.txtidusuariofaja.Clear();
            this.txtNombresCompletos.Clear();
            this.txtFirma.Clear();
            this.txtUsuarioCodigo.Clear();
            cboTipo.SelectedValue = 'O';
            pbFirma.Image = null;
        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            try
            {
                if (userSelect != null)
                {
                    if (userSelect.idusuariofaja != 0)
                    {
                        UsuariosRendimientosBoltPreviewCarnet ofrm = new UsuariosRendimientosBoltPreviewCarnet(conection, Convert.ToInt32(userSelect.idusuariofaja));
                        ofrm.ShowDialog();
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnVerImagen_Click(object sender, EventArgs e)
        {
            VerImagen(this.txtFirma.Text.Trim());
        }

        private void VerImagen(string rutaDeImagen)
        {
            try
            {

                imagen = rutaDeImagen;
                int largo = imagen.Length;

                if (largo > 0)
                {
                    imagen2 = string.Empty;
                    imagen2 = imagen.Substring(largo - 3, 3);
                    if (imagen2.ToUpper() == "jpg")
                    //                        pictureBox1.Load(imagen);
                    {
                        //Image.FromFile(imagen);
                        //pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath, imagen));
                        pbFirma.Image = Image.FromFile(@imagen);

                        //if (File.Exists(imagen))
                        //{
                        //    FileStream fs = new System.IO.FileStream(@imagen, FileMode.Open, FileAccess.Read);
                        //    pictureBox1.Image = Image.FromStream(fs);
                        //    fs.Close();
                        //}
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

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\SOLUTION\firmas\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFirma.Text = openFileDialog1.FileName;
                pbFirma.ImageLocation = openFileDialog1.FileName;
            }

            //try
            //{
            //    if (openFileDialog1.FileName != string.Empty)
            //    {
            //        imagen = string.Empty;
            //        int largo = imagen.Length;

            //        if (largo > 0)
            //        {
            //            imagen2 = string.Empty;
            //            imagen2 = imagen2.Substring(largo - 2, largo);
            //            if (imagen2.ToUpper() != "gif".ToUpper() && imagen2.ToUpper() != "bpm".ToUpper() && imagen2.ToUpper() != "jpg".ToUpper() && imagen2.ToUpper() != "jpeg".ToUpper())
            //            {
            //                imagen2 = imagen2.Substring(largo - 3, largo);
            //                if (imagen2.ToUpper() != "JPEG".ToUpper() && imagen2.ToUpper() != "LOG1".ToUpper() && imagen2.ToUpper() != "jpg".ToUpper() && imagen2.ToUpper() != "jpeg".ToUpper())
            //                {
            //                    MessageBox.Show("Formato no válido", "Mensaje del sistema");
            //                }
            //                else
            //                {
            //                    pictureBox1.Load(imagen);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{

            //    MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
            //    return;
            //}

        }
    }
}

