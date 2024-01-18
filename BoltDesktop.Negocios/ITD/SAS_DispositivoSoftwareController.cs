using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoSoftwareController
    {

        public List<SAS_DispositivoSoftwareByDeviceResult> GetDispositivoSoftwareByDevice(string conection, SAS_Dispostivo device )
        {
            List<SAS_DispositivoSoftwareByDeviceResult> resultado = new List<SAS_DispositivoSoftwareByDeviceResult>();
            

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoSoftwareByDevice(device.id).ToList();

            }

            return resultado;
        }


    }
}
