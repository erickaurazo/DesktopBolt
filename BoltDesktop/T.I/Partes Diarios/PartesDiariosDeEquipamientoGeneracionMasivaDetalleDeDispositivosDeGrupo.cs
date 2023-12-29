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



namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    public partial class PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo : Form
    {
        #region Variables()
        string nombreformulario = "ParteDeEquipamientoITD";
        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes, tipoDePrioridades;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string companyId;
        private string conection;
        private string messageValidation;
        private string fileName = "DEFAULT";
        private string desde;
        private string hasta;
        private string CodigoProveedor = string.Empty;
        private string CodigoSedeDeTrabajo = string.Empty;
        private string CodigoTipoDipositivo = string.Empty;
        private string Fecha = string.Empty;
        private int ultimoItemEnListaDetalle = 0;
        private int codigoDispositivo;
        private int codigoSelecionado = 0;
        private bool exportVisualSettings = true;

        private int resultadoOperacion;
        private List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> itemsSelect;
        private SAS_DispostivoController deviceModelo;
        private string proveedor;
        private string proveedorCodigo;
        private string sede;
        private string sedeCodigo;
        private string tipoDispositivoCodigo;
        private string tipoDispositivo;
        #endregion

        public PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo()
        {
            try
            {
                InitializeComponent();
                nombreformulario = "ParteDeEquipamientoITD";
                conection = "SAS";                                                                
                Inicio();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        public PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> _itemsSelect)
        {
            try
            {
                InitializeComponent();
                nombreformulario = "ParteDeEquipamientoITD";
                conection = _conection;
                user2 = _user2;
                companyId = _companyId;
                privilege = _privilege;
                itemsSelect = _itemsSelect;

                 proveedor = itemsSelect[0].razonSocial.Trim();
                 proveedorCodigo = itemsSelect[0].idClieprov.Trim();

                 sede = itemsSelect[0].sedeDescripcion.Trim();
                 sedeCodigo = itemsSelect[0].sedecodigo.Trim();

                 tipoDispositivoCodigo = itemsSelect[0].tipoDispositivoCodigo.Trim();
                 tipoDispositivo = itemsSelect[0].tipoDispositivo.Trim();

                Inicio();
                Actualizar();
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
                items1.Add(new GridViewSummaryItem("chdispositivo", "Count : {0:N0}; ", GridAggregateFunction.Count));
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

        private void btnActualizarListado_Click(object sender, EventArgs e)
        {
            Actualizar();
        }


        private void Actualizar()
        {
            try
            {
                BarraPrincipal.Enabled = false;
                gbListado.Enabled = false;                
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                itemsSelect = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
                deviceModelo = new SAS_DispostivoController();
                itemsSelect = deviceModelo.ObtenerListadoDeDispositivosPorProveedorSedeTipo(conection, proveedorCodigo, sedeCodigo, tipoDispositivoCodigo).ToList();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (itemsSelect != null && itemsSelect.ToList().Count > 0)
                {
                    this.txtProveedor.Text = itemsSelect[0].razonSocial.Trim();
                    this.txtProveedorCodigo.Text = itemsSelect[0].idClieprov.Trim();

                    this.txtSede.Text = itemsSelect[0].sedeDescripcion.Trim();
                    this.txtSedeCodigo.Text = itemsSelect[0].sedecodigo.Trim();

                    this.txtTipoDispositivoCodigo.Text = itemsSelect[0].tipoDispositivoCodigo.Trim();
                    this.txtTipoDispositivoDescripcion.Text = itemsSelect[0].tipoDispositivo.Trim();
                }

                else
                {
                    this.txtProveedor.Text = string.Empty;
                    this.txtProveedorCodigo.Text = string.Empty;

                    this.txtSede.Text = string.Empty;
                    this.txtSedeCodigo.Text = string.Empty;

                    this.txtTipoDispositivoCodigo.Text = string.Empty;
                    this.txtTipoDispositivoDescripcion.Text = string.Empty;
                }
               

                dgvListado.DataSource = itemsSelect.ToList<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
                dgvListado.Refresh();

                BarraPrincipal.Enabled = !false;
                gbListado.Enabled = !false;
                progressBar1.Visible = !true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
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

        private void btnVerDispositivo_Click(object sender, EventArgs e)
        {
            VerDispositivo();
        }

        private void VerDispositivo()
        {
            if (codigoDispositivo != null)
            {
                if (codigoDispositivo > 0)
                {
                    SAS_ListadoDeDispositivosAllResult oDispositivo = new SAS_ListadoDeDispositivosAllResult();
                    oDispositivo.id = codigoDispositivo;
                    deviceModelo = new SAS_DispostivoController();
                    oDispositivo = deviceModelo.ObtenerDatosDeDispositivo(conection, codigoDispositivo);

                    DispositivosEdicion oFron = new DispositivosEdicion(conection, oDispositivo, user2, companyId, privilege);
                    //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                    oFron.MdiParent = PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo.ActiveForm;
                    oFron.WindowState = FormWindowState.Maximized;
                    oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    oFron.Show();

                }
            }
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            btnVerDispositivo.Enabled = false;
            try
            {
                #region 
                
                codigoDispositivo = 0;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chcodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString() != string.Empty)
                            {
                                codigoDispositivo = (dgvListado.CurrentRow.Cells["chcodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chcodigo"].Value.ToString()) : 0);                               
                                if (codigoDispositivo > 0)
                                {
                                    btnVerDispositivo.Enabled = true;
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


        private void gbDocumento_Enter(object sender, EventArgs e)
        {

        }

        private void PartesDiariosDeEquipamientoGeneracionMasivaDetalleDeDispositivosDeGrupo_Load(object sender, EventArgs e)
        {

        }
    }
}
