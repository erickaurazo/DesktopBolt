using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.T.I.Equipamiento_tecnologico;


namespace ComparativoHorasVisualSATNISIRA.T.I.ReportesEntregaDevolucion
{
    public partial class VistaPreviaSolicitudEquipamiento : Form
    {

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ActaDeEntregaEquipamientoDS dsReporte;
        private Equipamiento_tecnologico.ActaDeEntregaEquipamientoDSTableAdapters._SAS_SolicitudDeEquipamientoTecnologicoFullByCodigoTableAdapter adaptador;
        private int correlativo;


        public VistaPreviaSolicitudEquipamiento()
        {
            InitializeComponent();
        }


        public VistaPreviaSolicitudEquipamiento(int codigo, string tipoDeSolicitud)
        {
            #region 
            InitializeComponent();
            this.correlativo = codigo;

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new ActaDeEntregaEquipamientoDS();
                adaptador = new Equipamiento_tecnologico.ActaDeEntregaEquipamientoDSTableAdapters._SAS_SolicitudDeEquipamientoTecnologicoFullByCodigoTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte._SAS_SolicitudDeEquipamientoTecnologicoFullByCodigo, this.correlativo);
                dta = new DataTable();

                if (dsReporte._SAS_SolicitudDeEquipamientoTecnologicoFullByCodigo.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                if (tipoDeSolicitud.ToUpper() == "ALTA")
                {
                    oRpt.Load(@"C:\SOLUTION\SolicitudEntregaOtro.rpt");
                }
                else if (tipoDeSolicitud.ToUpper() == "BAJA")
                {
                    oRpt.Load(@"C:\SOLUTION\SolicitudDevolucionOtro.rpt");
                }
                else
                {
                    Close();
                }
               
               // oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\T.I\ReportesEntregaDevolucion\SolicitudEntregaOtro.rpt");
                dta = dsReporte._SAS_SolicitudDeEquipamientoTecnologicoFullByCodigo;
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


        private void VistaPreviaSolicitudEquipamiento_Load(object sender, EventArgs e)
        {

        }
    }
}
