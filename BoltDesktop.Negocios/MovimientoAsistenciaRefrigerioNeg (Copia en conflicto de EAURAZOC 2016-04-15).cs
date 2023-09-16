using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensionistasRefrigerios.Datos;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding;
using MyControlsDataBinding.Busquedas;
using System.Globalization;

namespace PensionistasRefrigerios.Negocios
{
    public class MovimientoAsistenciaRefrigerioNeg
    {
        private SJ_RHPensionRefrigerioPersona Personal;
        private string nuevoCodigo;
        private SJM_Pensione tranferenciaPension;
        private List<SJ_RHPensionRefrigerioBuscarPersonaResult> listadoTodoPersonalActivoBD = new List<SJ_RHPensionRefrigerioBuscarPersonaResult>();
        private decimal? importeDistribuir;
        private decimal? totalMinutosTrabajador;
        private decimal? totalRacimosTrabajador;
        private string pensiones;
        private string idPension;
        private string subplanilla;

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

        public string RegistrarAsistenciaRefrigerios(List<SJM_Pensione> Listado, List<SJM_Pensione> ListadoEliminados)
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
                        foreach (SJM_Pensione item in Listado)
                        {
                            try
                            {
                                if (Modelo.SJM_Pensiones.Where(x => x.IdPension == item.IdPension).ToList().Count == 1)
                                {
                                    SJM_Pensione Obj = new SJM_Pensione();
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
                foreach (SJM_Pensione item in Listado)
                {
                    if (item.IdPension == null || item.IdPension.ToString().Trim() == "")
                    {
                        #region Nuevo()
                        try
                        {
                            if (item.IdPension == 0)
                            {
                                #region Nuevo Registro()
                                SJM_Pensione Obj = new SJM_Pensione();
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
                                    SJM_Pensione Obj = new SJM_Pensione();
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
                                SJM_Pensione Obj = new SJM_Pensione();
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

        // LISTAR TODO EL PERSONAL de la programacion del personal con la relacion de muchos a muchos
        public List<SJ_RHPensionRefrigerioPersonaListarResult> ObtenerListadoRefrigerioByTrabajadores(string periodo)
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
                Modelo.Connection.Close();
                Modelo.Dispose();
            }

            return Lista;
        }

        // LISTAR TODO EL PERSONAL de la programacion del personal agrupado por trabajador
        public List<SJ_RHPensionRefrigerioPersonaListarResult> ObtenerListadoRefrigerioByTrabajador(List<SJ_RHPensionRefrigerioPersonaListarResult> listadoRefrigerioTrabajadorByPension)
        {
            List<SJ_RHPensionRefrigerioPersonaListarResult> Lista = new List<SJ_RHPensionRefrigerioPersonaListarResult>();
            try
            {
                if (listadoRefrigerioTrabajadorByPension != null && listadoRefrigerioTrabajadorByPension.ToList().Count > 0)
                {
                    var trabajadores = (from item in listadoRefrigerioTrabajadorByPension
                                        where item.Id != null && item.IdCodigoPersonal != null
                                        group item by new { item.IdCodigoPersonal } into j
                                        select new
                                        {
                                            idCodigoGeneral = j.Key.IdCodigoPersonal.ToString().Trim(),
                                            nroDocumento = j.FirstOrDefault().NroDocumento != null ? j.FirstOrDefault().NroDocumento.ToString().Trim() : "",
                                            nombresCompletos = j.FirstOrDefault().NombresCompletos != null ? j.FirstOrDefault().NombresCompletos.ToString().Trim() : "",
                                            idSubPlanilla = j.FirstOrDefault().IdSubPlanilla != null ? j.FirstOrDefault().IdSubPlanilla : "",
                                        }).ToList();

                    if (trabajadores != null && trabajadores.ToList().Count > 0)
                    {
                        foreach (var trabajador in trabajadores)
                        {
                            var listadoPersonalByNroDNI = listadoRefrigerioTrabajadorByPension.Where(x => x.IdCodigoPersonal.ToString().Trim() == trabajador.idCodigoGeneral.ToString().Trim()).ToList();

                            if (listadoPersonalByNroDNI != null && listadoPersonalByNroDNI.ToList().Count == 1)
                            {
                                SJ_RHPensionRefrigerioPersonaListarResult oRegistroPersonal = new SJ_RHPensionRefrigerioPersonaListarResult();
                                oRegistroPersonal = listadoPersonalByNroDNI.Single();
                                Lista.Add(oRegistroPersonal);
                            }
                            else if (listadoPersonalByNroDNI.ToList().Count > 1)
                            {
                                SJ_RHPensionRefrigerioPersonaListarResult oRegistroPersonal = new SJ_RHPensionRefrigerioPersonaListarResult();
                                oRegistroPersonal.Id = 0;
                                oRegistroPersonal.IdCodigoPersonal = trabajador.idCodigoGeneral;
                                oRegistroPersonal.NroDocumento = trabajador.nroDocumento;
                                oRegistroPersonal.NombresCompletos = trabajador.nombresCompletos;

                                var subPlanillasByPersona = (from item in listadoPersonalByNroDNI
                                                             where item.Id != null && item.IdSubPlanilla != null
                                                             group item by new { item.IdSubPlanilla } into j
                                                             select new
                                                             {
                                                                 idPlanilla = j.Key.IdSubPlanilla.ToString().Trim(),
                                                                 subPlanilla = j.FirstOrDefault().SubPlanilla != null ? j.FirstOrDefault().SubPlanilla.ToString().Trim() : "",
                                                             }).ToList();

                                if (subPlanillasByPersona != null && subPlanillasByPersona.ToList().Count == 1)
                                {
                                    oRegistroPersonal.IdSubPlanilla = subPlanillasByPersona.FirstOrDefault().idPlanilla.ToString().Trim();
                                    oRegistroPersonal.SubPlanilla = subPlanillasByPersona.FirstOrDefault().subPlanilla.ToString().Trim();
                                }
                                else if (subPlanillasByPersona != null && subPlanillasByPersona.ToList().Count > 1)
                                {
                                    oRegistroPersonal.IdSubPlanilla = "0";
                                    string subPlanillasTrabajador = string.Empty;
                                    foreach (var item in subPlanillasByPersona)
                                    {
                                        subPlanillasTrabajador += item.subPlanilla + " - ";
                                    }
                                    oRegistroPersonal.SubPlanilla = subPlanillasTrabajador;
                                }

                                var pensionByPersona = (from item in listadoPersonalByNroDNI
                                                        where item.Id != null && item.IdPension != null
                                                        group item by new { item.IdPension } into j
                                                        select new
                                                        {
                                                            idPension = j.Key.IdPension,
                                                            nroDniPension = j.FirstOrDefault().NroDNIPension != null ? j.FirstOrDefault().NroDNIPension.ToString().Trim() : "",
                                                            pension = j.FirstOrDefault().Pension != null ? j.FirstOrDefault().Pension.ToString().Trim() : "",
                                                        }).ToList();



                                if (pensionByPersona != null && pensionByPersona.ToList().Count == 1)
                                {
                                    oRegistroPersonal.IdPension = pensionByPersona.FirstOrDefault().idPension;
                                    oRegistroPersonal.NroDNIPension = pensionByPersona.FirstOrDefault().nroDniPension.ToString().Trim();
                                    oRegistroPersonal.Pension = pensionByPersona.FirstOrDefault().pension.ToString().Trim();
                                }
                                else if (pensionByPersona != null && pensionByPersona.ToList().Count > 1)
                                {
                                    oRegistroPersonal.IdPension = 0;
                                    string dniPensionByTrabajador = string.Empty;
                                    string pensionByTrabajador = string.Empty;
                                    foreach (var item in pensionByPersona)
                                    {
                                        dniPensionByTrabajador += item.nroDniPension + " - ";
                                        pensionByTrabajador += item.pension + " - ";
                                    }
                                    oRegistroPersonal.NroDNIPension = dniPensionByTrabajador;
                                    oRegistroPersonal.Pension = pensionByTrabajador;
                                }

                                var CondicionByPersona = (from item in listadoPersonalByNroDNI
                                                          where item.Id != null && item.Condicion != null
                                                          group item by new { item.Condicion } into j
                                                          select new
                                                          {
                                                              condicion = j.Key.Condicion,
                                                          }).ToList();

                                if (CondicionByPersona != null && CondicionByPersona.ToList().Count == 1)
                                {
                                    oRegistroPersonal.Condicion = CondicionByPersona.FirstOrDefault().condicion.ToString().Trim();
                                }
                                else if (CondicionByPersona != null && CondicionByPersona.ToList().Count > 1)
                                {
                                    oRegistroPersonal.Condicion = "ACTIVO";
                                }

                                oRegistroPersonal.Desayuno = listadoPersonalByNroDNI.Max(x => x.Desayuno);
                                oRegistroPersonal.Almuerzo = listadoPersonalByNroDNI.Max(x => x.Almuerzo);
                                oRegistroPersonal.Cena = listadoPersonalByNroDNI.Max(x => x.Cena);
                                oRegistroPersonal.Otro = listadoPersonalByNroDNI.Max(x => x.Otro);
                                oRegistroPersonal.ValidoDesde = listadoPersonalByNroDNI.Max(x => x.ValidoDesde);
                                oRegistroPersonal.ValidoHasta = listadoPersonalByNroDNI.Max(x => x.ValidoHasta);
                                oRegistroPersonal.IdEstado = listadoPersonalByNroDNI.Max(x => x.IdEstado);
                                oRegistroPersonal.Estado = (listadoPersonalByNroDNI.Max(x => x.IdEstado) == "AC" ? "ACTIVO" : "ANULADO");
                                Lista.Add(oRegistroPersonal);
                            }
                        }
                    }

                }
            }
            catch (Exception Ex)
            {

                throw Ex;
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

                    /* mejora en este reporte es asociar a cuantas planillas esta asociado este trabajador, mejor dicho agrupar por codigo de transferencia */
                    var listadoResultadoConsulta = Modelo.SJ_RHAsistenciasRefrigeriosPendientesMovimiento(fechaDesde, fechaHasta, (dniPension != null ? dniPension : "")).ToList();

                    /* OBTENGO TODAS LAS TRANSFERENCIAS VALIDAS PARA EL PROCESO */
                    var listadoCodigoTransferenciaAsistencia = (from item in listadoResultadoConsulta
                                                                where item.idMovimientoTransferencia != null
                                                                group item by new { item.idMovimientoTransferencia } into j
                                                                select new
                                                                {
                                                                    codigoTransfereciaAsistencia = j.Key.idMovimientoTransferencia,
                                                                }).ToList();


                    if (listadoCodigoTransferenciaAsistencia != null && listadoCodigoTransferenciaAsistencia.ToList().Count > 0)
                    {
                        foreach (var itemTransferido in listadoCodigoTransferenciaAsistencia)
                        {
                            var resultadoByItem = listadoResultadoConsulta.Where(x => x.idMovimientoTransferencia == itemTransferido.codigoTransfereciaAsistencia).ToList();
                            if (resultadoByItem != null && resultadoByItem.ToList().Count > 0)
                            {
                                SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult registroTransferencia = new SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult();
                                registroTransferencia.idMovimientoTransferencia = itemTransferido.codigoTransfereciaAsistencia;
                                //registroTransferencia.FechaRefrigerio = resultadoByItem.FirstOrDefault().FechaRefrigerio != null ? resultadoByItem.FirstOrDefault().FechaRefrigerio.Value : (DateTime?)null;
                                registroTransferencia.idCodigoGeneral = resultadoByItem.FirstOrDefault().idCodigoGeneral != null ? resultadoByItem.FirstOrDefault().idCodigoGeneral.ToString().Trim() : "";
                                registroTransferencia.NombresTrabajador = resultadoByItem.FirstOrDefault().NombresTrabajador != null ? resultadoByItem.FirstOrDefault().NombresTrabajador.ToString().Trim() : "";
                                registroTransferencia.semanaAsistencia = resultadoByItem.FirstOrDefault().semanaAsistencia != null ? resultadoByItem.FirstOrDefault().semanaAsistencia : 0;
                                registroTransferencia.FechaRefrigerio = resultadoByItem.FirstOrDefault().FechaRefrigerio != null ? resultadoByItem.FirstOrDefault().FechaRefrigerio.Value : (DateTime?)null;
                                registroTransferencia.DniTrabajador = resultadoByItem.FirstOrDefault().DniTrabajador != null ? resultadoByItem.FirstOrDefault().DniTrabajador.ToString().Trim() : "";
                                registroTransferencia.NombresTrabajadorSistema = resultadoByItem.FirstOrDefault().NombresTrabajadorSistema != null ? resultadoByItem.FirstOrDefault().NombresTrabajadorSistema.ToString().Trim() : "";
                                registroTransferencia.idRefrigerio = resultadoByItem.FirstOrDefault().idRefrigerio != null ? resultadoByItem.FirstOrDefault().idRefrigerio : 0;
                                registroTransferencia.Refrigerio = resultadoByItem.FirstOrDefault().Refrigerio != null ? resultadoByItem.FirstOrDefault().Refrigerio.ToString().Trim() : "";
                                registroTransferencia.DniPension = resultadoByItem.FirstOrDefault().DniPension != null ? resultadoByItem.FirstOrDefault().DniPension.ToString().Trim() : "";
                                registroTransferencia.NombresPension = resultadoByItem.FirstOrDefault().NombresPension != null ? resultadoByItem.FirstOrDefault().NombresPension.ToString().Trim() : "";
                                registroTransferencia.FechaRegistro = resultadoByItem.FirstOrDefault().FechaRegistro != null ? resultadoByItem.FirstOrDefault().FechaRegistro.Value : (DateTime?)null;
                                registroTransferencia.EsProcesado = resultadoByItem.FirstOrDefault().EsProcesado != null ? resultadoByItem.FirstOrDefault().EsProcesado : 0;
                                registroTransferencia.idMovimientoAsistencia = resultadoByItem.FirstOrDefault().idMovimientoAsistencia != null ? resultadoByItem.FirstOrDefault().idMovimientoAsistencia.ToString().Trim() : "";
                                registroTransferencia.idPension = resultadoByItem.FirstOrDefault().idPension != null ? resultadoByItem.FirstOrDefault().idPension : 0;
                                registroTransferencia.estado = resultadoByItem.FirstOrDefault().estado != null ? resultadoByItem.FirstOrDefault().estado : 0;
                                registroTransferencia.SubPlanilla = resultadoByItem.FirstOrDefault().SubPlanilla != null ? resultadoByItem.FirstOrDefault().SubPlanilla.ToString().Trim() : "";
                                registroTransferencia.fechaTransferencia = resultadoByItem.FirstOrDefault().fechaTransferencia != null ? resultadoByItem.FirstOrDefault().fechaTransferencia : (DateTime?)null;
                                registroTransferencia.TipoEstadoPersonal = resultadoByItem.FirstOrDefault().TipoEstadoPersonal != null ? resultadoByItem.FirstOrDefault().TipoEstadoPersonal : "";
                                registroTransferencia.IDPLANILLA = resultadoByItem.FirstOrDefault().IDPLANILLA != null ? resultadoByItem.FirstOrDefault().IDPLANILLA.ToString().Trim() : "";
                                registroTransferencia.idparadero = resultadoByItem.FirstOrDefault().idparadero != null ? resultadoByItem.FirstOrDefault().idparadero.ToString().Trim() : "";
                                registroTransferencia.paradero = resultadoByItem.FirstOrDefault().paradero != null ? resultadoByItem.FirstOrDefault().paradero.ToString().Trim() : "";
                                Listado.Add(registroTransferencia);
                            }
                        }
                    }
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

        /* Lista de asistencias a las personas que reciben refigerios y que se encuentran pendientes de movimiento de asistencia diario */
        public List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ObtenerListaAsistenciasPersonalDesconocidoPendientesMovimientoAsistencia(string periodo, string fechaDesde, string fechaHasta, string dniPension)
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

                    /* mejora en este reporte es asociar a cuantas planillas esta asociado este trabajador, mejor dicho agrupar por codigo de transferencia */

                    var listadoResultadoConsulta = Modelo.SJ_RHAsistenciasRefrigeriosPendientesMovimientoDesconocidos(fechaDesde, fechaHasta, (dniPension != null ? dniPension : "")).ToList();


                    var listadoCodigoTransferenciaAsistencia = (from item in listadoResultadoConsulta
                                                                where item.idMovimientoTransferencia != null
                                                                group item by new { item.idMovimientoTransferencia } into j
                                                                select new
                                                                {
                                                                    codigoTransfereciaAsistencia = j.Key.idMovimientoTransferencia,
                                                                }).ToList();


                    if (listadoCodigoTransferenciaAsistencia != null && listadoCodigoTransferenciaAsistencia.ToList().Count > 0)
                    {
                        foreach (var itemTransferido in listadoCodigoTransferenciaAsistencia)
                        {

                            var resultadoByItem = listadoResultadoConsulta.Where(x => x.idMovimientoTransferencia == itemTransferido.codigoTransfereciaAsistencia).ToList();

                            if (resultadoByItem != null && resultadoByItem.ToList().Count > 0)
                            {
                                SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult registroTransferencia = new SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult();
                                registroTransferencia.idMovimientoTransferencia = itemTransferido.codigoTransfereciaAsistencia;
                                registroTransferencia.FechaRegistro = resultadoByItem.FirstOrDefault().FechaRegistro != null ? resultadoByItem.FirstOrDefault().FechaRegistro.Value : (DateTime?)null;
                                registroTransferencia.idCodigoGeneral = resultadoByItem.FirstOrDefault().idCodigoGeneral != null ? resultadoByItem.FirstOrDefault().idCodigoGeneral.ToString().Trim() : "";
                                registroTransferencia.NombresTrabajador = resultadoByItem.FirstOrDefault().NombresTrabajador != null ? resultadoByItem.FirstOrDefault().NombresTrabajador.ToString().Trim() : "";
                                registroTransferencia.semanaAsistencia = resultadoByItem.FirstOrDefault().semanaAsistencia != null ? resultadoByItem.FirstOrDefault().semanaAsistencia : 0;

                                registroTransferencia.FechaRefrigerio = resultadoByItem.FirstOrDefault().FechaRefrigerio != null ? resultadoByItem.FirstOrDefault().FechaRefrigerio.Value : (DateTime?)null;

                                registroTransferencia.DniTrabajador = resultadoByItem.FirstOrDefault().DniTrabajador != null ? resultadoByItem.FirstOrDefault().DniTrabajador.ToString().Trim() : "";
                                registroTransferencia.NombresTrabajadorSistema = resultadoByItem.FirstOrDefault().NombresTrabajadorSistema != null ? resultadoByItem.FirstOrDefault().NombresTrabajadorSistema.ToString().Trim() : "";
                                registroTransferencia.idRefrigerio = resultadoByItem.FirstOrDefault().idRefrigerio != null ? resultadoByItem.FirstOrDefault().idRefrigerio : 0;
                                registroTransferencia.Refrigerio = resultadoByItem.FirstOrDefault().Refrigerio != null ? resultadoByItem.FirstOrDefault().Refrigerio.ToString().Trim() : "";
                                registroTransferencia.DniPension = resultadoByItem.FirstOrDefault().DniPension != null ? resultadoByItem.FirstOrDefault().DniPension.ToString().Trim() : "";
                                registroTransferencia.NombresPension = resultadoByItem.FirstOrDefault().NombresPension != null ? resultadoByItem.FirstOrDefault().NombresPension.ToString().Trim() : "";

                                registroTransferencia.EsProcesado = resultadoByItem.FirstOrDefault().EsProcesado != null ? resultadoByItem.FirstOrDefault().EsProcesado : 0;
                                registroTransferencia.idMovimientoAsistencia = resultadoByItem.FirstOrDefault().idMovimientoAsistencia != null ? resultadoByItem.FirstOrDefault().idMovimientoAsistencia.ToString().Trim() : "";
                                registroTransferencia.idPension = resultadoByItem.FirstOrDefault().idPension != null ? resultadoByItem.FirstOrDefault().idPension : 0;
                                registroTransferencia.estado = resultadoByItem.FirstOrDefault().estado != null ? resultadoByItem.FirstOrDefault().estado : 0;
                                registroTransferencia.SubPlanilla = resultadoByItem.FirstOrDefault().SubPlanilla != null ? resultadoByItem.FirstOrDefault().SubPlanilla.ToString().Trim() : "";
                                registroTransferencia.fechaTransferencia = resultadoByItem.FirstOrDefault().fechaTransferencia != null ? resultadoByItem.FirstOrDefault().fechaTransferencia : (DateTime?)null;
                                registroTransferencia.TipoEstadoPersonal = resultadoByItem.FirstOrDefault().TipoEstadoPersonal != null ? resultadoByItem.FirstOrDefault().TipoEstadoPersonal : "";

                                registroTransferencia.IDPLANILLA = resultadoByItem.FirstOrDefault().IDPLANILLA != null ? resultadoByItem.FirstOrDefault().IDPLANILLA.ToString().Trim() : "";

                                Listado.Add(registroTransferencia);
                            }


                        }



                    }



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
                        tranferenciaPension = new SJM_Pensione();
                        tranferenciaPension = Modelo.SJM_Pensiones.Where(x => x.IdPension == codigo).Single();
                        tranferenciaPension.estado = 0;
                        Modelo.SubmitChanges();
                        #endregion
                    }
                    else
                    {
                        #region Activar()
                        tranferenciaPension = new SJM_Pensione();
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
            //using (TransactionScope Scope = new TransactionScope())
            //{
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
                                    oDetalle.ValidoDesde = item.ValidoDesde != null ? item.ValidoDesde.Value : (DateTime?)null;
                                    oDetalle.ValidoHasta = item.ValidoHasta != null ? item.ValidoHasta.Value : (DateTime?)null;
                                    oDetalle.Observacion = item.Observacion;
                                    oDetalle.IdEstado = item.IdEstado;
                                    Modelo.SJ_RHPensionRefrigerioPersonaDetalle.InsertOnSubmit(oDetalle);
                                    Modelo.SubmitChanges();
                                    #endregion
                                }
                            }
                            #endregion
                        }
                        #region Registrar Log Historial
                        SJ_LogTablasPension logTablas = new SJ_LogTablasPension();
                        logTablas.IDEMPRESA = "001";
                        logTablas.IDLOG = Personal.Id.ToString().PadLeft(16, '0');
                        logTablas.ITEM = "001";
                        logTablas.TABLA = "SJ_RHPensionRefrigerioPersona";
                        logTablas.IDCAMPO = "";
                        logTablas.CAMPOCLAVE = "";
                        logTablas.IDTABLA = "Id";
                        logTablas.EVENTO = "NUEVO";
                        logTablas.VALORANTERIOR = Personal.NroDocumento + Objeto.Pension;
                        logTablas.VALORACTUAL = Personal.SubPlanilla;
                        logTablas.IDUSUARIO = Environment.UserName;
                        logTablas.MAQUINA = Environment.MachineName;
                        logTablas.FECHACREACION = DateTime.Now;
                        logTablas.VENTANA = "ProgramacionRefrigerioxPersonalMantenimiento";
                        Modelo.SJ_LogTablasPension.InsertOnSubmit(logTablas);
                        Modelo.SubmitChanges();
                        #endregion
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
                                            oDetalle.ValidoDesde = item.ValidoDesde != null ? item.ValidoDesde.Value : (DateTime?)null;
                                            oDetalle.ValidoHasta = item.ValidoHasta != null ? item.ValidoHasta.Value : (DateTime?)null;
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
                                            oDetalle.ValidoDesde = item.ValidoDesde != null ? item.ValidoDesde.Value : (DateTime?)null;
                                            oDetalle.ValidoHasta = item.ValidoHasta != null ? item.ValidoHasta.Value : (DateTime?)null;
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


                        #region Registrar Log Historial
                        SJ_LogTablasPension logTablas = new SJ_LogTablasPension();
                        logTablas.IDEMPRESA = "001";
                        logTablas.IDLOG = Personal.Id.ToString().PadLeft(16, '0');
                        //logTablas.ITEM = "001";
                        logTablas.ITEM = ObtenerNumeroItemLogTablas(logTablas.IDLOG, "SJ_RHPensionRefrigerioPersona").ToString();
                        logTablas.TABLA = "SJ_RHPensionRefrigerioPersona";
                        logTablas.IDCAMPO = "";
                        logTablas.CAMPOCLAVE = "";
                        logTablas.IDTABLA = "Id";
                        logTablas.EVENTO = "MODIFICADO";
                        logTablas.VALORANTERIOR = Personal.NroDocumento + Objeto.Pension;
                        logTablas.VALORACTUAL = Personal.SubPlanilla;
                        logTablas.IDUSUARIO = Environment.UserName;
                        logTablas.MAQUINA = Environment.MachineName;
                        logTablas.FECHACREACION = DateTime.Now;
                        logTablas.VENTANA = "ProgramacionRefrigerioxPersonalMantenimiento";
                        Modelo.SJ_LogTablasPension.InsertOnSubmit(logTablas);
                        Modelo.SubmitChanges();
                        #endregion


                        #endregion
                    }

                    #endregion
                }

                Modelo.Connection.Close();
                #endregion
            }

            #endregion
            //    Scope.Complete();
            //}
            return Codigo;
        }

