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
        private int cantidadPalletRegistrados = 0;

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

            btnRegistrarPallet.Enabled = false;

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
                cantidadPalletRegistrados = resultListDetail.Where(x => x.IdRegistroPaleta != null).ToList().Count;

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
                    txtCantidadPalletRegistrados.Text = cantidadPalletRegistrados.ToString();
                }

                //listadoCalibres = new List<Grupo>();
                if (listadoPalletPendientesByClienteId != null && listadoPalletPendientesByClienteId.ToList().Count > 0)
                {
                    foreach (var item in listadoTipopallet)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboTipoPallet.Items.Add(oTipoPallet);
                    }


                    //listadoCalibres = new List<Grupo>();
                    foreach (var item in listadoCalibres)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboCalibre.Items.Add(oTipoPallet);
                    }

                    //listadoVariedad = new List<Grupo>();
                    foreach (var item in listadoVariedad)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboVariedad.Items.Add(oTipoPallet);
                    }



                    //listadoColor = new List<Grupo>();                    
                    foreach (var item in listadoColor)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboColor.Items.Add(oTipoPallet);
                    }


                    //listadoEnvase = new List<Grupo>();                    
                    foreach (var item in listadoEnvase)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboEnvase.Items.Add(oTipoPallet);
                    }

                    //listadoPatihuela = new List<Grupo>();
                    foreach (var item in listadoPatihuela)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboParihuela.Items.Add(oTipoPallet);
                    }

                    //listadoCategoria = new List<Grupo>();
                    foreach (var item in listadoCategoria)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
                        cboCategoria.Items.Add(oTipoPallet);
                    }

                    //listadoEmbalaje = new List<Grupo>();
                    foreach (var item in listadoEmbalaje)
                    {
                        RadCheckedListDataItem oTipoPallet = new Telerik.WinControls.UI.RadCheckedListDataItem();
                        oTipoPallet.Checked = true;
                        oTipoPallet.Text = item.Descripcion.Trim();
                        oTipoPallet.Value = item.Valor.Trim();
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

            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro01 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro02 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro03 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro04 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro05 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro06 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro07 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult> listadoPalletPendientesByClienteIdFiltro08 = new List<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();


            #region TipoPalletElegido
            //listadoTipopallet = new List<Grupo>();
            listadoTipoPalletElegido = new List<string>();
            foreach (var itemChecked in cboTipoPallet.CheckedItems)
            {
                listadoTipoPalletElegido.Add(itemChecked.Value.ToString());
            }

            listadoPalletPendientesByClienteIdFiltro01 = (from items in listadoPalletPendientesByClienteId.ToList()
                                                          where (listadoTipoPalletElegido.Contains(items.TipoDePaletaId.ToString().ToUpper().Trim()))
                                                          select items).ToList();

            //if (listadoTipoPalletElegido != null && listadoTipoPalletElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoTipoPalletElegido.Contains(items.TipoDePaletaId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}
            #endregion


            #region CalibresElegido
            //listadoCalibres = new List<Grupo>();
            listadoCalibresElegido = new List<string>();
            foreach (var itemChecked in cboCalibre.CheckedItems)
            {
                listadoCalibresElegido.Add(itemChecked.Value.ToString());
            }

            listadoPalletPendientesByClienteIdFiltro02 = (from items in listadoPalletPendientesByClienteIdFiltro01.ToList()
                                                          where (listadoCalibresElegido.Contains(items.CalibreId.ToString().ToUpper().Trim()))
                                                          select items).ToList();

            //if (listadoCalibresElegido != null && listadoCalibresElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoCalibresElegido.Contains(items.CalibreId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}
            #endregion


            #region VariedadElegido
            //listadoVariedad = new List<Grupo>();
            listadoVariedadElegido = new List<string>();
            foreach (var itemChecked in cboVariedad.CheckedItems)
            {
                listadoVariedadElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro03 = (from items in listadoPalletPendientesByClienteIdFiltro02.ToList()
                                                          where (listadoVariedadElegido.Contains(items.VariedadId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoVariedadElegido != null && listadoVariedadElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoVariedadElegido.Contains(items.VariedadId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}
            #endregion


            #region ColorElegido
            //listadoColor = new List<Grupo>();
            listadoColorElegido = new List<string>();
            foreach (var itemChecked in cboColor.CheckedItems)
            {
                listadoColorElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro04 = (from items in listadoPalletPendientesByClienteIdFiltro03.ToList()
                                                          where (listadoColorElegido.Contains(items.ColorId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoColorElegido != null && listadoColorElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoColorElegido.Contains(items.ColorId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}

            #endregion


            #region EnvaseElegido
            //listadoEnvase = new List<Grupo>();
            listadoEnvaseElegido = new List<string>();
            foreach (var itemChecked in cboEnvase.CheckedItems)
            {
                listadoEnvaseElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro05 = (from items in listadoPalletPendientesByClienteIdFiltro04.ToList()
                                                          where (listadoEnvaseElegido.Contains(items.EnvaseId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoEnvaseElegido != null && listadoEnvaseElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoEnvaseElegido.Contains(items.EnvaseId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}

            #endregion

            #region ParihuelaElegido
            //listadoPatihuela = new List<Grupo>();
            listadoParihuelaElegido = new List<string>();
            foreach (var itemChecked in cboParihuela.CheckedItems)
            {
                listadoParihuelaElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro06 = (from items in listadoPalletPendientesByClienteIdFiltro05.ToList()
                                                          where (listadoParihuelaElegido.Contains(items.PahiruelaId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoParihuelaElegido != null && listadoParihuelaElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoParihuelaElegido.Contains(items.PahiruelaId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}

            #endregion

            #region CategoriaElegido
            //listadoCategoria = new List<Grupo>();
            listadoCategoriaElegido = new List<string>();
            foreach (var itemChecked in cboCategoria.CheckedItems)
            {
                listadoCategoriaElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro07 = (from items in listadoPalletPendientesByClienteIdFiltro06.ToList()
                                                          where (listadoCategoriaElegido.Contains(items.CategoriaId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoCategoriaElegido != null && listadoCategoriaElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoCategoriaElegido.Contains(items.CategoriaId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}

            #endregion

            #region EmbalajeElegido
            //listadoEmbalaje = new List<Grupo>();
            listadoEmbalajeElegido = new List<string>();
            foreach (var itemChecked in cboEmbalaje.CheckedItems)
            {
                listadoEmbalajeElegido.Add(itemChecked.Value.ToString());
            }
            listadoPalletPendientesByClienteIdFiltro08 = (from items in listadoPalletPendientesByClienteIdFiltro07.ToList()
                                                          where (listadoEmbalajeElegido.Contains(items.EmbalajeId.ToString().ToUpper().Trim()))
                                                          select items).ToList();
            //if (listadoEmbalajeElegido != null && listadoEmbalajeElegido.ToList().Count > 0)
            //{
            //    listadoPalletPendientesByClienteIdFiltro = (from items in listadoPalletPendientesByClienteIdFiltro.ToList()
            //                                                where (listadoEmbalajeElegido.Contains(items.EmbalajeId.ToString().ToUpper().Trim()))
            //                                                select items).ToList();
            //}

            #endregion

            #region Llenar Grilla de paletas pendientes()                
            dgvResultados.DataSource = listadoPalletPendientesByClienteIdFiltro08.ToDataTable<SAS_ListadoConformacionDeCargaDisponiblesByIdClienteResult>();
            dgvResultados.Refresh();
            #endregion




        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvResultados.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvResultados.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvResultados.MasterTemplate.AutoExpandGroups = true;
            this.dgvResultados.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.GroupDescriptors.Clear();
            this.dgvResultados.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chNumeroPaleta", "# Reg. : {0:N2}; ", GridAggregateFunction.Count));
            items1.Add(new GridViewSummaryItem("chpesoReferencial", "Sum. : {0:N2}; ", GridAggregateFunction.Sum));
            items1.Add(new GridViewSummaryItem("chCantidadCajas", "Sum : {0:N2}; ", GridAggregateFunction.Sum));
            this.dgvResultados.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void ConformacionDeCargaAgregarPallets_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarPallet_Click(object sender, EventArgs e)
        {
            EjecutarSumatoriasDeSeleccion();
            if (this.txtCajasSelecionadas.Text != string.Empty)
            {
                decimal CantidadPalletsRegistrados = 0;
                decimal CantidadPalletSelecionados = 0;

                CantidadPalletsRegistrados = Convert.ToDecimal(txtCantidadPalletRegistrados.Text.Trim() != string.Empty ? txtCantidadPalletRegistrados.Text : "0");
                CantidadPalletSelecionados = Convert.ToDecimal(txtPalletSeleccionados.Text.Trim() != string.Empty ? txtPalletSeleccionados.Text : "0");

                if ((CantidadPalletsRegistrados + CantidadPalletSelecionados) != 20)
                {
                    DialogResult dialogResult = MessageBox.Show("Sí desea continuar con el registro presione SI", "Se han totalizado una cantidad que no es igual a 20 Palletas", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (dialogResult == DialogResult.Yes)
                    {
                        AgregarListaDePalletSeleccionados();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        btnRegistrarPallet.DialogResult = DialogResult.None;

                        return;

                    }
                }

            }
            else
            {
                RadMessageBox.SetThemeName(dgvResultados.ThemeName);
                RadMessageBox.Show(this, "Debe totalizar los pallets seleccionados", "Advertencia del sistema", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                EjecutarSumatoriasDeSeleccion();
                return;

            }

        }

        private void AgregarListaDePalletSeleccionados()
        {
            try
            {
                gbCabecera.Enabled = false;
                gbDetalle.Enabled = false;
                gbFiltro.Enabled = false;
                pgbar.Visible = true;

                #region Registrar lista de palletas
                List<SAS_ConformacionDeCargaDetalle> listaAAgregar = new List<SAS_ConformacionDeCargaDetalle>();
                SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                oItem.Id = Convert.ToInt32(this.txtCodigo.Text.Trim());
                foreach (GridViewRowInfo rows in dgvResultados.Rows)
                {
                    if (rows.Cells["chSeleccionado"].Value.ToString() == "1")
                    {
                        SAS_ConformacionDeCargaDetalle item = new SAS_ConformacionDeCargaDetalle();
                        item.IdConformacionCarga = Convert.ToInt32(this.txtCodigo.Text.Trim());
                        item.Id = 0;
                        item.IdRegistroPaleta = rows.Cells["chIdRegistroPaleta"].Value.ToString();
                        item.Estado = Convert.ToByte(1);
                        listaAAgregar.Add(item);
                    }
                }

                if (listaAAgregar != null && listaAAgregar.ToList().Count > 0)
                {
                    model = new SAS_CondormidadDeCargaController();
                    int resultadoAccion = model.ToRegisterListDetail(connection, oItem, listaAAgregar);
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

        private void dgvResultados_SelectionChanged(object sender, EventArgs e)
        {
            btnRegistrarPallet.Enabled = false;
        }

        private void dgvResultados_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void StyleCell(GridViewCellInfo cell)
        {
            cell.Style.CustomizeFill = true;
            cell.Style.GradientStyle = GradientStyles.Solid;
            cell.Style.BackColor = Color.Red;
        }

        private void dgvResultados_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
        }

        private void dgvResultados_CellEndEdit(object sender, GridViewCellEventArgs e)
        {

        }

        private void dgvResultados_CellValueChanged(object sender, GridViewCellEventArgs e)
        {

        }

        private void EjecutarSumatoriasDeSeleccion()
        {
            decimal sumCajas = 0;
            decimal sumKg = 0;
            decimal CountPallet = 0;

            //for (int i = 0; i < dgvResultados.Rows.Count - 1; ++i)
            //{
            //    if (dgvResultados.Rows[i].Cells["chselecionado"].Value.ToString() == "1")
            //    {
            //        sumCajas += int.Parse(dgvResultados.Rows[i].Cells["chNumeroDeCajas"].Value.ToString());
            //        sumKg += int.Parse(dgvResultados.Rows[i].Cells["chpesoReferencial"].Value.ToString());
            //        sumCajas += 1;
            //    }

            //}


            foreach (GridViewRowInfo rows in dgvResultados.Rows)
            {
                if (rows.Cells["chSeleccionado"].Value != null)
                {
                    if (rows.Cells["chSeleccionado"].Value.ToString() != string.Empty)
                    {
                        if (rows.Cells["chSeleccionado"].Value.ToString() == "1")
                        {
                            sumCajas += Convert.ToDecimal(rows.Cells["chCantidadCajas"].Value.ToString().Trim());
                            sumKg += Convert.ToDecimal(rows.Cells["chpesoReferencial"].Value.ToString().Trim());
                            CountPallet += 1;
                        }
                    }

                }

            }

            this.txtCajasSelecionadas.Text = sumCajas.ToString();
            this.txtKgSeleccionados.Text = sumKg.ToString("N2");
            this.txtPalletSeleccionados.Text = CountPallet.ToString();
        }

        private void dgvResultados_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {

        }

        private void dgvResultados_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {

        }

        private void dgvResultados_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvResultados_CellMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void dgvResultados_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {

        }

        private void dgvResultados_CurrentViewChanged(object sender, GridViewCurrentViewChangedEventArgs e)
        {

        }

        private void dgvResultados_CellFormatting(object sender, CellFormattingEventArgs e)
        {

        }

        private void dgvResultados_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {

        }

        private void dgvResultados_Leave(object sender, EventArgs e)
        {

        }

        private void dgvResultados_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvResultados_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnTotalizar_Click(object sender, EventArgs e)
        {
            EjecutarSumatoriasDeSeleccion();
            btnRegistrarPallet.Enabled = true;
        }
    }
}
