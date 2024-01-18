using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_LicenciaCorreoController
    {
        public List<SAS_LicenciaCorreo> LicenseTypeList(string conection)
        {
            List<SAS_LicenciaCorreo> list = new List<SAS_LicenciaCorreo>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_LicenciaCorreo.ToList();
            }
            return list.OrderBy(x => x.descripcion).ToList();
        }

        public int ToRegister(string conection, SAS_LicenciaCorreo item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_LicenciaCorreo.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.SAS_LicenciaCorreo.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            SAS_LicenciaCorreo oregistro = new SAS_LicenciaCorreo();
                            //oregistro.id = item.id;
                            oregistro.descripcion = item.descripcion != null ? item.descripcion.Trim() : string.Empty;
                            oregistro.estado = item.estado != null ? item.estado.Value : 0;
                            oregistro.observacion = item.observacion != null ? item.observacion.Trim() : string.Empty;
                            Modelo.SAS_LicenciaCorreo.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_LicenciaCorreo oregistro = new SAS_LicenciaCorreo();
                            oregistro = resultado.Single();
                            oregistro.descripcion = item.descripcion != null ? item.descripcion.Trim() : string.Empty;
                            oregistro.estado = item.estado != null ? item.estado.Value : 0;
                            oregistro.observacion = item.observacion != null ? item.observacion.Trim() : string.Empty;
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

        public int ChangeState(string conection, SAS_LicenciaCorreo item)
        {
            SAS_LicenciaCorreo oregistro = new SAS_LicenciaCorreo();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_LicenciaCorreo.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_LicenciaCorreo();
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

        

    }
}
