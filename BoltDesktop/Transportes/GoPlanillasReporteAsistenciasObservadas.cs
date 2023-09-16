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
using Asistencia.Datos;
using Asistencia.Negocios;
using Asistencia;

namespace Asistencia
{
    public partial class GoPlanillasReporteAsistenciasObservadas : Form
    {
        private string desde;
        private string fileName;
        private bool exportVisualSettings;
        private string hasta;
        
        private RegistroTransferenciaTransportesController negocio;
        private List<ASJ_ReporteAsistenciaObservadosResult> listadoAsistenciaObservados;
        private List<ASJ_ReporteAsistenciaObservadosResult> listadoAsistenciaObservadosSelecionados;
        private ASJ_ReporteAsistenciaObservadosResult registroObservado;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public MesController MesesNeg { get; private set; }
        public ControlIngresoSalidaPersonalController AsistenciaModelo { get; private set; }

        public GoPlanillasReporteAsistenciasObservadas()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();

        }

        public GoPlanillasReporteAsistenciasObservadas(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            CargarMeses();
            ObtenerFechasIniciales();
        }

        protected override void OnLoad(EventArgs e)
        {

            this.dgvAsistenciaObservados.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvAsistenciaObservados.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvAsistenciaObservados.MasterTemplate.AutoExpandGroups = true;
            this.dgvAsistenciaObservados.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvAsistenciaObservados.GroupDescriptors.Clear();
            this.dgvAsistenciaObservados.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chnombres", "Registros : {0:N0}; ", GridAggregateFunction.Count));
            this.dgvAsistenciaObservados.MasterTemplate.SummaryRowsTop.Add(items1);



        }



        private void ReporteAsistenciaObservados_Load(object sender, EventArgs e)
        {
            //
        }

