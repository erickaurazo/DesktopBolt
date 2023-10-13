using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
namespace Asistencia.Negocios
{
    public class SAS_RegistroDeAbastecimientoController
    {

        public List<SAS_RegistroAbastecientoALineasDeProceso> GetListAll(string conection, string desde, string hasta)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_RegistroAbastecientoALineasDeProceso> listado = new List<SAS_RegistroAbastecientoALineasDeProceso>();

            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listado = Modelo.SAS_RegistroAbastecientoALineasDeProceso.Where(x => x.fechaAcopio.Value >= Convert.ToDateTime(desde) && x.fechaAcopio.Value <= Convert.ToDateTime(hasta)).ToList();
            }

            return listado;
        }

        public int RegistrarAbastecimientoAlineaDeProceso(string conection, RegistroAbastecimientoDDetalle oIngresoALineaProceso)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int resultOperation = 0;
            List<RegistroAbastecimientoDDetalle> listado = new List<RegistroAbastecimientoDDetalle>();
            RegistroAbastecimientoDDetalle item = new RegistroAbastecimientoDDetalle();

            if (oIngresoALineaProceso != null)
            {
                using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
                {
                    listado = Modelo.RegistroAbastecimientoDDetalle.Where(x => x.itemDetalle == oIngresoALineaProceso.itemDetalle).ToList();

                    if (listado != null)
                    {
                        if (listado.ToList().Count == 0)
                        {
                            item = new RegistroAbastecimientoDDetalle();
                            //item.itemDDetalle = 0;
                            item.itemDetalle = oIngresoALineaProceso.itemDetalle != null ? oIngresoALineaProceso.itemDetalle : 0;
                            item.fechaRegistro = oIngresoALineaProceso.fechaRegistro != null ? oIngresoALineaProceso.fechaRegistro : (DateTime?)null;
                            item.cantidad = oIngresoALineaProceso.cantidad != null ? oIngresoALineaProceso.cantidad : 0;
                            item.hora = oIngresoALineaProceso.hora != null ? oIngresoALineaProceso.hora : (DateTime?)null;
                            item.idMovil = oIngresoALineaProceso.idMovil != null ? oIngresoALineaProceso.idMovil : "00";
                            item.idLinea = oIngresoALineaProceso.idLinea != null ? oIngresoALineaProceso.idLinea : "000";
                            item.esOrganico = oIngresoALineaProceso.esOrganico != null ? oIngresoALineaProceso.esOrganico : 0;
                            item.usuario = oIngresoALineaProceso.usuario != null ? oIngresoALineaProceso.usuario.Substring(0, 6) : Environment.UserName.ToString().Trim().Substring(0, 6);
                            item.host = oIngresoALineaProceso.host != null ? oIngresoALineaProceso.host : Environment.MachineName.ToString().Trim();
                            item.idTicketReservado = 0;
                            item.idRegistroFormato = oIngresoALineaProceso.idRegistroFormato != null ? oIngresoALineaProceso.idRegistroFormato : 0;
                            Modelo.RegistroAbastecimientoDDetalle.InsertOnSubmit(item);
                            Modelo.SubmitChanges();
                        }
                    }

                }
            }



            return resultOperation;
        }

        public int ToRemove(string conection, SAS_RegistroAbastecientoALineasDeProceso selectedItem)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int resultOperation = 0;
            List<RegistroAbastecimientoDDetalle> listado = new List<RegistroAbastecimientoDDetalle>();
            RegistroAbastecimientoDDetalle item = new RegistroAbastecimientoDDetalle();

            RegistroAbastecimientoDDetalleEliminado itemHistorico = new RegistroAbastecimientoDDetalleEliminado();

            if (selectedItem != null)
            {
                using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
                {
                    listado = Modelo.RegistroAbastecimientoDDetalle.Where(x => x.itemDetalle == selectedItem.itemDetalle).ToList();

                    if (listado != null)
                    {
                        if (listado.ToList().Count == 1)
                        {
                            item = new RegistroAbastecimientoDDetalle();
                            item = listado.ElementAt(0);

                            itemHistorico = new RegistroAbastecimientoDDetalleEliminado();
                            //itemHistorico.id = item.
                            itemHistorico.itemDetalle = item.itemDetalle;
                            itemHistorico.fechaRegistro = item.fechaRegistro;
                            itemHistorico.cantidad = item.cantidad;
                            itemHistorico.hora = item.hora;
                            itemHistorico.idMovil = item.idMovil;
                            itemHistorico.idLinea = item.idLinea;
                            itemHistorico.esOrganico = item.esOrganico;
                            itemHistorico.usuario = item.usuario.Substring(0,6);
                            itemHistorico.host = item.host;
                            Modelo.RegistroAbastecimientoDDetalleEliminado.InsertOnSubmit(itemHistorico);
                            Modelo.SubmitChanges();

                            Modelo.RegistroAbastecimientoDDetalle.DeleteOnSubmit(item);
                            Modelo.SubmitChanges();
                        }
                        else if (listado.ToList().Count > 1)
                        {
                            foreach (var itemAEliminar in listado)
                            {
                                item = new RegistroAbastecimientoDDetalle();
                                item = itemAEliminar;

                                itemHistorico = new RegistroAbastecimientoDDetalleEliminado();
                                //itemHistorico.id = item.
                                itemHistorico.itemDetalle = item.itemDetalle;
                                itemHistorico.fechaRegistro = item.fechaRegistro;
                                itemHistorico.cantidad = item.cantidad;
                                itemHistorico.hora = item.hora;
                                itemHistorico.idMovil = item.idMovil;
                                itemHistorico.idLinea = item.idLinea;
                                itemHistorico.esOrganico = item.esOrganico;
                                itemHistorico.usuario = item.usuario.Substring(0, 6);
                                itemHistorico.host = item.host;
                                Modelo.RegistroAbastecimientoDDetalleEliminado.InsertOnSubmit(itemHistorico);
                                Modelo.SubmitChanges();

                                Modelo.RegistroAbastecimientoDDetalle.DeleteOnSubmit(item);
                                Modelo.SubmitChanges();
                            }
                        }
                        
                    }

                }
            }



            return resultOperation;
        }
    }
}
