using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class SAS_TipoMaestroCalidadController
    {

        #region Grupo()
        public List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo> ListAll(string connection)
        {


            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.OrderBy(x => x.Descripcion).ToList();

            }
            return listado;
        }

        public int ToRegister(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo();
                    //oItem.Id = item.Id;
                    oItem.Descripcion = item.Descripcion;
                    oItem.Estado = Convert.ToByte(1);
                    Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);
                    oItem.Descripcion = item.Descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatus(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo();
                    //oItem.Id = item.Id;
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


        public int ToDelete(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo();
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestrosTipo.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }
            }
            return resultado;
        }


        #endregion

        #region Detalle()
        public List<SAS_ListadoGrupoTipoDetalleEvaluacion> ListDetailAll(string connection)
        {


            List<SAS_ListadoGrupoTipoDetalleEvaluacion> listado = new List<SAS_ListadoGrupoTipoDetalleEvaluacion>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoGrupoTipoDetalleEvaluacion.OrderBy(x => x.GrupoTipo).OrderBy(x => x.ItemDetalle).ToList();

            }
            return listado;
        }

        public int ToRegisterDetail(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros();
                    //oItem.Id = item.Id;
                    oItem.IdEmpresa = item.IdEmpresa;
                    oItem.Abreviatura = item.Abreviatura;
                    oItem.Descripcion = item.Descripcion;
                    oItem.IdTipo = item.IdTipo;
                    oItem.VisibleEnReportes = item.VisibleEnReportes;
                    oItem.Orden = item.Orden;
                    oItem.Estado = Convert.ToByte(1);
                    Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = Convert.ToInt32(oItem.Id);
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);
                    oItem.Abreviatura = item.Abreviatura;
                    oItem.IdEmpresa = item.IdEmpresa;
                    oItem.Descripcion = item.Descripcion;
                    oItem.IdTipo = item.IdTipo;
                    oItem.VisibleEnReportes = item.VisibleEnReportes;
                    oItem.Orden = item.Orden;
                    Modelo.SubmitChanges();
                    resultado = Convert.ToInt32(oItem.Id);
                    #endregion
                }

            }
            return resultado;
        }


        public int ToChangeStatusDetail(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros();
                    //oItem.Id = item.Id;
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
                    resultado = Convert.ToInt32(oItem.Id);
                    #endregion
                }

            }
            return resultado;
        }


        public int ToDeleteDetail(string connection, SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros item)
        {
            int resultado = 1;
            List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros> listado = new List<SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros oItem = new SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros();
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_Calidad_Form_EvaluacionTrazabilidadFCLDespachoDetalleMaestros.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = Convert.ToInt32(oItem.Id);
                    #endregion
                }
            }
            return resultado;
        }


        #endregion


    }
}
