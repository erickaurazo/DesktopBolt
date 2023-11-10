using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Gasificado;

namespace ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._045
{
    public partial class ChecklistRevisionDelContenedorPreview : Form
    {

        #region Variables() 
        string conexion = string.Empty;
        int codigo = 0;
        public int IdEvaluacion = 0;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ReporteCheckListRevisionByIdEvaluacionTableAdapter adaptador;

        #endregion

        public ChecklistRevisionDelContenedorPreview()
        {
            InitializeComponent();
        }

        public ChecklistRevisionDelContenedorPreview(string _conexion, int _IdEvaluacion)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            IdEvaluacion = _IdEvaluacion;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ReporteCheckListRevisionByIdEvaluacionTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_ReporteCheckListRevisionByIdEvaluacion, IdEvaluacion);
                dta = new DataTable();
                if (dsReporte.SAS_ReporteCheckListRevisionByIdEvaluacion.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ChecklistRevisionDelContenedorRPT.rpt");
                //oRpt.Load(@"C:\Users\eaurazo.SAGSA\Source\Repos\erickaurazo\DesktopBolt\BoltDesktop\Calidad\CalidadPackingPostCosecha\Frio y Despacho\045\ChecklistRevisionDelContenedorRPT.rpt");
                oRpt.SetDatabaseLogon("eaurazo", "J^G|T1jS");
                dta = dsReporte.SAS_ReporteCheckListRevisionByIdEvaluacion;
                oRpt.SetDataSource(dta);
                crystalReportViewer1.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Reporte de inspección", "MENSAJE DEL SISTEMA");
                return;
            }
            #endregion

        }

        private void ChecklistRevisionDelContenedorPreview_Load(object sender, EventArgs e)
        {

        }
    }
}
