using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
   public class RevisionDatosContablesControllers
    {

        public List<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult> ObtenerListadoDetalldoMovimientoCuentaPorModuloEntrePeriodo(string conection, string idcuenta, string periodoHasta)
        {
            List<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult> listado = new List<SAS_ListadoDetalleCuentasPorModuloEntrePeriodoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999999;
                listado = Modelo.SAS_ListadoDetalleCuentasPorModuloEntrePeriodo(idcuenta,periodoHasta, periodoHasta).ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }


        public List<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult> ObtenerListadoDetalldoMovimientoCuentaPorBalanceEntrePeriodo(string conection, string idcuenta, string periodoHasta)
        {
            List<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult> listado = new List<SAS_ListadoDetalleCuentasPorBalanceEntrePeriodoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999999;
                listado = Modelo.SAS_ListadoDetalleCuentasPorBalanceEntrePeriodo(idcuenta, periodoHasta, periodoHasta).ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }


    }
}
