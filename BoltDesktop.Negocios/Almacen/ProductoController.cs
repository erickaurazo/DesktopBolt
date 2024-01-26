using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
namespace Asistencia.Negocios
{
    public class ProductoController
    {
        public List<SAS_ListadoProducto> GetListAll(string connection, int incluirAnulados)
        {
            List<SAS_ListadoProducto> resultado = new List<SAS_ListadoProducto>();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoProductos.OrderBy(x => x.descripcion).ToList();
                if (incluirAnulados == 0)
                {
                    resultado = resultado.Where(x => x.estado == "1").ToList();
                }
               
            }

            return resultado;
        }


        public SAS_ListadoProducto GetDescriptionProductById(string connection, string idproducto)
        {
            SAS_ListadoProducto resultado = new SAS_ListadoProducto();

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
               var result = Modelo.SAS_ListadoProductos.Where(x=> x.idproducto.Trim().ToUpper() == idproducto.Trim().ToUpper()).OrderBy(x => x.descripcion).ToList();

                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        resultado = result.ElementAt(0);
                    }
                }

              

            }

            return resultado;
        }

    }
}
