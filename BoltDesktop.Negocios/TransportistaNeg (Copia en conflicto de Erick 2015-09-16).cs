using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensionistasRefrigerios.Datos;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding;
using MyControlsDataBinding.Busquedas;

namespace PensionistasRefrigerios.Negocios
{
    public class TransportistaNeg
    {
        private string cnx;
        private List<SJ_RHListarDetalleTransportistaContratoResult> ListadoContrato;
        private List<SJ_RHListarDetalleTransportistaChoferResult> ListadoChofer;
        private List<SJ_RHListarDetalleTransportistaRutaResult> ListadoRuta;

        private SJ_RHTransportista oTransportista;
        private SJ_RHTransportistaContrato oContrato;
        private SJ_RHTransportistaChofer oChofer;
        private SJ_RHTransportistaRuta oRuta;

        public List<SJ_RHListarTransportistasResult> ListarTransportistas()
        {
            List<SJ_RHListarTransportistasResult> Listado = new List<SJ_RHListarTransportistasResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                Listado = Modelo.SJ_RHListarTransportistas().ToList();
            }
            return Listado;
        }

        public List<SJ_RHListarTransportistasResult> ListarTransportistasActivos()
        {
            List<SJ_RHListarTransportistasResult> Listado = new List<SJ_RHListarTransportistasResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                Listado = Modelo.SJ_RHListarTransportistas().Where(x => x.IdEstado == "AC").ToList();
            }
            return Listado;
        }

        //Registrar Datos con Detalles del Transportista()
        public Int32 RegistrarTransportista(SJ_RHTransportista ObjetoTransportista, List<SJ_RHTransportistaChofer> listadoChoferes, List<SJ_RHTransportistaContrato> listadoContratos, List<SJ_RHTransportistaChofer> ListaEliminadosChoferes, List<SJ_RHTransportistaContrato> ListaEliminadosContratos, List<SJ_RHTransportistaRuta> ListaEliminadosRuta, List<SJ_RHTransportistaRuta> ListadoRutas)
        {
            Int32 CodigoRegistro;
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd2014"].ToString();

                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {

                    // 1.- Genero la cabecera
                    // 2.- Con el Id Obtenido Actualizo o registro los detalles de los choferes y contratos y rutas

                    if (ObjetoTransportista.Id == 0)
                    {
                        #region Registro Nuevo()

                        // Registro Cabecera --> SJ_RHTransportista
                        oTransportista = new SJ_RHTransportista();
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
                        oTransportista.EsInterLocal = ObjetoTransportista.EsInterLocal;
                        oTransportista.EsMovilidadLocal = ObjetoTransportista.EsMovilidadLocal;
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
                                    oChofer = new SJ_RHTransportistaChofer();
                                    oChofer.IdTransportista = item.IdTransportista;
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
                                    oContrato = new SJ_RHTransportistaContrato();
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
                                    oRuta = new SJ_RHTransportistaRuta();
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
                                        Modelo.SJ_RHTransportistaRuta.InsertOnSubmit(oRuta);
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
                        oTransportista = new SJ_RHTransportista();
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
                        oTransportista.EsInterLocal = ObjetoTransportista.EsInterLocal;
                        oTransportista.EsMovilidadLocal = ObjetoTransportista.EsMovilidadLocal;
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
                                            EliminarChofer(item);
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
                                            EliminarContrato(item);
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
                                            EliminarRuta(item);
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
                                        oChofer = new SJ_RHTransportistaChofer();
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
                                        oChofer = new SJ_RHTransportistaChofer();
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
                                        oContrato = new SJ_RHTransportistaContrato();
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
                                        oContrato = new SJ_RHTransportistaContrato();
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
                                    if (Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == CodigoRegistro && x.IdRuta.ToString().Trim() == item.IdRuta.ToString().Trim()).ToList().Count == 1)
                                    {
                                        #region Actualizar Ruta()
                                        oRuta = new SJ_RHTransportistaRuta();
                                        oRuta = Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == CodigoRegistro && x.Item.ToString().Trim() == item.Item.ToString().Trim()).Single();
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
                                        oRuta = new SJ_RHTransportistaRuta();
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
                                            Modelo.SJ_RHTransportistaRuta.InsertOnSubmit(oRuta);
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
                Scope.Complete();
            }

            return CodigoRegistro;
        }

        public void EliminarTransportista(int CodigoRegistro)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {
                    Modelo.SJ_RHEliminarTransportista(CodigoRegistro);
                }
                #endregion

                Scope.Complete();
            }
        }

        public void AnularTransportista(int CodigoRegistro)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {
                    if (Modelo.SJ_RHTransportista.Where(x => x.Id == CodigoRegistro).ToList().Count == 1)
                    {
                        //Anular
                        try
                        {
                            SJ_RHTransportista oTransportista = new SJ_RHTransportista();
                            oTransportista = Modelo.SJ_RHTransportista.Where(x => x.Id == CodigoRegistro).Single();
                            oTransportista.IdEstado = "AN";
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

        #region Metodos del Objeto Detalle Chofer()

        public List<SJ_RHListarDetalleTransportistaChoferResult> ListarDetalleChofer(int Codigo)
        {
            ListadoChofer = new List<SJ_RHListarDetalleTransportistaChoferResult>();
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                ListadoChofer = Modelo.SJ_RHListarDetalleTransportistaChofer(Codigo).ToList();
            }
            return ListadoChofer;
        }

        public void EliminarChofer(SJ_RHTransportistaChofer Objeto)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {

                    SJ_RHTransportistaChofer Chofer = new SJ_RHTransportistaChofer();
                    if (Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == Objeto.Id && x.Item.ToString().Trim() == Objeto.Item.ToString().Trim()).ToList().Count == 1)
                    {
                        Chofer = Modelo.SJ_RHTransportistaChofer.Where(x => x.IdTransportista == Objeto.Id && x.Item.ToString().Trim() == Objeto.Item.ToString().Trim()).Single();
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

        public List<SJ_RHListarDetalleTransportistaContratoResult> ListarDetalleContrato(int Codigo)
        {
            ListadoContrato = new List<SJ_RHListarDetalleTransportistaContratoResult>();
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                ListadoContrato = Modelo.SJ_RHListarDetalleTransportistaContrato(Codigo).ToList();
            }
            return ListadoContrato;
        }

        public void EliminarContrato(SJ_RHTransportistaContrato Objeto)
        {
            #region Transacción()

            using (TransactionScope Scope = new TransactionScope())
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {
                    #region Transacción SQL()
                    SJ_RHTransportistaContrato Contrato = new SJ_RHTransportistaContrato();
                    if (Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == Objeto.Id && x.Item.ToString().Trim() == Objeto.Item.ToString().Trim()).ToList().Count == 1)
                    {
                        Contrato = Modelo.SJ_RHTransportistaContrato.Where(x => x.Id == Objeto.Id && x.Item.ToString().Trim() == Objeto.Item.ToString().Trim()).Single();
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
        public List<SJ_RHListarDetalleTransportistaRutaResult> ListarDetalleRuta(int Codigo)
        {
            ListadoRuta = new List<SJ_RHListarDetalleTransportistaRutaResult>();
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                ListadoRuta = Modelo.SJ_RHListarDetalleTransportistaRuta(Codigo).ToList();
            }
            return ListadoRuta;
        }

        public void EliminarRuta(SJ_RHTransportistaRuta Objeto)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
                {

                    SJ_RHTransportistaRuta Ruta = new SJ_RHTransportistaRuta();
                    if (Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == Objeto.Id && x.IdRuta.ToString().Trim() == Objeto.IdRuta.ToString().Trim()).ToList().Count == 1)
                    {
                        Ruta = Modelo.SJ_RHTransportistaRuta.Where(x => x.Id == Objeto.Id && x.IdRuta.ToString().Trim() == Objeto.IdRuta.ToString().Trim()).Single();
                        try
                        {
                            Modelo.SJ_RHTransportistaRuta.DeleteOnSubmit(Ruta);
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

        public List<DFormatoSimple> GetRutas()
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();

            List<SJ_RHListaRutasResult> rutas = new List<SJ_RHListaRutasResult>();

            string cnx;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Contexto = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                rutas = Contexto.SJ_RHListaRutas().Where(x => x.IdEstado.ToString().Trim() == "AC").ToList();
                listado = (from items in rutas

                           select new DFormatoSimple
                           {
                               Codigo = items.Id.ToString(),
                               Descripcion = items.DistritoOrigen.ToString().Trim() + " / " + items.DistritoDestino.ToString().Trim()
                           }).ToList();
            }

            return listado;
        }

        public List<SJ_RHTransportistaChoferListarResult> ObtenerListaChoferesxTransportista(int codigoTransportista, string periodo)
        {
            List<SJ_RHTransportistaChoferListarResult> Choferes = new List<SJ_RHTransportistaChoferListarResult>();

            cnx = ConfigurationManager.AppSettings["bd" + periodo].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                Choferes = Modelo.SJ_RHTransportistaChoferListar(codigoTransportista).ToList();
            }

            return Choferes;
        }

        public List<SJ_ListaMovimientoFacturacionPorProveedorResult> ObtenerListaMovimientoFacturacionTransportistaPorProveedor(int codigoTransportista)
        {
            List<SJ_ListaMovimientoFacturacionPorProveedorResult> lista  = new List<SJ_ListaMovimientoFacturacionPorProveedorResult>();

            cnx = ConfigurationManager.AppSettings["bd2014" ].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                lista = Modelo.SJ_ListaMovimientoFacturacionPorProveedor(codigoTransportista).ToList();
            }

            return lista;
        }


        public List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult> ObtenerListaMovimientoParteRecorridoTransportistaPorProveedor(int codigoTransportista)
        {
            List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult> lista = new List<SJ_ListaMovimientoPartesRecorridoPorProveedorResult>();

            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                lista = Modelo.SJ_ListaMovimientoPartesRecorridoPorProveedor(codigoTransportista).ToList();
            }

            return lista;
        }

    }
}
