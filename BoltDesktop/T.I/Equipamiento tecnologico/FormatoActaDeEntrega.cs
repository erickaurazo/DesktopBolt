using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.T.I.Equipamiento_tecnologico;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class FormatoActaDeEntrega : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private FormatoActaDeEntregaDS dsReporte;
        private Equipamiento_tecnologico.FormatoActaDeEntregaDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudAltaTableAdapter adaptador;
        private int correlativo;

        public FormatoActaDeEntrega()
        {
            InitializeComponent();
        }



        public FormatoActaDeEntrega(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new FormatoActaDeEntregaDS();
                adaptador = new Equipamiento_tecnologico.FormatoActaDeEntregaDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudAltaTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAlta, this.correlativo);
                dta = new DataTable();

                if (dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAlta.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\FormatoActaDeEntregaRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\FormatoActaDeEntregaRPT.rpt");
                dta = dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAlta;
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

        private void FormatoActaDeEntrega_Load(object sender, EventArgs e)
        {

        }
    }
}
