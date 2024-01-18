using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
   public class SAS_PlanesDeTelefoniaMovilController
    {
        
        public List<SAS_PlanesDeTelefoniaMovilListado> ListOfMobilePhonePlans(string conection)
        {
            List<SAS_PlanesDeTelefoniaMovilListado> list = new List<SAS_PlanesDeTelefoniaMovilListado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_PlanesDeTelefoniaMovilListado.ToList();
            }
            return list.OrderBy(x => x.descripcion).ToList();
        }

        public int ToRegister(string conection, SAS_PlanesDeTelefoniaMovil item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_PlanesDeTelefoniaMovil.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            //int ObtenerUltimoItem = Modelo.AREAS.ToList().Count > 0 ? Convert.ToInt32(Modelo.AREAS.ToList().Max(x => x.IDAREA)) + 1 : 0;
                            #region Nuevo() 
                            SAS_PlanesDeTelefoniaMovil oregistro = new SAS_PlanesDeTelefoniaMovil();                           
                            oregistro.CF = item.CF != null ? item.CF : 0;
                            oregistro.descripcion = item.descripcion != null ? item.descripcion.Trim() : string.Empty;
                            oregistro.datos = item.datos != null ? item.datos : 0;
                            oregistro.GbInternacional = item.GbInternacional != null ? item.GbInternacional : 0;
                            oregistro.zonaNavegacion = item.zonaNavegacion != null ? item.zonaNavegacion.Trim() : string.Empty;
                            oregistro.whastappInternacional = item.whastappInternacional != null ? item.whastappInternacional.Trim() : string.Empty;
                            oregistro.bpIliminatado = item.bpIliminatado != null ? item.bpIliminatado.Trim() : string.Empty;
                            oregistro.minutosVozR1 = item.minutosVozR1 != null ? item.minutosVozR1.Trim() : string.Empty;
                            oregistro.minutosVozR2 = item.minutosVozR2 != null ? item.minutosVozR2.Trim() : string.Empty;
                            oregistro.tipo = item.tipo != null ? item.tipo.Trim() : string.Empty;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.idProveedor = item.idProveedor != null ? item.idProveedor : 1;
                            oregistro.fechaCreacion = item.fechaCreacion != null ? item.fechaCreacion.Value : (DateTime?)null;
                            oregistro.creadoPor = item.creadoPor != null ? item.creadoPor.Trim() : string.Empty;                        
                            Modelo.SAS_PlanesDeTelefoniaMovil.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_PlanesDeTelefoniaMovil oregistro = new SAS_PlanesDeTelefoniaMovil();
                            oregistro = resultado.Single();
                            oregistro.CF = item.CF != null ? item.CF : 0;
                            oregistro.descripcion = item.descripcion != null ? item.descripcion.Trim() : string.Empty;
                            oregistro.datos = item.datos != null ? item.datos : 0;
                            oregistro.GbInternacional = item.GbInternacional != null ? item.GbInternacional : 0;
                            oregistro.zonaNavegacion = item.zonaNavegacion != null ? item.zonaNavegacion.Trim() : string.Empty;
                            oregistro.whastappInternacional = item.whastappInternacional != null ? item.whastappInternacional.Trim() : string.Empty;
                            oregistro.bpIliminatado = item.bpIliminatado != null ? item.bpIliminatado.Trim() : string.Empty;
                            oregistro.minutosVozR1 = item.minutosVozR1 != null ? item.minutosVozR1.Trim() : string.Empty;
                            oregistro.minutosVozR2 = item.minutosVozR2 != null ? item.minutosVozR2.Trim() : string.Empty;
                            oregistro.tipo = item.tipo != null ? item.tipo.Trim() : string.Empty;
                            oregistro.estado = item.estado != null ? item.estado : 1;
                            oregistro.idProveedor = item.idProveedor != null ? item.idProveedor : 1;
                            oregistro.fechaCreacion = item.fechaCreacion != null ? item.fechaCreacion.Value : (DateTime?)null;
                            oregistro.creadoPor = item.creadoPor != null ? item.creadoPor.Trim() : string.Empty;
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

        public int ChangeState(string conection, SAS_PlanesDeTelefoniaMovil item)
        {
            SAS_PlanesDeTelefoniaMovil oregistro = new SAS_PlanesDeTelefoniaMovil();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_PlanesDeTelefoniaMovil.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_PlanesDeTelefoniaMovil();
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

        public int Remove(string conection, SAS_PlanesDeTelefoniaMovil item)
        {
            SAS_PlanesDeTelefoniaMovil oregistro = new SAS_PlanesDeTelefoniaMovil();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_PlanesDeTelefoniaMovil.Where(x => x.id == item.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_PlanesDeTelefoniaMovil();
                        oregistro = resultado.Single();
                        Modelo.SAS_PlanesDeTelefoniaMovil.DeleteOnSubmit(oregistro);
                        Modelo.SubmitChanges();
                        tipoResultadoOperacion = 4;
                        #endregion
                    }
                }
            }
            return tipoResultadoOperacion;
        }


    }
}
