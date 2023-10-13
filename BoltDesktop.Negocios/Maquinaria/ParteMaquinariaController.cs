using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class ParteMaquinariaController
    {

        public List<SAS_ListadoPartesDeMaquinariaPorSemanaResult> GetListadoParteMaquinariaPorPeriodo(string conection, string desde, string hasta, string periodo, string semana)
        {

            List<SAS_ListadoPartesDeMaquinariaPorSemanaResult> listado = new List<SAS_ListadoPartesDeMaquinariaPorSemanaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (DBBDODataContext Modelo = new DBBDODataContext(cnx))
            {
                listado = Modelo.SAS_ListadoPartesDeMaquinariaPorSemana(desde, hasta, periodo, semana).ToList();
                //Modelo.Connection.Close();
            }
            return listado.OrderByDescending(x => x.fecha).ToList();

        }

        public List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult> GetListadoConsumoCombustibleDeMaquinariaPorPeriodo(string conection, string desde, string hasta, string periodo, string semana)
        {
            List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult> listado = new List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (DBBDODataContext Modelo = new DBBDODataContext(cnx))
            {
                listado = Modelo.SAS_ListadoCombustibleAbastecidosATractorPorSemana(desde, hasta, periodo, semana).ToList();
                Modelo.Connection.Close();
            }
            return listado.OrderByDescending(x => x.fecha).ToList();
        }

        public List<SAS_ListadoHorasDeTractorPorSemanaResult> GetListadoRecorridoPorGPSDeMaquinariaPorPeriodo(string conection, string desde, string hasta, string periodo, string semana)
        {
            List<SAS_ListadoHorasDeTractorPorSemanaResult> listado = new List<SAS_ListadoHorasDeTractorPorSemanaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDVISUALSATDataContext Modelo = new BDVISUALSATDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoHorasDeTractorPorSemana(desde, hasta, periodo, semana).ToList();
                Modelo.Connection.Close();
            }
            return listado.OrderByDescending(x => x.fecha).ToList();
        }


        public List<TractorVS> GetListadoTractoresHomologadosVS(string conection)
        {
            List<TractorVS> listado = new List<TractorVS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (DBBDODataContext Modelo = new DBBDODataContext(cnx))
            {
                listado = Modelo.TractorVS.ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }


        public List<TablaComparativaHorasVSNISIRA> GetTablaComparativaPorSemana(List<TractorVS> listadoTractoresHomologados, List<SAS_ListadoHorasDeTractorPorSemanaResult> listadoRegistroVisualSAT, List<SAS_ListadoPartesDeMaquinariaPorSemanaResult> listadoRegistroNISIRA, List<SAS_ListadoCombustibleAbastecidosATractorPorSemanaResult> listadoRegistroAbastecimientoCombustibleDeTractores)
        {
            List<TablaComparativaHorasVSNISIRA> listado = new List<TablaComparativaHorasVSNISIRA>();
            TablaComparativaHorasVSNISIRA oRegistro = new TablaComparativaHorasVSNISIRA();

            //1.. pasos totalizar partes maquinaria, por idmaquinaria, fecha
            #region Partes de maquinaria() 
            if (listadoRegistroNISIRA != null && listadoRegistroNISIRA.ToList().Count > 0)
            {
                #region Agrupar por Idmaquinaria y semana() 
                var listadoCodigoUnicoMaquinarias = (from item in listadoRegistroNISIRA
                                                     where item.idMaquinaria != null && item.idMaquinaria.Trim() != string.Empty
                                                     group item by new { item.idMaquinaria } into j
                                                     select new
                                                     {
                                                         idMaquinaria = j.Key.idMaquinaria
                                                     }).ToList();


                foreach (var itemMaquinaria in listadoCodigoUnicoMaquinarias)
                {
                    var listadoRegistroPorMaquinaria = listadoRegistroNISIRA.Where(x => x.idMaquinaria == itemMaquinaria.idMaquinaria).ToList();


                    var listadoSemanasDeTrabajoPorMaquinaria = (from item in listadoRegistroPorMaquinaria
                                                                where item.semana != null
                                                                group item by new { item.semana } into j
                                                                select new
                                                                {
                                                                    semana = j.Key.semana
                                                                }).ToList();

                    foreach (var itemSemanaPorMaquinaria in listadoSemanasDeTrabajoPorMaquinaria)
                    {
                        var listadoResultado = listadoRegistroPorMaquinaria.Where(x => x.semana == itemSemanaPorMaquinaria.semana).ToList();

                        oRegistro = new TablaComparativaHorasVSNISIRA();
                        oRegistro.periodo = listadoResultado.Max(x => x.PERIODO);
                        oRegistro.semana = itemSemanaPorMaquinaria.semana;
                        oRegistro.idMaquinaria = itemMaquinaria.idMaquinaria;
                        oRegistro.maquinaria = listadoResultado.Max(x => x.maquinaria);
                        oRegistro.horasVisualSAT = 0;
                        oRegistro.horasParteMaquinariaNISIRA = listadoResultado.Sum(x => x.diferencia);
                        oRegistro.horasDiferencia = 0;
                        oRegistro.cantidadCombustible = 0;
                        oRegistro.galonHoraPorParteMaquinaria = 0;
                        oRegistro.galonHoraPorRegistroVisualSAT = 0;
                        listado.Add(oRegistro);
                    }
                }
                #endregion
            }
            #endregion

            //2.- pasos totalizar Y homologar registros de visual SAT, por idmaquinaria, fecha
            #region Registro Visual SAT() 
            if (listadoRegistroVisualSAT != null && listadoRegistroVisualSAT.ToList().Count > 0)
            {
                #region Agrupar por Idmaquinaria y semana() 

                var listadoCodigoUnicoMaquinarias = (from vsat in listadoRegistroVisualSAT
                                                     where vsat.tracto != null && vsat.tracto.Trim() != string.Empty
                                                     group vsat by new { vsat.tracto } into j
                                                     select new
                                                     {
                                                         idMaquinaria = j.Key.tracto
                                                     }).ToList();

                foreach (var itemMaquinaria in listadoCodigoUnicoMaquinarias)
                {
                    var listadoRegistroPorMaquinaria = listadoRegistroVisualSAT.Where(x => x.tracto == itemMaquinaria.idMaquinaria).ToList();


                    var listadoSemanasDeTrabajoPorMaquinaria = (from item in listadoRegistroPorMaquinaria
                                                                where item.semana != null
                                                                group item by new { item.semana } into j
                                                                select new
                                                                {
                                                                    semana = j.Key.semana
                                                                }).ToList();

                    foreach (var itemSemanaPorMaquinaria in listadoSemanasDeTrabajoPorMaquinaria)
                    {
                        var listadoResultado = listadoRegistroPorMaquinaria.Where(x => x.semana == itemSemanaPorMaquinaria.semana).ToList();

                        ////var listadoResultado = from vsat in listadoRegistroPorMaquinaria
                        ////                       join regHomologados in listadoTractoresHomologados on vsat.tracto equals regHomologados.idConsumidor
                        ////                       where vsat.semana == itemSemanaPorMaquinaria.semana
                        ////                       select new SAS_ListadoHorasDeTractorPorSemanaResult
                        ////                       {

                        ////                           semana = vsat.semana,
                        ////                           tracto = regHomologados.idConsumidor,
                        ////                           horasTrabajadas = vsat.horasTrabajadas
                        //                       };

                        oRegistro = new TablaComparativaHorasVSNISIRA();
                        oRegistro.periodo = listadoResultado.FirstOrDefault().anio.ToString() + listadoResultado.FirstOrDefault().nroMes.ToString().PadLeft(2, '0');
                        oRegistro.semana = itemSemanaPorMaquinaria.semana;
                        oRegistro.idMaquinaria = listadoTractoresHomologados != null && listadoTractoresHomologados.ToList().Count() > 0 ? listadoTractoresHomologados.Where(x => x.TractorVisualSAT.Trim() == itemMaquinaria.idMaquinaria.Trim()).FirstOrDefault().idConsumidor.Trim(): string.Empty;
                        oRegistro.maquinaria = string.Empty;
                        oRegistro.horasVisualSAT = listadoResultado.Sum(x => x.horasTrabajadas);
                        oRegistro.horasParteMaquinariaNISIRA = 0;
                        oRegistro.horasDiferencia = 0;
                        oRegistro.cantidadCombustible = 0;
                        oRegistro.galonHoraPorParteMaquinaria = 0;
                        oRegistro.galonHoraPorRegistroVisualSAT = 0;
                        listado.Add(oRegistro);
                    }
                }
                #endregion
            }
            #endregion

            //3.- pasos totalizar el combustible
            #region Registro Combustible NISIRA() 
            if (listadoRegistroAbastecimientoCombustibleDeTractores != null && listadoRegistroAbastecimientoCombustibleDeTractores.ToList().Count > 0)
            {
                #region Agrupar por Idmaquinaria y semana() 
                var listadoCodigoUnicoMaquinarias = (from item in listadoRegistroAbastecimientoCombustibleDeTractores
                                                     where item.idConsumidor != null && item.idConsumidor.Trim() != string.Empty
                                                     group item by new { item.idConsumidor } into j
                                                     select new
                                                     {
                                                         idMaquinaria = j.Key.idConsumidor
                                                     }).ToList();


                foreach (var itemMaquinaria in listadoCodigoUnicoMaquinarias)
                {
                    var listadoRegistroPorMaquinaria = listadoRegistroAbastecimientoCombustibleDeTractores.Where(x => x.idConsumidor == itemMaquinaria.idMaquinaria).ToList();


                    var listadoSemanasDeTrabajoPorMaquinaria = (from item in listadoRegistroPorMaquinaria
                                                                where item.semana != null
                                                                group item by new { item.semana } into j
                                                                select new
                                                                {
                                                                    semana = j.Key.semana
                                                                }).ToList();

                    foreach (var itemSemanaPorMaquinaria in listadoSemanasDeTrabajoPorMaquinaria)
                    {
                        var listadoResultado = listadoRegistroPorMaquinaria.Where(x => x.semana == itemSemanaPorMaquinaria.semana).ToList();

                        oRegistro = new TablaComparativaHorasVSNISIRA();
                        oRegistro.periodo = listadoResultado.Max(x => x.periodo);
                        oRegistro.semana = itemSemanaPorMaquinaria.semana;
                        oRegistro.idMaquinaria = itemMaquinaria.idMaquinaria;
                        oRegistro.maquinaria = listadoResultado.Max(x=> x.maquinaria);
                        oRegistro.horasVisualSAT = 0;
                        oRegistro.horasParteMaquinariaNISIRA = 0;
                        oRegistro.horasDiferencia = 0;
                        oRegistro.cantidadCombustible = listadoResultado.Sum(x => x.cantidad);
                        oRegistro.galonHoraPorParteMaquinaria = 0;
                        oRegistro.galonHoraPorRegistroVisualSAT = 0;
                        listado.Add(oRegistro);
                    }
                }
                #endregion
            }
            #endregion

            //4.- pasos unir y comparar listado de l punto 1 y punto 2

            //5.- Al punto 4 generar el ratio de combustible por tractor por lista de NISIRA Y VS


            listado = (from item in listado
                       group item by new { item.idMaquinaria, item.periodo, item.semana } into j
                       select new TablaComparativaHorasVSNISIRA
                       {
                           periodo = j.Key.periodo.ToString(),
                           semana = j.Key.semana,
                           idMaquinaria = j.Key.idMaquinaria,
                           maquinaria = j.Max(x => x.maquinaria),
                           horasVisualSAT = j.Sum(x => x.horasVisualSAT),
                           horasParteMaquinariaNISIRA = j.Sum(x => x.horasParteMaquinariaNISIRA),
                           horasDiferencia = j.Sum(x => x.horasVisualSAT) - j.Sum(x => x.horasParteMaquinariaNISIRA),
                           cantidadCombustible = j.Sum(x => x.cantidadCombustible),
                           galonHoraPorParteMaquinaria = ((j.Sum(x => x.cantidadCombustible) >0 && j.Sum(x => x.horasParteMaquinariaNISIRA) > 0)  ? Math.Round(Convert.ToDecimal(j.Sum(x => x.cantidadCombustible) / Convert.ToDecimal(j.Sum(x => x.horasParteMaquinariaNISIRA))), 2) : 0),
                           galonHoraPorRegistroVisualSAT = ((j.Sum(x => x.cantidadCombustible) > 0 && j.Sum(x => x.horasVisualSAT) > 0) ? Math.Round(Convert.ToDecimal(j.Sum(x => x.cantidadCombustible) / Convert.ToDecimal(j.Sum(x => x.horasVisualSAT))), 2) : 0),
                       }).ToList();


            return listado;
        }


        public void TransferirInformacion(string conection)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (DBBDODataContext Modelo = new DBBDODataContext(cnx))
            {
                Modelo.SAS_ActualizarTransferenciaDeInformacion();
                Modelo.Connection.Close();
            }
        }


        // Metodo para listar filtros
        public List<SAS_ListadoFiltroReporteVisualSAT_NISIRAResult> ListadoFiltroReporteVisualSAT_NISIRA(string conection)
        {
            List<SAS_ListadoFiltroReporteVisualSAT_NISIRAResult> listado = new List<SAS_ListadoFiltroReporteVisualSAT_NISIRAResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (DBBDODataContext Modelo = new DBBDODataContext(cnx))
            {
                listado = Modelo.SAS_ListadoFiltroReporteVisualSAT_NISIRA().ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }


    }
}
