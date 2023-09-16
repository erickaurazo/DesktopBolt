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
using MyDataGridViewColumns;
using System.Drawing;
using MyControlsDataBinding.Controles;


namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class ExonerarTicketACamaraDeGasificadoEdicion : Form
    {
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        SAS_ListadoDeRegistrosExoneradosByIdResult document;
        List<SAS_RegistroTicketCamaraGasificadoExonerados> documents;
        SAS_ListadoDeRegistrosExoneradosByDatesResult documentDate;
        List<SAS_ListadoDeRegistrosExoneradosByIdResult> documentsById;
        SAS_RegistroTicketCamaraGasificadoExoneradosController model;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private GlobalesHelper globalHelper;
        private SAS_RegistroTicketCamaraGasificadoExonerados cabecera = new SAS_RegistroTicketCamaraGasificadoExonerados();
        List<SAS_RegistroTicketCamaraGasificadoExonerados> listadoDetalleEliminado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
        List<SAS_RegistroTicketCamaraGasificadoExonerados> listadoDetalleFull = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
        List<SAS_ListadoDeRegistrosExoneradosByIdResult> listadoDetalleFullDate = new List<SAS_ListadoDeRegistrosExoneradosByIdResult>();
        private string mensajeRegistro;
        private SAS_RegistroTicketCamaraGasificadoExonerados oRegistro;

        public ExonerarTicketACamaraDeGasificadoEdicion()
        {
            InitializeComponent();
        }

        public ExonerarTicketACamaraDeGasificadoEdicion(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_ListadoDeRegistrosExoneradosByDatesResult _documentDate)
        {
            InitializeComponent();
            Inicio();

            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            CargarCombos();
            documentDate = _documentDate;
            gbDocumento.Enabled = false;
            gbDatosDeTicket.Enabled = false;
            btnBarraPrincipal.Enabled = false;
            progressBar1.Visible = true;

            ConsultarRegistro(documentDate);


        }

        public void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["NSFAJA"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void CargarCombos()
        {
            try
            {
                model = new SAS_RegistroTicketCamaraGasificadoExoneradosController();
                cboMotivo.DisplayMember = "descripcion";
                cboMotivo.ValueMember = "valor";
                cboMotivo.DataSource = model.GetListOfDocuments(conection, "RegistroDeIngresoSalidaGasificadoEdicion").ToList();
                cboMotivo.SelectedValue = "000";


                model = new SAS_RegistroTicketCamaraGasificadoExoneradosController();
                cboSerie.DisplayMember = "descripcion";
                cboSerie.ValueMember = "valor";
                cboSerie.DataSource = model.GetSeries(conection, "RegistroDeIngresoSalidaGasificadoEdicion").ToList();
                cboSerie.SelectedValue = "2022";


                model = new SAS_RegistroTicketCamaraGasificadoExoneradosController();
                cboDocumento.DisplayMember = "descripcion";
                cboDocumento.ValueMember = "valor";
                cboDocumento.DataSource = model.GetDocument(conection, "RegistroDeIngresoSalidaGasificadoEdicion").ToList();
                cboDocumento.SelectedValue = "EXG";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void ConsultarRegistro(SAS_ListadoDeRegistrosExoneradosByDatesResult documentDate)
        {
            if (documentDate != null)
            {
                if (documentDate.codigoExoneracion != (int?)null)
                {
                    bgwHilo.RunWorkerAsync();
                }
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                model = new SAS_RegistroTicketCamaraGasificadoExoneradosController();
                document = new SAS_ListadoDeRegistrosExoneradosByIdResult();
                document = model.GetListById(conection, documentDate.codigoExoneracion);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }



        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (document != null)
                {
                    if (document.codigoExoneracion != null)
                    {
                        if (document.codigoExoneracion == 0)
                        {
                            #region Nuevo() 
                            txtUsuarioAsignado.Text = user2.IdCodigoGeneral.Trim() + " " + user2.NombreCompleto;
                            cboDocumento.SelectedValue = "EXG";
                            cboSerie.SelectedValue = "2022";
                            txtNumeroDocumento.Text = "0".PadLeft(7, '0');
                            txtFecha.Text = DateTime.Now.ToShortDateString();
                            cboMotivo.SelectedValue = "000";
                            txtTicket.Text = string.Empty;
                            txtTicketNumero.Text = string.Empty;
                            txtFechaRegistro.Text = DateTime.Now.ToShortDateString();
                            txtNota.Text = string.Empty;
                            txtCodigo.Text = "0";
                            this.txtEstado.Text = "PENDIENTE";
                            btnEditar.Enabled = false;
                            btnRegistrar.Enabled = true;
                            btnEliminarRegistro.Enabled = false;
                            btnAnular.Enabled = false;
                            btnExportarAExcel.Enabled = false;
                            btnNuevo.Enabled = true;
                            gbDatosDeTicket.Enabled = true;
                            gbDocumento.Enabled = true;
                            this.txtFecha.Enabled = true;
                            this.txtFechaRegistro.Text = DateTime.Now.ToString();
                            this.txtFecha.ReadOnly = false;
                            #endregion
                        }
                        else
                        {
                            #region Editar() 
                            txtUsuarioAsignado.Text = document.responsableRegistroExoneracion.Trim() + " " + document.responsableRegistroExoneracionNombres.Trim();
                            cboDocumento.SelectedValue = document.iddocumento != null ? document.iddocumento.Trim() : "EXG";
                            cboSerie.SelectedValue = document.serie != null ? document.serie.Trim() : DateTime.Now.Year.ToString();
                            txtNumeroDocumento.Text = document.codigoExoneracion.ToString().PadLeft(7, '0');
                            txtFecha.Text = document.fechaAcopio != null ? document.fechaAcopio.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                            cboMotivo.SelectedValue = document.codigoPorMotivoDeExoneracion != null ? document.codigoPorMotivoDeExoneracion.Trim() : "000";
                            txtTicket.Text = document.itemDetalle != null ? document.itemDetalle.Value.ToString().Trim() : string.Empty;
                            txtTicketNumero.Text = document.itemDetalle != null ? "Ticket numero : " + document.itemDetalle.Value.ToString().Trim() : string.Empty;
                            txtFechaRegistro.Text = document.fechaRegistroDocumentoExonerado != null ? document.fechaRegistroDocumentoExonerado.Value.ToString() : DateTime.Now.ToShortDateString();
                            txtNota.Text = document.comentarioDeExoneracion != null ? document.comentarioDeExoneracion.ToString().Trim() : string.Empty;
                            txtCodigo.Text = document.codigoExoneracion != null ? document.codigoExoneracion.ToString().Trim() : "0";
                            if (document.estadoDocumentoExonerado == 0)
                            {
                                this.txtEstado.Text = "ANULADO";
                            }
                            else if (document.estadoDocumentoExonerado == 1)
                            {
                                this.txtEstado.Text = "PENDIENTE";
                            }
                            else
                            {
                                this.txtEstado.Text = "APROBADO";
                            }
                            gbDatosDeTicket.Enabled = false;
                            gbDocumento.Enabled = false;
                            btnEditar.Enabled = true;
                            btnRegistrar.Enabled = false;
                            btnEliminarRegistro.Enabled = true;
                            btnAnular.Enabled = true;
                            btnExportarAExcel.Enabled = true;
                            btnNuevo.Enabled = true;
                            this.txtFecha.ReadOnly = true;
                            #endregion
                        }
                    }
                }
                btnBarraPrincipal.Enabled = true;
                gbDocumento.Enabled = !false;
                gbDatosDeTicket.Enabled = !false;
                progressBar1.Visible = !true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }



        }

        private void ExonerarTicketACamaraDeGasificadoEdicion_Load(object sender, EventArgs e)
        {

        }

        private void txtFecha_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                btnTicketBuscar.P_TablaConsulta = "SAS_TicketsPendientesDeLectura where fechaAcopio >= '" + this.txtFecha.Text.ToString() + " 00:00:00'" + " and fechaAcopio < '" + this.txtFecha.Text.ToString() + " 23:59:59'";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void cboMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnTicketBuscar.P_TablaConsulta = "SAS_TicketsPendientesDeLectura where fechaAcopio >= '" + this.txtFecha.Text.ToString() + " 00:00:00'" + " and fechaAcopio < '" + this.txtFecha.Text.ToString() + " 23:59:59'";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarRegistro() == true)
                {
                    oRegistro = new SAS_RegistroTicketCamaraGasificadoExonerados();
                    oRegistro = ObtenerObjeto();
                    model = new SAS_RegistroTicketCamaraGasificadoExoneradosController();
                    int resultadoRegistro = model.ToRegister(conection, oRegistro);
                    MessageBox.Show("El documento ha sido registrado correctamente", "Mensaje del sistema");
                    documentDate = new SAS_ListadoDeRegistrosExoneradosByDatesResult();
                    documentDate.codigoExoneracion = resultadoRegistro;
                    ConsultarRegistro(documentDate);
                    btnRegistrar.Enabled = false;
                    btnEditar.Enabled = true;
                }
                else
                {
                    MessageBox.Show(mensajeRegistro, "MENSAJE DEL SISTEMA");
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private SAS_RegistroTicketCamaraGasificadoExonerados ObtenerObjeto()
        {
            SAS_RegistroTicketCamaraGasificadoExonerados registro = new SAS_RegistroTicketCamaraGasificadoExonerados();
            registro.itemDDetalle = Convert.ToInt32(this.txtCodigo.Text);
            registro.itemDetalle = Convert.ToInt32(this.txtTicket.Text);
            registro.fechaRegistro = Convert.ToDateTime(this.txtFechaRegistro.Text);
            registro.cantidad = 0;
            registro.hora = Convert.ToDateTime(this.txtFechaRegistro.Text);
            registro.idMovil = "01";
            registro.idCamara = "999";
            registro.idusuario = user2.IdCodigoGeneral != null ? user2.IdCodigoGeneral : "soporte";
            registro.idmotivo = cboMotivo.SelectedValue.ToString().Trim();
            registro.glosa = this.txtNota.Text.Trim();
            registro.idestado = 1;
            return registro;
        }

        private bool ValidarRegistro()
        {
            bool resultado = true;
            mensajeRegistro = string.Empty;
            if (this.txtTicket.Text == string.Empty)
            {
                resultado = false;
                mensajeRegistro += "\nDebe ingresar un ticket valido ";
            }

            if (this.txtTicketNumero.Text == string.Empty)
            {
                resultado = false;
                mensajeRegistro += "\nDebe ingresar un ticket valido ";
            }

            if (this.cboMotivo.SelectedValue.ToString().Trim() == "000")
            {
                resultado = false;
                mensajeRegistro += "\nDebe ingresar un motivo valido ";
            }

            var resultadoFechaValidacion = txtCajaFechaValidad.Text;

            if (this.txtFechaRegistro.Text == resultadoFechaValidacion)
            {
                resultado = false;
                mensajeRegistro += "\nDebe ingresar una fecha de registro valida ";
            }

            if (this.txtEstado.Text.Trim().ToUpper() == "ANULADO" || this.txtEstado.Text.Trim().ToUpper() == "APROBADO")
            {
                resultado = false;
                mensajeRegistro += "\nEl documento no tiene estado para edición ";
            }

            return resultado;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            btnAtras.Enabled = true;
            btnRegistrar.Enabled = true;
            btnNuevo.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = !false;
            btnAtras.Enabled = !true;
            btnRegistrar.Enabled = !true;
            btnNuevo.Enabled = !false;
            btnAnular.Enabled = !false;
            btnEliminarRegistro.Enabled = !false;
        }

        private void txtFecha_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtFecha_Leave(object sender, EventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha;
                if (documentDate.fechaAcopio != null)
                {
                    if (!DateTime.TryParse(this.txtFecha.Text, out fecha))
                    {

                    }
                    else
                    {
                        if (documentDate.fechaAcopio.Value.ToShortDateString() != Convert.ToDateTime(this.txtFecha.Text).ToShortDateString())
                        {
                            this.txtTicket.Clear();
                            this.txtTicketNumero.Clear();
                            documentDate.fechaAcopio = Convert.ToDateTime(this.txtFecha.Text);
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
    }
}
