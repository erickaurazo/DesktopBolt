using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_SegmentoRedController
    {

        public List<SAS_SegmentoRed> GetNetworkSegment(string conection)
        {
            List<SAS_SegmentoRed> listado = new List<SAS_SegmentoRed>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_SegmentoRed.ToList();
            }
            return listado.OrderBy(x => x.descripcion).ToList();
        }


        public int Register(string conection, SAS_SegmentoRed item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_SegmentoRed.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            int ObtenerUltimoItem = Modelo.SAS_SegmentoRed.ToList().Count > 0 ? Convert.ToInt32(Modelo.SAS_SegmentoRed.ToList().Max(x => x.id)) + 1 : 0;
                            #region Nuevo();
                            SAS_SegmentoRed oregistro = new SAS_SegmentoRed();
                            oregistro.id = ObtenerUltimoItem.ToString().PadLeft(3, '0');
                            oregistro.descripcion = item.descripcion;
                            oregistro.segmento = item.segmento;
                            oregistro.mascaraSubRed = item.mascaraSubRed;
                            oregistro.puertaEnlace = item.puertaEnlace;
                            oregistro.estado = item.estado;
                            oregistro.esSedeTrabajo = item.esSedeTrabajo;



                            Modelo.SAS_SegmentoRed.InsertOnSubmit(oregistro);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_SegmentoRed oregistro = new SAS_SegmentoRed();
                            oregistro = resultado.Single();
                            oregistro.descripcion = item.descripcion;
                            oregistro.segmento = item.segmento;
                            oregistro.mascaraSubRed = item.mascaraSubRed;
                            oregistro.puertaEnlace = item.puertaEnlace;
                            oregistro.estado = item.estado;
                            oregistro.esSedeTrabajo = item.esSedeTrabajo;

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

        public int ChangeState(string conection, SAS_SegmentoRed networkSegment)
        {
            SAS_SegmentoRed oregistro = new SAS_SegmentoRed();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_SegmentoRed.Where(x => x.id == networkSegment.id).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()
                        oregistro = new SAS_SegmentoRed();
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
