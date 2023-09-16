using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DispositivoContadoresController
    {
        // get listing by code | Obtener listado por codigo
        public List<SAS_DispositivoaccountantsByDeviceIDResult> GetListingByCode(string conection, int codigo)
        {
            List<SAS_DispositivoaccountantsByDeviceIDResult> listado = new List<SAS_DispositivoaccountantsByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoaccountantsByDeviceID(codigo).ToList();
            }
            return listado;
        }

        public List<SAS_DispositivoaccountantsByDeviceIDResult> GetListingByCode(string conection, SAS_Dispostivo device)
        {
            List<SAS_DispositivoaccountantsByDeviceIDResult> listado = new List<SAS_DispositivoaccountantsByDeviceIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DispositivoaccountantsByDeviceID(device.id).ToList();
            }
            return listado;
        }


        public int Register(string conection, SAS_DispositivoContadores item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            SAS_DispositivoContadores oItem = new SAS_DispositivoContadores();
            oItem = item;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 0)
                        {
                            #region Nuevo();

                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                            recordObject.codigoDispositivo = item.codigoDispositivo;
                            recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                            recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                            recordObject.estado= item.estado != (decimal?)null ? item.estado : 0;
                            recordObject.seVisualizaEnReportes= item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value :0;
                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;


                            Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
                            Modelo.SubmitChanges();
                            tipoResultadoOperacion = 0; // registrar
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                            recordObject = resultado.Single();
                            //recordObject.codigoDispositivo = item.codigoDispositivo;
                            //recordObject.item = item.item != string.Empty ? item.item : string.Empty;
                            recordObject.periodo = item.periodo != string.Empty ? item.periodo : string.Empty;
                            recordObject.cantidad = item.cantidad != (decimal?)null ? item.cantidad : 0;
                            recordObject.IdMedida = item.IdMedida != string.Empty ? item.IdMedida : string.Empty;
                            recordObject.observacion = item.observacion != string.Empty ? item.observacion : string.Empty;
                            recordObject.desde = item.desde != (DateTime?)null ? item.desde.Value : (DateTime?)null;
                            recordObject.hasta = item.hasta != (DateTime?)null ? item.hasta.Value : (DateTime?)null;
                            recordObject.estado = item.estado != (decimal?)null ? item.estado : 0;
                            recordObject.seVisualizaEnReportes = item.seVisualizaEnReportes != (decimal?)null ? item.seVisualizaEnReportes : 0;
                            recordObject.usuario = item.usuario != string.Empty ? item.usuario : string.Empty;
                            recordObject.contadorInicial = item.contadorInicial != (decimal?)null ? item.contadorInicial.Value : 0;
                            recordObject.contadorFinal = item.contadorFinal != (decimal?)null ? item.contadorFinal.Value : 0;
                            recordObject.itemContrato = item.itemContrato != string.Empty ? item.itemContrato : string.Empty;
                            //Modelo.SAS_DispositivoContadores.InsertOnSubmit(recordObject);
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

        public int ChangeState(string conection, SAS_DispositivoContadores item)
        {

            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_DispositivoContadores.Where(x => x.codigoDispositivo == item.codigoDispositivo && x.item == item.item).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_DispositivoContadores recordObject = new SAS_DispositivoContadores();
                            recordObject = resultado.Single();

                            if (recordObject.estado == 1)
                            {
                                recordObject.estado = 0;
                            }
                            else
                            {
                                recordObject.estado = 0;
                            }

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


    }
}
