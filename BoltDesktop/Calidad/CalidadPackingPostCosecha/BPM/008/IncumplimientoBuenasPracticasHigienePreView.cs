using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Gasificado;

namespace ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha
{
    public partial class IncumplimientoBuenasPracticasHigieneView : Form
    {

        string conexion = string.Empty;        
        private int codigo;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ReporteIncumplimientoPracticasHigieneByIdTableAdapter adaptador;

        public IncumplimientoBuenasPracticasHigieneView()
        {
            InitializeComponent();
        }


        public IncumplimientoBuenasPracticasHigieneView(string _conexion, int _codigo)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            codigo = _codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ReporteIncumplimientoPracticasHigieneByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_ReporteIncumplimientoPracticasHigieneById, codigo);
                dta = new DataTable();
                if (dsReporte.SAS_ReporteIncumplimientoPracticasHigieneById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\IncumplimientoBuenasPracticasHigieneRTP.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Calidad\CalidadPackingPostCosecha\BPM\008\IncumplimientoBuenasPracticasHigieneRTP.rpt");               
                dta = dsReporte.SAS_ReporteIncumplimientoPracticasHigieneById;
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


        private void IncumplimientoBuenasPracticasHigieneView_Load(object sender, EventArgs e)
        {

        }
    }
}
