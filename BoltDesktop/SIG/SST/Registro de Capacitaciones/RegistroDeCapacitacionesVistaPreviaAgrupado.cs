using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones;

namespace ComparativoHorasVisualSATNISIRA.SIG.SST
{
    public partial class RegistroDeCapacitacionesVistaPrevia : Form
    {

        string conexion = string.Empty;
        private string ID;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeCapacitacionesVistaPreviaAgrupadoDS dsReporte;        
        private SIG.SST.Registro_de_Capacitaciones.RegistroDeCapacitacionesVistaPreviaAgrupadoDSTableAdapters.RegistroDeCapacitacionesVistaPreviaAgrupadoReporteTableAdapter adaptador;


        public RegistroDeCapacitacionesVistaPrevia()
        {
            InitializeComponent();
        }


        public RegistroDeCapacitacionesVistaPrevia(string _conexion, string _ID)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            ID = _ID;

            this.crAgrupado.PrintReport();
            try
            {
                dsReporte = new RegistroDeCapacitacionesVistaPreviaAgrupadoDS();
                adaptador = new Registro_de_Capacitaciones.RegistroDeCapacitacionesVistaPreviaAgrupadoDSTableAdapters.RegistroDeCapacitacionesVistaPreviaAgrupadoReporteTableAdapter();
                dsReporte.EnforceConstraints = false;
                
                adaptador.Fill(dsReporte.RegistroDeCapacitacionesVistaPreviaAgrupadoReporte, ID);
                dta = new DataTable();
                if (dsReporte.RegistroDeCapacitacionesVistaPreviaAgrupadoReporte.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"C:\SOLUTION\RegistroDeCapacitacionesVistaPreviaAgrupadoRPT.rpt");
                oRpt.Load(@"C:\Users\eaurazo.SAGSA\Source\Repos\erickaurazo\DesktopBolt\BoltDesktop\SIG\SST\Registro de Capacitaciones\RegistroDeCapacitacionesVistaPreviaAgrupadoRPT.rpt");
                oRpt.SetDatabaseLogon("tmovil", "admin");
                dta = dsReporte.RegistroDeCapacitacionesVistaPreviaAgrupadoReporte;
                oRpt.SetDataSource(dta);
                crAgrupado.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Reporte Agrupado de registro de asistencia", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion

        }


        private void RegistroDeCapacitacionesVistaPrevia_Load(object sender, EventArgs e)
        {

        }

        private void crAgrupado_Load(object sender, EventArgs e)
        {

        }
    }
}
