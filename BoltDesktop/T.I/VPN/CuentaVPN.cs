using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asistencia.Datos;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using Asistencia.Negocios;
using MyControlsDataBinding.Extensions;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using Asistencia.Helper;
using System.Configuration;
using Telerik.WinControls.UI.Localization;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class CuentaVPN : Form
    {

        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName;
        private bool exportVisualSettings;
        private SAS_CuentasVPN odetalle;
        private SAS_CuentasVPN odetalleSelecionado;
        private List<SAS_CuentasVPN> listado;
        private SAS_CuentasVPNController Modelo;

        public CuentaVPN()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Actualizar();

            Inicio();
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


        public CuentaVPN(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            Actualizar();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvRegistro.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvRegistro.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistro.GroupDescriptors.Clear();
            this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chcuenta", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void Grabar()
        {
            try
            {
                ObtenerObjeto();
                Modelo =new SAS_CuentasVPNController();
                int resultado = Modelo.Register("SAS", odetalle);
                btnGrabar.Enabled = !false;
                btnCancelar.Enabled = !false;
                if (resultado == 0)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se registró satisfactoriamente", "Confirmación del sistema");
                }
                else if (resultado == 1)
                {
                    MessageBox.Show("El registro " + this.txtCodigo.Text.Trim() + " se actualizó satisfactoriamente", "Confirmación del sistema");
                }
                Actualizar();
                btnGrabar.Enabled = false;
                gbEdit.Enabled = false;
                gbList.Enabled = true;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
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

        private void btnExportar_Click(object sender, EventArgs e)
        {

            if (dgvRegistro != null)
            {
                if (dgvRegistro.Rows.Count > 0)
                {
                    Exportar(dgvRegistro);
                }

                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void ObtenerObjeto()
        {

            try
            {
                odetalle = new SAS_CuentasVPN();
                odetalle.id = Convert.ToInt32(this.txtCodigo.Text);
                odetalle.idcodigoGeneral = this.txtIdCodigoGeneral.Text.Trim();
                odetalle.NombresCompletos = this.txtNombres.Text.Trim();
                odetalle.cuenta = this.txtCuenta.Text.Trim();
                odetalle.empresa = this.txtEmpresa.Text.Trim();
                odetalle.clave = this.txtclave.Text.Trim();
                odetalle.observacion = this.txtObservaciones.Text.Trim();
                odetalle.instruccion = this.txtInstrucciones.Text.Trim();
                odetalle.glosa = this.txtGlosa.Text.Trim();
                odetalle.estado = 1;
                odetalle.fechaCreacion = DateTime.Now;
                odetalle.creadoPor = Environment.UserName;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void CambiarEstado()
        {
            try
            {
                ObtenerObjeto();
                if (odetalle.cuenta != string.Empty)
                {
                    Modelo =new SAS_CuentasVPNController();
                    int resultado = 0;
                    resultado = Modelo.ChangeState("SAS", odetalle);
                    if (resultado == 2)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de anulación");
                        Actualizar();
                    }
                    else if (resultado == 3)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Activación");
                        Actualizar();
                    }
                    btnGrabar.Enabled = false;
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnEditar.Enabled = true;
                    btnCancelar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtCuenta.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            Editar();
        }

        private void Editar()
        {
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnGrabar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {

            Actualizar();

            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;
                progressBar1.Visible = false;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void CuentaVPN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void AsingarObjeto(SAS_CuentasVPN oDetalle)
        {
            try
            {
                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : string.Empty;
                this.txtIdCodigoGeneral.Text = oDetalle.idcodigoGeneral != null ? oDetalle.idcodigoGeneral.Trim() : string.Empty;
                this.txtNombres.Text = oDetalle.NombresCompletos != null ? oDetalle.NombresCompletos.Trim() : string.Empty;
                this.txtCuenta.Text = oDetalle.cuenta != null ? oDetalle.cuenta.Trim() : string.Empty;
                this.txtEmpresa.Text = oDetalle.empresa != null ? oDetalle.empresa.Trim() : string.Empty;
                this.txtclave.Text = oDetalle.clave != null ? oDetalle.clave.Trim() : string.Empty;
                this.txtObservaciones.Text = oDetalle.observacion != null ? oDetalle.observacion.Trim() : string.Empty;
                this.txtInstrucciones.Text = oDetalle.instruccion != null ? oDetalle.instruccion.Trim() : string.Empty;
                this.txtGlosa.Text = oDetalle.glosa != null ? oDetalle.glosa.Trim() : string.Empty;
                this.txtEstado.Text = Convert.ToInt32(oDetalle.estado.Value) == 1 ? "ACTIVO" : "ANULADO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }



        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                odetalleSelecionado = new SAS_CuentasVPN();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chId"].Value.ToString()) : 0);
                                var resultado = listado.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    odetalleSelecionado = resultado.Single();
                                    odetalleSelecionado.id = codigo;
                                    AsingarObjeto(odetalleSelecionado);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    odetalleSelecionado = resultado.ElementAt(0);
                                    odetalleSelecionado.id = codigo;
                                    AsingarObjeto(odetalleSelecionado);
                                }
                                else
                                {
                                    Limpiar();
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void CuentaVPN_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
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
            excelExporter.SheetName = "Listado registros";
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


        private void Nuevo()
        {
            try
            {
                odetalle = new SAS_CuentasVPN();
                odetalle.id = 0;
                odetalle.idcodigoGeneral = string.Empty;
                odetalle.NombresCompletos = string.Empty;
                odetalle.cuenta = string.Empty;
                odetalle.empresa = string.Empty;
                odetalle.clave = string.Empty;
                odetalle.observacion = string.Empty;
                odetalle.instruccion = string.Empty;
                odetalle.glosa = string.Empty;
                odetalle.estado = 1;
                odetalle.fechaCreacion = DateTime.Now;
                odetalle.creadoPor = Environment.UserName;


                Limpiar();
                Cancelar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void Cancelar()
        {
            btnGrabar.Enabled = !false;
            gbEdit.Enabled = !false;
            gbList.Enabled = !true;
            btnEditar.Enabled = !true;
            btnCancelar.Enabled = !true;
        }

        private void Limpiar()
        {
            try
            {
                odetalle = new SAS_CuentasVPN();
                odetalle.id = 0;
                odetalle.idcodigoGeneral = string.Empty;
                odetalle.NombresCompletos = string.Empty;
                odetalle.cuenta = string.Empty;
                odetalle.empresa = string.Empty;
                odetalle.clave = string.Empty;
                odetalle.observacion = string.Empty;
                odetalle.instruccion = string.Empty;
                odetalle.glosa = string.Empty;
                odetalle.estado = 1;
                odetalle.fechaCreacion = DateTime.Now;
                odetalle.creadoPor = Environment.UserName;

                odetalleSelecionado = new SAS_CuentasVPN();
                odetalleSelecionado.id = 0;
                odetalleSelecionado.idcodigoGeneral = string.Empty;
                odetalleSelecionado.NombresCompletos = string.Empty;
                odetalleSelecionado.cuenta = string.Empty;
                odetalleSelecionado.empresa = string.Empty;
                odetalleSelecionado.clave = string.Empty;
                odetalleSelecionado.observacion = string.Empty;
                odetalleSelecionado.instruccion = string.Empty;
                odetalleSelecionado.glosa = string.Empty;
                odetalleSelecionado.estado = 1;
                odetalleSelecionado.fechaCreacion = DateTime.Now;
                odetalleSelecionado.creadoPor = Environment.UserName;

                this.txtCodigo.Text = "0";
                this.txtEstado.Text = "ACTIVO";
                this.txtIdEstado.Text = "1";
                this.txtCuenta.Text = string.Empty;
                this.txtIdCodigoGeneral.Text = string.Empty;
                this.txtNombres.Text = string.Empty;
                this.txtclave.Text = string.Empty;
                this.txtObservaciones.Text = string.Empty;
                this.txtInstrucciones.Text = string.Empty;
                this.txtGlosa.Text = string.Empty;
                this.txtEmpresa.Text = string.Empty;                
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_CuentasVPN>();
                Modelo =new SAS_CuentasVPNController();
                listado = Modelo.GetVPNaccounts("SAS");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = listado.OrderBy(x => x.cuenta).ToList().ToDataTable<SAS_CuentasVPN>();
                dgvRegistro.Refresh();
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }
    }
}
