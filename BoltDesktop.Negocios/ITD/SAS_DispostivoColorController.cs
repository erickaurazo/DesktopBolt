using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DispostivoColorController
    {
        public List<SAS_DispostivoColor> ColorList(string conection)
        {
            List<SAS_DispostivoColor> list = new List<SAS_DispostivoColor>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_DispostivoColor.ToList();
            }
            return list.OrderBy(x => x.DESCRIPCION).ToList();
        }

        public int ToRegister(string conection, SAS_DispostivoColor item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispostivoColor.Where(x => x.IdDispostivoColor == item.IdDispostivoColor).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion()  
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.SAS_DispostivoColor.ToList().Count > 0 ? Convert.ToInt32(Modelo.SAS_DispostivoColor.ToList().Max(x => x.IdDispostivoColor)) + 1 : 0;
                            #region Nuevo() 
                            SAS_DispostivoColor oregistro = new SAS_DispostivoColor();                            
                            oregistro.IdDispostivoColor = ObtenerUltimoItem.ToString().PadLeft(3, '0');
                            oregistro.DESCRIPCION = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.COLOR = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.ESTADO = item.ESTADO != null ? item.ESTADO.Value : 1;
                            oregistro.SINCRONIZA = item.SINCRONIZA != null ? item.SINCRONIZA.Value : Convert.ToChar("N");
                            oregistro.FECHACREACION = item.FECHACREACION != null ? item.FECHACREACION.Value : (DateTime?)null;
                            oregistro.NOMBRE_CORTO = item.NOMBRE_CORTO != null ? item.NOMBRE_CORTO.Trim() : string.Empty;
                            oregistro.IDCONTROL = item.IDCONTROL != null ? item.IDCONTROL.Trim() : string.Empty;
                            Modelo.SAS_DispostivoColor.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_DispostivoColor oregistro = new SAS_DispostivoColor();
                            oregistro = resultado.Single();
                            oregistro.DESCRIPCION = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.COLOR = item.DESCRIPCION != null ? item.DESCRIPCION.Trim() : string.Empty;
                            oregistro.ESTADO = item.ESTADO != null ? item.ESTADO.Value : 1;
                            oregistro.SINCRONIZA = item.SINCRONIZA != null ? item.SINCRONIZA.Value : Convert.ToChar("N");
                            oregistro.FECHACREACION = item.FECHACREACION != null ? item.FECHACREACION.Value : (DateTime?)null;
                            oregistro.NOMBRE_CORTO = item.NOMBRE_CORTO != null ? item.NOMBRE_CORTO.Trim() : string.Empty;
                            oregistro.IDCONTROL = item.IDCONTROL != null ? item.IDCONTROL.Trim() : string.Empty;
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

        public int ChangeState(string conection, SAS_DispostivoColor item)
        {
            SAS_DispostivoColor oregistro = new SAS_DispostivoColor();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispostivoColor.Where(x => x.IdDispostivoColor == item.IdDispostivoColor).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado() 
                        oregistro = new SAS_DispostivoColor();
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
