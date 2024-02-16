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
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class SolicitudDeRenovaciónDeEquipoCelular : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado;
        private SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult itemSeleccionado = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult();
        private SAS_SolicitudDeRenovacionTelefoniaCelularController Modelo;
        private SAS_SolicitudDeRenovacionTelefoniaCelular solicitud;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado1;
        private List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> listado2;

        public MesController MesesNeg;
        public string FechaDesdeConsulta;
        public string FechaHastaConsulta;
        private int ClickFiltro = 1;
        private int ClickResaltarResultados = 1;

        public SolicitudDeRenovaciónDeEquipoCelular()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.AREA = "TI";
            user2.email = "EAURAZO@SATURNO.NET.PE";
            user2.EmpresaID = "001";
            user2.IdCodigoGeneral = "100369";
            user2.idestado = "PE";
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            user2.SUCURSAL = "001";

            companyId = "001";
            this.privilege = new PrivilegesByUser();

            lblCodeUser.Text = user2.IdUsuario != null ? user2.IdUsuario : Environment.UserName.ToString();
            lblFullName.Text = user2.NombreCompleto != null ? user2.NombreCompleto : Environment.MachineName.ToString();
            CargarMeses();
            ObtenerFechasIniciales();
            Actualizar();
        }

        public SolicitudDeRenovaciónDeEquipoCelular(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
                items1.Add(new GridViewSummaryItem("chnombres", "Count : {0:N2}; ", GridAggregateFunction.Count));
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

        private void SolicitudDeRenovaciónDeEquipoCelular_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {


                listado = new List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult>();
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                listado = Modelo.ListRequestsByDate("SAS", FechaDesdeConsulta, FechaHastaConsulta);
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
                dgvListado.DataSource = listado.ToList().ToDataTable<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult>();
                dgvListado.Refresh();
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

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            Editar();
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnActualizarEstado.Enabled = false;
            btnDesactivarSolicitud.Enabled = false;
            btnVistaPreviaSolicitudAltaAnexo.Enabled = false;
            btnAsociarAreaDeTrabajo.Enabled = false;
            btnVerDispositivoBaja.Enabled = false;
            btnVerLineaCelular.Enabled = false;
            btnVerSolicitudReferencia.Enabled = false;
            btnEditarRegistro.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnVerDatosGeneralesDelColaborador.Enabled = false;
            btnVerSolicitudDeAlta.Enabled = false;
            btnVerDispositivoAlta.Enabled = false;
            btnVerSolicitudDeBaja.Enabled = false;
            btnAprobarSolicitud.Enabled = false;
            btnRechazarSolicitud.Enabled = false;
            btnActivarSolicitud.Enabled = false;
            btnVistaPreviaSolicitudAlta.Enabled = false;
            btnVistaPreviaSolicitudBaja.Enabled = false;
            btnLiberarSolicitud.Enabled = false;
            btnRetornarEstadoASolicitud.Enabled = false;
            btnDVerDocumentosAdjuntos.Enabled = false;
            btnAtendidoParcial.Enabled = false;
            btnAtendidoTotal.Enabled = false;


            try
            {
                #region Cuando se recorre dentro de la grilla detalle()
                itemSeleccionado = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult();
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
                                string nombres = (dgvListado.CurrentRow.Cells["chnombres"].Value != null ? dgvListado.CurrentRow.Cells["chnombres"].Value.ToString() : string.Empty);

                                string tipoDeSolicitud = (dgvListado.CurrentRow.Cells["chmotivoSolicitud"].Value != null ? dgvListado.CurrentRow.Cells["chmotivoSolicitud"].Value.ToString() : string.Empty);


                                if (tipoDeSolicitud.ToUpper().Trim() == "RENOVACION" || tipoDeSolicitud.ToUpper().Trim() == "RENOVACIÓN")
                                {
                                    btnActualizarEstado.Enabled = true;
                                }
                                else
                                {
                                    btnActualizarEstado.Enabled = false;
                                }

                                var resultado = listado.Where(x => x.id.ToString() == id).ToList();
                                #region
                                if (resultado.ToList().Count > 0)
                                {
                                    #region Unico registro()
                                    btnDVerDocumentosAdjuntos.Enabled = true;

                                    itemSeleccionado = resultado.ElementAt(0);
                                    itemSeleccionado.idCodigoGeneral = codigo;
                                    itemSeleccionado.nombres = nombres;

                                    // habilitar dispositivo si tiene un IdDispositivo.
                                    if (itemSeleccionado.idDispositivoBaja != null)
                                    {
                                        if (itemSeleccionado.idDispositivoBaja != 0)
                                        {
                                            btnVerDispositivoBaja.Enabled = true;

                                        }
                                    }

                                    if (itemSeleccionado.idDispositivoAlta != null)
                                    {
                                        if (itemSeleccionado.idDispositivoAlta != 0)
                                        {
                                            btnVerDispositivoAlta.Enabled = true;

                                        }
                                    }

                                    if (itemSeleccionado.idReferenciaAlta != null)
                                    {
                                        if (itemSeleccionado.idReferenciaAlta != 0)
                                        {
                                            btnVerSolicitudDeAlta.Enabled = true;
                                            btnVistaPreviaSolicitudAlta.Enabled = true;
                                            btnVistaPreviaSolicitudAltaAnexo.Enabled = true;

                                        }
                                    }

                                    if (itemSeleccionado.idReferenciaBaja != null)
                                    {
                                        if (itemSeleccionado.idReferenciaBaja != 0)
                                        {
                                            btnVerSolicitudDeBaja.Enabled = true;
                                            btnVistaPreviaSolicitudBaja.Enabled = true;
                                        }
                                    }

                                    // habilitar dispositivo si tiene línea celular.
                                    if (itemSeleccionado.numeroCelular != null)
                                    {
                                        if (itemSeleccionado.numeroCelular != string.Empty)
                                        {
                                            btnVerLineaCelular.Enabled = true;
                                        }
                                    }

                                    // habilitar las opciones de edición y anular.
                                    if (itemSeleccionado.estadoCodigo != null)
                                    {
                                        if (itemSeleccionado.estadoCodigo == "PE")
                                        {
                                            btnRetornarEstadoASolicitud.Enabled = false;
                                            btnEditarRegistro.Enabled = false;
                                            btnDesactivarSolicitud.Enabled = true;
                                            btnAnular.Enabled = false;
                                            btnEliminarRegistro.Enabled = false;
                                            btnRechazarSolicitud.Enabled = true;


                                            if (user2.IdUsuario == "EAURAZO")
                                            {
                                                btnLiberarSolicitud.Enabled = true;

                                                btnAtendidoParcial.Enabled = true;
                                                btnAtendidoTotal.Enabled = true;
                                                btnRechazarSolicitud.Enabled = true;

                                            }
                                        }
                                        else if (itemSeleccionado.estadoCodigo == "AN")
                                        {
                                            btnRetornarEstadoASolicitud.Enabled = false;
                                            btnActivarSolicitud.Enabled = true;
                                            btnAnular.Enabled = true;
                                            btnLiberarSolicitud.Enabled = false;
                                        }
                                        else if (itemSeleccionado.estadoCodigo == "SO")
                                        {
                                            btnEditarRegistro.Enabled = true;
                                            btnDesactivarSolicitud.Enabled = false;
                                            btnAnular.Enabled = true;
                                            btnEliminarRegistro.Enabled = true;
                                            btnAprobarSolicitud.Enabled = true;
                                            btnRechazarSolicitud.Enabled = true;
                                            btnLiberarSolicitud.Enabled = false;
                                            btnRetornarEstadoASolicitud.Enabled = false;                                            
                                            btnAtendidoParcial.Enabled = true;
                                            btnAtendidoTotal.Enabled = true;

                                            if (user2.IdUsuario == "EAURAZO")
                                            {
                                                if (itemSeleccionado.idReferenciaBaja != (int?)null || itemSeleccionado.idReferenciaAlta != (int?)null)
                                                {
                                                    btnRetornarEstadoASolicitud.Enabled = true;

                                                    btnAtendidoParcial.Enabled = true;
                                                    btnAtendidoTotal.Enabled = true;
                                                    btnRechazarSolicitud.Enabled = true;
                                                }
                                            }
                                        }
                                    }

                                    // habilitar documento de referencia (Desde equipamiento tecnologico) 
                                    if (itemSeleccionado.idReferencia != null)
                                    {
                                        if (itemSeleccionado.idReferencia != 0)
                                        {
                                            btnVerSolicitudReferencia.Enabled = true;
                                        }
                                    }

                                    // habilitar opción de asociar área de trabajo
                                    if (itemSeleccionado.idCodigoGeneral != null)
                                    {
                                        if (itemSeleccionado.idCodigoGeneral != string.Empty)
                                        {
                                            btnAsociarAreaDeTrabajo.Enabled = true;
                                            btnVerDatosGeneralesDelColaborador.Enabled = true;
                                        }
                                    }

                                  
                                   


                                    #endregion
                                }
                                else
                                {
                                    itemSeleccionado.idCodigoGeneral = string.Empty;
                                    btnDesactivarSolicitud.Enabled = false;
                                    btnAsociarAreaDeTrabajo.Enabled = false;
                                    btnVerDispositivoBaja.Enabled = false;
                                    btnVerLineaCelular.Enabled = false;
                                    btnVerSolicitudReferencia.Enabled = false;
                                    btnEditarRegistro.Enabled = false;
                                    btnAnular.Enabled = false;
                                    btnEliminarRegistro.Enabled = false;
                                    btnVerDatosGeneralesDelColaborador.Enabled = false;
                                    btnVerSolicitudDeAlta.Enabled = false;
                                    btnVerDispositivoAlta.Enabled = false;
                                    btnVerSolicitudDeBaja.Enabled = false;
                                    btnAprobarSolicitud.Enabled = false;
                                    btnRechazarSolicitud.Enabled = false;
                                    btnActivarSolicitud.Enabled = false;
                                    btnVistaPreviaSolicitudAlta.Enabled = false;
                                    btnVistaPreviaSolicitudBaja.Enabled = false;
                                    btnVistaPreviaSolicitudAltaAnexo.Enabled = false;
                                    btnAtendidoParcial.Enabled = false;
                                    btnAtendidoTotal.Enabled = false;
                                    btnRechazarSolicitud.Enabled = !true;
                                }
                            }
                        }
                    }
                }
                #endregion
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
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

            solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            solicitud.id = 0;
            SolicitudDeRenovaciónDeEquipoCelularDetalle ofrm = new SolicitudDeRenovaciónDeEquipoCelularDetalle(conection, user2, companyId, privilege, solicitud);
            ofrm.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
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
            Editar();
        }

        private void Editar()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitud.id = itemSeleccionado.id;
                        SolicitudDeRenovaciónDeEquipoCelularDetalle ofrm = new SolicitudDeRenovaciónDeEquipoCelularDetalle(conection, user2, companyId, privilege, solicitud);
                        ofrm.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
                        ofrm.WindowState = FormWindowState.Maximized;
                        ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        ofrm.Show();

                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CancelDocument();
        }

        private void CancelDocument()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.estadoCodigo != null)
                {
                    if (itemSeleccionado.estadoCodigo == "PE" || itemSeleccionado.estadoCodigo == "AN")
                    {
                        Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                        Modelo.ChangeState("SAS", itemSeleccionado);
                        Actualizar();
                    }
                }
            }

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            DeleteDocument();
        }

        private void DeleteDocument()
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.estadoCodigo != null)
                {
                    if (itemSeleccionado.estadoCodigo == "PE")
                    {
                        Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                        Modelo.DeleteRecord("SAS", itemSeleccionado);
                        Actualizar();
                    }
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvListado != null)
            {
                if (dgvListado.Rows.Count > 0)
                {
                    Exportar(dgvListado);
                }
                else
                {
                    MessageBox.Show("No existen registros para exportar", "MENSAJE DEL SISTEMA");
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

        private void SolicitudDeRenovaciónDeEquipoCelular_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

        private void btnAnularRegistro_Click(object sender, EventArgs e)
        {
            CancelDocument();
        }

        private void btnAsociarAreaDeTrabajo_Click(object sender, EventArgs e)
        {
            AssociateWorkerToWorkArea();

        }

        private void AssociateWorkerToWorkArea()
        {
            #region Asociar Trabajador A Area de Trabajo() 
            ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(conection, user2, companyId, privilege, itemSeleccionado.idCodigoGeneral);
            ofrm.Show();
            #endregion
        }

        private void btnVerDispositivo_Click(object sender, EventArgs e)
        {
            int codigoDeDispositivo = itemSeleccionado.idDispositivoBaja;
            GoToDeviceCatalog(codigoDeDispositivo);

        }

        private void GoToDeviceCatalog(int codigoDeDispositivo)
        {

            #region Ir a catálogo de dispositivos            
            DispositivosEdicion oFron = new DispositivosEdicion("SAS", codigoDeDispositivo, user2, companyId, privilege);
            //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
            oFron.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
            oFron.WindowState = FormWindowState.Maximized;
            oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            oFron.Show();
            #endregion
        }

        private void btnVerLineaCelular_Click(object sender, EventArgs e)
        {
            GoToCellLineCatalog();

        }

        private void GoToCellLineCatalog()
        {
            #region Ir a catalogo de línea celulares() 
            LineasCelulares ofrm = new LineasCelulares(conection, user2, companyId, privilege, itemSeleccionado.numeroCelular);
            ofrm.Show();
            #endregion
        }

        private void btnVerSolicitudReferencia_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferencia.Value;
            GoToReferenceRequest(codigoSolicitudReferencia);
        }

        private void GoToReferenceRequest(int codigoReferencia)
        {
            #region Ir a solicitud de referencia(EQuipamiento tecnológico)           
            SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(conection, user2, companyId, privilege, codigoReferencia);
            ofrm.Show();
            #endregion
        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnActivarSolicitud_Click(object sender, EventArgs e)
        {
            CancelDocument();
        }

        private void btnVerDatosGeneralesDelColaborador_Click(object sender, EventArgs e)
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

        private void btnVerDispositivoAlta_Click(object sender, EventArgs e)
        {
            int codigoDeDispositivo = itemSeleccionado.idDispositivoAlta;
            GoToDeviceCatalog(codigoDeDispositivo);
        }

        private void btnRechazarSolicitud_Click(object sender, EventArgs e)
        {
            RejectRequest();
        }

        private void RejectRequest()
        {
            int codigoDeDispositivo = itemSeleccionado.id;
            Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
            Modelo.RejectRequest("SAS", codigoDeDispositivo);
            Actualizar();
        }

        private void btnAprobarSolicitud_Click(object sender, EventArgs e)
        {
            ApproveRejectRequest();

        }

        private void ApproveRejectRequest()
        {
            try
            {
                int codigoDeDispositivo = itemSeleccionado.id;
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultadoAprobacion = Modelo.ApproveRequest("SAS", codigoDeDispositivo, user2);
                MessageBox.Show(resultadoAprobacion, "CONFIRMACION DEL SISTEMA");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnVerSolicitudDeBaja_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferenciaBaja;
            GoToReferenceRequest(codigoSolicitudReferencia);
        }

        private void btnVerSolicitudDeAlta_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferenciaAlta;
            GoToReferenceRequest(codigoSolicitudReferencia);
        }

        private void btnVistaPreviaSolicitudBaja_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferenciaBaja;
            PrintRequetsdown(codigoSolicitudReferencia);
        }
        private void PrintRequetsdown(int id)
        {

            if (id > 0)
            {
                FormatoActaDeDevolucion ofrm = new FormatoActaDeDevolucion(id);
                ofrm.ShowDialog();
            }

        }

        private void btnVistaPreviaSolicitudAlta_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferenciaAlta;
            PrintRequetsUp(codigoSolicitudReferencia);
        }

        private void PrintRequetsUp(int id)
        {

            if (id > 0)
            {
                FormatoActaDeEntrega ofrm = new FormatoActaDeEntrega(id);
                ofrm.ShowDialog();
            }

        }

        private void btnVistaPreviaSolicitudAltaAnexo_Click(object sender, EventArgs e)
        {
            int codigoSolicitudReferencia = itemSeleccionado.idReferenciaAlta;
            PrintRequetsUpAnexo(codigoSolicitudReferencia);
        }

        private void PrintRequetsUpAnexo(int id)
        {

            if (id > 0)
            {
                FormatoActaDeEntregaAnexo ofrm = new FormatoActaDeEntregaAnexo(id);
                ofrm.ShowDialog();
            }

        }

        private void btnLiberarSolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoDeDispositivo = itemSeleccionado.id;
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultadoAprobacion = Modelo.ReleaseRequest("SAS", codigoDeDispositivo, user2);
                MessageBox.Show(resultadoAprobacion, "CONFIRMACION DEL SISTEMA");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnRetornarEstadoASolicitud_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoDeDispositivo = itemSeleccionado.id;
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultadoAprobacion = Modelo.ReturnRequestStatus("SAS", codigoDeDispositivo, user2);
                MessageBox.Show(resultadoAprobacion, "CONFIRMACION DEL SISTEMA");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnCVerDatosColaborador_Click(object sender, EventArgs e)
        {
            GoToWorkerCatalog();
        }

        private void btnCAsocicarDatosDeAreaYGerencia_Click(object sender, EventArgs e)
        {
            AssociateWorkerToWorkArea();
        }

        private void btnDVerDocumentosAdjuntos_Click(object sender, EventArgs e)
        {
            VerDocumentosAdjuntos();
        }

        private void VerDocumentosAdjuntos()
        {
            try
            {
                #region Attach()
                if (itemSeleccionado.id != 0)
                {
                    int codigoSelecionado = itemSeleccionado.id;
                    AdjuntarArchivos ofrm = new AdjuntarArchivos(conection, user2, companyId, privilege, codigoSelecionado.ToString(), "RenovacionDeEquiposCelulares");
                    ofrm.Show();

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

        private void btnActualizarEstado_Click(object sender, EventArgs e)
        {
            if (itemSeleccionado != null)
            {
                if (itemSeleccionado.id != null)
                {
                    if (itemSeleccionado.id != 0)
                    {
                        solicitud = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitud.id = itemSeleccionado.id;
                        SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud ofrm = new SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud(conection, user2, companyId, privilege, solicitud);
                        //ofrm.MdiParent = SolicitudDeRenovaciónDeEquipoCelular.ActiveForm;
                        ////ofrm.WindowState = FormWindowState.Maximized;
                        ////ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        //ofrm.Show();
                        ofrm.ShowDialog();
                        if (ofrm.DialogResult == DialogResult.OK)
                        {
                            Actualizar();
                        }


                    }
                }
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
                dgvListado.EnableFiltering = !true;
                dgvListado.ShowHeaderCellButtons = false;
                #endregion
            }
            else
            {

                #region Par() | Activar Filtro()
                dgvListado.EnableFiltering = true;
                dgvListado.ShowHeaderCellButtons = true;
                #endregion
            }
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
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ANULADO", string.Empty, true);
                c1.RowBackColor = Color.LightPink;
                c1.CellBackColor = Color.LightPink;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c1);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "POR REASIGNAR | POR RECUPERAR", "", true);
                c2.RowBackColor = Utiles.colorAmarilloClaro;
                c2.CellBackColor = Utiles.colorAmarilloClaro;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c2);

                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "PENDIENTE DE SOLICITUD", "", true);
                c3.RowBackColor = Color.LightCyan;
                c3.CellBackColor = Color.LightCyan;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c3);

                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "APROBADO POR GERENCIA INMEDIATA", string.Empty, true);
                c4.RowBackColor = Color.SkyBlue;
                c4.CellBackColor =  Color.SkyBlue;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c4);

                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "EN SOLICITUD", "", true);
                c5.RowBackColor = Color.White;
                c5.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c5);

                ConditionalFormattingObject c6 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "FINALIZADO", "", true);
                c6.RowBackColor = Color.PaleGreen;
                c6.CellBackColor = Color.PaleGreen;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c6);

                ConditionalFormattingObject c7 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "Atendido Total".ToUpper(), "", true);
                c7.RowBackColor = Color.PaleGreen;
                c7.CellBackColor = Color.PaleGreen;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c7);

                ConditionalFormattingObject c8 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "Atendido Parcial".ToUpper(), "", true);
                c8.RowBackColor = Color.Moccasin;
                c8.CellBackColor = Color.Moccasin;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c8);

                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "ANULADO", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c1);

                ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "POR REASIGNAR | POR RECUPERAR", "", true);
                c2.RowBackColor = Color.White;
                c2.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c2);

                ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "PENDIENTE DE SOLICITUD", "", true);
                c3.RowBackColor = Color.White;
                c3.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c3);

                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Contains, "APROBADO POR GERENCIA INMEDIATA", string.Empty, true);
                c4.RowBackColor = Color.White;
                c4.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c4);

                ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "EN SOLICITUD", "", true);
                c5.RowBackColor = Color.White;
                c5.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c5);

                ConditionalFormattingObject c6 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "FINALIZADO", "", true);
                c6.RowBackColor = Color.White;
                c6.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c6);


                ConditionalFormattingObject c7 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "Atendido Total".ToUpper(), "", true);
                c7.RowBackColor = Color.White;
                c7.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c7);

                ConditionalFormattingObject c8 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "Atendido Parcial".ToUpper(), "", true);
                c8.RowBackColor = Color.White;
                c8.CellBackColor = Color.White;
                dgvListado.Columns["chESTADO"].ConditionalFormattingObjectList.Add(c8);

                #endregion
            }
        }

        private void finalizarAtenciónToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void AtendidoTotal()
        {
            try
            {
                int codigoDeDispositivo = itemSeleccionado.id;
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultadoAprobacion = Modelo.CambiarAEstadoAtendidoTotal("SAS", codigoDeDispositivo, user2);
                MessageBox.Show(resultadoAprobacion, "CONFIRMACION DEL SISTEMA");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAtendidoParcial_Click(object sender, EventArgs e)
        {
            AtendidoParcial();
        }

        private void AtendidoParcial()
        {
            try
            {
                int codigoDeDispositivo = itemSeleccionado.id;
                Modelo = new SAS_SolicitudDeRenovacionTelefoniaCelularController();
                string resultadoAprobacion = Modelo.CambiarAEstadoAtendidoParcial("SAS", codigoDeDispositivo, user2);
                MessageBox.Show(resultadoAprobacion, "CONFIRMACION DEL SISTEMA");
                Actualizar();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

    }
}
