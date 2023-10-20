using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;
using Asistencia;


namespace Asistencia.Negocios
{
    public class RegistroAbastecimientoController
    {


        public List<ListadoAcopioByTiktesResult> ObtenerListadoRecepcionEntrePeriodos(string conection, string desde, string hasta)
        {
            List<ListadoAcopioByTiktesResult> listado = new List<ListadoAcopioByTiktesResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                listado = Modelo.ListadoAcopioByTiktes(desde, hasta).ToList();
            }

            return listado;
        }


        public RegistroAbastecimiento ToRegister(string conection, RegistroAbastecimiento oRegistroAbastecimiento, List<RegistroAbastecimientoDetalle> listadoDetalle)
        {
            RegistroAbastecimiento oRegistro = new RegistroAbastecimiento();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {

                if (listadoDetalle != null && listadoDetalle.ToList().Count > 0)
                {
                    #region Grabar() 

                    if (oRegistroAbastecimiento != null)
                    {
                        if (oRegistroAbastecimiento.Idingresosalidaacopio != null && oRegistroAbastecimiento.item != null)
                        {
                            var oAbastecimiento = Modelo.RegistroAbastecimiento.Where(x =>
                            x.Idingresosalidaacopio == oRegistroAbastecimiento.Idingresosalidaacopio &&
                            x.item == oRegistroAbastecimiento.item &&
                            x.correlativo == oRegistroAbastecimiento.correlativo).ToList();

                            if (oAbastecimiento.Count == 0)
                            {
                                #region Nuevo()
                                RegistroAbastecimiento registro = new RegistroAbastecimiento();
                                //oAbastecimiento.correlativo = 0;
                                registro.Idingresosalidaacopio = oRegistroAbastecimiento.Idingresosalidaacopio.Trim();
                                registro.item = oRegistroAbastecimiento.item.Trim();
                                registro.fechaRegistro = oRegistroAbastecimiento.fechaRegistro;
                                registro.cantidad = oRegistroAbastecimiento.cantidad;
                                registro.hora = oRegistroAbastecimiento.hora;
                                registro.tipoRegistro = oRegistroAbastecimiento.tipoRegistro;
                                Modelo.RegistroAbastecimiento.InsertOnSubmit(registro);
                                Modelo.SubmitChanges();

                                oRegistro = registro;

                                foreach (var item in listadoDetalle)
                                {
                                    #region Registrar detalle()
                                    var detalleAsistencia = Modelo.RegistroAbastecimientoDetalle.Where(x => x.itemDetalle == item.itemDetalle).ToList();
                                    if (oAbastecimiento.Count == 0)
                                    {
                                        #region                                         
                                        int codigoTicktReservadoAnterior = 0;

                                        RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        //det.itemDetalle = item.itemDetalle;
                                        det.correlativo = oRegistro.correlativo;
                                        det.Idingresosalidaacopio = item.Idingresosalidaacopio;
                                        det.item = item.item;
                                        det.fechaRegistro = item.fechaRegistro;
                                        det.cantidad = item.cantidad;
                                        det.impreso = item.impreso;
                                        det.tipoCultivo = item.tipoCultivo;
                                        int? codigoTicktReservadoPosterior = (item.idTicketReservado != (int?)null ? item.idTicketReservado : 0);
                                        det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        Modelo.RegistroAbastecimientoDetalle.InsertOnSubmit(det);
                                        Modelo.SubmitChanges();

                                        // de 0 paso 9000
                                        if (codigoTicktReservadoAnterior != det.idTicketReservado)
                                        {
                                            #region
                                            if (codigoTicktReservadoAnterior == 0 && codigoTicktReservadoPosterior > 0)
                                            {
                                                #region Asociar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoPosterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 1;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            else if (codigoTicktReservadoAnterior >= 0 && codigoTicktReservadoPosterior == 0)
                                            {
                                                #region Liberar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoAnterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 0;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                        //RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        ////det.itemDetalle = item.itemDetalle;
                                        //det.correlativo = registro.correlativo;
                                        //det.Idingresosalidaacopio = item.Idingresosalidaacopio;
                                        //det.item = item.item;
                                        //det.fechaRegistro = item.fechaRegistro;
                                        //det.cantidad = item.cantidad;
                                        //det.tipoCultivo = item.tipoCultivo;
                                        //det.impreso = item.impreso;
                                        //det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        //Modelo.RegistroAbastecimientoDetalle.InsertOnSubmit(det);
                                        //Modelo.SubmitChanges();
                                    }
                                    else if (oAbastecimiento.Count == 1)
                                    {
                                        #region 
                                        RegistroAbastecimientoDetalle detSelecionado = new RegistroAbastecimientoDetalle();
                                        detSelecionado = detalleAsistencia.ElementAt(0);
                                        int codigoTicktReservadoAnterior = detSelecionado.idTicketReservado.Value != (int?)null ? detSelecionado.idTicketReservado.Value : 0;

                                        RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        det = detalleAsistencia.Single();
                                        det.fechaRegistro = item.fechaRegistro;
                                        det.cantidad = item.cantidad;
                                        det.impreso = item.impreso;
                                        det.tipoCultivo = item.tipoCultivo;
                                        int? codigoTicktReservadoPosterior = (item.idTicketReservado != (int?)null ? item.idTicketReservado : 0);
                                        det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        Modelo.SubmitChanges();

                                        // de 0 paso 9000
                                        if (codigoTicktReservadoAnterior != det.idTicketReservado)
                                        {
                                            #region 
                                            if (codigoTicktReservadoAnterior == 0 && codigoTicktReservadoPosterior > 0)
                                            {
                                                #region Asociar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoPosterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 1;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            else if (codigoTicktReservadoAnterior >= 0 && codigoTicktReservadoPosterior == 0)
                                            {
                                                #region Liberar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoAnterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 0;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                        //RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        //det = detalleAsistencia.Single();
                                        //det.fechaRegistro = item.fechaRegistro;
                                        //det.cantidad = item.cantidad;
                                        //det.tipoCultivo = item.tipoCultivo;
                                        //det.impreso = item.impreso;
                                        //det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        //Modelo.SubmitChanges();
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                            else if (oAbastecimiento.Count == 1)
                            {
                                #region Modificar()
                                RegistroAbastecimiento registro = new RegistroAbastecimiento();
                                registro = oAbastecimiento.Single();
                                registro.fechaRegistro = oRegistroAbastecimiento.fechaRegistro;
                                registro.cantidad = oRegistroAbastecimiento.cantidad;
                                Modelo.SubmitChanges();
                                oRegistro = registro;

                                foreach (var item in listadoDetalle)
                                {
                                    #region Registrar detalle()
                                    var detalleAsistencia = Modelo.RegistroAbastecimientoDetalle.Where(x => x.itemDetalle == item.itemDetalle).ToList();
                                    if (detalleAsistencia.Count == 0)
                                    {
                                        #region                                         
                                        int codigoTicktReservadoAnterior = 0;

                                        RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        //det.itemDetalle = item.itemDetalle;
                                        det.correlativo = item.correlativo;
                                        det.Idingresosalidaacopio = item.Idingresosalidaacopio;
                                        det.item = item.item;
                                        det.fechaRegistro = item.fechaRegistro;
                                        det.cantidad = item.cantidad;
                                        det.impreso = item.impreso;
                                        det.tipoCultivo = item.tipoCultivo;
                                        int? codigoTicktReservadoPosterior = (item.idTicketReservado != (int?)null ? item.idTicketReservado : 0);
                                        det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        Modelo.RegistroAbastecimientoDetalle.InsertOnSubmit(det);
                                        Modelo.SubmitChanges();

                                        // de 0 paso 9000
                                        if (codigoTicktReservadoAnterior != det.idTicketReservado)
                                        {
                                            #region
                                            if (codigoTicktReservadoAnterior == 0 && codigoTicktReservadoPosterior > 0)
                                            {
                                                #region Asociar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoPosterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 1;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            else if (codigoTicktReservadoAnterior >= 0 && codigoTicktReservadoPosterior == 0)
                                            {
                                                #region Liberar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoAnterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 0;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    else if (detalleAsistencia.Count == 1)
                                    {
                                        #region 
                                        RegistroAbastecimientoDetalle detSelecionado = new RegistroAbastecimientoDetalle();
                                        detSelecionado = detalleAsistencia.ElementAt(0);
                                        int codigoTicktReservadoAnterior = detSelecionado.idTicketReservado.Value != (int?)null ? detSelecionado.idTicketReservado.Value : 0;

                                        RegistroAbastecimientoDetalle det = new RegistroAbastecimientoDetalle();
                                        det = detalleAsistencia.Single();
                                        det.fechaRegistro = item.fechaRegistro;
                                        det.cantidad = item.cantidad;
                                        det.impreso = item.impreso;
                                        det.tipoCultivo = item.tipoCultivo;
                                        int? codigoTicktReservadoPosterior = (item.idTicketReservado != (int?)null ? item.idTicketReservado : 0);
                                        det.idTicketReservado = (item.idTicketReservado != (int?)null ? item.idTicketReservado : (int?)null);
                                        Modelo.SubmitChanges();

                                        // de 0 paso 9000
                                        if (codigoTicktReservadoAnterior != det.idTicketReservado)
                                        {
                                            #region 
                                            if (codigoTicktReservadoAnterior == 0 && codigoTicktReservadoPosterior > 0)
                                            {
                                                #region Asociar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoPosterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 1;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            else if (codigoTicktReservadoAnterior >= 0 && codigoTicktReservadoPosterior == 0)
                                            {
                                                #region Liberar ticket Reservado()
                                                var tickerReservadoEnQuery = Modelo.TicketReservado.Where(x => x.id == codigoTicktReservadoAnterior).ToList();
                                                if (tickerReservadoEnQuery.Count > 0)
                                                {
                                                    TicketReservado oTicketReservado = new TicketReservado();
                                                    oTicketReservado = tickerReservadoEnQuery.ElementAt(0);
                                                    oTicketReservado.estaAsociado = 0;
                                                    Modelo.SubmitChanges();
                                                }
                                                #endregion
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }

                                #endregion
                            }

                        }
                    }
                    #endregion
                }
            }

            return oRegistro;
        }


        public int ObtenerCorrelativoDesdeIngresoAcopioPorItem(string conection, ListadoAcopioByTiktesResult oRegistroAbastecimiento)
        {
            int CorrelativoTicketAbastecimiento = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                if (oRegistroAbastecimiento != null)
                {
                    if (oRegistroAbastecimiento.IDINGRESOSALIDAACOPIOCAMPO != null)
                    {
                        if (oRegistroAbastecimiento.IDINGRESOSALIDAACOPIOCAMPO != "")
                        {
                            if (oRegistroAbastecimiento.item != null)
                            {
                                if (oRegistroAbastecimiento.item != string.Empty)
                                {
                                    var obtenerResultado = Modelo.RegistroAbastecimiento.Where(x => x.Idingresosalidaacopio == oRegistroAbastecimiento.IDINGRESOSALIDAACOPIOCAMPO && x.item == oRegistroAbastecimiento.item).ToList();
                                    if (obtenerResultado != null)
                                    {
                                        if (obtenerResultado.ToList().Count() == 1)
                                        {
                                            CorrelativoTicketAbastecimiento = obtenerResultado.FirstOrDefault().correlativo;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



            }

            return CorrelativoTicketAbastecimiento;
        }


        public List<RegistroAbastecimientoDetalle> ObtenerDetalleTicketByCorrelativo(string conection, int correlativo)
        {

            List<RegistroAbastecimientoDetalle> listado = new List<RegistroAbastecimientoDetalle>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listado = Modelo.RegistroAbastecimientoDetalle.Where(x => x.correlativo == correlativo).ToList();

            }

            return listado;
        }

        public int EliminarTicketGenerado(string conection, int idTicketCabecera)
        {
            int ResultadoAccion = 0;
            List<RegistroAbastecimiento> ListadoTicket = new List<RegistroAbastecimiento>();
            List<RegistroAbastecimientoDetalle> ListadoTicketsDetalle = new List<RegistroAbastecimientoDetalle>();
            List<RegistroAbastecimientoDDetalle> ListadoTicketsDetalleLeidosPorPDA = new List<RegistroAbastecimientoDDetalle>();
            List<IngresoSalidaGasificado> ListadoTicketsDetalleLeidosParaGasificado = new List<IngresoSalidaGasificado>();
            List<SAS_RegistroTicketCamaraGasificadoExonerados> ListadoTicketsDetalleExoneradosParaGasificado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();


            string cnx = ConfigurationManager.AppSettings["NSFAJA"].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                // CABECERA DE TIECKET
                ListadoTicket = Modelo.RegistroAbastecimiento.Where(x => x.correlativo == idTicketCabecera).ToList();
                if (ListadoTicket != null && ListadoTicket.ToList().Count > 0)
                {
                    foreach (var TicketCreado in ListadoTicket)
                    {
                        RegistroAbastecimiento TicketCabecera = new RegistroAbastecimiento();
                        TicketCabecera = TicketCreado;

                        // DETALLES DE TICKET
                        ListadoTicketsDetalle = Modelo.RegistroAbastecimientoDetalle.Where(x => x.correlativo == TicketCabecera.correlativo).ToList();
                        if (ListadoTicketsDetalle != null && ListadoTicketsDetalle.ToList().Count > 0)
                        {
                            foreach (var TicketDetalleCreado in ListadoTicketsDetalle)
                            {
                                RegistroAbastecimientoDetalle TicketDetalle = new RegistroAbastecimientoDetalle();
                                TicketDetalle = TicketDetalleCreado;

                                //Eliminar Tickets Leidos a lineas de proceso                                                               
                                ListadoTicketsDetalleLeidosPorPDA = new List<RegistroAbastecimientoDDetalle>();
                                ListadoTicketsDetalleLeidosPorPDA = Modelo.RegistroAbastecimientoDDetalle.Where(x => x.itemDetalle == TicketDetalleCreado.itemDetalle).ToList();
                                if (ListadoTicketsDetalleLeidosPorPDA != null && ListadoTicketsDetalleLeidosPorPDA.ToList().Count > 0)
                                {
                                    #region Lineas() 
                                    foreach (var TicketDetalleLeidoEnLineas in ListadoTicketsDetalleLeidosPorPDA)
                                    {
                                        RegistroAbastecimientoDDetalle TicketLeido = new RegistroAbastecimientoDDetalle();
                                        TicketLeido = TicketDetalleLeidoEnLineas;

                                        // Agregar a log de ticket eliminados
                                        RegistroAbastecimientoDDetalleEliminado TicketAEliminar = new RegistroAbastecimientoDDetalleEliminado();
                                        TicketAEliminar.itemDetalle = TicketLeido.itemDetalle;
                                        TicketAEliminar.fechaRegistro = DateTime.Now;
                                        TicketAEliminar.cantidad = TicketLeido.cantidad;
                                        TicketAEliminar.hora = TicketLeido.hora;
                                        TicketAEliminar.idMovil = TicketLeido.idMovil;
                                        TicketAEliminar.idLinea = TicketLeido.idLinea;
                                        TicketAEliminar.esOrganico = TicketLeido.esOrganico;
                                        TicketAEliminar.usuario = Environment.UserName.Trim();
                                        TicketAEliminar.host = TicketLeido.host;
                                        Modelo.RegistroAbastecimientoDDetalleEliminado.InsertOnSubmit(TicketAEliminar);
                                        Modelo.SubmitChanges();

                                        Modelo.RegistroAbastecimientoDDetalle.DeleteOnSubmit(TicketLeido);
                                        Modelo.SubmitChanges();
                                    }
                                    #endregion
                                }

                                // Tickets en Cámaras de gasificado
                                ListadoTicketsDetalleLeidosParaGasificado = new List<IngresoSalidaGasificado>();
                                ListadoTicketsDetalleLeidosParaGasificado = Modelo.IngresoSalidaGasificado.Where(x => x.itemDetalle == TicketDetalleCreado.itemDetalle).ToList();
                                if (ListadoTicketsDetalleLeidosParaGasificado != null && ListadoTicketsDetalleLeidosParaGasificado.ToList().Count > 0)
                                {
                                    #region Gasificado()
                                    foreach (var TicketLeidoEnGasificado in ListadoTicketsDetalleLeidosParaGasificado)
                                    {
                                        IngresoSalidaGasificado TicketGasificado = new IngresoSalidaGasificado();
                                        TicketGasificado = TicketLeidoEnGasificado;

                                        IngresoSalidaGasificadoEliminado TicketGasificadoAEliminar = new IngresoSalidaGasificadoEliminado();
                                        //TicketGasificadoAEliminar.id = 0;
                                        TicketGasificadoAEliminar.idIngresoSalidaGasificado = TicketLeidoEnGasificado.idGasificado.Value;
                                        TicketGasificadoAEliminar.idCamara = TicketLeidoEnGasificado.idCamara;
                                        TicketGasificadoAEliminar.itemDetalle = TicketLeidoEnGasificado.itemDetalle;
                                        TicketGasificadoAEliminar.fecha = DateTime.Now;
                                        TicketGasificadoAEliminar.tipoRegistro = TicketLeidoEnGasificado.tipoRegistro;
                                        TicketGasificadoAEliminar.estado = TicketLeidoEnGasificado.estado;
                                        TicketGasificadoAEliminar.tipo = TicketLeidoEnGasificado.tipo;
                                        Modelo.IngresoSalidaGasificadoEliminado.InsertOnSubmit(TicketGasificadoAEliminar);
                                        Modelo.SubmitChanges();


                                        Modelo.IngresoSalidaGasificado.DeleteOnSubmit(TicketGasificado);
                                        Modelo.SubmitChanges();

                                    }
                                    #endregion
                                }

                                // Ticket Exonerado
                                ListadoTicketsDetalleExoneradosParaGasificado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
                                ListadoTicketsDetalleExoneradosParaGasificado = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == TicketDetalleCreado.itemDetalle).ToList();
                                if (ListadoTicketsDetalleExoneradosParaGasificado != null && ListadoTicketsDetalleExoneradosParaGasificado.ToList().Count > 0)
                                {
                                    #region Ticket Exonerado() 
                                    foreach (SAS_RegistroTicketCamaraGasificadoExonerados ItemExonerado in ListadoTicketsDetalleExoneradosParaGasificado)
                                    {
                                        SAS_RegistroTicketCamaraGasificadoExonerados TicketExoneradoAEliminar = new SAS_RegistroTicketCamaraGasificadoExonerados();
                                        TicketExoneradoAEliminar = ItemExonerado;
                                        Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.DeleteOnSubmit(TicketExoneradoAEliminar);
                                        Modelo.SubmitChanges();

                                    }
                                    #endregion
                                }


                                Modelo.RegistroAbastecimientoDetalle.DeleteOnSubmit(TicketDetalle);
                                Modelo.SubmitChanges();

                            }
                        }

                        Modelo.RegistroAbastecimiento.DeleteOnSubmit(TicketCabecera);
                        Modelo.SubmitChanges();

                    }
                }


            }

            return ResultadoAccion;
        }
    }
}
