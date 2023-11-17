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
using System.Drawing;
using MyControlsDataBinding.Controles;
using Asistencia.Negocios.Calidad;
using Asistencia.Negocios.ProduccionPacking;

namespace ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga
{
    public partial class ConformacionDeCargaAgregarPallets : Form
    {

        #region Variables() 
        private int periodo;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private PrivilegesByUser privilege;
        private SAS_ListadoConformacionDeCargaPBIByIdResult itemCabecera;
        private List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteId = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
        private List<SAS_ListadoConformacionDeCargaPBIByIdResult> resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();

        SAS_CondormidadDeCargaController model;
        SAS_ConformacionDeCarga itemRegistar;
        List<SAS_ConformacionDeCargaDetalle> detalleRegistro;
        private int ClickResaltarResultados = 0;
        private int ClickFiltro = 0;
        private string connection = "SAS";
        private string companyId = "001";
        private ExportToExcelHelper modelExportToExcel;
        private int resultadoOperacion = 0;
        private int Id = 0;
        private int ParImparFiltro;
        private string ClienteId;
        private List<Grupo> listadoTipopallet;
        private List<Grupo> listadoCalibres;
        private List<Grupo> listadoVariedad;
        private List<Grupo> listadoColor;
        private List<Grupo> listadoEnvase;
        private List<Grupo> listadoPatihuela;
        private List<Grupo> listadoCategoria;
        private List<Grupo> listadoEmbalaje;
        private List<string> listadoTipoPalletElegido;
        private List<string> listadoCalibresElegido;
        private List<string> listadoVariedadElegido;
        private List<string> listadoColorElegido;
        private List<string> listadoEnvaseElegido;
        private List<string> listadoParihuelaElegido;
        private List<string> listadoCategoriaElegido;
        private List<string> listadoEmbalajeElegido;

        #endregion


        public ConformacionDeCargaAgregarPallets()
        {
            InitializeComponent();
        }

        public ConformacionDeCargaAgregarPallets(string _connection, SAS_USUARIOS _userLogin, string _companyId, PrivilegesByUser _privilege, int _Id, string _ClienteId)
        {
            InitializeComponent();
            resultadoOperacion = 0;
            Id = _Id;
            ClienteId = _ClienteId;
            connection = _connection;
            userLogin = _userLogin;
            user = _userLogin;
            companyId = _companyId;
            privilege = _privilege;
            lblCodeUser.Text = userLogin.IdUsuario != null ? userLogin.IdUsuario.Trim() : Environment.UserName;
            lblFullName.Text = userLogin.NombreCompleto != null ? userLogin.NombreCompleto.Trim() : Environment.UserName;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();
            if (_Id > 0)
            {
                Consultar();
            }


        }

        private void Consultar()
        {
            gbCabecera.Enabled = false;
            gbDetalle.Enabled = false;
            gbDetalle.Enabled = false;
            pgbar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            EjecutarConsultaAsincrona();
        }

