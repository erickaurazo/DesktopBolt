using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;

namespace ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._010
{
    public partial class CumplimientoDiarioDeLavadoDeManosPreview : Form
    {

        string conexion = string.Empty;
        int codigo = 0;
        public SAS_ListadoLavadoDeManoAllByDatesResult item;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoLavadoDeManoBySemanaPeriodoRPTTableAdapter adaptador;

        public CumplimientoDiarioDeLavadoDeManosPreview()
        {
            InitializeComponent();
        }


        public CumplimientoDiarioDeLavadoDeManosPreview(string _conexion, SAS_ListadoLavadoDeManoAllByDatesResult _item)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            item = _item;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoLavadoDeManoBySemanaPeriodoRPTTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_ListadoLavadoDeManoBySemanaPeriodoRPT,Convert.ToInt32(item.Semana),item.periodo, item.TurnoId);
                dta = new DataTable();
                if (dsReporte.SAS_ListadoLavadoDeManoBySemanaPeriodoRPT.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"C:\SOLUTION\CumplimientoDiarioDeLavadoDeManosRPT.rpt");
                oRpt.Load(@"C:\Users\LENOVO\OneDrive\Documentos\Visual Studio 2015\Projects\BoltDesktop\BoltDesktop\Calidad\CalidadPackingPostCosecha\BPM\010\CumplimientoDiarioDeLavadoDeManosRPT.rpt");
                dta = dsReporte.SAS_ListadoLavadoDeManoBySemanaPeriodoRPT;
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


        private void CumplimientoDiarioDeLavadoDeManosPreview_Load(object sender, EventArgs e)
        {

        }
    }
}
