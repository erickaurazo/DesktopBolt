using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_SoporteCanalDeAtencionController
    {

        public List<SAS_SoporteCanalDeAtencion> ListAll(string conection)
        {
            List<SAS_SoporteCanalDeAtencion> result = new List<SAS_SoporteCanalDeAtencion>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_SoporteCanalDeAtencion.ToList();
            }
            return result;
        }

    }
}
