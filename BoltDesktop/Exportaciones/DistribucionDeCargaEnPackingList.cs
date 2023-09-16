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
using System.Globalization;
using ComparativoHorasVisualSATNISIRA.Produccion;

namespace ComparativoHorasVisualSATNISIRA.Exportaciones
{
    public partial class DistribucionDeCargaEnPackingList : Form
    {
        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private ComboBoxHelper comboBoxHelper;
        private string desde;
        private string hasta;
        private string desdeFormato112;
        private string hastaFormato112;
        private PackingListController modelo;
        private List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listado;
        private List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listadoAGrabar;
        private List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listadoFiltro;
        private SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult itemSelecionado;
        private SAS_ListadoDePackingList itemSelecionadoFormularioPadre;
        private int VerSoloCultivoVID = 0;
        private int VerSoloExportable = 0;

        public DistribucionDeCargaEnPackingList()
        {
            InitializeComponent();
        }

        public DistribucionDeCargaEnPackingList(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, SAS_ListadoDePackingList _itemSelecionadoFormularioPadre)
        {
            InitializeComponent();

            //btnEditarRegistro.Enabled = false;
            //btnAnularRegistro.Enabled = false;
            //btnEliminarRegistro.Enabled = false;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            itemSelecionadoFormularioPadre = _itemSelecionadoFormularioPadre;

            this.txtCliente.Text = itemSelecionadoFormularioPadre.cliente != null ? itemSelecionadoFormularioPadre.cliente.Trim() : string.Empty;
            this.txtCodigo.Text = itemSelecionadoFormularioPadre.codigo != null ? itemSelecionadoFormularioPadre.codigo.Trim() : string.Empty;
            this.txtContenedor.Text = itemSelecionadoFormularioPadre.contenedor != null ? itemSelecionadoFormularioPadre.contenedor.Trim() : string.Empty;
            this.txtDocumento.Text = itemSelecionadoFormularioPadre.documento != null ? itemSelecionadoFormularioPadre.documento.Trim() : string.Empty;
            this.txtFecha.Text = itemSelecionadoFormularioPadre.fecha != null ? itemSelecionadoFormularioPadre.fecha.ToPresentationDate().Trim() : string.Empty;
            this.txtGRE.Text = itemSelecionadoFormularioPadre.guiaDeRemision != null ? itemSelecionadoFormularioPadre.guiaDeRemision.Trim() : string.Empty;
            this.txtNumeroPackingList.Text = itemSelecionadoFormularioPadre.numeroDePackingList != null ? itemSelecionadoFormularioPadre.numeroDePackingList.Trim() : string.Empty;
            this.txtTermoregistro.Text = itemSelecionadoFormularioPadre.termoregistros != null ? itemSelecionadoFormularioPadre.termoregistros.Trim() : string.Empty;


            Consultar();



        }

        private void Consultar()
        {
            gbDetalle.Enabled = false;
            GbDatosGeneralesPCK.Enabled = false;
            pbDetalle.Visible = true;
            btnActualizarDistribucion.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void DistribucionDeCargaEnPackingList_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                modelo = new PackingListController();
                listado = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
                listado = modelo.GetListadoPalletasParaDistribuir(conection, itemSelecionadoFormularioPadre.codigo);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                dgvPalletADistribuir.DataSource = listado.ToDataTable<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
                dgvPalletADistribuir.Refresh();
                gbDetalle.Enabled = !false;
                GbDatosGeneralesPCK.Enabled = !false;
                pbDetalle.Visible = !true;
                btnActualizarDistribucion.Enabled = !false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEA");
                return;
            }

        }

        private void dgvPalletADistribuir_SelectionChanged(object sender, EventArgs e)
        {
            #region 
            if (dgvPalletADistribuir.Rows.Count > 0)
            {
                if (dgvPalletADistribuir.CurrentRow != null && dgvPalletADistribuir.CurrentRow.Cells["chCodigo"].Value != null)
                {
                    int valor = dgvPalletADistribuir.CurrentRow.Cells["chposicion"].Value != null ? Convert.ToInt32(dgvPalletADistribuir.CurrentRow.Cells["chposicion"].Value) : 0;

                    if (valor == 0)
                    {
                        dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 0;
                    }
                    else
                    {


                        if (valor % 2 == 0)
                        {
                            // es par
                            dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 1;
                        }
                        else
                        {
                            // impar
                            dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 2;
                        }
                    }
                    switch (valor)
                    {
                        case 1:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 1;
                            break;


                        case 2:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 1;
                            break;


                        case 3:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 2;
                            break;


                        case 4:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 2;
                            break;


                        case 5:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 3;
                            break;


                        case 6:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 3;
                            break;


                        case 7:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 4;
                            break;


                        case 8:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 4;
                            break;

                        case 9:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 5;
                            break;


                        case 10:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 5;
                            break;


                        case 11:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 6;
                            break;


                        case 12:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 6;
                            break;


                        case 13:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 7;
                            break;


                        case 14:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 7;
                            break;


                        case 15:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 8;
                            break;


                        case 16:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 8;
                            break;


                        case 17:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 9;
                            break;


                        case 18:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 9;
                            break;

                        case 19:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 10;
                            break;


                        case 20:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 10;
                            break;


                        default:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 0;
                            break;
                    }

                }
            }

            #endregion

        }

