using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class ParaderosController
    {
        string cnx = string.Empty;

        public string Grabar(string conection, SJ_Paraderos Whereabout)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SJ_Paraderos> resultadoConsulta = new List<SJ_Paraderos>();
            SJ_Paraderos oSJ_Paraderos = new SJ_Paraderos();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.SJ_Paraderos.Where(x => x.IdParadero == Whereabout.IdParadero).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.SJ_Paraderos.Max(x => x.IdParadero)) + 1;
                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Editar()
                    oSJ_Paraderos = resultadoConsulta.Single();
                    // oSJ_Paraderos.IdEmpresa = paradero.IdEmpresa;
                    //oSJ_Paraderos.IdParadero = paradero.IdParadero;
                    oSJ_Paraderos.DescripcionParadero = Whereabout.DescripcionParadero;
                    oSJ_Paraderos.Observacion = Whereabout.Observacion;
                    //oSJ_Paraderos.ESTADO = paradero.ESTADO;
                    oSJ_Paraderos.tipo = Whereabout.tipo;


                    modelo.SubmitChanges();
                    resultado = "Transacción satisfactorio";
                    #endregion
                }
                else if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 0)
                {
                    #region Registrar()
                    oSJ_Paraderos = new SJ_Paraderos();
                    oSJ_Paraderos.IdEmpresa = Whereabout.IdEmpresa;
                    oSJ_Paraderos.IdParadero = nuevoCodigo.ToString().PadLeft(3, '0');
                    oSJ_Paraderos.DescripcionParadero = Whereabout.DescripcionParadero;
                    oSJ_Paraderos.Observacion = Whereabout.Observacion;
                    oSJ_Paraderos.ESTADO = Whereabout.ESTADO;
                    modelo.SJ_Paraderos.InsertOnSubmit(oSJ_Paraderos);
                    modelo.SubmitChanges();
                    resultado = "Transacción satisfactorio";
                    #endregion
                }
            }
            return resultado;
        }

        public string Anular(string conection, SJ_Paraderos Whereabout)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SJ_Paraderos> resultadoConsulta = new List<SJ_Paraderos>();
            SJ_Paraderos oSJ_Paraderos = new SJ_Paraderos();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.SJ_Paraderos.Where(x => x.IdParadero == Whereabout.IdParadero && x.IdEmpresa == Whereabout.IdEmpresa).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.SJ_Paraderos.Max(x => x.IdParadero)) + 1;

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Activar o Anular()
                    oSJ_Paraderos = resultadoConsulta.Single();


                    if (oSJ_Paraderos.ESTADO == 1)
                    {
                        oSJ_Paraderos.ESTADO = 0;
                        resultado = "Registro Anulado";
                    }
                    else if (oSJ_Paraderos.ESTADO == 0)
                    {
                        oSJ_Paraderos.ESTADO = 1;
                        resultado = "Registro Activado";
                    }

                    modelo.SubmitChanges();

                    #endregion
                }

            }

            return resultado;
        }

        public string Eliminar(string conection, SJ_Paraderos Whereabout)
        {
            string resultado = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SJ_Paraderos> resultadoConsulta = new List<SJ_Paraderos>();
            SJ_Paraderos oSJ_Paraderos = new SJ_Paraderos();

            using (BDAsistenciaDataContext modelo = new BDAsistenciaDataContext(cnx))
            {
                modelo.CommandTimeout = 9999900;
                resultadoConsulta = modelo.SJ_Paraderos.Where(x => x.IdParadero == Whereabout.IdParadero && x.IdEmpresa == Whereabout.IdEmpresa).ToList();
                int nuevoCodigo = Convert.ToInt32(modelo.SJ_Paraderos.Max(x => x.IdParadero)) + 1;

                if (resultadoConsulta != null && resultadoConsulta.ToList().Count == 1)
                {
                    #region Activar o Anular()
                    oSJ_Paraderos = resultadoConsulta.Single();


                    if (oSJ_Paraderos.ESTADO == 1)
                    {
                        modelo.SJ_Paraderos.DeleteOnSubmit(oSJ_Paraderos);
                        resultado = "Registro eliminado";
                    }
                    else if (oSJ_Paraderos.ESTADO == 0)
                    {
                        oSJ_Paraderos.ESTADO = 1;
                        resultado = "Registro Activado";
                    }

                    modelo.SubmitChanges();

                    #endregion
                }

            }

            return resultado;
        }

        public List<SJ_Paraderos> ListarTodos(string conection)
        {
            List<SJ_Paraderos> listado = new List<SJ_Paraderos>();

            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.SJ_Paraderos.ToList();
            }

            return listado;
        }


    }
}
