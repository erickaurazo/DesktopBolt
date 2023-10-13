using Asistencia.Datos;
using Asistencia.Negocios;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System;
using MyControlsDataBinding.Busquedas;

namespace Asistencia.Helper
{
    public class ComboBoxHelper
    {
        private CompaniesController companyModel;

        public List<Grupo> GetComboBoxStatus()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Code = 1, Descripcion = "Activo" });
            result.Add(new Grupo { Code = 0, Descripcion = "Anulado" });
            return result;
        }

        public List<Grupo> GetComboBoxStatusUser()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "1", Descripcion = "Activo" });
            result.Add(new Grupo { Codigo = "0", Descripcion = "Anulado" });
            return result;
        }

        public List<Grupo> GetComboBoxModule(string periodo)
        {
            ModuloSistemaController Modelo = new ModuloSistemaController();
            var modules = new List<ModuleSystem>();
            modules = Modelo.GetListAll(periodo);
            var result = (from item in modules
                          where item.moduloCodigo != null && item.moduloCodigo.Trim() != string.Empty
                          group item by new { item.moduloCodigo } into j
                          select new Grupo
                          {
                              Codigo = j.Key.moduloCodigo.Trim(),
                              Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                          }
                          ).ToList();

            return result;
        }

        internal List<Grupo> GetDocumentSeriesForForm(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                if (formulario == "Equipamiento tecnologico")
                {
                    result.Add(new Grupo { Codigo = "0001", Descripcion = "0001" });
                }
                if (formulario.ToUpper() == "MANTENIMIENTOS TI".ToUpper())
                {
                    result.Add(new Grupo { Codigo = "0001", Descripcion = "0001" });
                }
                if (formulario.ToUpper() == "Soporte funcional".ToUpper())
                {
                    result.Add(new Grupo { Codigo = "0001", Descripcion = "0001" });
                }
                if (formulario.ToUpper() == "Programacion maquinaria".ToUpper())
                {
                    result.Add(new Grupo { Codigo = "0001", Descripcion = "0001" });
                }
                if (formulario.ToUpper() == "ParteDeEquipamientoITD".ToUpper())
                {
                    result.Add(new Grupo { Codigo = "0001", Descripcion = "0001" });
                }



            }
            //    if (formulario.ToUpper() == " TIPO ESTADO DISPOSITIVOS".ToUpper())
            //    {
            //        result = (from item in Modelo.SAS_DispositivoEstado
            //                        where item.estado != 0 && (item.id == 1 || item.id == 2 || item.id == 4 || item.id == 6  || item.id == 7 || item.id == 9 || item.id == 12)
            //                        group item by new { item.id } into j
            //                        select new Grupo
            //                        {
            //                            Codigo = j.Key.id.ToString().Trim(),
            //                            Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
            //                        }
            //                  ).ToList();
            //    }
            //}



            return result;
        }



        internal List<Grupo> GetTypeStatusByDevice(string conection)
        {
            List<Grupo> result = new List<Grupo>();
            //List<SAS_DispositivosTipoMantenimiento> resultado = new List<SAS_DispositivosTipoMantenimiento>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivoEstado.Where(x => x.id != 0).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.id.ToString().Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.ToString().Trim() });
                }
            }

            return result.OrderBy(x => x.Descripcion).ToList();

            //

        }

        internal List<Grupo> getNumberDocument(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            int codigo = 1;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                if (formulario.ToUpper() == "Equipamiento tecnologico".ToUpper())
                {
                    //result.Add(new Grupo { Codigo = "SOL", Descripcion = "SOL" });
                    var resultado = Modelo.SAS_ProgramacionMaquinaria.ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count > 0)
                        {
                            codigo = resultado.Max(x => x.idProgramacionMaquinaria);
                        }
                    }
                }

                result.Add(new Grupo { Codigo = codigo.ToString().PadLeft(7, '0'), Descripcion = codigo.ToString().PadLeft(7, '0') });
            }



            //Programacion maquinaria
            return result;
        }



        internal List<Grupo> GetDocumentTypeForForm(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            if (formulario.ToUpper() == "Equipamiento tecnologico".ToUpper())
            {
                result.Add(new Grupo { Codigo = "SOL", Descripcion = "SOL" });
            }
            if (formulario.ToUpper() == "MANTENIMIENTOS TI".ToUpper())
            {
                result.Add(new Grupo { Codigo = "MIT", Descripcion = "MIT" });
            }
            if (formulario.ToUpper() == "Soporte funcional".ToUpper())
            {
                result.Add(new Grupo { Codigo = "FUN", Descripcion = "FUN" });
            }
            if (formulario.ToUpper() == "Tipo de solicitudes de Renovacion de celulares".ToUpper())
            {
                result.Add(new Grupo { Codigo = "REN", Descripcion = "REN" });
            }
            if (formulario.ToUpper() == "Programacion maquinaria".ToUpper())
            {
                result.Add(new Grupo { Codigo = "PRO", Descripcion = "PRO" });
            }
            if (formulario.ToUpper() == "ParteDeEquipamientoITD".ToUpper())
            {
                result.Add(new Grupo { Codigo = "PET", Descripcion = "PET" });
            }






            return result;
        }



        internal List<Grupo> GetUsuarioParaAsignar(string conection)
        {
            List<Grupo> result = new List<Grupo>();
            //result.Add(new Grupo { Codigo = "EAURAZO", Descripcion = "EAURAZO" });
            //result.Add(new Grupo { Codigo = "FCERNA", Descripcion = "FCERNA" });
            //result.Add(new Grupo { Codigo = "JCARMEN", Descripcion = "JCARMEN" });
            //result.Add(new Grupo { Codigo = "YCASIANO", Descripcion = "YCASIANO" });
            //result.Add(new Grupo { Codigo = "HVALENCIA", Descripcion = "HVALENCIA" });
            //result.Add(new Grupo { Codigo = "JCHERO", Descripcion = "JCHERO" });
            //return result;


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_USUARIOS.Where(x => x.idestado == "1" && x.AREA == "010").ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.IdUsuario.ToString().Trim(), Descripcion = item.NombreCompleto.Trim(), Codigo = item.IdUsuario.ToString().Trim() });
                }
            }

            return result.OrderBy(x => x.Descripcion).ToList();

        }

        internal List<Grupo> GetListStatesForRequest(string connection)
        {
            List<SAS_EstadoEnSolicitudRenovacionListado> resultado = new List<SAS_EstadoEnSolicitudRenovacionListado>();
            List<Grupo> resultado1 = new List<Grupo>();
            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_EstadoEnSolicitudRenovacionListado.ToList();

                foreach (var item in resultado)
                {
                    resultado1.Add(new Grupo { Valor = item.codigo.Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.codigo.Trim() });
                }


            }
            return resultado1;

        }

        internal List<Grupo> GetRequestTypes(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            if (formulario == "Equipamiento tecnologico")
            {
                #region Tipo de solicitudes para equipamiento tecnológico                
                #region método anterior()
                //result.Add(new Grupo { Codigo = "1", Descripcion = "Alta" });
                //result.Add(new Grupo { Codigo = "2", Descripcion = "Modificación" });
                //result.Add(new Grupo { Codigo = "3", Descripcion = "Baja" });
                //result.Add(new Grupo { Codigo = "4", Descripcion = "Renovación" });
                //result.Add(new Grupo { Codigo = "5", Descripcion = "Línea Nueva" });
                //result.Add(new Grupo { Codigo = "6", Descripcion = "Suspención" });
                //result.Add(new Grupo { Codigo = "7", Descripcion = "Activación" });
                #endregion
                List<SAS_MotivoEquipamientoTecnologico> resultado = new List<SAS_MotivoEquipamientoTecnologico>();
                string cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    resultado = Modelo.SAS_MotivoEquipamientoTecnologico.Where(x => x.estado == 1).ToList();
                    foreach (var item in resultado)
                    {
                        result.Add(new Grupo { Valor = item.id.ToString().Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.ToString().Trim() });
                    }
                }
                #endregion
            }
            else if (formulario == "Tipo de solicitudes de Renovacion de celulares")
            {
                #region Tipo de solicitud de renovaciones para celulares corporativos()
                List<SAS_MotivoRenovacionCelular> resultado = new List<SAS_MotivoRenovacionCelular>();
                string cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    resultado = Modelo.SAS_MotivoRenovacionCelular.Where(x => x.estado == 1).ToList();
                    foreach (var item in resultado)
                    {
                        result.Add(new Grupo { Valor = item.id.ToString().Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.ToString().Trim() });
                    }
                }
                #endregion
            }

            return result;
        }


        internal List<Grupo> GetRequGetRequestTypesRenovations(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();

            #region Tipo de solicitudes para equipamiento tecnológico                

            List<SAS_SolicitidudDeRenovacionCelularTipoDeJustificacion> resultado = new List<SAS_SolicitidudDeRenovacionCelularTipoDeJustificacion>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_SolicitidudDeRenovacionCelularTipoDeJustificacion.Where(x => x.estado == 1).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.id.ToString().Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.ToString().Trim() });
                }
            }
            #endregion

            return result;
        }




        internal List<Grupo> GetTypesOfMaintenance(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            List<SAS_DispositivosTipoMantenimiento> resultado = new List<SAS_DispositivosTipoMantenimiento>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivosTipoMantenimiento.Where(x => x.estado == 1).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.id.Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.Trim() });
                }
            }

            return result.OrderBy(x => x.Id).ToList();
        }

        // Tipo de mantenimientos para soporte funcional TypesOfFunctionalCare
        internal List<Grupo> TypesOfFunctionalCare(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            List<SAS_DispositivoTipoSoporteFuncional> resultado = new List<SAS_DispositivoTipoSoporteFuncional>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoTipoSoporteFuncional.Where(x => x.estado == 1).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.codigo.Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.codigo.Trim() });
                }
            }

            return result.OrderBy(x => x.Id).ToList();
        }

        internal List<Grupo> GetPriorityList(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Valor = "1", Descripcion = "ALTA", Codigo = "1", Id = "1" });
            result.Add(new Grupo { Valor = "2", Descripcion = "MEDIA", Codigo = "2", Id = "2" });
            result.Add(new Grupo { Valor = "3", Descripcion = "BAJA", Codigo = "3", Id = "3" });
            return result.OrderBy(x => x.Id).ToList();
        }




        internal List<Grupo> GetComboBoxTypeOfSoftware(string conection)
        {
            List<Grupo> result = new List<Grupo>();
            List<SAS_DispositivoClasificacionDeSoftware> resultado = new List<SAS_DispositivoClasificacionDeSoftware>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_DispositivoClasificacionDeSoftware.Where(x => x.estado == 1).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.ID.Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.ID.Trim() });
                }
            }

            return result.OrderBy(x => x.Id).ToList();
        }

        public List<Grupo> GetComboBoxHierarchy(List<SAS_FormularioSistema> forms)
        {
            var result = new List<Grupo>();
            if (forms != null && forms.ToList().Count > 0)
            {
                result = (from item in forms
                          where item.Jerarquia != null && item.Jerarquia.Trim() != string.Empty
                          group item by new { item.Jerarquia } into j
                          select new Grupo
                          {
                              Codigo = j.Key.Jerarquia.Trim(),
                              Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty
                          }
                         ).ToList();
            }


            return result;
        }

        public List<Grupo> GetComboBoxTypeForm(List<SAS_FormularioSistema> forms)
        {
            var result = new List<Grupo>();
            if (forms != null && forms.ToList().Count > 0)
            {
                result = (from item in forms
                          where item.formulario != null && item.formulario.Trim() != string.Empty
                          group item by new { item.formulario } into j
                          select new Grupo
                          {
                              Codigo = j.Key.formulario.Trim(),
                              Descripcion = j.Key.formulario.Trim()
                          }
                         ).ToList();
            }


            return result;
        }

        public List<Grupo> GetComboBoxParentForm(List<SAS_FormularioSistema> forms)
        {
            var result = new List<Grupo>();
            var formModul = forms;

            if (forms != null && forms.ToList().Count > 0)
            {
                result = (from item in forms
                          where item.barraPadre != null && item.barraPadre.Trim() != string.Empty && item.estado == 1
                          group item by new { item.barraPadre } into j
                          select new Grupo
                          {
                              Codigo = j.Key.barraPadre.Trim(),
                              Descripcion = j.Max(x => x.barraPadre.Trim())
                              //Descripcion = formModul.Where(
                              //    x => x.moduloCodigo.Trim() == j.Key.barraPadre.Trim()) != null ?
                              //    formModul.Where(x => x.Jerarquia.Trim() == j.Key.barraPadre.Trim()).Single().descripcion :
                              //    string.Empty
                          }
                         ).ToList();
            }

            return result;
        }

        public List<Grupo> GetComboBoxLocal()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "000", Descripcion = "-- Seleccionar item --" });

            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                // SELECT * FROM SAS_SegmentoRed WHERE ESSEDETRABAJO = 1

                var LugarDeTrabajo = Modelo.SAS_SegmentoRed.Where(x => x.esSedeTrabajo == 1).ToList();
                if (LugarDeTrabajo != null)
                {
                    if (LugarDeTrabajo.ToList().Count > 0)
                    {
                        foreach (var item in LugarDeTrabajo)
                        {
                            result.Add(new Grupo { Codigo = item.id.ToString(), Descripcion = item.descripcion });
                        }
                    }
                }
            }
            //result.Add(new Grupo { Codigo = "BALSA", Descripcion = "BALSA" });
            //result.Add(new Grupo { Codigo = "IMP", Descripcion = "IMP" });
            //result.Add(new Grupo { Codigo = "SAN JOSE", Descripcion = "SAN JOSE" });
            //result.Add(new Grupo { Codigo = "SANTA MARIA", Descripcion = "SANTA MARIA" });
            //result.Add(new Grupo { Codigo = "TABLAZO", Descripcion = "TABLAZO" });
            return result;

        }

        public List<Grupo> GetComboBoxAccessLevel()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "1", Descripcion = "Sin Acceso" });
            result.Add(new Grupo { Codigo = "2", Descripcion = "Administrador".ToUpper() });
            result.Add(new Grupo { Codigo = "3", Descripcion = "Usuario".ToUpper() });
            result.Add(new Grupo { Codigo = "4", Descripcion = "Soporte".ToUpper() });
            return result;
        }

        public List<Grupo> GetComboBoxBranchOffice()
        {
            List<Grupo> result = new List<Grupo>();
            //result.Add(new Grupo { Codigo = "0", Descripcion = "-- Seleccionar item --" });
            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var LugarDeTrabajo = Modelo.SUCURSALES.Where(x => x.ESTADO == 1).ToList();
                if (LugarDeTrabajo != null)
                {
                    if (LugarDeTrabajo.ToList().Count > 0)
                    {
                        foreach (var item in LugarDeTrabajo)
                        {
                            result.Add(new Grupo { Codigo = item.IDSUCURSAL, Descripcion = item.DESCRIPCION.Trim() });
                        }
                    }
                }
            }
            //result.Add(new Grupo { Codigo = "001", Descripcion = "Piura".ToUpper() });
            //result.Add(new Grupo { Codigo = "002", Descripcion = "Sullana".ToUpper() });
            //result.Add(new Grupo { Codigo = "003", Descripcion = "Catacaos".ToUpper() });
            return result;

        }

        public List<Grupo> GetComboBoxDoorAccess()
        {
            List<Grupo> result = new List<Grupo>();
            //result.Add(new Grupo { Codigo = "1", Descripcion = "-- Sin asignar --" });
            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultadoConsulta = Modelo.SAS_PuertaDeIngreso.Where(x => x.estado == 1).ToList();
                if (resultadoConsulta != null)
                {
                    if (resultadoConsulta.ToList().Count > 0)
                    {
                        foreach (var item in resultadoConsulta)
                        {
                            result.Add(new Grupo { Codigo = (item.id).ToString(), Descripcion = item.nombres });
                        }
                    }
                }
            }
            //result.Add(new Grupo { Codigo = "1", Descripcion = "BOTA" });
            //result.Add(new Grupo { Codigo = "2", Descripcion = "BALSA" });
            //result.Add(new Grupo { Codigo = "3", Descripcion = "TABLAZO" });
            //result.Add(new Grupo { Codigo = "4", Descripcion = "SANTA MARIA" });
            //result.Add(new Grupo { Codigo = "5", Descripcion = "IMP" });
            //result.Add(new Grupo { Codigo = "6", Descripcion = "PCK VID ASJ" });
            //result.Add(new Grupo { Codigo = "7", Descripcion = "PCK VID ASR" });
            //result.Add(new Grupo { Codigo = "8", Descripcion = "PCK BANANO" });
            //result.Add(new Grupo { Codigo = "9", Descripcion = "COMEDOR ASJ" });
            //result.Add(new Grupo { Codigo = "10", Descripcion = "COMEDOR PCK VID ASJ" });
            //result.Add(new Grupo { Codigo = "11", Descripcion = "COMEDOR PCK VID ASS" });
            return result;

        }
        public List<Grupo> GetComboBoxAreaAccess()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "000", Descripcion = "-- Seleccionar item --" });
            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var areasDeTrabajo = Modelo.AREAS.Where(x => x.ESTADO == 1).ToList();
                if (areasDeTrabajo != null)
                {
                    if (areasDeTrabajo.ToList().Count > 0)
                    {
                        foreach (var item in areasDeTrabajo)
                        {
                            result.Add(new Grupo { Codigo = item.IDAREA.Trim(), Descripcion = item.DESCRIPCION.Trim() });
                        }
                    }
                }
            }            
            return result;
        }


        public List<Grupo> GetComboBoxDBsByLogin()
        {
            string cnx = string.Empty;
            var dbs = new List<Grupo>();
            dbs.Add(new Grupo { Codigo = "000", Descripcion = "Seleccionar item" });
            string path = Path.Combine(@"C:\SOLUTION\AsistenciaConfig.txt");
            path = Path.GetFullPath(path);
            string[] lines = System.IO.File.ReadAllLines(path);
            int count = 0;
            foreach (string line in lines)
            {
                count += 1;
                switch (count)
                {
                    case 1:
                        // dejalo pasar
                        break;

                    case 2:
                        // usuario que se conecta a la base de datos
                        break;

                    case 3:
                        // clave del usuario que se conecta a la base de datos
                        break;

                    case 4:
                        //Instancia local
                        break;

                    case 5:
                        //Instancia publica
                        break;

                    case 6:
                        //Base de datos 1
                        string[] db01 = line.Split(':');
                        dbs.Add(new Grupo { Codigo = "001", Descripcion = db01[1].Trim() });
                        break;

                    case 7:
                        //Base de datos 1
                        string[] db02 = line.Split(':');
                        dbs.Add(new Grupo { Codigo = "002", Descripcion = db02[1].Trim() });
                        break;

                    //case 8:
                    //    //Base de datos 1
                    //    string[] db03 = line.Split(':');
                    //    dbs.Add(new Grupo { Codigo = "003", Descripcion = db03[1].Trim() });
                    //    break;

                    //case 9:
                    //    //Base de datos 1
                    //    string[] db04 = line.Split(':');
                    //    dbs.Add(new Grupo { Codigo = "004", Descripcion = db04[1].Trim() });
                    //    break;

                    default:
                        break;
                }
                // Use a tab to indent each line of the file.               
            }

            return dbs.ToList();
        }

        public List<Grupo> GetComboBoxCompanysByLogin(string cnx)
        {
            var companies = new List<Grupo>();
            companyModel = new CompaniesController();
            companies = companyModel.ObtenerListadoDeEmpresas(cnx);
            return companies;
        }

        public List<Grupo> GetComboBoxCompanysById(string db, string idCompany)
        {
            string cnx = string.Empty;
            var companies = new List<Grupo>();
            companyModel = new CompaniesController();
            companies = companyModel.BuscarEmpresaPorId(db, idCompany);
            return companies;

        }


        public List<Grupo> GetComboBoxSedes(string db)
        {
            string cnx = string.Empty;
            var skils = new List<Grupo>();
            SedesController Modelo = new SedesController();
            skils = Modelo.FindSilks(db);
            return skils;

        }

        public List<Grupo> GetComboBoxTerms(string db)
        {
            string cnx = string.Empty;
            var tems = new List<Grupo>();
            TermController Modelo = new TermController();
            tems = Modelo.FindTerms(db);
            return tems;

        }

        // tipoDeFacturacionDeConsumoEnergetico 28.05.022
        public List<Grupo> GettipoDeFacturacionDeConsumoEnergetico(string db)
        {
            string cnx = string.Empty;
            var tems = new List<Grupo>();
            tems.Add(new Grupo { Codigo = "0", Descripcion = "Selecionar item" });
            tems.Add(new Grupo { Codigo = "1", Descripcion = "Rural" });
            return tems;

        }


        public List<Grupo> GetComboBoxTypeOfDevices(string db)
        {
            string cnx = string.Empty;
            var TypeOfDevice = new List<Grupo>();
            DeviceTypeController Modelo = new DeviceTypeController();
            TypeOfDevice = Modelo.FindDeviceType(db);
            return TypeOfDevice;

        }


        public List<Grupo> TypeOfWorkAreas(string db)
        {
            List<Grupo> listado = new List<Grupo>();
            string cnx = ConfigurationManager.AppSettings[db].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = (
                        from item in Modelo.AREAS.Where(x => x.ESTADO == 1).ToList()
                        where item.IDAREA.Trim() != string.Empty
                        group item by new { item.IDAREA } into j
                        select new Grupo
                        {
                            Codigo = j.Key.IDAREA.Trim(),
                            Descripcion = j.FirstOrDefault().DESCRIPCION != null ? j.FirstOrDefault().DESCRIPCION.Trim() : string.Empty,
                        }
                        ).ToList();
            }
            return listado;

        }








        public List<Grupo> GetComboTypeWhereabouts()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "", Descripcion = "Seleccionar item" });
            result.Add(new Grupo { Codigo = "P", Descripcion = "PRINCIPAL" });
            result.Add(new Grupo { Codigo = "I", Descripcion = "INTERMEDIO" });

            return result.OrderBy(x => x.Codigo).ToList(); ;
        }


        public List<Grupo> GetComboMonthOfYear()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Valor = "01", Descripcion = "ENERO" });
            result.Add(new Grupo { Valor = "02", Descripcion = "FEBRERO" });
            result.Add(new Grupo { Valor = "03", Descripcion = "MARZO" });
            result.Add(new Grupo { Valor = "04", Descripcion = "ABRIL" });
            result.Add(new Grupo { Valor = "05", Descripcion = "MAYO" });
            result.Add(new Grupo { Valor = "06", Descripcion = "JUNIO" });
            result.Add(new Grupo { Valor = "07", Descripcion = "JULIO" });
            result.Add(new Grupo { Valor = "08", Descripcion = "AGOSTO" });
            result.Add(new Grupo { Valor = "09", Descripcion = "SETIEMBRE" });
            result.Add(new Grupo { Valor = "10", Descripcion = "OCTUBRE" });
            result.Add(new Grupo { Valor = "11", Descripcion = "NOVIEMBRE" });
            result.Add(new Grupo { Valor = "12", Descripcion = "DICIEMBRE" });
            return result;
        }

        public List<Grupo> GetComboMonth()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Valor = "00", Descripcion = "TODOS" });
            result.Add(new Grupo { Valor = "01", Descripcion = "ENERO" });
            result.Add(new Grupo { Valor = "02", Descripcion = "FEBRERO" });
            result.Add(new Grupo { Valor = "03", Descripcion = "MARZO" });
            result.Add(new Grupo { Valor = "04", Descripcion = "ABRIL" });
            result.Add(new Grupo { Valor = "05", Descripcion = "MAYO" });
            result.Add(new Grupo { Valor = "06", Descripcion = "JUNIO" });
            result.Add(new Grupo { Valor = "07", Descripcion = "JULIO" });
            result.Add(new Grupo { Valor = "08", Descripcion = "AGOSTO" });
            result.Add(new Grupo { Valor = "09", Descripcion = "SETIEMBRE" });
            result.Add(new Grupo { Valor = "10", Descripcion = "OCTUBRE" });
            result.Add(new Grupo { Valor = "11", Descripcion = "NOVIEMBRE" });
            result.Add(new Grupo { Valor = "12", Descripcion = "DICIEMBRE" });
            result.Add(new Grupo { Valor = "13", Descripcion = "PERSONALIZADO" });
            return result;
        }



        public List<Grupo> GetComboCultivosActivos(string conection)
        {
            List<Grupo> result = new List<Grupo>();
            List<CULTIVOS> resultado = new List<CULTIVOS>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.CULTIVOS.Where(x => x.ESTADO == 1).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.IDCULTIVO.Trim(), Descripcion = item.DESCRIPCION.Trim() });
                }
            }

            return result;
        }

        internal List<Grupo> ListadoDeTipoDesistemaPorCodigoTipoDispositivo(string conection, string idTipoDispositivo)
        {
            List<Grupo> result = new List<Grupo>();
            List<SAS_TipoDeSistema> resultado = new List<SAS_TipoDeSistema>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_TipoDeSistema.Where(x => x.estado == 1 && x.tipoDispositivoCodigo == idTipoDispositivo).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new Grupo { Valor = item.id.ToString().Trim(), Descripcion = item.descripcion.Trim(), Codigo = item.id.ToString() });
                }
            }

            return result;
        }

        public List<DFormatoSimple> GetListState(string conection)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();
            result.Add(new DFormatoSimple { Descripcion = "FINALIZADO", Codigo = "2" });
            result.Add(new DFormatoSimple { Descripcion = "EXONERADO", Codigo = "3" });

            return result;
        }


        public List<DFormatoSimple> GetListHerramientasIT(string conection)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_DispositivosListadoHerramientasParaTareasIT.ToList();

                result = (
                       from item in resultado
                       where item.idproducto.Trim() != string.Empty
                       group item by new { item.idproducto } into j
                       select new DFormatoSimple
                       {
                           Codigo = j.Key.idproducto.Trim(),
                           Descripcion = j.FirstOrDefault().descripcion != null ? (j.FirstOrDefault().descripcion.Trim() + '|' + j.FirstOrDefault().idmedida.Trim() + '|' + j.FirstOrDefault().unidadMedida.Trim()) : string.Empty,
                       }
                       ).ToList();
            }
            return result;
        }

        public List<DFormatoSimple> GetListUser(string conection)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();
            //result.Add(new DFormatoSimple { Descripcion = "EAURAZO", Codigo = "EAURAZO" });
            //result.Add(new DFormatoSimple { Descripcion = "FCERNA", Codigo = "FCERNA" });
            //result.Add(new DFormatoSimple { Descripcion = "YCASIANO", Codigo = "YCASIANO" });
            //result.Add(new DFormatoSimple { Descripcion = "JCARMEN", Codigo = "JCARMEN" });
            //result.Add(new DFormatoSimple { Descripcion = "HVALENCIA", Codigo = "HVALENCIA" });
            //return result;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_USUARIOS.Where(x => x.idestado == "1" && x.AREA == "010").ToList();
                foreach (var item in resultado)
                {
                    result.Add(new DFormatoSimple { Codigo = item.IdUsuario.ToString().Trim(), Descripcion = item.NombreCompleto.Trim() });
                }
            }

            return result.OrderBy(x => x.Descripcion).ToList();


        }

        internal List<DFormatoSimple> GetIdERPDevice(string conection, string codigoColaborador, string tipoDispositivo)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();
            //List<SAS_ListadoColaboradoresByDispositivo> resultado = new List<SAS_ListadoColaboradoresByDispositivo>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                //resultado = Modelo.SAS_ListadoColaboradoresByDispositivo.Where(x => x.idcodigogeneral == codigoColaborador && x.tipoDispositivoCodigo == tipoDispositivo /*&& x.estado == "ACTIVO" */ && tipoDispositivo != null).ToList();
                var resultado = Modelo.SAS_ListadoColaboradoresByDispositivoByIdColaborador(codigoColaborador).ToList().Where(x => x.tipoDispositivoCodigo == tipoDispositivo).ToList();

                foreach (var item in resultado)
                {
                    result.Add(new DFormatoSimple { Codigo = item.dispositivoCodigo.ToString().Trim(), Descripcion = item.dispositivo.Trim() });
                }
            }
            // SAS_ListadoColaboradoresByDispositivo where  dispositivoCodigo != 0 and estadoitem = 1 and idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            return result;
        }

        internal List<DFormatoSimple> GetListCelNumberByIdPerson(string conection, string codigoColaborador)
        {
            List<DFormatoSimple> result = new List<DFormatoSimple>();
            List<SAS_LineasCelularesCoporativasAll> resultado = new List<SAS_LineasCelularesCoporativasAll>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_LineasCelularesCoporativasAlls.Where(x => x.idCodigoGeneral.Trim() == codigoColaborador).ToList();
                foreach (var item in resultado)
                {
                    result.Add(new DFormatoSimple { Codigo = item.id.ToString().Trim(), Descripcion = item.lineaCelular.Trim() });
                }
            }
            // SAS_ListadoColaboradoresByDispositivo where  dispositivoCodigo != 0 and estadoitem = 1 and idcodigogeneral = '" + this.txtPersonalCodigo.Text.Trim() + " '";
            return result;
        }


        internal List<Grupo> GetListPeriodosByProgramacionMaquinaria(string conection, string formulario)
        {
            List<Grupo> result = new List<Grupo>();
            if (formulario == "Programacion maquinaria")
            {
                string cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    result = (from item in Modelo.SAS_PeriodoMaquinaria.ToList()
                              group item by new { item.anio } into j
                              select new Grupo { Codigo = j.Key.anio.ToString(), Descripcion = j.Key.anio.ToString() }
                              ).ToList();
                }
            }

            return result;
        }

        internal List<Grupo> GetListSemanaPeriodosByProgramacionMaquinaria(string conection, string formulario, int periodo)
        {
            List<Grupo> result = new List<Grupo>();
            List<string> listaExonerar = new List<string>();
            if (formulario == "Programacion maquinaria")
            {
                string cnx = ConfigurationManager.AppSettings[conection].ToString();
                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {

                    var resultSemanasEnProgramacionMaquinaria = Modelo.SAS_ProgramacionMaquinaria.Where(x => x.periodo == periodo).ToList();
                    listaExonerar = (from item in resultSemanasEnProgramacionMaquinaria
                                     group item by item.semana into j
                                     select j.Key.ToString()
                                     ).ToList();



                    result = (from item in Modelo.SAS_PeriodoMaquinaria.Where(x => x.anio == periodo.ToString()).ToList()
                              group item by new { item.semana } into j
                              select new Grupo { Code = Convert.ToInt32(j.Key.semana), Codigo = j.Key.semana.ToString(), Descripcion = "Semana : " + j.Key.semana.ToString() + " | de " + j.FirstOrDefault().fechaInicio.Value.ToLongDateString() + " al " + j.FirstOrDefault().fechaFinal.Value.ToLongDateString() }
                              ).ToList();

                    if (resultSemanasEnProgramacionMaquinaria != null)
                    {
                        if (resultSemanasEnProgramacionMaquinaria.ToList().Count > 0)
                        {
                            result = (from items in result
                                      where !(listaExonerar.Contains(items.Codigo.ToString()))
                                      select items
                         ).ToList().OrderBy(x => x.Codigo).ToList();
                        }
                    }

                }
            }

            return result;
        }


        internal List<Grupo> GetSemanaProgramacionDesdeFecha(string conection, DateTime fecha, List<Grupo> listSemanas)
        {
            List<Grupo> result = new List<Grupo>();
            int numeroSemana = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                var numeroSemanaActual = Modelo.SAS_PeriodoMaquinaria.Where(x => x.fechaInicio.Value <= fecha && x.fechaFinal.Value >= fecha).ToList();
                var semanasDisponiblesPosteriorALaActual = listSemanas.Where(x => Convert.ToInt32(x.Codigo) > Convert.ToInt32(numeroSemanaActual.ElementAt(0).semana)).OrderBy(x => Convert.ToInt32(x.Codigo)).ToList();

                result = (from item in semanasDisponiblesPosteriorALaActual
                          group item by new { item.Codigo } into j
                          select new Grupo { Codigo = j.Key.Codigo, Descripcion = j.Key.Codigo }
                          ).ToList();

            }



            //            var listado = listSemanas.Where()

            return result;
        }


        internal List<Grupo> GetCanalesDeATencion(string conection)
        {
            List<Grupo> result = new List<Grupo>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = (from item in Modelo.SAS_SoporteCanalDeAtencion.ToList()
                          group item by new { item.id } into j
                          select new Grupo { Codigo = j.Key.id.ToString(), Descripcion = j.FirstOrDefault().Descripcion.ToString() }
                          ).ToList();
            }


            return result;
        }

        internal List<Grupo> TiempoEjecutado(string conection)
        {
            Grupo item = new Grupo();
            List<Grupo> listado = new List<Grupo>();
            item = new Grupo(); item.Valor = "0"; item.Codigo = "0"; item.Id = "0"; item.Descripcion = item.Id + " | " + "0 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "2.5"; item.Codigo = "2.5"; item.Id = "2.5"; item.Descripcion = item.Id + " | " + "2.5 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "5"; item.Codigo = "5"; item.Id = "5"; item.Descripcion = item.Id + " | " + "5 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "10"; item.Codigo = "10"; item.Id = "10"; item.Descripcion = item.Id + " | " + "10 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "15"; item.Codigo = "15"; item.Id = "15"; item.Descripcion = item.Id + " | " + "15 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "20"; item.Codigo = "20"; item.Id = "20"; item.Descripcion = item.Id + " | " + "20 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "25"; item.Codigo = "25"; item.Id = "25"; item.Descripcion = item.Id + " | " + "25 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "30"; item.Codigo = "30"; item.Id = "30"; item.Descripcion = item.Id + " | " + "30 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "35"; item.Codigo = "35"; item.Id = "35"; item.Descripcion = item.Id + " | " + "35 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "40"; item.Codigo = "40"; item.Id = "40"; item.Descripcion = item.Id + " | " + "40 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "45"; item.Codigo = "45"; item.Id = "45"; item.Descripcion = item.Id + " | " + "45 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "50"; item.Codigo = "50"; item.Id = "50"; item.Descripcion = item.Id + " | " + "50 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "55"; item.Codigo = "55"; item.Id = "55"; item.Descripcion = item.Id + " | " + "55 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "60"; item.Codigo = "60"; item.Id = "60"; item.Descripcion = item.Id + " | " + "60 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "65"; item.Codigo = "65"; item.Id = "65"; item.Descripcion = item.Id + " | " + "1 Hora con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "70"; item.Codigo = "70"; item.Id = "70"; item.Descripcion = item.Id + " | " + "1 Hora con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "75"; item.Codigo = "75"; item.Id = "75"; item.Descripcion = item.Id + " | " + "1 Hora con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "80"; item.Codigo = "80"; item.Id = "80"; item.Descripcion = item.Id + " | " + "1 Hora con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "85"; item.Codigo = "85"; item.Id = "85"; item.Descripcion = item.Id + " | " + "1 Hora con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "90"; item.Codigo = "90"; item.Id = "90"; item.Descripcion = item.Id + " | " + "1 Hora con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "95"; item.Codigo = "95"; item.Id = "95"; item.Descripcion = item.Id + " | " + "1 Hora con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "100"; item.Codigo = "100"; item.Id = "100"; item.Descripcion = item.Id + " | " + "1 Hora con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "105"; item.Codigo = "105"; item.Id = "105"; item.Descripcion = item.Id + " | " + "1 Hora con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "110"; item.Codigo = "110"; item.Id = "110"; item.Descripcion = item.Id + " | " + "1 Hora con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "115"; item.Codigo = "115"; item.Id = "115"; item.Descripcion = item.Id + " | " + "1 Hora con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "120"; item.Codigo = "120"; item.Id = "120"; item.Descripcion = item.Id + " | " + "2 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "125"; item.Codigo = "125"; item.Id = "125"; item.Descripcion = item.Id + " | " + "2 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "130"; item.Codigo = "130"; item.Id = "130"; item.Descripcion = item.Id + " | " + "2 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "135"; item.Codigo = "135"; item.Id = "135"; item.Descripcion = item.Id + " | " + "2 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "140"; item.Codigo = "140"; item.Id = "140"; item.Descripcion = item.Id + " | " + "2 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "145"; item.Codigo = "145"; item.Id = "145"; item.Descripcion = item.Id + " | " + "2 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "150"; item.Codigo = "150"; item.Id = "150"; item.Descripcion = item.Id + " | " + "2 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "155"; item.Codigo = "155"; item.Id = "155"; item.Descripcion = item.Id + " | " + "2 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "160"; item.Codigo = "160"; item.Id = "160"; item.Descripcion = item.Id + " | " + "2 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "165"; item.Codigo = "165"; item.Id = "165"; item.Descripcion = item.Id + " | " + "2 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "170"; item.Codigo = "170"; item.Id = "170"; item.Descripcion = item.Id + " | " + "2 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "175"; item.Codigo = "175"; item.Id = "175"; item.Descripcion = item.Id + " | " + "2 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "180"; item.Codigo = "180"; item.Id = "180"; item.Descripcion = item.Id + " | " + "3 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "185"; item.Codigo = "185"; item.Id = "185"; item.Descripcion = item.Id + " | " + "3 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "190"; item.Codigo = "190"; item.Id = "190"; item.Descripcion = item.Id + " | " + "3 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "195"; item.Codigo = "195"; item.Id = "195"; item.Descripcion = item.Id + " | " + "3 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "200"; item.Codigo = "200"; item.Id = "200"; item.Descripcion = item.Id + " | " + "3 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "205"; item.Codigo = "205"; item.Id = "205"; item.Descripcion = item.Id + " | " + "3 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "210"; item.Codigo = "210"; item.Id = "210"; item.Descripcion = item.Id + " | " + "3 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "215"; item.Codigo = "215"; item.Id = "215"; item.Descripcion = item.Id + " | " + "3 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "220"; item.Codigo = "220"; item.Id = "220"; item.Descripcion = item.Id + " | " + "3 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "225"; item.Codigo = "225"; item.Id = "225"; item.Descripcion = item.Id + " | " + "3 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "230"; item.Codigo = "230"; item.Id = "230"; item.Descripcion = item.Id + " | " + "3 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "235"; item.Codigo = "235"; item.Id = "235"; item.Descripcion = item.Id + " | " + "3 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "240"; item.Codigo = "240"; item.Id = "240"; item.Descripcion = item.Id + " | " + "4 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "245"; item.Codigo = "245"; item.Id = "245"; item.Descripcion = item.Id + " | " + "4 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "250"; item.Codigo = "250"; item.Id = "250"; item.Descripcion = item.Id + " | " + "4 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "255"; item.Codigo = "255"; item.Id = "255"; item.Descripcion = item.Id + " | " + "4 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "260"; item.Codigo = "260"; item.Id = "260"; item.Descripcion = item.Id + " | " + "4 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "265"; item.Codigo = "265"; item.Id = "265"; item.Descripcion = item.Id + " | " + "4 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "270"; item.Codigo = "270"; item.Id = "270"; item.Descripcion = item.Id + " | " + "4 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "275"; item.Codigo = "275"; item.Id = "275"; item.Descripcion = item.Id + " | " + "4 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "280"; item.Codigo = "280"; item.Id = "280"; item.Descripcion = item.Id + " | " + "4 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "285"; item.Codigo = "285"; item.Id = "285"; item.Descripcion = item.Id + " | " + "4 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "290"; item.Codigo = "290"; item.Id = "290"; item.Descripcion = item.Id + " | " + "4 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "295"; item.Codigo = "295"; item.Id = "295"; item.Descripcion = item.Id + " | " + "4 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "300"; item.Codigo = "300"; item.Id = "300"; item.Descripcion = item.Id + " | " + "5 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "305"; item.Codigo = "305"; item.Id = "305"; item.Descripcion = item.Id + " | " + "5 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "310"; item.Codigo = "310"; item.Id = "310"; item.Descripcion = item.Id + " | " + "5 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "315"; item.Codigo = "315"; item.Id = "315"; item.Descripcion = item.Id + " | " + "5 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "320"; item.Codigo = "320"; item.Id = "320"; item.Descripcion = item.Id + " | " + "5 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "325"; item.Codigo = "325"; item.Id = "325"; item.Descripcion = item.Id + " | " + "5 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "330"; item.Codigo = "330"; item.Id = "330"; item.Descripcion = item.Id + " | " + "5 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "335"; item.Codigo = "335"; item.Id = "335"; item.Descripcion = item.Id + " | " + "5 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "340"; item.Codigo = "340"; item.Id = "340"; item.Descripcion = item.Id + " | " + "5 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "345"; item.Codigo = "345"; item.Id = "345"; item.Descripcion = item.Id + " | " + "5 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "350"; item.Codigo = "350"; item.Id = "350"; item.Descripcion = item.Id + " | " + "5 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "355"; item.Codigo = "355"; item.Id = "355"; item.Descripcion = item.Id + " | " + "5 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "360"; item.Codigo = "360"; item.Id = "360"; item.Descripcion = item.Id + " | " + "6 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "365"; item.Codigo = "365"; item.Id = "365"; item.Descripcion = item.Id + " | " + "6 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "370"; item.Codigo = "370"; item.Id = "370"; item.Descripcion = item.Id + " | " + "6 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "375"; item.Codigo = "375"; item.Id = "375"; item.Descripcion = item.Id + " | " + "6 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "380"; item.Codigo = "380"; item.Id = "380"; item.Descripcion = item.Id + " | " + "6 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "385"; item.Codigo = "385"; item.Id = "385"; item.Descripcion = item.Id + " | " + "6 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "390"; item.Codigo = "390"; item.Id = "390"; item.Descripcion = item.Id + " | " + "6 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "395"; item.Codigo = "395"; item.Id = "395"; item.Descripcion = item.Id + " | " + "6 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "400"; item.Codigo = "400"; item.Id = "400"; item.Descripcion = item.Id + " | " + "6 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "405"; item.Codigo = "405"; item.Id = "405"; item.Descripcion = item.Id + " | " + "6 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "410"; item.Codigo = "410"; item.Id = "410"; item.Descripcion = item.Id + " | " + "6 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "415"; item.Codigo = "415"; item.Id = "415"; item.Descripcion = item.Id + " | " + "6 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "420"; item.Codigo = "420"; item.Id = "420"; item.Descripcion = item.Id + " | " + "7 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "425"; item.Codigo = "425"; item.Id = "425"; item.Descripcion = item.Id + " | " + "7 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "430"; item.Codigo = "430"; item.Id = "430"; item.Descripcion = item.Id + " | " + "7 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "435"; item.Codigo = "435"; item.Id = "435"; item.Descripcion = item.Id + " | " + "7 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "440"; item.Codigo = "440"; item.Id = "440"; item.Descripcion = item.Id + " | " + "7 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "445"; item.Codigo = "445"; item.Id = "445"; item.Descripcion = item.Id + " | " + "7 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "450"; item.Codigo = "450"; item.Id = "450"; item.Descripcion = item.Id + " | " + "7 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "455"; item.Codigo = "455"; item.Id = "455"; item.Descripcion = item.Id + " | " + "7 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "460"; item.Codigo = "460"; item.Id = "460"; item.Descripcion = item.Id + " | " + "7 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "465"; item.Codigo = "465"; item.Id = "465"; item.Descripcion = item.Id + " | " + "7 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "470"; item.Codigo = "470"; item.Id = "470"; item.Descripcion = item.Id + " | " + "7 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "475"; item.Codigo = "475"; item.Id = "475"; item.Descripcion = item.Id + " | " + "7 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "480"; item.Codigo = "480"; item.Id = "480"; item.Descripcion = item.Id + " | " + "7 Horas con 60 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "485"; item.Codigo = "485"; item.Id = "485"; item.Descripcion = item.Id + " | " + "8 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "490"; item.Codigo = "490"; item.Id = "490"; item.Descripcion = item.Id + " | " + "8 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "495"; item.Codigo = "495"; item.Id = "495"; item.Descripcion = item.Id + " | " + "8 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "500"; item.Codigo = "500"; item.Id = "500"; item.Descripcion = item.Id + " | " + "8 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "505"; item.Codigo = "505"; item.Id = "505"; item.Descripcion = item.Id + " | " + "8 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "510"; item.Codigo = "510"; item.Id = "510"; item.Descripcion = item.Id + " | " + "8 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "515"; item.Codigo = "515"; item.Id = "515"; item.Descripcion = item.Id + " | " + "8 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "520"; item.Codigo = "520"; item.Id = "520"; item.Descripcion = item.Id + " | " + "8 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "525"; item.Codigo = "525"; item.Id = "525"; item.Descripcion = item.Id + " | " + "8 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "530"; item.Codigo = "530"; item.Id = "530"; item.Descripcion = item.Id + " | " + "8 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "535"; item.Codigo = "535"; item.Id = "535"; item.Descripcion = item.Id + " | " + "8 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "540"; item.Codigo = "540"; item.Id = "540"; item.Descripcion = item.Id + " | " + "9 Horas con 0 minutos "; listado.Add(item);

            return listado;
        }


        internal List<Grupo> TiempoProgramado(string conection)
        {
            Grupo item = new Grupo();
            List<Grupo> listado = new List<Grupo>();
            item = new Grupo(); item.Valor = "0"; item.Codigo = "0"; item.Id = "0"; item.Descripcion = item.Id + " | " + "0 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "2.5"; item.Codigo = "2.5"; item.Id = "2.5"; item.Descripcion = item.Id + " | " + "2.5 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "5"; item.Codigo = "5"; item.Id = "5"; item.Descripcion = item.Id + " | " + "5 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "10"; item.Codigo = "10"; item.Id = "10"; item.Descripcion = item.Id + " | " + "10 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "15"; item.Codigo = "15"; item.Id = "15"; item.Descripcion = item.Id + " | " + "15 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "20"; item.Codigo = "20"; item.Id = "20"; item.Descripcion = item.Id + " | " + "20 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "25"; item.Codigo = "25"; item.Id = "25"; item.Descripcion = item.Id + " | " + "25 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "30"; item.Codigo = "30"; item.Id = "30"; item.Descripcion = item.Id + " | " + "30 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "35"; item.Codigo = "35"; item.Id = "35"; item.Descripcion = item.Id + " | " + "35 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "40"; item.Codigo = "40"; item.Id = "40"; item.Descripcion = item.Id + " | " + "40 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "45"; item.Codigo = "45"; item.Id = "45"; item.Descripcion = item.Id + " | " + "45 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "50"; item.Codigo = "50"; item.Id = "50"; item.Descripcion = item.Id + " | " + "50 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "55"; item.Codigo = "55"; item.Id = "55"; item.Descripcion = item.Id + " | " + "55 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "60"; item.Codigo = "60"; item.Id = "60"; item.Descripcion = item.Id + " | " + "60 minutos"; listado.Add(item);
            item = new Grupo(); item.Valor = "65"; item.Codigo = "65"; item.Id = "65"; item.Descripcion = item.Id + " | " + "1 Hora con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "70"; item.Codigo = "70"; item.Id = "70"; item.Descripcion = item.Id + " | " + "1 Hora con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "75"; item.Codigo = "75"; item.Id = "75"; item.Descripcion = item.Id + " | " + "1 Hora con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "80"; item.Codigo = "80"; item.Id = "80"; item.Descripcion = item.Id + " | " + "1 Hora con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "85"; item.Codigo = "85"; item.Id = "85"; item.Descripcion = item.Id + " | " + "1 Hora con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "90"; item.Codigo = "90"; item.Id = "90"; item.Descripcion = item.Id + " | " + "1 Hora con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "95"; item.Codigo = "95"; item.Id = "95"; item.Descripcion = item.Id + " | " + "1 Hora con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "100"; item.Codigo = "100"; item.Id = "100"; item.Descripcion = item.Id + " | " + "1 Hora con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "105"; item.Codigo = "105"; item.Id = "105"; item.Descripcion = item.Id + " | " + "1 Hora con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "110"; item.Codigo = "110"; item.Id = "110"; item.Descripcion = item.Id + " | " + "1 Hora con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "115"; item.Codigo = "115"; item.Id = "115"; item.Descripcion = item.Id + " | " + "1 Hora con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "120"; item.Codigo = "120"; item.Id = "120"; item.Descripcion = item.Id + " | " + "2 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "125"; item.Codigo = "125"; item.Id = "125"; item.Descripcion = item.Id + " | " + "2 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "130"; item.Codigo = "130"; item.Id = "130"; item.Descripcion = item.Id + " | " + "2 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "135"; item.Codigo = "135"; item.Id = "135"; item.Descripcion = item.Id + " | " + "2 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "140"; item.Codigo = "140"; item.Id = "140"; item.Descripcion = item.Id + " | " + "2 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "145"; item.Codigo = "145"; item.Id = "145"; item.Descripcion = item.Id + " | " + "2 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "150"; item.Codigo = "150"; item.Id = "150"; item.Descripcion = item.Id + " | " + "2 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "155"; item.Codigo = "155"; item.Id = "155"; item.Descripcion = item.Id + " | " + "2 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "160"; item.Codigo = "160"; item.Id = "160"; item.Descripcion = item.Id + " | " + "2 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "165"; item.Codigo = "165"; item.Id = "165"; item.Descripcion = item.Id + " | " + "2 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "170"; item.Codigo = "170"; item.Id = "170"; item.Descripcion = item.Id + " | " + "2 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "175"; item.Codigo = "175"; item.Id = "175"; item.Descripcion = item.Id + " | " + "2 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "180"; item.Codigo = "180"; item.Id = "180"; item.Descripcion = item.Id + " | " + "3 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "185"; item.Codigo = "185"; item.Id = "185"; item.Descripcion = item.Id + " | " + "3 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "190"; item.Codigo = "190"; item.Id = "190"; item.Descripcion = item.Id + " | " + "3 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "195"; item.Codigo = "195"; item.Id = "195"; item.Descripcion = item.Id + " | " + "3 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "200"; item.Codigo = "200"; item.Id = "200"; item.Descripcion = item.Id + " | " + "3 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "205"; item.Codigo = "205"; item.Id = "205"; item.Descripcion = item.Id + " | " + "3 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "210"; item.Codigo = "210"; item.Id = "210"; item.Descripcion = item.Id + " | " + "3 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "215"; item.Codigo = "215"; item.Id = "215"; item.Descripcion = item.Id + " | " + "3 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "220"; item.Codigo = "220"; item.Id = "220"; item.Descripcion = item.Id + " | " + "3 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "225"; item.Codigo = "225"; item.Id = "225"; item.Descripcion = item.Id + " | " + "3 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "230"; item.Codigo = "230"; item.Id = "230"; item.Descripcion = item.Id + " | " + "3 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "235"; item.Codigo = "235"; item.Id = "235"; item.Descripcion = item.Id + " | " + "3 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "240"; item.Codigo = "240"; item.Id = "240"; item.Descripcion = item.Id + " | " + "4 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "245"; item.Codigo = "245"; item.Id = "245"; item.Descripcion = item.Id + " | " + "4 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "250"; item.Codigo = "250"; item.Id = "250"; item.Descripcion = item.Id + " | " + "4 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "255"; item.Codigo = "255"; item.Id = "255"; item.Descripcion = item.Id + " | " + "4 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "260"; item.Codigo = "260"; item.Id = "260"; item.Descripcion = item.Id + " | " + "4 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "265"; item.Codigo = "265"; item.Id = "265"; item.Descripcion = item.Id + " | " + "4 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "270"; item.Codigo = "270"; item.Id = "270"; item.Descripcion = item.Id + " | " + "4 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "275"; item.Codigo = "275"; item.Id = "275"; item.Descripcion = item.Id + " | " + "4 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "280"; item.Codigo = "280"; item.Id = "280"; item.Descripcion = item.Id + " | " + "4 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "285"; item.Codigo = "285"; item.Id = "285"; item.Descripcion = item.Id + " | " + "4 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "290"; item.Codigo = "290"; item.Id = "290"; item.Descripcion = item.Id + " | " + "4 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "295"; item.Codigo = "295"; item.Id = "295"; item.Descripcion = item.Id + " | " + "4 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "300"; item.Codigo = "300"; item.Id = "300"; item.Descripcion = item.Id + " | " + "5 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "305"; item.Codigo = "305"; item.Id = "305"; item.Descripcion = item.Id + " | " + "5 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "310"; item.Codigo = "310"; item.Id = "310"; item.Descripcion = item.Id + " | " + "5 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "315"; item.Codigo = "315"; item.Id = "315"; item.Descripcion = item.Id + " | " + "5 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "320"; item.Codigo = "320"; item.Id = "320"; item.Descripcion = item.Id + " | " + "5 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "325"; item.Codigo = "325"; item.Id = "325"; item.Descripcion = item.Id + " | " + "5 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "330"; item.Codigo = "330"; item.Id = "330"; item.Descripcion = item.Id + " | " + "5 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "335"; item.Codigo = "335"; item.Id = "335"; item.Descripcion = item.Id + " | " + "5 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "340"; item.Codigo = "340"; item.Id = "340"; item.Descripcion = item.Id + " | " + "5 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "345"; item.Codigo = "345"; item.Id = "345"; item.Descripcion = item.Id + " | " + "5 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "350"; item.Codigo = "350"; item.Id = "350"; item.Descripcion = item.Id + " | " + "5 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "355"; item.Codigo = "355"; item.Id = "355"; item.Descripcion = item.Id + " | " + "5 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "360"; item.Codigo = "360"; item.Id = "360"; item.Descripcion = item.Id + " | " + "6 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "365"; item.Codigo = "365"; item.Id = "365"; item.Descripcion = item.Id + " | " + "6 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "370"; item.Codigo = "370"; item.Id = "370"; item.Descripcion = item.Id + " | " + "6 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "375"; item.Codigo = "375"; item.Id = "375"; item.Descripcion = item.Id + " | " + "6 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "380"; item.Codigo = "380"; item.Id = "380"; item.Descripcion = item.Id + " | " + "6 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "385"; item.Codigo = "385"; item.Id = "385"; item.Descripcion = item.Id + " | " + "6 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "390"; item.Codigo = "390"; item.Id = "390"; item.Descripcion = item.Id + " | " + "6 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "395"; item.Codigo = "395"; item.Id = "395"; item.Descripcion = item.Id + " | " + "6 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "400"; item.Codigo = "400"; item.Id = "400"; item.Descripcion = item.Id + " | " + "6 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "405"; item.Codigo = "405"; item.Id = "405"; item.Descripcion = item.Id + " | " + "6 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "410"; item.Codigo = "410"; item.Id = "410"; item.Descripcion = item.Id + " | " + "6 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "415"; item.Codigo = "415"; item.Id = "415"; item.Descripcion = item.Id + " | " + "6 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "420"; item.Codigo = "420"; item.Id = "420"; item.Descripcion = item.Id + " | " + "7 Horas con 0 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "425"; item.Codigo = "425"; item.Id = "425"; item.Descripcion = item.Id + " | " + "7 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "430"; item.Codigo = "430"; item.Id = "430"; item.Descripcion = item.Id + " | " + "7 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "435"; item.Codigo = "435"; item.Id = "435"; item.Descripcion = item.Id + " | " + "7 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "440"; item.Codigo = "440"; item.Id = "440"; item.Descripcion = item.Id + " | " + "7 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "445"; item.Codigo = "445"; item.Id = "445"; item.Descripcion = item.Id + " | " + "7 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "450"; item.Codigo = "450"; item.Id = "450"; item.Descripcion = item.Id + " | " + "7 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "455"; item.Codigo = "455"; item.Id = "455"; item.Descripcion = item.Id + " | " + "7 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "460"; item.Codigo = "460"; item.Id = "460"; item.Descripcion = item.Id + " | " + "7 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "465"; item.Codigo = "465"; item.Id = "465"; item.Descripcion = item.Id + " | " + "7 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "470"; item.Codigo = "470"; item.Id = "470"; item.Descripcion = item.Id + " | " + "7 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "475"; item.Codigo = "475"; item.Id = "475"; item.Descripcion = item.Id + " | " + "7 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "480"; item.Codigo = "480"; item.Id = "480"; item.Descripcion = item.Id + " | " + "7 Horas con 60 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "485"; item.Codigo = "485"; item.Id = "485"; item.Descripcion = item.Id + " | " + "8 Horas con 5 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "490"; item.Codigo = "490"; item.Id = "490"; item.Descripcion = item.Id + " | " + "8 Horas con 10 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "495"; item.Codigo = "495"; item.Id = "495"; item.Descripcion = item.Id + " | " + "8 Horas con 15 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "500"; item.Codigo = "500"; item.Id = "500"; item.Descripcion = item.Id + " | " + "8 Horas con 20 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "505"; item.Codigo = "505"; item.Id = "505"; item.Descripcion = item.Id + " | " + "8 Horas con 25 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "510"; item.Codigo = "510"; item.Id = "510"; item.Descripcion = item.Id + " | " + "8 Horas con 30 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "515"; item.Codigo = "515"; item.Id = "515"; item.Descripcion = item.Id + " | " + "8 Horas con 35 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "520"; item.Codigo = "520"; item.Id = "520"; item.Descripcion = item.Id + " | " + "8 Horas con 40 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "525"; item.Codigo = "525"; item.Id = "525"; item.Descripcion = item.Id + " | " + "8 Horas con 45 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "530"; item.Codigo = "530"; item.Id = "530"; item.Descripcion = item.Id + " | " + "8 Horas con 50 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "535"; item.Codigo = "535"; item.Id = "535"; item.Descripcion = item.Id + " | " + "8 Horas con 55 minutos "; listado.Add(item);
            item = new Grupo(); item.Valor = "540"; item.Codigo = "540"; item.Id = "540"; item.Descripcion = item.Id + " | " + "9 Horas con 0 minutos "; listado.Add(item);

            return listado;
        }

        internal List<Grupo> MinutosActivosPartesEquipamiento(string conection)
        {
            Grupo item = new Grupo();
            List<Grupo> listado = new List<Grupo>();
            item = new Grupo(); item.Valor = "0"; item.Codigo = "0"; item.Id = "0"; item.Descripcion = item.Id + " | " + "0 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "30"; item.Codigo = "30"; item.Id = "30"; item.Descripcion = item.Id + " | " + "0 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "60"; item.Codigo = "60"; item.Id = "60"; item.Descripcion = item.Id + " | " + "1 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "90"; item.Codigo = "90"; item.Id = "90"; item.Descripcion = item.Id + " | " + "1 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "120"; item.Codigo = "120"; item.Id = "120"; item.Descripcion = item.Id + " | " + "2 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "150"; item.Codigo = "150"; item.Id = "150"; item.Descripcion = item.Id + " | " + "2 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "180"; item.Codigo = "180"; item.Id = "180"; item.Descripcion = item.Id + " | " + "3 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "210"; item.Codigo = "210"; item.Id = "210"; item.Descripcion = item.Id + " | " + "3 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "240"; item.Codigo = "240"; item.Id = "240"; item.Descripcion = item.Id + " | " + "4 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "270"; item.Codigo = "270"; item.Id = "270"; item.Descripcion = item.Id + " | " + "4 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "300"; item.Codigo = "300"; item.Id = "300"; item.Descripcion = item.Id + " | " + "5 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "330"; item.Codigo = "330"; item.Id = "330"; item.Descripcion = item.Id + " | " + "5 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "360"; item.Codigo = "360"; item.Id = "360"; item.Descripcion = item.Id + " | " + "6 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "390"; item.Codigo = "390"; item.Id = "390"; item.Descripcion = item.Id + " | " + "6 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "420"; item.Codigo = "420"; item.Id = "420"; item.Descripcion = item.Id + " | " + "7 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "450"; item.Codigo = "450"; item.Id = "450"; item.Descripcion = item.Id + " | " + "7 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "480"; item.Codigo = "480"; item.Id = "480"; item.Descripcion = item.Id + " | " + "8 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "510"; item.Codigo = "510"; item.Id = "510"; item.Descripcion = item.Id + " | " + "8 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "540"; item.Codigo = "540"; item.Id = "540"; item.Descripcion = item.Id + " | " + "9 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "570"; item.Codigo = "570"; item.Id = "570"; item.Descripcion = item.Id + " | " + "9 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "600"; item.Codigo = "600"; item.Id = "600"; item.Descripcion = item.Id + " | " + "10 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "630"; item.Codigo = "630"; item.Id = "630"; item.Descripcion = item.Id + " | " + "10 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "660"; item.Codigo = "660"; item.Id = "660"; item.Descripcion = item.Id + " | " + "11 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "690"; item.Codigo = "690"; item.Id = "690"; item.Descripcion = item.Id + " | " + "11 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "720"; item.Codigo = "720"; item.Id = "720"; item.Descripcion = item.Id + " | " + "12 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "750"; item.Codigo = "750"; item.Id = "750"; item.Descripcion = item.Id + " | " + "12 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "780"; item.Codigo = "780"; item.Id = "780"; item.Descripcion = item.Id + " | " + "13 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "810"; item.Codigo = "810"; item.Id = "810"; item.Descripcion = item.Id + " | " + "13 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "840"; item.Codigo = "840"; item.Id = "840"; item.Descripcion = item.Id + " | " + "14 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "870"; item.Codigo = "870"; item.Id = "870"; item.Descripcion = item.Id + " | " + "14 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "900"; item.Codigo = "900"; item.Id = "900"; item.Descripcion = item.Id + " | " + "15 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "930"; item.Codigo = "930"; item.Id = "930"; item.Descripcion = item.Id + " | " + "15 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "960"; item.Codigo = "960"; item.Id = "960"; item.Descripcion = item.Id + " | " + "16 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "990"; item.Codigo = "990"; item.Id = "990"; item.Descripcion = item.Id + " | " + "16 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1020"; item.Codigo = "1020"; item.Id = "1020"; item.Descripcion = item.Id + " | " + "17 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1050"; item.Codigo = "1050"; item.Id = "1050"; item.Descripcion = item.Id + " | " + "17 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1080"; item.Codigo = "1080"; item.Id = "1080"; item.Descripcion = item.Id + " | " + "18 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1110"; item.Codigo = "1110"; item.Id = "1110"; item.Descripcion = item.Id + " | " + "18 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1140"; item.Codigo = "1140"; item.Id = "1140"; item.Descripcion = item.Id + " | " + "19 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1170"; item.Codigo = "1170"; item.Id = "1170"; item.Descripcion = item.Id + " | " + "19 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1200"; item.Codigo = "1200"; item.Id = "1200"; item.Descripcion = item.Id + " | " + "20 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1230"; item.Codigo = "1230"; item.Id = "1230"; item.Descripcion = item.Id + " | " + "20 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1260"; item.Codigo = "1260"; item.Id = "1260"; item.Descripcion = item.Id + " | " + "21 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1290"; item.Codigo = "1290"; item.Id = "1290"; item.Descripcion = item.Id + " | " + "21 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1320"; item.Codigo = "1320"; item.Id = "1320"; item.Descripcion = item.Id + " | " + "22 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1350"; item.Codigo = "1350"; item.Id = "1350"; item.Descripcion = item.Id + " | " + "22 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1380"; item.Codigo = "1380"; item.Id = "1380"; item.Descripcion = item.Id + " | " + "23 Horas"; listado.Add(item);
            item = new Grupo(); item.Valor = "1410"; item.Codigo = "1410"; item.Id = "1410"; item.Descripcion = item.Id + " | " + "23 Horas y media"; listado.Add(item);
            item = new Grupo(); item.Valor = "1440"; item.Codigo = "1440"; item.Id = "1440"; item.Descripcion = item.Id + " | " + "24 Horas"; listado.Add(item);
            return listado;
        }


        internal List<DFormatoSimple> HorasActivosPartesEquipamiento(string conection)
        {
            DFormatoSimple item = new DFormatoSimple();
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            item = new DFormatoSimple(); item.Codigo = "0.5"; item.Descripcion = "Media Hora"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "1"; item.Descripcion = "Una Hora"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "1.5"; item.Descripcion = "Una Hora y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "2"; item.Descripcion = "Dos Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "2.5"; item.Descripcion = "Dos Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "3"; item.Descripcion = "Tres Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "3.5"; item.Descripcion = "Tres Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "4"; item.Descripcion = "Cuatro Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "4.5"; item.Descripcion = "Cuatro Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "5"; item.Descripcion = "Cinco Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "5.5"; item.Descripcion = "Cinco Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "6"; item.Descripcion = "Seis Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "6.5"; item.Descripcion = "Seis Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "7"; item.Descripcion = "Siete Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "7.5"; item.Descripcion = "Siete Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "8"; item.Descripcion = "Ocho Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "8.5"; item.Descripcion = "Ocho Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "9"; item.Descripcion = "Nueve Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "9.5"; item.Descripcion = "Nueve Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "10"; item.Descripcion = "Diez Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "10.5"; item.Descripcion = "Diez Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "11"; item.Descripcion = "Once Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "11.5"; item.Descripcion = "Once Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "12"; item.Descripcion = "Doce Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "12.5"; item.Descripcion = "Doce Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "13"; item.Descripcion = "Trece Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "13.5"; item.Descripcion = "Trece Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "14"; item.Descripcion = "Catorce Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "14.5"; item.Descripcion = "Catorce Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "15"; item.Descripcion = "Quince Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "15.5"; item.Descripcion = "Dieciséis Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "16"; item.Descripcion = "Diez Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "16.5"; item.Descripcion = "Dieciséis Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "17"; item.Descripcion = "Diecisiete Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "17.5"; item.Descripcion = "Diecisiete Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "18"; item.Descripcion = "Dieciocho Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "18.5"; item.Descripcion = "Dieciocho Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "19"; item.Descripcion = "Diecinueve Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "19.5"; item.Descripcion = "Diecinueve Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "20"; item.Descripcion = "Veinte Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "20.5"; item.Descripcion = "Veinte Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "21"; item.Descripcion = "Veintiuno Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "21.5"; item.Descripcion = "Veintiuno Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "22"; item.Descripcion = "Veintidos Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "22.5"; item.Descripcion = "Veintidos Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "23"; item.Descripcion = "Veintitres Horas"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "23.5"; item.Descripcion = "Veintitres Horas y media"; listado.Add(item);
            item = new DFormatoSimple(); item.Codigo = "24"; item.Descripcion = "Veinticuatro Horas"; listado.Add(item);
            return listado;
        }


        internal List<DFormatoSimple> ObtenerListadoDeMotivosDeInactividad(string conection)
        {
            List<DFormatoSimple> resultado = new List<DFormatoSimple>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado01 = Modelo.SAS_ParteDiariosDeDispositivosMotivoInactivo.Where(x => x.Estado == 1).ToList();
                resultado = (from items in resultado01.ToList()
                             group items by new { items.Codigo } into j
                             select new DFormatoSimple
                             {
                                 Codigo = j.Key.Codigo.ToString(),
                                 Descripcion = j.FirstOrDefault().Descripcion
                             }).ToList();

            }
            return resultado;
        }


        internal List<DFormatoSimple> ObtenerListadoTipoDeDispositivos(string conection)
        {
            List<DFormatoSimple> resultado = new List<DFormatoSimple>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado01 = Modelo.SAS_DispositivoTipoDispositivo.Where(x => x.enFormatoSolicitud == 1).ToList();
                resultado = (from items in resultado01.ToList()
                             group items by new { items.id } into j
                             select new DFormatoSimple
                             {
                                 Codigo = j.Key.id.ToString(),
                                 Descripcion = j.FirstOrDefault().descripcion.Trim().ToUpper()
                             }).ToList();

            }
            return resultado;
        }

    }
}

