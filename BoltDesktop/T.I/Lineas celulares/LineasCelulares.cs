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
using System.Reflection;
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;
using Telerik.WinControls.Data;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class LineasCelulares : Form
    {
        string nombreformulario = "LINEAS CELULARES";
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_LineasCelularesCoporativasListadoAllResult> listado;
        private SAS_LineasCelularesCoporativa item;
        private SAS_LineasCelularesCoporativasListadoAllResult itemSelecionado;
        private SAS_LineasCelularesCoporativasController modelo;
        public List<int> valores_permitidos = new List<int>() { 8, 13, 37, 38, 39, 40, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 46 };
        private string lineaCelular = string.Empty;
        private string mensajeDeValidacion;
        private string result;
        private int ClickFiltro = 0;
        private int ClickResaltarResultados;

        public int IdLineaCelular = 0;

        public LineasCelulares()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = Environment.UserName.Trim();
            Actualizar();
        }

        public LineasCelulares(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            Actualizar();
        }

        public LineasCelulares(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, string _lineaCelular)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            lineaCelular = _lineaCelular;
            Actualizar();
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

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvRegistro.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvRegistro.TableElement.EndUpdate();
                //this.SetConditions();
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
            this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistro.GroupDescriptors.Clear();
            this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chlineaCelular", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
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

        private void LineasCelulares_Load(object sender, EventArgs e)
        {
            btnGrabar.Enabled = false;
            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_LineasCelularesCoporativasListadoAllResult>();
                modelo = new SAS_LineasCelularesCoporativasController();
                listado = modelo.ListOfCellLines("SAS");
                //if (lineaCelular != null)
                //{
                //    if (lineaCelular != string.Empty)
                //    {
                //        listado = modelo.ListOfCellLines("SAS", lineaCelular);
                //    }
                //    else
                //    {
                //        listado = modelo.ListOfCellLines("SAS");
                //    }
                //}


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

                if (lineaCelular != null)
                {
                    if (lineaCelular != string.Empty)
                    {

                        FilterDescriptor filter1 = new FilterDescriptor();
                        filter1.Operator = FilterOperator.Contains;
                        filter1.Value = lineaCelular;
                        filter1.IsFilterEditor = true;
                        this.dgvRegistro.Columns["chlineaCelular"].FilterDescriptor = filter1;
                        dgvRegistro.DataSource = listado.OrderBy(x => x.lineaCelular).ToList().ToDataTable<SAS_LineasCelularesCoporativasListadoAllResult>();
                        dgvRegistro.Refresh();
                    }
                    else
                    {
                        dgvRegistro.DataSource = listado.OrderBy(x => x.lineaCelular).ToList().ToDataTable<SAS_LineasCelularesCoporativasListadoAllResult>();
                        dgvRegistro.Refresh();

                    }
                }
                progressBar1.Visible = false;
                btnActualizarLista.Enabled = true;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
            btnActualizarLista.Enabled = false;
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

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CambiarEstado();
        }

        private void ObtenerObjeto()
        {

            try
            {
                item = new SAS_LineasCelularesCoporativa();
                item.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : Convert.ToInt32("0");
                item.idOperador = this.txtClieProvCodigo.Text != string.Empty ? Convert.ToInt32(this.txtClieProvCodigo.Text.Trim()) : Convert.ToInt32("1");
                item.operador = this.txtClieProv.Text != string.Empty ? Convert.ToString(this.txtClieProv.Text.Trim()) : string.Empty;
                item.lineaCelular = this.txtLineaCelular.Text != string.Empty ? Convert.ToString(this.txtLineaCelular.Text.Trim()) : string.Empty;
                item.FechaDeAlta = this.txtFechaAlta.Text != string.Empty ? Convert.ToDateTime(this.txtFechaAlta.Text.Trim()) : (DateTime?)null;
                item.estado = this.txtIdEstado.Text != string.Empty ? Convert.ToInt32(this.txtIdEstado.Text.Trim()) : Convert.ToInt32("0");
                item.estadoDescripcion = this.txtEstado.Text != string.Empty ? Convert.ToString(this.txtEstado.Text.Trim()) : string.Empty;
                item.idProducto = this.txtProductoCodigo.Text != string.Empty ? Convert.ToString(this.txtProductoCodigo.Text.Trim()) : string.Empty;
                item.equipo = this.txtEquipo.Text != string.Empty ? Convert.ToString(this.txtEquipo.Text.Trim()) : string.Empty;
                item.idPlanDeTelefoniaMovil = this.txtPlanId.Text != string.Empty ? Convert.ToInt32(this.txtPlanId.Text.Trim()) : (Int32?)null;
                item.planDeTelefoniaMovil = this.txtPlan.Text != string.Empty ? Convert.ToString(this.txtPlan.Text.Trim()) : string.Empty;
                item.valorPlan = this.txtValorPlan.Text != string.Empty ? Convert.ToDecimal(this.txtValorPlan.Text.Trim()) : (decimal?)null;
                item.permanenciaFalta = this.txtDiasRestantesParaRenovacion.Text != string.Empty ? Convert.ToInt32(this.txtDiasRestantesParaRenovacion.Text.Trim()) : (Int32?)null;
                item.penalidad = this.txtPenalidad.Text != string.Empty ? Convert.ToDecimal(this.txtPenalidad.Text.Trim()) : (decimal?)null;
                item.idCodigoGeneral = this.txtIdCodigoGeneral.Text != string.Empty ? Convert.ToString(this.txtIdCodigoGeneral.Text.Trim()) : string.Empty;
                item.idCCostoFijo = this.txtCCFijoCodigo.Text != string.Empty ? Convert.ToString(this.txtCCFijoCodigo.Text.Trim()) : string.Empty;
                item.idCCostoVariable = this.txtCCVarCodigo.Text != string.Empty ? Convert.ToString(this.txtCCVarCodigo.Text.Trim()) : string.Empty;
                item.glosa = this.txtGlosa.Text != string.Empty ? Convert.ToString(this.txtGlosa.Text.Trim()) : string.Empty;
                item.codigoERP = this.txtERPCodigo.Text != string.Empty ? Convert.ToInt32(this.txtERPCodigo.Text.Trim()) : (int?)null;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void ObtenerObjetoEliminar()
        {

            try
            {
                item = new SAS_LineasCelularesCoporativa();
                item.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : Convert.ToInt32("0");
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
                if (item.lineaCelular != string.Empty)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.ChangeState("SAS", item);
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
                    this.txtLineaCelular.Focus();
                    return;
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
            Eliminar();
        }

        private void Eliminar()
        {
            #region Eliminar() 
            if (user2 != null)
            {
                if (user2.IdUsuario != null)
                {
                    if (user2.IdUsuario.Trim().ToUpper() == "Admin".ToUpper() || user2.IdUsuario.Trim() == "Administrador".ToUpper() || user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || user2.IdUsuario.Trim().ToUpper() == "erickaurazo".ToUpper())
                    {
                        try
                        {
                            ObtenerObjetoEliminar();
                            if (item.lineaCelular != string.Empty)
                            {
                                modelo = new SAS_LineasCelularesCoporativasController();
                                int resultado = 0;
                                resultado = modelo.Remove("SAS", item);
                                if (resultado == 4)
                                {
                                    MessageBox.Show("Se cambio ha eliminado correctamente", "Confirmación de Eliminación del registro");
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
                                this.txtLineaCelular.Focus();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Opción no habilitada para el usuario ", "Advertencia del sistema");
                return;
            }
            #endregion
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

        private void LineasCelulares_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        private void Cancelar()
        {

            gbEdit.Enabled = false;
            gbList.Enabled = true;
            btnGrabar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario() == true)
            {
                Registrar();
            }
        }

        private void Registrar()
        {
            try
            {

                ObtenerObjeto();
                modelo = new SAS_LineasCelularesCoporativasController();
                int resultado = modelo.ToRegister("SAS", item);
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

        private bool ValidarFormulario()
        {
            mensajeDeValidacion = string.Empty;
            bool resultado = true;

            if (this.txtLineaCelular.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar una línea valida ";
                resultado = false;
                return resultado;
            }

            if (this.txtLineaCelular.Text.Trim().Length != 9)
            {
                mensajeDeValidacion += "\n Debe ingresar una línea en el formato correcto ";
                resultado = false;
                return resultado;
            }

            if (txtClieProvCodigo.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar código del empresa operadora o asociado valido";
                resultado = false;
                return resultado;
            }

            if (txtClieProv.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar una empresa operadora valido ";
                resultado = false;
                return resultado;
            }

            if (txtIdCodigoGeneral.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar código del colaborador o asociado valido";
                resultado = false;
                return resultado;
            }

            if (txtNombres.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar colaborador o asociado valido";
                resultado = false;
                return resultado;
            }

            if (txtPlanId.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar código del plan de datos y voz valido";
                resultado = false;
                return resultado;
            }

            if (txtPlan.Text.Trim() == string.Empty)
            {
                mensajeDeValidacion += "\n Debe ingresar plan de datos y voz valido";
                resultado = false;
                return resultado;
            }

            string fechaAValidar = this.txtValidarFecha.Text;

            if (this.txtFechaAlta.Text == fechaAValidar)
            {
                mensajeDeValidacion += "\n Debe ingresar una fecha de activación valida";
                resultado = false;
                return resultado;
            }

            return resultado;
        }

        private void Limpiar()
        {
            try
            {
                item = new SAS_LineasCelularesCoporativa();
                item.id = 0;
                item.lineaCelular = string.Empty;
                item.estado = 0;

                itemSelecionado = new SAS_LineasCelularesCoporativasListadoAllResult();
                item.id = 0;
                item.lineaCelular = string.Empty;
                item.estado = 0;

                txtCodigo.Text = string.Empty;
                txtEstado.Text = "ACTIVO";
                txtIdEstado.Text = "1";
                txtClieProvCodigo.Text = string.Empty;
                txtClieProv.Text = string.Empty;
                txtCCVarCodigo.Text = string.Empty;
                txtCCFijoCodigo.Text = string.Empty;
                txtIdCodigoGeneral.Text = string.Empty;
                txtPenalidad.Text = string.Empty;
                txtDiasRestantesParaRenovacion.Text = string.Empty;
                txtValorPlan.Text = string.Empty;
                txtPlan.Text = string.Empty;
                txtPlanId.Text = string.Empty;
                txtEquipo.Text = string.Empty;
                txtProductoCodigo.Text = string.Empty;
                txtFechaAlta.Text = string.Empty;
                txtLineaCelular.Text = string.Empty;
                txtGlosa.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtPlan.Text = string.Empty;
                txtCCFijo.Text = string.Empty;
                txtCCVar.Text = string.Empty;
                txtNombres.Text = string.Empty;
                this.txtERPCodigo.Text = string.Empty;
                txtDispositivoERP.Text = string.Empty;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }


        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            IdLineaCelular = 0;
            try
            {
                Limpiar();
                #region 
                itemSelecionado = new SAS_LineasCelularesCoporativasListadoAllResult();
                itemSelecionado.lineaCelular = string.Empty;
                itemSelecionado.estado = 0;
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chLineaCelular"].Value != null)
                        {
                            //if (dgvRegistro.CurrentRow.Cells["chLineaCelular"].Value.ToString() != string.Empty)
                            //{
                            string codigo = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString()) : "0");
                            IdLineaCelular = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chId"].Value.ToString()) : 0);
                            int codigoNumerico = Convert.ToInt32(codigo);
                            var resultado = listado.Where(x => x.id == codigoNumerico).ToList();
                            if (resultado.ToList().Count == 1)
                            {
                                itemSelecionado = resultado.Single();
                                itemSelecionado.id = codigoNumerico;
                                AsignarObjetoEnFormularioDeEdicion(itemSelecionado);
                            }
                            else if (resultado.ToList().Count > 1)
                            {
                                itemSelecionado = resultado.ElementAt(0);
                                itemSelecionado.id = codigoNumerico;
                                AsignarObjetoEnFormularioDeEdicion(itemSelecionado);
                            }
                            else
                            {
                                Limpiar();
                            }
                            //}
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

        private void AsignarObjetoEnFormularioDeEdicion(SAS_LineasCelularesCoporativasListadoAllResult itemSelecionado)
        {
            try
            {
                this.txtCodigo.Text = itemSelecionado.id != null ? itemSelecionado.id.ToString().Trim() : string.Empty;
                this.txtLineaCelular.Text = itemSelecionado.lineaCelular != null ? itemSelecionado.lineaCelular.Trim() : string.Empty;
                if (itemSelecionado.estado == 1)
                {
                    this.txtEstado.Text = "ACTIVO";
                    this.txtIdEstado.Text = "1";
                }
                else
                {
                    this.txtEstado.Text = "ANULADO";
                    this.txtIdEstado.Text = "0";
                }
                txtClieProvCodigo.Text = itemSelecionado.idOperador != null ? itemSelecionado.idOperador.Value.ToString().Trim() : string.Empty;
                txtClieProv.Text = itemSelecionado.operador != null ? itemSelecionado.operador.ToString().Trim() : string.Empty;
                txtCCVarCodigo.Text = itemSelecionado.idCCostoVariable != null ? itemSelecionado.idCCostoVariable.ToString().Trim() : string.Empty;
                txtCCFijoCodigo.Text = itemSelecionado.idCCostoFijo != null ? itemSelecionado.idCCostoFijo.ToString().Trim() : string.Empty;
                txtIdCodigoGeneral.Text = itemSelecionado.idCodigoGeneral != null ? itemSelecionado.idCodigoGeneral.ToString().Trim() : string.Empty;
                txtPenalidad.Text = itemSelecionado.penalidad != null ? itemSelecionado.penalidad.Value.ToString().Trim() : string.Empty;
                txtDiasRestantesParaRenovacion.Text = itemSelecionado.permanenciaFalta != null ? itemSelecionado.permanenciaFalta.Value.ToString().Trim() : string.Empty;
                txtValorPlan.Text = itemSelecionado.valorPlan != null ? itemSelecionado.valorPlan.Value.ToString().Trim() : string.Empty;
                txtPlan.Text = itemSelecionado.planDeTelefoniaMovil != null ? itemSelecionado.planDeTelefoniaMovil.ToString().Trim() : string.Empty;
                txtPlanId.Text = itemSelecionado.idPlanDeTelefoniaMovil != null ? itemSelecionado.idPlanDeTelefoniaMovil.Value.ToString().Trim() : string.Empty;
                txtEquipo.Text = itemSelecionado.equipo != null ? itemSelecionado.equipo.ToString().Trim() : string.Empty;
                txtProductoCodigo.Text = itemSelecionado.idProducto != null ? itemSelecionado.idProducto.ToString().Trim() : string.Empty;
                txtFechaAlta.Text = itemSelecionado.FechaDeAlta != null ? itemSelecionado.FechaDeAlta.Value.ToString().Trim() : string.Empty;
                txtGlosa.Text = itemSelecionado.glosa != null ? itemSelecionado.glosa.ToString().Trim() : string.Empty;
                txtProducto.Text = itemSelecionado.producto != null ? itemSelecionado.producto.ToString().Trim() : string.Empty;
                txtCCFijo.Text = itemSelecionado.CCostoFijo != null ? itemSelecionado.CCostoFijo.ToString().Trim() : string.Empty;
                txtCCVar.Text = itemSelecionado.CCostoVariable != null ? itemSelecionado.CCostoVariable.ToString().Trim() : string.Empty;
                txtNombres.Text = itemSelecionado.nombresCompletos != null ? itemSelecionado.nombresCompletos.ToString().Trim() : string.Empty;
                this.txtERPCodigo.Text = itemSelecionado.codigoErp != 0 ? itemSelecionado.codigoErp.ToString().Trim() : string.Empty;
                this.txtDispositivoERP.Text = itemSelecionado.dispositivo != null ? itemSelecionado.dispositivo.ToString().Trim() : string.Empty;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void Nuevo()
        {
            try
            {
                item = new SAS_LineasCelularesCoporativa();
                item.id = 0;
                item.lineaCelular = string.Empty;
                item.estado = 0;


                Limpiar();
                Cancelar();
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

        public void solo_numeros(ref TextBox textbox, KeyPressEventArgs e)
        {
            char signo_decimal = (char)46; //Si pulsan el punto .

            if (char.IsNumber(e.KeyChar) | valores_permitidos.Contains(e.KeyChar) |
                e.KeyChar == (char)Keys.Escape | e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false; // No hacemos nada y dejamos que el sistema controle la pulsación de tecla
                return;
            }
            else if (e.KeyChar == signo_decimal)
            {
                //Si no hay caracteres, o si ya hay un punto, no dejaremos poner el punto(.)
                if (textbox.Text.Length == 0 | textbox.Text.LastIndexOf(signo_decimal) >= 0)
                {
                    e.Handled = true; // Interceptamos la pulsación para que no permitirla.
                }
                else //Si hay caracteres continuamos las comprobaciones
                {
                    //Cambiamos la pulsación al separador decimal definido por el sistema 
                    e.KeyChar = Convert.ToChar(System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator);
                    e.Handled = false; // No hacemos nada y dejamos que el sistema controle la pulsación de tecla
                }
                return;
            }
            else if (e.KeyChar == (char)13) // Si es un enter
            {
                e.Handled = true; //Interceptamos la pulsación para que no la permita.
                SendKeys.Send("{TAB}"); //Pulsamos la tecla Tabulador por código
            }
            else //Para el resto de las teclas
            {
                e.Handled = true; // Interceptamos la pulsación para que no tenga lugar
            }
        }

        public void solo_numeros_KeyDown(ref TextBox textbox, KeyEventArgs e)
        {
            if (valores_permitidos.Contains(e.KeyValue) || (e.KeyCode == Keys.C && e.Control) ||
            (e.KeyCode == Keys.V && e.Control) || (e.KeyCode == Keys.X && e.Control))
                e.SuppressKeyPress = false;
            else
                e.SuppressKeyPress = true;
        }

        private void txtValorPlan_KeyPress(object sender, KeyPressEventArgs e)
        {

            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtValorPlan_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPenalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtDiasRestantesParaRenovacion_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            solo_numeros_KeyDown(ref textbox, e);
        }

        private void txtDiasRestantesParaRenovacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            solo_numeros(ref textbox, e); // Llamamos a nuestro método
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            ElegirColumnas();
        }
        private void ElegirColumnas()
        {
            this.dgvRegistro.ShowColumnChooser();
        }

        private void SetConditions()
        {             //add a couple of sample formatting objects            
            //ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
            //c1.RowBackColor = Utiles.colorRojoClaro;
            //c1.CellBackColor = Utiles.colorRojoClaro;
            //dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c1);

            //ConditionalFormattingObject c2 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
            //c2.RowBackColor = Utiles.colorAmarilloClaro;
            //c2.CellBackColor = Utiles.colorAmarilloClaro;
            //dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c2);

        }

        private void SetUnConditions()
        {             //add a couple of sample formatting objects            
            //ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
            //c1.RowBackColor = Color.White;
            //c1.CellBackColor = Color.White;
            //dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c1);

            //ConditionalFormattingObject c2 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
            //c2.RowBackColor = Color.White;
            //c2.CellBackColor = Color.White;
            //dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c2);


        }

        private void chkResaltarResultados_CheckedChanged(object sender, EventArgs e)
        {
            // RETIRADO

            //if (dgvRegistro != null)
            //{
            //    if (dgvRegistro.RowCount > 0)
            //    {
            //        if (chkResaltarResultados.Checked == true)
            //        {
            //            SetConditions();
            //        }
            //        else
            //        {
            //            SetUnConditions();
            //        }
            //    }
            //}
        }

        private void btnLineaActivar_Click(object sender, EventArgs e)
        {
            ActivarLinea();
        }

        private void ActivarLinea()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.ActivarLinea("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Activacion de Línea");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("NO se cambio el estado correctamente", "Confirmación de Activacion de Línea");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnLineaSuspender_Click(object sender, EventArgs e)
        {
            SuspenderLinea();
        }

        private void SuspenderLinea()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.SuspenderLinea("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("NO se cambio el estado correctamente", "Confirmación de Suspención de Línea celular");
                    }

                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnLineaAveriada_Click(object sender, EventArgs e)
        {
            LineaAveriada();
        }

        private void LineaAveriada()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.AveriarLinea("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Notificación de suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación de Notificación de suspención de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnLineaBaja_Click(object sender, EventArgs e)
        {
            LineaDeBaja();
        }

        private void LineaDeBaja()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.BajarLinea("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Baja de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("o se cambio el estado correctamente", "Confirmación de Baja de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnLineaEnProcesoSuspención_Click(object sender, EventArgs e)
        {
            LineaEnProcesoSuspencion();
        }

        private void LineaEnProcesoSuspencion()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.EnProcesoDeSuspencion("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnLineaEnProcesoDeActivacion_Click(object sender, EventArgs e)
        {
            LineaEnProcesoDeActivacion();
        }

        private void LineaEnProcesoDeActivacion()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.EnProcesoDeActivacion("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación de Activación de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación de Activación de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
        }

        private void ActivateFilter()
        {
            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | DesActivar Filtro()
                dgvRegistro.EnableFiltering = true;
                #endregion
            }
            else
            {

                #region Par() | Activar Filtro()
                dgvRegistro.EnableFiltering = !true;
                #endregion
            }
        }

        private void btbResaltarResultados_Click(object sender, EventArgs e)
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
                dgvRegistro.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c1);



                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistro.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c4);


                ConditionalFormattingObject c11 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c11.RowBackColor = Utiles.colorRojoClaro;
                c11.CellBackColor = Utiles.colorRojoClaro;
                dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c11);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
                c2.RowBackColor = Utiles.colorAmarilloClaro;
                c2.CellBackColor = Utiles.colorAmarilloClaro;
                dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c2);

                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvRegistro.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c1);



                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "0", string.Empty, true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistro.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c4);

                ConditionalFormattingObject c11 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "RETIRADO", "", true);
                c11.RowBackColor = Color.White;
                c11.CellBackColor = Color.White;
                dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c11);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                dgvRegistro.Columns["chestadoPlanilla"].ConditionalFormattingObjectList.Add(c2);

                #endregion
            }
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        private void Adjuntar()
        {
            try
            {
                #region Attach()
                if (this.txtCodigo.Text != string.Empty)
                {
                    if (this.txtCodigo.Text != "0")
                    {

                        AdjuntarArchivos ofrm = new AdjuntarArchivos(conection, user2, companyId, privilege, IdLineaCelular.ToString(), nombreformulario);
                        ofrm.Show();

                    }
                    else
                    {
                        MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnEnProcesoDeBaja_Click(object sender, EventArgs e)
        {
            EnProcesoDeBaja();
        }

        private void EnProcesoDeBaja()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.EnProcesoDeBaja("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }


        private void btnEnProcesoDeCesion_Click(object sender, EventArgs e)
        {
            EnProcesoDeCesion();
        }

        private void EnProcesoDeCesion()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.EnProcesoDeCesion("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnCesionDeTitularidad_Click(object sender, EventArgs e)
        {
            CesionDeTitularidad();
        }


        private void CesionDeTitularidad()
        {
            try
            {
                if (IdLineaCelular > 0)
                {
                    modelo = new SAS_LineasCelularesCoporativasController();
                    int resultado = 0;
                    resultado = modelo.CesionDeTitularidad("SAS", IdLineaCelular);
                    if (resultado == 1)
                    {
                        MessageBox.Show("Se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                        Actualizar();
                    }
                    else
                    {
                        MessageBox.Show("No se cambio el estado correctamente", "Confirmación proceso de suspención de Línea celular");
                    }


                }
                else
                {
                    MessageBox.Show("Debe incluir una descripción", "Advertencia del sistema");
                    this.txtLineaCelular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

    }
}
