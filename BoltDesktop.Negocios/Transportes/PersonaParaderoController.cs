using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class PersonaParaderoController
    {

        
        public List<SJM_PersonaParadero> GetListPersonalBywhereabouts(string conection)
        {
            List<SJM_PersonaParadero> listado = new List<SJM_PersonaParadero>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999;

                var listaPersonalParadero = Modelo.SJM_PersonaParadero.OrderBy(x => x.Id).ToList().ToList();
                var listaParaderos = Modelo.SJ_Paraderos.ToList();

                var resultado =
                    from per in listaPersonalParadero
                    join par in listaParaderos
                    on per.IdParadero.ToString().Trim() equals par.IdParadero.ToString().Trim()
                    select new SJM_PersonaParadero
                    {
                        Id = per.Id,
                        idCodigoPersonalGeneral = per.idCodigoPersonalGeneral != null ? per.idCodigoPersonalGeneral.ToString().Trim() : "",
                        dniTrabajador = per.dniTrabajador != null ? per.dniTrabajador.ToString().Trim() : "",
                        NombresTrabajador = per.NombresTrabajador.ToString().Trim(),
                        Fecha = per.Fecha.Value,
                        FechaTransferencia = per.FechaTransferencia != null ? per.FechaTransferencia.Value : DateTime.Now,
                        IdParadero = per.IdParadero.ToString().Trim() + " - " + par.DescripcionParadero.ToString().Trim(),
                        direccion = per.direccion != null ? per.direccion.ToString().Trim() : "",
                        paradero = per.paradero != null ? per.paradero.ToString().Trim() : "",
                        estado = per.estado,
                        contacto = per.contacto != null ? per.contacto.Trim() : "",
                        nroContacto = per.nroContacto != null ? per.nroContacto.Trim() : "",
                    };

                foreach (var item in resultado)
                {

                    try
                    {
                        listado.Add(item);
                    }
                    catch (Exception Ex)
                    {
                        string mensage = item.idCodigoPersonalGeneral.Trim();
                        throw Ex;
                    }

                }

                //listado = innerJoinQuery.ToList();

                Modelo.Connection.Close();
            }
            return listado;
        }

        public SJM_PersonaParadero GetPersonBywhereabouts(string conection, SJM_PersonaParadero person)
        {
            SJM_PersonaParadero oParaderoPersona = new SJM_PersonaParadero();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 99999;
                var listado = Modelo.SJM_PersonaParadero.Where(x => x.IdParadero.ToString().Trim() == person.IdParadero.ToString().Trim()).ToList().OrderBy(x => x.IdParadero).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    oParaderoPersona = listado.Single();
                }
                Modelo.Connection.Close();
            }
            return oParaderoPersona;
        }

        public void Add(string conection, SJM_PersonaParadero person)
        {
            SJM_PersonaParadero oPersonaParadero = new SJM_PersonaParadero();
            //using (TransactionScope Scope = new TransactionScope())
            //{
            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJM_PersonaParadero.Where(x => x.Id == person.Id).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar
                    oPersonaParadero = resultadoSubConsulta.Single();
                    //oParadero.Id = oSJ_PersonaParadero.Id;
                    oPersonaParadero.idCodigoPersonalGeneral = person.idCodigoPersonalGeneral != null ? person.idCodigoPersonalGeneral.ToString().Trim() : "";
                    oPersonaParadero.dniTrabajador = person.dniTrabajador != null ? person.dniTrabajador.ToString().Trim() : "";
                    oPersonaParadero.NombresTrabajador = person.NombresTrabajador != null ? person.NombresTrabajador.ToString().Trim() : "";
                    //oParadero.Fecha = oSJ_PersonaParadero.Fecha;
                    //oParadero.FechaTransferencia = oSJ_PersonaParadero.FechaTransferencia;
                    oPersonaParadero.IdParadero = person.IdParadero != null ? person.IdParadero.ToString().Trim() : "";
                    oPersonaParadero.paradero = person.paradero != null ? person.paradero.ToString().Trim() : "";
                    oPersonaParadero.direccion = person.direccion != null ? person.direccion.ToString().Trim() : "";
                    oPersonaParadero.contacto = person.contacto != null ? person.contacto.ToString().Trim() : "";
                    oPersonaParadero.nroContacto = person.nroContacto != null ? person.nroContacto.ToString().Trim() : "";
                    //oParadero.estado = oSJ_PersonaParadero.estado;
                    Modelo.SubmitChanges();
                    #endregion
                }
                else
                {
                    if (person.Id == 0)
                    {
                        #region Registro()
                        //oParadero.Id = oSJ_PersonaParadero.Id;
                        oPersonaParadero.idCodigoPersonalGeneral = person.idCodigoPersonalGeneral != null ? person.idCodigoPersonalGeneral.ToString().Trim() : "";
                        oPersonaParadero.dniTrabajador = person.dniTrabajador != null ? person.dniTrabajador.ToString().Trim() : "";
                        oPersonaParadero.NombresTrabajador = person.NombresTrabajador != null ? person.NombresTrabajador.ToString().Trim() : "";
                        oPersonaParadero.Fecha = person.Fecha;
                        oPersonaParadero.FechaTransferencia = person.FechaTransferencia;
                        oPersonaParadero.IdParadero = person.IdParadero != null ? person.IdParadero.ToString().Trim() : "";
                        oPersonaParadero.estado = person.estado;
                        oPersonaParadero.paradero = person.paradero != null ? person.paradero.ToString().Trim() : "";
                        oPersonaParadero.direccion = person.direccion != null ? person.direccion.ToString().Trim() : "";
                        oPersonaParadero.contacto = person.contacto != null ? person.contacto.ToString().Trim() : "";
                        oPersonaParadero.nroContacto = person.nroContacto != null ? person.nroContacto.ToString().Trim() : "";
                        Modelo.SJM_PersonaParadero.InsertOnSubmit(oPersonaParadero);
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

        public void Delete(string conection, SJM_PersonaParadero person)
        {
            SJM_PersonaParadero oPersonaParadero = new SJM_PersonaParadero();

            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJM_PersonaParadero.Where(x => x.Id == person.Id).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar

                    oPersonaParadero = resultadoSubConsulta.Single();
                    Modelo.SJM_PersonaParadero.DeleteOnSubmit(oPersonaParadero);
                    Modelo.SubmitChanges();
                    #endregion
                }

                Modelo.Connection.Close();
                #endregion
            }
            #endregion
        }

        /* Se puede anular o activar el paradero */
        public void ChangeStatus(string conection, SJM_PersonaParadero person)
        {
            SJM_PersonaParadero oPersonaParadero = new SJM_PersonaParadero();

            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                #region
                Modelo.CommandTimeout = 99999;

                var resultadoSubConsulta = Modelo.SJM_PersonaParadero.Where(x => x.Id == person.Id).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count == 1)
                {
                    #region Actualizar

                    oPersonaParadero = resultadoSubConsulta.Single();
                    if (oPersonaParadero.estado == 1)
                    {
                        oPersonaParadero.estado = 0;
                    }
                    else
                    {
                        oPersonaParadero.estado = 1;
                    }
                    Modelo.SubmitChanges();
                    #endregion
                }

                Modelo.Connection.Close();
                #endregion
            }
            #endregion

        }

        public bool ValidarDuplicidadPersonal(string conection, SJM_PersonaParadero person)
        {
            bool estado = false;

            #region Transaccion
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
            {
                #region
                context.CommandTimeout = 99999;

                var resultadoSubConsulta = context.SJM_PersonaParadero.Where(x => x.dniTrabajador == person.dniTrabajador).ToList();

                if (resultadoSubConsulta != null && resultadoSubConsulta.ToList().Count > 0)
                {
                    estado = true;
                }

                context.Connection.Close();
                #endregion
            }
            #endregion

            return estado;
        }
    }
}
