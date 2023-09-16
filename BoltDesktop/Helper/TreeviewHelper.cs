using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Asistencia.Helper
{
    public class TreeviewHelper
    {
        public RadTreeView BuildTreeViewForms(RadTreeView myTreeview, List<SAS_FormularioSistema> listFormsQuery)
        {
            myTreeview.Nodes.Clear();
            List<SAS_FormularioSistema> listadoAgregados = new List<SAS_FormularioSistema>();
            if (listFormsQuery != null && listFormsQuery.ToList().Count > 1)
            {
                #region Generar listado para evaluar nivel 01 (Módulos)
                // obtener sólo módulos y agregarlo
                var resultListNivel01 = (from item in listFormsQuery
                                         where item.EsModuloPrincipal == 1
                                         group item by new { item.moduloCodigo } into j
                                         select new
                                         {
                                             Id = j.Key.moduloCodigo.Trim(),
                                             Description = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                                             idFormulario = j.FirstOrDefault().formularioCodigo != null ? j.FirstOrDefault().formularioCodigo.Trim() : string.Empty,
                                         }
                           ).OrderBy(x => x.Id).ToList();
                #endregion

                if (resultListNivel01 != null && resultListNivel01.OrderBy(x => x.Id).ToList().Count > 0)
                {
                    #region Algotirmo para nivel 01, 02, 03, 04, 05() 
                    foreach (var itemNivel01 in resultListNivel01.OrderBy(x => x.Id).ToList())
                    {
                        #region Registro Nivel 01 y Apertura nivel 02()    
                        #region Agregar a lista el nivel 01 y al arbol también()
                        RadTreeNode nivel01 = myTreeview.Nodes.Add(itemNivel01.idFormulario.Trim(), itemNivel01.Description.Trim(), 1);
                        listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel01.idFormulario.Trim() });
                        #endregion

                        // obtener si este módulo tiene hijos.
                        #region Consultar si hay nivel de detalle para el nivel 02
                        var resultListNivel02 = listFormsQuery.Where(x => x.moduloCodigo.Trim() == itemNivel01.Id.Trim() && x.EsModuloPrincipal == 0).ToList();
                        if (resultListNivel02 != null)
                        {
                            if (resultListNivel02.ToList().Count > 0)
                            {
                                var resultListNivel02Detail = (from item in resultListNivel02
                                                               group item by new { item.formularioCodigo } into j
                                                               select new
                                                               {
                                                                   IdSubForm = j.Key.formularioCodigo.Trim(),
                                                                   Description = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                                                                   Jerarquia = j.FirstOrDefault().Jerarquia.Trim(),
                                                               }).OrderBy(x => x.Jerarquia).ToList();

                                #region Condicional para buscar hijos del codigo del formulario en evaluación() - Hijos del módulo(Catálogo, Movimiento, Reportes, Procesos)
                                if (resultListNivel02Detail != null && resultListNivel02Detail.ToList().Count > 0)
                                {
                                    #region 
                                    foreach (var itemNivel02 in resultListNivel02Detail)
                                    {
                                        #region 
                                        string nombreFormularioNivel02 = itemNivel02.Description.Trim();
                                        string CodigoFormularioNivel02 = itemNivel02.IdSubForm.Trim();

                                        // Consulto si el código del formulario en evaluación no este en la lista de formulario agregado al árbol
                                        if (listadoAgregados.Where(x => x.formularioCodigo == itemNivel02.IdSubForm).ToList().Count == 0)
                                        {
                                            #region 
                                            // consulto si el nivel 02 tiene hijos para ver si lo agrego como final o como padre
                                            var resultListNivel03 = listFormsQuery.Where(x => x.barraPadre.Trim() == itemNivel02.Jerarquia.Trim()).OrderBy(x => x.Jerarquia).ToList();

                                            // si no tiene hijos se agrega como final en el nivel 02
                                            if (resultListNivel03 != null && resultListNivel03.ToList().Count == 0)
                                            {
                                                #region Registro en Nivel 02
                                                if (listadoAgregados.Where(x => x.formularioCodigo.Trim() == (itemNivel02.IdSubForm.Trim())).ToList().Count == 0)
                                                {
                                                    RadTreeNode nivel02 = nivel01.Nodes.Add(itemNivel02.IdSubForm.Trim(), itemNivel02.Description.Trim(), 3);
                                                    listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel02.IdSubForm.Trim() });
                                                }
                                                #endregion
                                            }
                                            // si tiene hijos, lo agrego como padre en el nivel 02
                                            else if (resultListNivel03 != null && resultListNivel03.ToList().Count > 0)
                                            {
                                                #region Registro de Nivel 02 y apertura para evaluar nivel 03()                                                                         
                                                #region Registro en Nivel 02
                                                RadTreeNode nivel02 = nivel01.Nodes.Add(itemNivel02.IdSubForm.Trim(), itemNivel02.Description.Trim(), 2);
                                                listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel02.IdSubForm.Trim() });
                                                #endregion

                                                #region Condicional para buscar hijos del nivel 02, es decir hijos de los catálogos, movimientos, reportes, procesos
                                                //root.Nodes.Add(folderSub01);
                                                var resultListNivel03Detail = (from item in resultListNivel03
                                                                               group item by new { item.formularioCodigo } into j
                                                                               select new
                                                                               {
                                                                                   IdSubForm03 = j.Key.formularioCodigo,
                                                                                   Description03 = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                                                                                   Jerarquia03 = j.FirstOrDefault().Jerarquia != null ? j.FirstOrDefault().Jerarquia.Trim() : string.Empty,
                                                                               }).ToList();

                                                if (resultListNivel03Detail != null)
                                                {
                                                    if (resultListNivel03Detail.ToList().Count > 0)
                                                    {
                                                        #region Agrupar por sub nivel 02.01
                                                        foreach (var itemNivel03 in resultListNivel03Detail)
                                                        {
                                                            var nombreFormularioNivel03 = itemNivel03.Description03.Trim();
                                                            var CodigoFormularioNivel03 = itemNivel03.IdSubForm03.Trim();

                                                            if (listadoAgregados.Where(x => x.formularioCodigo == itemNivel03.IdSubForm03).ToList().Count == 0)
                                                            {
                                                                var resultListNivel04 = listFormsQuery.Where(x => x.barraPadre.Trim() == itemNivel03.Jerarquia03.Trim()).OrderBy(x => x.Jerarquia).ToList();
                                                                if (listadoAgregados.Where(x => x.formularioCodigo.Trim() == (itemNivel03.IdSubForm03.Trim())).ToList().Count == 0)
                                                                {
                                                                    #region Registro de Nivel 03 y apertura para evaluar nivel 04() 
                                                                    // el el subnivel tiene más subniveles entonces verifico
                                                                    if (resultListNivel04 != null && resultListNivel04.ToList().Count == 0)
                                                                    {
                                                                        #region Agregar a Nivel 03
                                                                        RadTreeNode nivel03 = nivel02.Nodes.Add(itemNivel03.IdSubForm03.Trim(), itemNivel03.Description03.Trim(), 3);
                                                                        listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel03.IdSubForm03.Trim() });
                                                                        #endregion
                                                                    }
                                                                    else if (resultListNivel04 != null && resultListNivel04.ToList().Count > 0)
                                                                    {
                                                                        #region Agregar Nivel 03 y empezar el 04
                                                                        if (listadoAgregados.Where(x => x.formularioCodigo.Trim() == (itemNivel03.IdSubForm03.Trim())).ToList().Count == 0)
                                                                        {
                                                                            RadTreeNode nivel03 = nivel02.Nodes.Add(itemNivel03.IdSubForm03.Trim(), itemNivel03.Description03.Trim(), 2);
                                                                            listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel03.IdSubForm03.Trim() });

                                                                            #region Empezando el Nivel 04


                                                                            var resultListNivel04Detail = (from item in resultListNivel04
                                                                                                           group item by new { item.formularioCodigo } into j
                                                                                                           select new
                                                                                                           {
                                                                                                               IdSubForm04 = j.Key.formularioCodigo,
                                                                                                               Description04 = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                                                                                                               Jerarquia04 = j.FirstOrDefault().Jerarquia != null ? j.FirstOrDefault().Jerarquia.Trim() : string.Empty,
                                                                                                           }).ToList();

                                                                            if (resultListNivel04Detail != null)
                                                                            {
                                                                                if (resultListNivel04Detail.ToList().Count > 0)
                                                                                {
                                                                                    foreach (var itemNivel04 in resultListNivel04Detail)
                                                                                    {
                                                                                        #region Nivel 03.01
                                                                                        var nombreFormularioNivel04 = itemNivel04.Description04.Trim();
                                                                                        var CodigoFormularioNivel04 = itemNivel04.IdSubForm04.Trim();

                                                                                        #region
                                                                                        if (listadoAgregados.Where(x => x.formularioCodigo.Trim() == (itemNivel04.IdSubForm04.Trim())).ToList().Count == 0)
                                                                                        {
                                                                                            var resultListNivel05 = listFormsQuery.Where(x => x.barraPadre.Trim() == itemNivel04.Jerarquia04.Trim()).OrderBy(x => x.Jerarquia).ToList();

                                                                                            if (resultListNivel05 != null && resultListNivel05.ToList().Count == 0)
                                                                                            {

                                                                                                RadTreeNode nivel04 = nivel03.Nodes.Add(itemNivel04.IdSubForm04.Trim(), itemNivel04.Description04.Trim(), 3);
                                                                                                listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel04.IdSubForm04.Trim() });

                                                                                            }
                                                                                            else if (resultListNivel05 != null && resultListNivel05.ToList().Count > 0)
                                                                                            {

                                                                                                RadTreeNode nivel04 = nivel03.Nodes.Add(itemNivel04.IdSubForm04.Trim(), itemNivel04.Description04.Trim(), 2);
                                                                                                listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel04.IdSubForm04.Trim() });

                                                                                                var resultListNivel05Detail = (from item in resultListNivel05
                                                                                                                               group item by new { item.formularioCodigo } into j
                                                                                                                               select new
                                                                                                                               {
                                                                                                                                   IdSubForm05 = j.Key.formularioCodigo,
                                                                                                                                   Description05 = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                                                                                                                                   Jerarquia05 = j.FirstOrDefault().Jerarquia != null ? j.FirstOrDefault().Jerarquia.Trim() : string.Empty,
                                                                                                                               }).ToList();

                                                                                                if (resultListNivel05Detail != null)
                                                                                                {
                                                                                                    if (resultListNivel05Detail.ToList().Count > 0)
                                                                                                    {
                                                                                                        foreach (var itemNivel05 in resultListNivel05Detail)
                                                                                                        {
                                                                                                            #region Registrando Nivel 05()
                                                                                                            var nombreFormularioNivel05 = itemNivel05.Description05.Trim();
                                                                                                            var CodigoFormularioNivel05 = itemNivel05.IdSubForm05.Trim();

                                                                                                            var resultListNivel06 = listFormsQuery.Where(x => x.barraPadre.Trim() == itemNivel05.Jerarquia05.Trim()).OrderBy(x => x.Jerarquia).ToList();
                                                                                                            if (listadoAgregados.Where(x => x.formularioCodigo.Trim() == (itemNivel05.IdSubForm05.Trim())).ToList().Count == 0)
                                                                                                            {
                                                                                                                
                                                                                                                if (resultListNivel06.ToList().Count == 0)
                                                                                                                {
                                                                                                                    RadTreeNode nivel05 = nivel04.Nodes.Add(itemNivel05.IdSubForm05.Trim(), itemNivel05.Description05.Trim(), 3);
                                                                                                                    listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel05.IdSubForm05.Trim() });
                                                                                                                }
                                                                                                                else if (resultListNivel06.ToList().Count > 0)
                                                                                                                {
                                                                                                                    RadTreeNode nivel05 = nivel04.Nodes.Add(itemNivel05.IdSubForm05.Trim(), itemNivel05.Description05.Trim(), 2);
                                                                                                                    listadoAgregados.Add(new SAS_FormularioSistema { formularioCodigo = itemNivel05.IdSubForm05.Trim() });
                                                                                                                }
                                                                                                            }
                                                                                                            #endregion
                                                                                                        }
                                                                                                    }


                                                                                                }
                                                                                            }
                                                                                        }
                                                                                        #endregion
                                                                                        #endregion
                                                                                    }
                                                                                }
                                                                            }
                                                                            #endregion

                                                                        }
                                                                        #endregion
                                                                    }
                                                                }
                                                                #endregion

                                                            }

                                                        }
                                                        #endregion
                                                    }
                                                }
                                                #endregion
                                                #endregion

                                            }
                                            else
                                            {

                                            }

                                            #endregion
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                        }

                        #endregion



                        #endregion
                    }
                    #endregion
                }
            }
            return myTreeview;
        }


        private void AddNode(RadTreeNodeCollection nodes, RadTreeView myTreeview)
        {
            RadTreeNode newNode = new RadTreeNode();
            newNode.Text = "RadTreeNode";
            nodes.Add(newNode);
            // this.myTreeview.SelectedNode = newNode;
        }

    }
}