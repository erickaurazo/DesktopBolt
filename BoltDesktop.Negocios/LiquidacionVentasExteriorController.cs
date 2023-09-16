using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class LiquidacionVentasExteriorController
    {
        public string ActualizarEsatadoDeUltimaLiquidacion(string conection, string idOperacion)
        {
            string mensajeConfirmacion = "Cambio no confirmados";
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.LIQVENTASEXTERIOR.Where(x => x.IDLIQVENTASEXTERIOR == idOperacion).ToList();
                if (resultado != null && resultado.ToList().Count > 0)
                {
                    // tienes coincidencia, por tanto tiene un registros.
                    LIQVENTASEXTERIOR oDocumento = new LIQVENTASEXTERIOR();
                    oDocumento = Modelo.LIQVENTASEXTERIOR.Where(x => x.IDLIQVENTASEXTERIOR == idOperacion).Single();
                    if (oDocumento.ES_ULTIMO == 0)
                    {
                        oDocumento.ES_ULTIMO = 1;
                    }
                    else if (oDocumento.ES_ULTIMO == 1)
                    {
                        oDocumento.ES_ULTIMO = 0;
                    }
                    Modelo.SubmitChanges();
                    mensajeConfirmacion = "Cambio de estado satisfactorio";
                }
            }
            return mensajeConfirmacion;
        }

        public List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult> ObtenerListadoinviocePorLiquidacionVentasExterior(string conection, string desde, string hasta)
        {
            List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult> resultado = new List<SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoInvoicesPorLiquidacionVentasPorPeriodo(desde, hasta).ToList();
            }

            return resultado;
        }

    }
}