        private void CargarMeses()
        {

            MesesNeg = new MesController();
            cboMes.DisplayMember = "descripcion";
            cboMes.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboMes.DataSource = MesesNeg.ListarMeses().ToList();
            //cboMes.SelectedValue = DateTime.Now.ToString("MM");
            cboMes.SelectedValue = ("13");
        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);

            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
        }

        private void ObtenerFechasMes()
        {
            DateTime fecha1;
            DateTime fecha2;
            
            if (cboMes.SelectedValue.ToString() != "00")
            {
                #region
                this.txtFechaDesde.Enabled = false;
                this.txtFechaHasta.Enabled = false;
                if (cboMes.SelectedValue.ToString() == "12")
                {
                    #region Si es mes diciembre
                    fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString() + "/" + this.txtPeriodo.Value.ToString()));// 
                    fecha2 = Convert.ToDateTime("31/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Value.ToString());// 
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();
                    #endregion

                }
                else
                {
                    #region Si es mes 13 habilitar controles de fecha, caso contrario es un mes de enero a noviembre.
                    if (cboMes.SelectedValue.ToString() == "13")
                    {
                        this.txtFechaDesde.Enabled = true;
                        this.txtFechaHasta.Enabled = true;


                    }
                    else
                    {
                        fecha2 = Convert.ToDateTime("01/" + (Convert.ToInt32(cboMes.SelectedValue) + 1) + "/" + this.txtPeriodo.Value.ToString()).AddDays(-1);// 
                        fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Value.ToString());// 
                        this.txtFechaDesde.Text = fecha1.ToShortDateString();
                        this.txtFechaHasta.Text = fecha2.ToShortDateString();


                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (cboMes.SelectedValue.ToString() == "00")
                {
                    fecha2 = Convert.ToDateTime("31/12/" + this.txtPeriodo.Value.ToString());// 
                    fecha1 = Convert.ToDateTime("01/01/" + this.txtPeriodo.Value.ToString());//
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();

                }

            }
        }


        public void Inicio()
        {
            try
            {
                
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void Exportar(RadGridView radGridView)
        {
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                RadMessageBox.Show("Ingrese nombre al archivo.");
                return;
            }

            fileName = this.saveFileDialog.FileName.Trim();
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(@fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(@fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            ObtenerFechasMes();
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            negocio = new RegistroTransferenciaTransportesController();
            listadoAsistenciaObservados = new List<ASJ_ReporteAsistenciaObservadosResult>();
            listadoAsistenciaObservados = negocio.GetListAssistanceObserved(_conection, desde, hasta).ToList();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvAsistenciaObservados.DataSource = listadoAsistenciaObservados.ToDataTable<ASJ_ReporteAsistenciaObservadosResult>();
            dgvAsistenciaObservados.Refresh();

            menuPrincipal.Enabled = !false;
            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            ProgressBar.Visible = !true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar(dgvAsistenciaObservados);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();

        }

        private void Consultar()
        {
            try
            {
                desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd");
                hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");
                menuPrincipal.Enabled = false;
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void dgvAsistenciaObservados_SelectionChanged(object sender, EventArgs e)
        {
            registroObservado = new ASJ_ReporteAsistenciaObservadosResult();
            registroObservado.IDCONTROLINGRESO = string.Empty;
            registroObservado.ITEM = string.Empty;
            registroObservado.CORRELATIVO = 0;
            registroObservado.idpersonal = string.Empty;
            registroObservado.nombres = string.Empty;

            sbmActualizarCódigoControl.Enabled = false;
            sbmTransferirEstaAsistencia.Enabled = false;
            sbmTransferirTodasLasAsistenciasDelTrabajador.Enabled = false;

            if (dgvAsistenciaObservados.Rows.Count > 0)
            {
                if (dgvAsistenciaObservados.CurrentRow != null && dgvAsistenciaObservados.CurrentRow.Cells["chIDCONTROLINGRESO"].Value != null)
                {
                    if (dgvAsistenciaObservados.CurrentRow.Cells["chIDCONTROLINGRESO"].Value.ToString().Trim() != string.Empty)
                    {
                        if (dgvAsistenciaObservados.CurrentRow.Cells["chITEM"].Value != null && dgvAsistenciaObservados.CurrentRow.Cells["chITEM"].Value.ToString().Trim() != string.Empty)
                        {
                            if (dgvAsistenciaObservados.CurrentRow.Cells["chCorrelativo"].Value != null && dgvAsistenciaObservados.CurrentRow.Cells["chCorrelativo"].Value.ToString().Trim() != string.Empty)
                            {
                                try
                                {
                                    string codigoRegistro = dgvAsistenciaObservados.CurrentRow.Cells["chIDCONTROLINGRESO"].Value.ToString().Trim();
                                    string itemRegistro = dgvAsistenciaObservados.CurrentRow.Cells["chITEM"].Value.ToString().Trim();
                                    string correlativoRegistro = dgvAsistenciaObservados.CurrentRow.Cells["chCorrelativo"].Value.ToString().Trim();

                                    var resultadoConsulta = listadoAsistenciaObservados.Where(x => x.IDCONTROLINGRESO.Trim() == codigoRegistro && x.ITEM == itemRegistro).ToList();
                                    if (resultadoConsulta != null && resultadoConsulta.ToList().Count > 0)
                                    {
                                        registroObservado = resultadoConsulta.ElementAt(0);
                                        if (registroObservado.esimportado == 0)
                                        {
                                            //sbmActualizarCódigoControl.Enabled = !false;
                                            if (registroObservado.nombres.ToString().Trim() != string.Empty && registroObservado.nombres.ToString().Trim() != "DESCONOCIDO")
                                            {
                                                if (registroObservado.observacion.ToString().Trim() == string.Empty)
                                                {
                                                    sbmActualizarCódigoControl.Enabled = false;
                                                    sbmTransferirEstaAsistencia.Enabled = !false;
                                                    sbmTransferirTodasLasAsistenciasDelTrabajador.Enabled = !false;
                                                }
                                                else
                                                {
                                                    sbmActualizarCódigoControl.Enabled = false;
                                                }
                                            }
                                            else if (registroObservado.nombres.ToString().Trim() != string.Empty && registroObservado.nombres.ToString().Trim() == "DESCONOCIDO")
                                            {
                                                sbmActualizarCódigoControl.Enabled = !false;
                                            }
                                            else
                                            {

                                            }


                                        }

                                    }
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString() + "", "MENSAJE DEL SISTEMA");
                                    return;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void transferirEstaAsistenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registroObservado.IDCONTROLINGRESO != string.Empty && registroObservado.ITEM != string.Empty)
            {
                if (registroObservado.idpersonal != string.Empty && registroObservado.nombres != string.Empty)
                {
                    if (registroObservado.nombres.Trim().ToUpper() != "DESCONOCIDO")
                    {
                        AsistenciaModelo = new ControlIngresoSalidaPersonalController();
                        AsistenciaModelo.TransferirAsistenciasObservadaByPersona(_conection, registroObservado,_user);
                        Consultar();
                    }
                }
            }
        }

        private void transferirTodasLasAsistenciasDelTrabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registroObservado.IDCONTROLINGRESO != string.Empty && registroObservado.ITEM != string.Empty)
            {
                if (registroObservado.idpersonal != string.Empty && registroObservado.nombres != string.Empty)
                {
                    if (registroObservado.nombres.Trim().ToUpper() != "DESCONOCIDO")
                    {
                        AsistenciaModelo = new ControlIngresoSalidaPersonalController();
                        AsistenciaModelo.TransferirAsistenciasObservadaByPersona(_conection, registroObservado,_user);
                        Consultar();
                    }
                }
            }
        }

        private void actualizarCódigoControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (registroObservado.IDCONTROLINGRESO != string.Empty && registroObservado.ITEM != string.Empty)
            {
                if (registroObservado.idpersonal != string.Empty && registroObservado.nombres != string.Empty)
                {
                    if (registroObservado.nombres.Trim().ToUpper() == "DESCONOCIDO")
                    {
                        GoPlanillasReporteAsistenciasObservadasActualizarDNI oFrm = new GoPlanillasReporteAsistenciasObservadasActualizarDNI(_conection, registroObservado.IDCONTROLINGRESO, registroObservado.ITEM, registroObservado.nombres.ToString(), registroObservado.idpersonal.ToString(), _conection, _companyId, _user);
                        oFrm.ShowDialog();
                        Consultar();
                    }
                }
            }
        }

        private void btnTransferirSeleccion_Click(object sender, EventArgs e)
        {
            try
            {
                menuPrincipal.Enabled = false;
                gbConsulta.Enabled = false;
                gbDetalle.Enabled = false;
                ProgressBar.Visible = true;
                bgwHiloTransferencia.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString() + "", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void bgwHiloTransferencia_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                negocio = new RegistroTransferenciaTransportesController();
                listadoAsistenciaObservadosSelecionados = new List<ASJ_ReporteAsistenciaObservadosResult>();
                listadoAsistenciaObservadosSelecionados = listadoAsistenciaObservados.Where(x => x.selecionado == 1).ToList();

                if (listadoAsistenciaObservadosSelecionados != null && listadoAsistenciaObservadosSelecionados.ToList().Count > 0)
                {
                    foreach (var item in listadoAsistenciaObservadosSelecionados)
                    {
                        AsistenciaModelo = new ControlIngresoSalidaPersonalController();
                        AsistenciaModelo.TransferirAsistenciasObservadaByPersona(_conection, item, _user);
                    }

                }

               
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHiloTransferencia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dgvAsistenciaObservados.DataSource = listadoAsistenciaObservados.ToDataTable<ASJ_ReporteAsistenciaObservadosResult>();
            dgvAsistenciaObservados.Refresh();

            menuPrincipal.Enabled = !false;
            gbConsulta.Enabled = !false;
            gbDetalle.Enabled = !false;
            ProgressBar.Visible = !true;
        }
    }
}
