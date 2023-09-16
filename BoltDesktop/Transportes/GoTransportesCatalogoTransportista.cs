using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using MyControlsDataBinding.Extensions;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using System.Configuration;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using System.IO;
using Telerik.WinControls.UI.Localization;
using Asistencia.Negocios;
using Asistencia.Datos;

namespace Asistencia
{
    public partial class CatalogoEmpresaDeServicioDeTransporteDePersonal : RadForm
    {
        #region Declaración de Variables()        
        private TipoMovilidadController typeOfMobilityController; // tipo de movilidad
        private List<TipoMovilidad> typesOfMobility;
        private List<SJ_RHTransportistaChofer> driversRemoved = new List<SJ_RHTransportistaChofer>();
        private List<SJ_RHTransportistaContrato> contractRemoved = new List<SJ_RHTransportistaContrato>();
        private List<SJ_RHTransportistaContrato> documentRemoved = new List<SJ_RHTransportistaContrato>();
        private List<SJ_RHTransportistaRuta> routeRemoved = new List<SJ_RHTransportistaRuta>();
        private List<SJ_RHTransportistaChofer> drivers = new List<SJ_RHTransportistaChofer>();
        private List<SJ_RHTransportistaContrato> contracts = new List<SJ_RHTransportistaContrato>();
        private List<SJ_RHTransportistaRuta> routes = new List<SJ_RHTransportistaRuta>();
        private List<SJ_RHTransportistaContrato> documents = new List<SJ_RHTransportistaContrato>();
        private CarrierController Modelo;
        private List<SJ_RHListarDetalleTransportistaContratoResult> contractListByPerQuery;
        private List<SJ_RHListarDetalleTransportistaContratoResult> listOfDocumentsPerQuery;
        private List<SJ_RHListarDetalleTransportistaChoferResult> driversListPerQuery;
        private List<SJ_RHListarDetalleTransportistaRutaResult> routeListPerQuery;
        private string message;
        private SJ_RHTransportista oTransportista;
        private int posicionX;
        private int posicionY;
        private string CodigoTransportista;
        private string fileName;
        private bool exportVisualSettings;
        private string statusCode;
        private List<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult> ListadoTransportista;
        private object item;
        private SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult transportUnit;
        private bool duplicado;
        private string tradename;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private PrivilegesByUser _privilege;
        #endregion

        public CatalogoEmpresaDeServicioDeTransporteDePersonal()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();

            ObtenerListacboTipoMovilidad();
            ObtenerListaTransportistas();
            gbEdit.Enabled = false;
            gbTransporte.Enabled = false;
            gbList.Enabled = false;
            bgwHilo.RunWorkerAsync();

        }

