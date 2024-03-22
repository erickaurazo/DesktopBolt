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
using MyDataGridViewColumns;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Maestros
{
    public partial class AccionesCorrectivas : Form
    {

        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoAccionesCorrectivasAll selectedItem;
        private List<SAS_ListadoAccionesCorrectivasAll> resultList;
        SAS_AccionesCorrectivasRegistroController model;
        SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO itemRegistar;
        SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO itemDelete;
        private int ParImparFiltro = 0;
        private int ParImpar = 0;

        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;

        public AccionesCorrectivas()
        {
            InitializeComponent();
            connection = "SAS";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();

        }

        public AccionesCorrectivas(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            connection = _connection;
            userLogin = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            Actualizar();
        }


        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
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
            items1.Add(new GridViewSummaryItem("chDescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            LimpiarFomularioEdicion();
            gbEdit.Enabled = true;
            gbList.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizar.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnRegistrar.Enabled = true;
            btnAtras.Enabled = true;

        }

        private void LimpiarFomularioEdicion()
        {
            txtCodigo.Text = "0";
            txtIdEstado.Text = "1";
            txtEstado.Text = "ACTIVO";
            txtFormularioCodigo.Clear();
            txtFormulario.Clear();
            txtDescripcion.Clear();
            chkVisibleEnReportes.Checked = true;
            //txtFormularioCodigo.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Actualizar()
        {
            BarraPrincipal.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnRegistrar.Enabled = false;
            btnAtras.Enabled = false;
            pgBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            if (this.txtEstado.Text != null)
            {
                if (this.txtEstado.Text != "ANULADO")
                {
                    gbEdit.Enabled = true;
                    gbList.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnActualizar.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
                    btnRegistrar.Enabled = true;
                    btnAtras.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El documento no tiene el estado para la edición", "Confirmación del sistema");
                    return;
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registrar();
        }

        private void Registrar()
        {
            #region Registrar()  
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtFormularioCodigo.Text.Trim() != string.Empty && this.txtFormulario.Text.Trim() != string.Empty)
            {
                if (ValidateForm() == true)
                {
                    itemRegistar = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                    itemRegistar.idAccion = Convert.ToInt32(this.txtCodigo.Text);
                    itemRegistar.descripcion = this.txtDescripcion.Text.Trim();
                    itemRegistar.idRegistro = Convert.ToInt32(this.txtFormularioCodigo.Text);
                    itemRegistar.Estado = this.txtEstado.Text.Trim() == "1" ? Convert.ToByte("1") : Convert.ToByte("0");
                    itemRegistar.Visible = chkVisibleEnReportes.Checked == true ? Convert.ToByte("1") : Convert.ToByte("0");


                    model = new SAS_AccionesCorrectivasRegistroController();
                    if (model.ToRegister(connection, itemRegistar) > 0)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        bgwHilo.RunWorkerAsync();
                        gbEdit.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
                        btnGrabar.Enabled = true;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;
                       
                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
            #endregion
        }


        private bool ValidateForm()
        {
            bool status = true;
            if (this.txtCodigo.Text.Trim() == string.Empty)
            {
                status = false;
            }


            if (this.txtFormularioCodigo.Text.Trim() == string.Empty)
            {
                status = false;
            }

            if (this.txtFormulario.Text.Trim() == string.Empty)
            {
                status = false;
            }

            return status;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void Atras()
        {
            LimpiarFomularioEdicion();
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnRegistrar.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void Anular()
        {
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtFormularioCodigo.Text.Trim() != string.Empty && this.txtFormulario.Text.Trim() != string.Empty && this.txtFormularioCodigo.Text.Trim() != "0")
            {
                itemRegistar = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                itemRegistar.idAccion = Convert.ToInt32(this.txtCodigo.Text);
                model = new SAS_AccionesCorrectivasRegistroController();
                if (model.ToChangeStatus(connection, itemRegistar) > 0)
                {
                    MessageBox.Show("Operación realizada satisfactoriamente", "Confirmación del sistema");
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnActualizar.Enabled = true;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnGrabar.Enabled = false;
                    btnEditar.Enabled = true;
                    btnAtras.Enabled = false;
                    Actualizar();
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtFormularioCodigo.Text.Trim() != string.Empty && this.txtFormulario.Text.Trim() != string.Empty && this.txtFormularioCodigo.Text.Trim() != "0")
            {
                itemRegistar = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                itemRegistar.idAccion = Convert.ToInt32(this.txtCodigo.Text);
                model = new SAS_AccionesCorrectivasRegistroController();
                if (model.ToDelete(connection, itemRegistar) > 0)
                {
                    MessageBox.Show("Operación realizada satisfactoriamente", "Confirmación del sistema");
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                    btnNuevo.Enabled = true;
                    btnActualizar.Enabled = true;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnGrabar.Enabled = false;
                    btnEditar.Enabled = true;
                    btnAtras.Enabled = false;
                    Actualizar();
                }
                else
                {
                    MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Faltan datos para poder registrar el formulario", "Confirmación del sistema");
                return;
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            VerHistorial();
        }

        private void VerHistorial()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void btnFlujoAprobacion_Click(object sender, EventArgs e)
        {
            FlujoDeAprobacion();
        }

        private void FlujoDeAprobacion()
        {
            MessageBox.Show("No tiene accesos para este formulario", "Mensaje del sistema");
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        private void Adjuntar()
        {

        }

        private void btnNotificar_Click(object sender, EventArgs e)
        {

        }

        private void Notificar()
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Registrar();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecuarConsultaAsincrona();
        }

        private void EjecuarConsultaAsincrona()
        {

            try
            {
                #region Consultar()

                resultList = new List<SAS_ListadoAccionesCorrectivasAll>();
                model = new SAS_AccionesCorrectivasRegistroController();

                resultList = model.ListAlll(connection).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarResultados();
        }

        private void PresentarResultados()
        {
            try
            {
                #region PresentarResultados()
                dgvRegistro.DataSource = resultList;
                dgvRegistro.Refresh();

                PintarResultadosEnGrilla();
                BarraPrincipal.Enabled = true;
                gbEdit.Enabled = true;
                gbList.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = true;
                btnAnular.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminarRegistro.Enabled = true;
                btnRegistrar.Enabled = false;
                btnAtras.Enabled = false;
                pgBar.Visible = true;


                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        private void PintarResultadosEnGrilla()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);



                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c1);



                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "0", "", true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistro.Columns["chEstado"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void Exportar()
        {
            modelExportToExcel = new ExportToExcelHelper();
            modelExportToExcel.ExportarToExcel(dgvRegistro, saveFileDialog);
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ResaltarResultados();
        }

        private void ResaltarResultados()
        {
            ParImparFiltro += 1;
            PintarResultadosEnGrilla();
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
        }

        private void ElegirColumna()
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            #region Selection Changed() 
            selectedItem = new SAS_ListadoAccionesCorrectivasAll();
            selectedItem.AccionCorrectivaId = 0;
            selectedItem.AccionCorrectiva = string.Empty;
            selectedItem.FormularioEvaluacionCodigo = 0;
            selectedItem.Formulario = string.Empty;

            selectedItem.Estado = 0;
            selectedItem.EstadoAccionCorrectiva = string.Empty;
            selectedItem.Visible = 0;
            selectedItem.VisibleEnReportes = string.Empty;

            LimpiarFomularioEdicion();
            if (dgvRegistro.Rows.Count > 0)
            {
                if (dgvRegistro.CurrentRow != null && dgvRegistro.CurrentRow.Cells["chIdAccion"].Value != null)
                {
                    string idSelect = dgvRegistro.CurrentRow.Cells["chIdAccion"].Value != null ? Convert.ToString(dgvRegistro.CurrentRow.Cells["chIdAccion"].Value.ToString().Trim()) : string.Empty;

                    var resultByFilterId = resultList.Where(x => x.AccionCorrectivaId.ToString().Trim() == idSelect).ToList();
                    if (resultByFilterId.ToList().Count == 1)
                    {
                        selectedItem = new SAS_ListadoAccionesCorrectivasAll();
                        selectedItem = resultByFilterId.ElementAt(0);
                        txtCodigo.Text = selectedItem.AccionCorrectivaId.ToString();
                        txtIdEstado.Text = selectedItem.Estado.ToString();
                        txtEstado.Text = selectedItem.EstadoAccionCorrectiva.ToString();
                        txtFormularioCodigo.Text = selectedItem.FormularioEvaluacionCodigo.ToString();
                        txtFormulario.Text = selectedItem.Formulario.ToString();
                        txtDescripcion.Text = selectedItem.AccionCorrectiva.ToString();
                        if (selectedItem.Visible.Value.ToString() == "1")
                        {
                            chkVisibleEnReportes.Checked = true;
                        }
                        else
                        {
                            chkVisibleEnReportes.Checked = !true;
                        }
                    }
                }
            }
            #endregion
        }

        private void AccionesCorrectivas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AccionesCorrectivas_Load(object sender, EventArgs e)
        {

        }

        private void btnActivarfiltro_Click(object sender, EventArgs e)
        {
            ParImparFiltro += 1;
            ActivateFilter();
        }

        private void ActivateFilter()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                dgvRegistro.ShowHeaderCellButtons = !true;
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
