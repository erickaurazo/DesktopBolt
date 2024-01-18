using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Transactions;
using MyControlsDataBinding.Busquedas;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class CentroDeCostosController
    {

        public List<SAS_ListadoCultivosVariedadTipoCultivo> GetListAllCultivosVariedadTipoCultivo(string conection)
        {
            List<SAS_ListadoCultivosVariedadTipoCultivo> Listado = new List<SAS_ListadoCultivosVariedadTipoCultivo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                Listado = modelo.SAS_ListadoCultivosVariedadTipoCultivo.ToList();
            }

            return Listado;
        }

        public List<Grupo> GetListCultivosVariedadTipoCultivo(string conection)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }

        public List<Grupo> ListadoCultivos(string conection)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           group item by item.IdCultivo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().cultivo != null ? j.FirstOrDefault().cultivo.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }


        public List<Grupo> ListaCampañaAgricolaByCultivoId(string conection, string idCultivo)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           where item.IdCultivo.Trim() == idCultivo.Trim()
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }

        public List<Grupo> GetCampañaAnual(string conection)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }

        public List<Grupo> GetCampañaAnual(string conection, string idcultivo)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           where item.IdCultivo.Trim() == idcultivo.Trim()
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }


        public List<Grupo> GetListCultivosEnCampañaAgricolaActivas(string conection)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {

                var list = modelo.SAS_CultivosCb.ToList();
                Listado = (from item in list
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                               
                           }
                        ).ToList();
            }

            return Listado;
        }

        public List<Grupo> GetListCultivosEnCampañaAgricolaActivas(string conection, string IdCultivo)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext modelo = new ITDContextDataContext(cnx))
            {

                var list = modelo.SAS_FechaCampanaAnual.ToList();
                Listado = (from item in list
                           where item.IdCultivo.Trim() == IdCultivo.Trim()
                           group item by item.codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                           }
                        ).ToList();
            }

            return Listado;
        }


        public List<Grupo> GetListCultivos(List<SAS_ListadoCultivosVariedadTipoCultivo> list)
        {
            List<Grupo> Listado = new List<Grupo>();

            Listado = (from item in list
                       group item by item.cultivoCodigo into j
                       select new Grupo
                       {
                           Codigo = j.Key.Trim(),
                           Descripcion = j.FirstOrDefault().cultivo != null ? j.FirstOrDefault().cultivo.Trim() : string.Empty,
                       }
                        ).ToList();
            return Listado;
        }

        public List<Grupo> GetListCultivos(List<Grupo> list)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            
                Listado = (from item in list
                           group item by item.Codigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().Descripcion != null ? j.FirstOrDefault().Descripcion.Trim() : string.Empty,
                           }
                        ).ToList();
           
            return Listado;
        }


        public List<Grupo> GetListVariedades(List<SAS_ListadoCultivosVariedadTipoCultivo> list, string cultivoId)
        {
            List<Grupo> Listado = new List<Grupo>();

            Listado = (from item in list.Where(x => x.cultivoCodigo == cultivoId)
                       group item by item.variedadCodigo into j
                       select new Grupo
                       {
                           Codigo = j.Key.Trim(),
                           Descripcion = j.FirstOrDefault().variedad != null ? j.FirstOrDefault().variedad.Trim() : string.Empty,
                       }
                        ).ToList();
            return Listado;
        }


        public List<Grupo> GetListVariedades(string conection, string cultivoId)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {

                var listCultivos = modelo.SAS_ListadoCultivosVariedadTipoCultivo.ToList();

                Listado = (from item in listCultivos.Where(x => x.cultivoCodigo == cultivoId)
                           group item by item.variedadCodigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().variedad != null ? j.FirstOrDefault().variedad.Trim() : string.Empty,
                           }
                        ).ToList();
            }
            return Listado;
        }

        public List<Grupo> GetTipoCultivo(List<SAS_ListadoCultivosVariedadTipoCultivo> list, string cultivoId, string variedadId)
        {
            List<Grupo> Listado = new List<Grupo>();

            Listado = (from item in list.Where(x => x.cultivoCodigo == cultivoId && x.variedadCodigo == variedadId)
                       group item by item.tipoCultivoCodigo into j
                       select new Grupo
                       {
                           Codigo = j.Key.Trim(),
                           Descripcion = j.FirstOrDefault().tipoCultivo != null ? j.FirstOrDefault().tipoCultivo.Trim() : string.Empty,
                       }
                        ).ToList();
            return Listado;
        }

        public List<Grupo> GetTipoCultivo(string conection,  string cultivoId, string variedadId)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {

                var listCultivos = modelo.SAS_ListadoCultivosVariedadTipoCultivo.ToList();

                Listado = (from item in listCultivos.Where(x => x.cultivoCodigo == cultivoId && x.variedadCodigo == variedadId)
                           group item by item.tipoCultivoCodigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().tipoCultivo != null ? j.FirstOrDefault().tipoCultivo.Trim() : string.Empty,
                           }
                        ).ToList();
            }
            return Listado;
        }

        public List<Grupo> GetTipoCultivo(string conection, string cultivoId)
        {
            List<Grupo> Listado = new List<Grupo>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {

                var listCultivos = modelo.SAS_ListadoCultivosVariedadTipoCultivo.ToList();

                Listado = (from item in listCultivos.Where(x => x.cultivoCodigo == cultivoId)
                           group item by item.tipoCultivoCodigo into j
                           select new Grupo
                           {
                               Codigo = j.Key.Trim(),
                               Descripcion = j.FirstOrDefault().tipoCultivo != null ? j.FirstOrDefault().tipoCultivo.Trim() : string.Empty,
                           }
                        ).ToList();
            }
            return Listado;
        }


        public List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo> ListadoConsumidoresPorCampaña(string conection, string periodo)
        {
            List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo> listado = new List<SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = modelo.SAS_ListadoConsumidoresPorCampanaAgricolaPorVariedadYCultivo.Where(x =>  x.anioCampana == periodo).ToList();
            }

            return listado;
        }

        public int  ActualizarFechaDePoda(string conection, CAMPANA_CULTIVO oCampanaCultivo)
        {
            int  resultado = 0;
            List<CAMPANA_CULTIVO> listado = new List<CAMPANA_CULTIVO>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = modelo.CAMPANA_CULTIVO.Where(x => x.IDEMPRESA == oCampanaCultivo.IDEMPRESA && x.IDCONSUMIDOR == oCampanaCultivo.IDCONSUMIDOR && x.IDSIEMBRA == oCampanaCultivo.IDSIEMBRA && x.IDCAMPANA == oCampanaCultivo.IDCAMPANA).ToList();

                if (listado != null)
                {
                    if (listado.ToList().Count == 1)
                    {
                        CAMPANA_CULTIVO oRegistro = new CAMPANA_CULTIVO();
                        oRegistro = listado.Single();
                        oRegistro.FPODA_FORMACION = oCampanaCultivo.FPODA_FORMACION;
                        oRegistro.FPODA_PRODUCCION = oCampanaCultivo.FPODA_PRODUCCION;
                        modelo.SubmitChanges();
                        resultado = 1;
                    }
                }

            }

            return resultado;
        }


        public List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult> ListadoConsumidoresConFechaAplicacionCianamidaPorCampaña(string conection, string periodo)
        {
            List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult> listado = new List<SAS_ConsumidorFechaAplicacionCListadoPorAnioResult>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["BOLT"].ToString();
            using (BoltCoorpADDataContext modelo = new BoltCoorpADDataContext(cnx))
            {
                listado = modelo.SAS_ConsumidorFechaAplicacionCListadoPorAnio(periodo).ToList();
            }

            return listado;
        }

        public int ActualizarFechaAplicacionCianamida(string conection, ConsumidorFechaAplicacionC oCampanaCultivo)
        {
            int resultado = 0;
            List<ConsumidorFechaAplicacionC> listado = new List<ConsumidorFechaAplicacionC>();

            string cnx = ConfigurationManager.AppSettings["BOLT"].ToString();
            using (BoltCoorpADDataContext modelo = new BoltCoorpADDataContext(cnx))
            {
                listado = modelo.ConsumidorFechaAplicacionC.Where(x => x.IdEmpresa == oCampanaCultivo.IdEmpresa && x.ConsumidorId == oCampanaCultivo.ConsumidorId && x.SiembraId == oCampanaCultivo.SiembraId && x.SiembraId == oCampanaCultivo.SiembraId).ToList();

                if (listado != null)
                {
                    if (listado.ToList().Count == 1)
                    {
                        ConsumidorFechaAplicacionC oRegistro = new ConsumidorFechaAplicacionC();
                        oRegistro = listado.Single();
                        oRegistro.FechaAplicacionEnFormacion = oCampanaCultivo.FechaAplicacionEnFormacion;
                        oRegistro.FechaAplicacionEnProduccion = oCampanaCultivo.FechaAplicacionEnProduccion;
                        modelo.SubmitChanges();
                        resultado = 1;
                    }
                }

            }

            return resultado;
        }


    }
}
