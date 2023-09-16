using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_DocumentosAdjuntosAlFormularioController
    {

        public List<SAS_DocumentosAdjuntosAlFormulario> GetListByIdReference(string conection, string formulario, string codigoReferencia)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.idReferencia.Trim() == codigoReferencia && x.formulario.Trim().ToUpper() == formulario.Trim().ToUpper() && x.estado == 1).ToList();
            }
            return listado;
        }


        public List<SAS_DocumentosAdjuntosAlFormulario> GetListById(string conection, string codigo)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id.ToString().Trim() == codigo && x.estado == 1).ToList();
            }
            return listado;
        }


        public int ToRegister(string conection, SAS_DocumentosAdjuntosAlFormulario item)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            int resultadoOperacion = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                #region
                var result = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id == item.id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count == 0)
                    {
                        #region Nuevo Registro()
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        //oItem.id = item.id;
                        oItem.nombre = item.nombre;
                        oItem.ruta = item.ruta;
                        oItem.extension = item.extension;
                        oItem.formulario = item.formulario;
                        oItem.idReferencia = item.idReferencia;
                        oItem.estado = item.estado;
                        oItem.visibleEnReporte = item.visibleEnReporte;
                        oItem.idusuario = item.idusuario;
                        oItem.fecha = item.fecha;
                        Modelo.SAS_DocumentosAdjuntosAlFormulario.InsertOnSubmit(oItem);
                        Modelo.SubmitChanges();
                        resultadoOperacion = 1;
                        #endregion
                    }
                    else
                    {
                        #region Editar()
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        oItem = result.ElementAt(0);

                        //oItem.id = item.id;
                        oItem.nombre = item.nombre;
                        oItem.ruta = item.ruta;
                        oItem.extension = item.extension;
                        //oItem.formulario = item.formulario;
                        //oItem.idReferencia = item.idReferencia;
                        oItem.estado = item.estado;
                        oItem.visibleEnReporte = item.visibleEnReporte;
                        //oItem.idusuario = item.idusuario;
                        //oItem.fecha = item.fecha;
                        Modelo.SubmitChanges();
                        resultadoOperacion = 2;
                        #endregion

                    }
                }
                return resultadoOperacion;
                #endregion

            }
        }


        public int ChangeState(string conection, SAS_DocumentosAdjuntosAlFormulario item)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            int resultadoOperacion = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id == item.id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        #region Cambiar de estado()
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        oItem = result.ElementAt(0);

                        if (oItem.estado == 1)
                        {
                            oItem.estado = 0;
                            resultadoOperacion = 3;
                        }
                        else
                        {
                            oItem.estado = 1;
                            resultadoOperacion = 4;
                        }

                        #endregion
                    }

                }
                return resultadoOperacion;
            }
        }

        public int ChangeState(string conection, int id)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            int resultadoOperacion = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id == id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        oItem = result.ElementAt(0);

                        if (oItem.estado == 1)
                        {
                            oItem.estado = 0;
                            resultadoOperacion = 3;
                            Modelo.SubmitChanges();
                        }
                    }
                }
            }


            return resultadoOperacion;
        }


        public int EnabledInReports(string conection, int id)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            int resultadoOperacion = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id == id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        oItem = result.ElementAt(0);

                        if (oItem.visibleEnReporte == 1)
                        {
                            oItem.visibleEnReporte = 0;
                            resultadoOperacion = 7;
                            Modelo.SubmitChanges();
                        }
                        else
                        {
                            oItem.visibleEnReporte = 1;
                            resultadoOperacion = 8;
                            Modelo.SubmitChanges();
                        }
                    }
                }
            }


            return resultadoOperacion;
        }


        


        public int DeleteRecord(string conection, SAS_DocumentosAdjuntosAlFormulario item)
        {
            List<SAS_DocumentosAdjuntosAlFormulario> listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            int resultadoOperacion = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_DocumentosAdjuntosAlFormulario.Where(x => x.id == item.id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        SAS_DocumentosAdjuntosAlFormulario oItem = new SAS_DocumentosAdjuntosAlFormulario();
                        oItem = result.ElementAt(0);
                        Modelo.SAS_DocumentosAdjuntosAlFormulario.DeleteOnSubmit(oItem);
                        Modelo.SubmitChanges();
                    }
                }
                return resultadoOperacion;
            }
        }


    }
}
