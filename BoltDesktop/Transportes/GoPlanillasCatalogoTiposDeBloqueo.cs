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
using Asistencia.Negocios;
using Asistencia.Datos;


namespace Asistencia
{

    public partial class GoPlanillasCatalogoTiposDeBloqueo : Form
    {        
        
        private string fileName;
        private bool exportVisualSettings;        
        private TipoBloqueoParaAsistenciaController modelo;
        private List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult> listado;        
        private ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult oTipoBloqueoSeleccionado;
        private ASJ_PersonalTipoBloqueo personalBloqueo;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public GoPlanillasCatalogoTiposDeBloqueo()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Consultar();
        }

        public GoPlanillasCatalogoTiposDeBloqueo(string conection, ASJ_USUARIOS user, string companyId)
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
            Consultar();
        }

        public void Inicio()
        {
            try
            {
                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "Erick Aurazo";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void CatalogoTipoBloqueoAsistencia_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new TipoBloqueoParaAsistenciaController();
            listado = new List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult>();
            listado = modelo.GetTypeLock(_conection).ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                dgvListado.DataSource = listado.ToDataTable<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult>();
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

        private void CatalogoTipoBloqueoAsistencia_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
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

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            oTipoBloqueoSeleccionado = new ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult();
            oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo = string.Empty;
            oTipoBloqueoSeleccionado.descripcionEstado = string.Empty;
            oTipoBloqueoSeleccionado.color = string.Empty;
            oTipoBloqueoSeleccionado.descripcion = string.Empty;
            oTipoBloqueoSeleccionado.estado = 0;


            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chcodigoPersonalTipoBloqueo"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigoPersonalTipoBloqueo"].Value.ToString() != string.Empty)
                        {
                            if (listado.Where(x => x.codigoPersonalTipoBloqueo.Trim() == dgvListado.CurrentRow.Cells["chcodigoPersonalTipoBloqueo"].Value.ToString().Trim()).ToList().Count > 0)
                            {
                                oTipoBloqueoSeleccionado = listado.Where(x => x.codigoPersonalTipoBloqueo.Trim() == dgvListado.CurrentRow.Cells["chcodigoPersonalTipoBloqueo"].Value.ToString().Trim()).Single();
                            }
                        }
                    }
                }
            }


            this.txtCodigo.Text = oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo.Trim();
            this.txtDescripcion.Text = oTipoBloqueoSeleccionado.descripcion.Trim();
            this.txtIdEstado.Text = oTipoBloqueoSeleccionado.estado.Value.ToString();
            this.txtEstado.Text = oTipoBloqueoSeleccionado.descripcionEstado.Trim();
            this.cboColor.Text = oTipoBloqueoSeleccionado.color.Trim();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                oTipoBloqueoSeleccionado = new ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult();
                oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo = string.Empty;
                oTipoBloqueoSeleccionado.color = string.Empty;
                oTipoBloqueoSeleccionado.descripcion = string.Empty;
                oTipoBloqueoSeleccionado.descripcionEstado = "ACTIVO";
                oTipoBloqueoSeleccionado.estado = 1;

                this.txtCodigo.Text = oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo.Trim();
                this.txtDescripcion.Text = oTipoBloqueoSeleccionado.descripcion.Trim();
                this.txtIdEstado.Text = oTipoBloqueoSeleccionado.estado.Value.ToString();
                this.txtEstado.Text = oTipoBloqueoSeleccionado.descripcionEstado.Trim();
                this.cboColor.Text = oTipoBloqueoSeleccionado.color.Trim();

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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtEstado.Text != "1")
                {
                    personalBloqueo = new ASJ_PersonalTipoBloqueo();
                    personalBloqueo.codigoPersonalTipoBloqueo = this.txtCodigo.Text;
                    personalBloqueo.color = this.cboColor.Text;
                    personalBloqueo.descripcion = this.txtDescripcion.Text.Trim();
                    personalBloqueo.estado = Convert.ToByte(this.txtIdEstado.Text.Trim());

                    string resultadoConsulta = modelo.Add(_conection, personalBloqueo);
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
            catch (IOException ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }



        }

        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void btnAnular_Click(object sender, EventArgs e)
        {

            try
            {
                if (oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo != string.Empty)
                {
                    personalBloqueo = new ASJ_PersonalTipoBloqueo();
                    personalBloqueo.codigoPersonalTipoBloqueo = this.txtCodigo.Text;
                    personalBloqueo.color = this.cboColor.Text;
                    personalBloqueo.descripcion = this.txtDescripcion.Text.Trim();
                    personalBloqueo.estado = Convert.ToByte(this.txtIdEstado.Text.Trim());
                    modelo.ChangeState(_conection, personalBloqueo);
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
            catch (Exception EX)
            {

                MessageBox.Show(EX.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (oTipoBloqueoSeleccionado.codigoPersonalTipoBloqueo != string.Empty)
            {
                personalBloqueo = new ASJ_PersonalTipoBloqueo();
                personalBloqueo.codigoPersonalTipoBloqueo = this.txtCodigo.Text;
                personalBloqueo.color = this.cboColor.Text;
                personalBloqueo.descripcion = this.txtDescripcion.Text.Trim();
                personalBloqueo.estado = Convert.ToByte(this.txtIdEstado.Text.Trim());
                modelo.Delete(_conection, personalBloqueo);
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar(dgvListado);
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

    }
}
