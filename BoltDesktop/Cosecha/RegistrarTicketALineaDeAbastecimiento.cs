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
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.Data;
using System.Drawing;
using MyControlsDataBinding.Controles;

namespace ComparativoHorasVisualSATNISIRA.Cosecha
{
    public partial class RegistrarTicketALineaDeAbastecimiento : Form
    {
        int codigoDeTicket = 0;
        private int esAgrupado = 0;
        private int codigoDispotivo = 0;
        private string conection = "SAS";
        private string companyId = "001";
        private string desde;
        private string hasta;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private List<Grupo> listadoDeLineascbo;
        private List<Grupo> listadoHoraCambioFormatoCbo;
        private List<Grupo> listadoFormatoEmpaqueCbo;
        private List<SAS_RegistroAbastecientoALineasDeProceso> listado;
        private SAS_OrdenProduccionLineasController modeloOrdenProduccion;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user2;
        private SAS_RegistroAbastecientoALineasDeProceso odetalleSelecionado;
        private SAS_RegistroDeAbastecimientoController model;
        private List<SAS_RegistroAbastecientoALineasDeProceso> listing; //Listado
        private List<SAS_RegistroAbastecientoALineasDeProceso> selectedList; // ListaSelecionada
        private SAS_RegistroAbastecientoALineasDeProceso selectedItem; // Item Selecionado
        private GlobalesHelper globalHelper;
        private MesController MesesNeg;
        private SAS_RegistroAbastecientoALineasDeProceso registro;
        private string mensajeAMostrar;

        public List<SAS_OrdenProduccionLineas> GetListAllOP { get; private set; }

        public RegistrarTicketALineaDeAbastecimiento()
        {
            InitializeComponent();
        }


        public RegistrarTicketALineaDeAbastecimiento(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoDeTicket, SAS_RegistroAbastecientoALineasDeProceso _registro)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            user2 = new SAS_USUARIOS();
            privilege = new PrivilegesByUser();
            registro = new SAS_RegistroAbastecientoALineasDeProceso();

            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoDeTicket = _codigoDeTicket;
            registro = _registro;

            gbAcciones.Enabled = false;
            gbDatosDelTicket.Enabled = false;
            gbLinea.Enabled = false;
            bgwHilo.RunWorkerAsync();
            //ObtenerFechasIniciales();
            //Actualizar();
        }


        public static void Limpiar(Control control, GroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }


