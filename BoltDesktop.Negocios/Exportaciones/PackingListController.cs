using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
namespace Asistencia.Negocios
{
    public class PackingListController
    {

        public List<SAS_ListadoDePackingListAllResult> GetListPackingList01(string conection)
        {
            List<SAS_ListadoDePackingListAllResult> result = new List<SAS_ListadoDePackingListAllResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDePackingListAll().ToList().OrderBy(x => x.fecha).ToList();
            }
            return result;
        }


        public int GenerarDistribucionEnBlancoDePalletasEnContenedor(string conection, string packingListCodigo)
        {
            List<SAS_ListadoDePackingListAllResult> result = new List<SAS_ListadoDePackingListAllResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.SAS_GenerarBaseDeDistribucionDePalletParaContenedor(packingListCodigo);
            }
            return 1;
        }

        public List<SAS_ListaDistribucionPackingListByCodigoResult> GetListPackingListToDistribute(string conection, string packingListCodigo)
        {
            List<SAS_ListaDistribucionPackingListByCodigoResult> result = new List<SAS_ListaDistribucionPackingListByCodigoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListaDistribucionPackingListByCodigo(packingListCodigo).ToList();
            }
            return result;
        }

        public List<DDPACKINGLIST> GetPalletsByListPackingListById(string conection, string packingListCodigo)
        {
            List<DDPACKINGLIST> result = new List<DDPACKINGLIST>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.DDPACKINGLIST.Where(x => x.IDPACKINGLIST.Trim() == packingListCodigo).ToList();
            }
            return result;
        }



        public int ToRegisterDistributionOnPalets(string conection, List<DDISTRIBUCIONPALETAS_CONTENEDOR> listaPalletDistribuidos)
        {

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                if (listaPalletDistribuidos != null)
                {
                    if (listaPalletDistribuidos.ToList().Count > 0)
                    {
                        foreach (var item in listaPalletDistribuidos)
                        {
                            var result = Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.Where(x =>
                            x.IDPACKINGLIST.Trim() == item.IDPACKINGLIST &&
                            x.ITEM.Trim() == item.ITEM &&
                            x.COLUMNA.Trim() == item.COLUMNA &&
                            x.FILA.Trim() == item.FILA
                            ).ToList();

                            if (result.ToList().Count == 1)
                            {
                                DDISTRIBUCIONPALETAS_CONTENEDOR oRegistro = new DDISTRIBUCIONPALETAS_CONTENEDOR();
                                oRegistro = result.ElementAt(0);
                                oRegistro.IDPRODUCTO = item.IDPRODUCTO != null ? item.IDPRODUCTO.Trim() : string.Empty;
                                oRegistro.PALETAS = item.PALETAS != null ? item.PALETAS.Trim() : string.Empty;
                                oRegistro.POSICION = item.POSICION;
                                Modelo.SubmitChanges();
                            }

                        }
                    }
                }
            }
            return 1;
        }


        public int UpdateTermoRegisterEnPallets(string conection, List<DDPACKINGLIST> listaPalletsInPackingList)
        {

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                if (listaPalletsInPackingList != null)
                {
                    if (listaPalletsInPackingList.ToList().Count > 0)
                    {
                        foreach (var item in listaPalletsInPackingList)
                        {
                            var result = Modelo.DDPACKINGLIST.Where(x =>
                            x.IDPACKINGLIST.Trim() == item.IDPACKINGLIST &&
                            x.ITEM.Trim() == item.ITEM &&
                            x.NUMPALETA.Trim() == item.NUMPALETA &&
                            x.CANTIDAD == item.CANTIDAD
                            ).ToList();


                            if (result.ToList().Count == 1)
                            {
                                DDPACKINGLIST oRegistro = new DDPACKINGLIST();
                                oRegistro = result.ElementAt(0);
                                oRegistro.FORMATO = item.FORMATO != null ? item.FORMATO.Trim() : string.Empty;
                                Modelo.SubmitChanges();
                            }

                        }
                    }
                }
            }
            return 1;
        }




        public List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> GetListadoPalletasParaDistribuir(string conection, string packingListCodigo)
        {
            List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> result = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_ObtenerListadoPalletDisponiblesByCodigoPL(packingListCodigo).OrderBy(x => x.posicion).ToList();
            }
            return result;
        }

        public void ToUpdateDistributionPallet(string conection, List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listadoAGrabar)
        {
            #region Actualizar el termoRegistro()

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                if (listadoAGrabar != null)
                {
                    #region Actualizar termoregistro()
                    if (listadoAGrabar.ToList().Count > 0)
                    {
                        foreach (var item in listadoAGrabar)
                        {
                            var result = Modelo.DDPACKINGLIST.Where(x =>
                            x.IDPACKINGLIST.Trim() == item.idpackinglist.Trim() &&
                            x.NUMPALETA.Trim() == item.numpaleta.Trim()
                            ).ToList();

                            if (result.ToList().Count == 0)
                            {

                            }
                            else if (result.ToList().Count == 1)
                            {
                                DDPACKINGLIST oRegistro = new DDPACKINGLIST();
                                oRegistro = result.ElementAt(0);
                                oRegistro.FORMATO = item.ubicacionTermoRegistro != null ? item.ubicacionTermoRegistro.Trim() : string.Empty;
                                Modelo.SubmitChanges();
                            }
                            else
                            {
                                foreach (var itemDetail in result)
                                {
                                    var result02 = Modelo.DDPACKINGLIST.Where(x => x.IDPACKINGLIST.Trim() == itemDetail.IDPACKINGLIST.Trim() && x.ITEM.Trim() == itemDetail.ITEM && x.NUMPALETA.Trim() == itemDetail.NUMPALETA.Trim() && x.CANTIDAD == itemDetail.CANTIDAD).ToList();
                                    if (result02.ToList().Count == 1)
                                    {
                                        DDPACKINGLIST oRegistro = new DDPACKINGLIST();
                                        oRegistro = result02.ElementAt(0);

                                        //if (oRegistro.NUMPALETA.Trim() == "SASUV220106893")
                                        //{
                                        //    string dds = "222";
                                        //}

                                        oRegistro.FORMATO = item.ubicacionTermoRegistro != null ? item.ubicacionTermoRegistro.Trim() : string.Empty;
                                        Modelo.SubmitChanges();
                                    }
                                }
                            }

                        }
                    }
                    #endregion
                }




                if (listadoAGrabar != null)
                {
                    #region 
                    if (listadoAGrabar.ToList().Count > 0)
                    {
                        foreach (var item in listadoAGrabar)
                        {
                            var result = Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.Where(x => x.IDPACKINGLIST.Trim() == item.idpackinglist.Trim() && x.PALETAS.Trim() == item.numpaleta.Trim()).ToList();
                            if (result.ToList().Count == 1)
                            {
                                #region 
                                DDISTRIBUCIONPALETAS_CONTENEDOR oRegistroEliminar = new DDISTRIBUCIONPALETAS_CONTENEDOR();
                                oRegistroEliminar = result.ElementAt(0);
                                Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.DeleteOnSubmit(oRegistroEliminar);
                                Modelo.SubmitChanges();


                                #region  Edicion()

                                if (item.posicion == 0)
                                {

                                }
                                else
                                {
                                    DDISTRIBUCIONPALETAS_CONTENEDOR oRegistro = new DDISTRIBUCIONPALETAS_CONTENEDOR();
                                    oRegistro.IDEMPRESA = "001";
                                    oRegistro.IDPACKINGLIST = item.idpackinglist.Trim();
                                    oRegistro.ITEM = item.fila.Trim().PadLeft(3, '0');
                                    oRegistro.COLUMNA = item.columna.Trim();
                                    oRegistro.FILA = item.fila.Trim();
                                    oRegistro.IDPRODUCTO = item.IDPRODUCTO.Trim();
                                    oRegistro.PALETAS = item.numpaleta.Trim();
                                    oRegistro.FECHACREACION = DateTime.Now;
                                    oRegistro.POSICION = item.posicion;
                                    oRegistro.TEMPERATURA = (decimal?)null;
                                    oRegistro.PESO = (decimal?)null;
                                    Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.InsertOnSubmit(oRegistro);
                                    Modelo.SubmitChanges();
                                }
                                #endregion
                                #endregion
                            }
                            else if (result.ToList().Count == 0)
                            {
                                #region Nuevo() 

                                var result02 = Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.Where(x => x.IDPACKINGLIST.Trim() == item.idpackinglist && x.POSICION == item.posicion).ToList();
                                if (result02 != null)
                                {
                                    if (result02.ToList().Count == 1)
                                    {
                                        DDISTRIBUCIONPALETAS_CONTENEDOR oRegistroEliminar = new DDISTRIBUCIONPALETAS_CONTENEDOR();
                                        oRegistroEliminar = result02.ElementAt(0);
                                        Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.DeleteOnSubmit(oRegistroEliminar);
                                        Modelo.SubmitChanges();
                                    }
                                    else if (result02.ToList().Count > 1)
                                    {
                                        Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.DeleteAllOnSubmit(result02);
                                        Modelo.SubmitChanges();
                                    }
                                }

                                if (item.posicion > 0)
                                {
                                    DDISTRIBUCIONPALETAS_CONTENEDOR oRegistro = new DDISTRIBUCIONPALETAS_CONTENEDOR();
                                    oRegistro.IDEMPRESA = "001";
                                    oRegistro.IDPACKINGLIST = item.idpackinglist.Trim();
                                    oRegistro.ITEM = item.fila.Trim().PadLeft(3, '0');
                                    oRegistro.COLUMNA = item.columna.Trim();
                                    oRegistro.FILA = item.fila.Trim();
                                    oRegistro.IDPRODUCTO = item.IDPRODUCTO.Trim();
                                    oRegistro.PALETAS = item.numpaleta.Trim();
                                    oRegistro.FECHACREACION = DateTime.Now;
                                    oRegistro.POSICION = item.posicion;
                                    oRegistro.TEMPERATURA = (decimal?)null;
                                    oRegistro.PESO = (decimal?)null;
                                    Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.InsertOnSubmit(oRegistro);
                                    Modelo.SubmitChanges();
                                }



                                #endregion
                            }
                            else if (result.ToList().Count > 1)
                            {
                                #region MyRegion                                
                                Modelo.DDISTRIBUCIONPALETAS_CONTENEDOR.DeleteAllOnSubmit(result);
                                Modelo.SubmitChanges();
                                #endregion
                            }

                        }
                    }
                    #endregion
                }

            }

            #endregion
        }

        public List<SAS_ListadoConformidadCargaByBookingResult> listadDeDistribucionDeCargaBolt(string conection, string booking)
        {
            List<SAS_ListadoConformidadCargaByBookingResult> result = new List<SAS_ListadoConformidadCargaByBookingResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ProduccionContextDataContext Modelo = new ProduccionContextDataContext(cnx))
            {
                result = Modelo.SAS_ListadoConformidadCargaByBooking(booking).OrderBy(x => x.UbicacionDelPalletEnContenedor).ToList();
            }
            return result;
        }

        public List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> GenerarListadoConformidadDesdeDistribucionDeCarga(List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> listado, List<SAS_ListadoConformidadCargaByBookingResult> listadoDeDistribucionDeCarga)
        {
            List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult> result = new List<SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult>();
            SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult palletDistribuido = new SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult();
            foreach (var item in listado)
            {
                var Coincidencia = listadoDeDistribucionDeCarga.Where(x => x.PackingListId.Trim() == item.idpackinglist.Trim() && x.NumeroPaleta.Trim() == item.numpaleta.Trim()).ToList();

                if (Coincidencia.ToList().Count > 0)
                {
                    palletDistribuido = new SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult();
                    palletDistribuido.idpackinglist = item.idpackinglist;
                    palletDistribuido.numpaleta = item.numpaleta;
                    palletDistribuido.cantidad = item.cantidad;
                    palletDistribuido.ubicacionTermoRegistro = Coincidencia.ElementAt(0).Termoregistro.Trim();
                    palletDistribuido.IDPRODUCTO = item.IDPRODUCTO;
                    palletDistribuido.posicion = Coincidencia.ElementAt(0).UbicacionDelPalletEnContenedor;
                    palletDistribuido.fila = Coincidencia.ElementAt(0).Fila.Value.ToString();
                    palletDistribuido.columna = Coincidencia.ElementAt(0).Columna.Value.ToString();
                    palletDistribuido.numeroManual = Coincidencia.ElementAt(0).NumeroManual;
                    result.Add(palletDistribuido);
                }
                else
                {
                    palletDistribuido = new SAS_ObtenerListadoPalletDisponiblesByCodigoPLResult();
                    palletDistribuido = item;
                    result.Add(palletDistribuido);
                }

                
            }
            

            return result;
        }
    }
}
