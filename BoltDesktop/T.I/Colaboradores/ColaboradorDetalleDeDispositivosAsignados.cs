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
using System.Reflection;
using System.Drawing;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ColaboradorDetalleDeDispositivosAsignados : Form
    {

        private string conection, idcodigoPersonal, companyId = string.Empty;
        private SAS_ListadoColaboradoresByDispositivo itemSelecionado;
        private SAS_USUARIOS user2;
        private PrivilegesByUser privilege;
        private SAS_DispositivoUsuariosController modelo;
        private List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult> listado;
        private SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult itemSelecionadoDispositivo;
        private int codigoDispotivo = 0;
        int focoPintado = 0;
        int focoFiltroHistorico = 0;
        int focoFiltroFuncionamiento = 0;
        int focoFiltroEstadoDispositivo = 0;
        private List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult> listadoFiltroHistorico, listadoFiltroFuncionamiento, listadoFiltroEstadoDispostivo;

        private SAS_ListadoDeDispositivosAllResult oDispositivo;
        private string fileName;
        private bool exportVisualSettings;

        public ColaboradorDetalleDeDispositivosAsignados()
        {
            InitializeComponent();


        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.dgvListado.TableElement.BeginUpdate();
                this.LoadFreightSummary();
                this.dgvListado.TableElement.EndUpdate();

                base.OnLoad(e);
                this.PintarGrillaResultado();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
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
                items1.Add(new GridViewSummaryItem("chdispositivo", "Count : {0:N2}; ", GridAggregateFunction.Count));
                this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        public ColaboradorDetalleDeDispositivosAsignados(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_ListadoColaboradoresByDispositivo _itemSelecionado)
        {
            InitializeComponent();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            itemSelecionado = _itemSelecionado;
            idcodigoPersonal = itemSelecionado.idcodigogeneral != null ? itemSelecionado.idcodigogeneral.Trim() : string.Empty;
            txtCodigo.Text = itemSelecionado.idcodigogeneral != null ? itemSelecionado.idcodigogeneral.Trim() : string.Empty;
            txtDNI.Text = itemSelecionado.nrodocumento != null ? itemSelecionado.nrodocumento.Trim() : string.Empty;
            txtNombresCompletos.Text = itemSelecionado.apenom != null ? itemSelecionado.apenom.Trim() : string.Empty;
            txtSexo.Text = itemSelecionado.sexo != null ? (itemSelecionado.sexo.Trim() == "M" ? "MASCULINO" : "FEMENINO") : string.Empty;
            txtPlanilla.Text = itemSelecionado.dsc_planilla != null ? itemSelecionado.dsc_planilla.Trim() : string.Empty;
            txtSituacion.Text = itemSelecionado.estado != null ? itemSelecionado.estado.Trim() : string.Empty;
            txtCargo.Text = itemSelecionado.cargo != null ? itemSelecionado.cargo.Trim() : string.Empty;
            txtArea.Text = itemSelecionado.area != null ? itemSelecionado.area.Trim() : string.Empty;
            txtGerencia.Text = itemSelecionado.gerencia != null ? itemSelecionado.gerencia.Trim() : string.Empty;
            txtEmail.Text = itemSelecionado.email != null ? itemSelecionado.email.Trim() : string.Empty;
            txtEmailCorporativo.Text = itemSelecionado.correcorporativo != null ? itemSelecionado.correcorporativo.Trim() : string.Empty;
            txtTelefono.Text = itemSelecionado.lineaCelular != null ? itemSelecionado.lineaCelular.Trim() : string.Empty;
            txtCorreoCorporativo.Text = itemSelecionado.lineaCorporativo != null ? itemSelecionado.lineaCorporativo.Trim() : string.Empty;
            Consultar();
        }

        private void ColaboradorDetalleDeDispositivosAsignados_Load(object sender, EventArgs e)
        {

        }

        private void PintarGrillaResultado()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "ACTIVO", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "FINALIZACIÓN DE VIGENCIA AUTORIZADA", "", true);
            c2.RowBackColor = Color.LightGray;
            c2.CellBackColor = Color.LightGray;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c2);

            ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "EN PROCESO DE BAJA", "", true);
            c3.RowBackColor = Color.LightSeaGreen;
            c3.CellBackColor = Color.LightSeaGreen;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c3);


            ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado applied to entire row", ConditionTypes.Equal, "ANULADO", "", true);
            c4.RowForeColor = Color.LightCoral;
            c4.RowFont = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c4);

            ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado applied to entire row", ConditionTypes.Equal, "MANTENIMIENTO", "", true);
            c5.RowBackColor = Color.WhiteSmoke;
            c5.CellBackColor = Color.WhiteSmoke;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c5);

            ConditionalFormattingObject c6 = new ConditionalFormattingObject("Funcionamiento applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
            c6.RowFont = new Font("Segoe UI", 8, FontStyle.Italic);
            dgvListado.Columns["chFuncionamientoDescripcion"].ConditionalFormattingObjectList.Add(c6);

            //update the grid view for the conditional formatting to take effect            
            //radGridView1.TableElement.Update(false);        
        }

        private void DespintarGrillaResultado()
        {             //add a couple of sample formatting objects            
            ConditionalFormattingObject c1 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "ACTIVO", "", true);
            c1.RowBackColor = Color.White;
            c1.CellBackColor = Color.White;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c1);

            ConditionalFormattingObject c2 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "FINALIZACIÓN DE VIGENCIA AUTORIZADA", "", true);
            c2.RowBackColor = Color.White;
            c2.CellBackColor = Color.White;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c2);

            ConditionalFormattingObject c3 = new ConditionalFormattingObject("Estado, applied to entire row", ConditionTypes.Equal, "EN PROCESO DE BAJA", "", true);
            c3.RowBackColor = Color.White;
            c3.CellBackColor = Color.White;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c3);


            ConditionalFormattingObject c4 = new ConditionalFormattingObject("Estado applied to entire row", ConditionTypes.Equal, "ANULADO", "", true);
            c4.RowForeColor = Color.White;
            c4.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c4);

            ConditionalFormattingObject c5 = new ConditionalFormattingObject("Estado applied to entire row", ConditionTypes.Equal, "MANTENIMIENTO", "", true);
            c5.RowBackColor = Color.White;
            c5.CellBackColor = Color.White;
            dgvListado.Columns["chnombreEstado"].ConditionalFormattingObjectList.Add(c5);

            ConditionalFormattingObject c6 = new ConditionalFormattingObject("Funcionamiento applied to entire row", ConditionTypes.Equal, "INACTIVO", "", true);
            c6.RowFont = new Font("Segoe UI", 8, FontStyle.Regular);
            dgvListado.Columns["chFuncionamientoDescripcion"].ConditionalFormattingObjectList.Add(c6);

            //update the grid view for the conditional formatting to take effect            
            //radGridView1.TableElement.Update(false);        
        }

        private void ColaboradorDetalleDeDispositivosAsignados_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Consultar()
        {
            gbDatosColaborador.Enabled = false;
            gbListado.Enabled = false;
            bgwHilo.RunWorkerAsync();

        }

        private void btnIrACatalogo_Click(object sender, EventArgs e)
        {
            IrADispositivo();
        }

        private void IrADispositivo()
        {
            if (codigoDispotivo > 0)
            {
                SAS_ListadoDeDispositivosAllResult oDispositivo = new SAS_ListadoDeDispositivosAllResult();
                modelo = new SAS_DispositivoUsuariosController();
                oDispositivo = modelo.ObtenerDispositivoById("SAS", codigoDispotivo);

                DispositivosEdicion oFron = new DispositivosEdicion("SAS", oDispositivo, user2, companyId, privilege);
                //oFron.Show(); Actualizado el 24.04.2022, para que no salga del formulario
                oFron.MdiParent = ColaboradoresListado.ActiveForm;
                oFron.WindowState = FormWindowState.Maximized;
                oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                oFron.Show();

            }
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            itemSelecionadoDispositivo = new SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult();
            oDispositivo = new SAS_ListadoDeDispositivosAllResult();
            oDispositivo.id = 0;
            oDispositivo.nombres = string.Empty;
            oDispositivo.estado = string.Empty;
            oDispositivo.idestado = 0;


            btnIrACatalogo.Enabled = false;
            codigoDispotivo = 0;
            try
            {
                #region 

                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value.ToString() != string.Empty)
                            {
                                codigoDispotivo = (dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chdispositivoCodigo"].Value) : 0);
                                var resultado = listado.Where(x => x.dispositivoCodigo == codigoDispotivo).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    itemSelecionadoDispositivo = resultado.Single();
                                    oDispositivo = new SAS_ListadoDeDispositivosAllResult();
                                    oDispositivo.id = itemSelecionadoDispositivo.dispositivoCodigo;
                                    oDispositivo.nombres = itemSelecionadoDispositivo.dispositivo;
                                    oDispositivo.estado = itemSelecionadoDispositivo.nombreEstado;
                                    oDispositivo.idestado = itemSelecionadoDispositivo.estadoDeDispositivo ;
                                    btnIrACatalogo.Enabled = true;

                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    itemSelecionadoDispositivo = resultado.ElementAt(0);
                                    oDispositivo = new SAS_ListadoDeDispositivosAllResult();
                                    oDispositivo.id = itemSelecionadoDispositivo.dispositivoCodigo;
                                    oDispositivo.nombres = itemSelecionadoDispositivo.dispositivo;
                                    oDispositivo.estado = itemSelecionadoDispositivo.nombreEstado;
                                    oDispositivo.idestado = itemSelecionadoDispositivo.estadoDeDispositivo;
                                    btnIrACatalogo.Enabled = true;
                                }
                                else
                                {
                                    codigoDispotivo = 0;
                                    itemSelecionadoDispositivo.dispositivoCodigo = 0;
                                    btnIrACatalogo.Enabled = false;
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

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnResaltarResultados_Click(object sender, EventArgs e)
        {
            focoPintado += 1;

            long sobrante = (focoPintado % 2);
            {
                if (sobrante == 0)
                {
                    PintarGrillaResultado();
                }
                else
                {
                    DespintarGrillaResultado();
                }
            }
        }

        private void btnResultadoFiltrado_Click(object sender, EventArgs e)
        {
            focoFiltroHistorico += 1;
            MostrarResultadosConFiltros();
        }

        private void MostrarResultadosConFiltros()
        {
            long sobranteHistorico = (focoFiltroHistorico % 2);
            long sobranteFuncionamiento = (focoFiltroFuncionamiento% 2);
            long sobranteEstadoDispositivo = (focoFiltroEstadoDispositivo % 2);
            try
            {
                if (sobranteHistorico == 0)
                {
                    listadoFiltroHistorico = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                    if (listado != null && listado.ToList().Count > 0)
                    {
                        listadoFiltroHistorico = listado.Where(x => x.estadoItem == 1).ToList();

                        listadoFiltroFuncionamiento = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                        if (sobranteFuncionamiento == 0)
                        {
                            listadoFiltroFuncionamiento = listadoFiltroHistorico.Where(x => x.FuncionamientoDescripcion.Trim().ToUpper() == "OPERATIVO").ToList();
                            listadoFiltroEstadoDispostivo = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                            if (sobranteEstadoDispositivo == 0)
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento.Where(x => x.nombreEstado.Trim().ToUpper() == "ACTIVO" || x.nombreEstado.Trim().ToUpper() == "En mantenimiento".ToUpper() || x.nombreEstado.Trim().ToUpper() == "Proximos a devolver a proveedor".ToUpper()).ToList();
                            }
                            else
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento;
                            }

                        }
                        else
                        {
                            listadoFiltroFuncionamiento = listadoFiltroHistorico;
                            listadoFiltroEstadoDispostivo = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                            if (sobranteEstadoDispositivo == 0)
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento.Where(x => x.nombreEstado.Trim().ToUpper() == "ACTIVO" || x.nombreEstado.Trim().ToUpper() == "En mantenimiento".ToUpper() || x.nombreEstado.Trim().ToUpper() == "Proximos a devolver a proveedor".ToUpper()).ToList();
                            }
                            else
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento;
                            }
                        }


                    }
                }
                else
                {
                    listadoFiltroHistorico = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                    if (listado != null && listado.ToList().Count > 0)
                    {
                        listadoFiltroHistorico = listado.ToList();
                        listadoFiltroFuncionamiento = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                        if (sobranteFuncionamiento == 0)
                        {
                            listadoFiltroFuncionamiento = listadoFiltroHistorico.Where(x => x.FuncionamientoDescripcion.Trim().ToUpper() == "OPERATIVO").ToList();
                            listadoFiltroEstadoDispostivo = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                            if (sobranteEstadoDispositivo == 0)
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento.Where(x => x.nombreEstado.Trim().ToUpper() == "ACTIVO" || x.nombreEstado.Trim().ToUpper() == "En mantenimiento".ToUpper() || x.nombreEstado.Trim().ToUpper() == "Proximos a devolver a proveedor".ToUpper()).ToList();
                            }
                            else
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento;
                            }

                        }
                        else
                        {
                            listadoFiltroFuncionamiento = listadoFiltroHistorico;
                            listadoFiltroEstadoDispostivo = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                            if (sobranteEstadoDispositivo == 0)
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento.Where(x => x.nombreEstado.Trim().ToUpper() == "ACTIVO" || x.nombreEstado.Trim().ToUpper() == "En mantenimiento".ToUpper() || x.nombreEstado.Trim().ToUpper() == "Proximos a devolver a proveedor".ToUpper()).ToList();
                            }
                            else
                            {
                                listadoFiltroEstadoDispostivo = listadoFiltroFuncionamiento;
                            }
                        }
                    }

                }

                dgvListado.DataSource = listadoFiltroEstadoDispostivo.ToDataTable<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                dgvListado.Refresh();
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

        private void cambiarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            try
            {
               // oDispositivo = new SAS_ListadoDeDispositivos();

                if (oDispositivo != null)
                {
                    if (oDispositivo.idestado != null)
                    {

                        if (oDispositivo.idestado > 0)
                        {
                            DispositivosCambioEstado oFron = new DispositivosCambioEstado("SAS", oDispositivo.id, oDispositivo.nombres, oDispositivo.estado, user2, companyId, privilege);
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

        private void btnFiltroEstadoDispositivo_Click(object sender, EventArgs e)
        {
            focoFiltroEstadoDispositivo += 1;
            MostrarResultadosConFiltros();
        }

        private void btnFiltroFuncionamiento_Click(object sender, EventArgs e)
        {
            focoFiltroFuncionamiento += 1;
            MostrarResultadosConFiltros();
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
            excelExporter.SheetName = "Documento";
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                modelo = new SAS_DispositivoUsuariosController();
                listado = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
                listado = modelo.ListadoDetalleDeDispositivosPorColaborador(conection, idcodigoPersonal).ToList();



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
                MostrarResultadosConFiltros();
                gbDatosColaborador.Enabled = true;
                gbListado.Enabled = true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

    }
}
