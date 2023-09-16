using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls;
using System.IO;
using System.Configuration;
using Asistencia.Datos;
using Asistencia.Negocios;


namespace Asistencia
{
    public partial class GoPlanillasCatalogoPersonalBloqueado : Form
    {
        
        private string fileName;
        private bool exportVisualSettings;

        private List<ASJ_ObtenerListadoDePersonalbloqueadoResult> listado = new List<ASJ_ObtenerListadoDePersonalbloqueadoResult>();
        private TipoBloqueoParaAsistenciaController modeloTipoBloqueo;
        private PersonalBloqueoController modeloPersonalBloqueado;
        private ASJ_ObtenerListadoDePersonalbloqueadoResult oBloqueo;
        private ASJ_PersonalBloqueo oPersonalBloqueada;
        private ASJ_PersonalBloqueo oPersonaBloqueada;
        private List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult> listaTipoBloqueos;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public GoPlanillasCatalogoPersonalBloqueado()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            CargarComboMotivoBloqueo();
            Consultar();
        }

        public GoPlanillasCatalogoPersonalBloqueado(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            CargarComboMotivoBloqueo();
            Consultar();
        }

        private void CargarComboMotivoBloqueo()
        {


            listaTipoBloqueos = new List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult>();
            modeloTipoBloqueo = new TipoBloqueoParaAsistenciaController();
            listaTipoBloqueos = modeloTipoBloqueo.GetTypeLock(_conection).ToList();

            cboMotivoBloqueo.DisplayMember = "descripcion";
            cboMotivoBloqueo.ValueMember = "codigoPersonalTipoBloqueo";
            cboMotivoBloqueo.DataSource = listaTipoBloqueos.ToList();

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
                Globales.NombreUsuarioSistema = "Erick Aurazo";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
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

            fileName = this.saveFileDialog.FileName;
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(fileName);
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
            excelExporter.SheetName = "Documento";
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


        private void CatalogoPerosnalBloqueadoParaAsistencia_Load(object sender, EventArgs e)
        {

        }

        private void Consultar()
        {
            try
            {
                
                mnPrincipal.Enabled = false;
                gbListado.Enabled = false;
                gbMantenimiento.Enabled = false;
                ProgressBar.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                mnPrincipal.Enabled = !false;
                gbListado.Enabled = !false;
                gbMantenimiento.Enabled = !false;
                ProgressBar.Visible = !true;
                return;
            }
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modeloPersonalBloqueado = new PersonalBloqueoController();
            listado = new List<ASJ_ObtenerListadoDePersonalbloqueadoResult>();
            listado = modeloPersonalBloqueado.GetListPersonLock(_conection).ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.ToDataTable<ASJ_ObtenerListadoDePersonalbloqueadoResult>();
                dgvListado.Refresh();
                mnPrincipal.Enabled = !false;
                gbListado.Enabled = !false;
                gbMantenimiento.Enabled = !false;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                mnPrincipal.Enabled = !false;
                gbListado.Enabled = !false;
                gbMantenimiento.Enabled = !false;
                ProgressBar.Visible = !true;
                return;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                txtCodigo.Text = string.Empty;
                txtIdEstado.Text = string.Empty;
                txtEstado.Text = string.Empty;
                txtPersonalCodigo.Text = string.Empty;
                txtPersonalNombresCompletos.Text = string.Empty;
                txtFechaDesde.Text = string.Empty;
                txtFechaHasta.Text = string.Empty;
                txtObservaciones.Text = string.Empty;
                cboMotivoBloqueo.SelectedValue = "001";
                chkEsPeriodo.Checked = false;
                txtPersonalCodigo.Focus();

                oPersonaBloqueada = new ASJ_PersonalBloqueo();
                oPersonaBloqueada.codigoPersonalBloqueo = 0;
                oPersonaBloqueada.codigoPersonalGeneral = string.Empty;
                oPersonaBloqueada.codigoBloqueo = string.Empty;
                oPersonaBloqueada.esPeriodo = '0';
                oPersonaBloqueada.fechaRegistro = DateTime.Now;
                oPersonaBloqueada.inicioBloqueo = DateTime.Now;
                oPersonaBloqueada.terminoBloqueo = (DateTime?)null;
                oPersonaBloqueada.observaciones = string.Empty;
                oPersonaBloqueada.estado = 1;


                oBloqueo = new ASJ_ObtenerListadoDePersonalbloqueadoResult();
                oBloqueo.codigoPersonalBloqueo = 0;
                oBloqueo.codigoPersonalGeneral = string.Empty;
                oBloqueo.nombres = string.Empty;
                oBloqueo.codigoBloqueo = string.Empty;
                oBloqueo.esPeriodo = '0';
                oBloqueo.fechaRegistro = DateTime.Now;
                oBloqueo.inicioBloqueo = DateTime.Now;
                oBloqueo.terminoBloqueo = (DateTime?)null;
                oBloqueo.observaciones = string.Empty;
                oBloqueo.estado = 1;
                oBloqueo.descripcionEstado = string.Empty;

                gbListado.Enabled = false;
                gbMantenimiento.Enabled = true;
                btnEliminar.Enabled = false;
                btnAnular.Enabled = false;
                btnAtras.Enabled = true;
                btnExportar.Enabled = false;
                btnNuevo.Enabled = false;
                btnSalir.Enabled = true;
                btnActualizarLista.Enabled = false;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                mnPrincipal.Enabled = !false;
                gbListado.Enabled = !false;
                gbMantenimiento.Enabled = !false;
                ProgressBar.Visible = !true;
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            gbListado.Enabled = !false;
            gbMantenimiento.Enabled = !true;
            btnEliminar.Enabled = !false;
            btnAnular.Enabled = !false;
            btnAtras.Enabled = !true;
            btnExportar.Enabled = !false;
            btnNuevo.Enabled = !false;
            btnSalir.Enabled = !true;
            btnActualizarLista.Enabled = !false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            gbListado.Enabled = false;
            gbMantenimiento.Enabled = true;
            btnEliminar.Enabled = false;
            btnAnular.Enabled = false;
            btnAtras.Enabled = true;
            btnExportar.Enabled = false;
            btnNuevo.Enabled = false;
            btnSalir.Enabled = true;
            btnActualizarLista.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                if (oPersonaBloqueada.codigoPersonalBloqueo > 0)
                {
                    modeloPersonalBloqueado = new PersonalBloqueoController();
                    modeloPersonalBloqueado.CnageStatus(_conection, oPersonaBloqueada);
                    gbListado.Enabled = !false;
                    gbMantenimiento.Enabled = !true;
                    btnEliminar.Enabled = !false;
                    btnAnular.Enabled = !false;
                    btnAtras.Enabled = !true;
                    btnExportar.Enabled = !false;
                    btnNuevo.Enabled = !false;
                    btnSalir.Enabled = true;
                    btnActualizarLista.Enabled = !false;
                    Consultar();
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (oPersonaBloqueada.codigoPersonalBloqueo > 0)
            {
                modeloPersonalBloqueado = new PersonalBloqueoController();
                modeloPersonalBloqueado.Delete(_conection, oPersonaBloqueada);
                gbListado.Enabled = !false;
                gbMantenimiento.Enabled = !true;
                btnEliminar.Enabled = !false;
                btnAnular.Enabled = !false;
                btnAtras.Enabled = !true;
                btnExportar.Enabled = !false;
                btnNuevo.Enabled = !false;
                btnSalir.Enabled = true;
                btnActualizarLista.Enabled = !false;
                Consultar();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar(dgvListado);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                oPersonaBloqueada = new ASJ_PersonalBloqueo();
                oPersonaBloqueada.codigoPersonalBloqueo = 0;
                oPersonaBloqueada.codigoPersonalGeneral = string.Empty;
                oPersonaBloqueada.codigoBloqueo = string.Empty;
                oPersonaBloqueada.esPeriodo = '0';
                oPersonaBloqueada.fechaRegistro = DateTime.Now;
                oPersonaBloqueada.inicioBloqueo = DateTime.Now;
                oPersonaBloqueada.terminoBloqueo = (DateTime?)null;
                oPersonaBloqueada.observaciones = string.Empty;
                oPersonaBloqueada.estado = 1;


                oBloqueo = new ASJ_ObtenerListadoDePersonalbloqueadoResult();
                oBloqueo.codigoPersonalBloqueo = 0;
                oBloqueo.codigoPersonalGeneral = string.Empty;
                oBloqueo.nombres = string.Empty;
                oBloqueo.codigoBloqueo = string.Empty;
                oBloqueo.esPeriodo = '0';
                oBloqueo.fechaRegistro = DateTime.Now;
                oBloqueo.inicioBloqueo = DateTime.Now;
                oBloqueo.terminoBloqueo = (DateTime?)null;
                oBloqueo.observaciones = string.Empty;
                oBloqueo.estado = 1;
                oBloqueo.descripcionEstado = string.Empty;


                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigoPersonalBloqueo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chcodigoPersonalBloqueo"].Value.ToString() != string.Empty)
                            {
                                var resultadoSingle = listado.Where(x => x.codigoPersonalBloqueo == Convert.ToInt32(dgvListado.CurrentRow.Cells["chcodigoPersonalBloqueo"].Value)).ToList();
                                if (resultadoSingle.ToList().Count == 1)
                                {
                                    oBloqueo = new ASJ_ObtenerListadoDePersonalbloqueadoResult();
                                    oBloqueo = resultadoSingle.Single();
                                    oPersonaBloqueada = new ASJ_PersonalBloqueo();
                                    oPersonaBloqueada.codigoPersonalBloqueo = oBloqueo.codigoPersonalBloqueo;
                                    oPersonaBloqueada.codigoPersonalGeneral = oBloqueo.codigoPersonalGeneral;
                                    oPersonaBloqueada.codigoBloqueo = oBloqueo.codigoBloqueo;
                                    oPersonaBloqueada.esPeriodo = oBloqueo.esPeriodo;
                                    oPersonaBloqueada.fechaRegistro = oBloqueo.fechaRegistro;
                                    oPersonaBloqueada.inicioBloqueo = oBloqueo.inicioBloqueo;
                                    oPersonaBloqueada.terminoBloqueo = oBloqueo.terminoBloqueo;
                                    oPersonaBloqueada.observaciones = oBloqueo.observaciones;
                                    oPersonaBloqueada.estado = oBloqueo.estado;
                                }
                            }
                        }
                    }
                }

                txtCodigo.Text = oBloqueo.codigoPersonalBloqueo.ToString();
                txtIdEstado.Text = oBloqueo.estado.ToString();
                txtEstado.Text = oBloqueo.descripcionEstado.ToString();
                txtPersonalCodigo.Text = oBloqueo.codigoPersonalGeneral.ToString();
                txtPersonalNombresCompletos.Text = oBloqueo.nombres.ToString();
                txtFechaDesde.Text = oBloqueo.inicioBloqueo.ToShortDateString();
                txtFechaHasta.Text = oBloqueo.terminoBloqueo != (DateTime?)null ? oBloqueo.terminoBloqueo.Value.ToShortDateString() : string.Empty;
                txtObservaciones.Text = oBloqueo.observaciones.ToString();
                cboMotivoBloqueo.SelectedValue = oBloqueo.codigoBloqueo.Trim();
                chkEsPeriodo.Checked = false;
                if (oBloqueo.esPeriodo == 1)
                {
                    chkEsPeriodo.Checked = true;
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }




        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtEstado.Text != "1")
                {
                    if (this.txtPersonalCodigo.Text != string.Empty)
                    {
                        if (this.txtPersonalNombresCompletos.Text != string.Empty)
                        {
                            oPersonaBloqueada = new ASJ_PersonalBloqueo();
                            oPersonaBloqueada.codigoPersonalBloqueo = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                            oPersonaBloqueada.codigoPersonalGeneral = this.txtPersonalCodigo.Text;
                            oPersonaBloqueada.codigoBloqueo = cboMotivoBloqueo.SelectedValue.ToString();
                            oPersonaBloqueada.esPeriodo = '0';
                            oPersonaBloqueada.fechaRegistro = DateTime.Now;
                            if (this.txtFechaDesde.Text != string.Empty)
                            {
                                oPersonaBloqueada.inicioBloqueo = Convert.ToDateTime(this.txtFechaDesde.Text);
                            }
                            if (this.txtFechaHasta.Text != string.Empty && this.txtFechaHasta.Text != string.Empty)
                            {
                                oPersonaBloqueada.terminoBloqueo = Convert.ToDateTime(this.txtFechaHasta.Text);
                            }
                            if ((this.txtFechaDesde.Text != string.Empty && (this.txtFechaHasta.Text != string.Empty))) ;
                            {
                                oPersonaBloqueada.esPeriodo = '1';
                            }

                            oPersonaBloqueada.observaciones = this.txtObservaciones.Text.Trim();
                            oPersonaBloqueada.estado = 1;

                            modeloPersonalBloqueado = new PersonalBloqueoController();
                            string resultadoConsulta = modeloPersonalBloqueado.Add(_conection, oPersonaBloqueada);
                            MessageBox.Show(resultadoConsulta, "MENSAJE DEL SISTEMA");
                            gbListado.Enabled = !false;
                            gbMantenimiento.Enabled = !true;
                            btnEliminar.Enabled = !false;
                            btnAnular.Enabled = !false;
                            btnAtras.Enabled = !true;
                            btnExportar.Enabled = !false;
                            btnNuevo.Enabled = !false;
                            btnSalir.Enabled = !true;
                            btnActualizarLista.Enabled = !false;
                            Consultar();
                        }
                    }

                }
            }
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }
    }
}
