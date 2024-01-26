using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios.Almacen
{
    public class SolicitudesDeRequerimientoInternoControllers
    {

        public List<SAS_RptSolicitudDeRequerimientoConProgramaSemanal> GetListadoReporteRequerimientoConProgramaSemanalByArea(string conection, string Desde, string Hasta, string AreaID )
        {
            List<SAS_RptSolicitudDeRequerimientoConProgramaSemanal> Listado = new List<SAS_RptSolicitudDeRequerimientoConProgramaSemanal>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                Listado = modelo.SAS_OBJREPORTE_RETURNREQCONSALIDAS("001", Desde, Hasta, AreaID,"","CON").ToList();
            }

            return Listado;
        }

        public List<SAS_ListadoAreaEnProgramaSemanalResult> GetListadoAreasConProgamasSemanal(string conection)
        {
            List<SAS_ListadoAreaEnProgramaSemanalResult> Listado = new List<SAS_ListadoAreaEnProgramaSemanalResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                Listado = modelo.SAS_ListadoAreaEnProgramaSemanal().ToList();
            }

            return Listado;
        }


    }
}
