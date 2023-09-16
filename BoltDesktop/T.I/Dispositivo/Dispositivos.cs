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
using ComparativoHorasVisualSATNISIRA.T.I;
using ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class DispositivosListado : Form
    {
        private List<SAS_ListadoDeDispositivos> listado;
        private SAS_DispositivoIPController modelo;
        private SAS_DispostivoController deviceModelo;
        private SAS_ListadoDeDispositivos oDispositivo;
        private SAS_Dispostivo dispositivo;
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private int codigo = 0;
        private string fileName;
        private bool exportVisualSettings;

        public DispositivosListado()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consultar();
            Inicio();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = -1;
        }

        public DispositivosListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            Consultar();
            Inicio();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = -1;
        }

        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
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
            this.dgvDispositivo.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvDispositivo.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvDispositivo.MasterTemplate.AutoExpandGroups = true;
            this.dgvDispositivo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDispositivo.GroupDescriptors.Clear();
            this.dgvDispositivo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chdispositivo", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvDispositivo.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new SAS_DispositivoIPController();
            listado = new List<SAS_ListadoDeDispositivos>();

            listado = modelo.ListadoDeDispositivos("SAS").ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvDispositivo.DataSource = listado.ToDataTable<SAS_ListadoDeDispositivos>();
            dgvDispositivo.Refresh();
            dgvDispositivo.Enabled = true;

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                dgvDispositivo.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void DispositivosListado_Load(object sender, EventArgs e)
        {

        }

        private void dgvDispositivo_SelectionChanged(object sender, EventArgs e)
        {
            btnVerProgramacionDiaria.Enabled = false;
            btnCambiarEstado.Enabled = false;
            btnHabilitar.Enabled = true;
            btnDesactivar.Enabled = false;
            btnDuplicarRegistro.Enabled = false;
            btnEditarRegistro.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnVerDocumentosAsociados.Enabled = false;
            btnImprimirTicketQR.Enabled = false;
            btnVistaPreviaTicketQR.Enabled = false;
            btnVistaPreviaDeFichaDeDispositivo.Enabled = false;
            btnVerLineaCelular.Enabled = false;
            codigo = 0;
            string imgQr = string.Empty;
            oDispositivo = new SAS_ListadoDeDispositivos();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = 0;
            oDispositivo.nombres = string.Empty;
            oDispositivo.dispositivo = string.Empty;

            if (dgvDispositivo != null && dgvDispositivo.Rows.Count > 0)
            {
                if (dgvDispositivo.CurrentRow != null)
                {
                    if (dgvDispositivo.CurrentRow.Cells["chid"].Value != null)
                    {
                        if (dgvDispositivo.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                        {
                            codigo = (dgvDispositivo.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvDispositivo.CurrentRow.Cells["chid"].Value) : 0);

                            if (codigo != (int?)null)
                            {
                                if (codigo > 0)
                                {
                                    var resultado = listado.Where(x => x.id == codigo).ToList();
                                    if (resultado.ToList().Count >= 1)
                                    {
                                        btnDesactivar.Enabled = true;
                                        btnVerDocumentosAsociados.Enabled = true;
                                        btnDuplicarRegistro.Enabled = true;
                                        btnEditarRegistro.Enabled = true;
                                        btnEliminarRegistro.Enabled = false;
                                        btnImprimirTicketQR.Enabled = false;
                                        btnVistaPreviaTicketQR.Enabled = true;
                                        btnVistaPreviaDeFichaDeDispositivo.Enabled = false;
                                        btnVerLineaCelular.Enabled = false;
                                        btnVerProgramacionDiaria.Enabled = !false;

                                        #region
                                        oDispositivo = resultado.ElementAt(0);
                                        dispositivo.id = codigo;
                                        if (oDispositivo.imagen != null)
                                        {
                                            if (oDispositivo.imagen.ToString().Length > 10)
                                            {
                                                btnVistaPreviaTicketQR.Enabled = true;
                                            }
                                        }

                                        if (oDispositivo.lineaCelular != null)
                                        {
                                            if (oDispositivo.lineaCelular.Trim() != string.Empty || oDispositivo.lineaCelular.ToString().Trim().Length >= 9)
                                            {
                                                btnVerLineaCelular.Enabled = true;
                                            }
                                        }

                                        if (oDispositivo.idestado == 0)
                                        {
                                            btnAnular.Enabled = true;
                                            btnHabilitar.Enabled = true;
                                            btnDesactivar.Enabled = false;
                                            btnCambiarEstado.Enabled = false;
                                            btnEliminar.Enabled = false;
                                        }
                                        else if (oDispositivo.idestado == 1)
                                        {
                                            btnAnular.Enabled = true;
                                            btnHabilitar.Enabled = false;
                                            btnDesactivar.Enabled = true;
                                            btnEliminar.Enabled = true;
                                            btnCambiarEstado.Enabled = true;
                                        }
                                        else
                                        {
                                            btnAnular.Enabled = false;
                                            btnHabilitar.Enabled = false;
                                            btnDesactivar.Enabled = false;
                                            btnCambiarEstado.Enabled = true;
                                        }

                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            if (oDispositivo != null)
            {
                if (oDispositivo.id > 0)
                {
                    DispositivosEdicion oFron = new DispositivosEdicion(conection, oDispositivo, user2, companyId, privilege);
                    //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                    oFron.MdiParent = DispositivosListado.ActiveForm;
                    oFron.WindowState = FormWindowState.Maximized;
                    oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    oFron.Show();

                }
            }
        }

        private void dgvDispositivo_DoubleClick(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void Anular()
        {
            if (oDispositivo.id > 0)
            {
                if (oDispositivo.idestado == 1 || oDispositivo.idestado == 0)
                {
                    #region Anular ()                 
                    deviceModelo = new SAS_DispostivoController();
                    SAS_Dispostivo oDevice = new SAS_Dispostivo();
                    int resultadoAccion = deviceModelo.Unregister("SAS", dispositivo);
                    MessageBox.Show("Se ejecutado exitosamente la acción para el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                    Consultar();
                    #endregion
                }
                else
                {
                    MessageBox.Show("El documento no tiene el estado para \n Anular y/o Activar", "Mensaje del sistema");
                    return;
                }



            }
        }


        private void Eliminar()
        {
            int resultadoAccion = 0;
            if (oDispositivo.id >= 0)
            {

                int CantidadDeReferencia = deviceModelo.GetCountReferencias(conection, dispositivo);

                if (CantidadDeReferencia != null && CantidadDeReferencia == 0)
                {

                    #region Eliminar ()                 
                    deviceModelo = new SAS_DispostivoController();
                    SAS_Dispostivo oDevice = new SAS_Dispostivo();
                    resultadoAccion = deviceModelo.GetCountReferencias("SAS", dispositivo);
                    MessageBox.Show("Se ejecutado exitosamente la acción para el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                    Consultar();
                    #endregion
                }
                else
                {
                    MessageBox.Show("Operación no permitida \nEl dispositivo tiene " + resultadoAccion.ToString().PadLeft(3, '0') + "referencias . \n Primero anular los documentos de referencia", "Mensaje del sistema");
                    return;

                }


            }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void anularRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void verDocumentosYMovimientoAsociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewReferencesDocumentos();
        }

        private void ViewReferencesDocumentos()
        {
            //ReporteDetalleReferenciasBySolicitud ofrm = new ReporteDetalleReferenciasBySolicitud();
            //ofrm.Show();

            if (oDispositivo != null)
            {
                if (oDispositivo.id != null)
                {
                    ReporteDetalleReferenciasBySolicitud oFron = new ReporteDetalleReferenciasBySolicitud(conection, oDispositivo.id, oDispositivo.nombres, user2, companyId, privilege);
                    //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                    //                    oFron.MdiParent = Menu.ActiveForm;
                    oFron.WindowState = FormWindowState.Maximized;
                    oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    oFron.ShowDialog();

                }
            }


        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void imprimirTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewQR();
        }

        private void PreviewQR()
        {
            if (oDispositivo != null)
            {
                if (oDispositivo.id != null)
                {
                    if (oDispositivo.id > 0)
                    {
                        DispositivosEdicionImprimirEtiquetas ofrm = new DispositivosEdicionImprimirEtiquetas(Convert.ToInt32(oDispositivo.id));
                        ofrm.ShowDialog();
                    }
                }
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

        private void DispositivosListado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnDuplicarRegistro_Click(object sender, EventArgs e)
        {
            Duplicate();
        }

        private void Duplicate()
        {

            try
            {
                #region Duplicate ()                 
                deviceModelo = new SAS_DispostivoController();
                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                int resultadoAccion = deviceModelo.DuplicarDispositivoDesdeId("SAS", dispositivo.id, dispositivo.lineaCelular);
                MessageBox.Show("Se ejecutado exitosamente la acción para el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                Consultar();
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }


        }

        private void btnImprimirTicketQR_Click(object sender, EventArgs e)
        {
            PrintQR();
        }

        private void PrintQR()
        {

        }

        private void btnVistaPreviaDeFichaDeDispositivo_Click(object sender, EventArgs e)
        {
            PreviewDeviceFile();
        }

        private void PreviewDeviceFile()
        {

        }

        private void btnVerLineaCelular_Click(object sender, EventArgs e)
        {
            ViewCelPhone();
        }

        private void ViewCelPhone()
        {
            try
            {
                if (oDispositivo != null)
                {
                    if (oDispositivo.lineaCelular != null)
                    {

                        if (oDispositivo.lineaCelular.Trim() != string.Empty)
                        {
                            if (oDispositivo.lineaCelular.Trim().Count() == 9)
                            {
                                LineasCelulares oFron = new LineasCelulares(conection, user2, companyId, privilege, oDispositivo.lineaCelular);
                                //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                                oFron.MdiParent = DispositivosListado.ActiveForm;
                                oFron.WindowState = FormWindowState.Maximized;
                                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                                oFron.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            try
            {
                if (oDispositivo != null)
                {
                    if (oDispositivo.idestado != null)
                    {

                        if (oDispositivo.idestado > 0)
                        {
                            DispositivosCambioEstado oFron = new DispositivosCambioEstado(conection, oDispositivo.id, oDispositivo.nombres, oDispositivo.estado, user2, companyId, privilege);
                            //oFron.MdiParent = DispositivosListado.ActiveForm;
                            //oFron.WindowState = FormWindowState.Normal;
                            //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                            oFron.ShowDialog();
                            if (oFron.DialogResult == DialogResult.OK)
                            {
                                Consultar();
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvDispositivo.ShowColumnChooser();
        }

        private void btnCCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void btnListaVariosDocumentos_Click(object sender, EventArgs e)
        {
            ViewReferencesDocumentos();
        }

        private void btnCDuplicar_Click(object sender, EventArgs e)
        {
            Duplicate();
        }

        private void btnCVistaPrevia_Click(object sender, EventArgs e)
        {
            PreviewQR();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Exportar(dgvDispositivo);
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

        private void btnVerProgramacionDiaria_Click(object sender, EventArgs e)
        {
            VerProgramacionDiario();
        }

        private void VerProgramacionDiario()
        {
            try
            {
                #region Ver programacion diario() 
                if (oDispositivo != null)
                {
                    if (oDispositivo.id > 0)
                    {
                        PartesDiariosDeEquipamientoByIdDevice oFron = new PartesDiariosDeEquipamientoByIdDevice(conection, user2, companyId, privilege, oDispositivo.id, oDispositivo.dispositivo.Trim());                                                
                        oFron.MdiParent = DispositivosListado.ActiveForm;
                        oFron.WindowState = FormWindowState.Maximized;
                        oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                        oFron.Show();

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
    }
}
