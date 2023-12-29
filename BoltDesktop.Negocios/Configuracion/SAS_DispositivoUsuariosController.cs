using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoUsuariosController
    {

        public List<SAS_ListadoColaboradoresByDispositivo> ListadoDeColaboradoresByDispositivo(string conection, int tipoPresentacion)
        {
            List<SAS_ListadoColaboradoresByDispositivo> resultado = new List<SAS_ListadoColaboradoresByDispositivo>();
            List<SAS_ListadoColaboradoresByDispositivo> resultado2 = new List<SAS_ListadoColaboradoresByDispositivo>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoColaboradoresByDispositivo.ToList();

                if (tipoPresentacion == 1)
                {
                    #region 
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count > 1)
                        {
                            var personal = (from item in resultado
                                            group item by new { item.idcodigogeneral } into j
                                            select new { codigoPersonal = j.Key.idcodigogeneral }).ToList();

                            foreach (var colaborador in personal)
                            {

                                var result1 = resultado.Where(x => x.idcodigogeneral == colaborador.codigoPersonal).ToList();

                                if (result1.ToList().Count == 1)
                                {
                                    resultado2.Add(result1.Single());

                                }
                                else if (result1.ToList().Count > 1)
                                {
                                    // detalle de equipos asociados al colaborador
                                    string NombreEquipos = string.Empty;

                                    var equiposPorUsuario = (from item in result1
                                                             group item by new { item.dispositivoCodigo, item.dispositivo } into j
                                                             select new { codigoDispositivo = j.Key.dispositivoCodigo, dispositivo = j.Key.dispositivo }).ToList();

                                    if (equiposPorUsuario != null)
                                    {
                                        if (equiposPorUsuario.ToList().Count > 0)
                                        {
                                            foreach (var device in equiposPorUsuario)
                                            {
                                                NombreEquipos += "Cod " + device.codigoDispositivo.ToString().PadLeft(5, '0').Trim() + " - " + device.dispositivo.Trim() + " |";
                                            }
                                        }
                                    }
                                    // falta ponerlo todo en una sola línea.

                                    SAS_ListadoColaboradoresByDispositivo userDispositivo = new SAS_ListadoColaboradoresByDispositivo();
                                    userDispositivo.idcodigogeneral = colaborador.codigoPersonal.Trim();
                                    userDispositivo.nrodocumento = result1.FirstOrDefault().nrodocumento != null ? result1.FirstOrDefault().nrodocumento : string.Empty;
                                    userDispositivo.apenom = result1.FirstOrDefault().apenom != null ? result1.FirstOrDefault().apenom : string.Empty;
                                    userDispositivo.a_paterno = result1.FirstOrDefault().a_paterno != null ? result1.FirstOrDefault().a_paterno : string.Empty;
                                    userDispositivo.a_materno = result1.FirstOrDefault().a_materno != null ? result1.FirstOrDefault().a_materno : string.Empty;
                                    userDispositivo.nombres = result1.FirstOrDefault().nombres != null ? result1.FirstOrDefault().nombres : string.Empty;
                                    userDispositivo.sexo = result1.FirstOrDefault().sexo != null ? result1.FirstOrDefault().sexo : "X";
                                    userDispositivo.afp_dsc = result1.FirstOrDefault().afp_dsc != null ? result1.FirstOrDefault().afp_dsc : string.Empty;
                                    userDispositivo.Asig_fam = result1.FirstOrDefault().Asig_fam != null ? result1.FirstOrDefault().Asig_fam : string.Empty;
                                    userDispositivo.l_negra = result1.FirstOrDefault().l_negra != null ? result1.FirstOrDefault().l_negra : string.Empty;
                                    userDispositivo.estado = result1.FirstOrDefault().estado != null ? result1.FirstOrDefault().estado : string.Empty;
                                    userDispositivo.PlanActual = result1.FirstOrDefault().PlanActual != null ? result1.FirstOrDefault().PlanActual : string.Empty;
                                    userDispositivo.ListaPlan = result1.FirstOrDefault().ListaPlan != null ? result1.FirstOrDefault().ListaPlan : string.Empty;
                                    userDispositivo.dsc_planilla = result1.FirstOrDefault().dsc_planilla != null ? result1.FirstOrDefault().dsc_planilla : string.Empty;
                                    userDispositivo.NroPlanAct = result1.FirstOrDefault().NroPlanAct != null ? result1.FirstOrDefault().NroPlanAct : 0;
                                    userDispositivo.codigo_control = result1.FirstOrDefault().codigo_control != null ? result1.FirstOrDefault().codigo_control : string.Empty;
                                    userDispositivo.cargo = result1.FirstOrDefault().cargo != null ? result1.FirstOrDefault().cargo : string.Empty;
                                    userDispositivo.personalexterno = result1.FirstOrDefault().personalexterno != null ? result1.FirstOrDefault().personalexterno : 0;
                                    userDispositivo.vacaciones = result1.FirstOrDefault().vacaciones != null ? result1.FirstOrDefault().vacaciones : 0;
                                    userDispositivo.descanso = result1.FirstOrDefault().descanso != null ? result1.FirstOrDefault().descanso : 0;
                                    userDispositivo.fechaActualizacion = result1.FirstOrDefault().fechaActualizacion != null ? result1.FirstOrDefault().fechaActualizacion : (DateTime?)null;
                                    userDispositivo.dispositivoCodigo = 0;
                                    userDispositivo.dispositivo = NombreEquipos;
                                    userDispositivo.item = "001";
                                    userDispositivo.estadoItem = 1;
                                    userDispositivo.desde = result1.FirstOrDefault().desde != null ? result1.FirstOrDefault().desde : DateTime.Now;
                                    userDispositivo.hasta = result1.FirstOrDefault().hasta != null ? result1.FirstOrDefault().hasta : DateTime.Now;
                                    userDispositivo.observacion = NombreEquipos;
                                    userDispositivo.fechaCreacion = result1.FirstOrDefault().fechaCreacion != null ? result1.FirstOrDefault().fechaCreacion : DateTime.Now;
                                    userDispositivo.registradoPor = result1.FirstOrDefault().registradoPor != null ? result1.FirstOrDefault().registradoPor : string.Empty;
                                    userDispositivo.tipo = result1.FirstOrDefault().tipo != null ? result1.FirstOrDefault().tipo : "X";
                                    resultado2.Add(userDispositivo);

                                }
                            }
                        }
                    }
                    #endregion

                }
                else if (tipoPresentacion == 0)
                {
                    // detallado
                    resultado2 = resultado;
                }
            }

            return resultado2;
        }


        public List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult> ListadoDetalleDeDispositivosPorColaborador(string conection, string idCodigoGeneral)
        {
            List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult> resultado = new List<SAS_ListadoColaboradoresByDispositivoByIdColaboradorResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoColaboradoresByDispositivoByIdColaborador(idCodigoGeneral).ToList();
            }
            return resultado;
        }




        public List<SAS_ListadoColaboradoresByDispositivo> ListadoDeColaboradoresByDispositivo(string conection, int tipoPresentacion, string idCodigoTrabajador)
        {
            List<SAS_ListadoColaboradoresByDispositivo> resultado = new List<SAS_ListadoColaboradoresByDispositivo>();
            List<SAS_ListadoColaboradoresByDispositivo> resultado2 = new List<SAS_ListadoColaboradoresByDispositivo>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoColaboradoresByDispositivo.Where(x => x.idcodigogeneral == idCodigoTrabajador).ToList();

                if (tipoPresentacion == 1) // si es uno es agrupado
                {
                    #region 
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count > 1)
                        {
                            var personal = (from item in resultado
                                            group item by new { item.idcodigogeneral } into j
                                            select new { codigoPersonal = j.Key.idcodigogeneral }).ToList();

                            foreach (var colaborador in personal)
                            {

                                var result1 = resultado.Where(x => x.idcodigogeneral == colaborador.codigoPersonal).ToList();

                                if (result1.ToList().Count == 1)
                                {
                                    resultado2.Add(result1.Single());

                                }
                                else if (result1.ToList().Count > 1)
                                {
                                    // detalle de equipos asociados al colaborador
                                    string NombreEquipos = string.Empty;

                                    var equiposPorUsuario = (from item in result1
                                                             group item by new { item.dispositivoCodigo, item.dispositivo } into j
                                                             select new { codigoDispositivo = j.Key.dispositivoCodigo, dispositivo = j.Key.dispositivo }).ToList();

                                    if (equiposPorUsuario != null)
                                    {
                                        if (equiposPorUsuario.ToList().Count > 0)
                                        {
                                            foreach (var device in equiposPorUsuario)
                                            {
                                                NombreEquipos += "Cod " + device.codigoDispositivo.ToString().PadLeft(5, '0').Trim() + " - " + device.dispositivo.Trim() + " |";
                                            }
                                        }
                                    }
                                    // falta ponerlo todo en una sola línea.

                                    SAS_ListadoColaboradoresByDispositivo userDispositivo = new SAS_ListadoColaboradoresByDispositivo();
                                    userDispositivo.idcodigogeneral = colaborador.codigoPersonal.Trim();
                                    userDispositivo.nrodocumento = result1.FirstOrDefault().nrodocumento != null ? result1.FirstOrDefault().nrodocumento : string.Empty;
                                    userDispositivo.apenom = result1.FirstOrDefault().apenom != null ? result1.FirstOrDefault().apenom : string.Empty;
                                    userDispositivo.a_paterno = result1.FirstOrDefault().a_paterno != null ? result1.FirstOrDefault().a_paterno : string.Empty;
                                    userDispositivo.a_materno = result1.FirstOrDefault().a_materno != null ? result1.FirstOrDefault().a_materno : string.Empty;
                                    userDispositivo.nombres = result1.FirstOrDefault().nombres != null ? result1.FirstOrDefault().nombres : string.Empty;
                                    userDispositivo.sexo = result1.FirstOrDefault().sexo != null ? result1.FirstOrDefault().sexo : "X";
                                    userDispositivo.afp_dsc = result1.FirstOrDefault().afp_dsc != null ? result1.FirstOrDefault().afp_dsc : string.Empty;
                                    userDispositivo.Asig_fam = result1.FirstOrDefault().Asig_fam != null ? result1.FirstOrDefault().Asig_fam : string.Empty;
                                    userDispositivo.l_negra = result1.FirstOrDefault().l_negra != null ? result1.FirstOrDefault().l_negra : string.Empty;
                                    userDispositivo.estado = result1.FirstOrDefault().estado != null ? result1.FirstOrDefault().estado : string.Empty;
                                    userDispositivo.PlanActual = result1.FirstOrDefault().PlanActual != null ? result1.FirstOrDefault().PlanActual : string.Empty;
                                    userDispositivo.ListaPlan = result1.FirstOrDefault().ListaPlan != null ? result1.FirstOrDefault().ListaPlan : string.Empty;
                                    userDispositivo.dsc_planilla = result1.FirstOrDefault().dsc_planilla != null ? result1.FirstOrDefault().dsc_planilla : string.Empty;
                                    userDispositivo.NroPlanAct = result1.FirstOrDefault().NroPlanAct != null ? result1.FirstOrDefault().NroPlanAct : 0;
                                    userDispositivo.codigo_control = result1.FirstOrDefault().codigo_control != null ? result1.FirstOrDefault().codigo_control : string.Empty;
                                    userDispositivo.cargo = result1.FirstOrDefault().cargo != null ? result1.FirstOrDefault().cargo : string.Empty;
                                    userDispositivo.personalexterno = result1.FirstOrDefault().personalexterno != null ? result1.FirstOrDefault().personalexterno : 0;
                                    userDispositivo.vacaciones = result1.FirstOrDefault().vacaciones != null ? result1.FirstOrDefault().vacaciones : 0;
                                    userDispositivo.descanso = result1.FirstOrDefault().descanso != null ? result1.FirstOrDefault().descanso : 0;
                                    userDispositivo.fechaActualizacion = result1.FirstOrDefault().fechaActualizacion != null ? result1.FirstOrDefault().fechaActualizacion : (DateTime?)null;
                                    userDispositivo.dispositivoCodigo = 0;
                                    userDispositivo.dispositivo = NombreEquipos;
                                    userDispositivo.item = "001";
                                    userDispositivo.estadoItem = 1;
                                    userDispositivo.desde = result1.FirstOrDefault().desde != null ? result1.FirstOrDefault().desde : DateTime.Now;
                                    userDispositivo.hasta = result1.FirstOrDefault().hasta != null ? result1.FirstOrDefault().hasta : DateTime.Now;
                                    userDispositivo.observacion = NombreEquipos;
                                    userDispositivo.fechaCreacion = result1.FirstOrDefault().fechaCreacion != null ? result1.FirstOrDefault().fechaCreacion : DateTime.Now;
                                    userDispositivo.registradoPor = result1.FirstOrDefault().registradoPor != null ? result1.FirstOrDefault().registradoPor : string.Empty;
                                    userDispositivo.tipo = result1.FirstOrDefault().tipo != null ? result1.FirstOrDefault().tipo : "X";
                                    resultado2.Add(userDispositivo);

                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (tipoPresentacion == 0)
                {
                    // detallado
                    resultado2 = resultado;
                }
            }

            return resultado2;
        }

        public List<SAS_ListadoColaboradoresByDispositivo> ListadoDeColaboradoresByDispositivo2(string conection, int tipoPresentacion)
        {
            List<SAS_ListadoColaboradoresByDispositivo> resultado = new List<SAS_ListadoColaboradoresByDispositivo>();
            List<SAS_ListadoColaboradoresByDispositivo> resultado2 = new List<SAS_ListadoColaboradoresByDispositivo>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoColaboradoresByDispositivo.ToList();
            }

            return resultado;
        }

        public List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> ListadoColaboradoresByDispositivoByCodigo(string conection, int codigo)
        {
            List<SAS_ListadoColaboradoresByDispositivoByCodigoResult> resultado = new List<SAS_ListadoColaboradoresByDispositivoByCodigoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoColaboradoresByDispositivoByCodigo(codigo).ToList();
            }
            return resultado;
        }

        public int Registrar(string conection, SAS_DispositivoUsuarios colaborador)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == colaborador.dispositivoCodigo && x.item == colaborador.item).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 0)
                        {
                            #region Registrar() 
                            SAS_DispositivoUsuarios oRegistro = new SAS_DispositivoUsuarios();
                            oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            oRegistro.item = colaborador.item;
                            oRegistro.estado = colaborador.estado;
                            oRegistro.idcodigoGeneral = colaborador.idcodigoGeneral;
                            oRegistro.desde = colaborador.desde;
                            oRegistro.hasta = colaborador.hasta;
                            oRegistro.observacion = colaborador.observacion;
                            oRegistro.fechaCreacion = colaborador.fechaCreacion;
                            oRegistro.registradoPor = colaborador.registradoPor;
                            oRegistro.tipo = colaborador.tipo;
                            Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoRegistro = 1;
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_DispositivoUsuarios oRegistro = new SAS_DispositivoUsuarios();
                            //oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            //oRegistro.item = colaborador.item;
                            oRegistro = resultado.Single();
                            oRegistro.estado = colaborador.estado;
                            oRegistro.idcodigoGeneral = colaborador.idcodigoGeneral;
                            oRegistro.desde = colaborador.desde;
                            oRegistro.hasta = colaborador.hasta;
                            oRegistro.observacion = colaborador.observacion;
                            oRegistro.fechaCreacion = colaborador.fechaCreacion;
                            oRegistro.registradoPor = colaborador.registradoPor;
                            oRegistro.tipo = colaborador.tipo;
                            //Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoRegistro = 2;
                            #endregion  
                        }
                    }
                    Scope.Complete();
                }
            }
            return tipoRegistro;
        }

        public int CambiarDeEstado(string conection, SAS_DispositivoUsuarios colaborador)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == colaborador.dispositivoCodigo && x.item == colaborador.item).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_DispositivoUsuarios oRegistro = new SAS_DispositivoUsuarios();
                            //oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            //oRegistro.item = colaborador.item;
                            oRegistro = resultado.Single();
                            if (oRegistro.estado == 0)
                            {
                                oRegistro.estado = 1;
                                tipoRegistro = 3; //Activar
                            }
                            else
                            {
                                oRegistro.estado = 0;
                                tipoRegistro = 4; //Desactivar
                            }
                            //Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }

                    Scope.Complete();
                }

            }



            return tipoRegistro;
        }

        public int Eliminar(string conection, SAS_DispositivoUsuarios colaborador)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoUsuarios.Where(x => x.dispositivoCodigo == colaborador.dispositivoCodigo && x.item == colaborador.item).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_DispositivoUsuarios oRegistro = new SAS_DispositivoUsuarios();
                            //oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            //oRegistro.item = colaborador.item;
                            oRegistro = resultado.Single();
                            if (oRegistro.estado == 0)
                            {
                                oRegistro.estado = 1;
                                tipoRegistro = 6; //debe cambiar 
                            }
                            else
                            {
                                Modelo.SAS_DispositivoUsuarios.DeleteOnSubmit(oRegistro);
                                tipoRegistro = 5; //Eliminar
                            }
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }
                    Scope.Complete();
                }
            }
            return tipoRegistro;
        }

        public void AsociarAAreaDeTrabajo(string conection, SAS_ColaboradorAreaTrabajo item)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_ColaboradorAreaTrabajo.Where(x => x.idCodigoGeneral == item.idCodigoGeneral).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 0)
                        {
                            #region Registrar() 
                            SAS_ColaboradorAreaTrabajo oRegistro = new SAS_ColaboradorAreaTrabajo();
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.idArea = item.idArea;
                            oRegistro.estado = item.estado;
                            oRegistro.idGerencia = item.idGerencia;
                            oRegistro.EsJefe = item.EsJefe;
                            oRegistro.EsGerente = item.EsGerente;

                            Modelo.SAS_ColaboradorAreaTrabajo.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_ColaboradorAreaTrabajo oRegistro = new SAS_ColaboradorAreaTrabajo();
                            //oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            //oRegistro.item = colaborador.item;
                            oRegistro = resultado.Single();
                            oRegistro.idArea = item.idArea;
                            oRegistro.estado = item.estado;
                            oRegistro.idGerencia = item.idGerencia;
                            oRegistro.EsJefe = item.EsJefe;
                            oRegistro.EsGerente = item.EsGerente;
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }
                    Scope.Complete();
                }
            }
        }

        public SAS_ColaboradorAreaTrabajo ObtenerDatosDeAsignacionPorAreayGerenciaDeColaboradorPorCodigoEmpleado(string conection, SAS_ColaboradorAreaTrabajo item)
        {
            SAS_ColaboradorAreaTrabajo oItem = new SAS_ColaboradorAreaTrabajo();
            oItem.idGerencia = 0;
            oItem.idArea = string.Empty;

            string cnx = ConfigurationManager.AppSettings[conection != null ? conection : "SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultQuery = Modelo.SAS_ColaboradorAreaTrabajo.Where(x => x.idCodigoGeneral.Trim().ToUpper() == item.idCodigoGeneral).ToList();

                if (resultQuery != null)
                {
                    if (resultQuery.ToList().Count == 1)
                    {
                        oItem = resultQuery.Single();
                    }
                }

            }
            return oItem;
        }

        public SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult ObtenerDatosDeAsignacionPorAreayGerenciaDeColaboradorPorCodigoEmpleado(string conection, SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult item)
        {
            SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult oItem = new SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonalResult();
            oItem.idGerencia = 0;
            oItem.idArea = string.Empty;
            oItem.idcodigoGeneral = item.idcodigoGeneral;

            string cnx = ConfigurationManager.AppSettings[conection != null ? conection : "SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultQuery = Modelo.SAS_EquipamientoObtenerDatosGerenciaAreaByCodigoPersonal(item.idcodigoGeneral).ToList();

                if (resultQuery != null)
                {
                    if (resultQuery.ToList().Count == 1)
                    {
                        oItem = resultQuery.Single();
                    }
                }

            }
            return oItem;
        }

        public SAS_ListadoDeDispositivosAllResult ObtenerDispositivoById(string conection, int id)
        {
            SAS_ListadoDeDispositivosAllResult resultado = new SAS_ListadoDeDispositivosAllResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            //using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var result = Modelo.SAS_ListadoDeDispositivosAll().ToList().Where(x => x.id == id).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        resultado = result.Single();
                    }
                    else if (result.ToList().Count > 1)
                    {
                        resultado = result.ElementAt(0);
                    }
                }

            }

            return resultado;
        }




    }
}
