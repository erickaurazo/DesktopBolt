using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{

    public class SAS_TipoDeSistemaController 
    {

        public List<SAS_TipoDeSistema> GetToList(string conection)
        {
            List<SAS_TipoDeSistema> listado = new List<SAS_TipoDeSistema>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listado = Modelo.SAS_TipoDeSistema.ToList();
            }
            return listado.OrderBy(x => x.descripcion).ToList();
        }


        public int ToRegister(string conection, SAS_TipoDeSistema item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_TipoDeSistema.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.SAS_SegmentoRed.ToList().Count > 0 ? Convert.ToInt32(Modelo.SAS_SegmentoRed.ToList().Max(x => x.id)) + 1 : 0;
                            #region Nuevo();
                            SAS_TipoDeSistema oregistro = new SAS_TipoDeSistema();
                            //oregistro.id = ObtenerUltimoItem.ToString().PadLeft(3, '0');
                            oregistro.descripcion = item.descripcion;
                            oregistro.estado = item.estado;
                            oregistro.visibleEnEstado = item.visibleEnEstado;
                            oregistro.tipoDispositivoCodigo = item.tipoDispositivoCodigo;
                            Modelo.SAS_TipoDeSistema.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_TipoDeSistema oregistro = new SAS_TipoDeSistema();
                            oregistro = resultado.Single();
                            oregistro.descripcion = item.descripcion;
                            oregistro.estado = item.estado;
                            oregistro.visibleEnEstado = item.visibleEnEstado;
                            oregistro.tipoDispositivoCodigo = item.tipoDispositivoCodigo;
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

        public int ToChangeState(string conection, SAS_TipoDeSistema networkSegment)
        {
            SAS_TipoDeSistema oregistro = new SAS_TipoDeSistema();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var resultado = Modelo.SAS_TipoDeSistema.Where(x => x.id == networkSegment.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()
                        oregistro = new SAS_TipoDeSistema();
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
