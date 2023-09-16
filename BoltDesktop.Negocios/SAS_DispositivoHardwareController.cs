using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
   public class SAS_DispositivoHardwareController
    {

        public List<SAS_DispositivoHardwareByDeviceResult> GetDispositivoHardwareByDevice(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoHardwareByDeviceResult> resultado = new List<SAS_DispositivoHardwareByDeviceResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoHardwareByDevice(device.id).ToList();

            }

            return resultado;
        }

    }
}
