using Asistencia.Datos;
using Asistencia.Negocios;
using ComparativoHorasVisualSATNISIRA;
using ComparativoHorasVisualSATNISIRA.Produccion;
using ComparativoHorasVisualSATNISIRA.T.I;
using CrystalDecisions.CrystalReports.Engine;
using MyControlsDataBinding.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ReporteDeFormatosDeImpresion : Form
    {

        private int childFormNumber = 0;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;
        private UsersController modelPrivileges;
        private List<PrivilegesByUser> privilegesByUser;
        private string _descripcionConexion;
        private PrivilegesByUser _privilege;
        private ReportDocument oRpt;

        public ReporteDeFormatosDeImpresion()
        {
            InitializeComponent();
        }


        public ReporteDeFormatosDeImpresion(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Consultar();
        }

        private void Consultar()
        {

        }

        private void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["SAS"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnVistaPreviaFormatoEstadoCelulares_Click(object sender, EventArgs e)
        {

            RegistroTicketsAbastecimientoMateriaPrimaFormato03 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato03(0);
            ofrm.ShowDialog();

        }

        private void btnVistaPreviaFormatoDeDispositivo_Click(object sender, EventArgs e)
        {
            RegistroTicketsAbastecimientoMateriaPrimaFormato04 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato04(0);
            ofrm.ShowDialog();
        }

        private void btnImprimirFormatoEstadoCelulares_Click(object sender, EventArgs e)
        {
            Imprimir("EstadoCelulares");
        }

        private void Imprimir(string tipoFormato)
        {
            PrintDialog impresion = new PrintDialog();
            DialogResult Result = impresion.ShowDialog();
            //if (impresion.ShowDialog() == DialogResult.OK)
            if (Result == DialogResult.OK)
            {                
                oRpt = new ReportDocument();
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (tipoFormato.ToUpper() == "EstadoCelulares".ToUpper())
                {
                    oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato03.rpt");
                }
                else if (tipoFormato.ToUpper() == "EstadoDispositivo".ToUpper())
                {
                    oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato04.rpt");
                }

                reportDocumento = oRpt;
                oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
            }

        }

        private void btnImprimirFormatoDeDispositivo_Click(object sender, EventArgs e)
        {
            Imprimir("EstadoDispositivo");
        }
    }
}
