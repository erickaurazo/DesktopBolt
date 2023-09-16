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
    public partial class RegistroTicketsAbastecimientoMateriaPrimaFormato03 : Form
    {
        #region Variables() 
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS dsReporte;
        private Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter adaptador;
        private int correlativo;
        #endregion 

        public RegistroTicketsAbastecimientoMateriaPrimaFormato03()
        {
            InitializeComponent();
        }

        public RegistroTicketsAbastecimientoMateriaPrimaFormato03(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crptFormato02.PrintReport();
            try
            {
                
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato03.rpt");                
                crptFormato02.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion
        }

        private void RegistroTicketsAbastecimientoMateriaPrimaFormato03_Load(object sender, EventArgs e)
        {

        }
    }
}
