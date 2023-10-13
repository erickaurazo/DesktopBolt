using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class SJ_RHRutaNegocios
    {
        public List<SJ_RHListaRutasResult> ListarRutasDeRecorridos(string conection)
        {
            List<SJ_RHListaRutasResult> ListadoRutas = new List<SJ_RHListaRutasResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                ListadoRutas = Modelo.SJ_RHListaRutas().ToList();
                Modelo.Connection.Close();
            }
            return ListadoRutas.OrderByDescending(x => x.Id).ToList();
        }

        public bool AddRoute(SJ_RHRuta route, string conection)
        {
            bool estate = false;
            using (TransactionScope Scope = new TransactionScope())
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    #region Transaccion()

                    if (route.Id == 0)
                    {
                        #region Nuevo()
                        try
                        {
                            SJ_RHRuta objeto = new SJ_RHRuta();
                            objeto.Id = 0;
                            objeto.RutaOrigen = route.RutaOrigen;
                            objeto.RutaDestino = route.RutaDestino;
                            objeto.Distancia = route.Distancia;
                            objeto.TiempoViaje = route.TiempoViaje;
                            objeto.Observacion = route.Observacion;
                            objeto.IdEstado = route.IdEstado;
                            objeto.FechaRegistro = DateTime.Now;
                            objeto.abreviaturaRutaOrigen = route.abreviaturaRutaOrigen.ToString().Trim();
                            objeto.abreviaturaRuraDestino = route.abreviaturaRuraDestino.ToString().Trim();
                            objeto.descripcionCortaDestino = route.descripcionCortaDestino.ToString().Trim();
                            objeto.descripcionCortaOrigen = route.descripcionCortaOrigen.ToString().Trim();
                            objeto.esIngreso = route.esIngreso.ToString().Trim();
                            Modelo.SJ_RHRuta.InsertOnSubmit(objeto);
                            Modelo.SubmitChanges();

                            estate = true;

                        }
                        catch (Exception Ex)
                        {
                            throw Ex;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Modificar()
                        if (Modelo.SJ_RHRuta.Where(x => x.Id == route.Id).ToList().Count == 1)
                        {
                            SJ_RHRuta objeto = new SJ_RHRuta();
                            objeto = Modelo.SJ_RHRuta.Where(x => x.Id == route.Id).Single();
                            objeto.RutaOrigen = route.RutaOrigen;
                            objeto.RutaDestino = route.RutaDestino;
                            objeto.Distancia = route.Distancia;
                            objeto.TiempoViaje = route.TiempoViaje;
                            objeto.Observacion = route.Observacion;
                            objeto.abreviaturaRutaOrigen = route.abreviaturaRutaOrigen.ToString().Trim();
                            objeto.abreviaturaRuraDestino = route.abreviaturaRuraDestino.ToString().Trim();
                            objeto.descripcionCortaDestino = route.descripcionCortaDestino.ToString().Trim();
                            objeto.descripcionCortaOrigen = route.descripcionCortaOrigen.ToString().Trim();
                            objeto.esIngreso = route.esIngreso.ToString().Trim();
                            Modelo.SubmitChanges();
                            estate = true;
                        }
                        #endregion
                    }
                    #endregion
                    Modelo.Connection.Close();
                }
                Scope.Complete();
            }

            return estate;
        }

        // agregar ruta de ida y vuelta
        public bool AddRoundTripRoute(SJ_RHRuta route, string conection)
        {
            bool estate = false;
            using (TransactionScope Scope = new TransactionScope())
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
                {
                    #region Transaccion()

                    var resultIda = context.SJ_RHRuta.Where(x =>
                 x.RutaOrigen == route.RutaOrigen &&
                 x.RutaDestino == route.RutaDestino &&
                 x.abreviaturaRutaOrigen == route.abreviaturaRutaOrigen &&
                 x.abreviaturaRuraDestino == route.abreviaturaRuraDestino
                    ).ToList();

                    var resultVuelta = context.SJ_RHRuta.Where(x =>
                    x.RutaOrigen == route.RutaDestino &&
                    x.RutaDestino == route.RutaOrigen &&
                    x.abreviaturaRutaOrigen == route.abreviaturaRuraDestino &&
                    x.abreviaturaRuraDestino == route.abreviaturaRutaOrigen).ToList();

                    #region Ida()
                    if (resultIda != null && resultIda.ToList().Count == 0)
                    {
                        #region Agregar ida
                        SJ_RHRuta objeto = new SJ_RHRuta();
                        //objeto.Id = 0;
                        objeto.RutaOrigen = route.RutaOrigen;
                        objeto.RutaDestino = route.RutaDestino;
                        objeto.Distancia = route.Distancia;
                        objeto.TiempoViaje = route.TiempoViaje;
                        objeto.Observacion = route.Observacion;
                        objeto.IdEstado = route.IdEstado;
                        objeto.FechaRegistro = DateTime.Now;
                        objeto.abreviaturaRutaOrigen = route.abreviaturaRutaOrigen.ToString().Trim();
                        objeto.abreviaturaRuraDestino = route.abreviaturaRuraDestino.ToString().Trim();
                        objeto.descripcionCortaDestino = route.descripcionCortaDestino.ToString().Trim();
                        objeto.descripcionCortaOrigen = route.descripcionCortaOrigen.ToString().Trim();
                        objeto.esIngreso = "1";
                        context.SJ_RHRuta.InsertOnSubmit(objeto);
                        context.SubmitChanges();
                        estate = true;
                        #endregion
                    }
                    //else if (resultIda != null && resultIda.ToList().Count == 1)
                    //{
                    //    #region Editar ida();
                    //    SJ_RHRuta objeto = new SJ_RHRuta();
                    //    objeto = resultIda.Single();
                    //    objeto.RutaOrigen = oRuta.RutaOrigen;
                    //    objeto.RutaDestino = oRuta.RutaDestino;
                    //    objeto.Distancia = oRuta.Distancia;
                    //    objeto.TiempoViaje = oRuta.TiempoViaje;
                    //    objeto.Observacion = oRuta.Observacion;
                    //    objeto.abreviaturaRutaOrigen = oRuta.abreviaturaRutaOrigen.ToString().Trim();
                    //    objeto.abreviaturaRuraDestino = oRuta.abreviaturaRuraDestino.ToString().Trim();
                    //    objeto.descripcionCortaDestino = oRuta.descripcionCortaDestino.ToString().Trim();
                    //    objeto.descripcionCortaOrigen = oRuta.descripcionCortaOrigen.ToString().Trim();
                    //    objeto.esIngreso = "1";
                    //    context.SubmitChanges();
                    //    estate = true;
                    //    #endregion
                    //}
                    #endregion


                    #region Vuelta()
                    if (resultVuelta != null && resultVuelta.ToList().Count == 0)
                    {
                        #region Agregar Vuelta
                        SJ_RHRuta objeto = new SJ_RHRuta();
                        //objeto.Id = 0;
                        objeto.RutaOrigen = route.RutaDestino;
                        objeto.RutaDestino = route.RutaOrigen;
                        objeto.Distancia = route.Distancia;
                        objeto.TiempoViaje = route.TiempoViaje;
                        objeto.Observacion = route.Observacion;
                        objeto.IdEstado = route.IdEstado;
                        objeto.FechaRegistro = DateTime.Now;
                        objeto.abreviaturaRutaOrigen = route.abreviaturaRuraDestino.ToString().Trim();
                        objeto.abreviaturaRuraDestino = route.abreviaturaRutaOrigen.ToString().Trim();
                        objeto.descripcionCortaDestino = route.descripcionCortaOrigen.ToString().Trim();
                        objeto.descripcionCortaOrigen = route.descripcionCortaDestino.ToString().Trim();
                        objeto.esIngreso = "0";
                        context.SJ_RHRuta.InsertOnSubmit(objeto);
                        context.SubmitChanges();
                        estate = true;
                        #endregion
                    }
                    //else if (resultVuelta != null && resultVuelta.ToList().Count == 1)
                    //{
                    //    #region Editar Vuelta();
                    //    SJ_RHRuta objeto = new SJ_RHRuta();
                    //    objeto = resultVuelta.Single();
                    //    objeto.RutaOrigen = oRuta.RutaDestino;
                    //    objeto.RutaDestino = oRuta.RutaOrigen;
                    //    objeto.Distancia = oRuta.Distancia;
                    //    objeto.TiempoViaje = oRuta.TiempoViaje;
                    //    objeto.Observacion = oRuta.Observacion;
                    //    objeto.abreviaturaRutaOrigen = oRuta.abreviaturaRuraDestino.ToString().Trim();
                    //    objeto.abreviaturaRuraDestino = oRuta.abreviaturaRutaOrigen.ToString().Trim();
                    //    objeto.descripcionCortaDestino = oRuta.descripcionCortaOrigen.ToString().Trim();
                    //    objeto.descripcionCortaOrigen = oRuta.descripcionCortaDestino.ToString().Trim();
                    //    objeto.esIngreso = "0";
                    //    context.SubmitChanges();
                    //    estate = true;
                    //    #endregion
                    //}
                    #endregion                 
                    #endregion
                    context.Connection.Close();
                }
                Scope.Complete();
            }

            return estate;
        }


        public void Anular(int routerId, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings[conection].ToString();

                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    if (Modelo.SJ_RHRuta.Where(x => x.Id == routerId).ToList().Count == 1)
                    {
                        //Anular
                        try
                        {
                            SJ_RHRuta oRuta = new SJ_RHRuta();
                            oRuta = Modelo.SJ_RHRuta.Where(x => x.Id == routerId).Single();
                            oRuta.IdEstado = "AN";
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }

        public void Delete(int idRoute, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    if (Modelo.SJ_RHRuta.Where(x => x.Id == idRoute).ToList().Count == 1)
                    {
                        //Anular
                        try
                        {
                            SJ_RHRuta oRuta = new SJ_RHRuta();
                            oRuta = Modelo.SJ_RHRuta.Where(x => x.Id == idRoute).Single();
                            Modelo.SJ_RHRuta.DeleteOnSubmit(oRuta);
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                            return;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }

        public string ValirdarIntegridadReferencialRoute(int routerId, string conection)
        {
            string obs = string.Empty;
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    var result = Modelo.ASJ_RegistroTransferenciaTransportes.Where(x => x.idRutaOrigen == routerId).ToList();
                    if (result.Count > 0)
                    {
                        // relacion con registro de asistencia
                        obs += " Transferencia de asistencia por tablets";
                    }


                }
                #endregion

                Scope.Complete();
            }

            return obs;
        }

        public string GetSourceAbbreviatedPathName(string conection, string abbreviateCode, string routeSource)
        {
            string cnx, abbreviatePathName = string.Empty;

            cnx = ConfigurationManager.AppSettings["bd" + conection].ToString();
            using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
            {
                var resulta = context.SJ_RHRuta.Where(x => x.RutaOrigen == routeSource && x.abreviaturaRutaOrigen.Trim() == abbreviateCode);
                if (resulta != null && resulta.ToList().Count > 0)
                {
                    abbreviatePathName = resulta.FirstOrDefault().descripcionCortaOrigen.Trim();
                }
            }
            return abbreviatePathName;
        }

        public string GetDestinationAbbreviatedPathName(string conection, string abbreviateCode, string rutaDestino)
        {
            string cnx, abbreviatePathName = string.Empty;

            cnx = ConfigurationManager.AppSettings["bd" + conection].ToString();
            using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
            {
                var resulta = context.SJ_RHRuta.Where(x => x.RutaDestino.Trim() == rutaDestino.Trim() && x.abreviaturaRuraDestino.Trim() == abbreviateCode.Trim());
                if (resulta != null && resulta.ToList().Count > 0)
                {
                    abbreviatePathName = resulta.FirstOrDefault().descripcionCortaDestino.Trim();
                }
            }
            return abbreviatePathName;
        }

    }
}
