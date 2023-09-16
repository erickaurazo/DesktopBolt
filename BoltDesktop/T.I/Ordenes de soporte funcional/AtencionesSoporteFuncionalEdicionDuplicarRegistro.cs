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

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class AtencionesSoporteFuncionalEdicionDuplicarRegistro : Form
    {
        string nombreformulario = "AtencionesSoporteFuncional";
        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private int codigoSelecionado = 0;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_DispositivoSoporteFuncionalController model;
        private List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> listing; //Listado
        private List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalleEliminado = new List<SAS_DispositivoSoporteFuncionalDetalle>();
        private List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
        private SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult selectedItem; // Item Selecionado
        private SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult item;
        private SAS_DispositivoSoporteFuncional ordenTrabajo;
        List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult> listDetalleByCodigoMantenimiento;
        private int ultimoItemEnListaDetalle;
        private int codigoDispositivo;
        private SAS_DispositivoUsuariosController modeloDispositivo;

        public AtencionesSoporteFuncionalEdicionDuplicarRegistro()
        {
            InitializeComponent();
        }

        public AtencionesSoporteFuncionalEdicionDuplicarRegistro(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoSelecionado)
        {

            InitializeComponent();
            nombreformulario = "AtencionesSoporteFuncional";
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSelecionado;
            Inicio();
            CargarCombos();
            progressBar1.Visible = true;
            gbColaborador.Enabled = false;
            gbReasignarCaso.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                documentos = new List<Grupo>();
                series = new List<Grupo>();
                tipoSolicitudes = new List<Grupo>();


                documentos = comboHelper.GetDocumentTypeForForm("SAS", "Soporte funcional");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                series = comboHelper.GetDocumentSeriesForForm("SAS", "Soporte funcional");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoSoporteFuncionalController();
                item = new SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult();
                item = model.GetListById("SAS", codigoSelecionado);

                listDetalleByCodigoMantenimiento = new List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>();
                listDetalleByCodigoMantenimiento = model.GetListDetalleByCodigoMantenimiento("SAS", this.codigoSelecionado).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                progressBar1.Visible = !true;
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                txtCodigo.Text = item.codigo.ToString();
                this.txtCorrelativo.Text = item.codigo.ToString().PadLeft(7, '0');
                cboSerie.SelectedValue = item.idSerie.ToString();
                cboDocumento.SelectedValue = item.iddocumento.ToString();
                txtFecha.Text = item.fecha.ToShortDateString();
                this.txtPersonal.Clear();
                this.txtPersonalCodigo.Clear();
                gbColaborador.Enabled = !false;
                gbReasignarCaso.Enabled = !false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (this.txtPersonal.Text != string.Empty && this.txtPersonalCodigo.Text != string.Empty)
            {
                string codigoEmpleado = this.txtPersonalCodigo.Text.Trim();
                #region Condicion para editar()
                model = new SAS_DispositivoSoporteFuncionalController();
                int resultadoOperacion = model.DuplicateRegister(conection, codigoSelecionado, codigoEmpleado);
                gbColaborador.Enabled = false;
                gbReasignarCaso.Enabled = false;
                btnRegistrar.Enabled = false;
                btnCancelar.Enabled = true;


                #endregion
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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



        private void AtencionesSoporteFuncionalEdicionDuplicarRegistro_Load(object sender, EventArgs e)
        {

        }
    }
}
