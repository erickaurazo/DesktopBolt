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
using Telerik.WinControls.UI;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class ReporteDetalleReferenciasBySolicitud : Form
    {

        private int _idDispositivo;
        private string _nombreDispositivo;
        private string _oConexion;
        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user2;
        private ComboBoxHelper comboHelper;
        private List<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult> listado;
        private SAS_DispostivoController deviceModelo;
        public ReporteDetalleReferenciasBySolicitud()
        {
            InitializeComponent();
            Inicio();
        }


        public ReporteDetalleReferenciasBySolicitud(string oConexion, int idDispositivo, string nombreDispositivo, SAS_USUARIOS user2, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            Inicio();
            _oConexion = oConexion;
            _idDispositivo = idDispositivo;
            _user2 = user2;
            _privilege = privilege;
            _nombreDispositivo = nombreDispositivo;
            this.txtDispositivoCodigo.Text = _idDispositivo.ToString().PadLeft(7, '0');
            this.txtDispositivoDescripcion.Text = _nombreDispositivo.Trim();
            Consultar();
        }


        protected override void OnLoad(EventArgs e)
        {
            this.dgvListado.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chdocumento", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
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


        private void btnConsultar_Click(object sender, EventArgs e)
        {

            
            Consultar();

        }

        private void Consultar()
        {
            _idDispositivo = this.txtDispositivoCodigo.Text != string.Empty ? Convert.ToInt32(this.txtDispositivoCodigo.Text) : 0;
            gbDispositivo.Enabled = false;
            gbList.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void ReporteDetalleReferenciasBySolicitud_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            deviceModelo = new SAS_DispostivoController();
            listado = new List<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult>();
            listado = deviceModelo.ObtainListOfReferenceDocumentsbyDeviceCode("SAS", _idDispositivo).ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvListado.DataSource = listado.ToDataTable<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult>();
            dgvListado.Refresh();
            gbDispositivo.Enabled = !false;
            gbList.Enabled = !false;
            progressBar1.Visible = !true;
        }
    }
}
