using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones;

namespace ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones
{
    public partial class RegistroDeCapacitacionesVistaPreviaIndividual : Form
    {

        string conexion = string.Empty;
        private string ID = string.Empty;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeCapacitacionesVistaPreviaIndividualDS dsReporte;        
        private SIG.SST.Registro_de_Capacitaciones.RegistroDeCapacitacionesVistaPreviaIndividualDSTableAdapters.RegistroDeCapacitacionesVistaPreviaIndividualReporteTableAdapter adaptador;
        private string TemaID = string.Empty;

        public RegistroDeCapacitacionesVistaPreviaIndividual()
        {
            InitializeComponent();
        }


        public RegistroDeCapacitacionesVistaPreviaIndividual(string _conexion, string _ID, string _TemaID)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            ID = _ID;
            TemaID = _TemaID;

            this.crAgrupado.PrintReport();
            try
            {
                dsReporte = new RegistroDeCapacitacionesVistaPreviaIndividualDS();
                adaptador = new RegistroDeCapacitacionesVistaPreviaIndividualDSTableAdapters.RegistroDeCapacitacionesVistaPreviaIndividualReporteTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.RegistroDeCapacitacionesVistaPreviaIndividualReporte, ID, TemaID);
                dta = new DataTable();
                if (dsReporte.RegistroDeCapacitacionesVistaPreviaIndividualReporte.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"C:\SOLUTION\RegistroDeCapacitacionesVistaPreviaAgrupadoRPT.rpt");
                oRpt.Load(@"C:\Users\eaurazo.SAGSA\Source\Repos\erickaurazo\DesktopBolt\BoltDesktop\SIG\SST\Registro de Capacitaciones\RegistroDeCapacitacionesVistaPreviaIndividualRPT.rpt");
                oRpt.SetDatabaseLogon("tmovil", "admin");
                dta = dsReporte.RegistroDeCapacitacionesVistaPreviaIndividualReporte;
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

        private void RegistroDeCapacitacionesVistaPreviaIndividual_Load(object sender, EventArgs e)
        {

        }
    }
}
