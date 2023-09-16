using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using ComparativoHorasVisualSATNISIRA.T.I;


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class DispositivosCambioEstado : Form
    {
        private string _estado;
        private int _idDispositivo;
        private string _nombreDispositivo;
        private string _oConexion;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user2;
        private ComboBoxHelper comboHelper;
        private List<Grupo> listado;
        private SAS_DispostivoController deviceModelo;
        private int idMotivoCambioEstado;

        public DispositivosCambioEstado()
        {
            InitializeComponent();
        }
        public DispositivosCambioEstado(string oConexion, int idDispositivo, string nombreDispositivo, string estado, SAS_USUARIOS user2, string companyId, PrivilegesByUser privilege)
        {
            _oConexion = oConexion;
            _idDispositivo = idDispositivo;
            _user2 = user2;
            _privilege = privilege;
            _nombreDispositivo = nombreDispositivo;
            _estado = estado;
            idMotivoCambioEstado = -1;
            InitializeComponent();

            this.txtCodigo.Text = _idDispositivo.ToString().PadLeft(7, '0');
            this.txtNombre.Text = _nombreDispositivo.Trim().ToUpper();
            this.txtEstado.Text = _estado.Trim().ToUpper();
            this.txtMotivo.Clear();
            this.txtMotivo.Focus();
            CargarCombos();

        }


        private void CargarCombos()
        {

            comboHelper = new ComboBoxHelper();
            listado = new List<Grupo>();

            listado = comboHelper.GetTypeStatusByDevice("SAS");
            cboMotivo.DisplayMember = "Descripcion";
            cboMotivo.ValueMember = "Codigo";
            cboMotivo.DataSource = listado.ToList();


        }



        private void DispositivosCambioEstado_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Confirmar();
        }

        private void Confirmar()
        {
            try
            {
                #region Confirmar()
                idMotivoCambioEstado = cboMotivo.SelectedValue != string.Empty ? Convert.ToInt32(cboMotivo.SelectedValue) : 0;
                string motivo = this.txtMotivo.Text.Trim();
                if (this.txtMotivo.Text.Length > 10 && idMotivoCambioEstado >= 1)
                {
                    deviceModelo = new SAS_DispostivoController();
                    int resultadoRespuesta = deviceModelo.ChangeStatusDevice("SAS",_idDispositivo, idMotivoCambioEstado, motivo);
                }
                else
                {
                    MessageBox.Show("Ingrese una descripción válida", "Mensaje del sistema");
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
