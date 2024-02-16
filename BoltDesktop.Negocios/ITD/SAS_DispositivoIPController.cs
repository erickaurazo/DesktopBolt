using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{

    public class SAS_DispositivoIPController
    {

        public List<SAS_ListadoDeDispositivosAllResult> ListadoDeDispositivos(string conection)
        {
            List<SAS_ListadoDeDispositivosAllResult> resultado = new List<SAS_ListadoDeDispositivosAllResult>();            

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            //using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx)) 28.12.2023
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeDispositivosAll().ToList();

            }

            return resultado;
        }

        public SAS_ListadoDeDispositivosByIdDeviceResult GetDeviceByIdDevice(string conection, int idDispositivo)
        {
            List<SAS_ListadoDeDispositivosByIdDeviceResult> resultado = new List<SAS_ListadoDeDispositivosByIdDeviceResult>();
            SAS_ListadoDeDispositivosByIdDeviceResult resultado2 = new SAS_ListadoDeDispositivosByIdDeviceResult();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeDispositivosByIdDevice(idDispositivo).ToList();
                if (resultado.ToList().Count() > 1)
                {
                    resultado2 = resultado.ElementAt(0);
                }
                else if (resultado.ToList().Count() == 1)
                {
                    resultado2 = resultado.Single();
                }

            }
            return resultado2;
        }

        public int ActualizarFoto(string connection, SAS_Dispostivo dispositivo)
        {
            int resulta = 0;
            return resulta;
        }

        

        //     List<SAS_EstadoDispositivoCBOResult> listadoCboEstadoDispositivos = new List<SAS_EstadoDispositivoCBOResult>();
        public List<SAS_EstadoDispositivoCBOResult> ObtenerListadoCboEstadoDispositivos(string connection)
        {
            List<SAS_EstadoDispositivoCBOResult> listadoCboEstadoDispositivos = new List<SAS_EstadoDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                listadoCboEstadoDispositivos = Modelo.SAS_EstadoDispositivoCBO().ToList();

            }

            return listadoCboEstadoDispositivos;
        }


        //List<SAS_ProveedorDispositivoCBOResult> listadoCboProveedorDispositivos = new List<SAS_ProveedorDispositivoCBOResult>();
        public List<SAS_ProveedorDispositivoCBOResult> ObtenerListadoProveedorDispositivoCBO(string connection)
        {
            List<SAS_ProveedorDispositivoCBOResult> result = new List<SAS_ProveedorDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_ProveedorDispositivoCBO().ToList();

            }

            return result;
        }


        //List<SAS_FuncionamientoDispositivoCBOResult> listadoCboFuncionamientoDispositivos = new List<SAS_FuncionamientoDispositivoCBOResult>();
        public List<SAS_FuncionamientoDispositivoCBOResult> ObtenerListadoFuncionamientoDispositivoCBO(string connection)
        {
            List<SAS_FuncionamientoDispositivoCBOResult> result = new List<SAS_FuncionamientoDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_FuncionamientoDispositivoCBO().ToList();

            }

            return result;
        }


        //List<SAS_SedeDispositivoCBOResult> listadoCboSedeDispositivos = new List<SAS_SedeDispositivoCBOResult>();
        public List<SAS_SedeDispositivoCBOResult> ObtenerListadoSedeDispositivoCBO(string connection)
        {
            List<SAS_SedeDispositivoCBOResult> result = new List<SAS_SedeDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_SedeDispositivoCBO().ToList();

            }

            return result;
        }


        //List<SAS_ModeloDispositivoCBOResult> listadoCboModeloDispositivos = new List<SAS_ModeloDispositivoCBOResult>();
        public List<SAS_ModeloDispositivoCBOResult> ObtenerListadoModeloDispositivoCBO(string connection)
        {
            List<SAS_ModeloDispositivoCBOResult> result = new List<SAS_ModeloDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_ModeloDispositivoCBO().ToList();

            }

            return result;
        }


        //List<SAS_MarcaDispositivoCBOResult> listadoCboMarcaDispositivos = new List<SAS_MarcaDispositivoCBOResult>();
        public List<SAS_MarcaDispositivoCBOResult> ObtenerListadoMarcaDispositivoCBO(string connection)
        {
            List<SAS_MarcaDispositivoCBOResult> result = new List<SAS_MarcaDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_MarcaDispositivoCBO().ToList();

            }

            return result;
        }


        //List<SAS_EsPropioCBOResult> listadoCboEsPropioDispositivos = new List<SAS_EsPropioCBOResult>();
        public List<SAS_EsPropioCBOResult> ObtenerListadoEsPropioCBO(string connection)
        {
            List<SAS_EsPropioCBOResult> result = new List<SAS_EsPropioCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_EsPropioCBO().ToList();

            }

            return result;
        }



        //List<SAS_TipoDispositivoCBOResult> listadoCboTipoDispositivos = new List<SAS_TipoDispositivoCBOResult>();
        public List<SAS_TipoDispositivoCBOResult> ObtenerListadoTipoDispositivoCBO(string connection)
        {
            List<SAS_TipoDispositivoCBOResult> result = new List<SAS_TipoDispositivoCBOResult>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                result = Modelo.SAS_TipoDispositivoCBO().ToList();

            }

            return result;
        }



    }
}
