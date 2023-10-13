using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class DestinoMovilidadController
    {

        public List<SJ_RHDestinoMovilidad> ListadoDestinos(string conection)
        {
            List<SJ_RHDestinoMovilidad> listado = new List<SJ_RHDestinoMovilidad>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                listado = Modelo.SJ_RHDestinoMovilidads.ToList();
            }
            return listado;
        }

    }
}
