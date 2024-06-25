using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios.RRHH.Tareo
{
   public class TareoMovilController
    {


        public List<SAS_TareoMovilListadoByFechasResult> GetTareoMovilListadoByFechas(string conection, string fechaDesde, string fechaHasta)
        {
            List<SAS_TareoMovilListadoByFechasResult> listado = new List<SAS_TareoMovilListadoByFechasResult>();
            
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (RRHHModelDataContext Modelo = new RRHHModelDataContext(cnx))
            {
                Modelo.CommandTimeout = 999909999;
                listado = Modelo.SAS_TareoMovilListadoByFechas(fechaDesde, fechaHasta).ToList();
            }

            return listado;
        }




    }
}
