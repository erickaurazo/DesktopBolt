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
using System.Drawing;
using System.Collections;

namespace ComparativoHorasVisualSATNISIRA.Maquinaria
{
    public partial class ProgramacionDeMaquinariaEdicion : Form
    {

        private List<SAS_ProgramacionMaquinariaListAllByTurn> listado;
        private SAS_ProgramacionDetalleMaquinariaController model;
        private int periodo;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos;
        private List<Grupo> series;
        private List<Grupo> numeroDocumento;
        private List<Grupo> listPeriodos;
        private List<Grupo> listSemanas;
        private List<Grupo> listSemanaActual;
        private SAS_ProgramacionMaquinariaListAllByTurn _selectedItem;
        private List<SAS_ProgramacionDetalleMaquinariaByIdResult> ListDetailById = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
        private SAS_ProgramacionMaquinaria oPrograma = new SAS_ProgramacionMaquinaria();
        private List<SAS_ProgramacionDetalleMaquinaria> oDetailListOfProgram = new List<SAS_ProgramacionDetalleMaquinaria>();
        private List<SAS_ProgramacionDetalleMaquinaria> oDetailListOfProgramDelete = new List<SAS_ProgramacionDetalleMaquinaria>();
        private Int16 semanaPeriodo;
        private List<SAS_ProgramacionDetalleMaquinariaByIdResult> selectedList;
        private List<Grupo> listaDiasParaCabeceraDeGrilla;

        public ProgramacionDeMaquinariaEdicion()
        {
            InitializeComponent();
            _conection = "SAS";
            _user = new SAS_USUARIOS();
            _user.IdUsuario = "EAURAZO";
            _user.NombreCompleto = "ERICK AURAZO";
            _companyId = "001";
            _privilege = new PrivilegesByUser();
            Inicio();
            CargarCombos();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

        }


        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                documentos = new List<Grupo>();
                series = new List<Grupo>();
                numeroDocumento = new List<Grupo>();

                listPeriodos = new List<Grupo>();
                listSemanas = new List<Grupo>();
                listSemanaActual = new List<Grupo>();

                documentos = comboHelper.GetDocumentTypeForForm("SAS", "Programacion maquinaria");
                cboDocumento.DisplayMember = "Descripcion";
                cboDocumento.ValueMember = "Codigo";
                cboDocumento.DataSource = documentos.ToList();

                listPeriodos = new List<Grupo>();
                listPeriodos = comboHelper.GetListPeriodosByProgramacionMaquinaria("SAS", "Programacion maquinaria");
                cboPeriodo.DisplayMember = "Descripcion";
                cboPeriodo.ValueMember = "Codigo";
                cboPeriodo.DataSource = listPeriodos.ToList();
                cboPeriodo.SelectedValue = DateTime.Now.Year.ToString();
                periodo = Convert.ToInt32(cboPeriodo.SelectedValue);

                listSemanas = new List<Grupo>();
                //listSemanas = comboHelper.GetListSemanaPeriodosByProgramacionMaquinaria("SAS", "Programacion maquinaria", periodo);
                //cboSemana.DisplayMember = "Descripcion";
                //cboSemana.ValueMember = "Codigo";
                //cboSemana.DataSource = listSemanas.ToList();

                //listSemanaActual = comboHelper.GetSemanaProgramacionDesdeFecha("SAS", DateTime.Now, listSemanas);
                //cboSemana.SelectedValue = listSemanaActual.ElementAt(0).Codigo.ToString();
                //semanaPeriodo = Convert.ToInt16(cboSemana.SelectedValue);

                series = comboHelper.GetDocumentSeriesForForm("SAS", "Programacion maquinaria");
                cboSerie.DisplayMember = "Descripcion";
                cboSerie.ValueMember = "Codigo";
                cboSerie.DataSource = series.ToList();

                numeroDocumento = new List<Grupo>();
                numeroDocumento = comboHelper.getNumberDocument("SAS", "Programacion maquinaria");
                this.txtNumeroDocumento.Text = numeroDocumento.ElementAt(0).Codigo;




            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }



        public ProgramacionDeMaquinariaEdicion(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            Inicio();
            CargarCombos();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();

        }

        public ProgramacionDeMaquinariaEdicion(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege, SAS_ProgramacionMaquinariaListAllByTurn selectedItem)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;

            _companyId = companyId;
            _privilege = privilege;
            _selectedItem = selectedItem;
            Inicio();
            CargarCombos();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
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

        private void btnGenerarPrograma_Click(object sender, EventArgs e)
        {

        }

        private void ProgramacionDeMaquinariaEdicion_Load(object sender, EventArgs e)
        {

        }

