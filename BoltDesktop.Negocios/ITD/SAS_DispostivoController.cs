using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DispostivoController
    {
        public Int32 Register(string Connection, SAS_Dispostivo device)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;


                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;


                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;
                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;


                        Modelo.SubmitChanges();
                        codigo = oDevice.id;
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> ObtenerListadoDeDispositivosPorProveedorSedeTipo(string conection, string proveedorCodigo, string sedeCodigo, string tipoDispositivoCodigo)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> result = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_ListadoDeDispositivosAllResult> result2 = new List<SAS_ListadoDeDispositivosAllResult>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                 result2 = Modelo.SAS_ListadoDeDispositivosAll().ToList().Where(x => x.idClieprov.Trim() == proveedorCodigo && x.sedeCodigo.Trim() == sedeCodigo.Trim() && x.tipoDispositivoCodigo.Trim() == tipoDispositivoCodigo).ToList();
            }

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                var result3 = result2.Where(x => x.esFinal == 1 && x.idestado == Convert.ToByte(1) && x.funcionamientoCodigo == 1).ToList();
                var result4 = result3.Where(x => x.estado.Trim().ToUpper() == "ACTIVO" || x.estado.Trim().ToUpper() == "En mantenimiento" || x.estado.Trim().ToUpper() == "Proximos a devolver a proveedor" || x.estado.Trim().ToUpper() == "n proceso de baja").ToList();
                result = (from item in result4
                          group item by new { item.id } into j
                          select new SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor
                          {
                              ID = j.Key.id,
                              dispositivo = j.FirstOrDefault().dispositivo.Trim(),
                              personal = j.FirstOrDefault().item.Trim(),
                              area = j.FirstOrDefault().area.Trim(),
                              sedecodigo = j.FirstOrDefault().sedeCodigo.Trim(),
                              sedeDescripcion = j.FirstOrDefault().sedeDescripcion.Trim(),
                              idClieprov = j.FirstOrDefault().idClieprov.Trim(),
                              razonSocial = j.FirstOrDefault().razonSocial.Trim(),
                              tipoDispositivoCodigo = j.FirstOrDefault().tipoDispositivoCodigo.Trim(),
                              tipoDispositivo = j.FirstOrDefault().tipoDispositivo.Trim(),
                              selecionado = 0,
                              cantidad = 1,
                              fecha = DateTime.Now,
                              documento = string.Empty,
                              codigoMacro = string.Empty
                          }).ToList();
            }

            return result;
        }

        public int ChangeStatusDevice(string Connection, int _idDispositivo, int idMotivoCambioEstado, string motivo)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == _idDispositivo).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        if (oDevice.estado != 0)
                        {
                            oDevice.estado = Convert.ToByte(idMotivoCambioEstado);
                            oDevice.caracteristicas = (motivo).Trim() + " | " + oDevice.caracteristicas.Trim();
                        }

                        Modelo.SubmitChanges();
                        codigo = Convert.ToInt32(oDevice.id);
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        public List<SAS_Dispostivo> GetDeviceByIdProduct(string Connection, string idProducto)
        {
            List<SAS_Dispostivo> resultado = new List<SAS_Dispostivo>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_Dispostivo.Where(x => x.idProducto == idProducto).ToList();
            }
            return resultado;
        }

        // copiar detalle de hardware desde otro dispositivo a un dispositivo en específico
        public void CopiarDetalleDeHardwareDeUnDispositivoAOtro(string Connection, string idCodigoOrigen, string idCodigoBase)
        {

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                Modelo.SAS_DispositivoHardwareCopiarEntreDispositivo(Convert.ToInt32(idCodigoOrigen), Convert.ToInt32(idCodigoBase));
            }

        }
        // copiar detalle de hardware desde otro dispositivo a todos los que tienen como código de producto

        public void CopiarDetalleDeHardwareDeUnDispositivoATodos(string Connection, string idCodigoOrigen, string idCodigoBase, string codigoProducto)
        {

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var listado = Modelo.SAS_Dispostivo.Where(x => x.idProducto.Trim().ToUpper() == codigoProducto).ToList();

                if (listado != null)
                {
                    if (listado.Count > 0)
                    {
                        foreach (var item in listado)
                        {

                            // si tiene menos de 10
                            var listadoDetalleHardware = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == item.id).ToList();
                            if (listadoDetalleHardware != null)
                            {
                                if (listadoDetalleHardware.ToList().Count() < 10)
                                {
                                    Modelo.SAS_DispositivoHardwareCopiarEntreDispositivo(item.id, Convert.ToInt32(idCodigoBase));
                                }
                            }

                        }

                    }
                }

            }

        }

        public SAS_ListadoDeDispositivosAllResult ObtenerDatosDeDispositivo(string conection, int codigoDispositivo)
        {
            SAS_ListadoDeDispositivosAllResult item = new SAS_ListadoDeDispositivosAllResult();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            //  using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_ListadoDeDispositivosAll().ToList().Where(x => x.id == codigoDispositivo).ToList();

                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        item = result.ElementAt(0);
                    }
                }
            }

            return item;
        }

        public int DuplicarDispositivoDesdeId(string Connection, int codigo, string numeroCelular)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                Modelo.SAS_DispositivoDuplicarDesdeId(codigo, numeroCelular);
            }

            return 1;

        }


        // Registrar con listado detalle de ips eliminados y nuevos 
        public Int32 Register(string Connection, SAS_Dispostivo device, List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;


                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;


                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                }

                                #endregion
                            }
                        }


                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        // Eliminar lista de eliminados de los ips por device.
                        if (listOfDeletedIPs != null)
                        {
                            if (listOfDeletedIPs.Count > 0)
                            {
                                foreach (var detalle in listOfDeletedIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                        }

                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 
                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
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
            return codigo;
        }

        // Registrar con listado detalle de ips eliminados y nuevos 
        // Registrar con listado detalle de ips eliminados y nuevos , listado de usuarios nuevos y eliminados
        public int Register(string Connection, SAS_Dispostivo device, List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs, List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados, List<SAS_DispositivoUsuarios> listadoColaboradores)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;

                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        //registrar Número de IP
                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listOfIPs)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Colaboradores
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        // Eliminar lista de eliminados de los ips por device.
                        if (listOfDeletedIPs != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listOfDeletedIPs.Count > 0)
                            {
                                foreach (var detalle in listOfDeletedIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }


                        // Eliminar lista de eliminados de USER  POR device.
                        if (listadoColaboradoresEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoColaboradoresEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoColaboradoresEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoUsuarios oDetalle = new SAS_DispositivoUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }


                        // Modificar y registrar listado de Ip
                        if (listOfIPs != null)
                        {
                            #region Editar y registrar listado de IP                            
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 
                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                }

                                #endregion
                            }
                            #endregion
                        }

                        // Modificar y registrar listado de usuarios
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }





                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        public int Register(string Connection, SAS_Dispostivo device, List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs, List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados, List<SAS_DispositivoUsuarios> listadoColaboradores, List<SAS_DispositivoHardware> listadoHardwareEliminados, List<SAS_DispositivoHardware> listadoHardware, List<SAS_DispositivoSoftware> listadoSoftwareEliminados, List<SAS_DispositivoSoftware> listadoSoftware)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;

                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        //registrar Número de IP
                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listOfIPs)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Colaboradores
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        // Eliminar lista de eliminados de los ips por device.
                        if (listOfDeletedIPs != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listOfDeletedIPs.Count > 0)
                            {
                                foreach (var detalle in listOfDeletedIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }


                        // Eliminar lista de eliminados de USER  POR device.
                        if (listadoColaboradoresEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoColaboradoresEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoColaboradoresEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoUsuarios oDetalle = new SAS_DispositivoUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de hardware.
                        if (listadoHardwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoHardwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoHardwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoHardware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de software.
                        if (listadoSoftwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoSoftwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoSoftwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoSoftware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }


                        // Modificar y registrar listado de Ip
                        if (listOfIPs != null)
                        {
                            #region Editar y registrar listado de IP                            
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 
                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                }

                                #endregion
                            }
                            #endregion
                        }

                        // Modificar y registrar listado de usuarios
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }




                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        public SAS_Dispostivo ObtenerDispositivoFilterByID(string Connection, int idDispositivo)
        {
            SAS_Dispostivo item = new SAS_Dispostivo();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var result = Modelo.SAS_Dispostivo.Where(x => x.id == idDispositivo).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        item = result.ElementAt(0);
                    }
                }

            }
            return item;
        }

        public int Register(string Connection, SAS_Dispostivo device, List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs, List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados, List<SAS_DispositivoUsuarios> listadoColaboradores, List<SAS_DispositivoHardware> listadoHardwareEliminados, List<SAS_DispositivoHardware> listadoHardware, List<SAS_DispositivoSoftware> listadoSoftwareEliminados, List<SAS_DispositivoSoftware> listadoSoftware, List<SAS_DispositivoComponentes> listadoComponentesEliminados, List<SAS_DispositivoComponentes> listadoComponentes, List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuariosEliminados, List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuarios, List<SAS_DispositivoDocumento> listadoDocumentosEliminados, List<SAS_DispositivoDocumento> listadoDocumentos)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                        oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                        oDevice.imagen = device.imagen != null ? device.imagen : null;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;

                        oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                        oDevice.ubicacion = device.ubicacion;
                        oDevice.costoUSD = device.costoUSD;
                        oDevice.lineaCelular = device.lineaCelular;

                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        //registrar Número de IP
                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listOfIPs)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Colaboradores
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Componentes
                        if (listadoComponentes != null)
                        {
                            if (listadoComponentes.Count > 0)
                            {
                                #region Registrar listado detalle de listadoComponentes() 

                                foreach (var detalle in listadoComponentes)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar cuentas de usuario
                        if (listadoCuentasUsuarios != null)
                        {
                            if (listadoCuentasUsuarios.Count > 0)
                            {
                                #region Registrar listado detalle de cuentas de usuario() 

                                foreach (var detalle in listadoCuentasUsuarios)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;


                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;

                                            Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;


                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar documentos
                        if (listadoDocumentos != null)
                        {
                            if (listadoDocumentos.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 

                                foreach (var detalle in listadoDocumentos)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;

                                            Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;

                        oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                        oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;

                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                        oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                        oDevice.imagen = device.imagen != null ? device.imagen : null;

                        oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                        oDevice.ubicacion = device.ubicacion;
                        oDevice.costoUSD = device.costoUSD;
                        oDevice.lineaCelular = device.lineaCelular;

                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        // Eliminar lista de eliminados de los ips por device.
                        if (listOfDeletedIPs != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listOfDeletedIPs.Count > 0)
                            {
                                foreach (var detalle in listOfDeletedIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de USER  POR device.
                        if (listadoColaboradoresEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoColaboradoresEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoColaboradoresEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoUsuarios oDetalle = new SAS_DispositivoUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de hardware.
                        if (listadoHardwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoHardwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoHardwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoHardware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de software.
                        if (listadoSoftwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoSoftwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoSoftwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoSoftware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de componentes.
                        if (listadoComponentesEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoComponentesEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoComponentesEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoComponentes.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de cuentas de usuario.
                        if (listadoCuentasUsuariosEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoCuentasUsuariosEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoCuentasUsuariosEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoCuentaUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de documentos.
                        if (listadoDocumentosEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoDocumentosEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoDocumentosEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoDocumento.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Modificar y registrar listado de Ip
                        if (listOfIPs != null)
                        {
                            #region Editar y registrar listado de IP                            
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 
                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                }

                                #endregion
                            }
                            #endregion
                        }

                        // Modificar y registrar listado de usuarios
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Componentes
                        if (listadoComponentes != null)
                        {
                            if (listadoComponentes.Count > 0)
                            {
                                #region Registrar listado detalle de listadoComponentes() 

                                foreach (var detalle in listadoComponentes)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar cuentas de usuario
                        if (listadoCuentasUsuarios != null)
                        {
                            if (listadoCuentasUsuarios.Count > 0)
                            {
                                #region Registrar listado detalle de cuentas de usuario() 

                                foreach (var detalle in listadoCuentasUsuarios)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;

                                            Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar documentos
                        if (listadoDocumentos != null)
                        {
                            if (listadoDocumentos.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 

                                foreach (var detalle in listadoDocumentos)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;

                                            Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }




                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        // este es 20.04.2022
        public int Register(string Connection, SAS_Dispostivo device, List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs, List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados, List<SAS_DispositivoUsuarios> listadoColaboradores, List<SAS_DispositivoHardware> listadoHardwareEliminados, List<SAS_DispositivoHardware> listadoHardware, List<SAS_DispositivoSoftware> listadoSoftwareEliminados, List<SAS_DispositivoSoftware> listadoSoftware, List<SAS_DispositivoComponentes> listadoComponentesEliminados, List<SAS_DispositivoComponentes> listadoComponentes, List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuariosEliminados, List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuarios, List<SAS_DispositivoDocumento> listadoDocumentosEliminados, List<SAS_DispositivoDocumento> listadoDocumentos, List<SAS_DispositivoContadores> listadoContadoresEliminados, List<SAS_DispositivoContadores> listadoContadores, List<SAS_DispositivoMovimientoMantenimientos> listadoMantenimientosEliminados, List<SAS_DispositivoMovimientoMantenimientos> listadoMantenimientos, List<SAS_DispositivoMovimientoAlmacen> listadoMovimientoAlmacenEliminados, List<SAS_DispositivoMovimientoAlmacen> listadoMovimientoAlmacen)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 0)
                    {
                        #region Registrar() 
                        #region Dispositivo()
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                        oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;
                        oDevice.estado = 1;
                        oDevice.fechacreacion = DateTime.Now;
                        oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                        oDevice.imagen = device.imagen != null ? device.imagen : null;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;

                        oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                        oDevice.ubicacion = device.ubicacion;
                        oDevice.costoUSD = device.costoUSD;
                        oDevice.lineaCelular = device.lineaCelular;

                        oDevice.idSistemaDeImpresion = device.idSistemaDeImpresion;
                        oDevice.costoMantenimientoAnualUSD = device.costoMantenimientoAnualUSD;
                        oDevice.costoSuministroAnualUSD = device.costoSuministroAnualUSD;

                        oDevice.kilovatioHora = device.kilovatioHora.Value;
                        oDevice.tipoDeFacturacionDeConsumoEnergético = device.tipoDeFacturacionDeConsumoEnergético;

                        Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        #endregion
                        codigo = oDevice.id;

                        //registrar Número de IP
                        if (listOfIPs != null)
                        {
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listOfIPs)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Colaboradores
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Componentes
                        if (listadoComponentes != null)
                        {
                            if (listadoComponentes.Count > 0)
                            {
                                #region Registrar listado detalle de listadoComponentes() 

                                foreach (var detalle in listadoComponentes)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar cuentas de usuario
                        if (listadoCuentasUsuarios != null)
                        {
                            if (listadoCuentasUsuarios.Count > 0)
                            {
                                #region Registrar listado detalle de cuentas de usuario() 

                                foreach (var detalle in listadoCuentasUsuarios)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;


                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                            oDetalle.cuenta = detalle.cuenta;
                                            Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.cuenta = detalle.cuenta;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;


                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar documentos
                        if (listadoDocumentos != null)
                        {
                            if (listadoDocumentos.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 

                                foreach (var detalle in listadoDocumentos)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;

                                            Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar contadores
                        if (listadoContadores != null)
                        {
                            if (listadoContadores.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 
                                foreach (var item in listadoContadores)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultado.ToList().Count == 0)
                                        {
                                            #region Nuevo();


                                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                            recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;

                                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;


                                            Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultado.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                            recordObject = resultadoConsulta.Single();
                                            //recordObject.codigoDispositivo = item.codigoDispositivo;
                                            //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                            recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;

                                            //Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }


                        //registrar mantenimientos por dispositivo
                        if (listadoMantenimientos != null)
                        {
                            if (listadoMantenimientos.Count > 0)
                            {
                                #region Registrar listado detalle mantenimiento () 
                                foreach (var item in listadoMantenimientos)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultado.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                            //codigoDispositivo, item, codigoTipoManteniento, codigoColaborador, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario, codigoOrdenTrabajo
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                            recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                            recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultado.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                            recordObject = resultadoConsulta.Single();
                                            //recordObject.codigoDispositivo = item.codigoDispositivo;
                                            //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                            recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                            recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }

                        //registrar movimiento almacen () 
                        if (listadoMovimientoAlmacen != null)
                        {
                            if (listadoMovimientoAlmacen.Count > 0)
                            {
                                #region Registrar listado detalle movimiento almacen () 
                                foreach (var item in listadoMovimientoAlmacen)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultado.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                            recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                            recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultado.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                            recordObject = resultadoConsulta.Single();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            // recordObject.codigoDispositivo = item.codigoDispositivo;
                                            // recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                            recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                            recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            // Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }



                        #endregion
                    }
                    else if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        #region Dispositivo() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                        oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;

                        oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                        oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;

                        oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                        oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                        oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                        oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                        oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                        oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                        oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                        oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                        oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                        oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                        oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                        oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                        oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                        oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                        oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                        oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                        oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                        oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                        oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                        oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                        oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                        oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                        oDevice.imagen = device.imagen != null ? device.imagen : null;

                        oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                        oDevice.ubicacion = device.ubicacion;
                        oDevice.costoUSD = device.costoUSD;
                        oDevice.lineaCelular = device.lineaCelular;

                        oDevice.idSistemaDeImpresion = device.idSistemaDeImpresion;
                        oDevice.costoMantenimientoAnualUSD = device.costoMantenimientoAnualUSD;
                        oDevice.costoSuministroAnualUSD = device.costoSuministroAnualUSD;


                        oDevice.kilovatioHora = device.kilovatioHora.Value;
                        oDevice.tipoDeFacturacionDeConsumoEnergético = device.tipoDeFacturacionDeConsumoEnergético;

                        Modelo.SubmitChanges();
                        #endregion
                        codigo = oDevice.id;

                        // Eliminar lista de eliminados de los ips por device.
                        if (listOfDeletedIPs != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listOfDeletedIPs.Count > 0)
                            {
                                foreach (var detalle in listOfDeletedIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de USER  POR device.
                        if (listadoColaboradoresEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoColaboradoresEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoColaboradoresEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoUsuarios oDetalle = new SAS_DispositivoUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de hardware.
                        if (listadoHardwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoHardwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoHardwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoHardware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de software.
                        if (listadoSoftwareEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoSoftwareEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoSoftwareEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoSoftware.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de componentes.
                        if (listadoComponentesEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoComponentesEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoComponentesEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoComponentes.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de cuentas de usuario.
                        if (listadoCuentasUsuariosEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoCuentasUsuariosEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoCuentasUsuariosEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoCuentaUsuarios.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de documentos.
                        if (listadoDocumentosEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoDocumentosEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoDocumentosEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoDocumento.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Add el 14.04.2022
                        // Eliminar lista de eliminados de detalle contadores() 
                        if (listadoContadoresEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoContadoresEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoContadoresEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoContadores oDetalle = new SAS_DispositivoContadores();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoContadores.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de detalle mantenimientos() 
                        if (listadoMantenimientosEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoMantenimientosEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoMantenimientosEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoMovimientoMantenimientos oDetalle = new SAS_DispositivoMovimientoMantenimientos();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoMovimientoMantenimientos.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Eliminar lista de eliminados de detalle movimiento almacen() 
                        if (listadoMovimientoAlmacenEliminados != null)
                        {
                            #region Eliminar lista de Ip para eliminar() 
                            if (listadoMovimientoAlmacenEliminados.Count > 0)
                            {
                                foreach (var detalle in listadoMovimientoAlmacenEliminados)
                                {
                                    var result1 = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoMovimientoAlmacen oDetalle = new SAS_DispositivoMovimientoAlmacen();
                                            oDetalle = result1.Single();
                                            Modelo.SAS_DispositivoMovimientoAlmacen.DeleteOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }



                        // Modificar y registrar listado de Ip
                        if (listOfIPs != null)
                        {
                            #region Editar y registrar listado de IP                            
                            if (listOfIPs.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 
                                foreach (var detalle in listOfIPs)
                                {
                                    var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.direcionMAC = detalle.direcionMAC;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            oIp.idIP = detalle.idIP;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                }

                                #endregion
                            }
                            #endregion
                        }

                        // Modificar y registrar listado de usuarios
                        if (listadoColaboradores != null)
                        {
                            if (listadoColaboradores.Count > 0)
                            {
                                #region Registrar listado detalle de IPs. 

                                foreach (var detalle in listadoColaboradores)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp.dispositivoCodigo = codigo;
                                            oIp.item = detalle.item;
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            oIp.fechaCreacion = DateTime.Now;
                                            oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                            oIp = result1.Single();
                                            oIp.estado = detalle.estado;
                                            oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                            oIp.hasta = detalle.hasta;
                                            oIp.desde = detalle.desde;
                                            oIp.observacion = detalle.observacion;
                                            //oIp.fechaCreacion = DateTime.Now;
                                            //oIp.registradoPor = Environment.UserName;
                                            oIp.tipo = detalle.tipo;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Hardware
                        if (listadoHardware != null)
                        {
                            if (listadoHardware.Count > 0)
                            {
                                #region Registrar listado detalle Hardware()  

                                foreach (var detalle in listadoHardware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoHardware = detalle.codigoHardware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.capacidad = detalle.capacidad;
                                            oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        // Modificar y registrar Software
                        if (listadoSoftware != null)
                        {
                            if (listadoSoftware.Count > 0)
                            {
                                #region Registrar listado detalle de Software() 

                                foreach (var detalle in listadoSoftware)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoSoftware = detalle.codigoSoftware;
                                            oDetalle.serie = detalle.serie;
                                            oDetalle.version = detalle.version;
                                            oDetalle.informacionAdicional = detalle.informacionAdicional;
                                            oDetalle.numeroParte = detalle.numeroParte;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar Componentes
                        if (listadoComponentes != null)
                        {
                            if (listadoComponentes.Count > 0)
                            {
                                #region Registrar listado detalle de listadoComponentes() 

                                foreach (var detalle in listadoComponentes)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar cuentas de usuario
                        if (listadoCuentasUsuarios != null)
                        {
                            if (listadoCuentasUsuarios.Count > 0)
                            {
                                #region Registrar listado detalle de cuentas de usuario() 

                                foreach (var detalle in listadoCuentasUsuarios)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                            oDetalle.cuenta = detalle.cuenta;
                                            Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                            oDetalle.clave = detalle.clave;
                                            oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                            oDetalle.cuenta = detalle.cuenta;
                                            oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }

                        //registrar documentos
                        if (listadoDocumentos != null)
                        {
                            if (listadoDocumentos.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 

                                foreach (var detalle in listadoDocumentos)
                                {
                                    #region 
                                    var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 0)
                                        {
                                            #region Nuevo()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle.codigoDispositivo = codigo;
                                            oDetalle.item = detalle.item;
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;
                                            oDetalle.cargoFijo = detalle.cargoFijo;
                                            oDetalle.cargoVariable = detalle.cargoVariable;
                                            oDetalle.idMoneda = detalle.idMoneda;
                                            oDetalle.idMedida = detalle.idMedida;
                                            oDetalle.cantidadContratada = detalle.cantidadContratada;
                                            oDetalle.frecuenciaDeFacturacion = detalle.frecuenciaDeFacturacion;
                                            Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                        else if (result1.ToList().Count == 1)
                                        {
                                            #region Editar()
                                            SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                            oDetalle = result1.Single();
                                            oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                            oDetalle.observacion = detalle.observacion;
                                            oDetalle.hasta = detalle.hasta;
                                            oDetalle.desde = detalle.desde;
                                            oDetalle.estado = detalle.estado;
                                            oDetalle.link = detalle.link;
                                            oDetalle.cargoFijo = detalle.cargoFijo;
                                            oDetalle.cargoVariable = detalle.cargoVariable;
                                            oDetalle.idMoneda = detalle.idMoneda;
                                            oDetalle.idMedida = detalle.idMedida;
                                            oDetalle.cantidadContratada = detalle.cantidadContratada;
                                            oDetalle.frecuenciaDeFacturacion = detalle.frecuenciaDeFacturacion;

                                            Modelo.SubmitChanges();
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                        }


                        //registrar contadores
                        if (listadoContadores != null)
                        {
                            if (listadoContadores.Count > 0)
                            {
                                #region Registrar listado detalle de documentos() 
                                foreach (var item in listadoContadores)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultadoConsulta.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;
                                            Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultadoConsulta.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                            recordObject = resultadoConsulta.Single();
                                            //recordObject.codigoDispositivo = item.codigoDispositivo;
                                            //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                            recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;

                                            //Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }


                        //registrar mantenimientos por dispositivo
                        if (listadoMantenimientos != null)
                        {
                            if (listadoMantenimientos.Count > 0)
                            {
                                #region Registrar listado detalle mantenimiento () 
                                foreach (var item in listadoMantenimientos)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultadoConsulta.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                            //codigoDispositivo, item, codigoTipoManteniento, codigoColaborador, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario, codigoOrdenTrabajo
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                            recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                            recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultadoConsulta.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                            recordObject = resultadoConsulta.Single();
                                            //recordObject.codigoDispositivo = item.codigoDispositivo;
                                            //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                            recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                            recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }

                        //registrar movimiento almacen () 
                        if (listadoMovimientoAlmacen != null)
                        {
                            if (listadoMovimientoAlmacen.Count > 0)
                            {
                                #region Registrar listado detalle movimiento almacen () 
                                foreach (var item in listadoMovimientoAlmacen)
                                {
                                    #region 
                                    var resultadoConsulta = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultadoConsulta.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            recordObject.codigoDispositivo = item.codigoDispositivo;
                                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                            recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                            recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultadoConsulta.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                            recordObject = resultadoConsulta.Single();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            // recordObject.codigoDispositivo = item.codigoDispositivo;
                                            // recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                            recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                            recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                            recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                            // Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                            Modelo.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }



                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        // este es 26.12.2023
        public int ToRegister(string Connection, SAS_Dispostivo device,
            List<SAS_DispositivoIP> listOfDeletedIPs, List<SAS_DispositivoIP> listOfIPs,
            List<SAS_DispositivoUsuarios> listadoColaboradoresEliminados, List<SAS_DispositivoUsuarios> listadoColaboradores,
            List<SAS_DispositivoHardware> listadoHardwareEliminados, List<SAS_DispositivoHardware> listadoHardware,
            List<SAS_DispositivoSoftware> listadoSoftwareEliminados, List<SAS_DispositivoSoftware> listadoSoftware,
            List<SAS_DispositivoComponentes> listadoComponentesEliminados, List<SAS_DispositivoComponentes> listadoComponentes,
            List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuariosEliminados, List<SAS_DispositivoCuentaUsuarios> listadoCuentasUsuarios,
            List<SAS_DispositivoDocumento> listadoDocumentosEliminados, List<SAS_DispositivoDocumento> listadoDocumentos,
            List<SAS_DispositivoContadores> listadoContadoresEliminados, List<SAS_DispositivoContadores> listadoContadores,
            List<SAS_DispositivoMovimientoMantenimientos> listadoMantenimientosEliminados, List<SAS_DispositivoMovimientoMantenimientos> listadoMantenimientos,
            List<SAS_DispositivoMovimientoAlmacen> listadoMovimientoAlmacenEliminados, List<SAS_DispositivoMovimientoAlmacen> listadoMovimientoAlmacen,
            List<SAS_DispositivoImagene> ListadoImagenesEliminadas, List<SAS_DispositivoImagene> ListadoImagenesRegistro)

        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                //using (TransactionScope Scope = new TransactionScope())
                //{
                if (resultado.ToList().Count == 0)
                {
                    #region Registrar() 
                    #region Dispositivo()
                    SAS_Dispostivo oDevice = new SAS_Dispostivo();
                    oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                    oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;
                    oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                    oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                    oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                    oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                    oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;
                    oDevice.estado = 1;
                    oDevice.fechacreacion = DateTime.Now;
                    oDevice.creadoPor = Environment.MachineName.ToString() + " | " + Environment.UserName;
                    oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                    oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                    oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                    oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                    oDevice.imagen = device.imagen != null ? device.imagen : null;
                    oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                    oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                    oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                    oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : 'X';
                    oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                    oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                    oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                    oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                    oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                    oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                    oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                    oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                    oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                    oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                    oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;

                    oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                    oDevice.ubicacion = device.ubicacion;
                    oDevice.costoUSD = device.costoUSD;
                    oDevice.lineaCelular = device.lineaCelular;

                    oDevice.idSistemaDeImpresion = device.idSistemaDeImpresion;
                    oDevice.costoMantenimientoAnualUSD = device.costoMantenimientoAnualUSD;
                    oDevice.costoSuministroAnualUSD = device.costoSuministroAnualUSD;

                    oDevice.kilovatioHora = device.kilovatioHora.Value;
                    oDevice.tipoDeFacturacionDeConsumoEnergético = device.tipoDeFacturacionDeConsumoEnergético;

                    Modelo.SAS_Dispostivo.InsertOnSubmit(oDevice);
                    Modelo.SubmitChanges();
                    #endregion
                    codigo = oDevice.id;

                    //registrar Número de IP
                    if (listOfIPs != null)
                    {
                        if (listOfIPs.Count > 0)
                        {
                            #region Registrar listado detalle de IPs. 

                            foreach (var detalle in listOfIPs)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                        oIp.dispositivoCodigo = codigo;
                                        oIp.item = detalle.item;
                                        oIp.estado = detalle.estado;
                                        oIp.direcionMAC = detalle.direcionMAC;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        oIp.fechaCreacion = DateTime.Now;
                                        oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        oIp.idIP = detalle.idIP;
                                        Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                        oIp = result1.Single();
                                        oIp.estado = detalle.estado;
                                        oIp.direcionMAC = detalle.direcionMAC;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        //oIp.fechaCreacion = DateTime.Now;
                                        //oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        oIp.idIP = detalle.idIP;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar Colaboradores
                    if (listadoColaboradores != null)
                    {
                        if (listadoColaboradores.Count > 0)
                        {
                            #region Registrar listado detalle de IPs. 

                            foreach (var detalle in listadoColaboradores)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                        oIp.dispositivoCodigo = codigo;
                                        oIp.item = detalle.item;
                                        oIp.estado = detalle.estado;
                                        oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        oIp.fechaCreacion = DateTime.Now;
                                        oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                        oIp = result1.Single();
                                        oIp.estado = detalle.estado;
                                        oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        //oIp.fechaCreacion = DateTime.Now;
                                        //oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }


                    //registrar Hardware
                    if (listadoHardware != null)
                    {
                        if (listadoHardware.Count > 0)
                        {
                            #region Registrar listado detalle Hardware()  

                            foreach (var detalle in listadoHardware)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoHardware = detalle.codigoHardware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.capacidad = detalle.capacidad;
                                        oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoHardware = detalle.codigoHardware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.capacidad = detalle.capacidad;
                                        oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }


                    //registrar Software
                    if (listadoSoftware != null)
                    {
                        if (listadoSoftware.Count > 0)
                        {
                            #region Registrar listado detalle de Software() 

                            foreach (var detalle in listadoSoftware)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoSoftware = detalle.codigoSoftware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.version = detalle.version;
                                        oDetalle.informacionAdicional = detalle.informacionAdicional;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoSoftware = detalle.codigoSoftware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.version = detalle.version;
                                        oDetalle.informacionAdicional = detalle.informacionAdicional;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar Componentes
                    if (listadoComponentes != null)
                    {
                        if (listadoComponentes.Count > 0)
                        {
                            #region Registrar listado detalle de listadoComponentes() 

                            foreach (var detalle in listadoComponentes)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar cuentas de usuario
                    if (listadoCuentasUsuarios != null)
                    {
                        if (listadoCuentasUsuarios.Count > 0)
                        {
                            #region Registrar listado detalle de cuentas de usuario() 

                            foreach (var detalle in listadoCuentasUsuarios)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;


                                        oDetalle.clave = detalle.clave;
                                        oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                        oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                        oDetalle.cuenta = detalle.cuenta;
                                        Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        oDetalle.clave = detalle.clave;
                                        oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                        oDetalle.cuenta = detalle.cuenta;
                                        oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;


                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar documentos
                    if (listadoDocumentos != null)
                    {
                        if (listadoDocumentos.Count > 0)
                        {
                            #region Registrar listado detalle de documentos() 

                            foreach (var detalle in listadoDocumentos)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.link = detalle.link;

                                        Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.link = detalle.link;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar contadores
                    if (listadoContadores != null)
                    {
                        if (listadoContadores.Count > 0)
                        {
                            #region Registrar listado detalle de documentos() 
                            foreach (var item in listadoContadores)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultado.ToList().Count == 0)
                                    {
                                        #region Nuevo();


                                        SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                        recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                        recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;

                                        recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                        recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                        recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;


                                        Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultado.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                        recordObject = resultadoConsulta.Single();
                                        //recordObject.codigoDispositivo = item.codigoDispositivo;
                                        //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                        recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                        recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                        recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                        recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;

                                        //Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }


                    //registrar mantenimientos por dispositivo
                    if (listadoMantenimientos != null)
                    {
                        if (listadoMantenimientos.Count > 0)
                        {
                            #region Registrar listado detalle mantenimiento () 
                            foreach (var item in listadoMantenimientos)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultado.ToList().Count == 0)
                                    {
                                        #region Nuevo();
                                        SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                        //codigoDispositivo, item, codigoTipoManteniento, codigoColaborador, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario, codigoOrdenTrabajo
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                        recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                        recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultado.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                        recordObject = resultadoConsulta.Single();
                                        //recordObject.codigoDispositivo = item.codigoDispositivo;
                                        //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                        recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                        recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }

                    //registrar movimiento almacen () 
                    if (listadoMovimientoAlmacen != null)
                    {
                        if (listadoMovimientoAlmacen.Count > 0)
                        {
                            #region Registrar listado detalle movimiento almacen () 
                            foreach (var item in listadoMovimientoAlmacen)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultado.ToList().Count == 0)
                                    {
                                        #region Nuevo();
                                        SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                        // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                        recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                        recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultado.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                        recordObject = resultadoConsulta.Single();
                                        // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                        // recordObject.codigoDispositivo = item.codigoDispositivo;
                                        // recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                        recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                        recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        // Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }

                    using (ITDContextDataContext modelImage = new ITDContextDataContext(cnx))
                    {
                        //registrar Imagenes () 
                        if (ListadoImagenesRegistro != null)
                        {
                            if (ListadoImagenesRegistro.Count > 0)
                            {
                                #region Registrar listado detalle movimiento almacen () 
                                foreach (var item in ListadoImagenesRegistro)
                                {
                                    #region 
                                    var resultadoConsulta = modelImage.SAS_DispositivoImagenes.Where(x => x.DispositivoId == item.DispositivoId && x.Item == item.Item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultado.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoImagene recordObject = new SAS_DispositivoImagene();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            recordObject.DispositivoId = item.DispositivoId;
                                            recordObject.Item = item.Item != (int?)null ? item.Item : 0;
                                            recordObject.EsPrincipal = item.EsPrincipal != (byte?)null ? item.EsPrincipal : Convert.ToByte(0);
                                            recordObject.Ruta = item.Ruta != string.Empty ? item.Ruta : string.Empty;
                                            recordObject.Fecha = item.Fecha != (DateTime?)null ? item.Fecha : DateTime.Now;
                                            recordObject.Latitud = item.Latitud != string.Empty ? item.Latitud : string.Empty;
                                            recordObject.Longitud = item.Longitud != string.Empty ? item.Longitud : string.Empty;
                                            recordObject.Nota = item.Nota != string.Empty ? item.Nota : string.Empty;
                                            recordObject.Estado = item.Estado != (byte?)null ? item.Estado : Convert.ToByte(0);
                                            modelImage.SAS_DispositivoImagenes.InsertOnSubmit(recordObject);
                                            modelImage.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultado.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoImagene recordObject = new SAS_DispositivoImagene();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            //recordObject.DispositivoId = item.DispositivoId;
                                            //recordObject.Item = item.Item != (int?)null ? item.Item : 0;
                                            recordObject.EsPrincipal = item.EsPrincipal != (byte?)null ? item.EsPrincipal : Convert.ToByte(0);
                                            recordObject.Ruta = item.Ruta != string.Empty ? item.Ruta : string.Empty;
                                            recordObject.Fecha = item.Fecha != (DateTime?)null ? item.Fecha : DateTime.Now;
                                            recordObject.Latitud = item.Latitud != string.Empty ? item.Latitud : string.Empty;
                                            recordObject.Longitud = item.Longitud != string.Empty ? item.Longitud : string.Empty;
                                            recordObject.Nota = item.Nota != string.Empty ? item.Nota : string.Empty;
                                            recordObject.Estado = item.Estado != (byte?)null ? item.Estado : Convert.ToByte(0);
                                            //modelImage.SAS_DispositivoImagenes.InsertOnSubmit(recordObject);
                                            modelImage.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion
                }
                else if (resultado.ToList().Count == 1)
                {
                    #region Editar() 
                    #region Dispositivo() 
                    SAS_Dispostivo oDevice = new SAS_Dispostivo();
                    oDevice = resultado.Single();
                    oDevice.nombres = device.nombres != null ? device.nombres.Trim() : string.Empty;
                    oDevice.descripcion = device.descripcion != null ? device.descripcion.Trim() : string.Empty;

                    oDevice.latitud = device.latitud != null ? device.latitud.Trim() : string.Empty;
                    oDevice.longitud = device.longitud != null ? device.longitud.Trim() : string.Empty;

                    oDevice.sedeCodigo = device.sedeCodigo != null ? device.sedeCodigo.Trim() : string.Empty;
                    oDevice.numeroSerie = device.numeroSerie != null ? device.numeroSerie.Trim() : string.Empty;
                    oDevice.caracteristicas = device.caracteristicas != null ? device.caracteristicas.Trim() : string.Empty;
                    oDevice.activoCodigoERP = device.activoCodigoERP != null ? device.activoCodigoERP.Trim() : string.Empty;
                    oDevice.tipoDispositivoCodigo = device.tipoDispositivoCodigo != null ? device.tipoDispositivoCodigo.Trim() : string.Empty;
                    oDevice.IdDispostivoColor = device.IdDispostivoColor != null ? device.IdDispostivoColor.Trim() : string.Empty;
                    oDevice.idModelo = device.idModelo != null ? device.idModelo.Trim() : string.Empty;
                    oDevice.idMarca = device.idMarca != null ? device.idMarca.Trim() : string.Empty;
                    oDevice.numeroParte = device.numeroParte != null ? device.numeroParte.Trim() : string.Empty;
                    oDevice.IdEstadoProducto = device.IdEstadoProducto != null ? Convert.ToChar(device.IdEstadoProducto.ToString().Trim()) : Convert.ToChar('X');
                    oDevice.EsPropio = device.EsPropio != null ? Convert.ToByte(device.EsPropio.Value) : Convert.ToByte(1);
                    oDevice.idProducto = device.idProducto != null ? device.idProducto.Trim() : string.Empty;
                    oDevice.rutaImagen = device.rutaImagen != null ? device.rutaImagen.Trim() : string.Empty;
                    oDevice.funcionamiento = device.funcionamiento != null ? device.funcionamiento.Value : 0;
                    oDevice.idClieprov = device.idClieprov != null ? device.idClieprov.Trim() : string.Empty;
                    oDevice.coordenada = device.coordenada != null ? device.coordenada.Trim() : string.Empty;
                    oDevice.fechaActivacion = device.fechaActivacion != null ? device.fechaActivacion.Value : (DateTime?)null;
                    oDevice.idCobrarpagarDoc = device.idCobrarpagarDoc != null ? device.idCobrarpagarDoc.Trim() : string.Empty;
                    oDevice.fechaBaja = device.fechaBaja != null ? device.fechaBaja.Value : (DateTime?)null;
                    oDevice.fechaProduccion = device.fechaProduccion != null ? device.fechaProduccion.Value : (DateTime?)null;
                    oDevice.esFinal = device.esFinal != null ? device.esFinal.Value : 0;
                    oDevice.idArea = device.idArea != null ? device.idArea.Trim() : "010";
                    oDevice.imagen = device.imagen != null ? device.imagen : null;

                    oDevice.AnioParaDepreciar = device.AnioParaDepreciar;
                    oDevice.ubicacion = device.ubicacion;
                    oDevice.costoUSD = device.costoUSD;
                    oDevice.lineaCelular = device.lineaCelular;

                    oDevice.idSistemaDeImpresion = device.idSistemaDeImpresion;
                    oDevice.costoMantenimientoAnualUSD = device.costoMantenimientoAnualUSD;
                    oDevice.costoSuministroAnualUSD = device.costoSuministroAnualUSD;


                    oDevice.kilovatioHora = device.kilovatioHora.Value;
                    oDevice.tipoDeFacturacionDeConsumoEnergético = device.tipoDeFacturacionDeConsumoEnergético;

                    Modelo.SubmitChanges();
                    #endregion
                    codigo = oDevice.id;

                    // Eliminar lista de eliminados de los ips por device.
                    if (listOfDeletedIPs != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listOfDeletedIPs.Count > 0)
                        {
                            foreach (var detalle in listOfDeletedIPs)
                            {
                                var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                        oIp = result1.Single();
                                        Modelo.SAS_DispositivoIP.DeleteOnSubmit(oIp);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de USER  POR device.
                    if (listadoColaboradoresEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoColaboradoresEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoColaboradoresEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoUsuarios oDetalle = new SAS_DispositivoUsuarios();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de hardware.
                    if (listadoHardwareEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoHardwareEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoHardwareEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoHardware.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de software.
                    if (listadoSoftwareEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoSoftwareEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoSoftwareEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoSoftware.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de componentes.
                    if (listadoComponentesEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoComponentesEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoComponentesEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoComponentes.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de cuentas de usuario.
                    if (listadoCuentasUsuariosEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoCuentasUsuariosEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoCuentasUsuariosEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoCuentaUsuarios.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de documentos.
                    if (listadoDocumentosEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoDocumentosEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoDocumentosEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoDocumento.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Add el 14.04.2022
                    // Eliminar lista de eliminados de detalle contadores() 
                    if (listadoContadoresEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoContadoresEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoContadoresEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoContadores oDetalle = new SAS_DispositivoContadores();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoContadores.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de detalle mantenimientos() 
                    if (listadoMantenimientosEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoMantenimientosEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoMantenimientosEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoMovimientoMantenimientos oDetalle = new SAS_DispositivoMovimientoMantenimientos();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // Eliminar lista de eliminados de detalle movimiento almacen() 
                    if (listadoMovimientoAlmacenEliminados != null)
                    {
                        #region Eliminar lista de Ip para eliminar() 
                        if (listadoMovimientoAlmacenEliminados.Count > 0)
                        {
                            foreach (var detalle in listadoMovimientoAlmacenEliminados)
                            {
                                var result1 = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 1)
                                    {
                                        SAS_DispositivoMovimientoAlmacen oDetalle = new SAS_DispositivoMovimientoAlmacen();
                                        oDetalle = result1.Single();
                                        Modelo.SAS_DispositivoMovimientoAlmacen.DeleteOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    // 27.12.2023
                    using (ITDContextDataContext modelImage = new ITDContextDataContext(cnx))
                    {
                        if (listadoMovimientoAlmacenEliminados != null)
                        {
                            #region Eliminar lista de Imagenes para eliminar() 
                            if (ListadoImagenesEliminadas.Count > 0)
                            {
                                foreach (var detalle in ListadoImagenesEliminadas)
                                {
                                    var result1 = modelImage.SAS_DispositivoImagenes.Where(x => x.DispositivoId == codigo && x.Item == detalle.Item).ToList();
                                    if (result1 != null)
                                    {
                                        if (result1.ToList().Count == 1)
                                        {
                                            SAS_DispositivoImagene oDetalle = new SAS_DispositivoImagene();
                                            oDetalle = result1.Single();
                                            modelImage.SAS_DispositivoImagenes.DeleteOnSubmit(oDetalle);
                                            modelImage.SubmitChanges();
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }



                    // Modificar y registrar listado de Ip
                    if (listOfIPs != null)
                    {
                        #region Editar y registrar listado de IP                            
                        if (listOfIPs.Count > 0)
                        {
                            #region Registrar listado detalle de IPs. 
                            foreach (var detalle in listOfIPs)
                            {
                                var result1 = Modelo.SAS_DispositivoIP.Where(x => x.dispositivoCodigo == codigo && x.item.Trim() == detalle.item.Trim()).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                        oIp.dispositivoCodigo = codigo;
                                        oIp.item = detalle.item;
                                        oIp.estado = detalle.estado;
                                        oIp.direcionMAC = detalle.direcionMAC;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        oIp.fechaCreacion = DateTime.Now;
                                        oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        oIp.idIP = detalle.idIP;
                                        Modelo.SAS_DispositivoIP.InsertOnSubmit(oIp);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoIP oIp = new SAS_DispositivoIP();
                                        oIp = result1.Single();
                                        oIp.estado = detalle.estado;
                                        oIp.direcionMAC = detalle.direcionMAC;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        //oIp.fechaCreacion = DateTime.Now;
                                        //oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        oIp.idIP = detalle.idIP;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                            }

                            #endregion
                        }
                        #endregion
                    }

                    // Modificar y registrar listado de usuarios
                    if (listadoColaboradores != null)
                    {
                        if (listadoColaboradores.Count > 0)
                        {
                            #region Registrar listado detalle de IPs. 

                            foreach (var detalle in listadoColaboradores)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                        oIp.dispositivoCodigo = codigo;
                                        oIp.item = detalle.item;
                                        oIp.estado = detalle.estado;
                                        oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        oIp.fechaCreacion = DateTime.Now;
                                        oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oIp);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoUsuarios oIp = new SAS_DispositivoUsuarios();
                                        oIp = result1.Single();
                                        oIp.estado = detalle.estado;
                                        oIp.idcodigoGeneral = detalle.idcodigoGeneral;
                                        oIp.hasta = detalle.hasta;
                                        oIp.desde = detalle.desde;
                                        oIp.observacion = detalle.observacion;
                                        //oIp.fechaCreacion = DateTime.Now;
                                        //oIp.registradoPor = Environment.UserName;
                                        oIp.tipo = detalle.tipo;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }


                    // Modificar y registrar Hardware
                    if (listadoHardware != null)
                    {
                        if (listadoHardware.Count > 0)
                        {
                            #region Registrar listado detalle Hardware()  

                            foreach (var detalle in listadoHardware)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoHardware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoHardware = detalle.codigoHardware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.capacidad = detalle.capacidad;
                                        oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SAS_DispositivoHardware.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoHardware oDetalle = new SAS_DispositivoHardware();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoHardware = detalle.codigoHardware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.capacidad = detalle.capacidad;
                                        oDetalle.unidadMedidaCapacidad = detalle.unidadMedidaCapacidad;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }


                    // Modificar y registrar Software
                    if (listadoSoftware != null)
                    {
                        if (listadoSoftware.Count > 0)
                        {
                            #region Registrar listado detalle de Software() 

                            foreach (var detalle in listadoSoftware)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoSoftware.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoSoftware = detalle.codigoSoftware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.version = detalle.version;
                                        oDetalle.informacionAdicional = detalle.informacionAdicional;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SAS_DispositivoSoftware.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoSoftware oDetalle = new SAS_DispositivoSoftware();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoSoftware = detalle.codigoSoftware;
                                        oDetalle.serie = detalle.serie;
                                        oDetalle.version = detalle.version;
                                        oDetalle.informacionAdicional = detalle.informacionAdicional;
                                        oDetalle.numeroParte = detalle.numeroParte;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar Componentes
                    if (listadoComponentes != null)
                    {
                        if (listadoComponentes.Count > 0)
                        {
                            #region Registrar listado detalle de listadoComponentes() 

                            foreach (var detalle in listadoComponentes)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoComponentes.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SAS_DispositivoComponentes.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoComponentes oDetalle = new SAS_DispositivoComponentes();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoDispositivoComponente = detalle.codigoDispositivoComponente;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar cuentas de usuario
                    if (listadoCuentasUsuarios != null)
                    {
                        if (listadoCuentasUsuarios.Count > 0)
                        {
                            #region Registrar listado detalle de cuentas de usuario() 

                            foreach (var detalle in listadoCuentasUsuarios)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoCuentaUsuarios.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        oDetalle.clave = detalle.clave;
                                        oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                        oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                        oDetalle.cuenta = detalle.cuenta;
                                        Modelo.SAS_DispositivoCuentaUsuarios.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoCuentaUsuarios oDetalle = new SAS_DispositivoCuentaUsuarios();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoTipoCuenta = detalle.codigoTipoCuenta;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.seVisualizaEnReportes = detalle.seVisualizaEnReportes;
                                        oDetalle.clave = detalle.clave;
                                        oDetalle.correoDeRecuperacion = detalle.correoDeRecuperacion;
                                        oDetalle.cuenta = detalle.cuenta;
                                        oDetalle.NumeroTelefonoRecuperacion = detalle.NumeroTelefonoRecuperacion;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }

                    //registrar documentos
                    if (listadoDocumentos != null)
                    {
                        if (listadoDocumentos.Count > 0)
                        {
                            #region Registrar listado detalle de documentos() 

                            foreach (var detalle in listadoDocumentos)
                            {
                                #region 
                                var result1 = Modelo.SAS_DispositivoDocumento.Where(x => x.codigoDispositivo == codigo && x.item == detalle.item).ToList();
                                if (result1 != null)
                                {
                                    if (result1.ToList().Count == 0)
                                    {
                                        #region Nuevo()
                                        SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                        oDetalle.codigoDispositivo = codigo;
                                        oDetalle.item = detalle.item;
                                        oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.link = detalle.link;
                                        oDetalle.cargoFijo = detalle.cargoFijo;
                                        oDetalle.cargoVariable = detalle.cargoVariable;
                                        oDetalle.idMoneda = detalle.idMoneda;
                                        oDetalle.idMedida = detalle.idMedida;
                                        oDetalle.cantidadContratada = detalle.cantidadContratada;
                                        oDetalle.frecuenciaDeFacturacion = detalle.frecuenciaDeFacturacion;
                                        Modelo.SAS_DispositivoDocumento.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result1.ToList().Count == 1)
                                    {
                                        #region Editar()
                                        SAS_DispositivoDocumento oDetalle = new SAS_DispositivoDocumento();
                                        oDetalle = result1.Single();
                                        oDetalle.codigoTipoDocumento = detalle.codigoTipoDocumento;
                                        oDetalle.observacion = detalle.observacion;
                                        oDetalle.hasta = detalle.hasta;
                                        oDetalle.desde = detalle.desde;
                                        oDetalle.estado = detalle.estado;
                                        oDetalle.link = detalle.link;
                                        oDetalle.cargoFijo = detalle.cargoFijo;
                                        oDetalle.cargoVariable = detalle.cargoVariable;
                                        oDetalle.idMoneda = detalle.idMoneda;
                                        oDetalle.idMedida = detalle.idMedida;
                                        oDetalle.cantidadContratada = detalle.cantidadContratada;
                                        oDetalle.frecuenciaDeFacturacion = detalle.frecuenciaDeFacturacion;

                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            #endregion
                        }
                    }


                    //registrar contadores
                    if (listadoContadores != null)
                    {
                        if (listadoContadores.Count > 0)
                        {
                            #region Registrar listado detalle de documentos() 
                            foreach (var item in listadoContadores)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultadoConsulta.ToList().Count == 0)
                                    {
                                        #region Nuevo();
                                        SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                        recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                        recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                        recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;
                                        Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultadoConsulta.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                                        recordObject = resultadoConsulta.Single();
                                        //recordObject.codigoDispositivo = item.codigoDispositivo;
                                        //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        //recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                                        recordObject.periodo = item.desde != (DateTime?)null ? item.desde.Value.ToString("ddMMyyyy") : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                                        recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                                        recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                                        recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;

                                        //Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }


                    //registrar mantenimientos por dispositivo
                    if (listadoMantenimientos != null)
                    {
                        if (listadoMantenimientos.Count > 0)
                        {
                            #region Registrar listado detalle mantenimiento () 
                            foreach (var item in listadoMantenimientos)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoMovimientoMantenimientos.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultadoConsulta.ToList().Count == 0)
                                    {
                                        #region Nuevo();
                                        SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                        //codigoDispositivo, item, codigoTipoManteniento, codigoColaborador, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario, codigoOrdenTrabajo
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                        recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                        recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultadoConsulta.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoMovimientoMantenimientos recordObject = new SAS_DispositivoMovimientoMantenimientos();
                                        recordObject = resultadoConsulta.Single();
                                        //recordObject.codigoDispositivo = item.codigoDispositivo;
                                        //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.codigoTipoManteniento = item.codigoTipoManteniento != string.Empty ? item.codigoTipoManteniento : string.Empty;
                                        recordObject.codigoColaborador = item.codigoColaborador != string.Empty ? item.codigoColaborador : string.Empty;
                                        recordObject.codigoOrdenTrabajo = item.codigoOrdenTrabajo != (decimal?)null ? item.codigoOrdenTrabajo : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        //Modelo.SAS_DispositivoMovimientoMantenimientos.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }

                    //registrar movimiento almacen () 
                    if (listadoMovimientoAlmacen != null)
                    {
                        if (listadoMovimientoAlmacen.Count > 0)
                        {
                            #region Registrar listado detalle movimiento almacen () 
                            foreach (var item in listadoMovimientoAlmacen)
                            {
                                #region 
                                var resultadoConsulta = Modelo.SAS_DispositivoMovimientoAlmacen.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                                if (resultadoConsulta != null)
                                {
                                    #region Registro | Actualizacion() 
                                    if (resultadoConsulta.ToList().Count == 0)
                                    {
                                        #region Nuevo();
                                        SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                        // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                        recordObject.codigoDispositivo = item.codigoDispositivo;
                                        recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                        recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                        recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                    else if (resultadoConsulta.ToList().Count == 1)
                                    {
                                        #region Actualizar()
                                        SAS_DispositivoMovimientoAlmacen recordObject = new SAS_DispositivoMovimientoAlmacen();
                                        recordObject = resultadoConsulta.Single();
                                        // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                        // recordObject.codigoDispositivo = item.codigoDispositivo;
                                        // recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                                        recordObject.idMovimientoAlmacen = item.idMovimientoAlmacen != string.Empty ? item.idMovimientoAlmacen : string.Empty;
                                        recordObject.itemDocAlmacen = item.itemDocAlmacen != string.Empty ? item.itemDocAlmacen : string.Empty;
                                        recordObject.idproducto = item.idproducto != string.Empty ? item.idproducto : string.Empty;
                                        recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad.Value : 0;
                                        recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                                        recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                                        recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                                        recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                                        recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                                        recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                                        // Modelo.SAS_DispositivoMovimientoAlmacen.InsertOnSubmit(recordObject);
                                        Modelo.SubmitChanges();
                                        #endregion

                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }

                    using (ITDContextDataContext modelImage = new ITDContextDataContext(cnx))
                    {
                        //registrar Imagenes () 
                        if (ListadoImagenesRegistro != null)
                        {
                            if (ListadoImagenesRegistro.Count > 0)
                            {
                                #region Registrar listado detalle movimiento almacen () 
                                foreach (var item in ListadoImagenesRegistro)
                                {
                                    #region 
                                    var resultadoConsulta = modelImage.SAS_DispositivoImagenes.Where(x => x.DispositivoId == item.DispositivoId && x.Item == item.Item).ToList();
                                    if (resultadoConsulta != null)
                                    {
                                        #region Registro | Actualizacion() 
                                        if (resultadoConsulta.ToList().Count == 0)
                                        {
                                            #region Nuevo();
                                            SAS_DispositivoImagene recordObject = new SAS_DispositivoImagene();
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            recordObject.DispositivoId = item.DispositivoId;
                                            // recordObject.Item = item.Item != (int?)null ? item.Item : 0;
                                            recordObject.EsPrincipal = item.EsPrincipal != (byte?)null ? item.EsPrincipal : Convert.ToByte(0);
                                            recordObject.Ruta = item.Ruta != string.Empty ? item.Ruta : string.Empty;
                                            recordObject.Fecha = item.Fecha != (DateTime?)null ? item.Fecha : DateTime.Now;
                                            recordObject.Latitud = item.Latitud != string.Empty ? item.Latitud : string.Empty;
                                            recordObject.Longitud = item.Longitud != string.Empty ? item.Longitud : string.Empty;
                                            recordObject.Nota = item.Nota != string.Empty ? item.Nota : string.Empty;
                                            recordObject.Estado = item.Estado != (byte?)null ? item.Estado : Convert.ToByte(0);
                                            modelImage.SAS_DispositivoImagenes.InsertOnSubmit(recordObject);
                                            modelImage.SubmitChanges();

                                            #endregion
                                        }
                                        else if (resultadoConsulta.ToList().Count == 1)
                                        {
                                            #region Actualizar()
                                            SAS_DispositivoImagene recordObject = new SAS_DispositivoImagene();
                                            recordObject = resultadoConsulta.ElementAt(0);
                                            // codigoDispositivo, item, idMovimientoAlmacen, itemDocAlmacen, idproducto, cantidad, desde, hasta, observacion, estado, seVisualizaEnReportes, usuario
                                            //recordObject.DispositivoId = item.DispositivoId;
                                            //recordObject.Item = item.Item != (int?)null ? item.Item : 0;
                                            recordObject.EsPrincipal = item.EsPrincipal != (byte?)null ? item.EsPrincipal : Convert.ToByte(0);
                                            recordObject.Ruta = item.Ruta != string.Empty ? item.Ruta : string.Empty;
                                            // recordObject.Fecha = item.Fecha != (DateTime?)null ? item.Fecha : DateTime.Now;
                                            recordObject.Latitud = item.Latitud != string.Empty ? item.Latitud : string.Empty;
                                            recordObject.Longitud = item.Longitud != string.Empty ? item.Longitud : string.Empty;
                                            recordObject.Nota = item.Nota != string.Empty ? item.Nota : string.Empty;
                                            recordObject.Estado = item.Estado != (byte?)null ? item.Estado : Convert.ToByte(0);
                                            //modelImage.SAS_DispositivoImagenes.InsertOnSubmit(recordObject);
                                            modelImage.SubmitChanges();
                                            #endregion

                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }
                    }



                    #endregion
                }
                //  Scope.Complete();
                //}
            }
            return codigo;
        }

        public Int32 Unregister(string Connection, SAS_Dispostivo device)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        if (oDevice.estado == 1)
                        {
                            oDevice.estado = 0;
                        }
                        else
                        {
                            oDevice.estado = 1;
                        }
                        Modelo.SubmitChanges();
                        codigo = Convert.ToInt32(oDevice.id);
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        public Int32 GetCountReferencias(string Connection, SAS_Dispostivo device)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_ListadoReferenciaDeDispositivosEnGestionTI(device.id).ToList();

                if (resultado != null)
                {
                    if (resultado.ToList().Count > 0)
                    {
                        codigo = resultado.ToList().Count;
                    }
                }

            }
            return codigo;
        }

        public List<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult> ObtainListOfReferenceDocumentsbyDeviceCode(string Connection, int idDispositivo)
        {
            string cnx = string.Empty;
            List<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult> listado = new List<SAS_ListadoReferenciaDeDispositivosEnGestionTIResult>();
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                Modelo.CommandTimeout = 999999;
                listado = Modelo.SAS_ListadoReferenciaDeDispositivosEnGestionTI(idDispositivo).ToList();
            }
            return listado;
        }

        public Int32 DeleteDevice(string Connection, SAS_Dispostivo device)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == device.id).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        Modelo.SAS_Dispostivo.DeleteOnSubmit(oDevice);
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;

                        codigo = Convert.ToInt32(device.id);
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }

        // Obtener detalle de IP por código de dispositivo
        public List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult> DetalleDeDispositivosPorIPByCodigoDispositivo(string Connection, SAS_Dispostivo device)
        {
            List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult> resultado = new List<SAS_DetalleDeDispositivosPorIPByCodigoDispositivoResult>();

            if (device != null)
            {
                if (device.id != null)
                {
                    string cnx = string.Empty;
                    cnx = ConfigurationManager.AppSettings[Connection].ToString();
                    using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
                    {
                        resultado = Modelo.SAS_DetalleDeDispositivosPorIPByCodigoDispositivo(device.id).ToList();
                    }
                }
            }


            return resultado;
        }

        public List<DFormatoSimple> GetTypeOfSegments(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfSegments = new List<SAS_SegmentoRed>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                typeOfSegments = Modelo.SAS_SegmentoRed.Where(x => x.estado == 1).ToList();
                listado = (from segment in typeOfSegments

                           select new DFormatoSimple
                           {
                               Codigo = segment.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = segment.descripcion.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }

        public List<DFormatoSimple> GetTypeInterface(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfInterfaces = new List<SAS_SegmentoRed>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado.Add(new DFormatoSimple { Codigo = "F", Descripcion = "FISICO" });
                listado.Add(new DFormatoSimple { Codigo = "W", Descripcion = "WIRELESS" });
            }
            return listado;
        }

        public List<DFormatoSimple> GetCollaborators(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_ListadoColaboradoresByDispositivo> Collaborators = new List<SAS_ListadoColaboradoresByDispositivo>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                Collaborators = Modelo.SAS_ListadoColaboradoresByDispositivo.ToList();
                listado = (from item in Collaborators
                           group item by new { item.idcodigogeneral, item.apenom }
                           into j
                           select new DFormatoSimple
                           {
                               Codigo = j.Key.idcodigogeneral.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = j.Key.apenom.ToString()
                           }).ToList();
            }
            return listado;
        }

        public List<DFormatoSimple> GetALLCollaborators(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_ListadoPersonalEmpresaYExterno> Collaborators = new List<SAS_ListadoPersonalEmpresaYExterno>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                Collaborators = Modelo.SAS_ListadoPersonalEmpresaYExterno.ToList();
                listado = (from item in Collaborators
                           group item by new { item.codigo, item.nombres }
                           into j
                           select new DFormatoSimple
                           {
                               Codigo = j.Key.codigo.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = j.Key.nombres.ToString()
                           }).ToList();
            }
            return listado.OrderBy(X => X.Codigo).ToList();
        }

        public List<DFormatoSimple> GetHardwares(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivoTipoHardware> items = new List<SAS_DispositivoTipoHardware>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                items = Modelo.SAS_DispositivoTipoHardware.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }

        public List<DFormatoSimple> GetComponentesInternos(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_Dispostivo> items = new List<SAS_Dispostivo>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                items = Modelo.SAS_Dispostivo.Where(x => x.esFinal != 1).ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToString().Trim() + " | S/N: " + item.numeroSerie.Trim()
                           }).ToList();
            }
            return listado;
        }

        public List<DFormatoSimple> GetTypeUserAccount(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivoTipoCuentaUsuario> items = new List<SAS_DispositivoTipoCuentaUsuario>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                items = Modelo.SAS_DispositivoTipoCuentaUsuario.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }

        public List<DFormatoSimple> GetTypeDocumentByDevice(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivoTipoDocumento> items = new List<SAS_DispositivoTipoDocumento>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                items = Modelo.SAS_DispositivoTipoDocumento.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }

        // Tipo de frecuencia de contratos | Get list of frequencies | GetListOfFrequencies
        public List<DFormatoSimple> GetListOfFrequencies(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            DFormatoSimple item = new DFormatoSimple();
            item.Codigo = "00";
            item.Descripcion = "SIN FRECUENCIA";
            listado.Add(item);

            DFormatoSimple item1 = new DFormatoSimple();
            item1.Codigo = "01";
            item1.Descripcion = "HORAS";
            listado.Add(item1);


            DFormatoSimple item2 = new DFormatoSimple();
            item2.Codigo = "02";
            item2.Descripcion = "DIARIA";
            listado.Add(item2);

            DFormatoSimple item3 = new DFormatoSimple();
            item3.Codigo = "03";
            item3.Descripcion = "MENSUAL";
            listado.Add(item3);


            DFormatoSimple item4 = new DFormatoSimple();
            item4.Codigo = "04";
            item4.Descripcion = "TRIMESTRAL";
            listado.Add(item4);

            DFormatoSimple item5 = new DFormatoSimple();
            item5.Codigo = "05";
            item5.Descripcion = "SEMESTRAL";
            listado.Add(item5);

            DFormatoSimple item6 = new DFormatoSimple();
            item6.Codigo = "06";
            item6.Descripcion = "ANUAL";
            listado.Add(item6);

            return listado;
        }

        // GetCurrencyTypeListing || tipo de monedas
        public List<DFormatoSimple> GetCurrencyTypeListing(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<MONEDAS> items = new List<MONEDAS>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                items = Modelo.MONEDAS.Where(X => X.ESTADO == 1).ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.IDMONEDA.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.DESCRIPCION.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }


        // GetListOfUnitsOfMeasurement || Unidad de medidas
        public List<DFormatoSimple> GetListOfUnitsOfMeasurement(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<UNIMEDIDA> items = new List<UNIMEDIDA>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                items = Modelo.UNIMEDIDA.Where(x => x.ESTADO == 1).ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.IDMEDIDA.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.DESCRIPCION.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }


        public List<DFormatoSimple> GetSoftwares(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivoTipoSoftware> items = new List<SAS_DispositivoTipoSoftware>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                items = Modelo.SAS_DispositivoTipoSoftware.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }


        public List<DFormatoSimple> GetMaintenanceListing(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivosTipoMantenimiento> items = new List<SAS_DispositivosTipoMantenimiento>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                items = Modelo.SAS_DispositivosTipoMantenimiento.Where(x => x.estado != 0).ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.descripcion.ToUpper().ToString().Trim()
                           }).ToList();
            }
            return listado;
        }



        public List<DFormatoSimple> GetPeriodoParaContadores(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_ListadoPeriodoDiaResult> items = new List<SAS_ListadoPeriodoDiaResult>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                items = Modelo.SAS_ListadoPeriodoDia().ToList();



                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.periodo.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.dia.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }


        public List<DFormatoSimple> GetUnidadMedidaContadores(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<UNIMEDIDA> items = new List<UNIMEDIDA>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                items = Modelo.UNIMEDIDA.ToList();
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.IDMEDIDA.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.DESCRIPCION.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }




        public List<DFormatoSimple> GetListOfCollaboratorsAssignedToTheDevice(string conection, int idDevice)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> items = new List<SAS_ListadoColaboradoresByDispositivoByCodigoResult>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var result1 = Modelo.SAS_ListadoColaboradoresByDispositivoByCodigo(idDevice).ToList();

                if (result1 != null && result1.ToList().Count > 0)
                {
                    items = result1.Where(x => x.estado != "1").ToList();
                }
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.idCodigoGeneral.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.colaborador.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }


        public List<DFormatoSimple> GetEstadoParaVisualizacionEnReportes(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            DFormatoSimple item = new DFormatoSimple();
            item.Codigo = "0";
            item.Descripcion = "No se mostrará visible en reportes";
            listado.Add(item);

            DFormatoSimple item2 = new DFormatoSimple();
            item2.Codigo = "1";
            item2.Descripcion = "Visible en reportes";
            listado.Add(item2);


            return listado;
        }

        public List<DFormatoSimple> GetDocumentosAsociadosAlDispositivo(string conection, int codigoDispositivo)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_DispositivoDocumentoByDeviceResult> items = new List<SAS_DispositivoDocumentoByDeviceResult>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var result1 = Modelo.SAS_DispositivoDocumentoByDevice(codigoDispositivo).ToList();

                if (result1 != null && result1.ToList().Count > 0)
                {
                    items = result1.Where(x => x.idestado != 0).ToList();
                }
                listado = (from item in items

                           select new DFormatoSimple
                           {
                               Codigo = item.item.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = item.tipoDocumento.ToString().Trim() + " del " + item.desde.Value.ToShortDateString() + " al " + item.hasta.Value.ToShortDateString()
                           }).ToList();
            }
            return listado;
        }

        // SAS_DispositivoaccountantsByDeviceID


        public List<DFormatoSimple> GetNumberOfIpsPerSegment(string conection, string segmentoCodigo)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_ListadoNumeroIPBySegmento> ipNumbersPerSegment = new List<SAS_ListadoNumeroIPBySegmento>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                ipNumbersPerSegment = Modelo.SAS_ListadoNumeroIPBySegmento.Where(x => x.segmentoCodigo == segmentoCodigo).ToList();
                listado = (from segment in ipNumbersPerSegment

                           select new DFormatoSimple
                           {
                               Codigo = segment.codigoIp.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = segment.numeroIP.ToString().Trim()
                           }).ToList();
            }
            return listado;
        }

        // Obtener detalle de usuarios.
        public List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> ListadoColaboradoresByDispositivoByCodigo(string Connection, int codigo)
        {
            List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> resultado = new List<SAS_ListadoColaboradoresByDispositivoByCodigoResult>();

            if (codigo > 0)
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[Connection].ToString();
                using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
                {
                    resultado = Modelo.SAS_ListadoColaboradoresByDispositivoByCodigo(codigo).ToList();
                }

            }


            return resultado;
        }

        public SAS_ListadoDeDispositivosByIdDeviceResult GetDeviceByIdDevice(string conection, int idDispositivo)
        {
            List<SAS_ListadoDeDispositivosByIdDeviceResult> resultado = new List<SAS_ListadoDeDispositivosByIdDeviceResult>();
            SAS_ListadoDeDispositivosByIdDeviceResult resultado2 = new SAS_ListadoDeDispositivosByIdDeviceResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeDispositivosByIdDevice(idDispositivo).ToList();
                if (resultado.ToList().Count() > 1)
                {
                    resultado2 = resultado.ElementAt(0);
                }
                else if (resultado.ToList().Count() == 1)
                {
                    resultado2 = resultado.Single();
                }

            }
            return resultado2;
        }

        public SAS_Dispostivo GetDeviceById(string conection, int idDispositivo)
        {
            List<SAS_Dispostivo> resultado = new List<SAS_Dispostivo>();
            SAS_Dispostivo resultado2 = new SAS_Dispostivo();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_Dispostivo.Where(x => x.id == idDispositivo).ToList();
                if (resultado.ToList().Count() > 0)
                {
                    resultado2 = resultado.ElementAt(0);
                }


            }
            return resultado2;
        }


        // 15.05.2022
        public List<SAS_ListadoMacByIpByColaborador> GetListMACs(string conection)
        {
            List<SAS_ListadoMacByIpByColaborador> resultado = new List<SAS_ListadoMacByIpByColaborador>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoMacByIpByColaborador.ToList();
            }
            return resultado;

        }


        // 15.05.2022
        public List<SAS_ListadoSoftwareByDevice> GetListSoftwareAllDevice(string conection)
        {
            List<SAS_ListadoSoftwareByDevice> resultado = new List<SAS_ListadoSoftwareByDevice>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoSoftwareByDevice.ToList();
            }
            return resultado;

        }

        // 28.05.2022
        public List<SAS_ListadoHardwareByDevice> GetListHardwareAllDevice(string conection)
        {
            List<SAS_ListadoHardwareByDevice> resultado = new List<SAS_ListadoHardwareByDevice>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoHardwareByDevice.OrderBy(x => x.dispositivo).OrderBy(x => x.hardware).ToList();
            }
            return resultado;

        }

        // 28.05.2022
        public List<SAS_DispositivoCuentaUsuariosByAllDeviceResult> DispositivoCuentaUsuariosByAllDevice(string conection)
        {
            List<SAS_DispositivoCuentaUsuariosByAllDeviceResult> resultado = new List<SAS_DispositivoCuentaUsuariosByAllDeviceResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoCuentaUsuariosByAllDevice().ToList();
            }
            return resultado;

        }



        // 28.05.2022
        public List<SAS_ListadoComponentesByDevice> DispositivoComponentesByAllDevice(string conection)
        {
            List<SAS_ListadoComponentesByDevice> resultado = new List<SAS_ListadoComponentesByDevice>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoComponentesByDevice.OrderBy(x => x.tipoDeDispositivo).OrderBy(x => x.dispositivo).OrderBy(x => x.componente).ToList();
            }
            return resultado;

        }



        // 26/12/2023
        public List<SAS_DispositivoImagene> ListadoTodasImagenesDeDispositivos(string conection)
        {
            List<SAS_DispositivoImagene> resultado = new List<SAS_DispositivoImagene>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoImagenes.ToList();
            }
            return resultado;
        }


        public List<SAS_DispositivoImagene> ListadoImagenesPorDispositivoID(string conection, int DispositivoID)
        {
            List<SAS_DispositivoImagene> resultado = new List<SAS_DispositivoImagene>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoImagenes.Where(x => x.DispositivoId == DispositivoID).ToList();
            }
            return resultado;
        }


        public int LimpiarDispositivo(string Connection, int DispositivoID)
        {
            int codigo = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[Connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_Dispostivo.Where(x => x.id == DispositivoID).ToList();
                using (TransactionScope Scope = new TransactionScope())
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Editar() 
                        SAS_Dispostivo oDevice = new SAS_Dispostivo();
                        oDevice = resultado.Single();
                        oDevice.nombres = "-";
                        oDevice.descripcion = "-";
                        oDevice.esFinal = 0;
                        oDevice.tipoDispositivoCodigo = "000";
                        Modelo.SubmitChanges();
                        codigo = oDevice.id;
                        #endregion
                    }
                    Scope.Complete();
                }
            }
            return codigo;
        }


    }
}
