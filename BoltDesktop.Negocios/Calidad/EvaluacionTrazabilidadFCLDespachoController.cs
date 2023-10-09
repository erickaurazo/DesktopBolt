using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios.Calidad
{
    public class EvaluacionTrazabilidadFCLDespachoController
    {
        public List<SAS_EvaluacionTrazabilidadFCLDespachoResult> GetListByDate(string conection, string desde, string hasta)
        {
            List<SAS_EvaluacionTrazabilidadFCLDespachoResult> ListAll = new List<SAS_EvaluacionTrazabilidadFCLDespachoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                ListAll = Modelo.SAS_EvaluacionTrazabilidadFCLDespacho(desde, hasta).ToList();
            }

            return ListAll;
        }


        


    }
}
