﻿using System;
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
using Asistencia.Negocios.Calidad;
using Asistencia.Negocios.ProduccionPacking;


namespace ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga
{
    public partial class ConformacionDeCarga : Form
    {

        #region Variables()
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId, desde, hasta = string.Empty;
        private string conection;
        private SAS_ListadoConformacionDeCargaByPeriodoResult selectedItem;
        private List<SAS_ListadoConformacionDeCargaByPeriodoResult> listAll;
        SAS_CondormidadDeCargaController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        public MesController MesesNeg;
        private ExportToExcelHelper modelExportToExcel;
        public int ParImparFiltro = 0;
        private int ClickFiltro;
        private int Id;
        private string EstadoId;

        public int ClickResaltarResultados { get; private set; }

        #endregion

        public ConformacionDeCarga()
        {
            InitializeComponent();
            selectedItem = new SAS_ListadoConformacionDeCargaByPeriodoResult();
            selectedItem.Id = 0;

            CargarMeses();
            ObtenerFechasIniciales();
            conection = "SAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            user.IdCodigoGeneral = "100369";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.nuevo = 1;
            privilege.editar = 1;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;
            //btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            //btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnResaltar.Enabled = true;
            //btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            gbCabecera.Enabled = false;
            gbList.Enabled = false;
            MakeInquiry();
        }


        public ConformacionDeCarga(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            selectedItem = new SAS_ListadoConformacionDeCargaByPeriodoResult();
            selectedItem.Id = 0;
            CargarMeses();
            ObtenerFechasIniciales();
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            btnNuevo.Enabled = true;
            //btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            //btnRegistrar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            //btnFlujoAprobacion.Enabled = false;
            btnResaltar.Enabled = true;
            //btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            gbCabecera.Enabled = false;
            gbList.Enabled = false;
            MakeInquiry();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            Historial();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Resaltar();
        }

        private void Resaltar()
        {
            ClickResaltarResultados += 1;
            ResaltarResultados();
        }


