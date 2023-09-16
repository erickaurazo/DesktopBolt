using Asistencia.Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComparativoHorasVisualSATNISIRA.Produccion
{
    public partial class CantidadDeTickets : Form
    {
        private string _tipo;
        private SAS_TicketReservadoController model;
        private short cantidadDeTicketsAGenerar = 2;
        private string _tipoTicketAGenerar;

        public CantidadDeTickets()
        {
            InitializeComponent();
        }

        public CantidadDeTickets(string tipo)
        {
            InitializeComponent();
            _tipo = tipo;
            this.txtTipoTicket.Text = _tipo;


        }

        private void CantidadDeTickets_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerarTicketConvencionales_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = !true;
            btnGenerarTicketConvencionales.Enabled = false;
            cantidadDeTicketsAGenerar = Convert.ToInt16(this.txtCantidad.Value);
            if (_tipo.ToUpper() == "CONVENCIONAL")
            {
                _tipoTicketAGenerar = "C";
            }
            else
            {
                _tipoTicketAGenerar = "O";
            }

            txtCantidad.Enabled = false;

            bgwHilo.RunWorkerAsync();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            model = new SAS_TicketReservadoController();
            model.RegisterticketsbyQuantity("NSFAJAS", cantidadDeTicketsAGenerar, _tipoTicketAGenerar);
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Creación realizada exitosamente", "CONFIRMACION DEL SISTEMA");
            btnOk.Enabled = true;
            btnGenerarTicketConvencionales.Enabled = false;
        }
    }
}
