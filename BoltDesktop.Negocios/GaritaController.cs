using System.Collections.Generic;
using System.Linq;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class GaritaController
    {
        private Garita garita;

        public List<Garita> Listado()
        {
            List<Garita> listado = new List<Garita>();

            garita = new Garita();
            garita.codigo = "1";
            garita.descripcion = "BOTA";
            listado.Add(garita);

            garita = new Garita();
            garita.codigo = "2";
            garita.descripcion = "BALSA";
            listado.Add(garita);


            garita = new Garita();
            garita.codigo = "3";
            garita.descripcion = "TABLAZO";
            listado.Add(garita);



            garita = new Garita();
            garita.codigo = "4";
            garita.descripcion = "SANTA MARIA";
            listado.Add(garita);


            garita = new Garita();
            garita.codigo = "5";
            garita.descripcion = "OFICINAS PIURA";
            listado.Add(garita);


            garita = new Garita();
            garita.codigo = "6";
            garita.descripcion = "PACKING UVA";
            listado.Add(garita);


            garita = new Garita();
            garita.codigo = "0";
            garita.descripcion = "TODOS";
            listado.Add(garita);



            return listado.OrderBy(x=> x.codigo).ToList();
        }


    }
}
