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
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using Telerik.WinControls.Data;
using System.Threading;
using Telerik.WinControls.UI.Localization;
using ComparativoHorasVisualSATNISIRA.T.I.ReportesEntregaDevolucion;
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class SolicitudDeEquipamientoTecnologico : Form
    {

        string nombreformulario = "EquipamientoTecnologico";
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result> listado;
        private SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result itemSeleccionado = new SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result();
        private SAS_SolicitudDeEquipamientoTecnologicoController Modelo;
        private SAS_SolicitudDeEquipamientoTecnologico solicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDatesResult> listado1;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListado> listado2;

        public MesController MesesNeg;
        public string FechaDesdeConsulta;
        public string FechaHastaConsulta;
        private string tipoSolicitudDeEquipamiento = string.Empty;
        private int cerradoAReferencias;
        private SAS_SolicitudDeEquipamientoTecnologicoController modeloEnvioNotificaciones;
        private int codigoSelecionado;

        public SolicitudDeEquipamientoTecnologico()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = Environment.UserName;
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.anular = 1;
            privilege.consultar = 1;
            privilege.eliminar = 1;
            privilege.editar = 1;
            privilege.exportar = 1;
            privilege.nuevo = 1;

            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            FechaDesdeConsulta = oPrimerDiaDelMes.ToShortDateString();
            FechaHastaConsulta = oUltimoDiaDelMes.ToShortDateString();


            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();



        }


        public SolicitudDeEquipamientoTecnologico(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();

        }


        public SolicitudDeEquipamientoTecnologico(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _idSolicitud)
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
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();

        }


        private void Actualizar()
        {
            try
            {
                //btnMenu.Enabled = true;
                //gbEdit.Enabled = true;
                //gbList.Enabled = true;
                FechaDesdeConsulta = this.txtFechaDesde.Text.Trim();
                FechaHastaConsulta = this.txtFechaHasta.Text.Trim();

                btnActualizarLista.Enabled = false;
                btnConsultar.Enabled = false;
                progressBar1.Visible = false;
                bgwHilo.RunWorkerAsync();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
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

        private void ObtenerFechasIniciales()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);

            this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
            this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);
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
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();

                base.OnLoad(e);
                SetConditions();
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

            try
            {
                this.dgvListado.MasterTemplate.AutoExpandGroups = true;
                this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                this.dgvListado.GroupDescriptors.Clear();
                this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
                GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
                items1.Add(new GridViewSummaryItem("chnombresCompletos", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            //catch (TargetInvocationException ex)
            //{
            //    result = ex.InnerException.Message;
            //}
            //catch (Exception ex)
            //{
            //    result = ex.Message;
            //}
            catch (FilterExpressionException ex)
            {
                //FilterDescriptor wrongFilter = this.dgvListado.Columns["chcuenta"].FilterDescriptor;

                //FilterDescriptor filterDescriptor =
                //    new FilterDescriptor(wrongFilter.PropertyName, wrongFilter.Operator, correctValue);
                //filterDescriptor.IsFilterEditor = wrongFilter.IsFilterEditor;

                //this.dgvListado.FilterDescriptors.Remove(wrongFilter);
                //this.dgvListado.FilterDescriptors.Add(filterDescriptor);

                MessageBox.Show(ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }

        }


        private void SetConditions()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "ANULADO", "", true);
            c1.RowBackColor = Color.Peru;
            c1.CellBackColor = Color.Peru;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "APROBADO", "", true);
            c2.RowBackColor = Utiles.colorVerdeClaro;
            c2.CellBackColor = Utiles.colorVerdeClaro;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c2);

            ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "PENDIENTE", "", true);
            c3.RowBackColor = Color.Transparent;
            c3.CellBackColor = Color.Transparent;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c3);


            ConditionalFormattingObject c4 = new ConditionalFormattingObject("Cerrado applied to entire row", ConditionTypes.Equal, "1", "", true);
            c4.RowForeColor = Color.Black;
            c4.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvListado.Columns["chCerrado"].ConditionalFormattingObjectList.Add(c4);

            //update the grid view for the conditional formatting to take effect            
            //radGridView1.TableElement.Update(false);        
        }

        private void SetUnConditions()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Green, applied to entire row", ConditionTypes.Equal, "ANULADO", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "APROBADO", "", true);
            c2.RowBackColor = Color.White;
            c2.CellBackColor = Color.White;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c2);

            ConditionalFormattingObject c3 = new ConditionalFormattingObject("Yellow, applied to entire row", ConditionTypes.Equal, "PENDIENTE", "", true);
            c3.RowBackColor = Color.White;
            c3.CellBackColor = Color.White;
            dgvListado.Columns["chestado"].ConditionalFormattingObjectList.Add(c3);


            ConditionalFormattingObject c4 = new ConditionalFormattingObject("White,  applied to entire row", ConditionTypes.Equal, "1", "", true);
            c4.RowForeColor = Color.Transparent;
            dgvListado.Columns["chCerrado"].ConditionalFormattingObjectList.Add(c4);
        }

        private void SolicitudDeEquipamientoTecnologico_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                listado1 = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDatesResult>();
                listado2 = new List<SAS_SolicitudDeEquipamientoTecnologicoListado>();

                Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                listado = Modelo.ListRequestsByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);
                //listado = Modelo.ListRequests("SAS");
                //listado2 = Modelo.ListRequestsByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);




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
                dgvListado.DataSource = listado.ToList().ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                dgvListado.Refresh();


                //dgvListado.DataSource = listado2.ToList().ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoListado>();
                //dgvListado.Refresh();


                progressBar1.Visible = false;
                btnActualizarLista.Enabled = !false;
                btnConsultar.Enabled = !false;

            }
            catch (Exception ex)
            {
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }


        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {

            solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
            solicitud.id = 0;
            SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(conection, user2, companyId, privilege, solicitud);
            ofrm.MdiParent = SolicitudDeEquipamientoTecnologico.ActiveForm;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            ofrm.Show();


        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
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

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla1)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla1);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla1.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
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

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {

            btnDActivarSolicitud.Enabled = false;
            btnDAsociarColaboradorAAreaDeTrabajo.Enabled = false;
            btnDDesactivarSolicitud.Enabled = false;
            btnDEditarRegistro.Enabled = false;
            btnDImprimirSolicitud.Enabled = false;
            btnDVerDatosGeneralesDeColaborador.Enabled = false;
            btnDVistaPreviaActaDeAlta.Enabled = false;
            btnDVistaPreviaActaDeAltaAnexo.Enabled = false;
            btnDVistaPreviaActaDeBaja.Enabled = false;
            btnDVistaPreviaDeSolicitud.Enabled = false;
            btnDVistaPreviaFormatoDeCapacitacion.Enabled = false;
            // add 26.05.2023
            btnDLiberarSolicitud.Enabled = false;
            btnModificarSolicitudAPartirDeUnaAlta.Enabled = false;
            btnBajaParcialDeSolicitudDeAlta.Enabled = false;
            btnAsociarReferenciasEntreSolicitudes.Enabled = false;
            btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = false;
            btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = false;
            // 27/05/2023
            btnVerDocumentosAdjuntos.Enabled = false;
            btnEnviarNotificacion.Enabled = false;
            btnAprobarSolicitud.Enabled = false;
            btnGenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento.Enabled = false;
            btnVerSolicitudDeRenovacionDeLineasCelulares.Enabled = false;


            try
            {
                #region 
                itemSeleccionado = new SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result();
                itemSeleccionado.idCodigoGeneral = string.Empty;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                string id = (dgvListado.CurrentRow.Cells["chid"].Value != null ? dgvListado.CurrentRow.Cells["chid"].Value.ToString() : string.Empty);
                                string codigo = (dgvListado.CurrentRow.Cells["chidCodigoGeneral"].Value != null ? dgvListado.CurrentRow.Cells["chidCodigoGeneral"].Value.ToString() : string.Empty);
                                string nombres = (dgvListado.CurrentRow.Cells["chnombresCompletos"].Value != null ? dgvListado.CurrentRow.Cells["chnombresCompletos"].Value.ToString() : string.Empty);
                                string estadoSolicitudAnterior = (dgvListado.CurrentRow.Cells["chestadoCodigoAnterior"].Value != null ? dgvListado.CurrentRow.Cells["chestadoCodigoAnterior"].Value.ToString() : string.Empty);
                                int? codigoSolicitudRenovacionReferencia = ((dgvListado.CurrentRow.Cells["chidReferenciaSolicitudRenovacion"].Value != null && dgvListado.CurrentRow.Cells["chidReferenciaSolicitudRenovacion"].Value.ToString() != string.Empty) ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chidReferenciaSolicitudRenovacion"].Value) : (int?)null);





                                tipoSolicitudDeEquipamiento = (dgvListado.CurrentRow.Cells["chtipoSolicitud"].Value != null ? dgvListado.CurrentRow.Cells["chtipoSolicitud"].Value.ToString() : string.Empty);
                                cerradoAReferencias = (dgvListado.CurrentRow.Cells["chCerrado"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chCerrado"].Value.ToString()) : 0);

                                var resultado = listado.Where(x => x.id.ToString() == id).ToList();
                                if (resultado.ToList().Count >= 1)
                                {
                                    #region 
                                    btnDVerDatosGeneralesDeColaborador.Enabled = true;
                                    btnDAsociarColaboradorAAreaDeTrabajo.Enabled = true;

                                    itemSeleccionado = resultado.ElementAt(0);
                                    itemSeleccionado.idCodigoGeneral = codigo;
                                    itemSeleccionado.nombresCompletos = nombres;

                                    if (itemSeleccionado.estadoCodigo == "PE")
                                    {
                                        #region Estado pendiente() 
                                        btnDVistaPreviaDeSolicitud.Enabled = false;
                                        btnDEditarRegistro.Enabled = true;
                                        btnDDesactivarSolicitud.Enabled = true;
                                        btnDLiberarSolicitud.Enabled = false;
                                        btnDActivarSolicitud.Enabled = false;

                                        btnVerDocumentosAdjuntos.Enabled = true;
                                        btnEnviarNotificacion.Enabled = false;

                                        if (cerradoAReferencias == 1)
                                        {
                                            btnDLiberarSolicitud.Enabled = true;
                                        }

                                        if (estadoSolicitudAnterior != string.Empty)
                                        {
                                            if (user2.IdUsuario != null)
                                            {
                                                if ((user2.IdUsuario != "EAURAZO") || (user2.IdUsuario != "ADMINISTRADOR") || (user2.IdUsuario != "FCERNA"))
                                                {
                                                    btnAprobarSolicitud.Enabled = true;
                                                }
                                            }

                                        }


                                        #endregion
                                    }
                                    else if (itemSeleccionado.estadoCodigo == "AN")
                                    {
                                        #region Estado Anulado() 
                                        btnDVistaPreviaDeSolicitud.Enabled = false;
                                        btnDEditarRegistro.Enabled = false;
                                        btnDDesactivarSolicitud.Enabled = true;
                                        btnDLiberarSolicitud.Enabled = false;

                                        btnVerDocumentosAdjuntos.Enabled = false;
                                        btnEnviarNotificacion.Enabled = false;

                                        if (user2.IdUsuario.Trim() == "EAURAZO" || user2.IdUsuario.Trim() == "ADMINISTRADOR" || user2.IdUsuario.Trim() == "FCERNA")
                                        {
                                            btnDActivarSolicitud.Enabled = true;
                                        }

                                        if (cerradoAReferencias == 1)
                                        {
                                            btnDLiberarSolicitud.Enabled = true;
                                        }
                                        #endregion
                                    }
                                    else if (itemSeleccionado.estadoCodigo == "AP")
                                    {
                                        #region Estado Aprobado() 
                                        btnDActivarSolicitud.Enabled = true;
                                        btnDEditarRegistro.Enabled = false;
                                        btnDDesactivarSolicitud.Enabled = true;
                                        btnDVistaPreviaDeSolicitud.Enabled = true;
                                        btnVerDocumentosAdjuntos.Enabled = true;
                                        btnEnviarNotificacion.Enabled = true;

                                        if (user2.IdUsuario != null)
                                        {
                                            if (user2.IdUsuario.Trim() == "EAURAZO" || user2.IdUsuario.Trim() == "ADMINISTRADOR" || user2.IdUsuario.Trim() == "FCERNA")
                                            {

                                                if (cerradoAReferencias == 1)
                                                {
                                                    btnDLiberarSolicitud.Enabled = true;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Otros estados()
                                        btnVerDocumentosAdjuntos.Enabled = true;
                                        btnEnviarNotificacion.Enabled = false;
                                        btnDVistaPreviaDeSolicitud.Enabled = false;
                                        btnDEditarRegistro.Enabled = false;
                                        btnDDesactivarSolicitud.Enabled = false;
                                        btnDLiberarSolicitud.Enabled = false;
                                        btnDActivarSolicitud.Enabled = false;
                                        #endregion
                                    }


                                    if (tipoSolicitudDeEquipamiento.ToUpper().Trim() == "ALTA")
                                    {
                                        #region MyRegion


                                        //if (cerradoAReferencias == 0)
                                        //{
                                        btnModificarSolicitudAPartirDeUnaAlta.Enabled = true;
                                        btnBajaParcialDeSolicitudDeAlta.Enabled = true;
                                        btnAsociarReferenciasEntreSolicitudes.Enabled = true;
                                        btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = true;
                                        btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = false;
                                        //}
                                        //else
                                        //{
                                        //    btnModificarSolicitudAPartirDeUnaAlta.Enabled = false;
                                        //    btnBajaParcialDeSolicitudDeAlta.Enabled = true;
                                        //    btnAsociarReferenciasEntreSolicitudes.Enabled = true;
                                        //    btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = true;
                                        //    btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = false;
                                        //}


                                        if (itemSeleccionado.estadoCodigo == "AP")
                                        {
                                            btnGenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento.Enabled = true;
                                        }

                                        #endregion
                                    }
                                    else if (tipoSolicitudDeEquipamiento.ToUpper().Trim() == "BAJA")
                                    {
                                        #region
                                        //if (cerradoAReferencias == 0)
                                        //{
                                        btnModificarSolicitudAPartirDeUnaAlta.Enabled = false;
                                        btnBajaParcialDeSolicitudDeAlta.Enabled = false;
                                        btnAsociarReferenciasEntreSolicitudes.Enabled = true;
                                        btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = false;
                                        btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = true;
                                        //}
                                        //else
                                        //{
                                        //    btnModificarSolicitudAPartirDeUnaAlta.Enabled = false;
                                        //    btnBajaParcialDeSolicitudDeAlta.Enabled = false;
                                        //    btnAsociarReferenciasEntreSolicitudes.Enabled = true;
                                        //    btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = false;
                                        //    btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = true;
                                        //}

                                        if (itemSeleccionado.estadoCodigo == "AP")
                                        {
                                            btnGenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento.Enabled = true;
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        #region 
                                        btnModificarSolicitudAPartirDeUnaAlta.Enabled = false;
                                        btnBajaParcialDeSolicitudDeAlta.Enabled = false;
                                        btnAsociarReferenciasEntreSolicitudes.Enabled = false;
                                        btnGenerarSolicitudDeBajaAPartirDeEsteDocumento.Enabled = false;
                                        btnGenerarSolicitudDeAltaAPartirDeEsteDocumento.Enabled = false;
                                        #endregion
                                    }
                                    #endregion


                                    if (codigoSolicitudRenovacionReferencia != null && codigoSolicitudRenovacionReferencia.ToString() != string.Empty)
                                    {
                                        btnVerSolicitudDeRenovacionDeLineasCelulares.Enabled = true;
                                    }

                                }
                                else
                                {
                                    itemSeleccionado.idCodigoGeneral = string.Empty;
                                }




                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {
            AsociarAreaDeTrabajo();
        }

        private void AsociarAreaDeTrabajo()
        {
            try
            {
                if (itemSeleccionado.idCodigoGeneral != string.Empty)
                {
                    SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oColaboradorParaAsociar = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
                    oColaboradorParaAsociar.idcodigoGeneral = itemSeleccionado.idCodigoGeneral;
                    oColaboradorParaAsociar.nombresCompletos = itemSeleccionado.nombresCompletos;

                    ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(conection, user2, companyId, privilege, oColaboradorParaAsociar);
                    ofrm.Show();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void EditarRegistro()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(conection, user2, companyId, privilege, solicitud);
                        ofrm.MdiParent = SolicitudDeEquipamientoTecnologico.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();

                    }
                }
            }
        }

        private void chkMostarAgrupado_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.RowCount > 0)
                {
                    if (chkMostarAgrupado.Checked == true)
                    {
                        SetConditions();
                    }
                    else
                    {
                        SetUnConditions();
                    }
                }
            }
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {

            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex >= 0)
            {
                globalHelper = new GlobalesHelper();
                globalHelper.ObtenerFechasMes(cboMes, txtFechaDesde, txtFechaHasta, txtPeriodo);
            }
        }

        private void btnVerDatosGeneralesDeColaborador_Click(object sender, EventArgs e)
        {
            GoToWorkerCatalog();
        }

        private void GoToWorkerCatalog()
        {
            #region Ir a catalogo de colaboradores con el filtro del idcolaborador() 
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.idCodigoGeneral != null)
                {
                    if (itemSeleccionado.idCodigoGeneral.ToString().Trim() != string.Empty)
                    {
                        string codigoColaboradorFiltrado = itemSeleccionado.idCodigoGeneral.ToString().Trim();
                        ColaboradoresListado ofrm = new ColaboradoresListado(conection, user2, companyId, privilege, codigoColaboradorFiltrado);
                        ofrm.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();
                    }
                }
            }
            #endregion
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void vistaPreviaDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDEditarRegistro_Click(object sender, EventArgs e)
        {
            EditarRegistro();
        }

        private void btnDActivarSolicitud_Click(object sender, EventArgs e)
        {
            ActivarSolicitud();
        }

        private void ActivarSolicitud()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                        Modelo.ActivarSolicitud(solicitud, conection != null ? conection : "SAS");
                        Actualizar();
                    }
                }
            }
        }

        private void btnDDesactivarSolicitud_Click(object sender, EventArgs e)
        {
            DesactivarSolicitud();
        }

        private void DesactivarSolicitud()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                        Modelo.DesactivarSolicitud(solicitud, conection != null ? conection : "SAS");
                        Actualizar();
                    }
                }
            }
        }

        private void btnDVistaPreviaDeSolicitud_Click(object sender, EventArgs e)
        {
            VistaPreviaDeSolicitud();
        }

        private void VistaPreviaDeSolicitud()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.idTipoSolicitud.ToString().Trim() == "1" || itemSeleccionado.idTipoSolicitud.ToString().Trim() == "10" || itemSeleccionado.idTipoSolicitud.ToString().Trim() == "8") //1   Alta
                    {
                        VistaPreviaSolicitudEquipamiento ofrm = new VistaPreviaSolicitudEquipamiento(Convert.ToInt32((itemSeleccionado.id.ToString().Trim())), "ALTA");
                        ofrm.ShowDialog();
                    }
                    else if (itemSeleccionado.idTipoSolicitud.ToString().Trim() == "3" || itemSeleccionado.idTipoSolicitud.ToString().Trim() == "11" || itemSeleccionado.idTipoSolicitud.ToString().Trim() == "6") //3   Baja
                    {
                        VistaPreviaSolicitudEquipamiento ofrm = new VistaPreviaSolicitudEquipamiento(Convert.ToInt32((itemSeleccionado.id.ToString().Trim())), "BAJA");
                        ofrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se tiene configurado un formato para esta categoria de equipamiento", "MENSAJE DEL SISTEMA");
                    }

                    // id descripcion
                    //0-- Selecionar--
                    //1   Alta -- /////// ALTA /////// 
                    //2   Modificación
                    //3   Baja -- ****BAJA
                    //4   Renovación
                    //5   Línea Nueva 
                    //6   Suspención -- ***BAJA
                    //7   Activación
                    //8   Prestamo -- /////// ALTA /////// 
                    //9   Duplicado
                    //10  Prestamo equipo -- /////// ALTA /////// 
                    //11  Devolucion -- **** BAJA



                }
            }
        }

        private void btnDImprimirSolicitud_Click(object sender, EventArgs e)
        {

        }

        private void btnDVistaPreviaActaDeAltaAnexo_Click(object sender, EventArgs e)
        {

        }

        private void btnDVistaPreviaActaDeBaja_Click(object sender, EventArgs e)
        {

        }

        private void btnDVistaPreviaFormatoDeCapacitacion_Click(object sender, EventArgs e)
        {

        }

        private void btnVerSolicitudDeRenovacionDeLineasCelulares_Click(object sender, EventArgs e)
        {
            VerSolicitudDeRenovacionDeLineasCelulares();
        }


        private void VerSolicitudDeRenovacionDeLineasCelulares()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacionCelularReferencia = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacionCelularReferencia.id = itemSeleccionado.id;
                        SolicitudDeRenovaciónDeEquipoCelularDetalle ofrm = new SolicitudDeRenovaciónDeEquipoCelularDetalle(conection, user2, companyId, privilege, solicitudRenovacionCelularReferencia);
                        ofrm.MdiParent = SolicitudDeEquipamientoTecnologico.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();

                    }
                }
            }
        }

        private void btnGenerarSolicitudDeAltaAPartirDeEsteDocumento_Click(object sender, EventArgs e)
        {
            int idRequest = itemSeleccionado.id != null ? itemSeleccionado.id : 0;
            if (idRequest > 0)
            {
                GenerarSolicitudDeAltaAPartirDeEsteDocumento(idRequest);
            }

        }



        private void btnGenerarSolicitudDeBajaAPartirDeEsteDocumento_Click(object sender, EventArgs e)
        {
            int idRequest = solicitud.id;
            if (idRequest > 0)
            {
                GenerarSolicitudDeBajaAPartirDeEsteDocumento(idRequest);
            }

        }

        private void btnAsociarReferenciasEntreSolicitudes_Click(object sender, EventArgs e)
        {

            AsociarReferenciasEntreSolicitudes(0);
        }

        private void AsociarReferenciasEntreSolicitudes(int idDocumentoOriginal)
        {

        }
        private void GenerarSolicitudDeAltaAPartirDeEsteDocumento(int idMovimientoSolicitud)
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = idMovimientoSolicitud;
                        gbCabecera.Enabled = false;
                        gbListado.Enabled = false;
                        progressBar1.Visible = true;
                        BarraPrincipal.Enabled = !false;
                        bgwGenerarSolicitudAltaDesdeBaja.RunWorkerAsync();
                        

                    }
                }
            }
        }

        private void GenerarSolicitudDeBajaAPartirDeEsteDocumento(int idMovimientoSolicitud)
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        gbCabecera.Enabled = false;
                        gbListado.Enabled = false;
                        progressBar1.Visible = true;
                        BarraPrincipal.Enabled = !false;
                        bgwGenerarSolicitudBajaDesdeAlta.RunWorkerAsync();
                        MessageBox.Show("Opercion realizada correctamente", "Confirmación del sistema");
                    }
                }
            }
        }

        private void modificarSolicitudAPartirDeUnaAltaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnDLiberarSolicitud_Click(object sender, EventArgs e)
        {
            LiberarSolicitud();
        }

        public void LiberarSolicitud()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                        Modelo.LiberarSolicitud(solicitud, conection != null ? conection : "SAS");
                        Actualizar();
                    }
                }
            }
        }

        private void btnVerDocumentosAdjuntos_Click(object sender, EventArgs e)
        {
            VerDocumentosAdjuntos();
        }


        private void VerDocumentosAdjuntos()
        {
            try
            {
                #region Attach()
                if (itemSeleccionado != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        int codigoSelecionado = itemSeleccionado.id;

                        AdjuntarArchivos ofrm = new AdjuntarArchivos(conection, user2, companyId, privilege, codigoSelecionado.ToString(), nombreformulario);
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

        private void bgwNotify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modeloEnvioNotificaciones = new SAS_SolicitudDeEquipamientoTecnologicoController();
                modeloEnvioNotificaciones.Notify(conection, "soporte@saturno.net.pe", "Solicitud de Equipamiento tecnológico", codigoSelecionado);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwNotify_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Notificación enviada satisfactoriamente", "Confirmación del sistema");
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = true;
                return;
            }
        }

        private void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            try
            {
                #region Notify() 
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id > 0)
                    {
                        codigoSelecionado = itemSeleccionado.id;
                        gbCabecera.Enabled = false;
                        gbListado.Enabled = false;
                        progressBar1.Visible = true;
                        BarraPrincipal.Enabled = false;
                        bgwNotify.RunWorkerAsync();
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

        private void btnAprobarSolicitud_Click(object sender, EventArgs e)
        {
            AprobarSolicitud();
        }

        private void AprobarSolicitud()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                        Modelo.AprobarSolicitud(solicitud, conection != null ? conection : "SAS");
                        Actualizar();
                    }
                }
            }
        }

        private void btnGenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento_Click(object sender, EventArgs e)
        {
            GenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento();
        }

        private void GenerarSolicitudDeAltaParaOtroPersonalAPartirDeEsteDocumento()
        {
            try
            {
                #region Generar solicitud()

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwGenerarSolicitudAltaDesdeBaja_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                Modelo.GenerarSolicitudDeBajaoAltaAPartirDeEsteDocumento(solicitud, conection != null ? conection : "SAS", user2, 0);
                listado = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                listado = Modelo.ListRequestsByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }

        }

        private void bgwGenerarSolicitudBajaDesdeAlta_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                Modelo.GenerarSolicitudDeBajaoAltaAPartirDeEsteDocumento(solicitud, conection != null ? conection : "SAS", user2, 1);

                listado = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                listado = Modelo.ListRequestsByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }

            
        }

        private void bgwGenerarSolicitudAltaDesdeBaja_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.ToList().ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                dgvListado.Refresh();

                gbCabecera.Enabled = !false;
                gbListado.Enabled = true;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
                MessageBox.Show("Opercion realizada correctamente", "Confirmación del sistema | Solicitud de alta de equipamiento y accesos");

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
            
        }

        private void bgwGenerarSolicitudBajaDesdeAlta_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvListado.DataSource = listado.ToList().ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
                dgvListado.Refresh();

                gbCabecera.Enabled = !false;
                gbListado.Enabled = true;
                progressBar1.Visible = !true;
                BarraPrincipal.Enabled = !false;
                MessageBox.Show("Opercion realizada correctamente", "Confirmación del sistema | Solicitud de baja de equipamiento y accesos");

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }
    }
}
