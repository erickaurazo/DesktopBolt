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


namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class OrdenDeTrabajoITEdicionAsignarSoporte : Form
    {

        private PrivilegesByUser privilege;
        private ComboBoxHelper comboHelper;
        private List<Grupo> documentos, series, tipoSolicitudes;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private int codigoSelecionado = 0;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private string desde;
        private string hasta;
        private SAS_DispositivoOrdenTrabajoController model;
        private SAS_ListadoDeDispositivoOrdenTrabajoByIdResult item;
        

        private int ultimoItemEnListaDetalle;
        private int codigoDispositivo;
        private SAS_DispositivoUsuariosController modeloDispositivo;
        private SAS_DispositivoOrdenTrabajo ordenTrabajo;

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
                item = model.GetListById("SAS", this.codigoSelecionado);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void CargarCombo()
        {
            comboHelper = new ComboBoxHelper();
            documentos = new List<Grupo>();
            series = new List<Grupo>();
            tipoSolicitudes = new List<Grupo>();


            documentos = comboHelper.GetDocumentTypeForForm("SAS", "MANTENIMIENTOS TI");
            cboDocumento.DisplayMember = "Descripcion";
            cboDocumento.ValueMember = "Codigo";
            cboDocumento.DataSource = documentos.ToList();

            series = comboHelper.GetDocumentSeriesForForm("SAS", "MANTENIMIENTOS TI");
            cboSerie.DisplayMember = "Descripcion";
            cboSerie.ValueMember = "Codigo";
            cboSerie.DataSource = series.ToList();

            tipoSolicitudes = comboHelper.GetUsuarioParaAsignar("SAS");
            cboUsuarioAsignado.DisplayMember = "Descripcion";
            cboUsuarioAsignado.ValueMember = "Codigo";
            cboUsuarioAsignado.DataSource = tipoSolicitudes.OrderBy(x => x.Descripcion).ToList();



        }


        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (item != null)
                {
                    #region 
                    if (item.codigo != (int?)null)
                    {
                        if (item.codigo == 0)
                        {
                            #region Nuevo() 
                            LimpiarFormulario();
                            btnRegistrar.Enabled = false;
                            btnCancelar.Enabled = true;
                            #endregion
                        }
                        else if (item.codigo > 0)
                        {
                            #region Asignar Objeto para edición() 
                            txtCodigo.Text = item.codigo.ToString();
                            this.txtCorrelativo.Text = item.codigo.ToString().PadLeft(7, '0');
                            cboSerie.SelectedValue = item.idSerie.ToString();
                            cboDocumento.SelectedValue = item.iddocumento.ToString();
                            txtFecha.Text = item.fecha.ToShortDateString();
                            cboUsuarioAsignado.SelectedValue = item.codigoPersonal.Trim();
                            this.txtEstado.Text = item.estado != null ? item.estado.Trim() : string.Empty;
                            #endregion

                            btnRegistrar.Enabled = true;
                            btnCancelar.Enabled = true;

                        }
                    }
                    else
                    {
                        #region Nuevo() 
                        LimpiarFormulario();

                        btnRegistrar.Enabled = true;

                        #endregion
                    }
                    #endregion
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                return;
            }
        }


        private void LimpiarFormulario()
        {
            model = new SAS_DispositivoOrdenTrabajoController();
            int ultimoRegistro = model.ObtenerUltimoOperacion("SAS");
            ultimoItemEnListaDetalle = 1;
            this.txtCodigo.Text = "0"; // cuando es 0 es nuevo
            this.txtCorrelativo.Text = ultimoRegistro.ToString().PadLeft(7, '0'); // traer el último registrado + 1, que solo se va a mostrar.                    
            this.txtEstado.Text = "PENDIENTE";
            this.txtFecha.Text = DateTime.Now.ToShortDateString();
            item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
            item.codigo = 0;

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SAS_DispositivoOrdenTrabajo ObtenerObjeto()
        {
            SAS_DispositivoOrdenTrabajo ot = new SAS_DispositivoOrdenTrabajo();
            ot.codigo = (this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0);
            ot.usuario = cboUsuarioAsignado.SelectedValue.ToString();
            return ot;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                model = new SAS_DispositivoOrdenTrabajoController();
                ordenTrabajo = new SAS_DispositivoOrdenTrabajo();
                //modelForm = new AtencionesSoporteFuncionalReasignarCaso();
                ordenTrabajo = ObtenerObjeto();

                int resultadoRegistro = model.ReasignarUsuario("SAS", ordenTrabajo);
                MessageBox.Show("Se reasigno al colaborador", "MENSAJE DEL SISTEMA");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                return;
            }

            
        }

        public OrdenDeTrabajoITEdicionAsignarSoporte()
        {
            InitializeComponent();
        }

        public OrdenDeTrabajoITEdicionAsignarSoporte(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege, int _codigoSelecionado)
        {            
            InitializeComponent();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            this.codigoSelecionado = _codigoSelecionado;
            gbReasignarCaso.Enabled = false;
            CargarCombo();
            bgwHilo.RunWorkerAsync();
        }

        private void OrdenDeTrabajoITAsignarSoporte_Load(object sender, EventArgs e)
        {

        }
    }
}
