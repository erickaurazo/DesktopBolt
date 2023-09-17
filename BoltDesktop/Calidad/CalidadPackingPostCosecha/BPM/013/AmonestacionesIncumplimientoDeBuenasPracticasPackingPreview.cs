using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;

namespace ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._013
{
    public partial class AmonestacionesIncumplimientoDeBuenasPracticasPackingPreview : Form
    {
        string conexion = string.Empty;
        int codigo = 0;
        public SAS_ListadoAmonestacionesIncumplimientosByDatesResult item;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoAmonestacionesIncumplimientosByIdTableAdapter adaptador;


        public AmonestacionesIncumplimientoDeBuenasPracticasPackingPreview()
        {
            InitializeComponent();
        }

        private void AmonestacionesIncumplimientoDeBuenasPracticasPackingPreview_Load(object sender, EventArgs e)
        {

        }

        public AmonestacionesIncumplimientoDeBuenasPracticasPackingPreview(string _conexion, SAS_ListadoAmonestacionesIncumplimientosByDatesResult _item)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            item = _item;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoAmonestacionesIncumplimientosByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_ListadoAmonestacionesIncumplimientosById, Convert.ToInt32(item.CabeceraId));
                dta = new DataTable();
                if (dsReporte.SAS_ListadoAmonestacionesIncumplimientosById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                //oRpt.Load(@"C:\SOLUTION\CumplimientoDiarioDeLavadoDeManosRPT.rpt");
                oRpt.Load(@"C:\Users\LENOVO\OneDrive\Documentos\Visual Studio 2015\Projects\BoltDesktop\BoltDesktop\Calidad\CalidadPackingPostCosecha\BPM\013\AmonestacionesIncumplimientoDeBuenasPracticasPackingRPT.rpt");
                dta = dsReporte.SAS_ListadoAmonestacionesIncumplimientosById;
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


    }
}
