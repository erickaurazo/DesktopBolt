using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_RegistroGasificadoController
    {

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
                resultado = Modelo.SAS_RegistroIngresoSalidaACamaraGasificadoByDates(desde,hasta ).ToList();
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
            SAS_RegistroGasificado result = new SAS_RegistroGasificado();
            int resultQuery = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listResult = Modelo.SAS_RegistroGasificado.Where(x => x.idGasificado == oRegistroGasificado.idGasificado).ToList();
                if (listResult != null)
                {
                    if (listResult.ToList().Count == 0)
                    {
                        #region Nuevo()
                        result = new SAS_RegistroGasificado();


                        resultQuery = 1;
                        #endregion

                    }
                    else
                    {
                        #region Editar()
                        result = new SAS_RegistroGasificado();
                        result = listResult.ElementAt(0);
                        result.horaInyeccion = oRegistroGasificado.horaInyeccion != null ? oRegistroGasificado.horaInyeccion.Value : (DateTime?)null;
                        result.horaGasificado = oRegistroGasificado.horaGasificado != null ? oRegistroGasificado.horaGasificado.Value : (DateTime?)null;
                        result.horaVentilacion = oRegistroGasificado.horaVentilacion != null ? oRegistroGasificado.horaVentilacion.Value : (DateTime?)null;
                        result.fechaSalida = oRegistroGasificado.fechaSalida != null ? oRegistroGasificado.fechaSalida.Value : (DateTime?)null;
                        result.idProductoAplicado = oRegistroGasificado.idProductoAplicado != null ? oRegistroGasificado.idProductoAplicado :  string.Empty;
                        result.dosisSO2 = oRegistroGasificado.dosisSO2 != null ? oRegistroGasificado.dosisSO2.Value :  0;
                        result.tempAgua = oRegistroGasificado.tempAgua != null ? oRegistroGasificado.tempAgua.Value : 0;
                        result.lecturaPpm = oRegistroGasificado.lecturaPpm != null ? oRegistroGasificado.lecturaPpm.Value : 0;
                        Modelo.SubmitChanges();
                        resultQuery = result.idGasificado;
                        #endregion
                    }
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

        public List<SAS_RegistroGasificadoAllByIDResult> ResumirListadoByIdGasificado(List<SAS_RegistroGasificadoAllByIDResult> result)
        {

            List<SAS_RegistroGasificadoAllByIDResult> listado = new List<SAS_RegistroGasificadoAllByIDResult>();

            if (result != null)
            {
                if (result.ToList().Count > 0)
                {
                    listado = (from item in result
                               group item by new { item.idGasificado, item.documentoGasificado } into j
                               select new SAS_RegistroGasificadoAllByIDResult
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

    }
}
