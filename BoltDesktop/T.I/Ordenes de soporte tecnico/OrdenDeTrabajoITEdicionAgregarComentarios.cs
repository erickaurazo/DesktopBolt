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
    public partial class OrdenDeTrabajoITEdicionAgregarComentarios : Form
    {
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
        private SAS_DispositivoOrdenTrabajoController model;
        private SAS_ListadoDeDispositivoOrdenTrabajoByIdResult item;
        private int ultimoItemEnListaDetalle;
        private int codigoDispositivo;
        private SAS_DispositivoUsuariosController modeloDispositivo;
        private SAS_DispositivoOrdenTrabajo ordenTrabajo;

        public OrdenDeTrabajoITEdicionAgregarComentarios()
        {
            InitializeComponent();
        }

        public OrdenDeTrabajoITEdicionAgregarComentarios(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, int _codigoSelecionado)
        {
            InitializeComponent();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSelecionado;
            gbReasignarCaso.Enabled = false;
            CargarCombo();
            bgwHilo.RunWorkerAsync();
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

        }


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

        private void btnCancelar_Click(object sender, EventArgs e)
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


        private SAS_DispositivoOrdenTrabajo ObtenerObjeto()
        {
            SAS_DispositivoOrdenTrabajo ot = new SAS_DispositivoOrdenTrabajo();
            ot.codigo = (this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text.Trim()) : 0);
            ot.Glosa01 = this.txtComentario01.Text.Trim();
            ot.Glosa02 = this.txtComentario02.Text.Trim();
            ot.Glosa03 = this.txtComentario03.Text.Trim();
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

                int resultadoRegistro = model.SetComments("SAS", ordenTrabajo);
                MessageBox.Show("Actualización realizada correctamente", "MENSAJE DEL SISTEMA");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                return;
            }
        }

        private void OrdenDeTrabajoITEdicionAgregarComentarios_Load(object sender, EventArgs e)
        {

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
            this.txtComentario01.Clear();
            this.txtComentario02.Clear();
            this.txtComentario03.Clear();
            item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
            item.codigo = 0;

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
                            txtComentario01.Text = item.glosa01.Trim();
                            txtComentario02.Text = item.glosa02.Trim();
                            txtComentario03.Text = item.glosa03.Trim();
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
    }
}
