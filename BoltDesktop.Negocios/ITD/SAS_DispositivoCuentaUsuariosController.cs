using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace Asistencia.Negocios
{
    public class SAS_DispositivoCuentaUsuariosController
    {

        public List<SAS_DispositivoCuentaUsuariosByDeviceResult> GetDispositivoCuentaUsuariosByDevice(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoCuentaUsuariosByDeviceResult> resultado = new List<SAS_DispositivoCuentaUsuariosByDeviceResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoCuentaUsuariosByDevice(device.id).ToList();

            }

            return resultado;
        }

    }
}
