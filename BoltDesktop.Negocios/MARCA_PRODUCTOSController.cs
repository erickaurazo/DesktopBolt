using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class MARCA_PRODUCTOSController
    {


        public List<MARCA_PRODUCTOS> GetProductBrand(string conection)
        {
            List<MARCA_PRODUCTOS> listado = new List<MARCA_PRODUCTOS>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.MARCA_PRODUCTOS.ToList();
            }
            return listado.OrderBy(x => x.DESCRIPCION).ToList();
        }


        public int Register(string conection, MARCA_PRODUCTOS item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.MARCA_PRODUCTOS.Where(x => x.IDMARCA == item.IDMARCA).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.MARCA_PRODUCTOS.ToList().Count > 0 ? Convert.ToInt32(Modelo.MARCA_PRODUCTOS.ToList().Max(x => x.IDMARCA)) + 1 : 0;
                            #region Nuevo() 
                            MARCA_PRODUCTOS oregistro = new MARCA_PRODUCTOS();
                            oregistro.IDEMPRESA = "001";
                            oregistro.IDMARCA = ObtenerUltimoItem.ToString().PadLeft(4, '0');
                            oregistro.DESCRIPCION = item.DESCRIPCION;
                            oregistro.ESTADO = item.ESTADO;
                            oregistro.SINCRONIZA = 'N';
                            oregistro.FECHACREACION = DateTime.Now;
                            oregistro.ES_MANTENIMIENTO = 0;
                            oregistro.ES_PRODUCCION = 0;
                            oregistro.ES_ACTIVO = item.ES_ACTIVO; ;
                            oregistro.ES_VEHICULO = 0;
                            oregistro.IDCLIEPROV = string.Empty;
                            oregistro.ES_EQUIPOMOVIL = item.ES_EQUIPOMOVIL;
                            Modelo.MARCA_PRODUCTOS.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            MARCA_PRODUCTOS oregistro = new MARCA_PRODUCTOS();
                            oregistro = resultado.Single();
                            oregistro.DESCRIPCION = item.DESCRIPCION;
                            oregistro.ESTADO = item.ESTADO;
                            oregistro.SINCRONIZA = 'N';
                            oregistro.FECHACREACION = DateTime.Now;
                            oregistro.ES_MANTENIMIENTO = 0;
                            oregistro.ES_PRODUCCION = 0;
                            oregistro.ES_ACTIVO = 1;
                            oregistro.ES_VEHICULO = 0;
                            oregistro.IDCLIEPROV = string.Empty;
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

        public int ChangeState(string conection, MARCA_PRODUCTOS item)
        {
            MARCA_PRODUCTOS oregistro = new MARCA_PRODUCTOS();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.MARCA_PRODUCTOS.Where(x => x.IDMARCA == item.IDMARCA).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new MARCA_PRODUCTOS();
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
