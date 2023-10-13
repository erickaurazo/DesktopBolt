using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoMovimientoAlmacenController
    {

        // get listing by code | Obtener listado por codigo
        public List<SAS_DispositivoWharehouseMovementsByDeviceIDResult> GetListingByCode(string conection, int codigo)
        {
            List<SAS_DispositivoWharehouseMovementsByDeviceIDResult> listado = new List<SAS_DispositivoWharehouseMovementsByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoWharehouseMovementsByDeviceID(codigo).ToList();
            }
            return listado;
        }


        public List<SAS_DispositivoWharehouseMovementsByDeviceIDResult> GetListingByCode(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoWharehouseMovementsByDeviceIDResult> listado = new List<SAS_DispositivoWharehouseMovementsByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoWharehouseMovementsByDeviceID(device.id).ToList();
            }
            return listado;
        }


    }
}
