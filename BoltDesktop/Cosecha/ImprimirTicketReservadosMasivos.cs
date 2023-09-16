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
    public partial class ImprimirTicketReservadosMasivos : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ImprimirTicketReservadosMasivosDS dsReporte;
        private Cosecha.ImprimirTicketReservadosMasivosDSTableAdapters.SAS_TicketReservadoByPrinterTableAdapter adaptador;
        private int correlativo;

        public ImprimirTicketReservadosMasivos()
        {
            InitializeComponent();
        }

        public ImprimirTicketReservadosMasivos(int codigo, string tipoTicket)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crptFormato01.PrintReport();
            try
            {
                dsReporte = new ImprimirTicketReservadosMasivosDS();
                adaptador = new Cosecha.ImprimirTicketReservadosMasivosDSTableAdapters.SAS_TicketReservadoByPrinterTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_TicketReservadoByPrinter, codigo, tipoTicket);
                dta = new DataTable();
                if (dsReporte.SAS_TicketReservadoByPrinter.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosMasivosRPT.rpt");
                dta = dsReporte.SAS_TicketReservadoByPrinter;
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


        private void ImprimirTicketReservadosMasivos_Load(object sender, EventArgs e)
        {

        }
    }
}
