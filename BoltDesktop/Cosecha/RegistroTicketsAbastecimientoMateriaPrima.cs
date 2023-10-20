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
using System.Globalization;
using ComparativoHorasVisualSATNISIRA.Produccion;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class ImpresionTicketsAbastecimientoMateriaPrima : Form
    {
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private ComboBoxHelper comboBoxHelper;
        private string desde;
        private string hasta;
        private string desdeFormato112;
        private string hastaFormato112;
        private RegistroAbastecimientoController modelo;
        private List<ListadoAcopioByTiktesResult> listadoRegistroDeAcopio;
        private List<ListadoAcopioByTiktesResult> listadoRegistroDeAcopioFiltro;
        private ListadoAcopioByTiktesResult itemSelecionado;
        private int ocultarTicketsImpresos = 1;
        private int visualizarJabasExporables = 1;
        private int soloMostrarResultadosDelDia;
        private int ClickResaltarResultados;
        private int ClickFiltro;
        private int IdTicketCabecera;

        public ImpresionTicketsAbastecimientoMateriaPrima()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            _conection = "SAS";
            _user2 = new SAS_USUARIOS();
            _user2.IdUsuario = "EAURAZO";
            _user2.NombreCompleto = "ERICK AURAZO";

            _companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.anular = 1;
            privilege.consultar = 1;
            privilege.editar = 1;
            privilege.eliminar = 1;
            privilege.nuevo = 1;


            CargarMeses();
            GetDateInicial();
            Consult();
        }

        public ImpresionTicketsAbastecimientoMateriaPrima(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            Inicio();
            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            CargarMeses();
            GetDateInicial();
            Consult();
        }

        public void Inicio()
        {
            try
            {


                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJAS"].ToString();
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
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

        private void CargarMeses()
        {
            try
            {
                comboBoxHelper = new ComboBoxHelper();
                cboMes.DisplayMember = "Descripcion";
                cboMes.ValueMember = "Valor";
                cboMes.DataSource = comboBoxHelper.GetComboMonth().ToList();
                cboMes.SelectedValue = DateTime.Now.ToString("MM");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;

            }

        }

        private void GetDateInicial()
        {
            this.txtPeriodo.Value = Convert.ToDecimal(DateTime.Now.Year);

            if (chkDiaActual.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                this.txtFechaDesde.Text = "01" + DateTime.Now.ToString("/MM/yyyy");
                this.txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }


        }

        //public MesController MesesNeg { get; private set; }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
        }

        private void ObtenerFechasMes()
        {
            DateTime fecha1;
            DateTime fecha2;

            if (cboMes.SelectedValue.ToString() != "00")
            {
                #region
                this.txtFechaDesde.Enabled = false;
                this.txtFechaHasta.Enabled = false;
                if (cboMes.SelectedValue.ToString() == "12")
                {
                    #region Si es mes diciembre
                    fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString() + "/" + this.txtPeriodo.Text.ToString()));// 
                    fecha2 = Convert.ToDateTime("31/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Text.ToString());// 
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();
                    #endregion
                }
                else
                {
                    #region Si es mes 13 habilitar controles de fecha, caso contrario es un mes de enero a noviembre.
                    if (cboMes.SelectedValue.ToString() == "13")
                    {
                        this.txtFechaDesde.Enabled = true;
                        this.txtFechaHasta.Enabled = true;
                    }
                    else
                    {
                        fecha2 = Convert.ToDateTime("01/" + (Convert.ToInt32(cboMes.SelectedValue) + 1) + "/" + this.txtPeriodo.Text.ToString()).AddDays(-1);// 
                        fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString()) + "/" + this.txtPeriodo.Text.ToString());// 
                        this.txtFechaDesde.Text = fecha1.ToShortDateString();
                        this.txtFechaHasta.Text = fecha2.ToShortDateString();
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (cboMes.SelectedValue.ToString() == "00")
                {
                    fecha2 = Convert.ToDateTime("31/12/" + this.txtPeriodo.Text.ToString());// 
                    fecha1 = Convert.ToDateTime("01/01/" + this.txtPeriodo.Text.ToString());//
                    this.txtFechaDesde.Text = fecha1.ToShortDateString();
                    this.txtFechaHasta.Text = fecha2.ToShortDateString();
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }

        private void cboMes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }
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
            dgvRegistros.MasterTemplate.AutoExpandGroups = true;
            dgvRegistros.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            dgvRegistros.GroupDescriptors.Clear();
            this.dgvRegistros.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chDESCRIPCION", "Count : {0:N0}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chPESOBRUTO", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chPESONETO", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chNROJABAS", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            dgvRegistros.MasterTemplate.SummaryRowsTop.Add(items1);

        }

        private void ImpresionTicketsAbastecimientoMateriaPrima_Load(object sender, EventArgs e)
        {

        }

        private void txtPeriodo_ValueChanged_1(object sender, EventArgs e)
        {
            if (cboMes.SelectedIndex > -1)
            {
                ObtenerFechasMes();
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                modelo = new RegistroAbastecimientoController();
                itemSelecionado = new ListadoAcopioByTiktesResult();
                listadoRegistroDeAcopio = new List<ListadoAcopioByTiktesResult>();
                listadoRegistroDeAcopio = modelo.ObtenerListadoRecepcionEntrePeriodos("NSFAJAS", desde, hasta).ToList();

                listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
                listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopio.ToList();

                if (chkOcultarTicketImpresos.Checked == true)
                {
                    //listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
                    listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.estadoImpresion != "IMPRESO").ToList();

                    if (chkSoloMostrarTicketsExportables.Checked == true)
                    {
                        listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion").ToList();
                    }

                }
                else
                {

                    if (chkSoloMostrarTicketsExportables.Checked == true)
                    {
                        listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion").ToList();
                    }
                }

                //if (ocultarTicketsImpresos == 1)
                //{
                //    listadoRegistroDeAcopio = listadoRegistroDeAcopio.Where(x => x.estadoImpresion != "IMPRESO").ToList();
                //}

                //if (visualizarJabasExporables == 1)
                //{
                //    listadoRegistroDeAcopio = listadoRegistroDeAcopio.Where(x => x.mercado == "Exportacion").ToList();
                //}



            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistros.DataSource = listadoRegistroDeAcopioFiltro.ToDataTable<ListadoAcopioByTiktesResult>();
                dgvRegistros.Refresh();
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            Consult();
        }

        private void Consult()
        {
            try
            {
                soloMostrarResultadosDelDia = 1;
                ocultarTicketsImpresos = 1;
                visualizarJabasExporables = 1;

                if (chkOcultarTicketImpresos.Checked == false)
                {
                    ocultarTicketsImpresos = 0;

                }
                if (chkSoloMostrarTicketsExportables.Checked == false)
                {
                    visualizarJabasExporables = 0;
                }


                /* Formato de busqueda '20190321', '20191221' */
                //desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("yyyyMMdd");
                //hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("yyyyMMdd");
                //desdeFormato112 = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                //hastaFormato112 = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
                if (chkDiaActual.Checked == true)
                {
                    desde = DateTime.Now.ToString("dd/MM/yyyy");
                    hasta = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    desde = Convert.ToDateTime(this.txtFechaDesde.Text).ToString("dd/MM/yyyy");
                    hasta = Convert.ToDateTime(this.txtFechaHasta.Text).ToString("dd/MM/yyyy");
                }


                bgwHilo.RunWorkerAsync();
                gbConsulta.Enabled = !true;
                gbDetalle.Enabled = !true;
                ProgressBar.Visible = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbConsulta.Enabled = true;
                gbDetalle.Enabled = true;
                ProgressBar.Visible = !true;
                return;
            }
        }

        private void dgvRegistros_DoubleClick(object sender, EventArgs e)
        {

            Editar();

        }

        private void Editar()
        {
            if (itemSelecionado != null)
            {
                if (itemSelecionado.item != null && itemSelecionado.IDINGRESOSALIDAACOPIOCAMPO != null)
                {
                    if (itemSelecionado.item != string.Empty && itemSelecionado.IDINGRESOSALIDAACOPIOCAMPO != string.Empty)
                    {
                        RegistroTicketsAbastecimientoMateriaPrimaDetalle oFron = new RegistroTicketsAbastecimientoMateriaPrimaDetalle(itemSelecionado, _conection, _user2, _companyId, privilege);
                        //ofrm.ShowDialog();
                        oFron.MdiParent = ImpresionTicketsAbastecimientoMateriaPrima.ActiveForm;
                        oFron.WindowState = FormWindowState.Maximized;
                        oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        oFron.Show();
                    }
                }
            }
        }

        private void dgvRegistros_SelectionChanged(object sender, EventArgs e)
        {
            #region selecionar registro()
            IdTicketCabecera = 0;

            btnEditar.Enabled = false;
            btnEditarRegistro.Enabled = false;
            btnImprimirEtiquetaGrande.Enabled = false;
            btnImprimirEtiquetaPequena.Enabled = false;
            btnVistaPreviaEtiquetaGrande.Enabled = false;
            btnVistaPreviaEtiquetaPequena.Enabled = false;
            btnEliminarTicket.Enabled = false;

            itemSelecionado = new ListadoAcopioByTiktesResult();
            if (dgvRegistros.Rows.Count > 0)
            {
                if (dgvRegistros.CurrentRow != null && dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value != null)
                {
                    try
                    {
                        string codigo = dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value != null ? dgvRegistros.CurrentRow.Cells["chIDINGRESOSALIDAACOPIOCAMPO"].Value.ToString() : string.Empty;
                        string item = dgvRegistros.CurrentRow.Cells["chItem"].Value != null ? dgvRegistros.CurrentRow.Cells["chItem"].Value.ToString() : string.Empty;
                        string estadoImpreso = dgvRegistros.CurrentRow.Cells["chestadoImpresion"].Value != null ? dgvRegistros.CurrentRow.Cells["chestadoImpresion"].Value.ToString() : "0";
                        IdTicketCabecera = dgvRegistros.CurrentRow.Cells["chCorrelativo"].Value != null ? Convert.ToInt32 (dgvRegistros.CurrentRow.Cells["chCorrelativo"].Value.ToString()) : 0;

                        var result = listadoRegistroDeAcopio.Where(x => x.IDINGRESOSALIDAACOPIOCAMPO == codigo && x.item == item).ToList();
                        btnImprimirEtiquetaGrande.Enabled = false;
                        btnImprimirEtiquetaPequena.Enabled = false;
                        btnVistaPreviaEtiquetaGrande.Enabled = false;
                        btnVistaPreviaEtiquetaPequena.Enabled = false;
                        btnEditar.Enabled = false;
                        btnEditarRegistro.Enabled = false;

                        if (IdTicketCabecera > 0)
                        {
                            btnEliminarTicket.Enabled = true;
                        }

                        if (result != null && result.ToList().Count > 0)
                        {
                            itemSelecionado = result.ElementAt(0);
                            if (itemSelecionado.estadoImpresion != null)
                            {
                                if (itemSelecionado.estadoImpresion.Trim() != string.Empty)
                                {
                                    if (itemSelecionado.estadoImpresion.Trim() != "0")
                                    {
                                        btnImprimirEtiquetaGrande.Enabled = !false;
                                        btnImprimirEtiquetaPequena.Enabled = !false;
                                        btnVistaPreviaEtiquetaGrande.Enabled = !false;
                                        btnVistaPreviaEtiquetaPequena.Enabled = !false;
                                    }
                                }
                            }
                            if (itemSelecionado.IDINGRESOSALIDAACOPIOCAMPO != null)
                            {
                                if (itemSelecionado.IDINGRESOSALIDAACOPIOCAMPO.Trim() != string.Empty)
                                {
                                    if (itemSelecionado.item != null)
                                    {
                                        if (itemSelecionado.item.Trim() != string.Empty)
                                        {
                                            if (itemSelecionado.mercado.ToUpper().Trim() == "Exportacion".ToUpper())
                                            {
                                                btnEditar.Enabled = true;
                                                btnEditarRegistro.Enabled = true;
                                            }
                                            else
                                            {
                                                btnEditar.Enabled = false;
                                                btnEditarRegistro.Enabled = false;
                                            }


                                        }
                                    }
                                }
                            }
                        }



                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "", "MENSAJE DEL SISTEMA");
                        return;
                    }
                }

            }

            #endregion 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvRegistros != null && dgvRegistros.Rows.Count > 0)
            {
                Exportar(dgvRegistros);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnEditarRegistro_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimirEtiquetaPequena_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPreviaEtiquetaPequena_Click(object sender, EventArgs e)
        {

            if (itemSelecionado != null)
            {
                if (itemSelecionado.correlativo != 0)
                {
                    RegistroTicketsAbastecimientoMateriaPrimaFormato02 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato02(Convert.ToInt32(itemSelecionado.correlativo));
                    //ofrm.MdiParent = SolicitudDeEquipamientoTecnologico.ActiveForm;
                    ofrm.WindowState = FormWindowState.Maximized;
                    ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    ofrm.ShowDialog();

                    //solicitud = new SAS_SolicitudDeEquipamientoTecnologico();
                    //solicitud.id = itemSeleccionado.id;
                    //RegistroTicketsAbastecimientoMateriaPrimaFormato02 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato02(_conection, _user2, _companyId, privilege, solicitud);
                    //ofrm.MdiParent = SolicitudDeEquipamientoTecnologico.ActiveForm;
                    //ofrm.WindowState = FormWindowState.Maximized;
                    //ofrm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    //ofrm.Show();

                }
            }
        }

        private void btnImprimirEtiquetaGrande_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPreviaEtiquetaGrande_Click(object sender, EventArgs e)
        {


            if (itemSelecionado != null)
            {
                if (itemSelecionado.correlativo != 0)
                {
                    ImpresionTicketsAbastecimientoMateriaPrimaImprimir ofrm = new ImpresionTicketsAbastecimientoMateriaPrimaImprimir(Convert.ToInt32(itemSelecionado.correlativo));
                    ofrm.ShowDialog();
                }
            }
        }

        private void chkDiaActual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiaActual.Checked == true)
            {
                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                if (cboMes.SelectedIndex > -1)
                {
                    ObtenerFechasMes();
                }
            }
        }

        private void chkOcultarTicketImpresos_CheckedChanged(object sender, EventArgs e)
        {
            listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
            listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopio.ToList();

            if (chkOcultarTicketImpresos.Checked == true)
            {
                listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
                listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.estadoImpresion != "IMPRESO").ToList();

                if (chkSoloMostrarTicketsExportables.Checked == true)
                {                    
                    listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion").ToList();
                }
                
            }
            else
            {
               
                if (chkSoloMostrarTicketsExportables.Checked == true)
                {
                    listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion").ToList();
                }
            }

            dgvRegistros.DataSource = listadoRegistroDeAcopioFiltro.ToDataTable<ListadoAcopioByTiktesResult>();
            dgvRegistros.Refresh();


        }

        private void chkSoloMostrarTicketsExportables_CheckedChanged(object sender, EventArgs e)
        {
            listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
            listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopio.ToList();

            if (chkOcultarTicketImpresos.Checked == true)
            {
                listadoRegistroDeAcopioFiltro = new List<ListadoAcopioByTiktesResult>();
                listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.estadoImpresion != "IMPRESO").ToList();

                if (chkSoloMostrarTicketsExportables.Checked == true)
                {
                    listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion" && x.IDMOTIVO =="IAC").ToList();
                }

            }
            else
            {

                if (chkSoloMostrarTicketsExportables.Checked == true)
                {
                    listadoRegistroDeAcopioFiltro = listadoRegistroDeAcopioFiltro.Where(x => x.mercado == "Exportacion" && x.IDMOTIVO == "IAC").ToList();
                }
            }

            dgvRegistros.DataSource = listadoRegistroDeAcopioFiltro.ToDataTable<ListadoAcopioByTiktesResult>();
            dgvRegistros.Refresh();

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistros.ShowColumnChooser();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ClickFiltro += 1;
            ActivateFilter();
        }

        private void ActivateFilter()
        {

            if ((ClickFiltro % 2) == 0)
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

        private void btnResaltar_Click(object sender, EventArgs e)
        {
            ClickResaltarResultados += 1;
            ResaltarResultados();
        }

        private void ResaltarResultados()
        {

            if ((ClickResaltarResultados % 2) == 0)
            {
                #region Par() | Acción pintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Impreso, applied to entire row", ConditionTypes.Contains, "IMPRESO", string.Empty, true);
                c1.RowBackColor = Color.IndianRed;
                c1.CellBackColor = Color.IndianRed;
                dgvRegistros.Columns["estadoImpresion"].ConditionalFormattingObjectList.Add(c1);

                


                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Impreso, applied to entire row", ConditionTypes.Contains, "IMPRESO", string.Empty, true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Strikeout);
                dgvRegistros.Columns["estadoImpresion"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
            else
            {
                #region Par() | Acción despintar()
                ConditionalFormattingObject c1 = new ConditionalFormattingObject("Impreso, applied to entire row", ConditionTypes.Contains, "IMPRESO", string.Empty, true);
                c1.RowBackColor = Color.White;
                c1.CellBackColor = Color.White;
                dgvRegistros.Columns["estadoImpresion"].ConditionalFormattingObjectList.Add(c1);



                ConditionalFormattingObject c4 = new ConditionalFormattingObject("Impreso, applied to entire row", ConditionTypes.Contains, "IMPRESO", string.Empty, true);
                c4.RowForeColor = Color.Black;
                c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
                dgvRegistros.Columns["estadoImpresion"].ConditionalFormattingObjectList.Add(c4);
                #endregion
            }
        }

        private void btnEliminarTicket_Click(object sender, EventArgs e)
        {
            EliminarTicket();
        }

        private void EliminarTicket()
        {
            if (IdTicketCabecera > 0)
            {
                modelo = new RegistroAbastecimientoController();
                int ResultadoAccion = modelo.EliminarTicketGenerado(_conection, IdTicketCabecera);
                Consult();
            }
        }

        private void subMenu_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
