using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Extensions;
using System.Net.Mail;
using MyControlsDataBinding.Busquedas;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoSoporteFuncionalController
    {
        const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        const string passwordCorreo = @"iompqiiuhkjngkjr";

        public SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult GetListById(string conection, int codigo)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult item = new SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult();
            item.codigo = 0;

            if (codigo != 0)
            {
                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    item = Modelo.SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigo(codigo).Single();
                }
            }

            return item;
        }

        public List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> GetListByDate(string conection, string fechaDesde, string fechaHasta)
        {
            List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult> list = new List<SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodos(fechaDesde, fechaHasta).ToList();
            }
            return list.OrderByDescending(x => x.codigo).ToList();
        }

        public int RegisterObject(string conection, SAS_DispositivoSoporteFuncional item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoSoporteFuncional oRegistro = new SAS_DispositivoSoporteFuncional();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_DispositivoSoporteFuncional();
                            //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                            //oRegistro.id = obtenerultimoregistro;
                            //oRegistro.codigo = item.codigo;
                            oRegistro.codigoPersonal = item.codigoPersonal;
                            oRegistro.idSerie = item.idSerie;
                            oRegistro.iddocumento = item.iddocumento;
                            oRegistro.fecha = item.fecha;
                            oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                            oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                            oRegistro.Observación = item.Observación;
                            oRegistro.idEstado = item.idEstado;
                            oRegistro.idTipoSoftware = item.idTipoSoftware;
                            oRegistro.usuario = item.usuario;
                            oRegistro.fechaCreacion = item.fechaCreacion;
                            oRegistro.idEmpresa = item.idEmpresa;
                            oRegistro.idSucursal = item.idSucursal;
                            oRegistro.costoUSD = item.costoUSD;
                            oRegistro.horasEstimadas = item.horasEstimadas;
                            oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                            oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                            oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                            oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                            oRegistro.numeroDePedido = item.numeroDePedido;
                            oRegistro.Glosa01 = item.Glosa01;
                            oRegistro.Glosa02 = item.Glosa02;
                            oRegistro.Glosa03 = item.Glosa03;
                            oRegistro.Glosa04 = item.Glosa04;
                            oRegistro.Glosa05 = item.Glosa05;
                            oRegistro.prioridad = item.prioridad;
                            oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                            oRegistro.EsReprogracion = item.EsReprogracion;

                            Modelo.SAS_DispositivoSoporteFuncional.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = oRegistro.codigo; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.codigo == 0)
                            {
                                #region Nuevo()
                                tipoResultadoOperacion = 0; // Nuevo() 
                                oRegistro = new SAS_DispositivoSoporteFuncional();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                //oRegistro.codigo = item.codigo;
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idTipoSoftware = item.idTipoSoftware;
                                oRegistro.usuario = item.usuario;
                                oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.costoUSD = item.costoUSD;
                                oRegistro.horasEstimadas = item.horasEstimadas;
                                oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                                oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                                oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                                oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                                oRegistro.idclieprov = item.idclieprov;
                                oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                                oRegistro.numeroDePedido = item.numeroDePedido;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                                oRegistro.EsReprogracion = item.EsReprogracion;

                                Modelo.SAS_DispositivoSoporteFuncional.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.codigo; // registrar                                
                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                oRegistro = new SAS_DispositivoSoporteFuncional();
                                oRegistro = resultado.Single();
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idTipoSoftware = item.idTipoSoftware;
                                oRegistro.usuario = item.usuario;
                                //oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.costoUSD = item.costoUSD;
                                oRegistro.horasEstimadas = item.horasEstimadas;
                                oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                                oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                                oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                                oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                                oRegistro.idclieprov = item.idclieprov;
                                oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                                oRegistro.numeroDePedido = item.numeroDePedido;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                                oRegistro.EsReprogracion = item.EsReprogracion;

                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.codigo; // registrar                                   

                                #endregion
                            }
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return tipoResultadoOperacion;

        }

        public int RegisterObject(string conection, SAS_DispositivoSoporteFuncional item, List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalleEliminado, List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalle)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoSoporteFuncionalController model = new SAS_DispositivoSoporteFuncionalController();
            SAS_DispositivoSoporteFuncional oRegistro = new SAS_DispositivoSoporteFuncional();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            #region Objeto OT() 
                            oRegistro = new SAS_DispositivoSoporteFuncional();
                            oRegistro.codigoPersonal = item.codigoPersonal;
                            oRegistro.idSerie = item.idSerie;
                            oRegistro.iddocumento = item.iddocumento;
                            oRegistro.fecha = item.fecha;
                            oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                            oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                            oRegistro.Observación = item.Observación;
                            oRegistro.idEstado = item.idEstado;
                            oRegistro.idTipoSoftware = item.idTipoSoftware;
                            oRegistro.usuario = item.usuario;
                            oRegistro.fechaCreacion = item.fechaCreacion;
                            oRegistro.idEmpresa = item.idEmpresa;
                            oRegistro.idSucursal = item.idSucursal;
                            oRegistro.costoUSD = item.costoUSD;
                            oRegistro.horasEstimadas = item.horasEstimadas;
                            oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                            oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                            oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                            oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                            oRegistro.numeroDePedido = item.numeroDePedido;
                            oRegistro.Glosa01 = item.Glosa01;
                            oRegistro.Glosa02 = item.Glosa02;
                            oRegistro.Glosa03 = item.Glosa03;
                            oRegistro.Glosa04 = item.Glosa04;
                            oRegistro.Glosa05 = item.Glosa05;
                            oRegistro.prioridad = item.prioridad;
                            oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                            oRegistro.EsReprogracion = item.EsReprogracion;

                            oRegistro.minutosProgramados = item.minutosProgramados;
                            oRegistro.CanalDeAtencionCodigo = item.CanalDeAtencionCodigo;

                            Modelo.SAS_DispositivoSoporteFuncional.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = oRegistro.codigo; // registrar

                            #region Agregar lista detalle() 
                            if (listadoDetalle != null)
                            {
                                if (listadoDetalle.ToList().Count > 0)
                                {
                                    foreach (var detalle in listadoDetalle)
                                    {
                                        SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                        oDetail.codigo = oRegistro.codigo;
                                        oDetail.item = detalle.item;
                                        oDetail.accion = detalle.accion;
                                        oDetail.desde = detalle.desde;
                                        oDetail.hasta = detalle.hasta;
                                        oDetail.estado = detalle.estado;
                                        oDetail.usuario = detalle.usuario;
                                        oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                        oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value != (decimal?)null ? detalle.HorasFormatoPlanilla.Value : 0);
                                        oDetail.costoUSD = detalle.costoUSD;
                                        oDetail.glosa = detalle.glosa;
                                        oDetail.fechacreacion = DateTime.Now;
                                        Modelo.SAS_DispositivoSoporteFuncionalDetalle.InsertOnSubmit(oDetail);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion


                            #region Agregar en la pestaña de dispositivos el mantenimiento generado()
                            //var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                            //if (resultadoByMto != null)
                            //{
                            //    if (resultadoByMto.ToList().Count == 0)
                            //    {
                            //        // es su primer registro
                            //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                            //        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                            //        oDispositivoMantenimiento.item = "001";
                            //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                            //        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                            //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                            //        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                            //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                            //        oDispositivoMantenimiento.estado = 1;
                            //        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                            //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                            //        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                            //        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                            //        Modelo.SubmitChanges();
                            //    }
                            //    else if (resultadoByMto.ToList().Count > 0)
                            //    {
                            //        // Evaluar que número de item le corresponde
                            //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                            //        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                            //        int ultimo = Convert.ToInt32(resultadoByMto.Max(x => x.item)) + 1;
                            //        oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                            //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                            //        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                            //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                            //        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                            //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                            //        oDispositivoMantenimiento.estado = 1;
                            //        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                            //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                            //        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                            //        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                            //        Modelo.SubmitChanges();
                            //    }
                            //}
                            #endregion

                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.codigo == 0)
                            {
                                #region Nuevo()
                                #region Registrar OT() 
                                oRegistro = new SAS_DispositivoSoporteFuncional();
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idTipoSoftware = item.idTipoSoftware;
                                oRegistro.usuario = item.usuario;
                                oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.costoUSD = item.costoUSD;
                                oRegistro.horasEstimadas = item.horasEstimadas;
                                oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                                oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                                oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                                oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                                oRegistro.idclieprov = item.idclieprov;
                                oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                                oRegistro.numeroDePedido = item.numeroDePedido;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                                oRegistro.EsReprogracion = item.EsReprogracion;
                                oRegistro.minutosProgramados = item.minutosProgramados;
                                oRegistro.CanalDeAtencionCodigo = item.CanalDeAtencionCodigo;


                                Modelo.SAS_DispositivoSoporteFuncional.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                #endregion
                                tipoResultadoOperacion = oRegistro.codigo; // registrar      

                                #region Agregar lista detalle()
                                if (listadoDetalle != null)
                                {
                                    if (listadoDetalle.ToList().Count > 0)
                                    {
                                        foreach (var detalle in listadoDetalle)
                                        {
                                            SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                            oDetail.codigo = oRegistro.codigo;
                                            oDetail.item = detalle.item;
                                            oDetail.accion = detalle.accion;
                                            oDetail.desde = detalle.desde;
                                            oDetail.hasta = detalle.hasta;
                                            oDetail.estado = detalle.estado;
                                            oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value != (decimal?)null ? detalle.HorasFormatoPlanilla.Value : 0);
                                            oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                            oDetail.usuario = detalle.usuario;
                                            oDetail.costoUSD = detalle.costoUSD;
                                            oDetail.glosa = detalle.glosa;
                                            oDetail.fechacreacion = DateTime.Now;
                                            Modelo.SAS_DispositivoSoporteFuncionalDetalle.InsertOnSubmit(oDetail);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Agregar en la pestaña de dispositivos el mantenimiento generado()
                                //var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                                //if (resultadoByMto != null)
                                //{
                                //    if (resultadoByMto.ToList().Count == 0)
                                //    {
                                //        // es su primer registro
                                //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                //        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                //        oDispositivoMantenimiento.item = "001";
                                //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                //        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                //        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                //        oDispositivoMantenimiento.estado = 1;
                                //        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                //        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                //        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                //        Modelo.SubmitChanges();
                                //    }
                                //    else if (resultadoByMto.ToList().Count > 0)
                                //    {
                                //        // Evaluar que número de item le corresponde
                                //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                //        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                //        int ultimo = Convert.ToInt32(resultadoByMto.Max(x => x.item)) + 1;
                                //        oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                                //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                //        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                //        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                //        oDispositivoMantenimiento.estado = 1;
                                //        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                //        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                //        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                //        Modelo.SubmitChanges();
                                //    }
                                //}
                                #endregion

                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                #region Actualizar OT() 
                                oRegistro = new SAS_DispositivoSoporteFuncional();
                                oRegistro = resultado.Single();
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idTipoSoftware = item.idTipoSoftware;
                                //oRegistro.usuario = item.usuario;
                                //oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.costoUSD = item.costoUSD;
                                oRegistro.horasEstimadas = item.horasEstimadas;
                                oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                                oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                                oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                                oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                                oRegistro.idclieprov = item.idclieprov;
                                oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                                oRegistro.numeroDePedido = item.numeroDePedido;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                                oRegistro.EsReprogracion = item.EsReprogracion;
                                oRegistro.minutosProgramados = item.minutosProgramados;
                                oRegistro.CanalDeAtencionCodigo = item.CanalDeAtencionCodigo;

                                Modelo.SubmitChanges();
                                #endregion
                                tipoResultadoOperacion = oRegistro.codigo; // registrar  

                                // region eliminar lista detalle
                                #region Elimiar lista detalle()
                                if (listadoDetalleEliminado != null)
                                {
                                    if (listadoDetalleEliminado.ToList().Count > 0)
                                    {
                                        foreach (var detalle in listadoDetalleEliminado)
                                        {
                                            SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                            var resultadoPorEliminar = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == detalle.codigo && x.item == detalle.item).ToList();
                                            if (resultadoPorEliminar != null)
                                            {
                                                if (resultadoPorEliminar.ToList().Count == 1)
                                                {
                                                    oDetail = resultadoPorEliminar.Single();
                                                    Modelo.SAS_DispositivoSoporteFuncionalDetalle.DeleteOnSubmit(oDetail);
                                                    Modelo.SubmitChanges();
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                // agregar lista detalle, aqui evaluo si es actualizacion o registro nuevo
                                #region Agregar lista detalle() 
                                if (listadoDetalle != null)
                                {
                                    if (listadoDetalle.ToList().Count > 0)
                                    {
                                        foreach (var detalle in listadoDetalle)
                                        {

                                            var resultadoParaAgregaroEditar = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == detalle.codigo && x.item == detalle.item).ToList();

                                            if (resultadoParaAgregaroEditar != null)
                                            {
                                                if (resultadoParaAgregaroEditar.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle() 
                                                    SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                                    oDetail.codigo = oRegistro.codigo;
                                                    oDetail.item = detalle.item;
                                                    oDetail.accion = detalle.accion;
                                                    oDetail.desde = detalle.desde;
                                                    oDetail.hasta = detalle.hasta;
                                                    oDetail.estado = detalle.estado;
                                                    oDetail.usuario = detalle.usuario;
                                                    oDetail.costoUSD = detalle.costoUSD;
                                                    oDetail.glosa = detalle.glosa;
                                                    oDetail.fechacreacion = DateTime.Now;
                                                    oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                                    oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value != (decimal?)null ? detalle.HorasFormatoPlanilla.Value : 0);
                                                    Modelo.SAS_DispositivoSoporteFuncionalDetalle.InsertOnSubmit(oDetail);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoParaAgregaroEditar.ToList().Count == 1)
                                                {
                                                    #region Editar() 
                                                    SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                                    oDetail = resultadoParaAgregaroEditar.Single();
                                                    oDetail.accion = detalle.accion;
                                                    oDetail.desde = detalle.desde;
                                                    oDetail.hasta = detalle.hasta;
                                                    oDetail.estado = detalle.estado;
                                                    oDetail.usuario = detalle.usuario;
                                                    oDetail.costoUSD = detalle.costoUSD;
                                                    oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                                    oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value != (decimal?)null ? detalle.HorasFormatoPlanilla.Value : 0);
                                                    oDetail.glosa = detalle.glosa;
                                                    Modelo.SubmitChanges();

                                                    #endregion
                                                }
                                            }


                                        }
                                    }
                                }
                                #endregion


                                #region Agregar en la pestaña de dispositivos el mantenimiento generado()
                                //var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo && x.codigoOrdenTrabajo == oRegistro.codigo).ToList();
                                //var resultadoByMtoTotal = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                                //int ultimo = Convert.ToInt32(resultadoByMtoTotal.Max(x => x.item)) + 1;

                                //if (resultadoByMto != null)
                                //{
                                //    if (resultadoByMto.ToList().Count == 0)
                                //    {
                                //        // es su primer registro
                                //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                //        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                //        oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                                //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                //        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                //        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                //        oDispositivoMantenimiento.estado = 1;
                                //        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                //        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                //        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                //        Modelo.SubmitChanges();
                                //    }
                                //    else if (resultadoByMto.ToList().Count == 1)
                                //    {
                                //        // Evaluar que número de item le corresponde
                                //        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                //        oDispositivoMantenimiento = resultadoByMto.Single();
                                //        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                //        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                //        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                //        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                //        //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                //        Modelo.SubmitChanges();
                                //    }
                                //}
                                #endregion


                            }
                            #endregion

                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;
        }

        public int FinalizarCaso(string conection, SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "AN" || item.idEstado != "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado == 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoSoporteFuncionalDetalle itemDetalle = new SAS_DispositivoSoporteFuncionalDetalle();
                                        itemDetalle = itemAFinalizar;
                                        itemDetalle.estado = 2;
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        item.idEstado = "AT";
                        Modelo.SubmitChanges();
                    }
                }
                #endregion
            }
            return resultado;
        }

        public int ArchivarCaso(string conection, SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "AN" || item.idEstado != "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado == 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoSoporteFuncionalDetalle itemDetalle = new SAS_DispositivoSoporteFuncionalDetalle();
                                        itemDetalle = itemAFinalizar;
                                        itemDetalle.estado = 3;
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        item.idEstado = "CE";
                        Modelo.SubmitChanges();
                    }
                }
                #endregion
            }
            return resultado;
        }


        public int CambiarAEstadoReprogramado(string conection, int codigoSolicitud)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
            SAS_DispositivoSoporteFuncional itemNuevo = new SAS_DispositivoSoporteFuncional();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region 
                itemNuevo = new SAS_DispositivoSoporteFuncional();
                var result01 = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == codigoSolicitud).ToList();
                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        item = new SAS_DispositivoSoporteFuncional();
                        item = result01.ElementAt(0);
                        DateTime fechaAReasignar = item.fecha;
                        item.idEstado = "RR";
                        Modelo.SubmitChanges();


                        itemNuevo = result01.ElementAt(0);
                        itemNuevo.codigo = 0;
                        itemNuevo.Observación = "REPROGRAMACION | " + (item.Observación).Trim();
                        itemNuevo.fecha = fechaAReasignar.AddDays(1);
                        itemNuevo.fechaEstimadaFinalizacion = fechaAReasignar.AddDays(1);
                        itemNuevo.idEstado = "PE";

                        List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalle = new List<SAS_DispositivoSoporteFuncionalDetalle>();
                        List<SAS_DispositivoSoporteFuncionalDetalle> listadoDetalleEliminado = new List<SAS_DispositivoSoporteFuncionalDetalle>();
                        var newListado = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == codigoSolicitud).ToList();
                        if (newListado != null || newListado.ToList().Count > 0)
                        {

                            listadoDetalle = (from detalle in newListado
                                              where detalle.codigo > 0 && detalle.item != string.Empty
                                              group detalle by new { detalle.item, detalle.codigo } into j
                                              select new SAS_DispositivoSoporteFuncionalDetalle
                                              {
                                                  codigo = 0,
                                                  item = j.Key.item,
                                                  accion = j.FirstOrDefault().accion != null ? j.FirstOrDefault().accion : string.Empty,
                                                  desde = fechaAReasignar,
                                                  hasta = fechaAReasignar,
                                                  estado = j.FirstOrDefault().estado != (byte?)null ? j.FirstOrDefault().estado : (byte?)null,
                                                  usuario = j.FirstOrDefault().usuario != null ? j.FirstOrDefault().usuario : string.Empty,
                                                  costoUSD = j.FirstOrDefault().costoUSD != (decimal?)null ? j.FirstOrDefault().costoUSD : (decimal?)null,
                                                  HorasFormatoReloj = j.FirstOrDefault().costoUSD != (decimal?)null ? j.FirstOrDefault().costoUSD : (decimal?)null,
                                                  HorasFormatoPlanilla = j.FirstOrDefault().costoUSD != (decimal?)null ? j.FirstOrDefault().costoUSD : (decimal?)null,
                                                  fechacreacion = DateTime.Now,
                                                  glosa = j.FirstOrDefault().glosa != null ? j.FirstOrDefault().glosa : string.Empty,
                                                  modulo = j.FirstOrDefault().modulo != null ? j.FirstOrDefault().modulo : string.Empty,
                                              }).ToList();
                        }

                        SAS_DispositivoSoporteFuncionalController claseMadre = new SAS_DispositivoSoporteFuncionalController();
                        resultado = claseMadre.RegisterObject(conection, itemNuevo, listadoDetalleEliminado, listadoDetalle);

                    }



                }
                #endregion
            }
            return resultado;
        }


        public int ReAperturarCaso(string conection, SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = result01.ElementAt(0);
                        if (item.idEstado == "AN" || item.idEstado == "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado != 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoSoporteFuncionalDetalle itemDetalle = new SAS_DispositivoSoporteFuncionalDetalle();
                                        itemDetalle = itemAFinalizar;
                                        itemDetalle.estado = 1;
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        item.idEstado = "PE";
                        Modelo.SubmitChanges();
                    }
                }
                #endregion
            }
            return resultado;
        }

        public int CambiarEstadoDeDocumentoAReprogramado(string conection, SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = result01.ElementAt(0);
                        item.idEstado = "RR";
                        Modelo.SubmitChanges();
                    }
                }
                #endregion
            }
            return resultado;
        }


        

        public int ActivarTarea(string conection, SAS_ListadoDeAtencionesDeSoporteFuncionalByPeriodosResult item)
        {
            SAS_DispositivoSoporteFuncional oRegistro = new SAS_DispositivoSoporteFuncional();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oRegistro = resultado.Single();
                        oRegistro.idEstado = "PE";
                        tipoResultadoOperacion = 3; // Activar
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int ReasignarUsuario(string conection, SAS_DispositivoSoporteFuncional item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoSoporteFuncionalController model = new SAS_DispositivoSoporteFuncionalController();
            SAS_DispositivoSoporteFuncional oRegistro = new SAS_DispositivoSoporteFuncional();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.codigo == 0)
                            {
                            }
                            else
                            {
                                #region Actualizar() 
                                #region Actualizar OT() 
                                oRegistro = new SAS_DispositivoSoporteFuncional();
                                oRegistro = resultado.Single();
                                oRegistro.usuario = item.usuario;
                                Modelo.SubmitChanges();
                                #endregion
                                tipoResultadoOperacion = oRegistro.codigo; // registrar  

                            }
                            #endregion

                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;
        }



        public decimal ObtenerHoraBase10ABase6(decimal horaBase10)
        {
            int numeroEntero = Convert.ToInt32(Math.Truncate(horaBase10));
            decimal newHours = (horaBase10 - numeroEntero) * 100;
            decimal newMinute = Math.Round(((newHours * 60) / 10000), 2);

            decimal result = 0;
            if (newHours > 0)
            {
                result = Convert.ToDecimal(numeroEntero + newMinute);
            }
            else
            {
                result = Convert.ToDecimal(numeroEntero);
            }


            return result;
        }

        public int ObtenerUltimoOperacion(string conection)
        {
            int ultimo = 1;
            int correlativo = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                correlativo = Modelo.SAS_DispositivoSoporteFuncional.ToList().Count > 0 ? Modelo.SAS_DispositivoSoporteFuncional.Max(x => x.codigo) : 0;
            }

            return correlativo + ultimo;
        }

        public int ChangeState(string conection, SAS_DispositivoSoporteFuncional item)
        {
            SAS_DispositivoSoporteFuncional oregistro = new SAS_DispositivoSoporteFuncional();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.idEstado == "PE" || oregistro.idEstado == "AN")
                        {
                            if (oregistro.idEstado == "PE")
                            {
                                oregistro.idEstado = "AN";
                                tipoResultadoOperacion = 2; // desactivar
                            }
                            else
                            {
                                oregistro.idEstado = "PE";
                                tipoResultadoOperacion = 3; // Activar
                            }
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int DeleteRecord(string conection, SAS_DispositivoSoporteFuncional item)
        {
            SAS_DispositivoSoporteFuncional oregistro = new SAS_DispositivoSoporteFuncional();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.idEstado == "PE")
                        {
                            Modelo.SAS_DispositivoSoporteFuncional.DeleteOnSubmit(oregistro);
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public void Notify(string conection, string Para, string Asunto, int codigoSolicitudSelecionada)
        {
            List<SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult> listadoCabecera = new List<SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigoResult>();
            List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult> listadoDetalle = new List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listadoCabecera = Modelo.SAS_ListadoDeAtencionesDeSoporteFuncionalByCodigo(codigoSolicitudSelecionada).ToList();
                listadoDetalle = Modelo.SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigo(codigoSolicitudSelecionada).ToList();
            }

            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica solicitud de atención en soporte funcional.\n"));
                Mensaje.Append(string.Format("Solicitud Generada : " + listadoCabecera.ElementAt(0).documento + "\n"));
                Mensaje.Append(string.Format("Datos del colaborador : " + listadoCabecera.ElementAt(0).codigoPersonal + " - " + listadoCabecera.ElementAt(0).nombresCompletos + "\n"));
                Mensaje.Append(string.Format("Descripción : " + listadoCabecera.ElementAt(0).Observación + "\n"));
                Mensaje.Append(string.Format("Fecha de solicitud {0:dd/MM/yyyy} \n\n", listadoCabecera.ElementAt(0).fecha));
                Mensaje.Append(string.Format("Atendido Por : " + listadoCabecera.ElementAt(0).usuario + "\n"));
                Mensaje.Append(string.Format("Nro de Actividades Programadas : " + listadoCabecera.ElementAt(0).numeroDeLabores.ToString() + "\n"));
                Mensaje.Append(string.Format("Nro de Actividades Ejecutadas : " + listadoCabecera.ElementAt(0).numeroDeLaboresTerminadas.ToString() + "\n"));
                Mensaje.Append(string.Format("Avance (%) : " + listadoCabecera.ElementAt(0).cumplimiento.Value.ToDecimalPresentation() + "\n"));

                if (listadoCabecera.ElementAt(0).idEstado != "AT")
                {
                    Mensaje.Append(string.Format("Días transcurridos (%) : " + (DateTime.Now - listadoCabecera.ElementAt(0).fecha).Days.ToString() + "\n\n\n"));
                }
                else
                {
                    Mensaje.Append(string.Format("Días transcurridos (%) : " + (listadoCabecera.ElementAt(0).fechaEstimadaFinalizacion.Value - listadoCabecera.ElementAt(0).fecha).Days.ToString() + "\n\n\n"));
                }


                Mensaje.Append(string.Format("Detalle de las acciones programadas" + "\n"));
                if (listadoDetalle != null && listadoDetalle.ToList().Count > 0)
                {
                    foreach (var item in listadoDetalle)
                    {
                        Mensaje.Append(string.Format("Acción : " + item.accion + " | " + item.glosa.Trim() + " | Estado : " + item.estado + "\n"));
                    }
                }

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2022 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));

                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mail.From = new MailAddress(usuarioCorreo, "Notificaciones - ERP | Sociedad Agrícola Saturno SA");
                mail.To.Add(Para);
                mail.Subject = Asunto + " | " + listadoCabecera.ElementAt(0).documento;
                mail.Body = Mensaje.ToString();

                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(usuarioCorreo, passwordCorreo);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                //Error = "Éxito";
                //MessageBox.Show(Error, "Mensaje del sistema");


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString().Trim();
                //MessageBox.Show(Error, "ADVERTANCIA DEL SISTEMA");                
            }
            #endregion
        }

        public List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult> GetListDetalleByCodigoMantenimiento(string conection, int codigo)
        {
            List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult> list = new List<SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeDetalleDeAtencionesDeSoporteFuncionalByCodigo(codigo).ToList();
            }
            return list.OrderBy(x => x.item).ToList();
        }

        public int DuplicateRegister(string conection, int codigo, string codigoEmpleado)
        {
            int resultadoOperacion = 0;
            SAS_DispositivoSoporteFuncionalController model = new SAS_DispositivoSoporteFuncionalController();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = resultado.ElementAt(0);


                        SAS_DispositivoSoporteFuncional oRegistro = new SAS_DispositivoSoporteFuncional();
                        oRegistro.codigoPersonal = codigoEmpleado;
                        oRegistro.idSerie = item.idSerie;
                        oRegistro.iddocumento = item.iddocumento;
                        oRegistro.fecha = item.fecha;
                        oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                        oRegistro.idTipoSoporteFuncional = item.idTipoSoporteFuncional;
                        oRegistro.Observación = item.Observación;
                        oRegistro.idEstado = "PE";
                        oRegistro.idTipoSoftware = item.idTipoSoftware;
                        oRegistro.usuario = item.usuario;
                        oRegistro.fechaCreacion = item.fechaCreacion;
                        oRegistro.idEmpresa = item.idEmpresa;
                        oRegistro.idSucursal = item.idSucursal;
                        oRegistro.costoUSD = item.costoUSD;
                        oRegistro.horasEstimadas = item.horasEstimadas;
                        oRegistro.fechaEstimadaFinalizacion = item.fechaEstimadaFinalizacion;
                        oRegistro.EsejecutadoPorPersonalExterno = item.EsejecutadoPorPersonalExterno;
                        oRegistro.requiereSupervisionSST = item.requiereSupervisionSST;
                        oRegistro.esUnTrabajoProgramado = item.esUnTrabajoProgramado;
                        oRegistro.idclieprov = item.idclieprov;
                        oRegistro.numeroDeTicketEmpresaExterna = item.numeroDeTicketEmpresaExterna;
                        oRegistro.numeroDePedido = item.numeroDePedido;
                        Modelo.SAS_DispositivoSoporteFuncional.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        resultadoOperacion = oRegistro.codigo; // registrar


                        var listadoDetalle = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == codigo).ToList();

                        if (listadoDetalle != null)
                        {
                            if (listadoDetalle.ToList().Count > 0)
                            {
                                foreach (var detalle in listadoDetalle)
                                {

                                    SAS_DispositivoSoporteFuncionalDetalle oDetail = new SAS_DispositivoSoporteFuncionalDetalle();
                                    oDetail.codigo = resultadoOperacion;
                                    oDetail.item = detalle.item;
                                    oDetail.accion = detalle.accion;
                                    oDetail.desde = item.fecha;
                                    oDetail.hasta = item.fecha;
                                    oDetail.estado = 1;
                                    oDetail.usuario = detalle.usuario;
                                    oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                    oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value != (decimal?)null ? detalle.HorasFormatoPlanilla.Value : 0);
                                    oDetail.costoUSD = detalle.costoUSD;
                                    oDetail.glosa = detalle.glosa;
                                    oDetail.fechacreacion = DateTime.Now;
                                    Modelo.SAS_DispositivoSoporteFuncionalDetalle.InsertOnSubmit(oDetail);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }


                    }
                }
            }
            return resultadoOperacion;
        }

        public int SetPriority(string conection, int codigo, byte codigoPrioridad)
        {
            int resultadoOperacion = 0;
            SAS_DispositivoSoporteFuncionalController model = new SAS_DispositivoSoporteFuncionalController();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = resultado.ElementAt(0);
                        item.prioridad = codigoPrioridad;
                        Modelo.SubmitChanges();
                        resultadoOperacion = item.codigo; // registrar
                    }
                }
            }
            return resultadoOperacion;
        }
        public int SetComments(string conection, int codigo, string comentario01, string comentario02, string comentario03)
        {
            int resultadoOperacion = 0;
            SAS_DispositivoSoporteFuncionalController model = new SAS_DispositivoSoporteFuncionalController();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoSoporteFuncional.Where(x => x.codigo == codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        SAS_DispositivoSoporteFuncional item = new SAS_DispositivoSoporteFuncional();
                        item = resultado.ElementAt(0);
                        item.Glosa01 = comentario01;
                        item.Glosa02 = comentario02;
                        item.Glosa03 = comentario03;
                        Modelo.SubmitChanges();
                        resultadoOperacion = item.codigo; // registrar
                    }
                }
            }
            return resultadoOperacion;
        }

        public List<DFormatoSimple> ObtenerListadoDeMinutos(string conection)
        {
            DFormatoSimple item = new DFormatoSimple();
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            item = new DFormatoSimple(); item.Codigo = "0"; item.Descripcion = "0 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "1.5"; item.Descripcion = "1.5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "2.5"; item.Descripcion = "2.5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "5"; item.Descripcion = "5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "10"; item.Descripcion = "10 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "15"; item.Descripcion = "15 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "20"; item.Descripcion = "20 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "25"; item.Descripcion = "25 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "30"; item.Descripcion = "30 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "35"; item.Descripcion = "35 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "40"; item.Descripcion = "40 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "45"; item.Descripcion = "45 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "50"; item.Descripcion = "50 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "55"; item.Descripcion = "55 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "60"; item.Descripcion = "60 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "65"; item.Descripcion = "1 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "70"; item.Descripcion = "1 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "75"; item.Descripcion = "1 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "80"; item.Descripcion = "1 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "85"; item.Descripcion = "1 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "90"; item.Descripcion = "1 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "95"; item.Descripcion = "1 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "100"; item.Descripcion = "1 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "105"; item.Descripcion = "1 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "110"; item.Descripcion = "1 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "115"; item.Descripcion = "1 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "120"; item.Descripcion = "2 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "125"; item.Descripcion = "2 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "130"; item.Descripcion = "2 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "135"; item.Descripcion = "2 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "140"; item.Descripcion = "2 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "145"; item.Descripcion = "2 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "150"; item.Descripcion = "2 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "155"; item.Descripcion = "2 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "160"; item.Descripcion = "2 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "165"; item.Descripcion = "2 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "170"; item.Descripcion = "2 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "175"; item.Descripcion = "2 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "180"; item.Descripcion = "3 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "185"; item.Descripcion = "3 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "190"; item.Descripcion = "3 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "195"; item.Descripcion = "3 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "200"; item.Descripcion = "3 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "205"; item.Descripcion = "3 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "210"; item.Descripcion = "3 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "215"; item.Descripcion = "3 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "220"; item.Descripcion = "3 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "225"; item.Descripcion = "3 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "230"; item.Descripcion = "3 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "235"; item.Descripcion = "3 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "240"; item.Descripcion = "4 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "245"; item.Descripcion = "4 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "250"; item.Descripcion = "4 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "255"; item.Descripcion = "4 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "260"; item.Descripcion = "4 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "265"; item.Descripcion = "4 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "270"; item.Descripcion = "4 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "275"; item.Descripcion = "4 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "280"; item.Descripcion = "4 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "285"; item.Descripcion = "4 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "290"; item.Descripcion = "4 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "295"; item.Descripcion = "4 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "300"; item.Descripcion = "5 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "305"; item.Descripcion = "5 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "310"; item.Descripcion = "5 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "315"; item.Descripcion = "5 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "320"; item.Descripcion = "5 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "325"; item.Descripcion = "5 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "330"; item.Descripcion = "5 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "335"; item.Descripcion = "5 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "340"; item.Descripcion = "5 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "345"; item.Descripcion = "5 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "350"; item.Descripcion = "5 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "355"; item.Descripcion = "5 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "360"; item.Descripcion = "6 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "365"; item.Descripcion = "6 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "370"; item.Descripcion = "6 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "375"; item.Descripcion = "6 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "380"; item.Descripcion = "6 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "385"; item.Descripcion = "6 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "390"; item.Descripcion = "6 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "395"; item.Descripcion = "6 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "400"; item.Descripcion = "6 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "405"; item.Descripcion = "6 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "410"; item.Descripcion = "6 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "415"; item.Descripcion = "6 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "420"; item.Descripcion = "7 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "425"; item.Descripcion = "7 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "430"; item.Descripcion = "7 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "435"; item.Descripcion = "7 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "440"; item.Descripcion = "7 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "445"; item.Descripcion = "7 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "450"; item.Descripcion = "7 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "455"; item.Descripcion = "7 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "460"; item.Descripcion = "7 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "465"; item.Descripcion = "7 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "470"; item.Descripcion = "7 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "475"; item.Descripcion = "7 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "480"; item.Descripcion = "7 Hora con 60 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "485"; item.Descripcion = "8 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "490"; item.Descripcion = "8 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "495"; item.Descripcion = "8 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "500"; item.Descripcion = "8 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "505"; item.Descripcion = "8 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "510"; item.Descripcion = "8 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "515"; item.Descripcion = "8 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "520"; item.Descripcion = "8 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "525"; item.Descripcion = "8 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "530"; item.Descripcion = "8 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "535"; item.Descripcion = "8 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "540"; item.Descripcion = "9 Hora con 0 minutos "; listado.Add(item);



            return listado;
        }



    }
}
