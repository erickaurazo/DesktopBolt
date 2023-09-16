using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;
using Asistencia;
using MyControlsDataBinding.Busquedas;

namespace Asistencia.Negocios
{
    public class SAS_TicketReservadoController
    {

        public List<TicketReservado> GetList(string conection)
        {
            List<TicketReservado> listado = new List<TicketReservado>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                listado = Modelo.TicketReservado.ToList();
            }

            return listado;
        }


        public List<DFormatoSimple> GetListTickerReservados(string conection, string tipoCultivo)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            List<SAS_SegmentoRed> typeOfInterfaces = new List<SAS_SegmentoRed>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                if (tipoCultivo == "C")
                {
                    var listadoOrganicos = Modelo.TicketReservado.Where(x => x.id >= 900000000 && x.id <= 999999999 && x.estaAsociado == 0).ToList();
                    listado = (from item in listadoOrganicos
                               select new DFormatoSimple { Codigo = item.id.ToString(), Descripcion = item.id.ToString() }
                               ).ToList();
                }
                else if (tipoCultivo == "O")
                {
                  var  listadoOrganicos = Modelo.TicketReservado.Where(x => x.id >= 800000000 && x.id < 900000000 && x.estaAsociado == 0).ToList();
                    listado = (from item in listadoOrganicos
                               select new DFormatoSimple { Codigo = item.id.ToString(), Descripcion = item.id.ToString() }
                               ).ToList();
                }
                //listado.Add(new DFormatoSimple { Codigo = "F", Descripcion = "FISICO" });
                //listado.Add(new DFormatoSimple { Codigo = "W", Descripcion = "WIRELESS" });
            }
            return listado;
        }

        public List<SAS_TicketReservados> GetFullViewList(string conection)
        {
            List<SAS_TicketReservados> listado = new List<SAS_TicketReservados>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                listado = Modelo.SAS_TicketReservados.ToList();
            }
            return listado;
        }

        public int ToRegister(string conection, TicketReservado ticket)
        {
            List<TicketReservado> listado = new List<TicketReservado>();
            TicketReservado oTicket = new TicketReservado();
            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                listado = Modelo.TicketReservado.Where(x => x.id == ticket.id).ToList();
                if (listado != null)
                {
                    if (listado.ToList().Count == 0)
                    {
                        #region Registrar()
                        oTicket = new TicketReservado();

                        oTicket.id = ticket.id;
                        oTicket.cantidad = 30;
                        oTicket.fechaCreacion = DateTime.Now;
                        oTicket.estaAsociado = 0;
                        oTicket.impreso = 0;
                        Modelo.TicketReservado.InsertOnSubmit(oTicket);
                        Modelo.SubmitChanges();

                        #endregion
                        codigo = oTicket.id;
                    }
                    if (listado.ToList().Count > 1)
                    {
                        #region Editar()
                        oTicket = new TicketReservado();
                        oTicket = listado.ElementAt(0);
                        oTicket.cantidad = ticket.cantidad;
                        //oTicket.fechaCreacion = DateTime.Now;
                        oTicket.estaAsociado = ticket.estaAsociado;
                        oTicket.impreso = ticket.impreso;
                        Modelo.SubmitChanges();
                        #endregion
                        codigo = oTicket.id;
                    }
                }
            }

            return codigo;
        }


        // registrar masivamente Tickets reservados en blanco, el tope es 200 tanto como orgánico como en convencional()
        public int MassRegisterBlankTickets(string conection)
        {
            List<TicketReservado> listadoOrganicos = new List<TicketReservado>();
            List<TicketReservado> listadoConvencional = new List<TicketReservado>();
            TicketReservado oTicket = new TicketReservado();
            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                listadoOrganicos = Modelo.TicketReservado.Where(x => x.id >= 800000000 && x.id < 900000000 && x.impreso == 0).ToList();
                listadoConvencional = Modelo.TicketReservado.Where(x => x.id >= 900000000 && x.id <= 999999999 && x.impreso == 0).ToList();

                #region Tickets orgánicos()
                if (listadoOrganicos != null)
                {
                    if (listadoOrganicos.ToList().Count < 200)
                    {
                        #region Agregar tickets orgánicos()
                        // cantidad de tickets faltantes
                        int NumberOfMissingTickets = 200 - (listadoOrganicos.ToList().Count);

                        for (int i = 0; i < NumberOfMissingTickets; i++)
                        {
                            #region Registrar Tickets()
                            int obtenerUltimoTicketsOrgánico = Modelo.TicketReservado.Where(x => x.id >= 800000000 && x.id < 900000000).ToList().Max(x => x.id) + 1;
                            oTicket = new TicketReservado();
                            oTicket.id = obtenerUltimoTicketsOrgánico;
                            oTicket.cantidad = 30;
                            oTicket.fechaCreacion = DateTime.Now;
                            oTicket.estaAsociado = 0;
                            oTicket.impreso = 0;
                            Modelo.TicketReservado.InsertOnSubmit(oTicket);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                        #endregion
                    }
                }
                #endregion

                #region Tickets Convencionales()
                if (listadoConvencional != null)
                {
                    if (listadoConvencional.ToList().Count < 200)
                    {
                        #region Agregar tickets orgánicos()
                        // cantidad de tickets faltantes
                        int NumberOfMissingTickets = 200 - (listadoConvencional.ToList().Count);

                        for (int i = 0; i < NumberOfMissingTickets; i++)
                        {
                            #region Registrar Tickets()
                            int obtenerUltimoTicketsConvencional = Modelo.TicketReservado.Where(x => x.id >= 900000000 && x.id < 999999999).ToList().Max(x => x.id) + 1;
                            oTicket = new TicketReservado();
                            oTicket.id = obtenerUltimoTicketsConvencional;
                            oTicket.cantidad = 30;
                            oTicket.fechaCreacion = DateTime.Now;
                            oTicket.estaAsociado = 0;
                            oTicket.impreso = 0;
                            Modelo.TicketReservado.InsertOnSubmit(oTicket);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                        #endregion
                    }
                }
                #endregion

            }

            return codigo;
        }

        // registrar número de tickets
        public int RegisterticketsbyQuantity(string conection, int cantidad, string tipoCultivo)
        {
            List<TicketReservado> listadoOrganicos = new List<TicketReservado>();
            List<TicketReservado> listadoConvencional = new List<TicketReservado>();
            List<TicketReservado> listadoTicket = new List<TicketReservado>();
            TicketReservado oTicket = new TicketReservado();
            int codigo = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                if (tipoCultivo == "C")
                {
                    for (int i = 0; i < cantidad; i++)
                    {
                        listadoTicket = Modelo.TicketReservado.Where(x => x.id >= 900000000 && x.id <= 999999999).ToList();
                        #region Registrar Tickets()
                        int obtenerUltimoTicketsConvencional = listadoTicket.Max(x => x.id) + 1;
                        oTicket = new TicketReservado();
                        oTicket.id = obtenerUltimoTicketsConvencional;
                        oTicket.cantidad = 30;
                        oTicket.fechaCreacion = DateTime.Now;
                        oTicket.estaAsociado = 0;
                        oTicket.impreso = 0;
                        Modelo.TicketReservado.InsertOnSubmit(oTicket);
                        Modelo.SubmitChanges();
                        #endregion
                    }
                }
                else
                {
                    if (tipoCultivo == "O")
                    {

                        for (int i = 0; i < cantidad; i++)
                        {
                            listadoTicket = Modelo.TicketReservado.Where(x => x.id >= 800000000 && x.id < 900000000).ToList();
                            #region Registrar Tickets()
                            int obtenerUltimoTicketsOrgánico = listadoTicket.Max(x => x.id) + 1;
                            oTicket = new TicketReservado();
                            oTicket.id = obtenerUltimoTicketsOrgánico;
                            oTicket.cantidad = 30;
                            oTicket.fechaCreacion = DateTime.Now;
                            oTicket.estaAsociado = 0;
                            oTicket.impreso = 0;
                            Modelo.TicketReservado.InsertOnSubmit(oTicket);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }
                }


            }

            return codigo;
        }

        public List<int> GetMaxAndMin(string conection, int cantidad, string tipoTicket)
        {
            List<int> listado = new List<int>();
            List<TicketReservado> listadoTicket = new List<TicketReservado>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                Modelo.CommandTimeout = 9000;
                if (tipoTicket == "9")
                {
                    #region Convencional
                    listadoTicket = Modelo.TicketReservado.Where(x => x.id >= 900000000 && x.id <= 999999999 && x.estaAsociado == 0).ToList();
                    if (listadoTicket != null)
                    {
                        if (listadoTicket.Count() > cantidad)
                        {
                            int primerTicket = listadoTicket.Min(x => x.id);
                            int ultimoTicket = 0;
                            listado.Add(primerTicket);
                            int ultimoNumeroAEvaluar = primerTicket + cantidad;

                            for (int i = primerTicket; i < ultimoNumeroAEvaluar; i++)
                            {
                                ultimoTicket = i;
                            }
                            listado.Add(ultimoTicket);
                        }
                        else
                        {
                            if (listadoTicket.Count() == 0)
                            {
                                listado.Add(0);
                                listado.Add(0);
                            }
                            else
                            {
                                int primerTicket = listadoTicket.Min(x => x.id);
                                int ultimoTicket = listadoTicket.Max(x => x.id);
                                listado.Add(primerTicket);
                                listado.Add(ultimoTicket);
                            }
                        }
                    }
#endregion
                }
                else if (tipoTicket == "8")
                {
                    #region orgánico
                    listadoTicket = Modelo.TicketReservado.Where(x => x.id >= 800000000 && x.id < 900000000 && x.estaAsociado == 0).ToList();
                    if (listadoTicket != null)
                    {
                        if (listadoTicket.Count() > cantidad)
                        {
                            int primerTicket = listadoTicket.Min(x => x.id);
                            int ultimoTicket = 0;
                            listado.Add(primerTicket);
                            int ultimoNumeroAEvaluar = primerTicket + cantidad;

                            for (int i = primerTicket; i < ultimoNumeroAEvaluar; i++)
                            {
                                ultimoTicket = i;
                            }
                            listado.Add(ultimoTicket);
                        }
                        else
                        {
                            if (listadoTicket.Count() == 0)
                            {
                                listado.Add(0);
                                listado.Add(0);
                            }
                            else
                            {
                                int primerTicket = listadoTicket.Min(x => x.id);
                                int ultimoTicket = listadoTicket.Max(x => x.id);
                                listado.Add(primerTicket);
                                listado.Add(ultimoTicket);
                            }
                        }
                    }
                    #endregion
                }
            }

            return listado;
        }
    }
}
