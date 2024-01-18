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
    public class SAS_DispositivoTipoSoporteFuncionalController
    {
        public List<SAS_DispositivoTipoSoporteFuncional> GetToList(string conection)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_DispositivoTipoSoporteFuncional> list = new List<SAS_DispositivoTipoSoporteFuncional>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                list = Modelo.SAS_DispositivoTipoSoporteFuncional.ToList();
            }

            return list;
        }

        public List<SAS_DispositivoTipoSoporteFuncionalDetalleByIdCodigoResult> GetToListDetail(string conection, SAS_DispositivoTipoSoporteFuncional item)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_DispositivoTipoSoporteFuncionalDetalleByIdCodigoResult> list = new List<SAS_DispositivoTipoSoporteFuncionalDetalleByIdCodigoResult>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                //list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x=> x.codigo.Trim()== item.codigo.Trim()).ToList();
                list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalleByIdCodigo(item.codigo.Trim()).ToList();
            }

            return list;
        }


        public List<SAS_DispositivoTipoSoporteFuncionalDetalle> GetToListDetailByIdByTypeSoftware(string conection, SAS_DispositivoTipoSoporteFuncional item, int tipoSoftware)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_DispositivoTipoSoporteFuncionalDetalle> list = new List<SAS_DispositivoTipoSoporteFuncionalDetalle>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                //list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x=> x.codigo.Trim()== item.codigo.Trim()).ToList();
                list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == item.codigo.Trim() && x.idtipoSoftware == tipoSoftware).ToList();
            }

            return list;
        }




        public int ToRegister(string conection, SAS_DispositivoTipoSoporteFuncional item, List<SAS_DispositivoTipoSoporteFuncionalDetalle> listAdd, List<SAS_DispositivoTipoSoporteFuncionalDetalle> ListDelete)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int operation = 0;

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {

                var result01 = Modelo.SAS_DispositivoTipoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();

                if (result01.Count == 0)
                {
                    #region New Register()
                    int ultimoRegistro = Convert.ToInt32(Modelo.SAS_DispositivoTipoSoporteFuncional.Max(x => x.codigo)) + 1;
                    SAS_DispositivoTipoSoporteFuncional oItem = new SAS_DispositivoTipoSoporteFuncional();
                    oItem.codigo = ultimoRegistro.ToString().PadLeft(3, '0');
                    oItem.descripcion = item.descripcion.Trim();
                    oItem.abreviatura = item.abreviatura.Trim();
                    oItem.estado = 1;
                    oItem.fechaCreacion = DateTime.Now;
                    oItem.usuario = item.usuario;
                    Modelo.SAS_DispositivoTipoSoporteFuncional.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();

                    #region Listado Eliminados() 
                    if (ListDelete != null)
                    {
                        if (ListDelete.ToList().Count > 0)
                        {
                            foreach (var oDetail in ListDelete)
                            {
                                var result02 = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oDetail.codigo.Trim() && x.item == oDetail.item.Trim()).ToList();
                                if (result02.Count == 1)
                                {
                                    #region Eliminar detalle()
                                    SAS_DispositivoTipoSoporteFuncionalDetalle detailDelete = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    detailDelete = result02.ElementAt(0);
                                    Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.DeleteOnSubmit(detailDelete);
                                    Modelo.SubmitChanges();
                                    #endregion
                                }

                            }
                        }
                    }
                    #endregion

                    #region Listado detalle() 
                    if (listAdd != null)
                    {
                        if (listAdd.ToList().Count > 0)
                        {
                            foreach (var oDetail in listAdd)
                            {
                                SAS_DispositivoTipoSoporteFuncionalDetalle oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                var result03 = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oItem.codigo.Trim() && x.item == oDetail.item.Trim()).ToList();
                                if (result03.Count == 0)
                                {
                                    //int ultimoRegistroDetalle = Convert.ToInt32(Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oDetail.codigo).Max(x => x.codigo)) + 1;
                                    oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    oDetailRegister.codigo = oItem.codigo;
                                    oDetailRegister.item = oDetail.item;
                                    oDetailRegister.descripcion = oDetail.descripcion;
                                    oDetailRegister.estado = 1;
                                    oDetailRegister.idtipoSoftware = oDetail.idtipoSoftware;
                                    oDetailRegister.visibleEnReportes = oDetail.visibleEnReportes;
                                    oDetailRegister.creadoPor = oDetail.creadoPor;
                                    oDetailRegister.fechaCreacion = DateTime.Now;
                                    oDetailRegister.hostname = oDetail.hostname;
                                    oDetailRegister.minutos = oDetail.minutos;

                                    Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.InsertOnSubmit(oDetailRegister);
                                    Modelo.SubmitChanges();
                                }
                                else if (result03.Count == 1)
                                {
                                    oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    oDetailRegister = result03.ElementAt(0);
                                    oDetailRegister.descripcion = oDetail.descripcion;
                                    oDetailRegister.visibleEnReportes = oDetail.visibleEnReportes;
                                    oDetailRegister.minutos = oDetail.minutos;
                                    //Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.InsertOnSubmit(oDetailRegister);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                    }
                    #endregion
                    operation = 0;
                    #endregion
                }
                else if (result01.Count == 1)
                {
                    #region Edit()
                    SAS_DispositivoTipoSoporteFuncional oItem = new SAS_DispositivoTipoSoporteFuncional();
                    oItem = result01.ElementAt(0);
                    oItem.descripcion = item.descripcion.Trim();
                    oItem.abreviatura = item.abreviatura.Trim();
                    Modelo.SubmitChanges();

                    #region Listado Eliminados() 
                    if (ListDelete != null)
                    {
                        if (ListDelete.ToList().Count > 0)
                        {
                            foreach (var oDetail in ListDelete)
                            {
                                var result02 = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oDetail.codigo.Trim() && x.item == oDetail.item.Trim()).ToList();
                                if (result02.Count == 1)
                                {
                                    #region Eliminar detalle()
                                    SAS_DispositivoTipoSoporteFuncionalDetalle detailDelete = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    detailDelete = result02.ElementAt(0);
                                    Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.DeleteOnSubmit(detailDelete);
                                    Modelo.SubmitChanges();
                                    #endregion
                                }

                            }
                        }
                    }
                    #endregion

                    #region Listado detalle() 
                    if (listAdd != null)
                    {
                        if (listAdd.ToList().Count > 0)
                        {
                            foreach (var oDetail in listAdd)
                            {
                                SAS_DispositivoTipoSoporteFuncionalDetalle oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                var result03 = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oDetail.codigo.Trim() && x.item == oDetail.item.Trim()).ToList();
                                if (result03.Count == 0)
                                {
                                    //int ultimoRegistroDetalle = Convert.ToInt32(Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x => x.codigo == oDetail.codigo).Max(x => x.codigo)) + 1;
                                    oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    oDetailRegister.codigo = oItem.codigo; // ultimoRegistroDetalle.ToString().PadLeft(3, '0'); ;
                                    oDetailRegister.item = oDetail.item;
                                    oDetailRegister.descripcion = oDetail.descripcion;
                                    oDetailRegister.estado = 1;
                                    oDetailRegister.visibleEnReportes = oDetail.visibleEnReportes;
                                    oDetailRegister.creadoPor = oDetail.creadoPor;
                                    oDetailRegister.fechaCreacion = DateTime.Now;
                                    oDetailRegister.idtipoSoftware = oDetail.idtipoSoftware;
                                    oDetailRegister.hostname = oDetail.hostname;
                                    oDetailRegister.minutos = oDetail.minutos;
                                    Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.InsertOnSubmit(oDetailRegister);
                                    Modelo.SubmitChanges();
                                }
                                else if (result03.Count == 1)
                                {
                                    oDetailRegister = new SAS_DispositivoTipoSoporteFuncionalDetalle();
                                    oDetailRegister = result03.ElementAt(0);
                                    oDetailRegister.descripcion = oDetail.descripcion;
                                    oDetailRegister.visibleEnReportes = oDetail.visibleEnReportes;
                                    oDetailRegister.minutos = oDetail.minutos;
                                    //Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.InsertOnSubmit(oDetailRegister);
                                    Modelo.SubmitChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    operation = 1;
                    #endregion
                }

                return operation;
            }
        }


        public int Delete(string conection, SAS_DispositivoTipoSoporteFuncional item)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int operation = 0;

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var result01 = Modelo.SAS_DispositivoTipoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                if (result01.Count == 1)
                {
                    #region Delete()
                    SAS_DispositivoTipoSoporteFuncional oItem = new SAS_DispositivoTipoSoporteFuncional();
                    oItem = result01.ElementAt(0);

                    var resultado = Modelo.SAS_DispositivoTipoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                    if (resultado.Count > 0)
                    {
                        Modelo.SAS_DispositivoTipoSoporteFuncional.DeleteAllOnSubmit(resultado);
                        Modelo.SubmitChanges();
                    }

                    Modelo.SAS_DispositivoTipoSoporteFuncional.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();

                    operation = 3;
                    #endregion
                }

                return operation;
            }
        }


        public int ChangeState(string conection, SAS_DispositivoTipoSoporteFuncional item)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            int operation = 0;

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                var result01 = Modelo.SAS_DispositivoTipoSoporteFuncional.Where(x => x.codigo == item.codigo).ToList();
                if (result01.Count == 1)
                {
                    #region Edit()
                    SAS_DispositivoTipoSoporteFuncional oItem = new SAS_DispositivoTipoSoporteFuncional();
                    oItem = result01.ElementAt(0);

                    if (item.estado == 1)
                    {
                        oItem.estado = 0;
                    }
                    else
                    {
                        oItem.estado = 1;
                    }

                    Modelo.SubmitChanges();
                    operation = 4;
                    #endregion
                }

                return operation;
            }
        }

        public List<DFormatoSimple> ObtenerListadoDeMinutos(string conection)
        {
            DFormatoSimple item = new DFormatoSimple();
            List<DFormatoSimple> listado = new List<DFormatoSimple>();

            
            item = new DFormatoSimple(); item.Codigo = "0"; item.Descripcion = "0 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "1.5"; item.Descripcion = "1.5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "2.5"; item.Descripcion = "2.5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "5"; item.Descripcion = "5 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "10"; item.Descripcion = "10 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "15"; item.Descripcion = "15 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "20"; item.Descripcion = "20 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "25"; item.Descripcion = "25 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "30"; item.Descripcion = "30 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "35"; item.Descripcion = "35 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "40"; item.Descripcion = "40 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "45"; item.Descripcion = "45 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "50"; item.Descripcion = "50 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "55"; item.Descripcion = "55 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "60"; item.Descripcion = "60 minutos"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "65"; item.Descripcion = "1 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "70"; item.Descripcion = "1 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "75"; item.Descripcion = "1 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "80"; item.Descripcion = "1 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "85"; item.Descripcion = "1 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "90"; item.Descripcion = "1 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "95"; item.Descripcion = "1 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "100"; item.Descripcion = "1 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "105"; item.Descripcion = "1 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "110"; item.Descripcion = "1 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "115"; item.Descripcion = "1 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "120"; item.Descripcion = "2 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "125"; item.Descripcion = "2 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "130"; item.Descripcion = "2 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "135"; item.Descripcion = "2 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "140"; item.Descripcion = "2 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "145"; item.Descripcion = "2 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "150"; item.Descripcion = "2 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "155"; item.Descripcion = "2 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "160"; item.Descripcion = "2 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "165"; item.Descripcion = "2 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "170"; item.Descripcion = "2 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "175"; item.Descripcion = "2 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "180"; item.Descripcion = "3 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "185"; item.Descripcion = "3 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "190"; item.Descripcion = "3 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "195"; item.Descripcion = "3 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "200"; item.Descripcion = "3 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "205"; item.Descripcion = "3 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "210"; item.Descripcion = "3 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "215"; item.Descripcion = "3 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "220"; item.Descripcion = "3 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "225"; item.Descripcion = "3 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "230"; item.Descripcion = "3 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "235"; item.Descripcion = "3 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "240"; item.Descripcion = "4 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "245"; item.Descripcion = "4 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "250"; item.Descripcion = "4 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "255"; item.Descripcion = "4 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "260"; item.Descripcion = "4 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "265"; item.Descripcion = "4 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "270"; item.Descripcion = "4 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "275"; item.Descripcion = "4 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "280"; item.Descripcion = "4 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "285"; item.Descripcion = "4 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "290"; item.Descripcion = "4 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "295"; item.Descripcion = "4 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "300"; item.Descripcion = "5 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "305"; item.Descripcion = "5 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "310"; item.Descripcion = "5 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "315"; item.Descripcion = "5 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "320"; item.Descripcion = "5 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "325"; item.Descripcion = "5 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "330"; item.Descripcion = "5 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "335"; item.Descripcion = "5 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "340"; item.Descripcion = "5 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "345"; item.Descripcion = "5 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "350"; item.Descripcion = "5 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "355"; item.Descripcion = "5 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "360"; item.Descripcion = "6 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "365"; item.Descripcion = "6 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "370"; item.Descripcion = "6 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "375"; item.Descripcion = "6 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "380"; item.Descripcion = "6 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "385"; item.Descripcion = "6 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "390"; item.Descripcion = "6 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "395"; item.Descripcion = "6 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "400"; item.Descripcion = "6 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "405"; item.Descripcion = "6 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "410"; item.Descripcion = "6 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "415"; item.Descripcion = "6 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "420"; item.Descripcion = "7 Hora con 0 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "425"; item.Descripcion = "7 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "430"; item.Descripcion = "7 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "435"; item.Descripcion = "7 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "440"; item.Descripcion = "7 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "445"; item.Descripcion = "7 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "450"; item.Descripcion = "7 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "455"; item.Descripcion = "7 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "460"; item.Descripcion = "7 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "465"; item.Descripcion = "7 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "470"; item.Descripcion = "7 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "475"; item.Descripcion = "7 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "480"; item.Descripcion = "7 Hora con 60 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "485"; item.Descripcion = "8 Hora con 5 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "490"; item.Descripcion = "8 Hora con 10 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "495"; item.Descripcion = "8 Hora con 15 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "500"; item.Descripcion = "8 Hora con 20 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "505"; item.Descripcion = "8 Hora con 25 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "510"; item.Descripcion = "8 Hora con 30 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "515"; item.Descripcion = "8 Hora con 35 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "520"; item.Descripcion = "8 Hora con 40 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "525"; item.Descripcion = "8 Hora con 45 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "530"; item.Descripcion = "8 Hora con 50 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "535"; item.Descripcion = "8 Hora con 55 minutos "; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "540"; item.Descripcion = "9 Hora con 0 minutos "; listado.Add(item);



            return listado;
        }


        public List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutos> DispositivoTipoSoporteFuncionalDetalleByMinutos(string conection)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutos> list = new List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutos>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                //list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x=> x.codigo.Trim()== item.codigo.Trim()).ToList();
                list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalleByMinutos.ToList();
            }

            return list;
        }


        public List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutosGroupByAplicacion> DispositivoTipoSoporteFuncionalDetalleByMinutosGroupByAplicacion(string conection)
        {
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutosGroupByAplicacion> list = new List<SAS_DispositivoTipoSoporteFuncionalDetalleByMinutosGroupByAplicacion>();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                //list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalle.Where(x=> x.codigo.Trim()== item.codigo.Trim()).ToList();
                list = Modelo.SAS_DispositivoTipoSoporteFuncionalDetalleByMinutosGroupByAplicacion.ToList();
            }

            return list;
        }


        


    }
}
