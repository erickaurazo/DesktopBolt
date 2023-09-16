using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using MyControlsDataBinding.Extensions;
using System.Globalization;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class AsistenciaController
    {

        public List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> GetListAsistanceByDoor(string conection, string fechaDesde, string fechaHasta, string idplanilla, char? idfundo)
        {
            List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listado = new List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();
            List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listadoFinal = new List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 999909999;
                listado = Modelo.ASJ_ReporteAsistenciaDiariaByPuertaIngreso(fechaDesde, fechaHasta, idplanilla, idfundo).ToList();

                //var resultadoByFecha = (from item in listado
                //                        group item by new { item.FECHA.Value } into j
                //                        select new
                //                        {
                //                            fecha = j.Key.Value.ToPresentationDate()
                //                        }).ToList();

                //foreach (var itemFecha in resultadoByFecha)
                //{
                //    var resultadoByPersonal = listado.Where(x => x.FECHA.Value.ToShortDateString() == itemFecha.fecha.ToString()).ToList();

                //    var resultadoByPersona = (from item in resultadoByPersonal
                //                              where item.IDPERSONAL.Trim() != null
                //                              group item by new
                //                              {
                //                                  item.IDPERSONAL
                //                              }
                //                              into j
                //                              select new
                //                              {
                //                                  codigoPersona = j.Key.IDPERSONAL.Trim()
                //                              }).ToList();


                //    foreach (var itemPersonal in resultadoByPersona)
                //    {
                //        var resultadoByPersonaListado = resultadoByPersonal.Where(x => x.IDPERSONAL.Trim() == itemPersonal.codigoPersona.ToString().Trim()).ToList();

                //        ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult oMarcacion = new ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult();
                //        oMarcacion.FECHA = Convert.ToDateTime(itemFecha.fecha);
                //        oMarcacion.id_puerta = resultadoByPersonaListado.FirstOrDefault().id_puerta != null ? resultadoByPersonaListado.FirstOrDefault().id_puerta.Value : 0;
                //        oMarcacion.puerta = resultadoByPersonaListado.FirstOrDefault().puerta != null ? resultadoByPersonaListado.FirstOrDefault().puerta.ToString() : string.Empty;
                //        oMarcacion.IdUsuario = resultadoByPersonaListado.FirstOrDefault().IdUsuario != null ? resultadoByPersonaListado.FirstOrDefault().IdUsuario.ToString() : string.Empty;
                //        oMarcacion.NOMBRES = resultadoByPersonaListado.FirstOrDefault().NOMBRES != null ? resultadoByPersonaListado.FirstOrDefault().NOMBRES.ToString() : string.Empty;
                //        oMarcacion.IDPERSONAL = itemPersonal.codigoPersona;
                //        oMarcacion.IDPLANILLA = resultadoByPersonaListado.FirstOrDefault().IDPLANILLA != null ? resultadoByPersonaListado.FirstOrDefault().IDPLANILLA : string.Empty;
                //        oMarcacion.MARCACION1 = resultadoByPersonaListado.Max(x=> x.MARCACION1) != null ? resultadoByPersonaListado.Max(x => x.MARCACION1) : string.Empty;
                //        oMarcacion.MARCACION2 = resultadoByPersonaListado.Max(x => x.MARCACION2) != null ? resultadoByPersonaListado.Max(x => x.MARCACION2) : string.Empty;
                //        oMarcacion.MARCACION3 = resultadoByPersonaListado.Max(x => x.MARCACION3) != null ? resultadoByPersonaListado.Max(x => x.MARCACION3) : string.Empty;
                //        oMarcacion.MARCACION4 = resultadoByPersonaListado.Max(x => x.MARCACION4) != null ? resultadoByPersonaListado.Max(x => x.MARCACION4) : string.Empty;
                //        oMarcacion.MARCACION5 = resultadoByPersonaListado.Max(x => x.MARCACION5) != null ? resultadoByPersonaListado.Max(x => x.MARCACION5) : string.Empty;
                //        oMarcacion.MARCACION6 = resultadoByPersonaListado.Max(x => x.MARCACION6) != null ? resultadoByPersonaListado.Max(x => x.MARCACION6) : string.Empty;
                //        oMarcacion.MARCACION7 = resultadoByPersonaListado.Max(x => x.MARCACION7) != null ? resultadoByPersonaListado.Max(x => x.MARCACION7) : string.Empty;
                //        oMarcacion.MARCACION8 = resultadoByPersonaListado.Max(x => x.MARCACION8) != null ? resultadoByPersonaListado.Max(x => x.MARCACION8) : string.Empty;
                //        oMarcacion.MARCACION9 = resultadoByPersonaListado.Max(x => x.MARCACION9) != null ? resultadoByPersonaListado.Max(x => x.MARCACION9) : string.Empty;
                //        oMarcacion.MARCACION10 = resultadoByPersonaListado.Max(x => x.MARCACION10) != null ? resultadoByPersonaListado.Max(x => x.MARCACION10) : string.Empty;
                //        listadoFinal.Add(oMarcacion);
                //    }

                //}

            }

            return listado;
        }


        public List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> AgruparListadoByFechaByPersona(List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listado)
        {
            List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult> listadoFinal = new List<ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult>();
            string cnx = string.Empty;



            var resultadoByFecha = (from item in listado
                                    group item by new { item.FECHA.Value } into j
                                    select new
                                    {
                                        fecha = j.Key.Value.ToPresentationDate()
                                    }).ToList();

            foreach (var itemFecha in resultadoByFecha)
            {
                var resultadoByPersonal = listado.Where(x => x.FECHA.Value.ToShortDateString() == itemFecha.fecha.ToString()).ToList();

                var resultadoByPersona = (from item in resultadoByPersonal
                                          where item.IDPERSONAL.Trim() != null
                                          group item by new
                                          {
                                              item.IDPERSONAL
                                          }
                                          into j
                                          select new
                                          {
                                              codigoPersona = j.Key.IDPERSONAL.Trim()
                                          }).ToList();


                foreach (var itemPersonal in resultadoByPersona)
                {
                    var resultadoByPersonaListado = resultadoByPersonal.Where(x => x.IDPERSONAL.Trim() == itemPersonal.codigoPersona.ToString().Trim()).ToList();

                    ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult oMarcacion = new ASJ_ReporteAsistenciaDiariaByPuertaIngresoResult();
                    oMarcacion.FECHA = Convert.ToDateTime(itemFecha.fecha);
                    oMarcacion.id_puerta = resultadoByPersonaListado.FirstOrDefault().id_puerta != null ? resultadoByPersonaListado.FirstOrDefault().id_puerta.Value : 0;
                    oMarcacion.puerta = resultadoByPersonaListado.FirstOrDefault().puerta != null ? resultadoByPersonaListado.FirstOrDefault().puerta.ToString() : string.Empty;
                    oMarcacion.IdUsuario = resultadoByPersonaListado.FirstOrDefault().IdUsuario != null ? resultadoByPersonaListado.FirstOrDefault().IdUsuario.ToString() : string.Empty;
                    oMarcacion.NOMBRES = resultadoByPersonaListado.FirstOrDefault().NOMBRES != null ? resultadoByPersonaListado.FirstOrDefault().NOMBRES.ToString() : string.Empty;
                    oMarcacion.IDPERSONAL = itemPersonal.codigoPersona;
                    oMarcacion.IDPLANILLA = resultadoByPersonaListado.FirstOrDefault().IDPLANILLA != null ? resultadoByPersonaListado.FirstOrDefault().IDPLANILLA : string.Empty;
                    oMarcacion.MARCACION1 = resultadoByPersonaListado.Max(x => x.MARCACION1) != null ? resultadoByPersonaListado.Max(x => x.MARCACION1) : string.Empty;
                    oMarcacion.MARCACION2 = resultadoByPersonaListado.Max(x => x.MARCACION2) != null ? resultadoByPersonaListado.Max(x => x.MARCACION2) : string.Empty;
                    oMarcacion.MARCACION3 = resultadoByPersonaListado.Max(x => x.MARCACION3) != null ? resultadoByPersonaListado.Max(x => x.MARCACION3) : string.Empty;
                    oMarcacion.MARCACION4 = resultadoByPersonaListado.Max(x => x.MARCACION4) != null ? resultadoByPersonaListado.Max(x => x.MARCACION4) : string.Empty;
                    oMarcacion.MARCACION5 = resultadoByPersonaListado.Max(x => x.MARCACION5) != null ? resultadoByPersonaListado.Max(x => x.MARCACION5) : string.Empty;
                    oMarcacion.MARCACION6 = resultadoByPersonaListado.Max(x => x.MARCACION6) != null ? resultadoByPersonaListado.Max(x => x.MARCACION6) : string.Empty;
                    oMarcacion.MARCACION7 = resultadoByPersonaListado.Max(x => x.MARCACION7) != null ? resultadoByPersonaListado.Max(x => x.MARCACION7) : string.Empty;
                    oMarcacion.MARCACION8 = resultadoByPersonaListado.Max(x => x.MARCACION8) != null ? resultadoByPersonaListado.Max(x => x.MARCACION8) : string.Empty;
                    oMarcacion.MARCACION9 = resultadoByPersonaListado.Max(x => x.MARCACION9) != null ? resultadoByPersonaListado.Max(x => x.MARCACION9) : string.Empty;
                    oMarcacion.MARCACION10 = resultadoByPersonaListado.Max(x => x.MARCACION10) != null ? resultadoByPersonaListado.Max(x => x.MARCACION10) : string.Empty;
                    listadoFinal.Add(oMarcacion);
                }

            }



            return listadoFinal;
        }

        public List<ASJ_ReporteAsistenciaByPuertaResult> ObtenerReporteAsistenciaByPuerta(string conection, string desde, string hasta)
        {
            List<ASJ_ReporteAsistenciaByPuertaResult> listado = new List<ASJ_ReporteAsistenciaByPuertaResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 999909999;
                listado = Modelo.ASJ_ReporteAsistenciaByPuerta(desde, hasta).ToList();
            }

            return listado;
        }

        public List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult> ObtenerReporteAsistenciaByPuertaDatosCompletos(string conection, string desde, string hasta)
        {
            List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult> listado = new List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 999909999;
                listado = Modelo.ASJ_ReporteAsistenciaByPuertaByDatosCompletos(desde, hasta).ToList();
            }

            return listado;
        }


        public List<ASJ_ReporteAsistenciaByPuertaResult> ObtenerReporteAsistenciaByPuertaOnlyRegistro(List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult> listadoCompleto, List<ASJ_ReporteAsistenciaByPuertaResult> listadoAsistencia)
        {
            List<ASJ_ReporteAsistenciaByPuertaResult> listado = new List<ASJ_ReporteAsistenciaByPuertaResult>();
            List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult> listadoCompletoP = new List<ASJ_ReporteAsistenciaByPuertaByDatosCompletosResult>();
            List<ASJ_ReporteAsistenciaByPuertaResult> listadoAsistenciaP = new List<ASJ_ReporteAsistenciaByPuertaResult>();


            if (listadoAsistencia != null && listadoAsistencia.ToList().Count > 0)
            {
                listadoAsistenciaP = listadoAsistencia.Where(x => x.ROW_ == 1).ToList();
            }

            if (listadoCompleto != null && listadoCompleto.ToList().Count > 0)
            {
                listadoCompletoP = listadoCompleto.Where(x => x.fila == 1).ToList();
            }


            var listaFechaAgrupada = (from item in listadoAsistenciaP
                                      where item.IDPERSONAL.Trim() != null && item.NOMBRES != null && item.NOMBRES != string.Empty
                                      group item by new
                                      {
                                          item.FECHA.Value
                                      }
                                          into j
                                      select new
                                      {
                                          fechaAgrupado = Convert.ToDateTime(j.Key.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                                      }).ToList();

            if (listaFechaAgrupada != null && listaFechaAgrupada.ToList().Count > 0)
            {
                foreach (var itemFecha in listaFechaAgrupada)
                {
                    var listaAgrupadoByFecha = listadoAsistenciaP.Where(x => Convert.ToDateTime(x.FECHA.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)) == Convert.ToDateTime(itemFecha.fechaAgrupado)).ToList();

                    if (listaAgrupadoByFecha != null && listaAgrupadoByFecha.ToList().Count > 0)
                    {

                        var listaDNIAgrupada = (from item in listaAgrupadoByFecha
                                                where item.IDPERSONAL.Trim() != null && item.NOMBRES != null && item.NOMBRES != string.Empty
                                                group item by new
                                                {
                                                    item.IDPERSONAL,

                                                }
                                         into j
                                                select new
                                                {
                                                    dniAgrupado = j.Key.IDPERSONAL.Trim(),
                                                    dniNombres = j.FirstOrDefault().NOMBRES.Trim(),
                                                }).ToList();

                        if (listaDNIAgrupada != null && listaDNIAgrupada.ToList().Count > 0)
                        {
                            foreach (var itemdni in listaDNIAgrupada)
                            {
                                var listadoByDNI = listaAgrupadoByFecha.Where(x => x.IDPERSONAL.Trim() == itemdni.dniAgrupado).ToList();
                                var listadoByDNICompleto = listadoCompletoP.Where(x => x.idcodigoGeneral.Trim() == itemdni.dniAgrupado.Trim()).ToList();
                                try
                                {
                                    
                                    ASJ_ReporteAsistenciaByPuertaResult registroAsistencia = new ASJ_ReporteAsistenciaByPuertaResult();
                                    registroAsistencia.FECHA = Convert.ToDateTime(itemFecha.fechaAgrupado);
                                    registroAsistencia.IDPERSONAL = itemdni.dniAgrupado != null ? itemdni.dniAgrupado : string.Empty;
                                    registroAsistencia.NOMBRES = itemdni.dniNombres != null ? itemdni.dniNombres.Trim() : string.Empty;
                                    registroAsistencia.IDPLANILLA = listadoByDNI.FirstOrDefault().IDPLANILLA != null ? listadoByDNI.FirstOrDefault().IDPLANILLA : string.Empty;
                                    registroAsistencia.puerta = listadoByDNI.FirstOrDefault().puerta != null ? listadoByDNI.FirstOrDefault().puerta : string.Empty;
                                    registroAsistencia.INGRESO = listadoByDNI.Count > 0 ?  listadoByDNI.Where(x => x.INGRESO != string.Empty).Max(x => x.INGRESO) : string.Empty;
                                    registroAsistencia.SALIDA = listadoByDNI.Count > 0 ? listadoByDNI.Where(x => x.SALIDA != string.Empty).Max(x => x.SALIDA) : string.Empty;
                                    registroAsistencia.ROW_ = 1;
                                    if (itemdni.dniAgrupado == "45038264")
                                    {
                                        string asass = string.Empty;
                                    }
                                    registroAsistencia.sexo = (listadoByDNICompleto.Count > 0 & listadoByDNICompleto.FirstOrDefault() != null) ? listadoByDNICompleto.FirstOrDefault().SEXO.ToString().Trim() : "N";
                                    registroAsistencia.tiempo = string.Empty;
                                    registroAsistencia.estadoMarcacion = "P";

                                    if (itemdni.dniAgrupado == "45038264")
                                    {
                                        string asass = string.Empty;
                                    }

                                    if (registroAsistencia.SALIDA != null && registroAsistencia.INGRESO != null)
                                    {
                                        if (registroAsistencia.SALIDA != string.Empty && registroAsistencia.INGRESO != string.Empty)
                                        {

                                            DateTime tiempoFinal = Convert.ToDateTime(registroAsistencia.FECHA.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " " + registroAsistencia.SALIDA);
                                            DateTime tiempoInicio = Convert.ToDateTime(registroAsistencia.FECHA.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " " + registroAsistencia.INGRESO);

                                            var timeSpan = tiempoFinal - tiempoInicio;
                                            registroAsistencia.tiempo = timeSpan.TotalHours.ToDoublePresentation();
                                            registroAsistencia.estadoMarcacion = "C";
                                        }

                                    }

                                    registroAsistencia.cargo = (listadoByDNICompleto.Count > 0 && listadoByDNICompleto.FirstOrDefault().cargo != null) ? listadoByDNICompleto.FirstOrDefault().cargo.ToString() : string.Empty;
                                    registroAsistencia.grupoTrabajador = (listadoByDNICompleto.Count > 0 && listadoByDNICompleto.FirstOrDefault().grupo != null) ? listadoByDNICompleto.FirstOrDefault().grupo.ToString() : string.Empty;

                                    listado.Add(registroAsistencia);
                                }
                                catch (Exception Ex)
                                {

                                    throw Ex;

                                }

                            }


                        }
                    }

                }
            }



            return listado;
        }

    }
}
