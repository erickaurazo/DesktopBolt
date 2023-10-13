using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace Asistencia.Negocios.ProduccionPacking
{
    public class SAS_CondormidadDeCargaController
    {

        public List<SAS_ConformacionDeCargaByDateResult> ListAllByDates(string conection, string desde, string hasta)
        {
            List<SAS_ConformacionDeCargaByDateResult> List = new List<SAS_ConformacionDeCargaByDateResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                List = Modelo.SAS_ConformacionDeCargaByDate(desde, hasta).ToList();               
            }
            return List;  
        }

        public List<SAS_ListadoConformacionDeCargaPBIByIdResult> ListAllDetailById(string conection, int Id)
        {
            List<SAS_ListadoConformacionDeCargaPBIByIdResult> List = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                List = Modelo.SAS_ListadoConformacionDeCargaPBIById(Id).ToList();
            }
            return List;
        }

        public List<SAS_ListadoConformacionDeCargaPBI> ListAllDetailByDates(string conection, int Id)
        {
            List<SAS_ListadoConformacionDeCargaPBI> List = new List<SAS_ListadoConformacionDeCargaPBI>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                List = Modelo.SAS_ListadoConformacionDeCargaPBIs.ToList();
            }
            return List;
        }

    }
}
