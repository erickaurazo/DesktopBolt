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
    public class SAS_LineasCelularesCoporativasController
    {

        public List<SAS_LineasCelularesCoporativasListadoAllResult> ListOfCellLines(string conection)
        {
            List<SAS_LineasCelularesCoporativasListadoAllResult> list = new List<SAS_LineasCelularesCoporativasListadoAllResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasListadoAll().ToList();
            }
            return list.OrderBy(x => x.lineaCelular).ToList();
        }


        public List<SAS_LineasCelularesCoporativasListadoAllResult> ListOfCellLines(string conection, string numeroCelular)
        {
            List<SAS_LineasCelularesCoporativasListadoAllResult> list = new List<SAS_LineasCelularesCoporativasListadoAllResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasListadoAll().Where(x => x.lineaCelular == numeroCelular).ToList();
            }
            return list.OrderBy(x => x.lineaCelular).ToList();
        }

        public int ToRegister(string conection, SAS_LineasCelularesCoporativa item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.AREAS.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
                            //oregistro.id = item.id != null ? item.id : 0;
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
                            oregistro.EstadoId = "AC";
                            Modelo.SAS_LineasCelularesCoporativas.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
                            oregistro = resultado.Single();
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 1; // modificar
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;

        }

        public int ToRegister(string conection, SAS_LineasCelularesCoporativa item, List<SAS_LineasCelularesCoporativasPersonalAsignado> DetalleEliminado, List<SAS_LineasCelularesCoporativasPersonalAsignado> ListadoRegistrar)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.AREAS.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
                            //oregistro.id = item.id != null ? item.id : 0;
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
                            oregistro.EstadoId = "AC";
                            Modelo.SAS_LineasCelularesCoporativas.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion

                            if (ListadoRegistrar != null && ListadoRegistrar.ToList().Count > 0)
                            {
                                foreach (var itemDetalleRegistro in ListadoRegistrar)
                                {
                                    SAS_LineasCelularesCoporativasPersonalAsignado detalle = new SAS_LineasCelularesCoporativasPersonalAsignado();
                                    //detalle.LineaCelularPersonalID = itemDetalleRegistro.LineaCelularPersonalID != null ? itemDetalleRegistro.LineaCelularPersonalID : 0;
                                    detalle.LineaCelularID = itemDetalleRegistro.LineaCelularID != null ? itemDetalleRegistro.LineaCelularID : 0;
                                    detalle.PersonalID = itemDetalleRegistro.PersonalID != null ? itemDetalleRegistro.PersonalID : string.Empty;
                                    detalle.Desde = itemDetalleRegistro.Desde != null ? itemDetalleRegistro.Desde : DateTime.Now;
                                    detalle.Hasta = itemDetalleRegistro.Hasta != null ? itemDetalleRegistro.Hasta : DateTime.Now;
                                    detalle.ReferenciaID = itemDetalleRegistro.ReferenciaID != null ? itemDetalleRegistro.ReferenciaID : (int?)null;
                                    detalle.ReferenciaSolicitudID = itemDetalleRegistro.ReferenciaSolicitudID != null ? itemDetalleRegistro.ReferenciaSolicitudID : (int?)null;
                                    detalle.Glosa = itemDetalleRegistro.Glosa != null ? itemDetalleRegistro.Glosa : string.Empty;
                                    detalle.Estado = itemDetalleRegistro.Estado != null ? itemDetalleRegistro.Estado : 1;
                                    Modelo.SAS_LineasCelularesCoporativasPersonalAsignados.InsertOnSubmit(detalle);
                                    Modelo.SubmitChanges();
                                }
                            }

                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
                            oregistro = resultado.Single();
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
                            oregistro.EstadoId = item.EstadoId != null ? item.EstadoId : "AN";
                            Modelo.SubmitChanges();
                            #endregion

                            #region Eliminar Detalles()
                            if (DetalleEliminado != null && DetalleEliminado.ToList().Count > 0)
                            {
                                foreach (var itemDetalleRegistro in DetalleEliminado)
                                {
                                    var result001 = Modelo.SAS_LineasCelularesCoporativasPersonalAsignados.Where(x => x.LineaCelularPersonalID == itemDetalleRegistro.LineaCelularPersonalID).ToList();
                                    if (result001 != null && result001.ToList().Count == 1)
                                    {
                                        SAS_LineasCelularesCoporativasPersonalAsignado detalle = new SAS_LineasCelularesCoporativasPersonalAsignado();
                                        detalle = result001.ElementAt(0);
                                        Modelo.SAS_LineasCelularesCoporativasPersonalAsignados.DeleteOnSubmit(detalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Registrar Y/o Actualizar()
                            if (ListadoRegistrar != null && ListadoRegistrar.ToList().Count > 0)
                            {
                                foreach (var itemDetalleRegistro in ListadoRegistrar)
                                {

                                    var result001 = Modelo.SAS_LineasCelularesCoporativasPersonalAsignados.Where(x => x.LineaCelularPersonalID == itemDetalleRegistro.LineaCelularPersonalID).ToList();
                                    if (result001 != null && result001.ToList().Count == 1)
                                    {
                                        SAS_LineasCelularesCoporativasPersonalAsignado detalle = new SAS_LineasCelularesCoporativasPersonalAsignado();
                                        detalle = result001.ElementAt(0);
                                        //detalle.LineaCelularPersonalID = itemDetalleRegistro.LineaCelularPersonalID != null ? itemDetalleRegistro.LineaCelularPersonalID : 0;
                                        // detalle.LineaCelularID = itemDetalleRegistro.LineaCelularID != null ? itemDetalleRegistro.LineaCelularID : 0;
                                        detalle.PersonalID = itemDetalleRegistro.PersonalID != null ? itemDetalleRegistro.PersonalID : string.Empty;
                                        detalle.Desde = itemDetalleRegistro.Desde != null ? itemDetalleRegistro.Desde : DateTime.Now;
                                        detalle.Hasta = itemDetalleRegistro.Hasta != null ? itemDetalleRegistro.Hasta : DateTime.Now;
                                        detalle.ReferenciaID = itemDetalleRegistro.ReferenciaID != null ? itemDetalleRegistro.ReferenciaID : (int?)null;
                                        detalle.ReferenciaSolicitudID = itemDetalleRegistro.ReferenciaSolicitudID != null ? itemDetalleRegistro.ReferenciaSolicitudID : (int?)null;
                                        detalle.Glosa = itemDetalleRegistro.Glosa != null ? itemDetalleRegistro.Glosa : string.Empty;
                                        detalle.Estado = itemDetalleRegistro.Estado != null ? itemDetalleRegistro.Estado : 1;
                                        Modelo.SubmitChanges();
                                    }
                                    else if (result001 != null && result001.ToList().Count == 0)
                                    {
                                        SAS_LineasCelularesCoporativasPersonalAsignado detalle = new SAS_LineasCelularesCoporativasPersonalAsignado();
                                        //detalle.LineaCelularPersonalID = itemDetalleRegistro.LineaCelularPersonalID != null ? itemDetalleRegistro.LineaCelularPersonalID : 0;
                                        detalle.LineaCelularID = itemDetalleRegistro.LineaCelularID != null ? itemDetalleRegistro.LineaCelularID : 0;
                                        detalle.PersonalID = itemDetalleRegistro.PersonalID != null ? itemDetalleRegistro.PersonalID : string.Empty;
                                        detalle.Desde = itemDetalleRegistro.Desde != null ? itemDetalleRegistro.Desde : DateTime.Now;
                                        detalle.Hasta = itemDetalleRegistro.Hasta != null ? itemDetalleRegistro.Hasta : DateTime.Now;
                                        detalle.ReferenciaID = itemDetalleRegistro.ReferenciaID != null ? itemDetalleRegistro.ReferenciaID : (int?)null;
                                        detalle.ReferenciaSolicitudID = itemDetalleRegistro.ReferenciaSolicitudID != null ? itemDetalleRegistro.ReferenciaSolicitudID : (int?)null;
                                        detalle.Glosa = itemDetalleRegistro.Glosa != null ? itemDetalleRegistro.Glosa : string.Empty;
                                        detalle.Estado = itemDetalleRegistro.Estado != null ? itemDetalleRegistro.Estado : 1;
                                        Modelo.SAS_LineasCelularesCoporativasPersonalAsignados.InsertOnSubmit(detalle);
                                        Modelo.SubmitChanges();
                                    }


                                }
                            }
                            #endregion

                            tipoResultadoOperacion = 1; // modificar

                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;

        }


        public List<SAS_LineasCelularesCoporativasPersonalAsignadoAllResult> ObtejerListadoDeLineasCelularesCoporativasPersonalAsignadoAll(string conection)
        {
            List<SAS_LineasCelularesCoporativasPersonalAsignadoAllResult> list = new List<SAS_LineasCelularesCoporativasPersonalAsignadoAllResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasPersonalAsignadoAll().ToList();
            }
            return list;
        }


        public List<SAS_LineasCelularesCoporativasPersonalByLineaIDResult> ObtejerListadoDeLineasCelularesCoporativasPersonalByLineaID(string conection, int LineaCelularID)
        {
            List<SAS_LineasCelularesCoporativasPersonalByLineaIDResult> list = new List<SAS_LineasCelularesCoporativasPersonalByLineaIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasPersonalByLineaID(LineaCelularID).ToList();
            }
            return list;
        }



        public int ChangeState(string conection, SAS_LineasCelularesCoporativa item)
        {
            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_LineasCelularesCoporativa();
                        oregistro = resultado.Single();

                        if (oregistro.estado == 1)
                        {
                            oregistro.estado = 0;
                            tipoResultadoOperacion = 2; // desactivar
                        }
                        else
                        {
                            oregistro.estado = 1;
                            tipoResultadoOperacion = 3; // Activar
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int ActivarLinea(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "AC";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int SuspenderLinea(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "S0";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int AveriarLinea(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "A0";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int BajarLinea(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "B0";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int EnProcesoDeSuspencion(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "ES";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int EnProcesoDeActivacion(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "H0";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        //
        public int EnProcesoDeCesion(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "E1";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int EnProcesoDeBaja(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "P0";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int CesionDeTitularidad(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "E2";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        //
        public int EnProcesoDeReasignacion(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "PZ";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int EnProcesoDeDevolucionAEmpresa(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "0D";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int PorValidarAsignacion(string conection, int IdLineaCelular)
        {
            SAS_LineasCelularesCoporativa LineaCelular = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 0; // 0 es No se realizo , 1 se actualizo el estado
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == IdLineaCelular).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        LineaCelular = new SAS_LineasCelularesCoporativa();
                        LineaCelular = resultado.Single();

                        if (LineaCelular.estado == 1)
                        {
                            LineaCelular.EstadoId = "PX";
                            tipoResultadoOperacion = 1;
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        //



        public int Remove(string conection, SAS_LineasCelularesCoporativa item)
        {
            SAS_LineasCelularesCoporativa oregistro = new SAS_LineasCelularesCoporativa();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_LineasCelularesCoporativa();
                        oregistro = resultado.Single();
                        Modelo.SAS_LineasCelularesCoporativas.DeleteOnSubmit(oregistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = 4;
                        #endregion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        // para el reporte de listado de líneas
        public List<SAS_ListadoDeLineasTelefonica> ListOfCellLinesforReport(string conection)
        {
            List<SAS_ListadoDeLineasTelefonica> list = new List<SAS_ListadoDeLineasTelefonica>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeLineasTelefonicas.ToList();
            }
            return list.OrderBy(x => x.FechaDeAlta).ToList();
        }

        public List<DFormatoSimple> ObtenerListadoDeColaboradores(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_InfoPersonal> items = new List<SAS_InfoPersonal>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                items = Modelo.SAS_InfoPersonal.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.idcodigogeneral.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.apenom.ToString().Trim()
                           }).ToList();
            }
            return listado;

        }


        public List<SAS_SolicitudesDeRenovacionTelefoniaCelularByCellPhoneNumberResult> ListadoDeSolicitudesPorLineaCelular(string conection, string LineaCelular)
        {
            List<SAS_SolicitudesDeRenovacionTelefoniaCelularByCellPhoneNumberResult> list = new List<SAS_SolicitudesDeRenovacionTelefoniaCelularByCellPhoneNumberResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudesDeRenovacionTelefoniaCelularByCellPhoneNumber(LineaCelular).ToList();
            }
            return list;
        }

    }
}
