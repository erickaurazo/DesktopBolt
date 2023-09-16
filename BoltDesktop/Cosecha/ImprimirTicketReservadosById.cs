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
    public partial class ImprimirTicketReservadosById : Form
    {

        #region Variables() 
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ImprimirTicketReservadosByIdDS dsReporte;
        private Cosecha.ImprimirTicketReservadosByIdDSTableAdapters.SAS_TicketReservadoByIdTableAdapter adaptador;
        private int correlativo;
        #endregion 


        public ImprimirTicketReservadosById()
        {
            InitializeComponent();
        }


        public ImprimirTicketReservadosById(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crptFormato01.PrintReport();
            try
            {
                dsReporte = new ImprimirTicketReservadosByIdDS();
                adaptador = new Cosecha.ImprimirTicketReservadosByIdDSTableAdapters.SAS_TicketReservadoByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_TicketReservadoById, this.correlativo);
                dta = new DataTable();
                if (dsReporte.SAS_TicketReservadoById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosByIdRPT.rpt");
                dta = dsReporte.SAS_TicketReservadoById;
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


        private void ImprimirTicketReservadosById_Load(object sender, EventArgs e)
        {

        }
    }
}
