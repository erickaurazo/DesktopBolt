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
        public List<SAS_ListadoProductos> GetListAll(string connection, int incluirAnulados)
        {
            List<SAS_ListadoProductos> resultado = new List<SAS_ListadoProductos>();

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


        public SAS_ListadoProductos GetDescriptionProductById(string connection, string idproducto)
        {
            SAS_ListadoProductos resultado = new SAS_ListadoProductos();

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
