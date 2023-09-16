using Asistencia.Datos;
using Asistencia.Negocios;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Asistencia.Negocios
{
    public class SedesController
    {


        public List<Grupo> FindSilks(string conection) // Obtener comboBox de sedes
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["SAS"];
            var silks = new List<Grupo>();
            //seat.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

            if (conection != string.Empty)
            {
               using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {

                    silks = (
                        from item in Modelo.SAS_SegmentoRed
                        where item.id.Trim() != string.Empty
                        group item by new { item.id } into j
                        select new Grupo
                        {
                            Codigo = j.Key.id.Trim(),
                            Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                        }
                        ).ToList();                    

                }
            }

            return silks.OrderBy(x => x.Codigo).ToList();

        }

    }
}
