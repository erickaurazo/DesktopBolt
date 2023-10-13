using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using Asistencia.Datos;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Gasificado;

namespace ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha.BPM._014
{
    public partial class CheckListBuenasPractivasManufacturaPreview : Form
    {

        string conexion = string.Empty;
        int codigo = 0;
        public SAS_ListadoCheckListManufacturaAllByDatesResult item;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private RegistroDeIngresoSalidaGasificadoDS dsReporte;
        private CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodoTableAdapter adaptador;

        public CheckListBuenasPractivasManufacturaPreview()
        {
            InitializeComponent();
        }

        public CheckListBuenasPractivasManufacturaPreview(string _conexion, SAS_ListadoCheckListManufacturaAllByDatesResult _item)
        {
            #region Vista previa PDF
            InitializeComponent();
            conexion = _conexion;
            item = _item;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new RegistroDeIngresoSalidaGasificadoDS();
                adaptador = new CalidadPackingPostCosecha.Gasificado.RegistroDeIngresoSalidaGasificadoDSTableAdapters.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodoTableAdapter();
                dsReporte.EnforceConstraints = false;
                adaptador.Fill(dsReporte.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodo, item.periodo, item.Semana);
                dta = new DataTable();
                if (dsReporte.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodo.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\CheckListBuenasPractivasManufacturaRPT.rpt");
                //oRpt.Load(@"C:\Users\LENOVO\OneDrive\Documentos\Visual Studio 2015\Projects\BoltDesktop\BoltDesktop\Calidad\CalidadPackingPostCosecha\BPM\014\CheckListBuenasPractivasManufacturaRPT.rpt");
                dta = dsReporte.SAS_ListadoCheckListBuenasPracticasManufacturaAllByWeekPeriodo;
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

        private void CheckListBuenasPractivasManufacturaPreview_Load(object sender, EventArgs e)
        {

        }
    }
}
