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
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class DispositivosEdicion : Form
    {
        #region Declaración de variables()        
        private string conection;
        private SAS_USUARIOS user2;
        private string companyId;
        private PrivilegesByUser privilege;
        private SAS_ListadoDeDispositivos DispositivoEdicionByList;
        private SAS_ListadoDeDispositivosByIdDeviceResult DispositivoQuery;
        private SAS_Dispostivo oDispositivo;
        private string oConexion = string.Empty;
        private ComboBoxHelper comboHelper;
        private List<Grupo> sedes;
        private List<Grupo> condicionesProductos;
        private SAS_Dispostivo dispositivo;
        private SAS_DispostivoController modelo;
        private List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult> ipListByDevice;
        private List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> colaboradoresPorDevice;
        private List<SAS_DispositivoComponentesByDeviceResult> componentesPorDevice;
        private List<SAS_DispositivoCuentaUsuariosByDeviceResult> cuentasUsuariosPorDevice;
        private List<SAS_DispositivoDocumentoByDeviceResult> documentoPorDevice;

        private List<SAS_DispositivoIP> listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
        private List<SAS_DispositivoIP> listadoNumeroIp = new List<SAS_DispositivoIP>();
        private List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados = new List<SAS_DispositivoUsuarios>();
        private List<SAS_DispositivoUsuarios> listadoColaboradores;
        private List<SAS_DispositivoHardwareByDeviceResult> hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>();
        private List<SAS_DispositivoSoftwareByDeviceResult> softwarePorDevice = new List<SAS_DispositivoSoftwareByDeviceResult>();
        private SAS_DispositivoHardwareController modelHardware;
        private SAS_DispositivoSoftwareController modelSoftware;
        private List<SAS_DispositivoHardware> listadoHardware, listadoHardwareEliminados = new List<SAS_DispositivoHardware>();
        private List<SAS_DispositivoSoftware> listadoSoftware, listadoSoftwareEliminados = new List<SAS_DispositivoSoftware>();
        private List<SAS_DispositivoContadores> listadoContadores, listadoContadoresEliminados = new List<SAS_DispositivoContadores>();
        private List<SAS_DispositivoMovimientoMantenimientos> listadoMantenimientos, listadoMantenimientosEliminados = new List<SAS_DispositivoMovimientoMantenimientos>();
        private List<SAS_DispositivoMovimientoAlmacen> listadoMovimientoAlmacen, listadoMovimientoAlmacenEliminados = new List<SAS_DispositivoMovimientoAlmacen>();
        private List<SAS_DispositivoComponentes> listadoComponentesEliminados = new List<SAS_DispositivoComponentes>();
        private List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuariosEliminados = new List<SAS_DispositivoCuentaUsuarios>();
        private List<SAS_DispositivoDocumento> listadoDocumentosEliminados = new List<SAS_DispositivoDocumento>();
        private List<SAS_DispositivoComponentes> listadoComponentes = new List<SAS_DispositivoComponentes>();
        private List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuarios = new List<SAS_DispositivoCuentaUsuarios>();
        private List<SAS_DispositivoDocumento> listadoDocumentos = new List<SAS_DispositivoDocumento>();
        private SAS_DispositivoComponentesController modelComponente = new SAS_DispositivoComponentesController();
        private SAS_DispositivoCuentaUsuariosController modelCuentasUsuario = new SAS_DispositivoCuentaUsuariosController();
        private SAS_DispositivoDocumentoController modelDocumentos = new SAS_DispositivoDocumentoController();
        private string msgError = string.Empty;
        private List<Grupo> workAreas;
        Byte[] pic;
        private SAS_ListadoDeDispositivosByIdDeviceResult dispositivoAsociado;
        private int codigoPrincipalDetalleComponente;
        private int codigoComponenteDetalleComponente;
        private int codigoEstadoDetalleComponente;
        private int idDispositivo = 0;
        private int ultimoItemNumeroIP, ultimoColaborador, ultimoItemHardware, ultimoItemSoftware, ultimoItemComponente, ultimoItemDocumento, ultimoItemCuentaDeUsuario, ultimoItemContador, ultimoItemManteniento, ultimoMovimientoAlmacen = 1;

        private SAS_DispositivoContadoresController modelContadores;
        private SAS_DispositivoMovimientoMantenimientosController modelMantenimiento;
        private SAS_DispositivoMovimientoAlmacenController modelMovimientoAlmacen;
        private List<SAS_DispositivoaccountantsByDeviceIDResult> contadoresPorDevice;
        private List<SAS_DispositivoMaintenanceByDeviceIDResult> manteninientosPorDevice;
        private List<SAS_DispositivoWharehouseMovementsByDeviceIDResult> movimientoAlmacenPorDevice;
        private int resultadoAccion;
        private string nombreformulario = "DISPOSITIVO";
        #endregion

        public DispositivosEdicion()
        {
            InitializeComponent();
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            oDispositivo = new SAS_Dispostivo();
            //oDispositivo.id = 0;
            idDispositivo = 0;
            CargarObjeto();
        }

        public DispositivosEdicion(string _oConexion, SAS_ListadoDeDispositivos _oDispositivoQuery)
        {
            InitializeComponent();
            oConexion = _oConexion;
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            oDispositivo = new SAS_Dispostivo();
            DispositivoEdicionByList = _oDispositivoQuery;
            oDispositivo.id = _oDispositivoQuery.id;
            idDispositivo = _oDispositivoQuery.id;
            CargarCombos();
            Inicio();
            CargarObjeto();
            AccionFormulario("Edicion");

        }

        public DispositivosEdicion(string _oConexion, SAS_ListadoDeDispositivosByIdDeviceResult _oDispositivoQuery)
        {
            InitializeComponent();
            oConexion = _oConexion;
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            oDispositivo = new SAS_Dispostivo();
            DispositivoQuery = _oDispositivoQuery;
            oDispositivo.id = _oDispositivoQuery.id;
            idDispositivo = _oDispositivoQuery.id;
            CargarCombos();
            Inicio();
            #region
            //_Dispositivo = new SAS_ListadoDeDispositivos();
            //_Dispositivo.id = _DispositivoQuery.id;
            //_Dispositivo.latitud = _DispositivoQuery.latitud;
            //_Dispositivo.longitud = _DispositivoQuery.longitud;
            //_Dispositivo.nombres = _DispositivoQuery.nombres;
            //_Dispositivo.dispositivo = _DispositivoQuery.dispositivo;
            //_Dispositivo.sedeCodigo = _DispositivoQuery.sedeCodigo;
            //_Dispositivo.sedeDescripcion = _DispositivoQuery.sedeDescripcion;
            //_Dispositivo.numeroSerie = _DispositivoQuery.numeroSerie;
            //_Dispositivo.caracteristicas = _DispositivoQuery.caracteristicas;
            //_Dispositivo.idestado = _DispositivoQuery.idestado;
            //_Dispositivo.tipoDispositivoCodigo = _DispositivoQuery.tipoDispositivoCodigo;
            //_Dispositivo.tipoDispositivo = _DispositivoQuery.tipoDispositivo;
            //_Dispositivo.estado = _DispositivoQuery.estado;
            //_Dispositivo.item = _DispositivoQuery.item;
            //_Dispositivo.codigoSegmentoIP = _DispositivoQuery.codigoSegmentoIP;
            //_Dispositivo.numeroIP = _DispositivoQuery.numeroIP;
            //_Dispositivo.creadoPor = _DispositivoQuery.creadoPor;
            //_Dispositivo.fechacreacion = _DispositivoQuery.fechacreacion;
            //_Dispositivo.activoCodigoERP = _DispositivoQuery.activoCodigoERP;
            //_Dispositivo.activo = _DispositivoQuery.activo;
            //_Dispositivo.IdDispostivoColor = _DispositivoQuery.IdDispostivoColor;
            //_Dispositivo.color = _DispositivoQuery.color;
            //_Dispositivo.idModelo = _DispositivoQuery.idModelo;
            //_Dispositivo.MODELO = _DispositivoQuery.MODELO;
            //_Dispositivo.idMarca = _DispositivoQuery.idMarca;
            //_Dispositivo.marca = _DispositivoQuery.marca;
            //_Dispositivo.numeroParte = _DispositivoQuery.numeroParte;
            //_Dispositivo.IdEstadoProducto = _DispositivoQuery.IdEstadoProducto;
            //_Dispositivo.estadoProducto = _DispositivoQuery.estadoProducto;
            //_Dispositivo.EsPropio = _DispositivoQuery.EsPropio;
            //_Dispositivo.AlquiladoPropio = _DispositivoQuery.AlquiladoPropio;
            //_Dispositivo.idProducto = _DispositivoQuery.idProducto;
            //_Dispositivo.producto = _DispositivoQuery.producto;
            //_Dispositivo.rutaImagen = _DispositivoQuery.rutaImagen;
            //_Dispositivo.funcionamientoCodigo = _DispositivoQuery.funcionamientoCodigo;
            //_Dispositivo.funcionamiento = _DispositivoQuery.funcionamiento;
            //_Dispositivo.idClieprov = _DispositivoQuery.idClieprov;
            //_Dispositivo.razonSocial = _DispositivoQuery.razonSocial;
            //_Dispositivo.coordenada = _DispositivoQuery.coordenada;
            //_Dispositivo.fechaActivacion = _DispositivoQuery.fechaActivacion;
            //_Dispositivo.idCobrarpagarDoc = _DispositivoQuery.idCobrarpagarDoc;
            //_Dispositivo.documentoCompra = _DispositivoQuery.documentoCompra;
            //_Dispositivo.fechaBaja = _DispositivoQuery.fechaBaja;
            //_Dispositivo.fechaProduccion = _DispositivoQuery.fechaProduccion;
            //_Dispositivo.esFinal = _DispositivoQuery.esFinal;
            //_Dispositivo.registro = _DispositivoQuery.registro;
            //_Dispositivo.RegistradoNoRegistrado = _DispositivoQuery.RegistradoNoRegistrado;
            //_Dispositivo.idarea = _DispositivoQuery.idarea;
            //_Dispositivo.area = _DispositivoQuery.area;
            //_Dispositivo.imagen = _DispositivoQuery.imagen;

            //_Dispositivo.ubicacion = _DispositivoQuery.ubicacion;
            //_Dispositivo.costoUSD = _DispositivoQuery.costoUSD;
            //_Dispositivo.lineaCelular = _DispositivoQuery.lineaCelular;
            //_Dispositivo.AniosParaDepreciar = _DispositivoQuery.AniosParaDepreciar;
            #endregion

            CargarObjeto();
            #region 
            //this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
            //this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
            //this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
            //this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
            //this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
            //this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
            //this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
            //this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
            //this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
            //this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
            //this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
            //this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
            //this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
            //this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
            //this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
            //this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
            //this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
            //this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

            //this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
            //this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;


            //this.txtUbicacion.Text = this._Dispositivo.ubicacion != null ? this._Dispositivo.ubicacion.Trim() : string.Empty;
            //this.txtCostoUSD.Text = this._Dispositivo.costoUSD != (decimal?)null ? this._Dispositivo.costoUSD.ToDecimalPresentation() : "0.0";
            //this.txtLineaCelularCodigo.Text = this._Dispositivo.lineaCelular != null ? this._Dispositivo.lineaCelular.Trim() : string.Empty;
            //this.txtAnioDepreciar.Value = this._Dispositivo.AniosParaDepreciar != (decimal?)null ? this._Dispositivo.AniosParaDepreciar : 0;


            //this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
            //this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;
            //if (oDispositivoQuery.EsPropio == 1)
            //{
            //    rbtPropio.Checked = true;
            //}
            //else
            //{
            //    if (oDispositivoQuery.EsPropio == 2)
            //    {

            //        rbtInvitado.Checked = true;
            //    }
            //    else
            //    {
            //        rbtAlquilado.Checked = true;
            //    }
            //}




            //if (oDispositivoQuery.funcionamientoCodigo == 1)
            //{
            //    btnEnOperacion.Checked = true;
            //}
            //else
            //{
            //    btnNoActivo.Checked = true;
            //}
            //if (oDispositivoQuery.esFinal == 1)
            //{
            //    chkEsFinal.Checked = true;
            //}
            //else
            //{
            //    chkEsFinal.Checked = false;
            //}
            //this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
            //this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
            //this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
            //this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
            //this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
            //this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
            //txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
            //txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
            //this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
            //this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";
            #endregion
            AccionFormulario("Edicion");


        }

        private void CargarObjeto()
        {
            try
            {
                gbDispositivo.Enabled = false;
                gbDetalles.Enabled = false;
                BarraPrincipal.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }
        }

        public DispositivosEdicion(string _oConexion, SAS_ListadoDeDispositivos _oDispositivoQuery, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            conection = _oConexion.Trim() != string.Empty ? _oConexion.Trim() : "SAS";
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            oConexion = _oConexion.Trim() != string.Empty ? _oConexion.Trim() : "SAS";
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            oDispositivo = new SAS_Dispostivo();
            oDispositivo.id = _oDispositivoQuery.id;
            idDispositivo = _oDispositivoQuery.id;
            CargarCombos();
            Inicio();

            #region MyRegion            
            //this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
            //this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
            //this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
            //this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
            //this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
            //this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
            //this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
            //this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
            //this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
            //this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
            //this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
            //this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
            //this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
            //this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
            //this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
            //this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
            //this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
            //this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

            //this.txtUbicacion.Text = this._Dispositivo.ubicacion != null ? this._Dispositivo.ubicacion.Trim() : string.Empty;
            //this.txtCostoUSD.Text = this._Dispositivo.costoUSD != (decimal?)null ? this._Dispositivo.costoUSD.ToDecimalPresentation() : "0.0";
            //this.txtLineaCelularCodigo.Text = this._Dispositivo.lineaCelular != null ? this._Dispositivo.lineaCelular.Trim() : string.Empty;
            //this.txtAnioDepreciar.Value = this._Dispositivo.AniosParaDepreciar != (decimal?)null ? this._Dispositivo.AniosParaDepreciar : 0;

            //this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
            //this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;


            //this.txtMtoUSD.Text = this._Dispositivo.costoMantenimientoAnualUSD != (decimal?)null ? this._Dispositivo.costoMantenimientoAnualUSD.ToDecimalPresentation() : "0.00";
            //this.txtSuministroUSD.Text = this._Dispositivo.costoSuministroAnualUSD != (decimal?)null ? this._Dispositivo.costoSuministroAnualUSD.ToDecimalPresentation() : "0.00";
            //this.txtKiloVatioHora.Text = this._Dispositivo.kilovatioHora != (decimal?)null ? this._Dispositivo.kilovatioHora.ToString() : "0.00";
            //cboFacturaciónDeConsumoEnergético.SelectedValue = this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico != null ? this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico.ToString().Trim() : "0";

            //this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
            //this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;

            //rbtPropio.Checked = false;
            //rbtInvitado.Checked = false;
            //rbtAlquilado.Checked = false;

            //if (oDispositivoQuery.EsPropio == 1)
            //{
            //    rbtPropio.Checked = true;                
            //}
            //else
            //{
            //    if (oDispositivoQuery.EsPropio == 2)
            //    {

            //        rbtInvitado.Checked = true;
            //    }
            //    else
            //    {
            //        rbtAlquilado.Checked = true;
            //    }
            //}

            //if (oDispositivoQuery.funcionamientoCodigo == 1)
            //{
            //    btnEnOperacion.Checked = true;
            //}
            //else
            //{
            //    btnNoActivo.Checked = true;
            //}
            //if (oDispositivoQuery.esFinal == 1)
            //{
            //    chkEsFinal.Checked = true;
            //}
            //else
            //{
            //    chkEsFinal.Checked = false;
            //}
            //this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
            //this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
            //this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
            //this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
            //this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
            //this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
            //txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
            //txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
            //this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
            //this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";

            //workAreas = comboHelper.ListadoDeTipoDesistemaPorCodigoTipoDispositivo("SAS", (this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.ToString() : "000"));
            //cboTipoDeSistema.DisplayMember = "Descripcion";
            //cboTipoDeSistema.ValueMember = "Codigo";
            //cboTipoDeSistema.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();
            //this.cboTipoDeSistema.SelectedValue = this._Dispositivo.idSistemaDeImpresion != null ? this._Dispositivo.idSistemaDeImpresion.ToString().Trim() : "7";
            #endregion
            CargarObjeto();
            AccionFormulario("Edicion");

        }

        public DispositivosEdicion(string _oConexion, SAS_ListadoDeDispositivosByIdDeviceResult _oDispositivoQuery, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            conection = _oConexion;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            oConexion = _oConexion;
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            oDispositivo = new SAS_Dispostivo();
            DispositivoQuery = _oDispositivoQuery;
            oDispositivo.id = _oDispositivoQuery.id;
            idDispositivo = _oDispositivoQuery.id;

            CargarCombos();
            Inicio();
            #region 
            //_Dispositivo = new SAS_ListadoDeDispositivos();
            //_Dispositivo.id = _DispositivoQuery.id;
            //_Dispositivo.latitud = _DispositivoQuery.latitud;
            //_Dispositivo.longitud = _DispositivoQuery.longitud;
            //_Dispositivo.nombres = _DispositivoQuery.nombres;
            //_Dispositivo.dispositivo = _DispositivoQuery.dispositivo;
            //_Dispositivo.sedeCodigo = _DispositivoQuery.sedeCodigo;
            //_Dispositivo.sedeDescripcion = _DispositivoQuery.sedeDescripcion;
            //_Dispositivo.numeroSerie = _DispositivoQuery.numeroSerie;
            //_Dispositivo.caracteristicas = _DispositivoQuery.caracteristicas;
            //_Dispositivo.idestado = _DispositivoQuery.idestado;
            //_Dispositivo.tipoDispositivoCodigo = _DispositivoQuery.tipoDispositivoCodigo;
            //_Dispositivo.tipoDispositivo = _DispositivoQuery.tipoDispositivo;
            //_Dispositivo.estado = _DispositivoQuery.estado;
            //_Dispositivo.item = _DispositivoQuery.item;
            //_Dispositivo.codigoSegmentoIP = _DispositivoQuery.codigoSegmentoIP;
            //_Dispositivo.numeroIP = _DispositivoQuery.numeroIP;
            //_Dispositivo.creadoPor = _DispositivoQuery.creadoPor;
            //_Dispositivo.fechacreacion = _DispositivoQuery.fechacreacion;
            //_Dispositivo.activoCodigoERP = _DispositivoQuery.activoCodigoERP;
            //_Dispositivo.activo = _DispositivoQuery.activo;
            //_Dispositivo.IdDispostivoColor = _DispositivoQuery.IdDispostivoColor;
            //_Dispositivo.color = _DispositivoQuery.color;
            //_Dispositivo.idModelo = _DispositivoQuery.idModelo;
            //_Dispositivo.MODELO = _DispositivoQuery.MODELO;
            //_Dispositivo.idMarca = _DispositivoQuery.idMarca;
            //_Dispositivo.marca = _DispositivoQuery.marca;
            //_Dispositivo.numeroParte = _DispositivoQuery.numeroParte;
            //_Dispositivo.IdEstadoProducto = _DispositivoQuery.IdEstadoProducto;
            //_Dispositivo.estadoProducto = _DispositivoQuery.estadoProducto;
            //_Dispositivo.EsPropio = _DispositivoQuery.EsPropio;
            //_Dispositivo.AlquiladoPropio = _DispositivoQuery.AlquiladoPropio;
            //_Dispositivo.idProducto = _DispositivoQuery.idProducto;
            //_Dispositivo.producto = _DispositivoQuery.producto;
            //_Dispositivo.rutaImagen = _DispositivoQuery.rutaImagen;
            //_Dispositivo.funcionamientoCodigo = _DispositivoQuery.funcionamientoCodigo;
            //_Dispositivo.funcionamiento = _DispositivoQuery.funcionamiento;
            //_Dispositivo.idClieprov = _DispositivoQuery.idClieprov;
            //_Dispositivo.razonSocial = _DispositivoQuery.razonSocial;
            //_Dispositivo.coordenada = _DispositivoQuery.coordenada;
            //_Dispositivo.fechaActivacion = _DispositivoQuery.fechaActivacion;
            //_Dispositivo.idCobrarpagarDoc = _DispositivoQuery.idCobrarpagarDoc;
            //_Dispositivo.documentoCompra = _DispositivoQuery.documentoCompra;
            //_Dispositivo.fechaBaja = _DispositivoQuery.fechaBaja;
            //_Dispositivo.fechaProduccion = _DispositivoQuery.fechaProduccion;
            //_Dispositivo.esFinal = _DispositivoQuery.esFinal;
            //_Dispositivo.registro = _DispositivoQuery.registro;
            //_Dispositivo.RegistradoNoRegistrado = _DispositivoQuery.RegistradoNoRegistrado;
            //_Dispositivo.idarea = _DispositivoQuery.idarea;
            //_Dispositivo.area = _DispositivoQuery.area;
            //_Dispositivo.imagen = _DispositivoQuery.imagen;

            //_Dispositivo.ubicacion = _DispositivoQuery.ubicacion;
            //_Dispositivo.costoUSD = _DispositivoQuery.costoUSD;
            //_Dispositivo.lineaCelular = _DispositivoQuery.lineaCelular;
            //_Dispositivo.AniosParaDepreciar = _DispositivoQuery.AniosParaDepreciar;

            //_Dispositivo.kilovatioHora = _DispositivoQuery.kilovatioHora;
            //_Dispositivo.tipoDeFacturacionDeConsumoEnergetico = _DispositivoQuery.tipoDeFacturacionDeConsumoEnergetico;





            //this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
            //this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
            //this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
            //this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
            //this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
            //this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
            //this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
            //this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
            //this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
            //this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
            //this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
            //this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
            //this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
            //this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
            //this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
            //this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
            //this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
            //this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

            //this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
            //this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;

            //this.txtMtoUSD.Text = this._Dispositivo.costoMantenimientoAnualUSD != (decimal?)null ? this._Dispositivo.costoMantenimientoAnualUSD.ToDecimalPresentation() : "0.00";
            //this.txtSuministroUSD.Text = this._Dispositivo.costoSuministroAnualUSD != (decimal?)null ? this._Dispositivo.costoSuministroAnualUSD.ToDecimalPresentation() : "0.00";

            //this.txtKiloVatioHora.Text = this._Dispositivo.kilovatioHora != (decimal?)null ? this._Dispositivo.kilovatioHora.ToDecimalPresentation() : "0.00";
            //cboFacturaciónDeConsumoEnergético.SelectedValue = this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico != null ? this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico.ToString().Trim() : "0";


            //this.txtUbicacion.Text = this._Dispositivo.ubicacion != null ? this._Dispositivo.ubicacion.Trim() : string.Empty;
            //this.txtCostoUSD.Text = this._Dispositivo.costoUSD != (decimal?)null ? this._Dispositivo.costoUSD.ToDecimalPresentation() : "0.0";
            //this.txtLineaCelularCodigo.Text = this._Dispositivo.lineaCelular != null ? this._Dispositivo.lineaCelular.Trim() : string.Empty;
            //this.txtAnioDepreciar.Value = this._Dispositivo.AniosParaDepreciar != (decimal?)null ? this._Dispositivo.AniosParaDepreciar : 0;


            //this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
            //this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;
            //if (oDispositivoQuery.EsPropio == 1)
            //{
            //    rbtPropio.Checked = true;
            //}
            //else
            //{
            //    if (oDispositivoQuery.EsPropio == 2)
            //    {

            //        rbtInvitado.Checked = true;
            //    }
            //    else
            //    {
            //        rbtAlquilado.Checked = true;
            //    }
            //}




            //if (oDispositivoQuery.funcionamientoCodigo == 1)
            //{
            //    btnEnOperacion.Checked = true;
            //}
            //else
            //{
            //    btnNoActivo.Checked = true;
            //}
            //if (oDispositivoQuery.esFinal == 1)
            //{
            //    chkEsFinal.Checked = true;
            //}
            //else
            //{
            //    chkEsFinal.Checked = false;
            //}
            //this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
            //this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
            //this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
            //this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
            //this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
            //this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
            //txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
            //txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
            //this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
            //this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";

            //this.cboTipoDeSistema.SelectedValue = this._Dispositivo.idSistemaDeImpresion != null ? this._Dispositivo.idSistemaDeImpresion.ToString().Trim() : "7";
            #endregion
            CargarObjeto();
            AccionFormulario("Edicion");

        }

        public DispositivosEdicion(string _oConexion, int _idDispositivo, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            try
            {
                InitializeComponent();
                conection = _oConexion;
                user2 = _user2;
                idDispositivo = _idDispositivo;
                companyId = _companyId;
                privilege = _privilege;
                oConexion = _oConexion;
                DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
                listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
                DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
                oDispositivo = new SAS_Dispostivo();
                oDispositivo.id = _idDispositivo;
                idDispositivo = _idDispositivo;
                dispositivo = new SAS_Dispostivo();
                dispositivo.id = _idDispositivo;

                CargarCombos();
                Inicio();
                #region 
                //_Dispositivo.id = _DispositivoQuery.id;
                //_Dispositivo.latitud = _DispositivoQuery.latitud;
                //_Dispositivo.longitud = _DispositivoQuery.longitud;
                //_Dispositivo.nombres = _DispositivoQuery.nombres;
                //_Dispositivo.dispositivo = _DispositivoQuery.dispositivo;
                //_Dispositivo.sedeCodigo = _DispositivoQuery.sedeCodigo;
                //_Dispositivo.sedeDescripcion = _DispositivoQuery.sedeDescripcion;
                //_Dispositivo.numeroSerie = _DispositivoQuery.numeroSerie;
                //_Dispositivo.caracteristicas = _DispositivoQuery.caracteristicas;
                //_Dispositivo.idestado = _DispositivoQuery.idestado;
                //_Dispositivo.tipoDispositivoCodigo = _DispositivoQuery.tipoDispositivoCodigo;
                //_Dispositivo.tipoDispositivo = _DispositivoQuery.tipoDispositivo;
                //_Dispositivo.estado = _DispositivoQuery.estado;
                //_Dispositivo.item = _DispositivoQuery.item;
                //_Dispositivo.codigoSegmentoIP = _DispositivoQuery.codigoSegmentoIP;
                //_Dispositivo.numeroIP = _DispositivoQuery.numeroIP;
                //_Dispositivo.creadoPor = _DispositivoQuery.creadoPor;
                //_Dispositivo.fechacreacion = _DispositivoQuery.fechacreacion;
                //_Dispositivo.activoCodigoERP = _DispositivoQuery.activoCodigoERP;
                //_Dispositivo.activo = _DispositivoQuery.activo;
                //_Dispositivo.IdDispostivoColor = _DispositivoQuery.IdDispostivoColor;
                //_Dispositivo.color = _DispositivoQuery.color;
                //_Dispositivo.idModelo = _DispositivoQuery.idModelo;
                //_Dispositivo.MODELO = _DispositivoQuery.MODELO;
                //_Dispositivo.idMarca = _DispositivoQuery.idMarca;
                //_Dispositivo.marca = _DispositivoQuery.marca;
                //_Dispositivo.numeroParte = _DispositivoQuery.numeroParte;
                //_Dispositivo.IdEstadoProducto = _DispositivoQuery.IdEstadoProducto;
                //_Dispositivo.estadoProducto = _DispositivoQuery.estadoProducto;
                //_Dispositivo.EsPropio = _DispositivoQuery.EsPropio;
                //_Dispositivo.AlquiladoPropio = _DispositivoQuery.AlquiladoPropio;
                //_Dispositivo.idProducto = _DispositivoQuery.idProducto;
                //_Dispositivo.producto = _DispositivoQuery.producto;
                //_Dispositivo.rutaImagen = _DispositivoQuery.rutaImagen;
                //_Dispositivo.funcionamientoCodigo = _DispositivoQuery.funcionamientoCodigo;
                //_Dispositivo.funcionamiento = _DispositivoQuery.funcionamiento;
                //_Dispositivo.idClieprov = _DispositivoQuery.idClieprov;
                //_Dispositivo.razonSocial = _DispositivoQuery.razonSocial;
                //_Dispositivo.coordenada = _DispositivoQuery.coordenada;
                //_Dispositivo.fechaActivacion = _DispositivoQuery.fechaActivacion;
                //_Dispositivo.idCobrarpagarDoc = _DispositivoQuery.idCobrarpagarDoc;
                //_Dispositivo.documentoCompra = _DispositivoQuery.documentoCompra;
                //_Dispositivo.fechaBaja = _DispositivoQuery.fechaBaja;
                //_Dispositivo.fechaProduccion = _DispositivoQuery.fechaProduccion;
                //_Dispositivo.esFinal = _DispositivoQuery.esFinal;
                //_Dispositivo.registro = _DispositivoQuery.registro;
                //_Dispositivo.RegistradoNoRegistrado = _DispositivoQuery.RegistradoNoRegistrado;
                //_Dispositivo.idarea = _DispositivoQuery.idarea;
                //_Dispositivo.area = _DispositivoQuery.area;
                //_Dispositivo.imagen = _DispositivoQuery.imagen;
                //_Dispositivo.ubicacion = _DispositivoQuery.ubicacion;
                //_Dispositivo.costoUSD = _DispositivoQuery.costoUSD;
                //_Dispositivo.lineaCelular = _DispositivoQuery.lineaCelular;
                //_Dispositivo.AniosParaDepreciar = _DispositivoQuery.AniosParaDepreciar;
                //_Dispositivo.kilovatioHora = _DispositivoQuery.kilovatioHora;
                //_Dispositivo.tipoDeFacturacionDeConsumoEnergetico = _DispositivoQuery.tipoDeFacturacionDeConsumoEnergetico;

                //listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
                //this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
                //this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
                //this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
                //this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
                //this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
                //this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
                //this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
                //this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
                //this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
                //this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
                //this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
                //this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
                //this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
                //this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
                //this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
                //this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
                //this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
                //this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

                //this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
                //this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;

                //this.txtMtoUSD.Text = this._Dispositivo.costoMantenimientoAnualUSD != (decimal?)null ? this._Dispositivo.costoMantenimientoAnualUSD.ToDecimalPresentation() : "0.00";
                //this.txtSuministroUSD.Text = this._Dispositivo.costoSuministroAnualUSD != (decimal?)null ? this._Dispositivo.costoSuministroAnualUSD.ToDecimalPresentation() : "0.00";

                //this.txtKiloVatioHora.Text = this._Dispositivo.kilovatioHora != (decimal?)null ? this._Dispositivo.kilovatioHora.ToDecimalPresentation() : "0.00";
                //cboFacturaciónDeConsumoEnergético.SelectedValue = this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico != null ? this._Dispositivo.tipoDeFacturacionDeConsumoEnergetico.ToString().Trim() : "0";


                //this.txtUbicacion.Text = this._Dispositivo.ubicacion != null ? this._Dispositivo.ubicacion.Trim() : string.Empty;
                //this.txtCostoUSD.Text = this._Dispositivo.costoUSD != (decimal?)null ? this._Dispositivo.costoUSD.ToDecimalPresentation() : "0.0";
                //this.txtLineaCelularCodigo.Text = this._Dispositivo.lineaCelular != null ? this._Dispositivo.lineaCelular.Trim() : string.Empty;
                //this.txtAnioDepreciar.Value = this._Dispositivo.AniosParaDepreciar != (decimal?)null ? this._Dispositivo.AniosParaDepreciar : 0;


                //this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
                //this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;
                //if (_Dispositivo.EsPropio == 1)
                //{
                //    rbtPropio.Checked = true;
                //}
                //else
                //{
                //    if (_Dispositivo.EsPropio == 2)
                //    {

                //        rbtInvitado.Checked = true;
                //    }
                //    else
                //    {
                //        rbtAlquilado.Checked = true;
                //    }
                //}






                //if (_Dispositivo.funcionamientoCodigo == 1)
                //{
                //    btnEnOperacion.Checked = true;
                //}
                //else
                //{
                //    btnNoActivo.Checked = true;
                //}
                //if (_Dispositivo.esFinal == 1)
                //{
                //    chkEsFinal.Checked = true;
                //}
                //else
                //{
                //    chkEsFinal.Checked = false;
                //}
                //this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
                //this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
                //this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
                //this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
                //this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
                //this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
                //txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
                //txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
                //this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
                //this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";

                //this.cboTipoDeSistema.SelectedValue = this._Dispositivo.idSistemaDeImpresion != null ? this._Dispositivo.idSistemaDeImpresion.ToString().Trim() : "7";
                #endregion
                CargarObjeto();
                AccionFormulario("Edicion");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        // SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege
        private void AccionFormulario(string accion)
        {
            if (accion == "Edicion")
            {
                btnEditar.Enabled = false;
                btnAtras.Enabled = true;
                btnExportToExcel.Enabled = true;
                btnGrabar.Enabled = true;
                gbDetalles.Enabled = true;
                gbDispositivo.Enabled = true;
            }
            if (accion == "Registrar")
            {
                btnEditar.Enabled = !false;
                btnAtras.Enabled = !true;
                btnExportToExcel.Enabled = !true;
                btnGrabar.Enabled = !true;
                gbDetalles.Enabled = !true;
                gbDispositivo.Enabled = !true;
            }
            if (accion == "Nuevo")
            {
                LimpiarFormulario();
                btnEditar.Enabled = !false;
                btnAtras.Enabled = !true;
                btnExportToExcel.Enabled = !true;
                btnGrabar.Enabled = !true;
                gbDetalles.Enabled = !true;
                gbDispositivo.Enabled = !true;
            }

        }

        private void LimpiarFormulario()
        {

        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                sedes = new List<Grupo>();
                condicionesProductos = new List<Grupo>();
                workAreas = new List<Grupo>();

                condicionesProductos = comboHelper.GettipoDeFacturacionDeConsumoEnergetico("SAS");
                cboFacturaciónDeConsumoEnergético.DisplayMember = "Descripcion";
                cboFacturaciónDeConsumoEnergético.ValueMember = "Codigo";
                cboFacturaciónDeConsumoEnergético.DataSource = condicionesProductos.ToList();


                condicionesProductos = comboHelper.GetComboBoxTerms("SAS");
                cboCondicion.DisplayMember = "Descripcion";
                cboCondicion.ValueMember = "Codigo";
                cboCondicion.DataSource = condicionesProductos.ToList();

                sedes = comboHelper.GetComboBoxSedes("SAS");
                cboSede.DisplayMember = "Descripcion";
                cboSede.ValueMember = "Codigo";
                cboSede.DataSource = sedes.ToList();

                condicionesProductos = comboHelper.GetComboBoxTypeOfDevices("SAS");
                cboTipoDispositivo.DisplayMember = "Descripcion";
                cboTipoDispositivo.ValueMember = "Codigo";
                cboTipoDispositivo.DataSource = condicionesProductos.OrderBy(x => x.Descripcion).ToList();
                cboTipoDispositivo.SelectedValue = "1";


                workAreas = comboHelper.TypeOfWorkAreas("SAS");
                cboArea.DisplayMember = "Descripcion";
                cboArea.ValueMember = "Codigo";
                cboArea.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();

                workAreas = comboHelper.TypeOfWorkAreas("SAS");
                cboArea.DisplayMember = "Descripcion";
                cboArea.ValueMember = "Codigo";
                cboArea.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();


                workAreas = comboHelper.ListadoDeTipoDesistemaPorCodigoTipoDispositivo("SAS", "000");
                cboTipoDeSistema.DisplayMember = "Descripcion";
                cboTipoDeSistema.ValueMember = "Codigo";
                cboTipoDeSistema.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();



            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensajes del sistema");
                return;
            }
        }

        private void DispositivosEdicion_Load(object sender, EventArgs e)
        {

        }

        private void MostrarQr()
        {
            QrEncoder Codificador = new QrEncoder(ErrorCorrectionLevel.H);

            // crear un codigo QR
            QrCode Codigo = new QrCode();

            // generar generar  un codigo apartir de datos, y pasar el codigo por referencia
            Codificador.TryEncode(txtCodigo.Text, out Codigo);

            // generar un graficador 
            GraphicsRenderer Renderisado = new GraphicsRenderer(new FixedCodeSize(200, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            // generar un flujo de datos 
            MemoryStream ms = new MemoryStream();

            // escribir datos en el renderizado
            Renderisado.WriteToStream(Codigo.Matrix, ImageFormat.Png, ms);

            // generar controles para ponerlos en el form
            var ImagenQR = new Bitmap(ms);
            var ImgenSalida = new Bitmap(ImagenQR, new Size(PanelResultado.Width, PanelResultado.Height));

            // asignar la imagen al panel 
            PanelResultado.BackgroundImage = ImgenSalida;

            MemoryStream straem = new MemoryStream();
            //PanelResultado.Image.Save(straem, System.Drawing.Imaging.ImageFormat.Jpeg)
            pic = ms.ToArray();


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

        protected override void OnLoad(EventArgs e)
        {
            //this.dgvDispositivo.TableElement.BeginUpdate();


            //this.LoadFreightSummary();
            //this.dgvDispositivo.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            //this.dgvDispositivo.MasterTemplate.AutoExpandGroups = true;
            //this.dgvDispositivo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            //this.dgvDispositivo.GroupDescriptors.Clear();
            //this.dgvDispositivo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            //GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            //items1.Add(new GridViewSummaryItem("chnombres", "Count : {0:N2}; ", GridAggregateFunction.Count));
            //this.dgvDispositivo.MasterTemplate.SummaryRowsTop.Add(items1);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Registar();
        }

        private void Registar()
        {
            try
            {
                gbDispositivo.Enabled = false;
                gbDetalles.Enabled = false;
                pbDispositivo.Visible = true;

                #region Registrar () 
                dispositivo = new SAS_Dispostivo();
                modelo = new SAS_DispostivoController();
                //              SAS_Dispostivo oDevice = new SAS_Dispostivo();
                //                Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                dispositivo.nombres = this.txtNombre.Text.Trim();
                dispositivo.descripcion = this.txtDescripcion.Text.Trim();
                dispositivo.sedeCodigo = this.cboSede.SelectedValue.ToString().Trim();
                dispositivo.latitud = this.txtLatitud.Text.Trim();
                dispositivo.longitud = this.txtLongitud.Text.Trim();
                dispositivo.numeroSerie = this.txtNumeroSerie.Text.Trim();
                dispositivo.caracteristicas = this.txtCaracterísticas.Text.Trim();
                dispositivo.activoCodigoERP = this.txtActivoCodigo.Text.Trim();
                dispositivo.tipoDispositivoCodigo = this.cboTipoDispositivo.SelectedValue.ToString().Trim();
                dispositivo.idArea = this.cboArea.SelectedValue.ToString().Trim();

                dispositivo.tipoDeFacturacionDeConsumoEnergético = Convert.ToChar(this.cboFacturaciónDeConsumoEnergético.SelectedValue.ToString().Trim());
                dispositivo.kilovatioHora = this.txtKiloVatioHora.Text != string.Empty ? Convert.ToDecimal(this.txtKiloVatioHora.Text) : 0;

                dispositivo.idSistemaDeImpresion = this.cboTipoDeSistema.SelectedValue != null ? Convert.ToInt32(this.cboTipoDeSistema.SelectedValue.ToString().Trim()) : 7;
                dispositivo.imagen = pic;

                dispositivo.AnioParaDepreciar = this.txtAnioDepreciar.Value;
                dispositivo.lineaCelular = this.txtLineaCelularCodigo.Text.Trim();
                dispositivo.costoUSD = this.txtCostoUSD.Text != string.Empty ? Convert.ToDecimal(this.txtCostoUSD.Text) : 0;
                dispositivo.ubicacion = this.txtUbicacion.Text;

                dispositivo.costoMantenimientoAnualUSD = this.txtMtoUSD.Text != string.Empty ? Convert.ToDecimal(this.txtMtoUSD.Text) : 0;
                dispositivo.costoSuministroAnualUSD = this.txtSuministroUSD.Text != string.Empty ? Convert.ToDecimal(this.txtSuministroUSD.Text) : 0;



                dispositivo.IdDispostivoColor = this.txtColorCodigo.Text.ToString().Trim();
                dispositivo.idModelo = this.txtModeloCodigo.Text.ToString().Trim();
                dispositivo.idMarca = this.txtMarcaCodigo.Text.ToString().Trim();
                dispositivo.numeroParte = this.txtNroParte.Text.ToString().Trim();
                dispositivo.IdEstadoProducto = this.cboCondicion.SelectedValue != null ? Convert.ToChar(this.cboCondicion.SelectedValue.ToString().Trim()) : Convert.ToChar(1);
                if (rbtPropio.Checked == true)
                {
                    dispositivo.EsPropio = 1;
                }
                else
                {
                    if (rbtAlquilado.Checked == true)
                    {
                        dispositivo.EsPropio = 0;
                    }
                    else
                    {
                        dispositivo.EsPropio = 2;
                    }

                }


                dispositivo.idProducto = this.txtProductoCodigo.Text.ToString().Trim();
                dispositivo.rutaImagen = string.Empty;
                if (btnEnOperacion.Checked == true)
                {
                    dispositivo.funcionamiento = 1;
                }
                else
                {
                    dispositivo.funcionamiento = 0;
                }
                dispositivo.idClieprov = this.txtProveedorCodigo.Text.ToString().Trim();
                dispositivo.coordenada = this.txtCoordenada.Text.ToString().Trim();
                dispositivo.idCobrarpagarDoc = this.txtDocCompraCodigo.Text.ToString().Trim();

                string ASCD = this.txtValidar.Text.ToString().Trim();

                if (this.txtFechaBaja.Text.ToString().Trim() != ASCD)
                {
                    if (this.txtFechaBaja.Text != null)
                    {
                        if (this.txtFechaBaja.Text.ToString().Trim() != string.Empty)
                        {
                            dispositivo.fechaBaja = this.txtFechaBaja.Text.ToString().Trim() != "" ? Convert.ToDateTime(this.txtFechaBaja.Text.ToString().Trim()) : (DateTime?)null;
                        }
                    }
                }

                if (this.txtFechaProduccion.Text.ToString().Trim() != ASCD)
                {
                    if (this.txtFechaProduccion.Text != null)
                    {
                        if (this.txtFechaProduccion.Text.ToString().Trim() != string.Empty)
                        {
                            dispositivo.fechaProduccion = this.txtFechaProduccion.Text.ToString().Trim() != "" ? Convert.ToDateTime(this.txtFechaProduccion.Text.ToString().Trim()) : (DateTime?)null;
                        }
                    }
                }

                if (this.txtFechaActivacion.Text.ToString().Trim() != ASCD)
                {
                    if (this.txtFechaActivacion.Text != null)
                    {
                        if (this.txtFechaActivacion.Text.ToString().Trim() != string.Empty)
                        {
                            dispositivo.fechaActivacion = this.txtFechaActivacion.Text.ToString().Trim() != "" ? Convert.ToDateTime(this.txtFechaActivacion.Text.ToString().Trim()) : (DateTime?)null;
                        }
                    }
                }

                // dispositivo.fechaBaja = (this.txtFechaBaja.Text.ToString().Trim() != ASCD | this.txtFechaBaja.Text.ToString().Trim() != "") ? Convert.ToDateTime(this.txtFechaBaja.Text.ToString().Trim()) : (DateTime?)null;
                //dispositivo.fechaProduccion = (this.txtFechaProduccion.Text.ToString().Trim() != ASCD || this.txtFechaProduccion.Text.ToString().Trim() != string.Empty) ? Convert.ToDateTime(this.txtFechaProduccion.Text.ToString().Trim()) : (DateTime?)null;
                //  dispositivo.fechaActivacion = (this.txtFechaActivacion.Text.ToString().Trim() !=  ASCD || this.txtFechaActivacion.Text.ToString().Trim() != string.Empty) ? Convert.ToDateTime(this.txtFechaActivacion.Text.ToString().Trim()) : (DateTime?)null;

                if (chkEsFinal.Checked == true)
                {
                    dispositivo.esFinal = 1;
                }
                else
                {
                    dispositivo.esFinal = 0;
                }

                #region Obtener Colaboradores()
                listadoColaboradores = new List<SAS_DispositivoUsuarios>();
                if (this.dgvColaborador != null)
                {
                    if (this.dgvColaborador.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvColaborador.Rows)
                        {
                            if (fila.Cells["chdispositivoCodigoColaborador"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoUsuarios oColaborador = new SAS_DispositivoUsuarios();
                                    oColaborador.dispositivoCodigo = fila.Cells["chdispositivoCodigoColaborador"].Value != null ? Convert.ToInt32(fila.Cells["chdispositivoCodigoColaborador"].Value.ToString().Trim()) : 0;
                                    oColaborador.item = fila.Cells["chItemColaborador"].Value != null ? fila.Cells["chItemColaborador"].Value.ToString().Trim() : string.Empty;
                                    oColaborador.estado = fila.Cells["chestadoItemColaborador"].Value != null ? Convert.ToByte(fila.Cells["chestadoItemColaborador"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    oColaborador.idcodigoGeneral = fila.Cells["chidCodigoGeneral"].Value != null ? fila.Cells["chidCodigoGeneral"].Value.ToString().Trim() : string.Empty;
                                    oColaborador.desde = fila.Cells["chDesdeColaborador"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeColaborador"].Value.ToString().Trim()) : (DateTime?)null;
                                    oColaborador.hasta = fila.Cells["chHastaColaborador"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaColaborador"].Value.ToString().Trim()) : (DateTime?)null;
                                    oColaborador.observacion = fila.Cells["chObservacionColaborador"].Value != null ? fila.Cells["chObservacionColaborador"].Value.ToString().Trim() : "";
                                    oColaborador.fechaCreacion = DateTime.Now;
                                    oColaborador.registradoPor = Environment.UserName.ToString();
                                    oColaborador.tipo = fila.Cells["chTipoColaborador"].Value != null ? Convert.ToChar(fila.Cells["chTipoColaborador"].Value.ToString().Trim()) : Convert.ToChar('P');
                                    oColaborador.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesColaborador"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesColaborador"].Value.ToString().Trim()) : Convert.ToInt32('1');


                                    #endregion
                                    listadoColaboradores.Add(oColaborador);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Obtener Ips
                listadoNumeroIp = new List<SAS_DispositivoIP>();
                if (this.dgvDetalleIP != null)
                {
                    if (this.dgvDetalleIP.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDetalleIP.Rows)
                        {
                            if (fila.Cells["chdispositivoCodigoIP"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoIP oNumeroIP = new SAS_DispositivoIP();
                                    oNumeroIP.dispositivoCodigo = fila.Cells["chdispositivoCodigoIP"].Value != null ? Convert.ToInt32(fila.Cells["chdispositivoCodigoIP"].Value.ToString().Trim()) : 0;
                                    oNumeroIP.item = fila.Cells["chItemIP"].Value != null ? fila.Cells["chItemIP"].Value.ToString().Trim() : string.Empty;
                                    oNumeroIP.estado = fila.Cells["chEstadoIdIP"].Value != null ? Convert.ToByte(fila.Cells["chEstadoIdIP"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    oNumeroIP.direcionMAC = fila.Cells["chdirecionMAC"].Value != null ? fila.Cells["chdirecionMAC"].Value.ToString().Trim() : string.Empty;
                                    oNumeroIP.desde = fila.Cells["chFechaInicioIP"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaInicioIP"].Value.ToString().Trim()) : (DateTime?)null;
                                    oNumeroIP.hasta = fila.Cells["chFechaTerminoIP"].Value != null ? Convert.ToDateTime(fila.Cells["chFechaTerminoIP"].Value.ToString().Trim()) : (DateTime?)null;
                                    oNumeroIP.observacion = fila.Cells["chObservacionIP"].Value != null ? fila.Cells["chObservacionIP"].Value.ToString().Trim() : "";
                                    oNumeroIP.fechaCreacion = DateTime.Now;
                                    oNumeroIP.registradoPor = Environment.UserName.ToString();
                                    oNumeroIP.tipo = fila.Cells["chTipo"].Value != null ? Convert.ToChar(fila.Cells["chTipo"].Value.ToString().Trim()) : Convert.ToChar('F');
                                    oNumeroIP.idIP = fila.Cells["chidIP"].Value != null ? Convert.ToInt32(fila.Cells["chidIP"].Value.ToString().Trim()) : 0;
                                    #endregion
                                    listadoNumeroIp.Add(oNumeroIP);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Obtener Hardware()
                listadoHardware = new List<SAS_DispositivoHardware>();
                if (this.dgvHardware != null)
                {
                    if (this.dgvHardware.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvHardware.Rows)
                        {
                            if (fila.Cells["chcodigoDispositivoHW"].Value.ToString().Trim() != String.Empty)
                            {
                                if (fila.Cells["chitemHW"].Value.ToString().Trim() != String.Empty)
                                {
                                    try
                                    {
                                        #region Obtener detalle por linea detalle() 
                                        SAS_DispositivoHardware oItem = new SAS_DispositivoHardware();
                                        oItem.codigoDispositivo = fila.Cells["chcodigoDispositivoHW"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoDispositivoHW"].Value.ToString().Trim()) : 0;
                                        oItem.item = fila.Cells["chitemHW"].Value != null ? fila.Cells["chitemHW"].Value.ToString().Trim() : string.Empty;
                                        oItem.codigoHardware = fila.Cells["chcodigoHardware"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoHardware"].Value) : Convert.ToInt32(0);
                                        oItem.serie = fila.Cells["chserieHW"].Value != null ? fila.Cells["chserieHW"].Value.ToString().Trim() : string.Empty;
                                        oItem.capacidad = fila.Cells["chcapacidadHW"].Value != null ? Convert.ToDecimal(fila.Cells["chcapacidadHW"].Value.ToString().Trim()) : Convert.ToDecimal(0);
                                        oItem.unidadMedidaCapacidad = fila.Cells["chunidadMedidaCapacidad"].Value != null ? fila.Cells["chunidadMedidaCapacidad"].Value.ToString().Trim() : string.Empty;
                                        oItem.numeroParte = fila.Cells["chnumeroParteHW"].Value != null ? fila.Cells["chnumeroParteHW"].Value.ToString().Trim() : string.Empty;
                                        oItem.observacion = fila.Cells["chobservacionHW"].Value != null ? fila.Cells["chobservacionHW"].Value.ToString().Trim() : string.Empty;
                                        oItem.desde = fila.Cells["chdesdeHW"].Value != null ? Convert.ToDateTime(fila.Cells["chdesdeHW"].Value.ToString().Trim()) : (DateTime?)null;
                                        oItem.hasta = fila.Cells["chhastaHW"].Value != null ? Convert.ToDateTime(fila.Cells["chhastaHW"].Value.ToString().Trim()) : (DateTime?)null;
                                        oItem.estado = fila.Cells["chidestadoHW"].Value != null ? Convert.ToByte(fila.Cells["chidestadoHW"].Value.ToString().Trim()) : Convert.ToByte(0);
                                        oItem.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesHW"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesHW"].Value.ToString().Trim()) : Convert.ToInt32(1);

                                        #endregion
                                        listadoHardware.Add(oItem);
                                    }
                                    catch (Exception Ex)
                                    {
                                        MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                        return;
                                    }
                                }


                            }
                        }

                    }
                }


                #endregion

                #region Obtener Software()
                listadoSoftware = new List<SAS_DispositivoSoftware>();
                if (this.dgvSoftware != null)
                {
                    if (this.dgvSoftware.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvSoftware.Rows)
                        {
                            if (fila.Cells["chcodigoDispositivoSW"].Value.ToString().Trim() != String.Empty)
                            {
                                if (fila.Cells["chitemSW"].Value.ToString().Trim() != String.Empty)
                                {
                                    try
                                    {
                                        #region Obtener detalle por linea detalle() 
                                        SAS_DispositivoSoftware oItem = new SAS_DispositivoSoftware();
                                        oItem.codigoDispositivo = fila.Cells["chcodigoDispositivoSW"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoDispositivoSW"].Value.ToString().Trim()) : 0;
                                        oItem.item = fila.Cells["chitemSW"].Value != null ? fila.Cells["chitemSW"].Value.ToString().Trim() : string.Empty;
                                        oItem.codigoSoftware = fila.Cells["chcodigoSoftware"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoSoftware"].Value) : Convert.ToInt32(0);
                                        oItem.serie = fila.Cells["chserieSW"].Value != null ? fila.Cells["chserieSW"].Value.ToString().Trim() : string.Empty;
                                        oItem.version = fila.Cells["chversionSW"].Value != null ? fila.Cells["chversionSW"].Value.ToString().Trim() : string.Empty;
                                        oItem.informacionAdicional = fila.Cells["chinformacionAdicional"].Value != null ? fila.Cells["chinformacionAdicional"].Value.ToString().Trim() : string.Empty;
                                        oItem.numeroParte = fila.Cells["chnumeroParte"].Value != null ? fila.Cells["chnumeroParte"].Value.ToString().Trim() : string.Empty;
                                        oItem.observacion = fila.Cells["chObservacionSW"].Value != null ? fila.Cells["chObservacionSW"].Value.ToString().Trim() : string.Empty;
                                        oItem.desde = fila.Cells["chdesdeSW"].Value != null ? Convert.ToDateTime(fila.Cells["chdesdeSW"].Value.ToString().Trim()) : (DateTime?)null;
                                        oItem.hasta = fila.Cells["chhastaSW"].Value != null ? Convert.ToDateTime(fila.Cells["chhastaSW"].Value.ToString().Trim()) : (DateTime?)null;
                                        oItem.estado = fila.Cells["chidestadoSW"].Value != null ? Convert.ToByte(fila.Cells["chidestadoSW"].Value.ToString().Trim()) : Convert.ToByte(0);
                                        oItem.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesSW"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesSW"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                        #endregion
                                        listadoSoftware.Add(oItem);
                                    }
                                    catch (Exception Ex)
                                    {
                                        MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Componente()
                listadoComponentes = new List<SAS_DispositivoComponentes>();
                if (this.dgvComponente != null)
                {
                    if (this.dgvComponente.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvComponente.Rows)
                        {
                            if (fila.Cells["chdispositivoCodigoComponente"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoComponentes odetalle = new SAS_DispositivoComponentes();
                                    odetalle.codigoDispositivo = fila.Cells["chdispositivoCodigoComponente"].Value != null ? Convert.ToInt32(fila.Cells["chdispositivoCodigoComponente"].Value.ToString().Trim()) : 0;
                                    odetalle.item = fila.Cells["chItemComponente"].Value != null ? fila.Cells["chItemComponente"].Value.ToString().Trim() : string.Empty;
                                    odetalle.codigoDispositivoComponente = fila.Cells["chCodigoComponente"].Value != null ? Convert.ToInt32(fila.Cells["chCodigoComponente"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    odetalle.observacion = fila.Cells["chObservacionComponente"].Value != null ? fila.Cells["chObservacionComponente"].Value.ToString().Trim() : "";
                                    odetalle.desde = fila.Cells["chDesdeComponente"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeComponente"].Value.ToString().Trim()) : (DateTime?)null;
                                    odetalle.hasta = fila.Cells["chHastaComponente"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaComponente"].Value.ToString().Trim()) : (DateTime?)null;
                                    odetalle.estado = fila.Cells["chIdEstadoComponente"].Value != null ? Convert.ToInt32(fila.Cells["chIdEstadoComponente"].Value.ToString().Trim()) : 0;
                                    odetalle.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesComponente"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesComponente"].Value.ToString().Trim()) : 0;
                                    #endregion
                                    listadoComponentes.Add(odetalle);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Cuentas de usuario() 
                listadoCuentasUsuarios = new List<SAS_DispositivoCuentaUsuarios>();
                if (this.dgvCuentaDeUsuario != null)
                {
                    if (this.dgvCuentaDeUsuario.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvCuentaDeUsuario.Rows)
                        {
                            if (fila.Cells["chcodigoDispositivoCuentaUsuario"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoCuentaUsuarios detalle = new SAS_DispositivoCuentaUsuarios();
                                    detalle.codigoDispositivo = fila.Cells["chcodigoDispositivoCuentaUsuario"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoDispositivoCuentaUsuario"].Value.ToString().Trim()) : 0;
                                    detalle.codigoTipoCuenta = fila.Cells["chcodigoTipoCuenta"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoTipoCuenta"].Value.ToString().Trim()) : 0;
                                    detalle.item = fila.Cells["chitemCuentaUsuario"].Value != null ? fila.Cells["chitemCuentaUsuario"].Value.ToString().Trim() : string.Empty;
                                    detalle.observacion = fila.Cells["chObservacionCuentaUsuario"].Value != null ? fila.Cells["chObservacionCuentaUsuario"].Value.ToString().Trim() : "";
                                    detalle.desde = fila.Cells["chDesdeCuentaUsuario"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeCuentaUsuario"].Value.ToString().Trim()) : (DateTime?)null;
                                    detalle.hasta = fila.Cells["chHastaCuentaUsuario"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaCuentaUsuario"].Value.ToString().Trim()) : (DateTime?)null;
                                    detalle.estado = fila.Cells["chidestadoCuentaUsuario"].Value != null ? Convert.ToByte(fila.Cells["chidestadoCuentaUsuario"].Value.ToString().Trim()) : Convert.ToByte(0);
                                    detalle.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesCuentaUsuario"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesCuentaUsuario"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    detalle.clave = fila.Cells["chClave"].Value != null ? fila.Cells["chClave"].Value.ToString().Trim() : string.Empty;
                                    detalle.correoDeRecuperacion = fila.Cells["chCorreoderecuperacion"].Value != null ? fila.Cells["chCorreoderecuperacion"].Value.ToString().Trim() : string.Empty;
                                    detalle.NumeroTelefonoRecuperacion = fila.Cells["chNumeroDeRecuperacion"].Value != null ? fila.Cells["chNumeroDeRecuperacion"].Value.ToString().Trim() : string.Empty;
                                    detalle.cuenta = fila.Cells["chCuenta"].Value != null ? fila.Cells["chCuenta"].Value.ToString().Trim() : string.Empty;

                                    #endregion
                                    listadoCuentasUsuarios.Add(detalle);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Documentos() 
                listadoDocumentos = new List<SAS_DispositivoDocumento>();
                if (this.dgvDocumento != null)
                {
                    if (this.dgvDocumento.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvDocumento.Rows)
                        {
                            if (fila.Cells["chdispositivoCodigoDocumento"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoDocumento detalle = new SAS_DispositivoDocumento();
                                    detalle.codigoDispositivo = fila.Cells["chdispositivoCodigoDocumento"].Value != null ? Convert.ToInt32(fila.Cells["chdispositivoCodigoDocumento"].Value.ToString().Trim()) : 0;
                                    detalle.codigoTipoDocumento = fila.Cells["chcodigoTipoDocumentoDocumento"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoTipoDocumentoDocumento"].Value) : 0;
                                    detalle.item = fila.Cells["chItemDocumento"].Value != null ? fila.Cells["chItemDocumento"].Value.ToString().Trim() : string.Empty;
                                    detalle.observacion = fila.Cells["chObservacionDocumento"].Value != null ? fila.Cells["chObservacionDocumento"].Value.ToString().Trim() : "";
                                    detalle.desde = fila.Cells["chDesdeDocumento"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeDocumento"].Value.ToString().Trim()) : (DateTime?)null;
                                    detalle.hasta = fila.Cells["chHastaDocumento"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaDocumento"].Value.ToString().Trim()) : (DateTime?)null;
                                    detalle.estado = fila.Cells["chIdEstadoDocumento"].Value != null ? Convert.ToByte(fila.Cells["chIdEstadoDocumento"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    detalle.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesDocumento"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesDocumento"].Value.ToString().Trim()) : Convert.ToInt32(1);
                                    detalle.link = fila.Cells["chLink"].Value != null ? fila.Cells["chLink"].Value.ToString().Trim() : string.Empty;
                                    detalle.cargoFijo = fila.Cells["chcargoFijo"].Value != null ? Convert.ToDecimal(fila.Cells["chcargoFijo"].Value) : 0;
                                    detalle.cargoVariable = fila.Cells["chcargoVariable"].Value != null ? Convert.ToDecimal(fila.Cells["chcargoVariable"].Value) : 0;
                                    detalle.idMoneda = fila.Cells["chidMoneda"].Value != null ? fila.Cells["chidMoneda"].Value.ToString().Trim() : string.Empty;
                                    detalle.idMedida = fila.Cells["chidMedida"].Value != null ? fila.Cells["chidMedida"].Value.ToString().Trim() : string.Empty;
                                    detalle.cantidadContratada = fila.Cells["chcantidadContratada"].Value != null ? Convert.ToDecimal(fila.Cells["chcantidadContratada"].Value) : 0;
                                    detalle.frecuenciaDeFacturacion = fila.Cells["chfrecuenciaDeFacturacion"].Value != null ? fila.Cells["chfrecuenciaDeFacturacion"].Value.ToString().Trim() : string.Empty;
                                    #endregion
                                    listadoDocumentos.Add(detalle);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion


                // ad 14.04.2022
                #region Contadpres() 
                listadoContadores = new List<SAS_DispositivoContadores>();
                if (this.dgvContadores != null)
                {
                    if (this.dgvContadores.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvContadores.Rows)
                        {
                            if (fila.Cells["chcodigoDispositivoContador"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                    recordObject.codigoDispositivo = fila.Cells["chcodigoDispositivoContador"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoDispositivoContador"].Value.ToString().Trim()) : 0;
                                    recordObject.item = fila.Cells["chItemContador"].Value != null ? fila.Cells["chItemContador"].Value.ToString().Trim() : string.Empty;
                                    recordObject.periodo = fila.Cells["chPeriodo"].Value != null ? fila.Cells["chPeriodo"].Value.ToString().Trim() : string.Empty;
                                    recordObject.cantidad = fila.Cells["chCantidad"].Value != null ? Convert.ToDecimal(fila.Cells["chCantidad"].Value.ToString().Trim()) : Convert.ToDecimal(0);

                                    recordObject.contadorInicial = fila.Cells["chcontadorInicial"].Value != null ? Convert.ToDecimal(fila.Cells["chcontadorInicial"].Value.ToString().Trim()) : Convert.ToDecimal(0);
                                    recordObject.contadorFinal = fila.Cells["chcontadorFinal"].Value != null ? Convert.ToDecimal(fila.Cells["chcontadorFinal"].Value.ToString().Trim()) : Convert.ToDecimal(0);
                                    recordObject.itemContrato = fila.Cells["chitemContrato"].Value != null ? fila.Cells["chitemContrato"].Value.ToString().Trim() : "000";

                                    recordObject.IdMedida = fila.Cells["chIdMedidaContadores"].Value != null ? fila.Cells["chIdMedidaContadores"].Value.ToString().Trim() : string.Empty;
                                    recordObject.observacion = fila.Cells["chObservacionContador"].Value != null ? fila.Cells["chObservacionContador"].Value.ToString().Trim() : string.Empty;
                                    recordObject.desde = fila.Cells["chDesdeContadores"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeContadores"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.hasta = fila.Cells["chHastaContadores"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaContadores"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.estado = fila.Cells["chidestadoContadores"].Value != null ? Convert.ToInt32(fila.Cells["chidestadoContadores"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesContadores"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesContadores"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.usuario = fila.Cells["chIdusuarioContadores"].Value != null ? fila.Cells["chIdusuarioContadores"].Value.ToString().Trim() : string.Empty;
                                    #endregion
                                    listadoContadores.Add(recordObject);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Mantenimiento() 
                listadoMantenimientos = new List<SAS_DispositivoMovimientoMantenimientos>();
                if (this.dgvMantenimiento != null)
                {
                    if (this.dgvMantenimiento.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvMantenimiento.Rows)
                        {
                            if (fila.Cells["chcodigoDispositivoMantenimiento"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                    recordObject.codigoDispositivo = fila.Cells["chcodigoDispositivoMantenimiento"].Value != null ? Convert.ToInt32(fila.Cells["chcodigoDispositivoMantenimiento"].Value.ToString().Trim()) : 0;
                                    recordObject.item = fila.Cells["chItemMantenimiento"].Value != null ? fila.Cells["chItemMantenimiento"].Value.ToString().Trim() : string.Empty;
                                    recordObject.codigoTipoManteniento = fila.Cells["chcodigoTipoMantenimiento"].Value != null ? fila.Cells["chcodigoTipoMantenimiento"].Value.ToString().Trim() : string.Empty;
                                    recordObject.codigoColaborador = fila.Cells["chCodigoColaborador"].Value != null ? Convert.ToString(fila.Cells["chCodigoColaborador"].Value.ToString().Trim()) : string.Empty;
                                    recordObject.codigoOrdenTrabajo = fila.Cells["chCodigoOrdenTrabajo"].Value != null ? Convert.ToInt32(fila.Cells["chCodigoOrdenTrabajo"].Value) : 0;
                                    recordObject.observacion = fila.Cells["chObservacionMantenimiento"].Value != null ? fila.Cells["chObservacionMantenimiento"].Value.ToString().Trim() : string.Empty;
                                    recordObject.desde = fila.Cells["chdesdeMantenimiento"].Value != null ? Convert.ToDateTime(fila.Cells["chdesdeMantenimiento"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.hasta = fila.Cells["chhastaMantenimiento"].Value != null ? Convert.ToDateTime(fila.Cells["chhastaMantenimiento"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.estado = fila.Cells["chidestadoMantenimiento"].Value != null ? Convert.ToInt32(fila.Cells["chidestadoMantenimiento"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesMantenimiento"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesMantenimiento"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.usuario = fila.Cells["chUsuarioAsignado"].Value != null ? fila.Cells["chUsuarioAsignado"].Value.ToString().Trim() : string.Empty;
                                    #endregion
                                    listadoMantenimientos.Add(recordObject);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                #region Mov. Almacén() 
                listadoMovimientoAlmacen = new List<SAS_DispositivoMovimientoAlmacen>();
                if (this.dgvMovimientoAlmacen != null)
                {
                    if (this.dgvMovimientoAlmacen.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow fila in this.dgvMovimientoAlmacen.Rows)
                        {
                            if (fila.Cells["codigoDispositivoMovAlmacen"].Value.ToString().Trim() != String.Empty)
                            {
                                try
                                {
                                    #region Obtener detalle por linea detalle() 
                                    SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                    recordObject.codigoDispositivo = fila.Cells["codigoDispositivoMovAlmacen"].Value != null ? Convert.ToInt32(fila.Cells["codigoDispositivoMovAlmacen"].Value.ToString().Trim()) : 0;
                                    recordObject.item = fila.Cells["chItemMovAlmacen"].Value != null ? fila.Cells["chItemMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    recordObject.idMovimientoAlmacen = fila.Cells["chidMovAlmacen"].Value != null ? fila.Cells["chidMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    recordObject.cantidad = fila.Cells["chidMovAlmacen"].Value != null ? Convert.ToDecimal(fila.Cells["chidMovAlmacen"].Value.ToString().Trim()) : Convert.ToDecimal(0);
                                    recordObject.itemDocAlmacen = fila.Cells["chItemDocMovAlmacen"].Value != null ? fila.Cells["chItemDocMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    recordObject.idproducto = fila.Cells["idProductoMovAlmacen"].Value != null ? fila.Cells["idProductoMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    recordObject.observacion = fila.Cells["chObservacionMovAlmacen"].Value != null ? fila.Cells["chObservacionMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    recordObject.desde = fila.Cells["chDesdeMovAlmacen"].Value != null ? Convert.ToDateTime(fila.Cells["chDesdeMovAlmacen"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.hasta = fila.Cells["chHastaMovAlmacen"].Value != null ? Convert.ToDateTime(fila.Cells["chHastaMovAlmacen"].Value.ToString().Trim()) : (DateTime?)null;
                                    recordObject.estado = fila.Cells["chidestadoMovAlmacen"].Value != null ? Convert.ToInt32(fila.Cells["chidestadoMovAlmacen"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.seVisualizaEnReportes = fila.Cells["chseVisualizaEnReportesMovAlmacen"].Value != null ? Convert.ToInt32(fila.Cells["chseVisualizaEnReportesMovAlmacen"].Value.ToString().Trim()) : Convert.ToInt32(0);
                                    recordObject.usuario = fila.Cells["chusuarioMovAlmacen"].Value != null ? fila.Cells["chusuarioMovAlmacen"].Value.ToString().Trim() : string.Empty;
                                    #endregion
                                    listadoMovimientoAlmacen.Add(recordObject);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                                    return;
                                }

                            }
                        }

                    }
                }


                #endregion

                //rutaImagen
                //int resultadoAccion = modelo.Register("SAS", dispositivo);
                //int resultadoAccion = modelo.Register("SAS", dispositivo, listadoNumeroIpEliminados, listadoNumeroIp, listadoColaboradoresEliminados, listadoColaboradores, listadoHardwareEliminados, listadoHardware, listadoSoftwareEliminados, listadoSoftware);
                //MessageBox.Show("Se ha registrado exitosamente el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                bgwRegistrar.RunWorkerAsync();
                #endregion
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

            if (this.txtCodigo.Text != null)
            {

            }

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text != null)
            {
                #region Anuar () 
                dispositivo = new SAS_Dispostivo();
                modelo = new SAS_DispostivoController();
                SAS_Dispostivo Dispositivo = new SAS_Dispostivo();
                Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                int resultadoAccion = modelo.Unregister("SAS", Dispositivo);
                AccionFormulario("Grabar");
                MessageBox.Show("Se ha registrado exitosamente el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                #endregion

            }

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text != null)
            {
                #region Eliminar () 
                modelo = new SAS_DispostivoController();

                SAS_Dispostivo Dispositivo = new SAS_Dispostivo();
                Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                int resultadoAccion = modelo.GetCountReferencias("SAS", Dispositivo);
                AccionFormulario("Grabar");
                MessageBox.Show("Se ha registrado exitosamente el registro" + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");

                #endregion

            }
        }

        private void lblPertenencia_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            AddItemIp();
        }

        private void AddItemIp()
        {
            try
            {
                if (dgvDetalleIP != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // ID                  
                    array.Add((ObtenerItemDetalleIP(ultimoItemNumeroIP))); // ITEM
                    array.Add("F"); // TIPO
                    array.Add("FISICO"); // TIPODESRIPCION
                    array.Add("00-00-00-00-00-00"); // MAC
                    array.Add(string.Empty); // SEGMENTOCODIGO
                    array.Add(string.Empty); // SEGMENTO
                    array.Add(string.Empty); // NUMERO
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta
                    array.Add(string.Empty); // OBSERVACIONES
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(0); //Codigo del IP
                    dgvDetalleIP.AgregarFila(array);
                    ultimoItemNumeroIP += 1;
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

        private string ObtenerItemDetalleIP(int numeroRegistros)
        {
            #region
            numeroRegistros += 1;
            return numeroRegistros.ToString().PadLeft(3, '0');
            #endregion
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            DeleteItemIP();
        }

        private void DeleteItemIP()
        {
            if (this.dgvDetalleIP != null)
            {
                #region
                if (dgvDetalleIP.CurrentRow != null && dgvDetalleIP.CurrentRow.Cells["chdispositivoCodigoIP"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvDetalleIP.CurrentRow.Cells["chdispositivoCodigoIP"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDetalleIP.CurrentRow.Cells["chdispositivoCodigoIP"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvDetalleIP.CurrentRow.Cells["chItemIP"].Value != null | dgvDetalleIP.CurrentRow.Cells["chItemIP"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleIP.CurrentRow.Cells["chItemIP"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoNumeroIpEliminados.Add(new SAS_DispositivoIP
                                {
                                    dispositivoCodigo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }

                        }

                        dgvDetalleIP.Rows.Remove(dgvDetalleIP.CurrentRow);
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

        private void DeleteItemColaborador()
        {
            if (this.dgvColaborador != null)
            {
                #region Eliminar detalle
                if (dgvColaborador.CurrentRow != null && dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string item = ((dgvColaborador.CurrentRow.Cells["chItemColaborador"].Value != null | dgvColaborador.CurrentRow.Cells["chItemColaborador"].Value.ToString().Trim() != string.Empty) ? (dgvDetalleIP.CurrentRow.Cells["chItemColaborador"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && item != string.Empty)
                            {

                                listadoColaboradoresEliminados.Add(new SAS_DispositivoUsuarios
                                {
                                    dispositivoCodigo = dispositivoCodigo,
                                    item = item,
                                });
                            }

                        }

                        dgvColaborador.Rows.Remove(dgvColaborador.CurrentRow);
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

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region Primer proceso asincrono
                string msgError = string.Empty;

                ObtenerListarOperativas();


                //modelo = new SAS_DispostivoController();
                //modelHardware = new SAS_DispositivoHardwareController();
                //modelSoftware = new SAS_DispositivoSoftwareController();
                //modelComponente = new SAS_DispositivoComponentesController();
                //modelCuentasUsuario = new SAS_DispositivoCuentaUsuariosController();
                //modelDocumentos = new SAS_DispositivoDocumentoController();

                //modelContadores = new SAS_DispositivoContadoresController();
                //modelMantenimiento = new SAS_DispositivoMovimientoMantenimientosController();
                //modelMovimientoAlmacen = new SAS_DispositivoMovimientoAlmacenController();
                //DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
                //DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
                //dispositivo = new SAS_Dispostivo();
                //oDispositivo = new SAS_Dispostivo();

                //DispositivoQuery = modelo.GetDeviceByIdDevice("SAS", idDispositivo);
                //DispositivoEdicionByList = modelo.ObtenerDatosDeDispositivo(oConexion, idDispositivo);
                //oDispositivo = modelo.ObtenerDispositivoFilterByID(oConexion, idDispositivo);
                //dispositivo = oDispositivo;


                //ipListByDevice = new List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult>();
                //ipListByDevice = modelo.DetalleDeDispositivosPorIPByCodigoDispositivo("SAS", oDispositivo); // Obtener listado de IP
                //msgError += "IP OK | ";

                //colaboradoresPorDevice = new List<SAS_ListadoColaboradoresByDispositivoByCodigoResult>(); // Obtener listado de colaboradores() 
                //colaboradoresPorDevice = modelo.ListadoColaboradoresByDispositivoByCodigo("SAS", oDispositivo.id);
                //msgError += "COLABORADOR OK | ";

                //hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>(); // Obtener listado de colaboradores() 
                //hardwarePorDevice = modelHardware.GetDispositivoHardwareByDevice("SAS", oDispositivo);
                //msgError += " HW OK | ";

                //softwarePorDevice = new List<SAS_DispositivoSoftwareByDeviceResult>(); // Obtener listado de colaboradores() 
                //softwarePorDevice = modelSoftware.GetDispositivoSoftwareByDevice("SAS", oDispositivo);
                //msgError += " SF OK| ";


                //componentesPorDevice = new List<SAS_DispositivoComponentesByDeviceResult>(); // Obtener listado de componentes hijos para un componente PAPÁ() 
                //componentesPorDevice = modelComponente.GetDispositivoCuentaUsuariosByDevice("SAS", oDispositivo);
                //msgError += " COMPO OK| ";


                //cuentasUsuariosPorDevice = new List<SAS_DispositivoCuentaUsuariosByDeviceResult>(); // Obtener listado de cuentas asociadas() 
                //cuentasUsuariosPorDevice = modelCuentasUsuario.GetDispositivoCuentaUsuariosByDevice("SAS", oDispositivo);
                //msgError += " USER ACCOUNT OK| ";

                //documentoPorDevice = new List<SAS_DispositivoDocumentoByDeviceResult>(); // Obtener listado de documentos() 
                //documentoPorDevice = modelDocumentos.GetDispositivoDocumentoByDevice("SAS", oDispositivo);
                //msgError += " DOCUMENTO OK| ";

                //// ADD 15.04.2022
                //contadoresPorDevice = new List<SAS_DispositivoaccountantsByDeviceIDResult>(); // Obtener listado de Contadores() 
                //contadoresPorDevice = modelContadores.GetListingByCode("SAS", oDispositivo);
                //msgError += " CONTADOR OK| ";

                //// ADD 15.04.2022
                //manteninientosPorDevice = new List<SAS_DispositivoMaintenanceByDeviceIDResult>(); // Obtener listado de Mantenimientos() 
                //manteninientosPorDevice = modelMantenimiento.GetListingByCode("SAS", oDispositivo);
                //msgError += " MANTENIMIENTO OK| ";


                //// ADD 15.04.2022
                //movimientoAlmacenPorDevice = new List<SAS_DispositivoWharehouseMovementsByDeviceIDResult>(); // Obtener listado de Mantenimientos() 
                //movimientoAlmacenPorDevice = modelMovimientoAlmacen.GetListingByCode("SAS", oDispositivo);
                //msgError += " MOVIMIENTO ALMACEN OK| ";

                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + msgError, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MostrarResultados();
        }

        private void MostrarResultados()
        {
            string msgError = string.Empty;
            try
            {
                #region Cargar controles()
                this.txtCodigo.Text = DispositivoEdicionByList.id != null ? DispositivoEdicionByList.id.ToString().PadLeft(7, '0') : string.Empty;
                this.txtEstado.Text = DispositivoEdicionByList.estado != null ? DispositivoEdicionByList.estado.Trim() : "INACTIVO";
                this.txtCreadoPor.Text = DispositivoEdicionByList.creadoPor != null ? DispositivoEdicionByList.creadoPor.Trim() : string.Empty;
                this.txtDescripcion.Text = DispositivoEdicionByList.dispositivo != null ? DispositivoEdicionByList.dispositivo.Trim() : string.Empty;
                this.txtNombre.Text = DispositivoEdicionByList.nombres != null ? DispositivoEdicionByList.nombres.Trim() : string.Empty;
                this.cboTipoDispositivo.SelectedValue = DispositivoEdicionByList.tipoDispositivoCodigo != null ? DispositivoEdicionByList.tipoDispositivoCodigo.Trim() : "000";
                this.cboSede.SelectedValue = DispositivoEdicionByList.sedeCodigo != null ? DispositivoEdicionByList.sedeCodigo.Trim() : "000";
                this.txtNumeroSerie.Text = DispositivoEdicionByList.numeroSerie != null ? DispositivoEdicionByList.numeroSerie.Trim() : string.Empty;
                this.txtCaracterísticas.Text = DispositivoEdicionByList.caracteristicas != null ? DispositivoEdicionByList.caracteristicas.Trim() : string.Empty;
                this.txtActivoCodigo.Text = DispositivoEdicionByList.activoCodigoERP != null ? DispositivoEdicionByList.activoCodigoERP.Trim() : string.Empty;
                this.txtActivoDescripcion.Text = DispositivoEdicionByList.activo != null ? DispositivoEdicionByList.activo.Trim() : string.Empty;
                this.txtMarcaCodigo.Text = DispositivoEdicionByList.idMarca != null ? DispositivoEdicionByList.idMarca.Trim() : string.Empty;
                this.txtMarcaDescripcion.Text = DispositivoEdicionByList.marca != null ? DispositivoEdicionByList.marca.Trim() : string.Empty;
                this.txtModeloCodigo.Text = DispositivoEdicionByList.idModelo != null ? DispositivoEdicionByList.idModelo.Trim() : string.Empty;
                this.txtModeloDescripción.Text = DispositivoEdicionByList.MODELO != null ? DispositivoEdicionByList.MODELO.Trim() : string.Empty;
                this.txtColorCodigo.Text = DispositivoEdicionByList.IdDispostivoColor != null ? DispositivoEdicionByList.IdDispostivoColor.Trim() : string.Empty;
                this.txtColorDescripcion.Text = DispositivoEdicionByList.color != null ? DispositivoEdicionByList.color.Trim() : string.Empty;
                this.txtNroParte.Text = DispositivoEdicionByList.numeroParte != null ? DispositivoEdicionByList.numeroParte.Trim() : string.Empty;

                this.txtUbicacion.Text = DispositivoEdicionByList.ubicacion != null ? this.DispositivoEdicionByList.ubicacion.Trim() : string.Empty;
                this.txtCostoUSD.Text = DispositivoEdicionByList.costoUSD != (decimal?)null ? this.DispositivoEdicionByList.costoUSD.ToDecimalPresentation() : "0.0";
                this.txtLineaCelularCodigo.Text = DispositivoEdicionByList.lineaCelular != null ? this.DispositivoEdicionByList.lineaCelular.Trim() : string.Empty;
                this.txtAnioDepreciar.Value = DispositivoEdicionByList.AniosParaDepreciar != (decimal?)null ? this.DispositivoEdicionByList.AniosParaDepreciar : 0;

                this.txtLongitud.Text = DispositivoEdicionByList.longitud != null ? DispositivoEdicionByList.longitud.Trim() : string.Empty;
                this.txtLatitud.Text = DispositivoEdicionByList.latitud != null ? DispositivoEdicionByList.latitud.Trim() : string.Empty;

                this.txtProductoCodigo.Text = DispositivoEdicionByList.idProducto != null ? DispositivoEdicionByList.idProducto.Trim() : string.Empty;
                this.txtProductoDescripcion.Text = DispositivoEdicionByList.producto != null ? DispositivoEdicionByList.producto.Trim() : string.Empty;
                if (DispositivoEdicionByList.EsPropio == 1)
                {
                    rbtPropio.Checked = true;
                }
                else
                {
                    rbtAlquilado.Checked = true;
                }
                if (DispositivoEdicionByList.funcionamientoCodigo == 1)
                {
                    btnEnOperacion.Checked = true;
                }
                else
                {
                    btnNoActivo.Checked = true;
                }
                if (DispositivoEdicionByList.esFinal == 1)
                {
                    chkEsFinal.Checked = true;
                }
                else
                {
                    chkEsFinal.Checked = false;
                }
                this.txtProveedorCodigo.Text = DispositivoEdicionByList.idClieprov != null ? DispositivoEdicionByList.idClieprov.Trim() : string.Empty;
                this.txtProveedorDescripcion.Text = DispositivoEdicionByList.razonSocial != null ? DispositivoEdicionByList.razonSocial.Trim() : string.Empty;
                this.txtCoordenada.Text = DispositivoEdicionByList.coordenada != null ? DispositivoEdicionByList.coordenada.Trim() : string.Empty;
                this.txtFechaActivacion.Text = DispositivoEdicionByList.fechaActivacion != null ? DispositivoEdicionByList.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
                this.txtDocCompraCodigo.Text = DispositivoEdicionByList.idCobrarpagarDoc != null ? DispositivoEdicionByList.idCobrarpagarDoc.Trim() : string.Empty;
                this.txtDocCompraDescripcion.Text = DispositivoEdicionByList.documentoCompra != null ? DispositivoEdicionByList.documentoCompra.Trim() : string.Empty;
                txtFechaProduccion.Text = DispositivoEdicionByList.fechaProduccion != null ? DispositivoEdicionByList.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
                txtFechaBaja.Text = DispositivoEdicionByList.fechaBaja != null ? DispositivoEdicionByList.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
                this.cboCondicion.SelectedValue = DispositivoEdicionByList.IdEstadoProducto != null ? DispositivoEdicionByList.IdEstadoProducto.ToString().Trim() : "X";
                this.cboArea.SelectedValue = DispositivoEdicionByList.idarea != null ? DispositivoEdicionByList.idarea.ToString().Trim() : "010";

                #endregion

                #region otros tab()
                ultimoItemNumeroIP = 1;
                ultimoItemHardware = 1;
                ultimoColaborador = 1;
                ultimoItemSoftware = 1;
                ultimoItemComponente = 1;
                ultimoItemCuentaDeUsuario = 1;
                ultimoItemDocumento = 1;
                ultimoItemContador = 1;
                ultimoMovimientoAlmacen = 1;
                ultimoItemManteniento = 1;
                if (ipListByDevice != null)
                {
                    if (ipListByDevice.Count > 0)
                    {
                        ultimoItemNumeroIP = Convert.ToInt32(ipListByDevice.Max(X => X.item));
                    }
                }
                if (colaboradoresPorDevice != null)
                {
                    if (colaboradoresPorDevice.Count > 0)
                    {
                        ultimoColaborador = Convert.ToInt32(colaboradoresPorDevice.Max(X => X.item));
                    }
                }
                if (hardwarePorDevice != null)
                {
                    if (hardwarePorDevice.Count > 0)
                    {
                        ultimoItemHardware = Convert.ToInt32(hardwarePorDevice.Max(X => X.item));
                    }
                }
                if (softwarePorDevice != null)
                {
                    if (softwarePorDevice.Count > 0)
                    {
                        ultimoItemSoftware = Convert.ToInt32(softwarePorDevice.Max(X => X.item));
                    }
                }
                // dispositivos
                if (componentesPorDevice != null)
                {
                    if (componentesPorDevice.Count > 0)
                    {
                        ultimoItemComponente = Convert.ToInt32(componentesPorDevice.Max(X => X.item));
                    }
                }
                // Cuenta de usuario
                if (cuentasUsuariosPorDevice != null)
                {
                    if (cuentasUsuariosPorDevice.Count > 0)
                    {
                        ultimoItemCuentaDeUsuario = Convert.ToInt32(cuentasUsuariosPorDevice.Max(X => X.item));
                    }
                }
                // documentos
                if (documentoPorDevice != null)
                {
                    if (documentoPorDevice.Count > 0)
                    {
                        ultimoItemDocumento = Convert.ToInt32(documentoPorDevice.Max(X => X.item));
                    }
                }
                //add 15.04.2022  contadoresPorDevice manteninientosPorDevice movimientoAlmacenPorDevice
                // Contadores
                if (contadoresPorDevice != null)
                {
                    if (contadoresPorDevice.Count > 0)
                    {
                        ultimoItemContador = Convert.ToInt32(contadoresPorDevice.Max(X => X.item));
                    }
                }
                // Mantenimiento
                if (manteninientosPorDevice != null)
                {
                    if (manteninientosPorDevice.Count > 0)
                    {
                        ultimoItemManteniento = Convert.ToInt32(manteninientosPorDevice.Max(X => X.item));
                    }
                }

                // movimiento almacen
                if (movimientoAlmacenPorDevice != null)
                {
                    if (movimientoAlmacenPorDevice.Count > 0)
                    {
                        ultimoMovimientoAlmacen = Convert.ToInt32(movimientoAlmacenPorDevice.Max(X => X.item));
                    }
                }

                dgvDetalleIP.CargarDatos(ipListByDevice.ToDataTable<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult>());
                dgvDetalleIP.Refresh();
                msgError += "IP OK GRILLA ";

                dgvColaborador.CargarDatos(colaboradoresPorDevice.ToDataTable<SAS_ListadoColaboradoresByDispositivoByCodigoResult>());
                dgvColaborador.Refresh();
                msgError += "COLABORADOR OK GRILLA ";

                dgvHardware.CargarDatos(hardwarePorDevice.ToDataTable<SAS_DispositivoHardwareByDeviceResult>());
                dgvHardware.Refresh();
                msgError += "HW OK GRILLA ";

                dgvSoftware.CargarDatos(softwarePorDevice.ToDataTable<SAS_DispositivoSoftwareByDeviceResult>());
                dgvSoftware.Refresh();
                msgError += "SW OK GRILLA ";

                dgvComponente.CargarDatos(componentesPorDevice.ToDataTable<SAS_DispositivoComponentesByDeviceResult>());
                dgvComponente.Refresh();
                msgError += "COMPONENTE OK GRILLA ";

                dgvCuentaDeUsuario.CargarDatos(cuentasUsuariosPorDevice.ToDataTable<SAS_DispositivoCuentaUsuariosByDeviceResult>());
                dgvCuentaDeUsuario.Refresh();
                msgError += "ACOUNT OK GRILLA ";

                dgvDocumento.CargarDatos(documentoPorDevice.ToDataTable<SAS_DispositivoDocumentoByDeviceResult>());
                dgvDocumento.Refresh();
                msgError += "DOCUMENTOS OK GRILLA ";

                // llenar grilla de contadores
                dgvContadores.CargarDatos(contadoresPorDevice.ToDataTable<SAS_DispositivoaccountantsByDeviceIDResult>());
                dgvContadores.Refresh();
                msgError += "DOCUMENTOS OK Contadores ";

                // llenar grilla de mantenimiento
                dgvMantenimiento.CargarDatos(manteninientosPorDevice.ToDataTable<SAS_DispositivoMaintenanceByDeviceIDResult>());
                dgvMantenimiento.Refresh();
                msgError += "DOCUMENTOS OK Mantenimientos ";

                // llenar grilla de movimiento de almacen
                dgvMovimientoAlmacen.CargarDatos(movimientoAlmacenPorDevice.ToDataTable<SAS_DispositivoWharehouseMovementsByDeviceIDResult>());
                dgvMovimientoAlmacen.Refresh();
                msgError += "DOCUMENTOS OK Movimiento almacen ";


                gbDispositivo.Enabled = !false;
                gbDetalles.Enabled = !false;
                BarraPrincipal.Enabled = !false;

                MostrarQr();
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + msgError, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void dgvDetalleIP_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de interface() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chtipoDescripcion")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetTypeInterface("SAS");
                        search.Text = "Buscar tipo de Interface";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chTipo"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chtipoDescripcion"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


                #region Tipo de Segmento() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chSegmentoIP")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetTypeOfSegments("SAS");
                        search.Text = "Buscar tipo de segmento";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo;
                            this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chsegmentoCodigoIP"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chSegmentoIP"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion


                #region número de IP() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chNumeroIP")
                {
                    if (this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chsegmentoCodigoIP"].Value != null)
                    {
                        if (this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chsegmentoCodigoIP"].Value.ToString() != string.Empty)
                        {
                            string codigoSegmento = this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chsegmentoCodigoIP"].Value.ToString().Trim();
                            if (e.KeyCode == Keys.F3)
                            {
                                frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                                search.ListaGeneralResultado = modelo.GetNumberOfIpsPerSegment("SAS", codigoSegmento);
                                search.Text = "Buscar número de IP por segmento";
                                search.txtTextoFiltro.Text = "";
                                if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                    this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidIP"].Value = search.ObjetoRetorno.Codigo;
                                    this.dgvDetalleIP.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chNumeroIP"].Value = search.ObjetoRetorno.Descripcion;
                                }
                            }
                        }
                    }


                }
                #endregion 


            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregarDetalleUsuario_Click(object sender, EventArgs e)
        {
            AddItemColaborador();
        }

        private void dgvColaborador_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvColaborador_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de interface() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chColaborador")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetALLCollaborators("SAS");
                        search.Text = "Buscar colaboradores";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvColaborador.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidCodigoGeneral"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvColaborador.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chColaborador"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void btnHardwareAdd_Click(object sender, EventArgs e)
        {
            AddItemHardware();
        }

        private void AddItemHardware()
        {
            try
            {
                if (dgvHardware != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // codigoDispositivo                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemHardware))); // item
                    array.Add(""); // codigoHardware
                    array.Add(""); // hardware         
                    array.Add(string.Empty); // numeroParte    
                    array.Add(1); // capacidad          
                    array.Add("UNID"); // unidadMedidaCapacidad    
                    //array.Add(string.Empty); // numeroParte    
                    array.Add(string.Empty); // serie    
                    array.Add(string.Empty); // observacion
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta                    
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(1); // seVisualizaEnReportes
                    dgvHardware.AgregarFila(array);
                    ultimoItemHardware += 1;
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

        private void btnSoftwareRemove_Click(object sender, EventArgs e)
        {
            RemoveItemSoftware();
        }

        private void RemoveItemHardware()
        {
            if (this.dgvHardware != null)
            {
                #region delete item() 
                if (dgvHardware.CurrentRow != null && dgvHardware.CurrentRow.Cells["chcodigoDispositivoHW"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvHardware.CurrentRow.Cells["chcodigoDispositivoHW"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvHardware.CurrentRow.Cells["chcodigoDispositivoHW"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvHardware.CurrentRow.Cells["chitemHW"].Value != null | dgvHardware.CurrentRow.Cells["chitemHW"].Value.ToString().Trim() != string.Empty) ? (dgvHardware.CurrentRow.Cells["chitemHW"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoHardwareEliminados.Add(new SAS_DispositivoHardware
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvHardware.Rows.Remove(dgvHardware.CurrentRow);
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

        private void dgvHardware_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chhardware")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetHardwares("SAS");
                        search.Text = "Buscar tipo componentes internos";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcodigoHardware"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvHardware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chhardware"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void dgvSoftware_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chsoftware")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetSoftwares("SAS");
                        search.Text = "Buscar software";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcodigoSoftware"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chsoftware"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void btnComponenteChangeState_Click(object sender, EventArgs e)
        {
            ChangeDetailComponents();
        }


        private void ChangeDetailComponents()
        {
            try
            {

                if (dgvComponente.CurrentRow.Cells["chIdEstadoComponente"].Value.ToString() == "1")
                {
                    dgvComponente.CurrentRow.Cells["chIdEstadoComponente"].Value = "0";
                    dgvComponente.CurrentRow.Cells["chEstadoComponente"].Value = "INACTIVO";
                }
                else
                {
                    dgvComponente.CurrentRow.Cells["chIdEstadoComponente"].Value = "1";
                    dgvComponente.CurrentRow.Cells["chEstadoComponente"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }



        private void btnComponenteAdd_Click(object sender, EventArgs e)
        {
            AddItemComponente();
        }

        private void btnComponenteRemove_Click(object sender, EventArgs e)
        {
            RemoveItemComponente();
        }

        private void RemoveItemComponente()
        {
            if (this.dgvComponente != null)
            {
                #region delete item() 
                if (dgvComponente.CurrentRow != null && dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value != null)
                {
                    try
                    {
                        Int32 dispositivoCodigo = (dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvComponente.CurrentRow.Cells["chItemComponente"].Value != null | dgvComponente.CurrentRow.Cells["chItemComponente"].Value.ToString().Trim() != string.Empty) ? (dgvComponente.CurrentRow.Cells["chItemComponente"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {
                                listadoComponentesEliminados.Add(new SAS_DispositivoComponentes
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvComponente.Rows.Remove(dgvComponente.CurrentRow);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message.ToString() + "No se puede eliminar el item selecionado", "MENSAJE DE TEXTO");
                        return;
                    }
                }
                #endregion
            }
        }

        private void AddItemComponente()
        {
            try
            {
                #region add Item()
                if (dgvComponente != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // codigoDispositivo                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemComponente))); // item
                    array.Add(0); // codigoDispositivoComponente
                    array.Add(""); // Dispositivo                             
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta                    
                    array.Add(string.Empty); // observacion
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(1); // seVisualizaEnReportes
                    dgvComponente.AgregarFila(array);
                    ultimoItemComponente += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnCuentaUsuarioChangeState_Click(object sender, EventArgs e)
        {
            ChangeAccountUser();
        }

        private void ChangeAccountUser()
        {
            try
            {

                if (dgvCuentaDeUsuario.CurrentRow.Cells["chidestadoCuentaUsuario"].Value.ToString() == "1")
                {
                    dgvCuentaDeUsuario.CurrentRow.Cells["chidestadoCuentaUsuario"].Value = "0";
                    dgvCuentaDeUsuario.CurrentRow.Cells["chEstadoCuentaUsuario"].Value = "INACTIVO";
                }
                else
                {
                    dgvCuentaDeUsuario.CurrentRow.Cells["chidestadoCuentaUsuario"].Value = "1";
                    dgvCuentaDeUsuario.CurrentRow.Cells["chEstadoCuentaUsuario"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }


        private void btnCuentaUsuarioAdd_Click(object sender, EventArgs e)
        {
            AddItemCuentaUsuario();
        }

        private void btnCuentaUsuarioRemove_Click(object sender, EventArgs e)
        {
            RemoveItemCuentaUsuario();
        }

        private void RemoveItemCuentaUsuario()
        {
            if (this.dgvCuentaDeUsuario != null)
            {
                #region delete item() 
                if (dgvCuentaDeUsuario.CurrentRow != null && dgvCuentaDeUsuario.CurrentRow.Cells["chcodigoDispositivoCuentaUsuario"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvCuentaDeUsuario.CurrentRow.Cells["chcodigoDispositivoCuentaUsuario"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvCuentaDeUsuario.CurrentRow.Cells["chcodigoDispositivoCuentaUsuario"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvCuentaDeUsuario.CurrentRow.Cells["chitemCuentaUsuario"].Value != null | dgvCuentaDeUsuario.CurrentRow.Cells["chitemCuentaUsuario"].Value.ToString().Trim() != string.Empty) ? (dgvCuentaDeUsuario.CurrentRow.Cells["chitemCuentaUsuario"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoCuentasUsuariosEliminados.Add(new SAS_DispositivoCuentaUsuarios
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvCuentaDeUsuario.Rows.Remove(dgvCuentaDeUsuario.CurrentRow);
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

        private void AddItemCuentaUsuario()
        {
            try
            {
                #region add Item()
                if (dgvCuentaDeUsuario != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // codigoDispositivo                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemCuentaDeUsuario))); // item
                    array.Add(0); // codigoTipoCuenta
                    array.Add(""); // tipocuenta     
                    array.Add(string.Empty); // clave
                    array.Add(string.Empty); // cuenta
                    array.Add(string.Empty); // correo de recuperacion
                    array.Add(string.Empty); // telefono                    
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta                    
                    array.Add(string.Empty); // observacion
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(1); // seVisualizaEnReportes
                    dgvCuentaDeUsuario.AgregarFila(array);
                    ultimoItemCuentaDeUsuario += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnDocumentosStateChange_Click(object sender, EventArgs e)
        {
            ChangeDocument();
        }

        private void ChangeDocument()
        {
            try
            {

                if (dgvDocumento.CurrentRow.Cells["chIdEstadoDocumento"].Value.ToString() == "1")
                {
                    dgvDocumento.CurrentRow.Cells["chIdEstadoDocumento"].Value = "0";
                    dgvDocumento.CurrentRow.Cells["chEstadoDocumento"].Value = "INACTIVO";
                }
                else
                {
                    dgvDocumento.CurrentRow.Cells["chIdEstadoDocumento"].Value = "1";
                    dgvDocumento.CurrentRow.Cells["chEstadoDocumento"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnDocumentosAdd_Click(object sender, EventArgs e)
        {
            AddItemDocumentos();
        }

        private void btnQuitarDetalleUsuario_Click(object sender, EventArgs e)
        {
            RemoveItemColaboradores();
        }

        private void btnDocumentosRemove_Click(object sender, EventArgs e)
        {
            RemoveItemDocumentos();
        }

        private void dgvDocumento_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();

            // tipo de documento
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name.ToUpper() == "chtipoDocumento".ToUpper())
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetTypeDocumentByDevice("SAS");
                        search.Text = "Buscar tipo de documentos";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcodigoTipoDocumentoDocumento"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chtipoDocumento"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }

            // Unidad de medida.
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name.ToUpper() == "chmedida".ToUpper())
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetListOfUnitsOfMeasurement("SAS");
                        search.Text = "Buscar tipo de unidad de medida";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidMedida"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chmedida"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }

            // Moneda
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name.ToUpper() == "chMoneda".ToUpper())
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetCurrencyTypeListing("SAS");
                        search.Text = "Buscar tipo de monedas";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidMoneda"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chMoneda"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }


            // Frecuencia
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name.ToUpper() == "chfrecuencia".ToUpper())
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetListOfFrequencies("SAS");
                        search.Text = "Buscar Frecuencia del contrato";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chfrecuenciaDeFacturacion"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvDocumento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chfrecuencia"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }

        }

        private void dgvCuentaDeUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chtipocuenta")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetTypeUserAccount("SAS");
                        search.Text = "Buscar tipo cuentas";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvCuentaDeUsuario.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcodigoTipoCuenta"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvCuentaDeUsuario.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chtipocuenta"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void dgvComponente_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chDispositivoHijo")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetComponentesInternos("SAS");
                        search.Text = "Buscar tipo componentes internos";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvComponente.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chCodigoComponente"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvComponente.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chDispositivoHijo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 


            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            gbDetalles.Enabled = true;
            gbDispositivo.Enabled = true;
            gbFuncionamiento.Enabled = true;
            gbPertenencia.Enabled = true;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void BtnImprimirEtiqueta_Click(object sender, EventArgs e)
        {
            PrintLabels();
        }

        private void PrintLabels()
        {
            if (this.txtCodigo.Text != string.Empty)
            {
                if (this.txtCodigo.Text != "0")
                {
                    if (dispositivo != null)
                    {
                        if (dispositivo.imagen != null)
                        {
                            if (dispositivo.imagen.ToString().Length > 10)
                            {
                                DispositivosEdicionImprimirEtiquetas ofrm = new DispositivosEdicionImprimirEtiquetas(Convert.ToInt32(this.txtCodigo.Text));
                                ofrm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No se ha registrado el QR en la base de datos, Editar y grabar", "MENSAJE DEL SISTEMA");
                                return;
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se ha registrado el QR en la base de datos, Editar y grabar", "MENSAJE DEL SISTEMA");
                            return;
                        }
                    }

                }
            }
        }

        private void btnIrACatalogoDispositivo_Click(object sender, EventArgs e)
        {
            RetornarACatalogoDeDispositivos();
        }

        private void RetornarACatalogoDeDispositivos()
        {
            if (codigoPrincipalDetalleComponente > 0 && codigoComponenteDetalleComponente > 0 && codigoEstadoDetalleComponente > 0)
            {
                dispositivoAsociado = new SAS_ListadoDeDispositivosByIdDeviceResult();
                modelo = new SAS_DispostivoController();
                dispositivoAsociado = modelo.GetDeviceByIdDevice("SAS", codigoComponenteDetalleComponente);
                DispositivosEdicion ofrm = new DispositivosEdicion("SAS", dispositivoAsociado);
                ofrm.Show();
            }


        }

        private void btnImprimirEtiquetaComponente_Click(object sender, EventArgs e)
        {
            ImprimirEtiquetaComponente();
        }

        private void ImprimirEtiquetaComponente()
        {
            if (codigoPrincipalDetalleComponente > 0 && codigoComponenteDetalleComponente > 0 && codigoEstadoDetalleComponente > 0)
            {
                dispositivoAsociado = new SAS_ListadoDeDispositivosByIdDeviceResult();
                modelo = new SAS_DispostivoController();
                dispositivoAsociado = modelo.GetDeviceByIdDevice("SAS", codigoComponenteDetalleComponente);

                if (dispositivoAsociado != null)
                {
                    if (dispositivoAsociado.imagen.ToString().Length > 10)
                    {
                        DispositivosEdicionImprimirEtiquetas ofrm = new DispositivosEdicionImprimirEtiquetas(Convert.ToInt32(dispositivoAsociado.id));
                        ofrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se ha registrado el QR en la base de datos, Editar y grabar", "MENSAJE DEL SISTEMA");
                        return;
                    }
                }

            }
        }

        private void dgvComponente_SelectionChanged(object sender, EventArgs e)
        {
            codigoPrincipalDetalleComponente = 0;
            codigoComponenteDetalleComponente = 0;
            codigoEstadoDetalleComponente = 0;
            if (dgvComponente != null && dgvComponente.Rows.Count > 0)
            {
                if (dgvComponente.CurrentRow != null)
                {
                    if (dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value != null)
                    {
                        if (dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value.ToString() != string.Empty)
                        {
                            codigoPrincipalDetalleComponente = (dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value != null ? Convert.ToInt32(dgvComponente.CurrentRow.Cells["chdispositivoCodigoComponente"].Value) : 0);
                            codigoComponenteDetalleComponente = (dgvComponente.CurrentRow.Cells["chCodigoComponente"].Value != null ? Convert.ToInt32(dgvComponente.CurrentRow.Cells["chCodigoComponente"].Value) : 0);
                            codigoEstadoDetalleComponente = (dgvComponente.CurrentRow.Cells["chIdEstadoComponente"].Value != null ? Convert.ToInt32(dgvComponente.CurrentRow.Cells["chIdEstadoComponente"].Value) : 0);
                        }
                    }
                }
            }
        }

        private void btnAgregarDetalleActivarUsuario_Click(object sender, EventArgs e)
        {
            ChangeAssignmentToTheCollaborator();
        }

        private void btnCambiarEstadoDetalleContadores_Click(object sender, EventArgs e)
        {
            ChangeStatusInItemCounters();
        }

        private void ChangeStatusInItemCounters()
        {
            try
            {

                if (dgvContadores.CurrentRow.Cells["chidestadoContadores"].Value.ToString() == "1")
                {
                    dgvContadores.CurrentRow.Cells["chidestadoContadores"].Value = "0";
                    dgvContadores.CurrentRow.Cells["chEstadoContadores"].Value = "INACTIVO";
                }
                else
                {
                    dgvContadores.CurrentRow.Cells["chidestadoContadores"].Value = "1";
                    dgvContadores.CurrentRow.Cells["chEstadoContadores"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAgregarDetalleContadores_Click(object sender, EventArgs e)
        {
            AddItemContadores();
        }

        private void AddItemContadores()
        {
            try
            {
                #region add Item()
                if (dgvContadores != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chcodigoDispositivoContador                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemContador))); // Item | chItemContador                                        
                    array.Add(DateTime.Now.ToString("ddMMyyyy")); // periodo         | chPeriodo 
                    array.Add(0); // Cantidad    | chCantidad                
                    array.Add(0); // contadorInicial        | chcontadorInicial
                    array.Add(0); // contadorFinal      | chcontadorFinal
                    array.Add(""); // itemContrato       | chitemContrato
                    array.Add("SIN CONTRATO"); // Contrato         | chContrato
                    array.Add("HOJA"); // IdMedida       | chIdMedidaContadores                  
                    array.Add(DateTime.Now.AddMonths(-1).ToPresentationDate()); // desde   | chDesdeContadores      
                    array.Add(DateTime.Now.ToPresentationDate()); // hasta           | chHastaContadores
                    array.Add(string.Empty); // observacion | chObservacionContador
                    array.Add(1); // IdEstado | chidestadoContadores
                    array.Add("ACTIVO"); // Estado          | chEstadoContadores
                    array.Add(1); // seVisualizaEnReportes | chseVisualizaEnReportesContadores
                    array.Add(user2.IdUsuario != null ? user2.IdUsuario.Trim() : Environment.UserName); // seVisualizaEnReportes | chIdusuarioContadores
                    dgvContadores.AgregarFila(array);
                    ultimoItemContador += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnQuitarDetalleContadores_Click(object sender, EventArgs e)
        {
            RemoveItemContadores();
        }

        private void RemoveItemContadores()
        {
            if (this.dgvContadores != null)
            {
                #region delete item() 
                if (dgvContadores.CurrentRow != null && dgvContadores.CurrentRow.Cells["chcodigoDispositivoContador"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvContadores.CurrentRow.Cells["chcodigoDispositivoContador"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvContadores.CurrentRow.Cells["chcodigoDispositivoContador"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvContadores.CurrentRow.Cells["chItemContador"].Value != null | dgvContadores.CurrentRow.Cells["chItemContador"].Value.ToString().Trim() != string.Empty) ? (dgvContadores.CurrentRow.Cells["chItemContador"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoContadoresEliminados.Add(new SAS_DispositivoContadores
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvContadores.Rows.Remove(dgvContadores.CurrentRow);
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

        private void btnCambiarEstadoDetalleMantenimiento_Click(object sender, EventArgs e)
        {
            ChangeStatusInMaintenanceDetail();
        }

        private void ChangeStatusInMaintenanceDetail()
        {
            try
            {

                if (dgvMantenimiento.CurrentRow.Cells["chidestadoMantenimiento"].Value.ToString() == "1")
                {
                    dgvMantenimiento.CurrentRow.Cells["chidestadoMantenimiento"].Value = "0";
                    dgvMantenimiento.CurrentRow.Cells["chEstadoMantenimiento"].Value = "INACTIVO";
                }
                else
                {
                    dgvMantenimiento.CurrentRow.Cells["chidestadoMantenimiento"].Value = "1";
                    dgvMantenimiento.CurrentRow.Cells["chEstadoMantenimiento"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAgregarDetalleMantenimiento_Click(object sender, EventArgs e)
        {
            AddItemMantenimientos();
        }

        private void AddItemMantenimientos()
        {
            try
            {
                #region add Item()
                if (dgvMantenimiento != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // chcodigoDispositivoMantenimiento                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemManteniento))); // chItemMantenimiento                                        
                    array.Add("002"); // chcodigoTipoMantenimiento    
                    array.Add("CORRECTIVO"); //chTipoMantenimientoMantenimiento
                    array.Add(string.Empty); //chCodigoColaborador
                    array.Add(string.Empty); //chColaboradorMantenimiento                        
                    array.Add(DateTime.Now.ToPresentationDateTime()); // chdesdeMantenimiento         
                    array.Add(DateTime.Now.AddYears(1).ToPresentationDateTime()); // chhastaMantenimiento           
                    array.Add(string.Empty); // chObservacionMantenimiento
                    array.Add(1); // chidestadoMantenimiento
                    array.Add("ACTIVO"); // chEstadoMantenimiento          
                    array.Add(1); // chseVisualizaEnReportesMantenimiento
                    array.Add(user2.IdUsuario != null ? user2.IdUsuario.Trim() : Environment.UserName); // chUsuarioAsignado
                    array.Add(0); // chCodigoOrdenTrabajo
                    array.Add("OTT - 0001 - 0000001"); // chdocumentoOT
                    dgvMantenimiento.AgregarFila(array);
                    ultimoItemManteniento += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void btnQuitarDetalleMantenimiento_Click(object sender, EventArgs e)
        {
            RemoveIteMantenimiento();
        }

        private void RemoveIteMantenimiento()
        {
            if (this.dgvMantenimiento != null)
            {
                #region delete item() 
                if (dgvMantenimiento.CurrentRow != null && dgvMantenimiento.CurrentRow.Cells["chcodigoDispositivoMantenimiento"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvMantenimiento.CurrentRow.Cells["chcodigoDispositivoMantenimiento"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvMantenimiento.CurrentRow.Cells["chcodigoDispositivoMantenimiento"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvMantenimiento.CurrentRow.Cells["chItemMantenimiento"].Value != null | dgvMantenimiento.CurrentRow.Cells["chItemMantenimiento"].Value.ToString().Trim() != string.Empty) ? (dgvMantenimiento.CurrentRow.Cells["chItemMantenimiento"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoMantenimientosEliminados.Add(new SAS_DispositivoMovimientoMantenimientos
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvMantenimiento.Rows.Remove(dgvMantenimiento.CurrentRow);
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

        private void dgvContadores_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            // PERIODO
            if (((DataGridView)sender).RowCount > 0)
            {
                #region PERIODO() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chPeriodo")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetPeriodoParaContadores("SAS");
                        search.Text = "Buscar Periodo";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvContadores.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPeriodo"].Value = search.ObjetoRetorno.Codigo;
                            //this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPeriodo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }

            // UNIDAD DE MEDIDA
            if (((DataGridView)sender).RowCount > 0)
            {
                #region UNIDAD DE MEDIDA()  
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chIdMedidaContadores")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetUnidadMedidaContadores("SAS");
                        search.Text = "Buscar Unidades de medida";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvContadores.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chIdMedidaContadores"].Value = search.ObjetoRetorno.Codigo;
                            //this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPeriodo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }


            // visiable en reportes
            if (((DataGridView)sender).RowCount > 0)
            {
                #region VISIBLE EN REPORTE() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chseVisualizaEnReportesContadores")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetEstadoParaVisualizacionEnReportes("SAS");
                        search.Text = "Buscar Estado para visualizacion en reportes";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvContadores.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chseVisualizaEnReportesContadores"].Value = search.ObjetoRetorno.Codigo;
                            //this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPeriodo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }


            // Contrato. chitemContrato
            if (((DataGridView)sender).RowCount > 0)
            {
                #region VISIBLE EN REPORTE() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name.ToUpper() == "chContrato".ToUpper())
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetDocumentosAsociadosAlDispositivo("SAS", Convert.ToInt32(this.txtCodigo.Text));
                        search.Text = "Buscar Estado para visualizacion en reportes";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvContadores.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chitemContrato"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvContadores.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chContrato"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }


        }

        private void dgvMantenimiento_KeyUp(object sender, KeyEventArgs e)
        {
            modelo = new SAS_DispostivoController();
            // TIPO DE MANTENIMIENTO
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de mantenimiento() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chTipoMantenimientoMantenimiento")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetMaintenanceListing("SAS");
                        search.Text = "Buscar tipo de mantenimiento";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvMantenimiento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chcodigoTipoMantenimiento"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvMantenimiento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chTipoMantenimientoMantenimiento"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }

            // COLARADORES ASIGNADOS
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Colaboradores asignados() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chColaboradorMantenimiento")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetListOfCollaboratorsAssignedToTheDevice("SAS", Convert.ToInt32(txtCodigo.Text));
                        search.Text = "Buscar colaboradores asignados al dispositivo";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvMantenimiento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chCodigoColaborador"].Value = search.ObjetoRetorno.Codigo;
                            this.dgvMantenimiento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chColaboradorMantenimiento"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }


            // visiable en reportes
            if (((DataGridView)sender).RowCount > 0)
            {
                #region VISIBLE EN REPORTE() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chseVisualizaEnReportesMantenimiento")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        search.ListaGeneralResultado = modelo.GetEstadoParaVisualizacionEnReportes("SAS");
                        search.Text = "Buscar Estado para visualizacion en reportes";
                        search.txtTextoFiltro.Text = "";
                        if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            //idRetorno = busquedas.ObjetoRetorno.Codigo; 
                            this.dgvMantenimiento.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chseVisualizaEnReportesMantenimiento"].Value = search.ObjetoRetorno.Codigo;
                            //this.dgvSoftware.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chPeriodo"].Value = search.ObjetoRetorno.Descripcion;
                        }
                    }
                }
                #endregion 
            }
        }

        private void cboTipoDeSistema_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarDetalleActivarIP_Click(object sender, EventArgs e)
        {
            ChangeStateDetailIP();
        }

        private void ChangeStateDetailIP()
        {
            try
            {

                if (dgvDetalleIP.CurrentRow.Cells["chEstadoIdIP"].Value.ToString() == "1")
                {
                    dgvDetalleIP.CurrentRow.Cells["chEstadoIdIP"].Value = "0";
                    dgvDetalleIP.CurrentRow.Cells["chEstadoIP"].Value = "INACTIVO";
                }
                else
                {
                    dgvDetalleIP.CurrentRow.Cells["chEstadoIdIP"].Value = "1";
                    dgvDetalleIP.CurrentRow.Cells["chEstadoIP"].Value = "ACTIVO";
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void btnSoftwareChangeStatus_Click(object sender, EventArgs e)
        {
            ChangeStateDetailSoftware();
        }

        private void ChangeStateDetailSoftware()
        {
            try
            {

                if (dgvSoftware.CurrentRow.Cells["chidestadoSW"].Value.ToString() == "1")
                {
                    dgvSoftware.CurrentRow.Cells["chidestadoSW"].Value = "0";
                    dgvSoftware.CurrentRow.Cells["chEstadoSW"].Value = "INACTIVO";
                }
                else
                {
                    dgvSoftware.CurrentRow.Cells["chidestadoSW"].Value = "1";
                    dgvSoftware.CurrentRow.Cells["chEstadoSW"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnHardwareCambiarEstado_Click(object sender, EventArgs e)
        {
            ChangeStateDetailHardware();
        }

        private void ChangeStateDetailHardware()
        {
            try
            {
                if (dgvHardware.CurrentRow.Cells["chidestadoHW"].Value.ToString() == "1")
                {
                    dgvHardware.CurrentRow.Cells["chidestadoHW"].Value = "0";
                    dgvHardware.CurrentRow.Cells["chEstadoHW"].Value = "INACTIVO";
                }
                else
                {
                    dgvHardware.CurrentRow.Cells["chidestadoHW"].Value = "1";
                    dgvHardware.CurrentRow.Cells["chEstadoHW"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnCambiarEstadoDetalleMovimientoAlmacen_Click(object sender, EventArgs e)
        {

        }

        private void btnImportarCaracteristicasHardwareDesdeOtroDispositivo_Click(object sender, EventArgs e)
        {

            if (txtProductoCodigo.Text.Trim() != string.Empty)
            {
                SAS_Dispostivo oDispositivo = new SAS_Dispostivo();
                oDispositivo.id = Convert.ToInt32(this.txtCodigo.Text);
                DipositivosEdicionImportarHardware ofrm = new DipositivosEdicionImportarHardware(oDispositivo);
                ofrm.ShowDialog();
            }

        }

        private void btnCompletarEspeficacionesHardware_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizarDetalleRegistro_Click(object sender, EventArgs e)
        {

            try
            {
                hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>(); // Obtener listado de colaboradores() 
                hardwarePorDevice = modelHardware.GetDispositivoHardwareByDevice("SAS", oDispositivo);
                dgvHardware.CargarDatos(hardwarePorDevice.ToDataTable<SAS_DispositivoHardwareByDeviceResult>());
                dgvHardware.Refresh();
                MessageBox.Show("Listado actualizado correctamente", "Confirmación del sistema");
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "NOTIFICACION DEL SISTEMA");
                return;
            }


        }

        private void bgwRegistrar_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new SAS_DispostivoController();
                resultadoAccion = modelo.Register("SAS", dispositivo, listadoNumeroIpEliminados, listadoNumeroIp, listadoColaboradoresEliminados, listadoColaboradores, listadoHardwareEliminados, listadoHardware, listadoSoftwareEliminados, listadoSoftware, listadoComponentesEliminados, listadoComponentes, listadoCuentasUsuariosEliminados, listadoCuentasUsuarios, listadoDocumentosEliminados, listadoDocumentos, listadoContadoresEliminados, listadoContadores, listadoMantenimientosEliminados, listadoMantenimientos, listadoMovimientoAlmacenEliminados, listadoMovimientoAlmacen);
                idDispositivo = resultadoAccion;
                string msgError = string.Empty;
                ObtenerListarOperativas();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void ObtenerListarOperativas()
        {
            modelHardware = new SAS_DispositivoHardwareController();
            modelSoftware = new SAS_DispositivoSoftwareController();
            modelComponente = new SAS_DispositivoComponentesController();
            modelCuentasUsuario = new SAS_DispositivoCuentaUsuariosController();
            modelDocumentos = new SAS_DispositivoDocumentoController();
            modelContadores = new SAS_DispositivoContadoresController();
            modelMantenimiento = new SAS_DispositivoMovimientoMantenimientosController();
            modelMovimientoAlmacen = new SAS_DispositivoMovimientoAlmacenController();
            modelo = new SAS_DispostivoController();
            DispositivoQuery = new SAS_ListadoDeDispositivosByIdDeviceResult();
            DispositivoEdicionByList = new SAS_ListadoDeDispositivos();
            oDispositivo = new SAS_Dispostivo();
            dispositivo = new SAS_Dispostivo();
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            listadoColaboradoresEliminados = new List<SAS_DispositivoUsuarios>();
            listadoHardwareEliminados = new List<SAS_DispositivoHardware>();
            listadoSoftwareEliminados = new List<SAS_DispositivoSoftware>();
            listadoComponentesEliminados = new List<SAS_DispositivoComponentes>();
            listadoCuentasUsuariosEliminados = new List<SAS_DispositivoCuentaUsuarios>();
            listadoDocumentosEliminados = new List<SAS_DispositivoDocumento>();
            listadoContadoresEliminados = new List<SAS_DispositivoContadores>();
            listadoMantenimientosEliminados = new List<SAS_DispositivoMovimientoMantenimientos>();
            listadoMovimientoAlmacenEliminados = new List<SAS_DispositivoMovimientoAlmacen>();
            listadoNumeroIp = new List<SAS_DispositivoIP>();
            listadoColaboradores = new List<SAS_DispositivoUsuarios>();
            listadoHardware = new List<SAS_DispositivoHardware>();
            listadoSoftware = new List<SAS_DispositivoSoftware>();
            listadoComponentes = new List<SAS_DispositivoComponentes>();
            listadoCuentasUsuarios = new List<SAS_DispositivoCuentaUsuarios>();
            listadoDocumentos = new List<SAS_DispositivoDocumento>();
            listadoContadores = new List<SAS_DispositivoContadores>();
            listadoMantenimientos = new List<SAS_DispositivoMovimientoMantenimientos>();
            listadoMovimientoAlmacen = new List<SAS_DispositivoMovimientoAlmacen>();

            DispositivoQuery = modelo.GetDeviceByIdDevice("SAS", idDispositivo);
            DispositivoEdicionByList = modelo.ObtenerDatosDeDispositivo(oConexion, idDispositivo);
            oDispositivo = modelo.ObtenerDispositivoFilterByID(oConexion, idDispositivo);
            dispositivo = oDispositivo;

            ipListByDevice = new List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult>();
            ipListByDevice = modelo.DetalleDeDispositivosPorIPByCodigoDispositivo("SAS", oDispositivo); // Obtener listado de IP
            msgError += "IP OK | ";

            colaboradoresPorDevice = new List<SAS_ListadoColaboradoresByDispositivoByCodigoResult>(); // Obtener listado de colaboradores() 
            colaboradoresPorDevice = modelo.ListadoColaboradoresByDispositivoByCodigo("SAS", oDispositivo.id);
            msgError += "COLABORADOR OK | ";

            hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>(); // Obtener listado de colaboradores() 
            hardwarePorDevice = modelHardware.GetDispositivoHardwareByDevice("SAS", oDispositivo);
            msgError += " HW OK | ";

            softwarePorDevice = new List<SAS_DispositivoSoftwareByDeviceResult>(); // Obtener listado de colaboradores() 
            softwarePorDevice = modelSoftware.GetDispositivoSoftwareByDevice("SAS", oDispositivo);
            msgError += " SF OK| ";

            componentesPorDevice = new List<SAS_DispositivoComponentesByDeviceResult>(); // Obtener listado de componentes hijos para un componente PAPÁ() 
            componentesPorDevice = modelComponente.GetDispositivoCuentaUsuariosByDevice("SAS", oDispositivo);
            msgError += " COMPO OK| ";

            cuentasUsuariosPorDevice = new List<SAS_DispositivoCuentaUsuariosByDeviceResult>(); // Obtener listado de cuentas asociadas() 
            cuentasUsuariosPorDevice = modelCuentasUsuario.GetDispositivoCuentaUsuariosByDevice("SAS", oDispositivo);
            msgError += " USER ACCOUNT OK| ";

            documentoPorDevice = new List<SAS_DispositivoDocumentoByDeviceResult>(); // Obtener listado de documentos() 
            documentoPorDevice = modelDocumentos.GetDispositivoDocumentoByDevice("SAS", oDispositivo);
            msgError += " DOCUMENTO OK| ";

            // ADD 15.04.2022
            contadoresPorDevice = new List<SAS_DispositivoaccountantsByDeviceIDResult>(); // Obtener listado de Contadores() 
            contadoresPorDevice = modelContadores.GetListingByCode("SAS", oDispositivo);
            msgError += " CONTADOR OK| ";

            // ADD 15.04.2022
            manteninientosPorDevice = new List<SAS_DispositivoMaintenanceByDeviceIDResult>(); // Obtener listado de Mantenimientos() 
            manteninientosPorDevice = modelMantenimiento.GetListingByCode("SAS", oDispositivo);
            msgError += " MANTENIMIENTO OK| ";

            // ADD 15.04.2022
            movimientoAlmacenPorDevice = new List<SAS_DispositivoWharehouseMovementsByDeviceIDResult>(); // Obtener listado de Mantenimientos() 
            movimientoAlmacenPorDevice = modelMovimientoAlmacen.GetListingByCode("SAS", oDispositivo);
            msgError += " MOVIMIENTO ALMACEN OK| ";


        }

        private void bgwRegistrar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Se ha registrado correctamente el documento", "Confirmación del sistema | Registro de dispositivo");
                MostrarResultados();
                AccionFormulario("Grabar");
                gbDispositivo.Enabled = !false;
                gbDetalles.Enabled = !false;
                pbDispositivo.Visible = !true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            VerDocumentosAdjuntosDelFormulario();
        }

        private void VerDocumentosAdjuntosDelFormulario()
        {

            try
            {
                #region Attach()
                if (this.txtCodigo.Text != string.Empty)
                {
                    if (this.txtCodigo.Text != "0")
                    {
                        int codigoSelecionado = Convert.ToInt32(this.txtCodigo.Text);
                        AdjuntarArchivos ofrm = new AdjuntarArchivos(conection, user2, companyId, privilege, codigoSelecionado.ToString(), nombreformulario);
                        ofrm.Show();

                    }
                    else
                    {
                        MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("El registro no se encuentra asociado en el sistema", "MENSAJE DEL SISTEMA");
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnCambiarEstadoDispositivo_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void ChangeStatus()
        {
            try
            {
                if (dispositivo != null)
                {
                    if (dispositivo.estado != null)
                    {

                        if (dispositivo.estado > 0)
                        {

                            DispositivosCambioEstado oFron = new DispositivosCambioEstado("SAS", oDispositivo.id, oDispositivo.nombres, DispositivoEdicionByList.estado, user2, companyId, privilege);
                            //oFron.MdiParent = DispositivosListado.ActiveForm;
                            //oFron.WindowState = FormWindowState.Normal;
                            //oFron.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                            oFron.ShowDialog();
                            if (oFron.DialogResult == DialogResult.OK)
                            {
                                gbDispositivo.Enabled = false;
                                gbDetalles.Enabled = false;
                                pbDispositivo.Visible = true;
                                bgwCambiarEstado.RunWorkerAsync();
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }


        private void bgwCambiarEstado_DoWork(object sender, DoWorkEventArgs e)
        {
            ObtenerListarOperativas();
        }

        private void bgwCambiarEstado_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                MessageBox.Show("Se ha actualizo correctamente el estado del documento", "Confirmación del sistema | Actualización de estado del dispositivo");
                MostrarResultados();
                AccionFormulario("Grabar");
                gbDispositivo.Enabled = !false;
                gbDetalles.Enabled = !false;
                pbDispositivo.Visible = !true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void ChangeDetailMoveWarehouse()
        {
            try
            {
                if (dgvMovimientoAlmacen.CurrentRow.Cells["chidestadoMovAlmacen"].Value.ToString() == "1")
                {
                    dgvMovimientoAlmacen.CurrentRow.Cells["chidestadoMovAlmacen"].Value = "0";
                    dgvMovimientoAlmacen.CurrentRow.Cells["chEstadoMovAlmacen"].Value = "INACTIVO";
                }
                else
                {
                    dgvMovimientoAlmacen.CurrentRow.Cells["chidestadoMovAlmacen"].Value = "1";
                    dgvMovimientoAlmacen.CurrentRow.Cells["chEstadoMovAlmacen"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void btnAgregarDetalleMovAlmacen_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitarDetalleMovAlmacen_Click(object sender, EventArgs e)
        {

        }

        //private void radButton1_Click(object sender, EventArgs e)
        //{

        //}

        //private void radButton3_Click(object sender, EventArgs e)
        //{

        //}

        //private void radButton2_Click(object sender, EventArgs e)
        //{

        //}

        //private void radButton4_Click(object sender, EventArgs e)
        //{

        //}

        //private void radButton6_Click(object sender, EventArgs e)
        //{

        //}

        //private void radButton5_Click(object sender, EventArgs e)
        //{

        //}

        private void ChangeAssignmentToTheCollaborator()
        {
            try
            {

                if (dgvColaborador.CurrentRow.Cells["chestadoItemColaborador"].Value.ToString() == "1")
                {
                    dgvColaborador.CurrentRow.Cells["chestadoItemColaborador"].Value = "0";
                    dgvColaborador.CurrentRow.Cells["chEstadoColaborador"].Value = "INACTIVO";
                }
                else
                {
                    dgvColaborador.CurrentRow.Cells["chestadoItemColaborador"].Value = "1";
                    dgvColaborador.CurrentRow.Cells["chEstadoColaborador"].Value = "ACTIVO";
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void gbDispositivo_Click(object sender, EventArgs e)
        {

        }

        private void cboTipoDispositivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoDispositivo.Text.ToUpper().Contains("CELULAR".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("MODEM".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("Tablet".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("Terminal Portatil".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("Sondas".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("PDA".ToUpper())
                || cboTipoDispositivo.Text.ToUpper().Contains("Modem Inalámbrico".ToUpper())
                )
            {
                btnLineaCelularBusqueda.Enabled = true;
                txtLineaCelularCodigo.ReadOnly = false;
                txtLineaCelularDescripcion.ReadOnly = false;

                workAreas = comboHelper.ListadoDeTipoDesistemaPorCodigoTipoDispositivo("SAS", (cboTipoDispositivo.SelectedValue != null ? cboTipoDispositivo.SelectedValue.ToString() : "000"));
                cboTipoDeSistema.DisplayMember = "Descripcion";
                cboTipoDeSistema.ValueMember = "Codigo";
                cboTipoDeSistema.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();
            }
            else
            {
                workAreas = comboHelper.ListadoDeTipoDesistemaPorCodigoTipoDispositivo("SAS", (cboTipoDispositivo.SelectedValue != null ? cboTipoDispositivo.SelectedValue.ToString() : "000"));
                cboTipoDeSistema.DisplayMember = "Descripcion";
                cboTipoDeSistema.ValueMember = "Codigo";
                cboTipoDeSistema.DataSource = workAreas.OrderBy(x => x.Descripcion).ToList();

                btnLineaCelularBusqueda.Enabled = !true;
                txtLineaCelularCodigo.ReadOnly = !false;
                txtLineaCelularDescripcion.ReadOnly = !false;
                txtLineaCelularCodigo.Clear();
                txtLineaCelularDescripcion.Clear();

            }
        }

        private void RemoveItemColaboradores()
        {
            // chdispositivoCodigoColaborador | chItemColaborador
            if (this.dgvColaborador != null)
            {
                #region delete item() 
                if (dgvColaborador.CurrentRow != null && dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {
                        Int32 dispositivoCodigo = (dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvColaborador.CurrentRow.Cells["chdispositivoCodigoColaborador"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvColaborador.CurrentRow.Cells["chItemColaborador"].Value != null | dgvColaborador.CurrentRow.Cells["chItemColaborador"].Value.ToString().Trim() != string.Empty) ? (dgvColaborador.CurrentRow.Cells["chItemColaborador"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoColaboradoresEliminados.Add(new SAS_DispositivoUsuarios
                                {
                                    dispositivoCodigo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvColaborador.Rows.Remove(dgvColaborador.CurrentRow);
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

        private void RemoveItemDocumentos()
        {

            if (this.dgvDocumento != null)
            {
                #region delete item() 
                if (dgvDocumento.CurrentRow != null && dgvDocumento.CurrentRow.Cells["chdispositivoCodigoDocumento"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvDocumento.CurrentRow.Cells["chdispositivoCodigoDocumento"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvDocumento.CurrentRow.Cells["chdispositivoCodigoDocumento"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvDocumento.CurrentRow.Cells["chItemDocumento"].Value != null | dgvDocumento.CurrentRow.Cells["chItemDocumento"].Value.ToString().Trim() != string.Empty) ? (dgvDocumento.CurrentRow.Cells["chItemDocumento"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoDocumentosEliminados.Add(new SAS_DispositivoDocumento
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvDocumento.Rows.Remove(dgvDocumento.CurrentRow);
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

        private void AddItemDocumentos()
        {
            try
            {
                #region add Item()
                if (dgvDocumento != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // codigoDispositivo                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemDocumento))); // item
                    array.Add(0); // codigoTipoCuenta
                    array.Add(string.Empty); // tipocuenta     
                    array.Add(Convert.ToDecimal(0));// cargoFijo
                    array.Add(Convert.ToDecimal(0));// cargoVariable
                    array.Add("01");  // idMoneda
                    array.Add("SOLES"); // moneda
                    array.Add("UNID"); // idMedida
                    array.Add("UNIDAD");// medida
                    array.Add(Convert.ToDecimal(0));// cantidadContratada
                    array.Add("00");// frecuenciaDeFacturacion
                    array.Add("Sin frecuencia".ToUpper());// frecuencia
                    array.Add(string.Empty); // link                    
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta                    
                    array.Add(string.Empty); // observacion
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(1); // seVisualizaEnReportes
                    dgvDocumento.AgregarFila(array);
                    ultimoItemDocumento += 1;
                }
                else
                {
                    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                }
                #endregion
            }
            catch (Exception ex)
            {
                Formateador.ControlExcepcion(this, this.Name, ex);
            }
        }

        private void RemoveItemSoftware()
        {
            if (this.dgvSoftware != null)
            {
                #region
                if (dgvSoftware.CurrentRow != null && dgvSoftware.CurrentRow.Cells["chcodigoDispositivoSW"].Value != null)
                {
                    //if (MessageBox.Show(this, "¿Desea eliminar el elemento seleccionado?", "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    try
                    {

                        Int32 dispositivoCodigo = (dgvSoftware.CurrentRow.Cells["chcodigoDispositivoSW"].Value.ToString().Trim() != "" ? Convert.ToInt32(dgvSoftware.CurrentRow.Cells["chcodigoDispositivoSW"].Value) : 0);
                        if (dispositivoCodigo != 0)
                        {
                            string itemIP = ((dgvSoftware.CurrentRow.Cells["chitemSW"].Value != null | dgvSoftware.CurrentRow.Cells["chitemSW"].Value.ToString().Trim() != string.Empty) ? (dgvSoftware.CurrentRow.Cells["chitemSW"].Value.ToString()) : string.Empty);
                            if (dispositivoCodigo != 0 && itemIP != string.Empty)
                            {

                                listadoSoftwareEliminados.Add(new SAS_DispositivoSoftware
                                {
                                    codigoDispositivo = dispositivoCodigo,
                                    item = itemIP,
                                });
                            }
                        }

                        dgvSoftware.Rows.Remove(dgvSoftware.CurrentRow);
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

        private void btnHardwareRemove_Click(object sender, EventArgs e)
        {
            RemoveItemHardware();
        }

        private void btnSoftwareAdd_Click(object sender, EventArgs e)
        {
            AddItemSoftware();
        }

        private void AddItemColaborador()
        {
            try
            {
                if (dgvColaborador != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // dispositivoCodigo                 
                    array.Add((ObtenerItemDetalleIP(ultimoColaborador))); // item
                    array.Add(""); // idCodigoGeneral
                    array.Add(""); // colaborador                    
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta
                    array.Add(string.Empty); // OBSERVACIONES
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado          
                    array.Add(0); // tipo
                    array.Add(1); // seVisualizaEnReportes
                    dgvColaborador.AgregarFila(array);
                    ultimoColaborador += 1;
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

        private void AddItemSoftware()
        {
            try
            {
                if (dgvSoftware != null)
                {
                    ArrayList array = new ArrayList();
                    array.Add(Convert.ToDecimal(txtCodigo.Text.Trim() != String.Empty ? txtCodigo.Text.Trim() : "0")); // codigoDispositivo                 
                    array.Add((ObtenerItemDetalleIP(ultimoItemSoftware))); // item
                    array.Add(""); // codigoSoftware
                    array.Add(""); // software                    
                    array.Add(string.Empty); // serie
                    array.Add(string.Empty); // version
                    array.Add(string.Empty); // informacionAdicional
                    array.Add(string.Empty); // numeroParte
                    array.Add(string.Empty); // observacion
                    array.Add(DateTime.Now.ToShortDateString()); // desde
                    array.Add(DateTime.Now.AddYears(1).ToShortDateString()); // Hasta                    
                    array.Add(1); // IdEstado
                    array.Add("ACTIVO"); // Estado       
                    array.Add(1); // seVisualizaEnReportes                       
                    dgvSoftware.AgregarFila(array);
                    ultimoItemSoftware += 1;
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
    }
}
