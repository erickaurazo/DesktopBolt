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

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class DispositivosEdicion : Form
    {
        #region Declaración de variables()         
        private SAS_ListadoDeDispositivos _Dispositivo;
        private SAS_ListadoDeDispositivosByIdDeviceResult _DispositivoQuery;
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
        private int ultimoItemNumeroIP, ultimoColaborador, ultimoItemHardware, ultimoItemSoftware, ultimoItemComponente, ultimoItemDocumento, ultimoItemCuentaDeUsuario = 1;
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
        #endregion

        public DispositivosEdicion()
        {
            InitializeComponent();
        }

        public DispositivosEdicion(string oConexion, SAS_ListadoDeDispositivos oDispositivoQuery)
        {
            InitializeComponent();
            CargarCombos();
            Inicio();
            this.oConexion = oConexion;
            _Dispositivo = oDispositivoQuery;
            oDispositivo = new SAS_Dispostivo();
            oDispositivo.id = oDispositivoQuery.id;
            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
            this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
            this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
            this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
            this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
            this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
            this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
            this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
            this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
            this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
            this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
            this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
            this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
            this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
            this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
            this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
            this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
            this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

            this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
            this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;

            this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
            this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;
            if (oDispositivoQuery.EsPropio == 1)
            {
                rbtPropio.Checked = true;
            }
            else
            {
                rbtAlquilado.Checked = true;
            }
            if (oDispositivoQuery.funcionamientoCodigo == 1)
            {
                btnEnOperacion.Checked = true;
            }
            else
            {
                btnNoActivo.Checked = true;
            }
            if (oDispositivoQuery.esFinal == 1)
            {
                chkEsFinal.Checked = true;
            }
            else
            {
                chkEsFinal.Checked = false;
            }
            this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
            this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
            this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
            this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
            this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
            this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
            txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
            txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
            this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
            this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";
            AccionFormulario("Edicion");
            gbDispositivo.Enabled = false;
            gbDetalles.Enabled = false;
            BarraPrincipal.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }

        public DispositivosEdicion(string oConexion, SAS_ListadoDeDispositivosByIdDeviceResult oDispositivoQuery)
        {
            InitializeComponent();
            CargarCombos();
            Inicio();
            this.oConexion = oConexion;
            _DispositivoQuery = oDispositivoQuery;
            oDispositivo = new SAS_Dispostivo();
            oDispositivo.id = _DispositivoQuery.id;

            _Dispositivo = new SAS_ListadoDeDispositivos();
            _Dispositivo.id = _DispositivoQuery.id;
            _Dispositivo.latitud = _DispositivoQuery.latitud;
            _Dispositivo.longitud = _DispositivoQuery.longitud;
            _Dispositivo.nombres = _DispositivoQuery.nombres;
            _Dispositivo.dispositivo = _DispositivoQuery.dispositivo;
            _Dispositivo.sedeCodigo = _DispositivoQuery.sedeCodigo;
            _Dispositivo.sedeDescripcion = _DispositivoQuery.sedeDescripcion;
            _Dispositivo.numeroSerie = _DispositivoQuery.numeroSerie;
            _Dispositivo.caracteristicas = _DispositivoQuery.caracteristicas;
            _Dispositivo.idestado = _DispositivoQuery.idestado;
            _Dispositivo.tipoDispositivoCodigo = _DispositivoQuery.tipoDispositivoCodigo;
            _Dispositivo.tipoDispositivo = _DispositivoQuery.tipoDispositivo;
            _Dispositivo.estado = _DispositivoQuery.estado;
            _Dispositivo.item = _DispositivoQuery.item;
            _Dispositivo.codigoSegmentoIP = _DispositivoQuery.codigoSegmentoIP;
            _Dispositivo.numeroIP = _DispositivoQuery.numeroIP;
            _Dispositivo.creadoPor = _DispositivoQuery.creadoPor;
            _Dispositivo.fechacreacion = _DispositivoQuery.fechacreacion;
            _Dispositivo.activoCodigoERP = _DispositivoQuery.activoCodigoERP;
            _Dispositivo.activo = _DispositivoQuery.activo;
            _Dispositivo.IdDispostivoColor = _DispositivoQuery.IdDispostivoColor;
            _Dispositivo.color = _DispositivoQuery.color;
            _Dispositivo.idModelo = _DispositivoQuery.idModelo;
            _Dispositivo.MODELO = _DispositivoQuery.MODELO;
            _Dispositivo.idMarca = _DispositivoQuery.idMarca;
            _Dispositivo.marca = _DispositivoQuery.marca;
            _Dispositivo.numeroParte = _DispositivoQuery.numeroParte;
            _Dispositivo.IdEstadoProducto = _DispositivoQuery.IdEstadoProducto;
            _Dispositivo.estadoProducto = _DispositivoQuery.estadoProducto;
            _Dispositivo.EsPropio = _DispositivoQuery.EsPropio;
            _Dispositivo.AlquiladoPropio = _DispositivoQuery.AlquiladoPropio;
            _Dispositivo.idProducto = _DispositivoQuery.idProducto;
            _Dispositivo.producto = _DispositivoQuery.producto;
            _Dispositivo.rutaImagen = _DispositivoQuery.rutaImagen;
            _Dispositivo.funcionamientoCodigo = _DispositivoQuery.funcionamientoCodigo;
            _Dispositivo.funcionamiento = _DispositivoQuery.funcionamiento;
            _Dispositivo.idClieprov = _DispositivoQuery.idClieprov;
            _Dispositivo.razonSocial = _DispositivoQuery.razonSocial;
            _Dispositivo.coordenada = _DispositivoQuery.coordenada;
            _Dispositivo.fechaActivacion = _DispositivoQuery.fechaActivacion;
            _Dispositivo.idCobrarpagarDoc = _DispositivoQuery.idCobrarpagarDoc;
            _Dispositivo.documentoCompra = _DispositivoQuery.documentoCompra;
            _Dispositivo.fechaBaja = _DispositivoQuery.fechaBaja;
            _Dispositivo.fechaProduccion = _DispositivoQuery.fechaProduccion;
            _Dispositivo.esFinal = _DispositivoQuery.esFinal;
            _Dispositivo.registro = _DispositivoQuery.registro;
            _Dispositivo.RegistradoNoRegistrado = _DispositivoQuery.RegistradoNoRegistrado;
            _Dispositivo.idarea = _DispositivoQuery.idarea;
            _Dispositivo.area = _DispositivoQuery.area;
            _Dispositivo.imagen = _DispositivoQuery.imagen;




            listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
            this.txtCodigo.Text = this._Dispositivo.id != null ? this._Dispositivo.id.ToString().PadLeft(7, '0') : string.Empty;
            this.txtEstado.Text = this._Dispositivo.estado != null ? this._Dispositivo.estado.Trim() : "INACTIVO";
            this.txtCreadoPor.Text = this._Dispositivo.creadoPor != null ? this._Dispositivo.creadoPor.Trim() : string.Empty;
            this.txtDescripcion.Text = this._Dispositivo.dispositivo != null ? this._Dispositivo.dispositivo.Trim() : string.Empty;
            this.txtNombre.Text = this._Dispositivo.nombres != null ? this._Dispositivo.nombres.Trim() : string.Empty;
            this.cboTipoDispositivo.SelectedValue = this._Dispositivo.tipoDispositivoCodigo != null ? this._Dispositivo.tipoDispositivoCodigo.Trim() : "000";
            this.cboSede.SelectedValue = this._Dispositivo.sedeCodigo != null ? this._Dispositivo.sedeCodigo.Trim() : "000";
            this.txtNumeroSerie.Text = this._Dispositivo.numeroSerie != null ? this._Dispositivo.numeroSerie.Trim() : string.Empty;
            this.txtCaracterísticas.Text = this._Dispositivo.caracteristicas != null ? this._Dispositivo.caracteristicas.Trim() : string.Empty;
            this.txtActivoCodigo.Text = this._Dispositivo.activoCodigoERP != null ? this._Dispositivo.activoCodigoERP.Trim() : string.Empty;
            this.txtActivoDescripcion.Text = this._Dispositivo.activo != null ? this._Dispositivo.activo.Trim() : string.Empty;
            this.txtMarcaCodigo.Text = this._Dispositivo.idMarca != null ? this._Dispositivo.idMarca.Trim() : string.Empty;
            this.txtMarcaDescripcion.Text = this._Dispositivo.marca != null ? this._Dispositivo.marca.Trim() : string.Empty;
            this.txtModeloCodigo.Text = this._Dispositivo.idModelo != null ? this._Dispositivo.idModelo.Trim() : string.Empty;
            this.txtModeloDescripción.Text = this._Dispositivo.MODELO != null ? this._Dispositivo.MODELO.Trim() : string.Empty;
            this.txtColorCodigo.Text = this._Dispositivo.IdDispostivoColor != null ? this._Dispositivo.IdDispostivoColor.Trim() : string.Empty;
            this.txtColorDescripcion.Text = this._Dispositivo.color != null ? this._Dispositivo.color.Trim() : string.Empty;
            this.txtNroParte.Text = this._Dispositivo.numeroParte != null ? this._Dispositivo.numeroParte.Trim() : string.Empty;

            this.txtLongitud.Text = this._Dispositivo.longitud != null ? this._Dispositivo.longitud.Trim() : string.Empty;
            this.txtLatitud.Text = this._Dispositivo.latitud != null ? this._Dispositivo.latitud.Trim() : string.Empty;

            this.txtProductoCodigo.Text = this._Dispositivo.idProducto != null ? this._Dispositivo.idProducto.Trim() : string.Empty;
            this.txtProductoDescripcion.Text = this._Dispositivo.producto != null ? this._Dispositivo.producto.Trim() : string.Empty;
            if (oDispositivoQuery.EsPropio == 1)
            {
                rbtPropio.Checked = true;
            }
            else
            {
                rbtAlquilado.Checked = true;
            }
            if (oDispositivoQuery.funcionamientoCodigo == 1)
            {
                btnEnOperacion.Checked = true;
            }
            else
            {
                btnNoActivo.Checked = true;
            }
            if (oDispositivoQuery.esFinal == 1)
            {
                chkEsFinal.Checked = true;
            }
            else
            {
                chkEsFinal.Checked = false;
            }
            this.txtProveedorCodigo.Text = this._Dispositivo.idClieprov != null ? this._Dispositivo.idClieprov.Trim() : string.Empty;
            this.txtProveedorDescripcion.Text = this._Dispositivo.razonSocial != null ? this._Dispositivo.razonSocial.Trim() : string.Empty;
            this.txtCoordenada.Text = this._Dispositivo.coordenada != null ? this._Dispositivo.coordenada.Trim() : string.Empty;
            this.txtFechaActivacion.Text = this._Dispositivo.fechaActivacion != null ? this._Dispositivo.fechaActivacion.Value.ToShortDateString().Trim() : string.Empty;
            this.txtDocCompraCodigo.Text = this._Dispositivo.idCobrarpagarDoc != null ? this._Dispositivo.idCobrarpagarDoc.Trim() : string.Empty;
            this.txtDocCompraDescripcion.Text = this._Dispositivo.documentoCompra != null ? this._Dispositivo.documentoCompra.Trim() : string.Empty;
            txtFechaProduccion.Text = this._Dispositivo.fechaProduccion != null ? this._Dispositivo.fechaProduccion.Value.ToShortDateString().Trim() : string.Empty;
            txtFechaBaja.Text = this._Dispositivo.fechaBaja != null ? this._Dispositivo.fechaBaja.Value.ToShortDateString().Trim() : string.Empty;
            this.cboCondicion.SelectedValue = this._Dispositivo.IdEstadoProducto != null ? this._Dispositivo.IdEstadoProducto.ToString().Trim() : "X";
            this.cboArea.SelectedValue = this._Dispositivo.idarea != null ? this._Dispositivo.idarea.ToString().Trim() : "010";
            AccionFormulario("Edicion");
            gbDispositivo.Enabled = false;
            gbDetalles.Enabled = false;
            BarraPrincipal.Enabled = false;
            bgwHilo.RunWorkerAsync();
        }


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

        }

        private void CargarCombos()
        {
            try
            {
                comboHelper = new ComboBoxHelper();
                sedes = new List<Grupo>();
                condicionesProductos = new List<Grupo>();
                condicionesProductos = new List<Grupo>();
                workAreas = new List<Grupo>();

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
                #region Registrar () 
                dispositivo = new SAS_Dispostivo();
                modelo = new SAS_DispostivoController();
                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                _Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
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
                dispositivo.imagen = pic;



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
                    dispositivo.EsPropio = 0;
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


                //rutaImagen
                //int resultadoAccion = modelo.Register("SAS", dispositivo);
                //int resultadoAccion = modelo.Register("SAS", dispositivo, listadoNumeroIpEliminados, listadoNumeroIp, listadoColaboradoresEliminados, listadoColaboradores, listadoHardwareEliminados, listadoHardware, listadoSoftwareEliminados, listadoSoftware);
                int resultadoAccion = modelo.Register("SAS", dispositivo, listadoNumeroIpEliminados, listadoNumeroIp, listadoColaboradoresEliminados, listadoColaboradores, listadoHardwareEliminados, listadoHardware, listadoSoftwareEliminados, listadoSoftware, listadoComponentesEliminados, listadoComponentes, listadoCuentasUsuariosEliminados, listadoCuentasUsuarios, listadoDocumentosEliminados, listadoDocumentos);
                AccionFormulario("Grabar");
                listadoNumeroIpEliminados = new List<SAS_DispositivoIP>();
                listadoColaboradoresEliminados = new List<SAS_DispositivoUsuarios>();
                listadoHardwareEliminados = new List<SAS_DispositivoHardware>();
                listadoSoftwareEliminados = new List<SAS_DispositivoSoftware>();
                listadoComponentesEliminados = new List<SAS_DispositivoComponentes>();
                listadoCuentasUsuariosEliminados = new List<SAS_DispositivoCuentaUsuarios>();
                listadoDocumentosEliminados = new List<SAS_DispositivoDocumento>();

                listadoNumeroIp = new List<SAS_DispositivoIP>();
                listadoColaboradores = new List<SAS_DispositivoUsuarios>();
                listadoHardware = new List<SAS_DispositivoHardware>();
                listadoSoftware = new List<SAS_DispositivoSoftware>();
                listadoComponentes = new List<SAS_DispositivoComponentes>();
                listadoCuentasUsuarios = new List<SAS_DispositivoCuentaUsuarios>();
                listadoDocumentos = new List<SAS_DispositivoDocumento>();


                MessageBox.Show("Se ha registrado exitosamente el registro" + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
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
                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                _Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                int resultadoAccion = modelo.Unregister("SAS", dispositivo);
                AccionFormulario("Grabar");
                MessageBox.Show("Se ha registrado exitosamente el registro" + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                #endregion

            }

        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text != null)
            {
                #region Eliminar () 
                modelo = new SAS_DispostivoController();

                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                _Dispositivo.id = this.txtCodigo.Text != string.Empty ? Convert.ToInt32(this.txtCodigo.Text) : 0;
                int resultadoAccion = modelo.Delete("SAS", dispositivo);
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
                string msgError = string.Empty;

                modelo = new SAS_DispostivoController();
                modelHardware = new SAS_DispositivoHardwareController();
                modelSoftware = new SAS_DispositivoSoftwareController();
                modelComponente = new SAS_DispositivoComponentesController();
                modelCuentasUsuario = new SAS_DispositivoCuentaUsuariosController();
                modelDocumentos = new SAS_DispositivoDocumentoController();



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


                //ultimoItemColaboradores colaboradoresPorDevice


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString() + msgError, "MENSAJE DEL SISTEMA");
                return;
            }



        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string msgError = string.Empty;
            try
            {


                ultimoItemNumeroIP = 1;
                ultimoItemHardware = 1;
                ultimoColaborador = 1;
                ultimoItemSoftware = 1;
                ultimoItemComponente = 1;
                ultimoItemCuentaDeUsuario = 1;
                ultimoItemDocumento = 1;

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

                gbDispositivo.Enabled = !false;
                gbDetalles.Enabled = !false;
                BarraPrincipal.Enabled = !false;

                MostrarQr();
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
                        search.ListaGeneralResultado = modelo.GetCollaborators("SAS");
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
                    array.Add(string.Empty); // capacidad          
                    array.Add(string.Empty); // unidadMedidaCapacidad    
                    array.Add(string.Empty); // numeroParte    
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
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de componente Interno() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chtipoDocumento")
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
            throw new NotImplementedException();
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
                    if (_Dispositivo != null)
                    {
                        if (_Dispositivo.imagen != null)
                        {
                            if (_Dispositivo.imagen.ToString().Length > 10)
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
