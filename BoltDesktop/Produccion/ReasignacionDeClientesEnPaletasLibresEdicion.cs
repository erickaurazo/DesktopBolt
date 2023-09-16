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
using System.Globalization;
using ComparativoHorasVisualSATNISIRA.Produccion;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ReasignacionDeClientesEnPaletasLibresEdicion : Form
    {
        private PaletasPendientesParaPackingList oRegistro;
        private string oclienteAnterior;
        private string onombreClienteAnterior;
        private PaletasPendientesParaPackingListControllers modelo;

        public ReasignacionDeClientesEnPaletasLibresEdicion()
        {
            InitializeComponent();
        }

        public ReasignacionDeClientesEnPaletasLibresEdicion(PaletasPendientesParaPackingList oRegistro, string clienteAnterior, string nombreClienteAnterior)
        {
            InitializeComponent();
            Inicio();
            this.oRegistro = oRegistro;
            this.oclienteAnterior = clienteAnterior;
            this.onombreClienteAnterior = nombreClienteAnterior;
            this.txtPeriodoContable.BackColor = System.Drawing.Color.LawnGreen;
            this.txtPeriodoAlmacen.BackColor = System.Drawing.Color.LawnGreen;
            this.txtPeriodoContable.ReadOnly = true;
            this.txtPeriodoAlmacen.ReadOnly = true;

            if (this.oRegistro.CierraContable == 0)
            {
                this.txtPeriodoContable.Text = "Aperturado para el periodo " + this.oRegistro.FECHA.Value.ToString("yyyyMM");
            }
            else
            {
                this.txtPeriodoContable.Text = "Cerrado para el periodo " + this.oRegistro.FECHA.Value.ToString("yyyyMM");
                this.txtPeriodoContable.BackColor = System.Drawing.Color.LightCoral;

            }

            if (this.oRegistro.CierreAlmacen == 0)
            {
                this.txtPeriodoAlmacen.Text = "Aperturado para el periodo " + this.oRegistro.FECHA.Value.ToString("yyyyMM");
            }
            else
            {
                this.txtPeriodoAlmacen.Text = "Cerrado para el periodo " + this.oRegistro.FECHA.Value.ToString("yyyyMM");
                this.txtPeriodoAlmacen.BackColor = System.Drawing.Color.LightCoral;
            }

            this.txtNumeroPaleta.Text = this.oRegistro.NROPALETA.Trim();
            this.txtClienteAnterior.Text = this.onombreClienteAnterior;
            this.txtCodigoClienteAnterior.Text = this.oclienteAnterior;
            this.txtCodigoClienteActual.Focus();

        }

        public void Inicio()
        {
            try
            {
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["bdSaturno"].ToString();
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void commandBarButton4_Click(object sender, EventArgs e)
        {

        }

        private void ReasignacionDeClientesEnPaletasLibresEdicion_Load(object sender, EventArgs e)
        {

        }

        private void reasigarClienteAPaletaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            gbEdición.Enabled = !false;
            btnRegistrar.Enabled = !false;
            btnEditar.Enabled = !true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (this.txtClienteActual.Text.Trim() != string.Empty)
            {
                modelo = new PaletasPendientesParaPackingListControllers();
                string mensaje = modelo.CambiarCliente("SAS", this.oRegistro.IDREFERENCIA, this.txtCodigoClienteActual.Text.Trim());                
                //mensaje = modelo.CambiarCliente("SAS", this.oRegistro.IDREFERENCIA, this.txtClienteActual.Text.Trim());                
                this.txtClienteAnterior.Text = this.txtClienteActual.Text;
                this.txtCodigoClienteAnterior.Text = this.txtCodigoClienteActual.Text;
                MessageBox.Equals(mensaje, "Mensaje del sistema");
            }

            gbEdición.Enabled = false;
            btnRegistrar.Enabled = false;
            btnEditar.Enabled = true;
        }
    }
}
