using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using System.Net.Mail;

namespace Asistencia.Negocios
{
    public class SAS_SolicitudDeRenovacionTelefoniaCelularController
    {
        const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        const string passwordCorreo = @"iompqiiuhkjngkjr";
        // listar todos los registro de solicitudes de renovación.
        public List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoAll> ListRequests(string conection)
        {
            List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoAll> list = new List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoAll>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelularListadoAll.ToList();
            }
            return list.OrderByDescending(x => x.id).ToList();
        }

        // Obtener objeto desde un Id de la solicitud
        public SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult GetRequestsById(string conection, int id)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult item = new SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult();
            item.id = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var result = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelularListadoByID(id).ToList();

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

        // Obtener listado de solicitudes desde un rango de fecha
        public List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> ListRequestsByDate(string conection, string fechaDesde, string fechaHasta)
        {
            List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult> list = new List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDate(fechaDesde, fechaHasta).ToList();
            }
            return list.OrderByDescending(x => x.id).ToList();
        }

        // registrar
        public int ToRegister(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item, SAS_USUARIOS user, string nombreColaborador)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_SolicitudDeRenovacionTelefoniaCelular oRegistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int IdDispositivoAltaGenerado = 0;
            int IdDispositivoBajaGenerado = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            oRegistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                            int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;

                            #region Generar dispositivo de baja()
                            if (item.motivoCodigo == 1 && item.idDispositivoBaja == (int?)null)
                            {
                                SAS_Dispostivo dispositivoBaja = new SAS_Dispostivo();
                                dispositivoBaja.descripcion = "CELULAR ** DEVOLUCION";
                                dispositivoBaja.nombres = "CELULAR ** DEVOLUCION";
                                dispositivoBaja.estado = 1;
                                dispositivoBaja.EsPropio = 1;
                                dispositivoBaja.creadoPor = user.IdUsuario;
                                dispositivoBaja.esFinal = 1;
                                dispositivoBaja.fechacreacion = DateTime.Now;
                                dispositivoBaja.funcionamiento = 1;
                                dispositivoBaja.idMarca = "0000";
                                dispositivoBaja.idModelo = "0000";
                                dispositivoBaja.IdDispostivoColor = "000";
                                dispositivoBaja.caracteristicas = "- " + nombreColaborador;
                                dispositivoBaja.idArea = "010";
                                dispositivoBaja.sedeCodigo = "003";
                                dispositivoBaja.tipoDispositivoCodigo = "006";
                                dispositivoBaja.idSistemaDeImpresion = 13;
                                dispositivoBaja.lineaCelular = item.numeroCelular;
                                Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoBaja);
                                Modelo.SubmitChanges();

                                IdDispositivoBajaGenerado = dispositivoBaja.id;
                                item.idDispositivoBaja = dispositivoBaja.id;


                                SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoBajaGenerado;
                                dispositivoAsociadoAColaborador.item = "001";
                                dispositivoAsociadoAColaborador.estado = 1;
                                dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                dispositivoAsociadoAColaborador.tipo = '0';
                                dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                Modelo.SubmitChanges();
                            }
                            #endregion

                            #region Generar dispositivo de alta()                               
                            if (item.motivoCodigo == 1 && item.idDispositivoAlta == (int?)null)
                            {
                                SAS_Dispostivo dispositivoAlta = new SAS_Dispostivo();
                                dispositivoAlta.descripcion = "CELULAR ** GESTIONAR";
                                dispositivoAlta.nombres = "CELULAR ** GESTIONAR";
                                dispositivoAlta.estado = 1;
                                dispositivoAlta.EsPropio = 1;
                                dispositivoAlta.creadoPor = user.IdUsuario;
                                dispositivoAlta.esFinal = 1;
                                dispositivoAlta.fechacreacion = DateTime.Now;
                                dispositivoAlta.funcionamiento = 1;
                                dispositivoAlta.idMarca = "0000";
                                dispositivoAlta.idModelo = "0000";
                                dispositivoAlta.IdDispostivoColor = "000";
                                dispositivoAlta.caracteristicas = "- " + nombreColaborador;
                                dispositivoAlta.idArea = "010";
                                dispositivoAlta.sedeCodigo = "003";
                                dispositivoAlta.tipoDispositivoCodigo = "006";
                                dispositivoAlta.idSistemaDeImpresion = 13;
                                dispositivoAlta.lineaCelular = item.numeroCelular;
                                Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoAlta);
                                Modelo.SubmitChanges();

                                IdDispositivoAltaGenerado = dispositivoAlta.id;
                                item.idDispositivoAlta = dispositivoAlta.id;

                                SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoAltaGenerado;
                                dispositivoAsociadoAColaborador.item = "001";
                                dispositivoAsociadoAColaborador.estado = 1;
                                dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                dispositivoAsociadoAColaborador.tipo = '0';
                                dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                Modelo.SubmitChanges();
                            }
                            #endregion


                            oRegistro.id = obtenerultimoregistro;
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.idEmpresa = item.idEmpresa;
                            oRegistro.idSucursal = item.idSucursal;
                            oRegistro.fecha = item.fecha;
                            oRegistro.fechaCreacion = item.fechaCreacion;
                            oRegistro.estacionDeTrabajo = item.estacionDeTrabajo;
                            oRegistro.serie = item.serie;
                            oRegistro.iddocumento = item.iddocumento;
                            oRegistro.estadoCodigo = item.estadoCodigo;
                            oRegistro.numeroCelular = item.numeroCelular;
                            oRegistro.idDispositivoBaja = item.idDispositivoBaja;
                            oRegistro.idDispositivoAlta = item.idDispositivoAlta;
                            oRegistro.idReferencia = item.idReferencia;
                            oRegistro.idReferenciaAlta = item.idReferenciaAlta;
                            oRegistro.idReferenciaBaja = item.idReferenciaBaja;
                            oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                            oRegistro.motivoCodigo = item.motivoCodigo;
                            oRegistro.nota = item.nota;
                            oRegistro.justificacion = item.justificacion;
                            oRegistro.glosa = item.nota;
                            oRegistro.justificacionDeReposicion = item.justificacionDeReposicion != null ? item.justificacionDeReposicion.Trim() : "00";

                            Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.InsertOnSubmit(oRegistro);
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
                                oRegistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                                int obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeEquipamientoTecnologico.Max(x => x.id)) + 1;
                                #region Generar dispositivo de baja()
                                if (item.motivoCodigo == 1 && item.idDispositivoBaja == (int?)null)
                                {
                                    SAS_Dispostivo dispositivoBaja = new SAS_Dispostivo();
                                    dispositivoBaja.descripcion = "CELULAR ** DEVOLUCION";
                                    dispositivoBaja.nombres = "CELULAR ** DEVOLUCION";
                                    dispositivoBaja.estado = 1;
                                    dispositivoBaja.EsPropio = 1;
                                    dispositivoBaja.creadoPor = user.IdUsuario;
                                    dispositivoBaja.esFinal = 1;
                                    dispositivoBaja.fechacreacion = DateTime.Now;
                                    dispositivoBaja.funcionamiento = 1;
                                    dispositivoBaja.idMarca = "0000";
                                    dispositivoBaja.idModelo = "0000";
                                    dispositivoBaja.IdDispostivoColor = "000";
                                    dispositivoBaja.caracteristicas = "- " + nombreColaborador;
                                    dispositivoBaja.idArea = "010";
                                    dispositivoBaja.sedeCodigo = "003";
                                    dispositivoBaja.tipoDispositivoCodigo = "006";
                                    dispositivoBaja.idSistemaDeImpresion = 13;
                                    dispositivoBaja.lineaCelular = item.numeroCelular;
                                    Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoBaja);
                                    Modelo.SubmitChanges();

                                    IdDispositivoBajaGenerado = dispositivoBaja.id;
                                    item.idDispositivoBaja = dispositivoBaja.id;


                                    SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                    dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoBajaGenerado;
                                    dispositivoAsociadoAColaborador.item = "001";
                                    dispositivoAsociadoAColaborador.estado = 1;
                                    dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                    dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                    dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                    dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                    dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                    dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                    dispositivoAsociadoAColaborador.tipo = '0';
                                    dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                    Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                    Modelo.SubmitChanges();
                                }
                                #endregion

                                #region Generar dispositivo de alta()                               
                                if (item.motivoCodigo == 1 && item.idDispositivoAlta == (int?)null)
                                {
                                    SAS_Dispostivo dispositivoAlta = new SAS_Dispostivo();
                                    dispositivoAlta.descripcion = "CELULAR ** GESTIONAR";
                                    dispositivoAlta.nombres = "CELULAR ** GESTIONAR";
                                    dispositivoAlta.estado = 1;
                                    dispositivoAlta.EsPropio = 1;
                                    dispositivoAlta.creadoPor = user.IdUsuario;
                                    dispositivoAlta.esFinal = 1;
                                    dispositivoAlta.fechacreacion = DateTime.Now;
                                    dispositivoAlta.funcionamiento = 1;
                                    dispositivoAlta.idMarca = "0000";
                                    dispositivoAlta.idModelo = "0000";
                                    dispositivoAlta.IdDispostivoColor = "000";
                                    dispositivoAlta.caracteristicas = "- " + nombreColaborador;
                                    dispositivoAlta.idArea = "010";
                                    dispositivoAlta.sedeCodigo = "003";
                                    dispositivoAlta.tipoDispositivoCodigo = "006";
                                    dispositivoAlta.idSistemaDeImpresion = 13;
                                    dispositivoAlta.lineaCelular = item.numeroCelular;
                                    Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoAlta);
                                    Modelo.SubmitChanges();

                                    IdDispositivoAltaGenerado = dispositivoAlta.id;
                                    item.idDispositivoAlta = dispositivoAlta.id;

                                    SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                    dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoAltaGenerado;
                                    dispositivoAsociadoAColaborador.item = "001";
                                    dispositivoAsociadoAColaborador.estado = 1;
                                    dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                    dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                    dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                    dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                    dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                    dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                    dispositivoAsociadoAColaborador.tipo = '0';
                                    dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                    Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                    Modelo.SubmitChanges();
                                }
                                #endregion

                                oRegistro.id = obtenerultimoregistro;
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.estacionDeTrabajo = item.estacionDeTrabajo;
                                oRegistro.serie = item.serie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.numeroCelular = item.numeroCelular;
                                oRegistro.idDispositivoBaja = item.idDispositivoBaja;
                                oRegistro.idDispositivoAlta = item.idDispositivoAlta;
                                oRegistro.idReferencia = item.idReferencia;
                                oRegistro.idReferenciaAlta = item.idReferenciaAlta;
                                oRegistro.idReferenciaBaja = item.idReferenciaBaja;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.motivoCodigo = item.motivoCodigo;
                                oRegistro.nota = item.nota;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.glosa = item.glosa;
                                oRegistro.justificacionDeReposicion = item.justificacionDeReposicion != null ? item.justificacionDeReposicion.Trim() : "00";

                                Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.InsertOnSubmit(oRegistro);
                                Modelo.SubmitChanges();
                                tipoResultadoOperacion = oRegistro.id; // registrar
                                #endregion
                            }
                            else
                            {
                                #region Actualizar() 

                                #region Generar dispositivo de baja()
                                if (item.motivoCodigo == 1 && item.idDispositivoBaja == (int?)null)
                                {
                                    SAS_Dispostivo dispositivoBaja = new SAS_Dispostivo();
                                    dispositivoBaja.descripcion = "CELULAR ** DEVOLUCION";
                                    dispositivoBaja.nombres = "CELULAR ** DEVOLUCION";
                                    dispositivoBaja.estado = 1;
                                    dispositivoBaja.EsPropio = 1;
                                    dispositivoBaja.creadoPor = user.IdUsuario;
                                    dispositivoBaja.esFinal = 1;
                                    dispositivoBaja.fechacreacion = DateTime.Now;
                                    dispositivoBaja.funcionamiento = 1;
                                    dispositivoBaja.idMarca = "0000";
                                    dispositivoBaja.idModelo = "0000";
                                    dispositivoBaja.IdDispostivoColor = "000";
                                    dispositivoBaja.caracteristicas = "- " + nombreColaborador;
                                    dispositivoBaja.idArea = "010";
                                    dispositivoBaja.sedeCodigo = "003";
                                    dispositivoBaja.tipoDispositivoCodigo = "006";
                                    dispositivoBaja.idSistemaDeImpresion = 13;
                                    dispositivoBaja.lineaCelular = item.numeroCelular;
                                    Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoBaja);
                                    Modelo.SubmitChanges();

                                    IdDispositivoBajaGenerado = dispositivoBaja.id;
                                    item.idDispositivoBaja = dispositivoBaja.id;


                                    SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                    dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoBajaGenerado;
                                    dispositivoAsociadoAColaborador.item = "001";
                                    dispositivoAsociadoAColaborador.estado = 1;
                                    dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                    dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                    dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                    dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                    dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                    dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                    dispositivoAsociadoAColaborador.tipo = '0';
                                    dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                    Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                    Modelo.SubmitChanges();
                                }
                                #endregion

                                #region Generar dispositivo de alta()                               
                                if (item.motivoCodigo == 1 && item.idDispositivoAlta == (int?)null)
                                {
                                    SAS_Dispostivo dispositivoAlta = new SAS_Dispostivo();
                                    dispositivoAlta.descripcion = "CELULAR ** GESTIONAR";
                                    dispositivoAlta.nombres = "CELULAR ** GESTIONAR";
                                    dispositivoAlta.estado = 1;
                                    dispositivoAlta.EsPropio = 1;
                                    dispositivoAlta.creadoPor = user.IdUsuario;
                                    dispositivoAlta.esFinal = 1;
                                    dispositivoAlta.fechacreacion = DateTime.Now;
                                    dispositivoAlta.funcionamiento = 1;
                                    dispositivoAlta.idMarca = "0000";
                                    dispositivoAlta.idModelo = "0000";
                                    dispositivoAlta.IdDispostivoColor = "000";
                                    dispositivoAlta.caracteristicas = "- " + nombreColaborador;
                                    dispositivoAlta.idArea = "010";
                                    dispositivoAlta.sedeCodigo = "003";
                                    dispositivoAlta.tipoDispositivoCodigo = "006";
                                    dispositivoAlta.idSistemaDeImpresion = 13;
                                    dispositivoAlta.lineaCelular = item.numeroCelular;
                                    Modelo.SAS_Dispostivo.InsertOnSubmit(dispositivoAlta);
                                    Modelo.SubmitChanges();

                                    IdDispositivoAltaGenerado = dispositivoAlta.id;
                                    item.idDispositivoAlta = dispositivoAlta.id;

                                    SAS_DispositivoUsuarios dispositivoAsociadoAColaborador = new SAS_DispositivoUsuarios();
                                    dispositivoAsociadoAColaborador.dispositivoCodigo = IdDispositivoAltaGenerado;
                                    dispositivoAsociadoAColaborador.item = "001";
                                    dispositivoAsociadoAColaborador.estado = 1;
                                    dispositivoAsociadoAColaborador.idcodigoGeneral = item.idCodigoGeneral;
                                    dispositivoAsociadoAColaborador.desde = DateTime.Now;
                                    dispositivoAsociadoAColaborador.hasta = DateTime.Now;
                                    dispositivoAsociadoAColaborador.observacion = "Generado desde solicitud de renovación";
                                    dispositivoAsociadoAColaborador.fechaCreacion = DateTime.Now;
                                    dispositivoAsociadoAColaborador.registradoPor = user.IdUsuario;
                                    dispositivoAsociadoAColaborador.tipo = '0';
                                    dispositivoAsociadoAColaborador.seVisualizaEnReportes = 1;
                                    Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(dispositivoAsociadoAColaborador);
                                    Modelo.SubmitChanges();
                                }
                                #endregion


                                oRegistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
                                oRegistro = resultado.Single();
                                oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                                oRegistro.idEmpresa = item.idEmpresa;
                                oRegistro.idSucursal = item.idSucursal;
                                oRegistro.fecha = item.fecha;
                                oRegistro.fechaCreacion = item.fechaCreacion;
                                oRegistro.estacionDeTrabajo = item.estacionDeTrabajo;
                                oRegistro.serie = item.serie;
                                oRegistro.iddocumento = item.iddocumento;
                                oRegistro.estadoCodigo = item.estadoCodigo;
                                oRegistro.numeroCelular = item.numeroCelular;
                                oRegistro.idDispositivoBaja = item.idDispositivoBaja;
                                oRegistro.idDispositivoAlta = item.idDispositivoAlta;
                                oRegistro.idReferencia = item.idReferencia;
                                oRegistro.idReferenciaAlta = item.idReferenciaAlta;
                                oRegistro.idReferenciaBaja = item.idReferenciaBaja;
                                oRegistro.usuarioEnAtencion = item.usuarioEnAtencion;
                                oRegistro.motivoCodigo = item.motivoCodigo;

                                oRegistro.nota = item.nota;
                                oRegistro.justificacion = item.justificacion;
                                oRegistro.glosa = item.glosa;
                                oRegistro.justificacionDeReposicion = item.justificacionDeReposicion != null ? item.justificacionDeReposicion.Trim() : "00";

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

        // cambiar estado
        public int ChangeState(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
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


        public int ChangeStateRequest(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();
                        oregistro.estadoCodigo = item.estadoCodigo.Trim();
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int ChangeState(string conection, SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
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


        // cambiar estado del documento
        public int ChangeStateDocument(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item, string codigoEstadoFlujoAprobacion)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        oregistro.estadoCodigo = codigoEstadoFlujoAprobacion;
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = 4; // Flujo de aprobacion
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public string ApproveRequest(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item, SAS_USUARIOS user)
        {
            string resultadoProceso = string.Empty;
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var scopeOptions = new TransactionOptions();
                scopeOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                scopeOptions.Timeout = TimeSpan.MaxValue;

                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        if (oregistro.estadoCodigo == "SO")
                        {
                            #region Evaluar el tipo de solicitud por el motivo selecionado() 
                            //var motivosSolicitud = Modelo.SAS_MotivoRenovacionCelular.Where(x => x.estado == 1 && x.descripcion.Contains("RENOVACIÓN")).ToList();//
                            var motivosSolicitud = Modelo.SAS_MotivoRenovacionCelular.Where(x => x.estado == 1 && x.id == oregistro.motivoCodigo).ToList();//
                            if (motivosSolicitud != null)
                            {
                                if (motivosSolicitud.ToList().Count > 0)
                                {
                                    #region Renovación()
                                    var SolicitudTipoRenovacion = motivosSolicitud.Where(x => x.id == oregistro.motivoCodigo).ToList();
                                    if (SolicitudTipoRenovacion != null)
                                    {
                                        if (SolicitudTipoRenovacion.ToList().Count > 0)
                                        {
                                            #region Registrar una solicitud de equipamiento con el motivo Renovación.
                                            SAS_MotivoRenovacionCelular motivoDeSolicitudRegistrado = new SAS_MotivoRenovacionCelular();
                                            motivoDeSolicitudRegistrado = SolicitudTipoRenovacion.ElementAt(0);

                                            SAS_SolicitudDeEquipamientoTecnologicoController modeoEquipamientoTecnologico = new SAS_SolicitudDeEquipamientoTecnologicoController();
                                            resultadoProceso = modeoEquipamientoTecnologico.ToRegisterBySolicitudRenovacion("SAS", oregistro, motivoDeSolicitudRegistrado.descripcion.Trim(), user, "PE");
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }

                            #endregion                          
                        }
                    }
                }



            }
            return resultadoProceso;
        }


        public string ApproveRequest(string conection, int codigoSolicitud, SAS_USUARIOS user)
        {
            string resultadoProceso = string.Empty;
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var scopeOptions = new TransactionOptions();
                scopeOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                scopeOptions.Timeout = TimeSpan.MaxValue;

                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == codigoSolicitud).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        if (oregistro.estadoCodigo == "SO")
                        {
                            #region Evaluar el tipo de solicitud por el motivo selecionado() 
                            //var motivosSolicitud = Modelo.SAS_MotivoRenovacionCelular.Where(x => x.estado == 1 && x.descripcion.Contains("RENOVACIÓN")).ToList();//
                            var motivosSolicitud = Modelo.SAS_MotivoRenovacionCelular.Where(x => x.estado == 1 && x.id == oregistro.motivoCodigo).ToList();//
                            if (motivosSolicitud != null)
                            {
                                if (motivosSolicitud.ToList().Count > 0)
                                {
                                    #region Renovación()
                                    var SolicitudTipoRenovacion = motivosSolicitud.Where(x => x.id == oregistro.motivoCodigo).ToList();
                                    if (SolicitudTipoRenovacion != null)
                                    {
                                        if (SolicitudTipoRenovacion.ToList().Count > 0)
                                        {
                                            #region Registrar una solicitud de equipamiento con el motivo Renovación.
                                            SAS_MotivoRenovacionCelular motivoDeSolicitudRegistrado = new SAS_MotivoRenovacionCelular();
                                            motivoDeSolicitudRegistrado = SolicitudTipoRenovacion.ElementAt(0);

                                            SAS_SolicitudDeEquipamientoTecnologicoController modeoEquipamientoTecnologico = new SAS_SolicitudDeEquipamientoTecnologicoController();
                                            resultadoProceso = modeoEquipamientoTecnologico.ToRegisterBySolicitudRenovacion("SAS", oregistro, motivoDeSolicitudRegistrado.descripcion.Trim(), user, "PE");
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }

                            #endregion                          
                        }
                    }
                }



            }
            return resultadoProceso;
        }



        public string ReleaseRequest(string conection, int codigoSolicitud, SAS_USUARIOS user)
        {
            string resultadoProceso = string.Empty;
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var scopeOptions = new TransactionOptions();
                scopeOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                scopeOptions.Timeout = TimeSpan.MaxValue;

                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == codigoSolicitud).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        oregistro = resultado.ElementAt(0);
                        oregistro.estadoCodigo = "SO";
                        Modelo.SubmitChanges();

                    }
                }
            }
            return resultadoProceso;
        }

        
        public string ReturnRequestStatus(string conection, int codigoSolicitud, SAS_USUARIOS user)
        {
            string resultadoProceso = string.Empty;
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var scopeOptions = new TransactionOptions();
                scopeOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                scopeOptions.Timeout = TimeSpan.MaxValue;

                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == codigoSolicitud).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        oregistro = resultado.ElementAt(0);
                        oregistro.estadoCodigo = "PE";
                        Modelo.SubmitChanges();

                    }
                }
            }
            return resultadoProceso;
        }


        public int RejectRequest(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        if (oregistro.estadoCodigo == "SO")
                        {
                            oregistro.estadoCodigo = "RE";
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 5; // RECHAZAR
                        }
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int RejectRequest(string conection, int codigoSolicitud)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == codigoSolicitud).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oregistro = resultado.Single();
                        if (oregistro.estadoCodigo == "SO")
                        {
                            oregistro.estadoCodigo = "RE";
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 5; // RECHAZAR
                        }
                    }
                }
            }
            return tipoResultadoOperacion;
        }



        // eliminar registro
        public int DeleteRecord(string conection, SAS_SolicitudDeRenovacionTelefoniaCelular item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        // if (oregistro.estadoCodigo == "PE" || oregistro.estadoCodigo == "AN") 
                        if (oregistro.estadoCodigo == "PE") // Modificado el 25.06.2022
                        {
                            Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.DeleteOnSubmit(oregistro);
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }

        public int DeleteRecord(string conection, SAS_SolicitudDeRenovacionTelefoniaCelularListadoByDateResult item)
        {
            SAS_SolicitudDeRenovacionTelefoniaCelular oregistro = new SAS_SolicitudDeRenovacionTelefoniaCelular();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()                         
                        oregistro = resultado.Single();

                        if (oregistro.estadoCodigo == "PE")
                        {
                            Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.DeleteOnSubmit(oregistro);
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public int ObtenerNumeroCorrelativoDeCero(string conection, string tabla)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int obtenerultimoregistro = 0;
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                if (tabla.ToUpper() == "SAS_SolicitudDeRenovacionTelefoniaCelular".ToUpper())
                {
                    obtenerultimoregistro = Convert.ToInt32(Modelo.SAS_SolicitudDeRenovacionTelefoniaCelular.Max(x => x.id)) + 1;
                }

            }

            return obtenerultimoregistro;
        }

        public void Notify(string conection, string Para, string Asunto, int codigoSolicitudSelecionada)
        {
            List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult> listadoCabecera = new List<SAS_SolicitudDeRenovacionTelefoniaCelularListadoByIDResult>();
            

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listadoCabecera = Modelo.SAS_SolicitudDeRenovacionTelefoniaCelularListadoByID(codigoSolicitudSelecionada).ToList();                
            }

            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica solicitud de " + listadoCabecera.ElementAt(0).motivoSolicitud + " \n"));
                Mensaje.Append(string.Format("Solicitud Generada : " + listadoCabecera.ElementAt(0).documento + "\n"));                
                Mensaje.Append(string.Format("Fecha de solicitud {0:dd/MM/yyyy} \n\n", listadoCabecera.ElementAt(0).fecha));
                Mensaje.Append(string.Format("Datos del colaborador : " + listadoCabecera.ElementAt(0).idCodigoGeneral + " - " + listadoCabecera.ElementAt(0).nombres +  "\n"));
                Mensaje.Append(string.Format("Línea celular: \n\n", listadoCabecera.ElementAt(0).numeroCelular));

                Mensaje.Append(string.Format("Atendido Por : " + listadoCabecera.ElementAt(0).usuarioEnAtencion + "\n"));
                Mensaje.Append(string.Format("Equipo para baja | devolución : " + listadoCabecera.ElementAt(0).idDispositivoBaja.ToString() + " | " +  listadoCabecera.ElementAt(0).dispositivoBaja.ToString() + "\n"));
                Mensaje.Append(string.Format("Equipo para alta | préstamo " + listadoCabecera.ElementAt(0).idDispositivoAlta.ToString() + " | " + listadoCabecera.ElementAt(0).dispositivoAlta.ToString() + "\n"));
                
               
               
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
                mail.Subject = Asunto + " | " + listadoCabecera.ElementAt(0).motivoSolicitud + " | " + listadoCabecera.ElementAt(0).documento;
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
    }
}
