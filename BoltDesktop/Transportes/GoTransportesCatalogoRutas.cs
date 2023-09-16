using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls;
using MyControlsDataBinding.Extensions;
using Asistencia.Negocios;
using Asistencia.Datos;
using System.Configuration;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;

namespace Asistencia
{
    public partial class GoTransportesCatalogoRutas : Telerik.WinControls.UI.RadForm
    {
        private List<SJ_RHListaRutasResult> routes;
        private string routeCode;
        private string estatusCode;
        private string msg;
        private SJ_RHRuta route;
        private SJ_RHRutaNegocios Modelo;
        private bool exportVisualSettings;
        private string fileName;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;

        public GoTransportesCatalogoRutas()
        {
            InitializeComponent();
            Inicio();
            LimpiarFormulario();
            RefreshList();
            ActivarDesactivarControlEdicion(false);
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnActualizarLista.Enabled = true;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = true;
            btnAnular.Enabled = true;
            btnSalir.Enabled = true;
            btnExportar.Enabled = true;
        }

        public GoTransportesCatalogoRutas(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            Inicio();
            LimpiarFormulario();
            RefreshList();
            ActivarDesactivarControlEdicion(false);
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnActualizarLista.Enabled = true;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = true;
            btnAnular.Enabled = true;
            btnSalir.Enabled = true;
            btnExportar.Enabled = true;
        }

        private void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "erick";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "Sistemas";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void FletePorMovilidad_Load(object sender, EventArgs e)
        {



        }

        private void ObtenerListaRutas()
        {
            SJ_RHRutaNegocios RutaNeg = new SJ_RHRutaNegocios();
            routes = new List<SJ_RHListaRutasResult>();
            routes = RutaNeg.ListarRutasDeRecorridos(_conection);

        }

        private void MostrarListarRutas()
        {
            this.dgvRutas.DataSource = routes.ToDataTable<SJ_RHListaRutasResult>();
            this.dgvRutas.Refresh();
            this.dgvRutas.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ActivarDesactivarControlEdicion(bool estate)
        {
            this.gbEdit.Enabled = estate;
            this.gbList.Enabled = !estate;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
            ActivarDesactivarControlEdicion(false);
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnActualizarLista.Enabled = true;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = true;
            btnAnular.Enabled = true;
            btnSalir.Enabled = true;
            btnExportar.Enabled = true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosCabecera() == true)
            {
                try
                {
                    SetRuta("RUTAS");
                    if (chkGenerarIdaVuelta.Checked == true)
                    {
                        if (AddRutaIdaVuelta(route) != true)
                        {
                            RadMessageBox.Show("Los datos del fomulario no se pueden registrar, valor duplicado", "Sistema");
                            return;
                        }
                    }
                    else
                    {
                        if (AddRuta(route) != true)
                        {
                            RadMessageBox.Show("Los datos del fomulario no se pueden registrar, valor duplicado", "Sistema");
                            return;
                        }

                    }

                    RadMessageBox.Show("Operación realizada satisfactoriamente", "Confirmación del sistema");
                    ActivarDesactivarControlEdicion(false);

                    RefreshList();
                    btnNuevo.Enabled = true;
                    btnEditar.Enabled = true;
                    btnGrabar.Enabled = false;
                    btnActualizarLista.Enabled = true;
                    btnCancelar.Enabled = false;
                    btnEliminar.Enabled = true;
                    btnAnular.Enabled = true;
                    btnSalir.Enabled = true;
                    btnExportar.Enabled = true;

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE EL SISTEMA");
                    return;
                }
            }
            else
            {
                RadMessageBox.Show(msg);
            }
        }

