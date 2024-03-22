using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class SAS_AccionesCorrectivasRegistroController
    {


        public List<SAS_ListadoAccionesCorrectivasAll> ListAlll(string connection)
        {
           

            List<SAS_ListadoAccionesCorrectivasAll> listado = new List<SAS_ListadoAccionesCorrectivasAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoAccionesCorrectivasAll.OrderBy(x=> x.Formulario).OrderBy(x=> x.AccionCorrectiva).ToList();
                
            }
            return listado;
        }



        public  int ToRegister(string connection, SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO item )
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO> listado = new List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO.Where(x => x.idAccion == item.idAccion).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO oItem = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                    //oItem.idAccion = item.idAccion;
                    oItem.descripcion = item.descripcion;
                    oItem.idRegistro = item.idRegistro;
                    oItem.Visible= item.Visible;
                    oItem.Estado = Convert.ToByte(1);
                    Modelo.SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idAccion;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO oItem = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.idRegistro = item.idRegistro;
                    oItem.Visible = item.Visible;
                    //oItem.Estado = item.Estado;                    
                    Modelo.SubmitChanges();
                    resultado = oItem.idAccion;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatus(string connection, SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO> listado = new List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO.Where(x => x.idAccion == item.idAccion).ToList();

               if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO oItem = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);

                    if (oItem.Estado == 1)
                    {
                        oItem.Estado = 0;
                    }
                    else
                    {
                        oItem.Estado = 1;
                    }
                                     
                    Modelo.SubmitChanges();
                    resultado = oItem.idAccion;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToDelete(string connection, SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO> listado = new List<SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO.Where(x => x.idAccion == item.idAccion).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO oItem = new SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO();                    
                    oItem = listado.ElementAt(0);                    
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_ACCION_CORRECTIVA_REGISTRO.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idAccion;
                    #endregion
                }

            }
            return resultado;
        }



    }
}
