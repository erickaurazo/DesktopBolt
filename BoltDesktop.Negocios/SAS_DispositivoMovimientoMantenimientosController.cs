using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;


namespace Asistencia.Negocios
{
    public class SAS_DispositivoMovimientoMantenimientosController
    {
        public List<SAS_DispositivoMaintenanceByDeviceIDResult> GetListingByCode(string conection, int codigo)
        {
            List<SAS_DispositivoMaintenanceByDeviceIDResult> listado = new List<SAS_DispositivoMaintenanceByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoMaintenanceByDeviceID(codigo).ToList();
            }
            return listado;
        }



        public List<SAS_DispositivoMaintenanceByDeviceIDResult> GetListingByCode(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoMaintenanceByDeviceIDResult> listado = new List<SAS_DispositivoMaintenanceByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoMaintenanceByDeviceID(device.id).ToList();
            }
            return listado;
        }


    }
}
