using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
  public  class SAS_DispositivoTipoHardwareController
    {


        public List<SAS_DispositivoTipoHardware> GetTypeHardwares(string conection)
        {
            List<SAS_DispositivoTipoHardware> listado = new List<SAS_DispositivoTipoHardware>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoTipoHardware.ToList();
            }
            return listado;
        }


        public int Register(string conection, SAS_DispositivoTipoHardware item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoTipoHardware.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();
                            SAS_DispositivoTipoHardware oregistro = new SAS_DispositivoTipoHardware();
                            oregistro.id = item.id;
                            oregistro.descripcion = item.descripcion;
                            oregistro.nombreCorto = item.nombreCorto;
                            oregistro.enFormatoSolicitud = item.enFormatoSolicitud != null ? item.enFormatoSolicitud : 0;
                            oregistro.observaciones = item.observaciones != string.Empty ? item.observaciones : string.Empty;
                            oregistro.estado = item.estado;                                                                                  
                            Modelo.SAS_DispositivoTipoHardware.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_DispositivoTipoHardware oregistro = new SAS_DispositivoTipoHardware();
                            oregistro = resultado.Single();
                            oregistro.descripcion = item.descripcion;
                            oregistro.nombreCorto = item.nombreCorto;
                            oregistro.enFormatoSolicitud = item.enFormatoSolicitud != null ? item.enFormatoSolicitud : 0;
                            oregistro.observaciones = item.observaciones != string.Empty ? item.observaciones : string.Empty;
                            oregistro.estado = item.estado;

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

        public int ChangeState(string conection, SAS_DispositivoTipoHardware tipoSoftware)
        {
            
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoTipoHardware.Where(x => x.id == tipoSoftware.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()
                        SAS_DispositivoTipoHardware oregistro = new SAS_DispositivoTipoHardware();
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
