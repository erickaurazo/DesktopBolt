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

namespace ComparativoHorasVisualSATNISIRA.Calidad
{
    public partial class Calidades : Form
    {
        private int periodo;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user;
        private string companyId;
        private string conection;
        private SAS_CalidadFrutaVariedadListadoAll selectedItem;
        private List<SAS_CalidadFrutaVariedadListadoAll> result;
        SAS_CalidadFrutaVariedadController model;

        public string MensajeError { get; private set; }
        public string AccionDeconsuta { get; private set; }

        public Calidades()
        {
            InitializeComponent();
            LimparFormulario(gbEdit);
            conection = "SAS";
            user = new SAS_USUARIOS();
            user.IdUsuario = "EAURAZO";
            user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            companyId = "001";
            privilege = new PrivilegesByUser();
            privilege.anular = 1;
            privilege.consultar = 1;

            Inicio();
            lblCodeUser.Text = user.IdUsuario;
            lblFullName.Text = user.NombreCompleto;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            AccionDeconsuta = "C";
            Consult();
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

        }

        public Calidades(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            LimparFormulario(gbEdit);
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            Inicio();
            lblCodeUser.Text = user.IdUsuario != null ? user.IdUsuario.Trim() : string.Empty;
            lblFullName.Text = user.NombreCompleto != null ? user.NombreCompleto.Trim() : string.Empty;

            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            AccionDeconsuta = "C";
            Consult();
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;


        }

        private void Consult()
        {
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            BarraPrincipal.Enabled = false;
            pgBar.Visible = true;

            bgwHilo.RunWorkerAsync();
        }

        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings[conection].ToString();
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



        private void Calidades_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimparFormulario(gbEdit);
            this.txtEstado.Text = "ACTIVO";
            this.txtIdEstado.Text = "1";
            this.txtEmpresaCodigo.Text = "001";
            this.txtEmpresa.Text = "SOCIEDAD AGRÍCOLA SATURNO SA";
            this.txtVariedad.Focus();
            this.txtDesde.Text = "01/09/2022";
            this.txtHasta.Text = "31/01/2023";
            this.txtTipoCultivoCodigo.Text = "C";


            this.txtCalidad.Enabled = true;
            this.txtCalidadCodigo.Enabled = true;
            this.btnCalidad.Enabled = true;

            this.txtEstado.Enabled = true;
            this.txtIdEstado.Enabled = true;


            this.txtEmpresa.Enabled = false;
            this.txtEmpresaCodigo.Enabled = false;
            this.btnEmpresaBuscar.Enabled = false;


            this.txtCultivo.Enabled = true;
            this.txtCultivoCodigo.Enabled = true;
            this.btnCultivoBuscar.Enabled = true;

            this.txtVariedad.Enabled = true;
            this.txtVariedadCodigo.Enabled = true;
            this.btnVariedadBuscar.Enabled = true;

            this.txtTipoCultivo.Enabled = true;
            this.txtTipoCultivoCodigo.Enabled = true;
            this.btnTipoCultivoBuscar.Enabled = true;


            this.txtDesde.Enabled = true;
            this.txtHasta.Enabled = true;
            this.txtDescripcion.Enabled = true;
            this.txtGlosa.Enabled = true;
            this.txtNota.Enabled = true;

            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = false;
            btnGrabar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = true;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
            gbEdit.Enabled = true;
            gbList.Enabled = false;

        }

        private void LimparFormulario(Control control)
        {
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is RadTextBox)
                {
                    ((RadTextBox)txt).Clear();
                }
                if (txt is MyDataGridViewMaskedTextEditingControl)
                {
                    ((MyDataGridViewMaskedTextEditingControl)txt).Clear();
                }

            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            result = new List<SAS_CalidadFrutaVariedadListadoAll>();
            model = new SAS_CalidadFrutaVariedadController();

            try
            {
                result = model.ListadoVariedadesPorCultivoViewAll(conection);
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
                dgvRegistro.DataSource = result.ToDataTable<SAS_CalidadFrutaVariedadListadoAll>();
                dgvRegistro.Refresh();

                if (AccionDeconsuta == "C")
                {
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                }

                BarraPrincipal.Enabled = true;
                pgBar.Visible = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consult();
            AccionDeconsuta = "C";
            btnCalidad.Enabled = true;
            BarraPrincipal.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
            gbEdit.Enabled = false;
            gbList.Enabled = true;


        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            ToRegister();
        }

