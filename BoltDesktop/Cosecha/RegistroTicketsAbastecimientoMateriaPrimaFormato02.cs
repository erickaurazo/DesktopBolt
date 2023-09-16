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
    public partial class RegistroTicketsAbastecimientoMateriaPrimaFormato02 : Form
    {
        #region Variables() 
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS dsReporte;
        private Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter adaptador;
        private int correlativo;
        #endregion 


        public RegistroTicketsAbastecimientoMateriaPrimaFormato02()
        {
            InitializeComponent();
        }

        public RegistroTicketsAbastecimientoMateriaPrimaFormato02(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crptFormato02.PrintReport();
            try
            {
                dsReporte = new ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS();
                adaptador = new Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.ListadoAcopioByTiktesByCorrelativo, this.correlativo);
                dta = new DataTable();
                if (dsReporte.ListadoAcopioByTiktesByCorrelativo.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
                oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato02.rpt");
                dta = dsReporte.ListadoAcopioByTiktesByCorrelativo;
                oRpt.SetDataSource(dta);
                crptFormato02.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion
        }

        private void RegistroTicketsAbastecimientoMateriaPrimaFormato02_Load(object sender, EventArgs e)
        {

        }
    }
}
