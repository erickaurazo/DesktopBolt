using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class WhereaboutsController
    {
        private Grupo oTipoParadero;

        public List<Paradero> ObtenerListaParaderos(string conection)
        {
            List<Paradero> listado = new List<Paradero>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999;

                var resultadoConsulta = Modelo.SJ_Paraderos.ToList().OrderBy(x => x.IdParadero).ToList();

                listado = (from item in resultadoConsulta
                           where item.IdParadero != null
                           group item by new { item.IdParadero } into j
                           select new Paradero
                           {
                               idParadero = j.Key.IdParadero,
                               descripcionParadero = j.FirstOrDefault().DescripcionParadero != null ? j.FirstOrDefault().DescripcionParadero.Trim() : string.Empty,
                               observacion = j.FirstOrDefault().Observacion != null ? j.FirstOrDefault().Observacion.Trim() : string.Empty,
                               estado = j.FirstOrDefault().ESTADO != null ? Convert.ToInt32(j.FirstOrDefault().ESTADO.Value) : 0,
                               tipo = j.FirstOrDefault().tipo != null ? j.FirstOrDefault().tipo.ToString().Trim() : string.Empty,
                               estadoDescripcion = ((j.FirstOrDefault().ESTADO != null ? Convert.ToInt32(j.FirstOrDefault().ESTADO.Value) : 0) == 1) ? "ACTIVO" : "ANULADO",
                               tipoDescripcion = (j.FirstOrDefault().tipo != null ? j.FirstOrDefault().tipo.ToString().Trim() : string.Empty) == "T" ? "TRANSPORTE" : "PENSION",
                           }).ToList();
                    
                    
                Modelo.Connection.Close();
            }

            return listado;
        }

        public SJ_Paraderos ObtenerParadero(string conection, SJ_Paraderos whereabout)
        {
            SJ_Paraderos oParadero = new SJ_Paraderos();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999;
                var listado = Modelo.SJ_Paraderos.Where(x => x.IdParadero.ToString().Trim() == whereabout.IdParadero.ToString().Trim()).ToList().OrderBy(x => x.IdParadero).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    oParadero = listado.Single();
                }
                Modelo.Connection.Close();
            }
            return oParadero;
        }

        public void Registrar(string conection, SJ_Paraderos whereabout)
        {
            SJ_Paraderos oParadero = new SJ_Paraderos();
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJ_Paraderos.Where(x => x.IdParadero == whereabout.IdParadero).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar
                    oParadero = resultadoSubConsulta.Single();
                    //oParadero.IdParadero = oSJ_Paradero.IdParadero.ToString().Trim();
                    oParadero.DescripcionParadero = whereabout.DescripcionParadero != null ? whereabout.DescripcionParadero.ToString().Trim() : string.Empty;
                    oParadero.Observacion = whereabout.Observacion != null ? whereabout.Observacion.ToString().Trim() : string.Empty;
                    oParadero.tipo = whereabout.tipo; 

                    //oParadero.ESTADO = oSJ_Paradero.ESTADO;
                    Modelo.SubmitChanges();
                    #endregion
                }
                else
                {
                    if (whereabout.IdParadero == "")
                    {
                        #region Registro()
                        oParadero.IdParadero = ObtenerNuevoCodigo(conection);
                        oParadero.DescripcionParadero = whereabout.DescripcionParadero != null ? whereabout.DescripcionParadero.ToString().Trim() : "";
                        oParadero.Observacion = whereabout.Observacion != null ? whereabout.Observacion.ToString().Trim() : "";
                        oParadero.ESTADO = whereabout.ESTADO;
                        oParadero.tipo = whereabout.tipo; 
                        Modelo.SJ_Paraderos.InsertOnSubmit(oParadero);
                        Modelo.SubmitChanges();

                        #endregion
                    }
                }
                Modelo.Connection.Close();
                #endregion
            }
            #endregion
            //    Scope.Complete();
            //}

        }

        private string ObtenerNuevoCodigo(string conection)
        {
            string cnx = string.Empty;
            string codigo = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999;
                string resultado = Modelo.SJ_Paraderos.ToList().OrderByDescending(x => x.IdParadero).ElementAt(0).IdParadero.Substring(1, 3);
                int nuevoCodigo = Convert.ToInt32(resultado) + 1;
                codigo = "P" + nuevoCodigo.ToString().PadLeft(3, '0');
                Modelo.Connection.Close();
            }

            return codigo;
        }

        public void Eliminar(string conection, Paradero whereabout)
        {
            SJ_Paraderos oParadero = new SJ_Paraderos();
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJ_Paraderos.Where(x => x.IdParadero == whereabout.idParadero).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar

                    oParadero = resultadoSubConsulta.Single();
                    Modelo.SJ_Paraderos.DeleteOnSubmit(oParadero);
                    Modelo.SubmitChanges();
                    #endregion
                }

                Modelo.Connection.Close();
                #endregion
            }
            #endregion
            //    Scope.Complete();
            //}

        }

        /* Se puede anular o activar el paradero */
        public void CambiarEstadoDocumento(string conection, Paradero whereabout)
        {
            SJ_Paraderos oParadero = new SJ_Paraderos();
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJ_Paraderos.Where(x => x.IdParadero == whereabout.idParadero).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar

                    oParadero = resultadoSubConsulta.Single();
                    if (oParadero.ESTADO == 1)
                    {
                        oParadero.ESTADO = 0;
                    }
                    else
                    {
                        oParadero.ESTADO = 1;
                    }
                    Modelo.SubmitChanges();
                    #endregion
                }

                Modelo.Connection.Close();
                #endregion
            }
            #endregion
            //    Scope.Complete();
            //}


        }

        public List<Grupo> ObtenerListadoTipoParaderos()
        {
            List<Grupo> listadoTipoParaderos = new List<Grupo>();

            oTipoParadero = new Grupo();
            oTipoParadero.Codigo = "P";
            oTipoParadero.Descripcion = "PRINCIPAL";
            listadoTipoParaderos.Add(oTipoParadero);

            oTipoParadero = new Grupo();
            oTipoParadero.Codigo = "M";
            oTipoParadero.Descripcion = "INTERMEDIO";
            listadoTipoParaderos.Add(oTipoParadero);


            oTipoParadero = new Grupo();
            oTipoParadero.Codigo = string.Empty;
            oTipoParadero.Descripcion = "SIN ESPECIFICAR";
            listadoTipoParaderos.Add(oTipoParadero);

            return listadoTipoParaderos.OrderBy(x => x.Codigo).ToList();
        }
    }
}
