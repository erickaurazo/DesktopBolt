using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.Produccion;
using ComparativoHorasVisualSATNISIRA.Cosecha;

namespace ComparativoHorasVisualSATNISIRA.Produccion
{
    public partial class ImprimirTicketReservadosPorRangoDeTickets : Form
    {


        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ImprimirTicketReservadosPorRangoDeTicketsDS dsReporte;
        private Cosecha.ImprimirTicketReservadosPorRangoDeTicketsDSTableAdapters.SAS_TicketReservadoByNumbersTableAdapter adaptador;
        private int _desde , _hasta;
        private string _tipoTicket;

        public ImprimirTicketReservadosPorRangoDeTickets()
        {
            InitializeComponent();
        }


        public ImprimirTicketReservadosPorRangoDeTickets(int desde,  int hasta, string tipoTicket)
        {
            #region 
            InitializeComponent();
            _desde = desde;
            _hasta = hasta;
            _tipoTicket = tipoTicket;

            this.crptFormato01.PrintReport();
            try
            {
                dsReporte = new ImprimirTicketReservadosPorRangoDeTicketsDS();
                adaptador = new Cosecha.ImprimirTicketReservadosPorRangoDeTicketsDSTableAdapters.SAS_TicketReservadoByNumbersTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_TicketReservadoByNumbers, _desde, _hasta, tipoTicket);
                dta = new DataTable();
                if (dsReporte.SAS_TicketReservadoByNumbers.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosPorRangoDeTicketsRPT.rpt");
                dta = dsReporte.SAS_TicketReservadoByNumbers;
                oRpt.SetDataSource(dta);
                crptFormato01.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion
        }


        private void ImprimirTicketReservadosPorRangoDeTickets_Load(object sender, EventArgs e)
        {

        }
    }
}
