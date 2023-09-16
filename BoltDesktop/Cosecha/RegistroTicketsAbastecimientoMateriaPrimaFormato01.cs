using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.Produccion;
using ComparativoHorasVisualSATNISIRA.Cosecha;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ImpresionTicketsAbastecimientoMateriaPrimaImprimir : Form
    {
        #region Variables() 
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12; 
        private DataTable dta;
        private ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS dsReporte;
        private Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter adaptador;
        private int correlativo;
        #endregion 

        public ImpresionTicketsAbastecimientoMateriaPrimaImprimir()
        {
            #region 
            InitializeComponent();
            //this.correlativo = 3;
            //oRpt = new ReportDocument();            

            //crystalReportViewer1.PrintReport();
            //try
            //{
            //    dsReporte = new ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS();
            //    adaptador = new ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter();
            //    dsReporte.EnforceConstraints = false;
            //    adaptador.Fill(dsReporte.ListadoAcopioByTiktesByCorrelativo, this.correlativo);
            //    dta = new DataTable();
            //    if (dsReporte.ListadoAcopioByTiktesByCorrelativo.Rows.Count <= 0)
            //    {
            //        MessageBox.Show("No se encontró información para imprimir !");
            //        return;
            //    }
            //    oRpt = new ReportDocument();
                                
            //    //oRpt.Load(@"D:\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
            //    oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\ImpresionTicketsAbastecimientoMateriaPrimaImprimirRPT.rpt");
            //    //oRpt.Load(@"X:\Rpt_Solution\AplicacionesNet\rrhh\new\FacturacionMovilidadesImprimirDetalleByTipoTransporteRPT.rpt");
            //    //oRpt.Load(@"C:\Users\Erick\Dropbox\Agricola SJ\Desarrollo\Aplicaciones\Recursos Humanos\RecursosHumanos\PensionistasRefrigerios\TicketPrivilegioRefrigerioImprimirRPT.rpt");
            //    //oRpt.Load(@"C:\Users\erick.aurazo\Dropbox\Agricola SJ\Desarrollo\Aplicaciones\Recursos Humanos\RecursosHumanos\PensionistasRefrigerios\TicketPrivilegioRefrigerioImprimirRPT.rpt");
            //    dta = dsReporte.ListadoAcopioByTiktesByCorrelativo;
            //    oRpt.SetDataSource(dta);


            //    crystalReportViewer1.ReportSource = oRpt;
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
            //    return;
            //}
            #endregion
        }

        public ImpresionTicketsAbastecimientoMateriaPrimaImprimir(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crptFormato01.PrintReport();
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
                oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato01.rpt");                
                dta = dsReporte.ListadoAcopioByTiktesByCorrelativo;
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

        private void ImpresionTicketsAbastecimientoMateriaPrimaImprimir_Load(object sender, EventArgs e)
        {

        }

    }
}
