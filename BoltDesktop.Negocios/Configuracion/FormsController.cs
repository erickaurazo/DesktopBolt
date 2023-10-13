using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asistencia.Datos;
using System.Configuration;

namespace Asistencia.Negocios
{
    public class FormsController
    {
        public List<FormularioSistema> GetListForms(string conection)
        {
            var result = new List<FormularioSistema>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                result = Modelo.FormularioSistema.ToList();
            }
            return result;
        }


        public List<SAS_FormularioSistema> ObtenerListadoFormularios(string conection)
        {
            var result = new List<SAS_FormularioSistema>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["SAS"].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                //result = Modelo.SAS_FormularioSistema.Where(x=> x.moduloCodigo == "004").ToList();
                //                result = Modelo.SAS_FormularioSistema.Where(x => x.moduloCodigo == "004" && x.estado == 1 ).OrderBy(x=> x.Jerarquia).ToList();
                result = Modelo.SAS_FormularioSistema.OrderBy(x => x.Jerarquia).ToList();
            }
            return result;
        }


        public bool ToRegister(string conection, FormularioSistema form)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                var result = Modelo.FormularioSistema.Where(x => x.formularioCodigo.Trim() == form.formularioCodigo.Trim()).ToList();

                if (result != null && result.ToList().Count == 0)
                {
                    int maxResult = (
                        Modelo.FormularioSistema.ToList().Count > 0 ?
                        Convert.ToInt32(Modelo.FormularioSistema.ToList().Max(x => x.formularioCodigo.Trim()))
                        : 0
                        ) + 1;
                    #region Add() 
                    FormularioSistema oForm = new FormularioSistema();
                    oForm.formularioCodigo = maxResult.ToString().PadLeft(3, '0');
                    oForm.moduloCodigo = form.Jerarquia.Substring(0, 3);
                    oForm.descripcion = form.descripcion != null ? form.descripcion.Trim() : string.Empty;
                    oForm.nombreEnSistema = form.nombreEnSistema != null ? form.nombreEnSistema.Trim() : string.Empty;
                    oForm.estado = 1;
                    oForm.EsModuloPrincipal = 0;
                    oForm.Jerarquia = GetJerarquía(conection, form.Jerarquia);
                    oForm.formulario = form.formulario != null ? form.formulario.Trim() : string.Empty;
                    oForm.barraPadre = form.Jerarquia != null ? form.Jerarquia.Trim() : string.Empty;

                    Modelo.FormularioSistema.InsertOnSubmit(oForm);
                    Modelo.SubmitChanges();
                    status = true;
                    #endregion
                }
                else if (result != null && result.ToList().Count == 1)
                {
                    #region Update() 
                    FormularioSistema oForm = new FormularioSistema();
                    oForm = result.Single();
                    oForm.descripcion = form.descripcion.Trim();
                    oForm.nombreEnSistema = form.nombreEnSistema.Trim();
                    Modelo.SubmitChanges();
                    status = true;
                    #endregion
                }
            }

            return status;
        }

        public bool Agregar(string conection, FormularioSistema form)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings["SAS"].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_FormularioSistema.Where(x => x.formularioCodigo.Trim() == form.formularioCodigo.Trim()).ToList();

                if (result != null && result.ToList().Count == 0)
                {
                    int maxResult = (
                        Modelo.SAS_FormularioSistema.ToList().Count > 0 ?
                        Convert.ToInt32(Modelo.SAS_FormularioSistema.ToList().Max(x => x.formularioCodigo.Trim()))
                        : 0
                        ) + 1;
                    #region Add() 
                    SAS_FormularioSistema oForm = new SAS_FormularioSistema();
                    oForm.formularioCodigo = maxResult.ToString().PadLeft(3, '0');
                    oForm.moduloCodigo = form.Jerarquia.Substring(0, 3);
                    oForm.descripcion = form.descripcion != null ? form.descripcion.Trim() : string.Empty;
                    oForm.nombreEnSistema = form.nombreEnSistema != null ? form.nombreEnSistema.Trim() : string.Empty;
                    oForm.estado = 1;
                    oForm.EsModuloPrincipal = 0;
                    oForm.Jerarquia = GetJerarquía(conection, form.Jerarquia);
                    oForm.formulario = form.formulario != null ? form.formulario.Trim() : string.Empty;
                    oForm.barraPadre = form.Jerarquia != null ? form.Jerarquia.Trim() : string.Empty;

                    Modelo.SAS_FormularioSistema.InsertOnSubmit(oForm);
                    Modelo.SubmitChanges();
                    status = true;
                    #endregion
                }
                else if (result != null && result.ToList().Count == 1)
                {
                    #region Update() 
                    SAS_FormularioSistema oForm = new SAS_FormularioSistema();
                    oForm = result.Single();
                    oForm.descripcion = form.descripcion.Trim();
                    oForm.nombreEnSistema = form.nombreEnSistema.Trim();
                    Modelo.SubmitChanges();
                    status = true;
                    #endregion
                }
            }

            return status;
        }



        private string GetJerarquía(string conection, string jerarquia)
        {
            string cnx, hierarchy = string.Empty;            
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                //maxResult = (
                //    Modelo.FormularioSistema.ToList().Count > 0 ? 
                //    Convert.ToInt32(Modelo.FormularioSistema.Where(x => x.Jerarquia.Trim() == jerarquia).Max(x => x.formularioCodigo.Substring(1,jerarquia.Length))) 
                //    : 0) 
                //    + 1;
                var result = Modelo.FormularioSistema.Where(x => x.barraPadre.Trim() == jerarquia.Trim()).ToList();
                if (result.Count == 0)
                {
                    hierarchy = jerarquia.Trim() + "." + "001";
                }
                else if (result.Count > 0)
                {
                    var aa = result.Max(x => x.Jerarquia.Trim()).Trim();
                    int lenHasta = aa.Length;
                    int lenDesde = jerarquia.Trim().Length;
                    int lenDif = lenHasta - lenDesde;
                    var NewString = aa.Substring(lenDesde, lenDif).TrimStart('.');
                    int ff = Convert.ToInt32(NewString) + 1;
                    hierarchy = jerarquia.Trim() + "." + ff.ToString().PadLeft(3, '0');
                }

            }


            return hierarchy;
        }

        private string ObtenerJerarquía(string conection, string jerarquia)
        {
            string cnx, hierarchy = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                //maxResult = (
                //    Modelo.FormularioSistema.ToList().Count > 0 ? 
                //    Convert.ToInt32(Modelo.FormularioSistema.Where(x => x.Jerarquia.Trim() == jerarquia).Max(x => x.formularioCodigo.Substring(1,jerarquia.Length))) 
                //    : 0) 
                //    + 1;
                var result = Modelo.SAS_FormularioSistema.Where(x => x.barraPadre.Trim() == jerarquia.Trim()).ToList();
                if (result.Count == 0)
                {
                    hierarchy = jerarquia.Trim() + "." + "001";
                }
                else if (result.Count > 0)
                {
                    var aa = result.Max(x => x.Jerarquia.Trim()).Trim();
                    int lenHasta = aa.Length;
                    int lenDesde = jerarquia.Trim().Length;
                    int lenDif = lenHasta - lenDesde;
                    var NewString = aa.Substring(lenDesde, lenDif).TrimStart('.');
                    int ff = Convert.ToInt32(NewString) + 1;
                    hierarchy = jerarquia.Trim() + "." + ff.ToString().PadLeft(3, '0');
                }

            }


            return hierarchy;
        }



        public bool Remove(string conection, FormularioSistema form)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_FormularioSistema.Where(x => x.formularioCodigo.Trim() == form.formularioCodigo.Trim()).ToList();

                if (result != null && result.ToList().Count == 1)
                {
                    #region Update() 
                    SAS_FormularioSistema oForm = new SAS_FormularioSistema();
                    oForm = result.Single();
                    if (oForm.formulario.Trim() != "SubMenu" && (oForm.formulario.Trim() != "Menu"))
                    {
                        var privilegesByUser = Modelo.SAS_FormularioSistema.Where(x => x.formularioCodigo.Trim() == form.formularioCodigo.Trim()).ToList();
                        Modelo.SAS_FormularioSistema.DeleteAllOnSubmit(privilegesByUser);
                        Modelo.SAS_FormularioSistema.DeleteOnSubmit(oForm);
                        Modelo.SubmitChanges();
                        status = true;
                    }

                    #endregion
                }
            }

            return status;
        }

        public bool ChangeStatus(string conection, FormularioSistema form)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_FormularioSistema.Where(x => x.formularioCodigo.Trim() == form.formularioCodigo.Trim()).ToList();

                if (result != null && result.ToList().Count == 1)
                {
                    #region Update() 
                    SAS_FormularioSistema oForm = new SAS_FormularioSistema();
                    oForm = result.Single();
                    
                    if (oForm.estado == 1)
                    {
                        oForm.estado = 0;
                    }
                    else
                    {
                        oForm.estado = 1;
                    }

                    Modelo.SubmitChanges();
                    status = true;
                    #endregion
                }
            }

            return status;
        }
    }
}
