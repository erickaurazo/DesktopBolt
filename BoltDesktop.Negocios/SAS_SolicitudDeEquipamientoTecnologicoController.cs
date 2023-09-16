using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using System.Net.Mail;
using MyControlsDataBinding.Extensions;

namespace Asistencia.Negocios
{
    public class SAS_SolicitudDeEquipamientoTecnologicoController
    {
        private List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento;
        private List<SAS_DispositivoTipoDispositivo> listadoTipoDispositivoChip;
        const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        const string passwordCorreo = @"iompqiiuhkjngkjr";

        public List<SAS_SolicitudDeEquipamientoTecnologicoListado> ListRequests(string conection)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoListado> list = new List<SAS_SolicitudDeEquipamientoTecnologicoListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoListado.ToList();
            }
            return list.OrderByDescending(x => x.id).ToList();
        }

        public SAS_SolicitudDeEquipamientoTecnologico GetRequestsById(string conection, int id)
        {
            SAS_SolicitudDeEquipamientoTecnologico item = new SAS_SolicitudDeEquipamientoTecnologico();
            item.id = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == id).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        item = result.Single();
                    }
                }
            }
            return item;
        }


        public List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result> ListRequestsByDate(string conection, string fechaDesde, string fechaHasta)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result> list = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2Result>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoListadoByDates2(fechaDesde, fechaHasta).ToList();
            }
            return list.OrderByDescending(x => x.id).ToList();
        }


        public List<SAS_SolicitudDeEquipamientoTecnologicoListado> ListRequests(string conection, string desde, string hasta)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoListado> list = new List<SAS_SolicitudDeEquipamientoTecnologicoListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoListado.ToList();
            }
            return list.OrderByDescending(x => x.id).ToList();
        }

        public int ToRegister(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                            int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                            oRegistro.id = obtenerultimoregistro;
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.nombresCompletos = item.nombresCompletos;
                            oRegistro.esExterno = item.esExterno;
                            oRegistro.fecha = item.fecha;
                            oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                            oRegistro.esTemporal = item.esTemporal;
                            oRegistro.vencimientoContrato = item.vencimientoContrato;
                            oRegistro.itemInicioContrato = item.itemInicioContrato;
                            oRegistro.tipoContrato = item.tipoContrato;
                            oRegistro.justificacion = item.justificacion;
                            oRegistro.estadoCodigo = item.estadoCodigo;
                            oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                            oRegistro.tipoSolicitud = item.tipoSolicitud;
                            Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = oRegistro.id; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.id == 0)
                            {
                                #region Nuevo()
                                tipoResultadoOperacion = 0; // Nuevo() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // modificar
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

        public int ToRegister(string conection, SAS_SolicitudDeEquipamientoTecnologico item, List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> listadoSedesEnSolicitudRegistro)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                            int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                            oRegistro.id = obtenerultimoregistro;
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.nombresCompletos = item.nombresCompletos;
                            oRegistro.esExterno = item.esExterno;
                            oRegistro.fecha = item.fecha;
                            oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                            oRegistro.esTemporal = item.esTemporal;
                            oRegistro.vencimientoContrato = item.vencimientoContrato;
                            oRegistro.itemInicioContrato = item.itemInicioContrato;
                            oRegistro.tipoContrato = item.tipoContrato;
                            oRegistro.justificacion = item.justificacion;
                            oRegistro.estadoCodigo = item.estadoCodigo;
                            oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                            oRegistro.tipoSolicitud = item.tipoSolicitud;
                            Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = oRegistro.id; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            if (item.id == 0)
                            {
                                #region Nuevo()
                                tipoResultadoOperacion = 0; // Nuevo() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar


                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                            oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                            oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                            oSede.estado = 1;
                                            oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // modificar


                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {

                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.Where(x => x.idSolicitudEquipamientoTecnologico == itemSede.idSolicitudEquipamientoTecnologico && x.item == itemSede.item).ToList();

                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 0)
                                                {
                                                    #region Nuevo()
                                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                    oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                    oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                    oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                    oSede = resultadoCoincidencia.Single();
                                                    //oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                    //oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                    oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    //Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                    Modelo.SubmitChanges();
                                                }
                                            }


                                        }
                                    }
                                }

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

        public int ToRegister(string conection, SAS_SolicitudDeEquipamientoTecnologico item, List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> listadoSedesEnSolicitudRegistro, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareARegistrar)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            int codigo = 0; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (item.id == 0)
                    {
                        #region Nuevo() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                        oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = item.esExterno;
                        oRegistro.fecha = item.fecha;
                        oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                        oRegistro.esTemporal = item.esTemporal;
                        oRegistro.vencimientoContrato = item.vencimientoContrato;
                        oRegistro.itemInicioContrato = item.itemInicioContrato;
                        oRegistro.tipoContrato = item.tipoContrato;
                        oRegistro.justificacion = item.justificacion;
                        oRegistro.estadoCodigo = item.estadoCodigo;
                        oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                        oRegistro.tipoSolicitud = item.tipoSolicitud;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registrar listado de sedes que aplica para la solicitud  
                        if (listadoSedesEnSolicitudRegistro != null)
                        {
                            if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                    oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                    oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                    oSede.estado = itemSede.estado;
                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                        #region Registrar listado de Hardware()
                        if (listadoHardwareARegistrar != null)
                        {
                            if (listadoHardwareARegistrar.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                    oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                    oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.estado = 1;
                                    oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                    oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                    oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                    oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                    oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;

                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                        #region Registrar listado de Software()
                        if (listadoSoftwareARegistrar != null)
                        {
                            if (listadoSoftwareARegistrar.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                    oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                    oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.estado = 1;
                                    oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                    oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                    oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                    oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                    oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                        if (resultado != null)
                        {
                            #region Registro | Actualización()  
                            if (resultado.ToList().Count == 0)
                            {
                                //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                                #region Nuevo() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;

                                #region Registrar listado de sedes que aplica para la solicitud  
                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                            oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                            oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                            oSede.estado = 1;
                                            oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Hardware()
                                if (listadoHardwareARegistrar != null)
                                {
                                    if (listadoHardwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                            oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;
                                            oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Software()
                                if (listadoSoftwareARegistrar != null)
                                {
                                    if (listadoSoftwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                            oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                            oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion


                                #endregion
                            }
                            else if (resultado.ToList().Count == 1)
                            {
                                #region Actualizar() 


                                if (item.id == 0)
                                {
                                    #region Nuevo()
                                    tipoResultadoOperacion = 0; // Nuevo() 
                                    oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                    //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                    // oRegistro.id = obtenerultimoregistro;
                                    oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                    oRegistro.nombresCompletos = item.nombresCompletos;
                                    oRegistro.esExterno = item.esExterno;
                                    oRegistro.fecha = item.fecha;
                                    oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                    oRegistro.esTemporal = item.esTemporal;
                                    oRegistro.vencimientoContrato = item.vencimientoContrato;
                                    oRegistro.itemInicioContrato = item.itemInicioContrato;
                                    oRegistro.tipoContrato = item.tipoContrato;
                                    oRegistro.justificacion = item.justificacion;
                                    oRegistro.estadoCodigo = item.estadoCodigo;
                                    oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                    oRegistro.tipoSolicitud = item.tipoSolicitud;

                                    Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                    Modelo.SubmitChanges();
                                    tipoResultadoOperacion = oRegistro.id; // registrar
                                    codigo = oRegistro.id;

                                    #region Registrar listado de sedes que aplica para la solicitud  
                                    if (listadoSedesEnSolicitudRegistro != null)
                                    {
                                        if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                oSede.estado = 1;
                                                oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Hardware()
                                    if (listadoHardwareARegistrar != null)
                                    {
                                        if (listadoHardwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.estado = 1;
                                                oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                                oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                                oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                                //oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                                oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;

                                                oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Software()
                                    if (listadoSoftwareARegistrar != null)
                                    {
                                        if (listadoSoftwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.estado = 1;
                                                oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }
                                else
                                {
                                    #region Actualizar() 
                                    oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                    oRegistro = resultado.Single();
                                    oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                    oRegistro.nombresCompletos = item.nombresCompletos;
                                    oRegistro.esExterno = item.esExterno;
                                    oRegistro.fecha = item.fecha;
                                    oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                    oRegistro.esTemporal = item.esTemporal;
                                    oRegistro.justificacion = item.justificacion;
                                    oRegistro.estadoCodigo = item.estadoCodigo;
                                    oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                    oRegistro.tipoSolicitud = item.tipoSolicitud;
                                    oRegistro.vencimientoContrato = item.vencimientoContrato;
                                    oRegistro.itemInicioContrato = item.itemInicioContrato;
                                    oRegistro.tipoContrato = item.tipoContrato;

                                    Modelo.SubmitChanges();
                                    tipoResultadoOperacion = oRegistro.id; // modificar
                                    codigo = oRegistro.id;
                                    #region Registrar listado de sedes que aplica para la solicitud                                
                                    if (listadoSedesEnSolicitudRegistro != null)
                                    {
                                        if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.Where(x => x.idSolicitudEquipamientoTecnologico == itemSede.idSolicitudEquipamientoTecnologico && x.item == itemSede.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                                                        oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                        oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                        oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                        oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                        oSede = resultadoCoincidencia.Single();
                                                        //oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                        oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                        oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                        oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Hardware que aplica para la solicitud                                
                                    if (listadoHardwareARegistrar != null)
                                    {
                                        if (listadoHardwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemHardware in listadoHardwareARegistrar.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == itemHardware.idSolicitudEquipamientoTecnologico && x.item == itemHardware.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                        oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                        //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                        oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;

                                                        oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                        oItem = resultadoCoincidencia.Single();
                                                        //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                        //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                        oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;

                                                        oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;
                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Hardware que aplica para la solicitud                                
                                    if (listadoSoftwareARegistrar != null)
                                    {
                                        if (listadoSoftwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.Where(x => x.idSolicitudEquipamientoTecnologico == itemSoftware.idSolicitudEquipamientoTecnologico && x.item == itemSoftware.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                        oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                        oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                        oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                        oItem = resultadoCoincidencia.Single();
                                                        //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                        oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                        oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }


                                #endregion
                            }
                            #endregion
                        }
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;

        }

        public void GenerarSolicitudDeBajaoAltaAPartirDeEsteDocumento(SAS_SolicitudDeEquipamientoTecnologico solicitud, string conection, SAS_USUARIOS user, int EsSolicitudDeBaja)
        {
            SAS_SolicitudDeEquipamientoTecnologico SolicitudDeAlta = new SAS_SolicitudDeEquipamientoTecnologico();
            SAS_SolicitudDeEquipamientoTecnologico SolicitudDeBaja = new SAS_SolicitudDeEquipamientoTecnologico();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            string codigoAnteriorALiberacion = string.Empty;
            string nombresColaborador = string.Empty;

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                //using (TransactionScope Scope = new TransactionScope())
                //{

                if (solicitud.id > 0)
                {
                    var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == solicitud.id).ToList();
                    int codigoSolicitudOrigen = resultado.Single().id;
                    string codigoTranbajadorEnSolicitud = resultado.Single().idCodigoGeneral != null ? resultado.Single().idCodigoGeneral.Trim() : string.Empty;
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count >= 1)
                        {
                            SolicitudDeBaja = new SAS_SolicitudDeEquipamientoTecnologico();
                            SolicitudDeAlta = new SAS_SolicitudDeEquipamientoTecnologico();

                            SolicitudDeAlta = resultado.Single();
                            SolicitudDeBaja = resultado.Single();

                            nombresColaborador = Modelo.SAS_ListadoPersonalEmpresaYExterno.Where(x => x.codigo.Trim() == SolicitudDeAlta.idCodigoGeneral.Trim()).ToList().Single().nombres.Trim();

                            /* 1.- Copiar datos de la anterior solicitud a la nueva solicitud*/
                            SolicitudDeBaja.id = 0;
                            SolicitudDeBaja.tipoSolicitud = EsSolicitudDeBaja == 1 ? 3 : 1;
                            SolicitudDeBaja.estadoCodigo = "PE";
                            SolicitudDeBaja.fechaDeVencimiento = DateTime.Now.AddDays(360);
                            SolicitudDeBaja.fecha = DateTime.Now;
                            SolicitudDeBaja.justificacion = "Solicitud de baja Generada desde solicitud " + "SOL-0001-" + codigoSolicitudOrigen.ToString().PadLeft(7, '0');
                            SolicitudDeBaja.glosa = "Solicitud de " + (EsSolicitudDeBaja == 1 ? "BAJA" : "ALTA") + " Generada desde solicitud " + "SOL-0001-" + codigoSolicitudOrigen.ToString().PadLeft(7, '0');
                            SolicitudDeBaja.idReferencia = codigoSolicitudOrigen;
                            SolicitudDeBaja.Cerrado = 0;
                            SolicitudDeBaja.FechaCierre = (DateTime?)null;
                            SolicitudDeBaja.idReferenciaSolicitudRenovacion = (Int32?)null;
                            SolicitudDeBaja.estadoCodigoAnterior = string.Empty;


                            /* 2.- Copiar todos los datos detalles anterior a la nueva solicitud a la nueva solicitud*/
                            List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> listadoSedesEnSolicitudRegistro = new List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo>();

                            var resultadoSedes = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.Where(x => x.idSolicitudEquipamientoTecnologico == solicitud.id).ToList();

                            listadoSedesEnSolicitudRegistro = (from item in resultadoSedes
                                                               where item.idSolicitudEquipamientoTecnologico > 0 && item.item != string.Empty
                                                               group item by new { item.item } into j
                                                               select new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo
                                                               {
                                                                   idSolicitudEquipamientoTecnologico = 0,
                                                                   item = j.FirstOrDefault().item != null ? j.FirstOrDefault().item : string.Empty,
                                                                   sedeDeTrabajoCodigo = j.FirstOrDefault().sedeDeTrabajoCodigo != null ? j.FirstOrDefault().sedeDeTrabajoCodigo : string.Empty,
                                                                   estado = j.FirstOrDefault().estado != (decimal?)null ? j.FirstOrDefault().estado : (decimal?)null,
                                                                   desde = DateTime.Now,
                                                                   hasta = DateTime.Now.AddDays(360)
                                                               }).ToList();


                            List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                            var resultadoHardware = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == solicitud.id && x.elegido == 1).ToList();

                            listadoHardwareARegistrar = (from item in resultadoHardware
                                                         where item.idSolicitudEquipamientoTecnologico > 0 && item.item != string.Empty
                                                         group item by new { item.item } into j
                                                         select new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware
                                                         {
                                                             idSolicitudEquipamientoTecnologico = 0,
                                                             item = j.FirstOrDefault().item != null ? j.FirstOrDefault().item : string.Empty,
                                                             idDispositivoTipoHardware = j.FirstOrDefault().idDispositivoTipoHardware != null ? j.FirstOrDefault().idDispositivoTipoHardware : string.Empty,
                                                             desde = DateTime.Now,
                                                             hasta = DateTime.Now.AddDays(360),
                                                             estado = j.FirstOrDefault().estado != (decimal?)null ? j.FirstOrDefault().estado : (decimal?)null,
                                                             valor = j.FirstOrDefault().valor != (decimal?)null ? j.FirstOrDefault().valor : (decimal?)null,
                                                             glosa = j.FirstOrDefault().glosa != null ? j.FirstOrDefault().glosa : string.Empty,
                                                             actualizado = j.FirstOrDefault().actualizado != (decimal?)null ? j.FirstOrDefault().actualizado : (decimal?)null,
                                                             elegido = j.FirstOrDefault().elegido != (decimal?)null ? j.FirstOrDefault().elegido : (decimal?)null,
                                                             codigoERP = j.FirstOrDefault().codigoERP != (Int32?)null ? j.FirstOrDefault().codigoERP : (Int32?)null,
                                                             GeneraSolicitud = j.FirstOrDefault().GeneraSolicitud != (decimal?)null ? j.FirstOrDefault().GeneraSolicitud : (decimal?)null,
                                                             idReferenciaSoporteTecnico = j.FirstOrDefault().idReferenciaSoporteTecnico != (Int32?)null ? j.FirstOrDefault().idReferenciaSoporteTecnico : (Int32?)null,
                                                             RequiereCapacitacion = j.FirstOrDefault().RequiereCapacitacion != (Int32?)null ? j.FirstOrDefault().RequiereCapacitacion : (Int32?)null
                                                         }).ToList();




                            List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware>();
                            var resultadoSoftware = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.Where(x => x.idSolicitudEquipamientoTecnologico == solicitud.id && x.elegido == 1).ToList();


                            listadoSoftwareARegistrar = (from item in resultadoSoftware
                                                         where item.idSolicitudEquipamientoTecnologico > 0 && item.item != string.Empty
                                                         group item by new { item.item } into j
                                                         select new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware
                                                         {
                                                             idSolicitudEquipamientoTecnologico = 0,
                                                             item = j.FirstOrDefault().item != null ? j.FirstOrDefault().item : string.Empty,
                                                             idDispositivoTipoSoftware = j.FirstOrDefault().idDispositivoTipoSoftware != (Int32?)null ? j.FirstOrDefault().idDispositivoTipoSoftware : (Int32?)null,
                                                             desde = DateTime.Now,
                                                             hasta = DateTime.Now.AddDays(360),
                                                             estado = j.FirstOrDefault().estado != (decimal?)null ? j.FirstOrDefault().estado : (decimal?)null,
                                                             valor = j.FirstOrDefault().valor != (decimal?)null ? j.FirstOrDefault().valor : (decimal?)null,
                                                             glosa = j.FirstOrDefault().glosa != null ? j.FirstOrDefault().glosa : string.Empty,
                                                             actualizado = j.FirstOrDefault().actualizado != (decimal?)null ? j.FirstOrDefault().actualizado : (decimal?)null,
                                                             elegido = j.FirstOrDefault().elegido != (decimal?)null ? j.FirstOrDefault().elegido : (decimal?)null,
                                                             perfilDeAcceso = j.FirstOrDefault().perfilDeAcceso != (Int32?)null ? j.FirstOrDefault().perfilDeAcceso : (Int32?)null,
                                                             GeneraSolicitud = j.FirstOrDefault().GeneraSolicitud != (Int32?)null ? j.FirstOrDefault().GeneraSolicitud : (Int32?)null,
                                                             idReferenciaSoporteFuncional = j.FirstOrDefault().idReferenciaSoporteFuncional != (Int32?)null ? j.FirstOrDefault().idReferenciaSoporteFuncional : (Int32?)null,
                                                             RequiereCapacitacion = j.FirstOrDefault().RequiereCapacitacion != (Int32?)null ? j.FirstOrDefault().RequiereCapacitacion : (Int32?)null
                                                         }).ToList();


                            /* Actualizar la línea telefónica */



                            List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular> listadoLineaCelularARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular>();
                            var resultadoLineasCelulares = Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.Where(x => x.idSolicitudEquipamientoTecnologico == solicitud.id).ToList();


                            listadoLineaCelularARegistrar = (from item in resultadoLineasCelulares
                                                             where item.idSolicitudEquipamientoTecnologico > 0 && item.item != string.Empty
                                                             group item by new { item.item } into j
                                                             select new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular
                                                             {
                                                                 idSolicitudEquipamientoTecnologico = 0,
                                                                 item = j.FirstOrDefault().item != null ? j.FirstOrDefault().item : string.Empty,
                                                                 idLinea = j.FirstOrDefault().idLinea != (Int32?)null ? j.FirstOrDefault().idLinea : (Int32?)null,
                                                                 desde = DateTime.Now,
                                                                 hasta = DateTime.Now.AddDays(360),
                                                                 estado = j.FirstOrDefault().estado != (decimal?)null ? j.FirstOrDefault().estado : (decimal?)null,
                                                                 valor = j.FirstOrDefault().valor != (decimal?)null ? j.FirstOrDefault().valor : (decimal?)null,
                                                                 glosa = j.FirstOrDefault().glosa != null ? j.FirstOrDefault().glosa : string.Empty,
                                                                 actualizado = j.FirstOrDefault().actualizado != (decimal?)null ? j.FirstOrDefault().actualizado : (decimal?)null,
                                                                 elegido = j.FirstOrDefault().elegido != (decimal?)null ? j.FirstOrDefault().elegido : (decimal?)null
                                                             }).ToList();


                            // Registro la solicitud de baja
                            SAS_SolicitudDeEquipamientoTecnologicoController nuevoModelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                            int resultadoPrimeraParte = nuevoModelo.ToRegisterLocal(conection, SolicitudDeBaja, listadoSedesEnSolicitudRegistro, listadoHardwareARegistrar, listadoSoftwareARegistrar, listadoLineaCelularARegistrar);


                            listadoHardwareARegistrar = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();
                            listadoHardwareARegistrar = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == resultadoPrimeraParte && x.elegido == 1).ToList();

                            /* Actualizar el equipo de cómputo, portatil, Celular, Tablet con el nomnbre del nuevo colaborador*/
                            if (listadoHardwareARegistrar != null)
                            {
                                if (listadoHardwareARegistrar.ToList().Count > 0)
                                {
                                    foreach (var item in listadoHardwareARegistrar)
                                    {
                                        var DispositivoConsulta = Modelo.SAS_Dispostivo.Where(x => x.id == item.codigoERP).ToList();
                                        if (DispositivoConsulta != null && DispositivoConsulta.ToList().Count >= 1)
                                        {
                                            SAS_Dispostivo oDispositivo = new SAS_Dispostivo();
                                            oDispositivo = DispositivoConsulta.Single();
                                            oDispositivo.caracteristicas = nombresColaborador + " " + (oDispositivo.caracteristicas).Trim();


                                            var listadoDispositivoPorPersonaActivos = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == item.codigoERP).ToList();
                                            int ultimoNumeroDeItemListaDetalleDispositivoPorColaborador = 0;
                                            if (listadoDispositivoPorPersonaActivos != null && listadoDispositivoPorPersonaActivos.ToList().Count > 0)
                                            {
                                                ultimoNumeroDeItemListaDetalleDispositivoPorColaborador = Convert.ToInt32(listadoDispositivoPorPersonaActivos.Max(x => x.item));
                                            }

                                            var listadoFiltroSoloActivos = listadoDispositivoPorPersonaActivos.Where(x => x.estado == 1).ToList();

                                            if (listadoFiltroSoloActivos != null)
                                            {
                                                if (listadoFiltroSoloActivos.ToList().Count > 0)
                                                {
                                                    foreach (var itemColaboradorConDispositivo in listadoFiltroSoloActivos)
                                                    {
                                                        SAS_DispositivoUsuarios itemDetalle = new SAS_DispositivoUsuarios();
                                                        var listadoItemDetalle = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == itemColaboradorConDispositivo.dispositivoCodigo && x.item == itemColaboradorConDispositivo.item).ToList();

                                                        if (listadoItemDetalle != null && listadoItemDetalle.ToList().Count >= 1)
                                                        {
                                                            itemDetalle = new SAS_DispositivoUsuarios();
                                                            itemDetalle = listadoItemDetalle.Single();
                                                            itemDetalle.estado = Convert.ToByte(0);
                                                            itemDetalle.idcodigoGeneral = listadoItemDetalle.ElementAt(0).idcodigoGeneral;
                                                            itemDetalle.hasta = listadoItemDetalle.ElementAt(0).hasta;
                                                            itemDetalle.desde = listadoItemDetalle.ElementAt(0).desde;
                                                            itemDetalle.observacion = listadoItemDetalle.ElementAt(0).observacion;
                                                            itemDetalle.tipo = listadoItemDetalle.ElementAt(0).tipo;

                                                            nuevoModelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                                                            nuevoModelo.RegistaDispositivoUsuarios(conection, itemDetalle);


                                                        }

                                                    }
                                                    if (EsSolicitudDeBaja == 0)
                                                    {
                                                        SAS_DispositivoUsuarios itemDetalleRegistrar = new SAS_DispositivoUsuarios();
                                                        itemDetalleRegistrar.dispositivoCodigo = Convert.ToInt32(item.codigoERP);
                                                        itemDetalleRegistrar.item = (ultimoNumeroDeItemListaDetalleDispositivoPorColaborador + 1).ToString().PadLeft(3, '0');
                                                        itemDetalleRegistrar.estado = 1;
                                                        itemDetalleRegistrar.idcodigoGeneral = codigoTranbajadorEnSolicitud;
                                                        itemDetalleRegistrar.desde = DateTime.Now;
                                                        itemDetalleRegistrar.hasta = DateTime.Now.AddDays(360);
                                                        itemDetalleRegistrar.observacion = string.Empty;
                                                        itemDetalleRegistrar.fechaCreacion = DateTime.Now;
                                                        itemDetalleRegistrar.registradoPor = user.IdUsuario != null ? user.IdUsuario : Environment.UserName;
                                                        itemDetalleRegistrar.tipo = '0';
                                                        itemDetalleRegistrar.seVisualizaEnReportes = 0;

                                                        nuevoModelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                                                        nuevoModelo.RegistaDispositivoUsuarios(conection, itemDetalleRegistrar);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            SolicitudDeAlta.idReferencia = resultadoPrimeraParte;
                            SolicitudDeAlta.Cerrado = EsSolicitudDeBaja == 1 ? 1 : 0;
                            SolicitudDeAlta.FechaCierre = EsSolicitudDeBaja == 1 ? DateTime.Now : (DateTime?)null;

                            //Cerrar la primera solicitud, poniendo como datos de referencia, los de la nueva solicitud de baja que acabo de crear
                            nuevoModelo = new SAS_SolicitudDeEquipamientoTecnologicoController();
                            nuevoModelo.RegistarSolicitudDeEquipamientoTecnologico(conection, SolicitudDeAlta);

                        }
                    }
                }
                //    Scope.Complete();
                //}
            }
        }

        public void RegistarSolicitudDeEquipamientoTecnologico(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {

                    if (resultado.ToList().Count == 1)
                    {

                        SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        oRegistro = resultado.Single();
                        oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                        oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = item.esExterno;
                        oRegistro.fecha = item.fecha;
                        oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                        oRegistro.esTemporal = item.esTemporal;
                        oRegistro.justificacion = item.justificacion;
                        oRegistro.estadoCodigo = item.estadoCodigo;
                        oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                        oRegistro.tipoSolicitud = item.tipoSolicitud;
                        oRegistro.vencimientoContrato = item.vencimientoContrato;
                        oRegistro.itemInicioContrato = item.itemInicioContrato;
                        oRegistro.tipoContrato = item.tipoContrato;
                        oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                        oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                        oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                        oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                        oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                        oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;
                        Modelo.SubmitChanges();
                    }
                }
            }
        }



        public void RegistaDispositivoUsuarios(string conection, SAS_DispositivoUsuarios itemDetalle)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == itemDetalle.dispositivoCodigo && x.item == itemDetalle.item).ToList();
                if (resultado != null)
                {

                    if (resultado.ToList().Count == 0)
                    {
                        SAS_DispositivoUsuarios itemDetalleRegistrar = new SAS_DispositivoUsuarios();
                        itemDetalleRegistrar.dispositivoCodigo = itemDetalle.dispositivoCodigo;
                        itemDetalleRegistrar.item = itemDetalle.item;
                        itemDetalleRegistrar.estado = itemDetalle.estado;
                        itemDetalleRegistrar.idcodigoGeneral = itemDetalle.idcodigoGeneral;
                        itemDetalleRegistrar.desde = itemDetalle.desde;
                        itemDetalleRegistrar.hasta = itemDetalle.hasta;
                        itemDetalleRegistrar.observacion = itemDetalle.observacion;
                        itemDetalleRegistrar.fechaCreacion = itemDetalle.fechaCreacion;
                        itemDetalleRegistrar.registradoPor = itemDetalle.registradoPor;
                        itemDetalleRegistrar.tipo = itemDetalle.tipo;
                        itemDetalleRegistrar.seVisualizaEnReportes = itemDetalle.seVisualizaEnReportes;
                        Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(itemDetalleRegistrar);
                        Modelo.SubmitChanges();
                    }
                    else if (resultado.ToList().Count == 1)
                    {

                        SAS_DispositivoUsuarios itemDetalleRegistrar = new SAS_DispositivoUsuarios();
                        itemDetalleRegistrar = resultado.Single();
                        itemDetalleRegistrar.estado = itemDetalle.estado;
                        itemDetalleRegistrar.idcodigoGeneral = itemDetalle.idcodigoGeneral;
                        itemDetalleRegistrar.desde = itemDetalle.desde;
                        itemDetalleRegistrar.hasta = itemDetalle.hasta;
                        itemDetalleRegistrar.observacion = itemDetalle.observacion;
                        itemDetalleRegistrar.fechaCreacion = itemDetalle.fechaCreacion;
                        itemDetalleRegistrar.registradoPor = itemDetalle.registradoPor;
                        itemDetalleRegistrar.tipo = itemDetalle.tipo;
                        itemDetalleRegistrar.seVisualizaEnReportes = itemDetalle.seVisualizaEnReportes;
                        Modelo.SubmitChanges();
                    }
                }
            }
        }

        public void AprobarSolicitud(SAS_SolicitudDeEquipamientoTecnologico solicitud, string conection)
        {
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (solicitud.id > 0)
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == solicitud.id).ToList();
                        if (resultado != null)
                        {
                            if (resultado.ToList().Count == 1)
                            {
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                string codigoAnteriorALiberacion = oRegistro.estadoCodigo;
                                oRegistro.estadoCodigo = "AP";
                                oRegistro.estadoCodigoAnterior = codigoAnteriorALiberacion;
                                Modelo.SubmitChanges();

                            }
                        }
                    }
                    Scope.Complete();
                }
            }
        }

        public void LiberarSolicitud(SAS_SolicitudDeEquipamientoTecnologico solicitud, string conection)
        {
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (solicitud.id > 0)
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == solicitud.id).ToList();
                        if (resultado != null)
                        {
                            if (resultado.ToList().Count == 1)
                            {
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                string codigoAnteriorALiberacion = oRegistro.estadoCodigo;
                                oRegistro.estadoCodigo = "PE";
                                oRegistro.Cerrado = 0;
                                oRegistro.idReferencia = (int?)null;
                                oRegistro.FechaCierre = (DateTime?)null;
                                oRegistro.estadoCodigoAnterior = codigoAnteriorALiberacion;
                                Modelo.SubmitChanges();

                            }
                        }
                    }
                    Scope.Complete();
                }
            }
        }


        public void ActivarSolicitud(SAS_SolicitudDeEquipamientoTecnologico solicitud, string conection)
        {
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (solicitud.id > 0)
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == solicitud.id).ToList();
                        if (resultado != null)
                        {
                            if (resultado.ToList().Count == 1)
                            {
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                oRegistro.estadoCodigo = "PE";
                                Modelo.SubmitChanges();

                            }
                        }
                    }
                    Scope.Complete();
                }
            }
        }


        public void DesactivarSolicitud(SAS_SolicitudDeEquipamientoTecnologico solicitud, string conection)
        {
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (solicitud.id > 0)
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == solicitud.id).ToList();
                        if (resultado != null)
                        {
                            if (resultado.ToList().Count == 1)
                            {
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                oRegistro.estadoCodigo = "AN";
                                oRegistro.Cerrado = 1;
                                oRegistro.idReferencia = oRegistro.id;
                                oRegistro.FechaCierre = DateTime.Now;

                                Modelo.SubmitChanges();

                            }
                        }
                    }
                    Scope.Complete();
                }
            }
        }


        public int ToRegister(string conection, SAS_SolicitudDeEquipamientoTecnologico item, List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> listadoSedesEnSolicitudRegistro, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular> listadoLineaCelularARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareAEliminar, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareAEliminar)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            int codigo = 0; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {

                    if (item.id == 0)
                    {
                        #region Nuevo() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                        oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = item.esExterno;
                        oRegistro.fecha = item.fecha;
                        oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                        oRegistro.esTemporal = item.esTemporal;
                        oRegistro.vencimientoContrato = item.vencimientoContrato;
                        oRegistro.itemInicioContrato = item.itemInicioContrato;
                        oRegistro.tipoContrato = item.tipoContrato;
                        oRegistro.justificacion = item.justificacion;
                        oRegistro.estadoCodigo = item.estadoCodigo;
                        oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                        oRegistro.tipoSolicitud = item.tipoSolicitud;
                        oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                        oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                        oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                        oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                        oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                        oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;


                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registrar listado de sedes que aplica para la solicitud  
                        if (listadoSedesEnSolicitudRegistro != null)
                        {
                            if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                    oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                    oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                    oSede.estado = itemSede.estado;
                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                        #region Registrar listado de Hardware()
                        if (listadoHardwareARegistrar != null)
                        {
                            if (listadoHardwareARegistrar.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                    oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                    oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.estado = 1;
                                    oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                    oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                    oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                    oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                    oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;

                                    oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                    oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                    oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;


                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                        #region Registrar listado de Software()
                        if (listadoSoftwareARegistrar != null)
                        {
                            if (listadoSoftwareARegistrar.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                    oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                    oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.estado = 1;
                                    oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                    oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                    oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                    oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                    oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (Int32?)null ? itemSoftware.perfilDeAcceso.Value : 1;


                                    oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (Int32?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                    oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (Int32?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                    oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (Int32?)null ? itemSoftware.RequiereCapacitacion.Value : 1;

                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                        #region Registrar listado de Lineas celulares()
                        if (listadoLineaCelularARegistrar != null)
                        {
                            if (listadoLineaCelularARegistrar.ToList().Count > 0)
                            {
                                foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                {
                                    SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                    oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                    oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                    oItem.estado = 1;
                                    oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                    oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                    oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                    oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                    oItem.idReferencia = itemCelular.idReferencia != (int?)null ? itemCelular.idReferencia.Value : (int?)null;
                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(oItem);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                        if (resultado != null)
                        {
                            #region Registro | Actualización()  
                            if (resultado.ToList().Count == 0)
                            {
                                //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                                #region Nuevo() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                                oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                                oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                                oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                                oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                                oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;

                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;

                                #region Registrar listado de sedes que aplica para la solicitud  
                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                            oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                            oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                            oSede.estado = 1;
                                            oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Hardware()
                                if (listadoHardwareARegistrar != null)
                                {
                                    if (listadoHardwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                            oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;
                                            oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;


                                            oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                            oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                            oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;

                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Software()
                                if (listadoSoftwareARegistrar != null)
                                {
                                    if (listadoSoftwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                            oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                            oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;


                                            oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                            oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                            oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;

                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Lineas celulares()
                                if (listadoLineaCelularARegistrar != null)
                                {
                                    if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                            oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                            oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                            oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                            oItem.idReferencia = itemCelular.idReferencia != (int?)null ? itemCelular.idReferencia.Value : (int?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion


                                #endregion
                            }
                            else if (resultado.ToList().Count == 1)
                            {
                                #region Actualizar() 


                                if (item.id == 0)
                                {
                                    #region Nuevo()
                                    tipoResultadoOperacion = 0; // Nuevo() 
                                    oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                    //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                    // oRegistro.id = obtenerultimoregistro;
                                    oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                    oRegistro.nombresCompletos = item.nombresCompletos;
                                    oRegistro.esExterno = item.esExterno;
                                    oRegistro.fecha = item.fecha;
                                    oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                    oRegistro.esTemporal = item.esTemporal;
                                    oRegistro.vencimientoContrato = item.vencimientoContrato;
                                    oRegistro.itemInicioContrato = item.itemInicioContrato;
                                    oRegistro.tipoContrato = item.tipoContrato;
                                    oRegistro.justificacion = item.justificacion;
                                    oRegistro.estadoCodigo = item.estadoCodigo;
                                    oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                    oRegistro.tipoSolicitud = item.tipoSolicitud;
                                    oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                                    oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                                    oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                                    oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                                    oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                                    oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;

                                    Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                    Modelo.SubmitChanges();
                                    tipoResultadoOperacion = oRegistro.id; // registrar
                                    codigo = oRegistro.id;

                                    #region Registrar listado de sedes que aplica para la solicitud  
                                    if (listadoSedesEnSolicitudRegistro != null)
                                    {
                                        if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                oSede.estado = 1;
                                                oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Hardware()
                                    if (listadoHardwareARegistrar != null)
                                    {
                                        if (listadoHardwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.estado = 1;
                                                oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                                oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                                oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                                //oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                                oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;


                                                oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                                oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                                oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;

                                                oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Software()
                                    if (listadoSoftwareARegistrar != null)
                                    {
                                        if (listadoSoftwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.estado = 1;
                                                oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (Int32?)null ? itemSoftware.perfilDeAcceso.Value : 0;

                                                oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (Int32?)null ? itemSoftware.GeneraSolicitud.Value : 0;
                                                oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (Int32?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 0;
                                                oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (Int32?)null ? itemSoftware.RequiereCapacitacion.Value : 0;


                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Lineas celulares()
                                    if (listadoLineaCelularARegistrar != null)
                                    {
                                        if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                            {
                                                SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                                oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                oItem.estado = 1;
                                                oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                                oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                                oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                                oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                                oItem.idReferencia = itemCelular.idReferencia != (int?)null ? itemCelular.idReferencia.Value : (int?)null;
                                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                                Modelo.SubmitChanges();
                                            }
                                        }
                                    }
                                    #endregion


                                    #endregion
                                }
                                else
                                {
                                    #region Actualizar() 
                                    oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                    oRegistro = resultado.Single();
                                    oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                    oRegistro.nombresCompletos = item.nombresCompletos;
                                    oRegistro.esExterno = item.esExterno;
                                    oRegistro.fecha = item.fecha;
                                    oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                    oRegistro.esTemporal = item.esTemporal;
                                    oRegistro.justificacion = item.justificacion;
                                    oRegistro.estadoCodigo = item.estadoCodigo;
                                    oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                    oRegistro.tipoSolicitud = item.tipoSolicitud;
                                    oRegistro.vencimientoContrato = item.vencimientoContrato;
                                    oRegistro.itemInicioContrato = item.itemInicioContrato;
                                    oRegistro.tipoContrato = item.tipoContrato;
                                    oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                                    oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                                    oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                                    oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                                    oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                                    oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;

                                    Modelo.SubmitChanges();
                                    tipoResultadoOperacion = oRegistro.id; // modificar
                                    codigo = oRegistro.id;

                                    #region Eliminar listado de Hardware()

                                    if (listadoHardwareAEliminar != null && listadoHardwareAEliminar.ToList().Count > 0)
                                    {
                                        foreach (var oDetalle in listadoHardwareAEliminar)
                                        {
                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == oDetalle.idSolicitudEquipamientoTecnologico && x.item == oDetalle.item).ToList();
                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                    oItem = resultadoCoincidencia.Single();
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.DeleteOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Eliminar listado Software
                                    if (listadoSoftwareAEliminar != null && listadoSoftwareAEliminar.ToList().Count > 0)
                                    {
                                        foreach (var oDetalle in listadoSoftwareAEliminar)
                                        {
                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.Where(x => x.idSolicitudEquipamientoTecnologico == oDetalle.idSolicitudEquipamientoTecnologico && x.item == oDetalle.item).ToList();
                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                    oItem = resultadoCoincidencia.Single();
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.DeleteOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Registrar listado de sedes que aplica para la solicitud                                
                                    if (listadoSedesEnSolicitudRegistro != null)
                                    {
                                        if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.Where(x => x.idSolicitudEquipamientoTecnologico == itemSede.idSolicitudEquipamientoTecnologico && x.item == itemSede.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                                                        oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                        oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                        oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                        oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                        oSede = resultadoCoincidencia.Single();
                                                        //oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                        oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                        oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                        oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Hardware que aplica para la solicitud             

                                    List<string> codigoDetalle = new List<string>();
                                    List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> resultadoRegister = new List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware>();

                                    codigoDetalle = (from itemEliminado in listadoHardwareAEliminar
                                                     group itemEliminado by new { itemEliminado.idSolicitudEquipamientoTecnologico, itemEliminado.item } into j
                                                     select
                                                       j.Key.idSolicitudEquipamientoTecnologico.ToString().Trim() + j.Key.item.Trim()
                                                     ).ToList();

                                    resultadoRegister = (from itemsRegister in listadoHardwareARegistrar
                                                         where !(codigoDetalle.Contains(itemsRegister.idSolicitudEquipamientoTecnologico.ToString().Trim() + item.ToString()))
                                                         select itemsRegister
                                                   ).ToList();

                                    if (resultadoRegister != null)
                                    {
                                        if (resultadoRegister.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemHardware in resultadoRegister.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == itemHardware.idSolicitudEquipamientoTecnologico && x.item == itemHardware.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                        oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                        //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                        oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;


                                                        oItem.GeneraSolicitud = itemHardware.GeneraSolicitud != (decimal?)null ? itemHardware.GeneraSolicitud.Value : (decimal?)null;
                                                        oItem.idReferenciaSoporteTecnico = itemHardware.idReferenciaSoporteTecnico != (int?)null ? itemHardware.idReferenciaSoporteTecnico.Value : (int?)null;
                                                        oItem.RequiereCapacitacion = itemHardware.RequiereCapacitacion != (decimal?)null ? itemHardware.RequiereCapacitacion.Value : (decimal?)null;

                                                        oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                        oItem = resultadoCoincidencia.Single();
                                                        //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                        //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                        oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;


                                                        oItem.GeneraSolicitud = itemHardware.GeneraSolicitud != (decimal?)null ? itemHardware.GeneraSolicitud.Value : (decimal?)null;
                                                        oItem.idReferenciaSoporteTecnico = itemHardware.idReferenciaSoporteTecnico != (int?)null ? itemHardware.idReferenciaSoporteTecnico.Value : (int?)null;
                                                        oItem.RequiereCapacitacion = itemHardware.RequiereCapacitacion != (decimal?)null ? itemHardware.RequiereCapacitacion.Value : (decimal?)null;

                                                        oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;
                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de Software que aplica para la solicitud                                
                                    if (listadoSoftwareARegistrar != null)
                                    {
                                        if (listadoSoftwareARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.Where(x => x.idSolicitudEquipamientoTecnologico == itemSoftware.idSolicitudEquipamientoTecnologico && x.item == itemSoftware.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                        oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                        oItem.elegido = itemSoftware.elegido != (Int32?)null ? itemSoftware.elegido.Value : 0;

                                                        oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (Int32?)null ? itemSoftware.GeneraSolicitud.Value : 0;
                                                        oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (Int32?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 0;
                                                        oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (Int32?)null ? itemSoftware.RequiereCapacitacion.Value : 0;

                                                        oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                        oItem = resultadoCoincidencia.Single();
                                                        //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                        //oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                        oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                        oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                        oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (Int32?)null ? itemSoftware.perfilDeAcceso.Value : 1;

                                                        oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (Int32?)null ? itemSoftware.GeneraSolicitud.Value : 0;
                                                        oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (Int32?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 0;
                                                        oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (Int32?)null ? itemSoftware.RequiereCapacitacion.Value : 0;

                                                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Registrar listado de linea celular que aplica para la solicitud                                
                                    if (listadoLineaCelularARegistrar != null)
                                    {
                                        if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                        {
                                            foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                            {

                                                var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.Where(x => x.idSolicitudEquipamientoTecnologico == itemCelular.idSolicitudEquipamientoTecnologico && x.item == itemCelular.item).ToList();

                                                if (resultadoCoincidencia != null)
                                                {
                                                    if (resultadoCoincidencia.ToList().Count == 0)
                                                    {
                                                        #region Nuevo detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                        oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                                        oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                                        oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                                        oItem.idReferencia = itemCelular.idReferencia != (int?)null ? itemCelular.idReferencia.Value : (int?)null;
                                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                    else if (resultadoCoincidencia.ToList().Count == 1)
                                                    {
                                                        #region Editar detalle de la lista sedes ()
                                                        SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                                        oItem = resultadoCoincidencia.Single();
                                                        oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                        oItem.estado = 1;
                                                        oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                                        oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                                        oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                                        oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                                        oItem.idReferencia = itemCelular.idReferencia != (int?)null ? itemCelular.idReferencia.Value : (int?)null;
                                                        Modelo.SubmitChanges();
                                                        #endregion
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #endregion
                                }


                                #endregion
                            }
                            #endregion
                        }
                    }
                    Scope.Complete();
                }
            }

            return tipoResultadoOperacion;
        }


        public int ToRegisterLocal(string conection, SAS_SolicitudDeEquipamientoTecnologico item, List<SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo> listadoSedesEnSolicitudRegistro, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware> listadoHardwareARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware> listadoSoftwareARegistrar, List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelular> listadoLineaCelularARegistrar)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            int codigo = 0; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                //using (TransactionScope Scope = new TransactionScope())
                //{

                if (item.id == 0)
                {
                    #region Nuevo() 
                    oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                    //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                    //oRegistro.id = obtenerultimoregistro;
                    oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                    oRegistro.nombresCompletos = item.nombresCompletos;
                    oRegistro.esExterno = item.esExterno;
                    oRegistro.fecha = item.fecha;
                    oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                    oRegistro.esTemporal = item.esTemporal;
                    oRegistro.justificacion = item.justificacion;
                    oRegistro.estadoCodigo = item.estadoCodigo;
                    oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                    oRegistro.tipoSolicitud = item.tipoSolicitud;
                    oRegistro.vencimientoContrato = item.vencimientoContrato;
                    oRegistro.itemInicioContrato = item.itemInicioContrato;
                    oRegistro.tipoContrato = item.tipoContrato;
                    oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                    oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                    oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                    oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                    oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                    oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;

                    Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                    Modelo.SubmitChanges();
                    tipoResultadoOperacion = oRegistro.id; // registrar
                    codigo = oRegistro.id;
                    #endregion

                    #region Registrar listado de sedes que aplica para la solicitud  
                    if (listadoSedesEnSolicitudRegistro != null)
                    {
                        if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                        {
                            foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                            {
                                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                oSede.estado = itemSede.estado;
                                oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                Modelo.SubmitChanges();
                            }
                        }
                    }
                    #endregion

                    #region Registrar listado de Hardware()
                    if (listadoHardwareARegistrar != null)
                    {
                        if (listadoHardwareARegistrar.ToList().Count > 0)
                        {
                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                            {
                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.estado = 1;
                                oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;

                                oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;


                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                Modelo.SubmitChanges();
                            }
                        }
                    }
                    #endregion

                    #region Registrar listado de Software()
                    if (listadoSoftwareARegistrar != null)
                    {
                        if (listadoSoftwareARegistrar.ToList().Count > 0)
                        {
                            foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                            {
                                SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.estado = 1;
                                oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;


                                oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;

                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                Modelo.SubmitChanges();
                            }
                        }
                    }
                    #endregion


                    #region Registrar listado de Lineas celulares()
                    if (listadoLineaCelularARegistrar != null)
                    {
                        if (listadoLineaCelularARegistrar.ToList().Count > 0)
                        {
                            foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                            {
                                SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                oItem.idSolicitudEquipamientoTecnologico = codigo;
                                oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                oItem.estado = 1;
                                oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(oItem);
                                Modelo.SubmitChanges();
                            }
                        }
                    }
                    #endregion

                }
                else
                {
                    var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualización()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                            //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                            //oRegistro.id = obtenerultimoregistro;
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.nombresCompletos = item.nombresCompletos;
                            oRegistro.esExterno = item.esExterno;
                            oRegistro.fecha = item.fecha;
                            oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                            oRegistro.esTemporal = item.esTemporal;
                            oRegistro.vencimientoContrato = item.vencimientoContrato;
                            oRegistro.itemInicioContrato = item.itemInicioContrato;
                            oRegistro.tipoContrato = item.tipoContrato;
                            oRegistro.justificacion = item.justificacion;
                            oRegistro.estadoCodigo = item.estadoCodigo;
                            oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                            oRegistro.tipoSolicitud = item.tipoSolicitud;
                            oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                            oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                            oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                            oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                            oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                            oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;




                            Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = oRegistro.id; // registrar
                            codigo = oRegistro.id;

                            #region Registrar listado de sedes que aplica para la solicitud  
                            if (listadoSedesEnSolicitudRegistro != null)
                            {
                                if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                {
                                    foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                    {
                                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                        oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                        oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                        oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                        oSede.estado = 1;
                                        oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Registrar listado de Hardware()
                            if (listadoHardwareARegistrar != null)
                            {
                                if (listadoHardwareARegistrar.ToList().Count > 0)
                                {
                                    foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                    {
                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                        oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                        oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.estado = 1;
                                        oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                        oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                        oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                        oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;
                                        oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;

                                        oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                        oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                        oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;






                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Registrar listado de Software()
                            if (listadoSoftwareARegistrar != null)
                            {
                                if (listadoSoftwareARegistrar.ToList().Count > 0)
                                {
                                    foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                    {
                                        SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                        oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                        oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.estado = 1;
                                        oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                        oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                        oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                        oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                        oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;

                                        oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                        oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                        oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;


                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Registrar listado de Lineas celulares()
                            if (listadoLineaCelularARegistrar != null)
                            {
                                if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                {
                                    foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                    {
                                        SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                        oItem.idSolicitudEquipamientoTecnologico = codigo;
                                        oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                        oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                        oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                        oItem.estado = 1;
                                        oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                        oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                        oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                        oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion


                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 


                            if (item.id == 0)
                            {
                                #region Nuevo()
                                tipoResultadoOperacion = 0; // Nuevo() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                // oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                                oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                                oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                                oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                                oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                                oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;


                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;

                                #region Registrar listado de sedes que aplica para la solicitud  
                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                            oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                            oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                            oSede.estado = 1;
                                            oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Hardware()
                                if (listadoHardwareARegistrar != null)
                                {
                                    if (listadoHardwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemSede in listadoHardwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoHardware = itemSede.idDispositivoTipoHardware != null ? itemSede.idDispositivoTipoHardware.Trim() : string.Empty;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSede.valor != (decimal?)null ? itemSede.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSede.glosa != null ? itemSede.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSede.actualizado != (decimal?)null ? itemSede.actualizado.Value : 0;
                                            //oItem.elegido = itemSede.elegido != (decimal?)null ? itemSede.elegido.Value : 0;
                                            oItem.elegido = itemSede.elegido != (decimal?)null ? (itemSede.elegido.Value.ToString() != string.Empty ? itemSede.elegido.Value : 0) : 0;

                                            oItem.codigoERP = itemSede.codigoERP != (int?)null ? itemSede.codigoERP.Value : (int?)null;

                                            oItem.GeneraSolicitud = itemSede.GeneraSolicitud != (decimal?)null ? itemSede.GeneraSolicitud.Value : (decimal?)null;
                                            oItem.idReferenciaSoporteTecnico = itemSede.idReferenciaSoporteTecnico != (int?)null ? itemSede.idReferenciaSoporteTecnico.Value : (int?)null;
                                            oItem.RequiereCapacitacion = itemSede.RequiereCapacitacion != (decimal?)null ? itemSede.RequiereCapacitacion.Value : (decimal?)null;




                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Software()
                                if (listadoSoftwareARegistrar != null)
                                {
                                    if (listadoSoftwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                            oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                            oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                            oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                            oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;


                                            oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                            oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                            oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;


                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion



                                #region Registrar listado de Lineas celulares()
                                if (listadoLineaCelularARegistrar != null)
                                {
                                    if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                        {
                                            SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                            oItem.idSolicitudEquipamientoTecnologico = codigo;
                                            oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                            oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                            oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                            oItem.estado = 1;
                                            oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                            oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                            oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                            oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                            Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                                #endregion


                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                oRegistro = resultado.Single();
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = item.esExterno;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaDeVencimiento = item.fechaDeVencimiento;
                                oRegistro.esTemporal = item.esTemporal;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.tipoSolicitud = item.tipoSolicitud;
                                oRegistro.vencimientoContrato = item.vencimientoContrato;
                                oRegistro.itemInicioContrato = item.itemInicioContrato;
                                oRegistro.tipoContrato = item.tipoContrato;
                                oRegistro.glosa = item.glosa != null ? item.glosa.Trim() : string.Empty;
                                oRegistro.idReferencia = item.idReferencia != (Int32?)null ? item.idReferencia : (Int32?)null;
                                oRegistro.Cerrado = item.Cerrado != (Int32?)null ? item.Cerrado : (Int32?)null;
                                oRegistro.FechaCierre = item.FechaCierre != (DateTime?)null ? item.FechaCierre : (DateTime?)null;
                                oRegistro.estadoCodigoAnterior = item.estadoCodigoAnterior != null ? item.estadoCodigoAnterior.Trim() : string.Empty;
                                oRegistro.idReferenciaSolicitudRenovacion = item.idReferenciaSolicitudRenovacion != (Int32?)null ? item.idReferenciaSolicitudRenovacion : (Int32?)null;

                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // modificar
                                codigo = oRegistro.id;

                                #region Registrar listado de sedes que aplica para la solicitud                                
                                if (listadoSedesEnSolicitudRegistro != null)
                                {
                                    if (listadoSedesEnSolicitudRegistro.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo itemSede in listadoSedesEnSolicitudRegistro.ToList())
                                        {

                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.Where(x => x.idSolicitudEquipamientoTecnologico == itemSede.idSolicitudEquipamientoTecnologico && x.item == itemSede.item).ToList();

                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                    oSede.idSolicitudEquipamientoTecnologico = codigo;
                                                    oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                    oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    #region Editar detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                                    oSede = resultadoCoincidencia.Single();
                                                    //oSede.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                    //oSede.item = itemSede.item != null ? itemSede.item.Trim() : string.Empty;
                                                    oSede.sedeDeTrabajoCodigo = itemSede.sedeDeTrabajoCodigo != null ? itemSede.sedeDeTrabajoCodigo.Trim() : string.Empty;
                                                    oSede.estado = itemSede.estado != null ? itemSede.estado.Value : 0;
                                                    oSede.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oSede.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    //Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Hardware que aplica para la solicitud                                
                                if (listadoHardwareARegistrar != null)
                                {
                                    if (listadoHardwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware itemHardware in listadoHardwareARegistrar.ToList())
                                        {

                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.Where(x => x.idSolicitudEquipamientoTecnologico == itemHardware.idSolicitudEquipamientoTecnologico && x.item == itemHardware.item).ToList();

                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                    oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                    oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                    //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                    oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;


                                                    oItem.GeneraSolicitud = itemHardware.GeneraSolicitud != (decimal?)null ? itemHardware.GeneraSolicitud.Value : (decimal?)null;
                                                    oItem.idReferenciaSoporteTecnico = itemHardware.idReferenciaSoporteTecnico != (int?)null ? itemHardware.idReferenciaSoporteTecnico.Value : (int?)null;
                                                    oItem.RequiereCapacitacion = itemHardware.RequiereCapacitacion != (decimal?)null ? itemHardware.RequiereCapacitacion.Value : (decimal?)null;



                                                    oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    #region Editar detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                                    oItem = resultadoCoincidencia.Single();
                                                    //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                    //oItem.item = itemHardware.item != null ? itemHardware.item.Trim() : string.Empty;
                                                    oItem.idDispositivoTipoHardware = itemHardware.idDispositivoTipoHardware != null ? itemHardware.idDispositivoTipoHardware.Trim() : string.Empty;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemHardware.valor != (decimal?)null ? itemHardware.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemHardware.glosa != null ? itemHardware.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemHardware.actualizado != (decimal?)null ? itemHardware.actualizado.Value : 0;
                                                    //oItem.elegido = itemHardware.elegido != (decimal?)null ? itemHardware.elegido.Value : 0;
                                                    oItem.elegido = itemHardware.elegido != (decimal?)null ? (itemHardware.elegido.Value.ToString() != string.Empty ? itemHardware.elegido.Value : 0) : 0;

                                                    oItem.codigoERP = itemHardware.codigoERP != (int?)null ? itemHardware.codigoERP.Value : (int?)null;



                                                    oItem.GeneraSolicitud = itemHardware.GeneraSolicitud != (decimal?)null ? itemHardware.GeneraSolicitud.Value : (decimal?)null;
                                                    oItem.idReferenciaSoporteTecnico = itemHardware.idReferenciaSoporteTecnico != (int?)null ? itemHardware.idReferenciaSoporteTecnico.Value : (int?)null;
                                                    oItem.RequiereCapacitacion = itemHardware.RequiereCapacitacion != (decimal?)null ? itemHardware.RequiereCapacitacion.Value : (decimal?)null;

                                                    //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Registrar listado de Hardware que aplica para la solicitud                                
                                if (listadoSoftwareARegistrar != null)
                                {
                                    if (listadoSoftwareARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware itemSoftware in listadoSoftwareARegistrar.ToList())
                                        {

                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.Where(x => x.idSolicitudEquipamientoTecnologico == itemSoftware.idSolicitudEquipamientoTecnologico && x.item == itemSoftware.item).ToList();

                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                    oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                    oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                    oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                    oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;


                                                    oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                                    oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                                    oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;

                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    #region Editar detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware oItem = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware();
                                                    oItem = resultadoCoincidencia.Single();
                                                    //oItem.idSolicitudEquipamientoTecnologico = oRegistro.id;
                                                    //oItem.item = itemSoftware.item != null ? itemSoftware.item.Trim() : string.Empty;
                                                    oItem.idDispositivoTipoSoftware = itemSoftware.idDispositivoTipoSoftware != null ? itemSoftware.idDispositivoTipoSoftware : 0;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fecha.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemSoftware.valor != (decimal?)null ? itemSoftware.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemSoftware.glosa != null ? itemSoftware.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemSoftware.actualizado != (decimal?)null ? itemSoftware.actualizado.Value : 0;
                                                    oItem.elegido = itemSoftware.elegido != (decimal?)null ? itemSoftware.elegido.Value : 0;
                                                    oItem.perfilDeAcceso = itemSoftware.perfilDeAcceso != (decimal?)null ? itemSoftware.perfilDeAcceso.Value : 1;

                                                    oItem.GeneraSolicitud = itemSoftware.GeneraSolicitud != (decimal?)null ? itemSoftware.GeneraSolicitud.Value : 1;
                                                    oItem.idReferenciaSoporteFuncional = itemSoftware.idReferenciaSoporteFuncional != (decimal?)null ? itemSoftware.idReferenciaSoporteFuncional.Value : 1;
                                                    oItem.RequiereCapacitacion = itemSoftware.RequiereCapacitacion != (decimal?)null ? itemSoftware.RequiereCapacitacion.Value : 1;


                                                    //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeSoftware.InsertOnSubmit(oItem);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion


                                #region Registrar listado de linea celular que aplica para la solicitud                                
                                if (listadoLineaCelularARegistrar != null)
                                {
                                    if (listadoLineaCelularARegistrar.ToList().Count > 0)
                                    {
                                        foreach (SAS_SolicitudDeEquipamientoTecnologicoLineaCelular itemCelular in listadoLineaCelularARegistrar.ToList())
                                        {

                                            var resultadoCoincidencia = Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.Where(x => x.idSolicitudEquipamientoTecnologico == itemCelular.idSolicitudEquipamientoTecnologico && x.item == itemCelular.item).ToList();

                                            if (resultadoCoincidencia != null)
                                            {
                                                if (resultadoCoincidencia.ToList().Count == 0)
                                                {
                                                    #region Nuevo detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                                    oItem.idSolicitudEquipamientoTecnologico = codigo;
                                                    oItem.item = itemCelular.item != null ? itemCelular.item.Trim() : string.Empty;
                                                    oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                                    oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                                    Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelular.InsertOnSubmit(itemCelular);
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                                else if (resultadoCoincidencia.ToList().Count == 1)
                                                {
                                                    #region Editar detalle de la lista sedes ()
                                                    SAS_SolicitudDeEquipamientoTecnologicoLineaCelular oItem = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelular();
                                                    oItem = resultadoCoincidencia.Single();
                                                    oItem.idLinea = itemCelular.idLinea != null ? itemCelular.idLinea : 0;
                                                    oItem.desde = oRegistro.fecha != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.hasta = oRegistro.fechaDeVencimiento != (DateTime?)null ? oRegistro.fechaDeVencimiento.Value : (DateTime?)null;
                                                    oItem.estado = 1;
                                                    oItem.valor = itemCelular.valor != (decimal?)null ? itemCelular.valor.Value : (decimal?)null;
                                                    oItem.glosa = itemCelular.glosa != null ? itemCelular.glosa.Trim() : string.Empty;
                                                    oItem.actualizado = itemCelular.actualizado != (decimal?)null ? itemCelular.actualizado.Value : 0;
                                                    oItem.elegido = itemCelular.elegido != (decimal?)null ? itemCelular.elegido.Value : 0;
                                                    Modelo.SubmitChanges();
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #endregion
                            }


                            #endregion
                        }
                        #endregion
                    }
                }
                //    Scope.Complete();
                //}
            }

            return codigo;
        }

        public void Notify(string conection, string Para, string Asunto, int codigoSolicitudSelecionada)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult> listadoCabecera = new List<SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult>();
            List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult> listadoDetalleHardware = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
            List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult> listadoDetalleSoftware = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();
            List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> listadoDetalleCelular = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();



            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listadoCabecera = Modelo.SAS_SolicitudDeEquipamientoTecnologicoListadoById(codigoSolicitudSelecionada).ToList();
                listadoDetalleHardware = Modelo.SAS_SolicitudDeEquipamientoTecnologicoHardwareById(codigoSolicitudSelecionada).ToList();
                listadoDetalleSoftware = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSoftwareById(codigoSolicitudSelecionada).ToList();
                listadoDetalleCelular = Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelularById(codigoSolicitudSelecionada).ToList();
            }

            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica solicitud de equipamiento.\n"));
                Mensaje.Append(string.Format("Solicitud de : " + listadoCabecera.ElementAt(0).tipoSolicitud + " | Generada con Número: " + listadoCabecera.ElementAt(0).documento + "\n"));
                Mensaje.Append(string.Format("Colaborador : " + listadoCabecera.ElementAt(0).idCodigoGeneral + " - " + listadoCabecera.ElementAt(0).nombresCompletos + "\n"));
                Mensaje.Append(string.Format("Justificación : " + listadoCabecera.ElementAt(0).justificacion + "\n"));
                Mensaje.Append(string.Format("Fecha de solicitud {0:dd/MM/yyyy} \n\n", listadoCabecera.ElementAt(0).fecha));
                Mensaje.Append(string.Format("Atendido Por : " + listadoCabecera.ElementAt(0).usuarioEnAtencion + "\n"));
                Mensaje.Append(string.Format("Estado de la solicitud : " + listadoCabecera.ElementAt(0).estado + "\n"));

                Mensaje.Append(string.Format("Cantidad de actividades programadas hardware : " + listadoDetalleHardware.Where(x => x.elegido == 1).ToList().Count.ToString() + "\n"));
                Mensaje.Append(string.Format("Cantidad de actividades programadas software : " + listadoDetalleSoftware.Where(x => x.elegido == 1).ToList().Count.ToString() + "\n"));
                Mensaje.Append(string.Format("Cantidad de actividades programadas Línea Celular : " + listadoDetalleCelular.Where(x => x.elegido == 1).ToList().Count.ToString() + "\n\n\n"));

                Mensaje.Append(string.Format("Detalle de las acciones programadas Hardware" + "\n"));
                if (listadoDetalleHardware.Where(x => x.elegido == 1).ToList() != null && listadoDetalleHardware.Where(x => x.elegido == 1).ToList().Count > 0)
                {
                    foreach (var item in listadoDetalleHardware.Where(x => x.elegido == 1).ToList())
                    {
                        Mensaje.Append(string.Format("Hardware : " + item.hardware + " | Dispositivo : " + item.codigoERP + " - " + item.dispositivo + "\n"));
                    }
                }

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Detalle de las acciones programadas Software" + "\n"));
                if (listadoDetalleSoftware.Where(x => x.elegido == 1).ToList() != null && listadoDetalleSoftware.Where(x => x.elegido == 1).ToList().Count > 0)
                {
                    foreach (var item in listadoDetalleSoftware.Where(x => x.elegido == 1).ToList())
                    {
                        Mensaje.Append(string.Format("Software : " + item.Software + " | perfil : " + item.perfilDeAcceso + " - " + item.glosa.Trim() + "\n"));
                    }
                }

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Detalle de las acciones programadas Software" + "\n"));
                if (listadoDetalleCelular.Where(x => x.elegido == 1).ToList() != null && listadoDetalleCelular.Where(x => x.elegido == 1).ToList().Count > 0)
                {
                    foreach (var item in listadoDetalleCelular.Where(x => x.elegido == 1).ToList())
                    {
                        Mensaje.Append(string.Format("Linea : " + item.valor + "\n"));
                    }
                }

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2022 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));

                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mail.From = new MailAddress(usuarioCorreo, "Notificaciones - ERP | Sociedad Agrícola Saturno SA");
                mail.To.Add(Para);
                mail.Subject = Asunto + " | " + listadoCabecera.ElementAt(0).tipoSolicitud + " | " + listadoCabecera.ElementAt(0).documento;
                mail.Body = Mensaje.ToString();

                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(usuarioCorreo, passwordCorreo);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                //Error = "Éxito";
                //MessageBox.Show(Error, "Mensaje del sistema");


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString().Trim();
                //MessageBox.Show(Error, "ADVERTANCIA DEL SISTEMA");                
            }
            #endregion
        }

        // FLUJO DE APROBACION
        public string ToRegisterBySolicitudRenovacion(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular oSolicitud, string motivoSolicitud, SAS_USUARIOS user, string idEstadoAActualizarEnSolicitudDeRenovacionLineaCelular)
        {
            string resultadoProceso = string.Empty;
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            int codigo = 0; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeEquipamientoTecnologico oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
            SAS_MotivoEquipamientoTecnologicoALL motivoSolicitudEquipamiento = new SAS_MotivoEquipamientoTecnologicoALL();
            SAS_DispositivoTipoDispositivo tipoDeDispositivo = new SAS_DispositivoTipoDispositivo();
            SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                var scopeOptions = new TransactionOptions();
                scopeOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                scopeOptions.Timeout = TimeSpan.MaxValue;

                using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Required, scopeOptions))
                {
                    if (motivoSolicitud.ToUpper().Trim() == "RENOVACION" || motivoSolicitud.ToUpper().Trim() == "RENOVACIÓN")
                    {
                        #region  Renovación() 
                        // Con la renovación cuando esta en estado de solicitud, se generan dos documentos, el de solicitud de equipamiento y la solicitud de baja
                        // cuando entra en cotización se debe generar una solicitud de alta.

                        #region Registro solicitud de Renovación para Equipamiento()
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = new List<SAS_MotivoEquipamientoTecnologicoALL>();
                        listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "RENOVACION" || x.descripcion.Trim().ToUpper() == "RENOVACIÓN").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }
                        #endregion

                        if (oSolicitud != null)
                        {
                            if (oSolicitud.idReferenciaBaja == null)
                            {
                                #region Baja() 
                                #region Registro Solicitud de Equipamiento() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                                //oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                                oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                                oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                oRegistro.esTemporal = 0;
                                oRegistro.vencimientoContrato = (DateTime?)null;
                                oRegistro.itemInicioContrato = string.Empty;
                                oRegistro.tipoContrato = (decimal?)null;
                                oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                                oRegistro.estadoCodigo = "AP";
                                oRegistro.usuarioEnAtencion = user.IdUsuario;
                                oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;
                                #endregion

                                #region Registro detalle de Sede en solicitud de Equipamiento()
                                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                oSede.idSolicitudEquipamientoTecnologico = codigo;
                                oSede.item = "001";
                                oSede.sedeDeTrabajoCodigo = "003";
                                oSede.estado = 1;
                                oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                Modelo.SubmitChanges();

                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                                var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Trim().ToUpper() == "CELULAR").ToList();
                                if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "001";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = "Dispositivo para baja | " + oSolicitud.idDispositivoBaja.ToString().PadLeft(7, '0');
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();
                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Linea celular() 
                                var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Trim().ToUpper() == ("CHIP")).ToList();
                                if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "002";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = oSolicitud.numeroCelular;
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = (int?)null;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();

                                #endregion


                                resultadoProceso += "\nSolicitud de equipamiento generada con número " + codigo.ToString().PadLeft(7, '0');

                                SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                                solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                                solicitudRenovacion.idReferencia = codigo;
                                Modelo.SubmitChanges();

                                #region Registro de Baja y/o Devolución como Referencia() 
                                listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                                if (listadoTipoSolicitudesEquipamiento != null)
                                {
                                    if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                                    {
                                        var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                        if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                        {
                                            motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                        }
                                    }
                                }

                                #region Registro Solicitud de Equipamiento() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                                //oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                                oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                                oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                oRegistro.esTemporal = 0;
                                oRegistro.vencimientoContrato = (DateTime?)null;
                                oRegistro.itemInicioContrato = string.Empty;
                                oRegistro.tipoContrato = (decimal?)null;
                                oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                                oRegistro.estadoCodigo = "AP";
                                oRegistro.usuarioEnAtencion = user.IdUsuario;
                                oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;
                                #endregion

                                #region Registro detalle de Sede en solicitud de Equipamiento()
                                oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                oSede.idSolicitudEquipamientoTecnologico = codigo;
                                oSede.item = "001";
                                oSede.sedeDeTrabajoCodigo = "003";
                                oSede.estado = 1;
                                oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                                Modelo.SubmitChanges();

                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                                listadoTipoDispositivo = new List<SAS_DispositivoTipoDispositivo>();
                                listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                                if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "001";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();
                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                                listadoTipoDispositivoChip = new List<SAS_DispositivoTipoDispositivo>();
                                listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                                if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "002";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = oSolicitud.numeroCelular;
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = (int?)null;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();

                                #endregion

                                #endregion
                                resultadoProceso += "'\n Solicitud de baja y/o devolución generada con número " + codigo.ToString().PadLeft(7, '0');
                                solicitudRenovacion.idReferenciaBaja = codigo;
                                Modelo.SubmitChanges();
                                #endregion

                            }
                        }


                        if (oSolicitud != null)
                        {
                            if (oSolicitud.idReferenciaAlta == null)
                            {

                                #region Alta y/o Entrega() 
                                List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamientoAlta = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                                if (listadoTipoSolicitudesEquipamientoAlta != null)
                                {
                                    if (listadoTipoSolicitudesEquipamientoAlta.ToList().Count >= 0)
                                    {
                                        var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamientoAlta.Where(x => x.descripcion.Trim().ToUpper() == "ALTA").ToList();
                                        if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                        {
                                            motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                        }
                                    }
                                }

                                #region Registro Solicitud de Equipamiento() 
                                oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                                //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                //oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                                //oRegistro.nombresCompletos = item.nombresCompletos;
                                oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                                oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                                oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                oRegistro.esTemporal = 0;
                                oRegistro.vencimientoContrato = (DateTime?)null;
                                oRegistro.itemInicioContrato = string.Empty;
                                oRegistro.tipoContrato = (decimal?)null;
                                oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                                oRegistro.estadoCodigo = "AP";
                                oRegistro.usuarioEnAtencion = user.IdUsuario;
                                oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                codigo = oRegistro.id;
                                #endregion

                                #region Registro detalle de Sede en solicitud de Equipamiento()
                                SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSedeAlta = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                                oSedeAlta.idSolicitudEquipamientoTecnologico = codigo;
                                oSedeAlta.item = "001";
                                oSedeAlta.sedeDeTrabajoCodigo = "003";
                                oSedeAlta.estado = 1;
                                oSedeAlta.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oSedeAlta.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSedeAlta);
                                Modelo.SubmitChanges();

                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                                var listadoTipoDispositivoAlta = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                                if (listadoTipoDispositivoAlta != null && listadoTipoDispositivoAlta.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivoAlta.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "001";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = "Dispositivo para Alta y/o Entrega";
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();
                                #endregion

                                #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                                var listadoTipoDispositivoChipAlta = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                                if (listadoTipoDispositivoChipAlta != null && listadoTipoDispositivoChipAlta.ToList().Count > 0)
                                {
                                    tipoDeDispositivo = listadoTipoDispositivoChipAlta.ElementAt(0);
                                }

                                oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                                oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                                oItemHardware.item = "002";
                                oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                                oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                                oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                                oItemHardware.estado = 1;
                                oItemHardware.valor = (decimal?)null;
                                oItemHardware.glosa = oSolicitud.numeroCelular;
                                oItemHardware.actualizado = 0;
                                oItemHardware.elegido = 1;
                                oItemHardware.codigoERP = (int?)null;
                                Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                                Modelo.SubmitChanges();

                                #endregion

                                #endregion
                                resultadoProceso += " \nSolicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                                SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacionAlta = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                                solicitudRenovacionAlta = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                                //solicitudRenovacionAlta.idReferencia = codigo;
                                solicitudRenovacionAlta.idReferenciaAlta = codigo;
                                Modelo.SubmitChanges();



                            }
                        }
                        #endregion

                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Avería".ToUpper())
                    {
                        #region
                        #region Baja y/o Devolución | Avería() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja por avería generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }


                    if (motivoSolicitud.ToUpper().Trim() == "Perdida".ToUpper())
                    {
                        #region
                        #region Baja y/o Devolución | Avería | Perdida() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja por perdida generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Robo".ToUpper())
                    {
                        #region                        
                        #region Baja y/o Devolución | Avería | Perdida | Robo () 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja por robo generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Suspención de Equipo".ToUpper())
                    {
                        #region                        
                        #region Suspención de Equipo() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "PRESTAMO").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja()
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Prestamos de equipos y/o dispositivo";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion


                        #endregion
                        resultadoProceso += " Solicitud de suspención generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "BAJA".ToUpper())
                    {
                        #region 
                        #region Baja y/o Devolución() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja y/o devolución generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "ALTA".ToUpper())
                    {
                        #region 
                        #region Alta y/o Entrega() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "ALTA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para Alta y/o Entrega";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion

                    }

                    if (motivoSolicitud.ToUpper().Trim() == "PRESTAMO".ToUpper() || motivoSolicitud.ToUpper().Trim() == "PRÉSTAMO".ToUpper())
                    {
                        #region
                        #region Prestamo() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "PRESTAMO").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Prestamos de equipos y/o dispositivo";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Suspención de línea".ToUpper())
                    {
                        #region
                        #region Suspención de línea()
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "PRESTAMO").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede()

                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registrar Linea()
                        listadoTipoDispositivoChip = new List<SAS_DispositivoTipoDispositivo>();
                        listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivoChip.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }
                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de suspención generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Suspención de equipo y línea".ToUpper())
                    {
                        #region
                        #region Suspención de equipo y línea() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "Suspención").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()

                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja()

                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Prestamos de equipos y/o dispositivo";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de suspención generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }


                    if (motivoSolicitud.ToUpper().Trim() == "Nueva asignacion".ToUpper() || motivoSolicitud.ToUpper().Trim() == "Nueva asignación".ToUpper())
                    {
                        #region
                        #region Alta | Nueva asignación () 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "Línea Nueva".ToUpper()).ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }



                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular  
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para alta y/o asignación";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion()

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        #region Actualizar referencias en solicitud de renovacion()
                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion


                        #region Actualizar Linea celular en catalogo de Líneas Celulares()
                        if (oSolicitud.numeroCelular != null)
                        {
                            if (oSolicitud.numeroCelular.ToString().Trim() != string.Empty)
                            {
                                var result = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.lineaCelular == oSolicitud.numeroCelular).ToList();
                                if (result != null)
                                {
                                    if (result.ToList().Count > 0)
                                    {
                                        SAS_LineasCelularesCoporativas oLineaCelular = new SAS_LineasCelularesCoporativas();
                                        oLineaCelular = result.ElementAt(0);
                                        oLineaCelular.idCodigoGeneral = oSolicitud.idCodigoGeneral;
                                        oLineaCelular.glosa += ("Asignado y/o reasignado el " + oSolicitud.fecha.ToShortDateString() + " con " + resultadoProceso + " por el usuario " + user.IdUsuario);
                                        Modelo.SubmitChanges();
                                    }
                                }

                            }
                        }
                        #endregion



                        #region Actualizar Linea celular en catalogo dispositivos()

                        if (oSolicitud.idDispositivoAlta != null)
                        {
                            if (oSolicitud.idDispositivoAlta.ToString().Trim() != string.Empty)
                            {
                                var result = Modelo.SAS_Dispostivo.Where(x => x.id == oSolicitud.idDispositivoAlta).ToList();
                                if (result != null)
                                {
                                    if (result.ToList().Count > 0)
                                    {
                                        SAS_Dispostivo oDispositivoAlta = new SAS_Dispostivo();
                                        oDispositivoAlta = Modelo.SAS_Dispostivo.Where(x => x.id == oSolicitud.idDispositivoAlta).Single();
                                        oDispositivoAlta.lineaCelular = oSolicitud.numeroCelular;
                                        oDispositivoAlta.caracteristicas += ("Asignado y/o reasignado el " + oSolicitud.fecha.ToShortDateString() + " con " + resultadoProceso + " por el usuario " + user.IdUsuario);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }

                        }


                        #endregion

                        #endregion
                    }



                    //**
                    if (motivoSolicitud.ToUpper().Trim() == "Linea | Alta".ToUpper())
                    {
                        #region 
                        #region Alta y/o Entrega() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "ALTA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        //var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        //if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        //{
                        //    tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        //}

                        //oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        //oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        //oItemHardware.item = "001";
                        //oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        //oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        //oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        //oItemHardware.estado = 1;
                        //oItemHardware.valor = (decimal?)null;
                        //oItemHardware.glosa = "Dispositivo para Alta y/o Entrega";
                        //oItemHardware.actualizado = 0;
                        //oItemHardware.elegido = 1;
                        //oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        //Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivoChip.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion

                    }

                    //**
                    if (motivoSolicitud.ToUpper().Trim() == "Linea | Baja".ToUpper())
                    {
                        #region 
                        #region Baja y/o Devolución() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "BAJA").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        // var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        //if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        //{
                        //    tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        //}

                        //oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        //oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        //oItemHardware.item = "001";
                        //oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        //oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        //oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        //oItemHardware.estado = 1;
                        //oItemHardware.valor = (decimal?)null;
                        //oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        //oItemHardware.actualizado = 0;
                        //oItemHardware.elegido = 1;
                        //oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        //Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivoChip.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja y/o devolución generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    if (motivoSolicitud.ToUpper().Trim() == "Devolución".ToUpper() || (motivoSolicitud.ToUpper().Trim() == "Devolucion".ToUpper()))
                    {
                        #region 
                        #region Baja y/o Devolución() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "Devolucion").ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Baja y/o Devolución() 
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Dispositivo para baja y/o devolución";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoBaja;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "002";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de baja y/o devolución generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaBaja = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    //**
                    if (motivoSolicitud.ToUpper().Trim() == "Prestamo | Equipo".ToUpper())
                    {
                        #region
                        #region Prestamo() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "Prestamo equipo".ToUpper()).ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                        var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = "Prestamos de equipos y/o dispositivo";
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        //var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        //if (listadoTipoDispositivoChip != null && listadoTipoDispositivo.ToList().Count > 0)
                        //{
                        //    tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        //}

                        //oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        //oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        //oItemHardware.item = "002";
                        //oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        //oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        //oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        //oItemHardware.estado = 1;
                        //oItemHardware.valor = (decimal?)null;
                        //oItemHardware.glosa = oSolicitud.numeroCelular;
                        //oItemHardware.actualizado = 0;
                        //oItemHardware.elegido = 1;
                        //oItemHardware.codigoERP = (int?)null;
                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        //Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion
                    }

                    //**
                    if (motivoSolicitud.ToUpper().Trim() == "Duplicado de Chip".ToUpper())
                    {
                        #region 
                        #region Alta y/o Entrega() 
                        List<SAS_MotivoEquipamientoTecnologicoALL> listadoTipoSolicitudesEquipamiento = Modelo.SAS_MotivoEquipamientoTecnologicoALL.Where(x => x.estado == 1).ToList();
                        if (listadoTipoSolicitudesEquipamiento != null)
                        {
                            if (listadoTipoSolicitudesEquipamiento.ToList().Count >= 0)
                            {
                                var listaSolicituTipoRenovacion = listadoTipoSolicitudesEquipamiento.Where(x => x.descripcion.Trim().ToUpper() == "Duplicado".ToUpper()).ToList();
                                if (listaSolicituTipoRenovacion.ToList().Count() > 0)
                                {
                                    motivoSolicitudEquipamiento = listaSolicituTipoRenovacion.ElementAt(0);
                                }
                            }
                        }

                        #region Registro Solicitud de Equipamiento() 
                        oRegistro = new SAS_SolicitudDeEquipamientoTecnologico();
                        //int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                        //oRegistro.id = obtenerultimoregistro;
                        oRegistro.idCodigoGeneral = oSolicitud.idCodigoGeneral != null ? oSolicitud.idCodigoGeneral.Trim() : string.Empty;
                        //oRegistro.nombresCompletos = item.nombresCompletos;
                        oRegistro.esExterno = (oSolicitud.idCodigoGeneral.ToString().Substring(1, 1) == "E" ? 1 : 0);
                        oRegistro.fecha = oSolicitud.fecha != null ? oSolicitud.fecha : DateTime.Now;
                        oRegistro.fechaDeVencimiento = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        oRegistro.esTemporal = 0;
                        oRegistro.vencimientoContrato = (DateTime?)null;
                        oRegistro.itemInicioContrato = string.Empty;
                        oRegistro.tipoContrato = (decimal?)null;
                        oRegistro.justificacion = motivoSolicitud.Trim() + " | Generado desde la solicitud REN-0001-" + oSolicitud.id.ToString().PadLeft(7, '0');
                        oRegistro.estadoCodigo = "AP";
                        oRegistro.usuarioEnAtencion = user.IdUsuario;
                        oRegistro.tipoSolicitud = motivoSolicitudEquipamiento.id;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologico.InsertOnSubmit(oRegistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = oRegistro.id; // registrar
                        codigo = oRegistro.id;
                        #endregion

                        #region Registro detalle de Sede en solicitud de Equipamiento()
                        SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo oSede = new SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo();
                        oSede.idSolicitudEquipamientoTecnologico = codigo;
                        oSede.item = "001";
                        oSede.sedeDeTrabajoCodigo = "003";
                        oSede.estado = 1;
                        oSede.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oSede.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(30) : DateTime.Now.AddDays(30);
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedeDeTrabajo.InsertOnSubmit(oSede);
                        Modelo.SubmitChanges();

                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Linea celular()
                        //var listadoTipoDispositivo = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CELULAR")).ToList();
                        //if (listadoTipoDispositivo != null && listadoTipoDispositivo.ToList().Count > 0)
                        //{
                        //    tipoDeDispositivo = listadoTipoDispositivo.ElementAt(0);
                        //}

                        //oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        //oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        //oItemHardware.item = "001";
                        //oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        //oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        //oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360); 
                        //oItemHardware.estado = 1;
                        //oItemHardware.valor = (decimal?)null;
                        //oItemHardware.glosa = "Dispositivo para Alta y/o Entrega";
                        //oItemHardware.actualizado = 0;
                        //oItemHardware.elegido = 1;
                        //oItemHardware.codigoERP = oSolicitud.idDispositivoAlta;
                        //Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        //Modelo.SubmitChanges();
                        #endregion

                        #region Registro detalle en pestaña de Hardware | Tipo: Equipo Celular | Alta y/o Prestamo()
                        var listadoTipoDispositivoChip = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.estado == 1 && x.descripcion.Contains("CHIP")).ToList();
                        if (listadoTipoDispositivoChip != null && listadoTipoDispositivoChip.ToList().Count > 0)
                        {
                            tipoDeDispositivo = listadoTipoDispositivoChip.ElementAt(0);
                        }

                        oItemHardware = new SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware();
                        oItemHardware.idSolicitudEquipamientoTecnologico = codigo;
                        oItemHardware.item = "001";
                        oItemHardware.idDispositivoTipoHardware = tipoDeDispositivo.id;
                        oItemHardware.desde = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(0) : DateTime.Now.AddDays(0);
                        oItemHardware.hasta = oSolicitud.fecha != null ? oSolicitud.fecha.AddDays(360) : DateTime.Now.AddDays(360);
                        oItemHardware.estado = 1;
                        oItemHardware.valor = (decimal?)null;
                        oItemHardware.glosa = oSolicitud.numeroCelular;
                        oItemHardware.actualizado = 0;
                        oItemHardware.elegido = 1;
                        oItemHardware.codigoERP = (int?)null;
                        Modelo.SAS_SolicitudDeEquipamientoTecnologicoTipoDeHardware.InsertOnSubmit(oItemHardware);
                        Modelo.SubmitChanges();

                        #endregion

                        #endregion
                        resultadoProceso += " Solicitud de alta y/o entrega generada con número " + codigo.ToString().PadLeft(7, '0');

                        SAS_SolicitudDeRenovacionTelefoniaCelular solicitudRenovacion = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                        solicitudRenovacion = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).Single();
                        solicitudRenovacion.idReferencia = codigo;
                        solicitudRenovacion.idReferenciaAlta = codigo;
                        Modelo.SubmitChanges();
                        #endregion

                    }




                    SAS_SolicitudDeRenovacionTelefoniaCelular oSolicitudAActualizar = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                    var SolicutudAActualizar = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == oSolicitud.id).ToList();
                    if (SolicutudAActualizar != null)
                    {
                        if (SolicutudAActualizar.ToList().Count > 0)
                        {
                            oSolicitudAActualizar = SolicutudAActualizar.ElementAt(0);
                            oSolicitudAActualizar.estadoCodigo = idEstadoAActualizarEnSolicitudDeRenovacionLineaCelular;
                            Modelo.SubmitChanges();
                        }
                    }

                    Scope.Complete();
                }
            }
            return resultadoProceso;
        }


        public int ChangeState(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            SAS_SolicitudDeEquipamientoTecnologico oregistro = new SAS_SolicitudDeEquipamientoTecnologico();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.estadoCodigo == "PE" || oregistro.estadoCodigo == "AN")
                        {
                            if (oregistro.estadoCodigo == "PE")
                            {
                                oregistro.estadoCodigo = "AN";
                                tipoResultadoOperacion = 2; // desactivar
                            }
                            else
                            {
                                oregistro.estadoCodigo = "PE";
                                tipoResultadoOperacion = 3; // Activar
                            }
                        }
                        Modelo.SubmitChanges();
                        #endregion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int ChangeStateDocument(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            SAS_SolicitudDeEquipamientoTecnologico oregistro = new SAS_SolicitudDeEquipamientoTecnologico();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        oregistro.estadoCodigo = item.estadoCodigo;
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = 4; // Flujo de aprobacion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int DeleteRecord(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            SAS_SolicitudDeEquipamientoTecnologico oregistro = new SAS_SolicitudDeEquipamientoTecnologico();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeEquipamientoTecnologico.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.estadoCodigo == "PE" || oregistro.estadoCodigo == "AN")
                        {
                            Modelo.SAS_SolicitudDeEquipamientoTecnologico.DeleteOnSubmit(oregistro);
                        }
                        Modelo.SubmitChanges();
                        #endregion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        // Obtener Consulta completo de una solicitud por Id
        public SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult ListRequestsById(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult oItem = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_SolicitudDeEquipamientoTecnologicoListadoById(item.id).ToList();
                if (result.ToList().Count == 0)
                {
                    oItem = new SAS_SolicitudDeEquipamientoTecnologicoListadoByIdResult();
                    oItem.id = item.id;
                    oItem.idCodigoGeneral = string.Empty;
                    oItem.nombresCompletos = string.Empty;
                    oItem.esExterno = 0;
                    oItem.fecha = DateTime.Now;
                    oItem.fechaDeVencimiento = (DateTime?)null;
                    oItem.esTemporal = 0;
                    oItem.justificacion = string.Empty;
                    oItem.estadoCodigo = "PE";
                    oItem.estado = "PENDIENTE";
                    oItem.usuarioEnAtencion = string.Empty;
                    oItem.estadoEnPlanilla = string.Empty;
                    oItem.planillaCodigo = string.Empty;
                    oItem.numeroDocumento = string.Empty;
                    oItem.planilla = string.Empty;
                    oItem.cargo = string.Empty;
                    oItem.documento = "SOL-0001-0000000";
                    oItem.idTipoSolicitud = 1;
                    oItem.tipoSolicitud = "Alta";
                    oItem.idGerencia = 0;
                    oItem.gerencia = string.Empty;
                    oItem.idArea = string.Empty;
                    oItem.area = string.Empty;
                    oItem.vencimientoContrato = (DateTime?)null;
                    oItem.itemInicioContrato = string.Empty;
                    oItem.fecha_Ingreso = (DateTime?)null;
                    oItem.tipoContrato = (decimal?)null;
                    oItem.tipoContratoDescripcion = string.Empty;

                }
                else if (result.ToList().Count == 1)
                {
                    oItem = result.Single();
                }
                else if (result.ToList().Count > 1)
                {
                    oItem = result.ElementAt(0);
                }


            }
            return oItem;
        }

        #region Cargar listado en blanco para la solicitus (sedes, hardware, Software) 
        // Generar Sedes en blanco con Id en blanco pasará como 0
        public List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult> ObtainABlankListOfTheDetailsOfTheVenues(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedesResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSedes(item.id).ToList();

            }
            return list.OrderBy(x => x.item).ToList();
        }

        // Generar Hardware en blanco con Id en blanco pasará como 0
        public List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult> GetHardwareDetailBlanklistingForRequest(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardwareResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoEnBlancoHardware(item.id).ToList();

            }
            return list.OrderBy(x => x.id).ToList();
        }

        // Obtener Software completo de una solicitud por Id
        public List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult> GetSoftwareDetailBlanklistingForRequest(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftwareResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoEnBlancoSoftware(item.id).ToList();

            }
            return list.OrderByDescending(x => x.id).ToList();
        }
        #endregion

        #region Obtener listado detalles para la solicitud (sedes, hardware, Software) 
        // Generar Sedes en blanco con Id en blanco pasará como 0
        public List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult> GetDetailedListOfVenuesByRequestId(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoSedesByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSedesById(item.id).ToList();

            }
            return list.OrderBy(x => x.item).ToList();
        }

        // Generar Hardware en blanco con Id en blanco pasará como 0
        public List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult> GetListOfHardwareDetailByRequestId(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoHardwareByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoHardwareById(item.id).ToList();

            }
            return list.OrderBy(x => x.item).ToList();
        }

        // Obtener Software completo de una solicitud por Id
        public List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult> GetListOfSoftwareDetailByRequestId(string conection, SAS_SolicitudDeEquipamientoTecnologico item)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoSoftwareByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoSoftwareById(item.id).ToList();

            }
            return list.OrderBy(x => x.item).ToList();
        }

        #endregion

        public List<DFormatoSimple> GetListOfProfiles(string conection)
        {
            //1.- Si esta activado el check de elegido se puede elegir un perfil

            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfInterfaces = new List<SAS_SegmentoRed>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Usuario" });
                listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Soporte" });
                listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Administrador" });
            }
            return listado;
        }

        public List<DFormatoSimple> ObtenerTipoDeSoftware(string conection)
        {
            //1.- Si esta activado el check de elegido se puede elegir un perfil

            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfInterfaces = new List<SAS_SegmentoRed>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Usuario" });
                listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Soporte" });
                listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Administrador" });
            }
            return listado;
        }


        

        public int ObtenerNumeroCorrelativoDeCero(string conection, SAS_SolicitudDeEquipamientoTecnologico solicitud)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int obtenerultimoregistro = 0;
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
            }

            return obtenerultimoregistro;
        }

        // listado de todas las solicitudes, con su respectivo detalle, esta función es es para reportes
        public List<DFormatoSimple> ListRequestAll(string conection)
        {
            List<DFormatoSimple> resultado = new List<DFormatoSimple>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado01 = Modelo.SAS_DispositivoTipoSoftware.Where(x => x.enFormatoSolicitud == 1).ToList();
                resultado = (from items in resultado01.ToList()
                             group items by new { items.id } into j
                             select new DFormatoSimple
                             {
                                 Codigo = j.Key.id.ToString(),
                                 Descripcion = j.FirstOrDefault().descripcion.Trim().ToUpper()
                             }).ToList();

            }
            return resultado;
        }

        // listado de todas las solicitudes entre dos periodo, con su respectivo detalle, esta función es es para reportes
        public List<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult> ListRequestAllByDate(string conection, string desde, string hasta)
        {
            List<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult> list = new List<SAS_SolicitudesDeEquipamientoTecnologicoByPeriodosResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudesDeEquipamientoTecnologicoByPeriodos(desde, hasta).ToList();
            }

            return list;
        }



        public List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> ListDetailRequestByCelLineByIdRequest(string conection, int id)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeEquipamientoTecnologicoLineaCelularById(id).ToList();
            }

            return list;
        }

        public List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> ListDetailRequestByCelLineByIdRequestBlank(string conection, int id)
        {
            List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult> list = new List<SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult>();

            SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult item = new SAS_SolicitudDeEquipamientoTecnologicoLineaCelularByIdResult();
            item.id = 0;
            item.item = "001";
            item.idLinea = 0;
            item.desde = DateTime.Now;
            item.hasta = DateTime.Now.AddYears(1);
            item.estado = 1;
            item.valor = string.Empty;
            item.glosa = string.Empty;
            item.actualizado = 1;
            item.elegido = 0;
            item.idreferencia = 0;
            item.documentoReferencia = string.Empty;
            list.Add(item);
            return list;
        }



    }
}
