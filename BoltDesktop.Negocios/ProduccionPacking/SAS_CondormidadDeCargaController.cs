using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace Asistencia.Negocios.ProduccionPacking
{
    public class SAS_CondormidadDeCargaController
    {

        public List<SAS_ConformacionDeCargaByDateResult> ListAllByDates(string conection, string desde, string hasta)
        {
            List<SAS_ConformacionDeCargaByDateResult> List = new List<SAS_ConformacionDeCargaByDateResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                List = Modelo.SAS_ConformacionDeCargaByDate(desde, hasta).ToList();               
            }
            return List;  
        }

        public List<SAS_ListadoConformacionDeCargaByPeriodoResult> List(string conection, string desde, string hasta)
        {
            List<SAS_ListadoConformacionDeCargaByPeriodoResult> List = new List<SAS_ListadoConformacionDeCargaByPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                List = Modelo.SAS_ListadoConformacionDeCargaByPeriodo(desde, hasta).ToList();
            }
            return List;
        }


        public int ToRegister(string connection, SAS_ConformacionDeCarga item)
        {
            int resultado = 1;
            List<SAS_ConformacionDeCarga> listado = new List<SAS_ConformacionDeCarga>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                listado = Modelo.SAS_ConformacionDeCargas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                    //oItem.Id = item.Id;
                    oItem.Fecha = item.Fecha;
                    oItem.Descripcion = item.Descripcion;
                    oItem.NumeroContenedor = item.NumeroContenedor;
                    oItem.Booking = item.Booking;                    
                    oItem.Observacion = item.Observacion;
                    oItem.FechaRegistro = item.FechaRegistro;
                    oItem.UserId = item.UserId;
                    oItem.Hostname = item.Hostname;
                    oItem.EstadoId = item.EstadoId;
                    oItem.idCampania = item.idCampania;
                    oItem.IdClieprov = item.IdClieprov;
                    Modelo.SAS_ConformacionDeCargas.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                    oItem = listado.ElementAt(0);
                    oItem.Fecha = item.Fecha;
                    oItem.Descripcion = item.Descripcion;
                    oItem.NumeroContenedor = item.NumeroContenedor;
                    oItem.Booking = item.Booking;
                    oItem.Observacion = item.Observacion;
                    oItem.FechaRegistro = item.FechaRegistro;
                    oItem.UserId = item.UserId;
                    oItem.Hostname = item.Hostname;
                    oItem.EstadoId = item.EstadoId;
                    oItem.idCampania = item.idCampania;
                    oItem.IdClieprov = item.IdClieprov;
                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }

        public int ToChangeStatus(string connection, SAS_ConformacionDeCarga item)
        {
            int resultado = 1;
            List<SAS_ConformacionDeCarga> listado = new List<SAS_ConformacionDeCarga>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                listado = Modelo.SAS_ConformacionDeCargas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);

                    if (oItem.EstadoId == "PE")
                    {
                        oItem.EstadoId = "AN";

                        List<SAS_ConformacionDeCargaDetalle> ListDetail = new List<SAS_ConformacionDeCargaDetalle>();
                        ListDetail = Modelo.SAS_ConformacionDeCargaDetalles.Where(x => x.IdConformacionCarga == oItem.Id).ToList();

                        foreach (var detail in ListDetail)
                        {
                            SAS_ConformacionDeCargaDetalle oDetail = new SAS_ConformacionDeCargaDetalle();
                            oDetail = detail;
                            oDetail.Estado = 0;
                            Modelo.SubmitChanges();
                        }

                    }
                    else
                    {
                        oItem.EstadoId = "PE";

                        List<SAS_ConformacionDeCargaDetalle> ListDetail = new List<SAS_ConformacionDeCargaDetalle>();
                        ListDetail = Modelo.SAS_ConformacionDeCargaDetalles.Where(x => x.IdConformacionCarga == oItem.Id).ToList();
                        foreach (var detail in ListDetail)
                        {
                            SAS_ConformacionDeCargaDetalle oDetail = new SAS_ConformacionDeCargaDetalle();
                            oDetail = detail;
                            oDetail.Estado = 1;
                            Modelo.SubmitChanges();
                        }
                    }

                    Modelo.SubmitChanges();
                    resultado = oItem.Id;
                    #endregion
                }

            }
            return resultado;
        }

        public int ToDelete(string connection, SAS_ConformacionDeCarga item)
        {
            int resultado = 0;
            List<SAS_ConformacionDeCarga> listado = new List<SAS_ConformacionDeCarga>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                listado = Modelo.SAS_ConformacionDeCargas.Where(x => x.Id == item.Id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);

                    if (oItem.EstadoId == "PE")
                    {
                        oItem.EstadoId = "AN";

                        List<SAS_ConformacionDeCargaDetalle> ListDetail = new List<SAS_ConformacionDeCargaDetalle>();
                        ListDetail = Modelo.SAS_ConformacionDeCargaDetalles.Where(x => x.IdConformacionCarga == oItem.Id).ToList();

                        foreach (var detail in ListDetail)
                        {
                            SAS_ConformacionDeCargaDetalle oDetail = new SAS_ConformacionDeCargaDetalle();
                            oDetail = detail;
                            Modelo.SAS_ConformacionDeCargaDetalles.DeleteOnSubmit(oDetail);
                            Modelo.SubmitChanges();
                        }
                        Modelo.SAS_ConformacionDeCargas.DeleteOnSubmit(oItem);
                        Modelo.SubmitChanges();
                        resultado = oItem.Id;
                    }

                   
                    
                    #endregion
                }

            }
            return resultado;
        }



        public List<SAS_ListadoConformacionDeCargaPBIByIdResult> ListAllDetailById(string conection, int Id)
        {
            List<SAS_ListadoConformacionDeCargaPBIByIdResult> List = new List<SAS_ListadoConformacionDeCargaPBIByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                List = Modelo.SAS_ListadoConformacionDeCargaPBIById(Id).ToList();
            }
            return List;
        }

        public List<SAS_ListadoConformacionDeCargaPBI> ListAllDetailByDates(string conection, int Id)
        {
            List<SAS_ListadoConformacionDeCargaPBI> List = new List<SAS_ListadoConformacionDeCargaPBI>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                List = Modelo.SAS_ListadoConformacionDeCargaPBIs.ToList();
            }
            return List;
        }

        public int ActualizarEstado(string connection, int id, string estadoId)
        {
            int resultado = 1;
            List<SAS_ConformacionDeCarga> listado = new List<SAS_ConformacionDeCarga>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                listado = Modelo.SAS_ConformacionDeCargas.Where(x => x.Id == id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_ConformacionDeCarga oItem = new SAS_ConformacionDeCarga();
                    //oItem.idAccion = item.idAccion;
                    oItem = listado.ElementAt(0);

                    if (oItem.EstadoId == "PE" || oItem.EstadoId == "EP" || oItem.EstadoId == "FN")
                    {
                        oItem.EstadoId = estadoId;
                        Modelo.SubmitChanges();
                        resultado = oItem.Id;
                    }
                   
                    #endregion
                }

            }
            return resultado;
        }
    }
}
