using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class RegistroTransferenciaTransportesController
    {
        string cnx = string.Empty;

        // listado de asistencia desde el timbrado movil en los bueses
        public List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> ObtenerListadoRegistroMarcacionPersonalEnBuses(string conection, string desde, string hasta)
        {

            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listado = new List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.ASJ_ListarRegistroMarcacionPersonalEnBuses(desde, hasta).ToList();

                return listado;
            }
        }

        // listado de asistencia desde el timbrado movil en los bueses
        public List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult> ListarAsistenciaSalidaUnidadesTransportePersonalByPeriod(string conection, string desde, string hasta)
        {

            List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult> listado = new List<SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodo(desde, hasta).ToList();
                return listado;
            }
        }

        // actualizar Placa
        public void UpdatePlaca(string conection, SJ_ListarAsistenciaSalidaUnidadesTransportePersonalByPeriodoResult asistance, string placaNew, string routerIdNew)
        {
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                var resultado = Modelo.ASJ_RegistroTransferenciaTransportes.Where(
                    x => x.placa.Trim() == asistance.placa.Trim() && 
                    x.fecha >= Convert.ToDateTime(asistance.fecha + " 00:00:00") 
                    && x.fecha <= Convert.ToDateTime(asistance.fecha + " 23:59:59")
                    ).ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        ASJ_RegistroTransferenciaTransportes transferenciaAsistencia = new ASJ_RegistroTransferenciaTransportes();
                        var resultadoSubConsulta = Modelo.ASJ_RegistroTransferenciaTransportes.Where(x => x.idRegistroMovil == item.idRegistroMovil).ToList();

                        if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                        {
                            transferenciaAsistencia = resultadoSubConsulta.Single();

                            if (placaNew.Trim() != string.Empty)
                            {
                                transferenciaAsistencia.placa = placaNew.Trim();
                            }

                            if (routerIdNew.Trim() != string.Empty)
                            {
                                transferenciaAsistencia.idRutaOrigen = Convert.ToInt32(routerIdNew);
                            }

                            Modelo.SubmitChanges();
                        }

                    }
                }

            }
        }

        // ReporteAsistenciaObservados
        public List<ASJ_ReporteAsistenciaObservadosResult> GetListAssistanceObserved(string conection, string desde, string hasta)
        {

            List<ASJ_ReporteAsistenciaObservadosResult> listado = new List<ASJ_ReporteAsistenciaObservadosResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                var ResultadoConsulta = Modelo.ASJ_ReporteAsistenciaObservados(desde, hasta).ToList();

                if (ResultadoConsulta != null && ResultadoConsulta.ToList().Count > 0)
                {
                    foreach (var item in ResultadoConsulta)
                    {
                        ASJ_ReporteAsistenciaObservadosResult oRegistro = new ASJ_ReporteAsistenciaObservadosResult();
                        oRegistro.selecionado = 0;
                        if (item.esimportado == 0)
                        {
                            if (item.nombres.ToString().Trim() != "DESCONOCIDO")
                            {
                                if (item.observacion.Trim() == String.Empty)
                                {
                                    oRegistro.selecionado = 1;
                                }
                            }
                        }

                        oRegistro.idempresa = item.idempresa;
                        oRegistro.IDCONTROLINGRESO = item.IDCONTROLINGRESO;
                        oRegistro.ITEM = item.ITEM;
                        oRegistro.CORRELATIVO = item.CORRELATIVO;
                        oRegistro.fecha = item.fecha;
                        oRegistro.idpersonal = item.idpersonal;
                        oRegistro.nombres = item.nombres;
                        oRegistro.codigoPlanilla = item.codigoPlanilla;
                        oRegistro.tipo = item.tipo;
                        oRegistro.tipoMarcacion = item.tipoMarcacion;
                        oRegistro.MARCACION = item.MARCACION;
                        oRegistro.fechatransferencia = item.fechatransferencia;
                        oRegistro.idusuario = item.idusuario;
                        oRegistro.origen = item.origen;
                        oRegistro.idorigen = item.idorigen;
                        oRegistro.puerta = item.puerta;
                        oRegistro.placa = item.placa;
                        oRegistro.BLOQUEADO = item.BLOQUEADO;
                        oRegistro.observacion = item.observacion;
                        oRegistro.RUC = item.RUC;
                        oRegistro.razonSocial = item.razonSocial;
                        oRegistro.esimportado = item.esimportado;
                        oRegistro.estadoRegistro = item.estadoRegistro;
                        listado.Add(oRegistro);
                    }

                }

                return listado;
            }
        }

        // asistencia observada por buss
        public List<ASJ_ReporteAsistenciaObservadosResult> GetListAssistanceObservedByBuss(string conection, string desde, string hasta)
        {

            List<ASJ_ReporteAsistenciaObservadosResult> listado = new List<ASJ_ReporteAsistenciaObservadosResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.ASJ_ReporteAsistenciaObservados(desde, hasta).Where(x => x.placa != string.Empty).ToList();

                return listado;
            }
        }

        // Asistencia por puerta
        public List<ASJ_ReporteAsistenciaByPuertaResult> GetListAssistanceByDoor(string conection, string desde, string hasta)
        {

            List<ASJ_ReporteAsistenciaByPuertaResult> listado = new List<ASJ_ReporteAsistenciaByPuertaResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.ASJ_ReporteAsistenciaByPuerta(desde, hasta).ToList();

                return listado;
            }
        }

        public List<AsistenciaBusByDia> GenerarAsistenciaResumida(List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetallado, List<ASJ_ReporteAsistenciaObservadosResult> listadoAsistenciaObservados, int incluirAsistenciaObervada, int incluirRecorridosInternos)
        {
            List<AsistenciaBusByDia> listado = new List<AsistenciaBusByDia>();
            List<AsistenciaBusByDia> listadoPresentacion = new List<AsistenciaBusByDia>();
            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetalladoAgregarObservados;
            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetalladoByReporte;

            listadoDetalladoByReporte = listadoDetallado.ToList();

            if (incluirAsistenciaObervada == 1)
            {

                listadoDetalladoAgregarObservados = (from item in listadoAsistenciaObservados
                                                     where item.idpersonal.Trim() != string.Empty
                                                     && (item.idpersonal.Trim().Length == 8)
                                                     && item.tipo == 'I'
                                                     group item by new { item.idpersonal, item.fecha, item.puerta, item.placa }
                                                     into j
                                                     select new ASJ_ListarRegistroMarcacionPersonalEnBusesResult
                                                     {
                                                         fecha = Convert.ToDateTime(j.Key.fecha.Value.ToShortDateString()),
                                                         placa = j.Key.placa.Trim(),
                                                         razonSocial = j.FirstOrDefault().razonSocial,
                                                         puerta = j.Key.puerta.Trim(),
                                                         dniColaborador = j.Key.idpersonal.Trim(),
                                                         ruta = "Obs",
                                                         tipo = 'I',
                                                         hora = j.FirstOrDefault().MARCACION != null ? j.FirstOrDefault().MARCACION : "00:00:00"
                                                     }

                                                     ).ToList();
                if (listadoDetalladoAgregarObservados != null && listadoDetalladoAgregarObservados.ToList().Count > 0)
                {
                    listadoDetalladoByReporte.AddRange(listadoDetalladoAgregarObservados);
                }
            }

            var listadoFechas = (from item in listadoDetalladoByReporte.OrderBy(x => x.hora).ToList()
                                 where item.dniColaborador != string.Empty
                                 && item.tipo == 'I'
                                 group item by new { item.fecha } into j
                                 select new
                                 {
                                     fechaAgrupada = j.Key.fecha,
                                 }
                                ).ToList();

            if (listadoFechas != null && listadoFechas.ToList().Count > 0)
            {

                foreach (var itemFecha in listadoFechas)
                {

                    var ListarTodasPlacaByFecha = listadoDetalladoByReporte.Where(x => x.fecha == itemFecha.fechaAgrupada.Value && x.tipo == 'I').ToList();

                    if (ListarTodasPlacaByFecha != null && ListarTodasPlacaByFecha.ToList().Count > 0)
                    {
                        var listaPlacas = (from item in ListarTodasPlacaByFecha.OrderBy(x => x.hora).ToList()
                                           where item.placa != string.Empty
                                           group item by new { item.placa } into j
                                           select new
                                           {
                                               placaAgrupada = j.Key.placa.Trim(),
                                               empresaTransporte = j.FirstOrDefault().razonSocial.Trim()
                                           }).ToList();

                        if (listaPlacas != null && listaPlacas.ToList().Count > 0)
                        {
                            foreach (var itemPlaca in listaPlacas)
                            {
                                var listaOnlyPlacaByFecha = listadoDetalladoByReporte.Where(x => x.fecha == itemFecha.fechaAgrupada.Value && x.placa.Trim() == itemPlaca.placaAgrupada.Trim() && x.tipo == 'I').ToList();

                                if (listaOnlyPlacaByFecha != null && listaOnlyPlacaByFecha.ToList().Count > 0)
                                {
                                    var listaPuertasIngreso = (from item in listaOnlyPlacaByFecha.OrderBy(x => x.hora).ToList()
                                                               where item.ruta != string.Empty
                                                               group item by new { item.puerta } into j
                                                               select new
                                                               {
                                                                   PuertaAgrupada = j.Key.puerta.Trim(),
                                                               }).ToList();

                                    if (listaPuertasIngreso != null && listaPuertasIngreso.ToList().Count > 0)
                                    {

                                        foreach (var itemPuerta in listaPuertasIngreso)
                                        {
                                            var resultadoByPuerta = listaOnlyPlacaByFecha.Where(x => x.puerta == itemPuerta.PuertaAgrupada).ToList();

                                            if (resultadoByPuerta != null && resultadoByPuerta.ToList().Count > 0)
                                            {
                                                var listadoByPersona = (from item in resultadoByPuerta.OrderBy(x => x.hora).ToList()
                                                                        where item.dniColaborador != string.Empty
                                                                        group item by new { item.dniColaborador } into j
                                                                        select new
                                                                        {
                                                                            dniAgrupado = j.Key.dniColaborador.Trim(),
                                                                        }).ToList();

                                                AsistenciaBusByDia oAsistencia = new AsistenciaBusByDia();
                                                oAsistencia.fecha = itemFecha.fechaAgrupada.Value;
                                                oAsistencia.ruta = resultadoByPuerta.FirstOrDefault().ruta;
                                                oAsistencia.empresaTransporte = itemPlaca.empresaTransporte;
                                                oAsistencia.placa = itemPlaca.placaAgrupada;
                                                oAsistencia.capacidad = resultadoByPuerta.FirstOrDefault().capacidadTransporte;

                                                switch (itemPuerta.PuertaAgrupada)
                                                {
                                                    case "BOTA":
                                                        oAsistencia.Bota = listadoByPersona.Count;
                                                        break;
                                                    case "BALSA":
                                                        oAsistencia.Balsa = listadoByPersona.Count;
                                                        break;
                                                    case "TABLAZO":
                                                        oAsistencia.Tablazo = listadoByPersona.Count;
                                                        break;
                                                    case "SANTA MARIA":
                                                        oAsistencia.SantaMaria = listadoByPersona.Count;
                                                        break;
                                                    case "IMP":
                                                        oAsistencia.IMP = listadoByPersona.Count;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                listado.Add(oAsistencia);
                                            }

                                        }

                                    }
                                }
                            }

                        }
                    }


                }

            }

            if (listado != null && listado.ToList().Count > 0)
            {
                listadoPresentacion = (from item in listado
                                       where item.placa != string.Empty
                                       group item by new
                                       {
                                           item.placa,
                                           item.fecha,
                                           //item.ruta,
                                           item.empresaTransporte,
                                           item.capacidad

                                       } into j
                                       select new AsistenciaBusByDia
                                       {
                                           fecha = j.Key.fecha,
                                           ruta = j.FirstOrDefault().ruta,
                                           empresaTransporte = j.Key.empresaTransporte,
                                           placa = j.Key.placa,
                                           capacidad = j.Key.capacidad,
                                           Bota = j.Sum(x => x.Bota),
                                           Tablazo = j.Sum(x => x.Tablazo),
                                           Balsa = j.Sum(x => x.Balsa),
                                           SantaMaria = j.Sum(x => x.SantaMaria),
                                           IMP = j.Sum(x => x.IMP),
                                           totalAsistencia = j.Sum(x => x.Bota) + j.Sum(x => x.Tablazo) + j.Sum(x => x.Balsa) + j.Sum(x => x.SantaMaria) + j.Sum(x => x.IMP),
                                       }
                                  ).ToList();
            }



            return listadoPresentacion;
        }


        public List<AsistenciaBusByDia> GenerarAsistenciaResumidaSinRecorridosInternos(List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetallado, List<ASJ_ReporteAsistenciaObservadosResult> listadoAsistenciaObservados, int incluirAsistenciaObervada, int incluirRecorridosInternos)
        {
            List<AsistenciaBusByDia> listado = new List<AsistenciaBusByDia>();
            List<AsistenciaBusByDia> listadoPresentacion = new List<AsistenciaBusByDia>();
            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listaConformadaParaDepurar = new List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult>();
            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetalladoAgregarObservados;
            List<ASJ_ListarRegistroMarcacionPersonalEnBusesResult> listadoDetalladoByReporte;

            listadoDetalladoByReporte = listadoDetallado.ToList();

            if (incluirAsistenciaObervada == 1)
            {

                listadoDetalladoAgregarObservados = (from item in listadoAsistenciaObservados
                                                     where item.idpersonal.Trim() != string.Empty
                                                     && (item.idpersonal.Trim().Length == 8)
                                                     && item.tipo == 'I'
                                                     group item by new { item.idpersonal, item.fecha, item.puerta, item.placa }
                                                     into j
                                                     select new ASJ_ListarRegistroMarcacionPersonalEnBusesResult
                                                     {
                                                         fecha = Convert.ToDateTime(j.Key.fecha.Value.ToShortDateString()),
                                                         placa = j.Key.placa.Trim(),
                                                         razonSocial = j.FirstOrDefault().razonSocial,
                                                         puerta = j.Key.puerta.Trim(),
                                                         dniColaborador = j.Key.idpersonal.Trim(),
                                                         ruta = "Obs",
                                                         tipo = 'I',
                                                         hora = j.FirstOrDefault().MARCACION != null ? j.FirstOrDefault().MARCACION : "00:00:00"
                                                     }

                                                     ).ToList();
                if (listadoDetalladoAgregarObservados != null && listadoDetalladoAgregarObservados.ToList().Count > 0)
                {
                    listadoDetalladoByReporte.AddRange(listadoDetalladoAgregarObservados);
                }
            }

            // en esta lista obtengo el unico registro por colaborador.
            #region Discriminar por lista neta por placa y paradero.
            var listadoFechasResultados = (from item in listadoDetalladoByReporte.OrderBy(x => x.hora).ToList()
                                           where item.dniColaborador != string.Empty
                                           && item.tipo == 'I'
                                           group item by new { item.fecha } into j
                                           select new
                                           {
                                               fechaAgrupada = j.Key.fecha,
                                           }
                               ).ToList();

            foreach (var fechaResultado in listadoFechasResultados)
            {
                var subconsult01 = listadoDetalladoByReporte.Where(x => x.tipo == 'I' && x.fecha.Value == fechaResultado.fechaAgrupada.Value).OrderBy(x => Convert.ToDateTime(x.hora)).ToList();

                var listaPersonas = (from item in subconsult01
                                     where item.placa != string.Empty
                                     group item by new { item.dniColaborador } into j
                                     select new
                                     {
                                         codigoColaborador = j.Key.dniColaborador.Trim()
                                     }).ToList();

                if (listaPersonas != null && listaPersonas.ToList().Count > 0)
                {
                    foreach (var oTrabajador in listaPersonas)
                    {
                        var subconsult02 = subconsult01.Where(x => x.dniColaborador.Trim() == oTrabajador.codigoColaborador.Trim()).ToList();

                        if (subconsult02 != null && subconsult02.ToList().Count > 1)
                        {
                            #region Analizar Caso y actualizar lista

                            // este escenario es cuanto tiene dos marcaciones con el mismo transporte o cuando se ha desplazado con diferentes transportes.

                            var totalPlacas = (from item in subconsult02
                                               where item.placa != string.Empty
                                               group item by new { item.placa } into j
                                               select new
                                               {
                                                   placaUnidadTransporte = j.Key.placa.Trim()
                                               }).ToList();

                            //int listaUnica = 0;
                            //int OtrasListas = 0;
                            //int ListaGeneral = 0;

                            if (totalPlacas != null && totalPlacas.ToList().Count > 1)
                            {
                                #region
                                // cuando se ha desplazado con diferentes transportes.

                                // verifico primero cada transporte para ver en cual de ellos llego primero.


                                // tomo el primero
                                var subconsult021 = subconsult02.ToList();
                                string hora = subconsult021.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;

                                string horaInicioPlacaByPuerta1 = string.Empty;
                                string horaInicioPlacaByPuerta2 = string.Empty;
                                string horaInicioPlacaByPuerta3 = string.Empty;
                                string horaInicioPlacaByPuerta4 = string.Empty;
                                string horaInicioPlacaByPuerta5 = string.Empty;

                                string horUltimaMarcacioIngresonPlacaByPuerta1 = string.Empty;
                                string horUltimaMarcacioIngresonPlacaByPuerta2 = string.Empty;
                                string horUltimaMarcacioIngresonPlacaByPuerta3 = string.Empty;
                                string horUltimaMarcacioIngresonPlacaByPuerta4 = string.Empty;
                                string horUltimaMarcacioIngresonPlacaByPuerta5 = string.Empty;

                                int contador = 1;
                                foreach (var placaVehiculo in totalPlacas)
                                {
                                    var subconsult0211 = subconsult01.Where(x => x.placa == placaVehiculo.placaUnidadTransporte).ToList();

                                    if (contador == 1)
                                    {
                                        horaInicioPlacaByPuerta1 = subconsult0211.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                        horUltimaMarcacioIngresonPlacaByPuerta1 = subconsult0211.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                    }
                                    else if (contador == 2)
                                    {
                                        horaInicioPlacaByPuerta2 = subconsult0211.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                        horUltimaMarcacioIngresonPlacaByPuerta2 = subconsult0211.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                    }
                                    else if (contador == 3)
                                    {
                                        horaInicioPlacaByPuerta3 = subconsult0211.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                        horUltimaMarcacioIngresonPlacaByPuerta3 = subconsult0211.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                    }
                                    else if (contador == 4)
                                    {
                                        horaInicioPlacaByPuerta4 = subconsult0211.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                        horUltimaMarcacioIngresonPlacaByPuerta4 = subconsult0211.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                    }
                                    else if (contador == 4)
                                    {
                                        horaInicioPlacaByPuerta5 = subconsult0211.OrderBy(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                        horUltimaMarcacioIngresonPlacaByPuerta5 = subconsult0211.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;
                                    }
                                    contador += 1;
                                }


                                foreach (var registroByPlaca in subconsult021)
                                {
                                    if (registroByPlaca.hora == hora)
                                    {
                                        #region Actualizar registro como único
                                        registroByPlaca.UnicoLista = 1;
                                        listaConformadaParaDepurar.Add(registroByPlaca);
                                        #endregion
                                    }
                                    else
                                    {
                                        listaConformadaParaDepurar.Add(registroByPlaca);
                                    }
                                }

                                // una vez que tenga la hora, verifico si fue ese carro el primero en ingresar al primer punto, si no es asi no se considera.

                                #endregion  

                            }
                            else
                            {
                                #region 
                                //Verificar la hora ingreso de la placa del transporte, considero la ultima timbrada
                                // solo se considera como lista unica, cuando el caso es que el personal tiene recorridos internos, por tanto se tomará la última marcacion
                                foreach (var oPlaca in totalPlacas)
                                {
                                    var subconsult03 = subconsult02.Where(x => x.placa.Trim() == oPlaca.placaUnidadTransporte.Trim()).ToList();

                                    string hora = subconsult03.OrderByDescending(x => Convert.ToDateTime(x.hora)).FirstOrDefault().hora;

                                    foreach (var registroByPlaca in subconsult03)
                                    {
                                        if (registroByPlaca.hora == hora)
                                        {
                                            #region Actualizar registro como único
                                            registroByPlaca.UnicoLista = 1;
                                            listaConformadaParaDepurar.Add(registroByPlaca);
                                            #endregion
                                        }
                                        else
                                        {
                                            registroByPlaca.UnicoLista = 0;
                                            listaConformadaParaDepurar.Add(registroByPlaca);
                                        }
                                    }

                                }

                                #endregion
                            }


                            #endregion
                        }
                        else
                        {
                            #region Actualizar lista
                            subconsult02.Select(c =>
                            {
                                c.UnicoLista = 1;
                                return c;
                            }).ToList();

                            listaConformadaParaDepurar.AddRange(subconsult02);

                            #endregion
                        }

                    }
                }
            }








            #endregion
            var subconsult04 = listaConformadaParaDepurar.Where(x => x.UnicoLista == 1).ToList();

            var listadoFechas = (from item in subconsult04.OrderBy(x => x.hora).ToList()
                                 where item.dniColaborador != string.Empty
                                 && item.tipo == 'I'
                                 group item by new { item.fecha } into j
                                 select new
                                 {
                                     fechaAgrupada = j.Key.fecha,
                                 }
                                ).ToList();

            if (listadoFechas != null && listadoFechas.ToList().Count > 0)
            {

                foreach (var itemFecha in listadoFechas)
                {

                    var subconsult05 = subconsult04.Where(x => x.fecha == itemFecha.fechaAgrupada.Value && x.tipo == 'I').ToList();

                    if (subconsult05 != null && subconsult05.ToList().Count > 0)
                    {
                        var listaPlacas = (from item in subconsult05.OrderBy(x => x.hora).ToList()
                                           where item.placa != string.Empty
                                           group item by new { item.placa } into j
                                           select new
                                           {
                                               placaAgrupada = j.Key.placa.Trim(),
                                               empresaTransporte = j.FirstOrDefault().razonSocial.Trim()
                                           }).ToList();

                        if (listaPlacas != null && listaPlacas.ToList().Count > 0)
                        {
                            foreach (var itemPlaca in listaPlacas)
                            {
                                var subconsult06 = subconsult05.Where(x => x.fecha == itemFecha.fechaAgrupada.Value && x.placa.Trim() == itemPlaca.placaAgrupada.Trim() && x.tipo == 'I').ToList();

                                if (subconsult06 != null && subconsult06.ToList().Count > 0)
                                {
                                    var listaPuertasIngreso = (from item in subconsult06.OrderBy(x => x.hora).ToList()
                                                               where item.ruta != string.Empty
                                                               group item by new { item.puerta } into j
                                                               select new
                                                               {
                                                                   PuertaAgrupada = j.Key.puerta.Trim(),
                                                               }).ToList();




                                    if (listaPuertasIngreso != null && listaPuertasIngreso.ToList().Count > 0)
                                    {

                                        foreach (var itemPuerta in listaPuertasIngreso)
                                        {
                                            var subconsult07 = subconsult06.Where(x => x.puerta == itemPuerta.PuertaAgrupada).ToList();

                                            if (subconsult07 != null && subconsult07.ToList().Count > 0)
                                            {
                                                var listadoByPersona = (from item in subconsult07.OrderBy(x => x.hora).ToList()
                                                                        where item.dniColaborador != string.Empty
                                                                        group item by new { item.dniColaborador } into j
                                                                        select new
                                                                        {
                                                                            dniAgrupado = j.Key.dniColaborador.Trim(),
                                                                        }).ToList();



                                                AsistenciaBusByDia oAsistencia = new AsistenciaBusByDia();
                                                oAsistencia.fecha = itemFecha.fechaAgrupada.Value;
                                                oAsistencia.ruta = subconsult07.FirstOrDefault().ruta;
                                                oAsistencia.empresaTransporte = itemPlaca.empresaTransporte;
                                                oAsistencia.placa = itemPlaca.placaAgrupada;
                                                oAsistencia.capacidad = subconsult07.FirstOrDefault().capacidadTransporte;

                                                switch (itemPuerta.PuertaAgrupada)
                                                {
                                                    case "BOTA":
                                                        oAsistencia.Bota = listadoByPersona.Count;
                                                        break;
                                                    case "BALSA":
                                                        oAsistencia.Balsa = listadoByPersona.Count;
                                                        break;
                                                    case "TABLAZO":
                                                        oAsistencia.Tablazo = listadoByPersona.Count;
                                                        break;
                                                    case "SANTA MARIA":
                                                        oAsistencia.SantaMaria = listadoByPersona.Count;
                                                        break;
                                                    case "IMP":
                                                        oAsistencia.IMP = listadoByPersona.Count;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                listado.Add(oAsistencia);
                                            }

                                        }

                                    }
                                }
                            }

                        }
                    }


                }

            }

            if (listado != null && listado.ToList().Count > 0)
            {
                listadoPresentacion = (from item in listado
                                       where item.placa != string.Empty
                                       group item by new
                                       {
                                           item.placa,
                                           item.fecha,
                                           //item.ruta,
                                           item.empresaTransporte,
                                           item.capacidad

                                       } into j
                                       select new AsistenciaBusByDia
                                       {
                                           fecha = j.Key.fecha,
                                           ruta = j.FirstOrDefault().ruta,
                                           empresaTransporte = j.Key.empresaTransporte,
                                           placa = j.Key.placa,
                                           capacidad = j.Key.capacidad,
                                           Bota = j.Sum(x => x.Bota),
                                           Tablazo = j.Sum(x => x.Tablazo),
                                           Balsa = j.Sum(x => x.Balsa),
                                           SantaMaria = j.Sum(x => x.SantaMaria),
                                           IMP = j.Sum(x => x.IMP),
                                           totalAsistencia = j.Sum(x => x.Bota) + j.Sum(x => x.Tablazo) + j.Sum(x => x.Balsa) + j.Sum(x => x.SantaMaria) + j.Sum(x => x.IMP),
                                       }
                                  ).ToList();
            }



            return listadoPresentacion;
        }


    }
}
