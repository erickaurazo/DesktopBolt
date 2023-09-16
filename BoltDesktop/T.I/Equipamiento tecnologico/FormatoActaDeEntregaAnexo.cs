using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.T.I.Equipamiento_tecnologico;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class FormatoActaDeEntregaAnexo : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private FormatoActaDeEntregaAnexoDS dsReporte;
        private Equipamiento_tecnologico.FormatoActaDeEntregaAnexoDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudAltaAnexoTableAdapter adaptador;
        private int correlativo;

        public FormatoActaDeEntregaAnexo()
        {
            InitializeComponent();
        }

        public FormatoActaDeEntregaAnexo(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new FormatoActaDeEntregaAnexoDS();
                adaptador = new Equipamiento_tecnologico.FormatoActaDeEntregaAnexoDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudAltaAnexoTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAltaAnexo, this.correlativo);
                dta = new DataTable();

                if (dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAltaAnexo.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\FormatoActaDeEntregaAnexoRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\FormatoActaDeEntregaAnexoRPT.rpt");
                dta = dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudAltaAnexo;
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


        private void FormatoActaDeEntregaAnexo_Load(object sender, EventArgs e)
        {

        }
    }
}
