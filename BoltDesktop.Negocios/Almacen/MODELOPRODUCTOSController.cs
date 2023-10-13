using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class MODELOPRODUCTOSController
    {


        public List<MODELOPRODUCTOS> GetProductsModel(string conection)
        {
            List<MODELOPRODUCTOS> listado = new List<MODELOPRODUCTOS>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.MODELOPRODUCTOS.ToList();
            }
            return listado.OrderBy(x => x.DESCRIPCION).ToList();
        }


        public int Register(string conection, MODELOPRODUCTOS item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.MODELOPRODUCTOS.Where(x => x.IDMODELO == item.IDMODELO).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.MODELOPRODUCTOS.ToList().Count > 0 ? Convert.ToInt32(Modelo.MODELOPRODUCTOS.ToList().Max(x => x.IDMODELO)) + 1 : 0;
                            #region Nuevo() 
                            MODELOPRODUCTOS oregistro = new MODELOPRODUCTOS();
                            oregistro.IDEMPRESA = "001";
                            oregistro.IDMODELO = ObtenerUltimoItem.ToString().PadLeft(4, '0');
                            oregistro.DESCRIPCION = item.DESCRIPCION;
                            oregistro.ESTADO = item.ESTADO;
                            oregistro.SINCRONIZA = item.SINCRONIZA;
                            oregistro.FECHACREACION = DateTime.Now;
                            oregistro.ES_MANTENIMIENTO = 0;
                            oregistro.ES_PRODUCCION = 0;
                            oregistro.ES_ACTIVO = item.ES_ACTIVO;
                            oregistro.ES_VEHICULO = 0;                            
                            oregistro.ES_EQUIPOMOVIL = item.ES_EQUIPOMOVIL;
                            Modelo.MODELOPRODUCTOS.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            MODELOPRODUCTOS oregistro = new MODELOPRODUCTOS();
                            oregistro = resultado.Single();
                            oregistro.DESCRIPCION = item.DESCRIPCION;
                            oregistro.ESTADO = item.ESTADO;
                            oregistro.SINCRONIZA = item.SINCRONIZA;
                            oregistro.FECHACREACION = DateTime.Now;
                            oregistro.ES_MANTENIMIENTO = 0;
                            oregistro.ES_PRODUCCION = 0;
                            oregistro.ES_ACTIVO = item.ES_ACTIVO;
                            oregistro.ES_VEHICULO = 0;
                            oregistro.ES_EQUIPOMOVIL = item.ES_EQUIPOMOVIL;
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

        public int ChangeState(string conection, MODELOPRODUCTOS item)
        {
            MODELOPRODUCTOS oregistro = new MODELOPRODUCTOS();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.MODELOPRODUCTOS.Where(x => x.IDMODELO == item.IDMODELO).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new MODELOPRODUCTOS();
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
