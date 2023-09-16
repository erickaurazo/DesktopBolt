using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using Asistencia.Negocios;
using Asistencia.Datos;

namespace Asistencia
{
    public partial class Historial : Form
    {

        List<HistorialObj> listadoHistorial;
        HistorialController negocios;
        string codHistorial, formulario = string.Empty;
        private string tabla;

        public Historial()
        {
            InitializeComponent();
        }


        public Historial(string idHistorial, string oFormulario, string tabla)
        {

            try
            {
                InitializeComponent();
                codHistorial = idHistorial;
                this.tabla = tabla;
                this.formulario = oFormulario;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                if (formulario == "0") /*0 ES PROPIO*/
                {
                    listadoHistorial = new List<HistorialObj>();
                    negocios = new HistorialController();

                    listadoHistorial = negocios.ListarHistorialSJ(codHistorial.ToString().Trim().PadLeft(16, '0'), tabla).ToList();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (listadoHistorial != null)
                {
                    dgvHistorial.CargarDatos(listadoHistorial.ToDataTable<HistorialObj>());
                    dgvHistorial.Refresh();

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void Historial_Load(object sender, EventArgs e)
        {

        }
    }
}
