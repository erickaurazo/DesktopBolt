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
    public class SAS_Calidad_Form_CheckListTrazabilidadFCLDespachoController
    {

        public List<SAS_ReporteCheckListRevisionByIdEvaluacionResult> GetListById(string conection, int CodigoEvaluacion)
        {
            List<SAS_ReporteCheckListRevisionByIdEvaluacionResult> ListAll = new List<SAS_ReporteCheckListRevisionByIdEvaluacionResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                ListAll = Modelo.SAS_ReporteCheckListRevisionByIdEvaluacion(CodigoEvaluacion).ToList();
            }

            return ListAll;
        }

        public List<SAS_ReporteCheckListRevisionByIdDatesResult> GetListByDate(string conection, string desde, string hasta)
        {
            List<SAS_ReporteCheckListRevisionByIdDatesResult> ListAll = new List<SAS_ReporteCheckListRevisionByIdDatesResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                ListAll = Modelo.SAS_ReporteCheckListRevisionByIdDates(desde, hasta).ToList();
            }

            return ListAll;
        }

    }
}