        public CatalogoEmpresaDeServicioDeTransporteDePersonal(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();

            ObtenerListacboTipoMovilidad();
            ObtenerListaTransportistas();
            gbEdit.Enabled = false;
            gbTransporte.Enabled = false;
            gbList.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        public CatalogoEmpresaDeServicioDeTransporteDePersonal(string conection, ASJ_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Inicio();

            ObtenerListacboTipoMovilidad();
            ObtenerListaTransportistas();
            gbEdit.Enabled = false;
            gbTransporte.Enabled = false;
            gbList.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        private void Transportista_Load(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {

            this.dgvTransportista.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvTransportista.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvTransportista.MasterTemplate.AutoExpandGroups = true;
            this.dgvTransportista.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvTransportista.GroupDescriptors.Clear();
            this.dgvTransportista.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chPseudoNombre", "N° Registros: {0:N0}; ", GridAggregateFunction.Count));
            //this.dgvListaAgrupada.MasterTemplate.SummaryRowsBottom.Add(items1);
            this.dgvTransportista.MasterTemplate.SummaryRowsTop.Add(items1);


        }

        private void ObtenerListacboTipoMovilidad()
        {
            typeOfMobilityController = new TipoMovilidadController();
            typesOfMobility = new List<TipoMovilidad>();

            typesOfMobility = typeOfMobilityController.ListarTiposdeMovilidad();

            if (typesOfMobility.Count > 0)
            {
                this.cboTipoMovilidad.DataSource = typesOfMobility;
                cboTipoMovilidad.DisplayMember = "TipoMovil";
                cboTipoMovilidad.ValueMember = "IdTipoMovilidad";
                cboTipoMovilidad.SelectedValue = "000";
            }
        }

        private void ObtenerListaTransportistas()
        {
            CarrierController transportistaNeg = new CarrierController();
            ListadoTransportista = new List<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult>();
            ListadoTransportista = transportistaNeg.ListarCatalogoEmpresasTransportePersonalCampo(_conection);
        }

        private void ShowResult()
        {
            this.dgvTransportista.DataSource = ListadoTransportista.ToDataTable<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult>();
            this.dgvTransportista.Refresh();
            this.dgvTransportista.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            if (this.dgvTransportista.GroupDescriptors.Count == 0)
            {
                this.dgvTransportista.GroupDescriptors.Add("chPseudoNombre", System.ComponentModel.ListSortDirection.Ascending);
            }

            gbEdit.Enabled = false;
            gbTransporte.Enabled = false;
            gbList.Enabled = true;
            ProgressBar.Visible = false;

            btnNuevo.Enabled = !false;
            btnEditar.Enabled = !false;
            btnGrabar.Enabled = !true;
            btnCancelar.Enabled = !true;
            btnAtras.Enabled = !true;
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
            btnAnular.Enabled = !false;
            btnEliminar.Enabled = !false;


        }

        public void Inicio()
        {
            try
            {

                MyControlsDataBinding.Extensions.Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                MyControlsDataBinding.Extensions.Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                MyControlsDataBinding.Extensions.Globales.BaseDatos = ConfigurationManager.AppSettings[_conection].ToString();
                MyControlsDataBinding.Extensions.Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                MyControlsDataBinding.Extensions.Globales.IdEmpresa = "001";
                MyControlsDataBinding.Extensions.Globales.Empresa = "EMPRESA AGRICOLA SAN JOSE SA";
                MyControlsDataBinding.Extensions.Globales.UsuarioSistema = "CHRISTIAN";
                MyControlsDataBinding.Extensions.Globales.NombreUsuarioSistema = "Christian LLontop";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            try
            {
                #region Edición()
                if (_privilege != null)
                {
                    if (_privilege.editar == 1)
                    {
                        #region
                        if (statusCode != string.Empty && statusCode != "AN" && dgvTransportista.RowCount >= 0)
                        {
                            //posicionX = this.dgvTransportista.CurrentRow.Index;
                            //posicionY = this.dgvTransportista.CurrentColumn.Index;
                            ActivarDesactivarControlEdicion(true);
                            ActivarEdicionGrillaContrato(true);
                            ActivarEdicionGrillaChofer(true);
                            ActivarEdicionGrillaRuta(true);
                            ActivarEdicionGrillaRuta(true);

                            btnNuevo.Enabled = false;
                            btnEditar.Enabled = false;
                            btnGrabar.Enabled = true;
                            btnCancelar.Enabled = true;
                            btnAtras.Enabled = true;
                            gbEdit.Enabled = true;
                            gbList.Enabled = false;
                            btnAnular.Enabled = false;
                            btnEliminar.Enabled = false;

                        }
                        else
                        {
                            MessageBox.Show("No tiene el estado para la edición", "ADVERTENCIA DEL SISTEMA");
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void ActivarDesactivarControlEdicion(bool Estado)
        {
            this.gbEdit.Enabled = Estado;
            this.gbList.Enabled = !(Estado);
            gbTransporte.Enabled = Estado;


            if (this.txtCodigo.Text.ToString().Trim() == "")
            {
                //this.txtNumeroPlaca.ReadOnly = false;
            }
            else
            {
                //this.txtNumeroPlaca.ReadOnly = true;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Atras();
        }

        private void Atras()
        {
            #region Atras()

            ActivarDesactivarControlEdicion(false);
            ActivarEdicionGrillaContrato(false);
            ActivarEdicionGrillaChofer(false);

            btnNuevo.Enabled = !false;
            btnEditar.Enabled = !false;
            btnGrabar.Enabled = !true;
            btnCancelar.Enabled = !true;
            btnAtras.Enabled = !true;
            gbEdit.Enabled = !true;
            gbList.Enabled = !false;
            btnAnular.Enabled = !false;
            btnEliminar.Enabled = !false;

            #endregion
        }

        private void GetContractListByCarrieId(string carrierCode)
        {
            try
            {
                // Cargar registos en la Grilla, en caso tenga registros
                Modelo =new CarrierController();
                contractListByPerQuery = new List<SJ_RHListarDetalleTransportistaContratoResult>();
                contractListByPerQuery = Modelo.ListContractByCarrierId(Convert.ToInt32(carrierCode), _conection).ToList();
                dgvContratos.CargarDatos(contractListByPerQuery.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
                dgvContratos.Refresh();
                dgvContratos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvContratos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }


        }

        // get driver list| obtener listado de conductores
        private void GetDriverListByCarrieId(string carrierCode)
        {
            try
            {
                // Cargar registos en la Grilla, en caso tenga registros
                Modelo =new CarrierController();
                driversListPerQuery = new List<SJ_RHListarDetalleTransportistaChoferResult>();

                driversListPerQuery = Modelo.ListarDetalleChofer(Convert.ToInt32(carrierCode), _conection).ToList();
                dgvChofer.CargarDatos(driversListPerQuery.ToDataTable<SJ_RHListarDetalleTransportistaChoferResult>());
                dgvChofer.Refresh();
                dgvChofer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvChofer.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        // get list of routes | obtener listado de rutas
        private void GetListOfRoutesByCarrieId(string carrierCode)
        {
            try
            {
                // Cargar registos en la Grilla, en caso tenga registros
                Modelo =new CarrierController();
                routeListPerQuery = new List<SJ_RHListarDetalleTransportistaRutaResult>();

                routeListPerQuery = Modelo.ListRouterByCarrierId(Convert.ToInt32(carrierCode), _conection).ToList();
                this.dgvRuta.CargarDatos(routeListPerQuery.ToDataTable<SJ_RHListarDetalleTransportistaRutaResult>());
                this.dgvRuta.Refresh();
                dgvRuta.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvRuta.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        // get list of documents | obtener listado de documentos
        private void GetListOfDocumentsByCarrieId(string carrierCode)
        {
            try
            {
                // Cargar registos en la Grilla, en caso tenga registros
                Modelo =new CarrierController();
                listOfDocumentsPerQuery = new List<SJ_RHListarDetalleTransportistaContratoResult>();
                listOfDocumentsPerQuery = Modelo.ListDocumentsByCarrierId(Convert.ToInt32(carrierCode), _conection).ToList();
                dgvDocumentacion.CargarDatos(listOfDocumentsPerQuery.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
                dgvDocumentacion.Refresh();
                dgvDocumentacion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvDocumentacion.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {

            RefreshList();
        }

        private void RefreshList()
        {
            #region Actualizar Lista() 

            //if ((this.dgvTransportista.CurrentRow != null))
            //{
            //    if (this.dgvTransportista.CurrentRow.Index > 0 && this.dgvTransportista.CurrentColumn.Index > 0)
            //    {
            //posicionX = this.dgvTransportista.CurrentRow.Index;
            //posicionY = this.dgvTransportista.CurrentColumn.Index;
            //ObtenerListaTransportistas();            

            if (_privilege != null)
            {
                if (_privilege.consultar == 1)
                {
                    gbEdit.Enabled = false;
                    gbTransporte.Enabled = false;
                    gbList.Enabled = false;
                    ProgressBar.Visible = !false;
                    bgwHilo.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
            //        if (posicionX > 0)
            //        {
            //            dgvTransportista.CurrentRow = dgvTransportista.Rows[posicionX];
            ////        }
            //    }
            //}


            #endregion
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            #region Nuevo() 
            if (_privilege != null)
            {
                if (_privilege.nuevo == 1)
                {
                    LimpiarFormulario();
                    ActivarDesactivarControlEdicion(true);
                    ActivarEdicionGrillaChofer(true);
                    ActivarEdicionGrillaContrato(true);
                    ActivarEdicionGrillaRuta(true);
                    btnCancelar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "Advertencia del sistema");
                    return;
                }

            }
            #endregion
        }

        private void LimpiarFormulario()
        {
            //Limpiar de manera rapida
            this.txtCodigo.Clear();
            this.txtEstado.Clear();
            this.txtIdEstado.Clear();
            this.txtEstado.Text = "Activo";
            this.txtIdEstado.Text = "AC";
            this.txtNombreComercial.Clear();
            this.txtNumeroAsientos.Clear();
            this.txtNumeroPlaca.Clear();
            this.txtPesoMaximo.Clear();
            this.txtRucDescripcion.Clear();
            this.txtRUCNumero.Clear();
            this.cboTipoMovilidad.SelectedValue = "000";
            this.txtRUCNumero.Focus();
            this.txtAnioFabricación.Clear();
            this.txtMarca.Clear();
            this.txtModelo.Clear();
            this.chkTransporteInterlocal.Checked = false;
            this.chkTransporteInterno.Checked = false;

            //Limpiar Grilla Chofer
            List<SJ_RHListarDetalleTransportistaChoferResult> ListaVaciaGrillaChofer = new List<SJ_RHListarDetalleTransportistaChoferResult>();
            dgvChofer.CargarDatos(ListaVaciaGrillaChofer.ToDataTable<SJ_RHListarDetalleTransportistaChoferResult>());
            dgvChofer.Refresh();

            //Limpiar Grilla Contratos
            List<SJ_RHListarDetalleTransportistaContratoResult> ListaVaciaGrillaContratos = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            this.dgvContratos.CargarDatos(ListaVaciaGrillaContratos.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
            this.dgvContratos.Refresh();

            //Limpiar Grilla Ruta
            List<SJ_RHListarDetalleTransportistaRutaResult> ListaVaciaGrillaRuta = new List<SJ_RHListarDetalleTransportistaRutaResult>();
            this.dgvRuta.CargarDatos(ListaVaciaGrillaContratos.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
            this.dgvRuta.Refresh();

            List<SJ_RHListarDetalleTransportistaContratoResult> listEmptyDocument = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            this.dgvDocumentacion.CargarDatos(listEmptyDocument.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
            this.dgvDocumentacion.Refresh();

            driversRemoved = new List<SJ_RHTransportistaChofer>();
            contractRemoved = new List<SJ_RHTransportistaContrato>();
            documentRemoved = new List<SJ_RHTransportistaContrato>();
            routeRemoved = new List<SJ_RHTransportistaRuta>();

            drivers = new List<SJ_RHTransportistaChofer>();
            contracts = new List<SJ_RHTransportistaContrato>();
            routes = new List<SJ_RHTransportistaRuta>();
            documents = new List<SJ_RHTransportistaContrato>();

            contractListByPerQuery = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            listOfDocumentsPerQuery = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            driversListPerQuery = new List<SJ_RHListarDetalleTransportistaChoferResult>();
            routeListPerQuery = new List<SJ_RHListarDetalleTransportistaRutaResult>();


            //Mostrar por defecto el tabChofer
            TabRegistros.SelectedPage = tabChofer;


        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            #region Grabar()

            if (ValidarDatosCabecera() == true)
            {
                try
                {
                    ValidarPlaca(txtNumeroPlaca.Text);
                    if (GetCarrierObject("TRANSPORTISTA") == true)
                    {
                        if (SaveCarrier("TRANSPORTISTA", oTransportista) == true)
                        {
                            MessageBox.Show("Operación realizada Satisfactoriamente", "Sistema");
                            ActivarEdicionGrillaContrato(false);
                            ActivarEdicionGrillaChofer(false);
                            ActivarEdicionGrillaRuta(false);
                            LimpiarFormulario();
                            RefreshList();
                        }
                        else
                        {
                            MessageBox.Show("Error en la operación \n| El formulario contiene datos no validos", "Advetencia del sistema");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error en la operación \n| El formulario contiene datos no validos", "Advetencia del sistema");
                    }






                }
                catch (Exception Ex)
                {
                    RadMessageBox.Show(Ex.ToString(), "mensaje del sistema");
                    return;
                }
            }
            else
            {
                RadMessageBox.Show(message);
                return;
            }


            #endregion
        }

        private bool SaveCarrier(string nameObject, SJ_RHTransportista oTransportista)
        {
            bool status = false;
            try
            {


                if (dgvChofer.RowCount > 0)
                {
                    GetCarrierObject("DRIVER"); // CHOFER                   
                }

                if (dgvContratos.RowCount > 0)
                {
                    GetCarrierObject("CONTRACT"); // CONTRATO
                }

                if (dgvDocumentacion.RowCount > 0)
                {
                    GetCarrierObject("DOCUMENT"); // DOCUMENTO
                }

                if (dgvRuta.RowCount > 0)
                {
                    GetCarrierObject("ROUTE"); // RUTA
                }


                Modelo =new CarrierController();
                status = Modelo.AddCarrier(_conection, oTransportista, drivers, contracts, driversRemoved, contractRemoved, routeRemoved, routes, documentRemoved, documents);

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + "GRABAR TRANSPORTISTA", "MENSAJE DEL SISTEMA");
                return false;
            }

            return status;
        }

        private bool GetCarrierObject(string nameObject)
        {
            bool status = false;

            try
            {
                switch (nameObject)
                {
                    case "TRANSPORTISTA":
                        #region Obtener Objeto Transportista()
                        oTransportista = new SJ_RHTransportista();
                        oTransportista.Id = this.txtCodigo.Text.ToString() != "" ? Convert.ToInt32(this.txtCodigo.Text.ToString()) : 0;
                        oTransportista.RUC = this.txtRUCNumero.Text.ToString();
                        oTransportista.Placa = this.txtNumeroPlaca.Text.ToString();
                        oTransportista.NombreCorto = this.txtNombreComercial.Text.ToString();
                        oTransportista.TipoMovilidad = this.cboTipoMovilidad.SelectedValue.ToString().Trim();
                        oTransportista.IdEstado = this.txtIdEstado.Text.ToString();
                        oTransportista.NumeroAsientos = this.txtNumeroAsientos.Text.ToString() != "" ? Convert.ToInt32(this.txtNumeroAsientos.Text.ToString()) : 0;
                        oTransportista.PesoMaximo = this.txtPesoMaximo.Text.ToString() != "" ? Convert.ToDecimal(this.txtPesoMaximo.Text.ToString()) : 0;
                        oTransportista.AnioFabricacion = this.txtAnioFabricación.Text.ToString().Trim();
                        oTransportista.Marca = this.txtMarca.Text.ToString().Trim();
                        oTransportista.Modelo = this.txtModelo.Text.ToString().Trim();


                        if (this.chkTransporteInterno.Checked == true)
                        {
                            oTransportista.EsMovilidadLocal = 1;
                        }
                        else
                        {
                            oTransportista.EsMovilidadLocal = 0;
                        }

                        if (this.chkTransporteInterlocal.Checked == true)
                        {
                            oTransportista.EsInterLocal = 1;
                        }
                        else
                        {
                            oTransportista.EsInterLocal = 0;
                        }

                        status = true;

                        #endregion
                        break;

                    case "DRIVER":
                        drivers = new List<SJ_RHTransportistaChofer>();
                        #region Obtener Objeto Chofer()
                        if (this.dgvChofer != null)
                        {
                            if (this.dgvChofer.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow fila in this.dgvChofer.Rows)
                                {
                                    if (fila.Cells["chDNIChofer"].Value.ToString().Trim() != String.Empty)
                                    {
                                        try
                                        {
                                            SJ_RHTransportistaChofer oChofer = new SJ_RHTransportistaChofer();
                                            oChofer.IdTransportista = this.txtCodigo.Text.ToString().Trim() != "" ? Convert.ToInt32(txtCodigo.Text.ToString().Trim()) : 0;
                                            oChofer.Item = fila.Cells["chItemChofer"].Value != null ? fila.Cells["chItemChofer"].Value.ToString().Trim() : "";
                                            oChofer.DNI = fila.Cells["chDNIChofer"].Value != null ? fila.Cells["chDNIChofer"].Value.ToString().Trim() : "";
                                            oChofer.Nombres = fila.Cells["chNombresChofer"].Value != null ? fila.Cells["chNombresChofer"].Value.ToString().Trim() : "";
                                            oChofer.TipoLicencia = fila.Cells["chTipoLicenciaChofer"].Value != null ? fila.Cells["chTipoLicenciaChofer"].Value.ToString().Trim() : "";

                                            string ABC = fila.Cells["chEsCapacitadoChofer"].Value.ToString();
                                            if (fila.Cells["chEsCapacitadoChofer"].Value.ToString() != "")
                                            {
                                                if (fila.Cells["chEsCapacitadoChofer"].Value.ToString() == "0")
                                                {
                                                    oChofer.EsCapacitado = 0;
                                                }
                                                else
                                                {
                                                    if (fila.Cells["chEsCapacitadoChofer"].Value.ToString() == "false")
                                                    {
                                                        oChofer.EsCapacitado = 0;
                                                    }
                                                    else
                                                    {
                                                        oChofer.EsCapacitado = 1;
                                                    }

                                                }

                                            }
                                            else
                                            {
                                                oChofer.EsCapacitado = 0;
                                            }

                                            oChofer.Observacion = fila.Cells["chObservacionChofer"].Value != null ? fila.Cells["chObservacionChofer"].Value.ToString().Trim() : "";
                                            oChofer.IdEstado = fila.Cells["chIdEstadoChofer"].Value != null ? fila.Cells["chIdEstadoChofer"].Value.ToString().Trim() : "";
                                            drivers.Add(oChofer);

                                        }
                                        catch (Exception Ex)
                                        {

                                            MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                            return false;
                                        }

                                    }
                                }
                                status = true;
                            }
                        }
                        #endregion
                        break;

                    case "CONTRACT":
                        try
                        {
                            #region Obtener Objeto Contrato()
                            contracts = new List<SJ_RHTransportistaContrato>();
                            if (this.dgvContratos != null)
                            {
                                if (this.dgvContratos.Rows.Count > 0)
                                {
                                    foreach (DataGridViewRow fila in this.dgvContratos.Rows)
                                    {
                                        if (fila.Cells["chFechaInicioContrato"].Value != null && fila.Cells["chFechaTerminoContrato"].Value.ToString().Trim() != String.Empty)
                                        {
                                            SJ_RHTransportistaContrato oContrato = new SJ_RHTransportistaContrato();
                                            oContrato.Id = (fila.Cells["chIdContrato"].Value.ToString().Trim() != "" && fila.Cells["chIdContrato"].Value != null) ? Convert.ToInt32(fila.Cells["chIdContrato"].Value.ToString().Trim()) : 0;
                                            oContrato.Item = fila.Cells["chItemContrato"].Value != null ? fila.Cells["chItemContrato"].Value.ToString().Trim() : string.Empty;
                                            oContrato.FechaInicio = fila.Cells["chFechaInicioContrato"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaInicioContrato"].Value.ToString()) : (DateTime?)null;
                                            oContrato.FechaTermino = fila.Cells["chFechaTerminoContrato"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaTerminoContrato"].Value.ToString()) : (DateTime?)null;
                                            oContrato.Observacion = fila.Cells["chObservacionContrato"].Value != null ? fila.Cells["chObservacionContrato"].Value.ToString().Trim() : string.Empty;
                                            oContrato.IdEstado = fila.Cells["chIdEstadoContrato"].Value != null ? fila.Cells["chIdEstadoContrato"].Value.ToString().Trim() : "AC";

                                            contracts.Add(oContrato);
                                        }
                                    }
                                    status = true;
                                }
                            }
                            #endregion
                        }
                        catch (Exception Ex)
                        {

                            MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                            return false;
                        }

                        break;

                    case "ROUTE":
                        #region Obtener Objeto Ruta()
                        routes = new List<SJ_RHTransportistaRuta>();
                        if (this.dgvRuta != null)
                        {
                            if (this.dgvRuta.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow fila in this.dgvRuta.Rows)
                                {
                                    if (fila.Cells["chDescripcionRuta"].Value != null && fila.Cells["chDescripcionRuta"].Value.ToString().Trim() != String.Empty)
                                    {
                                        SJ_RHTransportistaRuta oRuta = new SJ_RHTransportistaRuta();
                                        oRuta.Id = (fila.Cells["chIdRuta"].Value != null && fila.Cells["chIdRuta"].Value.ToString().Trim() != "") ? Convert.ToInt32(fila.Cells["chIdRuta"].Value.ToString().Trim()) : 0;
                                        oRuta.Item = fila.Cells["chItemRuta"].Value != null ? fila.Cells["chItemRuta"].Value.ToString().Trim() : "";
                                        oRuta.IdRuta = fila.Cells["chCodRuta"].Value != null ? Convert.ToInt32(fila.Cells["chCodRuta"].Value.ToString()) : 0;
                                        oRuta.PrecioPersona = fila.Cells["chPrecioPersona"].Value != null ? Convert.ToDecimal(fila.Cells["chPrecioPersona"].Value.ToString()) : (decimal?)null;
                                        oRuta.PrecioFlete = fila.Cells["chPrecioFlete"].Value != null ? Convert.ToDecimal(fila.Cells["chPrecioFlete"].Value.ToString()) : (decimal?)null;
                                        oRuta.PrecioVuelta = fila.Cells["chPrecioVuelta"].Value != null ? Convert.ToDecimal(fila.Cells["chPrecioVuelta"].Value.ToString()) : (decimal?)null;
                                        oRuta.Observacion = fila.Cells["chObservacionRuta"].Value != null ? fila.Cells["chObservacionRuta"].Value.ToString().Trim() : "";
                                        oRuta.IdEstado = fila.Cells["chIdEstadoRuta"].Value != null ? fila.Cells["chIdEstadoRuta"].Value.ToString().Trim() : "";
                                        routes.Add(oRuta);
                                    }
                                }
                                status = true;
                            }
                        }
                        #endregion
                        break;

                    case "DOCUMENT":
                        #region Obtener Objeto Contrato()
                        documents = new List<SJ_RHTransportistaContrato>();
                        if (dgvDocumentacion != null)
                        {
                            if (dgvDocumentacion.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow fila in dgvDocumentacion.Rows)
                                {
                                    if (fila.Cells["chFechaInicioDD"].Value != null && fila.Cells["chFechaTerminoDD"].Value.ToString().Trim() != String.Empty)
                                    {
                                        if (Convert.ToDateTime(fila.Cells["chFechaInicioDD"].Value.ToString()) < Convert.ToDateTime(fila.Cells["chFechaTerminoDD"].Value.ToString().Trim()))
                                        {
                                            SJ_RHTransportistaContrato document = new SJ_RHTransportistaContrato();
                                            document.Id = (fila.Cells["chIIdDD"].Value.ToString().Trim() != "" && fila.Cells["chIIdDD"].Value != null) ? Convert.ToInt32(fila.Cells["chIIdDD"].Value.ToString().Trim()) : 0;
                                            document.Item = fila.Cells["chItemDD"].Value != null ? fila.Cells["chItemDD"].Value.ToString().Trim() : string.Empty;
                                            document.TypeDocumentId = (fila.Cells["chTypeDocumentId"].Value.ToString().Trim() != "" && fila.Cells["chTypeDocumentId"].Value != null) ? Convert.ToInt32(fila.Cells["chTypeDocumentId"].Value.ToString().Trim()) : 0;
                                            document.FechaInicio = fila.Cells["chFechaInicioDD"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaInicioDD"].Value.ToString()) : (DateTime?)null;
                                            document.FechaTermino = fila.Cells["chFechaTerminoDD"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaTerminoDD"].Value.ToString()) : (DateTime?)null;
                                            document.Observacion = fila.Cells["chObservacionDD"].Value != null ? fila.Cells["chObservacionDD"].Value.ToString().Trim() : string.Empty;
                                            document.IdEstado = fila.Cells["chEstadoIdDD"].Value != null ? fila.Cells["chEstadoIdDD"].Value.ToString().Trim() : "AC";
                                            documents.Add(document);
                                        }
                                        else
                                        {

                                            MessageBox.Show("La fecha de término no debe ser menor a la fecha de inicioa", "MENSAJE DEL SISTEMA");
                                            return false;
                                        }


                                    }
                                }
                                status = true;
                            }
                        }
                        #endregion
                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + "\nObtener Objeto Transportista y Detalle", "MENSAJE DEL SISTEMA");
                return false;

            }
            return status;
        }

        private bool ValidarDatosCabecera()
        {
            Boolean estado = true;

            message = "";


            if (txtEstado.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener un estado \n";
                estado = false;
            }

            if (txtNumeroAsientos.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener el numero de asientos \n";
                estado = false;
            }

            if (txtNumeroPlaca.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener el numero de placa \n";
                estado = false;
            }


            if (txtPesoMaximo.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener el peso máximo del movil \n";
                estado = false;
            }

            if (txtRucDescripcion.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener un Proveedor \n";
                estado = false;
            }

            if (txtRUCNumero.Text.ToString().Trim() == "")
            {
                message += "El documento debe tener un numero de RUC \n";
                estado = false;
            }


            if (cboTipoMovilidad.SelectedValue.ToString().Trim() == "000")
            {
                message += "Falta selecionar el tipo de Movilidad \n";
                estado = false;
            }
            return estado;
        }

        private void btnAgregarChofer_Click(object sender, EventArgs e)
        {
            //Solo se podra agregar un detalle si tiene la ingresada correctamente en la cabecera
            if (ValidarDatosCabecera() == true)
            {
                AgregarLineaChofer();
            }
            else
            {
                RadMessageBox.Show(message);
            }
        }

        private void AgregarLineaChofer()
        {
            try
            {
                if (this.dgvChofer != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(this.txtCodigo.Text.ToString().Trim()); // Codigo Cabecera                  
                    array.Add((AsignarNumeroItemsGrilla(ObtenerUltimoNumeroItem(dgvChofer)))); // item
                    array.Add(string.Empty); // DNI
                    array.Add(string.Empty); // Nombres
                    array.Add(string.Empty); // TipoLicencia
                    array.Add(false); // EsCapacitado
                    array.Add(string.Empty); // Observacion
                    array.Add("AC"); // Grupo
                    array.Add("Activo"); // Grupo                                        
                    this.dgvChofer.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                    return;
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
                return;
            }
        }

        private int ObtenerUltimoNumeroItem(DataGridView grilla)
        {
            List<int> numItem = new List<int>();
            if (grilla.Rows.Count > 0)
            {

                foreach (DataGridViewRow filas in grilla.Rows)
                {
                    /* agrego la columna 1, por que en ambos caso la filla items esta situada en la colunmna 1*/
                    numItem.Add((filas.Cells[1].Value) != null ? Convert.ToInt32(filas.Cells[1].Value) : 0);
                }
            }
            else
            {
                numItem.Add(0);
            }

            return numItem.Max();

        }

        private int ObtenerUltimoNumeroItemContrato(DataGridView grilla, DataGridView grilla2)
        {
            List<int> numItem = new List<int>();
            if (grilla.Rows.Count > 0)
            {

                foreach (DataGridViewRow filas in grilla.Rows)
                {
                    /* agrego la columna 1, por que en ambos caso la filla items esta situada en la colunmna 1*/
                    numItem.Add((filas.Cells[1].Value) != null ? Convert.ToInt32(filas.Cells[1].Value) : 0);
                }
            }
            else
            {
                numItem.Add(0);
            }

            if (grilla2.Rows.Count > 0)
            {

                foreach (DataGridViewRow filas in grilla2.Rows)
                {
                    /* agrego la columna 1, por que en ambos caso la filla items esta situada en la colunmna 1*/
                    numItem.Add((filas.Cells[1].Value) != null ? Convert.ToInt32(filas.Cells[1].Value) : 0);
                }
            }
            else
            {
                numItem.Add(0);
            }



            return numItem.Max();

        }

        private string AsignarNumeroItemsGrilla(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }

        private void btnQuitarChofer_Click(object sender, EventArgs e)
        {
            QuitarLineaChofer();
        }

        private void QuitarLineaChofer()
        {
            if (dgvChofer != null)
            {
                if (this.dgvChofer.CurrentRow != null && this.dgvChofer.CurrentRow.Cells["chIdChofer"].Value != null)
                {
                    if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            Int32 Codigo = (this.dgvChofer.CurrentRow.Cells["chIdChofer"].Value.ToString().Trim() != "" ? Convert.ToInt32(this.dgvChofer.CurrentRow.Cells["chIdChofer"].Value) : 0);
                            string Item = ((this.dgvChofer.CurrentRow.Cells["chItemChofer"].Value != null || this.dgvChofer.CurrentRow.Cells["chItemChofer"].Value.ToString().Trim() != "") ? Convert.ToString(this.dgvChofer.CurrentRow.Cells["chItemChofer"].Value) : "");

                            if (Codigo != 0)
                            {
                                driversRemoved.Add(new SJ_RHTransportistaChofer
                                {
                                    Id = Codigo,
                                    Item = Item,
                                });
                            }

                            dgvChofer.Rows.Remove(dgvChofer.CurrentRow);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                            return;
                        }
                    }
                }
            }
        }

        private void AgregarLineaContrato()
        {
            try
            {
                if (this.dgvContratos != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(0); // Codigo Cabecera                  
                    array.Add((AsignarNumeroItemsGrilla(ObtenerUltimoNumeroItemContrato(dgvContratos, dgvDocumentacion)))); // item                                                                                                                           
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta
                    array.Add(string.Empty); // Observacion
                    array.Add("AC"); // Grupo
                    array.Add("Activo"); // Grupo                                        
                    this.dgvContratos.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnAgregarContrato_Click(object sender, EventArgs e)
        {
            //Solo se podra agregar un detalle si tiene la ingresada correctamente en la cabecera
            if (ValidarDatosCabecera() == true)
            {
                AgregarLineaContrato();
            }
            else
            {
                RadMessageBox.Show(message);
            }
        }

        private void QuitarLineaContrato()
        {
            if (this.dgvContratos != null)
            {
                #region MyRegion


                if (this.dgvContratos.CurrentRow != null && this.dgvContratos.CurrentRow.Cells["chIdContrato"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {
                        Int32 Codigo = (this.dgvContratos.CurrentRow.Cells["chIdContrato"].Value.ToString().Trim() != "" ? Convert.ToInt32(this.dgvContratos.CurrentRow.Cells["chIdContrato"].Value) : 0);
                        string Item = ((this.dgvContratos.CurrentRow.Cells["chItemContrato"].Value != null || this.dgvContratos.CurrentRow.Cells["chItemContrato"].Value.ToString().Trim() != "") ? Convert.ToString(this.dgvContratos.CurrentRow.Cells["chItemContrato"].Value) : "");
                        if (Codigo != 0)
                        {
                            contractRemoved.Add(new SJ_RHTransportistaContrato
                            {
                                Id = Codigo,
                                Item = Item,
                            });
                        }

                        dgvContratos.Rows.Remove(dgvContratos.CurrentRow);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                        return;
                    }
                    //}
                }
                #endregion
            }
        }

        private void BtnQuitarContrato_Click(object sender, EventArgs e)
        {
            QuitarLineaContrato();
        }

        private void ActivarEdicionGrillaChofer(Boolean Estado)
        {
            if (Estado == true)
            {
                // Si el estado es activar (true), solo se activará fecha de Inicio, termino, Observacion, Estado
                for (int x = 0; x < this.dgvChofer.ColumnCount; x++)
                {

                    if (dgvChofer.Columns[x].Name == "chDNIChofer" |
                        dgvChofer.Columns[x].Name == "chNombresChofer" |
                        dgvChofer.Columns[x].Name == "chTipoLicenciaChofer" |
                        dgvChofer.Columns[x].Name == "chEsCapacitadoChofer" |
                        dgvChofer.Columns[x].Name == "chObservacionChofer" |
                        dgvChofer.Columns[x].Name == "chEstadoChofer"
                        )
                    {
                        dgvChofer.Columns[x].ReadOnly = !Estado;
                    }
                    else
                    {
                        dgvChofer.Columns[x].ReadOnly = Estado;
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.dgvChofer.ColumnCount; x++)
                {
                    dgvChofer.Columns[x].ReadOnly = !Estado;
                }
            }

        }

        private void ActivarEdicionGrillaContrato(Boolean Estado)
        {
            if (Estado == true)
            {
                // Si el estado es activar (true), solo se activará fecha de Inicio, termino, Observacion, Estado
                for (int x = 0; x < this.dgvContratos.ColumnCount; x++)
                {

                    if (dgvContratos.Columns[x].Name == "chFechaInicioContrato" |
                        dgvContratos.Columns[x].Name == "chFechaTerminoContrato" |
                        dgvContratos.Columns[x].Name == "chObservacionContrato" |
                        dgvContratos.Columns[x].Name == "chEstadoContrato")
                    {
                        dgvContratos.Columns[x].ReadOnly = !Estado;
                    }
                    else
                    {
                        dgvContratos.Columns[x].ReadOnly = Estado;
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.dgvContratos.ColumnCount; x++)
                {
                    dgvContratos.Columns[x].ReadOnly = !Estado;
                }
            }
        }

        private void ActivarEdicionGrillaRuta(Boolean Estado)
        {
            if (Estado == true)
            {
                // Si el estado es activar (true), solo se activará fecha de Inicio, termino, Observacion, Estado
                for (int x = 0; x < this.dgvRuta.ColumnCount; x++)
                {

                    if (dgvRuta.Columns[x].Name == "chObservacionRuta" |
                        dgvRuta.Columns[x].Name == "chPrecioFlete" |
                        dgvRuta.Columns[x].Name == "chPrecioPersona" |
                        dgvRuta.Columns[x].Name == "chPrecioVuelta" |
                        dgvRuta.Columns[x].Name == "chEstadoRuta")
                    {
                        dgvRuta.Columns[x].ReadOnly = !Estado;
                    }
                    else
                    {
                        dgvRuta.Columns[x].ReadOnly = Estado;
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.dgvRuta.ColumnCount; x++)
                {
                    dgvRuta.Columns[x].ReadOnly = !Estado;
                }
            }
        }

        private void ActivarEdicionGrillaDocumento(Boolean Estado)
        {
            if (Estado == true)
            {
                // Si el estado es activar (true), solo se activará fecha de Inicio, termino, Observacion, Estado
                for (int x = 0; x < this.dgvDocumentacion.ColumnCount; x++)
                {

                    if (dgvDocumentacion.Columns[x].Name == "chTypeDocumentId" |
                        dgvDocumentacion.Columns[x].Name == "chFechaInicioDD" |
                        dgvDocumentacion.Columns[x].Name == "chFechaTerminoDD" |
                        dgvDocumentacion.Columns[x].Name == "chObservacionDD")
                    {
                        dgvDocumentacion.Columns[x].ReadOnly = !Estado;
                    }
                    else
                    {
                        dgvDocumentacion.Columns[x].ReadOnly = Estado;
                    }
                }
            }
            else
            {
                for (int x = 0; x < this.dgvDocumentacion.ColumnCount; x++)
                {
                    dgvDocumentacion.Columns[x].ReadOnly = !Estado;
                }
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void EliminarRegistro()
        {
            bool status = true;
            try
            {
                if (_privilege != null)
                {
                    if (_privilege.eliminar == 1)
                    {
                        #region 
                        if (this.statusCode != string.Empty)
                        {
                            if (this.statusCode != "AN")
                            {

                                Modelo =new CarrierController();
                                var validarAsistenciasObservadas = Modelo.GetListAsistanceObserveredTransferByPlaca(
                                    Convert.ToInt32(CodigoTransportista),
                                    _conection,
                                    txtNumeroPlaca.Text.Trim());
                                if (validarAsistenciasObservadas != null && validarAsistenciasObservadas.Count > 0)
                                {
                                    MessageBox.Show("El transportista no se puede eliminar por que tiene asociadas " + validarAsistenciasObservadas.ToList().Count.ToString() + " Transferencias desde tablets.\nPara eliminar el registro, primero elimine las transferencias con este número de placa", "Mensaje del Sistema");
                                    status = false;
                                }


                                Modelo =new CarrierController();
                                var validarAsistenciasTransferidas = Modelo.GetListAsistanceTransferByPlaca(
                                    Convert.ToInt32(CodigoTransportista),
                                    _conection,
                                    txtNumeroPlaca.Text.Trim());
                                if (validarAsistenciasTransferidas != null && validarAsistenciasTransferidas.Count > 0)
                                {
                                    MessageBox.Show("El transportista no se puede eliminar por que tiene asociadas " + validarAsistenciasTransferidas.ToList().Count.ToString() + " Transferencias en observación desde tablets.\nPara eliminar el registro, primero elimine las transferencias observadas con este número de placa", "Mensaje del Sistema");
                                    status = false;
                                }

                                Modelo =new CarrierController();
                                var ValidarMovimientoFacturacion = Modelo.ObtenerListaMovimientoFacturacionTransportistaPorProveedor(Convert.ToInt32(CodigoTransportista), _conection).ToList();
                                if (ValidarMovimientoFacturacion != null && ValidarMovimientoFacturacion.ToList().Count > 0)
                                {
                                    MessageBox.Show("El transportista no se puede eliminar por que tiene asociadas " + ValidarMovimientoFacturacion.ToList().Count.ToString() + " facturas.\nPara eliminar el registro, primero elimine el movimiento de facturación asociado al este proveedor", "Mensaje del Sistema");
                                    status = false;
                                }

                                var ValidarMovimientoPartesRecorrido = Modelo.ObtenerListaMovimientoParteRecorridoTransportistaPorProveedor(Convert.ToInt32(CodigoTransportista), _conection).ToList();
                                if (ValidarMovimientoPartesRecorrido != null && ValidarMovimientoPartesRecorrido.ToList().Count > 0)
                                {
                                    string documentos = string.Empty;
                                    foreach (var item in ValidarMovimientoPartesRecorrido)
                                    {
                                        documentos += item.IdDocumento + " - " + item.Serie + " - " + item.Numero + " con el estado " + item.IdEstado + "\n";
                                    }
                                    MessageBox.Show("El transportista no se puede eliminar por que tiene asociadas " + ValidarMovimientoPartesRecorrido.ToList().Count.ToString() + " partes de recorrido" + "\n" + documentos + "\nPara eliminar el transportista, primero elimine el movimiento de facturación asociado al este proveedor", "Mensaje del Sistema");
                                    status = false;
                                }

                                if (status == true)
                                {
                                    Modelo =new CarrierController();
                                    Modelo.EliminarTransportista(Convert.ToInt32(CodigoTransportista), _conection);
                                    RadMessageBox.Show("Eliminado correctamente", "Atención");
                                    ObtenerListaTransportistas();
                                    GetContractListByCarrieId(this.txtCodigo.Text);
                                    GetDriverListByCarrieId(this.txtCodigo.Text);
                                    LimpiarFormulario();
                                    gbEdit.Enabled = false;
                                    gbTransporte.Enabled = false;
                                    gbList.Enabled = false;
                                    bgwHilo.RunWorkerAsync();

                                }

                            }
                            else
                            {
                                RadMessageBox.Show("El registro no tiene el estado para ser eliminado", "ADVERTENCIA DEL SISTEMA");
                                return;
                            }
                        }
                        else
                        {
                            RadMessageBox.Show("El registro no tiene el estado para ser eliminado", "ADVERTENCIA DEL SISTEMA");
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString() + "\nEliminar Registro", "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_privilege != null)
            {
                if (_privilege.exportar == 1)
                {
                    if (dgvTransportista != null)
                    {
                        if (dgvTransportista.Rows.Count > 0)
                        {
                            ExportarListaTransportistas();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void ExportarListaTransportistas()
        {

            Exportar(dgvTransportista);

        }

        private void Exportar(RadGridView radGridView)
        {
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                RadMessageBox.Show("Ingrese nombre al archivo.");
                return;
            }

            fileName = this.saveFileDialog.FileName;
            bool openExportFile = false;
            this.exportVisualSettings = true;
            RunExportToExcelML(fileName, ref openExportFile, radGridView);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla);
            excelExporter.SheetName = "Listado Transportistas";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = this.exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla.ThemeName);
                RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void Anular()
        {
            try
            {
                if (_privilege != null)
                {
                    if (_privilege.anular == 1)
                    {
                        #region
                        if (this.statusCode != "")
                        {
                            //if (this.CodigoEstado != "AN")
                            //{
                            posicionX = this.dgvTransportista.CurrentRow.Index;
                            posicionY = this.dgvTransportista.CurrentColumn.Index;

                            Modelo =new CarrierController();
                            Modelo.AnularTransportista(Convert.ToInt32(CodigoTransportista), _conection);
                            RadMessageBox.Show("Anulado correctamente", "Atención");

                            ObtenerListaTransportistas();
                            GetContractListByCarrieId(this.txtCodigo.Text);
                            GetDriverListByCarrieId(this.txtCodigo.Text);
                            GetListOfRoutesByCarrieId(this.txtCodigo.Text);
                            GetListOfDocumentsByCarrieId(this.txtCodigo.Text);

                            dgvTransportista.CurrentRow = dgvTransportista.Rows[posicionX];
                            //}
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString());
                return;

            }
        }

        private void AddRoute()
        {
            try
            {
                if (this.dgvRuta != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(this.txtCodigo.Text.ToString().Trim()); // Codigo Cabecera                  
                    array.Add((AsignarNumeroItemsGrilla(ObtenerUltimoNumeroItem(dgvRuta)))); // item
                    array.Add(0); // IdRuta
                    array.Add(string.Empty); // Ruta
                    array.Add(Convert.ToDecimal(0.0)); // PrecioPersona
                    array.Add(Convert.ToDecimal(0.0)); //PrecioFlete
                    array.Add(Convert.ToDecimal(0.0)); //PrecioVuelta
                    array.Add(string.Empty); // Observacion
                    array.Add("AC"); // IdEstado
                    array.Add("Activo"); // Estado                                        
                    this.dgvRuta.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnAgregarRuta_Click(object sender, EventArgs e)
        {
            //Solo se podra agregar un detalle si tiene la ingresada correctamente en la cabecera
            if (ValidarDatosCabecera() == true)
            {
                AddRoute();
            }
            else
            {
                RadMessageBox.Show(message);
            }
        }

        private void btnQuitarRuta_Click(object sender, EventArgs e)
        {
            QuitarLineaRuta();
        }

        private void QuitarLineaRuta()
        {
            if (this.dgvRuta != null)
            {
                #region
                if (this.dgvRuta.CurrentRow != null && this.dgvRuta.CurrentRow.Cells["chIdRuta"].Value != null)
                {
                    if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {

                            Int32 Codigo = (this.dgvRuta.CurrentRow.Cells["chIdRuta"].Value.ToString().Trim() != "" ? Convert.ToInt32(this.dgvRuta.CurrentRow.Cells["chIdRuta"].Value) : 0);
                            if (Codigo != 0)
                            {
                                Int32 IdRuta = ((this.dgvRuta.CurrentRow.Cells["chCodRuta"].Value != null | this.dgvRuta.CurrentRow.Cells["chCodRuta"].Value.ToString().Trim() != string.Empty) ? Convert.ToInt32(this.dgvRuta.CurrentRow.Cells["chCodRuta"].Value) : 0);
                                if (IdRuta != 0)
                                {
                                    routeRemoved.Add(new SJ_RHTransportistaRuta
                                    {
                                        Id = Codigo,
                                        IdRuta = IdRuta,
                                    });
                                }

                            }

                            dgvRuta.Rows.Remove(dgvRuta.CurrentRow);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                            return;
                        }
                    }
                }
                #endregion
            }
        }

        private void dgvRuta_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo =new CarrierController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chDescripcionRuta")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple busquedas = new frmBusquedaFormatoSimple();
                        busquedas.ListaGeneralResultado = Modelo.GetRutasSistemaTransportistas(_conection);
                        busquedas.Text = "Buscar Rutas";
                        busquedas.txtTextoFiltro.Text = "";
                        if (busquedas.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvRuta.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chCodRuta"].Value = busquedas.ObjetoRetorno.Codigo;
                            this.dgvRuta.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDescripcionRuta"].Value = busquedas.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }

        private void btnRUC_Click(object sender, EventArgs e)
        {

        }

        private void dgvTransportista_SelectionChanged(object sender, EventArgs e)
        {

            transportUnit = new SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult();
            if (dgvTransportista.Rows.Count > 0)
            {
                if (dgvTransportista.CurrentRow != null && dgvTransportista.CurrentRow.Cells["chId"].Value != null)
                {
                    try
                    {
                        #region
                        CodigoTransportista = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chId"].Value.ToString()) : "";

                        var listaCoincidencia = ListadoTransportista.Where(x => x.Id.ToString() == CodigoTransportista).ToList();

                        if (listaCoincidencia != null && listaCoincidencia.ToList().Count == 1)
                        {
                            transportUnit = listaCoincidencia.Single();
                        }

                        this.txtCodigo.Text = CodigoTransportista;
                        //this.txtCodigo.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chId"].Value.ToString()) : "";
                        statusCode = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chIdEstado"].Value.ToString()) : "";
                        this.txtIdEstado.Text = statusCode;
                        this.txtEstado.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chEstado"].Value.ToString()) : "";
                        this.txtRUCNumero.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chRUC"].Value.ToString()) : "";
                        this.txtRucDescripcion.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chRazonSocial"].Value.ToString()) : "";
                        this.txtNombreComercial.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chPseudoNombre"].Value.ToString()) : "";
                        this.txtNumeroPlaca.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chPlaca"].Value.ToString()) : "";
                        this.txtNumeroAsientos.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chNumeroAsientos"].Value.ToString()) : "";
                        this.txtPesoMaximo.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chPesoMaximo"].Value.ToString()) : "";
                        this.txtAnioFabricación.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chAnioFabricacion"].Value.ToString()) : "";
                        this.txtMarca.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chMarca"].Value.ToString()) : "";
                        this.txtModelo.Text = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chModelo"].Value.ToString()) : "";

                        #region Realiza transporte Local()
                        if (dgvTransportista.CurrentRow.Cells["chEsMovilidadLocal"].Value != null)
                        {
                            if (Convert.ToInt32(dgvTransportista.CurrentRow.Cells["chEsMovilidadLocal"].Value) == 1)
                            {
                                chkTransporteInterno.Checked = true;
                            }
                            else
                            {
                                chkTransporteInterno.Checked = false;
                            }
                        }
                        else
                        {
                            chkTransporteInterno.Checked = false;
                        }
                        #endregion

                        #region Realiza transporte InterLocalidad()
                        if (dgvTransportista.CurrentRow.Cells["chEsInterLocal"].Value != null)
                        {
                            if (Convert.ToInt32(dgvTransportista.CurrentRow.Cells["chEsInterLocal"].Value) == 1)
                            {
                                chkTransporteInterlocal.Checked = true;
                            }
                            else
                            {
                                chkTransporteInterlocal.Checked = false;
                            }
                        }
                        else
                        {
                            chkTransporteInterlocal.Checked = false;
                        }
                        #endregion


                        this.cboTipoMovilidad.SelectedValue = dgvTransportista.CurrentRow.Cells["chId"].Value != null ? Convert.ToString(dgvTransportista.CurrentRow.Cells["chIdTipoMovilidad"].Value.ToString().Trim()) : "000";

                        if (this.txtCodigo.Text != "")
                        {
                            #region
                            try
                            {
                                driversRemoved = new List<SJ_RHTransportistaChofer>();
                                contractRemoved = new List<SJ_RHTransportistaContrato>();
                                routeRemoved = new List<SJ_RHTransportistaRuta>();
                                documentRemoved = new List<SJ_RHTransportistaContrato>();

                                GetContractListByCarrieId(CodigoTransportista);
                                GetDriverListByCarrieId(CodigoTransportista);
                                GetListOfRoutesByCarrieId(CodigoTransportista);
                                GetListOfDocumentsByCarrieId(CodigoTransportista);


                            }
                            catch (Exception Ex)
                            {
                                throw Ex;
                            }
                            #endregion
                        }
                        else
                        {
                            #region
                            //Limpiar Grilla Chofer
                            List<SJ_RHListarDetalleTransportistaChoferResult> ListaVaciaGrillaChofer = new List<SJ_RHListarDetalleTransportistaChoferResult>();
                            dgvChofer.CargarDatos(ListaVaciaGrillaChofer.ToDataTable<SJ_RHListarDetalleTransportistaChoferResult>());
                            dgvChofer.Refresh();

                            //Limpiar Grilla Contratos
                            List<SJ_RHListarDetalleTransportistaContratoResult> ListaVaciaGrillaContratos = new List<SJ_RHListarDetalleTransportistaContratoResult>();
                            this.dgvContratos.CargarDatos(ListaVaciaGrillaContratos.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
                            this.dgvContratos.Refresh();

                            //Limpiar Grilla Ruta
                            List<SJ_RHListarDetalleTransportistaRutaResult> ListaVaciaGrillaRuta = new List<SJ_RHListarDetalleTransportistaRutaResult>();
                            this.dgvRuta.CargarDatos(ListaVaciaGrillaContratos.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
                            this.dgvRuta.Refresh();

                            //Limpiar Grilla Documentos
                            List<SJ_RHListarDetalleTransportistaContratoResult> listEmptyDocuments = new List<SJ_RHListarDetalleTransportistaContratoResult>();
                            this.dgvDocumentacion.CargarDatos(listEmptyDocuments.ToDataTable<SJ_RHListarDetalleTransportistaContratoResult>());
                            this.dgvDocumentacion.Refresh();

                            #endregion
                        }
                        #endregion
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "", "MENSAJE DEL SISTEMA");
                        return;
                    }
                }

            }
        }

        private void anularRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            ObtenerListaTransportistas();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowResult();

        }

        private void verDocumentosYMovimientoAsociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (oDatosUnidadTransporte != null && oDatosUnidadTransporte.Id != null)
            //{
            //    MovimientosAsociadosPartesRecorridoyFacturacionTransporte oFrm = new MovimientosAsociadosPartesRecorridoyFacturacionTransporte(oDatosUnidadTransporte);
            //    oFrm.ShowDialog();
            //}
        }

        private void txtNumeroPlaca_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtNumeroPlaca_Leave(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text.ToString().Trim() == "")
            {
                //txtNumeroPlaca.ReadOnly = false;
                ValidarPlaca(txtNumeroPlaca.Text);
            }
            else
            {
                //txtNumeroPlaca.ReadOnly = true;
                btnGrabar.Enabled = true;
            }
        }

        private void ValidarPlaca(string numeroPlaca)
        {
            Modelo =new CarrierController();
            duplicado = false;
            duplicado = Modelo.VerificarDuplicidadPlaca(numeroPlaca, _conection);

            pcbDuplicidadPlaca.Visible = false;
            pcbValidadorPlaca.Visible = true;
            btnGrabar.Enabled = true;
            if (duplicado == true)
            {
                pcbDuplicidadPlaca.Visible = true;
                pcbValidadorPlaca.Visible = true;
                btnGrabar.Enabled = false;
                this.txtNumeroPlaca.Focus();
                return;
            }

        }

        private void txtRUCNumero_Leave(object sender, EventArgs e)
        {
            if (txtRUCNumero.Text.ToString().Trim() != "")
            {
                ObtenerNombreComercialByNroRUC(txtRUCNumero.Text);
            }
        }

        private void ObtenerNombreComercialByNroRUC(string numeroRUC)
        {
            Modelo =new CarrierController();
            tradename = "";
            tradename = Modelo.ObtenerNombreComercialByNroRUC(numeroRUC, _conection);

            this.txtNombreComercial.Text = tradename;


        }

        private void btnAltaTarifa_Click(object sender, EventArgs e)
        {

        }

        private void btnAltaBaja_Click(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        }

        private void radButton4_Click(object sender, EventArgs e)
        {

        }

        private void radButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (_privilege != null)
            {
                if (_privilege.editar == 1)
                {

                }
                else
                {
                    MessageBox.Show("No tiene privilegios para esta acción", "ADVERTENCIA DEL SISTEMA");
                    return;
                }
            }
        }

        private void dgvTransportista_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void CatalogoEmpresasServiciosTransportePersonalCampo_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void gbMantenimientoRegistros_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtEstado_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtRUCNumero_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtRucDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtNombreComercial_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void cboTipoMovilidad_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void radGroupBox1_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtNumeroPlaca_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtAnioFabricación_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtMarca_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtModelo_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtNumeroAsientos_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void txtPesoMaximo_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void radButton4_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void radButton3_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void btnAgregarContrato_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void BtnQuitarContrato_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void dgvContratos_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void btnCancelar_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void btnGrabar_KeyDown(object sender, KeyEventArgs e)
        {
            EdicionDesdeTeclado(sender, e);
        }

        private void EdicionDesdeTeclado(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.G)
            {
                Save();
            }
            if (e.KeyData == (Keys.Control | Keys.E))
            {
                Edit();
            }
            if (e.KeyData == (Keys.Control | Keys.N))
            {
                Nuevo();
            }
            if (e.KeyData == (Keys.Control | Keys.Z))
            {
                Atras();
            }
            if (e.KeyData == (Keys.Escape))
            {
                Atras();
            }
        }

        private void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            if (ValidarDatosCabecera() == true)
            {
                AddDocument();
            }
            else
            {
                RadMessageBox.Show(message);
            }
        }

        private void AddDocument()
        {
            Modelo =new CarrierController();
            try
            {
                if (dgvDocumentacion != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(0); // Codigo Cabecera                  
                    array.Add((AsignarNumeroItemsGrilla(ObtenerUltimoNumeroItemContrato(dgvDocumentacion, dgvContratos)))); // item
                    array.Add(0); // TypeDocumentId (int)
                    array.Add(string.Empty); // documento (readlony = true)
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta
                    array.Add(string.Empty); // Observacion
                    array.Add("AC"); // IdEstado
                    array.Add("Activo"); // Estado                                        
                    dgvDocumentacion.AgregarFila(array);
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnQuitarDocumento_Click(object sender, EventArgs e)
        {
            DeleteDocument();
        }

        private void DeleteDocument()
        {
            if (dgvDocumentacion != null)
            {
                #region
                if (dgvDocumentacion.CurrentRow != null && dgvDocumentacion.CurrentRow.Cells["chIIdDD"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 Codigo = (dgvDocumentacion.CurrentRow.Cells["chIIdDD"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDocumentacion.CurrentRow.Cells["chIIdDD"].Value) : 0);
                        if (Codigo != 0)
                        {
                            string itemDD = ((dgvDocumentacion.CurrentRow.Cells["chItemDD"].Value != null | dgvDocumentacion.CurrentRow.Cells["chItemDD"].Value.ToString().Trim() != string.Empty) ? (dgvDocumentacion.CurrentRow.Cells["chItemDD"].Value.ToString()) : string.Empty);
                            if (Codigo != 0 && itemDD != string.Empty)
                            {

                                documentRemoved.Add(new SJ_RHTransportistaContrato
                                {
                                    Id = Codigo,
                                    Item = itemDD,
                                });
                            }

                        }

                        dgvDocumentacion.Rows.Remove(dgvDocumentacion.CurrentRow);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                        return;
                    }
                    //}
                }
                #endregion
            }
        }

        private void radLabel14_Click(object sender, EventArgs e)
        {

        }

        private void lblMarca_Click(object sender, EventArgs e)
        {

        }

        private void txtMarca_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDocumentacion_KeyUp(object sender, KeyEventArgs e)
        {
            Modelo =new CarrierController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chTypeDocumentId")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = Modelo.GetTypeDocumentoFormat(_conection);
                        search.Text = "Buscar tipo de documento";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDocumentacion.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chTypeDocumentId"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDocumentacion.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chdocumentoDD"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion
            }
        }

        private void tabDocumentacion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CatalogoEmpresaDeServicioDeTransporteDePersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
