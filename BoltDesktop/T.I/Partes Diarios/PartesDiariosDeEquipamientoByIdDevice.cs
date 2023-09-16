using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using System.Reflection;
using Telerik.WinControls.UI.Export;
using System.Globalization;
using System.Threading;
namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    public partial class PartesDiariosDeEquipamientoByIdDevice : Form
    {
        #region Variables()
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_ParteDiariosDeDispositivosController model;
        private int NumeroSemana;
        private int periodoSelecionado;
        private int semanaSelecionada;
        private List<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult> list;
        private ComboBoxHelper comboHelper;
        private List<SAS_ParteDiariosDeDispositivosDetalle> detail;
        public int ResultadoConsulta = 0;
        private int CodigoDispositivo = 0;

        #endregion
        public PartesDiariosDeEquipamientoByIdDevice()
        {
            InitializeComponent();
        }


        public PartesDiariosDeEquipamientoByIdDevice(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            try
            {
                InitializeComponent();
                Inicio();
                RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
                RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
                RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
                RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
                conection = _conection;
                user2 = _user2;
                companyId = _companyId;
                privilege = _privilege;
                CargarSemanas();
                ObtenerFechasIniciales();
                CodigoDispositivo = 0;
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }

        public PartesDiariosDeEquipamientoByIdDevice(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _CodigoDispositivo, string dispositivo)
        {
            try
            {
                InitializeComponent();
                Inicio();
                RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
                RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
                RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
                RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
                CodigoDispositivo = _CodigoDispositivo;
                this.txtDipositivoCodigo.Text = CodigoDispositivo.ToString();
                this.txtDipositivoDescripcion.Text = dispositivo.Trim();
                conection = _conection;
                user2 = _user2;
                companyId = _companyId;
                privilege = _privilege;
                CargarSemanas();
                ObtenerFechasIniciales();
                Actualizar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void CargarSemanas()
        {

            MesesNeg = new MesController();
            cboSemana.DisplayMember = "descripcion";
            cboSemana.ValueMember = "valor";
            //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
            cboSemana.DataSource = MesesNeg.ListadoSemanasPorAnio(conection, Convert.ToInt32(this.txtPeriodo.Value)).ToList();


            var d = DateTime.Now;
            CultureInfo cul = CultureInfo.CurrentCulture;

            // Usa la fecha formateada y calcula el número de la semana
            NumeroSemana = cul.Calendar.GetWeekOfYear(
                 d,
                 CalendarWeekRule.FirstDay,
                 DayOfWeek.Sunday);



            cboSemana.SelectedValue = NumeroSemana.ToString();
        }

        private void ObtenerFechasIniciales()
        {
            try
            {
                model = new SAS_ParteDiariosDeDispositivosController();
                SAS_PeriodoMaquinaria result01 = new SAS_PeriodoMaquinaria();
                result01 = model.ObtenerSemana(conection, NumeroSemana, DateTime.Now.Year);
                //this.txtPeriodo.Value = Convert.ToDecimal(result01.anio);
                this.txtFechaDesde.Text = result01.fechaInicio.Value.ToPresentationDate();
                this.txtFechaHasta.Text = result01.fechaFinal.Value.ToPresentationDate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void ObtenerFechas(int numeroSemana, int anio)
        {
            try
            {

                model = new SAS_ParteDiariosDeDispositivosController();
                SAS_PeriodoMaquinaria result01 = new SAS_PeriodoMaquinaria();
                result01 = model.ObtenerSemana(conection, numeroSemana, anio);
                this.txtFechaDesde.Text = result01.fechaInicio.Value.ToPresentationDate();
                this.txtFechaHasta.Text = result01.fechaFinal.Value.ToPresentationDate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void Actualizar()
        {
            try
            {
                BarraPrincipal.Enabled = false;
                btnNuevo.Enabled = false;
                gbCabecera02.Enabled = false;
                gbDetalle.Enabled = false;
                btnConsultar.Enabled = false;
                pbar.Visible = true;
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
                CodigoDispositivo = Convert.ToInt32(this.txtDipositivoCodigo.Text.Trim() != string.Empty ? (this.txtDipositivoCodigo.Text.Trim()) : "0");
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
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


        private void PartesDiariosDeEquipamientoByIdDevice_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Método Asincrono
                EjecutarMetodoAsincrono();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void EjecutarMetodoAsincrono()
        {
            try
            {
                list = new List<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult>();
                model = new SAS_ParteDiariosDeDispositivosController();

                list = model.ObtenerDocumentosDePartesDiariosPorPeriodoYporCodigoDeDispositivo(conection, desde, hasta, CodigoDispositivo).ToList();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                #region Método Asincrono
                MostrarResultadosDeMetodoAsincrono();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMAS");
                return;
            }


        }

        private void MostrarResultadosDeMetodoAsincrono()
        {
            dgvDetalle.CargarDatos(list.ToDataTable<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult>());
            dgvDetalle.Refresh();
            BarraPrincipal.Enabled = !false;
            btnNuevo.Enabled = !false;
            gbCabecera02.Enabled = !false;
            gbDetalle.Enabled = !false;
            btnConsultar.Enabled = !false;
            pbar.Visible = !true;
        }

        private void txtPeriodo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                periodoSelecionado = Convert.ToInt32(this.txtPeriodo.Value);
                semanaSelecionada = Convert.ToInt32(cboSemana.SelectedValue);
                ObtenerFechas(semanaSelecionada, periodoSelecionado);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void cboSemana_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            try
            {
                periodoSelecionado = Convert.ToInt32(this.txtPeriodo.Value);
                semanaSelecionada = Convert.ToInt32(cboSemana.SelectedValue);
                ObtenerFechas(semanaSelecionada, periodoSelecionado);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            comboHelper = new ComboBoxHelper();

            int totalHorasPorTurno = 24;
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Eventos con la tecla presionar() 
                string estado = this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoId"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chEstadoId"].Value.ToString() : "0";
                if (estado == "1")
                {
                    if (user2 != null)
                    {

                        #region Minutos Inactivos()
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chHorasInactivas")
                        {
                            int codigoMotivoInactividad = Convert.ToInt32(this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value : 0);
                            if (codigoMotivoInactividad > 0)
                            {
                                #region Solo los que marcaron motivo 0
                                if (e.KeyCode == Keys.F3)
                                {
                                    frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                    comboHelper = new ComboBoxHelper();
                                    search.ListaGeneralResultado = comboHelper.HorasActivosPartesEquipamiento("SAS");
                                    search.Text = "Minutos Inactivos";
                                    search.txtTextoFiltro.Text = "";
                                    if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasInactivas"].Value = search.ObjetoRetorno.Codigo;
                                        Decimal totalHorasInactivas = Convert.ToDecimal(search.ObjetoRetorno.Codigo);
                                        Decimal totalHorasActivos = Convert.ToDecimal(totalHorasPorTurno - totalHorasInactivas);
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasActivas"].Value = totalHorasActivos.ToString();
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Motivo de Inactivo()
                        if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chMotivoInactivo")
                        {
                            //int codigoMotivoInactividad = Convert.ToInt32(this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value != null ? this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value : 0);
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                comboHelper = new ComboBoxHelper();
                                search.ListaGeneralResultado = comboHelper.ObtenerListadoDeMotivosDeInactividad("SAS");
                                search.Text = "Motivo de inactividad";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    int codigoMotivoInactivo = Convert.ToInt32(search.ObjetoRetorno.Codigo);
                                    if (codigoMotivoInactivo > 0)
                                    {
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value = search.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivo"].Value = search.ObjetoRetorno.Descripcion;
                                    }
                                    else
                                    {
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivoCodigo"].Value = search.ObjetoRetorno.Codigo;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMotivoInactivo"].Value = search.ObjetoRetorno.Descripcion;
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasInactivas"].Value = 0.ToString();
                                        this.dgvDetalle.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chHorasActivas"].Value = totalHorasPorTurno.ToString();
                                    }
                                }
                            }
                        }
                        #endregion

                    }
                }
                #endregion
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Actualizar();
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

        private void PartesDiariosDeEquipamientoByIdDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Registrar();
        }

        private void Registrar()
        {
            try
            {
                #region Registrar()
                BarraPrincipal.Enabled = false;
                btnNuevo.Enabled = false;
                gbCabecera02.Enabled = false;
                gbDetalle.Enabled = false;
                btnConsultar.Enabled = false;
                pbar.Visible = true;
                desde = this.txtFechaDesde.Text.Trim();
                hasta = this.txtFechaHasta.Text.Trim();
                CodigoDispositivo = Convert.ToInt32(this.txtDipositivoCodigo.Text.Trim());


                detail = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                if (dgvDetalle != null)
                {
                    if (dgvDetalle.RowCount > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalle.Rows)
                        {
                            if (fila.Cells["chId"].Value.ToString().Trim() != String.Empty)
                            {
                                SAS_ParteDiariosDeDispositivosDetalle oDetail = new SAS_ParteDiariosDeDispositivosDetalle();

                                oDetail.Codigo = fila.Cells["chId"].Value != null ? Convert.ToInt32(fila.Cells["chId"].Value.ToString().Trim()) : 0;
                                oDetail.DispositivoCodigo = CodigoDispositivo;
                                oDetail.Item = fila.Cells["chItemDetalle"].Value != null ? fila.Cells["chItemDetalle"].Value.ToString().Trim() : string.Empty;
                                oDetail.HorasActivas = fila.Cells["chHorasActivas"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasActivas"].Value.ToString().Trim()) : 0;
                                oDetail.HorasInactivas = fila.Cells["chHorasInactivas"].Value != null ? Convert.ToDecimal(fila.Cells["chHorasInactivas"].Value.ToString().Trim()) : 0;
                                oDetail.Observacion = fila.Cells["chObservacion"].Value != null ? fila.Cells["chObservacion"].Value.ToString().Trim() : string.Empty;
                                oDetail.MotivoInactivoCodigo = fila.Cells["chMotivoInactivoCodigo"].Value != null ? Convert.ToInt32(fila.Cells["chMotivoInactivoCodigo"].Value.ToString().Trim()) : 0;
                                oDetail.Estado = fila.Cells["chEstadoId"].Value != null ? Convert.ToByte(fila.Cells["chEstadoId"].Value.ToString().Trim()) : Convert.ToByte(0);
                                detail.Add(oDetail);
                            }

                        }
                    }
                }


                bgwRegistrar.RunWorkerAsync();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar método asincrono()
                ResultadoConsulta = model.ActualizarProgramacionDiariaDeDispositivoDesdeListaSemanal(conection, detail);

                EjecutarMetodoAsincrono();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Finalziar método asincrono()
                MostrarResultadosDeMetodoAsincrono();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }
    }
}
