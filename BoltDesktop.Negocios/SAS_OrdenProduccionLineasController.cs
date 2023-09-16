using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_OrdenProduccionLineasController
    {


        public List<SAS_OrdenProduccionLineas> GetListAllOP(string conection, string fechaProceso)
        {
            List<SAS_OrdenProduccionLineas> resultado = new List<SAS_OrdenProduccionLineas>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_OrdenProduccionLineas.Where(x => x.fechaProceso >= Convert.ToDateTime(fechaProceso + " 00:00:00") && x.fechaProceso <= Convert.ToDateTime(fechaProceso + " 23:59:59")).ToList();


            }
            return resultado.OrderBy(x => x.hora).ToList();

        }


        public List<Grupo> GetListByCbo(string conection, string fechaProceso)
        {
            List<SAS_OrdenProduccionLineas> resultado = new List<SAS_OrdenProduccionLineas>();
            List<Grupo> listado = new List<Grupo>();
            listado.Add(new Grupo { Codigo = "000", Descripcion = "Seleccionar Linea" });

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_OrdenProduccionLineas.Where(x => x.fechaProceso.Value <= Convert.ToDateTime(fechaProceso + " 00:00:00") && x.fechaProceso <= Convert.ToDateTime(fechaProceso + " 23:59:59")).ToList();

                if (resultado != null)
                {
                    var list = (from item in resultado
                                group item by new { item.idLinea } into j
                                select new Grupo
                                {
                                    Codigo = j.Key.idLinea.Trim(),
                                    Descripcion = "Línea " + j.Key.idLinea.Trim()
                                }
                               ).ToList();

                    listado.AddRange(list);

                }
            }
            return listado.OrderBy(x => x.Codigo).ToList();

        }


        public List<Grupo> GetListByCbo(List<SAS_OrdenProduccionLineas> listOP)
        {
            List<SAS_OrdenProduccionLineas> resultado = new List<SAS_OrdenProduccionLineas>();
            List<Grupo> listado = new List<Grupo>();
            listado.Add(new Grupo { Codigo = "000", Descripcion = "Seleccionar Linea" });
            resultado = listOP.ToList();

            if (resultado != null)
            {
                var list = (from item in resultado
                            group item by new { item.idLinea } into j
                            select new Grupo
                            {
                                Codigo = j.Key.idLinea.Trim(),
                                Descripcion = "Línea " + j.Key.idLinea.Trim()
                            }
                           ).ToList();

                listado.AddRange(list);

            }

            return listado.OrderBy(x => x.Codigo).ToList();

        }


        public List<Grupo> GetListByHoraCambioCbo(string idLineaSeleccionada, List<SAS_OrdenProduccionLineas> listOP)
        {
            List<SAS_OrdenProduccionLineas> resultado = new List<SAS_OrdenProduccionLineas>();
            List<Grupo> listado = new List<Grupo>();
            listado.Add(new Grupo { Codigo = "000", Descripcion = "Seleccionar hora de cambio" });

            resultado = listOP.Where(x => x.idLinea.Trim() == idLineaSeleccionada).ToList();

            if (resultado != null)
            {
                var list = (from item in resultado
                            group item by new { item.fechaRegistro } into j
                            select new Grupo
                            {
                                Codigo = j.Key.fechaRegistro.ToString(),
                                Descripcion = j.Key.fechaRegistro.ToString()
                            }
                           ).ToList();

                listado.AddRange(list);

            }

            return listado.OrderBy(x => x.Codigo).ToList();
        }

        public List<Grupo> GetLisFormatoEmpaqueCbo(List<SAS_OrdenProduccionLineas> listOP, string idLineaSeleccionada, DateTime? horaDeCambioSelecionada)
        {
            List<SAS_OrdenProduccionLineas> resultado = new List<SAS_OrdenProduccionLineas>();
            
            List<Grupo> listado = new List<Grupo>();
            listado.Add(new Grupo { Codigo = "00", Descripcion = "Seleccionar formato de empaque" });

            resultado = listOP.Where(x => x.idLinea.Trim() == idLineaSeleccionada && Convert.ToDateTime(x.fechaRegistro) == horaDeCambioSelecionada).ToList();
            if (resultado != null)
            {
                var list = (from item in resultado
                            group item by new { item.idRegistroFormato, item.codigoOpl } into j
                            select new Grupo
                            {
                                Codigo = j.Key.idRegistroFormato.ToString().Trim(),
                                Descripcion = (j.Key.codigoOpl.Trim() == "PL" ? "PESO LOOSE" : "PESO FIJO")
                            }
                           ).ToList();
                listado.AddRange(list);
            }
            return listado.OrderBy(x => x.Codigo).ToList();
        }



    }
}
