using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;
namespace Asistencia.Negocios
{
    public class SAS_AreasAInspeccionarController
    {

        public List<SAS_ListadoAreasAInspeccionAll> ListAll(string connection)
        {
            List<SAS_ListadoAreasAInspeccionAll> listado = new List<SAS_ListadoAreasAInspeccionAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoAreasAInspeccionAll.OrderBy(x => x.AreaInspeccion).OrderBy(x=> x.Formulario).ToList();

            }
            return listado;
        }

        public int ToRegister(string connection, SAS_CALIDAD_AREA_INSPECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AREA_INSPECCION> listado = new List<SAS_CALIDAD_AREA_INSPECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AREA_INSPECCION.Where(x => x.idArea == item.idArea).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_AREA_INSPECCION oItem = new SAS_CALIDAD_AREA_INSPECCION();
                    //oItem.idArea = item.idArea;
                    oItem.descripcion = item.descripcion;
                    oItem.idFormulario = item.idFormulario;
                    oItem.estado = item.estado;
                    oItem.orden = item.orden;
                    oItem.seccion = item.seccion;
                    oItem.visibleEnRpt = item.visibleEnRpt;
                    Modelo.SAS_CALIDAD_AREA_INSPECCION.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idArea;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_AREA_INSPECCION oItem = new SAS_CALIDAD_AREA_INSPECCION();
                    oItem = listado.ElementAt(0);
                    //oItem.idAccion = item.idAccion;
                    oItem.descripcion = item.descripcion;
                    //oItem.idFormulario = item.idFormulario;
                    //oItem.estado = item.estado;
                    oItem.orden = item.orden;
                    oItem.seccion = item.seccion;
                    oItem.visibleEnRpt = item.visibleEnRpt;
                    Modelo.SubmitChanges();
                    resultado = oItem.idArea;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatus(string connection, SAS_CALIDAD_AREA_INSPECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AREA_INSPECCION> listado = new List<SAS_CALIDAD_AREA_INSPECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AREA_INSPECCION.Where(x => x.idArea == item.idArea).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region ToChangeStatus()
                    SAS_CALIDAD_AREA_INSPECCION oItem = new SAS_CALIDAD_AREA_INSPECCION();
                    //oItem.Id = item.Id;
                    oItem = listado.ElementAt(0);
                    if (oItem.estado == '1')
                    {
                        oItem.estado = '0';
                    }
                    else
                    {
                        oItem.estado = '1';
                    }

                    Modelo.SubmitChanges();
                    resultado = oItem.idArea;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToDelete(string connection, SAS_CALIDAD_AREA_INSPECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AREA_INSPECCION> listado = new List<SAS_CALIDAD_AREA_INSPECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AREA_INSPECCION.Where(x => x.idArea == item.idArea).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_AREA_INSPECCION oItem = new SAS_CALIDAD_AREA_INSPECCION();
                    oItem = listado.ElementAt(0);                    
                    Modelo.SAS_CALIDAD_AREA_INSPECCION.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idArea;
                    #endregion
                }
            }
            return resultado;
        }


    }
}
