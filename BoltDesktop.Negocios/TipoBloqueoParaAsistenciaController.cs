using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class TipoBloqueoParaAsistenciaController
    {

        string cnx = string.Empty;
        public string Add(string conection, ASJ_PersonalTipoBloqueo person)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<ASJ_PersonalTipoBloqueo> resultadoConsulta = new List<ASJ_PersonalTipoBloqueo>();
            ASJ_PersonalTipoBloqueo oTipoBloqueo = new ASJ_PersonalTipoBloqueo();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.ASJ_PersonalTipoBloqueo.Where(x => x.codigoPersonalTipoBloqueo == person.codigoPersonalTipoBloqueo).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.ASJ_PersonalTipoBloqueo.Max(x => x.codigoPersonalTipoBloqueo)) + 1;

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Editar()
                    oTipoBloqueo = resultadoConsulta.Single();
                    oTipoBloqueo.descripcion = person.descripcion != null ? person.descripcion.Trim() : string.Empty;
                    oTipoBloqueo.color = person.color != null ? person.color.Trim() : string.Empty;
                    modelo.SubmitChanges();
                    resultado = "Transacción satisfactorio";
                    #endregion
                }
                else if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 0)
                {
                    #region Registrar()
                    oTipoBloqueo.codigoPersonalTipoBloqueo = nuevoCodigo.ToString().PadLeft(3, '0');
                    oTipoBloqueo.descripcion = person.descripcion != null ? person.descripcion.Trim() : string.Empty;
                    oTipoBloqueo.color = person.color != null ? person.color.Trim() : string.Empty;
                    oTipoBloqueo.estado = 1;
                    modelo.ASJ_PersonalTipoBloqueo.InsertOnSubmit(oTipoBloqueo);
                    modelo.SubmitChanges();
                    resultado = "Transacción satisfactorio";
                    #endregion
                }
            }

            return resultado;
        }

        public string AddList(string conection, List<ASJ_PersonalTipoBloqueo> persons)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<ASJ_PersonalTipoBloqueo> resultadoConsulta = new List<ASJ_PersonalTipoBloqueo>();
            ASJ_PersonalTipoBloqueo oTipoBloqueo = new ASJ_PersonalTipoBloqueo();






            return resultado;
        }

        public string ChangeState(string conection, ASJ_PersonalTipoBloqueo person)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<ASJ_PersonalTipoBloqueo> resultadoConsulta = new List<ASJ_PersonalTipoBloqueo>();
            ASJ_PersonalTipoBloqueo oTipoBloqueo = new ASJ_PersonalTipoBloqueo();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.ASJ_PersonalTipoBloqueo.Where(x => x.codigoPersonalTipoBloqueo == person.codigoPersonalTipoBloqueo).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.ASJ_PersonalTipoBloqueo.Max(x => x.codigoPersonalTipoBloqueo)) + 1;

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Activar o Anular()
                    oTipoBloqueo = resultadoConsulta.Single();


                    if (oTipoBloqueo.estado == 1)
                    {
                        oTipoBloqueo.estado = 0;
                        resultado = "Registro Anulado";
                    }
                    else if (oTipoBloqueo.estado == 0)
                    {
                        oTipoBloqueo.estado = 1;
                        resultado = "Registro Activado";
                    }

                    modelo.SubmitChanges();

                    #endregion
                }

            }

            return resultado;
        }

        public string Delete(string conection, ASJ_PersonalTipoBloqueo person)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<ASJ_PersonalTipoBloqueo> resultadoConsulta = new List<ASJ_PersonalTipoBloqueo>();
            ASJ_PersonalTipoBloqueo oTipoBloqueo = new ASJ_PersonalTipoBloqueo();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.ASJ_PersonalTipoBloqueo.Where(x => x.codigoPersonalTipoBloqueo == person.codigoPersonalTipoBloqueo).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.ASJ_PersonalTipoBloqueo.Max(x => x.codigoPersonalTipoBloqueo)) + 1;

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Activar o Anular()
                    oTipoBloqueo = resultadoConsulta.Single();


                    if (oTipoBloqueo.estado == 1)
                    {
                        modelo.ASJ_PersonalTipoBloqueo.DeleteOnSubmit(oTipoBloqueo);
                        resultado = "Registro Eliminado";
                    }
                    else if (oTipoBloqueo.estado == 0)
                    {
                        resultado = "No se puede eliminar registro anulado";
                    }

                    modelo.SubmitChanges();

                    #endregion
                }

            }

            return resultado;
        }

        public List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult> GetTypeLock(string conection)
        {
            List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult> listado = new List<ASJ_ObtenerListadoDeTipoPersonalbloqueadoResult>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.ASJ_ObtenerListadoDeTipoPersonalbloqueado().ToList();
            }

            return listado;
        }


    }
}
