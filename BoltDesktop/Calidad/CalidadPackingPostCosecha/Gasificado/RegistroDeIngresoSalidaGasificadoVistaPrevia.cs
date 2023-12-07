using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Gasificado;

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class RegistroDeIngresoSalidaGasificadoVistaPrevia : Form
    {

        string conexion = string.Empty;
        string desde = string.Empty;
        string hasta = string.Empty;

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_RegistroGasificadoAllByDatesTableAdapter adaptador;

        public RegistroDeIngresoSalidaGasificadoVistaPrevia()
        {
            InitializeComponent();
        }


        public RegistroDeIngresoSalidaGasificadoVistaPrevia(string _conexion, string _desde, string _hasta)
        {
            #region 
            InitializeComponent();
            conexion = _conexion;
            desde = _desde;
            hasta = _hasta;

            crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_RegistroGasificadoAllByDatesTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_RegistroGasificadoAllByDates, desde, hasta);
                dta = new DataTable();

                if (dsReporte.SAS_RegistroGasificadoAllByDates.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\RegistroDeIngresoSalidaGasificadoRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Calidad\RegistroDeIngresoSalidaGasificadoRPT.rpt");

                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Calidad\RegistroDeIngresoSalidaGasificadoRPT.rpt");                
                dta = dsReporte.SAS_RegistroGasificadoAllByDates;
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


        private void RegistroDeIngresoSalidaGasificadoVistaPrevia_Load(object sender, EventArgs e)
        {

        }
    }
}
