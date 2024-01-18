using Asistencia.Datos;
using Asistencia.Negocios;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Asistencia.Negocios
{
   public class DeviceTypeController
    {

        public List<Grupo> FindDeviceType(string conection) // Obtener comboBox tipo de dispositivos
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["SAS"];
            var typeOfDevices = new List<Grupo>();
            //seat.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

            if (conection != string.Empty)
            {
               using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
                {

                    typeOfDevices = (
                        from item in Modelo.SAS_DispositivoTipoDispositivo
                        where item.id.Trim() != string.Empty
                        group item by new { item.id } into j
                        select new Grupo
                        {
                            Codigo = j.Key.id.Trim(),
                            Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                        }
                        ).ToList();

                }
            }

            return typeOfDevices.OrderBy(x => x.Codigo).ToList();

        }

    }
}
