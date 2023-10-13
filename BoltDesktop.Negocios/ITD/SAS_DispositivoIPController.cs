using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{

    public class SAS_DispositivoIPController
    {

        public List<SAS_ListadoDeDispositivos> ListadoDeDispositivos(string conection)
        {
            List<SAS_ListadoDeDispositivos> resultado = new List<SAS_ListadoDeDispositivos>();
            List<SAS_ListadoDeDispositivos> resultado2 = new List<SAS_ListadoDeDispositivos>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeDispositivos.OrderBy(x=> x.nombres).ToList();

            }

            return resultado;
        }

        public SAS_ListadoDeDispositivosByIdDeviceResult GetDeviceByIdDevice(string conection, int idDispositivo)
        {
            List<SAS_ListadoDeDispositivosByIdDeviceResult> resultado = new List<SAS_ListadoDeDispositivosByIdDeviceResult>();
            SAS_ListadoDeDispositivosByIdDeviceResult resultado2 = new SAS_ListadoDeDispositivosByIdDeviceResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeDispositivosByIdDevice(idDispositivo).ToList();
                if (resultado.ToList().Count() > 1)
                {
                    resultado2 = resultado.ElementAt(0);
                }
                else if (resultado.ToList().Count() == 1)
                {
                    resultado2 = resultado.Single();
                }

            }
            return resultado2;
        }


    }
}
