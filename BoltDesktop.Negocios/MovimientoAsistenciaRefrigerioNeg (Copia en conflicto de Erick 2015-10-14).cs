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
    public class MovimientoAsistenciaRefrigerioNeg
    {
        private SJ_RHPensionRefrigerioPersona Personal;
        private string nuevoCodigo;
        private SJM_Pensiones tranferenciaPension;
        private List<SJ_RHPensionRefrigerioBuscarPersonaResult> listadoTodoPersonalActivoBD = new List<SJ_RHPensionRefrigerioBuscarPersonaResult>();
        private decimal? importeDistribuir;
        private decimal? totalMinutosTrabajador;
        private decimal? totalRacimosTrabajador;

        //Listado de todos los documentos con movimiento de registro de refrigerio
        public List<SJ_RHMovimientoAsistenciaRefrigeriosResult> ListarMovimientosAsistenciaRefrigerio(string Desde, string Hasta, string periodo)
        {
            List<SJ_RHMovimientoAsistenciaRefrigeriosResult> Listado = new List<SJ_RHMovimientoAsistenciaRefrigeriosResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + periodo.ToString().Trim()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                var lista = Modelo.SJ_RHMovimientoAsistenciaRefrigerios(Desde, Hasta).ToList();


                Listado = (from item in lista
                           where item.codigo != "" && item.codigo != null
                           group item by new { item.codigo } into j
                           select new SJ_RHMovimientoAsistenciaRefrigeriosResult
                           {

                               idempresa = j.FirstOrDefault().idempresa != null ? j.FirstOrDefault().idempresa.ToString().Trim() : "001",
                               idsucursal = j.FirstOrDefault().idsucursal != null ? j.FirstOrDefault().idsucursal.ToString().Trim() : "002",
                               codigo = j.Key.codigo.ToString().Trim(),
                               fecha = j.FirstOrDefault().fecha != null ? j.FirstOrDefault().fecha : "",
                               Documento = j.FirstOrDefault().Documento != null ? j.FirstOrDefault().Documento.ToString().Trim() : "",
                               //iddocumento = j.FirstOrDefault().iddocumento != null ? j.FirstOrDefault().iddocumento.ToString().Trim() : "RAR",
                               //serie = j.FirstOrDefault().serie != null ? j.FirstOrDefault().serie.ToString().Trim() : "0001",
                               //numero = j.FirstOrDefault().numero != null ? j.FirstOrDefault().numero.ToString().Trim() : "0000000",
                               numeroManual = j.FirstOrDefault().numeroManual != null ? j.FirstOrDefault().numeroManual.ToString().Trim() : "",
                               Idpension = j.FirstOrDefault().Idpension != null ? j.FirstOrDefault().Idpension.Value : 0,

                               NroDesayunos = j.Max(x => x.NroDesayunos) != null ? j.Max(x => x.NroDesayunos).Value : 0,
                               NroAlmuerzos = j.Max(x => x.NroAlmuerzos) != null ? j.Max(x => x.NroAlmuerzos).Value : 0,
                               NroCenas = j.Max(x => x.NroCenas) != null ? j.Max(x => x.NroCenas).Value : 0,

                               PensionNroRUC = j.FirstOrDefault().PensionNroRUC != null ? j.FirstOrDefault().PensionNroRUC.ToString().Trim() : "",
                               PensionDescripcion = j.FirstOrDefault().PensionDescripcion != null ? j.FirstOrDefault().PensionDescripcion.ToString().Trim() : "",
                               PensionNroDNI = j.FirstOrDefault().PensionNroDNI != null ? j.FirstOrDefault().PensionNroDNI.ToString().Trim() : "",

                               PensionDescripcionComercial = j.FirstOrDefault().PensionDescripcionComercial != null ? j.FirstOrDefault().PensionDescripcionComercial.ToString().Trim() : "",
                               idestado = j.FirstOrDefault().idestado != null ? j.FirstOrDefault().idestado.ToString().Trim() : "PE",
                               Estado = j.FirstOrDefault().Estado != null ? j.FirstOrDefault().Estado.ToString().Trim() : "PENDIENTE",
                           }
                               ).ToList();

            }
            return Listado;
        }

        //Obtengo la cabecera del movimiento del formulario MOVIMIENTO REGISTRO ASISTENCIAS REFRIGERIO
        public List<SJ_RHMovimientoAsistenciaRefrigerioObjetoResult> ObtenerObjetoAsistenciaRefrigerio(string codigo, string periodo)
        {
            List<SJ_RHMovimientoAsistenciaRefrigerioObjetoResult> Objeto = new List<SJ_RHMovimientoAsistenciaRefrigerioObjetoResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + periodo.ToString().Trim()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                var lista = Modelo.SJ_RHMovimientoAsistenciaRefrigerioObjeto(codigo).ToList();


                Objeto = (from item in lista
                          where item.codigo != "" && item.codigo != null
                          group item by new { item.codigo } into j
                          select new SJ_RHMovimientoAsistenciaRefrigerioObjetoResult
                          {

                              idempresa = j.FirstOrDefault().idempresa != null ? j.FirstOrDefault().idempresa.ToString().Trim() : "001",
                              idsucursal = j.FirstOrDefault().idsucursal != null ? j.FirstOrDefault().idsucursal.ToString().Trim() : "002",
                              codigo = j.Key.codigo.ToString().Trim(),
                              fecha = j.FirstOrDefault().fecha != null ? j.FirstOrDefault().fecha : "",
                              Documento = j.FirstOrDefault().Documento != null ? j.FirstOrDefault().Documento.ToString().Trim() : "",
                              iddocumento = j.FirstOrDefault().iddocumento != null ? j.FirstOrDefault().iddocumento.ToString().Trim() : "RAR",
                              serie = j.FirstOrDefault().serie != null ? j.FirstOrDefault().serie.ToString().Trim() : "0001",
                              numero = j.FirstOrDefault().numero != null ? j.FirstOrDefault().numero.ToString().Trim() : "0000000",
                              numeroManual = j.FirstOrDefault().numeroManual != null ? j.FirstOrDefault().numeroManual.ToString().Trim() : "",
                              Idpension = j.FirstOrDefault().Idpension != null ? j.FirstOrDefault().Idpension.Value : 0,

                              NroDesayunos = j.Max(x => x.NroDesayunos) != null ? j.Max(x => x.NroDesayunos).Value : 0,
                              NroAlmuerzos = j.Max(x => x.NroAlmuerzos) != null ? j.Max(x => x.NroAlmuerzos).Value : 0,
                              NroCenas = j.Max(x => x.NroCenas) != null ? j.Max(x => x.NroCenas).Value : 0,

                              PensionNroRUC = j.FirstOrDefault().PensionNroRUC != null ? j.FirstOrDefault().PensionNroRUC.ToString().Trim() : "",
                              PensionDescripcion = j.FirstOrDefault().PensionDescripcion != null ? j.FirstOrDefault().PensionDescripcion.ToString().Trim() : "",
                              PensionNroDNI = j.FirstOrDefault().PensionNroDNI != null ? j.FirstOrDefault().PensionNroDNI.ToString().Trim() : "",

                              PensionDescripcionComercial = j.FirstOrDefault().PensionDescripcionComercial != null ? j.FirstOrDefault().PensionDescripcionComercial.ToString().Trim() : "",
                              idestado = j.FirstOrDefault().idestado != null ? j.FirstOrDefault().idestado.ToString().Trim() : "PE",
                              Estado = j.FirstOrDefault().Estado != null ? j.FirstOrDefault().Estado.ToString().Trim() : "PENDIENTE",
                          }
                               ).ToList();

            }
            return Objeto;
        }

        //Obtengo el detalle de la cabecera del movimiento del formulario MOVIMIENTO REGISTRO ASISTENCIAS REFRIGERIO
        public List<SJ_RHMovimientoAsistenciaRefrigerioObjetoDetalleResult> ObtenerObjetoDetalleAsistenciaRefrigerio(string codigo, string dniPension, string Desde, string Hasta)
        {
            List<SJ_RHMovimientoAsistenciaRefrigerioObjetoDetalleResult> Listado = new List<SJ_RHMovimientoAsistenciaRefrigerioObjetoDetalleResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 1800000;
                try
                {
                    Listado = Modelo.SJ_RHMovimientoAsistenciaRefrigerioObjetoDetalle(codigo, dniPension, Desde, Hasta).ToList();
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }


            }
            return Listado;
        }

        //Obtener el nombre del trabajador x numero de DNI, solo si estan activos.
        public string ObtenerNombresCompletosxDNI(string nroDni)
        {
            string cnx, Nombres = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 1800000;
                try
                {
                    if (Modelo.SJ_RHPensionRefrigerioObtenerNombresxDNI(nroDni) != null)
                    {
                        if (Modelo.SJ_RHPensionRefrigerioObtenerNombresxDNI(nroDni).ToList().Count == 1)
                        {
                            Nombres = Modelo.SJ_RHPensionRefrigerioObtenerNombresxDNI(nroDni) != null ? Modelo.SJ_RHPensionRefrigerioObtenerNombresxDNI(nroDni).FirstOrDefault().NOMBRE.ToString() : "";
                        }
                        else
                        {
                            Nombres = "";
                        }
                    }
                    else
                    {
                        Nombres = "";
                    }


                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }
            return Nombres;
        }

        public string RegistrarAsistenciaRefrigerios(List<SJM_Pensiones> Listado, List<SJM_Pensiones> ListadoEliminados)
        {
            string codigo = string.Empty;
            //using (TransactionScope Scope = new TransactionScope())
            //{
            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                #region Eliminar Lista de Eliminados()
                if (ListadoEliminados != null)
                {
                    if (ListadoEliminados.ToList().Count > 0)
                    {
                        #region
                        foreach (SJM_Pensiones item in Listado)
                        {
                            try
                            {
                                if (Modelo.SJM_Pensiones.Where(x => x.IdPension == item.IdPension).ToList().Count == 1)
                                {
                                    SJM_Pensiones Obj = new SJM_Pensiones();
                                    Obj = Modelo.SJM_Pensiones.Where(x => x.IdPension == item.IdPension).Single();
                                    Modelo.SJM_Pensiones.DeleteOnSubmit(Obj);
                                    Modelo.SubmitChanges();
                                }
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

                nuevoCodigo = ObtenerCodigoRegistroAsistenciaRefrigerio().ToString().Trim();

                #region Registro()
                foreach (SJM_Pensiones item in Listado)
                {
                    if (item.IdPension == null || item.IdPension.ToString().Trim() == "")
                    {
                        #region Nuevo()
                        try
                        {
                            if (item.IdPension == 0)
                            {
                                #region Nuevo Registro()
                                SJM_Pensiones Obj = new SJM_Pensiones();
                                //Obj.IdPension = nuevoCodigo;
                                Obj.IdPension = item.IdPension;
                                Obj.DniPension = item.DniPension;
                                Obj.NombresPension = item.NombresPension;
                                Obj.DniTrabajador = item.DniTrabajador;
                                Obj.NombresTrabajador = item.NombresTrabajador;
                                Obj.TipoComida = item.TipoComida;
                                Obj.FechaPension = item.FechaPension;
                                Obj.FechaRegistro = item.FechaRegistro;
                                Obj.EsProcesado = item.EsProcesado;
                                Modelo.SJM_Pensiones.InsertOnSubmit(Obj);
                                Modelo.SubmitChanges();
                                codigo = Obj.IdPension.ToString();
                                #endregion
                            }
                            else
                            {
                                #region Actualizar()
                                if (Modelo.SJM_Pensiones.Where(x => x.IdPension == null && x.IdPension == item.IdPension).ToList().Count == 1)
                                {
                                    Modelo.CommandTimeout = 1890;
                                    SJM_Pensiones Obj = new SJM_Pensiones();
                                    Obj = Modelo.SJM_Pensiones.Where(x => x.IdPension == null && x.IdPension == item.IdPension).Single();
                                    //Obj.IdPension = nuevoCodigo;
                                    Obj.DniTrabajador = item.DniTrabajador;
                                    Obj.NombresTrabajador = item.NombresTrabajador;
                                    Obj.TipoComida = item.TipoComida;
                                    //Obj.FechaPension = item.FechaPension;
                                    //Obj.FechaRegistro = item.FechaRegistro;
                                    //Obj.EsProcesado = item.EsProcesado;
                                    Modelo.SubmitChanges();
                                    codigo = Obj.IdPension.ToString();
                                }
                                #endregion
                            }
                        }
                        catch (Exception Ex)
                        {

                            Ex.Message.ToString(); ;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Actualizar()
                        try
                        {
                            if (Modelo.SJM_Pensiones.Where(x => x.IdPension == item.IdPension && x.IdPension == item.IdPension).ToList().Count == 1)
                            {
                                SJM_Pensiones Obj = new SJM_Pensiones();
                                Obj = Modelo.SJM_Pensiones.Where(x => x.IdPension == item.IdPension && x.IdPension == item.IdPension).Single();
                                //Obj.IdPension = item.IdPension;
                                //Obj.DniPension = item.DniPension;
                                //Obj.NombresPension = item.NombresPension;
                                Obj.DniTrabajador = item.DniTrabajador;
                                Obj.NombresTrabajador = item.NombresTrabajador;
                                Obj.TipoComida = item.TipoComida;
                                //Obj.FechaPension = item.FechaPension;
                                //Obj.FechaRegistro = item.FechaRegistro;
                                //Obj.EsProcesado = item.EsProcesado;
                                Modelo.SubmitChanges();
                                codigo = Obj.IdPension.ToString();
                            }
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                        #endregion
                    }
                }
                #endregion
                //}
                //Scope.Complete();
            }
            return codigo;
        }

        public string ObtenerCodigoRegistroAsistenciaRefrigerio()
        {
            string codigo = string.Empty;
            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (SJ_RHFacturacionTransportistaDataContext Modelo = new SJ_RHFacturacionTransportistaDataContext(cnx))
            {
                codigo = Modelo.ObtenerId().FirstOrDefault().Codigo != null ? Modelo.ObtenerId().FirstOrDefault().Codigo.ToString().Trim() : "";
            }
            return codigo;
        }

        // LISTAR TODO EL PERSONAL
        public List<SJ_RHPensionRefrigerioPersonaListarResult> ListarRefrigerioxPersona()
        {
            List<SJ_RHPensionRefrigerioPersonaListarResult> Lista = new List<SJ_RHPensionRefrigerioPersonaListarResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 1800000;

                try
                {
                    Lista = Modelo.SJ_RHPensionRefrigerioPersonaListar().ToList();
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return Lista;
        }

        // para el boton buscar personal
        public List<SJ_RHPensionRefrigerioBuscarPersonaxCodigoResult> ObtenerInformacionGeneralDePersonaxCodigo(string IdCodigoGeneral)
        {
            List<SJ_RHPensionRefrigerioBuscarPersonaxCodigoResult> Listado = new List<SJ_RHPensionRefrigerioBuscarPersonaxCodigoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 180000;

                // Obtengo la lista de todos los trabajadores activos del 2014
                var ListadoTodos = Modelo.SJ_RHPensionRefrigerioBuscarPersonaxCodigo(IdCodigoGeneral).ToList();
                // Obtengo todos el personal registrado sin importar su estado
                var ListarTodosExistentes = Modelo.SJ_RHPensionRefrigerioPersonalRegistrado().ToList();
                // Creo un arreglo para obtener el codigo del trabajador
                List<string> ListarCodigoPersonalExistente = new List<string>();

                foreach (var item in ListarTodosExistentes)
                {
                    // lleno mi lista a comparar
                    ListarCodigoPersonalExistente.Add(item.IdCodigoPersonal.ToString().Trim());
                }

                Listado = (from items in ListadoTodos.ToList()
                           where !(ListarCodigoPersonalExistente.Contains(items.IdCodigoPersonal.ToString()))
                           select items
                           ).ToList();

                Modelo.Connection.Close();
            }

            return Listado;
        }

        public List<SJ_RHPensionRefrigerioPersonalRegistradoResult> ObtenerListaPersonalRegistrado()
        {
            List<SJ_RHPensionRefrigerioPersonalRegistradoResult> Listado = new List<SJ_RHPensionRefrigerioPersonalRegistradoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 180000;

                Listado = Modelo.SJ_RHPensionRefrigerioPersonalRegistrado().ToList();

                Modelo.Connection.Close();
            }
            return Listado;
        }

        public List<SJ_RHPensionRefrigerioPersonalRegistradoResult> ObtenerListaPersonalRegistrado(int Codigo)
        {
            List<SJ_RHPensionRefrigerioPersonalRegistradoResult> Listado = new List<SJ_RHPensionRefrigerioPersonalRegistradoResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 180000;

                Listado = Modelo.SJ_RHPensionRefrigerioPersonalRegistrado().Where(x => x.Id == Codigo).ToList();

                Modelo.Connection.Close();
            }
            return Listado;
        }

        public List<SJ_RHPensionRefrigerioBuscarPersonaResult> ObtenerListaPersonalNuevo()
        {
            List<SJ_RHPensionRefrigerioBuscarPersonaResult> Listado = new List<SJ_RHPensionRefrigerioBuscarPersonaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9980000;

                // Obtengo la lista de todos los trabajadores activos del 2014
                var ListadoTodos = Modelo.SJ_RHPensionRefrigerioBuscarPersona().ToList();

                //// Obtengo todos el personal registrado sin importar su estado
                //var ListarTodosExistentes = Modelo.SJ_RHPensionRefrigerioPersonalRegistrado().ToList();
                //// Creo un arreglo para obtener el codigo del trabajador
                //List<string> ListarCodigoPersonalExistente = new List<string>();

                //foreach (var item in ListarTodosExistentes)
                //{
                //    // lleno mi lista a comparar
                //    ListarCodigoPersonalExistente.Add(item.IdCodigoPersonal.ToString().Trim());
                //}

                //Listado = (from items in ListadoTodos.ToList()
                //           where !(ListarCodigoPersonalExistente.Contains(items.IdCodigoPersonal.ToString()))
                //           select items
                //           ).ToList();

                Listado = ListadoTodos;
                Modelo.Connection.Close();
            }

            return Listado;
        }

        public List<SJ_RHPensionRefrigerioPersonaDetalleListadoResult> ObtenerListaPersonalRegistradoDetallado(int Codigo)
        {
            List<SJ_RHPensionRefrigerioPersonaDetalleListadoResult> Listado = new List<SJ_RHPensionRefrigerioPersonaDetalleListadoResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9899900;

                try
                {
                    Listado = Modelo.SJ_RHPensionRefrigerioPersonaDetalleListado(Codigo).ToList();
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return Listado;
        }

        /* Lista de asistencias a las personas que reciben refigerios y que se encuentran pendientes de movimiento de asistencia diario */
        public List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ObtenerListaAsistenciasPersonalPendientesMovimientoAsistencia(string periodo, string fechaDesde, string fechaHasta, string dniPension)
        {
            List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> Listado = new List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + periodo.ToString().Substring(0, 4)].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                try
                {
                    #region Consultar()
                    Listado = Modelo.SJ_RHAsistenciasRefrigeriosPendientesMovimiento(fechaDesde, fechaHasta, dniPension).ToList();
                    #endregion
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
                Modelo.Connection.Close();
                Modelo.Dispose();
            }

            return Listado;
        }

        public void AnularAsistenciaTransferida(string periodo, int codigo)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + periodo.ToString()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                if (Modelo.SJM_Pensiones.Where(x => x.IdPension == codigo).ToList().Count == 1)
                {
                    if (Modelo.SJM_Pensiones.Where(x => x.IdPension == codigo).Single().estado == 1)
                    {
                        #region Anular()
                        tranferenciaPension = new SJM_Pensiones();
                        tranferenciaPension = Modelo.SJM_Pensiones.Where(x => x.IdPension == codigo).Single();
                        tranferenciaPension.estado = 0;
                        Modelo.SubmitChanges();
                        #endregion
                    }
                    else
                    {
                        #region Activar()
                        tranferenciaPension = new SJM_Pensiones();
                        tranferenciaPension = Modelo.SJM_Pensiones.Where(x => x.IdPension == codigo).Single();
                        tranferenciaPension.estado = 1;
                        Modelo.SubmitChanges();
                        #endregion
                    }
                }
                Modelo.Connection.Close();
            }
        }

        public int RegistrarProgramacionAsistenciaRefrigerio(SJ_RHPensionRefrigerioPersona Objeto, List<SJ_RHPensionRefrigerioPersonaDetalle> detalle)
        {
            int Codigo = 0;
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
                {
                    #region Registro / Edicion()
                    Modelo.CommandTimeout = 9998000;


                    if (Objeto.Id == 0)
                    {
                        #region Registro Nuevo
                        try
                        {
                            Personal = new SJ_RHPensionRefrigerioPersona();
                            //Personal.Id = Objeto.Id;
                            Personal.IdCodigoPersonal = Objeto.IdCodigoPersonal;
                            Personal.NroDocumento = Objeto.NroDocumento;
                            Personal.NombresCompletos = Objeto.NombresCompletos;
                            Personal.IdSubPlanilla = Objeto.IdSubPlanilla;
                            Personal.SubPlanilla = Objeto.SubPlanilla;
                            Personal.Condicion = Objeto.Condicion;
                            Personal.IdPension = Objeto.IdPension;
                            Personal.NroDNIPension = Objeto.NroDNIPension;
                            Personal.Pension = Objeto.Pension;
                            Personal.Desayuno = Objeto.Desayuno;
                            Personal.Almuerzo = Objeto.Almuerzo;
                            Personal.Cena = Objeto.Cena;
                            Personal.Otro = Objeto.Otro;
                            Personal.IdEstado = Objeto.IdEstado;

                            Modelo.SJ_RHPensionRefrigerioPersona.InsertOnSubmit(Personal);
                            Modelo.SubmitChanges();

                            Codigo = Personal.Id;

                            if (detalle != null)
                            {
                                #region Detalle()
                                if (detalle.ToList().Count() > 0)
                                {
                                    foreach (SJ_RHPensionRefrigerioPersonaDetalle item in detalle)
                                    {
                                        #region
                                        SJ_RHPensionRefrigerioPersonaDetalle oDetalle = new SJ_RHPensionRefrigerioPersonaDetalle();
                                        oDetalle.Id = Codigo;
                                        oDetalle.Item = item.Item;
                                        oDetalle.ValidoDesde = item.ValidoDesde;
                                        oDetalle.ValidoHasta = item.ValidoHasta;
                                        oDetalle.Observacion = item.Observacion;
                                        oDetalle.IdEstado = item.IdEstado;
                                        Modelo.SJ_RHPensionRefrigerioPersonaDetalle.InsertOnSubmit(oDetalle);
                                        Modelo.SubmitChanges();
                                        #endregion
                                    }
                                }
                                #endregion
                            }

                            Modelo.SJ_RHPensionRefrigerioPersonaDetalleActualizarFechaActivacion();

                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }


                        #endregion
                    }
                    else
                    {
                        #region Edicion
                        if (Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == Objeto.Id).ToList().Count() == 1)
                        {
                            #region Actualizar

                            Personal = new SJ_RHPensionRefrigerioPersona();
                            //Personal.Id = Objeto.Id;
                            Personal = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == Objeto.Id).Single();
                            Personal.IdCodigoPersonal = Objeto.IdCodigoPersonal;
                            Personal.NroDocumento = Objeto.NroDocumento;
                            Personal.NombresCompletos = Objeto.NombresCompletos;
                            Personal.IdSubPlanilla = Objeto.IdSubPlanilla;
                            Personal.SubPlanilla = Objeto.SubPlanilla;
                            Personal.Condicion = Objeto.Condicion;
                            Personal.IdPension = Objeto.IdPension;
                            Personal.NroDNIPension = Objeto.NroDNIPension;
                            Personal.Pension = Objeto.Pension;
                            Personal.Desayuno = Objeto.Desayuno;
                            Personal.Almuerzo = Objeto.Almuerzo;
                            Personal.Cena = Objeto.Cena;
                            Personal.Otro = Objeto.Otro;
                            //Personal.IdEstado = Objeto.IdEstado;

                            //Modelo.SJ_RHPensionRefrigerioPersona.InsertOnSubmit(Personal);
                            Modelo.SubmitChanges();
                            Codigo = Personal.Id;


                            if (detalle != null)
                            {
                                #region Detalle()
                                if (detalle.ToList().Count() > 0)
                                {
                                    #region
                                    foreach (SJ_RHPensionRefrigerioPersonaDetalle item in detalle)
                                    {
                                        #region

                                        if (Modelo.SJ_RHPensionRefrigerioPersonaDetalle.Where(x => x.Id == Objeto.Id && x.Item == item.Item).ToList().Count() == 1)
                                        {
                                            #region Actualizar
                                            try
                                            {
                                                SJ_RHPensionRefrigerioPersonaDetalle oDetalle = new SJ_RHPensionRefrigerioPersonaDetalle();
                                                //oDetalle.Id = Codigo;
                                                oDetalle = Modelo.SJ_RHPensionRefrigerioPersonaDetalle.Where(x => x.Id == Objeto.Id && x.Item == item.Item).Single();
                                                //oDetalle.Item = item.Item;
                                                oDetalle.ValidoDesde = item.ValidoDesde;
                                                oDetalle.ValidoHasta = item.ValidoHasta;
                                                oDetalle.Observacion = item.Observacion;
                                                oDetalle.IdEstado = item.IdEstado;
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
                                            #region Nuevo
                                            try
                                            {
                                                SJ_RHPensionRefrigerioPersonaDetalle oDetalle = new SJ_RHPensionRefrigerioPersonaDetalle();
                                                oDetalle.Id = Codigo;
                                                oDetalle.Item = item.Item;
                                                oDetalle.ValidoDesde = item.ValidoDesde;
                                                oDetalle.ValidoHasta = item.ValidoHasta;
                                                oDetalle.Observacion = item.Observacion;
                                                oDetalle.IdEstado = item.IdEstado;
                                                Modelo.SJ_RHPensionRefrigerioPersonaDetalle.InsertOnSubmit(oDetalle);
                                                Modelo.SubmitChanges();
                                            }
                                            catch (Exception Ex)
                                            {

                                                throw Ex;
                                            }


                                            #endregion
                                        }



                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }


                            #endregion
                        }

                        #endregion
                    }


                    Modelo.Connection.Close();
                    #endregion
                }

                #endregion
                Scope.Complete();
            }

            return Codigo;
        }

        public void AnularProgramacionAsistenciaRefrigerio(string Periodo, SJ_RHPensionRefrigerioPersona Objeto)
        {
            try
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    #region Transacción()
                    string cnx = string.Empty;

                    cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                    using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
                    {
                        Modelo.CommandTimeout = 9998000;
                        #region Actualizar Estado
                        if (Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == Objeto.Id).ToList().Count() == 1)
                        {
                            #region Actualizar

                            Personal = new SJ_RHPensionRefrigerioPersona();
                            Personal = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == Objeto.Id).Single();
                            if (Objeto.IdEstado == "AC")
                            {
                                Personal.IdEstado = "AN";
                            }
                            else
                            {
                                Personal.IdEstado = "AC";
                            }
                            //Personal.IdEstado = Objeto.IdEstado;                            
                            Modelo.SubmitChanges();
                            #endregion
                        }

                        #endregion
                        Modelo.Connection.Close();
                    }
                    Scope.Complete();
                    #endregion
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<PersonalCampo> ObtenerListaPosiblesAsistenciasTrabajadores(int desayuno, int almuerzo, int cena, string fecha, int tipoComida, string nroDNIPension)
        {
            List<PersonalCampo> listado = new List<PersonalCampo>();

            string cnx = string.Empty;

            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998999;

                if (nroDNIPension == "00000000")
                {
                    #region Buscar todo el personal Activo en la base de datos()

                    var listadoPersonalGeneral = Modelo.SJ_RHPensionRefrigerioBuscarPersona();
                    listado = (from item in listadoPersonalGeneral
                               group item by new { item.NRODocumento } into j
                               select new PersonalCampo
                               {
                                   nroDNI = j.Key.NRODocumento.ToString().Trim(),
                                   Nombres = j.FirstOrDefault().NombresTrabajador != null ? j.FirstOrDefault().NombresTrabajador.ToString().Trim() : "",
                               }).ToList();

                    #endregion


                }

                else if (nroDNIPension == "00000001")
                {
                    #region Buscar todo el personal que Asistio al menos un refrigerio en el año actual()
                    var listadoPersonalGeneral = Modelo.SJ_RHObtenerListaTrabajadoresAsistieronARefrigerios();
                    listado = (from item in listadoPersonalGeneral
                               group item by new { item.dniTrabajador } into j
                               select new PersonalCampo
                               {
                                   nroDNI = j.Key.dniTrabajador.ToString().Trim(),
                                   Nombres = j.FirstOrDefault().NombresTrabajador != null ? j.FirstOrDefault().NombresTrabajador.ToString().Trim() : "",
                               }).ToList();
                    #endregion
                }
                else
                {
                    #region Buscar los trabajadores que han sido asignados a la pensión segun su numero dNIPension
                    //var resultadoConsulta = Modelo.SJ_RHObtenerPosibleAsistenciaTrabajadores(desayuno, almuerzo, cena, fecha, tipoComida, nroDNIPension).ToList();
                    var resultadoConsulta = Modelo.SJ_RHObtenerPosibleAsistenciaTrabajadores(1, 1, 1, fecha, tipoComida, nroDNIPension).ToList();
                    listado = (from item in resultadoConsulta
                               group item by new { item.DNI } into j
                               select new PersonalCampo
                               {
                                   nroDNI = j.Key.DNI.ToString().Trim(),
                                   Nombres = j.FirstOrDefault().NombresCompletos.ToString().Trim() != null ? j.FirstOrDefault().NombresCompletos.ToString().Trim() : "",
                               }).ToList();
                    #endregion
                }

                Modelo.Connection.Close();
            }
            return listado;
        }

        /* Método para Actualizar el número de DNI del trabajado ("DESCONOCIDOS"), se actualiza con la distribucion de refriferios para el personal (PROGRAMACION DE ASISTENCIA REFRIGERIOS) */
        public void ActualizarNumeroDNITrabajor(SJM_Pensiones oTransferenciaAsistencia, PersonalCampo personalCampoDisponible)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
                {
                    Modelo.CommandTimeout = 9998998;
                    #region Actualizar Estado
                    try
                    {
                        #region Actualizar Número de DNI()
                        if (Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).ToList().Count == 1)
                        {
                            /* El estado 1 es importante por qye estaria dando actualizacion a un registro activo*/
                            if (Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).Single().estado == 1)
                            {
                                #region Anular()
                                tranferenciaPension = new SJM_Pensiones();
                                tranferenciaPension = Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).Single();
                                tranferenciaPension.DniTrabajador = personalCampoDisponible.nroDNI;
                                tranferenciaPension.NombresTrabajador = personalCampoDisponible.Nombres.ToString().Trim();
                                Modelo.SubmitChanges();
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
                    Modelo.Connection.Close();
                }
                Scope.Complete();
                #endregion
            }

        }

        /*Método para actualizar la fecha de la asistencia de refrigerio por nrodni del trabajador */
        public void ActualizarFechaAsistenciaRefrigerio(SJM_Pensiones oTransferenciaAsistencia, string fechaActualizar)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()
                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
                {
                    Modelo.CommandTimeout = 9998998;
                    #region Actualizar Estado
                    try
                    {
                        #region Actualizar Número de DNI()
                        if (Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).ToList().Count == 1)
                        {
                            /* El estado 1 es importante por qye estaria dando actualizacion a un registro activo*/
                            if (Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).Single().estado == 1)
                            {
                                #region Actualizar()
                                tranferenciaPension = new SJM_Pensiones();
                                tranferenciaPension = Modelo.SJM_Pensiones.Where(x => x.IdPension == oTransferenciaAsistencia.IdPension).Single();
                                tranferenciaPension.FechaPension = Convert.ToDateTime(fechaActualizar);
                                Modelo.SubmitChanges();
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
                    Modelo.Connection.Close();
                }
                Scope.Complete();
                #endregion
            }

        }

        /* Método para ver el historial por trabajo y por pension y por trabajador, para que se visualice en la grilla pivot */
        public List<SJ_RHConsultarHistorialAsistenciaRefrigerioxTrabajadorResult> ObtnerListaHistorialRefrigerioxTrabajador(string fecha, string dniPension, string dniTrabajador)
        {
            List<SJ_RHConsultarHistorialAsistenciaRefrigerioxTrabajadorResult> listado = new List<SJ_RHConsultarHistorialAsistenciaRefrigerioxTrabajadorResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + fecha.Substring(6, 4)].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998999;
                listado = Modelo.SJ_RHConsultarHistorialAsistenciaRefrigerioxTrabajador(fecha, "", dniTrabajador).ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }

        /* Método para ver el historial por trabajo y por pension, para que se visualice en la grilla pivot */
        public List<SJ_RHConsultarHistorialAsistenciaRefrigerioxPensionResult> ObtnerListaHistorialRefrigerioxPension(string fecha, string fechaHasta, string dniPension)
        {
            List<SJ_RHConsultarHistorialAsistenciaRefrigerioxPensionResult> listado = new List<SJ_RHConsultarHistorialAsistenciaRefrigerioxPensionResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + fecha.Substring(6, 4)].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998999;
                listado = Modelo.SJ_RHConsultarHistorialAsistenciaRefrigerioxPension(fecha, fechaHasta, dniPension).ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }

        /*Método para obtener que refrigerios recibio el trabajador durante el día de trabajo, siendo indiferente la si desayuno, almorzo o ceno en pensiones diferentes*/
        public List<SJ_RHObtenerRefrigeriosRecibidosxDniTrabadorResult> ObtnerListaRefrigerioRecibidosxTrabajadorxDia(string dniTrabajador, string desde, string hasta)
        {
            List<SJ_RHObtenerRefrigeriosRecibidosxDniTrabadorResult> listado = new List<SJ_RHObtenerRefrigeriosRecibidosxDniTrabadorResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + hasta.Substring(6, 4)].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998999;
                listado = Modelo.SJ_RHObtenerRefrigeriosRecibidosxDniTrabador(dniTrabajador, desde, hasta).ToList();
                Modelo.Connection.Close();
            }

            return listado;
        }


        #region Mantenimiento SJ_RHMovimientoAsistenciaPensiones Y SJ_RHMovimientoAsistenciaPensionDetalle()

        public string RegistrarMovimentoAsistenciaRefrigerios(SJ_RHMovimientoAsistenciaPension movimientoAsistenciaRefrigerio, List<SJ_RHMovimientoAsistenciaPensionDetalle> detallemovimientoAsistenciaRefrigerio, List<SJ_RHMovimientoAsistenciaPensionDetalle> detallemovimientoAsistenciaRefrigerioEliminados)
        {
            throw new NotImplementedException();
        }

        public string AnularMovimentoAsistenciaRefrigerios(SJ_RHMovimientoAsistenciaPension movimientoAsistenciaRefrigerio)
        {
            throw new NotImplementedException();
        }

        public string EliminarMovimentoAsistenciaRefrigerios(SJ_RHMovimientoAsistenciaPension movimientoAsistenciaRefrigerio)
        {
            throw new NotImplementedException();
        }

        public string ObtenerMovimentoAsistenciaRefrigerios(SJ_RHMovimientoAsistenciaPension movimientoAsistenciaRefrigerio)
        {
            throw new NotImplementedException();
        }


        #endregion



        public List<SJ_ReporteRefigeriosSubPlanilla> ListarAsistenciasPersonalPorSubPlanilla(int añoConsulta)
        {
            List<SJ_ReporteRefigeriosSubPlanilla> lista = new List<SJ_ReporteRefigeriosSubPlanilla>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + añoConsulta.ToString().Trim()].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                lista = Modelo.SJ_ListarReporteRefigeriosSubPlanilla(añoConsulta).OrderBy(x => x.nroMes).ToList();
            }
            return lista;
        }


        /*Listado de duplicidad de asistencia en mas de una pensión por refrigerio por día */

        public List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> ListarPersonasConDuplicidadEnRefrigeriosPorDia(string desde, string hasta)
        {
            List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> lista = new List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 980000;
                lista = Modelo.SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDia(desde, hasta).OrderBy(x => x.nombresTrabajador).ToList().OrderBy(x => x.fechapension).ToList();
                Modelo.Connection.Close();
                Modelo.Dispose();
            }
            return lista;
        }

        public List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> ListarPersonasConDuplicidadEnRefrigerioResumen(List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> listadoPersonasConDuplicidadEnRefrigeriosPorDia, List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ListaAsistenciasPersonalPendientesMovimientoAsistencia)
        {
            try
            {
                List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> listaResultado = new List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult>();

                if (listadoPersonasConDuplicidadEnRefrigeriosPorDia != null && listadoPersonasConDuplicidadEnRefrigeriosPorDia.ToList().Count > 0)
                {
                    foreach (var item in listadoPersonasConDuplicidadEnRefrigeriosPorDia)
                    {
                        string pension = string.Empty;
                        string subPlanilla = string.Empty;
                        SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult duplicado = new SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult();

                        var listadoPorAsistenciaDiaria = ListaAsistenciasPersonalPendientesMovimientoAsistencia.Where(x => x.DniTrabajador.ToString().Trim() == item.dniTrabajador.ToString().Trim() && x.FechaRefrigerio.Value == item.fechapension.Value && x.idRefrigerio == item.tipoComida).ToList();
                        if (listadoPorAsistenciaDiaria != null && listadoPorAsistenciaDiaria.Where(x => x.estado == 1).ToList().Count > 0)
                        {

                            foreach (var asistenciaDiaria in listadoPorAsistenciaDiaria)
                            {
                                pension += asistenciaDiaria.NombresPension.ToString().Trim() + " ,";
                            }
                            subPlanilla = listadoPorAsistenciaDiaria.Max(x => x.SubPlanilla).ToString().Trim();
                        }

                        duplicado.fechapension = item.fechapension;
                        duplicado.tipoComida = item.tipoComida;
                        duplicado.refrigerio = item.refrigerio;
                        duplicado.importeRefrigerio = item.importeRefrigerio;
                        duplicado.dniTrabajador = item.dniTrabajador;
                        duplicado.nombresTrabajador = item.nombresTrabajador;
                        duplicado.vecesPorRefrigerio = item.vecesPorRefrigerio;
                        duplicado.pension = pension;
                        duplicado.idPension = item.idPension;
                        duplicado.subplanilla = subPlanilla;
                        listaResultado.Add(duplicado);
                    }
                }
                return listaResultado;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }

        public List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> ListarPersonasConDuplicidadEnRefrigerioDetalle(List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> listadoPersonasConDuplicidadEnRefrigeriosPorDia, List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ListaAsistenciasPersonalPendientesMovimientoAsistencia)
        {


            try
            {
                List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> listaResultado = new List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult>();
                return listaResultado;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


        }
    }
}





