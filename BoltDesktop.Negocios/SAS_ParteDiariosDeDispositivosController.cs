using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MyControlsDataBinding.Busquedas;

namespace Asistencia.Negocios
{
    public class SAS_ParteDiariosDeDispositivosController
    {


        public List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> ListarPorPeriodo(string conection, string desde, string hasta)
        {
            List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> result = new List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ParteDiariosDeDispositivosAllByPeriodo(desde, hasta).ToList();
            }
            return result;
        }


        public List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult> GetListDetailById(string conection, int codigo)
        {
            List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult> result = new List<SAS_ParteDiariosDeDispositivosDetalleByCodigoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ParteDiariosDeDispositivosDetalleByCodigo(codigo).ToList();
            }
            return result;
        }

        public List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeResult> GetListByTipoHardwareProveedorSede(string conection, string idClieprov, string idTipoHardware, string idSede)
        {
            List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeResult> result = new List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDeDispositivosByProveedorByTipoHardwareySede(idClieprov, idTipoHardware, idSede).ToList();
            }
            return result;
        }

        public List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeRegisterResult> GetListByTipoHardwareProveedorSedeToRegister(string conection, string idClieprov, string idTipoHardware, string idSede, string fecha)
        {
            List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeRegisterResult> result = new List<SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeRegisterResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDeDispositivosByProveedorByTipoHardwareySedeRegister(idClieprov, idTipoHardware, idSede, fecha).ToList();
            }
            return result;
        }




        public int ObtenerUltimoOperacion(string conection)
        {
            int ultimo = 1;
            int correlativo = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                correlativo = Modelo.SAS_ParteDiariosDeDispositivos.ToList().Count > 0 ? Modelo.SAS_ParteDiariosDeDispositivos.Max(x => x.Codigo) : 0;
            }

            return correlativo + ultimo;
        }


        public void ChangeState(string conection, SAS_ParteDiariosDeDispositivos itemAnular)
        {
            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                if (itemAnular != null)
                {
                    if (itemAnular.Codigo > 0)
                    {
                        #region Puede ser nuevo registro o para editar.
                        var result01 = Modelo.SAS_ParteDiariosDeDispositivos.Where(x => x.Codigo == itemAnular.Codigo).ToList();

                        if (result01 != null && result01.ToList().Count > 0)
                        {
                            #region Modificar()                                                       
                            SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                            item = result01.ElementAt(0);
                            if (item.EstadoCodigo == "PE")
                            {
                                item.EstadoCodigo = "AN";
                            }
                            else if (item.EstadoCodigo == "AN")
                            {
                                item.EstadoCodigo = "PE";
                            }
                            Modelo.SubmitChanges();
                            #endregion
                        }
                        #endregion
                    }
                }
            }
        }


        public void DeleteRecord(string conection, SAS_ParteDiariosDeDispositivos itemDelete)
        {
            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                if (itemDelete != null)
                {
                    if (itemDelete.Codigo > 0)
                    {
                        #region Puede ser nuevo registro o para editar.
                        var result01 = Modelo.SAS_ParteDiariosDeDispositivos.Where(x => x.Codigo == itemDelete.Codigo).ToList();

                        if (result01 != null && result01.ToList().Count > 0)
                        {
                            #region Modificar()                           
                            List<SAS_ParteDiariosDeDispositivosDetalle> detailDelete = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                            detailDelete = Modelo.SAS_ParteDiariosDeDispositivosDetalle.Where(x => x.Codigo == itemDelete.Codigo).ToList();
                            Modelo.SAS_ParteDiariosDeDispositivosDetalle.DeleteAllOnSubmit(detailDelete);
                            Modelo.SubmitChanges();

                            SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                            item = result01.ElementAt(0);
                            Modelo.SAS_ParteDiariosDeDispositivos.DeleteOnSubmit(item);
                            Modelo.SubmitChanges();
                            #endregion
                        }


                        #endregion
                    }
                }
            }
        }

        public int RegistrarParteDiario(string conection, SAS_ParteDiariosDeDispositivos itemParteDiarioDispositivo, List<SAS_ParteDiariosDeDispositivosDetalle> detail, List<SAS_ParteDiariosDeDispositivosDetalle> detailDelete)
        {

            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                if (itemParteDiarioDispositivo != null)
                {
                    if (itemParteDiarioDispositivo.Codigo == 0)
                    {
                        #region Nuevo()
                        SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                        //item.Codigo = itemParteDiarioDispositivo.Codigo;
                        item.TipoHardwareCodigo = itemParteDiarioDispositivo.TipoHardwareCodigo;
                        item.idClieprov = itemParteDiarioDispositivo.idClieprov;
                        item.Fecha = itemParteDiarioDispositivo.Fecha;
                        item.SedeCodigo = itemParteDiarioDispositivo.SedeCodigo;
                        item.EstadoCodigo = itemParteDiarioDispositivo.EstadoCodigo;
                        item.Observacion = itemParteDiarioDispositivo.Observacion;
                        item.GeneradoPor = itemParteDiarioDispositivo.GeneradoPor;
                        item.FechaRegistro = DateTime.Now;
                        item.Hostname = itemParteDiarioDispositivo.Hostname;
                        Modelo.SAS_ParteDiariosDeDispositivos.InsertOnSubmit(item);
                        Modelo.SubmitChanges();
                        codigo = item.Codigo;

                        #region Registrar listadoDetalle();

                        if (detail != null && detail.ToList().Count > 0)
                        {
                            foreach (var detalleParteDetalle in detail)
                            {
                                SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                detalle.Codigo = codigo;
                                detalle.DispositivoCodigo = detalleParteDetalle.DispositivoCodigo;
                                detalle.Item = detalleParteDetalle.Item;
                                detalle.HorasActivas = detalleParteDetalle.HorasActivas;
                                detalle.HorasInactivas = detalleParteDetalle.HorasInactivas;
                                detalle.Observacion = detalleParteDetalle.Observacion;
                                detalle.MotivoInactivoCodigo = detalleParteDetalle.MotivoInactivoCodigo;
                                detalle.Estado = detalleParteDetalle.Estado;
                                Modelo.SAS_ParteDiariosDeDispositivosDetalle.InsertOnSubmit(detalle);
                                Modelo.SubmitChanges();
                            }
                        }
                        #endregion


                        #endregion
                    }
                    else
                    {
                        #region Puede ser nuevo registro o para editar.
                        var result01 = Modelo.SAS_ParteDiariosDeDispositivos.Where(x => x.Codigo == itemParteDiarioDispositivo.Codigo).ToList();

                        if (result01 != null && result01.ToList().Count > 0)
                        {
                            #region Modificar()
                            SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                            item = result01.ElementAt(0);
                            //item.TipoHardwareCodigo = itemParteDiarioDispositivo.TipoHardwareCodigo;
                            //item.idClieprov = itemParteDiarioDispositivo.idClieprov;
                            //item.Fecha = itemParteDiarioDispositivo.Fecha;
                            //item.SedeCodigo = itemParteDiarioDispositivo.SedeCodigo;
                            item.EstadoCodigo = itemParteDiarioDispositivo.EstadoCodigo;
                            item.Observacion = itemParteDiarioDispositivo.Observacion;
                            Modelo.SubmitChanges();
                            codigo = item.Codigo;


                            #region Eliminar detalles de lista eliminar();

                            if (detailDelete != null && detailDelete.ToList().Count > 0)
                            {
                                foreach (var detalleParteDetalle in detailDelete)
                                {

                                    var result02 = Modelo.SAS_ParteDiariosDeDispositivosDetalle.Where(x => x.Codigo == detalleParteDetalle.Codigo && x.DispositivoCodigo == detalleParteDetalle.DispositivoCodigo).ToList();

                                    if (result02 != null && result02.ToList().Count > 0)
                                    {
                                        SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                        detalle = result02.ElementAt(0);
                                        Modelo.SAS_ParteDiariosDeDispositivosDetalle.DeleteOnSubmit(detalle);
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Agregar detalles de lista();

                            if (detail != null && detail.ToList().Count > 0)
                            {
                                #region Detalle() 
                                foreach (var detalleParteDetalle in detail)
                                {
                                    var result02 = Modelo.SAS_ParteDiariosDeDispositivosDetalle.Where(x => x.Codigo == detalleParteDetalle.Codigo && x.DispositivoCodigo == detalleParteDetalle.DispositivoCodigo).ToList();
                                    if (result02 != null && result02.ToList().Count > 0)
                                    {
                                        #region Modificar detalle()
                                        SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                        detalle = result02.ElementAt(0);
                                        detalle.Item = detalleParteDetalle.Item;
                                        detalle.HorasActivas = detalleParteDetalle.HorasActivas;
                                        detalle.HorasInactivas = detalleParteDetalle.HorasInactivas;
                                        detalle.Observacion = detalleParteDetalle.Observacion;
                                        detalle.MotivoInactivoCodigo = detalleParteDetalle.MotivoInactivoCodigo;
                                        detalle.Estado = detalleParteDetalle.Estado;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Nuevo detalle()
                                        SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                        detalle.Codigo = detalleParteDetalle.Codigo;
                                        detalle.DispositivoCodigo = detalleParteDetalle.DispositivoCodigo;
                                        detalle.Item = detalleParteDetalle.Item;
                                        detalle.HorasActivas = detalleParteDetalle.HorasActivas;
                                        detalle.HorasInactivas = detalleParteDetalle.HorasInactivas;
                                        detalle.Observacion = detalleParteDetalle.Observacion;
                                        detalle.MotivoInactivoCodigo = detalleParteDetalle.MotivoInactivoCodigo;
                                        detalle.Estado = detalleParteDetalle.Estado;
                                        Modelo.SAS_ParteDiariosDeDispositivosDetalle.InsertOnSubmit(detalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #endregion
                        }
                        else
                        {
                            #region Nuevo()
                            SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                            //item.Codigo = itemParteDiarioDispositivo.Codigo;
                            item.TipoHardwareCodigo = itemParteDiarioDispositivo.TipoHardwareCodigo;
                            item.idClieprov = itemParteDiarioDispositivo.idClieprov;
                            item.Fecha = itemParteDiarioDispositivo.Fecha;
                            item.SedeCodigo = itemParteDiarioDispositivo.SedeCodigo;
                            item.EstadoCodigo = itemParteDiarioDispositivo.EstadoCodigo;
                            item.Observacion = itemParteDiarioDispositivo.Observacion;
                            item.GeneradoPor = itemParteDiarioDispositivo.GeneradoPor;
                            item.Fecha = itemParteDiarioDispositivo.Fecha;
                            item.Hostname = itemParteDiarioDispositivo.Hostname;
                            Modelo.SAS_ParteDiariosDeDispositivos.InsertOnSubmit(item);
                            Modelo.SubmitChanges();
                            codigo = item.Codigo;

                            #region Registrar listadoDetalle();

                            if (detail != null && detail.ToList().Count > 0)
                            {
                                foreach (var detalleParteDetalle in detail)
                                {
                                    SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                    detalle.Codigo = codigo;
                                    detalle.DispositivoCodigo = detalleParteDetalle.DispositivoCodigo;
                                    detalle.Item = detalleParteDetalle.Item;
                                    detalle.HorasActivas = detalleParteDetalle.HorasActivas;
                                    detalle.HorasInactivas = detalleParteDetalle.HorasInactivas;
                                    detalle.Observacion = detalleParteDetalle.Observacion;
                                    detalle.MotivoInactivoCodigo = detalleParteDetalle.MotivoInactivoCodigo;
                                    detalle.Estado = detalleParteDetalle.Estado;
                                    Modelo.SAS_ParteDiariosDeDispositivosDetalle.InsertOnSubmit(detalle);
                                    Modelo.SubmitChanges();
                                }
                            }
                            #endregion


                            #endregion
                        }

                        #endregion
                    }
                }
            }

            return codigo;
        }

        public List<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult> ObtenerDocumentosDePartesDiariosPorPeriodoYporCodigoDeDispositivo(string conection, string desde, string hasta, int codigoDispositivo)
        {
            List<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult> result = new List<SAS_PartesDiariosDeDispositivosByPeriodByCodigoResult>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_PartesDiariosDeDispositivosByPeriodByCodigo(codigoDispositivo, desde, hasta).ToList();
            }
            return result;
        }

        public int RegistrarParteDiario(string conection, SAS_ParteDiariosDeDispositivos itemParteDiarioDispositivo, List<SAS_ParteDiariosDeDispositivosDetalle> detail)
        {

            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                if (itemParteDiarioDispositivo != null)
                {
                    if (itemParteDiarioDispositivo.Codigo == 0)
                    {
                        #region Nuevo()
                        SAS_ParteDiariosDeDispositivos item = new SAS_ParteDiariosDeDispositivos();
                        //item.Codigo = itemParteDiarioDispositivo.Codigo;
                        item.TipoHardwareCodigo = itemParteDiarioDispositivo.TipoHardwareCodigo;
                        item.idClieprov = itemParteDiarioDispositivo.idClieprov;
                        item.Fecha = itemParteDiarioDispositivo.Fecha;
                        item.SedeCodigo = itemParteDiarioDispositivo.SedeCodigo;
                        item.EstadoCodigo = itemParteDiarioDispositivo.EstadoCodigo;
                        item.Observacion = itemParteDiarioDispositivo.Observacion;
                        item.GeneradoPor = itemParteDiarioDispositivo.GeneradoPor;
                        item.FechaRegistro = DateTime.Now;
                        item.Hostname = itemParteDiarioDispositivo.Hostname;
                        Modelo.SAS_ParteDiariosDeDispositivos.InsertOnSubmit(item);
                        Modelo.SubmitChanges();
                        codigo = item.Codigo;

                        #region Registrar listadoDetalle();

                        if (detail != null && detail.ToList().Count > 0)
                        {
                            int correlativo = 1;
                            foreach (var detalleParteDetalle in detail)
                            {
                                SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                                detalle.Codigo = codigo;
                                detalle.DispositivoCodigo = detalleParteDetalle.DispositivoCodigo;
                                detalle.Item = correlativo.ToString().PadLeft(4, '0');
                                detalle.HorasActivas = detalleParteDetalle.HorasActivas;
                                detalle.HorasInactivas = detalleParteDetalle.HorasInactivas;
                                detalle.Observacion = detalleParteDetalle.Observacion;
                                detalle.MotivoInactivoCodigo = detalleParteDetalle.MotivoInactivoCodigo;
                                detalle.Estado = detalleParteDetalle.Estado;
                                Modelo.SAS_ParteDiariosDeDispositivosDetalle.InsertOnSubmit(detalle);
                                Modelo.SubmitChanges();
                                correlativo += 1;
                            }
                        }
                        #endregion


                        #endregion
                    }

                }
            }

            return codigo;
        }

        public int ActualizarProgramacionDiariaDeDispositivoDesdeListaSemanal(string conection, List<SAS_ParteDiariosDeDispositivosDetalle> detail)
        {
            int resultadoDelProceso = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                foreach (var item in detail)
                {
                    var result02 = Modelo.SAS_ParteDiariosDeDispositivosDetalle.Where(x => x.Codigo == item.Codigo && x.DispositivoCodigo == item.DispositivoCodigo).ToList();
                    if (result02 != null && result02.ToList().Count > 0)
                    {
                        #region Modificar detalle()
                        SAS_ParteDiariosDeDispositivosDetalle detalle = new SAS_ParteDiariosDeDispositivosDetalle();
                        detalle = result02.ElementAt(0);                        
                        detalle.HorasActivas = item.HorasActivas;
                        detalle.HorasInactivas = item.HorasInactivas;
                        detalle.Observacion = item.Observacion;
                        detalle.MotivoInactivoCodigo = item.MotivoInactivoCodigo;
                        detalle.Estado = item.Estado;
                        Modelo.SubmitChanges();
                        #endregion
                    }
                }                
            }
            return resultadoDelProceso;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> GenerarListadoGenericoDeDispositivosQueYaTienenParteDiario(List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> listado)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultado = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();

            resultado = (from item in listado
                         group item by new { item.codigoMacro } into j
                         select new SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor
                         {
                             ID = j.FirstOrDefault().Codigo,
                             dispositivo = j.FirstOrDefault().tipoHardware,
                             personal = string.Empty,
                             area = string.Empty,
                             sedecodigo = j.FirstOrDefault().SedeCodigo,
                             sedeDescripcion = j.FirstOrDefault().sede,
                             idClieprov = j.FirstOrDefault().idClieprov,
                             razonSocial = j.FirstOrDefault().razonSocial,
                             tipoDispositivoCodigo = j.FirstOrDefault().TipoHardwareCodigo,
                             tipoDispositivo = j.FirstOrDefault().tipoHardware,
                             selecionado = 0,
                             cantidad = Convert.ToInt32(j.FirstOrDefault().cantidadDeEquipo.Value),
                             codigoMacro = j.Key.codigoMacro.Trim(),
                             documento = j.FirstOrDefault().documento,
                             fecha = j.FirstOrDefault().Fecha,
                         }).ToList();



            return resultado;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> DepurarListaPendienteDeCreacionConListaCreadaPorPeriodo(List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> listaPorDepurar, List<SAS_ParteDiariosDeDispositivosAllByPeriodoResult> listaFiltro)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultado = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();

            List<string> codigosMacros = new List<string>();

            codigosMacros = (from item in listaFiltro
                             group item by new { item.codigoMacro } into j
                             select
                                 j.Key.codigoMacro
                             ).ToList();

            resultado = (from items in listaPorDepurar
                         where !(codigosMacros.Contains(items.codigoMacro.ToString()))
                         select items
                           ).ToList();


            return resultado;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> CrearListadoDeDispositivosParaPartesDeEquipamientoPorPeriodos(string desde, string hasta, List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> listado)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultado = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> resultadoPorDia = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
            for (DateTime i = Convert.ToDateTime(desde); i <= Convert.ToDateTime(hasta); i = i.AddDays(1.0))
            {
                resultadoPorDia = (from item in listado
                                   group item by new { item.codigoMacro } into j
                                   select new SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor
                                   {
                                       ID = 0,
                                       dispositivo = j.FirstOrDefault().dispositivo,
                                       personal = string.Empty,
                                       area = string.Empty,
                                       sedecodigo = j.FirstOrDefault().sedecodigo,
                                       sedeDescripcion = j.FirstOrDefault().sedeDescripcion,
                                       idClieprov = j.FirstOrDefault().idClieprov,
                                       razonSocial = j.FirstOrDefault().razonSocial,
                                       tipoDispositivoCodigo = j.FirstOrDefault().tipoDispositivoCodigo,
                                       tipoDispositivo = j.FirstOrDefault().tipoDispositivo,
                                       selecionado = 1,
                                       cantidad = j.FirstOrDefault().cantidad,
                                       codigoMacro = i.ToString("yyyyMMdd") + j.FirstOrDefault().codigoMacro,
                                       documento = j.FirstOrDefault().documento,
                                       fecha = i,
                                   }).ToList();
                resultado.AddRange(resultadoPorDia);
            }



            return resultado;
        }

        public List<DFormatoSimple> ObtenerListadoDispositivosDisponibles(string conection, string fecha, List<int> listadoCodigoEnGrilla, string idClieprov, string sedecodigo, string tipoDispositivoCodigo)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                // Obtener listado Disponibles

                var resultado01 = Modelo.SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoPropios.ToList();

                // Obtener listado Disponibles filtrado por proveedor, sede y tipo de dispositivo
                var resultado02 = resultado01.Where(x => x.sedecodigo == sedecodigo && x.tipoDispositivoCodigo == tipoDispositivoCodigo && x.idClieprov == idClieprov).ToList();

                // Obtener listado de codigo de dispositivos que ya están en otros partes de equipamiento
                var resultado03 = Modelo.SAS_ParteDiariosDeDispositivosEnParteDiarioPorFecha(fecha).ToList();

                List<Int32> resultado03A = new List<int>();
                foreach (var item in resultado03)
                {
                    resultado03A.Add(item.DispositivoCodigo);
                }

                List<Int32> resultado03B = (from items in resultado03.ToList()
                                            group items by new { items.DispositivoCodigo } into j
                                            select new Int32
                                            {
                                            }
                              ).ToList();

                // Filtrar contra listado que ya están en un parte anterior.
                var resultado04 = (from items in resultado02.ToList()
                                   where !(resultado03A.Contains(items.ID))
                                   select items
                              ).ToList();

                // Filtrar contra listado de la grilla Actual

                var resultado05 = (from items in resultado04.ToList()
                                   where !(listadoCodigoEnGrilla.Contains(items.ID))
                                   select items
                                ).ToList();

                // Cargar al control codigo | Descripcion, Personal, Area de trabajo
                result = (from items in resultado05
                          group items by new { items.ID } into J
                          select new DFormatoSimple
                          {
                              Codigo = J.Key.ID.ToString(),
                              Descripcion = J.FirstOrDefault().dispositivo.Trim() + " | " + J.FirstOrDefault().personal.Trim() + " | " + J.FirstOrDefault().area.Trim()
                          }).ToList();

            }

            return result;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor(string conection)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> result = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor.ToList();
            }
            return result;
        }

        public int GenerarPartesDiariosMasivosDesdeProcesoMasivo(string conection, SAS_USUARIOS user2, List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> ListGroup, List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> listDetail)
        {
            int cantidadDeRegistroCreador = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                SAS_ParteDiariosDeDispositivosController model = new SAS_ParteDiariosDeDispositivosController();
                /* Separa por tres grupos: Sede | Proveedor | Tipo de dispositivos */

                foreach (var parteDiario in ListGroup)
                {
                    DateTime fechaRegistro = parteDiario.fecha;
                    int CodigoRegistro = 0;
                    int tamañanoCadena = parteDiario.codigoMacro.Trim().Length;
                    int tamañanoCadenaCorte = 8;
                    int tamañoLimite = tamañanoCadena - tamañanoCadenaCorte;

                    string CodigoMacroSinPrimeroOchoCaracteres = parteDiario.codigoMacro.Substring(8, tamañoLimite);
                    string observacion = "Generado desde proceso masivo de creación de partes diarios de equipamiento";
                    var ListaPrimerFiltro = listDetail.Where(x => x.codigoMacro.ToString() == CodigoMacroSinPrimeroOchoCaracteres).ToList();

                    if (ListaPrimerFiltro != null && ListaPrimerFiltro.ToList().Count > 0)
                    {
                        /* Crear lista cabecera */
                        SAS_ParteDiariosDeDispositivos cab = new SAS_ParteDiariosDeDispositivos();
                        List<SAS_ParteDiariosDeDispositivosDetalle> listDet = new List<SAS_ParteDiariosDeDispositivosDetalle>();
                        cab.Codigo = 0;
                        cab.TipoHardwareCodigo = parteDiario.tipoDispositivoCodigo;
                        cab.idClieprov = parteDiario.idClieprov;
                        cab.Fecha = fechaRegistro;
                        cab.SedeCodigo = parteDiario.sedecodigo;
                        cab.EstadoCodigo = "PE";
                        cab.Observacion = observacion;
                        cab.GeneradoPor = user2.IdUsuario;
                        cab.FechaRegistro = DateTime.Now;
                        cab.Hostname = Environment.UserName;

                        /* Crear lista detalle */
                        listDet = (from oDetail in ListaPrimerFiltro
                                   group oDetail by new { oDetail.ID } into j
                                   select new SAS_ParteDiariosDeDispositivosDetalle
                                   {
                                       Codigo = cab.Codigo,
                                       DispositivoCodigo = j.Key.ID,
                                       Item = string.Empty,
                                       HorasActivas = 24,
                                       HorasInactivas = 0,
                                       Observacion = string.Empty,
                                       MotivoInactivoCodigo = 0,
                                       Estado = 1
                                   }).ToList();

                        model = new SAS_ParteDiariosDeDispositivosController();
                        /* Registrar cabecera y detalle */
                        int Registro = model.RegistrarParteDiario(conection, cab, listDet);
                        cantidadDeRegistroCreador += 1;
                    }
                }
            }

            return cantidadDeRegistroCreador;
        }

        public List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedorAgrupado(List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> listado)
        {
            List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor> result = new List<SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor>();


            result = (from items in listado.ToList()
                      group items by new { items.idClieprov, items.razonSocial, items.sedecodigo, items.sedeDescripcion, items.tipoDispositivo, items.tipoDispositivoCodigo, items.codigoMacro } into j
                      select new SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor
                      {
                          ID = 0,
                          dispositivo = j.Key.tipoDispositivo,
                          personal = string.Empty,
                          area = string.Empty,
                          sedecodigo = j.Key.sedecodigo,
                          sedeDescripcion = j.Key.sedeDescripcion,
                          idClieprov = j.Key.idClieprov,
                          razonSocial = j.Key.razonSocial,
                          tipoDispositivoCodigo = j.Key.tipoDispositivoCodigo,
                          tipoDispositivo = j.Key.tipoDispositivo,
                          selecionado = 0,
                          cantidad = j.Sum(x => x.cantidad),
                          codigoMacro = j.Key.codigoMacro,
                          documento = "POR GENERAR",
                          fecha = DateTime.Now
                      }).ToList();


            return result;
        }


        public SAS_PeriodoMaquinaria ObtenerSemana(string conection, int semana, int anio)
        {
            SAS_PeriodoMaquinaria result01 = new SAS_PeriodoMaquinaria();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_PeriodoMaquinaria.Where(x => x.anio == anio.ToString() && x.semana == semana.ToString()).ToList();

                if (result != null && result.ToList().Count > 0)
                {
                    result01 = result.ElementAt(0);
                }
            }
            return result01;
        }



    }
}