        private void EjecutarConsultaAsincrona()
        {

            try
            {
                #region Consultar()

                resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                model = new SAS_CondormidadDeCargaController();
                resultListDetail = model.ListAllDetailById(connection, Id).ToList();

                listadoPalletPendientesByClienteId = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
                model = new SAS_CondormidadDeCargaController();

                listadoPalletPendientesByClienteId = model.ListadoPalletasPendientesByClienteId(connection, ClienteId.Trim());

                listadoTipopallet = new List<Grupo>();
                listadoCalibres = new List<Grupo>();
                listadoVariedad = new List<Grupo>();
                listadoColor = new List<Grupo>();
                listadoEnvase = new List<Grupo>();
                listadoPatihuela = new List<Grupo>();
                listadoCategoria = new List<Grupo>();
                listadoEmbalaje = new List<Grupo>();

                if (listadoPalletPendientesByClienteId != null && listadoPalletPendientesByClienteId.ToList().Count > 0)
                {
                    listadoTipopallet = (from item in listadoPalletPendientesByClienteId
                                         group item by new { item.TipoDePaletaId } into j
                                         select new Grupo
                                         {
                                             Valor = j.Key.TipoDePaletaId,
                                             Descripcion = j.FirstOrDefault().TipoDePaleta
                                         }).ToList();


                    listadoCalibres = (from item in listadoPalletPendientesByClienteId
                                         group item by new { item.CalibreId } into j
                                         select new Grupo
                                         {
                                             Valor = j.Key.CalibreId,
                                             Descripcion = j.FirstOrDefault().Calibre
                                         }).ToList();


                    listadoVariedad = (from item in listadoPalletPendientesByClienteId
                                       group item by new { item.VariedadId } into j
                                       select new Grupo
                                       {
                                           Valor = j.Key.VariedadId,
                                           Descripcion = j.FirstOrDefault().Variedad
                                       }).ToList();


                    listadoColor = (from item in listadoPalletPendientesByClienteId
                                       group item by new { item.ColorId } into j
                                       select new Grupo
                                       {
                                           Valor = j.Key.ColorId,
                                           Descripcion = j.FirstOrDefault().Color
                                       }).ToList();

                    listadoEnvase = (from item in listadoPalletPendientesByClienteId
                                       group item by new { item.EnvaseId } into j
                                       select new Grupo
                                       {
                                           Valor = j.Key.EnvaseId,
                                           Descripcion = j.FirstOrDefault().Envase
                                       }).ToList();

                    listadoPatihuela = (from item in listadoPalletPendientesByClienteId
                                       group item by new { item.PahiruelaId } into j
                                       select new Grupo
                                       {
                                           Valor = j.Key.PahiruelaId,
                                           Descripcion = j.FirstOrDefault().Pahiruela
                                       }).ToList();



                    listadoCategoria = (from item in listadoPalletPendientesByClienteId
                                        group item by new { item.CategoriaId } into j
                                        select new Grupo
                                        {
                                            Valor = j.Key.CategoriaId,
                                            Descripcion = j.FirstOrDefault().Categoria
                                        }).ToList();



                    listadoEmbalaje = (from item in listadoPalletPendientesByClienteId
                                        group item by new { item.EmbalajeId } into j
                                        select new Grupo
                                        {
                                            Valor = j.Key.EmbalajeId,
                                            Descripcion = j.FirstOrDefault().Embalaje
                                        }).ToList();

                }

                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;

            }
        }


