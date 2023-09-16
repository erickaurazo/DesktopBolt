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


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class SolicitudDeEquipamientoTecnologico : Form
    {

        private GlobalesHelper globalHelper;
        private string result;
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListado> listado;
        private SAS_SolicitudDeEquipamientoTecnologicoListado itemSeleccionado = new SAS_SolicitudDeEquipamientoTecnologicoListado();
        private SAS_SolicitudDeEquipamientoTecnologicoController Modelo;
        private SAS_SolicitudDeEquipamientoTecnologico solicitud;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDatesResult> listado1;
        private List<SAS_SolicitudDeEquipamientoTecnologicoListado> listado2;

        public MesController MesesNeg;
        public string FechaDesdeConsulta;
        public string FechaHastaConsulta;

        public SolicitudDeEquipamientoTecnologico()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

            this._conection = "SAS";
            this._user2 = new SAS_USUARIOS();
            this._user2.IdUsuario = Environment.UserName;
            this._companyId = "001";
            this.privilege = new PrivilegesByUser();
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


        public SolicitudDeEquipamientoTecnologico(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;            
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


        private void SolicitudDeEquipamientoTecnologico_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                listado = new List<SAS_SolicitudDeEquipamientoTecnologicoListado>();
                listado1 = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDatesResult>();
                listado2 = new List<SAS_SolicitudDeEquipamientoTecnologicoListado>();

                Modelo =new SAS_SolicitudDeEquipamientoTecnologicoController();
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
                dgvListado.DataSource = listado.ToList().ToDataTable<SAS_SolicitudDeEquipamientoTecnologicoListado>();
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
            SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(_conection, _user2, _companyId, privilege, solicitud);
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

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region 
                itemSeleccionado = new SAS_SolicitudDeEquipamientoTecnologicoListado();
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

                                var resultado = listado.Where(x => x.id.ToString() == id).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSeleccionado = resultado.Single();
                                    itemSeleccionado.idCodigoGeneral = codigo;
                                    itemSeleccionado.nombresCompletos = nombres;

                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    itemSeleccionado = resultado.ElementAt(0);
                                    itemSeleccionado.idCodigoGeneral = codigo;
                                    itemSeleccionado.nombresCompletos = nombres;

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

                    ColaboradorAsociarConAreaDeTrabajo ofrm = new ColaboradorAsociarConAreaDeTrabajo(_conection, _user2, _companyId, privilege, oColaboradorParaAsociar);
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
                        solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                        solicitud.id = itemSeleccionado.id;
                        SolicitudDeEquipamientoTecnologicoMantenimiento ofrm = new SolicitudDeEquipamientoTecnologicoMantenimiento(_conection, _user2, _companyId, privilege, solicitud);
                        ofrm.Show();

                    }
                }
            }
        }

        private void chkMostarAgrupado_CheckedChanged(object sender, EventArgs e)
        {

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
    }
}
