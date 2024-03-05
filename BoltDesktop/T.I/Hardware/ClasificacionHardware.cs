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
using Telerik.WinControls.UI.Localization;
using System.Configuration;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ClasificacionHardware : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private SAS_DispositivoTipoHardwareController Modelo;
        private List<SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult> listado;
        private SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult otipo;
        private SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult oDetalle;
        private string fileName = string.Empty;
        private bool exportVisualSettings;
        private int ClickFiltro = 0;
        private int ClickResaltarResultados;
        private SAS_DispositivoTipoHardware CaracteristicaTipoHardware;

        public ClasificacionHardware()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Actualizar();
        }

        public ClasificacionHardware(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;

            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();

            Actualizar();
        }

        private void TipoCaracteristicasHardware_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
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
            items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cancelar();
            btnCancelar.Enabled = true;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void CambiarEstado()
        {
            try
            {
                ObtenerObjeto();
                if (oDetalle.descripcion != string.Empty)
                {
                    Modelo =new SAS_DispositivoTipoHardwareController();
                    int resultado = 0;
                    resultado = Modelo.ChangeState(conection, oDetalle.id);
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
                    this.txtDescripcion.Focus();

                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no habilitada", "Advertencia del sistema");
            return;
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

        private void TipoCaracteristicasHardware_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = listado.OrderBy(x => x.descripcion).ToList().ToDataTable<SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult>();
                dgvRegistro.Refresh();

                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;
                progressBar1.Visible = false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo =new SAS_DispositivoTipoHardwareController();
                listado = new List<SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult>();
                listado = Modelo.GetTypeHardwaresAll("SAS");
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void Nuevo()
        {
            try
            {
                otipo = new SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult();
                otipo.id = 0;
                otipo.descripcion = string.Empty;
                otipo.nombreCorto = string.Empty;
                otipo.estado = 0;
                otipo.observaciones = string.Empty;
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
                oDetalle = new SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult();
                oDetalle.id = 0;
                oDetalle.descripcion = string.Empty;
                oDetalle.nombreCorto = string.Empty;
                oDetalle.estado = 0;
                oDetalle.observaciones = string.Empty;
                oDetalle.enFormatoSolicitud = 0;

                this.txtAbreviatura.Text = string.Empty;
                this.txtCodigo.Text = string.Empty;
                this.txtDescripcion.Text = string.Empty;
                this.txtObservacion.Text = string.Empty;
                this.txtEstado.Text = "ACTIVO";
                this.txtIdEstado.Text = "1";
                this.chkPresenteEnSolicitud.Checked = false;
                this.txtTipoHardwareDescripcion.Clear();
                this.txtTipoHardwareID.Clear();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObtenerObjeto() == true)
                {
                    #region Registrar()
                    if (CaracteristicaTipoHardware.descripcion != string.Empty)
                    {
                        Modelo = new SAS_DispositivoTipoHardwareController();
                        int resultado = 0;
                        resultado = Modelo.Register(conection, CaracteristicaTipoHardware);
                        if (resultado == 1)
                        {
                            MessageBox.Show("Se actualizó correctamente".ToUpper(), "Mensaje del sistema".ToUpper());
                           
                        }
                        else if (resultado == 0)
                        {
                            MessageBox.Show("Se registro correctamente".ToUpper(), "Mensaje del sistema".ToUpper());
                           
                        }

                       
                        btnGrabar.Enabled = false;
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnEditar.Enabled = true;
                        btnCancelar.Enabled = true;
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                        this.txtDescripcion.Focus();
                        return;
                    }

                    #endregion
                }
               
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private bool ObtenerObjeto()
        {
            try
            {
                CaracteristicaTipoHardware = new SAS_DispositivoTipoHardware();
                CaracteristicaTipoHardware.id = (this.txtCodigo.Text != string.Empty || this.txtCodigo.Text.Trim() != string.Empty) ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0;
                CaracteristicaTipoHardware.descripcion = (this.txtDescripcion.Text != string.Empty || this.txtDescripcion.Text.Trim() != string.Empty) ? (this.txtDescripcion.Text.Trim()) : string.Empty;
                CaracteristicaTipoHardware.nombreCorto = (this.txtAbreviatura.Text != string.Empty || this.txtAbreviatura.Text.Trim() != string.Empty) ? (this.txtAbreviatura.Text.Trim()) : string.Empty;
                CaracteristicaTipoHardware.estado = (this.txtIdEstado.Text != string.Empty || this.txtIdEstado.Text.Trim() != string.Empty) ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : 0;
                CaracteristicaTipoHardware.observaciones = (this.txtObservacion.Text != string.Empty || this.txtObservacion.Text.Trim() != string.Empty) ? (this.txtObservacion.Text.Trim()) : string.Empty;
                CaracteristicaTipoHardware.enFormatoSolicitud = chkPresenteEnSolicitud.Checked == true ? 1 : 0;
                CaracteristicaTipoHardware.TipoHardwareID = (this.txtTipoHardwareID.Text != string.Empty || this.txtTipoHardwareID.Text.Trim() != string.Empty) ? (this.txtTipoHardwareID.Text.Trim()) : string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return false;
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

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                oDetalle = new SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvRegistro.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chid"].Value) : 0);
                                var resultado = listado.Where(x => x.id == codigo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    oDetalle = resultado.Single();
                                    oDetalle.id = codigo;
                                    AsingarObjeto(oDetalle);
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    oDetalle = resultado.ElementAt(0);
                                    oDetalle.id = codigo;
                                    AsingarObjeto(oDetalle);
                                }
                                else
                                {
                                    Limpiar();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }



        private void AsingarObjeto(SAS_ListadoDeDispositivoTipoDeCaracteristicaHardwareAllResult oDetalle)
        {
            try
            {
                Limpiar();

                this.txtAbreviatura.Text = oDetalle.nombreCorto != null ? oDetalle.nombreCorto.Trim() : string.Empty;
                this.txtCodigo.Text = oDetalle.id != null ? oDetalle.id.ToString().Trim() : "0";
                this.txtDescripcion.Text = oDetalle.descripcion != null ? oDetalle.descripcion.Trim() : string.Empty;
                this.txtEstado.Text = oDetalle.estado != null ? (oDetalle.estado == 1 ? "ACTIVO" : "ANULADO") : "ANULADO";
                this.txtIdEstado.Text = oDetalle.estado != null ? oDetalle.estado.ToString().Trim() : "0";
                //this.cboCategoria.SelectedValue = oDetalle.tipoSoftware != null ? oDetalle.tipoSoftware.ToString().Trim() : "000";
                this.txtObservacion.Text = oDetalle.observaciones != null ? oDetalle.observaciones.Trim() : string.Empty;


                if (oDetalle.enFormatoSolicitud == 1)
                {
                    this.chkPresenteEnSolicitud.Checked = true;
                }
                else
                {
                    this.chkPresenteEnSolicitud.Checked = !true;
                }

                this.txtTipoHardwareID.Text = oDetalle.TipoHardwareID != null ? oDetalle.TipoHardwareID.Trim() : string.Empty;
                this.txtTipoHardwareDescripcion.Text = oDetalle.TipoHardware != null ? oDetalle.TipoHardware.Trim() : string.Empty;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString().ToUpper(), "Mensaje del sistema".ToUpper());
                return;
            }
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ClickResaltarResultados += 1;
            ResaltarResultados();
        }


        private void ResaltarResultados()
        {

            if ((ClickResaltarResultados % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "1", string.Empty, true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                c2.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c2);
                #endregion
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
           
        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                dgvRegistro.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistro.EnableFiltering = true;
                dgvRegistro.ShowHeaderCellButtons = true;
                #endregion
            }
        }

    }
}
