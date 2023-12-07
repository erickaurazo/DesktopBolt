using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.WinControls.UI;
using ComparativoHorasVisualSATNISIRA.Produccion;
using Asistencia.Datos;

namespace ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga
{
    public partial class ConformacionDeCargaVistaPrevia : Form
    {
        #region Variables() 
        private string connection;
        private SAS_USUARIOS user;
        private string companyId;
        private PrivilegesByUser privilege;
        private int id;
        public ReportDocument oRpt;
        public RadPdfViewer radPdfViewer12;
        private DataTable dta;
        private ConformidadDeCargaDS dsReporte;
        private ConformidadDeCargaDSTableAdapters.SAS_ListadoConformacionDeCargaPBIByIdTableAdapter adaptador;
        #endregion

        public ConformacionDeCargaVistaPrevia()
        {
            InitializeComponent();
        }

        private void ConformacionDeCargaVistaPrevia_Load(object sender, EventArgs e)
        {

        }

        public ConformacionDeCargaVistaPrevia(string _connection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege, int _id)
        {
            connection = _connection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            id = _id;
            crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new ConformidadDeCargaDS();
                adaptador = new ConformidadDeCargaDSTableAdapters.SAS_ListadoConformacionDeCargaPBIByIdTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_ListadoConformacionDeCargaPBIById, id);
                dta = new DataTable();

                if (dsReporte.SAS_ListadoConformacionDeCargaPBIById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ConformacionDeCargaRPT.rpt");
                dta = dsReporte.SAS_ListadoConformacionDeCargaPBIById;
                oRpt.SetDataSource(dta);
                crystalReportViewer1.ReportSource = oRpt;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n Presentar reporte control de unidades de horarios de salida", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        public ConformacionDeCargaVistaPrevia(string _conexion, int _Id)
        {
            #region 
            InitializeComponent();
            connection = _conexion;
            id = _Id;

            crystalReportViewer1.PrintReport();
            try
            {
                dsReporte = new ConformidadDeCargaDS();
                adaptador = new ConformidadDeCargaDSTableAdapters.SAS_ListadoConformacionDeCargaPBIByIdTableAdapter();
                dsReporte.EnforceConstraints = false;

                adaptador.Fill(dsReporte.SAS_ListadoConformacionDeCargaPBIById, id);
                dta = new DataTable();

                if (dsReporte.SAS_ListadoConformacionDeCargaPBIById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ConformacionDeCargaRPT.rpt");                              
                dta = dsReporte.SAS_ListadoConformacionDeCargaPBIById;
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
