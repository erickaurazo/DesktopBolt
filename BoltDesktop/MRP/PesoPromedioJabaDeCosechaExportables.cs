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


namespace ComparativoHorasVisualSATNISIRA.MRP
{
    public partial class PesoPromedioJabaDeCosechaExportables : Form
    {

        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection, codigoCultivo, codigoCampana = string.Empty;
        private SAS_PesoPromedioJabasCosechaExportablesController modelo;
        private List<SAS_PesoPromedioJabasCosechaExportablesListado> list;
        private SAS_PesoPromedioJabasCosechaExportablesListado itemSelect;

        private CentroDeCostosController modelCultivos;
        private List<Grupo> listadoCultivos;
        private SAS_PesoPromedioJabasCosechaExportables oItem;
        private List<Grupo> listadoCampañaFiltro;
        private List<Grupo> listadoCultivosEnCampañaFiltro;

        public PesoPromedioJabaDeCosechaExportables()
        {
            InitializeComponent();
            _conection = "SAS";
            _user = new SAS_USUARIOS();
            _user.IdUsuario = "EAURAZO";
            _user.NombreCompleto = "ERICK AURAZO";
            _companyId = "001";
            _privilege = new PrivilegesByUser();
            _privilege.consultar = 1;
            _privilege.editar = 1;
            _privilege.nuevo = 1;
            Inicio();

            Consultar();
        }


        public PesoPromedioJabaDeCosechaExportables(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            Inicio();

            Consultar();

        }

        public void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";

                modelCultivos = new CentroDeCostosController();
                listadoCultivos = new List<Grupo>();
                listadoCultivos = modelCultivos.ListadoCultivos("SAS").ToList();
                


                modelCultivos = new CentroDeCostosController();
                listadoCampañaFiltro = new List<Grupo>();
                listadoCultivosEnCampañaFiltro = new List<Grupo>();
                listadoCampañaFiltro = modelCultivos.GetCampañaAnual("SAS").ToList();
                

                cboCultivo.DisplayMember = "Descripcion";
                cboCultivo.ValueMember = "Codigo";
                cboCultivo.DataSource = listadoCultivos;


                cboCultivoFiltro.DisplayMember = "Descripcion";
                cboCultivoFiltro.ValueMember = "Codigo";
                cboCultivoFiltro.DataSource = listadoCultivos;
                string idcultivoInicio = cboCultivoFiltro.SelectedValue.ToString().Trim();
                listadoCultivosEnCampañaFiltro = modelCultivos.GetListCultivosEnCampañaAgricolaActivas("SAS", idcultivoInicio).ToList();


                cboCampañaAnualFiltro.DisplayMember = "Descripcion";
                cboCampañaAnualFiltro.ValueMember = "Codigo";
                cboCampañaAnualFiltro.DataSource = listadoCultivosEnCampañaFiltro;

                cboCampanaAnual.DisplayMember = "Descripcion";
                cboCampanaAnual.ValueMember = "Codigo";
                cboCampanaAnual.DataSource = listadoCultivosEnCampañaFiltro;

                modelCultivos = new CentroDeCostosController();
                cboVariedad.DisplayMember = "Descripcion";
                cboVariedad.ValueMember = "Codigo";
                cboVariedad.DataSource = modelCultivos.GetListVariedades(_conection, idcultivoInicio).ToList();
                string idVariedadInicio = cboVariedad.SelectedValue.ToString().Trim();

