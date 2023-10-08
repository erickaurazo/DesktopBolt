using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_RegistroGasificadoController
    {
        SAS_RegistroGasificadoController controller;

        public List<SAS_RegistroGasificadoAll> GetListRegistroGasificadoAll(string conection, string desde, string hasta)
        {
            List<SAS_RegistroGasificadoAll> resultado = new List<SAS_RegistroGasificadoAll>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificadoAll.Where(x => x.FECHA.Value >= Convert.ToDateTime(desde + " 00:00:00") && x.FECHA.Value <= Convert.ToDateTime(hasta + " 23:59:59")).ToList();
            }

            return resultado;
        }

        public List<SAS_RegistroGasificadoByDatesResult> GetListRegistroGasificadoByDates(string conection, string desde, string hasta)
        {
            List<SAS_RegistroGasificadoByDatesResult> resultado = new List<SAS_RegistroGasificadoByDatesResult>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificadoByDates(desde, hasta).ToList();
            }

            return resultado;
        }


        public List<SAS_IngresoSalidaGasificadoListadoByDatesResult> GetListRegistroGasificadoByDate(string conection, string desde, string hasta)
        {
            List<SAS_IngresoSalidaGasificadoListadoByDatesResult> resultado = new List<SAS_IngresoSalidaGasificadoListadoByDatesResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_IngresoSalidaGasificadoListadoByDates(desde, hasta).ToList();
            }

            return resultado;
        }

        public int ChangeStatus(string conection, SAS_RegistroGasificado item)
        {
            List<SAS_RegistroGasificado> resultado = new List<SAS_RegistroGasificado>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroGasificado oItem = new SAS_RegistroGasificado();
                    oItem = resultado.ElementAt(0);

                    if (oItem.estado == 0)
                    {
                        oItem.estado = 1;
                    }
                    else if (oItem.estado == 1)
                    {
                        oItem.estado = 0;
                    }
                    Modelo.SubmitChanges();
                }
            }

            return result;
        }

        public int Gasificar(string conection, SAS_RegistroGasificado item)
        {
            List<SAS_RegistroGasificado> resultado = new List<SAS_RegistroGasificado>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroGasificado oItem = new SAS_RegistroGasificado();
                    oItem = resultado.ElementAt(0);

                    if (oItem.estado == 2)
                    {
                        oItem.estado = 1;
                    }

                    Modelo.SubmitChanges();
                }
            }

            return result;
        }
        public int Duplicar(string conection, SAS_RegistroGasificado item)
        {
            List<SAS_RegistroGasificado> resultado = new List<SAS_RegistroGasificado>();
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroGasificado oItem = new SAS_RegistroGasificado(); // ORIGEN
                    SAS_RegistroGasificado oItemDuplicado = new SAS_RegistroGasificado(); // DESTINO
                    oItem = resultado.ElementAt(0);

                    oItemDuplicado = resultado.ElementAt(0);                    
                    oItemDuplicado.idGasificado = 0;
                    oItemDuplicado.estado = 1;
                    controller = new SAS_RegistroGasificadoController();
                    int ResultadoDelRegistroDuplicado = controller.ToRegister(conection, oItemDuplicado);                    
                }
            }
            return result;
        }

        public int LiberarTicketGasificado(string conection, IngresoSalidaGasificado item)
        {
            List<IngresoSalidaGasificado> resultado = new List<IngresoSalidaGasificado>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.IngresoSalidaGasificado.Where(x => x.itemDetalle == item.itemDetalle).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    IngresoSalidaGasificado oItem = new IngresoSalidaGasificado();
                    oItem = resultado.ElementAt(0);
                    Modelo.IngresoSalidaGasificado.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                }
            }

            return result;
        }

        public int LiberarTicketExonerado(string conection, SAS_RegistroTicketCamaraGasificadoExonerados item)
        {
            List<SAS_RegistroTicketCamaraGasificadoExonerados> resultado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == item.itemDetalle).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroTicketCamaraGasificadoExonerados oItem = new SAS_RegistroTicketCamaraGasificadoExonerados();
                    oItem = resultado.ElementAt(0);
                    Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                }
            }

            return result;
        }

        public int RegistrarTicketExonerado(string conection, SAS_RegistroTicketCamaraGasificadoExonerados item)
        {
            List<SAS_RegistroTicketCamaraGasificadoExonerados> resultado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == item.itemDetalle).ToList();

                if (resultado != null && resultado.ToList().Count == 0)
                {
                    SAS_RegistroTicketCamaraGasificadoExonerados oItem = new SAS_RegistroTicketCamaraGasificadoExonerados();
                    oItem.itemDDetalle = item.itemDDetalle != null ? item.itemDDetalle : 0;
                    oItem.itemDetalle = item.itemDetalle != null ? item.itemDDetalle : 0;
                    oItem.fechaRegistro = item.fechaRegistro != null ? item.fechaRegistro : DateTime.Now;
                    oItem.cantidad = item.cantidad != null ? item.cantidad.Value : 0;
                    oItem.hora = item.hora != null ? item.hora : DateTime.Now;
                    oItem.idMovil = item.idMovil != null ? item.idMovil.Trim() : "01";
                    oItem.idCamara = item.idCamara != null ? item.idCamara.Trim() : "999";
                    oItem.idusuario = item.idusuario != null ? item.idusuario.Trim() : "418648";
                    oItem.idmotivo = item.idmotivo != null ? item.idmotivo.Trim() : "003";
                    oItem.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                    oItem.idestado = item.idestado != null ? item.idestado : 0;
                    Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                }
            }
            return result;
        }

        public int CambiarMotivoDeTicketExonerado(string conection, SAS_RegistroTicketCamaraGasificadoExonerados item, string IdMotivo)
        {
            List<SAS_RegistroTicketCamaraGasificadoExonerados> resultado = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
            int result = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == item.itemDetalle).ToList();
                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroTicketCamaraGasificadoExonerados oItem = new SAS_RegistroTicketCamaraGasificadoExonerados();
                    oItem = resultado.ElementAt(0);
                    oItem.idmotivo = item.idmotivo != null ? item.idmotivo.Trim() : "003";
                    Modelo.SubmitChanges();
                }
            }
            return result;
        }

        public int FinalizarGasificado(string conection, SAS_RegistroGasificado item)
        {
            List<SAS_RegistroGasificado> resultado = new List<SAS_RegistroGasificado>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroGasificado oItem = new SAS_RegistroGasificado();
                    oItem = resultado.ElementAt(0);

                    if (oItem.estado == 1)
                    {
                        oItem.estado = 2;
                    }

                    Modelo.SubmitChanges();
                }
            }

            return result;
        }

        public int Eliminar(string conection, SAS_RegistroGasificado item)
        {
            List<SAS_RegistroGasificado> resultado = new List<SAS_RegistroGasificado>();
            List<IngresoSalidaGasificado> ListaDetalle = new List<IngresoSalidaGasificado>();
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();
                ListaDetalle = Modelo.IngresoSalidaGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();

                if (resultado != null && resultado.ToList().Count == 1)
                {
                    SAS_RegistroGasificado oItem = new SAS_RegistroGasificado();
                    oItem = resultado.ElementAt(0);
                    if (oItem.estado == 1 || oItem.estado == 2)
                    {
                        ListaDetalle = Modelo.IngresoSalidaGasificado.Where(x => x.idGasificado == item.idGasificado).ToList();
                        Modelo.IngresoSalidaGasificado.DeleteAllOnSubmit(ListaDetalle);
                        Modelo.SAS_RegistroGasificado.DeleteOnSubmit(oItem);
                    }
                    Modelo.SubmitChanges();
                }
            }

            return result;
        }



        public List<SAS_RegistroGasificadoAll> GetListRegistroGasificadoByIdGasigicado(string conection, int codigo)
        {
            List<SAS_RegistroGasificadoAll> resultado = new List<SAS_RegistroGasificadoAll>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificadoAll.Where(x => x.idGasificado == codigo).ToList();
            }

            return resultado;
        }

        //SAS_RegistroGasificadoAllByID

        public List<SAS_RegistroGasificadoAllByIDResult> GetListRegistroGasificadoByIdGasificado(string conection, int codigo)
        {
            List<SAS_RegistroGasificadoAllByIDResult> resultado = new List<SAS_RegistroGasificadoAllByIDResult>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroGasificadoAllByID(codigo).ToList();
            }

            return resultado;
        }

        public List<SAS_RegistroGasificadoAll> SetListRegistroGasificadoAll(List<SAS_RegistroGasificadoAll> result)
        {
            List<SAS_RegistroGasificadoAll> listAgrupada = new List<SAS_RegistroGasificadoAll>();
            if (result != null)
            {
                if (result.ToList().Count > 0)
                {

                    listAgrupada = (from item in result
                                    group item by new { item.guiaDeRemision, item.idGasificado } into j
                                    select new SAS_RegistroGasificadoAll
                                    {
                                        idGasificado = j.Key.idGasificado,
                                        idCamara = j.FirstOrDefault().idCamara,
                                        camara = j.FirstOrDefault().camara,
                                        dosisSO2 = j.FirstOrDefault().dosisSO2,
                                        tempAgua = j.FirstOrDefault().tempAgua,
                                        lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                        cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                        fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                        horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                        horaGasificado = j.FirstOrDefault().horaGasificado,
                                        horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                        fechaSalida = j.FirstOrDefault().fechaSalida,
                                        minutos = j.FirstOrDefault().minutos,
                                        idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                        estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                        productoAplicado = j.FirstOrDefault().productoAplicado,
                                        idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                        registradoPor = j.FirstOrDefault().registradoPor,
                                        ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                        AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                        idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                        itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                        fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,

                                        tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                        estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                        tipo = j.FirstOrDefault().tipo,
                                        idCampana = j.FirstOrDefault().idCampana,
                                        campaña = j.FirstOrDefault().campaña,
                                        IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                        item = j.FirstOrDefault().item,
                                        FECHA = j.FirstOrDefault().FECHA,
                                        IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                        sucursal = j.FirstOrDefault().sucursal,
                                        DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                        FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                        FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                        FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                        IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                        almacen = j.FirstOrDefault().almacen,
                                        IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                        consumidor = j.FirstOrDefault().consumidor,
                                        tipoProducto = j.FirstOrDefault().tipoProducto,
                                        IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                        DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                        IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                        PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                        PESONETO = j.FirstOrDefault().PESONETO,
                                        PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                        IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                        cultivo = j.FirstOrDefault().cultivo,
                                        IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                        variedad = j.FirstOrDefault().variedad,
                                        IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                        empaque = j.FirstOrDefault().empaque,
                                        NROJABAS = j.FirstOrDefault().NROJABAS,
                                        TARA1 = j.FirstOrDefault().TARA1,
                                        TARA = j.FirstOrDefault().TARA,
                                        TARA3 = j.FirstOrDefault().TARA3,
                                        naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                        hora = j.FirstOrDefault().hora,
                                        BALANZA = j.FirstOrDefault().BALANZA,
                                        estado = j.FirstOrDefault().estado,
                                        sector = j.FirstOrDefault().sector,
                                        correlativo = j.FirstOrDefault().correlativo,
                                        estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                        fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                        PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                        PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                        PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                        itemDetalle = 0,
                                        cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                        glosa = j.FirstOrDefault().glosa,
                                        chofer = j.FirstOrDefault().chofer,
                                        PLACA = j.FirstOrDefault().PLACA,
                                        NROENVIO = j.FirstOrDefault().NROENVIO,
                                        HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                        guiaDeRemision = j.Key.guiaDeRemision,                                       //
                                        semana = j.FirstOrDefault().semana,
                                        anio = j.FirstOrDefault().anio,
                                        empresaCodigo = j.FirstOrDefault().empresaCodigo,
                                        empresa = j.FirstOrDefault().empresa,
                                        sucursalCodigo = j.FirstOrDefault().sucursalCodigo,
                                        sucursalRegistroGasificado = j.FirstOrDefault().sucursalRegistroGasificado,
                                        registradoPorNombres = j.FirstOrDefault().registradoPorNombres,
                                        ValidadoPorNombres = j.FirstOrDefault().ValidadoPorNombres,
                                        AprobadoPorNombres = j.FirstOrDefault().AprobadoPorNombres

                                    }
                        ).ToList();

                }
            }

            return listAgrupada;
        }

        public List<SAS_RegistroGasificadoByDatesResult> SetListRegistroGasificadoAll(List<SAS_RegistroGasificadoByDatesResult> result)
        {
            List<SAS_RegistroGasificadoByDatesResult> listAgrupada = new List<SAS_RegistroGasificadoByDatesResult>();
            if (result != null)
            {
                if (result.ToList().Count > 0)
                {

                    listAgrupada = (from item in result
                                    group item by new { item.guiaDeRemision, item.idGasificado } into j
                                    select new SAS_RegistroGasificadoByDatesResult
                                    {
                                        idGasificado = j.Key.idGasificado,
                                        idCamara = j.FirstOrDefault().idCamara,
                                        camara = j.FirstOrDefault().camara,
                                        dosisSO2 = j.FirstOrDefault().dosisSO2,
                                        tempAgua = j.FirstOrDefault().tempAgua,
                                        lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                        cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                        fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                        horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                        horaGasificado = j.FirstOrDefault().horaGasificado,
                                        horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                        fechaSalida = j.FirstOrDefault().fechaSalida,
                                        minutos = j.FirstOrDefault().minutos,
                                        idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                        estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                        productoAplicado = j.FirstOrDefault().productoAplicado,
                                        idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                        registradoPor = j.FirstOrDefault().registradoPor,
                                        ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                        AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                        idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                        itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                        fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,

                                        tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                        estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                        tipo = j.FirstOrDefault().tipo,
                                        idCampana = j.FirstOrDefault().idCampana,
                                        campaña = j.FirstOrDefault().campaña,
                                        IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                        item = j.FirstOrDefault().item,
                                        FECHA = j.FirstOrDefault().FECHA,
                                        IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                        sucursal = j.FirstOrDefault().sucursal,
                                        DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                        FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                        FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                        FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                        IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                        almacen = j.FirstOrDefault().almacen,
                                        IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                        consumidor = j.FirstOrDefault().consumidor,
                                        tipoProducto = j.FirstOrDefault().tipoProducto,
                                        IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                        DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                        IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                        PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                        PESONETO = j.FirstOrDefault().PESONETO,
                                        PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                        IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                        cultivo = j.FirstOrDefault().cultivo,
                                        IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                        variedad = j.FirstOrDefault().variedad,
                                        IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                        empaque = j.FirstOrDefault().empaque,
                                        NROJABAS = j.FirstOrDefault().NROJABAS,
                                        TARA1 = j.FirstOrDefault().TARA1,
                                        TARA = j.FirstOrDefault().TARA,
                                        TARA3 = j.FirstOrDefault().TARA3,
                                        naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                        hora = j.FirstOrDefault().hora,
                                        BALANZA = j.FirstOrDefault().BALANZA,
                                        estado = j.FirstOrDefault().estado,
                                        sector = j.FirstOrDefault().sector,
                                        correlativo = j.FirstOrDefault().correlativo,
                                        estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                        fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                        PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                        PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                        PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                        itemDetalle = 0,
                                        cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                        glosa = j.FirstOrDefault().glosa,
                                        chofer = j.FirstOrDefault().chofer,
                                        PLACA = j.FirstOrDefault().PLACA,
                                        NROENVIO = j.FirstOrDefault().NROENVIO,
                                        HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                        guiaDeRemision = j.Key.guiaDeRemision,                                       //
                                        semana = j.FirstOrDefault().semana,
                                        anio = j.FirstOrDefault().anio,
                                        empresaCodigo = j.FirstOrDefault().empresaCodigo,
                                        empresa = j.FirstOrDefault().empresa,
                                        sucursalCodigo = j.FirstOrDefault().sucursalCodigo,
                                        sucursalRegistroGasificado = j.FirstOrDefault().sucursalRegistroGasificado,
                                        registradoPorNombres = j.FirstOrDefault().registradoPorNombres,
                                        ValidadoPorNombres = j.FirstOrDefault().ValidadoPorNombres,
                                        AprobadoPorNombres = j.FirstOrDefault().AprobadoPorNombres

                                    }
                        ).ToList();

                }
            }

            return listAgrupada;
        }

        public List<IngresoSalidaGasificado> GetDetailList(string conection, int idGasificado)
        {
            List<IngresoSalidaGasificado> result = new List<IngresoSalidaGasificado>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                result = Modelo.IngresoSalidaGasificado.Where(x => x.idGasificado == idGasificado).ToList();
            }

            return result;
        }

        public List<Grupo> GetListOfDocuments(string formName)
        {
            List<Grupo> result = new List<Grupo>();

            if (formName == "RegistroDeIngresoSalidaGasificadoEdicion")
            {
                result.Add(new Grupo { Descripcion = "GAS", Codigo = "GAS", Valor = "GAS" });
            }


            return result;
        }

        // get serial number list | obtener listado de numero de serie
        public List<Grupo> GetSerialNumberList(string formName)
        {
            List<Grupo> result = new List<Grupo>();

            if (formName == "RegistroDeIngresoSalidaGasificadoEdicion")
            {
                result.Add(new Grupo { Descripcion = "0001", Codigo = "0001", Valor = "0001" });
            }


            return result;
        }

        //obtain a list of gasification chambers | obtener listado de camaras de gasificado
        public List<Grupo> ObtainAListOfGasificacionChambers(string formName)
        {
            List<Grupo> result = new List<Grupo>();
            if (formName == "RegistroDeIngresoSalidaGasificadoEdicion")
            {
                result.Add(new Grupo { Descripcion = "-- Selecionar cámara --".ToUpper(), Codigo = "000", Valor = "000" });
                result.Add(new Grupo { Descripcion = "Cámara de gasificaco N° 001".ToUpper(), Codigo = "001", Valor = "001" });
                result.Add(new Grupo { Descripcion = "Cámara de gasificaco N° 002".ToUpper(), Codigo = "002", Valor = "002" });
            }
            return result;
        }

        public List<SAS_RegistroIngresoSalidaACamaraGasificado> GetListRegistroGasificadoNoLeidoAll(string conection, string desde, string hasta)
        {
            List<SAS_RegistroIngresoSalidaACamaraGasificado> resultado = new List<SAS_RegistroIngresoSalidaACamaraGasificado>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroIngresoSalidaACamaraGasificado.Where(x => x.fechaAcopio.Value >= Convert.ToDateTime(desde + " 00:00:00") && x.fechaAcopio.Value <= Convert.ToDateTime(hasta + " 23:59:59")).ToList();
            }

            return resultado;
        }

        public List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult> GetListRegistroGasificadoNoLeidoByDates(string conection, string desde, string hasta)
        {
            List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult> resultado = new List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_RegistroIngresoSalidaACamaraGasificadoByDates(desde, hasta).ToList();
            }
            return resultado;
        }

        public List<SAS_RegistroGasificadoAll> ResumirListado(List<SAS_RegistroGasificadoAll> result)
        {

            List<SAS_RegistroGasificadoAll> listado = new List<SAS_RegistroGasificadoAll>();

            if (result != null)
            {
                if (result.ToList().Count > 0)
                {
                    listado = (from item in result
                               group item by new { item.idGasificado, item.documentoGasificado } into j
                               select new SAS_RegistroGasificadoAll
                               {
                                   idGasificado = j.Key.idGasificado,
                                   fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                   fechaSalida = j.FirstOrDefault().fechaSalida,
                                   idCamara = j.FirstOrDefault().idCamara,
                                   camara = j.FirstOrDefault().camara,
                                   documentoGasificado = j.Key.documentoGasificado,
                                   dosisSO2 = j.FirstOrDefault().dosisSO2,
                                   tempAgua = j.FirstOrDefault().tempAgua,
                                   lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                   cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                   horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                   horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                   minutos = j.FirstOrDefault().minutos,
                                   idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                   estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                   productoAplicado = j.FirstOrDefault().productoAplicado,
                                   idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                   registradoPor = j.FirstOrDefault().registradoPor,
                                   ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                   AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                   idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                   itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                   fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,
                                   horaGasificado = j.FirstOrDefault().horaGasificado,
                                   tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                   estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                   tipo = j.FirstOrDefault().tipo,
                                   idCampana = j.FirstOrDefault().idCampana,
                                   campaña = j.FirstOrDefault().campaña,
                                   IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                   item = j.FirstOrDefault().item,
                                   FECHA = j.FirstOrDefault().FECHA,
                                   IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                   sucursal = j.FirstOrDefault().sucursal,
                                   DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                   FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                   FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                   FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                   IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                   almacen = j.FirstOrDefault().almacen,
                                   IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                   consumidor = j.FirstOrDefault().consumidor,
                                   tipoProducto = j.FirstOrDefault().tipoProducto,
                                   IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                   DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                   IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                   PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                   PESONETO = j.FirstOrDefault().PESONETO,
                                   PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                   IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                   cultivo = j.FirstOrDefault().cultivo,
                                   IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                   variedad = j.FirstOrDefault().variedad,
                                   IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                   empaque = j.FirstOrDefault().empaque,
                                   NROJABAS = j.FirstOrDefault().NROJABAS,
                                   TARA1 = j.FirstOrDefault().TARA1,
                                   TARA = j.FirstOrDefault().TARA,
                                   TARA3 = j.FirstOrDefault().TARA3,
                                   naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                   hora = j.FirstOrDefault().hora,
                                   BALANZA = j.FirstOrDefault().BALANZA,
                                   estado = j.FirstOrDefault().estado,
                                   sector = j.FirstOrDefault().sector,
                                   correlativo = j.FirstOrDefault().correlativo,
                                   estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                   fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                   PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                   PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                   PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                   itemDetalle = 0,
                                   cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                   glosa = j.FirstOrDefault().glosa,
                                   chofer = j.FirstOrDefault().chofer,
                                   PLACA = j.FirstOrDefault().PLACA,
                                   NROENVIO = j.FirstOrDefault().NROENVIO,
                                   HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                   guiaDeRemision = j.FirstOrDefault().guiaDeRemision,
                                   semana = j.FirstOrDefault().semana,
                                   anio = j.FirstOrDefault().anio
                               }).ToList();
                }
            }


            return listado;

        }

        public List<SAS_RegistroGasificadoByDatesResult> ResumirListado(List<SAS_RegistroGasificadoByDatesResult> result)
        {

            List<SAS_RegistroGasificadoByDatesResult> listado = new List<SAS_RegistroGasificadoByDatesResult>();

            if (result != null)
            {
                if (result.ToList().Count > 0)
                {
                    listado = (from item in result
                               group item by new { item.idGasificado, item.documentoGasificado } into j
                               select new SAS_RegistroGasificadoByDatesResult
                               {
                                   idGasificado = j.Key.idGasificado,
                                   fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                   fechaSalida = j.FirstOrDefault().fechaSalida,
                                   idCamara = j.FirstOrDefault().idCamara,
                                   camara = j.FirstOrDefault().camara,
                                   documentoGasificado = j.Key.documentoGasificado,
                                   dosisSO2 = j.FirstOrDefault().dosisSO2,
                                   tempAgua = j.FirstOrDefault().tempAgua,
                                   lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                   cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                   horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                   horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                   minutos = j.FirstOrDefault().minutos,
                                   idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                   estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                   productoAplicado = j.FirstOrDefault().productoAplicado,
                                   idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                   registradoPor = j.FirstOrDefault().registradoPor,
                                   ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                   AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                   idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                   itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                   fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,
                                   horaGasificado = j.FirstOrDefault().horaGasificado,
                                   tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                   estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                   tipo = j.FirstOrDefault().tipo,
                                   idCampana = j.FirstOrDefault().idCampana,
                                   campaña = j.FirstOrDefault().campaña,
                                   IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                   item = j.FirstOrDefault().item,
                                   FECHA = j.FirstOrDefault().FECHA,
                                   IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                   sucursal = j.FirstOrDefault().sucursal,
                                   DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                   FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                   FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                   FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                   IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                   almacen = j.FirstOrDefault().almacen,
                                   IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                   consumidor = j.FirstOrDefault().consumidor,
                                   tipoProducto = j.FirstOrDefault().tipoProducto,
                                   IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                   DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                   IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                   PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                   PESONETO = j.FirstOrDefault().PESONETO,
                                   PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                   IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                   cultivo = j.FirstOrDefault().cultivo,
                                   IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                   variedad = j.FirstOrDefault().variedad,
                                   IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                   empaque = j.FirstOrDefault().empaque,
                                   NROJABAS = j.FirstOrDefault().NROJABAS,
                                   TARA1 = j.FirstOrDefault().TARA1,
                                   TARA = j.FirstOrDefault().TARA,
                                   TARA3 = j.FirstOrDefault().TARA3,
                                   naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                   hora = j.FirstOrDefault().hora,
                                   BALANZA = j.FirstOrDefault().BALANZA,
                                   estado = j.FirstOrDefault().estado,
                                   sector = j.FirstOrDefault().sector,
                                   correlativo = j.FirstOrDefault().correlativo,
                                   estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                   fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                   PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                   PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                   PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                   itemDetalle = 0,
                                   cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                   glosa = j.FirstOrDefault().glosa,
                                   chofer = j.FirstOrDefault().chofer,
                                   PLACA = j.FirstOrDefault().PLACA,
                                   NROENVIO = j.FirstOrDefault().NROENVIO,
                                   HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                   guiaDeRemision = j.FirstOrDefault().guiaDeRemision,
                                   semana = j.FirstOrDefault().semana,
                                   anio = j.FirstOrDefault().anio
                               }).ToList();
                }
            }


            return listado;

        }

        public int ToRegister(string conection, SAS_RegistroGasificado oRegistroGasificado)
        {
            List<SAS_RegistroGasificado> listResult = new List<SAS_RegistroGasificado>();
            SAS_RegistroGasificado item = new SAS_RegistroGasificado();
            int IdRegistro = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listResult = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == oRegistroGasificado.idGasificado).ToList();
                if (listResult != null)
                {
                    if (listResult.ToList().Count == 0)
                    {
                        #region Nuevo()
                        item = new SAS_RegistroGasificado();
                        item.horaInyeccion = oRegistroGasificado.horaInyeccion != null ? oRegistroGasificado.horaInyeccion.Value : (DateTime?)null;
                        item.horaGasificado = oRegistroGasificado.horaGasificado != null ? oRegistroGasificado.horaGasificado.Value : (DateTime?)null;
                        item.horaVentilacion = oRegistroGasificado.horaVentilacion != null ? oRegistroGasificado.horaVentilacion.Value : (DateTime?)null;
                        item.fechaSalida = oRegistroGasificado.fechaSalida != null ? oRegistroGasificado.fechaSalida.Value : (DateTime?)null;
                        item.fechaIngreso = oRegistroGasificado.fechaIngreso != null ? oRegistroGasificado.fechaIngreso.Value : (DateTime?)null;
                        item.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "001";
                        item.idProductoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.dosisSO2 = oRegistroGasificado.dosisSO2 != null ? oRegistroGasificado.dosisSO2.Value : 0;
                        item.cantidadJabas = oRegistroGasificado.cantidadJabas != (int?)null ? oRegistroGasificado.cantidadJabas.Value : 0;
                        item.estado = Convert.ToByte("1");
                        item.productoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.registradoPor = oRegistroGasificado.registradoPor != null ? oRegistroGasificado.registradoPor : string.Empty;
                        //item.ValidadoPor = oRegistroGasificado.ValidadoPor != null ? oRegistroGasificado.ValidadoPor : string.Empty;
                        //item.AprobadoPor = oRegistroGasificado.AprobadoPor != null ? oRegistroGasificado.AprobadoPor : string.Empty;
                        item.tempAgua = oRegistroGasificado.tempAgua != null ? oRegistroGasificado.tempAgua.Value : 0;
                        item.lecturaPpm = oRegistroGasificado.lecturaPpm != null ? oRegistroGasificado.lecturaPpm.Value : 0;
                        Modelo.SAS_RegistroGasificado.InsertOnSubmit(item);
                        Modelo.SubmitChanges();
                        IdRegistro = item.idGasificado;
                        #endregion

                    }
                    else
                    {
                        #region Editar()
                        item = new SAS_RegistroGasificado();
                        item = listResult.ElementAt(0);
                        item.fechaIngreso = oRegistroGasificado.fechaIngreso != null ? oRegistroGasificado.fechaIngreso.Value : (DateTime?)null;
                        item.horaInyeccion = oRegistroGasificado.horaInyeccion != null ? oRegistroGasificado.horaInyeccion.Value : (DateTime?)null;
                        item.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "001";
                        item.horaGasificado = oRegistroGasificado.horaGasificado != null ? oRegistroGasificado.horaGasificado.Value : (DateTime?)null;
                        item.horaVentilacion = oRegistroGasificado.horaVentilacion != null ? oRegistroGasificado.horaVentilacion.Value : (DateTime?)null;
                        item.fechaSalida = oRegistroGasificado.fechaSalida != null ? oRegistroGasificado.fechaSalida.Value : (DateTime?)null;
                        item.idProductoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.dosisSO2 = oRegistroGasificado.dosisSO2 != null ? oRegistroGasificado.dosisSO2.Value : 0;
                        item.tempAgua = oRegistroGasificado.tempAgua != null ? oRegistroGasificado.tempAgua.Value : 0;
                        item.lecturaPpm = oRegistroGasificado.lecturaPpm != null ? oRegistroGasificado.lecturaPpm.Value : 0;
                        item.productoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        //item.registradoPor = oRegistroGasificado.registradoPor != null ? oRegistroGasificado.registradoPor : string.Empty;
                        //item.ValidadoPor = oRegistroGasificado.ValidadoPor != null ? oRegistroGasificado.ValidadoPor : string.Empty;
                        //item.AprobadoPor = oRegistroGasificado.AprobadoPor != null ? oRegistroGasificado.AprobadoPor : string.Empty;
                        Modelo.SubmitChanges();
                        IdRegistro = item.idGasificado;
                        #endregion
                    }
                }
            }

            return IdRegistro;
        }

        public int ToRegister(string conection, SAS_RegistroGasificado oRegistroGasificado, List<IngresoSalidaGasificado> detalleARegistrar, List<IngresoSalidaGasificado> detalleAEliminar)
        {
            List<SAS_RegistroGasificado> listResult = new List<SAS_RegistroGasificado>();
            SAS_RegistroGasificado item = new SAS_RegistroGasificado();
            List<IngresoSalidaGasificado> itemDetalleEliminar = new List<IngresoSalidaGasificado>();
            List<IngresoSalidaGasificado> itemDetalleRegistrar = new List<IngresoSalidaGasificado>();
            List<IngresoSalidaGasificado> itemDetalleResult = new List<IngresoSalidaGasificado>();
            int IdRegistro = 0;
            SAS_RegistroGasificadoController model = new SAS_RegistroGasificadoController();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listResult = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == oRegistroGasificado.idGasificado).ToList();
                if (listResult != null)
                {
                    if (listResult.ToList().Count == 0)
                    {
                        #region Nuevo()
                        item = new SAS_RegistroGasificado();
                        item.horaInyeccion = oRegistroGasificado.horaInyeccion != null ? oRegistroGasificado.horaInyeccion.Value : (DateTime?)null;
                        item.horaGasificado = oRegistroGasificado.horaGasificado != null ? oRegistroGasificado.horaGasificado.Value : (DateTime?)null;
                        item.horaVentilacion = oRegistroGasificado.horaVentilacion != null ? oRegistroGasificado.horaVentilacion.Value : (DateTime?)null;
                        item.fechaSalida = oRegistroGasificado.fechaSalida != null ? oRegistroGasificado.fechaSalida.Value : (DateTime?)null;
                        item.fechaIngreso = oRegistroGasificado.fechaIngreso != null ? oRegistroGasificado.fechaIngreso.Value : (DateTime?)null;
                        item.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "001";
                        item.idProductoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.dosisSO2 = oRegistroGasificado.dosisSO2 != null ? oRegistroGasificado.dosisSO2.Value : 0;
                        item.cantidadJabas = oRegistroGasificado.cantidadJabas != (int?)null ? oRegistroGasificado.cantidadJabas.Value : 0;
                        item.estado = Convert.ToByte("1");
                        item.productoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.registradoPor = oRegistroGasificado.registradoPor != null ? oRegistroGasificado.registradoPor : string.Empty;
                        //item.ValidadoPor = oRegistroGasificado.ValidadoPor != null ? oRegistroGasificado.ValidadoPor : string.Empty;
                        //item.AprobadoPor = oRegistroGasificado.AprobadoPor != null ? oRegistroGasificado.AprobadoPor : string.Empty;
                        item.tempAgua = oRegistroGasificado.tempAgua != null ? oRegistroGasificado.tempAgua.Value : 0;
                        item.lecturaPpm = oRegistroGasificado.lecturaPpm != null ? oRegistroGasificado.lecturaPpm.Value : 0;
                        Modelo.SAS_RegistroGasificado.InsertOnSubmit(item);
                        Modelo.SubmitChanges();
                        IdRegistro = item.idGasificado;

                        #region Detalle de item a eliminar()
                        if (detalleAEliminar != null && detalleAEliminar.ToList().Count > 0)
                        {
                            model.EliminarItemDetalle(conection, item, detalleAEliminar);
                        }
                        #endregion


                        #region Detalle de items a registrar()
                        if (detalleARegistrar != null && detalleARegistrar.ToList().Count > 0)
                        {
                            model.RegistrarItemDetalle(conection, item, detalleARegistrar);
                        }
                        #endregion


                        #endregion

                    }
                    else if (listResult.ToList().Count == 1)
                    {
                        #region Editar()
                        item = new SAS_RegistroGasificado();
                        item = listResult.ElementAt(0);
                        item.fechaIngreso = oRegistroGasificado.fechaIngreso != null ? oRegistroGasificado.fechaIngreso.Value : (DateTime?)null;
                        item.horaInyeccion = oRegistroGasificado.horaInyeccion != null ? oRegistroGasificado.horaInyeccion.Value : (DateTime?)null;
                        item.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "001";
                        item.horaGasificado = oRegistroGasificado.horaGasificado != null ? oRegistroGasificado.horaGasificado.Value : (DateTime?)null;
                        item.horaVentilacion = oRegistroGasificado.horaVentilacion != null ? oRegistroGasificado.horaVentilacion.Value : (DateTime?)null;
                        item.fechaSalida = oRegistroGasificado.fechaSalida != null ? oRegistroGasificado.fechaSalida.Value : (DateTime?)null;
                        item.idProductoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        item.dosisSO2 = oRegistroGasificado.dosisSO2 != null ? oRegistroGasificado.dosisSO2.Value : 0;
                        item.tempAgua = oRegistroGasificado.tempAgua != null ? oRegistroGasificado.tempAgua.Value : 0;
                        item.lecturaPpm = oRegistroGasificado.lecturaPpm != null ? oRegistroGasificado.lecturaPpm.Value : 0;
                        item.productoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado : string.Empty;
                        //item.registradoPor = oRegistroGasificado.registradoPor != null ? oRegistroGasificado.registradoPor : string.Empty;
                        //item.ValidadoPor = oRegistroGasificado.ValidadoPor != null ? oRegistroGasificado.ValidadoPor : string.Empty;
                        //item.AprobadoPor = oRegistroGasificado.AprobadoPor != null ? oRegistroGasificado.AprobadoPor : string.Empty;
                        Modelo.SubmitChanges();
                        IdRegistro = item.idGasificado;
                        #region Detalle de item a eliminar()
                        if (detalleAEliminar != null && detalleAEliminar.ToList().Count > 0)
                        {
                            model.EliminarItemDetalle(conection, item, detalleAEliminar);
                        }
                        #endregion


                        #region Detalle de items a registrar()
                        if (detalleARegistrar != null && detalleARegistrar.ToList().Count > 0)
                        {
                            model.RegistrarItemDetalle(conection, item, detalleARegistrar);
                        }
                        #endregion


                        #endregion
                    }
                }
            }

            return IdRegistro;
        }


        public int RegistrarItemDetalle(string conection, SAS_RegistroGasificado oRegistroGasificado, List<IngresoSalidaGasificado> detalleARegistrar)
        {
            int resultQuery = 0;
            List<IngresoSalidaGasificado> itemDetalleResult = new List<IngresoSalidaGasificado>();
            IngresoSalidaGasificado itemDetalle = new IngresoSalidaGasificado();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                foreach (var item in detalleARegistrar)
                {
                    itemDetalleResult = Modelo.IngresoSalidaGasificado.Where(x => x.idIngresoSalidaGasificado == item.idIngresoSalidaGasificado).ToList();
                    if (itemDetalleResult != null && itemDetalleResult.ToList().Count == 0)
                    {
                        itemDetalle = new IngresoSalidaGasificado();
                        // itemDetalle.idIngresoSalidaGasificado = item.idIngresoSalidaGasificado;
                        itemDetalle.idCamara = item.idCamara != null ? item.idCamara.Trim() : string.Empty;
                        itemDetalle.itemDetalle = item.itemDetalle;
                        itemDetalle.fecha = item.fecha != null ? oRegistroGasificado.fechaIngreso.Value : DateTime.Now;
                        itemDetalle.tipoRegistro = item.tipoRegistro != (char?)null ? item.tipoRegistro : (char?)null;
                        itemDetalle.estado = item.estado != (byte?)null ? item.estado : (byte?)null;
                        itemDetalle.tipo = item.tipo != (char?)null ? item.tipo : (char?)null;
                        itemDetalle.idGasificado = oRegistroGasificado.idGasificado != (int?)null ? oRegistroGasificado.idGasificado : (int?)null;
                        Modelo.IngresoSalidaGasificado.InsertOnSubmit(itemDetalle);
                        Modelo.SubmitChanges();
                    }
                    else if (itemDetalleResult != null && itemDetalleResult.ToList().Count == 1)
                    {
                        itemDetalle = new IngresoSalidaGasificado();
                        itemDetalle = itemDetalleResult.ElementAt(0);
                        //itemDetalle.idIngresoSalidaGasificado = item.idIngresoSalidaGasificado;
                        itemDetalle.idCamara = item.idCamara != null ? item.idCamara.Trim() : string.Empty;
                        itemDetalle.fecha = item.fecha != null ? oRegistroGasificado.fechaIngreso.Value : DateTime.Now;
                        itemDetalle.idGasificado = oRegistroGasificado.idGasificado != (int?)null ? oRegistroGasificado.idGasificado : (int?)null;
                        Modelo.SubmitChanges();
                    }
                }
            }
            return resultQuery;
        }

        public int EliminarItemDetalle(string conection, SAS_RegistroGasificado oRegistroGasificado, List<IngresoSalidaGasificado> detalleAEliminar)
        {
            int resultQuery = 0;
            List<IngresoSalidaGasificado> itemDetalleResult = new List<IngresoSalidaGasificado>();
            IngresoSalidaGasificado itemDetalle = new IngresoSalidaGasificado();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                if (detalleAEliminar != null && detalleAEliminar.ToList().Count > 0)
                {

                    Modelo.IngresoSalidaGasificado.DeleteAllOnSubmit(detalleAEliminar);
                    //foreach (var item in detalleAEliminar)
                    //{
                    //    itemDetalleResult = Modelo.IngresoSalidaGasificado.Where(x => x.idIngresoSalidaGasificado == item.idIngresoSalidaGasificado).ToList();
                    //    if (itemDetalleResult != null && itemDetalleResult.ToList().Count == 1)
                    //    {
                    //        itemDetalle = new IngresoSalidaGasificado();
                    //        itemDetalle = itemDetalleResult.ElementAt(0);
                    //        Modelo.IngresoSalidaGasificado.DeleteOnSubmit(itemDetalle);
                    //        Modelo.SubmitChanges();
                    //    }
                    //}
                }
            }
            return resultQuery;
        }

        public List<SAS_RegistroGasificadoAll> ResumirListadoByIdGasificado(List<SAS_RegistroGasificadoAll> result)
        {

            List<SAS_RegistroGasificadoAll> listado = new List<SAS_RegistroGasificadoAll>();

            if (result != null)
            {
                if (result.ToList().Count > 0)
                {
                    listado = (from item in result
                               group item by new { item.idGasificado, item.documentoGasificado } into j
                               select new SAS_RegistroGasificadoAll
                               {
                                   idGasificado = j.Key.idGasificado,
                                   fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                   fechaSalida = j.FirstOrDefault().fechaSalida,
                                   idCamara = j.FirstOrDefault().idCamara,
                                   camara = j.FirstOrDefault().camara,
                                   documentoGasificado = j.Key.documentoGasificado,
                                   dosisSO2 = j.FirstOrDefault().dosisSO2,
                                   tempAgua = j.FirstOrDefault().tempAgua,
                                   lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                   cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                   horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                   horaGasificado = j.FirstOrDefault().horaGasificado,
                                   horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                   minutos = j.FirstOrDefault().minutos,
                                   idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                   estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                   productoAplicado = j.FirstOrDefault().productoAplicado,
                                   idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                   registradoPor = j.FirstOrDefault().registradoPor,
                                   ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                   AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                   idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                   itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                   fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,
                                   tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                   estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                   tipo = j.FirstOrDefault().tipo,
                                   idCampana = j.FirstOrDefault().idCampana,
                                   campaña = j.FirstOrDefault().campaña,
                                   IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                   item = j.FirstOrDefault().item,
                                   FECHA = j.FirstOrDefault().FECHA,
                                   IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                   sucursal = j.FirstOrDefault().sucursal,
                                   DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                   FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                   FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                   FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                   IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                   almacen = j.FirstOrDefault().almacen,
                                   IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                   consumidor = j.FirstOrDefault().consumidor,
                                   tipoProducto = j.FirstOrDefault().tipoProducto,
                                   IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                   DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                   IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                   PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                   PESONETO = j.FirstOrDefault().PESONETO,
                                   PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                   IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                   cultivo = j.FirstOrDefault().cultivo,
                                   IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                   variedad = j.FirstOrDefault().variedad,
                                   IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                   empaque = j.FirstOrDefault().empaque,
                                   NROJABAS = j.FirstOrDefault().NROJABAS,
                                   TARA1 = j.FirstOrDefault().TARA1,
                                   TARA = j.FirstOrDefault().TARA,
                                   TARA3 = j.FirstOrDefault().TARA3,
                                   naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                   hora = j.FirstOrDefault().hora,
                                   BALANZA = j.FirstOrDefault().BALANZA,
                                   estado = j.FirstOrDefault().estado,
                                   sector = j.FirstOrDefault().sector,
                                   correlativo = j.FirstOrDefault().correlativo,
                                   estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                   fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                   PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                   PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                   PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                   itemDetalle = 0,
                                   cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                   glosa = j.FirstOrDefault().glosa,
                                   chofer = j.FirstOrDefault().chofer,
                                   PLACA = j.FirstOrDefault().PLACA,
                                   NROENVIO = j.FirstOrDefault().NROENVIO,
                                   HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                   guiaDeRemision = j.FirstOrDefault().guiaDeRemision,
                                   semana = j.FirstOrDefault().semana,
                                   anio = j.FirstOrDefault().anio,

                                   empresaCodigo = j.FirstOrDefault().empresaCodigo,
                                   empresa = j.FirstOrDefault().empresa,
                                   sucursalCodigo = j.FirstOrDefault().sucursalCodigo,
                                   sucursalRegistroGasificado = j.FirstOrDefault().sucursalRegistroGasificado,
                                   registradoPorNombres = j.FirstOrDefault().registradoPorNombres,
                                   ValidadoPorNombres = j.FirstOrDefault().ValidadoPorNombres,
                                   AprobadoPorNombres = j.FirstOrDefault().AprobadoPorNombres

                               }).ToList();
                }
            }


            return listado;

        }

        public SAS_RegistroGasificadoAllByIDResult ResumirListadoByIdGasificado(List<SAS_RegistroGasificadoAllByIDResult> result)
        {

            SAS_RegistroGasificadoAllByIDResult itemGasificado = new SAS_RegistroGasificadoAllByIDResult();

            if (result != null)
            {
                if (result.ToList().Count > 0)
                {
                    itemGasificado = (from item in result
                                      group item by new { item.idGasificado, item.documentoGasificado } into j
                                      select new SAS_RegistroGasificadoAllByIDResult
                                      {
                                          sucursalRegistroGasificado = j.FirstOrDefault().sucursalRegistroGasificado,
                                          idGasificado = j.Key.idGasificado,
                                          fechaIngreso = j.FirstOrDefault().fechaIngreso,
                                          fechaSalida = j.FirstOrDefault().fechaSalida,
                                          idCamara = j.FirstOrDefault().idCamara,
                                          camara = j.FirstOrDefault().camara,
                                          documentoGasificado = j.Key.documentoGasificado,
                                          dosisSO2 = j.FirstOrDefault().dosisSO2,
                                          tempAgua = j.FirstOrDefault().tempAgua,
                                          lecturaPpm = j.FirstOrDefault().lecturaPpm,
                                          cantidadJabasEnRegistroGasificado = j.FirstOrDefault().cantidadJabasEnRegistroGasificado, //
                                          horaInyeccion = j.FirstOrDefault().horaInyeccion,
                                          horaGasificado = j.FirstOrDefault().horaGasificado,
                                          horaVentilacion = j.FirstOrDefault().horaVentilacion,
                                          minutos = j.FirstOrDefault().minutos,
                                          idestadoRegistroGasificado = j.FirstOrDefault().idestadoRegistroGasificado,
                                          estadoRegistroGasificado = j.FirstOrDefault().estadoRegistroGasificado,
                                          productoAplicado = j.FirstOrDefault().productoAplicado,
                                          idProductoAplicado = j.FirstOrDefault().idProductoAplicado,
                                          registradoPor = j.FirstOrDefault().registradoPor,
                                          ValidadoPor = j.FirstOrDefault().ValidadoPor,
                                          AprobadoPor = j.FirstOrDefault().AprobadoPor,
                                          idIngresoSalidaGasificado = j.FirstOrDefault().idIngresoSalidaGasificado,
                                          itemDetalleEnRegistroGasificado = j.FirstOrDefault().itemDetalleEnRegistroGasificado,
                                          fechaRegistroDetalle = j.FirstOrDefault().fechaRegistroDetalle,
                                          tipoRegistro = j.FirstOrDefault().tipoRegistro,
                                          estadoItemDetalleGasificado = j.FirstOrDefault().estadoItemDetalleGasificado,
                                          tipo = j.FirstOrDefault().tipo,
                                          idCampana = j.FirstOrDefault().idCampana,
                                          campaña = j.FirstOrDefault().campaña,
                                          IDINGRESOSALIDAACOPIOCAMPO = j.FirstOrDefault().IDINGRESOSALIDAACOPIOCAMPO,
                                          item = j.FirstOrDefault().item,
                                          FECHA = j.FirstOrDefault().FECHA,
                                          IDSUCURSAL = j.FirstOrDefault().IDSUCURSAL,
                                          sucursal = j.FirstOrDefault().sucursal,
                                          DOCUMENTO = j.FirstOrDefault().DOCUMENTO,
                                          FECHA_RECEPCION = j.FirstOrDefault().FECHA_RECEPCION,
                                          FECHACREACION = j.FirstOrDefault().FECHACREACION,
                                          FechaCreacionitem = j.FirstOrDefault().FechaCreacionitem,
                                          IDALMACEN = j.FirstOrDefault().IDALMACEN,
                                          almacen = j.FirstOrDefault().almacen,
                                          IDCONSUMIDOR = j.FirstOrDefault().IDCONSUMIDOR,
                                          consumidor = j.FirstOrDefault().consumidor,
                                          tipoProducto = j.FirstOrDefault().tipoProducto,
                                          IDPRODUCTO = j.FirstOrDefault().IDPRODUCTO,
                                          DESCRIPCION = j.FirstOrDefault().DESCRIPCION,
                                          IDMEDIDA = j.FirstOrDefault().IDMEDIDA,
                                          PESOBRUTO = j.FirstOrDefault().PESOBRUTO,
                                          PESONETO = j.FirstOrDefault().PESONETO,
                                          PESOPROMEDIO = j.FirstOrDefault().PESOPROMEDIO,
                                          IDCULTIVO = j.FirstOrDefault().IDCULTIVO,
                                          cultivo = j.FirstOrDefault().cultivo,
                                          IDVARIEDAD = j.FirstOrDefault().IDVARIEDAD,
                                          variedad = j.FirstOrDefault().variedad,
                                          IDEMPAQUE = j.FirstOrDefault().IDEMPAQUE,
                                          empaque = j.FirstOrDefault().empaque,
                                          NROJABAS = j.FirstOrDefault().NROJABAS,
                                          TARA1 = j.FirstOrDefault().TARA1,
                                          TARA = j.FirstOrDefault().TARA,
                                          TARA3 = j.FirstOrDefault().TARA3,
                                          naturaleza = j.FirstOrDefault().IDEMPAQUE,
                                          hora = j.FirstOrDefault().hora,
                                          BALANZA = j.FirstOrDefault().BALANZA,
                                          estado = j.FirstOrDefault().estado,
                                          sector = j.FirstOrDefault().sector,
                                          correlativo = j.FirstOrDefault().correlativo,
                                          estadoImpresion = j.FirstOrDefault().estadoImpresion,
                                          fechaRegistro = j.FirstOrDefault().fechaRegistro,
                                          PorcentajeDeParticipacion = j.FirstOrDefault().PorcentajeDeParticipacion,
                                          PesoBrutoDistribuido = j.FirstOrDefault().PesoBrutoDistribuido,
                                          PesoNetoDistribuido = j.FirstOrDefault().PesoNetoDistribuido,
                                          itemDetalle = 0,
                                          cantidadEnTicket = j.Sum(x => x.cantidadEnTicket), //
                                          glosa = j.FirstOrDefault().glosa,
                                          chofer = j.FirstOrDefault().chofer,
                                          PLACA = j.FirstOrDefault().PLACA,
                                          NROENVIO = j.FirstOrDefault().NROENVIO,
                                          HORAENVIO = j.FirstOrDefault().HORAENVIO,
                                          guiaDeRemision = j.FirstOrDefault().guiaDeRemision,
                                          semana = j.FirstOrDefault().semana,
                                          anio = j.FirstOrDefault().anio,
                                          empresaCodigo = j.FirstOrDefault().empresaCodigo,
                                          empresa = j.FirstOrDefault().empresa,
                                          sucursalCodigo = j.FirstOrDefault().sucursalCodigo,
                                          registradoPorNombres = j.FirstOrDefault().registradoPorNombres,
                                          ValidadoPorNombres = j.FirstOrDefault().ValidadoPorNombres,
                                          AprobadoPorNombres = j.FirstOrDefault().AprobadoPorNombres

                                      }).ToList().ElementAt(0);
                }
            }


            return itemGasificado;

        }

        public decimal ObtenerCantidadDeTicketGasificadosPorIdGasificado(string conection, int IdGasificado)
        {
            decimal CantidadDeTicketGasificadosPorIdGasificado = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                var resultQuery = Modelo.SAS_ObtenerCantidadDeTicketGasificadosPorIdGasificado(IdGasificado).ToList();
                if (resultQuery != null && resultQuery.ToList().Count > 0)
                {

                    CantidadDeTicketGasificadosPorIdGasificado = resultQuery.ElementAt(0).cantidad != null ? resultQuery.ElementAt(0).cantidad.Value : 0;
                }
                    
            }

            return CantidadDeTicketGasificadosPorIdGasificado;
        }

        //GetListOfRecordPendingReading
        public List<DFormatoSimple> GetListOfRecordPendingReading(string conection, DateTime dateQuery, DateTime dateQueryFinal)
        {
            controller = new SAS_RegistroGasificadoController();
            List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosResult> listadoTicketNoLeidos = new List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosResult>();
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfInterfaces = new List<SAS_SegmentoRed>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listadoTicketNoLeidos = controller.ObtenerListadoTicketsPendientesDeRegistroByFecha(conection, dateQuery).ToList();
                if (listadoTicketNoLeidos != null && listadoTicketNoLeidos.ToList().Count > 0)
                {
                    listado = (from item in listadoTicketNoLeidos
                                   // where item.fechaRegistro <= dateQueryFinal
                               group item by new { item.idDetalle } into j
                               select new DFormatoSimple
                               {
                                   Codigo = j.Key.idDetalle.ToString(),
                                   Descripcion = (j.FirstOrDefault().fechaRegistro != null ? j.FirstOrDefault().fechaRegistro.Value.ToString() : string.Empty) +
                                   " | Ticket : " + j.FirstOrDefault().itemDetalle != null ? j.FirstOrDefault().itemDetalle.Value.ToString() : string.Empty +
                                   " | Cantidad " + j.FirstOrDefault().cantidadRegistrada != null ? j.FirstOrDefault().cantidadRegistrada.Value.ToString() : string.Empty
                               }
                               ).ToList();
                }
            }
            return listado;
        }

        public List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosResult> ObtenerListadoTicketsPendientesDeRegistroByFecha(string conection, DateTime dateQuery)
        {
            List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosResult> listado = new List<SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidos(dateQuery.ToShortDateString(), dateQuery.ToShortDateString()).ToList();
            }
            return listado;
        }


        public SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult ObtenerListadoTicketsPendientesDeRegistroByTicket(string conection, int itemDetalle)
        {
            SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult item = new SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicketResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                item = Modelo.SAS_RegistroIngresoSalidaACamaraGasificadoByDatesNoLeidosByTicket(itemDetalle).ToList().ElementAt(0);
            }
            return item;
        }


    }
}
