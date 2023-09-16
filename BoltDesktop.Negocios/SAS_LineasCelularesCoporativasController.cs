using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_LineasCelularesCoporativasController
    {

        public List<SAS_LineasCelularesCoporativasListado> ListOfCellLines(string conection)
        {
            List<SAS_LineasCelularesCoporativasListado> list = new List<SAS_LineasCelularesCoporativasListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasListado.ToList();
            }
            return list.OrderBy(x => x.lineaCelular).ToList();
        }


        public List<SAS_LineasCelularesCoporativasListado> ListOfCellLines(string conection, string numeroCelular)
        {
            List<SAS_LineasCelularesCoporativasListado> list = new List<SAS_LineasCelularesCoporativasListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_LineasCelularesCoporativasListado.Where(x=> x.lineaCelular == numeroCelular).ToList();
            }
            return list.OrderBy(x => x.lineaCelular).ToList();
        }

        public int ToRegister(string conection, SAS_LineasCelularesCoporativas item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.AREAS.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            SAS_LineasCelularesCoporativas oregistro = new SAS_LineasCelularesCoporativas();                            
                            //oregistro.id = item.id != null ? item.id : 0;
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
                            Modelo.SAS_LineasCelularesCoporativas.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_LineasCelularesCoporativas oregistro = new SAS_LineasCelularesCoporativas();
                            oregistro = resultado.Single();
                            oregistro.idOperador = item.idOperador != null ? item.idOperador : (int?)null;
                            oregistro.operador = item.operador != null ? item.operador : string.Empty;
                            oregistro.lineaCelular = item.lineaCelular != null ? item.lineaCelular : string.Empty;
                            oregistro.FechaDeAlta = item.FechaDeAlta != null ? item.FechaDeAlta.Value : (DateTime?)null;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.estadoDescripcion = item.estado == 1 ? "ACTIVO" : "ANULADO";
                            oregistro.idProducto = item.idProducto != null ? item.idProducto : string.Empty;
                            oregistro.equipo = item.equipo != null ? item.equipo : string.Empty;
                            oregistro.idPlanDeTelefoniaMovil = item.idPlanDeTelefoniaMovil != null ? item.idPlanDeTelefoniaMovil.Value : (Int32?)null;
                            oregistro.planDeTelefoniaMovil = item.planDeTelefoniaMovil != null ? item.planDeTelefoniaMovil : string.Empty;
                            oregistro.valorPlan = item.valorPlan != null ? item.valorPlan.Value : (decimal?)null;
                            oregistro.permanenciaFalta = item.permanenciaFalta != null ? item.permanenciaFalta.Value : (Int32?)null;
                            oregistro.penalidad = item.penalidad != null ? item.penalidad.Value : (decimal?)null;
                            oregistro.idCodigoGeneral = item.idCodigoGeneral != null ? item.idCodigoGeneral : string.Empty;
                            oregistro.idCCostoFijo = item.idCCostoFijo != null ? item.idCCostoFijo : string.Empty;
                            oregistro.idCCostoVariable = item.idCCostoVariable != null ? item.idCCostoVariable : string.Empty;
                            oregistro.glosa = item.glosa != null ? item.glosa : string.Empty;
                            oregistro.codigoERP = item.codigoERP != null ? item.codigoERP.Value : (int?)null;
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

        public int ChangeState(string conection, SAS_LineasCelularesCoporativas item)
        {
            SAS_LineasCelularesCoporativas oregistro = new SAS_LineasCelularesCoporativas();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_LineasCelularesCoporativas();
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

        public int Remove(string conection, SAS_LineasCelularesCoporativas item)
        {
            SAS_LineasCelularesCoporativas oregistro = new SAS_LineasCelularesCoporativas();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_LineasCelularesCoporativas.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_LineasCelularesCoporativas();
                        oregistro = resultado.Single();
                        Modelo.SAS_LineasCelularesCoporativas.DeleteOnSubmit(oregistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = 4;
                        #endregion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        // para el reporte de listado de líneas
        public List<SAS_ListadoDeLineasTelefonicas> ListOfCellLinesforReport(string conection)
        {
            List<SAS_ListadoDeLineasTelefonicas> list = new List<SAS_ListadoDeLineasTelefonicas>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeLineasTelefonicas.ToList();
            }
            return list.OrderBy(x => x.FechaDeAlta).ToList();
        }

    }
}
