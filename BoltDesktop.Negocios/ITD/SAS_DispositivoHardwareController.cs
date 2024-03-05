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
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoHardwareByDevice(device.id).ToList();

            }

            return resultado;
        }

        public List<SAS_DispositivoHardwareByDeviceResult> ObtenerListadoDeCaracteristicaHardwarePorTipoDispositivoID(string conection, int DispositivoID, string TipoDispositivoID)
        {
            List<SAS_DispositivoHardwareByTipoDispositivoIDResult> resultado = new List<SAS_DispositivoHardwareByTipoDispositivoIDResult>();
            List<SAS_DispositivoHardwareByDeviceResult> resultadoTransformado = new List<SAS_DispositivoHardwareByDeviceResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoHardwareByTipoDispositivoID(TipoDispositivoID, DispositivoID).ToList();

                if (resultado.ToList().Count > 0)
                {
                    resultadoTransformado = (from item in resultado
                                             group item by new { item.IDUnico } into j
                                             select new SAS_DispositivoHardwareByDeviceResult
                                             {
                                                 capacidad = j.FirstOrDefault().capacidad,
                                                 codigoDispositivo = j.FirstOrDefault().codigoDispositivo != (int?)null ? j.FirstOrDefault().codigoDispositivo.Value : 0,
                                                 codigoHardware = j.FirstOrDefault().codigoHardware,
                                                 desde = j.FirstOrDefault().desde,
                                                 Estado = j.FirstOrDefault().Estado,
                                                 hardware = j.FirstOrDefault().hardware,
                                                 hasta = j.FirstOrDefault().hasta,
                                                 idestado = j.FirstOrDefault().idestado,
                                                 item = j.FirstOrDefault().item,
                                                 numeroParte = j.FirstOrDefault().numeroParte,
                                                 observacion = j.FirstOrDefault().observacion,
                                                 serie = j.FirstOrDefault().serie,
                                                 seVisualizaEnReportes = j.FirstOrDefault().seVisualizaEnReportes,
                                                 unidadMedidaCapacidad = j.FirstOrDefault().unidadMedidaCapacidad,
                                             }

                             ).ToList();

                }

            }

            return resultadoTransformado;
        }


    }
}
