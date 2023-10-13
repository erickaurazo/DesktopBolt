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
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoColaboradoresByDispositivo odetalleSelecionado;
        private SAS_ListadoDeLineasTelefonica detalle;
        private SAS_CuentasCorreoListado odetalleSelecionadoByFormularioEmail;
        private SAS_DispositivoUsuariosController modelo;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oItem;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult item;
        private SAS_ColaboradorAreaTrabajo oColaboradorPorAreaDetrabajo;
        private SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oColaboradorPorAreaDetrabajoResult;

        private string _idCodigoGeneral;

        public ColaboradorAsociarConAreaDeTrabajo()
        {
            InitializeComponent();
            Inicio();
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = string.Empty;
            gbPersonalArea.Enabled = true;
            bgwHilo.RunWorkerAsync();
        }



        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_CuentasCorreoListado odetalleSelecionadoByFormularioEmail)
        {
            InitializeComponent();
            Inicio();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.odetalleSelecionadoByFormularioEmail = odetalleSelecionadoByFormularioEmail;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = odetalleSelecionadoByFormularioEmail.idcodigoGeneral != null ? odetalleSelecionadoByFormularioEmail.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = odetalleSelecionadoByFormularioEmail.nombresCompleto != null ? odetalleSelecionadoByFormularioEmail.nombresCompleto.Trim() : string.Empty;
            gbPersonalArea.Enabled = true;
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
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.detalle = detalle;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = this.detalle.idcodigoGeneral != null ? detalle.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = this.detalle.nombres != null ? detalle.nombres.Trim() : string.Empty;
            gbPersonalArea.Enabled = true;
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
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.odetalleSelecionado = odetalleSelecionado;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = this.odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            item.nombresCompletos = this.odetalleSelecionado.apenom != null ? odetalleSelecionado.apenom.Trim() : string.Empty;
            gbPersonalArea.Enabled = true;
            

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
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;            
            _idCodigoGeneral = idCodigoGeneral;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = idCodigoGeneral;
            item.nombresCompletos = idCodigoGeneral;
            gbPersonalArea.Enabled = true;
            bgwHilo.RunWorkerAsync();

        }



        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_ColaboradorAreaTrabajo oColaboradorPorAreaDetrabajo)
        {
            InitializeComponent();
            Inicio();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.oColaboradorPorAreaDetrabajo = oColaboradorPorAreaDetrabajo;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = odetalleSelecionado.idcodigogeneral != null ? odetalleSelecionado.idcodigogeneral.Trim() : string.Empty;
            item.nombresCompletos = odetalleSelecionado.apenom != null ? odetalleSelecionado.apenom.Trim() : string.Empty;
            gbPersonalArea.Enabled = true;
            bgwHilo.RunWorkerAsync();

        }

        public ColaboradorAsociarConAreaDeTrabajo(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oColaboradorPorAreaDetrabajo)
        {
            InitializeComponent();
            Inicio();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.oColaboradorPorAreaDetrabajoResult = oColaboradorPorAreaDetrabajo;
            item = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            item.idcodigoGeneral = oColaboradorPorAreaDetrabajo.idcodigoGeneral != null ? oColaboradorPorAreaDetrabajo.idcodigoGeneral.Trim() : string.Empty;
            item.nombresCompletos = oColaboradorPorAreaDetrabajo.nombresCompletos != null ? oColaboradorPorAreaDetrabajo.nombresCompletos.Trim() : string.Empty;
            gbPersonalArea.Enabled = true;
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

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {


            if (this.txtIdCodigoGeneral.Text != String.Empty)
            {
                modelo = new SAS_DispositivoUsuariosController();
                SAS_ColaboradorAreaTrabajo item = new SAS_ColaboradorAreaTrabajo();
                item.idCodigoGeneral = this.txtIdCodigoGeneral.Text;
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

                modelo.AsociarAAreaDeTrabajo("SAS", item);
                MessageBox.Show("Reigstro actualizado correctamente", "MENSAJE DEL SISTEMA");
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            oItem = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            modelo = new SAS_DispositivoUsuariosController();
            oItem = modelo.ObtenerDatosDeAsignacionPorAreayGerenciaDeColaboradorPorCodigoEmpleado("SAS", item);
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            txtIdCodigoGeneral.Text = string.Empty;
            txtNombres.Text = string.Empty;
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
                    txtIdCodigoGeneral.Text = oItem.idcodigoGeneral != null ? oItem.idcodigoGeneral.Trim(): string.Empty;
                    txtNombres.Text = oItem.nombresCompletos != null ? oItem.nombresCompletos.Trim() : string.Empty;
                    txtGerenciaCodigo.Text = oItem.idGerencia != null ? oItem.idGerencia.ToString().Trim() : string.Empty;
                    txtGerencia.Text = oItem.gerencia != null ? oItem.gerencia.Trim() : string.Empty;
                    txtAreaCodigo.Text = oItem.idArea != null ? oItem.idArea.Trim() : string.Empty;
                    txtArea.Text =  oItem.area != null ? oItem.area.Trim() : string.Empty;


                    if (oItem.EsGerente != null)
                    {
                        if (oItem.EsGerente == 1)
                        {
                            chkEsGerente.Checked = true;
                        }

                    }
                    if (oItem.EsJefe != null)
                    {
                        if (oItem.EsJefe == 1)
                        {
                            chkEsJefe.Checked = true;
                        }

                    }

                }
            }

            gbPersonalArea.Enabled = true;

        }
    }
}
