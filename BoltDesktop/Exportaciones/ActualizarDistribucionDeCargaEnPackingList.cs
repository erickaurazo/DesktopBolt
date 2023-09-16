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


namespace ComparativoHorasVisualSATNISIRA.Exportaciones
{
    public partial class ActualizarDistribucionDeCargaEnPackingList : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
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
        private PackingListController modelo;
        private List<SAS_ListadoDePackingList> listado;
        private List<SAS_ListadoDePackingList> listadoFiltro;
        private SAS_ListadoDePackingList itemSelecionado;
        private int VerSoloCultivoVID = 0;
        private int VerSoloExportable = 0;


        public ActualizarDistribucionDeCargaEnPackingList()
        {
            InitializeComponent();
            Inicio();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = "SAS";
            user2 = new SAS_USUARIOS();
            user2.IdUsuario = "EAURAZO";
            user2.NombreCompleto = "ERICK AURAZO";

            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.anular = 1;
            privilege.consultar = 1;
            privilege.editar = 1;
            privilege.eliminar = 1;
            privilege.nuevo = 1;


            VerificarCheckBox();
            Consult();
        }

        private void VerificarCheckBox()
        {
            if (chkCultivoVid.Checked == true)
            {
                VerSoloCultivoVID = 1;
            }
            else
            {
                VerSoloCultivoVID = 0;
            }

            if (chkTipoDeTarget.Checked == true)
            {
                VerSoloExportable = 1;
            }
            else
            {
                VerSoloExportable = 0;
            }

        }

        private void Consult()
        {
            bgwHilo.RunWorkerAsync();
            gbCabecera.Enabled = !true;
            gbListado.Enabled = !true;
            pbDetalle.Visible = true;
        }

        public ActualizarDistribucionDeCargaEnPackingList(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
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
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;

            VerificarCheckBox();

            Consult();
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



        protected override void OnLoad(EventArgs e)
        {


            this.dgvListado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();
            base.OnLoad(e);

        }

        private void LoadFreightSummary()
        {
            dgvListado.MasterTemplate.AutoExpandGroups = true;
            dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chdocumento", "Count : {0:N0}; ", GridAggregateFunction.Count));
            dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);

        }





        private void btnConsultar_Click(object sender, EventArgs e)
        {

            VerificarCheckBox();

            Consult();
        }

        private void ActualizarDistribucionDeCargaEnPackingList_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                modelo = new PackingListController();
                listado = new List<SAS_ListadoDePackingList>();
                listado = modelo.GetListPackingList01(conection);

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
                listadoFiltro = new List<SAS_ListadoDePackingList>();
                GenerarPresentacionDeGrilla();
                gbCabecera.Enabled = true;
                gbListado.Enabled = true;
                pbDetalle.Visible = !true;
                BarraPrincipal.Enabled = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEA");
                return;
            }
        }

        private void GenerarPresentacionDeGrilla()
        {
            listadoFiltro = new List<SAS_ListadoDePackingList>();
            if (VerSoloCultivoVID == 1)
            {
                var listado01 = listado.Where(x => x.cultivo.Trim() == "UVA").ToList();
                //listadoFiltro.AddRange(listado01);

                if (VerSoloExportable == 1)
                {
                    var listado02 = listado01.Where(x => x.targetCode == 1).ToList();
                    listadoFiltro.AddRange(listado02);
                }
                else
                {
                    listadoFiltro.AddRange(listado01);
                }

            }
            else
            {
                //listadoFiltro.AddRange(listado);
                if (VerSoloExportable == 1)
                {
                    var listado03 = listado.Where(x => x.targetCode == 1).ToList();
                    listadoFiltro.AddRange(listado03);
                }
                else
                {
                    listadoFiltro.AddRange(listado);
                }
            }

            dgvListado.DataSource = listadoFiltro.OrderByDescending(x => x.numeroDePackingList).ToList().ToDataTable<SAS_ListadoDePackingList>();
            dgvListado.Refresh();
        }

        private void chkCultivoVid_CheckedChanged(object sender, EventArgs e)
        {
            VerificarCheckBox();

            GenerarPresentacionDeGrilla();
        }

        private void chkTipoDeTarget_CheckedChanged(object sender, EventArgs e)
        {


            VerificarCheckBox();
            GenerarPresentacionDeGrilla();

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvListado.ShowColumnChooser();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                Exportar(dgvListado);
            }
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            #region selecionar registro()
            btnEditar.Enabled = false;
            btnDistribuirPalletEnPackingList.Enabled = false;
            btnGenerarBaseDeDistribucion.Enabled = false;
            itemSelecionado = new SAS_ListadoDePackingList();
            if (dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null && dgvListado.CurrentRow.Cells["chcodigo"].Value != null)
                {
                    try
                    {
                        string codigoPackingList = dgvListado.CurrentRow.Cells["chcodigo"].Value != null ? dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString() : string.Empty;
                        var result = listado.Where(x => x.codigo == codigoPackingList).ToList();

                        if (result != null && result.ToList().Count > 0)
                        {
                            itemSelecionado = result.ElementAt(0);
                            if (itemSelecionado.targetCode == 1)
                            {
                                btnDistribuirPalletEnPackingList.Enabled = true;
                            }
                            else
                            {
                                btnDistribuirPalletEnPackingList.Enabled = false;
                            }
                            if (itemSelecionado.cantidadPalletRegistradosEnContenedor == 0)
                            {
                                btnGenerarBaseDeDistribucion.Enabled = true;
                            }
                            else
                            {
                                btnGenerarBaseDeDistribucion.Enabled = false;
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

        private void btnDistribuirPalletEnPackingList_Click(object sender, EventArgs e)
        {
            DistribucionDeCargaEnPackingList ofrm = new DistribucionDeCargaEnPackingList(conection,user2,companyId,privilege,itemSelecionado );
            ofrm.ShowDialog();
        }
    }
}
