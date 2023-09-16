using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;

namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    public partial class ProductosImpresionCodigoBarras : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        //private DispositivosEdicionImprimirEtiquetasQRDS dsReporte;
        private ProductoImpresinCodigoBarrasDS dsReporte;
        private ProductoImpresinCodigoBarrasDSTableAdapters.SAS_ProductosByIdTableAdapter adaptador;


        //private DispositivosEdicionImprimirEtiquetasQRDSTableAdapters._SAS_DispositivoImpresionDeTicketsQRTableAdapter adaptador;        
        private int correlativo;
        private string _correlativo;

        public ProductosImpresionCodigoBarras()
        {
            InitializeComponent();
        }

        public ProductosImpresionCodigoBarras(string codigo)
        {
            #region 
            InitializeComponent();
            _correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                //dsReporte = new DispositivosEdicionImprimirEtiquetasQRDS();
                dsReporte = new ProductoImpresinCodigoBarrasDS();
                //adaptador = new DispositivosEdicionImprimirEtiquetasQRDSTableAdapters._SAS_DispositivoImpresionDeTicketsQRTableAdapter();
                adaptador = new ProductoImpresinCodigoBarrasDSTableAdapters.SAS_ProductosByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                //adaptador.Fill(dsReporte._SAS_DispositivoImpresionDeTicketsQR, this.correlativo);
                adaptador.Fill(dsReporte.SAS_ProductosById, _correlativo.ToString().Trim());
                dta = new DataTable();
                //if (dsReporte._SAS_DispositivoImpresionDeTicketsQR.Rows.Count <= 0)
                if (dsReporte.SAS_ProductosById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\DispositivosEdicionImprimirEtiquetasQRRPT.rpt");
                //Para cargarlo desde local
                //oRpt.Load(@"X:\Saturno\Reportes\NET\DispositivosEdicionImprimirEtiquetasQRRPT.rpt");
                //Para cargarlo desde el server
               oRpt.Load(@"C:\SOLUTION\ProductoImpresionCodigoBarraRPT.rpt");
                // oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Almacen\ProductoImpresionCodigoBarraRPT.rpt");

                //dta = dsReporte._SAS_DispositivoImpresionDeTicketsQR;
                dta = dsReporte.SAS_ProductosById;
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


        private void ProductosImpresionCodigoBarras_Load(object sender, EventArgs e)
        {

        }
    }
}
