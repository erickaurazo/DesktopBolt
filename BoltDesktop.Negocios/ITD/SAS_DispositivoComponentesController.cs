using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoComponentesController
    {
        public List<SAS_DispositivoComponentesByDeviceResult> GetDispositivoCuentaUsuariosByDevice(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoComponentesByDeviceResult> resultado = new List<SAS_DispositivoComponentesByDeviceResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoComponentesByDevice(device.id).ToList();
            }
            return resultado;
        }
    }
}
