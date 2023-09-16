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
    public partial class Login : Form
    {
        public string conection = "SAS";
        public SAS_USUARIOS user;
        public string companyId;
        public string descripcionConexion = string.Empty;
        private int cont = 0;
        private LoginController Modelo;
        private List<Grupo> dbs;
        private List<Grupo> companies;
        private string usuario;
        private ComboBoxHelper comboBoxHelper;
        private Grupo config;
        private string userId;
        private string password;
        private string databaseId;


        public Login()
        {
            InitializeComponent();
            //Inicio();
            InicialControls();
            cbodb.SelectedValue = "001";
            cboEmpresa.SelectedValue = "001";
            txtUsuario.Focus();

        }

        private void ShowCboDatabases()
        {
            #region Bases de Datos()
            cbodb.DisplayMember = "Descripcion";
            cbodb.ValueMember = "Codigo";
            cbodb.DataSource = dbs.ToList();
            cbodb.Refresh();
            cbodb.SelectedValue = "000";
            #endregion
        }

        private void ShowCboCompanies()
        {

            #region Empresas()
            cboEmpresa.DisplayMember = "Descripcion";
            cboEmpresa.ValueMember = "Codigo";
            cboEmpresa.DataSource = companies.ToList();
            cboEmpresa.Refresh();
            cboEmpresa.SelectedValue = "000";
            this.txtUsuario.Focus();
            // this.cboEmpresa.Enabled = false;
            #endregion

        }

        private void InicialControls()
        {
            companies = new List<Grupo>();
            dbs = new List<Grupo>();
            Modelo = new LoginController();
            comboBoxHelper = new ComboBoxHelper();

            config = new Grupo();
            var globalHelper = new GlobalesHelper();
            config = globalHelper.GetConfigInitialByLogin();

            dbs = comboBoxHelper.GetComboBoxDBsByLogin();
            companies = comboBoxHelper.GetComboBoxCompanysByLogin(conection);
            ShowCboDatabases();
            ShowCboCompanies();
        }

        private void InicialControls(string conexion)
        {
            companies = new List<Grupo>();
            comboBoxHelper = new ComboBoxHelper();
            //config = new Grupo();
            //var globalHelper = new GlobalesHelper();
            //config = globalHelper.GetConfigInitialByLogin();
            companies = comboBoxHelper.GetComboBoxCompanysByLogin(conection.Trim() != string.Empty ? conection : "SAS");
            ShowCboCompanies();
        }

        public void Inicio()
        {
            try
            {
                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "erick.aurazo";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "Erick Aurazo";
                this.txtUsuario.Focus();
            }
            catch (Exception Ex)
            {

                throw Ex;
                return;
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateForm()
        {
            bool estado = false;
            string msg = string.Empty;
            TextBox control = new TextBox();

            if (this.txtUsuario.Text.Trim() != string.Empty && this.txtContraseña.Text != string.Empty)
            {
                //control = txtUsuario;
                //msg += "Falta ingresar el usuario \n";
                //lblErrorUsuario.Visible = true;     
                if (cbodb != null && cboEmpresa != null)
                {
                    if (cbodb.SelectedValue.ToString().Trim() != "000" && cboEmpresa.SelectedValue.ToString().Trim() != "00")
                    {
                        companyId = cboEmpresa.SelectedValue.ToString().Trim();
                        userId = this.txtUsuario.Text.Trim();
                        password = this.txtContraseña.Text.Trim();
                        databaseId = this.cbodb.SelectedValue.ToString().Trim();
                        estado = true;
                    }
                }
            }
            //else
            //{
            //    lblErrorUsuario.Visible = false;
            //}

            //if (this.txtContraseña.Text == string.Empty)
            //{
            //    control = txtUsuario;
            //    msg += "Falta ingresar la Contraseña  \n";
            //    lblErrorContraseña.Visible = true;
            //    estado = false;
            //}
            //else
            //{
            //    lblErrorContraseña.Visible = false;
            //}


            //if (estado == false)
            //{
            //    //MessageBox.Show(msg, "Atención");
            //    lblMensajeUsuario.Text = msg;
            //    lblMensajeUsuario.Visible = true;
            //    control.Focus();
            //}
            //else
            //{
            //    lblMensajeUsuario.Text = string.Empty;
            //    lblMensajeUsuario.Visible = false;
            //}

            return estado;

        }

        private int CheckStatusSession(string usuario, string clave, string companyId, string conection)
        {
            int estadoOperacion = 0;
            Modelo = new LoginController();
            user = new SAS_USUARIOS();
            user = Modelo.VerificarStatusDeInicioSesion(usuario, clave, companyId, "SAS");

            if (user.IdUsuario.Trim() != string.Empty)
            {
                if (user.idestado.Trim() == "1")
                {
                    if (user.Password.Trim() != clave)
                    {
                        if ((user.Password == string.Empty))
                        {
                            estadoOperacion = 1; // en blanco
                        }
                        else
                        {
                            estadoOperacion = 2; // clave incorrecta
                        }
                    }
                }
                else
                {
                    estadoOperacion = 3; // Inactivo
                }
            }
            else
            {
                estadoOperacion = 4; // No existe
            }

            return estadoOperacion;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtUsuario.Focus();
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            try
            {
                #region 
                if (cbodb.SelectedValue.ToString() != "000" && cboEmpresa.SelectedValue.ToString().Trim() != "000")
                {
                    #region MyRegion                
                    if (this.txtUsuario.Text.Trim() != string.Empty && this.txtContraseña.Text.Trim() != string.Empty)
                    {
                        if (this.txtContraseña.Text.Trim().Length > 5)
                        {
                            IniciarSession();
                        }
                        else
                        {
                            MessageBox.Show("Ingrese una contraseña de 6 dígitos como mínimo", "ADVERTENCIA DEL SISTEMA");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese clave y/o contraseña válido", "ADVERTENCIA DEL SISTEMA");
                        return;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Seleccione empresa y base de conexión", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }



        }

        private void IniciarSession()
        {
            try
            {
                companyId = string.Empty;
                userId = string.Empty;
                password = string.Empty;
                databaseId = string.Empty;
                if (ValidateForm() == true)
                {
                    #region Verificar el estado de la sessión()
                    int result = CheckStatusSession(userId.ToUpper(), password, companyId, conection);
                    cont += 1;
                    switch (result)
                    {

                        case 0: // Ok
                            usuario = this.txtUsuario.Text.Trim();
                            this.btnOK.PerformClick();
                            break;

                        case 1: // blanco
                                // Formulario para configurar contraseña()
                            GoSistemaCatalogoUsersChangePassword ofrm = new GoSistemaCatalogoUsersChangePassword(conection, userId.ToUpper(), companyId);
                            ofrm.ShowDialog();
                            break;

                        case 2: // incorrecto
                            NumberOfAttemps(cont);
                            MessageBox.Show("Ha intentado " + cont.ToString() + " veces \nLa contraseña es incorrecta", "ADVERTENCIA DEL SISTEMA");
                            if (cont == 3)
                            {
                                this.Close();
                            }
                            break;


                        case 3: // Inactivo
                            MessageBox.Show("Ha intentado " + cont.ToString() + " veces \nEl Usuario se encuentra inactivo", "ADVERTENCIA DEL SISTEMA");
                            if (cont == 3)
                            {
                                this.Close();
                            }
                            break;

                        case 4: // No existe
                            NumberOfAttemps(cont);
                            break;

                        default:
                            break;
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

        private void NumberOfAttemps(int cont)
        {
            if (cont < 4)
            {
                cont += 1;
                if (cont == 3)
                {
                    usuario = string.Empty;
                    MessageBox.Show("Ha intentado " + cont.ToString() + " veces \nSu contraseña y/o el sistema se cerrara al tercer intento", "ADVERTENCIA DEL SISTEMA");
                    this.txtContraseña.Clear();
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    usuario = string.Empty;
                    MessageBox.Show(cont.ToString() + " intentos fallidos ");
                    this.txtContraseña.Clear();
                    this.txtUsuario.Focus();
                }
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            //this.Dispose();
            this.Close();
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                IniciarSession();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                // Lo que hará al presionar Escape 
                txtContraseña.Focus();
            }
        }

        private void cboBasesDatos_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cbodb.SelectedIndex > -1)
                {
                    conection = string.Empty;
                    #region
                    var codigoBaseDatos = cbodb.SelectedValue.ToString().Trim();
                    var descripcionBaseDatos = cbodb.Text.Trim();
                    if (codigoBaseDatos != "000" && descripcionBaseDatos != string.Empty)
                    {
                        // Lenar combo de empresas
                        string basedatos = string.Empty;

                        string[] db01 = descripcionBaseDatos.Split('|');
                        if (db01[1] != null)
                        {
                            // si tiene al menos un caracter '|' es decir discrimida entre una ip publica y local
                            var nombreIntanciaSelecionada = db01[1].Trim();
                            // si tiene la base por ejemplo así basedatosproduccion | Local , 
                            // seleccionare en el archivo de configuración la instancia local con el nombre de base de datos: basedeproduccion
                            if (nombreIntanciaSelecionada.Contains("Publica"))
                            {
                                basedatos = db01[0].Trim();
                                conection = ("P" + basedatos);
                                descripcionConexion = "Conexión remota | db: " + basedatos;
                                InicialControls(conection);

                            }
                            else if (nombreIntanciaSelecionada.Contains("Local"))
                            {
                                basedatos = db01[0].Trim();
                                conection = (basedatos);
                                descripcionConexion = "Conexión local | db: " + basedatos;
                                InicialControls(conection);
                            }
                        }
                        else
                        {
                            // si no tiene el caracter '|' sólo se configura o una ip publica o una local
                        }
                    }
                    else
                    {
                        InicialControls(conection);
                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }


        }
    }
}
