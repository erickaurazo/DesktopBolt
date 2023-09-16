using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class ModuloSistemaController
    {

        public bool AddModulo(ModuloSistema modulo, string conection)
        {
            bool estate = false;
            string cnx = string.Empty;
            FormularioSistema formularioSistema = new FormularioSistema();
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (TransactionScope scope = new TransactionScope())
            {
                using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
                {
                    context.CommandTimeout = 999909999;
                    var result = context.ModuloSistema.Where(x => x.moduloCodigo.Trim() == modulo.moduloCodigo.Trim()).ToList();
                    if (result.Count == 0)
                    {
                        #region Add() 
                        //TrimStart('0')
                        int id = (context.ModuloSistema.ToList().Count > 0 ? 
                            Convert.ToInt32(context.ModuloSistema.Max(x => x.moduloCodigo).TrimStart('0')) + 1 :
                            1);
                        ModuloSistema module = new ModuloSistema();
                        module.moduloCodigo = id.ToString().PadLeft(3, '0');
                        module.descripcion = modulo.descripcion != null ? modulo.descripcion : string.Empty;
                        module.abreviatura = modulo.abreviatura != null ? modulo.abreviatura : string.Empty;
                        module.estado = 1;
                        context.ModuloSistema.InsertOnSubmit(module);
                        context.SubmitChanges();

                        int idMenu = (
                            context.FormularioSistema.ToList().Count > 0 ?
                            Convert.ToInt32(context.FormularioSistema.Max(X => X.formularioCodigo).TrimStart('0')) + 1 :
                            1);
                        formularioSistema = new FormularioSistema();
                        formularioSistema.formularioCodigo = idMenu.ToString().PadLeft(3, '0');
                        formularioSistema.moduloCodigo = id.ToString().PadLeft(3, '0');
                        formularioSistema.descripcion = "Modulo | "+ modulo.descripcion;
                        formularioSistema.nombreEnSistema = "Go" + modulo.descripcion;
                        formularioSistema.estado = 1;
                        formularioSistema.EsModuloPrincipal = 1;
                        formularioSistema.Jerarquia = id.ToString().PadLeft(3, '0');
                        formularioSistema.formulario = "Menu";
                        formularioSistema.barraPadre = string.Empty;
                        context.FormularioSistema.InsertOnSubmit(formularioSistema);
                        context.SubmitChanges();

                        #region Agregar tambien al menu principal
                        for (int i = 1; i < 6; i++)
                        {
                            var resultd = context.FormularioSistema.ToList();
                            string aa = resultd.Max(x => x.formularioCodigo).ToString().TrimStart('0');

                            int idSubMenu = (
                                context.FormularioSistema.ToList().Count > 0 ?
                                Convert.ToInt32(context.FormularioSistema.Max(x => x.formularioCodigo).ToString().TrimStart('0')) + 1 :
                                1);
                            formularioSistema = new FormularioSistema();
                            formularioSistema.formularioCodigo = idSubMenu.ToString().PadLeft(3, '0');
                            formularioSistema.moduloCodigo = id.ToString().PadLeft(3, '0');
                            formularioSistema.descripcion = ConvetirNombreSubMenu(i);
                            formularioSistema.nombreEnSistema = "Go" + modulo.descripcion + ConvetirNombreSubMenu(i);
                            formularioSistema.estado = 1;
                            formularioSistema.EsModuloPrincipal = 0;
                            formularioSistema.Jerarquia = id.ToString().PadLeft(3, '0') + '.' + i.ToString().PadLeft(3, '0');
                            formularioSistema.formulario = "subMenu";
                            formularioSistema.barraPadre = id.ToString().PadLeft(3, '0');
                            context.FormularioSistema.InsertOnSubmit(formularioSistema);
                            context.SubmitChanges();
                        }
                        #endregion


                        estate = true;
                        #endregion
                    }
                    else if (result.Count == 1)
                    {
                        #region Update() 
                        ModuloSistema module = new ModuloSistema();
                        module = result.Single();
                        module.descripcion = modulo.descripcion;
                        module.abreviatura = modulo.abreviatura;
                        context.SubmitChanges();
                        estate = true;
                        #endregion
                    }

                }
                scope.Complete();
            }
            return estate;
        }

        public bool ToRegister(ModuloSistema modulo, string conection)
        {
            bool estate = false;
            string cnx = string.Empty;
            FormularioSistema formularioSistema = new FormularioSistema();
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (TransactionScope scope = new TransactionScope())
            {
                using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
                {
                    context.CommandTimeout = 999909999;
                    var result = context.ModuloSistema.Where(x => x.moduloCodigo.Trim() == modulo.moduloCodigo.Trim()).ToList();
                    if (result.Count == 0)
                    {
                        #region Add() 
                        //TrimStart('0')
                        int id = (context.ModuloSistema.ToList().Count > 0 ?
                            Convert.ToInt32(context.ModuloSistema.Max(x => x.moduloCodigo).TrimStart('0')) + 1 :
                            1);
                        ModuloSistema module = new ModuloSistema();
                        module.moduloCodigo = id.ToString().PadLeft(3, '0');
                        module.descripcion = modulo.descripcion != null ? modulo.descripcion : string.Empty;
                        module.abreviatura = modulo.abreviatura != null ? modulo.abreviatura : string.Empty;
                        module.estado = 1;
                        context.ModuloSistema.InsertOnSubmit(module);
                        context.SubmitChanges();

                        int idMenu = (
                            context.FormularioSistema.ToList().Count > 0 ?
                            Convert.ToInt32(context.FormularioSistema.Max(X => X.formularioCodigo).TrimStart('0')) + 1 :
                            1);
                        formularioSistema = new FormularioSistema();
                        formularioSistema.formularioCodigo = idMenu.ToString().PadLeft(3, '0');
                        formularioSistema.moduloCodigo = id.ToString().PadLeft(3, '0');
                        formularioSistema.descripcion = "Modulo | " + modulo.descripcion;
                        formularioSistema.nombreEnSistema = "Go" + modulo.descripcion;
                        formularioSistema.estado = 1;
                        formularioSistema.EsModuloPrincipal = 1;
                        formularioSistema.Jerarquia = id.ToString().PadLeft(3, '0');
                        formularioSistema.formulario = "Menu";
                        formularioSistema.barraPadre = string.Empty;
                        context.FormularioSistema.InsertOnSubmit(formularioSistema);
                        context.SubmitChanges();

                        #region Agregar tambien al menu principal
                        for (int i = 1; i < 6; i++)
                        {
                            var resultd = context.FormularioSistema.ToList();
                            string aa = resultd.Max(x => x.formularioCodigo).ToString().TrimStart('0');

                            int idSubMenu = (
                                context.FormularioSistema.ToList().Count > 0 ?
                                Convert.ToInt32(context.FormularioSistema.Max(x => x.formularioCodigo).ToString().TrimStart('0')) + 1 :
                                1);
                            formularioSistema = new FormularioSistema();
                            formularioSistema.formularioCodigo = idSubMenu.ToString().PadLeft(3, '0');
                            formularioSistema.moduloCodigo = id.ToString().PadLeft(3, '0');
                            formularioSistema.descripcion = ConvetirNombreSubMenu(i);
                            formularioSistema.nombreEnSistema = "Go" + modulo.descripcion + ConvetirNombreSubMenu(i);
                            formularioSistema.estado = 1;
                            formularioSistema.EsModuloPrincipal = 0;
                            formularioSistema.Jerarquia = id.ToString().PadLeft(3, '0') + '.' + i.ToString().PadLeft(3, '0');
                            formularioSistema.formulario = "subMenu";
                            formularioSistema.barraPadre = id.ToString().PadLeft(3, '0');
                            context.FormularioSistema.InsertOnSubmit(formularioSistema);
                            context.SubmitChanges();
                        }
                        #endregion


                        estate = true;
                        #endregion
                    }
                    else if (result.Count == 1)
                    {
                        #region Update() 
                        ModuloSistema module = new ModuloSistema();
                        module = result.Single();
                        module.descripcion = modulo.descripcion;
                        module.abreviatura = modulo.abreviatura;
                        context.SubmitChanges();
                        estate = true;
                        #endregion
                    }

                }
                scope.Complete();
            }
            return estate;
        }


        private string ConvetirNombreSubMenu(int i)
        {
            string nombre = string.Empty;
            switch (i)
            {
                case 1:
                    nombre = "Catalogo";
                    break;

                case 2:
                    nombre = "Movimiento";
                    break;

                case 3:
                    nombre = "Reporte";
                    break;

                case 4:
                    nombre = "Proceso";
                    break;

                case 5:
                    nombre = "Utilitario";
                    break;
                default:
                    break;
            }

            return nombre;
        }

        public bool RemoveModulo(ModuloSistema modulo, string conection)
        {
            bool estate = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
            {
                context.CommandTimeout = 999909999;
            }
            return estate;
        }

        public bool ChangeState(ModuloSistema modulo, string conection)
        {
            bool estate = false;
            string cnx = string.Empty;
            FormularioSistema formularioSistema = new FormularioSistema();
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext context = new BDAsistenciaDataContext(cnx))
            {

                context.CommandTimeout = 999909999;

                var result = context.ModuloSistema.Where(x => x.moduloCodigo.Trim() == modulo.moduloCodigo.Trim()).ToList();
                if (result.Count == 1)
                {
                    #region Change State() 
                    ModuloSistema module = new ModuloSistema();
                    module = result.Single();

                    if (module.estado == 1)
                    {
                        module.estado = 0;
                    }
                    else
                    {
                        module.estado = 1;
                    }
                    context.SubmitChanges();
                    estate = true;
                    #endregion
                }

            }
            return estate;
        }

        public List<ModuleSystem> GetListAll(string conection)
        {
            var result = new List<ModuleSystem>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection != null ? conection : "SAS"].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 999909999;
                result = (from item in Modelo.SAS_ModuloSistema.ToList()
                          where item.moduloCodigo != null
                          group item by new { item.moduloCodigo } into j
                          select new ModuleSystem
                          {
                              moduloCodigo = j.Key.moduloCodigo,
                              descripcion = j.FirstOrDefault().descripcion,
                              abreviatura = j.FirstOrDefault().abreviatura != null ? j.FirstOrDefault().abreviatura : string.Empty,
                              estado = j.FirstOrDefault().estado,
                              estadoDescripcion = j.FirstOrDefault().estado == 1 ? "Activo" : "Anulado"
                          }).ToList();
            }
            return result;
        }

    }
}
