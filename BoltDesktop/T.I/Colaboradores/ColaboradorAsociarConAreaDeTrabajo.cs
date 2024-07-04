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

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ColaboradorAsociarConAreaDeTrabajo : Form
    {
        private string cadenaDeConexion;
        private SAS_USUARIOS usuarioAutenticado;
        private string EmpresaID;
        private PrivilegesByUser privilegioDeUsuario;
        private SAS_ListadoColaboradoresByDispositivo odetalleSelecionado;
        private SAS_ListadoDeLineasTelefonica detalle;
        private SAS_CuentasCorreoListado odetalleSelecionadoByFormularioEmail;
        private SAS_DispositivoUsuariosController modelo;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oItem;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult item;
        private SAS_ColaboradorAreaTrabajo oColaboradorPorAreaDetrabajo;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oColaboradorPorAreaDetrabajoResult;

        private string PersonalID;
        private List<SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalHistoricoResult> ListaHistorico;
        private string PlanillaID;
        private string ItemFechaPlanillaID;

        public string mensaje { get; private set; }

        public ColaboradorAsociarConAreaDeTrabajo()
        {
            InitializeComponent();
            Inicio();
            PersonalID = "405993";
            cadenaDeConexion = "SAS";
            usuarioAutenticado = new SAS_USUARIOS();
            usuarioAutenticado.IdUsuario = "EAURAZO";
            usuarioAutenticado.NombreCompleto = "ERICK AURAZO";
            EmpresaID = "001";
            privilegioDeUsuario = new PrivilegesByUser();
            privilegioDeUsuario.nuevo = 1;
            privilegioDeUsuario.editar = 1;
            privilegioDeUsuario.eliminar = 1;
            privilegioDeUsuario.anular = 1;
            privilegioDeUsuario.imprimir = 1;


            odetalleSelecionadoByFormularioEmail = new SAS_CuentasCorreoListado();
            odetalleSelecionadoByFormularioEmail.idcodigoGeneral = PersonalID;
            odetalleSelecionadoByFormularioEmail.nombresCompleto = string.Empty;

            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = odetalleSelecionadoByFormularioEmail.idcodigoGeneral != null ? odetalleSelecionadoByFormularioEmail.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = odetalleSelecionadoByFormularioEmail.nombresCompleto != null ? odetalleSelecionadoByFormularioEmail.nombresCompleto.Trim() : string.Empty;
            gbCabecera.Enabled = true;
            bgwHilo.RunWorkerAsync();
        }



        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_CuentasCorreoListado odetalleSelecionadoByFormularioEmail)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            this.odetalleSelecionadoByFormularioEmail = odetalleSelecionadoByFormularioEmail;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = odetalleSelecionadoByFormularioEmail.idcodigoGeneral != null ? odetalleSelecionadoByFormularioEmail.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = odetalleSelecionadoByFormularioEmail.nombresCompleto != null ? odetalleSelecionadoByFormularioEmail.nombresCompleto.Trim() : string.Empty;
            gbCabecera.Enabled = true;
            bgwHilo.RunWorkerAsync();
            //this.txtIdCodigoGeneral.Text = odetalleSelecionadoByFormularioEmail.idcodigoGeneral != null ? odetalleSelecionadoByFormularioEmail.idcodigoGeneral.Trim() : string.Empty;
            //this.txtNombres.Text = odetalleSelecionadoByFormularioEmail.nombresCompleto != null ? odetalleSelecionadoByFormularioEmail.nombresCompleto.Trim() : string.Empty;
            //this.txtGerenciaCodigo.Text = odetalleSelecionadoByFormularioEmail.idGerencia != null ? odetalleSelecionadoByFormularioEmail.idGerencia.ToString().Trim() : string.Empty;
            //this.txtGerencia.Text = odetalleSelecionadoByFormularioEmail.gerencia != null ? odetalleSelecionadoByFormularioEmail.gerencia.Trim() : string.Empty;
            //this.txtAreaCodigo.Text = odetalleSelecionadoByFormularioEmail.idarea != null ? odetalleSelecionadoByFormularioEmail.idarea.Trim() : string.Empty;
            //this.txtArea.Text = odetalleSelecionadoByFormularioEmail.area != null ? odetalleSelecionadoByFormularioEmail.area.Trim() : string.Empty;
            //item = new SAS_ColaboradorAreaTrabajo();
            //item.idCodigoGeneral = odetalleSelecionadoByFormularioEmail.idcodigoGeneral != null ? odetalleSelecionadoByFormularioEmail.idcodigoGeneral.Trim() : string.Empty;
            //gbPersonalArea.Enabled = true;
            //bgwHilo.RunWorkerAsync();
        }

        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_ListadoDeLineasTelefonica detalle)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            this.detalle = detalle;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = this.detalle.idcodigoGeneral != null ? detalle.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = this.detalle.nombres != null ? detalle.nombres.Trim() : string.Empty;
            gbCabecera.Enabled = true;
            bgwHilo.RunWorkerAsync();

            //this.txtIdCodigoGeneral.Text = detalle.idcodigoGeneral != null ? detalle.idcodigoGeneral.Trim() : string.Empty;
            //this.txtNombres.Text = detalle.nombres != null ? detalle.nombres.Trim() : string.Empty;
            //this.txtGerenciaCodigo.Text = detalle.idGerencia != null ? detalle.idGerencia.ToString().Trim() : string.Empty;
            //this.txtGerencia.Text = detalle.gerencia != null ? detalle.gerencia.Trim() : string.Empty;
            //this.txtAreaCodigo.Text = detalle.idArea != null ? detalle.idArea.Trim() : string.Empty;
            //this.txtArea.Text = detalle.area != null ? detalle.area.Trim() : string.Empty;

            //item = new SAS_ColaboradorAreaTrabajo();
            //item.idCodigoGeneral = detalle.idcodigoGeneral != null ? detalle.idcodigoGeneral.Trim() : string.Empty;
            //gbPersonalArea.Enabled = true;
            //bgwHilo.RunWorkerAsync();
        }


        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_ListadoColaboradoresByDispositivo odetalleSelecionado)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            this.odetalleSelecionado = odetalleSelecionado;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = this.odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            item.nombresCompletos = this.odetalleSelecionado.apenom != null ? odetalleSelecionado.apenom.Trim() : string.Empty;
            gbCabecera.Enabled = true;


            //this.txtIdCodigoGeneral.Text = this.odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            //this.txtNombres.Text = this.odetalleSelecionado.apenom != null ? odetalleSelecionado.apenom.Trim() : string.Empty;
            //this.txtGerenciaCodigo.Text = this.odetalleSelecionado.idGerencia != null ? odetalleSelecionado.idGerencia.ToString().Trim() : string.Empty;
            //this.txtGerencia.Text = this.odetalleSelecionado.gerencia != null ? odetalleSelecionado.gerencia.Trim() : string.Empty;
            //this.txtAreaCodigo.Text = this.odetalleSelecionado.idarea != null ? odetalleSelecionado.idarea.Trim() : string.Empty;
            //this.txtArea.Text = this.odetalleSelecionado.area != null ? odetalleSelecionado.area.Trim() : string.Empty;

            //item = new SAS_ColaboradorAreaTrabajo();
            //item.idCodigoGeneral = odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            //gbPersonalArea.Enabled = true;
            bgwHilo.RunWorkerAsync();

        }

        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, string idCodigoGeneral)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            PersonalID = idCodigoGeneral;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = idCodigoGeneral;
            item.nombresCompletos = idCodigoGeneral;
            Consultar();

        }

        private void Consultar()
        {
            gbCabecera.Enabled = false;
            gbDetalle.Enabled = false;
            BarraPrincipal.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_ColaboradorAreaTrabajo oColaboradorPorAreaDetrabajo)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            this.oColaboradorPorAreaDetrabajo = oColaboradorPorAreaDetrabajo;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            item.nombresCompletos = odetalleSelecionado.apenom != null ? odetalleSelecionado.apenom.Trim() : string.Empty;
            gbCabecera.Enabled = true;
            bgwHilo.RunWorkerAsync();

        }

        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oColaboradorPorAreaDetrabajo)
        {
            InitializeComponent();
            Inicio();
            this.cadenaDeConexion = _conection;
            this.usuarioAutenticado = _user2;
            this.EmpresaID = _companyId;
            this.privilegioDeUsuario = privilege;
            this.oColaboradorPorAreaDetrabajoResult = oColaboradorPorAreaDetrabajo;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = oColaboradorPorAreaDetrabajo.idcodigoGeneral != null ? oColaboradorPorAreaDetrabajo.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = oColaboradorPorAreaDetrabajo.nombresCompletos != null ? oColaboradorPorAreaDetrabajo.nombresCompletos.Trim() : string.Empty;
            gbCabecera.Enabled = true;
            bgwHilo.RunWorkerAsync();

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



        private void ColaboradorAsociarConAreaDeTrabajo_Load(object sender, EventArgs e)
        {
            if (this.txtPersonal.Text.Trim() != string.Empty && this.txtPersonalID.Text.Trim() != string.Empty)
            {
                PersonalID = txtPersonalID.Text.Trim();
                btnBuscarPlanilla.P_TablaConsulta = string.Concat("SAS_ListadoPlanillasPorTrabajadores where PersonalID = '", PersonalID, "'");
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarRegistroParaGrabar() == "OK")
            {
                modelo = new SAS_DispositivoUsuariosController();
                SAS_ColaboradorAreaTrabajo item = new SAS_ColaboradorAreaTrabajo();
                item.idCodigoGeneral = this.txtPersonalID.Text;
                item.idGerencia = this.txtGerenciaCodigo.Text != string.Empty ? Convert.ToInt32(this.txtGerenciaCodigo.Text.Trim()) : 0;
                item.idArea = this.txtAreaCodigo.Text != string.Empty ? (this.txtAreaCodigo.Text.Trim()) : string.Empty;
                item.EsGerente = 0;
                item.EsJefe = 0;

                if (chkEsGerente.Checked == true)
                {
                    item.EsGerente = 1;
                }


                if (chkEsJefe.Checked == true)
                {
                    item.EsJefe = 1;
                }
                string ASCD = this.txtValidar.Text.ToString().Trim();

                item.Desde = (DateTime?)null;
                item.Hasta = (DateTime?)null;
                if (this.txtActivoDesde.Text.ToString().Trim() != ASCD)
                {
                    item.Desde = Convert.ToDateTime(txtActivoDesde.Text.Trim());
                }

                if (this.txtActivoHasta.Text.ToString().Trim() != ASCD  )
                {
                    if (this.txtActivoHasta.Text.ToString().Trim() != string.Empty)
                    {
                        item.Hasta = Convert.ToDateTime(txtActivoHasta.Text.Trim());
                    }
                    
                }                
                item.Item = 0;
                item.CargoID = this.txtCargoID.Text;
                item.PlanillaID = this.txtPlanillaID.Text;
                item.ItemPlanilla = this.txtItemPlanillaID.Text;


                modelo.AsociarAAreaDeTrabajo("SAS", item);
                MessageBox.Show("Confirmación de la operción exitosa" , "MENSAJE DEL SISTEMA");

            }
            else
            {
                MessageBox.Show("La información registrada no se puede procesar, revisar los items ingresados y selecionados" + mensaje, "MENSAJE DEL SISTEMA");
            }




        }

        private string ValidarRegistroParaGrabar()
        {
            mensaje = "OK";
            string ASCD = this.txtValidar.Text.ToString().Trim();


            /*Validar caja de texto de personal*/
            if (this.txtPersonalID.Text == String.Empty || (this.txtPersonal.Text == String.Empty))
            {
                mensaje += "\n Debe Ingresar código de Empleado con sus nombres correctos";
            }

            /*Validar caja de texto de Planilla*/
            if (this.txtPlanillaID.Text == String.Empty || (this.txtPlanilla.Text == String.Empty))
            {
                mensaje += "\n Debe Ingresar ingresar una planilla para el colaborador";
            }

            if (this.txtPlanilaDesde.Text.ToString().Trim() != ASCD)
            {
                if (this.txtPlanilaDesde.Text != null)
                {
                    if (this.txtPlanilaDesde.Text.ToString().Trim() == string.Empty)
                    {
                        mensaje += "\n Las fechas de activación de está planilla deben estar en un rango válido";
                    }
                }
            }
            else
            {
                mensaje += "\n Las fechas de activación de está planilla deben estar en un rango válido";
            }


            /*Validar caja de texto de ItemPlanilla*/
            if (this.txtItemPlanillaID.Text == String.Empty || (this.txtItemPlanilla.Text == String.Empty))
            {
                mensaje += "\n la planilla seleccionada debe tener un item de planilla válido";
            }

            if (this.txtItemPlanillaDesde.Text.ToString().Trim() != ASCD)
            {
                if (this.txtItemPlanillaDesde.Text != null)
                {
                    if (this.txtItemPlanillaDesde.Text.ToString().Trim() == string.Empty)
                    {
                        mensaje += "\n Las fechas de activación del item planilla deben estar en un rango válido";
                    }
                }
            }
            else
            {
                mensaje += "\n Las fechas de activación del item planilla deben estar en un rango válido";
            }




            /*Validar caja de texto de Gerencia*/
            if (this.txtGerenciaCodigo.Text == String.Empty || (this.txtGerencia.Text == String.Empty))
            {
                mensaje += "\n Debe ingresar una gerencia válida";
            }

            /*Validar caja de texto de Area*/
            if (this.txtAreaCodigo.Text == String.Empty || (this.txtArea.Text == String.Empty))
            {
                mensaje += "\n Debe ingresar un área de trabajo válida";
            }

            /*Validar caja de texto de Cargo*/
            if (this.txtCargoID.Text == String.Empty || (this.txtCargo.Text == String.Empty))
            {
                mensaje += "\n Debe ingresar un cargo válido";
            }



            if (this.txtActivoDesde.Text.ToString().Trim() != ASCD)
            {
                if (this.txtActivoDesde.Text != null)
                {
                    if (this.txtActivoDesde.Text.ToString().Trim() == string.Empty)
                    {
                        mensaje += "\n Las fechas de activación de está planilla deben estar en un rango válido";
                    }
                }
            }





            return mensaje;
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            oItem = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            modelo = new SAS_DispositivoUsuariosController();
            oItem = modelo.ObtenerDatosDeAsignacionPorAreayGerenciaDeColaboradorPorCodigoEmpleado("SAS", item);

            ListaHistorico = new List<SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalHistoricoResult>();
            ListaHistorico = modelo.ObtenerListadoHistoricoEnPlanillas(cadenaDeConexion, PersonalID);
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            txtPersonalID.Text = string.Empty;
            txtPersonal.Text = string.Empty;
            txtGerenciaCodigo.Text = string.Empty;
            txtGerencia.Text = string.Empty;
            txtAreaCodigo.Text = string.Empty;
            txtArea.Text = string.Empty;
            chkEsGerente.Checked = false;
            chkEsJefe.Checked = false;

            if (oItem != null)
            {
                chkEsGerente.Checked = false;
                chkEsJefe.Checked = false;
                if (oItem.idcodigoGeneral != null)
                {
                    txtPersonalID.Text = oItem.idcodigoGeneral != null ? oItem.idcodigoGeneral.Trim() : string.Empty;
                    txtPersonal.Text = oItem.nombresCompletos != null ? oItem.nombresCompletos.Trim() : string.Empty;
                    txtGerenciaCodigo.Text = oItem.idGerencia != null ? oItem.idGerencia.ToString().Trim() : string.Empty;
                    txtGerencia.Text = oItem.gerencia != null ? oItem.gerencia.Trim() : string.Empty;
                    txtAreaCodigo.Text = oItem.idArea != null ? oItem.idArea.Trim() : string.Empty;
                    txtArea.Text = oItem.area != null ? oItem.area.Trim() : string.Empty;

                    this.txtPlanilaDesde.Text = oItem.PlanillaInicio != null ? oItem.PlanillaInicio.Value.ToShortDateString() : string.Empty;
                    this.txtPlanilaHasta.Text = oItem.PlanillaFin != null ? oItem.PlanillaFin.Value.ToShortDateString() : string.Empty;

                    this.txtPlanillaID.Text = oItem.PlanillaID != null ? oItem.PlanillaID.Trim() : string.Empty;
                    this.txtPlanilla.Text = oItem.Planilla != null ? oItem.Planilla.Trim() : string.Empty;

                    this.txtItemPlanillaDesde.Text = oItem.ItemPlanillaInicio != null ? oItem.ItemPlanillaInicio.Value.ToShortDateString() : string.Empty;
                    this.txtItemPlanillaHasta.Text = oItem.ItemPlanillaFin != null ? oItem.ItemPlanillaFin.Value.ToShortDateString() : string.Empty;

                    txtItemPlanillaID.Text = oItem.ItemPlanilla != null ? oItem.ItemPlanilla.Trim() : string.Empty;
                    txtItemPlanilla.Text = oItem.ItemPlanilla != null ? "Item: " + oItem.ItemPlanilla.Trim() : string.Empty;

                    this.txtCargoID.Text = oItem.CargoID != null ? oItem.CargoID.Trim() : string.Empty;
                    this.txtCargo.Text = oItem.Cargo != null ? oItem.Cargo.Trim() : string.Empty;

                    this.txtActivoDesde.Text = oItem.Desde != null ? oItem.Desde.Value.ToShortDateString() : string.Empty;
                    this.txtActivoHasta.Text = oItem.hasta != null ? oItem.hasta.Value.ToShortDateString() : string.Empty;

                    if (oItem.EsGerente != (decimal?)null)
                    {
                        if (oItem.EsGerente == 1)
                        {
                            chkEsGerente.Checked = true;
                        }

                    }
                    if (oItem.EsJefe != (decimal?)null)
                    {
                        if (oItem.EsJefe == 1)
                        {
                            chkEsJefe.Checked = true;
                        }

                    }

                }
            }

            if (this.txtPersonal.Text.Trim() != string.Empty && this.txtPersonalID.Text.Trim() != string.Empty)
            {
                PersonalID = txtPersonalID.Text.Trim();
                btnBuscarPlanilla.P_TablaConsulta = string.Concat("SAS_ListadoPlanillasPorTrabajadores where PersonalID = '", PersonalID, "'");
            }

            dgvRegistro.DataSource = ListaHistorico.ToDataTable<SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalHistoricoResult>();
            dgvRegistro.Refresh();

            gbCabecera.Enabled = true;
            gbDetalle.Enabled = true;
            BarraPrincipal.Enabled = true;
            progressBar.Visible = true;
            txtActivoHasta.Focus();

        }

        private void commandBarButton5_Click(object sender, EventArgs e)
        {
        }

        private void btnBuscarPersonal_Leave(object sender, EventArgs e)
        {
            if (txtPersonalID.Text.Trim() != string.Empty || txtPersonal.Text.Trim() != string.Empty)
            {
                btnBuscarPlanilla.P_TablaConsulta = string.Concat("SAS_ListadoPlanillasPorTrabajadores where PersonalID = '", PersonalID, "'");
                btnBuscarPlanilla.Enabled = false;
                txtPlanilla.ReadOnly = true;
                txtPlanillaID.ReadOnly = true;
                btnBuscarItemPlanilla.Enabled = false;
                txtItemPlanillaID.ReadOnly = true;
                txtItemPlanilla.ReadOnly = true;
            }
        }

        private void btnBuscarPlanilla_Leave(object sender, EventArgs e)
        {

        }

        private void btnBuscarItemPlanilla_Leave(object sender, EventArgs e)
        {

        }

        private void txtIdCodigoGeneral_Leave(object sender, EventArgs e)
        {

        }

        private void txtNombres_Leave(object sender, EventArgs e)
        {

        }

        private void txtPlanillaCodigo_Leave(object sender, EventArgs e)
        {
            AsignarTablaDeBusquedaParaItemPlanilla();

        }

        private void txtPlanilla_Leave(object sender, EventArgs e)
        {
            AsignarTablaDeBusquedaParaItemPlanilla();
        }

        private void txtItemPlanillaID_Leave(object sender, EventArgs e)
        {

        }

        private void txtItemPlanilla_Leave(object sender, EventArgs e)
        {

        }

        private void btnLlenarFechas_Click(object sender, EventArgs e)
        {
            if (this.txtPlanillaID.Text != string.Empty && this.txtPlanilla.Text.Trim() != string.Empty)
            {
                PlanillaID = this.txtPlanillaID.Text.Trim();
                LlenarCampoDeFechas();
            }

            if (this.txtItemPlanillaID.Text != string.Empty && this.txtItemPlanilla.Text.Trim() != string.Empty)
            {
                ItemFechaPlanillaID = this.txtPlanillaID.Text.Trim();
                LlenarCampoDeItemPlanilla();
            }

        }

        private void LlenarCampoDeFechas()
        {
            string FechaDePlanillaDesdePlanilla = modelo.ObtenerFechaDePlanillaDesdePlanilla(cadenaDeConexion, PersonalID, PlanillaID);
            txtPlanilaDesde.Text = FechaDePlanillaDesdePlanilla;
        }

        private void LlenarCampoDeItemPlanilla()
        {
            string FechaDeItemPlanilla = modelo.ObtenerFechaItemPlanillaDesdePersonalID(cadenaDeConexion, PersonalID);
            txtItemPlanillaDesde.Text = FechaDeItemPlanilla;
        }

        private void txtGerenciaCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAreaCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPersonalID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPlanillaID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemPlanillaID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPersonal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPlanilla_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemPlanilla_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscarPlanilla_Click(object sender, EventArgs e)
        {

        }

        private void txtPlanillaID_KeyUp(object sender, KeyEventArgs e)
        {
            AsignarTablaDeBusquedaParaItemPlanilla();
        }

        private void AsignarTablaDeBusquedaParaItemPlanilla()
        {
            btnBuscarItemPlanilla.P_TablaConsulta = string.Concat("SAS_ListadoItemPorTrabajadores where PersonalID = ''");
            if (txtPersonalID.Text.Trim() != string.Empty && txtPersonal.Text.Trim() != string.Empty && txtPlanillaID.Text.Trim() != string.Empty && txtPlanilla.Text.Trim() != string.Empty)
            {
                btnBuscarItemPlanilla.P_TablaConsulta = string.Concat("SAS_ListadoItemPorTrabajadores where PersonalID = '", PersonalID, "'");
            }
        }

        private void txtPlanilla_KeyUp(object sender, KeyEventArgs e)
        {
            AsignarTablaDeBusquedaParaItemPlanilla();
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {

        }
    }
}
