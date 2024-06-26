using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios.SIG.SST.Registro_de_Capacitaciones
{
    public class RegistroDeCapacitacionesController
    {

        public List<ListadoRegistroCapacitacionesPorPeriodoResult> GetListByDate(string conection, string desde, string hasta)
        {
            List<ListadoRegistroCapacitacionesPorPeriodoResult> ListAll = new List<ListadoRegistroCapacitacionesPorPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltSSTDataContext Modelo = new BoltSSTDataContext(cnx))
            {
                ListAll = Modelo.ListadoRegistroCapacitacionesPorPeriodo(desde, hasta).ToList();
            }

            return ListAll;
        }


    }
}
