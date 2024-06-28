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
        private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
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

                if (Listado != null)
                {
                    if (Listado.Count() > 0)
                    {
                        RegistroCapacitacionDesdeID = Listado.ElementAt(0);
                    }
                }

            }

            return RegistroCapacitacionDesdeID;
        }

        public string Registrar(string conexionABaseDeDatos, CapacitacionCabecera capacitacionARegistrar)
        {
            string ID = GenerateUniqueId(17);
            CapacitacionCabecera Capacitacion = new CapacitacionCabecera();
            string cnx = ConfigurationManager.AppSettings[conexionABaseDeDatos].ToString();
            using (BoltSSTDataContext Modelo = new BoltSSTDataContext(cnx))
            {
                var Listado = Modelo.CapacitacionCabeceras.Where(x => x.CapacitacionID.Trim() == capacitacionARegistrar.CapacitacionID.Trim()).ToList();

                if (Listado != null)
                {
                    if (Listado.Count() == 0)
                    {
                        Capacitacion = new CapacitacionCabecera();
                        Capacitacion.CapacitacionID = ID;
                        Capacitacion.CapacitacionTipoID = capacitacionARegistrar.CapacitacionTipoID != null ? capacitacionARegistrar.CapacitacionTipoID.Trim() : string.Empty ;
                        Capacitacion.FechaCapacitacion = capacitacionARegistrar.FechaCapacitacion != null ? capacitacionARegistrar.FechaCapacitacion : DateTime.Now;
                        Capacitacion.Ubicación = capacitacionARegistrar.Ubicación != null ? capacitacionARegistrar.Ubicación.Trim() : string.Empty;
                        Capacitacion.LatLong = capacitacionARegistrar.LatLong != null ? capacitacionARegistrar.LatLong.Trim() : string.Empty;
                        Capacitacion.HoraInicio = capacitacionARegistrar.HoraInicio != null ? capacitacionARegistrar.HoraInicio : DateTime.Now;
                        Capacitacion.HoraFin = capacitacionARegistrar.HoraFin != null ? capacitacionARegistrar.HoraFin : DateTime.Now;
                        Capacitacion.Observacion = capacitacionARegistrar.Observacion != null ? capacitacionARegistrar.Observacion.Trim() : string.Empty;
                        Capacitacion.FechaRegistro = capacitacionARegistrar.FechaRegistro != null ? capacitacionARegistrar.FechaRegistro : DateTime.Now;
                        Capacitacion.PDFRuta = capacitacionARegistrar.PDFRuta != null ? capacitacionARegistrar.PDFRuta.Trim() : string.Empty;
                        Capacitacion.PDFPrint = capacitacionARegistrar.PDFPrint != null ? capacitacionARegistrar.PDFPrint.Value : 0;
                        Capacitacion.EstadoID = capacitacionARegistrar.EstadoID != null ? capacitacionARegistrar.EstadoID.Trim() : string.Empty;
                        //Capacitacion.Correlativo = capacitacionARegistrar.Correlativo != null ? capacitacionARegistrar.Correlativo : 0;
                        Capacitacion.IdReferencia = capacitacionARegistrar.IdReferencia != null ? capacitacionARegistrar.IdReferencia.Trim() : string.Empty;
                        Modelo.CapacitacionCabeceras.InsertOnSubmit(Capacitacion);
                        Modelo.SubmitChanges();

                    }
                    else
                    {
                        Capacitacion = Listado.ElementAt(0);
                        ID = Capacitacion.CapacitacionID.Trim();
                        Capacitacion.CapacitacionTipoID = capacitacionARegistrar.CapacitacionTipoID != null ? capacitacionARegistrar.CapacitacionTipoID.Trim() : string.Empty;
                        Capacitacion.FechaCapacitacion = capacitacionARegistrar.FechaCapacitacion != null ? capacitacionARegistrar.FechaCapacitacion : DateTime.Now;
                        Capacitacion.Ubicación = capacitacionARegistrar.Ubicación != null ? capacitacionARegistrar.Ubicación.Trim() : string.Empty;
                        Capacitacion.LatLong = capacitacionARegistrar.LatLong != null ? capacitacionARegistrar.LatLong.Trim() : string.Empty;
                        Capacitacion.HoraInicio = capacitacionARegistrar.HoraInicio != null ? capacitacionARegistrar.HoraInicio : DateTime.Now;
                        Capacitacion.HoraFin = capacitacionARegistrar.HoraFin != null ? capacitacionARegistrar.HoraFin : DateTime.Now;
                        Capacitacion.Observacion = capacitacionARegistrar.Observacion != null ? capacitacionARegistrar.Observacion.Trim() : string.Empty;
                        //Capacitacion.FechaRegistro = capacitacionARegistrar.FechaRegistro != null ? capacitacionARegistrar.FechaRegistro : DateTime.Now;
                        Capacitacion.PDFRuta = capacitacionARegistrar.PDFRuta != null ? capacitacionARegistrar.PDFRuta.Trim() : string.Empty;
                        Capacitacion.PDFPrint = capacitacionARegistrar.PDFPrint != null ? capacitacionARegistrar.PDFPrint.Value : 0;
                        //Capacitacion.EstadoID = capacitacionARegistrar.EstadoID != null ? capacitacionARegistrar.EstadoID.Trim() : string.Empty;
                        //Capacitacion.Correlativo = capacitacionARegistrar.Correlativo != null ? capacitacionARegistrar.Correlativo : 0;
                        Capacitacion.IdReferencia = capacitacionARegistrar.IdReferencia != null ? capacitacionARegistrar.IdReferencia.Trim() : string.Empty;                        
                        Modelo.SubmitChanges();


                    }
                }

            }
            return ID;
        }



        public static string GenerateUniqueId(int length = 17)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be a positive integer", nameof(length));

            byte[] buffer = new byte[length * 4];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }

            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                var rnd = BitConverter.ToUInt32(buffer, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}
