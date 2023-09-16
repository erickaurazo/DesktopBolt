using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class CarrierController
    {
        private string cnx;
        //private List<SJ_RHListarDetalleTransportistaContratoResult> ListadoContrato;
        //private List<SJ_RHListarDetalleTransportistaChoferResult> ListadoChofer;
        //private List<SJ_RHListarDetalleTransportistaRutaResult> ListadoRuta;

        //private SJ_RHTransportista oTransportista;
        //private SJ_RHTransportistaContrato oContrato;
        //private SJ_RHTransportistaChofer oChofer;
        //private SJ_RHTransportistaRuta oRuta;

        public List<SJ_RHListarTransportistasResult> ListarTransportistas(string conection)
        {
            List<SJ_RHListarTransportistasResult> Listado = new List<SJ_RHListarTransportistasResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                Listado = modelo.SJ_RHListarTransportistas().ToList();
            }

            return Listado;
        }

        public List<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult> ListarCatalogoEmpresasTransportePersonalCampo(string conection)
        {
            List<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult> Listado = new List<SJ_RHListarCatalogoEmpresasTransportePersonalCampoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Listado = Modelo.SJ_RHListarCatalogoEmpresasTransportePersonalCampo().ToList();
            }
            return Listado;
        }

        public List<SJ_RHListarTransportistasResult> ListarTransportistasActivos(string conection)
        {
            List<SJ_RHListarTransportistasResult> Listado = new List<SJ_RHListarTransportistasResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Listado = Modelo.SJ_RHListarTransportistas().Where(x => x.IdEstado == "AC").ToList();
            }
            return Listado;
        }

        //Registrar Datos con Detalles del Transportista()
        public Int32 RegistrarTransportista(
            SJ_RHTransportista ObjetoTransportista,
            List<SJ_RHTransportistaChofer> listadoChoferes,
            List<SJ_RHTransportistaContrato> listadoContratos,
            List<SJ_RHTransportistaChofer> ListaEliminadosChoferes,
            List<SJ_RHTransportistaContrato> ListaEliminadosContratos,
            List<SJ_RHTransportistaRuta> ListaEliminadosRuta,
            List<SJ_RHTransportistaRuta> ListadoRutas, string conection)
        {
            Int32 CodigoRegistro;
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transacción()
            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {

                // 1.- Genero la cabecera
                // 2.- Con el Id Obtenido Actualizo o registro los detalles de los choferes y contratos y rutas

                if (ObjetoTransportista.Id == 0)
                {
                    #region Registro Nuevo()

                    // Registro Cabecera --> SJ_RHTransportista
                    SJ_RHTransportista oTransportista = new SJ_RHTransportista();
                    oTransportista.RUC = ObjetoTransportista.RUC;
                    oTransportista.Placa = ObjetoTransportista.Placa;
                    oTransportista.NombreCorto = ObjetoTransportista.NombreCorto;
                    oTransportista.TipoMovilidad = ObjetoTransportista.TipoMovilidad;
                    oTransportista.IdEstado = ObjetoTransportista.IdEstado;
                    oTransportista.NumeroAsientos = ObjetoTransportista.NumeroAsientos;
                    oTransportista.PesoMaximo = ObjetoTransportista.PesoMaximo;
                    oTransportista.FechaRegistro = DateTime.Now;
                    oTransportista.AnioFabricacion = ObjetoTransportista.AnioFabricacion;
                    oTransportista.Marca = ObjetoTransportista.Marca;
                    oTransportista.Modelo = ObjetoTransportista.Modelo;
                    oTransportista.EsInterLocal = 1;
                    oTransportista.EsMovilidadLocal = 1;
                    oTransportista.idclieprov = ObjetoTransportista.RUC;

                    try
                    {
                        Modelo.SJ_RHTransportista.InsertOnSubmit(oTransportista);
                        Modelo.SubmitChanges();
                        CodigoRegistro = oTransportista.Id;

                        #region Registrar Tabla de Choferes(SJ_RHTransportistaChofer)

                        if (listadoChoferes.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaChofer item in listadoChoferes)
                            {
                                SJ_RHTransportistaChofer oChofer = new SJ_RHTransportistaChofer();
                                oChofer.IdTransportista = CodigoRegistro;
                                oChofer.Item = item.Item;
                                oChofer.DNI = item.DNI;
                                oChofer.Nombres = item.Nombres;
                                oChofer.TipoLicencia = item.TipoLicencia;
                                oChofer.EsCapacitado = item.EsCapacitado;
                                oChofer.Observacion = item.Observacion;
                                oChofer.IdEstado = item.IdEstado;
                                try
                                {
                                    Modelo.SJ_RHTransportistaChofer.InsertOnSubmit(oChofer);
                                    Modelo.SubmitChanges();
                                }
                                catch (Exception Ex)
                                {

                                    throw Ex;
                                }
                            }
                        }




                        #endregion

                        #region Registrar Tabla de Contratos (SJ_RHTransportistaContrato)

                        if (listadoContratos.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaContrato item in listadoContratos)
                            {
                                SJ_RHTransportistaContrato oContrato = new SJ_RHTransportistaContrato();
                                oContrato.Id = CodigoRegistro;
                                oContrato.Item = item.Item;
                                oContrato.FechaInicio = item.FechaInicio;
                                oContrato.FechaTermino = item.FechaTermino;
                                oContrato.Observacion = item.Observacion;
                                oContrato.IdEstado = item.IdEstado;

                                try
                                {
                                    Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oContrato);
                                    Modelo.SubmitChanges();
                                }
                                catch (Exception Ex)
                                {

                                    throw Ex;
                                }
                            }
                        }




                        #endregion

                        #region Registrar Tabla de Rutas x Contratista (SJ_RHTransportistaRuta)

                        if (ListadoRutas.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaRuta item in ListadoRutas)
                            {
                                #region Registrar Rutas x Transportista()
                                SJ_RHTransportistaRuta oRuta = new SJ_RHTransportistaRuta();
                                oRuta.Id = CodigoRegistro;
                                oRuta.Item = item.Item;
                                oRuta.IdRuta = item.IdRuta;
                                oRuta.Observacion = item.Observacion;
                                oRuta.PrecioFlete = item.PrecioFlete;
                                oRuta.PrecioPersona = item.PrecioPersona;
                                oRuta.PrecioVuelta = item.PrecioVuelta;
                                oRuta.IdEstado = item.IdEstado;

                                try
                                {
                                    Modelo.SJ_RHTransportistaRutas.InsertOnSubmit(oRuta);
                                    Modelo.SubmitChanges();
                                }
                                catch (Exception Ex)
                                {

                                    throw Ex;
                                }
                                #endregion
                            }
                        }

                        #endregion

                    }
                    catch (Exception Ex)
                    {

                        throw Ex;
                    }
                    #endregion
                }
                else
                {
                    #region Actualizar Registro()
                    SJ_RHTransportista oTransportista = new SJ_RHTransportista();
                    oTransportista = Modelo.SJ_RHTransportista.Where(x => x.Id == ObjetoTransportista.Id).Single();
                    oTransportista.RUC = ObjetoTransportista.RUC;
                    oTransportista.Placa = ObjetoTransportista.Placa;
                    oTransportista.NombreCorto = ObjetoTransportista.NombreCorto;
                    oTransportista.TipoMovilidad = ObjetoTransportista.TipoMovilidad;
                    //oTransportista.IdEstado = ObjetoTransportista.IdEstado;
                    oTransportista.NumeroAsientos = ObjetoTransportista.NumeroAsientos;
                    oTransportista.PesoMaximo = ObjetoTransportista.PesoMaximo;
                    oTransportista.AnioFabricacion = ObjetoTransportista.AnioFabricacion;
                    oTransportista.Marca = ObjetoTransportista.Marca;
                    oTransportista.Modelo = ObjetoTransportista.Modelo;
                    //oTransportista.EsInterLocal = 1;
                    //oTransportista.EsMovilidadLocal = 1;
                    oTransportista.idclieprov = ObjetoTransportista.RUC;
                    try
                    {
                        Modelo.SubmitChanges();
                        CodigoRegistro = oTransportista.Id;

                        #region Eliminar Choferes que fueron agregados a la lista de Eliminados()
                        if (ListaEliminadosChoferes != null)
                        {
                            if (ListaEliminadosChoferes.ToList().Count > 0)
                            {
                                #region Eliminar chofer x chofer()
                                foreach (SJ_RHTransportistaChofer item in ListaEliminadosChoferes)
                                {
                                    try
                                    {
                                        EliminarChofer(item,conection);
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Eliminar Contratos que fueron agregados a la lista de Eliminados()
                        if (ListaEliminadosContratos != null)
                        {
                            if (ListaEliminadosContratos.ToList().Count > 0)
                            {
                                #region Eliminar Contrato x Contrato()
                                foreach (SJ_RHTransportistaContrato item in ListaEliminadosContratos)
                                {
                                    try
                                    {
                                        EliminarContrato(item,conection);
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Eliminar Rutas que fueron agregados a la lista de Eliminados()
                        if (ListaEliminadosRuta != null)
                        {
                            if (ListaEliminadosRuta.ToList().Count > 0)
                            {
                                #region Eliminar Rutas x Contratista()
                                foreach (SJ_RHTransportistaRuta item in ListaEliminadosRuta)
                                {
                                    try
                                    {
                                        EliminarRuta(item,conection);
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        #region Actualizar o agregar registros de Choferes()
                        if (listadoChoferes.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaChofer item in listadoChoferes)
                            {
                                if (Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == CodigoRegistro && x.Item == item.Item).ToList().Count == 1)
                                {
                                    #region Actualizar Chofer()
                                    SJ_RHTransportistaChofer oChofer = new SJ_RHTransportistaChofer();
                                    oChofer = Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == CodigoRegistro && x.Item == item.Item).Single();
                                    //oChofer.IdTransportista = item.IdTransportista;
                                    oChofer.DNI = item.DNI;
                                    oChofer.Nombres = item.Nombres;
                                    oChofer.TipoLicencia = item.TipoLicencia;
                                    oChofer.EsCapacitado = item.EsCapacitado;
                                    oChofer.Observacion = item.Observacion;
                                    try
                                    {
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Registrar un Chofer Nuevo
                                    SJ_RHTransportistaChofer oChofer = new SJ_RHTransportistaChofer();
                                    oChofer.IdTransportista = CodigoRegistro;
                                    oChofer.Item = item.Item;
                                    oChofer.DNI = item.DNI;
                                    oChofer.Nombres = item.Nombres;
                                    oChofer.TipoLicencia = item.TipoLicencia;
                                    oChofer.EsCapacitado = item.EsCapacitado;
                                    oChofer.Observacion = item.Observacion;
                                    oChofer.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SJ_RHTransportistaChofer.InsertOnSubmit(oChofer);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region Actualizar o agregar registros de Contratos()
                        if (listadoContratos.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaContrato item in listadoContratos)
                            {
                                if (Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).ToList().Count == 1)
                                {
                                    #region Actualizar Chofer()
                                    SJ_RHTransportistaContrato oContrato = new SJ_RHTransportistaContrato();
                                    oContrato = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).Single();
                                    oContrato.Item = item.Item;
                                    oContrato.FechaInicio = item.FechaInicio;
                                    oContrato.FechaTermino = item.FechaTermino;
                                    oContrato.Observacion = item.Observacion;
                                    oContrato.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Registrar un SJ_RHTransportistaContrato Nuevo
                                    SJ_RHTransportistaContrato oContrato = new SJ_RHTransportistaContrato();
                                    oContrato.Id = CodigoRegistro;
                                    oContrato.Item = item.Item;
                                    oContrato.FechaInicio = item.FechaInicio;
                                    oContrato.FechaTermino = item.FechaTermino;
                                    oContrato.Observacion = item.Observacion;
                                    oContrato.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oContrato);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        #region Actualizar o agregar registros de Ruta()
                        if (ListadoRutas.ToList().Count > 0)
                        {
                            foreach (SJ_RHTransportistaRuta item in ListadoRutas)
                            {
                                //if (Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == CodigoRegistro && x.IdRuta.ToString().Trim() == item.IdRuta.ToString().Trim()  ).ToList().Count == 1)
                                if (Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).ToList().Count == 1)
                                {
                                    #region Actualizar Ruta()
                                    SJ_RHTransportistaRuta oRuta = new SJ_RHTransportistaRuta();
                                    oRuta = Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).Single();
                                    oRuta.Item = item.Item;
                                    oRuta.IdRuta = item.IdRuta;
                                    oRuta.PrecioFlete = item.PrecioFlete;
                                    oRuta.PrecioPersona = item.PrecioPersona;
                                    oRuta.PrecioVuelta = item.PrecioVuelta;
                                    oRuta.Observacion = item.Observacion;
                                    //oRuta.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Registrar una Ruta Nueva
                                    SJ_RHTransportistaRuta oRuta = new SJ_RHTransportistaRuta();
                                    oRuta.Id = CodigoRegistro;
                                    oRuta.Item = item.Item;
                                    oRuta.IdRuta = item.IdRuta;
                                    oRuta.PrecioFlete = item.PrecioFlete;
                                    oRuta.PrecioPersona = item.PrecioPersona;
                                    oRuta.PrecioVuelta = item.PrecioVuelta;
                                    oRuta.Observacion = item.Observacion;
                                    oRuta.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SJ_RHTransportistaRutas.InsertOnSubmit(oRuta);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {

                                        throw Ex;
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }

                    #endregion
                }

                Modelo.Connection.Close();
            }

            #endregion
            //    Scope.Complete();
            //}

            return CodigoRegistro;
        }

        public bool AddCarrier(
            string conection,
            SJ_RHTransportista carrier,
            List<SJ_RHTransportistaChofer>
            drivers, List<SJ_RHTransportistaContrato>
            contracts, List<SJ_RHTransportistaChofer>
            driversRemoved,
            List<SJ_RHTransportistaContrato> contractRemoved,
            List<SJ_RHTransportistaRuta> routeRemoved,
            List<SJ_RHTransportistaRuta> routes,
            List<SJ_RHTransportistaContrato> documentRemoved,
            List<SJ_RHTransportistaContrato> documents)
        {
            int CodigoRegistro = 0;
            bool status = false;
            int itemContratos = 1;
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    // 1.- Genero la cabecera
                    // 2.- Con el Id Obtenido Actualizo o registro los detalles de los choferes y contratos y rutas
                    var resultQuery = Modelo.SJ_RHTransportista.Where(x => x.Id == carrier.Id);

                    if (resultQuery != null && resultQuery.ToList().Count == 0)
                    {
                        #region Add()

                        // Registro Cabecera --> SJ_RHTransportista
                        SJ_RHTransportista oCarrier = new SJ_RHTransportista();
                        oCarrier.RUC = carrier.RUC;
                        oCarrier.Placa = carrier.Placa;
                        oCarrier.NombreCorto = carrier.NombreCorto;
                        oCarrier.TipoMovilidad = carrier.TipoMovilidad;
                        oCarrier.IdEstado = carrier.IdEstado;
                        oCarrier.NumeroAsientos = carrier.NumeroAsientos;
                        oCarrier.PesoMaximo = carrier.PesoMaximo;
                        oCarrier.FechaRegistro = DateTime.Now;
                        oCarrier.AnioFabricacion = carrier.AnioFabricacion;
                        oCarrier.Marca = carrier.Marca;
                        oCarrier.Modelo = carrier.Modelo;
                        oCarrier.EsInterLocal = 1;
                        oCarrier.EsMovilidadLocal = 1;
                        oCarrier.idclieprov = carrier.RUC;

                        try
                        {
                            Modelo.SJ_RHTransportista.InsertOnSubmit(oCarrier);
                            Modelo.SubmitChanges();
                            CodigoRegistro = oCarrier.Id;

                            #region Registrar Tabla de Choferes(SJ_RHTransportistaChofer)

                            if (drivers.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaChofer item in drivers)
                                {
                                    SJ_RHTransportistaChofer oChofer = new SJ_RHTransportistaChofer();
                                    oChofer.IdTransportista = CodigoRegistro;
                                    oChofer.Item = item.Item;
                                    oChofer.DNI = item.DNI;
                                    oChofer.Nombres = item.Nombres;
                                    oChofer.TipoLicencia = item.TipoLicencia;
                                    oChofer.EsCapacitado = item.EsCapacitado;
                                    oChofer.Observacion = item.Observacion;
                                    oChofer.IdEstado = item.IdEstado;
                                    try
                                    {
                                        Modelo.SJ_RHTransportistaChofer.InsertOnSubmit(oChofer);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {
                                        return false;
                                        throw Ex;

                                    }
                                }
                            }




                            #endregion

                            #region Registrar Tabla de Contratos (SJ_RHTransportistaContrato)

                            if (contracts.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaContrato item in contracts)
                                {
                                    itemContratos += 1;
                                    SJ_RHTransportistaContrato oContract = new SJ_RHTransportistaContrato();
                                    oContract.Id = CodigoRegistro;
                                    oContract.Item = item.Item;

                                    //oContract.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).ToList().Count > 0 ?
                                    //    (Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).Max(x => x.Item).TrimStart('0')) + 1).ToString().PadLeft(3, '0') :
                                    //    "001";
                                    oContract.FechaInicio = item.FechaInicio;
                                    oContract.FechaTermino = item.FechaTermino;
                                    oContract.Observacion = item.Observacion;
                                    oContract.IdEstado = item.IdEstado;
                                    oContract.TypeDocumentId = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().description != null ? Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().TypeDocumentId : 0;

                                    try
                                    {
                                        Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oContract);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {
                                        return false;
                                        throw Ex;

                                    }
                                }
                            }




                            #endregion

                            #region Registrar Tabla de Rutas x Contratista (SJ_RHTransportistaRuta)

                            if (routes.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaRuta item in routes)
                                {
                                    #region Registrar Rutas x Transportista()
                                    SJ_RHTransportistaRuta oRoute = new SJ_RHTransportistaRuta();
                                    oRoute.Id = CodigoRegistro;
                                    oRoute.Item = item.Item;
                                    oRoute.IdRuta = item.IdRuta;
                                    oRoute.Observacion = item.Observacion;
                                    oRoute.PrecioFlete = item.PrecioFlete;
                                    oRoute.PrecioPersona = item.PrecioPersona;
                                    oRoute.PrecioVuelta = item.PrecioVuelta;
                                    oRoute.IdEstado = item.IdEstado;

                                    try
                                    {
                                        Modelo.SJ_RHTransportistaRutas.InsertOnSubmit(oRoute);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {
                                        return false;
                                        throw Ex;

                                    }
                                    #endregion
                                }
                            }

                            #endregion

                            #region Registrar Tabla de documentos (SJ_RHTransportistaContrato)

                            if (documents.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaContrato item in documents)
                                {
                                    itemContratos += 1;
                                    SJ_RHTransportistaContrato oDocument = new SJ_RHTransportistaContrato();
                                    oDocument.Id = CodigoRegistro;
                                    oDocument.Item = item.Item;

                                    //oDocument.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).ToList().Count > 0 ?
                                    //    (Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).Max(x => x.Item).TrimStart('0')) + 1).ToString().PadLeft(3, '0') :
                                    //    "001";
                                    oDocument.FechaInicio = item.FechaInicio;
                                    oDocument.FechaTermino = item.FechaTermino;
                                    oDocument.Observacion = item.Observacion;
                                    oDocument.IdEstado = item.IdEstado;
                                    oDocument.TypeDocumentId = item.TypeDocumentId;

                                    try
                                    {
                                        Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oDocument);
                                        Modelo.SubmitChanges();
                                    }
                                    catch (Exception Ex)
                                    {
                                        return false;
                                        throw Ex;

                                    }
                                }
                            }




                            #endregion

                            status = true;
                        }
                        catch (Exception Ex)
                        {
                            return false;
                            throw Ex;

                        }
                        #endregion
                    }
                    else if (resultQuery != null && resultQuery.ToList().Count == 1)
                    {
                        #region Update()
                        SJ_RHTransportista oCarrier = new SJ_RHTransportista();
                        oCarrier = resultQuery.Single();
                        oCarrier.RUC = carrier.RUC;
                        oCarrier.Placa = carrier.Placa;
                        oCarrier.NombreCorto = carrier.NombreCorto;
                        oCarrier.TipoMovilidad = carrier.TipoMovilidad;
                        //oTransportista.IdEstado = ObjetoTransportista.IdEstado;
                        oCarrier.NumeroAsientos = carrier.NumeroAsientos;
                        oCarrier.PesoMaximo = carrier.PesoMaximo;
                        oCarrier.AnioFabricacion = carrier.AnioFabricacion;
                        oCarrier.Marca = carrier.Marca;
                        oCarrier.Modelo = carrier.Modelo;
                        //oTransportista.EsInterLocal = 1;
                        //oTransportista.EsMovilidadLocal = 1;
                        oCarrier.idclieprov = carrier.RUC;
                        CodigoRegistro = oCarrier.Id;
                        Modelo.SubmitChanges();

                        try
                        {

                            #region Eliminar Choferes que fueron agregados a la lista de Eliminados()
                            if (driversRemoved != null)
                            {
                                if (driversRemoved.ToList().Count > 0)
                                {
                                    #region Eliminar chofer x chofer()
                                    foreach (SJ_RHTransportistaChofer driver in driversRemoved)
                                    {
                                        var resultByQuery = Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == driver.Id && x.Item.ToString().Trim() == driver.Item.ToString().Trim());
                                        if (resultByQuery != null && resultByQuery.ToList().Count == 1)
                                        {
                                            Modelo.SJ_RHTransportistaChofer.DeleteOnSubmit(resultByQuery.Single());
                                            Modelo.SubmitChanges();
                                        }

                                    }

                                }
                                #endregion
                            }

                            #endregion

                            #region Eliminar Contratos que fueron agregados a la lista de Eliminados()
                            if (contractRemoved != null)
                            {
                                if (contractRemoved.ToList().Count > 0)
                                {
                                    #region Eliminar Contrato x Contrato()
                                    foreach (SJ_RHTransportistaContrato contract in contractRemoved)
                                    {
                                        var resultByQuery = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == contract.Id && x.Item.ToString().Trim() == contract.Item.ToString().Trim());
                                        if (resultByQuery != null && resultByQuery.ToList().Count == 1)
                                        {
                                            Modelo.SJ_RHTransportistaContrato.DeleteOnSubmit(resultByQuery.Single());
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion

                            #region Eliminar Rutas que fueron agregados a la lista de Eliminados()
                            if (routeRemoved != null)
                            {
                                if (routeRemoved.ToList().Count > 0)
                                {
                                    #region Eliminar Rutas x Contratista()
                                    foreach (SJ_RHTransportistaRuta route in routeRemoved)
                                    {
                                        var resultByQuery = Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == route.Id && x.IdRuta.ToString().Trim() == route.IdRuta.ToString().Trim());
                                        if (resultByQuery != null && resultByQuery.ToList().Count == 1)
                                        {
                                            Modelo.SJ_RHTransportistaRutas.DeleteOnSubmit(resultByQuery.Single());
                                            Modelo.SubmitChanges();
                                        }

                                    }
                                    #endregion
                                }
                            }
                            #endregion

                            #region Eliminar documentos que fueron agregados a la lista de Eliminados()
                            if (documentRemoved != null)
                            {
                                if (documentRemoved.ToList().Count > 0)
                                {
                                    #region Eliminar documento()
                                    foreach (SJ_RHTransportistaContrato document in documentRemoved)
                                    {
                                        var resultByQuery = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == document.Id && x.Item.ToString().Trim() == document.Item.ToString().Trim());
                                        if (resultByQuery != null && resultByQuery.ToList().Count == 1)
                                        {
                                            Modelo.SJ_RHTransportistaContrato.DeleteOnSubmit(resultByQuery.Single());
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion


                            #region Actualizar o agregar registros de Choferes()
                            if (drivers.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaChofer item in drivers)
                                {
                                    if (Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == item.IdTransportista && x.Item == item.Item).ToList().Count == 1)
                                    {
                                        #region Actualizar Chofer()
                                        SJ_RHTransportistaChofer driver = new SJ_RHTransportistaChofer();
                                        driver = Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == CodigoRegistro && x.Item == item.Item).Single();
                                        //oChofer.IdTransportista = item.IdTransportista;
                                        driver.DNI = item.DNI;
                                        driver.Nombres = item.Nombres;
                                        driver.TipoLicencia = item.TipoLicencia;
                                        driver.EsCapacitado = item.EsCapacitado;
                                        driver.Observacion = item.Observacion;
                                        try
                                        {
                                            Modelo.SubmitChanges();
                                        }
                                        catch (Exception Ex)
                                        {
                                            return false;
                                            throw Ex;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Registrar un Chofer Nuevo
                                        SJ_RHTransportistaChofer oDriver = new SJ_RHTransportistaChofer();
                                        oDriver.IdTransportista = CodigoRegistro;
                                        oDriver.Item = item.Item;
                                        oDriver.DNI = item.DNI;
                                        oDriver.Nombres = item.Nombres;
                                        oDriver.TipoLicencia = item.TipoLicencia;
                                        oDriver.EsCapacitado = item.EsCapacitado;
                                        oDriver.Observacion = item.Observacion;
                                        oDriver.IdEstado = item.IdEstado;
                                        try
                                        {
                                            Modelo.SJ_RHTransportistaChofer.InsertOnSubmit(oDriver);
                                            Modelo.SubmitChanges();
                                        }
                                        catch (Exception Ex)
                                        {
                                            return false;
                                            throw Ex;

                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Actualizar o agregar registros de Contratos()
                            if (contracts.ToList().Count > 0)
                            {

                                foreach (SJ_RHTransportistaContrato contract in contracts)
                                {
                                    var result = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == contract.Id && x.Item.ToString().Trim() == contract.Item.ToString().Trim()).ToList();
                                    if (result.Count == 1)
                                    {
                                        #region Actualizar Chofer()
                                        SJ_RHTransportistaContrato oContract = new SJ_RHTransportistaContrato();
                                        oContract = result.Single();
                                        //oContract.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).LastOrDefault().Item != null ? Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).LastOrDefault().Item.TrimStart('0') + 1).ToString().PadLeft(3, '0') : "001";
                                        oContract.FechaInicio = contract.FechaInicio;
                                        oContract.FechaTermino = contract.FechaTermino;
                                        oContract.Observacion = contract.Observacion;
                                        oContract.IdEstado = contract.IdEstado;
                                        oContract.TypeDocumentId = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().description != null ? Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().TypeDocumentId : 0;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result.Count == 0)
                                    {
                                        #region Registrar un SJ_RHTransportistaContrato Nuevo
                                        SJ_RHTransportistaContrato oContract = new SJ_RHTransportistaContrato();
                                        oContract.Id = CodigoRegistro;
                                        oContract.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).ToList().Count > 0 ?
                                        (Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).Max(x => x.Item).TrimStart('0')) + 1).ToString().PadLeft(3, '0') :
                                        "001";
                                        oContract.FechaInicio = contract.FechaInicio;
                                        oContract.FechaTermino = contract.FechaTermino;
                                        oContract.Observacion = contract.Observacion;
                                        oContract.IdEstado = contract.IdEstado;
                                        oContract.TypeDocumentId = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().description != null ? Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().TypeDocumentId : 0;
                                        Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oContract);
                                        Modelo.SubmitChanges();

                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Actualizar o agregar registros de Ruta()
                            if (routes.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaRuta item in routes)
                                {
                                    //if (Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == CodigoRegistro && x.IdRuta.ToString().Trim() == item.IdRuta.ToString().Trim()  ).ToList().Count == 1)
                                    if (Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).ToList().Count == 1)
                                    {
                                        #region Actualizar Ruta()
                                        SJ_RHTransportistaRuta oRoute = new SJ_RHTransportistaRuta();
                                        oRoute = Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).Single();
                                        oRoute.Item = item.Item;
                                        oRoute.IdRuta = item.IdRuta;
                                        oRoute.PrecioFlete = item.PrecioFlete;
                                        oRoute.PrecioPersona = item.PrecioPersona;
                                        oRoute.PrecioVuelta = item.PrecioVuelta;
                                        oRoute.Observacion = item.Observacion;
                                        //oRuta.IdEstado = item.IdEstado;
                                        try
                                        {
                                            Modelo.SubmitChanges();
                                        }
                                        catch (Exception Ex)
                                        {

                                            throw Ex;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Registrar una Ruta Nueva
                                        SJ_RHTransportistaRuta oRoute = new SJ_RHTransportistaRuta();
                                        oRoute.Id = CodigoRegistro;
                                        oRoute.Item = item.Item;
                                        oRoute.IdRuta = item.IdRuta;
                                        oRoute.PrecioFlete = item.PrecioFlete;
                                        oRoute.PrecioPersona = item.PrecioPersona;
                                        oRoute.PrecioVuelta = item.PrecioVuelta;
                                        oRoute.Observacion = item.Observacion;
                                        oRoute.IdEstado = item.IdEstado;
                                        try
                                        {
                                            Modelo.SJ_RHTransportistaRutas.InsertOnSubmit(oRoute);
                                            Modelo.SubmitChanges();
                                        }
                                        catch (Exception Ex)
                                        {

                                            throw Ex;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Actualizar o agregar registros de Documentos()
                            if (documents.ToList().Count > 0)
                            {
                                foreach (SJ_RHTransportistaContrato document in documents)
                                {
                                    var result = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == document.Id && x.Item.ToString().Trim() == document.Item.ToString().Trim()).ToList();
                                    if (result.Count == 1)
                                    {
                                        #region Actualizar Chofer()
                                        SJ_RHTransportistaContrato oDocument = new SJ_RHTransportistaContrato();
                                        oDocument = result.Single();
                                        //oDocument.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).LastOrDefault().Item != null ? Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).LastOrDefault().Item.TrimStart('0') + 1).ToString().PadLeft(3, '0') : "001";
                                        oDocument.FechaInicio = document.FechaInicio;
                                        oDocument.FechaTermino = document.FechaTermino;
                                        oDocument.Observacion = document.Observacion;
                                        oDocument.IdEstado = document.IdEstado;
                                        oDocument.TypeDocumentId = document.TypeDocumentId;
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                    else if (result.Count == 0)
                                    {
                                        #region Registrar un SJ_RHTransportistaContrato Nuevo
                                        SJ_RHTransportistaContrato oDocument = new SJ_RHTransportistaContrato();
                                        oDocument.Id = CodigoRegistro;
                                        oDocument.Item = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).ToList().Count > 0 ?
                                        (Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).Max(x => x.Item).TrimStart('0')) + 1).ToString().PadLeft(3, '0') :
                                        "001";
                                        oDocument.FechaInicio = document.FechaInicio;
                                        oDocument.FechaTermino = document.FechaTermino;
                                        oDocument.Observacion = document.Observacion;
                                        oDocument.IdEstado = document.IdEstado;
                                        oDocument.TypeDocumentId = document.TypeDocumentId;
                                        Modelo.SJ_RHTransportistaContrato.InsertOnSubmit(oDocument);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            status = true;
                        }
                        catch (Exception Ex)
                        {
                            return false;
                            throw Ex;
                        }

                        #endregion
                    }

                }

                #endregion
                Scope.Complete();
            }

            return status;
        }

        public void EliminarTransportista(int CodigoRegistro, string conection)
        {
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transacción()

            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.SJ_RHEliminarTransportista(CodigoRegistro);
            }
            #endregion

            //Scope.Complete();
            //}
        }

        public void AnularTransportista(int CodigoRegistro, string conection)
        {
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transacción()

            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                if (Modelo.SJ_RHTransportista.Where(x => x.Id == CodigoRegistro).ToList().Count == 1)
                {
                    //Anular
                    try
                    {
                        SJ_RHTransportista oTransportista = new SJ_RHTransportista();
                        oTransportista = Modelo.SJ_RHTransportista.Where(x => x.Id == CodigoRegistro).Single();

                        if (oTransportista.IdEstado == "AN")
                        {
                            oTransportista.IdEstado = "AC";
                        }
                        else
                        {
                            oTransportista.IdEstado = "AN";
                        }

                        Modelo.SubmitChanges();
                    }
                    catch (Exception Ex)
                    {

                        throw Ex;
                    }

                }

            }
            #endregion

            //Scope.Complete();
            //}
        }

        #region Metodos del Objeto Detalle Chofer()

        public List<SJ_RHListarDetalleTransportistaChoferResult> ListarDetalleChofer(int Codigo, string conection)
        {
            var ListadoChofer = new List<SJ_RHListarDetalleTransportistaChoferResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                ListadoChofer = Modelo.SJ_RHListarDetalleTransportistaChofer(Codigo).ToList();
            }
            return ListadoChofer;
        }

        public void EliminarChofer(SJ_RHTransportistaChofer driver, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {

                    SJ_RHTransportistaChofer Chofer = new SJ_RHTransportistaChofer();
                    if (Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == driver.Id && x.Item.ToString().Trim() == driver.Item.ToString().Trim()).ToList().Count == 1)
                    {
                        Chofer = Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == driver.Id && x.Item.ToString().Trim() == driver.Item.ToString().Trim()).Single();
                        try
                        {
                            Modelo.SJ_RHTransportistaChofer.DeleteOnSubmit(Chofer);
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                    Modelo.Connection.Close();
                }
                #endregion
                Scope.Complete();
            }
        }

        #endregion

        #region Metodos del Objeto Detalle Contrato()

        public List<SJ_RHListarDetalleTransportistaContratoResult> ListContractByCarrierId(int carrierId, string conection)
        {
            var ListadoContrato = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                int typeDocumentContract = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().description != null ? Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().TypeDocumentId : 0;
                ListadoContrato = Modelo.SJ_RHListarDetalleTransportistaContrato(carrierId).Where(x => x.TypeDocumentId == typeDocumentContract).ToList();

            }
            return ListadoContrato;
        }

        public void EliminarContrato(SJ_RHTransportistaContrato contract, string conection)
        {
            #region Transacción()

            using (TransactionScope Scope = new TransactionScope())
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {
                    #region Transacción SQL()
                    SJ_RHTransportistaContrato Contrato = new SJ_RHTransportistaContrato();
                    if (Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == contract.Id && x.Item.ToString().Trim() == contract.Item.ToString().Trim()).ToList().Count == 1)
                    {
                        Contrato = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == contract.Id && x.Item.ToString().Trim() == contract.Item.ToString().Trim()).Single();
                        try
                        {
                            Modelo.SJ_RHTransportistaContrato.DeleteOnSubmit(Contrato);
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                    Modelo.Connection.Close();
                    #endregion
                }
                Scope.Complete();
            }
            #endregion
        }
        #endregion

        #region Metodo del Objeto Detalle Ruta()
        public List<SJ_RHListarDetalleTransportistaRutaResult> ListRouterByCarrierId(int carrierId, string conection)
        {
            var ListadoRuta = new List<SJ_RHListarDetalleTransportistaRutaResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                ListadoRuta = Modelo.SJ_RHListarDetalleTransportistaRuta(carrierId).ToList();
            }
            return ListadoRuta;
        }

        public void EliminarRuta(SJ_RHTransportistaRuta carrier, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {

                    SJ_RHTransportistaRuta Ruta = new SJ_RHTransportistaRuta();
                    if (Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == carrier.Id && x.IdRuta.ToString().Trim() == carrier.IdRuta.ToString().Trim()).ToList().Count == 1)
                    {
                        Ruta = Modelo.SJ_RHTransportistaRutas.Where(x => x.Id == carrier.Id && x.IdRuta.ToString().Trim() == carrier.IdRuta.ToString().Trim()).Single();
                        try
                        {
                            Modelo.SJ_RHTransportistaRutas.DeleteOnSubmit(Ruta);
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                    Modelo.Connection.Close();
                }
                #endregion
                Scope.Complete();
            }
        }
        #endregion


        #region Metodos del Objeto Detalle Documentos()

        public List<SJ_RHListarDetalleTransportistaContratoResult> ListDocumentsByCarrierId(int carrierId, string conection)
        {
            var documents = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                int typeDocumentContract = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().description != null ? Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.description.Contains("CONTRATO")).FirstOrDefault().TypeDocumentId : 0;
                documents = Modelo.SJ_RHListarDetalleTransportistaContrato(carrierId).Where(x => x.TypeDocumentId != typeDocumentContract).ToList();
            }
            return documents;
        }


        #endregion


        public List<DFormatoSimple> GetRutasSistemaTransportistas(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();

            List<SJ_RHListaRutasResult> rutas = new List<SJ_RHListaRutasResult>();

            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                rutas = Modelo.SJ_RHListaRutas().Where(x => x.IdEstado.ToString().Trim() == "AC").ToList();
                listado = (from items in rutas

                           select new DFormatoSimple
                           {
                               Codigo = items.Id.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = items.descripcionCortaOrigen.ToString().Trim() + " / " + items.descripcionCortaDestino.ToString().Trim()
                           }).ToList();
            }

            return listado;
        }

        public string ObtenerItemByDocumento(int CodigoRegistro, string conection)
        {
            string cnx = string.Empty;
            string item = "001";
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                return Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).ToList().Count > 0 ?
                                       (Convert.ToInt32(Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == CodigoRegistro).Max(x => x.Item).TrimStart('0')) + 1).ToString().PadLeft(3, '0') :
                                       "001";
            }
            return item;
        }

        public List<DFormatoSimple> GetTypeDocumentoFormat(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();

            List<SJ_RHTransportistaTipoDocumento> typeDocuments = new List<SJ_RHTransportistaTipoDocumento>();

            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                typeDocuments = Modelo.SJ_RHTransportistaTipoDocumento.Where(x => x.status == 1).ToList();
                listado = (from typeDocument in typeDocuments

                           select new DFormatoSimple
                           {
                               Codigo = typeDocument.TypeDocumentId.ToString(),
                               //Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                               Descripcion = typeDocument.description.ToString().Trim()
                           }).ToList();
            }

            return listado;
        }


        public List<SJ_RHTransportistaChoferListarResult> ObtenerListaChoferesxTransportista(string RUCTransportista, string conection)
        {
            List<SJ_RHTransportistaChoferListarResult> Choferes = new List<SJ_RHTransportistaChoferListarResult>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Choferes = Modelo.SJ_RHTransportistaChoferListar(RUCTransportista).ToList();
            }

            return Choferes;
        }

        //obtener listado de Movimientos de facturacion por CarrierId
        public List<SJ_ListaMovimientoFacturacionPorProveedorResult> ObtenerListaMovimientoFacturacionTransportistaPorProveedor(int carrierId, string conection)
        {
            List<SJ_ListaMovimientoFacturacionPorProveedorResult> lista = new List<SJ_ListaMovimientoFacturacionPorProveedorResult>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                lista = Modelo.SJ_ListaMovimientoFacturacionPorProveedor(carrierId).ToList();
            }

            return lista;
        }

        //obtener listado de partes de recorrido por CarrierId
        public List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult> ObtenerListaMovimientoParteRecorridoTransportistaPorProveedor(int carrierId, string conection)
        {
            List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult> lista = new List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                lista = Modelo.SJ_ListaMovimientoPartesRecorridoPorProveedor(carrierId).ToList();
            }

            return lista;
        }

        //obtener listado de transferencias de asistencia
        public List<ASJ_RegistroTransferenciaTransportes>GetListAsistanceTransferByPlaca(int carrierId, string conection, string placa)
        {
            var lista = new List<ASJ_RegistroTransferenciaTransportes>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                lista = Modelo.ASJ_RegistroTransferenciaTransportes.Where(x => x.placa.Trim() == placa.Trim()).ToList();
            }

            return lista;
        }
        

        //obtener listado de transferencias de asistencia observadas
        public List<ASJ_DCONTROLINGRESOSALIDA_PERSONAL> GetListAsistanceObserveredTransferByPlaca(int carrierId, string conection, string placa)
        {
            var lista = new List<ASJ_DCONTROLINGRESOSALIDA_PERSONAL>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                lista = Modelo.ASJ_DCONTROLINGRESOSALIDA_PERSONAL.Where(x => x.garita.Trim() == placa.Trim()).ToList();
            }

            return lista;
        }


        public bool VerificarDuplicidadPlaca(string numeroPlaca, string conection)
        {
            bool estado = false;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                string cadena = numeroPlaca.Replace(" ", "");
                var resuladoConsulta = Modelo.SJ_RHTransportista.Where(x => x.Placa.ToString().Trim().Replace(" ", "") == cadena).ToList();

                estado = false;
                if (resuladoConsulta != null && resuladoConsulta.ToList().Count > 0)
                {
                    estado = true;
                }

            }
            return estado;
        }

        public string ObtenerNombreComercialByNroRUC(string numeroRUC, string conection)
        {
            string nombreComercial = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {

                var resuladoConsulta = Modelo.SJ_RHTransportista.Where(x => x.idclieprov.ToString().Trim().Replace(" ", "") == numeroRUC.ToString().Trim()).ToList();
                if (resuladoConsulta != null && resuladoConsulta.ToList().Count > 0)
                {
                    nombreComercial = resuladoConsulta.FirstOrDefault().NombreCorto != null ? resuladoConsulta.FirstOrDefault().NombreCorto.ToString().Trim().ToUpper() : "";
                }

            }
            return nombreComercial;
        }


        public List<SJ_RHListadoVencimientoDocumentosByUnidadTransportesResult> GetExpirationOfDocumentsPerTransportationUnit(string conection)
        {
            List<SJ_RHListadoVencimientoDocumentosByUnidadTransportesResult> expirationOfDocuments = new List<SJ_RHListadoVencimientoDocumentosByUnidadTransportesResult>();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                expirationOfDocuments = Modelo.SJ_RHListadoVencimientoDocumentosByUnidadTransportes().ToList();
            }
            return expirationOfDocuments;
        }

    }
}

