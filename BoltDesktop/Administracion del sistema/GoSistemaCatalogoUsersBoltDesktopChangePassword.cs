using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using System.Linq;

namespace Asistencia
{
    public partial class GoSistemaCatalogoUsersChangePassword : Form
    {
        private UsersController Modelo;
        private string _companyId;
        private string _conection;
        private string _userId;

        public GoSistemaCatalogoUsersChangePassword()
        {
            InitializeComponent();
        }

        public GoSistemaCatalogoUsersChangePassword(string conection, string userId, string companyId)
        {
            InitializeComponent();
            _userId = userId;
            _conection = conection;
            _companyId = companyId;
            this.txtContraseña.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (_conection != string.Empty)
            {
                if (this.txtContraseña.Text.Trim() != string.Empty && _userId != null)
                {
                    if (this.txtContraseña.Text.Trim().Length > 6 && _userId.Trim() != string.Empty)
                    {
                        Modelo =new UsersController();
                        SAS_USUARIOS user = new SAS_USUARIOS();
                        user.IdUsuario = _userId;
                        user.Password = this.txtContraseña.Text.Trim();
                        if (Modelo.ChangePasswordByUser(_conection, user, _companyId) == true)
                        {
                            MessageBox.Show("Cambio de clave satisfactoria", "ADVERTENCIA DEL SISTEMA");
                        }
                        else
                        {
                            MessageBox.Show("Intene con otra contraseña", "ADVERTENCIA DEL SISTEMA");
                            txtContraseña.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña debe tener un nímino de 6 caracteres", "ADVERTENCIA DEL SISTEMA");
                        txtContraseña.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña debe tener un nímino de 6 caracteres", "ADVERTENCIA DEL SISTEMA");
                    txtContraseña.Focus();
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
