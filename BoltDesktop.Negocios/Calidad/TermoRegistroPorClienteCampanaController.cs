using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;



namespace Asistencia.Negocios.Calidad
{
  public  class TermoRegistroPorClienteCampanaController
    {

        public List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult> ListAll(string connection)
        {
            List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult> listado = new List<SAS_ListadoTermoRegistroPorClienteCampanaAllResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoTermoRegistroPorClienteCampanaAll().ToList();

            }
            return listado;
        }

        public int ToRegister(string connection, SAS_TermoRegistroPorClienteCampana item)
        {
            int resultado = 1;
            List<SAS_TermoRegistroPorClienteCampana> listado = new List<SAS_TermoRegistroPorClienteCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_TermoRegistroPorClienteCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_TermoRegistroPorClienteCampana oItem = new SAS_TermoRegistroPorClienteCampana();
                    //oItem.Id = item.Id;
                    oItem.EmpresaId = item.EmpresaId;
                    oItem.CampanaId = item.CampanaId;
                    oItem.ClienteId = item.ClienteId;
                    oItem.TipoTermoRegistro = item.TipoTermoRegistro;                    
                    oItem.Estado = Convert.ToByte(1);
                    oItem.VisibleEnReportes = item.VisibleEnReportes;
                    oItem.FechaCreacion = item.FechaCreacion;
                    oItem.HostName = item.HostName;
                    oItem.Usuario = item.Usuario;
                    Modelo.SAS_TermoRegistroPorClienteCampanas.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_TermoRegistroPorClienteCampana oItem = new SAS_TermoRegistroPorClienteCampana();
                    //oItem.EmpresaId = item.EmpresaId;
                    //oItem.CampanaId = item.CampanaId;
                    //oItem.ClienteId = item.ClienteId;
                    oItem.TipoTermoRegistro = item.TipoTermoRegistro;
                    oItem.Estado = Convert.ToByte(1);
                    oItem.VisibleEnReportes = item.VisibleEnReportes;
                    oItem.FechaCreacion = item.FechaCreacion;
                    oItem.HostName = item.HostName;
                    oItem.Usuario = item.Usuario;
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatus(string connection, SAS_TermoRegistroPorClienteCampana item)
        {
            int resultado = 1;
            List<SAS_TermoRegistroPorClienteCampana> listado = new List<SAS_TermoRegistroPorClienteCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_TermoRegistroPorClienteCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_TermoRegistroPorClienteCampana oItem = new SAS_TermoRegistroPorClienteCampana();
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


        public int ToDelete(string connection, SAS_TermoRegistroPorClienteCampana item)
        {
            int resultado = 1;
            List<SAS_TermoRegistroPorClienteCampana> listado = new List<SAS_TermoRegistroPorClienteCampana>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_TermoRegistroPorClienteCampanas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_TermoRegistroPorClienteCampana oItem = new SAS_TermoRegistroPorClienteCampana();
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_TermoRegistroPorClienteCampanas.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }
        

    }
}
