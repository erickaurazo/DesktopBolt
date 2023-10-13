using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class SemanaController
    {

        //ObtenerSemanaPorNroSemana
        public SJ_Semanas GetWeekByNumberWeek(SJ_Semanas week, string conection)
        {
            SJ_Semanas oSemana = new SJ_Semanas();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            try
            {

                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    var resultadoConsulta = Modelo.SJ_Semanas.Where(x => x.año == week.año && x.semana == week.semana).ToList();

                    if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                    {
                        oSemana = resultadoConsulta.Single();
                    }
                    else
                    {
                        oSemana = new SJ_Semanas();
                        oSemana.semana = week.semana;
                        oSemana.año = week.año;
                        oSemana.desde = DateTime.Now;
                        oSemana.hasta = DateTime.Now;
                    }
                    return oSemana;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

         

        }

    }
}