        private void ToRegister()
        {

            try
            {
                if (Validar() == true)
                {
                    SAS_CalidadFrutaVariedad itemARegistrar = new SAS_CalidadFrutaVariedad();
                    itemARegistrar = AsingarObjeto();
                    model = new SAS_CalidadFrutaVariedadController();
                    AccionDeconsuta = "R";
                    int registrarOperacion = model.ToRegister(conection, itemARegistrar);
                    Consult();
                    btnNuevo.Enabled = true;
                    btnActualizar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnGrabar.Enabled = false;
                    btnRegistrar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnAtras.Enabled = false;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnHistorial.Enabled = true;
                    btnFlujoAprobacion.Enabled = false;
                    btnAdjuntar.Enabled = true;
                    btnNotificar.Enabled = true;
                    btnCerrar.Enabled = true;
                    gbEdit.Enabled = false;
                    gbList.Enabled = true;
                }
                else
                {
                    MessageBox.Show(MensajeError, "Mensaje del sistema");
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private SAS_CalidadFrutaVariedad AsingarObjeto()
        {
            SAS_CalidadFrutaVariedad item = new SAS_CalidadFrutaVariedad();
            item.idEmpresa = this.txtEmpresaCodigo.Text.Trim();
            item.idCultivo = this.txtCultivoCodigo.Text.Trim();
            item.idVariedad = this.txtVariedadCodigo.Text.Trim();
            item.tipoCultivo = Convert.ToChar(this.txtTipoCultivoCodigo.Text.Trim());
            item.idCalidad = this.txtCalidadCodigo.Text.Trim();
            item.descripcion = this.txtDescripcion.Text.Trim();
            item.descripcion2 = this.txtGlosa.Text.Trim();
            item.descripcion3 = this.txtNota.Text.Trim();
            item.desde = Convert.ToDateTime(this.txtDesde.Text.Trim());
            item.hasta = Convert.ToDateTime(this.txtHasta.Text.Trim());
            item.estado = Convert.ToInt32(this.txtIdEstado.Text.Trim());
            return item;
        }

        private bool Validar()
        {
            MensajeError = string.Empty;
            bool resultado = true;
            if (this.txtEmpresaCodigo.Text == string.Empty)
            {
                MensajeError += "Ingresar código de empresa \n";
                //return false;
            }
            if (this.txtCultivoCodigo.Text == string.Empty)
            {
                MensajeError += "Ingresar código de cultivo \n";
                //return false;
            }

            if (this.txtVariedadCodigo.Text == string.Empty)
            {
                MensajeError += "Ingresar código de variedad \n";
                //return false;
            }


            if (this.txtTipoCultivoCodigo.Text == string.Empty)
            {
                MensajeError += "Ingresar código de tipo de cultivo \n";
                //return false;
            }

            string ASCD = this.txtValidar.Text.ToString().Trim();

            if (this.txtDesde.Text.ToString().Trim() == ASCD)
            {
                MensajeError += "Ingresar fecha de inicio en el formato correcto \n";
                //return false;
            }

            if (this.txtHasta.Text.ToString().Trim() == ASCD)
            {
                MensajeError += "Ingresar fecha de finalización en el formato correcto \n";
                //return false;
            }

            if (this.txtDescripcion.Text == string.Empty)
            {
                MensajeError += "Ingresar una descripción válida \n";
                //return false;
            }

            if (this.txtDescripcion.Text.Length < 10)
            {
                MensajeError += "Ingresar una descripción válida \n";
                //return false;
            }

            if (MensajeError.Length > 0)
            {
                resultado = false;
            }

            return resultado;
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = true;
            btnCalidad.Enabled = true;
            btnActualizar.Enabled = false;
            btnEditar.Enabled = false;
            btnGrabar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;
            gbEdit.Enabled = true;
            gbList.Enabled = false;

            this.txtCalidad.Enabled = false;
            this.txtCalidadCodigo.Enabled = false;
            this.btnCalidadBuscar.Enabled = false;

            this.txtEstado.Enabled = false;
            this.txtIdEstado.Enabled = false;


            this.txtEmpresa.Enabled = false;
            this.txtEmpresaCodigo.Enabled = false;
            this.btnEmpresaBuscar.Enabled = false;


            this.txtCultivo.Enabled = false;
            this.txtCultivoCodigo.Enabled = false;
            this.btnCultivoBuscar.Enabled = false;

            this.txtVariedad.Enabled = false;
            this.txtVariedadCodigo.Enabled = false;
            this.btnVariedadBuscar.Enabled = false;

            this.txtTipoCultivo.Enabled = false;
            this.txtTipoCultivoCodigo.Enabled = false;
            this.btnTipoCultivoBuscar.Enabled = false;


            this.txtDesde.Enabled = false;
            this.txtHasta.Enabled = true;
            this.txtDescripcion.Enabled = true;
            this.txtGlosa.Enabled = true;
            this.txtNota.Enabled = true;

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ToRegister();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            AccionDeconsuta = "R";
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            Consult();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void ChangeState()
        {



            try
            {
                if (Validar() == true)
                {
                    SAS_CalidadFrutaVariedad itemARegistrar = new SAS_CalidadFrutaVariedad();
                    itemARegistrar = AsingarObjeto();
                    model = new SAS_CalidadFrutaVariedadController();
                    AccionDeconsuta = "C";
                    int registrarOperacion = model.ChangeState(conection, itemARegistrar);                    
                    btnNuevo.Enabled = true;
                    btnActualizar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnGrabar.Enabled = false;
                    btnRegistrar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnAtras.Enabled = false;
                    btnAnular.Enabled = true;
                    btnEliminarRegistro.Enabled = true;
                    btnHistorial.Enabled = true;
                    btnFlujoAprobacion.Enabled = false;
                    btnAdjuntar.Enabled = true;
                    btnNotificar.Enabled = true;
                    btnCerrar.Enabled = true;
                    Consult();
                }
                else
                {
                    MessageBox.Show(MensajeError, "Mensaje del sistema");
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void Atras()
        {
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEditar.Enabled = true;
            btnGrabar.Enabled = false;
            btnRegistrar.Enabled = false;
            btnCancelar.Enabled = true;
            btnAtras.Enabled = false;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnHistorial.Enabled = true;
            btnFlujoAprobacion.Enabled = false;
            btnAdjuntar.Enabled = true;
            btnNotificar.Enabled = true;
            btnCerrar.Enabled = true;

            gbList.Enabled = true;
            gbEdit.Enabled = false;
        }

        private void txtCultivoCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            btnVariedadBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
            btnCalidadBuscar.P_TablaConsulta = "CULTIVOCALIDAD where estado = 1 AND INCLUIR_DISTR = 1 AND idempresa = '" + this.txtEmpresaCodigo.Text.Trim() + "' and idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
        }

        private void txtCultivo_KeyUp(object sender, KeyEventArgs e)
        {
            btnVariedadBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
            btnCalidadBuscar.P_TablaConsulta = "CULTIVOCALIDAD where estado = 1 AND INCLUIR_DISTR = 1 AND idempresa = '" + this.txtEmpresaCodigo.Text.Trim() + "' and idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
        }

        private void btnCultivoBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            btnVariedadBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
            btnCalidadBuscar.P_TablaConsulta = "CULTIVOCALIDAD where estado = 1 AND INCLUIR_DISTR = 1 AND idempresa = '" + this.txtEmpresaCodigo.Text.Trim() + "' and idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
        }

        private void btnVariedadBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            btnTipoCultivoBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "' and idvariedad = '" + this.txtVariedadCodigo.Text.Trim() + "'";
        }

        private void txtVariedadCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            btnTipoCultivoBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "' and idvariedad = '" + this.txtVariedadCodigo.Text.Trim() + "'";
        }

        private void txtVariedad_KeyUp(object sender, KeyEventArgs e)
        {
            btnTipoCultivoBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "' and idvariedad = '" + this.txtVariedadCodigo.Text.Trim() + "'";
        }

        private void txtTipoCultivo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnCultivoBuscar_Click(object sender, EventArgs e)
        {
            btnVariedadBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
            btnCalidadBuscar.P_TablaConsulta = "CULTIVOCALIDAD where estado = 1 AND INCLUIR_DISTR = 1 AND idempresa = '" + this.txtEmpresaCodigo.Text.Trim() + "' and idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "'";
        }

        private void btnVariedadBuscar_Click(object sender, EventArgs e)
        {
            btnTipoCultivoBuscar.P_TablaConsulta = "SAS_ListadoVariedadPorTipoCultivo where idcultivo = '" + this.txtCultivoCodigo.Text.Trim() + "' and idvariedad = '" + this.txtVariedadCodigo.Text.Trim() + "'";
        }

        private void btnTipoCultivoBuscar_Click(object sender, EventArgs e)
        {

        }

        private void Calidades_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region Selecionar item
                selectedItem = new SAS_CalidadFrutaVariedadListadoAll();
                selectedItem.idEmpresa = string.Empty;
                selectedItem.idCultivo = string.Empty;
                selectedItem.idVariedad = string.Empty;
                selectedItem.tipoCultivoId = '\0';
                selectedItem.idCalidad = string.Empty;
                selectedItem.descripcion = string.Empty;
                selectedItem.descripcion2 = string.Empty;
                selectedItem.descripcion3 = string.Empty;
                selectedItem.Cultivo = string.Empty;
                selectedItem.Variedad = string.Empty;
                selectedItem.tipocultivo = string.Empty;
                selectedItem.desde = (DateTime?)null ;
                selectedItem.hasta = (DateTime?)null;
                selectedItem.estado = 0;

                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chidCultivo"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chidempresa"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chidCultivo"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chidVariedad"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chtipoCultivoId"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chidCalidad"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chdescripcion"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chdesde"].Value.ToString() != string.Empty ||
                                dgvRegistro.CurrentRow.Cells["chhasta"].Value.ToString() != string.Empty                                
                                )
                            {
                                string idempresa = (dgvRegistro.CurrentRow.Cells["chidempresa"].Value != null ? dgvRegistro.CurrentRow.Cells["chidempresa"].Value.ToString().Trim() : string.Empty);
                                string idcultivo = (dgvRegistro.CurrentRow.Cells["chidCultivo"].Value != null ? dgvRegistro.CurrentRow.Cells["chidCultivo"].Value.ToString().Trim() : string.Empty);
                                string idvariedad = (dgvRegistro.CurrentRow.Cells["chidVariedad"].Value != null ? dgvRegistro.CurrentRow.Cells["chidVariedad"].Value.ToString().Trim() : string.Empty);
                                string idtipoCultivo = (dgvRegistro.CurrentRow.Cells["chtipoCultivoId"].Value != null ? dgvRegistro.CurrentRow.Cells["chtipoCultivoId"].Value.ToString().Trim() : string.Empty);
                                string idcalidad = (dgvRegistro.CurrentRow.Cells["chidCalidad"].Value != null ? dgvRegistro.CurrentRow.Cells["chidCalidad"].Value.ToString().Trim() : string.Empty);
                                string descripcion = (dgvRegistro.CurrentRow.Cells["chdescripcion"].Value != null ? dgvRegistro.CurrentRow.Cells["chdescripcion"].Value.ToString().Trim() : string.Empty);
                                string desde = (dgvRegistro.CurrentRow.Cells["chdesde"].Value != null ? Convert.ToDateTime(dgvRegistro.CurrentRow.Cells["chdesde"].Value.ToString()).ToString() : string.Empty);
                                string hasta = (dgvRegistro.CurrentRow.Cells["chhasta"].Value != null ? Convert.ToDateTime(dgvRegistro.CurrentRow.Cells["chhasta"].Value.ToString()).ToString() : string.Empty);

                                var resultado = result.Where(x =>
                                x.idEmpresa.Trim() == idempresa &&
                                x.idCultivo.Trim() == idcultivo && 
                                x.idVariedad.Trim() == idvariedad && 
                                x.tipoCultivoId.ToString() == idtipoCultivo && 
                                x.idCalidad.Trim() == idcalidad && 
                                x.descripcion.Trim() == descripcion &&
                                x.desde.Value.ToString() == desde &&
                                x.hasta.Value.ToString() == hasta
                                ).ToList();

                                if (resultado.Count > 0)
                                {
                                    selectedItem = resultado.ElementAt(0);
                                    this.txtEstado.Text = selectedItem.estado.Value == 1 ? "ACTIVO" : "ANULADO";
                                    this.txtIdEstado.Text = selectedItem.estado.Value.ToString();
                                    this.txtEmpresa.Text = selectedItem.idEmpresa != null ? "SOCIEDAD AGRICOLA SATURNO SA" : string.Empty;
                                    this.txtEmpresaCodigo.Text = selectedItem.idEmpresa != null ? selectedItem.idEmpresa.Trim() : string.Empty;
                                    this.txtCultivo.Text = selectedItem.Cultivo != null ? selectedItem.Cultivo.Trim() : string.Empty;
                                    this.txtCultivoCodigo.Text = selectedItem.idCultivo != null ? selectedItem.idCultivo.Trim() : string.Empty;
                                    this.txtVariedad.Text = selectedItem.Variedad != null ? selectedItem.Variedad.Trim() : string.Empty;
                                    this.txtVariedadCodigo.Text = selectedItem.idVariedad != null ? selectedItem.idVariedad.Trim() : string.Empty;
                                    this.txtTipoCultivo.Text = selectedItem.tipocultivo != null ? selectedItem.tipocultivo.Trim() : string.Empty;
                                    this.txtTipoCultivoCodigo.Text = selectedItem.tipoCultivoId != (int?)null ? selectedItem.tipoCultivoId.ToString().Trim() : string.Empty;
                                    this.txtCalidad.Text = selectedItem.idCalidad != null ? "CALIDAD " + selectedItem.idCalidad.Trim() : string.Empty;
                                    this.txtCalidadCodigo.Text = selectedItem.idCalidad != null ? selectedItem.idCalidad.Trim() : string.Empty;
                                    this.txtDesde.Text = selectedItem.desde != (DateTime?)null ? selectedItem.desde.Value.ToShortDateString() : string.Empty;
                                    this.txtHasta.Text = selectedItem.hasta != (DateTime?)null ? selectedItem.hasta.Value.ToShortDateString() : string.Empty;
                                    this.txtDescripcion.Text = selectedItem.descripcion != null ? selectedItem.descripcion.Trim() : string.Empty;
                                    this.txtGlosa.Text = selectedItem.descripcion2 != null ? selectedItem.descripcion2.Trim() : string.Empty;
                                    this.txtNota.Text = selectedItem.descripcion3 != null ? selectedItem.descripcion3.Trim() : string.Empty;
                                }
                                else
                                {
                                    this.txtEstado.Text = selectedItem.estado.Value == 1 ? "ACTIVO" : "ANULADO";
                                    this.txtIdEstado.Text = selectedItem.estado.Value.ToString();
                                    this.txtEmpresa.Text = selectedItem.idEmpresa != null ? "SOCIEDAD AGRICOLA SATURNO SA" : string.Empty;
                                    this.txtEmpresaCodigo.Text = selectedItem.idEmpresa != null ? selectedItem.idEmpresa.Trim() : string.Empty;
                                    this.txtCultivo.Text = selectedItem.Cultivo != null ? selectedItem.Cultivo.Trim() : string.Empty;
                                    this.txtCultivoCodigo.Text = selectedItem.idCultivo != null ? selectedItem.idCultivo.Trim() : string.Empty;
                                    this.txtVariedad.Text = selectedItem.Variedad != null ? selectedItem.Variedad.Trim() : string.Empty;
                                    this.txtVariedadCodigo.Text = selectedItem.idVariedad != null ? selectedItem.idVariedad.Trim() : string.Empty;
                                    this.txtTipoCultivo.Text = selectedItem.tipocultivo != null ? selectedItem.tipocultivo.Trim() : string.Empty;
                                    this.txtTipoCultivoCodigo.Text = selectedItem.tipoCultivoId != (int?)null ? selectedItem.tipoCultivoId.ToString().Trim() : string.Empty;
                                    this.txtCalidad.Text = selectedItem.idCalidad != null ? "CALIDAD " + selectedItem.idCalidad.Trim() : string.Empty;
                                    this.txtCalidadCodigo.Text = selectedItem.idCalidad != null ? selectedItem.idCalidad.Trim() : string.Empty;
                                    this.txtDesde.Text = selectedItem.desde != (DateTime?)null ? selectedItem.desde.Value.ToShortDateString() : string.Empty;
                                    this.txtHasta.Text = selectedItem.hasta != (DateTime?)null ? selectedItem.hasta.Value.ToShortDateString() : string.Empty;
                                    this.txtDescripcion.Text = selectedItem.descripcion != null ? selectedItem.descripcion.Trim() : string.Empty;
                                    this.txtGlosa.Text = selectedItem.descripcion2 != null ? selectedItem.descripcion2.Trim() : string.Empty;
                                    this.txtNota.Text = selectedItem.descripcion3 != null ? selectedItem.descripcion3.Trim() : string.Empty;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Atras();
        }
    }
}
