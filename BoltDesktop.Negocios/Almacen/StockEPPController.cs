using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios.Almacen
{
   public class StockEPPController
    {

        public List<SAS_ListadoEPPResult> ListAll(string conection)
        {
            List<SAS_ListadoEPPResult> Listado = new List<SAS_ListadoEPPResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AlmacenModelDataContext modelo = new AlmacenModelDataContext(cnx))
            {
                Listado = modelo.SAS_ListadoEPP().ToList();
            }

            return Listado;
        }

        public List<SAS_ListadoEPPResult> ListNoEmpty(string conection)
        {
            List<SAS_ListadoEPPResult> Listado = new List<SAS_ListadoEPPResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AlmacenModelDataContext modelo = new AlmacenModelDataContext(cnx))
            {
                Listado = modelo.SAS_ListadoEPP().Where(x=> x.cantidadFactor.Value > 0).ToList();
            }

            return Listado;
        }

        public int Notificar(string conection)
        {
           
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AlmacenModelDataContext modelo = new AlmacenModelDataContext(cnx))
            {
                modelo.SAS_EnvioNotificacionesStockEPP();
            }

            return 1;
        }

    }
}