                modelCultivos = new CentroDeCostosController();
                cboTipoCultivo.DisplayMember = "Descripcion";
                cboTipoCultivo.ValueMember = "Codigo";
                cboTipoCultivo.DataSource = modelCultivos.GetTipoCultivo(_conection, idcultivoInicio, idVariedadInicio).ToList();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvRegistro.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvRegistro.TableElement.EndUpdate();
            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvRegistro.MasterTemplate.AutoExpandGroups = true;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistro.GroupDescriptors.Clear();
            this.dgvRegistro.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chcultivo", "Count : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chvalor", "AVG : {0:N2}; ", GridAggregateFunction.Avg));
            this.dgvRegistro.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar Consulta()
                EjecutarConsulta();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
            }

        }

        private void EjecutarConsulta()
        {
            modelo = new SAS_PesoPromedioJabasCosechaExportablesController();
            list = new List<SAS_PesoPromedioJabasCosechaExportablesListado>();
            list = modelo.GetListAll("SAS", codigoCampana, codigoCultivo);
        }

        private void PesoPromedioJabaDeCosechaExportables_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Mostrar resultados()
                MostrarResultados();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
            }
        }

        private void MostrarResultados()
        {
            dgvRegistro.DataSource = list.ToDataTable<SAS_PesoPromedioJabasCosechaExportablesListado>();
            dgvRegistro.Refresh();
            BarraPrincipal.Enabled = true;
            pbMenu01.Visible = false;


            gbEdit.Enabled = false;
            gbList.Enabled = true;
            pbMenu01.Visible = !true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario() == true)
            {
                oItem = new SAS_PesoPromedioJabasCosechaExportables();
                oItem = ObtenerObjeto();
                modelo = new SAS_PesoPromedioJabasCosechaExportablesController();
                modelo.ToRegister(_conection, oItem);
            }
        }

        private SAS_PesoPromedioJabasCosechaExportables ObtenerObjeto()
        {
            SAS_PesoPromedioJabasCosechaExportables pesoPromedio = new SAS_PesoPromedioJabasCosechaExportables();
            try
            {
                #region Validar objeto()
                pesoPromedio.id = Convert.ToInt32(this.txtCodigo.Text);
                pesoPromedio.idempresa = "001";
                pesoPromedio.tipoCultivo = Convert.ToChar(cboTipoCultivo.SelectedValue.ToString().Trim());
                pesoPromedio.idCultivo = cboCultivo.SelectedValue.ToString().Trim();
                pesoPromedio.idVariedad = cboVariedad.SelectedValue.ToString().Trim();
                pesoPromedio.desde = Convert.ToDateTime(this.txtFechaDesde.Text);
                pesoPromedio.hasta = Convert.ToDateTime(this.txtFechaHasta.Text);
                pesoPromedio.valor = Convert.ToDecimal(this.txtCodigo.Text);
                pesoPromedio.estado = Convert.ToByte(this.txtIdEstado.Text);
                pesoPromedio.glosa = Convert.ToString(this.txtObservacion.Text);
                pesoPromedio.registradoPor = _user.IdUsuario;
                pesoPromedio.fechaCreacion = DateTime.Now;
                pesoPromedio.paraReportes = chkVisibleEnReporte.Checked == true ? Convert.ToByte(1) : Convert.ToByte(0);
                pesoPromedio.valorReal = 0;
                pesoPromedio.porcentajeAprovechamiento = 0;
                pesoPromedio.pesoPresentacionPresupuesto = 0;
                pesoPromedio.kgPorFCLPresupuesto = 0;
                pesoPromedio.IdCampana = cboCampanaAnual.SelectedValue.ToString().Trim();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
            }

            return pesoPromedio;
        }

        private bool ValidarFormulario()
        {
            bool estado = false;
            try
            {
                #region Validar objeto()
                if (this.txtValor.Text.Trim() == string.Empty)
                {
                    estado = true;
                }
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return false;
            }

            return estado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            gbEdit.Enabled = true;
            gbList.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiarformulario();
            this.txtCodigo.Text = "0";
            this.txtCodigo.Enabled = true;
            this.txtCodigo.ReadOnly = true;

            this.txtEstado.Enabled = true;
            this.txtEstado.Text = "ACTIVO";
            this.txtEstado.ReadOnly = true;

            this.txtFechaDesde.Enabled = true;
            this.txtFechaDesde.ReadOnly = !true;

            this.txtFechaHasta.Enabled = true;
            this.txtFechaHasta.ReadOnly = !true;

            this.txtIdEstado.Text = "1";
            this.txtIdEstado.Enabled = true;
            this.txtIdEstado.ReadOnly = true;

            this.txtValor.Enabled = true;
            this.txtValor.ReadOnly = !true;

            chkVisibleEnReporte.Enabled = true;
            this.txtFechaDesde.Focus();
            gbEdit.Enabled = true;
            gbList.Enabled = false;


        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

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

        private void PesoPromedioJabaDeCosechaExportables_FormClosing(object sender, FormClosingEventArgs e)
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
            Limpiarformulario();

            try
            {
                #region 
                itemSelect = new SAS_PesoPromedioJabasCosechaExportablesListado();
                itemSelect.id = 0;
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chId"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chId"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvRegistro.CurrentRow.Cells["chId"].Value != null ? Convert.ToInt32(dgvRegistro.CurrentRow.Cells["chId"].Value.ToString()) : 0);

                                if (id != 0)
                                {
                                    var resultado = list.Where(x => x.id == id).ToList();
                                    if (resultado.ToList().Count > 0)
                                    {
                                        itemSelect = resultado.ElementAt(0);

                                        this.txtCodigo.Text = itemSelect.id.ToString();
                                        this.txtFechaDesde.Text = itemSelect.desde.ToPresentationDate();
                                        this.txtFechaHasta.Text = itemSelect.hasta.ToPresentationDate();
                                        this.txtIdEstado.Text = itemSelect.estado.Value.ToString();
                                        this.txtValor.Text = itemSelect.valor.ToString("N9");

                                        if (itemSelect.paraReportes.Value.ToString() == "0")
                                        {
                                            chkVisibleEnReporte.Checked = false;
                                        }
                                        else
                                        {
                                            chkVisibleEnReporte.Checked = !false;
                                        }


                                        if (itemSelect.estado.Value.ToString() == "0")
                                        {
                                            this.txtEstado.Text = "ANULADO";
                                        }
                                        else
                                        {
                                            this.txtEstado.Text = "ACTIVO";
                                        }

                                        //cboCultivo.DisplayMember = "Descripcion";
                                        //cboCultivo.ValueMember = "Codigo";
                                        ////cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                                        //cboCultivo.DataSource = modelCultivos.GetListCultivos(listadoCultivos.Where(x => x.Descripcion == itemSelect.idCultivo.Trim()).ToList()).ToList();

                                        cboCultivo.SelectedValue = itemSelect.idCultivo.Trim();
                                        cboVariedad.SelectedValue = itemSelect.idVariedad.Trim();
                                        cboTipoCultivo.SelectedValue = itemSelect.tipoCultivo.ToString().Trim();
                                        cboCampanaAnual.SelectedValue = itemSelect.IdCampana != null ? itemSelect.IdCampana.Trim() : string.Empty;
                                        //cboVariedad.DisplayMember = "Descripcion";
                                        //cboVariedad.ValueMember = "Codigo";
                                        //cboVariedad.DataSource = modelCultivos.GetListVariedades(_conection,  itemSelect.idCultivo.Trim()).ToList();

                                        //cboTipoCultivo.DisplayMember = "Descripcion";
                                        //cboTipoCultivo.ValueMember = "Codigo";
                                        ////cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                                        //cboTipoCultivo.DataSource = modelCultivos.GetTipoCultivo(_conection,  itemSelect.idCultivo.Trim(), itemSelect.idVariedad.Trim()).ToList();





                                        if (itemSelect.id > 0)
                                        {
                                            this.txtCodigo.Enabled = true;
                                            this.txtCodigo.ReadOnly = true;

                                            this.txtEstado.Enabled = true;
                                            this.txtEstado.ReadOnly = true;

                                            this.txtFechaDesde.Enabled = true;
                                            this.txtFechaDesde.ReadOnly = !true;

                                            this.txtFechaHasta.Enabled = true;
                                            this.txtFechaHasta.ReadOnly = !true;

                                            this.txtIdEstado.Enabled = true;
                                            this.txtIdEstado.ReadOnly = true;

                                            this.txtValor.Enabled = true;
                                            this.txtValor.ReadOnly = !true;

                                            chkVisibleEnReporte.Enabled = true;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        private void Limpiarformulario()
        {
            this.txtCodigo.Enabled = false;
            this.txtEstado.Enabled = false;
            this.txtFechaDesde.Enabled = false;
            this.txtFechaHasta.Enabled = false;
            this.txtIdEstado.Enabled = false;
            this.txtValor.Enabled = false;
            chkVisibleEnReporte.Enabled = false;
            this.txtCodigo.Clear();
            this.txtEstado.Clear();
            this.txtFechaDesde.Clear();
            this.txtFechaHasta.Clear();
            this.txtIdEstado.Clear();
            this.txtValor.Clear();
            chkVisibleEnReporte.Checked = false;            

            //cboCultivo.DisplayMember = "Descripcion";
            //cboCultivo.ValueMember = "Codigo";            
            //cboCultivo.DataSource = modelCultivos.GetListCultivos(listadoCultivos.Where(x => x.Codigo == "").ToList()).ToList();

            //cboVariedad.DisplayMember = "Descripcion";
            //cboVariedad.ValueMember = "Codigo";            
            //cboVariedad.DataSource = modelCultivos.GetListVariedades(_conection, "").ToList();

            //cboTipoCultivo.DisplayMember = "Descripcion";
            //cboTipoCultivo.ValueMember = "Codigo";            
            //cboTipoCultivo.DataSource = modelCultivos.GetTipoCultivo(_conection,"", "").ToList();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            BarraPrincipal.Enabled = !true;
            pbMenu01.Visible = !false;
            codigoCultivo = cboCultivoFiltro.SelectedValue.ToString().Trim();
            codigoCampana = cboCampañaAnualFiltro.SelectedValue.ToString().Trim();
            bgwHilo.RunWorkerAsync();
        }

        private void cboCultivo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboCultivo.SelectedIndex > -1)
            {
                cboVariedad.DisplayMember = "Descripcion";
                cboVariedad.ValueMember = "Codigo";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboVariedad.DataSource = modelCultivos.GetListVariedades(_conection, cboCultivo.SelectedValue.ToString()).ToList();
            }
        }

        private void cboCultivoFiltro_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboCultivoFiltro.SelectedIndex > -1)
            {
                string idcultivoInicio = cboCultivoFiltro.SelectedValue.ToString().Trim();
                listadoCultivosEnCampañaFiltro = modelCultivos.GetListCultivosEnCampañaAgricolaActivas("SAS", idcultivoInicio).ToList();


                cboCampañaAnualFiltro.DisplayMember = "Descripcion";
                cboCampañaAnualFiltro.ValueMember = "Codigo";
                cboCampañaAnualFiltro.DataSource = listadoCultivosEnCampañaFiltro;

                cboCampanaAnual.DisplayMember = "Descripcion";
                cboCampanaAnual.ValueMember = "Codigo";
                cboCampanaAnual.DataSource = listadoCultivosEnCampañaFiltro;

                modelCultivos = new CentroDeCostosController();
                cboVariedad.DisplayMember = "Descripcion";
                cboVariedad.ValueMember = "Codigo";
                cboVariedad.DataSource = modelCultivos.GetListVariedades(_conection, idcultivoInicio).ToList();
                string idVariedadInicio = cboVariedad.SelectedValue.ToString().Trim();

                modelCultivos = new CentroDeCostosController();
                cboTipoCultivo.DisplayMember = "Descripcion";
                cboTipoCultivo.ValueMember = "Codigo";
                cboTipoCultivo.DataSource = modelCultivos.GetTipoCultivo(_conection, idcultivoInicio).ToList();

            }
        }

        private void cboVariedad_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboVariedad.SelectedIndex > -1)
            {
                cboTipoCultivo.DisplayMember = "Descripcion";
                cboTipoCultivo.ValueMember = "Codigo";
                //cboMes.DataSource = MesesNeg.ListarMeses().Where(x => x.Valor != "13" && x.Valor != "00").ToList();
                cboTipoCultivo.DataSource = modelCultivos.GetTipoCultivo(_conection, cboCultivo.SelectedValue.ToString(), cboVariedad.SelectedValue.ToString()).ToList();
            }
        }

        private void btnElegirColumnas_Click(object sender, EventArgs e)
        {
            this.dgvRegistro.ShowColumnChooser();
        }
    }
}
