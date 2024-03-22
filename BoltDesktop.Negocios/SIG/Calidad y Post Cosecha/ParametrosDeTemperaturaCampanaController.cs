using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;


namespace Asistencia.Negocios.Calidad
{
    public class ParametrosDeTemperaturaCampanaController
    {


        public List<SAS_ParametrosDeTemperaturaCampanaListAllResult> ListAll(string connection)
        {
            List<SAS_ParametrosDeTemperaturaCampanaListAllResult> listado = new List<SAS_ParametrosDeTemperaturaCampanaListAllResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ParametrosDeTemperaturaCampanaListAll().ToList();

            }
            return listado;
        }

        public int ToRegister(string connection, SAS_ParametrosDeTemperaturaCampana item)
        {
            int resultado = 1;
            List<SAS_ParametrosDeTemperaturaCampana> listado = new List<SAS_ParametrosDeTemperaturaCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ParametrosDeTemperaturaCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_ParametrosDeTemperaturaCampana oItem = new SAS_ParametrosDeTemperaturaCampana();
                    //oItem.Id = item.Id;
                    oItem.EmpresaId = item.EmpresaId;
                    oItem.IdCampana = item.IdCampana;
                    oItem.ParametroInicial = item.ParametroInicial;
                    oItem.ParametroFinal = item.ParametroFinal;
                    oItem.Desde = item.Desde;
                    oItem.Hasta = item.Hasta;
                    oItem.Estado = Convert.ToByte(1);
                    oItem.RegistradoPor = item.RegistradoPor;
                    oItem.Hostname = item.Hostname;
                    oItem.FechaCreacion = item.FechaCreacion;
                    oItem.Glosa = item.Glosa;
                    Modelo.SAS_ParametrosDeTemperaturaCampanas.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ParametrosDeTemperaturaCampana oItem = new SAS_ParametrosDeTemperaturaCampana();
                    oItem = listado.ElementAt(0);
                    oItem.ParametroInicial = item.ParametroInicial;
                    oItem.ParametroFinal = item.ParametroFinal;
                    oItem.Desde = item.Desde;
                    oItem.Hasta = item.Hasta;
                    oItem.Estado = Convert.ToByte(1);
                    oItem.RegistradoPor = item.RegistradoPor;
                    oItem.Hostname = item.Hostname;
                    oItem.FechaCreacion = item.FechaCreacion;
                    oItem.Glosa = item.Glosa;
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatus(string connection, SAS_ParametrosDeTemperaturaCampana item)
        {
            int resultado = 1;
            List<SAS_ParametrosDeTemperaturaCampana> listado = new List<SAS_ParametrosDeTemperaturaCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ParametrosDeTemperaturaCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ParametrosDeTemperaturaCampana oItem = new SAS_ParametrosDeTemperaturaCampana();
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
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToDelete(string connection, SAS_ParametrosDeTemperaturaCampana item)
        {
            int resultado = 1;
            List<SAS_ParametrosDeTemperaturaCampana> listado = new List<SAS_ParametrosDeTemperaturaCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ParametrosDeTemperaturaCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ParametrosDeTemperaturaCampana oItem = new SAS_ParametrosDeTemperaturaCampana();
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_ParametrosDeTemperaturaCampanas.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }



    }
}
