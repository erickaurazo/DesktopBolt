using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class IncumplimientoBuenasPracticasHigieneController
    {

        public List<SAS_ReporteIncumplimientoPracticasHigieneByDateResult> GetListByDate(string conection, string desde, string hasta)
        {
            List<SAS_ReporteIncumplimientoPracticasHigieneByDateResult> listado = new List<SAS_ReporteIncumplimientoPracticasHigieneByDateResult>();            
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ReporteIncumplimientoPracticasHigieneByDate(desde, hasta).ToList();
                //if (list01 != null)
                //{
                //    if (list01.ToList().Count > 0)
                //    {
                //        listadoOrdenado = list01;
                //    }
                //}
            }

            return listado;
        }

        public List<SAS_ReporteIncumplimientoPracticasHigieneByIdResult> GetListById(string conection, int Id)
        {
            List<SAS_ReporteIncumplimientoPracticasHigieneByIdResult> listado = new List<SAS_ReporteIncumplimientoPracticasHigieneByIdResult>();
            List<SAS_ReporteIncumplimientoPracticasHigieneByIdResult> listadoOrdenado = new List<SAS_ReporteIncumplimientoPracticasHigieneByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ReporteIncumplimientoPracticasHigieneById(Id).ToList();
            }

            if (listado != null)
            {
                if (listado.ToList().Count > 0)
                {
                    listadoOrdenado = listado;
                }
            }

            return listadoOrdenado.OrderBy(x => x.trabajador).ToList();
        }


        public int Register(string conection, List<SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE> detail, SAS_CALIDAD_CABECERA_FORMULARIO item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CALIDAD_CABECERA_FORMULARIO.Where(x => x.idCabeceraRegistro == item.idCabeceraRegistro).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_CALIDAD_CABECERA_FORMULARIO oregistro = new SAS_CALIDAD_CABECERA_FORMULARIO();
                            oregistro = resultado.Single();

                            if (oregistro.estado == '1')
                            {
                                #region Editar() 
                                var resultado01 = Modelo.SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE.Where(x => x.idCabeceraRegistro == item.idCabeceraRegistro).ToList();
                                if (resultado01 != null && resultado01.ToList().Count > 0)
                                {
                                    foreach (var itemDetail in resultado01)
                                    {
                                        SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE oDetail = new SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE();
                                        var resultado02 = Modelo.SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE.Where(x => x.idDetalleRegistro == itemDetail.idDetalleRegistro).ToList();



                                        if (resultado02 != null && resultado02.ToList().Count == 1)
                                        {
                                            oDetail = resultado02.ElementAt(0);
                                            //oDetail.idDetalleRegistro = itemDetail.idDetalleRegistro;
                                            //oDetail.idCabeceraRegistro = itemDetail.idCabeceraRegistro;
                                            oDetail.fechaRegistro = itemDetail.fechaRegistro;
                                            oDetail.codigoTrabajador = itemDetail.codigoTrabajador;
                                            oDetail.uniformeCompleto = itemDetail.uniformeCompleto;
                                            oDetail.uniformeLimpio = itemDetail.uniformeLimpio;
                                            oDetail.uniformeIntegro = itemDetail.uniformeIntegro;
                                            oDetail.usoJoyeria = itemDetail.usoJoyeria;
                                            oDetail.manosDesinfectadas = itemDetail.manosDesinfectadas;
                                            oDetail.conBarba = itemDetail.conBarba;
                                            oDetail.conGomaDeMascar = itemDetail.conGomaDeMascar;
                                            oDetail.conPeloRecogido = itemDetail.conPeloRecogido;
                                            oDetail.conPerfume = itemDetail.conPerfume;
                                            oDetail.conUniasCortasSinEsmalte = itemDetail.conUniasCortasSinEsmalte;
                                            oDetail.conMaquillaje = itemDetail.conMaquillaje;
                                            oDetail.accionCorrectiva = itemDetail.accionCorrectiva;
                                            oDetail.vistoBueno = itemDetail.vistoBueno;
                                            Modelo.SubmitChanges();
                                        }
                                        else if (resultado02 != null && resultado02.ToList().Count == 0)
                                        {
                                            oDetail = new SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE();
                                            //oDetail.idDetalleRegistro = itemDetail.idDetalleRegistro;
                                            oDetail.idCabeceraRegistro = itemDetail.idCabeceraRegistro;
                                            oDetail.fechaRegistro = itemDetail.fechaRegistro;
                                            oDetail.codigoTrabajador = itemDetail.codigoTrabajador;
                                            oDetail.uniformeCompleto = itemDetail.uniformeCompleto;
                                            oDetail.uniformeLimpio = itemDetail.uniformeLimpio;
                                            oDetail.uniformeIntegro = itemDetail.uniformeIntegro;
                                            oDetail.usoJoyeria = itemDetail.usoJoyeria;
                                            oDetail.manosDesinfectadas = itemDetail.manosDesinfectadas;
                                            oDetail.conBarba = itemDetail.conBarba;
                                            oDetail.conGomaDeMascar = itemDetail.conGomaDeMascar;
                                            oDetail.conPeloRecogido = itemDetail.conPeloRecogido;
                                            oDetail.conPerfume = itemDetail.conPerfume;
                                            oDetail.conUniasCortasSinEsmalte = itemDetail.conUniasCortasSinEsmalte;
                                            oDetail.conMaquillaje = itemDetail.conMaquillaje;
                                            oDetail.accionCorrectiva = itemDetail.accionCorrectiva;
                                            oDetail.vistoBueno = itemDetail.vistoBueno;
                                            Modelo.SAS_CALIDAD_FORM_INCUMPLIMIENTO_PRACTICAS_HIGIENE.InsertOnSubmit(oDetail);
                                            Modelo.SubmitChanges();
                                        }
                                    }
                                }

                                #endregion
                            }

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


        public int Aprobed(string conection, SAS_CALIDAD_CABECERA_FORMULARIO item)
        {
            //SAS_DispositivoTipoSoftware oregistro = new SAS_DispositivoTipoSoftware();
            int tipoResultadoOperacion = 1; // 1 es registro , 0 es nuevo
            string cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_CALIDAD_CABECERA_FORMULARIO.Where(x => x.idCabeceraRegistro == item.idCabeceraRegistro).ToList();
                    if (resultado != null)
                    {
                        #region Registro | Actualizacion() 
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar()
                            SAS_CALIDAD_CABECERA_FORMULARIO oregistro = new SAS_CALIDAD_CABECERA_FORMULARIO();
                            oregistro = resultado.Single();

                            if (oregistro.estado == '1')
                            {
                                oregistro.estado = '2';
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
