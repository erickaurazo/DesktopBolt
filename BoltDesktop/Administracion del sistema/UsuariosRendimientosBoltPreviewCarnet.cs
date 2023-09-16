using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;

namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    public partial class UsuariosRendimientosBoltPreviewCarnet : Form
    {

        string conexion = string.Empty;
        int codigoUsuario = 0;        

        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private UsuarioBoltPreviewDS dsReporte;
        private UsuarioBoltPreviewDSTableAdapters.SAS_UsuarioBoltWebByCodigoTableAdapter adaptador;

        public UsuariosRendimientosBoltPreviewCarnet()
        {
            InitializeComponent();
        }

        public UsuariosRendimientosBoltPreviewCarnet(string _conexion, int _codigoUsuario)
        {
            #region 
            InitializeComponent();
            conexion = _conexion;
            codigoUsuario = _codigoUsuario;
            

            this.crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new UsuarioBoltPreviewDS();
                adaptador = new UsuarioBoltPreviewDSTableAdapters.SAS_UsuarioBoltWebByCodigoTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_UsuarioBoltWebByCodigo, codigoUsuario);
                dta = new DataTable();

                if (dsReporte.SAS_UsuarioBoltWebByCodigo.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\UsuariosBoltPreviewCarnetRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Calidad\RegistroDeIngresoSalidaGasificadoRPT.rpt");                
                dta = dsReporte.SAS_UsuarioBoltWebByCodigo;
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


        private void UsuariosBoltPreviewCarnet_Load(object sender, EventArgs e)
        {

        }
    }
}