        private bool AddRuta(SJ_RHRuta oRuta)
        {
            bool estate = false;
            try
            {
                if (oRuta.RutaDestino != null && oRuta.RutaOrigen != null)
                {
                    if (oRuta.RutaDestino != string.Empty && oRuta.RutaOrigen != string.Empty)
                    {
                        Modelo =new SJ_RHRutaNegocios();
                        estate = Modelo.AddRoute(oRuta, _conection);
                        //this.txtCodigo.Text = Codigo.ToString();
                    }
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE EL SISTEMA");
                return false;
            }

            return estate;
        }


        private bool AddRutaIdaVuelta(SJ_RHRuta oRuta)
        {
            bool estate = false;
            try
            {
                if (oRuta.RutaDestino != null && oRuta.RutaOrigen != null)
                {
                    if (oRuta.RutaDestino != string.Empty && oRuta.RutaOrigen != string.Empty)
                    {
                        Modelo =new SJ_RHRutaNegocios();
                        estate = Modelo.AddRoundTripRoute(oRuta, _conection);
                        //this.txtCodigo.Text = Codigo.ToString();
                    }
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE EL SISTEMA");
                return false;
            }
            return estate;
        }

        private void SetRuta(string nombreDelObjeto)
        {
            try
            {
                switch (nombreDelObjeto)
                {
                    case "RUTAS":
                        #region Obtener Objeto Transportista()
                        try
                        {
                            route = new SJ_RHRuta();
                            route.Id = this.txtCodigo.Text.ToString() != "" ? Convert.ToInt32(this.txtCodigo.Text.ToString()) : 0;
                            route.RutaOrigen = this.txtOrigenId.Text.ToString() != "" ? this.txtOrigenId.Text.ToString() : "";
                            route.RutaDestino = this.txtDestinoId.Text.ToString() != "" ? this.txtDestinoId.Text.ToString() : "";
                            route.Distancia = this.txtDistancia.Text.ToString() != "" ? Convert.ToDecimal(this.txtDistancia.Text.ToString()) : 0;
                            route.TiempoViaje = this.txtDuracion.Text.ToString() != "" ? Convert.ToDecimal(this.txtDuracion.Text.ToString()) : 0;
                            route.Observacion = this.txtObservacion.Text.ToString() != "" ? this.txtObservacion.Text.ToString() : "";
                            route.IdEstado = this.txtIdEstado.Text.ToString();
                            route.abreviaturaRutaOrigen = txtAbreviaturaRutaOrigen.Text.ToString() != "" ? this.txtAbreviaturaRutaOrigen.Text.ToString() : "";
                            route.descripcionCortaOrigen = txtDescripcionCortaRutaOrigen.Text.ToString() != "" ? this.txtDescripcionCortaRutaOrigen.Text.ToString() : "";
                            route.abreviaturaRuraDestino = txtAbreviaturaRutaDestino.Text.ToString() != "" ? this.txtAbreviaturaRutaDestino.Text.ToString() : "";
                            route.descripcionCortaDestino = txtDescripcionCortaRutaDestino.Text.ToString() != "" ? this.txtDescripcionCortaRutaDestino.Text.ToString() : "";
                            route.esIngreso = chkEsIngreso.Checked == true ? "1" : "0";
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString() + "\n Obtener Objeto de Transportista", "MENSAJE DE SISTEMA");
                            return;
                        }

                        #endregion
                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + "\n Capturar datos para registro", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private bool ValidarDatosCabecera()
        {
            Boolean estado = true;

            msg = "";


            if (txtEstado.Text.ToString().Trim() == "")
            {
                msg += "El documento debe tener un estado \n";
                estado = false;
            }

            if (txtOrigenId.Text.ToString().Trim() == "")
            {
                msg += "El documento debe tener la ruta origen \n";
                estado = false;
            }

            if (txtDestinoId.Text.ToString().Trim() == "")
            {
                msg += "El documento debe tener la ruta destino \n";
                estado = false;
            }


            if (txtDistancia.Text.ToString().Trim() == "")
            {
                msg += "El documento debe tener la distancia aproximada \n";
                estado = false;
            }

            if (txtDuracion.Text.ToString().Trim() == "")
            {
                msg += "El documento debe tener una duración aproximada del recorrido \n";
                estado = false;
            }



            return estado;
        }

        private void LimpiarFormulario()
        {
            txtCodigo.Clear();
            txtEstado.Clear();
            txtIdEstado.Clear();
            txtOrigenId.Clear();
            txtRutaOrigen.Clear();
            txtDistancia.Clear();
            txtObservacion.Clear();
            txtDuracion.Clear();
            txtDestinoId.Clear();
            txtRutaDestino.Clear();
            txtEstado.Text = "Activo";
            txtIdEstado.Text = "AC";
            txtAbreviaturaRutaOrigen.Clear();
            txtDescripcionCortaRutaOrigen.Clear();
            txtAbreviaturaRutaDestino.Clear();
            txtDescripcionCortaRutaDestino.Clear();
            txtOrigenId.Focus();
            chkEsIngreso.Checked = false;
            chkGenerarIdaVuelta.Checked = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NewRegister();
        }

        private void NewRegister()
        {
            try
            {
                LimpiarFormulario();
                ActivarDesactivarControlEdicion(true);
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGrabar.Enabled = true;
                btnActualizarLista.Enabled = false;
                btnCancelar.Enabled = true;
                btnEliminar.Enabled = false;
                btnAnular.Enabled = false;
                btnSalir.Enabled = true;
                btnExportar.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE EL SISTEMA");
                return;
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            try
            {
                LimpiarFormulario();
                gbList.Enabled = false;
                gbEdit.Enabled = false;
                btnMenu.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE EL SISTEMA");
                return;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (estatusCode != "")
            {
                if (estatusCode != "AN")
                {
                    //posicionX = this.dgvRutas.CurrentRow.Index;
                    //posicionY = this.dgvRutas.CurrentColumn.Index;
                    ActivarDesactivarControlEdicion(true);
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnGrabar.Enabled = true;
                    btnActualizarLista.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnEliminar.Enabled = false;
                    btnAnular.Enabled = false;
                    btnSalir.Enabled = true;
                    btnExportar.Enabled = false;
                }
                else
                {
                    RadMessageBox.Show("No tiene el estado para la edición", "Atención");
                }
            }
            else
            {
                RadMessageBox.Show("No tiene el estado para la edición", "Atención");
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            AnularRegistro();
        }

        private void AnularRegistro()
        {
            try
            {
                if (this.estatusCode != "")
                {
                    if (this.estatusCode != "AN")
                    {
                        //posicionX = this.dgvRutas.CurrentRow.Index;
                        //posicionY = this.dgvRutas.CurrentColumn.Index;

                        Modelo =new SJ_RHRutaNegocios();
                        Modelo.Anular(Convert.ToInt32(routeCode), _conection);
                        RadMessageBox.Show("Anulado correctamente", "Atención");
                        ObtenerListaRutas();

                        RefreshList();

                        //dgvRutas.CurrentRow = dgvRutas.Rows[posicionX];
                        ActivarDesactivarControlEdicion(false);
                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = true;
                        btnGrabar.Enabled = false;
                        btnActualizarLista.Enabled = true;
                        btnCancelar.Enabled = false;
                        btnEliminar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnSalir.Enabled = true;
                        btnExportar.Enabled = true;


                    }
                    else
                    {
                        RadMessageBox.Show("No tiene el estado para la anulación del registro", "Atención");
                    }
                }
                else
                {
                    RadMessageBox.Show("No tiene el estado para la anulación del registro", "Atención");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void EliminarRegistro()
        {
            try
            {
                if (this.estatusCode != "")
                {
                    if (this.estatusCode != "AN")
                    {
                        //posicionX = this.dgvRutas.CurrentRow.Index;
                        //posicionY = this.dgvRutas.CurrentColumn.Index;

                        Modelo =new SJ_RHRutaNegocios();
                        var result = Modelo.ValirdarIntegridadReferencialRoute(Convert.ToInt32(routeCode), _conection);
                        if (result == string.Empty)
                        {
                            Modelo.Delete(Convert.ToInt32(routeCode), _conection);
                            RadMessageBox.Show("Elimiado correctamente", "Atención");
                            ObtenerListaRutas();
                        }
                        else
                        {
                            RadMessageBox.Show("El registro no se puede eliminar porque está referenciado con :" + result, "Advertencia del sistema");
                            return;
                        }


                        //if (dgvRutas.RowCount >= posicionX)
                        //{
                        //    dgvRutas.CurrentRow = dgvRutas.Rows[posicionX];
                        //}
                        //else
                        //{
                        //    dgvRutas.CurrentRow = dgvRutas.Rows[dgvRutas.RowCount];
                        //}

                        RefreshList();

                        ActivarDesactivarControlEdicion(false);
                        btnNuevo.Enabled = true;
                        btnEditar.Enabled = true;
                        btnGrabar.Enabled = false;
                        btnActualizarLista.Enabled = true;
                        btnCancelar.Enabled = false;
                        btnEliminar.Enabled = true;
                        btnAnular.Enabled = true;
                        btnSalir.Enabled = true;
                        btnExportar.Enabled = true;


                    }
                    else
                    {
                        RadMessageBox.Show("No tiene el estado para la eliminación del registro", "Atención");
                    }
                }
                else
                {
                    RadMessageBox.Show("No tiene el estado para la eliminación del registro", "Atención");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvRutas != null)
            {
                if (dgvRutas.Rows.Count > 0)
                {
                    ExportarListaRuta();
                }
            }
        }

        private void ExportarListaRuta()
        {
            Exportar(dgvRutas);
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
            excelExporter.SheetName = "Listado Rutas";
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

        private void btnOrigen_Click(object sender, EventArgs e)
        {

        }

        private void dgvRutas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRutas.Rows.Count > 0)
            {
                #region
                if (dgvRutas.CurrentRow != null)
                {
                    try
                    {
                        routeCode = dgvRutas.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chId"].Value.ToString()) : "";
                        this.txtCodigo.Text = routeCode;
                        txtOrigenId.Text = dgvRutas.CurrentRow.Cells["chRutaOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chRutaOrigen"].Value.ToString()) : "";
                        txtRutaOrigen.Text =
                           ((dgvRutas.CurrentRow.Cells["chDepartamentoOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDepartamentoOrigen"].Value.ToString()) : "") + " / " +
                            (dgvRutas.CurrentRow.Cells["chProvinciaOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chProvinciaOrigen"].Value.ToString()) : "") + " / " +
                            (dgvRutas.CurrentRow.Cells["chDistritoOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDistritoOrigen"].Value.ToString()) : ""));
                        txtDistancia.Text = dgvRutas.CurrentRow.Cells["chDistancia"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDistancia"].Value.ToString()) : "";
                        this.txtDuracion.Text = dgvRutas.CurrentRow.Cells["chDuracionViaje"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDuracionViaje"].Value.ToString()) : "";
                        this.txtObservacion.Text = dgvRutas.CurrentRow.Cells["chObservacion"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chObservacion"].Value.ToString()) : "";
                        txtDestinoId.Text = dgvRutas.CurrentRow.Cells["chRutaDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chRutaDestino"].Value.ToString()) : "";
                        txtRutaDestino.Text =
                            ((dgvRutas.CurrentRow.Cells["chDepartamentoDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDepartamentoDestino"].Value.ToString()) : "") + " / " +
                            (dgvRutas.CurrentRow.Cells["chProvinciaDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chProvinciaDestino"].Value.ToString()) : "") + " / " +
                            (dgvRutas.CurrentRow.Cells["chDistritoDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chDistritoDestino"].Value.ToString()) : ""));
                        estatusCode = dgvRutas.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chIdEstado"].Value.ToString()) : "";
                        this.txtIdEstado.Text = estatusCode;
                        this.txtEstado.Text = dgvRutas.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chEstado"].Value.ToString()) : "";

                        txtAbreviaturaRutaOrigen.Text = dgvRutas.CurrentRow.Cells["chabreviaturaRutaOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chabreviaturaRutaOrigen"].Value.ToString()) : "";
                        txtDescripcionCortaRutaOrigen.Text = dgvRutas.CurrentRow.Cells["chdescripcionCortaOrigen"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chdescripcionCortaOrigen"].Value.ToString()) : "";
                        txtAbreviaturaRutaDestino.Text = dgvRutas.CurrentRow.Cells["chabreviaturaRuraDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chabreviaturaRuraDestino"].Value.ToString()) : "";
                        txtDescripcionCortaRutaDestino.Text = dgvRutas.CurrentRow.Cells["chdescripcionCortaDestino"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chdescripcionCortaDestino"].Value.ToString()) : "";

                        string esIngreso = dgvRutas.CurrentRow.Cells["chesIngreso"].Value != null ? Convert.ToString(dgvRutas.CurrentRow.Cells["chesIngreso"].Value.ToString()) : "0";

                        chkEsIngreso.Checked = false;

                        if (esIngreso == "1")
                        {
                            chkEsIngreso.Checked = !false;
                        }

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "\n No se pueden asignar correctamente los valores a los controles ", "MENSAJE DE SISTEMA");
                        return;
                    }
                }
                #endregion
            }
        }

        private void btnDestino_Click(object sender, EventArgs e)
        {

        }

        private void txtOrigenId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDestinoId_TextChanged(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            ObtenerListaRutas();
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarListarRutas();
            gbList.Enabled = !false;
            gbEdit.Enabled = false;
            btnMenu.Enabled = !false;

            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnActualizarLista.Enabled = true;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = true;
            btnAnular.Enabled = true;
            btnSalir.Enabled = true;
            btnExportar.Enabled = true;

        }

        private void chkEsIngreso_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAbreviaturaRutaOrigen_Leave(object sender, EventArgs e)
        {
            if (txtOrigenId.Text != string.Empty && txtRutaOrigen.Text != string.Empty)
            {
                // get abbreviated path name || obtener nombre abreviado de ruta origen
                Modelo =new SJ_RHRutaNegocios();
                txtDescripcionCortaRutaOrigen.Text = Modelo.GetSourceAbbreviatedPathName(_conection, txtAbreviaturaRutaOrigen.Text.ToString().Trim(), txtOrigenId.Text);
            }
        }

        private void txtOrigenId_Leave(object sender, EventArgs e)
        {

        }

        private void txtAbreviaturaRutaDestino_Leave(object sender, EventArgs e)
        {
            if (txtDestinoId.Text != string.Empty && txtRutaDestino.Text != string.Empty)
            {
                // get abbreviated path name || obtener nombre abreviado de ruta destino
                Modelo =new SJ_RHRutaNegocios();
                txtDescripcionCortaRutaDestino.Text = Modelo.GetDestinationAbbreviatedPathName(_conection, txtAbreviaturaRutaDestino.Text.ToString().Trim(), txtDestinoId.Text);
            }
        }
    }
}
