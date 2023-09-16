using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class AreasController
    {
        public List<AREAS> ListOfWorkAreas(string conection)
        {
            List<AREAS> list = new List<AREAS>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.AREAS.ToList(); 
            }
            return list.OrderBy(x => x.DESCRIPCION).ToList();
        }

        public int ToRegister(string conection, AREAS item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.AREAS.Where(x => x.IDAREA == item.IDAREA).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.AREAS.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            AREAS oregistro = new AREAS();
                            oregistro.IDEMPRESA = "001";
                            oregistro.IDAREA = ObtenerUltimoItem.ToString().PadLeft(3, '0');
                            oregistro.DESCRIPCION = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.ESTADO = item.ESTADO != null ? item.ESTADO.Value : 1;
                            oregistro.SINCRONIZA = item.SINCRONIZA != null ? item.SINCRONIZA.Value : Convert.ToChar("N");
                            oregistro.FECHACREACION = item.FECHACREACION != null ? item.FECHACREACION.Value : (DateTime?)null;
                            oregistro.idconsumidor = item.idconsumidor != null ? item.idconsumidor.Trim() : string.Empty;
                            oregistro.exige_docvinculadoliqgasto = item.exige_docvinculadoliqgasto != null ? item.exige_docvinculadoliqgasto : 0;
                            Modelo.AREAS.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            AREAS oregistro = new AREAS();
                            oregistro = resultado.Single();
                            oregistro.DESCRIPCION = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.ESTADO = item.ESTADO != null ? item.ESTADO.Value : 1;
                            oregistro.SINCRONIZA = item.SINCRONIZA != null ? item.SINCRONIZA.Value : Convert.ToChar("N");
                            oregistro.FECHACREACION = item.FECHACREACION != null ? item.FECHACREACION.Value : (DateTime?)null;
                            oregistro.idconsumidor = item.idconsumidor != null ? item.idconsumidor.Trim() : string.Empty;
                            oregistro.exige_docvinculadoliqgasto = item.exige_docvinculadoliqgasto != null ? item.exige_docvinculadoliqgasto : 0;
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

        public int ChangeState(string conection, AREAS item)
        {
            AREAS oregistro = new AREAS();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.AREAS.Where(x => x.IDAREA == item.IDAREA).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new AREAS();
                        oregistro = resultado.Single();

                        if (oregistro.ESTADO == 1)
                        {
                            oregistro.ESTADO = 0;
                            tipoResultadoOperacion = 2; // desactivar
                        }
                        else
                        {
                            oregistro.ESTADO = 1;
                            tipoResultadoOperacion = 3; // Activar
                        }
                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


    }
}