        #region Métodos()

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "EAURAZO";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "EAURAZO";

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Error en el sistema");
                return;
            }
        }

        #endregion

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            PresentarConsultaBusquedaAsincrona();

        }


        private void PresentarConsultaBusquedaAsincrona()
        {
            try
            {
                #region PresentarResultados()
                if (resultListDetail != null)
                {
                    txtFecha.Text = resultListDetail.ElementAt(0).ConformacionCargaFecha != null ? resultListDetail.ElementAt(0).ConformacionCargaFecha.ToShortDateString() : string.Empty;
                    txtEmpresaCodigoE.Text = resultListDetail.ElementAt(0).EmpresaID != null ? resultListDetail.ElementAt(0).EmpresaID.Trim() : string.Empty;
                    txtCampañaCodigo.Text = resultListDetail.ElementAt(0).CampaniaId != null ? resultListDetail.ElementAt(0).CampaniaId.Trim() : string.Empty;
                    txtProveedorCodigo.Text = resultListDetail.ElementAt(0).ClienteId != null ? resultListDetail.ElementAt(0).ClienteId.Trim() : string.Empty;
                    txtContenedor.Text = resultListDetail.ElementAt(0).NumeroContenedor != null ? resultListDetail.ElementAt(0).NumeroContenedor.Trim() : string.Empty;
                    txtBooking.Text = resultListDetail.ElementAt(0).Booking != null ? resultListDetail.ElementAt(0).Booking.Trim() : string.Empty;
                    txtIdEstado.Text = resultListDetail.ElementAt(0).EstadoId != null ? resultListDetail.ElementAt(0).EstadoId.Trim() : string.Empty;
                    txtDescripcion.Text = resultListDetail.ElementAt(0).ConformacionCarga != null ? resultListDetail.ElementAt(0).ConformacionCarga.Trim() : string.Empty;
                    txtObservaciones.Text = resultListDetail.ElementAt(0).Observacion != null ? resultListDetail.ElementAt(0).Observacion.Trim() : string.Empty;
                    txtCodigo.Text = resultListDetail.ElementAt(0).ConformacionCargaId != null ? resultListDetail.ElementAt(0).ConformacionCargaId.ToString().Trim() : "0";
                    txtEmpresaDescripcionE.Text = resultListDetail.ElementAt(0).Empresa != null ? resultListDetail.ElementAt(0).Empresa.Trim() : string.Empty;
                    txtCampañaDescripcion.Text = resultListDetail.ElementAt(0).Campania != null ? resultListDetail.ElementAt(0).Campania.Trim() : string.Empty;
                    txtProveedorDescripcion.Text = resultListDetail.ElementAt(0).Cliente != null ? resultListDetail.ElementAt(0).Cliente.Trim() : string.Empty;
                    txtEstado.Text = resultListDetail.ElementAt(0).EstadoConformidadCarga != null ? resultListDetail.ElementAt(0).EstadoConformidadCarga.Trim() : string.Empty;
                }

                //listadoCalibres = new List<Grupo>();
                if (listadoPalletPendientesByClienteId != null && listadoPalletPendientesByClienteId.ToList().Count > 0)
                {
                    foreach (var item in listadoTipopallet)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboTipoPallet.Items.Add(oTipoPallet);
                    }


                    //listadoCalibres = new List<Grupo>();
                    foreach (var item in listadoCalibres)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboCalibre.Items.Add(oTipoPallet);
                    }

                    //listadoVariedad = new List<Grupo>();
                    foreach (var item in listadoVariedad)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboVariedad.Items.Add(oTipoPallet);
                    }



                    //listadoColor = new List<Grupo>();                    
                    foreach (var item in listadoColor)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboColor.Items.Add(oTipoPallet);
                    }


                    //listadoEnvase = new List<Grupo>();                    
                    foreach (var item in listadoEnvase)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboEnvase.Items.Add(oTipoPallet);
                    }

                    //listadoPatihuela = new List<Grupo>();
                    foreach (var item in listadoPatihuela)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboParihuela.Items.Add(oTipoPallet);
                    }

                    //listadoCategoria = new List<Grupo>();
                    foreach (var item in listadoCategoria)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboCategoria.Items.Add(oTipoPallet);
                    }

                    //listadoEmbalaje = new List<Grupo>();
                    foreach (var item in listadoEmbalaje)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion;
                        oTipoPallet.Value = item.Valor;
                        cboEmbalaje.Items.Add(oTipoPallet);
                    }
                }                

               
                #region Llenar Grilla de paletas pendientes()                
                dgvResultados.DataSource = listadoPalletPendientesByClienteId.ToDataTable<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
                dgvResultados.Refresh();
                #endregion

                #region Limpiar Variables()                                
                //resultListDetail = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
                //model = new SAS_CondormidadDeCargaController();
                //itemRegistar = new SAS_ConformacionDeCarga();                
                //detalleRegistro = new List<SAS_ConformacionDeCargaDetalle>();                
                #endregion



                gbCabecera.Enabled = !false;
                gbDetalle.Enabled = !false;
                gbDetalle.Enabled = !false;
                pgbar.Visible = !true;
                #endregion
            }
            catch (Exception Ex)
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, Ex.Message.ToString(), "Error en el proceso", MessageBoxButtons.OK, RadMessageIcon.Error);
                return;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {


            //listadoTipopallet = new List<Grupo>();
            listadoTipoPalletElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoTipoPalletElegido.Add(itemChecked.Value.ToString());
            }


            //listadoCalibres = new List<Grupo>();
            listadoCalibresElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoCalibresElegido.Add(itemChecked.Value.ToString());
            }



            //listadoVariedad = new List<Grupo>();
            listadoVariedadElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoVariedadElegido.Add(itemChecked.Value.ToString());
            }


            //listadoColor = new List<Grupo>();
            listadoColorElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoColorElegido.Add(itemChecked.Value.ToString());
            }



            //listadoEnvase = new List<Grupo>();
            listadoEnvaseElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoEnvaseElegido.Add(itemChecked.Value.ToString());
            }

            //listadoPatihuela = new List<Grupo>();
            listadoParihuelaElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoParihuelaElegido.Add(itemChecked.Value.ToString());
            }


            //listadoCategoria = new List<Grupo>();
            listadoCategoriaElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoCategoriaElegido.Add(itemChecked.Value.ToString());
            }

            //listadoEmbalaje = new List<Grupo>();
            listadoEmbalajeElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoEmbalajeElegido.Add(itemChecked.Value.ToString());
            }






        }


    }
}
