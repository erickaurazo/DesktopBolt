using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using System.Net.Mail;
using MyControlsDataBinding.Extensions;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoOrdenTrabajoController
    {
        //const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        //const string passwordCorreo = @"iompqiiuhkjngkjr";

        const string usuarioCorreo = "notify.bolt@gmail.com";
        const string passwordCorreo = "wppi kiav vegf sesx";

        public SAS_ListadoDeDispositivoOrdenTrabajoByIdResult GetListById(string conection, int codigo)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            SAS_ListadoDeDispositivoOrdenTrabajoByIdResult item = new SAS_ListadoDeDispositivoOrdenTrabajoByIdResult();
            item.codigo = 0;

            if (codigo != 0)
            {
                using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
                {
                    item = Modelo.SAS_ListadoDeDispositivoOrdenTrabajoById(codigo).Single();
                }
            }

            return item;
        }

        public List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult> GetListByDate(string conection, string fechaDesde, string fechaHasta)
        {
            List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult> list = new List<SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeDispositivoOrdenTrabajoByPeriodos(fechaDesde, fechaHasta).ToList();
            }
            return list.OrderByDescending(x => x.codigo).ToList();
        }

        public int RegisterObject(string conection, SAS_DispositivoOrdenTrabajo item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoOrdenTrabajo oRegistro = new SAS_DispositivoOrdenTrabajo();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_DispositivoOrdenTrabajo();
                            //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                            //oRegistro.id = obtenerultimoregistro;
                            //oRegistro.codigo = item.codigo;
                            oRegistro.codigoPersonal = item.codigoPersonal;
                            oRegistro.idSerie = item.idSerie;
                            oRegistro.iddocumento = item.iddocumento;
                            oRegistro.fecha = item.fecha;
                            oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                            oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                            oRegistro.Observación = item.Observación;
                            oRegistro.idEstado = item.idEstado;
                            oRegistro.idDispositivo = item.idDispositivo;
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
                            oRegistro.prioridad = item.prioridad;
                            Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
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
                                oRegistro = new SAS_DispositivoOrdenTrabajo();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                //oRegistro.codigo = item.codigo;
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idDispositivo = item.idDispositivo;
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
                                oRegistro.prioridad = item.prioridad;
                                Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.codigo; // registrar                                
                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                oRegistro = new SAS_DispositivoOrdenTrabajo();
                                oRegistro = resultado.Single();
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idDispositivo = item.idDispositivo;
                                oRegistro.usuario = item.usuario;
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
                                oRegistro.prioridad = item.prioridad;
                                //oRegistro.fechaCreacion = item.fechaCreacion;
                                //Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
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

        public List<Grupo> GetTypeStatusForDevice(string conection)
        {
            List<Grupo> result = new List<Grupo>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                
                    result = (from item in Modelo.SAS_DispositivoEstado
                              where item.estado != 0 && (item.id == 1 || item.id == 2 || item.id == 4 || item.id == 6 || item.id == 7 || item.id == 9 || item.id == 12)
                              group item by new { item.id } into j
                              select new Grupo
                              {
                                  Codigo = j.Key.id.ToString().Trim(),
                                  Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                              }
                              ).ToList();
               
            }
            return result;


    }

        public int ReasignarUsuario(string conection, SAS_DispositivoOrdenTrabajo ordenTrabajo)
        {
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == ordenTrabajo.codigo).ToList();
                    if (resultado != null)
                    {
                        if (resultado.Count == 1)
                        {
                            SAS_DispositivoOrdenTrabajo oOrden = new SAS_DispositivoOrdenTrabajo();
                            oOrden = resultado.ElementAt(0);
                            oOrden.usuario = ordenTrabajo.usuario;
                            Modelo.SubmitChanges();
                            result = oOrden.codigo;
                        }
                    }
                    Scope.Complete();
                }
            }
            return result;

        }

        public int SetComments(string conection, SAS_DispositivoOrdenTrabajo ordenTrabajo)
        {
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == ordenTrabajo.codigo).ToList();
                    if (resultado != null)
                    {
                        if (resultado.Count > 0)
                        {
                            SAS_DispositivoOrdenTrabajo oOrden = new SAS_DispositivoOrdenTrabajo();
                            oOrden = resultado.ElementAt(0);
                            oOrden.Glosa01 = ordenTrabajo.Glosa01;
                            oOrden.Glosa02 = ordenTrabajo.Glosa02;
                            oOrden.Glosa03 = ordenTrabajo.Glosa03;
                            Modelo.SubmitChanges();
                            result = oOrden.codigo;
                        }
                    }
                    Scope.Complete();
                }
            }
            return result;

        }

        public int SetIdStatusDevice(string conection, SAS_DispositivoOrdenTrabajo ordenTrabajo)
        {
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == ordenTrabajo.codigo).ToList();
                    if (resultado != null)
                    {
                        if (resultado.Count > 0)
                        {
                            SAS_DispositivoOrdenTrabajo oOrden = new SAS_DispositivoOrdenTrabajo();
                            oOrden = resultado.ElementAt(0);
                            oOrden.EstadoCerradoDispositivo = ordenTrabajo.EstadoCerradoDispositivo;
                            oOrden.Cerrado = 1;
                            Modelo.SubmitChanges();
                            result = oOrden.codigo;


                            SAS_Dispostivo oDevice = new SAS_Dispostivo();
                            var result02 = Modelo.SAS_Dispostivo.Where(x => x.id == oOrden.idDispositivo).ToList();
                            if (result02 != null)
                            {
                                if (result02.ToList().Count == 1)
                                {
                                    oDevice = result02.Single();
                                    oDevice.estado = oOrden.EstadoCerradoDispositivo != (int?)null ? Convert.ToByte(oOrden.EstadoCerradoDispositivo) : (byte?)null;
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                    }
                    Scope.Complete();
                }
            }
            return result;

        }

        public int DuplicateRegister(string conection, SAS_DispositivoOrdenTrabajo item)
        {
            int result = 0;
            SAS_DispositivoOrdenTrabajoController model = new SAS_DispositivoOrdenTrabajoController();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        if (resultado.Count > 0)
                        {
                            SAS_DispositivoOrdenTrabajo oOrden = new SAS_DispositivoOrdenTrabajo();
                            oOrden = resultado.ElementAt(0);

                            SAS_DispositivoOrdenTrabajo oRegistro = new SAS_DispositivoOrdenTrabajo();
                            oRegistro.codigoPersonal = item.codigoPersonal;
                            oRegistro.idSerie = oOrden.idSerie;
                            oRegistro.iddocumento = oOrden.iddocumento;
                            oRegistro.fecha = DateTime.Now;
                            oRegistro.periodo = DateTime.Now.ToString("MM") + DateTime.Now.ToString("yyyy");
                            oRegistro.idTipoMantenimiento = oOrden.idTipoMantenimiento;
                            oRegistro.Observación = oOrden.Observación;
                            oRegistro.idEstado = oOrden.idEstado;
                            oRegistro.idDispositivo = oOrden.idDispositivo;
                            oRegistro.usuario = oOrden.usuario;
                            oRegistro.fechaCreacion = oOrden.fechaCreacion;
                            oRegistro.idEmpresa = oOrden.idEmpresa;
                            oRegistro.idSucursal = oOrden.idSucursal;
                            oRegistro.costoUSD = oOrden.costoUSD;
                            oRegistro.horasEstimadas = oOrden.horasEstimadas;
                            oRegistro.fechaEstimadaFinalizacion = oOrden.fechaEstimadaFinalizacion;
                            oRegistro.EsejecutadoPorPersonalExterno = oOrden.EsejecutadoPorPersonalExterno;
                            oRegistro.requiereSupervisionSST = oOrden.requiereSupervisionSST;
                            oRegistro.esUnTrabajoProgramado = oOrden.esUnTrabajoProgramado;
                            oRegistro.idclieprov = oOrden.idclieprov;
                            oRegistro.numeroDeTicketEmpresaExterna = oOrden.numeroDeTicketEmpresaExterna;
                            oRegistro.numeroDePedido = oOrden.numeroDePedido;
                            oRegistro.prioridad = oOrden.prioridad;
                            Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            result = oRegistro.codigo; // registrar

                            var listadoDetalle = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == item.codigo).ToList();
                            if (listadoDetalle != null)
                            {
                                if (listadoDetalle.ToList().Count > 0)
                                {
                                    foreach (var detalle in listadoDetalle)
                                    {
                                        SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
                                        oDetail.codigo = result;
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
                                        oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value);
                                        Modelo.SAS_DispositivoOrdenTrabajoDetalle.InsertOnSubmit(oDetail);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                    Scope.Complete();
                }
            }
            return result;
        }

        public int SetPriority(string conection, SAS_DispositivoOrdenTrabajo ordenTrabajo)
        {
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == ordenTrabajo.codigo).ToList();
                    if (resultado != null)
                    {
                        if (resultado.Count > 0)
                        {
                            SAS_DispositivoOrdenTrabajo oOrden = new SAS_DispositivoOrdenTrabajo();
                            oOrden = resultado.ElementAt(0);
                            oOrden.prioridad = ordenTrabajo.prioridad;
                            Modelo.SubmitChanges();
                            result = oOrden.codigo;
                        }
                    }
                    Scope.Complete();
                }
            }
            return result;

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


        public int RegisterObject(string conection, SAS_DispositivoOrdenTrabajo item, List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalleEliminado, List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalle)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            SAS_DispositivoOrdenTrabajoController model = new SAS_DispositivoOrdenTrabajoController();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoOrdenTrabajo oRegistro = new SAS_DispositivoOrdenTrabajo();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            #region Objeto OT() 
                            oRegistro = new SAS_DispositivoOrdenTrabajo();
                            oRegistro.codigoPersonal = item.codigoPersonal;
                            oRegistro.idSerie = item.idSerie;
                            oRegistro.iddocumento = item.iddocumento;
                            oRegistro.fecha = item.fecha;
                            oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                            oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                            oRegistro.Observación = item.Observación;
                            oRegistro.idEstado = item.idEstado;
                            oRegistro.idDispositivo = item.idDispositivo;
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
                            oRegistro.prioridad = item.prioridad;
                            oRegistro.Glosa01 = item.Glosa01;
                            oRegistro.Glosa02 = item.Glosa02;
                            oRegistro.Glosa03 = item.Glosa03;
                            oRegistro.Glosa04 = item.Glosa04;
                            oRegistro.Glosa05 = item.Glosa05;                            
                            oRegistro.Cerrado = item.Cerrado;
                            oRegistro.IdReferencia = item.IdReferencia;
                            oRegistro.EstadoCerradoDispositivo = item.EstadoCerradoDispositivo;
                            oRegistro.EsReprogracion = item.EsReprogracion;
                            oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                            oRegistro.minutosProgramados = item.minutosProgramados;
                            oRegistro.CanalDeAtencionCodigo = item.CanalDeAtencionCodigo;

                            Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
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
                                        SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
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
                                        oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value);

                                        Modelo.SAS_DispositivoOrdenTrabajoDetalle.InsertOnSubmit(oDetail);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion


                            #region Agregar en la pestaña de dispositivos el mantenimiento generado()
                            var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                            if (resultadoByMto != null)
                            {
                                if (resultadoByMto.ToList().Count == 0)
                                {
                                    // es su primer registro
                                    SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                    oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                    oDispositivoMantenimiento.item = "001";
                                    oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                    oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                    oDispositivoMantenimiento.desde = oRegistro.fecha;
                                    oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                    oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                    oDispositivoMantenimiento.estado = 1;
                                    oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                    oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                    oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                    Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                    Modelo.SubmitChanges();
                                }
                                else if (resultadoByMto.ToList().Count > 0)
                                {
                                    // Evaluar que número de item le corresponde
                                    SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                    oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                    int ultimo = Convert.ToInt32(resultadoByMto.Max(x => x.item)) + 1;
                                    oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                                    oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                    oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                    oDispositivoMantenimiento.desde = oRegistro.fecha;
                                    oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                    oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                    oDispositivoMantenimiento.estado = 1;
                                    oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                    oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                    oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                    Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                    Modelo.SubmitChanges();
                                }
                            }
                            #endregion

                            #region Cambiar estado del dispositivo a en martenimiento()
                            if (oRegistro.idTipoMantenimiento == "002")
                            {
                                // esta con mantenimiento correctivo 
                                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                                var result02 = Modelo.SAS_Dispostivo.Where(x => x.id == oRegistro.idDispositivo).ToList();
                                if (result02 != null)
                                {
                                    if (result02.ToList().Count == 1)
                                    {
                                        oDevice = result02.Single();
                                        oDevice.estado = 3;
                                        Modelo.SubmitChanges();
                                    }
                                }

                            }
                            #endregion


                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.codigo == 0)
                            {
                                #region Nuevo()
                                #region Registrar OT() 
                                oRegistro = new SAS_DispositivoOrdenTrabajo();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                //oRegistro.codigo = item.codigo;
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idDispositivo = item.idDispositivo;
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
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.Cerrado = item.Cerrado;
                                oRegistro.IdReferencia = item.IdReferencia;
                                oRegistro.EstadoCerradoDispositivo = item.EstadoCerradoDispositivo;
                                oRegistro.EsReprogracion = item.EsReprogracion;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
                                oRegistro.minutosProgramados = item.minutosProgramados;
                                oRegistro.CanalDeAtencionCodigo = item.CanalDeAtencionCodigo;

                                Modelo.SAS_DispositivoOrdenTrabajo.InsertOnSubmit(oRegistro);
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
                                            SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
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
                                            oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value);

                                            Modelo.SAS_DispositivoOrdenTrabajoDetalle.InsertOnSubmit(oDetail);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Agregar en la pestaña de dispositivos el mantenimiento generado()
                                var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                                if (resultadoByMto != null)
                                {
                                    if (resultadoByMto.ToList().Count == 0)
                                    {
                                        // es su primer registro
                                        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                        oDispositivoMantenimiento.item = "001";
                                        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                        oDispositivoMantenimiento.estado = 1;
                                        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                        Modelo.SubmitChanges();
                                    }
                                    else if (resultadoByMto.ToList().Count > 0)
                                    {
                                        // Evaluar que número de item le corresponde
                                        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                        int ultimo = Convert.ToInt32(resultadoByMto.Max(x => x.item)) + 1;
                                        oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                                        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                        oDispositivoMantenimiento.estado = 1;
                                        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                        Modelo.SubmitChanges();
                                    }
                                }
                                #endregion

                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                #region Actualizar OT() 
                                oRegistro = new SAS_DispositivoOrdenTrabajo();
                                oRegistro = resultado.Single();
                                oRegistro.codigoPersonal = item.codigoPersonal;
                                oRegistro.idSerie = item.idSerie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.fecha = item.fecha;
                                oRegistro.periodo = item.fecha.ToString("MM") + item.fecha.ToString("yyyy");
                                oRegistro.idTipoMantenimiento = item.idTipoMantenimiento;
                                oRegistro.Observación = item.Observación;
                                oRegistro.idEstado = item.idEstado;
                                oRegistro.idDispositivo = item.idDispositivo;
                                //oRegistro.usuario = item.usuario;
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
                                oRegistro.prioridad = item.prioridad;
                                oRegistro.Glosa01 = item.Glosa01;
                                oRegistro.Glosa02 = item.Glosa02;
                                oRegistro.Glosa03 = item.Glosa03;
                                oRegistro.Glosa04 = item.Glosa04;
                                oRegistro.Glosa05 = item.Glosa05;
                                oRegistro.Cerrado = item.Cerrado;
                                oRegistro.IdReferencia = item.IdReferencia;
                                oRegistro.EstadoCerradoDispositivo = item.EstadoCerradoDispositivo;
                                oRegistro.EsReprogracion = item.EsReprogracion;
                                oRegistro.cerradoEnPrimeraAtencion = item.cerradoEnPrimeraAtencion;
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
                                            SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
                                            var resultadoPorEliminar = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == detalle.codigo && x.item == detalle.item).ToList();
                                            if (resultadoPorEliminar != null)
                                            {
                                                if (resultadoPorEliminar.ToList().Count == 1)
                                                {
                                                    oDetail = resultadoPorEliminar.Single();
                                                    Modelo.SAS_DispositivoOrdenTrabajoDetalle.DeleteOnSubmit(oDetail);
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

                                            var resultadoParaAgregaroEditar = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == detalle.codigo && x.item == detalle.item).ToList();

                                            if (resultadoParaAgregaroEditar != null)
                                            {
                                                if (resultadoParaAgregaroEditar.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle() 
                                                    SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
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
                                                    oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value);

                                                    Modelo.SAS_DispositivoOrdenTrabajoDetalle.InsertOnSubmit(oDetail);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoParaAgregaroEditar.ToList().Count == 1)
                                                {
                                                    #region Editar() 
                                                    SAS_DispositivoOrdenTrabajoDetalle oDetail = new SAS_DispositivoOrdenTrabajoDetalle();
                                                    oDetail = resultadoParaAgregaroEditar.Single();
                                                    oDetail.accion = detalle.accion;
                                                    oDetail.desde = detalle.desde;
                                                    oDetail.hasta = detalle.hasta;
                                                    oDetail.estado = detalle.estado;
                                                    oDetail.usuario = detalle.usuario;
                                                    oDetail.costoUSD = detalle.costoUSD;
                                                    oDetail.HorasFormatoPlanilla = detalle.HorasFormatoPlanilla;
                                                    oDetail.HorasFormatoReloj = model.ObtenerHoraBase10ABase6(detalle.HorasFormatoPlanilla.Value);
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
                                var resultadoByMto = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo && x.codigoOrdenTrabajo == oRegistro.codigo).ToList();
                                var resultadoByMtoTotal = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == oRegistro.idDispositivo).ToList();
                                int ultimo = Convert.ToInt32(resultadoByMtoTotal.Max(x => x.item)) + 1;

                                if (resultadoByMto != null)
                                {
                                    if (resultadoByMto.ToList().Count == 0)
                                    {
                                        // es su primer registro
                                        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                        oDispositivoMantenimiento.codigoDispositivo = oRegistro.idDispositivo;
                                        oDispositivoMantenimiento.item = ultimo.ToString().PadLeft(3, '0');
                                        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                        oDispositivoMantenimiento.codigoColaborador = oRegistro.codigoPersonal;
                                        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                        oDispositivoMantenimiento.hasta = oRegistro.fecha;
                                        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                        oDispositivoMantenimiento.estado = 1;
                                        oDispositivoMantenimiento.seVisualizaEnReportes = 1;
                                        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                        oDispositivoMantenimiento.codigoOrdenTrabajo = oRegistro.codigo;
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                        Modelo.SubmitChanges();
                                    }
                                    else if (resultadoByMto.ToList().Count == 1)
                                    {
                                        // Evaluar que número de item le corresponde
                                        SAS_DispositivoMovimientoMantenimientos oDispositivoMantenimiento = new SAS_DispositivoMovimientoMantenimientos();
                                        oDispositivoMantenimiento = resultadoByMto.Single();
                                        oDispositivoMantenimiento.codigoTipoManteniento = oRegistro.idTipoMantenimiento;
                                        oDispositivoMantenimiento.desde = oRegistro.fecha;
                                        oDispositivoMantenimiento.observacion = oRegistro.Observación;
                                        oDispositivoMantenimiento.usuario = oRegistro.usuario;
                                        //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(oDispositivoMantenimiento);
                                        Modelo.SubmitChanges();
                                    }
                                }
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

        public int ArchivarCaso(string conection, SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "AN" || item.idEstado != "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado == 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoOrdenTrabajoDetalle itemDetalle = new SAS_DispositivoOrdenTrabajoDetalle();
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
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == codigoSolicitud).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        SAS_DispositivoOrdenTrabajo itemNuevo = new SAS_DispositivoOrdenTrabajo();                        
                        item = result01.ElementAt(0);
                        DateTime fechaAReasignar = item.fecha;
                        item.idEstado = "RR";
                        Modelo.SubmitChanges();


                        itemNuevo = result01.ElementAt(0);
                        itemNuevo.codigo = 0;
                        itemNuevo.idEstado = "PE";
                        itemNuevo.Observación = "REPROGRAMACION | " + (item.Observación).Trim();
                        itemNuevo.fecha = fechaAReasignar.AddDays(1);
                        itemNuevo.fechaEstimadaFinalizacion = fechaAReasignar.AddDays(1);

                        List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalle = new List<SAS_DispositivoOrdenTrabajoDetalle>();
                        List<SAS_DispositivoOrdenTrabajoDetalle> listadoDetalleEliminado = new List<SAS_DispositivoOrdenTrabajoDetalle>();
                        var newListado = Modelo.SAS_DispositivoSoporteFuncionalDetalle.Where(x => x.codigo == codigoSolicitud).ToList();
                        if (newListado != null || newListado.ToList().Count > 0)
                        {

                            listadoDetalle = (from detalle in newListado
                                              where detalle.codigo > 0 && detalle.item != string.Empty
                                              group detalle by new { detalle.item, detalle.codigo } into j
                                              select new SAS_DispositivoOrdenTrabajoDetalle
                                              {
                                                  codigo = 0,
                                                  item = j.Key.item,
                                                  accion = j.FirstOrDefault().accion != null ? j.FirstOrDefault().accion : string.Empty,
                                                  desde = fechaAReasignar,
                                                  hasta = fechaAReasignar,
                                                  estado = j.FirstOrDefault().estado != (byte?)null ? j.FirstOrDefault().estado : (byte?)null,
                                                  usuario = j.FirstOrDefault().usuario != null ? j.FirstOrDefault().usuario : string.Empty,
                                                  costoUSD = j.FirstOrDefault().costoUSD != (decimal?)null ? j.FirstOrDefault().costoUSD : (decimal?)null,
                                                  HorasFormatoReloj = j.FirstOrDefault().HorasFormatoReloj != (decimal?)null ? j.FirstOrDefault().HorasFormatoReloj : (decimal?)null,
                                                  HorasFormatoPlanilla = j.FirstOrDefault().HorasFormatoPlanilla != (decimal?)null ? j.FirstOrDefault().HorasFormatoPlanilla : (decimal?)null,
                                                  fechacreacion = DateTime.Now,
                                                  glosa = j.FirstOrDefault().glosa != null ? j.FirstOrDefault().glosa : string.Empty,                                                  
                                              }).ToList();
                        }

                        SAS_DispositivoOrdenTrabajoController claseMadre = new SAS_DispositivoOrdenTrabajoController();
                        resultado = claseMadre.RegisterObject(conection, itemNuevo, listadoDetalleEliminado, listadoDetalle);

                    }
                }
                #endregion
            }
            return resultado;
        }


        public int FinalizarCaso(string conection, SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "AN" || item.idEstado != "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado == 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoOrdenTrabajoDetalle itemDetalle = new SAS_DispositivoOrdenTrabajoDetalle();
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

        public int ReAperturarCaso(string conection, SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "TP" || item.idEstado != "PE")
                        {
                            var result02 = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado != 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoOrdenTrabajoDetalle itemDetalle = new SAS_DispositivoOrdenTrabajoDetalle();
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

        public int CambiarAEstadoReprogramado(string conection, SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        item = result01.ElementAt(0);
                        item.idEstado = "RR";
                        Modelo.SubmitChanges();
                    }
                }
                #endregion
            }
            return resultado;
        }


        

        public int ActivarTarea(string conection, SAS_ListadoDeDispositivoOrdenTrabajoByPeriodosResult selectedItem)
        {
            int resultado = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                #region 
                var result01 = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == selectedItem.codigo).ToList();

                if (result01 != null)
                {
                    if (result01.ToList().Count == 1)
                    {
                        SAS_DispositivoOrdenTrabajo item = new SAS_DispositivoOrdenTrabajo();
                        item = result01.ElementAt(0);
                        if (item.idEstado != "AN" || item.idEstado != "AT")
                        {
                            var result02 = Modelo.SAS_DispositivoOrdenTrabajoDetalle.Where(x => x.codigo == selectedItem.codigo).ToList();

                            var result03 = result02.Where(x => x.estado == 1).ToList();

                            if (result03 != null)
                            {
                                if (result03.ToList().Count > 0)
                                {
                                    foreach (var itemAFinalizar in result03)
                                    {
                                        SAS_DispositivoOrdenTrabajoDetalle itemDetalle = new SAS_DispositivoOrdenTrabajoDetalle();
                                        itemDetalle = itemAFinalizar;
                                        itemDetalle.estado = 3;
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

        public int ObtenerUltimoOperacion(string conection)
        {
            int ultimo = 1;
            int correlativo = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                correlativo = Modelo.SAS_DispositivoOrdenTrabajo.ToList().Count > 0 ? Modelo.SAS_DispositivoOrdenTrabajo.Max(x => x.codigo) : 0;
            }

            return correlativo + ultimo;
        }

        public int ChangeState(string conection, SAS_DispositivoOrdenTrabajo item)
        {
            SAS_DispositivoOrdenTrabajo oregistro = new SAS_DispositivoOrdenTrabajo();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == item.codigo).ToList();
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

        public int DeleteRecord(string conection, SAS_DispositivoOrdenTrabajo item)
        {
            SAS_DispositivoOrdenTrabajo oregistro = new SAS_DispositivoOrdenTrabajo();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoOrdenTrabajo.Where(x => x.codigo == item.codigo).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.idEstado == "PE" || oregistro.idEstado == "AN")
                        {
                            Modelo.SAS_DispositivoOrdenTrabajo.DeleteOnSubmit(oregistro);
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }



        public List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult> GetListDetalleByCodigoMantenimiento(string conection, int codigo)
        {

            List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult> list = new List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByID(codigo).ToList();
            }
            return list.OrderBy(x => x.item).ToList();
        }

        public List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult> GetListDetalleHerramientasByID(string conection, int codigo)
        {

            List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult> list = new List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeDispositivoOrdenTrabajoDetalleHerramientasByID(codigo).ToList();
            }
            return list.OrderBy(x => x.item).ToList();
        }

        public List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult> GetListDetalleSuministroByID(string conection, int codigo)
        {

            List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult> list = new List<SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeDispositivoOrdenTrabajoDetalleSuministroAlmacenByID(codigo).ToList();
            }
            return list.OrderBy(x => x.item).ToList();
        }

        public void Notify(string conection, string Para, string Asunto, int codigoSolicitudSelecionada)
        {
            List<SAS_ListadoDeDispositivoOrdenTrabajoByIdResult> listadoCabecera = new List<SAS_ListadoDeDispositivoOrdenTrabajoByIdResult>();
            List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult> listadoDetalle = new List<SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByIDResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listadoCabecera = Modelo.SAS_ListadoDeDispositivoOrdenTrabajoById(codigoSolicitudSelecionada).ToList();
                listadoDetalle = Modelo.SAS_ListadoDEDispositivosByOrdenTrabajoDetalleByID(codigoSolicitudSelecionada).ToList();
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
                Mensaje.Append(string.Format("Datos del colaborador : " + listadoCabecera.ElementAt(0).codigoPersonal + " - " + listadoCabecera.ElementAt(0).colaborador + "\n"));
                Mensaje.Append(string.Format("Descripción : " + listadoCabecera.ElementAt(0).observacion + "\n"));
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

                //SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
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

        public List<SAS_ListadoDeDispositivosByIdDeviceResult> ObtenerClaseDispositivo(string conection, int? codigoDispositivo)
        {
            List<SAS_ListadoDeDispositivosByIdDeviceResult> result = new List<SAS_ListadoDeDispositivosByIdDeviceResult>();
            

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDeDispositivosByIdDevice(codigoDispositivo != (int?)null ? codigoDispositivo : -3).ToList();
            }
            return result;
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
