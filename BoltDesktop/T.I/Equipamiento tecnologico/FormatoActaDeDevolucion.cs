using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.T.I.Equipamiento_tecnologico;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class FormatoActaDeDevolucion : Form
    {


        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;        
        private FormatoActaDeDevolucionDS dsReporte;
        private Equipamiento_tecnologico.FormatoActaDeDevolucionDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudBajaTableAdapter adaptador;                
        private int correlativo;

        public FormatoActaDeDevolucion()
        {
            InitializeComponent();
        }


        public FormatoActaDeDevolucion(int codigo)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {                
                dsReporte = new FormatoActaDeDevolucionDS();
                adaptador = new Equipamiento_tecnologico.FormatoActaDeDevolucionDSTableAdapters.SAS_ListadoDetalleParaPresentacionSolicitudBajaTableAdapter();
                dsReporte.EnforceConstraints = false;
                
                adaptador.Fill(dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudBaja, this.correlativo);
                dta = new DataTable();
                
                if (dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudBaja.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\FormatoActaDeDevolucionRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\FormatoActaDeDevolucionRPT.rpt");                
                dta = dsReporte.SAS_ListadoDetalleParaPresentacionSolicitudBaja;
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


        private void FormatoActaDeDevolucion_Load(object sender, EventArgs e)
        {
            
        }


      

    }
}
