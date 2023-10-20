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

        public int CambiarEstadoDeEvaluacion(string conection, int idEvaluacion)
        {
            int resultadoConsulta = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho item = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho();
                var list = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachos.Where(x => x.Id == idEvaluacion).ToList();

                if (list != null && list.ToList().Count > 0)
                {
                    item = list.ElementAt(0);
                    resultadoConsulta = item.Id;
                    if (item.EstadoIdFase01.Value == 0)
                    {
                        item.EstadoIdFase01 = 1;
                    }
                    else
                    {
                        item.EstadoIdFase01 = 0;
                    }
                }

                Modelo.SubmitChanges();

            }
            return resultadoConsulta;
        }

        public int CambiarEstadoDeDistribucion(string conection, int idEvaluacion)
        {
            int resultadoConsulta = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho item = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho();
                var list = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachos.Where(x => x.Id == idEvaluacion).ToList();

                if (list != null && list.ToList().Count > 0)
                {
                    item = list.ElementAt(0);
                    resultadoConsulta = item.Id;
                    if (item.EstadoIdFase02.Value == 0)
                    {
                        item.EstadoIdFase02 = 1;
                    }
                    else
                    {
                        item.EstadoIdFase02 = 0;
                    }
                }
                Modelo.SubmitChanges();
            }
            return resultadoConsulta;
        }

        public int CambiarEstadoDeRevision(string conection, int idEvaluacion)
        {
            int resultadoConsulta = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho item = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespacho();
                var list = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachos.Where(x => x.Id == idEvaluacion).ToList();

                if (list != null && list.ToList().Count > 0)
                {
                    item = list.ElementAt(0);
                    resultadoConsulta = item.Id;
                    if (item.EstadoIdFase03.Value == 0)
                    {
                        item.EstadoIdFase03 = 1;
                    }
                    else
                    {
                        item.EstadoIdFase03 = 0;
                    }
                }
                Modelo.SubmitChanges();
            }
            return resultadoConsulta;
        }
    }
}
