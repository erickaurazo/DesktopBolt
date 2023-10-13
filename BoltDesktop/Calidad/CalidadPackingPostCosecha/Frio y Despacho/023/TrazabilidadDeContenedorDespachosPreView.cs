using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Gasificado;

namespace ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._023
{
    public partial class TrazabilidadDeContenedorDespachosPreView : Form
    {
        #region Variables() 
        string conexion = string.Empty;
        int codigo = 0;
        public int IdEvaluacion = 0;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        //private RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodoTableAdapter adaptador;
        private Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleCargaByIdTableAdapter adaptador;

        #endregion

        public TrazabilidadDeContenedorDespachosPreView()
        {
            InitializeComponent();
        }

        public TrazabilidadDeContenedorDespachosPreView(string _conexion, int _IdEvaluacion)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            IdEvaluacion = _IdEvaluacion;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleCargaByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleCargaById, IdEvaluacion);
                dta = new DataTable();
                if (dsReporte.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleCargaById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();

                oRpt.Load(@"C:\SOLUTION\TrazabilidadDeContenedorDespachosRTP.rpt");
                //oRpt.Load(@"C:\Users\eaurazo.SAGSA\Source\Repos\erickaurazo\DesktopBolt\BoltDesktop\Calidad\CalidadPackingPostCosecha\Frio y Despacho\023\TrazabilidadDeContenedorDespachosRTP.rpt");
                oRpt.SetDatabaseLogon("sa", "usersql$$nisira");
                dta = dsReporte.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleCargaById;
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



        private void TrazabilidadDeContenedorDespachosPreView_Load(object sender, EventArgs e)
        {

        }
    }
}
