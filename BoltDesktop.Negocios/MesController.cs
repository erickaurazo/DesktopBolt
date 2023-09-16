using System.Collections.Generic;
using System.Linq;
using Asistencia.Datos;
using System.Configuration;

namespace Asistencia.Negocios
{
    public class MesController
    {

        List<MesesObj> Meses = new List<MesesObj>();
        MesesObj obj = new MesesObj();

        public List<MesesObj> ListarMeses()
        {
            obj = new MesesObj();
            obj.Valor = "00";
            obj.Descripcion = "TODOS";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "01";
            obj.Descripcion = "ENERO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "02";
            obj.Descripcion = "FEBRERO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "03";
            obj.Descripcion = "MARZO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "04";
            obj.Descripcion = "ABRIL";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "05";
            obj.Descripcion = "MAYO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "06";
            obj.Descripcion = "JUNIO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "07";
            obj.Descripcion = "JULIO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "08";
            obj.Descripcion = "AGOSTO";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "09";
            obj.Descripcion = "SETIEMBRE";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "10";
            obj.Descripcion = "OCTUBRE";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "11";
            obj.Descripcion = "NOVIEMBRE";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "12";
            obj.Descripcion = "DICIEMBRE";
            Meses.Add(obj);


            obj = new MesesObj();
            obj.Valor = "13";
            obj.Descripcion = "PERSONALIZADO";
            Meses.Add(obj);

            return Meses.ToList();
        }

        public List<MesesObj> ListadoSemanasPorAnio(string conection, int anio)
        {
            List<MesesObj> resultado = new List<MesesObj>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado01 = Modelo.SAS_PeriodoMaquinaria.Where(x => x.anio == anio.ToString()).ToList();


                resultado = (from items in resultado01.ToList()
                             group items by new { items.semana } into j
                             select new MesesObj
                             {
                                 Valor = j.Key.semana,
                                 Descripcion = j.FirstOrDefault().semana,
                             }
                             ).ToList();

            }

            return resultado;

        }


        public List<MesesObj> ObtenerListadoPeriodos()
        {

            obj = new MesesObj();
            obj.Valor = "202001";
            obj.Descripcion = "ENERO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202002";
            obj.Descripcion = "FEBRERO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202003";
            obj.Descripcion = "MARZO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202004";
            obj.Descripcion = "ABRIL 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202005";
            obj.Descripcion = "MAYO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202006";
            obj.Descripcion = "JUNIO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202007";
            obj.Descripcion = "JULIO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202008";
            obj.Descripcion = "AGOSTO 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202009";
            obj.Descripcion = "SETIEMBRE 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202010";
            obj.Descripcion = "OCTUBRE 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202011";
            obj.Descripcion = "NOVIEMBRE 2020";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202012";
            obj.Descripcion = "DICIEMBRE 2020";
            Meses.Add(obj);



            obj = new MesesObj();
            obj.Valor = "202101";
            obj.Descripcion = "ENERO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202102";
            obj.Descripcion = "FEBRERO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202103";
            obj.Descripcion = "MARZO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202104";
            obj.Descripcion = "ABRIL 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202105";
            obj.Descripcion = "MAYO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202106";
            obj.Descripcion = "JUNIO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202107";
            obj.Descripcion = "JULIO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202108";
            obj.Descripcion = "AGOSTO 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202109";
            obj.Descripcion = "SETIEMBRE 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202110";
            obj.Descripcion = "OCTUBRE 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202111";
            obj.Descripcion = "NOVIEMBRE 2021";
            Meses.Add(obj);

            obj = new MesesObj();
            obj.Valor = "202112";
            obj.Descripcion = "DICIEMBRE 2021";
            Meses.Add(obj);

            return Meses.ToList();
        }

    }
}
