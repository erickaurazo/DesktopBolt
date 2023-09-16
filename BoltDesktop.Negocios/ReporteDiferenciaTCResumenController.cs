using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
   public class ReporteDiferenciaTCResumenController
    {


        public List<SAS_ListarCuentasDiferenciaTCResumen01Result> ObtenerListadoResumenItem0001_008(string conection, string codigoEmpresa, string desde, string hasta)
        {
            List<SAS_ListarCuentasDiferenciaTCResumen01Result> resultado = new List<SAS_ListarCuentasDiferenciaTCResumen01Result>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListarCuentasDiferenciaTCResumen01(codigoEmpresa , desde, hasta).ToList();
            }

            return resultado;
        }


        public List<SAS_ListarCuentasDiferenciaTCResumen02Result> ObtenerListadoResumenItem0009(string conection, string codigoEmpresa, string desde, string hasta)
        {
            List<SAS_ListarCuentasDiferenciaTCResumen02Result> resultado = new List<SAS_ListarCuentasDiferenciaTCResumen02Result>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListarCuentasDiferenciaTCResumen02(codigoEmpresa, desde, hasta).ToList();
            }

            return resultado;
        }


    }
}
