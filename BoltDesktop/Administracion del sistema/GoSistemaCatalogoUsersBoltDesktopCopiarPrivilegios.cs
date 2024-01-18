using Asistencia.Datos;
using Asistencia.Helper;
using Asistencia.Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    public partial class GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios : Form
    {
        //const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        //const string passwordCorreo = @"iompqiiuhkjngkjr";

        const string usuarioCorreo = "notify.bolt@gmail.com";
        const string passwordCorreo = "wppi kiav vegf sesx";



        private string companyId;
        private string conection;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS userLogin;
        private SAS_USUARIOS userLoginBase;
        private SAS_USUARIOS userToModify;
        private UsersController model;
        private int enviarNotificacion;

        public GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios()
        {
            InitializeComponent();
            lbl1UsuarioCodigo.Text = Environment.UserName.ToString();
            lblNombreDelUsuario.Text = Environment.MachineName.ToString();
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

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }


        public GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios(string _conection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege, SAS_USUARIOS _userToModify, SAS_USUARIOS _userLoginBase)
        {
            try
            {
                #region Init()
                InitializeComponent();
                conection = _conection;
                userLogin = _userLogin;
                companyId = _companyId;
                privilege = _privilege;
                userToModify = _userToModify;
                userLoginBase = _userLoginBase;
                Inicio();
                this.txtUserId.Text = userToModify.IdUsuario.Trim();
                this.txtUserName.Text = userToModify.NombreCompleto.Trim();
                lblCodigoDeUsuario.Text = userLogin.IdUsuario;
                lblNombreDelUsuario.Text = userLogin.NombreCompleto;
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }

        }


        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void GoSistemaCatalogoUsersCopiarPrivilegios_Load(object sender, EventArgs e)
        {

        }

        private void btnAccessFromOtherUser_Click(object sender, EventArgs e)
        {

            try
            {
                #region Copy to User()
                userLoginBase = new SAS_USUARIOS();
                if (this.txtUserIdBase.Text != string.Empty && this.txtUserNameBase.Text != string.Empty)
                {
                    userLoginBase.IdUsuario = this.txtUserIdBase.Text;
                    btnUserFind.Enabled = false;
                    txtUserIdBase.Enabled = false;
                    txtUserNameBase.Enabled = false;
                    btnAccessFromOtherUser.Enabled = false;
                    btnCancelar.Enabled = false;
                    ProgressBar.Visible = true;
                    enviarNotificacion = 0;
                    if (chkNotificar.Checked == true)
                        enviarNotificacion = 1;

                    bgwHilo.RunWorkerAsync();
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }



        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new UsersController();
                model.CopyFromUserID(conection, userToModify.IdUsuario, userLoginBase.IdUsuario);

                if (enviarNotificacion == 1)
                {
                    string Error = string.Empty;
                    StringBuilder mensajeBuilder = new StringBuilder();
                    //mensajeBuilder.Append("Actualización de privilegios en Bolt");
                    model.EnviarCorreo("eaurazo@saturno.net.pe", "Actualización de privilegios en Bolt", userToModify);
                    //EnviarCorreo(mensajeBuilder, DateTime.Now, usuarioCorreo, "eaurazo@saturno.net.pe", "Actualización de privilegios en Bolt", out Error);
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }
        }

        private void EnviarCorreo(StringBuilder Mensaje, DateTime fechaEnvio, string De, string Para, string Asunto, out string Error)
        {

            Error = string.Empty;
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica actualización en los privilegios de usuarios.\n"));
                Mensaje.Append(string.Format("Usuario actualizado : " + userToModify.IdUsuario + "\n"));
                Mensaje.Append(string.Format("Este correo ha sido enviado el día {0:dd/MM/yyyy} a las {0:HH:mm:ss} hrs: \n\n", fechaEnvio));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2022 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));

                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mail.From = new MailAddress(De, "Notificaciones - ERP | Sociedad Agrícola Saturno SA");
                mail.To.Add(Para);
                mail.Subject = Asunto;
                mail.Body = Mensaje.ToString();

                // SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(usuarioCorreo, passwordCorreo);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Error = "Éxito";
                MessageBox.Show(Error, "Mensaje del sistema");


            }
            catch (Exception Ex)
            {
                Error = "Error" + Ex.Message.ToString().Trim();
                MessageBox.Show(Error, "ADVERTANCIA DEL SISTEMA");
                return;
            }

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Se registrado correctamente la operación en el sistema", "ADVERTANCIA DEL SISTEMA");
                btnUserFind.Enabled = !false;
                txtUserIdBase.Enabled = !false;
                txtUserNameBase.Enabled = !false;
                btnAccessFromOtherUser.Enabled = !false;
                btnCancelar.Enabled = !false;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().Trim(), "ADVERTANCIA DEL SISTEMA");
                return;
            }
        }

        private void GoSistemaCatalogoUsersCopiarPrivilegios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
    }
}
