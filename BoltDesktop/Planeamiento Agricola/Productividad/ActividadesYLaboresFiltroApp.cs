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
using Asistencia.Negocios.PlaneamientoAgricola;
using MyControlsDataBinding.Controles;


namespace ComparativoHorasVisualSATNISIRA.Planeamiento_Agricola.Producitivdad
{
    public partial class ActividadesYLaboresFiltroApp : Form
    {
        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoLaboresFiltroProductividadResult selectedItem;
        private List<SAS_ListadoLaboresFiltroProductividadResult> resultList;
        private LaboresFiltro model;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private GlobalesHelper globalHelper;
        private SAS_ActividadLaborProductividadEmpleado ItemRegistro;
        #endregion




        public ActividadesYLaboresFiltroApp(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege)
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
            btnActualizar.Enabled = false;
            Limpiar(this, gbEdit);
            Inicio();
            Consultar();
        }


        public ActividadesYLaboresFiltroApp()
        {
            InitializeComponent();
            connection = "SAS";
            userLogin = new SAS_USUARIOS();
            userLogin.IdUsuario = "eaurazo";
            userLogin.NombreCompleto = "ERICK AURAZO";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : string.Empty;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnActualizar.Enabled = false;
            Limpiar(this, gbEdit);
            Inicio();
            Consultar();
        }




        private void Consultar()
        {

            gbListado.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();



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
            this.dgvListado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chLabor", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }






        private void ActividadesYLaboresFiltroApp_Load(object sender, EventArgs e)
        {

        }



        #region Metodos()


        private void EdicionDesdeTeclado(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.G)
            {
                Grabar();
            }
            if (e.KeyData == (Keys.Control | Keys.E))
            {
                Editar(true);
            }
            if (e.KeyData == (Keys.Escape))
            {
                Atras();
            }
            if (Control.ModifierKeys == Keys.Alt && e.KeyCode == Keys.A)
            {
               Anular();
            }

        }