        private void ResaltarResultados()
        {

            if ((ClickResaltarResultados % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "AN", string.Empty, true);
                c1.RowBackColor = Color.Salmon;
                c1.CellBackColor = Color.Salmon;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c1);


                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "FN", string.Empty, true);
                c2.RowBackColor = Color.MintCream;
                c2.CellBackColor = Color.MintCream;
                c2.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c2);


                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "C0", string.Empty, true);
                c3.RowBackColor = Color.SandyBrown;
                c3.CellBackColor = Color.SandyBrown;
                c3.RowFont = new Font("Segoe UI", 8, FontStyle.Underline);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c3);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "RE", string.Empty, true);
                c4.RowBackColor = Color.LightYellow;
                c4.CellBackColor = Color.LightYellow;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c4);


                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "EP", string.Empty, true);
                c5.RowBackColor = Color.OldLace;
                c5.CellBackColor = Color.OldLace;
                c5.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c5);

                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "AN", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                c1.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c1);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "FN", string.Empty, true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                c2.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c2);


                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "C0", string.Empty, true);
                c3.RowBackColor = Color.White;
                c3.CellBackColor = Color.White;
                c3.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c3);


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "RE", string.Empty, true);
                c4.RowBackColor = Color.White;
                c4.CellBackColor = Color.White;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c4);


                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "PR", string.Empty, true);
                c5.RowBackColor = Color.White;
                c5.CellBackColor = Color.White;
                c5.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["chEstadoId"].ConditionalFormattingObjectList.Add(c5);


                #endregion
            }
        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
        }



        private void btnGenerarFormatosPDF_Click(object sender, EventArgs e)
        {
            NoImplementado();
        }

        private void btnElegirColumna_Click(object sender, EventArgs e)
        {
            this.dgvRegistros.ShowColumnChooser();

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MakeInquiry();
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            CambiarFechasComboBox();
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            CambiarFechasComboBox();
        }

        private void chkVisualizacionPorDia_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvRegistros_SelectionChanged(object sender, EventArgs e)
        {
            SumarElementosSeleccionadosGrilla(sender);
            #region Seleccion al cambiar cursor() 
            selectedItem = new SAS_ListadoConformacionDeCargaByPeriodoResult();
            selectedItem.Id = 0;
            Id = 0;
            EstadoId = string.Empty;
            btnPendiente.Enabled = false;
            btnProceso.Enabled = false;
            btnFinalizado.Enabled = false;


            try
            {
                #region Selecionar registro()                                                                
                if (dgvRegistros != null && dgvRegistros.Rows.Count > 0)
                {
                    if (dgvRegistros.CurrentRow != null)
                    {
                        if (dgvRegistros.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistros.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                Id = (dgvRegistros.CurrentRow.Cells["chId"].Value != null ? Convert.ToInt32(dgvRegistros.CurrentRow.Cells["chId"].Value.ToString()) : 0);
                                EstadoId = (dgvRegistros.CurrentRow.Cells["chEstadoId"].Value != null ? Convert.ToString(dgvRegistros.CurrentRow.Cells["chEstadoId"].Value.ToString()) : string.Empty);
                                var resultado = listAll.Where(x => x.Id == Id).ToList();
                                if (resultado.ToList().Count > 0)
                                {
                                    selectedItem = resultado.ElementAt(0);
                                    if (selectedItem.EstadoId == "PE")
                                    {
                                        btnPendiente.Enabled = false;
                                        btnProceso.Enabled = true;
                                        btnFinalizado.Enabled = false;
                                    }
                                    else if (selectedItem.EstadoId == "EP")
                                    {
                                        btnPendiente.Enabled = true;
                                        btnProceso.Enabled = false;
                                        btnFinalizado.Enabled = true;
                                    }
                                    else if (selectedItem.EstadoId == "FN")
                                    {
                                        btnPendiente.Enabled = false;
                                        btnProceso.Enabled = true;
                                        btnFinalizado.Enabled = false;
                                    }
                                }

                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + "\n Error al cargar los datos en el contenedor del formulario", "Mensaje del sistems");
                return;
            }
            #endregion
        }


        public void SumarElementosSeleccionadosGrilla(object senderGrilla)
        {
            try
            {
                if (((RadGridView)senderGrilla).CurrentRow != null && ((RadGridView)senderGrilla).CurrentCell != null)
                {
                    int fila = ((RadGridView)senderGrilla).CurrentRow.Index;
                    int columna = ((RadGridView)senderGrilla).CurrentCell.ColumnIndex;

                    decimal SumaSeleccionada = 0;
                    decimal promedioSeleccionado = 0;
                    int recuento = 0;

                    //foreach (DataGridViewCell celda in ((DataGridView)senderGrilla).SelectedCells)
                    foreach (GridViewCellInfo celda in ((RadGridView)senderGrilla).SelectedCells)
                    {
                        if (celda.Value != null)
                        {
                            string tipoDato = celda.Value.GetType().Name.ToString();
                            if (tipoDato != null && tipoDato != string.Empty)
                            {
                                #region
                                if (tipoDato == "Double" || tipoDato == "Decimal")
                                {
                                    SumaSeleccionada += Convert.ToDecimal(celda.Value != null ? celda.Value : 0);
                                    if (Convert.ToDecimal(celda.Value != null ? celda.Value : 0) == 0)
                                    {

                                    }
                                    else
                                    {
                                        recuento++;
                                    }

                                    promedioSeleccionado = (SumaSeleccionada / recuento);
                                }
                                else
                                {
                                    SumaSeleccionada = 0;
                                    recuento = 0;
                                    promedioSeleccionado = 0;
                                    break;
                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                SumaSeleccionada = 0;
                                recuento = 0;
                                promedioSeleccionado = 0;
                                break;
                                #endregion
                            }
                            this.lblSumaResultado.Text = SumaSeleccionada.ToDecimalPresentation();
                            this.lblRecuentoNumero.Text = recuento.ToString();
                            this.lblPromedioValor.Text = promedioSeleccionado.ToDecimalPresentation();
                        }


                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConformacionDeCarga_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
            ResaltarResultados();
        }



        private void ConformacionDeCarga_Load(object sender, EventArgs e)
        {

        }
        
        protected override void OnLoad(EventArgs e)
        {
            this.dgvRegistros.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvRegistros.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvRegistros.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistros.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistros.GroupDescriptors.Clear();
            this.dgvRegistros.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chDescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvRegistros.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void Nuevo()
        {

            try
            {
                Id = 0;
                ConformacionDeCargaDetalle oFron = new ConformacionDeCargaDetalle(conection, user, companyId, privilege, Id);
                oFron.MdiParent = ConformacionDeCarga.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvRegistros.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }

        }

        private void NoImplementado()
        {
            RadMessageBox.SetThemeName(dgvRegistros.ThemeName);
            RadMessageBox.Show(this, "No tiene privilegios para esta acción", "Respuesta del sistema", MessageBoxButtons.OK, RadMessageIcon.Info);
        }

        private void MakeInquiry()
        {
            if (chkVisualizacionPorDia.Checked == true)
            {
                desde = DateTime.Now.ToPresentationDate();
                hasta = DateTime.Now.ToPresentationDate();
            }
            else
            {
                desde = this.txtFechaDesde.Text;
                hasta = this.txtFechaHasta.Text;
            }

            gbList.Enabled = false;
            gbCabecera.Enabled = false;
            BarraPrincipal.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void Editar()
        {
            if (selectedItem != null)
            {
                if (selectedItem.Id != null)
                {
                    if (selectedItem.Id != 0)
                    {

                        ConformacionDeCargaDetalle oFron = new ConformacionDeCargaDetalle(conection, user, companyId, privilege, Id);
                        oFron.MdiParent = ConformacionDeCarga.ActiveForm;
                        oFron.WindowState = FormWindowState.Maximized;
                        oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        oFron.Show();
                    }
                }
            }
        }

       
        private void Grabar()
        {
            NoImplementado();
        }


        private void EliminarRegistro()
        {
            if (Id > 0 && EstadoId == "PE")
            {
                if (user.IdUsuario.Trim().ToUpper() == "ADMINISTRADOR" || user.IdUsuario.Trim().ToUpper() == "EAURAZO")
                {
                    model = new SAS_CondormidadDeCargaController();
                    int resultadoAccion = model.ToDelete(conection, Id);
                    MakeInquiry();
                }


            }
        }

        private void Atras()
        {
            NoImplementado();
        }

        private void Anular()
        {
            if (Id > 0 && (EstadoId == "PE" || EstadoId == "AN"))
            {
                model = new SAS_CondormidadDeCargaController();
                int resultadoAccion = model.ToChangeStatus(conection, Id);
                MakeInquiry();
            }
        }


        private void Historial()
        {
            NoImplementado();
        }

        private void ElegirColumna()
        {
            this.dgvRegistros.ShowColumnChooser();
        }


        private void Exportar()
        {
            try
            {
                modelExportToExcel = new ExportToExcelHelper();
                modelExportToExcel.ExportarToExcel(dgvRegistros, saveFileDialog);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void CambiarFechasComboBox()
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void CargarMeses()
        {

            MesesNeg = new MesController();
            cboMes.DisplayMember = "descripcion";
            cboMes.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboMes.DataSource = MesesNeg.ListarMeses().ToList();
            cboMes.SelectedValue = DateTime.Now.ToString("MM");
        }

        private void EjecutarConsulta()
        {

            listAll = new List<SAS_ListadoConformacionDeCargaByPeriodoResult>();
            model = new SAS_CondormidadDeCargaController();

            try
            {
                listAll = model.List(conection, desde, hasta);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void MostrarResultados()
        {
            try
            {
                dgvRegistros.DataSource = listAll.ToDataTable<SAS_ListadoConformacionDeCargaByPeriodoResult>();
                dgvRegistros.Refresh();
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                BarraPrincipal.Enabled = !false;
                progressBar1.Visible = !true;
                gbCabecera.Enabled = true;
                gbList.Enabled = true;
                return;
            }
        }

        private void dgvRegistros_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vistaPreviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //VistaPrevia(IdEvaluacion);
        }

        private void VistaPrevia(int Id)
        {
            if (Id != 0)
            {
                ConformacionDeCargaVistaPrevia ofrm = new ConformacionDeCargaVistaPrevia(conection, Id);
                ofrm.ShowDialog();
            }
        }

        private void btnAprobacionEvaluacionSub_Click(object sender, EventArgs e)
        {
            NoImplementado();
        }



        private void btnAprobacionDistribucionSub_Click(object sender, EventArgs e)
        {

            NoImplementado();

        }



        private void btnAprobacionRevisionSub_Click(object sender, EventArgs e)
        {
            NoImplementado();
        }



        private void elminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void btnActivarFiltro_Click(object sender, EventArgs e)
        {
            ParImparFiltro += 1;
            ActivarFiltro();
        }



        private void ActivarFiltro()
        {
            if ((ParImparFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistros.EnableFiltering = !true;
                dgvRegistros.ShowHeaderCellButtons = !true;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistros.EnableFiltering = true;
                dgvRegistros.ShowHeaderCellButtons = true;
                #endregion
            }
        }

        private void Adjuntar()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void CambiarEstadoDocumen()
        {
            MessageBox.Show("No tiene permisos para realizar esta accion", "MENSAJE DEL SISTEMA");
        }

        private void chkVisualizacionPorDia_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkVisualizacionPorDia.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                if (cboMes.SelectedIndex >= 0)
                {
                    globalHelper = new GlobalesHelper();
                    globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
                }
            }
        }

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);

            if (chkVisualizacionPorDia.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }


            //this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
        }

        private void pendienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CambiarEstado("PE");
        }

        private void enProcesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("EP");
        }

        private void finalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarEstado("FN");
        }

        private void bgwCambiarEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarActualizacion();
        }

        private void EjecutarActualizacion()
        {
            listAll = new List<SAS_ListadoConformacionDeCargaByPeriodoResult>();
            model = new SAS_CondormidadDeCargaController();

            try
            {
                int resultado = model.ActualizarEstado(conection, Id, EstadoId);
                listAll = model.List(conection, desde, hasta);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void CambiarEstado(string _EstadoId)
        {
            EstadoId = _EstadoId;
            if (Id > 0)
            {
                gbList.Enabled = false;
                gbCabecera.Enabled = false;
                BarraPrincipal.Enabled = false;
                progressBar1.Visible = true;
                bgwCambiarEstado.RunWorkerAsync();
            }



        }

        private void bgwCambiarEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
            ResaltarResultados();
        }

        private void anularToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Anular();
        }

        private void elminarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void vistaPreviaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                VistaPrevia(Id);
            }            
        }

        private void btnImprimirSub_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void Imprimir()
        {

        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            VerDetalle();
        }

        private void VerDetalle()
        {

        }

        private void btnCancelarReserva_Click(object sender, EventArgs e)
        {

            CancelarReserva();



        }

        private void CancelarReserva()
        {
            if (EstadoId != string.Empty)
            {
                if (EstadoId != "PE")
                {
                    CambiarEstado("C0");
                }
            }
        }

        private void btnRechazarCarga_Click(object sender, EventArgs e)
        {
            RechazarCarga();
        }

        private void RechazarCarga()
        {

            if (EstadoId != string.Empty)
            {
                if (EstadoId != "PE")
                {
                    CambiarEstado("RE");
                }
            }
        }

        private void dgvRegistros_DoubleClick(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                Editar();
            }
        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
            {
                #region Par() | Activar Filtro()
                dgvRegistros.EnableFiltering = !true;
                dgvRegistros.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {
                #region Par() | DesActivar Filtro()
                dgvRegistros.EnableFiltering = true;
                dgvRegistros.ShowHeaderCellButtons = true;
                #endregion
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

        


    }
}