        private void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPeriodo.SelectedIndex > 0)
            {
                periodo = Convert.ToInt32(cboPeriodo.SelectedValue);

                listSemanas = new List<Grupo>();
                listSemanas = comboHelper.GetListSemanaPeriodosByProgramacionMaquinaria("SAS", "Programacion maquinaria", periodo);
                cboSemana.DisplayMember = "Descripcion";
                cboSemana.ValueMember = "Codigo";
                cboSemana.DataSource = listSemanas.ToList();
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            model = new SAS_ProgramacionDetalleMaquinariaController();
            if (_selectedItem != null)
            {
                if (_selectedItem.idProgramacionMaquinaria == 0)
                {
                    #region Nuevo()
                    _selectedItem.idempresa = "001";
                    _selectedItem.empresa = "SOCIEDAD AGRÍCOLA SATURNO SA";
                    _selectedItem.idSucursal = "001";
                    _selectedItem.sucursal = "SEDE LOGISTICA AGRICOLA";
                    _selectedItem.estadoId = '0';
                    _selectedItem.estado = "PENDIENTE";
                    _selectedItem.idProgramacionMaquinaria = 0;
                    _selectedItem.documento = string.Empty;
                    _selectedItem.fechaRegistro = DateTime.Now;
                    _selectedItem.idResponsable = string.Empty;
                    _selectedItem.responsable = string.Empty;
                    _selectedItem.idusuario = _user != null ? (_user.IdUsuario != null ? _user.IdUsuario.Trim() : Environment.UserName) : Environment.UserName;
                    _selectedItem.periodo = periodo;
                    _selectedItem.semana = semanaPeriodo;
                    listSemanas = new List<Grupo>();
                    listSemanas = comboHelper.GetListSemanaPeriodosByProgramacionMaquinaria("SAS", "Programacion maquinaria", periodo);
                    listSemanaActual = comboHelper.GetSemanaProgramacionDesdeFecha("SAS", DateTime.Now, listSemanas);
                    oDetailListOfProgram = new List<SAS_ProgramacionDetalleMaquinaria>();
                    ListDetailById = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
                    numeroDocumento = new List<Grupo>();
                    numeroDocumento = comboHelper.getNumberDocument("SAS", "Programacion maquinaria");
                    ListDetailById = model.GetListDetailAllByIdBlank("SAS", _selectedItem.idProgramacionMaquinaria, periodo, semanaPeriodo).ToList();
                    // int idRegistro = model.ToRegister(_conection != null ? _conection : "SAS", oPrograma, oDetailListOfProgram);
                    //ListDetailById = model.GetListDetailAllById(_conection != null ? _conection : "SAS", idRegistro).ToList();
                    #endregion
                }
                else
                {
                    #region Editar()

                    listSemanas = new List<Grupo>();
                    listSemanas.Add(new Grupo { Codigo = _selectedItem.semana.ToString(), Descripcion = _selectedItem.semana.ToString() + " | del " + _selectedItem.fechaInicio.ToLongDateString() + " al " + _selectedItem.fechaFin.ToLongDateString() });
                    listPeriodos = new List<Grupo>();
                    listPeriodos.Add(new Grupo { Codigo = _selectedItem.periodo.ToString(), Descripcion = _selectedItem.periodo.ToString() });
                    ListDetailById = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
                    ListDetailById = model.GetListDetailAllById("SAS", _selectedItem.idProgramacionMaquinaria).ToList();
                    if (ListDetailById != null)
                    {
                        if (ListDetailById.ToList().Count > 0)
                        {
                            listaDiasParaCabeceraDeGrilla = new List<Grupo>();
                            listaDiasParaCabeceraDeGrilla = model.ObtenerListadoDiasSemana(ListDetailById);
                        }
                    }
                    #region Generar listado turno Diurno()

                    #endregion

                    #region Generar listado turno Tarde()

                    #endregion

                    #region Generar listado turno Noche()

                    #endregion

                    #endregion
                }
            }
        }

        private void GetObjets()
        {
            oPrograma = new SAS_ProgramacionMaquinaria();
            oPrograma.Idempresa = _selectedItem.idempresa;
            oPrograma.idProgramacionMaquinaria = 0;
            oPrograma.idSucursal = _selectedItem.idSucursal;
            oPrograma.iddocumento = "PRO";
            oPrograma.periodo = periodo;
            oPrograma.semana = semanaPeriodo;
            oPrograma.idResponsable = _selectedItem.idResponsable;
            //oPrograma.fechaInicio = oPrograma.fechaInicio;
            //oPrograma.fechaFin = oPrograma.fechaFin;
            oPrograma.estado = '0';
            oPrograma.fecharegistro = DateTime.Now;
            oPrograma.idusuario = oPrograma.idusuario;
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (_selectedItem != null)
            {
                if (_selectedItem.idProgramacionMaquinaria == 0)
                {
                    #region Nuevo()
                    txtEmpresaCodigo.Text = _selectedItem.idempresa;
                    txtEmpresa.Text = _selectedItem.idempresa;
                    txtSucursalCodigo.Text = _selectedItem.idSucursal;
                    txtSucursal.Text = _selectedItem.sucursal;
                    txtEstadoCodigo.Text = _selectedItem.estadoId.ToString();
                    txtEstado.Text = _selectedItem.estado;
                    txtCodigo.Text = _selectedItem.idProgramacionMaquinaria.ToString();
                    txtFecha.Text = _selectedItem.fechaRegistro.Value.ToShortDateString();
                    txtResponsableCodigo.Text = _selectedItem.idResponsable;
                    txResponsable.Text = _selectedItem.responsable;
                    txtUsuarioAsignado.Text = _user != null ? (_user.IdUsuario != null ? _user.IdUsuario.Trim() : Environment.UserName) : "EAURAZO";
                    this.txtNumeroDocumento.Text = numeroDocumento.ElementAt(0).Codigo;



                    cboSemana.DisplayMember = "Descripcion";
                    cboSemana.ValueMember = "Codigo";
                    cboSemana.DataSource = listSemanas.ToList();
                    cboSemana.SelectedValue = listSemanaActual.ElementAt(0).Codigo.ToString();
                    semanaPeriodo = Convert.ToInt16(cboSemana.SelectedValue);

                    this.txtTotalTurnoDia.Text = _selectedItem.diurno != null ? _selectedItem.diurno.ToString() : "0";
                    this.txtTotalTurnoTarde.Text = _selectedItem.tarde != null ? _selectedItem.tarde.ToString() : "0";
                    this.txtTotalTurnoNoche.Text = _selectedItem.noche != null ? _selectedItem.noche.ToString() : "0";

                    #endregion
                }
                else
                {
                    #region Editar()                   
                    txtEmpresaCodigo.Text = _selectedItem.idempresa;
                    txtEmpresa.Text = _selectedItem.idempresa;
                    txtSucursalCodigo.Text = _selectedItem.idSucursal;
                    txtSucursal.Text = _selectedItem.sucursal;
                    txtEstadoCodigo.Text = _selectedItem.estadoId.ToString();
                    txtEstado.Text = _selectedItem.estado;
                    txtCodigo.Text = _selectedItem.idProgramacionMaquinaria.ToString();
                    txtNumeroDocumento.Text = _selectedItem.documento.Substring(9, 7);

                    txtFecha.Text = _selectedItem.fechaRegistro.Value.ToShortDateString();
                    txtResponsableCodigo.Text = _selectedItem.idResponsable;
                    txResponsable.Text = _selectedItem.responsable;

                    cboPeriodo.DisplayMember = "Descripcion";
                    cboPeriodo.ValueMember = "Codigo";
                    cboPeriodo.DataSource = listPeriodos.ToList();
                    cboPeriodo.SelectedValue = _selectedItem.periodo.Value.ToString();

                    cboSemana.DisplayMember = "Descripcion";
                    cboSemana.ValueMember = "Codigo";
                    cboSemana.DataSource = listSemanas.ToList();
                    cboSemana.SelectedValue = _selectedItem.semana.ToString();
                    txtUsuarioAsignado.Text = _selectedItem.idusuario.Trim();

                    this.txtTotalTurnoDia.Text = _selectedItem.diurno != null ? _selectedItem.diurno.ToString() : "0";
                    this.txtTotalTurnoTarde.Text = _selectedItem.tarde != null ? _selectedItem.tarde.ToString() : "0";
                    this.txtTotalTurnoNoche.Text = _selectedItem.noche != null ? _selectedItem.noche.ToString() : "0";

                    #endregion
                }
            }




            #region Generar Grilla desde query();
            this.dgvDetalleTurnoDia.DataSource = null;
            this.dgvDetalleTurnoTarde.DataSource = null;
            this.dgvDetalleTurnoNoche.DataSource = null;
            this.dgvDetalleTurnoDia.Rows.Clear();
            this.dgvDetalleTurnoTarde.Rows.Clear();
            this.dgvDetalleTurnoNoche.Rows.Clear();

            selectedList = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
            if (ListDetailById != null)
            {
                #region Diurno()  
                if (ListDetailById.ToList().Count > 0)
                {
                    selectedList.Add(ListDetailById.ElementAt(0));
                    var ListConvertToDatatable = selectedList.ToDataTable<SAS_ProgramacionDetalleMaquinariaByIdResult>();

                    foreach (DataColumn item in ListConvertToDatatable.Columns)
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.ColumnName; // Comoviene del procedure
                        chcolumna.Frozen = true; // si quiere estar congelado
                        chcolumna.HeaderText = item.ColumnName; // Nombre para mostrar
                        chcolumna.Name = "chd" + item.ColumnName ; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        //dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        //dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.Visible = false;
                        chcolumna.ReadOnly = true;
                        if (
                            item.ColumnName.ToUpper().Trim() == "tipoLinea".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "sector".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "supervisor".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "idMaquinaria".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "implemento".ToUpper().Trim())
                        {
                            chcolumna.Visible = true;
                        }
                    }
                    System.Windows.Forms.DataGridViewTextBoxColumn chcolumnaSubTotal = new DataGridViewTextBoxColumn();
                    chcolumnaSubTotal.DataPropertyName = "SubTotalH"; // Comoviene del procedure
                    chcolumnaSubTotal.Frozen = true; // si quiere estar congelado
                    chcolumnaSubTotal.HeaderText = "SubTotalH"; // Nombre para mostrar
                    chcolumnaSubTotal.Name = "chd" + "SubTotalH"; // ch + nombre de columna                        
                    chcolumnaSubTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    chcolumnaSubTotal.ValueType = typeof(decimal?);
                    chcolumnaSubTotal.DefaultCellStyle.Format = "N0";
                    chcolumnaSubTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    chcolumnaSubTotal.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    chcolumnaSubTotal.ReadOnly = true;
                    dgvDetalleTurnoDia.Columns.Add(chcolumnaSubTotal);
                    //dgvDetalleTurnoTarde.Columns.Add(chcolumnaSubTotal);
                    //dgvDetalleTurnoNoche.Columns.Add(chcolumnaSubTotal);

                    foreach (var item in listaDiasParaCabeceraDeGrilla.OrderBy(x => x.Codigo))
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.Codigo; // Comoviene del procedure
                        chcolumna.Frozen = false; // si quiere estar congelado
                        chcolumna.HeaderText = item.Codigo; // Nombre para mostrar
                        chcolumna.Name = "chd" + item.Codigo; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        chcolumna.Visible = true;
                        chcolumna.ValueType = typeof(decimal?);
                        chcolumna.DefaultCellStyle.Format = "N0";
                        dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        //dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        //dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.ReadOnly = true;
                    }


                }
                #endregion

                #region Tarde()  
                if (ListDetailById.ToList().Count > 0)
                {
                    selectedList.Add(ListDetailById.ElementAt(0));
                    var ListConvertToDatatable = selectedList.ToDataTable<SAS_ProgramacionDetalleMaquinariaByIdResult>();

                    foreach (DataColumn item in ListConvertToDatatable.Columns)
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.ColumnName; // Comoviene del procedure
                        chcolumna.Frozen = true; // si quiere estar congelado
                        chcolumna.HeaderText = item.ColumnName; // Nombre para mostrar
                        chcolumna.Name = "cht" + item.ColumnName; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        //dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        //dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.Visible = false;
                        chcolumna.ReadOnly = true;
                        if (
                            item.ColumnName.ToUpper().Trim() == "tipoLinea".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "sector".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "supervisor".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "idMaquinaria".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "implemento".ToUpper().Trim())
                        {
                            chcolumna.Visible = true;
                        }
                    }
                    System.Windows.Forms.DataGridViewTextBoxColumn chcolumnaSubTotal = new DataGridViewTextBoxColumn();
                    chcolumnaSubTotal.DataPropertyName = "SubTotalH"; // Comoviene del procedure
                    chcolumnaSubTotal.Frozen = true; // si quiere estar congelado
                    chcolumnaSubTotal.HeaderText = "SubTotalH"; // Nombre para mostrar
                    chcolumnaSubTotal.Name = "cht" + "SubTotalH"; // ch + nombre de columna                        
                    chcolumnaSubTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    chcolumnaSubTotal.ValueType = typeof(decimal?);
                    chcolumnaSubTotal.DefaultCellStyle.Format = "N0";
                    chcolumnaSubTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    chcolumnaSubTotal.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    chcolumnaSubTotal.ReadOnly = true;
                    //dgvDetalleTurnoDia.Columns.Add(chcolumnaSubTotal);
                    dgvDetalleTurnoTarde.Columns.Add(chcolumnaSubTotal);
                    //dgvDetalleTurnoNoche.Columns.Add(chcolumnaSubTotal);

                    foreach (var item in listaDiasParaCabeceraDeGrilla.OrderBy(x => x.Codigo))
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.Codigo; // Comoviene del procedure
                        chcolumna.Frozen = false; // si quiere estar congelado
                        chcolumna.HeaderText = item.Codigo; // Nombre para mostrar
                        chcolumna.Name = "cht" + item.Codigo; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        chcolumna.Visible = true;
                        chcolumna.ValueType = typeof(decimal?);
                        chcolumna.DefaultCellStyle.Format = "N0";
                        //dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        //dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.ReadOnly = true;
                    }


                }
                #endregion

                #region Noche()  
                if (ListDetailById.ToList().Count > 0)
                {
                    selectedList.Add(ListDetailById.ElementAt(0));
                    var ListConvertToDatatable = selectedList.ToDataTable<SAS_ProgramacionDetalleMaquinariaByIdResult>();

                    foreach (DataColumn item in ListConvertToDatatable.Columns)
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.ColumnName; // Comoviene del procedure
                        chcolumna.Frozen = true; // si quiere estar congelado
                        chcolumna.HeaderText = item.ColumnName; // Nombre para mostrar
                        chcolumna.Name = "chn" + item.ColumnName; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        //dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        //dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.Visible = false;
                        chcolumna.ReadOnly = true;
                        if (
                            item.ColumnName.ToUpper().Trim() == "tipoLinea".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "sector".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "supervisor".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "idMaquinaria".ToUpper().Trim() ||
                            item.ColumnName.ToUpper().Trim() == "implemento".ToUpper().Trim())
                        {
                            chcolumna.Visible = true;
                        }
                    }
                    System.Windows.Forms.DataGridViewTextBoxColumn chcolumnaSubTotal = new DataGridViewTextBoxColumn();
                    chcolumnaSubTotal.DataPropertyName = "SubTotalH"; // Comoviene del procedure
                    chcolumnaSubTotal.Frozen = true; // si quiere estar congelado
                    chcolumnaSubTotal.HeaderText = "SubTotalH"; // Nombre para mostrar
                    chcolumnaSubTotal.Name = "chn" + "SubTotalH"; // ch + nombre de columna                        
                    chcolumnaSubTotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    chcolumnaSubTotal.ValueType = typeof(decimal?);
                    chcolumnaSubTotal.DefaultCellStyle.Format = "N0";
                    chcolumnaSubTotal.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    chcolumnaSubTotal.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    chcolumnaSubTotal.ReadOnly = true;
                    //dgvDetalleTurnoDia.Columns.Add(chcolumnaSubTotal);
                    //dgvDetalleTurnoTarde.Columns.Add(chcolumnaSubTotal);
                    dgvDetalleTurnoNoche.Columns.Add(chcolumnaSubTotal);

                    foreach (var item in listaDiasParaCabeceraDeGrilla.OrderBy(x => x.Codigo))
                    {
                        System.Windows.Forms.DataGridViewTextBoxColumn chcolumna = new DataGridViewTextBoxColumn();
                        chcolumna.DataPropertyName = item.Codigo; // Comoviene del procedure
                        chcolumna.Frozen = false; // si quiere estar congelado
                        chcolumna.HeaderText = item.Codigo; // Nombre para mostrar
                        chcolumna.Name = "chn" + item.Codigo ; // ch + nombre de columna
                        chcolumna.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        chcolumna.Visible = true;
                        chcolumna.ValueType = typeof(decimal?);
                        chcolumna.DefaultCellStyle.Format = "N0";
                        //dgvDetalleTurnoDia.Columns.Add(chcolumna);
                        //dgvDetalleTurnoTarde.Columns.Add(chcolumna);
                        dgvDetalleTurnoNoche.Columns.Add(chcolumna);
                        chcolumna.ReadOnly = true;
                    }


                }
                #endregion
            }

            CargarDatosAgrilla(ListDetailById, listaDiasParaCabeceraDeGrilla, "diurno");



            #region Llenar listado detalle turno Diurno()
            #endregion

            #endregion

            #region Llenar listado detalle turno Tarde()

            #endregion

            #region Llenar listado detalle turno Noche()

            #endregion

            //PintarResultadosGrilla();







        }

        private void CargarDatosAgrilla(List<SAS_ProgramacionDetalleMaquinariaByIdResult> listBuildQuery, List<Grupo> listaDiasParaCabeceraDeGrilla, string turno)
        {
            int contador = 0;
            // se tienen que considerar cuatro puntos de variedad, campo y luego con las semanas segun corresponde

            var listadoTipoLinea = (from itemVariedad in listBuildQuery.OrderBy(x => x.tipoLinea).ToList()
                                    group itemVariedad by new { itemVariedad.tipoLinea } into j
                                    select new { codigo = j.FirstOrDefault().tipoLineaId, descripcion = j.FirstOrDefault().tipoLinea }).ToList();

            if (listadoTipoLinea != null && listadoTipoLinea.ToList().Count > 0)
            {
                foreach (var _tipoLinea in listadoTipoLinea)
                {
                    var result01 = listBuildQuery.Where(x => x.tipoLineaId == _tipoLinea.codigo).ToList();

                    if (result01 != null)
                    {
                        if (result01.ToList().Count > 0)
                        {
                            var listoSector = (from itemSector in result01.OrderBy(x => x.sector).ToList()
                                               group itemSector by new { itemSector.idSector } into j
                                               select new { codigo = j.FirstOrDefault().idSector, descripcion = j.FirstOrDefault().sector }).ToList();

                            if (listoSector != null && listoSector.ToList().Count > 0)
                            {
                                foreach (var _sector in listoSector)
                                {
                                    var result02 = result01.Where(x => x.idSector == _sector.codigo).ToList();

                                    if (result02 != null)
                                    {
                                        if (result02.ToList().Count > 0)
                                        {
                                            var listoSupervidor = (from itemSupervisor in result02.OrderBy(x => x.supervisor).ToList()
                                                                   group itemSupervisor by new { itemSupervisor.idSupervisor } into j
                                                                   select new { codigo = j.FirstOrDefault().idSupervisor, descripcion = j.FirstOrDefault().supervisor }).ToList();

                                            if (listoSupervidor != null && listoSupervidor.ToList().Count > 0)
                                            {
                                                foreach (var _supervisor in listoSupervidor)
                                                {
                                                    var result03 = result02.Where(x => x.idSupervisor == _supervisor.codigo).ToList();

                                                    if (result03 != null)
                                                    {
                                                        if (result03.ToList().Count > 0)
                                                        {
                                                            var listoMaquinaria = (from itemMaquinaria in result03.OrderBy(x => x.idMaquinaria).ToList()
                                                                                   group itemMaquinaria by new { itemMaquinaria.idMaquinaria } into j
                                                                                   select new { codigo = j.FirstOrDefault().idMaquinaria, descripcion = j.FirstOrDefault().maquinaria }).ToList();

                                                            if (listoMaquinaria != null && listoMaquinaria.ToList().Count > 0)
                                                            {
                                                                foreach (var _maquinaria in listoMaquinaria)
                                                                {
                                                                    var result04 = result03.Where(x => x.idMaquinaria == _maquinaria.codigo).ToList();

                                                                    if (result04 != null)
                                                                    {
                                                                        if (result04.ToList().Count > 0)
                                                                        {
                                                                            var listoImplemento = (from itemImplemento in result04.OrderBy(x => x.idImplemento).ToList()
                                                                                                   group itemImplemento by new { itemImplemento.idMaquinaria } into j
                                                                                                   select new { codigo = j.FirstOrDefault().idImplemento, descripcion = j.FirstOrDefault().implemento }).ToList();

                                                                            if (listoImplemento != null && listoImplemento.ToList().Count > 0)
                                                                            {
                                                                                foreach (var _implemento in listoImplemento)
                                                                                {
                                                                                    var result05 = result04.Where(x => x.idImplemento == _implemento.codigo).ToList();

                                                                                    if (result05 != null)
                                                                                    {
                                                                                        if (result05.ToList().Count > 0)
                                                                                        {
                                                                                            var oSelecionado = result05.ElementAt(0);
                                                                                            decimal SumDiurno = result05.Sum(x => Convert.ToDecimal(x.diurno));
                                                                                            decimal SumTarde = result05.Sum(x => Convert.ToDecimal(x.tarde));
                                                                                            decimal SumNoche = result05.Sum(x => Convert.ToDecimal(x.noche));
                                                                                            ArrayList array = new ArrayList();
                                                                                            ArrayList arrayT = new ArrayList();
                                                                                            ArrayList arrayN = new ArrayList();

                                                                                            foreach (System.Windows.Forms.DataGridViewTextBoxColumn oItem in dgvDetalleTurnoDia.Columns)
                                                                                            {
                                                                                                #region LLenado turno diurno()
                                                                                                string nombreColumna = dgvDetalleTurnoDia.Columns[oItem.Index].Name.ToString();
                                                                                                nombreColumna = nombreColumna.Substring(3);

                                                                                                if (nombreColumna == "idProgramacionDetalle") { array.Add(oSelecionado.idProgramacionDetalle.ToString()); }
                                                                                                else if(nombreColumna == "idProgramacionMaquinaria") { array.Add(oSelecionado.idProgramacionMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "tipoLineaId") { array.Add(oSelecionado.tipoLineaId.ToString()); }
                                                                                                else if (nombreColumna == "tipoLinea") { array.Add(oSelecionado.tipoLinea.ToString()); }
                                                                                                else if (nombreColumna == "idSupervisor") { array.Add(oSelecionado.idSupervisor.ToString()); }
                                                                                                else if (nombreColumna == "supervisor") { array.Add(oSelecionado.supervisor.ToString()); }
                                                                                                else if (nombreColumna == "idMaquinaria") { array.Add(oSelecionado.idMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "maquinaria") { array.Add(oSelecionado.maquinaria.ToString()); }
                                                                                                else if (nombreColumna == "idImplemento") { array.Add(oSelecionado.idImplemento.ToString()); }
                                                                                                else if (nombreColumna == "implemento") { array.Add(oSelecionado.implemento.ToString()); }
                                                                                                else if (nombreColumna == "idSector") { array.Add(oSelecionado.idSector.ToString()); }
                                                                                                else if (nombreColumna == "sector") { array.Add(oSelecionado.sector.ToString()); }
                                                                                                else if (nombreColumna == "fecha") { array.Add(oSelecionado.fecha.ToString()); }
                                                                                                else if (nombreColumna == "diurno") { array.Add(oSelecionado.diurno.ToString()); }
                                                                                                else if (nombreColumna == "tarde") { array.Add(oSelecionado.tarde.ToString()); }
                                                                                                else if (nombreColumna == "noche") { array.Add(oSelecionado.noche.ToString()); }
                                                                                                else if (nombreColumna == "estado") { array.Add(oSelecionado.estado.ToString()); }
                                                                                                else if (nombreColumna == "SubTotalH") { array.Add(SumDiurno); }
                                                                                                else
                                                                                                {
                                                                                                    var valorSemanal = result05.Where(x => x.fecha.ToShortDateString() == nombreColumna).ToList();
                                                                                                    if (valorSemanal != null)
                                                                                                    {
                                                                                                        if (valorSemanal.ToList().Count > 0)
                                                                                                        {
                                                                                                            // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                                                                                            array.Add(Convert.ToDecimal(valorSemanal.Sum(x => x.diurno)));
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                                                                                            array.Add(Convert.ToDecimal(0));
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                                #endregion
                                                                                            }
                                                                                            dgvDetalleTurnoDia.AgregarFila(array);

                                                                                            foreach (System.Windows.Forms.DataGridViewTextBoxColumn oItem in dgvDetalleTurnoTarde.Columns)
                                                                                            {
                                                                                                #region LLenado turno diurno()
                                                                                                string nombreColumna = dgvDetalleTurnoTarde.Columns[oItem.Index].Name.ToString();
                                                                                                nombreColumna = nombreColumna.Substring(3);

                                                                                                if (nombreColumna == "idProgramacionDetalle") { arrayT.Add(oSelecionado.idProgramacionDetalle.ToString()); }
                                                                                                else if (nombreColumna == "idProgramacionMaquinaria") { arrayT.Add(oSelecionado.idProgramacionMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "tipoLineaId") { arrayT.Add(oSelecionado.tipoLineaId.ToString()); }
                                                                                                else if (nombreColumna == "tipoLinea") { arrayT.Add(oSelecionado.tipoLinea.ToString()); }
                                                                                                else if (nombreColumna == "idSupervisor") { arrayT.Add(oSelecionado.idSupervisor.ToString()); }
                                                                                                else if (nombreColumna == "supervisor") { arrayT.Add(oSelecionado.supervisor.ToString()); }
                                                                                                else if (nombreColumna == "idMaquinaria") { arrayT.Add(oSelecionado.idMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "maquinaria") { arrayT.Add(oSelecionado.maquinaria.ToString()); }
                                                                                                else if (nombreColumna == "idImplemento") { arrayT.Add(oSelecionado.idImplemento.ToString()); }
                                                                                                else if (nombreColumna == "implemento") { arrayT.Add(oSelecionado.implemento.ToString()); }
                                                                                                else if (nombreColumna == "idSector") { arrayT.Add(oSelecionado.idSector.ToString()); }
                                                                                                else if (nombreColumna == "sector") { arrayT.Add(oSelecionado.sector.ToString()); }
                                                                                                else if (nombreColumna == "fecha") { arrayT.Add(oSelecionado.fecha.ToString()); }
                                                                                                else if (nombreColumna == "diurno") { arrayT.Add(oSelecionado.diurno.ToString()); }
                                                                                                else if (nombreColumna == "tarde") { arrayT.Add(oSelecionado.tarde.ToString()); }
                                                                                                else if (nombreColumna == "noche") { arrayT.Add(oSelecionado.noche.ToString()); }
                                                                                                else if (nombreColumna == "estado") { arrayT.Add(oSelecionado.estado.ToString()); }
                                                                                                else if (nombreColumna == "SubTotalH") { arrayT.Add(SumTarde); }
                                                                                                else
                                                                                                {
                                                                                                    var valorSemanal = result05.Where(x => x.fecha.ToShortDateString() == nombreColumna).ToList();
                                                                                                    if (valorSemanal != null)
                                                                                                    {
                                                                                                        if (valorSemanal.ToList().Count > 0)
                                                                                                        {
                                                                                                            // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                                                                                            decimal sumTurnoTarde = (Convert.ToDecimal(valorSemanal.Sum(x => x.tarde)));
                                                                                                            arrayT.Add(sumTurnoTarde);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                                                                                            arrayT.Add(Convert.ToDecimal(0));
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                                #endregion
                                                                                            }
                                                                                            dgvDetalleTurnoTarde.AgregarFila(arrayT);

                                                                                            foreach (System.Windows.Forms.DataGridViewTextBoxColumn oItem in dgvDetalleTurnoNoche.Columns)
                                                                                            {
                                                                                                #region LLenado turno diurno()
                                                                                                string nombreColumna = dgvDetalleTurnoNoche.Columns[oItem.Index].Name.ToString();
                                                                                                nombreColumna = nombreColumna.Substring(3);

                                                                                                if (nombreColumna == "idProgramacionDetalle") { arrayN.Add(oSelecionado.idProgramacionDetalle.ToString()); }
                                                                                                else if (nombreColumna == "idProgramacionMaquinaria") { arrayN.Add(oSelecionado.idProgramacionMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "tipoLineaId") { arrayN.Add(oSelecionado.tipoLineaId.ToString()); }
                                                                                                else if (nombreColumna == "tipoLinea") { arrayN.Add(oSelecionado.tipoLinea.ToString()); }
                                                                                                else if (nombreColumna == "idSupervisor") { arrayN.Add(oSelecionado.idSupervisor.ToString()); }
                                                                                                else if (nombreColumna == "supervisor") { arrayN.Add(oSelecionado.supervisor.ToString()); }
                                                                                                else if (nombreColumna == "idMaquinaria") { arrayN.Add(oSelecionado.idMaquinaria.ToString()); }
                                                                                                else if (nombreColumna == "maquinaria") { arrayN.Add(oSelecionado.maquinaria.ToString()); }
                                                                                                else if (nombreColumna == "idImplemento") { arrayN.Add(oSelecionado.idImplemento.ToString()); }
                                                                                                else if (nombreColumna == "implemento") { arrayN.Add(oSelecionado.implemento.ToString()); }
                                                                                                else if (nombreColumna == "idSector") { arrayN.Add(oSelecionado.idSector.ToString()); }
                                                                                                else if (nombreColumna == "sector") { arrayN.Add(oSelecionado.sector.ToString()); }
                                                                                                else if (nombreColumna == "fecha") { arrayN.Add(oSelecionado.fecha.ToString()); }
                                                                                                else if (nombreColumna == "diurno") { arrayN.Add(oSelecionado.diurno.ToString()); }
                                                                                                else if (nombreColumna == "tarde") { arrayN.Add(oSelecionado.tarde.ToString()); }
                                                                                                else if (nombreColumna == "noche") { arrayN.Add(oSelecionado.noche.ToString()); }
                                                                                                else if (nombreColumna == "estado") { arrayN.Add(oSelecionado.estado.ToString()); }
                                                                                                else if (nombreColumna == "SubTotalH") { arrayN.Add(SumNoche); }
                                                                                                else
                                                                                                {
                                                                                                    var valorSemanal = result05.Where(x => x.fecha.ToShortDateString() == nombreColumna).ToList();
                                                                                                    if (valorSemanal != null)
                                                                                                    {
                                                                                                        if (valorSemanal.ToList().Count > 0)
                                                                                                        {
                                                                                                            // array.Add(valorSemanal.Sum(x => x.cantidad).Value.ToDecimalPresentation());
                                                                                                            arrayN.Add(Convert.ToDecimal(valorSemanal.Sum(x => x.noche)));
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            //array.Add(Convert.ToDecimal(0).ToDecimalPresentation().ToString());
                                                                                                            arrayN.Add(Convert.ToDecimal(0));
                                                                                                        }
                                                                                                    }
                                                                                                }

                                                                                                #endregion
                                                                                            }
                                                                                            dgvDetalleTurnoNoche.AgregarFila(arrayN);

                                                                                        }
                                                                                    }

                                                                                }
                                                                            }

                                                                        }
                                                                    }

                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

        }

        private void btnGrabarYSeguirEditando_Click(object sender, EventArgs e)
        {

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnNotificar_Click(object sender, EventArgs e)
        {

        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleAgregar_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {

        }
    }
}
