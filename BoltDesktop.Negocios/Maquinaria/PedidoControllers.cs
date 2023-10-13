using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class PedidoControllers
    {


        // Obtener el listado del procedimiento de seguimiento de pedidos para compra
        public List<SAS_SeguimientoPedidosParaCompraResult> ObternerListadoSeguimientoPorPeriodo(string conection, string desde, string hasta, string idPedido)
        {
            List<SAS_SeguimientoPedidosParaCompraResult> resultado = new List<SAS_SeguimientoPedidosParaCompraResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                resultado = Modelo.SAS_SeguimientoPedidosParaCompra(desde, hasta, idPedido).ToList();
            }

                return resultado;
        }


        public List<SAS_SeguimientoPedidosCompraFullResult> ObternerListadoSeguimientoPorPeriodoCompleto(string conection, string desde, string hasta, string idPedido)
        {
            List<SAS_SeguimientoPedidosCompraFullResult> resultado = new List<SAS_SeguimientoPedidosCompraFullResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                resultado = Modelo.SAS_SeguimientoPedidosCompraFull().ToList();
            }

            return resultado;
        }

        


        public List<SAS_SeguimientoPedidosServicio2Result> ObternerSeguimientoPedidosServicio(string conection, string desde, string hasta, string idPedido)
        {
            List<SAS_SeguimientoPedidosServicio2Result> resultado = new List<SAS_SeguimientoPedidosServicio2Result>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                resultado = Modelo.SAS_SeguimientoPedidosServicio2("001","", desde, hasta).ToList();
            }

            return resultado;
        }

        public List<SAS_SeguimientoOrdenServicioResult> ObternerListadoOrdenesServicioPorPeriodo(string conection, string desde, string hasta, string idOperacion)
        {
            List<SAS_SeguimientoOrdenServicioResult> resultado = new List<SAS_SeguimientoOrdenServicioResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                resultado = Modelo.SAS_SeguimientoOrdenServicio(desde, hasta, idOperacion).ToList();
            }

            return resultado;
        }


        public List<SAS_SeguimientoOrdenCompraResult> ObternerListadoOrdenesCompraPorPeriodo(string conection, string desde, string hasta, string idOperacion)
        {
            List<SAS_SeguimientoOrdenCompraResult> resultado = new List<SAS_SeguimientoOrdenCompraResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                resultado = Modelo.SAS_SeguimientoOrdenCompra(desde, hasta, idOperacion).ToList();
            }

            return resultado;
        }

        



        public List<SAS_SeguimientoPedidosParaCompraResult> AgruparResultadoPorPedidoPorItem(string conection, string desde, string hasta, string idPedido)
        {
            List<SAS_SeguimientoPedidosParaCompraResult> resultado = new List<SAS_SeguimientoPedidosParaCompraResult>();

            return resultado;
        }

        // Obtener el listado del procedimiento de seguimiento de pedidos para compra
        // PEDIDO PARA COMPRA        
        public string ActualizarEstadoPedidoCompraDocumento(string conection, string idPedido, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.PEDIDO.Where(x => x.IDPEDIDO == idPedido).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    PEDIDO oPedido = new PEDIDO();
                    oPedido = Modelo.PEDIDO.Where(x => x.IDPEDIDO == idPedido).Single();
                    oPedido.IDESTADO = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }

        public string ActualizarEstadoPedidoCompraDetalleProductoEnDocumento(string conection, string idPedido, string itemPedido, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.DPEDIDO.Where(x => x.IDPEDIDO == idPedido && x.ITEM == itemPedido).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    DPEDIDO oDPedido = new DPEDIDO();
                    oDPedido = Modelo.DPEDIDO.Where(x => x.IDPEDIDO == idPedido && x.ITEM == itemPedido).Single();
                    oDPedido.IDESTADO = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }


        // PEDIDO DE SERVICIO
        public string ActualizarEstadoPedidoServicioDocumento(string conection, string idPedido, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.PEDIDOSERVICIOS.Where(x => x.IDPEDIDO == idPedido).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    PEDIDOSERVICIOS oPedido = new PEDIDOSERVICIOS();
                    oPedido = Modelo.PEDIDOSERVICIOS.Where(x => x.IDPEDIDO == idPedido).Single();
                    oPedido.IDESTADO = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }

        public string ActualizarEstadoPedidoServicioDetalleProductoEnDocumento(string conection, string idPedido, string itemPedido, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.DPEDIDOSERVICIOS.Where(x => x.IDPEDIDO == idPedido && x.ITEM == itemPedido).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    DPEDIDOSERVICIOS oDPedido = new DPEDIDOSERVICIOS();
                    oDPedido = Modelo.DPEDIDOSERVICIOS.Where(x => x.IDPEDIDO == idPedido && x.ITEM == itemPedido).Single();
                    oDPedido.IDESTADO = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }

        // ORDEN DE COMPRA

        public string ActualizarEstadoOrdenCompraDocumento(string conection, string idOperacion, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.ORDENCOMPRA.Where(x => x.idcompra == idOperacion).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    ORDENCOMPRA oPedido = new ORDENCOMPRA();
                    oPedido = Modelo.ORDENCOMPRA.Where(x => x.idcompra == idOperacion).Single();
                    oPedido.idestado = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }

        public string ActualizarEstadoOrdenCompraDetalleProductoEnDocumento(string conection, string idOperacion, string itemDetalle, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.DORDENCOMPRA.Where(x => x.idcompra == idOperacion && x.item == itemDetalle).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    DORDENCOMPRA oDetalleDocumento = new DORDENCOMPRA();
                    oDetalleDocumento = Modelo.DORDENCOMPRA.Where(x => x.idcompra == idOperacion && x.item == itemDetalle).Single();
                    oDetalleDocumento.idestado = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }


        // OREN DE SERVICIO
        public string ActualizarEstadoOrdenServicioDocumento(string conection, string idOperacion, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.ORDENSERVICIO.Where(x => x.idservicio == idOperacion).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.

                    ORDENSERVICIO oDocumento = new ORDENSERVICIO();
                    oDocumento = Modelo.ORDENSERVICIO.Where(x => x.idservicio == idOperacion).Single();
                    oDocumento.idestado = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }

        public string ActualizarEstadoOrdenServicioDetalleProductoEnDocumento(string conection, string idOperacion, string itemDetalle, string idEstadoNuevo)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999999;
                var resultado = Modelo.DORDENSERVICIO.Where(x => x.idservicio == idOperacion && x.item == itemDetalle).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.
                    Modelo.CommandTimeout = 99999999;
                    DORDENSERVICIO oDetalleDocumento = new DORDENSERVICIO();
                    oDetalleDocumento = Modelo.DORDENSERVICIO.Where(x => x.idservicio == idOperacion && x.item == itemDetalle).Single();
                    oDetalleDocumento.IDESTADO = idEstadoNuevo;
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }


            }

            return mensajeConfirmacion;
        }



    }
}
