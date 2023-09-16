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

namespace ComparativoHorasVisualSATNISIRA.Presupuestos
{
    public partial class AperturaCierrePresupuestos : Form
    {

        string nombreformulario = "PRESUPUESTO";
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user;
        private string fileName;
        private bool exportVisualSettings;
        private List<SAS_ListadoPresupuestosResult> listadoConsulta;
        private List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult> listadoPresupuestoPeriodo;

        private PresupuestoController Modelo;
        private PRESUPUESTO oPresupuesto;
        private SAS_ListadoPresupuestosResult oDetalleSelecionado;
        private int lastItem;
        private string msgError;
        object result;
        int oParImpar = 0;
        private string codigoPresupuesto = string.Empty;

        public AperturaCierrePresupuestos()
        {
            InitializeComponent();
        }


        public AperturaCierrePresupuestos(string _conection, SAS_USUARIOS _user, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            oPresupuesto = new PRESUPUESTO();
            oPresupuesto.IDPRESUPUESTO = string.Empty;
            nombreformulario = "PRESUPUESTOS";
            conection = _conection;
            user = _user;
            companyId = _companyId;
            privilege = _privilege;
            Consultar();
        }



        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

      

        private void btnAnular_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opción no permitida", "MENSAJE DEL SISTEMA");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            gbEdit.Enabled = false;
            gbList.Enabled = false;
            btnMenu.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void AperturaCierrePresupuestos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsulta();
        }

        private void EjecutarConsulta()
        {
            try
            {
                #region Ejecutar Consulta() 
                Modelo = new PresupuestoController();
                listadoConsulta = new List<SAS_ListadoPresupuestosResult>();
                listadoConsulta = Modelo.GetListView(conection, user).ToList();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultado();

        }

        private void MostrarResultado()
        {
            try
            {
                #region Mostrar Resultados()
                dgvRegistro.DataSource = listadoConsulta.ToDataTable<SAS_ListadoPresupuestosResult>();
                dgvRegistro.Refresh();

                gbEdit.Enabled = !false;
                gbList.Enabled = !false;
                btnMenu.Enabled = !false;
                progressBar1.Visible = !true;

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void AperturaCierrePresupuestos_Load(object sender, EventArgs e)
        {

        }

        private void dgvRegistro_SelectionChanged(object sender, EventArgs e)
        {
            btnActicarPresupuesto.Enabled = false;
            codigoPresupuesto = string.Empty;
            try
            {
                #region Item selecionado();
                oDetalleSelecionado = new SAS_ListadoPresupuestosResult();
                if (dgvRegistro != null && dgvRegistro.Rows.Count > 0)
                {
                    if (dgvRegistro.CurrentRow != null)
                    {
                        if (dgvRegistro.CurrentRow.Cells["chIDPRESUPUESTO"].Value != null)
                        {
                            if (dgvRegistro.CurrentRow.Cells["chIDPRESUPUESTO"].Value.ToString() != string.Empty)
                            {
                                codigoPresupuesto = (dgvRegistro.CurrentRow.Cells["chIDPRESUPUESTO"].Value != null ? (string)Convert.ChangeType(dgvRegistro.CurrentRow.Cells["chIDPRESUPUESTO"].Value.ToString().Trim(), typeof(string)) : string.Empty);
                                var resultado = listadoConsulta.Where(x => x.IDPRESUPUESTO.Trim() == codigoPresupuesto).ToList();
                                if (resultado.ToList().Count == 1)
                                {
                                    oDetalleSelecionado = resultado.Single();
                                    AsignarControlesAObjeto(oDetalleSelecionado);

                                    if (oDetalleSelecionado.IDESTADO.Trim() == "AP")
                                    {
                                        btnActicarPresupuesto.Enabled = true;
                                    }
                                }
                                else if (resultado.ToList().Count > 1)
                                {
                                    oDetalleSelecionado = resultado.Single();
                                    AsignarControlesAObjeto(oDetalleSelecionado);
                                    if (oDetalleSelecionado.IDESTADO.Trim() == "AP")
                                    {
                                        btnActicarPresupuesto.Enabled = true;
                                    }
                                }
                                else
                                {
                                    Limpiar();
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
                throw;
            }


        }

        private void AsignarControlesAObjeto(SAS_ListadoPresupuestosResult oDetalleSelecionado)
        {
            try
            {
                #region Asignar Controles A Objeto ()

                this.txtCodigo.Text = oDetalleSelecionado.IDPRESUPUESTO != null ? oDetalleSelecionado.IDPRESUPUESTO.Trim() : string.Empty;
                this.txtPresupuesto.Text = oDetalleSelecionado.DESCRIPCION != null ? oDetalleSelecionado.DESCRIPCION.Trim() : string.Empty;
                this.txtEstado.Text = oDetalleSelecionado.estado != null ? oDetalleSelecionado.estado.Trim() : string.Empty;
                this.txtIdEstado.Text = oDetalleSelecionado.IDESTADO != null ? oDetalleSelecionado.IDESTADO.Trim() : string.Empty;
                this.txtIdMoneda.Text = oDetalleSelecionado.IDMONEDA != null ? oDetalleSelecionado.IDMONEDA.Trim() : string.Empty;
                this.txtMoneda.Text = oDetalleSelecionado.moneda != null ? oDetalleSelecionado.moneda.Trim() : string.Empty;
                this.txtTcambio.Text = oDetalleSelecionado.tcambio != (decimal?)null ? oDetalleSelecionado.tcambio.Value.ToDecimalPresentation() : string.Empty;

                listadoPresupuestoPeriodo = new List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult>();
                Modelo = new PresupuestoController();
                listadoPresupuestoPeriodo = Modelo.GetListDetail(conection, codigoPresupuesto, user).ToList();               
                dgvDetail.CargarDatos(listadoPresupuestoPeriodo.ToDataTable<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult>());
                dgvDetail.Refresh();

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void Limpiar()
        {
            try
            {
                #region Limpiar()
                this.txtCodigo.Text = string.Empty;
                this.txtPresupuesto.Text = string.Empty;
                this.txtEstado.Text = string.Empty;
                this.txtIdEstado.Text = string.Empty;
                this.txtIdMoneda.Text = string.Empty;
                this.txtMoneda.Text = string.Empty;
                this.txtTcambio.Text = string.Empty;

                listadoPresupuestoPeriodo = new List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult>();
                dgvDetail.DataSource = listadoPresupuestoPeriodo.ToDataTable<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult>();
                dgvDetail.Refresh();


                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnActicarPresupuesto_Click(object sender, EventArgs e)
        {
            Notificar();
        }

        private void Notificar()
        {
            oPresupuesto = new PRESUPUESTO();
            if (codigoPresupuesto != string.Empty)
            {               
                oPresupuesto.IDPRESUPUESTO = codigoPresupuesto;
                gbEdit.Enabled = false;
                gbList.Enabled = false;
                btnMenu.Enabled = false;
                progressBar1.Visible = true;
                bgwNotify.RunWorkerAsync();
            }

           
        }

        private void bgwNotify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Ejecutar consulta()
                Modelo = new PresupuestoController();
                //Modelo.Notify(conection, "RE-ACTIVACION DE PRESUPUESTO", codigoPresupuesto, user);
                Modelo.ChangeState(conection, oPresupuesto, user);
                EjecutarConsulta();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwNotify_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                #region Mostrar resultado de consulta()
                MostrarResultado();
                MessageBox.Show("Se ha cambiado satifactoria el estado del presupuesto", "MENSAJE DEL SISTEMA");

                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }



    }
}
