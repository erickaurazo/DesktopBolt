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
using Asistencia.Helper;

namespace Asistencia
{
    public partial class GoTransportesCatalogoParaderos : Form
    {

        private ParaderosController modelo;
        private List<SJ_Paraderos> listado;
        private SJ_Paraderos oSJ_Paraderos;
        private string fileName;
        private bool exportVisualSettings;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private ComboBoxHelper comboHelper;
        private List<Grupo> typesWereabouts;

        public GoTransportesCatalogoParaderos()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Consultar();
        }

        public GoTransportesCatalogoParaderos(string conection, ASJ_USUARIOS user, string companyId)
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


                comboHelper = new ComboBoxHelper();
                typesWereabouts = new List<Grupo>();
                typesWereabouts = comboHelper.GetComboTypeWhereabouts();
                cboTipoParadero.DisplayMember = "Descripcion";
                cboTipoParadero.ValueMember = "Codigo";
                cboTipoParadero.DataSource = typesWereabouts.ToList();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void CatalogoParadero_Load(object sender, EventArgs e)
        {

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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new ParaderosController();
            listado = new List<SJ_Paraderos>();
            listado = modelo.ListarTodos(_conection).ToList();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.ToDataTable<SJ_Paraderos>();
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

        private void CatalogoParadero_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
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
                if (oSJ_Paraderos.ESTADO == 1 && oSJ_Paraderos.IdParadero != string.Empty)
                {
                    modelo = new ParaderosController();
                    oSJ_Paraderos = new SJ_Paraderos();
                    oSJ_Paraderos.IdEmpresa = this.txtEmpresaCodigo.Text.Trim();
                    oSJ_Paraderos.IdParadero = this.txtCodigo.Text.Trim();
                    oSJ_Paraderos.DescripcionParadero = this.txtDescripcion.Text.Trim();
                    oSJ_Paraderos.Observacion = this.txtObservación.Text.Trim();
                    oSJ_Paraderos.ESTADO = Convert.ToDecimal(this.txtIdEstado.Text);

                    modelo.Anular(_conection, oSJ_Paraderos);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (oSJ_Paraderos.ESTADO == 1 && oSJ_Paraderos.IdParadero != string.Empty)
            {
                oSJ_Paraderos = new SJ_Paraderos();
                oSJ_Paraderos.IdEmpresa = this.txtEmpresaCodigo.Text.Trim();
                oSJ_Paraderos.IdParadero = this.txtCodigo.Text.Trim();
                modelo.Eliminar(_conection, oSJ_Paraderos);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                oSJ_Paraderos = new SJ_Paraderos();
                oSJ_Paraderos.IdEmpresa = string.Empty;
                oSJ_Paraderos.IdParadero = string.Empty;
                oSJ_Paraderos.DescripcionParadero = string.Empty;
                oSJ_Paraderos.Observacion = string.Empty;
                oSJ_Paraderos.ESTADO = 1;

                this.txtCodigo.Text = oSJ_Paraderos.IdParadero;
                this.txtDescripcion.Text = oSJ_Paraderos.DescripcionParadero;
                this.txtEmpresaDescripcion.Text = string.Empty;
                this.txtIdEstado.Text = "1";
                this.txtEstado.Text = "ACTIVO";
                
                this.txtObservación.Text = oSJ_Paraderos.Observacion;
                this.txtEmpresaCodigo.Text = oSJ_Paraderos.IdEmpresa;

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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtEstado.Text != "1")
                {
                    oSJ_Paraderos = new SJ_Paraderos();
                    oSJ_Paraderos.IdEmpresa = this.txtEmpresaCodigo.Text.Trim();
                    oSJ_Paraderos.IdParadero = this.txtCodigo.Text.Trim();
                    oSJ_Paraderos.DescripcionParadero = this.txtDescripcion.Text.ToString().Trim();
                    oSJ_Paraderos.Observacion = this.txtObservación.Text.ToString().Trim();
                    oSJ_Paraderos.ESTADO = this.txtIdEstado.Text == "1" ? 1 : 0;
                    oSJ_Paraderos.tipo = (cboTipoParadero.SelectedValue != null && cboTipoParadero.SelectedValue.ToString().Trim() != string.Empty) ? Convert.ToChar(cboTipoParadero.SelectedValue.ToString().Trim()) : 'P';
                    string resultadoConsulta = modelo.Grabar(_conection, oSJ_Paraderos);
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
            Cancelar();
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

        private void dgvPension_SelectionChanged(object sender, EventArgs e)
        {
            oSJ_Paraderos = new SJ_Paraderos();
            oSJ_Paraderos.IdEmpresa = string.Empty;
            oSJ_Paraderos.IdParadero = string.Empty;
            oSJ_Paraderos.DescripcionParadero = string.Empty;
            oSJ_Paraderos.Observacion = string.Empty;
            oSJ_Paraderos.ESTADO = 1;


            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chIdParadero"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chIdParadero"].Value.ToString() != string.Empty)
                        {
                            if (listado.Where(x => x.IdParadero.Trim() == dgvListado.CurrentRow.Cells["chIdParadero"].Value.ToString().Trim()).ToList().Count == 1)
                            {
                                oSJ_Paraderos = listado.Where(x => x.IdParadero.Trim() == dgvListado.CurrentRow.Cells["chIdParadero"].Value.ToString().Trim()).Single();
                            }
                        }
                    }
                }
            }


            this.txtCodigo.Text = oSJ_Paraderos.IdParadero;
            this.txtDescripcion.Text = oSJ_Paraderos.DescripcionParadero;
            this.txtEmpresaCodigo.Text = oSJ_Paraderos.IdEmpresa;
            this.txtEmpresaDescripcion.Text = oSJ_Paraderos.IdEmpresa == "001" ? "AGRICOLA SAN JOSE S.A" : string.Empty;
            this.txtIdEstado.Text = oSJ_Paraderos.ESTADO.ToString();
            this.txtEstado.Text = oSJ_Paraderos.ESTADO == 1 ? "ACTIVO" : "ANULADO";
            this.cboTipoParadero.SelectedValue = oSJ_Paraderos.tipo.ToString().Trim();
            this.txtObservación.Text = oSJ_Paraderos.Observacion.Trim();
        }
    }
}
