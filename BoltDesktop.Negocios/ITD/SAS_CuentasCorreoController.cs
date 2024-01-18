using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_CuentasCorreoController
    {

        // domainAccounts
        public List<SAS_CuentasCorreoListado> GetEmailAccounts(string conection)
        {
            List<SAS_CuentasCorreoListado> listado = new List<SAS_CuentasCorreoListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_CuentasCorreoListado.ToList();
            }
            return listado.OrderBy(x => x.cuenta).ToList();
        }

        public int Register(string conection, SAS_CuentasCorreo item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro.id = item.id;
                            oregistro.cuenta = item.cuenta;
                            oregistro.idcodigoGeneral = item.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oregistro.estado = 1;
                            oregistro.codigoSolicitud = item.codigoSolicitud;
                            oregistro.observaciones = item.observaciones;
                            oregistro.fechaActivacion = item.fechaActivacion;
                            //oregistro.fechaBaja = (DateTime?)null;
                            oregistro.esCorportativo = item.esCorportativo;
                            oregistro.clave = item.clave;
                            oregistro.idLicencia = item.idLicencia;
                            oregistro.nombres = item.nombres;
                            Modelo.SAS_CuentasCorreo.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro = resultado.Single();
                            oregistro.cuenta = item.cuenta;
                            //oregistro.estado = item.estado;
                            oregistro.idcodigoGeneral = item.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oregistro.codigoSolicitud = item.codigoSolicitud;
                            oregistro.observaciones = item.observaciones;
                            oregistro.idLicencia = item.idLicencia;
                            //oregistro.fechaActivacion = item.fechaActivacion;
                            // oregistro.fechaBaja = item.fechaBaja;
                            oregistro.esCorportativo = item.esCorportativo;
                            oregistro.clave = item.clave;
                            oregistro.nombres = item.nombres;
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


        public int Register(string conection, SAS_CuentasCorreo item, List<SAS_CuentasCorreoDetalle> detalleEliminados, List<SAS_CuentasCorreoDetalle> detalles)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro.id = item.id;
                            oregistro.cuenta = item.cuenta;
                            oregistro.idcodigoGeneral = item.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oregistro.estado = 1;
                            oregistro.codigoSolicitud = item.codigoSolicitud;
                            oregistro.observaciones = item.observaciones;
                            oregistro.fechaActivacion = item.fechaActivacion;
                            //oregistro.fechaBaja = (DateTime?)null;
                            oregistro.esCorportativo = item.esCorportativo;
                            oregistro.clave = item.clave;
                            oregistro.idLicencia = item.idLicencia;
                            oregistro.nombres = item.nombres;
                            Modelo.SAS_CuentasCorreo.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 0; // registrar

                            if (detalles != null)
                            {
                                #region Registrar detalle() 
                                if (detalles.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in detalles)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalle.Where(x => x.id == oregistro.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 0 )
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle.id = oregistro.id;
                                            oDetalle.item = itemDetalle.item;
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = Environment.UserName;                                            
                                            Modelo.SAS_CuentasCorreoDetalle.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle = resultadoDetalle.Single();
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = Environment.UserName;                                            
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }

                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar cabecera() 
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro = resultado.Single();
                            oregistro.cuenta = item.cuenta;
                            //oregistro.estado = item.estado;
                            oregistro.idcodigoGeneral = item.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oregistro.codigoSolicitud = item.codigoSolicitud;
                            oregistro.observaciones = item.observaciones;
                            oregistro.idLicencia = item.idLicencia;
                            //oregistro.fechaActivacion = item.fechaActivacion;
                            // oregistro.fechaBaja = item.fechaBaja;
                            oregistro.esCorportativo = item.esCorportativo;
                            oregistro.clave = item.clave;
                            oregistro.nombres = item.nombres;
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 1; // modificar


                            #region eliminar listado detalle() 
                            if (detalleEliminados != null)
                            {
                                #region Eliminar detalle() 
                                if (detalleEliminados.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in detalleEliminados)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalle.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle = resultadoDetalle.Single();
                                            Modelo.SAS_CuentasCorreoDetalle.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Registrar detalle() 
                            if (detalles != null)
                            {
                                #region Registrar detalle() 
                                if (detalles.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in detalles)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalle.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle.id = item.id;
                                            oDetalle.item = itemDetalle.item;
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = Environment.UserName;
                                            Modelo.SAS_CuentasCorreoDetalle.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle = resultadoDetalle.Single();
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = Environment.UserName;
                                            //Modelo.SAS_CuentasCorreoDetalle.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;

        }

        public int Eliminar(string conection, int codigoRegistro)
        {
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == codigoRegistro).ToList();
                    if (resultado != null)
                    {
                        #region REGISTRAR BAJA  
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro = resultado.Single();

                            if (oregistro.estado == 1)
                            {
                                List<SAS_CuentasCorreoDetalle> listaAEliminar = new List<SAS_CuentasCorreoDetalle>();
                                var resultadoDetail = Modelo.SAS_CuentasCorreoDetalle.Where(x => x.id == codigoRegistro).ToList();
                                if (resultadoDetail != null && resultadoDetail.ToList().Count > 0)
                                {
                                    Modelo.SAS_CuentasCorreoDetalle.DeleteAllOnSubmit(resultadoDetail);
                                }

                                Modelo.SAS_CuentasCorreo.DeleteOnSubmit(oregistro);
                                tipoResultadoOperacion = 5;
                            }

                            Modelo.SubmitChanges();
                            #endregion                            
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;
        }

        public int registerUnsubscribe(string conection, SAS_CuentasCorreo item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region REGISTRAR BAJA  
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro = resultado.Single();
                            oregistro.fechaBaja = item.fechaBaja;
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 5; // baja
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;

        }


        public int ChangeState(string conection, SAS_CuentasCorreo item)
        {

            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()
                        SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
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


        public List<SAS_CuentasCorreoDetalleByIdResult> GetEmailAccountsDetailById(string conection, SAS_CuentasCorreo item)
        {

            List<SAS_CuentasCorreoDetalleByIdResult> listado = new List<SAS_CuentasCorreoDetalleByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_CuentasCorreoDetalleById(item.id).ToList();
            }
            return listado.OrderBy(x => x.item).ToList();
        }


        public List<DFormatoSimple> GetType(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();            
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Generación de BackUp" });
                listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Imagen" });
                listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Cambio de contraseña" });                
                listado.Add(new DFormatoSimple { Codigo = "4", Descripcion = "Descarga y validación del Backup" });
                listado.Add(new DFormatoSimple { Codigo = "5", Descripcion = "Configuración de backup" });
                listado.Add(new DFormatoSimple { Codigo = "6", Descripcion = "Liberación de licencia" });
                listado.Add(new DFormatoSimple { Codigo = "7", Descripcion = "Asignación o reasignación de licencia" });
                listado.Add(new DFormatoSimple { Codigo = "8", Descripcion = "Upgrade licencia" });
                listado.Add(new DFormatoSimple { Codigo = "9", Descripcion = "Renombrar nombre de cuenta" });
                listado.Add(new DFormatoSimple { Codigo = "10", Descripcion = "Agregar Alias" });
                listado.Add(new DFormatoSimple { Codigo = "11", Descripcion = "Agregar a grupo" });
                listado.Add(new DFormatoSimple { Codigo = "12", Descripcion = "Baja de grupo" });
                listado.Add(new DFormatoSimple { Codigo = "13", Descripcion = "Downgrade licencia" }); 
                listado.Add(new DFormatoSimple { Codigo = "14", Descripcion = "Baja de cuenta" });
                listado.Add(new DFormatoSimple { Codigo = "15", Descripcion = "Suspención de cuenta" });
                listado.Add(new DFormatoSimple { Codigo = "16", Descripcion = "Activación y/o Reactivación de cuenta" });

            }
            return listado;
        }


    }
}

