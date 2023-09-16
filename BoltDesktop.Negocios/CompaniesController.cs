using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class CompaniesController
    {

        public List<Grupo> GetCompanies(string conection)
        {
            var companies = new List<Grupo>();
            companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            if (conection != string.Empty)
            {
                try
                {
                    using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                    {

                        var companiesQuery = (from item in Modelo.EMPRESAS
                                              where item.IDEMPRESA != string.Empty
                                              group item by new { item.IDEMPRESA } into j
                                              select new Grupo()
                                              {
                                                  Codigo = j.Key.IDEMPRESA.Trim(),
                                                  Descripcion = j.FirstOrDefault().RAZON_SOCIAL != null ? j.FirstOrDefault().RAZON_SOCIAL.Trim().ToUpper() : string.Empty
                                              }
                                       ).ToList();
                        companies.AddRange(companiesQuery);
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }

            }

            return companies.OrderBy(x => x.Codigo).ToList();

        }


        public List<Grupo> ObtenerListadoDeEmpresas(string conection)
        {
            var companies = new List<Grupo>();
            companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["SAS"];
            if (conection != string.Empty)
            {
                try
                {
                   using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                    {

                        var companiesQuery = (from item2 in Modelo.EMPRESAS_SAS
                                              where item2.IDEMPRESA != string.Empty
                                              group item2 by new { item2.IDEMPRESA } into j
                                              select new Grupo()
                                              {
                                                  Codigo = j.Key.IDEMPRESA.Trim(),
                                                  Descripcion = j.FirstOrDefault().RAZON_SOCIAL != null ? j.FirstOrDefault().RAZON_SOCIAL.Trim().ToUpper() : string.Empty
                                                  //
                                              }
                                       ).ToList();
                        companies.AddRange(companiesQuery);
                    }
                }
                catch (Exception Ex)
                {                                                            
                    throw Ex;

                }

            }

            return companies.OrderBy(x => x.Codigo).ToList();

        }


        public List<Grupo> FindCompanyById(string conection, string companyId)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[""];
            var companies = new List<Grupo>();
            companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

            if (conection != string.Empty)
            {
                using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
                {

                    companies = (
                        from item in Modelo.EMPRESAS
                        where item.IDEMPRESA.Trim() == companyId.Trim()
                        group item by new { item.IDEMPRESA } into j
                        select new Grupo
                        {
                            Codigo = j.Key.IDEMPRESA.Trim(),
                            Descripcion = j.FirstOrDefault().RAZON_SOCIAL != null ? j.FirstOrDefault().RAZON_SOCIAL.Trim() : string.Empty,
                        }
                        ).ToList();
                    companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

                }
            }

            return companies.OrderBy(x => x.Codigo).ToList();

        }


        public List<Grupo> BuscarEmpresaPorId(string conection, string companyId)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[""];
            var companies = new List<Grupo>();
            companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

            if (conection != string.Empty)
            {
               using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {

                    companies = (
                        from item in Modelo.EMPRESAS_SAS
                        where item.IDEMPRESA.Trim() == companyId.Trim()
                        group item by new { item.IDEMPRESA } into j
                        select new Grupo
                        {
                            Codigo = j.Key.IDEMPRESA.Trim(),
                            Descripcion = j.FirstOrDefault().RAZON_SOCIAL != null ? j.FirstOrDefault().RAZON_SOCIAL.Trim() : string.Empty,
                        }
                        ).ToList();
                    companies.Add(new Grupo { Codigo = "000", Descripcion = "Selecionar item" });

                }
            }

            return companies.OrderBy(x => x.Codigo).ToList();

        }


    }
}