        private string ObtenerNumeroItemLogTablas(string idCodigo, string tabla)
        {
            string cnx = string.Empty;
            string item = "001";
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                #region Registro / Edicion()
                Modelo.CommandTimeout = 9998000;

                var resultadoConsulta = Modelo.SJ_LogTablasPension.Where(x => x.IDLOG.ToString().Trim() == idCodigo && x.TABLA.ToString().Trim().Trim() == tabla).ToList();

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count > 0)
                {
                    int numero = Convert.ToInt32(resultadoConsulta.OrderByDescending(x => x.ITEM).FirstOrDefault().ITEM) + 1;
                    item = numero.ToString().Trim().PadLeft(3, '0');
                }

                Modelo.Connection.Close();
                Modelo.Dispose();
                #endregion
            }

            return item;
        }

        /* Anular un la programación de un trabajador */
        public void AnularProgramacionAsistenciaRefrigerioByTrabajador(string Periodo, SJ_RHPensionRefrigerioPersona Objeto, int tipoAnulado)
        {
            /* tipoAnulado 
             * 0 = Anular todos()
             * 1 = Activar todos()
             * 2 = activar uno desactivar el otro()
             */
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
                        else if (Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.IdCodigoPersonal == Objeto.IdCodigoPersonal && x.Id == 0).ToList().Count() > 0)
                        {
                            var resultado = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.IdCodigoPersonal == Objeto.IdCodigoPersonal).ToList();

                            foreach (var itemAnular in resultado)
                            {
                                if (Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == itemAnular.Id).ToList().Count() == 1)
                                {
                                    #region Cambiar estado()

                                    Personal = new SJ_RHPensionRefrigerioPersona();
                                    Personal = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == itemAnular.Id).Single();
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
                            }

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
        public void ActualizarNumeroDNITrabajor(SJM_Pensione oTransferenciaAsistencia, PersonalCampo personalCampoDisponible)
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
                                tranferenciaPension = new SJM_Pensione();
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
        public void ActualizarFechaAsistenciaRefrigerio(SJM_Pensione oTransferenciaAsistencia, string fechaActualizar)
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
                                tranferenciaPension = new SJM_Pensione();
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

        public List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> ListarPersonasConDuplicidadEnRefrigeriosPorDia(string desde, string hasta)
        {
            List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> lista = new List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                lista = Modelo.SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDia(desde, hasta).ToList();
            }

            return lista;
        }

        /* Método para obtener el Listado de dias Excluidos que no formarán como Descuento entre periodos 
         * Generado  : Erick Aurazo
         * Creacion  : 21.03.16  */
        public List<SJ_RHPensionFacturacionPensionDiasExcluido> ListarDiasExcluidosaDescuentoByPeriodo(string desde, string hasta)
        {
            List<SJ_RHPensionFacturacionPensionDiasExcluido> listado = new List<SJ_RHPensionFacturacionPensionDiasExcluido>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();

            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                listado = Modelo.SJ_RHPensionFacturacionPensionDiasExcluidos.Where(x => x.fecha >= Convert.ToDateTime(desde) && x.fecha <= Convert.ToDateTime(hasta + " 23:59:00")).ToList();
            }

            return listado;
        }

        public List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> ListarPersonasConDuplicidadEnRefrigerioResumen(List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> listado, List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ListaAsistenciasPersonalPendientesMovimientoAsistencia)
        {
            List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult> lista = new List<SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999899;
                if (listado != null && listado.ToList().Count > 0)
                {
                    #region

                    foreach (var itemDuplicado in listado)
                    {

                        var ObtenerPensionesPorDuplicidadEnRefrigerio = ListaAsistenciasPersonalPendientesMovimientoAsistencia.Where(x => x.FechaRefrigerio.Value == itemDuplicado.fechapension.Value && x.idRefrigerio.Value == itemDuplicado.tipoComida.Value && x.DniTrabajador.ToString().Trim() == itemDuplicado.dniTrabajador.ToString().Trim()).ToList();
                        pensiones = string.Empty;
                        idPension = string.Empty;
                        subplanilla = string.Empty;

                        if (ObtenerPensionesPorDuplicidadEnRefrigerio != null && ObtenerPensionesPorDuplicidadEnRefrigerio.ToList().Count > 0)
                        {
                            pensiones = string.Empty;
                            idPension = string.Empty;
                            subplanilla = string.Empty;
                            foreach (var itemDuplicadoListadoPendiente in ObtenerPensionesPorDuplicidadEnRefrigerio)
                            {
                                pensiones += itemDuplicadoListadoPendiente.NombresPension.ToString().Trim() + "; ";
                                idPension += itemDuplicadoListadoPendiente.idPension.ToString().Trim() + "; ";
                                subplanilla = itemDuplicadoListadoPendiente.SubPlanilla.ToString().Trim();
                            }
                        }

                        SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult item = new SJ_ListarPersonasConDuplicidadEnRefrigeriosPorDiaResult();
                        item.fechapension = itemDuplicado.fechapension;
                        item.tipoComida = itemDuplicado.tipoComida;
                        item.refrigerio = itemDuplicado.refrigerio;
                        item.importeRefrigerio = (itemDuplicado.vecesPorRefrigerio - 1) * (itemDuplicado.importeRefrigerio);
                        item.dniTrabajador = itemDuplicado.dniTrabajador;
                        item.nombresTrabajador = itemDuplicado.nombresTrabajador;
                        item.vecesPorRefrigerio = itemDuplicado.vecesPorRefrigerio;
                        item.pension = pensiones;
                        item.idPension = idPension;
                        item.subplanilla = subplanilla;
                        lista.Add(item);
                    }

                    #endregion
                }
                Modelo.Connection.Close();
                Modelo.Dispose();
            }
            return lista;
        }

        /*  PROCEDIMIENTO ALMACENADO PARA SINCRONIZAR LAS ASISTENCIAS DE CAMPOS A UNA TABLA TEMPORAR PARA VALIDAR LOS DESCUENTO PARA EL PROCESO DE DESCUENTO POR ASISTENCIAS A LOS REFRIGERIOS POR PARTE DE LOS TRABAJADORES       */
        public void SincronizarAsistenciasCampoParaDescuentoPorInasistenciasRefrigerios(string desde, string hasta)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999899;
                Modelo.SJ_RRHHCopiarAsistenciaPersonalGeneralParaSistemaMovilesPensiones(desde, hasta);
                Modelo.Connection.Close();
                Modelo.Dispose();
            }

        }

        /* Lista generada para los descuento */
        public List<SJM_Pensione> GenerarListaPersonalParaDescuento(List<SJ_RHAsistenciasRefrigeriosPendientesMovimientoResult> ListaAsistenciasPersonalPendientesMovimientoAsistencia, List<SJM_RefrigerioPensionesPersonalListaAsistenciaResult> ListaAsistencias, List<SJ_RHPensionFacturacionPensionDiasExcluido> olistadoDiasExcluidosaDescuentoByPeriodo)
        {
            List<SJM_Pensione> listaResultado = new List<SJM_Pensione>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;
                #region
                if (ListaAsistenciasPersonalPendientesMovimientoAsistencia != null && ListaAsistenciasPersonalPendientesMovimientoAsistencia.ToList().Count > 0)
                {
                    foreach (var item in ListaAsistenciasPersonalPendientesMovimientoAsistencia.Where(x => x.estado != 0).ToList())
                    {
                        #region
                        var resultadoConsultaByItem = ListaAsistenciasPersonalPendientesMovimientoAsistencia.Where(x => x.idMovimientoTransferencia == item.idMovimientoTransferencia).ToList();

                        if (resultadoConsultaByItem != null && resultadoConsultaByItem.ToList().Count == 1)
                        {
                            SJM_Pensione registroAsistencia = new SJM_Pensione();
                            registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                            registroAsistencia.estado = 1;
                            registroAsistencia.fechaBaja = (DateTime?)null;
                            modelo.SubmitChanges();
                            //listaResultado.Add(registroAsistencia);
                        }

                        #region
                        /*busco coincidecnia de las asistencias de los refrigerios dentro de las asistencias en campo*/
                        /* el filtro es por dnitrabajador y por fecha */
                        var listaResultadoCoincidencia = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && Convert.ToDateTime(x.Fecha) == item.FechaRefrigerio.Value).ToList();

                        /* De no haber asistencia o coincidencia de registros */
                        if (listaResultadoCoincidencia != null && listaResultadoCoincidencia.ToList().Count > 0)
                        {

                        }
                        else
                        {
                            #region
                            CultureInfo ci = new CultureInfo("Es-PE");
                            string numeroSemanaAsistenciaCampo = Convert.ToDateTime(item.FechaRefrigerio).DayOfWeek.ToString();
                            if (numeroSemanaAsistenciaCampo.ToUpper().Trim() == "SUNDAY")
                            {

                            }
                            else if (numeroSemanaAsistenciaCampo.ToUpper().Trim() == "SATURDAY")
                            {
                                #region
                                var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                                if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 4)
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 1;
                                    registroAsistencia.fechaActualizacion = DateTime.Now;
                                    modelo.SubmitChanges();
                                }
                                else
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 2;
                                    registroAsistencia.fechaBaja = DateTime.Now;
                                    modelo.SubmitChanges();
                                    listaResultado.Add(registroAsistencia);
                                }
                                #endregion
                            }
                            else if (item.FechaRefrigerio == Convert.ToDateTime("08/12/2015"))
                            {
                                #region
                                var resultadoPersonalQueLaboroMas4diasSemana2 = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim()).ToList();
                                var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                                if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 4)
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 1;
                                    registroAsistencia.fechaActualizacion = DateTime.Now;
                                    modelo.SubmitChanges();
                                }
                                else
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 2;
                                    registroAsistencia.fechaBaja = DateTime.Now;
                                    modelo.SubmitChanges();
                                    listaResultado.Add(registroAsistencia);
                                }
                                #endregion
                            }

                            else if (item.FechaRefrigerio == Convert.ToDateTime("24/12/2015"))
                            {
                                #region
                                var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                                if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 4)
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 1;
                                    registroAsistencia.fechaActualizacion = DateTime.Now;
                                    modelo.SubmitChanges();
                                }
                                else
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 2;
                                    registroAsistencia.fechaBaja = DateTime.Now;
                                    modelo.SubmitChanges();
                                    listaResultado.Add(registroAsistencia);
                                }
                                #endregion
                            }

                            else if (item.FechaRefrigerio == Convert.ToDateTime("25/12/2015"))
                            {
                                #region
                                var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                                if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 4)
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 1;
                                    registroAsistencia.fechaActualizacion = DateTime.Now;
                                    modelo.SubmitChanges();
                                }
                                else
                                {
                                    SJM_Pensione registroAsistencia = new SJM_Pensione();
                                    registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                    registroAsistencia.estado = 2;
                                    registroAsistencia.fechaBaja = DateTime.Now;
                                    modelo.SubmitChanges();
                                    listaResultado.Add(registroAsistencia);
                                }
                                #endregion
                            }

                            #region
                            /*
                        else if (item.FechaRefrigerio == Convert.ToDateTime("03/03/2016"))
                        {
                            #region
                            var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                            if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 4)
                            {
                                SJM_Pensiones registroAsistencia = new SJM_Pensiones();
                                registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                registroAsistencia.estado = 1;
                                registroAsistencia.fechaActualizacion = DateTime.Now;
                                modelo.SubmitChanges();
                            }
                            else
                            {
                                SJM_Pensiones registroAsistencia = new SJM_Pensiones();
                                registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                registroAsistencia.estado = 2;
                                registroAsistencia.fechaBaja = DateTime.Now;
                                modelo.SubmitChanges();
                                listaResultado.Add(registroAsistencia);
                            }
                            #endregion
                        }
                            
                             * 
                             * */
                            #endregion
                            /*  Agregado el 21.03.16 dias excluidos desde aplicacion de programación de dias que no entran al descuento */

                            if (olistadoDiasExcluidosaDescuentoByPeriodo != null && olistadoDiasExcluidosaDescuentoByPeriodo.ToList().Count > 0)
                            {
                                #region No incluir estos dias para el descuento()
                                foreach (var itemDiaExcluido in olistadoDiasExcluidosaDescuentoByPeriodo)
                                {
                                    if (item.FechaRefrigerio == itemDiaExcluido.fecha)
                                    {
                                        var resultadoPersonalQueLaboroMas4diasSemana = ListaAsistencias.Where(x => x.DniTrabajador.ToString().Trim() == item.DniTrabajador.ToString().Trim() && x.semanaAsistencia == item.semanaAsistencia).ToList();

                                        if (resultadoPersonalQueLaboroMas4diasSemana != null && resultadoPersonalQueLaboroMas4diasSemana.ToList().Count >= 0)
                                        {
                                            var resultadoCoincidenciaByAsistenciaTransferida = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia && x.estado != 0).ToList();

                                            if (resultadoCoincidenciaByAsistenciaTransferida != null && resultadoCoincidenciaByAsistenciaTransferida.ToList().Count >= 0)
                                            {
                                                SJM_Pensione registroAsistencia = new SJM_Pensione();
                                                registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                                registroAsistencia.estado = 1;
                                                registroAsistencia.fechaActualizacion = DateTime.Now;
                                                registroAsistencia.observacion = "Esta asistencia fue activada el " + DateTime.Now.ToShortDateString() + ", no habiendose trabajado el dia por razones de " + itemDiaExcluido.observacion.ToString().Trim() + " autorizado por " + itemDiaExcluido.usuarioRegistro.ToString().Trim();
                                                modelo.SubmitChanges();
                                            }

                                        }
                                        else
                                        {
                                            SJM_Pensione registroAsistencia = new SJM_Pensione();
                                            registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                            registroAsistencia.estado = 2;
                                            registroAsistencia.fechaBaja = DateTime.Now;
                                            modelo.SubmitChanges();
                                            listaResultado.Add(registroAsistencia);
                                        }

                                    }
                                }
                                #endregion
                            }

                            else
                            {
                                SJM_Pensione registroAsistencia = new SJM_Pensione();
                                registroAsistencia = modelo.SJM_Pensiones.Where(x => x.IdPension == item.idMovimientoTransferencia).Single();
                                registroAsistencia.estado = 2;
                                registroAsistencia.fechaBaja = DateTime.Now;
                                modelo.SubmitChanges();
                                listaResultado.Add(registroAsistencia);
                            }
                            #endregion
                        }
                        #endregion
                        #endregion
                    }
                }
                #endregion
                modelo.Connection.Close();
                modelo.Dispose();
            }
            return listaResultado;
        }

        /*13-01-2016*/
        public List<SJ_RHListaPersonalParaDescuentoPensionbyProveedorbyFechaResult> ListarNombresPersonalParaReporteDescuentoByPensionByProveedorByPeriodo(string dniResponsablePension, string fechaDesde, string fechaHasta)
        {
            List<SJ_RHListaPersonalParaDescuentoPensionbyProveedorbyFechaResult> listado = new List<SJ_RHListaPersonalParaDescuentoPensionbyProveedorbyFechaResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;
                listado = modelo.SJ_RHListaPersonalParaDescuentoPensionbyProveedorbyFecha(dniResponsablePension, fechaDesde, fechaHasta).ToList();
                modelo.Connection.Close();
                modelo.Dispose();
            }
            return listado;
        }

        public void ProcesoCorregirNombresPersonalDesconocido(string fechaDesde, string fechaHasta, string dniPension)
        {
            try
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
                using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
                {
                    modelo.CommandTimeout = 9888999;
                    modelo.SJ_RHProcesoCorregirNombresPersonalDesconocidoAsistenciaRefrigerios(fechaDesde, fechaHasta, dniPension);
                    modelo.Connection.Close();
                    modelo.Dispose();
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


        }

        public void ProcesoActualizarAsistenciaRefrigeriosAEstadoPendiente(string fechaDesde, string fechaHasta, string dniPension)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;
                modelo.SJ_RHProcesoActivarAsistenciaRefrigeriosByPensionByPeriodo(fechaDesde, fechaHasta, dniPension);
                modelo.Connection.Close();
                modelo.Dispose();
            }
        }

        public void ProcesoAnularAsistenciaDuplicadasByAsistenciasRefrigerios(string fechaDesde, string fechaHasta, string dniPension)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;
                modelo.SJ_RHProcesoActivarAsistenciaRefrigeriosByPensionByPeriodo(fechaDesde, fechaHasta, dniPension);
                modelo.Connection.Close();
                modelo.Dispose();
            }
        }

        public List<SJ_RHPensionRefrigerioPersona> ValidarDuplicidadRegistro(string Periodo, SJ_RHPensionRefrigerioPersona oProgramacionRefrigerio)
        {
            /* Si es true, que me deje grabar, si es false tiene duplicidad */

            List<SJ_RHPensionRefrigerioPersona> listado = new List<SJ_RHPensionRefrigerioPersona>();
            bool estado = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;

                /* 1.- Obtengo todos los registros de mi programación que esten activos */
               var  resultadolistado = modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.IdCodigoPersonal.ToString().Trim() == oProgramacionRefrigerio.IdCodigoPersonal.ToString().Trim() &&  x.IdEstado == "AC").ToList();

                /* 2.- Verifico si se trata de una edición o y es un nuevo registro*/
               if (resultadolistado.Where(x => x.IdPension == oProgramacionRefrigerio.IdPension).ToList().Count() == 1)
                {
                    if (resultadolistado.Where(x => x.IdPension == oProgramacionRefrigerio.IdPension).Single().Id > 0)
                    {
                        
                    }
                    else  
                    {
                        listado = resultadolistado.Where(x => x.IdPension == oProgramacionRefrigerio.IdPension).ToList();
                    }
                    listado = new List<SJ_RHPensionRefrigerioPersona>();                   
                }
                else
                {
                    listado = resultadolistado;
                }

                modelo.Connection.Close();
                modelo.Dispose();
            }

            return listado;
        }

        public void EliminarProgramacionAsistenciaRefrigerioByTrabajador(string periodoConsulta, SJ_RHPensionRefrigerioPersona ObjetoRefrigerioPersona, int p)
        {
            try
            {
                string cnx = string.Empty;
                cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
                using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
                {
                    Modelo.CommandTimeout = 9998000;
                    #region Actualizar Estado
                    if (Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == ObjetoRefrigerioPersona.Id).ToList().Count() == 1)
                    {
                        #region Actualizar

                        var detalleProgramacion = Modelo.SJ_RHPensionRefrigerioPersonaDetalle.Where(x => x.Id == ObjetoRefrigerioPersona.Id).ToList();

                        if (detalleProgramacion != null && detalleProgramacion.ToList().Count > 0)
                        {
                            foreach (var detalle in detalleProgramacion)
                            {
                                SJ_RHPensionRefrigerioPersonaDetalle oDetalle = new SJ_RHPensionRefrigerioPersonaDetalle();
                                oDetalle = Modelo.SJ_RHPensionRefrigerioPersonaDetalle.Where(x => x.Id == ObjetoRefrigerioPersona.Id && x.Item.ToString().Trim() == detalle.Item.ToString().Trim()).Single();
                                Modelo.SJ_RHPensionRefrigerioPersonaDetalle.DeleteOnSubmit(oDetalle);
                            }
                        }

                        Personal = new SJ_RHPensionRefrigerioPersona();
                        Personal = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.Id == ObjetoRefrigerioPersona.Id).Single();
                        Modelo.SJ_RHPensionRefrigerioPersona.DeleteOnSubmit(Personal);
                        Modelo.SubmitChanges();
                        #endregion
                    }
                    Modelo.Connection.Close();
                    Modelo.Dispose();
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<SJ_RHPensionRefrigerioPersonaListarResult> ObtenerProgramacionAsistenciaRefrigerioByTrabajador(string periodoConsulta, SJ_RHPensionRefrigerioPersona oRefrigerioPersonaSelecionado)
        {
            List<SJ_RHPensionRefrigerioPersonaListarResult> listado = new List<SJ_RHPensionRefrigerioPersonaListarResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd2014"].ToString();
            using (PensionRefrigeriosDataContext modelo = new PensionRefrigeriosDataContext(cnx))
            {
                modelo.CommandTimeout = 9888999;

                var resultadoConsulta = modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.IdCodigoPersonal.ToString().Trim() == oRefrigerioPersonaSelecionado.IdCodigoPersonal.ToString().Trim()).ToList();

                listado = (from item in resultadoConsulta
                           where item.Id > 0
                           group item by new { item.Id } into j
                           select new SJ_RHPensionRefrigerioPersonaListarResult
                           {
                               Id = j.Key.Id,
                               IdCodigoPersonal = j.FirstOrDefault().IdCodigoPersonal != null ? j.FirstOrDefault().IdCodigoPersonal.ToString().Trim() : "",
                               IdPension = j.FirstOrDefault().IdPension != null ? j.FirstOrDefault().IdPension : 0,
                               Pension = j.FirstOrDefault().Pension != null ? j.FirstOrDefault().Pension.ToString().Trim() : "",
                               Desayuno = j.FirstOrDefault().Desayuno != null ? j.FirstOrDefault().Desayuno : 0,
                               Almuerzo = j.FirstOrDefault().Almuerzo != null ? j.FirstOrDefault().Almuerzo : 0,
                               Cena = j.FirstOrDefault().Cena != null ? j.FirstOrDefault().Cena : 0,
                               Otro = j.FirstOrDefault().Otro != null ? j.FirstOrDefault().Otro : 0,
                               ValidoDesde = j.FirstOrDefault().ValidoDesde != null ? j.FirstOrDefault().ValidoDesde : (DateTime?)null,
                               ValidoHasta = j.FirstOrDefault().ValidoHasta != null ? j.FirstOrDefault().ValidoHasta : (DateTime?)null,
                               IdEstado = j.FirstOrDefault().IdEstado != null ? j.FirstOrDefault().IdEstado.ToString().Trim() : "AN",
                           }).ToList();

                modelo.Connection.Close();
                modelo.Dispose();
            }
            return listado;
        }

        public SJ_ListadoPersonalGeneral ObtenerRegistroPersonalGeneral(string numeroDocumentoPersonal)
        {
            SJ_ListadoPersonalGeneral registroPersonal = new SJ_ListadoPersonalGeneral();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998000;

                var resultadoConsulta = Modelo.SJ_ListadoPersonalGenerals.Where(x => x.numeroDocumento.ToString().Trim() == numeroDocumentoPersonal).ToList();

                if (resultadoConsulta.ToList().Count == 1)
                {
                    registroPersonal = resultadoConsulta.Single();
                }
                else
                {
                    registroPersonal = new SJ_ListadoPersonalGeneral();
                    registroPersonal.numeroDocumento = numeroDocumentoPersonal;
                    registroPersonal.nombresCompletos = "DESCONOCIDO";
                    registroPersonal.subPlanilla = "DESCONOCIDO";
                }

                Modelo.Connection.Close();
                Modelo.Dispose();
            }
            return registroPersonal;
        }

        public List<SJ_RHPensionRefrigerioPersona> ObtenerListadoProgramacionPersonalBaseDatosByTrabajador(string nroDniTrabajador)
        {
            List<SJ_RHPensionRefrigerioPersona> listado = new List<SJ_RHPensionRefrigerioPersona>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["bd" + DateTime.Now.Year.ToString()].ToString();
            using (PensionRefrigeriosDataContext Modelo = new PensionRefrigeriosDataContext(cnx))
            {
                Modelo.CommandTimeout = 9998000;

                listado = Modelo.SJ_RHPensionRefrigerioPersona.Where(x => x.NroDocumento.ToString().Trim() == nroDniTrabajador.ToString().Trim()).ToList();

                Modelo.Connection.Close();
                Modelo.Dispose();
            }

            return listado;
        }
    }
}