        private void CargarDatosAControler(SAS_RegistroAbastecientoALineasDeProceso _registro)
        {
            Limpiar(this, gbAcciones);
            Limpiar(this, gbDatosDelTicket);
            cboLinea.SelectedValue = DateTime.Now.ToString("000");
            cboHoraCambioDeFormato.SelectedValue = DateTime.Now.ToString("000");
            cboFormatoDeEmpaque.SelectedValue = DateTime.Now.ToString("000");

            try
            {
                if (_registro != null)
                {
                    if (_registro.fechaAcopio != null)
                    {
                        this.txtGuiaDeRemision.Text = _registro.guiaDeRemision != null ? _registro.guiaDeRemision.Trim() : string.Empty;
                        this.txtTipoDeCultivo.Text = _registro.tipoDeCultivoDelConsumidor != null ? _registro.tipoDeCultivoDelConsumidor.Trim() : string.Empty;
                        this.txtlDocumentoAcopio.Text = _registro.documento != null ? _registro.documento.Trim() : string.Empty;
                        this.txtTicket.Text = _registro.idDetalle != null ? _registro.idDetalle.Value.ToString() : string.Empty;
                        this.txtItemDetalle.Text = _registro.item != null ? _registro.item.Trim() : string.Empty;
                        this.txtVariedad.Text = _registro.variedad != null ? _registro.variedad.Trim() : string.Empty;
                        this.txtFecha.Text = _registro.fechaAcopio != null ? _registro.fechaAcopio.ToPresentationDate().Trim() : string.Empty;
                        this.txtCantidad.Text = _registro.cantidadRegistrada != null ? _registro.cantidadRegistrada.Value.ToDecimalPresentation().Trim() : string.Empty;
                        this.txtLote.Text = _registro.idconsumidor != null ? _registro.idconsumidor.Trim() : string.Empty;
                        this.txtFechaRegistroAAbastecimiento.Text = DateTime.Now.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void CargarComboBox(SAS_RegistroAbastecientoALineasDeProceso _registro)
        {

            try
            {
                if (_registro != null)
                {
                    if (_registro.fechaAcopio != null)
                    {
                        #region Cargar Lineas que se cargaron en esa fecha()
                        modeloOrdenProduccion = new SAS_OrdenProduccionLineasController();
                        listadoDeLineascbo = new List<Grupo>();
                        listadoHoraCambioFormatoCbo = new List<Grupo>();
                        listadoFormatoEmpaqueCbo = new List<Grupo>();

                        listadoDeLineascbo = modeloOrdenProduccion.GetListByCbo(GetListAllOP);
                        cboLinea.DisplayMember = "Descripcion";
                        cboLinea.ValueMember = "Codigo";
                        cboLinea.DataSource = listadoDeLineascbo;
                        cboLinea.SelectedValue = DateTime.Now.ToString("000");
                        string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();

                        listadoHoraCambioFormatoCbo = modeloOrdenProduccion.GetListByHoraCambioCbo(codigoLineaSelecionado, GetListAllOP);
                        cboHoraCambioDeFormato.DisplayMember = "Descripcion";
                        cboHoraCambioDeFormato.ValueMember = "Codigo";
                        cboHoraCambioDeFormato.DataSource = listadoHoraCambioFormatoCbo;
                        cboHoraCambioDeFormato.SelectedValue = DateTime.Now.ToString("000");
                        string HoraSelecionada = cboHoraCambioDeFormato.SelectedValue.ToString().Trim();
                        DateTime? horaSelecionadaFormato = DateTime.Now;
                        if (HoraSelecionada != string.Empty)
                        {
                            if (HoraSelecionada != "000")
                            {
                                horaSelecionadaFormato = Convert.ToDateTime(HoraSelecionada);
                            }
                        }



                        listadoFormatoEmpaqueCbo = modeloOrdenProduccion.GetLisFormatoEmpaqueCbo(GetListAllOP, codigoLineaSelecionado, horaSelecionadaFormato);
                        cboFormatoDeEmpaque.DisplayMember = "Descripcion";
                        cboFormatoDeEmpaque.ValueMember = "Codigo";
                        cboFormatoDeEmpaque.DataSource = listadoFormatoEmpaqueCbo;
                        cboFormatoDeEmpaque.SelectedValue = DateTime.Now.ToString("000");

                        #endregion
                    }

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

        }

        private void CargarComboBox(SAS_RegistroAbastecientoALineasDeProceso _registro, List<SAS_OrdenProduccionLineas> GetListAllOP)
        {

            try
            {
                if (_registro != null)
                {
                    if (_registro.fechaAcopio != null)
                    {
                        #region Cargar Lineas que se cargaron en esa fecha()
                        modeloOrdenProduccion = new SAS_OrdenProduccionLineasController();
                        listadoDeLineascbo = new List<Grupo>();
                        listadoHoraCambioFormatoCbo = new List<Grupo>();
                        listadoFormatoEmpaqueCbo = new List<Grupo>();

                        listadoDeLineascbo = modeloOrdenProduccion.GetListByCbo(GetListAllOP);
                        cboLinea.DisplayMember = "Descripcion";
                        cboLinea.ValueMember = "Codigo";
                        cboLinea.DataSource = listadoDeLineascbo;
                        cboLinea.SelectedValue = DateTime.Now.ToString("000");
                        string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();

                        listadoHoraCambioFormatoCbo = modeloOrdenProduccion.GetListByHoraCambioCbo(codigoLineaSelecionado, GetListAllOP);
                        cboHoraCambioDeFormato.DisplayMember = "Descripcion";
                        cboHoraCambioDeFormato.ValueMember = "Codigo";
                        cboHoraCambioDeFormato.DataSource = listadoHoraCambioFormatoCbo;
                        cboHoraCambioDeFormato.SelectedValue = DateTime.Now.ToString("000");
                        string HoraSelecionada = cboHoraCambioDeFormato.SelectedValue.ToString().Trim();
                        DateTime? horaSelecionadaFormato = DateTime.Now;
                        if (HoraSelecionada != string.Empty)
                        {
                            if (HoraSelecionada != "000")
                            {
                                horaSelecionadaFormato = Convert.ToDateTime(HoraSelecionada);
                            }
                        }


                        listadoFormatoEmpaqueCbo = modeloOrdenProduccion.GetLisFormatoEmpaqueCbo(GetListAllOP, codigoLineaSelecionado, horaSelecionadaFormato);
                        cboFormatoDeEmpaque.DisplayMember = "Descripcion";
                        cboFormatoDeEmpaque.ValueMember = "Codigo";
                        cboFormatoDeEmpaque.DataSource = listadoFormatoEmpaqueCbo;
                        cboFormatoDeEmpaque.SelectedValue = DateTime.Now.ToString("000");

                        #endregion
                    }

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ValidarRegistro() == true)
                {
                    model = new SAS_RegistroDeAbastecimientoController();
                    RegistroAbastecimientoDDetalle oIngresoALineaProceso = new RegistroAbastecimientoDDetalle();
                    oIngresoALineaProceso.itemDetalle = Convert.ToInt32(this.txtTicket.Text); ;
                    oIngresoALineaProceso.fechaRegistro = Convert.ToDateTime(this.txtFechaRegistroAAbastecimiento.Text);
                    oIngresoALineaProceso.cantidad = Convert.ToDecimal(this.txtCantidad.Text);
                    oIngresoALineaProceso.hora = Convert.ToDateTime(this.txtFechaRegistroAAbastecimiento.Text);
                    oIngresoALineaProceso.idMovil = "001";
                    oIngresoALineaProceso.idLinea = cboLinea.SelectedValue.ToString();
                    oIngresoALineaProceso.esOrganico = this.txtTipoDeCultivo.Text.Trim().ToUpper() == "CONVENCIONAL" ? Convert.ToByte(0) : Convert.ToByte(1);
                    oIngresoALineaProceso.usuario = user2.IdUsuario != null ? user2.IdUsuario.Trim() : Environment.UserName.Trim();
                    oIngresoALineaProceso.host = Environment.MachineName.Trim();
                    oIngresoALineaProceso.idTicketReservado = 0;
                    oIngresoALineaProceso.idRegistroFormato = Convert.ToInt32(cboFormatoDeEmpaque.SelectedValue.ToString());
                    int ResultadoOperacion = model.RegistrarAbastecimientoAlineaDeProceso(conection, oIngresoALineaProceso);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
          
        }

        private bool ValidarRegistro()
        {
            mensajeAMostrar = string.Empty;
            bool validado = false;

            if (cboFormatoDeEmpaque.SelectedValue != null)
            {
                if (cboFormatoDeEmpaque.SelectedValue.ToString() != "00")
                {
                    validado = true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar los campos de formato de empaque", "Mensaje del sistema");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar los campos de formato de empaque", "Mensaje del sistema");
                return false;
            }



            if (cboLinea.SelectedValue != null)
            {
                if (cboLinea.SelectedValue.ToString() != "000")
                {
                    validado = true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar el campo línea de empaque", "Mensaje del sistema");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el campo línea de empaque", "Mensaje del sistema");
                return false;
            }




            if (cboHoraCambioDeFormato.SelectedValue != null)
            {
                if (cboHoraCambioDeFormato.SelectedValue.ToString() != "000")
                {
                    validado = true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar el campo hora de cambio", "Mensaje del sistema");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el campo hora de cambio", "Mensaje del sistema");
                return false;
            }

            var validarControlDeFecha = this.txtValidarFecha.Text;
            if (this.txtFechaRegistroAAbastecimiento.Text != validarControlDeFecha)
            {
                if (this.txtFechaRegistroAAbastecimiento.Text != string.Empty)
                {
                    validado = true;
                }
                else
                {
                    MessageBox.Show("Debe ingresar el campo fecha y hora del registro a abastecimiento con el formato correcto", "Mensaje del sistema");
                    return false;
                }


            }
            else
            {
                MessageBox.Show("Debe ingresar el campo fecha y hora del registro a abastecimiento con el formato correcto", "Mensaje del sistema");
                return false;
            }

            return validado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarTicketALineaDeAbastecimiento_Load(object sender, EventArgs e)
        {



        }

        private void cboLinea_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();
            listadoHoraCambioFormatoCbo = new List<Grupo>();
            listadoHoraCambioFormatoCbo = modeloOrdenProduccion.GetListByHoraCambioCbo(codigoLineaSelecionado, GetListAllOP);
            cboHoraCambioDeFormato.DisplayMember = "Descripcion";
            cboHoraCambioDeFormato.ValueMember = "Codigo";
            cboHoraCambioDeFormato.DataSource = listadoHoraCambioFormatoCbo;

        }

        private void cboHoraCambioDeFormato_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            listadoFormatoEmpaqueCbo = new List<Grupo>();
            modeloOrdenProduccion = new SAS_OrdenProduccionLineasController();
            string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();
            string HoraSelecionada = cboHoraCambioDeFormato.SelectedValue != null ? cboHoraCambioDeFormato.SelectedValue.ToString().Trim() : "000";
            DateTime? horaSelecionadaFormato = DateTime.Now;
            if (HoraSelecionada != string.Empty && HoraSelecionada != "000")
            {
                horaSelecionadaFormato = Convert.ToDateTime(HoraSelecionada);
            }

            listadoFormatoEmpaqueCbo = modeloOrdenProduccion.GetLisFormatoEmpaqueCbo(GetListAllOP, codigoLineaSelecionado, horaSelecionadaFormato);
            cboFormatoDeEmpaque.DisplayMember = "Descripcion";
            cboFormatoDeEmpaque.ValueMember = "Codigo";
            cboFormatoDeEmpaque.DataSource = listadoFormatoEmpaqueCbo;
            //cboFormatoDeEmpaque.SelectedValue = DateTime.Now.ToString("000");
        }

        private void cboFormatoDeEmpaque_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void cboLinea_KeyUp(object sender, KeyEventArgs e)
        {

            //string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();
            //listadoHoraCambioFormatoCbo = modeloOrdenProduccion.GetListByHoraCambioCbo(codigoLineaSelecionado, GetListAllOP);
            //cboHoraCambioDeFormato.DisplayMember = "Descripcion";
            //cboHoraCambioDeFormato.ValueMember = "Codigo";
            //cboHoraCambioDeFormato.DataSource = listadoHoraCambioFormatoCbo;
            //cboHoraCambioDeFormato.SelectedValue = DateTime.Now.ToString("000");
            //string HoraSelecionada = cboHoraCambioDeFormato.SelectedValue.ToString().Trim();
            //DateTime? horaSelecionadaFormato = DateTime.Now;
            //if (HoraSelecionada != string.Empty)
            //{
            //    if (HoraSelecionada != "000")
            //    {
            //        horaSelecionadaFormato = Convert.ToDateTime(HoraSelecionada);
            //    }
            //}
        }

        private void cboHoraCambioDeFormato_KeyUp(object sender, KeyEventArgs e)
        {
            //    string codigoLineaSelecionado = cboLinea.SelectedValue.ToString().Trim();

            //    string HoraSelecionada = cboHoraCambioDeFormato.SelectedValue.ToString().Trim();
            //    DateTime? horaSelecionadaFormato = DateTime.Now;
            //    if (HoraSelecionada != string.Empty)
            //    {
            //        if (HoraSelecionada != "000")
            //        {
            //            horaSelecionadaFormato = Convert.ToDateTime(HoraSelecionada);
            //        }
            //    }

            //    listadoFormatoEmpaqueCbo = modeloOrdenProduccion.GetLisFormatoEmpaqueCbo(GetListAllOP, codigoLineaSelecionado, horaSelecionadaFormato);
            //    cboFormatoDeEmpaque.DisplayMember = "Descripcion";
            //    cboFormatoDeEmpaque.ValueMember = "Codigo";
            //    cboFormatoDeEmpaque.DataSource = listadoFormatoEmpaqueCbo;
            //    cboFormatoDeEmpaque.SelectedValue = DateTime.Now.ToString("000");
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                GetListAllOP = new List<SAS_OrdenProduccionLineas>();
                modeloOrdenProduccion = new SAS_OrdenProduccionLineasController();
                GetListAllOP = modeloOrdenProduccion.GetListAllOP(conection, registro.fechaAcopio.ToString().Substring(0, 10));
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }


           
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                CargarComboBox(registro, GetListAllOP);
                CargarDatosAControler(registro);
                gbAcciones.Enabled = !false;
                gbDatosDelTicket.Enabled = !false;
                gbLinea.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }

        }
    }
}
