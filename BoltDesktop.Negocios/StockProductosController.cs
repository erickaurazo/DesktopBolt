using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Asistencia.Negocios
{
    public class StockProductosController
    {

        public List<SAS_StockProductos> ObtenerListadoStockDeProducto(string connection)
        {
            List<SAS_StockProductos> resultado = new List<SAS_StockProductos>();
            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                string asc = DateTime.Now.ToString("yyyyMMdd");
                resultado = Modelo.SAS_ReporteStockPorAlmacen("001", "", "", DateTime.Now.ToString("yyyyMMdd"), "", "", "", "", "").ToList();
                resultado = Modelo.SAS_StockProductos.ToList();

                return resultado;


            }
        }
    }
}
