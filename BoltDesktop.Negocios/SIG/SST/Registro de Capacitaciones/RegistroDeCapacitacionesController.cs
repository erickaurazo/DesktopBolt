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

        public List<ListadoRegistroCapacitacionesPorPeriodoResult> ObtenerListaDeCapacitacionesDesdePeriodos(string conection, string desde, string hasta)
        {
            List<ListadoRegistroCapacitacionesPorPeriodoResult> ListAll = new List<ListadoRegistroCapacitacionesPorPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltSSTDataContext Modelo = new BoltSSTDataContext(cnx))
            {
                ListAll = Modelo.ListadoRegistroCapacitacionesPorPeriodo(desde, hasta).ToList();
            }

            return ListAll;
        }


        public ListadoRegistroCapacitacionesPorIDResult ObtenerRegistroDeCapacitacionesDesdeID(string conection, string ID)
        {
            DateTime fechaActual = DateTime.Now;
            List<ListadoRegistroCapacitacionesPorIDResult> Listado = new List<ListadoRegistroCapacitacionesPorIDResult>();
            ListadoRegistroCapacitacionesPorIDResult RegistroCapacitacionDesdeID = new ListadoRegistroCapacitacionesPorIDResult();
            RegistroCapacitacionDesdeID.Capacitacion = string.Empty;
            RegistroCapacitacionDesdeID.CapacitacionID = string.Empty;
            RegistroCapacitacionDesdeID.CapacitacionTipoID = string.Empty;
            RegistroCapacitacionDesdeID.Duracion = 0;
            RegistroCapacitacionDesdeID.Estado = "PENDIENTE";
            RegistroCapacitacionDesdeID.EstadoID = "PE";
            RegistroCapacitacionDesdeID.FechaCapacitacion = fechaActual;
            RegistroCapacitacionDesdeID.FechaRegistro = fechaActual;
            RegistroCapacitacionDesdeID.Folio = "0".PadLeft(7, '0');
            RegistroCapacitacionDesdeID.HoraFin = fechaActual;
            RegistroCapacitacionDesdeID.HoraInicio = fechaActual;
            RegistroCapacitacionDesdeID.LatLong = string.Empty;
            RegistroCapacitacionDesdeID.Observacion = string.Empty;
            RegistroCapacitacionDesdeID.PDFPrint = 0;
            RegistroCapacitacionDesdeID.PDFRuta = string.Empty;
            RegistroCapacitacionDesdeID.Ubicación = string.Empty;
           



            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltSSTDataContext Modelo = new BoltSSTDataContext(cnx))
            {
                Listado = Modelo.ListadoRegistroCapacitacionesPorID(ID).ToList();

                if (Listado != null  )
                {
                    if (Listado.Count() > 0)
                    {
                        RegistroCapacitacionDesdeID = Listado.ElementAt(0);
                    }
                }

            }

            return RegistroCapacitacionDesdeID;
        }


    }
}
