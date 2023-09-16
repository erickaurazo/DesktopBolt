using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Busquedas;


namespace Asistencia.Negocios
{
  public  class SAS_RegistroCompraEquipoCelularController
    {

        public List<SAS_RegistroCompraEquipoCelularesDetallePendientesResult> GetListDetailToDatePendiente(string conection, string fechaHasta)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroCompraEquipoCelularesDetallePendientesResult> listado = new List<SAS_RegistroCompraEquipoCelularesDetallePendientesResult>();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroCompraEquipoCelularesDetallePendientes(fechaHasta).ToList();
            }

            return listado;
        }


        public List<SAS_RegistroCompraEquipoCelularesAll> GetListAll(string conection)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroCompraEquipoCelularesAll> listado = new List<SAS_RegistroCompraEquipoCelularesAll>();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroCompraEquipoCelularesAll.ToList();
            }

            return listado;
        }


        public List<SAS_RegistroCompraEquipoCelularesByDatesResult> GetListAllByDates(string conection, string desde, string hasta)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroCompraEquipoCelularesByDatesResult> listado = new List<SAS_RegistroCompraEquipoCelularesByDatesResult>();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroCompraEquipoCelularesByDates(desde, hasta).ToList();
            }

            return listado;
        }


        public SAS_RegistroCompraEquipoCelularesByIdResult GetListAllById(string conection, int id)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroCompraEquipoCelularesByIdResult> listado = new List<SAS_RegistroCompraEquipoCelularesByIdResult>();
            SAS_RegistroCompraEquipoCelularesByIdResult item = new SAS_RegistroCompraEquipoCelularesByIdResult();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroCompraEquipoCelularesById(id).ToList();
                if (listado != null)
                {
                    if (listado.ToList().Count > 0)
                    {
                        item = listado.ElementAt(0);
                    }
                }
            }

            return item;
        }


        public List<SAS_RegistroCompraEquipoCelularesDetalleByIdResult> GetListDetailById(string conection, int id)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroCompraEquipoCelularesDetalleByIdResult> listado = new List<SAS_RegistroCompraEquipoCelularesDetalleByIdResult>();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroCompraEquipoCelularesDetalleById(id).ToList();
            }

            return listado;
        }


    }
}