        private void dgvPalletADistribuir_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            #region 
            if (dgvPalletADistribuir.Rows.Count > 0)
            {
                if (dgvPalletADistribuir.CurrentRow != null && dgvPalletADistribuir.CurrentRow.Cells["chCodigo"].Value != null)
                {
                    int valor = dgvPalletADistribuir.CurrentRow.Cells["chposicion"].Value != null ? Convert.ToInt32(dgvPalletADistribuir.CurrentRow.Cells["chposicion"].Value) : 0;

                    if (valor == 0)
                    {
                        dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 0;
                    }
                    else
                    {


                        if (valor % 2 == 0)
                        {
                            // es par
                            dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 1;
                        }
                        else
                        {
                            // impar
                            dgvPalletADistribuir.CurrentRow.Cells["chcolumna"].Value = 2;
                        }
                    }
                    switch (valor)
                    {
                        case 1:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 1;
                            break;


                        case 2:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 1;
                            break;


                        case 3:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 2;
                            break;


                        case 4:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 2;
                            break;


                        case 5:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 3;
                            break;


                        case 6:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 3;
                            break;


                        case 7:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 4;
                            break;


                        case 8:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 4;
                            break;

                        case 9:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 5;
                            break;


                        case 10:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 5;
                            break;


                        case 11:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 6;
                            break;


                        case 12:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 6;
                            break;


                        case 13:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 7;
                            break;


                        case 14:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 7;
                            break;


                        case 15:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 8;
                            break;


                        case 16:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 8;
                            break;


                        case 17:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 9;
                            break;


                        case 18:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 9;
                            break;

                        case 19:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 10;
                            break;


                        case 20:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 10;
                            break;


                        default:
                            dgvPalletADistribuir.CurrentRow.Cells["chfila"].Value = 0;
                            break;
                    }

                }
            }

            #endregion
        }

        private void btnActualizarDistribucion_Click(object sender, EventArgs e)
        {

            try
            {
                if (Validar() == true)
                {
                    listadoAGrabar = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
                    listadoAGrabar = ObtenerListadoObjeto();
                    modelo = new PackingListController();
                    modelo.ToUpdateDistributionPallet(conection, listadoAGrabar);
                    MessageBox.Show("Se ha realizado correctamente el registro", "CONFIRMACIÓ DEL SISTEMA");
                    Consultar();
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return;
            }


          
        }

        private List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> ObtenerListadoObjeto()
        {
            List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listadoRecord = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
            if (dgvPalletADistribuir != null)
            {
                if (dgvPalletADistribuir.Rows.Count > 0)
                {
                    foreach (Telerik.WinControls.UI.GridViewRowInfo row in dgvPalletADistribuir.Rows)
                    {
                        SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult itemRecord = new SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult();
                        itemRecord.idpackinglist = dgvPalletADistribuir.Rows[row.Index].Cells["chCodigo"].Value.ToString().Trim();
                        itemRecord.numpaleta = dgvPalletADistribuir.Rows[row.Index].Cells["chnumpaleta"].Value.ToString().Trim();
                        itemRecord.cantidad = dgvPalletADistribuir.Rows[row.Index].Cells["chCantidad"].Value != null ? Convert.ToInt32(dgvPalletADistribuir.Rows[row.Index].Cells["chCantidad"].Value) : 0;
                        itemRecord.ubicacionTermoRegistro = dgvPalletADistribuir.Rows[row.Index].Cells["chubicacionTermoRegistro"].Value.ToString().Trim();
                        itemRecord.IDPRODUCTO = dgvPalletADistribuir.Rows[row.Index].Cells["chIDPRODUCTO"].Value.ToString().Trim();
                        itemRecord.posicion = dgvPalletADistribuir.Rows[row.Index].Cells["chPOSICION"].Value != null ? Convert.ToInt32(dgvPalletADistribuir.Rows[row.Index].Cells["chPOSICION"].Value) : 0;
                        itemRecord.fila = dgvPalletADistribuir.Rows[row.Index].Cells["chFILA"].Value != null ? Convert.ToString(dgvPalletADistribuir.Rows[row.Index].Cells["chFILA"].Value).Trim() : string.Empty;
                        itemRecord.columna = dgvPalletADistribuir.Rows[row.Index].Cells["chCOLUMNA"].Value != null ? Convert.ToString(dgvPalletADistribuir.Rows[row.Index].Cells["chCOLUMNA"].Value).Trim() : string.Empty;
                        listadoRecord.Add(itemRecord);
                    }
                }
            }

            return listadoRecord;
        }

        private bool Validar()
        {
            bool estado = true;
            try
            {
                listadoAGrabar = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
                listadoAGrabar = ObtenerListadoObjeto();

                var resultValidar01 = (from itemValidar in listadoAGrabar.Where(x => x.posicion > 0).ToList()
                                       group itemValidar by new { itemValidar.posicion } into j
                                       select new
                                       {
                                           posicionValidar = j.Key.posicion,
                                           cantidadRegistro = j.Count()
                                       }
                                      ).ToList();

                if (resultValidar01 != null)
                {
                    var resultadoValidar02 = resultValidar01.Where(x => x.cantidadRegistro > 1).ToList();
                    if (resultadoValidar02.Count > 0)
                    {
                        string mensajeParAdvertir = string.Empty;

                        foreach (var item in resultadoValidar02)
                        {
                            mensajeParAdvertir += item.posicionValidar.ToString() + " | ";
                        }

                        MessageBox.Show("Existe posición(es) duplicada(s), por favor verificar la distribución : " + mensajeParAdvertir, "Mensaje del sistema");
                        return false;
                    }
                }
                return estado;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return false;
            }
        }

        private void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            GridViewDataRowInfo dataRowInfo = dgvPalletADistribuir.CurrentRow as GridViewDataRowInfo;
            if (dataRowInfo != null)
            {
                dgvPalletADistribuir.Rows.Remove(dataRowInfo);
            }         
        }
    }
}
