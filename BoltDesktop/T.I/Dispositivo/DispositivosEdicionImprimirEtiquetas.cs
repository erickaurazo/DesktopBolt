using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.T.I.Dispositivo;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class DispositivosEdicionImprimirEtiquetas : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        //private DispositivosEdicionImprimirEtiquetasQRDS dsReporte;
        private DispositivosImprimirEtiquetasQRDS dsReporte;
        private Dispositivo.DispositivosImprimirEtiquetasQRDSTableAdapters.SAS_DispositivoImpresionDeTicketsQRTableAdapter adaptador;

        //private DispositivosEdicionImprimirEtiquetasQRDSTableAdapters._SAS_DispositivoImpresionDeTicketsQRTableAdapter adaptador;        
        private int correlativo;

        public DispositivosEdicionImprimirEtiquetas()
        {
            InitializeComponent();
        }

        public DispositivosEdicionImprimirEtiquetas(int codigo)
        {            
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                //dsReporte = new DispositivosEdicionImprimirEtiquetasQRDS();
                dsReporte = new DispositivosImprimirEtiquetasQRDS();
                //adaptador = new DispositivosEdicionImprimirEtiquetasQRDSTableAdapters._SAS_DispositivoImpresionDeTicketsQRTableAdapter();
                adaptador = new Dispositivo.DispositivosImprimirEtiquetasQRDSTableAdapters.SAS_DispositivoImpresionDeTicketsQRTableAdapter();
                dsReporte.EnforceConstraints = false;
                //adaptador.Fill(dsReporte._SAS_DispositivoImpresionDeTicketsQR, this.correlativo);
                adaptador.Fill(dsReporte.SAS_DispositivoImpresionDeTicketsQR, this.correlativo);
                dta = new DataTable();
                //if (dsReporte._SAS_DispositivoImpresionDeTicketsQR.Rows.Count <= 0)
                if (dsReporte.SAS_DispositivoImpresionDeTicketsQR.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\DispositivosEdicionImprimirEtiquetasQRRPT.rpt");
                //oRpt.Load(@"X:\Saturno\Reportes\NET\DispositivosEdicionImprimirEtiquetasQRRPT.rpt");
                oRpt.Load(@"C:\SOLUTION\DispositivosEdicionImprimirEtiquetasQRRPT.rpt");
                //dta = dsReporte._SAS_DispositivoImpresionDeTicketsQR;
                dta = dsReporte.SAS_DispositivoImpresionDeTicketsQR;
                oRpt.SetDataSource(dta);
                crystalReportViewer1.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion

        }

        private void DispositivosEdicionImprimirEtiquetas_Load(object sender, EventArgs e)
        {

        }
    }
}
