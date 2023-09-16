using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Configuration;
using Telerik.WinControls.UI.Localization;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using ComparativoHorasVisualSATNISIRA.T.I;
using ComparativoHorasVisualSATNISIRA.Produccion;
using CrystalDecisions.CrystalReports.Engine;
using System.Reflection;
using Telerik.WinControls.Data;
using ComparativoHorasVisualSATNISIRA.Cosecha;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class TicketsReservados : Form
    {

        private int esAgrupado;
        private List<SAS_TicketReservados> listado;
        private SAS_TicketReservadoController model;
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private SAS_TicketReservados odetalleSelecionado;
        private string fileName;
        private bool exportVisualSettings;
        private int codigoDispotivo = 0;
        private string filtroEnReporte = string.Empty;
        private string _conection;
        private SAS_USUARIOS _user;
        private string _companyId;
        private PrivilegesByUser _privilege;
        private string result;
        private int operacion = 1;
        private int codigoSelecionado;
        public ReportDocument oRpt;
        private DataTable dta;
        private ImprimirTicketReservadosByIdDS dsReporte;
        private Cosecha.ImprimirTicketReservadosByIdDSTableAdapters.SAS_TicketReservadoByIdTableAdapter adaptador;

        private ImprimirTicketReservadosMasivosDS dsReporte2;
        private Cosecha.ImprimirTicketReservadosMasivosDSTableAdapters.SAS_TicketReservadoByPrinterTableAdapter adaptador2;


        private ImprimirTicketReservadosPorRangoDeTicketsDS dsReporte3;
        private Cosecha.ImprimirTicketReservadosPorRangoDeTicketsDSTableAdapters.SAS_TicketReservadoByNumbersTableAdapter adaptador3;

        public TicketsReservados()
        {
            InitializeComponent();
            _conection = "NSFAJAS";
            _user = new SAS_USUARIOS();
            _user.IdUsuario = "EAURAZO";
            _user.NombreCompleto = "ERICK AURAZO";
            _companyId = "001";
            _privilege = new PrivilegesByUser();
            _privilege.formularioCodigo = "205";
            _privilege.nuevo = 1;
            _privilege.editar = 1;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Consultar();
        }


        public TicketsReservados(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
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


        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvRegistro.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvRegistro.TableElement.EndUpdate();

                base.OnLoad(e);
            }
            catch (TargetInvocationException ex)
            {
                result = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

        }

        private void LoadFreightSummary()
        {

            try
            {
                this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
                this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvRegistro.GroupDescriptors.Clear();
                this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chTipoTicket", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (FilterExpressionException ex)
            {


                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }


        private void Consultar()
        {
            try
            {
                operacion = 1;
                btnGenerarTicketConvencionales.Enabled = false;
                btnGenerarTicketOrgánicos.Enabled = false;
                btnGenerarTicketsAutomáticos.Enabled = false;
                btnMenu.Enabled = false;
                progressBarMain.Visible = true;

                gbList.Enabled = false;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        public void Inicio()
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


        private void TicketReservado_Load(object sender, EventArgs e)
        {

        }

        private void chkEsActivo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            model = new SAS_TicketReservadoController();
            try
            {
                if (operacion == 1)
                {

                    listado = new List<SAS_TicketReservados>();
                    listado = model.GetFullViewList("NSFAJAS");
                }
                else if (operacion == 2)
                {
                    model.MassRegisterBlankTickets("NSFAJAS");
                    listado = model.GetFullViewList("NSFAJAS");
                }


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (operacion == 1)
                {
                    dgvRegistro.DataSource = listado.ToDataTable<SAS_TicketReservados>();
                    dgvRegistro.Refresh();
                }
                else if (operacion == 2)
                {
                    dgvRegistro.DataSource = listado.ToDataTable<SAS_TicketReservados>();
                    dgvRegistro.Refresh();
                }



                btnGenerarTicketConvencionales.Enabled = !false;
                btnGenerarTicketOrgánicos.Enabled = !false;
                btnGenerarTicketsAutomáticos.Enabled = !false;
                btnMenu.Enabled = !false;
                progressBarMain.Visible = !true;

                gbList.Enabled = !false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarDeEstado();
        }

        private void CambiarDeEstado()
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {

        }

        private void btnGenerarTicketsAutomáticos_Click(object sender, EventArgs e)
        {
            GenerarTicketsAutomaticos();
        }

        private void GenerarTicketsAutomaticos()
        {
            try
            {
                operacion = 2;
                btnGenerarTicketConvencionales.Enabled = false;
                btnGenerarTicketOrgánicos.Enabled = false;
                btnGenerarTicketsAutomáticos.Enabled = false;
                btnMenu.Enabled = false;
                progressBarMain.Visible = true;

                gbList.Enabled = false;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnGenerarTicketConvencionales_Click(object sender, EventArgs e)
        {
            GenerarTicketsConvencionales();
        }

        private void GenerarTicketsConvencionales()
        {

            CantidadDeTickets ofrm = new CantidadDeTickets("CONVENCIONAL");
            ofrm.ShowDialog();
            if (ofrm.DialogResult == DialogResult.OK)
            {
                Consultar();
            }

        }




        private void btnGenerarTicketOrgánicos_Click(object sender, EventArgs e)
        {
            GenerarTicketsOrganicos();
        }

        private void GenerarTicketsOrganicos()
        {
            CantidadDeTickets ofrm = new CantidadDeTickets("ORGÁNICO");
            ofrm.ShowDialog();
            if (ofrm.DialogResult == DialogResult.OK)
            {
                Consultar();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Registar();
        }

        private void Registar()
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {

        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            btnVistaPreviaTicket.Enabled = false;
            btnImprimirTicket.Enabled = false;
            codigoSelecionado = 0;
            odetalleSelecionado = new SAS_TicketReservados();
            odetalleSelecionado.id = 0;

            if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
            {
                if (dgvRegistro.CurrentRow != null)
                {
                    if (dgvRegistro.CurrentRow.Cells["chid"].Value != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                        {
                            codigoSelecionado = Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chid"].Value);
                            odetalleSelecionado = listado.Where(x => x.id == codigoSelecionado).ElementAt(0);
                            if (odetalleSelecionado.estaAsociado == 1)
                            {
                                btnVistaPreviaTicket.Enabled = false;
                                btnImprimirTicket.Enabled = false;
                            }
                            else
                            {
                                btnVistaPreviaTicket.Enabled = true;
                                btnImprimirTicket.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {

            if (odetalleSelecionado != null)
            {
                if (odetalleSelecionado.id != null)
                {
                    if (odetalleSelecionado.id > 0)
                    {
                        PrintCodeBar();
                    }
                }
            }

        }

        private void PrintCodeBar()
        {

            PrintDialog impresion = new PrintDialog();
            DialogResult Result = impresion.ShowDialog();
            //if (impresion.ShowDialog() == DialogResult.OK)
            if (Result == DialogResult.OK)
            {
                dsReporte = new ImprimirTicketReservadosByIdDS();
                adaptador = new Cosecha.ImprimirTicketReservadosByIdDSTableAdapters.SAS_TicketReservadoByIdTableAdapter();
                dsReporte.EnforceConstraints = false;
                //adaptador.Fill(dsReporte._SAS_DispositivoImpresionDeTicketsQR, this.correlativo);
                adaptador.Fill(dsReporte.SAS_TicketReservadoById, odetalleSelecionado.id);
                dta = new DataTable();
                //if (dsReporte._SAS_DispositivoImpresionDeTicketsQR.Rows.Count <= 0)
                if (dsReporte.SAS_TicketReservadoById.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosByIdRPT.rpt");
                //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Almacen\ProductoImpresionCodigoBarraRPT.rpt");

                //oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                //oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies,printDialog.PrinterSettings.Collate,printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
                dta = dsReporte.SAS_TicketReservadoById;
                oRpt.SetDataSource(dta);
                reportDocumento = oRpt;
                oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);

            }

        }



        private void btnVistaPreviaTicket_Click(object sender, EventArgs e)
        {
            PreviewCodeBar();
        }


        private void PreviewCodeBar()
        {
            if (odetalleSelecionado != null)
            {
                if (odetalleSelecionado.id != null)
                {
                    if (odetalleSelecionado.id > 0)
                    {
                        ImprimirTicketReservadosById ofrm = new ImprimirTicketReservadosById(odetalleSelecionado.id);
                        ofrm.ShowDialog();
                    }
                }
            }
        }

        private void btnVistaPreviaTicketsConvencionalPendientes_Click(object sender, EventArgs e)
        {
            ImprimirTicketReservadosMasivos ofrm = new ImprimirTicketReservadosMasivos(0, "9");
            ofrm.ShowDialog();
        }

        private void btnVistaPreviaTicketsOrganicoPendientes_Click(object sender, EventArgs e)
        {
            ImprimirTicketReservadosMasivos ofrm = new ImprimirTicketReservadosMasivos(0, "8");
            ofrm.ShowDialog();
        }

        private void btnImprimirTicketsConvencionalPendientes_Click(object sender, EventArgs e)
        {
            PrintCodeBarAll(0, "9");
        }

        private void PrintCodeBarAll(int codigo, string tipoTicket)
        {
            PrintDialog impresion = new PrintDialog();
            DialogResult Result = impresion.ShowDialog();
            //if (impresion.ShowDialog() == DialogResult.OK)
            if (Result == DialogResult.OK)
            {
                dsReporte2 = new ImprimirTicketReservadosMasivosDS();
                adaptador2 = new Cosecha.ImprimirTicketReservadosMasivosDSTableAdapters.SAS_TicketReservadoByPrinterTableAdapter();
                dsReporte2.EnforceConstraints = false;
                adaptador2.Fill(dsReporte2.SAS_TicketReservadoByPrinter, codigo, tipoTicket);
                dta = new DataTable();                
                if (dsReporte2.SAS_TicketReservadoByPrinter.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosMasivosRPT.rpt");                
                dta = dsReporte2.SAS_TicketReservadoByPrinter;
                oRpt.SetDataSource(dta);
                reportDocumento = oRpt;
                oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);

            }
        }

        private void btnImprimirTicketsOrganicoPendientes_Click(object sender, EventArgs e)
        {
            PrintCodeBarAll(0, "8");
        }

        private void btnVistaPreviaTicketsOrganicos_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            string tipoTicket = "8";
            int cantidad = Convert.ToInt32(this.txtCantidad.Value);
            model = new SAS_TicketReservadoController();
            List<int> listadoMinimoMaximoDeTicket = model.GetMaxAndMin("NSFAJAS", cantidad, tipoTicket);
            ImprimirTicketReservadosPorRangoDeTickets ofrm = new ImprimirTicketReservadosPorRangoDeTickets(listadoMinimoMaximoDeTicket.ElementAt(0), listadoMinimoMaximoDeTicket.ElementAt(1), tipoTicket);
            ofrm.ShowDialog();
        }

        private void btnVistaPreviaTicketsConvencional_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            string tipoTicket = "9";
            int cantidad = Convert.ToInt32(this.txtCantidad.Value);
            model = new SAS_TicketReservadoController();
            List<int> listadoMinimoMaximoDeTicket = model.GetMaxAndMin("NSFAJAS", cantidad, tipoTicket);
            ImprimirTicketReservadosPorRangoDeTickets ofrm = new ImprimirTicketReservadosPorRangoDeTickets(listadoMinimoMaximoDeTicket.ElementAt(0), listadoMinimoMaximoDeTicket.ElementAt(1), tipoTicket);
            ofrm.ShowDialog();
        }

        private void btnImprimirTicketsOrganicos_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            string tipoTicket = "8";
            int cantidad = Convert.ToInt32(this.txtCantidad.Value);
            model = new SAS_TicketReservadoController();
            List<int> listadoMinimoMaximoDeTicket = model.GetMaxAndMin("NSFAJAS", cantidad, tipoTicket);
            PrintCodeBarRange(listadoMinimoMaximoDeTicket.ElementAt(0), listadoMinimoMaximoDeTicket.ElementAt(1), tipoTicket);
        }

        private void PrintCodeBarRange(int minimo, int maximo, string tipoTicket)
        {
            PrintDialog impresion = new PrintDialog();
            DialogResult Result = impresion.ShowDialog();
            //if (impresion.ShowDialog() == DialogResult.OK)
            if (Result == DialogResult.OK)
            {
                dsReporte3 = new ImprimirTicketReservadosPorRangoDeTicketsDS();
                adaptador3 = new Cosecha.ImprimirTicketReservadosPorRangoDeTicketsDSTableAdapters.SAS_TicketReservadoByNumbersTableAdapter();
                dsReporte3.EnforceConstraints = false;
                adaptador3.Fill(dsReporte3.SAS_TicketReservadoByNumbers, minimo, maximo, tipoTicket);
                dta = new DataTable();
                if (dsReporte3.SAS_TicketReservadoByNumbers.Rows.Count <= 0)
                {
                    MessageBox.Show("No se encontró información para imprimir !");
                    return;
                }
                oRpt = new ReportDocument();
                CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                oRpt.Load(@"C:\SOLUTION\ImprimirTicketReservadosPorRangoDeTicketsRPT.rpt");
                dta = dsReporte3.SAS_TicketReservadoByNumbers;
                oRpt.SetDataSource(dta);
                reportDocumento = oRpt;
                oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);

            }
        }

        private void btnImprimirTicketsConvencional_Click(object sender, EventArgs e)
        {
            int desde = 0;
            int hasta = 0;
            string tipoTicket = "9";
            int cantidad = Convert.ToInt32(this.txtCantidad.Value);
            model = new SAS_TicketReservadoController();
            List<int> listadoMinimoMaximoDeTicket = model.GetMaxAndMin("NSFAJAS", cantidad, tipoTicket);
            PrintCodeBarRange(listadoMinimoMaximoDeTicket.ElementAt(0), listadoMinimoMaximoDeTicket.ElementAt(1), tipoTicket);
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }
    }
}
