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




        public int Register(string conection, SAS_CuentasCorreo iCuentaCorreo, List<SAS_CuentasCorreoDetalle> ListadoDetalleLogEliminar, List<SAS_CuentasCorreoDetalle> ListadoDetalleLogRegistrar)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreo.Where(x => x.id == iCuentaCorreo.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();
                            SAS_CuentasCorreo oregistro = new SAS_CuentasCorreo();
                            oregistro.id = iCuentaCorreo.id;
                            oregistro.cuenta = iCuentaCorreo.cuenta;
                            oregistro.idcodigoGeneral = iCuentaCorreo.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = iCuentaCorreo.vienesDesdeSolicitud;
                            oregistro.estado = 1;
                            oregistro.codigoSolicitud = iCuentaCorreo.codigoSolicitud;
                            oregistro.observaciones = iCuentaCorreo.observaciones;
                            oregistro.fechaActivacion = iCuentaCorreo.fechaActivacion;
                            //oregistro.fechaBaja = (DateTime?)null;
                            oregistro.esCorportativo = iCuentaCorreo.esCorportativo;
                            oregistro.clave = iCuentaCorreo.clave;
                            oregistro.idLicencia = iCuentaCorreo.idLicencia;
                            oregistro.nombres = iCuentaCorreo.nombres;
                            Modelo.SAS_CuentasCorreo.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 0; // registrar

                            if (ListadoDetalleLogRegistrar != null)
                            {
                                #region Registrar detalle() 
                                if (ListadoDetalleLogRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == oregistro.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle.id = oregistro.id;
                                            oDetalle.item = itemDetalle.item;
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = itemDetalle.creadoPor;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.Hostname = itemDetalle.Hostname;

                                            Modelo.SAS_CuentasCorreoDetalles.InsertOnSubmit(oDetalle);
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
                            oregistro.cuenta = iCuentaCorreo.cuenta;
                            //oregistro.estado = item.estado;
                            oregistro.idcodigoGeneral = iCuentaCorreo.idcodigoGeneral;
                            oregistro.vienesDesdeSolicitud = iCuentaCorreo.vienesDesdeSolicitud;
                            oregistro.codigoSolicitud = iCuentaCorreo.codigoSolicitud;
                            oregistro.observaciones = iCuentaCorreo.observaciones;
                            oregistro.idLicencia = iCuentaCorreo.idLicencia;
                            //oregistro.fechaActivacion = item.fechaActivacion;
                            // oregistro.fechaBaja = item.fechaBaja;
                            oregistro.esCorportativo = iCuentaCorreo.esCorportativo;
                            oregistro.clave = iCuentaCorreo.clave;
                            oregistro.nombres = iCuentaCorreo.nombres;
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 1; // modificar


                            #region eliminar listado detalle() 
                            if (ListadoDetalleLogEliminar != null)
                            {
                                #region Eliminar detalle() 
                                if (ListadoDetalleLogEliminar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogEliminar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle = resultadoDetalle.Single();
                                            Modelo.SAS_CuentasCorreoDetalles.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Registrar detalle() 
                            if (ListadoDetalleLogRegistrar != null)
                            {
                                #region Registrar detalle() 
                                if (ListadoDetalleLogRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle.id = iCuentaCorreo.id;
                                            oDetalle.item = itemDetalle.item;
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = itemDetalle.creadoPor;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.Hostname = itemDetalle.Hostname;

                                            Modelo.SAS_CuentasCorreoDetalles.InsertOnSubmit(oDetalle);
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

        public int Register(string conection, SAS_CuentasCorreo item,
            List<SAS_CuentasCorreoDetalle> ListadoDetalleLogEliminar,
            List<SAS_CuentasCorreoDetalle> ListadoDetalleLogRegistrar,
            List<SAS_CuentasCorreosHistoricoPlan> ListadoHistoricoPlanEliminar,
            List<SAS_CuentasCorreosHistoricoPlan> ListadoHistoricoPlanRegistrar,
            List<SAS_CuentasCorreoAsignacionPersonal> ListadoAsignacionCuentaEliminar,
            List<SAS_CuentasCorreoAsignacionPersonal> ListadoAsignacionCuentaRegistrar
            )

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
                            SAS_CuentasCorreo oCuentaDeCorreoCorporativo = new SAS_CuentasCorreo();
                            oCuentaDeCorreoCorporativo.id = item.id;
                            oCuentaDeCorreoCorporativo.cuenta = item.cuenta;
                            oCuentaDeCorreoCorporativo.idcodigoGeneral = item.idcodigoGeneral;
                            oCuentaDeCorreoCorporativo.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oCuentaDeCorreoCorporativo.estado = 1;
                            oCuentaDeCorreoCorporativo.codigoSolicitud = item.codigoSolicitud;
                            oCuentaDeCorreoCorporativo.observaciones = item.observaciones;
                            oCuentaDeCorreoCorporativo.fechaActivacion = item.fechaActivacion;
                            //oregistro.fechaBaja = (DateTime?)null;
                            oCuentaDeCorreoCorporativo.esCorportativo = item.esCorportativo;
                            oCuentaDeCorreoCorporativo.clave = item.clave;
                            oCuentaDeCorreoCorporativo.idLicencia = item.idLicencia;
                            oCuentaDeCorreoCorporativo.nombres = item.nombres;
                            Modelo.SAS_CuentasCorreo.InsertOnSubmit(oCuentaDeCorreoCorporativo);
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 0; // registrar

                            if (ListadoDetalleLogRegistrar != null)
                            {
                                #region Registrar detalle Log()
                                if (ListadoDetalleLogRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == oCuentaDeCorreoCorporativo.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle.id = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.item = itemDetalle.item;
                                            oDetalle.idTipo = itemDetalle.idTipo;
                                            oDetalle.link = itemDetalle.link;
                                            oDetalle.descripcion = itemDetalle.descripcion;
                                            oDetalle.estado = itemDetalle.estado;
                                            oDetalle.creadoPor = Environment.UserName;
                                            Modelo.SAS_CuentasCorreoDetalles.InsertOnSubmit(oDetalle);
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

                            if (ListadoHistoricoPlanRegistrar != null)
                            {
                                #region Registrar detalle Plan Historico() 
                                if (ListadoHistoricoPlanRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoHistoricoPlanRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreosHistoricoPlans.Where(x => x.CuentaCorreoTipoLicenciaId == itemDetalle.CuentaCorreoTipoLicenciaId).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreosHistoricoPlan oDetalle = new SAS_CuentasCorreosHistoricoPlan();
                                           // oDetalle.CuentaCorreoTipoLicenciaId = itemDetalle.CuentaCorreoTipoLicenciaId;
                                            oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.LicenciaTipoId = itemDetalle.LicenciaTipoId;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            oDetalle.Estado = itemDetalle.Estado;
                                            oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.HostName = itemDetalle.HostName;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;

                                            Modelo.SAS_CuentasCorreosHistoricoPlans.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                       
                                    }
                                }
                                #endregion
                            }

                            if (ListadoAsignacionCuentaRegistrar != null)
                            {
                                #region Registrar detalle Personal A Asignar() 
                                if (ListadoAsignacionCuentaRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoAsignacionCuentaRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoAsignacionPersonals.Where(x => x.CuentaCorreoAsignacionId == itemDetalle.CuentaCorreoAsignacionId).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoAsignacionPersonal oDetalle = new SAS_CuentasCorreoAsignacionPersonal();
                                           // oDetalle.CuentaCorreoAsignacionId = itemDetalle.CuentaCorreoAsignacionId;
                                            oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.PersonalID = itemDetalle.PersonalID;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            oDetalle.Estado = itemDetalle.Estado;
                                            oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.HostName = itemDetalle.HostName;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            Modelo.SAS_CuentasCorreoAsignacionPersonals.InsertOnSubmit(oDetalle);
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
                            SAS_CuentasCorreo oCuentaDeCorreoCorporativo = new SAS_CuentasCorreo();
                            oCuentaDeCorreoCorporativo = resultado.Single();
                            oCuentaDeCorreoCorporativo.cuenta = item.cuenta;
                            //oregistro.estado = item.estado;
                            oCuentaDeCorreoCorporativo.idcodigoGeneral = item.idcodigoGeneral;
                            oCuentaDeCorreoCorporativo.vienesDesdeSolicitud = item.vienesDesdeSolicitud;
                            oCuentaDeCorreoCorporativo.codigoSolicitud = item.codigoSolicitud;
                            oCuentaDeCorreoCorporativo.observaciones = item.observaciones;
                            oCuentaDeCorreoCorporativo.idLicencia = item.idLicencia;
                            //oregistro.fechaActivacion = item.fechaActivacion;
                            // oregistro.fechaBaja = item.fechaBaja;
                            oCuentaDeCorreoCorporativo.esCorportativo = item.esCorportativo;
                            oCuentaDeCorreoCorporativo.clave = item.clave;
                            oCuentaDeCorreoCorporativo.nombres = item.nombres;
                            Modelo.SubmitChanges();
                            #endregion
                            tipoResultadoOperacion = 1; // modificar


                            #region Eliminar listado detalle Log() 
                            if (ListadoDetalleLogEliminar != null)
                            {
                                #region Eliminar detalle() 
                                if (ListadoDetalleLogEliminar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogEliminar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
                                        if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoDetalle oDetalle = new SAS_CuentasCorreoDetalle();
                                            oDetalle = resultadoDetalle.Single();
                                            Modelo.SAS_CuentasCorreoDetalles.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Eliminar listado detalle Plan Historico() 
                            if (ListadoHistoricoPlanEliminar != null)
                            {
                                #region Eliminar detalle() 
                                if (ListadoHistoricoPlanEliminar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoHistoricoPlanEliminar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreosHistoricoPlans.Where(x => x.CuentaCorreoTipoLicenciaId == itemDetalle.CuentaCorreoTipoLicenciaId).ToList();
                                        if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreosHistoricoPlan oDetalle = new SAS_CuentasCorreosHistoricoPlan();
                                            oDetalle = resultadoDetalle.Single();
                                            Modelo.SAS_CuentasCorreosHistoricoPlans.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Eliminar listado detalle Asignacion a Colaborador() 
                            if (ListadoAsignacionCuentaEliminar != null)
                            {
                                #region Eliminar detalle() 
                                if (ListadoAsignacionCuentaEliminar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoAsignacionCuentaEliminar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoAsignacionPersonals.Where(x => x.CuentaCorreoAsignacionId == itemDetalle.CuentaCorreoAsignacionId).ToList();
                                        if (resultadoDetalle.Count == 1)
                                        {
                                            #region Modificar() 
                                            SAS_CuentasCorreoAsignacionPersonal oDetalle = new SAS_CuentasCorreoAsignacionPersonal();
                                            oDetalle = resultadoDetalle.Single();
                                            Modelo.SAS_CuentasCorreoAsignacionPersonals.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion  
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion


                            #region Registrar detalle Log() 
                            if (ListadoDetalleLogRegistrar != null)
                            {
                                #region Registrar detalle() 
                                if (ListadoDetalleLogRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoDetalleLogRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == itemDetalle.id && x.item == itemDetalle.item).ToList();
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
                                            Modelo.SAS_CuentasCorreoDetalles.InsertOnSubmit(oDetalle);
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

                            if (ListadoHistoricoPlanRegistrar != null)
                            {
                                #region Registrar detalle Plan Historico() 
                                if (ListadoHistoricoPlanRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoHistoricoPlanRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreosHistoricoPlans.Where(x => x.CuentaCorreoTipoLicenciaId == itemDetalle.CuentaCorreoTipoLicenciaId).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreosHistoricoPlan oDetalle = new SAS_CuentasCorreosHistoricoPlan();
                                           // oDetalle.CuentaCorreoTipoLicenciaId = itemDetalle.CuentaCorreoTipoLicenciaId;
                                            oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.LicenciaTipoId = itemDetalle.LicenciaTipoId;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            oDetalle.Estado = itemDetalle.Estado;
                                            oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.HostName = itemDetalle.HostName;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;

                                            Modelo.SAS_CuentasCorreosHistoricoPlans.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (resultadoDetalle.Count == 1)
                                        {
                                            SAS_CuentasCorreosHistoricoPlan oDetalle = new SAS_CuentasCorreosHistoricoPlan();
                                            oDetalle = resultadoDetalle.ElementAt(0);
                                            //oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.LicenciaTipoId = itemDetalle.LicenciaTipoId;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            //oDetalle.Estado = itemDetalle.Estado;
                                            //oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            //oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            //oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            //oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            //oDetalle.UserID = itemDetalle.UserID;
                                            //oDetalle.HostName = itemDetalle.HostName;
                                            //oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            //Modelo.SAS_CuentasCorreosHistoricoPlans.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }

                                    }
                                }
                                #endregion
                            }

                            if (ListadoAsignacionCuentaRegistrar != null)
                            {
                                #region Registrar detalle Personal A Asignar() 
                                if (ListadoAsignacionCuentaRegistrar.ToList().Count > 0)
                                {
                                    foreach (var itemDetalle in ListadoAsignacionCuentaRegistrar)
                                    {
                                        var resultadoDetalle = Modelo.SAS_CuentasCorreoAsignacionPersonals.Where(x => x.CuentaCorreoAsignacionId == itemDetalle.CuentaCorreoAsignacionId).ToList();
                                        if (resultadoDetalle.Count == 0)
                                        {
                                            #region Registrar() 
                                            SAS_CuentasCorreoAsignacionPersonal oDetalle = new SAS_CuentasCorreoAsignacionPersonal();
                                           // oDetalle.CuentaCorreoAsignacionId = itemDetalle.CuentaCorreoAsignacionId;
                                            oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.PersonalID = itemDetalle.PersonalID;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            oDetalle.Estado = itemDetalle.Estado;
                                            oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            oDetalle.UserID = itemDetalle.UserID;
                                            oDetalle.HostName = itemDetalle.HostName;
                                            oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            Modelo.SAS_CuentasCorreoAsignacionPersonals.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (resultadoDetalle.Count == 1)
                                        {
                                            SAS_CuentasCorreoAsignacionPersonal oDetalle = new SAS_CuentasCorreoAsignacionPersonal();
                                            oDetalle = resultadoDetalle.ElementAt(0);
                                            // oDetalle.CuentaCorreoAsignacionId = itemDetalle.CuentaCorreoAsignacionId;
                                            //oDetalle.CuentaCorreoID = oCuentaDeCorreoCorporativo.id;
                                            oDetalle.PersonalID = itemDetalle.PersonalID;
                                            oDetalle.Desde = itemDetalle.Desde;
                                            oDetalle.Hasta = itemDetalle.Hasta;
                                            oDetalle.Nota = itemDetalle.Nota;
                                            //oDetalle.Estado = itemDetalle.Estado;
                                            //oDetalle.ReferenciaSolicitudID = itemDetalle.ReferenciaSolicitudID;
                                            //oDetalle.ReferenciaID = itemDetalle.ReferenciaID;
                                            //oDetalle.TablaReferencia = itemDetalle.TablaReferencia;
                                            //oDetalle.TablaSolicitud = itemDetalle.TablaSolicitud;
                                            //oDetalle.UserID = itemDetalle.UserID;
                                            //oDetalle.HostName = itemDetalle.HostName;
                                            //oDetalle.FechaRegistro = itemDetalle.FechaRegistro;
                                            //Modelo.SAS_CuentasCorreoAsignacionPersonals.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }

                                    }
                                }
                                #endregion
                            }

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
                                var resultadoDetail = Modelo.SAS_CuentasCorreoDetalles.Where(x => x.id == codigoRegistro).ToList();
                                if (resultadoDetail != null && resultadoDetail.ToList().Count > 0)
                                {
                                    Modelo.SAS_CuentasCorreoDetalles.DeleteAllOnSubmit(resultadoDetail);
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

        public int ChangeState(string conection, SAS_CuentasCorreo item, int EstadoID)
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
                        oregistro.estado = EstadoID;
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


        public List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult> ObtenerListadoDePersonalAsignacionPorCodigoCorreoElectronico(string conection, int CorreoElectronicoID)
        {

            List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult> listado = new List<SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoDeCuentasCorreoAsignacionPersonalByCuentaCorreoId(CorreoElectronicoID).ToList();
            }
            return listado;
        }

        public List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult> ObtenerListadoDePlanHistoricoPorCodigoCorreoElectronico(string conection, int CorreoElectronicoID)
        {

            List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult> listado = new List<SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoDeCuentaCorreoHistoricoPlanByCuentaCorreoId(CorreoElectronicoID).ToList();
            }
            return listado;
        }

        

        public List<DFormatoSimple> GetType(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var ResultadoQuery = Modelo.SAS_CuentasCorreoTipoLogs.ToList();
                if (ResultadoQuery != null && ResultadoQuery.ToList().Count > 0)
                {
                    listado = (from items in ResultadoQuery.ToList()
                               group items by new { items.CuentaCorreoTipoLogID } into j
                               select new DFormatoSimple
                               {
                                   Codigo = j.Key.CuentaCorreoTipoLogID.ToString(),
                                   Descripcion = j.FirstOrDefault().Descripcion.Trim().ToUpper(),
                               }
                           ).ToList();
                }

                //C.Add(new DFormatoSimple { Codigo = "0", Descripcion = "Apertura de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Generación de BackUp" });
                //listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Imagen" });
                //listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Cambio de contraseña" });
                //listado.Add(new DFormatoSimple { Codigo = "4", Descripcion = "Descarga y validación del Backup" });
                //listado.Add(new DFormatoSimple { Codigo = "5", Descripcion = "Configuración de backup" });
                //listado.Add(new DFormatoSimple { Codigo = "6", Descripcion = "Liberación de licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "7", Descripcion = "Asignación o reasignación de licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "8", Descripcion = "Upgrade licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "9", Descripcion = "Renombrar nombre de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "10", Descripcion = "Agregar Alias" });
                //listado.Add(new DFormatoSimple { Codigo = "11", Descripcion = "Agregar a grupo" });
                //listado.Add(new DFormatoSimple { Codigo = "12", Descripcion = "Baja de grupo" });
                //listado.Add(new DFormatoSimple { Codigo = "13", Descripcion = "Downgrade licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "14", Descripcion = "Baja de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "15", Descripcion = "Suspención de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "16", Descripcion = "Activación y/o Reactivación de cuenta" });

            }
            return listado;
        }

        public List<DFormatoSimple> ObtenerListadoDePersonal(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var ResultadoQuery = Modelo.SAS_ListadoPersonalEmpresaYExterno.ToList();
                if (ResultadoQuery != null && ResultadoQuery.ToList().Count > 0)
                {
                    listado = (from items in ResultadoQuery.ToList()
                               group items by new { items.codigo } into j
                               select new DFormatoSimple
                               {
                                   Codigo = j.Key.codigo.ToString(),
                                   Descripcion = j.FirstOrDefault().nombres.Trim().ToUpper(),
                               }
                           ).ToList();
                }

                //C.Add(new DFormatoSimple { Codigo = "0", Descripcion = "Apertura de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Generación de BackUp" });
                //listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Imagen" });
                //listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Cambio de contraseña" });
                //listado.Add(new DFormatoSimple { Codigo = "4", Descripcion = "Descarga y validación del Backup" });
                //listado.Add(new DFormatoSimple { Codigo = "5", Descripcion = "Configuración de backup" });
                //listado.Add(new DFormatoSimple { Codigo = "6", Descripcion = "Liberación de licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "7", Descripcion = "Asignación o reasignación de licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "8", Descripcion = "Upgrade licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "9", Descripcion = "Renombrar nombre de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "10", Descripcion = "Agregar Alias" });
                //listado.Add(new DFormatoSimple { Codigo = "11", Descripcion = "Agregar a grupo" });
                //listado.Add(new DFormatoSimple { Codigo = "12", Descripcion = "Baja de grupo" });
                //listado.Add(new DFormatoSimple { Codigo = "13", Descripcion = "Downgrade licencia" });
                //listado.Add(new DFormatoSimple { Codigo = "14", Descripcion = "Baja de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "15", Descripcion = "Suspención de cuenta" });
                //listado.Add(new DFormatoSimple { Codigo = "16", Descripcion = "Activación y/o Reactivación de cuenta" });

            }
            return listado;
        }


        


        public List<SAS_CuentasCorreoTipoLog> ListadoTipoCuentaCorreoLog(string conection)
        {
            List<SAS_CuentasCorreoTipoLog> listado = new List<SAS_CuentasCorreoTipoLog>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_CuentasCorreoTipoLogs.ToList();
            }
            return listado.OrderBy(x => x.Descripcion).ToList();
        }



        public int RegistrarTipoCuentaCorreoLog(string conection, SAS_CuentasCorreoTipoLog Item)
        {
            int resultadoQuery = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreoTipoLogs.Where(x => x.CuentaCorreoTipoLogID == Item.CuentaCorreoTipoLogID).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();
                            SAS_CuentasCorreoTipoLog oItem = new SAS_CuentasCorreoTipoLog();
                            //oItem.CuentaCorreoTipoLogID = Item.CuentaCorreoTipoLogID;
                            oItem.Descripcion = Item.Descripcion;
                            oItem.Estado = Item.Estado;
                            Modelo.SAS_CuentasCorreoTipoLogs.InsertOnSubmit(oItem);
                            Modelo.SubmitChanges();
                            #endregion
                            resultadoQuery = oItem.CuentaCorreoTipoLogID; // registrar                           
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar cabecera() 
                            SAS_CuentasCorreoTipoLog oItem = new SAS_CuentasCorreoTipoLog();
                            oItem = resultado.ElementAt(0);
                            //oItem.CuentaCorreoTipoLogID = Item.CuentaCorreoTipoLogID;
                            oItem.Descripcion = Item.Descripcion;
                            //oItem.Estado = Item.Estado;                            
                            Modelo.SubmitChanges();
                            #endregion
                            resultadoQuery = oItem.CuentaCorreoTipoLogID; // modificar
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return resultadoQuery;
        }


        public int CambiarEstadoTipoCuentaCorreoLog(string conection, SAS_CuentasCorreoTipoLog Item)
        {
            int resultadoQuery = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CuentasCorreoTipoLogs.Where(x => x.CuentaCorreoTipoLogID == Item.CuentaCorreoTipoLogID).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar cabecera() 
                            SAS_CuentasCorreoTipoLog oItem = new SAS_CuentasCorreoTipoLog();
                            oItem = resultado.ElementAt(0);

                            if (oItem.Estado == 1)
                            {
                                oItem.Estado = 0;
                            }
                            else
                            {
                                oItem.Estado = 1;
                            }
                                                      
                            Modelo.SubmitChanges();
                            #endregion
                            resultadoQuery = oItem.CuentaCorreoTipoLogID; // modificar
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return resultadoQuery;
        }

        public int EliminarTipoCuentaCorreoLog(string conection, SAS_CuentasCorreoTipoLog Item)
        {
            int resultadoQuery = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    resultadoQuery = Item.CuentaCorreoTipoLogID;
                   var resultado = Modelo.SAS_CuentasCorreoTipoLogs.Where(x => x.CuentaCorreoTipoLogID == Item.CuentaCorreoTipoLogID).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 1)
                        {
                           
                            #region Actualizar cabecera() 
                            SAS_CuentasCorreoTipoLog oItem = new SAS_CuentasCorreoTipoLog();
                            oItem = resultado.ElementAt(0);
                            Modelo.SAS_CuentasCorreoTipoLogs.DeleteOnSubmit(oItem);
                            Modelo.SubmitChanges();
                            #endregion                            
                        }
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return resultadoQuery;
        }

    }
}

