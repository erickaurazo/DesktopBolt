using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoDocumentoController
    {

        public List<SAS_DispositivoDocumentoByDeviceResult> GetDispositivoDocumentoByDevice(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoDocumentoByDeviceResult> resultado = new List<SAS_DispositivoDocumentoByDeviceResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoDocumentoByDevice(device.id).ToList();
            }
            return resultado;
        }

    }
}
