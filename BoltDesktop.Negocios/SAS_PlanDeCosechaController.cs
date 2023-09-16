using Asistencia.Datos;
using Asistencia.Datos.MRP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;


namespace Asistencia.Negocios
{
    public class SAS_PlanDeCosechaController
    {



        public List<SAS_PlanDeCosechaListadoFull> GetList(string conection, string desde, string hasta)
        {
            List<SAS_PlanDeCosechaListadoFull> listado = new List<SAS_PlanDeCosechaListadoFull>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_PlanDeCosechaListadoFull.Where(x => x.fecha >= Convert.ToDateTime(desde) && x.fecha <= Convert.ToDateTime(hasta)).ToList();
            }
            return listado;
        }


        public List<SAS_PlanDeCosechaDetalleListadoFull> GetListDetailByID(string conection, int idplan)
        {
            List<SAS_PlanDeCosechaDetalleListadoFull> listado = new List<SAS_PlanDeCosechaDetalleListadoFull>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_PlanDeCosechaDetalleListadoFull.Where(x => x.idplan == idplan).ToList();
            }
            return listado;
        }

        public List<Grupo> ObtenerListadoSemana(List<SAS_PlanDeCosechaDetalleListadoFull> listing)
        {

            List<Grupo> listado = new List<Grupo>();


            listado = (from item in listing
                       group item by item.anio + "-" + item.semana into j
                       select new Grupo { Codigo = j.Key }
                       ).ToList();

            return listado;

        }

        public List<Grupo> ObtenerListadoSemana(List<SAS_PlanDeCosechaDetalleByJabasListadoFull> listing)
        {

            List<Grupo> listado = new List<Grupo>();


            listado = (from item in listing
                       group item by item.anio + "-" + item.semana into j
                       select new Grupo { Codigo = j.Key }
                       ).ToList();

            return listado;

        }

        


        public List<SAS_PlanDeCosechaDetalleByJabasListadoFull> GetListFull(string conection, int idplan)
        {
            List<SAS_PlanDeCosechaDetalleByJabasListadoFull> listado = new List<SAS_PlanDeCosechaDetalleByJabasListadoFull>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_PlanDeCosechaDetalleByJabasListadoFull.Where(x => x.idplan == idplan).ToList();
            }
            return listado;
        }


        public List<SAS_ListadoVersionPlanCosecha> GetListVersionPlanCosecha(string conection)
        {
            List<SAS_ListadoVersionPlanCosecha> listado = new List<SAS_ListadoVersionPlanCosecha>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoVersionPlanCosecha.OrderByDescending(x => x.codigo).ToList();
            }
            return listado;

        }
        public List<PlanCosechaAgrupado> ConvertGetListFull(List<SAS_PlanDeCosechaDetalleByJabasListadoFull> listFull)
        {
            List<PlanCosechaAgrupado> listado = new List<PlanCosechaAgrupado>();

            listado = (from item in listFull
                       group item by new  { item.idplan, item.mes, item.tipoCultivo, item.variedad, item.cultivo, item.idconsumidor, item.consumidor, item.cantidad, item.sector, item.periodoSemana, item.pesoPromedioJabaPorVariedad, item.numeroJabas, item.contenedores } into j
                       select new PlanCosechaAgrupado {
                           idplan = j.Key.idplan ,
                           mes = j.Key.mes,
                           tipocultivo = j.Key.tipoCultivo.Trim(),
                           variedad = j.Key.variedad.Trim(),
                           cultivo = j.Key.cultivo.Trim(),
                           idconsumidor = j.Key.idconsumidor.Trim(),
                           consumidor = j.Key.consumidor.Trim(),
                           cantidad = j.Key.cantidad,
                           sector = j.Key.sector,
                           periodoSemana = j.Key.periodoSemana,
                           pesoPromedioJabaPorVariedad = j.Key.pesoPromedioJabaPorVariedad,
                           numeroJabas = j.Key.numeroJabas,
                           contenedores = j.Key.contenedores,
                       }
                       ).ToList();

            return listado;

        }

        public List<Grupo> ObtenerTipoUnidadMedidaParaPresentacionDePlanDeCosecha()
        {
            List<Grupo> listado = new List<Grupo>();

            listado.Add(new Grupo { Codigo = "Kilogramos".ToUpper(), Descripcion = "Kilogramos".ToUpper() });
            listado.Add(new Grupo { Codigo = "JABAS".ToUpper(), Descripcion = "JABAS".ToUpper() });
            listado.Add(new Grupo { Codigo = "CONTENEDORES EQUIVALENTES", Descripcion = "CONTENEDORES EQUIVALENTES" });
            return listado;

        }


        

    }
}