        private void AsignarSeleccionarAControlesDeEdicion(SAS_ListadoLaboresFiltroProductividadResult selectedItem)
        {
            try
            {
                txtActividad.Text = selectedItem.Actividad != null ? selectedItem.Actividad.Trim() : string.Empty;
                txtActividadID.Text = selectedItem.ActividadID != null ? selectedItem.ActividadID.Trim() : string.Empty;
                txtCodigoActividadLabor.Text = selectedItem.ActividadLaborCodigo != null ? selectedItem.ActividadLaborCodigo.Trim() : string.Empty;
                txtEmpresa.Text = selectedItem.Empresa != null ? selectedItem.Empresa.Trim() : string.Empty;
                txtEmpresaCodigo.Text = selectedItem.EmpresaID != null ? selectedItem.EmpresaID.Trim() : string.Empty;


                txtEstado.Text = "INACTIVO";
                txtIdEstado.Text = "AN";
                if (selectedItem.VisibleEnAplicativo == 1)
                {
                    txtEstado.Text = "ACTIVO";
                    txtIdEstado.Text = "AC";
                }

               
                txtLabor.Text = selectedItem.Labor != null ? selectedItem.Labor.Trim() : string.Empty;
                txtLaborID.Text = selectedItem.LaborID != null ? selectedItem.LaborID.Trim() : string.Empty;


                txtUM.Text = selectedItem.RendimientoUnidadMedidaID != null ? selectedItem.RendimientoUnidadMedidaID.Trim() : string.Empty;
                txtUMID.Text = selectedItem.RendimientoUnidadMedidaID != null ? selectedItem.RendimientoUnidadMedidaID.Trim() : string.Empty;


               chkVisibleEnAplicativo.Checked = false;
                if (selectedItem.VisibleEnAplicativo == 1)
                {
                    chkVisibleEnAplicativo.Checked = true;

                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void EjecutarConsulta()
        {
            try
            {
                resultList = new List<SAS_ListadoLaboresFiltroProductividadResult>();
                model = new LaboresFiltro();
                resultList = model.ListAll(connection);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void PresentarDatosEnFormulario()
        {
            try
            {
                dgvListado.DataSource = resultList;
                dgvListado.Refresh();
                gbListado.Enabled = true;
                BarraPrincipal.Enabled = true;
                progressBar1.Visible = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void Nuevo()
        {
            try
            {
                LimpiarFormulario();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 1)
            {
                #region Par() | Activar Filtro()
                dgvListado.EnableFiltering = !true;
                dgvListado.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvListado.EnableFiltering = true;
                dgvListado.ShowHeaderCellButtons = true;
                #endregion
            }
        }

        private void ResaltarResultados()
        {

            if ((ClickResaltarResultados % 2) == 1)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "AN", string.Empty, true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvListado.Columns["chEstadoID"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "AN", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvListado.Columns["chEstadoID"].ConditionalFormattingObjectList.Add(c1);
                #endregion
            }

        }

        private void LimpiarFormulario()
        {
            Limpiar(this, gbEdit);
        }

        public static void Limpiar(Control control, GroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }

        public static void Limpiar(Control control, RadGroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }


        private void Editar(bool flag)
        {
            if (flag == true)
            {
                if (selectedItem.ActividadLaborCodigo != null)
                {
                    if (selectedItem.ActividadLaborCodigo.Trim() != string.Empty)
                    {
                        gbEdit.Enabled = true;
                        gbListado.Enabled = false;
                        btnGrabar.Enabled = true;
                        btnEditar.Enabled = false;
                        btnAtras.Enabled = true;
                    }
                }
            }
            else
            {
                if (selectedItem.ActividadLaborCodigo != null)
                {
                    if (selectedItem.ActividadLaborCodigo.Trim() != string.Empty)
                    {
                        gbEdit.Enabled = !true;
                        gbListado.Enabled = !false;
                        btnGrabar.Enabled = !true;
                        btnEditar.Enabled = !false;
                        btnAtras.Enabled = !true;
                    }
                }
            }



        }


        private void Grabar()
        {
            try
            {
                if (selectedItem.ActividadLaborCodigo != null)
                {
                    if (selectedItem.ActividadLaborCodigo.Trim() != string.Empty)
                    {

                        #region Registro

                        ItemRegistro = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegistro.ActividadLaborID = selectedItem.ActividadLaborCodigo;
                        ItemRegistro.EmpresaID = selectedItem.EmpresaID;
                        ItemRegistro.Estado = selectedItem.VisibleEnAplicativo != (byte?)null ? selectedItem.VisibleEnAplicativo : Convert.ToByte(0);
                        int resultadoOpercion = 0;
                        model = new LaboresFiltro();
                        resultadoOpercion = model.ToRegister(connection, ItemRegistro);
                        if (resultadoOpercion == 1)
                        {
                            MessageBox.Show("Se registro correctamente el consumidor al filtro del aplicativo", "Mensaje del sistema");
                        }
                        else
                        {
                            MessageBox.Show("Se actualizó correctamente el consumidor al filtro del aplicativo", "Mensaje del sistema");
                        }

                        Consultar();

                        gbEdit.Enabled = !true;
                        gbListado.Enabled = !false;
                        btnGrabar.Enabled = !true;
                        btnEditar.Enabled = !false;
                        btnAtras.Enabled = !true;
                        btnActualizar.Enabled = true;
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("No se puede editar el registro", "Mensaje del sistema");
                    return;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void Atras()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void Anular()
        {

            try
            {
                if (selectedItem.ActividadLaborCodigo != null)
                {
                    if (selectedItem.ActividadLaborCodigo.Trim() != string.Empty)
                    {
                        ItemRegistro = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegistro.ActividadLaborID = selectedItem.ActividadLaborCodigo;
                        ItemRegistro.EmpresaID = selectedItem.EmpresaID;
                        ItemRegistro.Estado = selectedItem.VisibleEnAplicativo;
                        int resultadoOpercion = 0;
                        model = new LaboresFiltro();
                        resultadoOpercion = model.RevokeRegistration(connection, ItemRegistro);
                        if (resultadoOpercion == 1)
                        {
                            MessageBox.Show("Se registro correctamente el consumidor al filtro del aplicativo", "Mensaje del sistema");
                        }
                        else
                        {
                            MessageBox.Show("Se actualizó correctamente el consumidor al filtro del aplicativo", "Mensaje del sistema");
                        }

                        Consultar();
                        btnActualizar.Enabled = true;

                    }
                }
                else
                {
                    MessageBox.Show("No se puede editar el registro", "Mensaje del sistema");
                    return;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Historial()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Eliminar()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ExportToExcel()
        {
            try
            {
                modelExportToExcel = new ExportToExcelHelper();
                modelExportToExcel.ExportarToExcel(dgvListado, saveFileDialog);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Adjuntar()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void CambiarEstadoDispositivo()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GenerarFormatosPDF()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ElegirColumna()
        {
            try
            {
                this.dgvListado.ShowColumnChooser();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void Cerrar()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Cancelar()
        {
            try
            {
                MessageBox.Show("Opción no dispobible", "MENSAJE DEL SISTEMA");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        #endregion

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Editar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar(true);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            ClickResaltarResultados += 1;
            ResaltarResultados();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Historial();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Adjuntar();
        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {
            CambiarEstadoDispositivo();
        }

        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            GenerarFormatosPDF();
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            ElegirColumna();
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void ActividadesYLaboresFiltroApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PresentarDatosEnFormulario();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region
                selectedItem = new SAS_ListadoLaboresFiltroProductividadResult();
                selectedItem.EmpresaID = string.Empty;
                selectedItem.Empresa = string.Empty;
                selectedItem.ActividadLaborCodigo = string.Empty;
                selectedItem.ActividadID = string.Empty;
                selectedItem.VisibleEnAplicativo = 0;
                selectedItem.Actividad = string.Empty;
                selectedItem.Labor = string.Empty;
                selectedItem.RendimientoUnidadMedida = string.Empty;
                selectedItem.RendimientoUnidadMedidaID = string.Empty;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chActividadLaborCodigo"].Value != null)
                        {
                            string CodigoSelecionado = (dgvListado.CurrentRow.Cells["chActividadLaborCodigo"].Value != null ? dgvListado.CurrentRow.Cells["chActividadLaborCodigo"].Value.ToString().Trim() : string.Empty);

                            
                            var resultadoDeSelección = resultList.Where(x => x.ActividadLaborCodigo.Trim() == CodigoSelecionado.Trim() ).ToList();

                            if (resultadoDeSelección.ToList().Count > 0)
                            {
                                selectedItem = resultadoDeSelección.ElementAt(0);
                                AsignarSeleccionarAControlesDeEdicion(selectedItem);
                            }

                        }
                    }
                }
                else
                {
                }
                #endregion
            }

            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void btnAnularD_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void btnEditarD_Click(object sender, EventArgs e)
        {
            Editar(true);
        }

        private void dgvListado_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void gbEdit_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);

        }

        private void btnCancelar_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void btnRegistrar_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void ActividadesYLaboresFiltroApp_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }
    }
}



