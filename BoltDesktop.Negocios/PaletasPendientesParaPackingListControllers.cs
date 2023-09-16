using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;



namespace Asistencia.Negocios
{

    public class PaletasPendientesParaPackingListControllers
    {
        public string GenerarListadoDePaletasPendientesParaPackingList(string conection, string CodigoOperacion, string codigoCliente, string idCultivo, string fechaconsulta)
        {
            string mensajeConfirmacion = "0 | No se tienen registros para la consulta";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_ObtenerPaletasPendientesParaPackingList(CodigoOperacion, "001", codigoCliente, "", idCultivo, "", fechaconsulta, 1, 'P', 0, 0, 1, 0, "").ToList();

                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.                                        
                    mensajeConfirmacion = "1 | Consulta exitosa";
                }
            }
            return mensajeConfirmacion;
        }


        public string ObtenerCodigoUnico(string conection)
        {
            string mensajeConfirmacion = "_673E318DTARNZ39";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                mensajeConfirmacion = Modelo.GenerarCodigoUnico().FirstOrDefault().Codigo.Trim();
            }
            return mensajeConfirmacion;
        }



        public List<PaletasPendientesParaPackingList> ObtenerListadoDePaletasPendientesParaPackingList(string conection, string codigoOperacion)
        {
            List<PaletasPendientesParaPackingList> resultado = new List<PaletasPendientesParaPackingList>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.PaletasPendientesParaPackingList.Where(x => x.CodigoOperacion == codigoOperacion).ToList();
            }

            return resultado;
        }


        public string CambiarCliente(string conection, string codigoRegistroPaleta, string codigoCliente)
        {
            string mensajeConfirmacion = "0 | No se se ha procesado la acción";
            List<REGISTROPALETA> resultado = new List<REGISTROPALETA>();
            REGISTROPALETA oRegistroPaleta = new REGISTROPALETA();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.REGISTROPALETA.Where(x => x.IDREGISTROPALETA == codigoRegistroPaleta).ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        oRegistroPaleta = resultado.Single();
                        oRegistroPaleta.idclieprov_destino = codigoCliente;
                        Modelo.SubmitChanges();
                        mensajeConfirmacion = "Actualización exitosa";
                    }
                }
            }

            return mensajeConfirmacion;
        }


        public List<CULTIVOS> ObtenerListadoDeCultivosActivos(string conection)
        {
            List<CULTIVOS> resultado = new List<CULTIVOS>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.CULTIVOS.Where(x => x.ESTADO == 1).ToList();
            }

            return resultado;
        }



    }

}