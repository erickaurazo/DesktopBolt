using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_FormatosDeInspeccionCalidadPacking
    {

        #region Formatos de Inspeccion Calidad packing
        public List<SAS_ListadoFormatoInspeccionAll> ListAllForm(string connection)
        {
            List<SAS_ListadoFormatoInspeccionAll> listado = new List<SAS_ListadoFormatoInspeccionAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoFormatoInspeccionAll.OrderBy(x => x.GrupoInspeccion).ThenBy(x => x.FormatoInspeccionCodificacion).ToList();


                var resultSet = listado.OrderBy(x => new { x.GrupoInspeccion, x.FormatoInspeccionCodificacion })
                          .Select(x => x);

            }
            return listado;
        }

        public int ToRegisterForm(string connection, SAS_CALIDAD_FORMULARIO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORMULARIO> listado = new List<SAS_CALIDAD_FORMULARIO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORMULARIO.Where(x => x.idRegistro == item.idRegistro).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORMULARIO oItem = new SAS_CALIDAD_FORMULARIO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem.nombreRegistro = item.nombreRegistro;
                    oItem.codigo = item.codigo;
                    oItem.versionRegistro = item.versionRegistro;
                    oItem.paginas = item.paginas;
                    oItem.idFrecuencia = item.idFrecuencia;
                    oItem.estado = Convert.ToChar("1");
                    oItem.idTipoRegistro = item.idTipoRegistro;
                    oItem.visible = Convert.ToDecimal(1);
                    Modelo.SAS_CALIDAD_FORMULARIO.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idRegistro;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORMULARIO oItem = new SAS_CALIDAD_FORMULARIO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    oItem.nombreRegistro = item.nombreRegistro;
                    oItem.codigo = item.codigo;
                    oItem.versionRegistro = item.versionRegistro;
                    oItem.paginas = item.paginas;
                    oItem.idFrecuencia = item.idFrecuencia;
                    oItem.idTipoRegistro = item.idTipoRegistro;
                    oItem.visible = Convert.ToDecimal(1);
                    //oItem.Estado = item.Estado;                    
                    Modelo.SubmitChanges();
                    resultado = oItem.idRegistro;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusForm(string connection, SAS_CALIDAD_FORMULARIO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORMULARIO> listado = new List<SAS_CALIDAD_FORMULARIO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORMULARIO.Where(x => x.idRegistro == item.idRegistro).ToList();
                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORMULARIO oItem = new SAS_CALIDAD_FORMULARIO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    if (oItem.estado == Convert.ToChar("1"))
                    {
                        oItem.estado = Convert.ToChar("0");
                    }
                    else
                    {
                        oItem.estado = Convert.ToChar("1");
                    }
                    Modelo.SubmitChanges();
                    resultado = oItem.idRegistro;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToDeleteForm(string connection, SAS_CALIDAD_FORMULARIO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORMULARIO> listado = new List<SAS_CALIDAD_FORMULARIO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORMULARIO.Where(x => x.idRegistro == item.idRegistro).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORMULARIO oItem = new SAS_CALIDAD_FORMULARIO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORMULARIO.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idRegistro;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Tipo de Formatos de Inspeccion Calidad packing
        public List<SAS_CALIDAD_TIPO_REGISTRO> ListAllGrupo(string connection)
        {
            List<SAS_CALIDAD_TIPO_REGISTRO> listado = new List<SAS_CALIDAD_TIPO_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_TIPO_REGISTRO.OrderBy(x => x.descripcion).ToList();

            }
            return listado;
        }

        public int ToRegisterGrupo(string connection, SAS_CALIDAD_TIPO_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_TIPO_REGISTRO> listado = new List<SAS_CALIDAD_TIPO_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_TIPO_REGISTRO.Where(x => x.idTipoRegistro == item.idTipoRegistro).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_TIPO_REGISTRO oItem = new SAS_CALIDAD_TIPO_REGISTRO();
                    //oItem.idTipoRegistro = item.idTipoRegistro;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_TIPO_REGISTRO.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idTipoRegistro;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_TIPO_REGISTRO oItem = new SAS_CALIDAD_TIPO_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idTipoRegistro;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusGrupo(string connection, SAS_CALIDAD_TIPO_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_TIPO_REGISTRO> listado = new List<SAS_CALIDAD_TIPO_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_TIPO_REGISTRO.Where(x => x.idTipoRegistro == item.idTipoRegistro).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_TIPO_REGISTRO oItem = new SAS_CALIDAD_TIPO_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    if (oItem.estado == Convert.ToChar("1"))
                    {
                        oItem.estado = Convert.ToChar("0");
                    }
                    else
                    {
                        oItem.estado = Convert.ToChar("1");
                    }
                    Modelo.SubmitChanges();
                    resultado = oItem.idTipoRegistro;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToDeleteGrupo(string connection, SAS_CALIDAD_TIPO_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_TIPO_REGISTRO> listado = new List<SAS_CALIDAD_TIPO_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_TIPO_REGISTRO.Where(x => x.idTipoRegistro == item.idTipoRegistro).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_TIPO_REGISTRO oItem = new SAS_CALIDAD_TIPO_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_TIPO_REGISTRO.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idTipoRegistro;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Frecuencia de Inspeccion Calidad packing
        public List<SAS_CALIDAD_FRECUENCIA_REGISTRO> ListAllFrecuenciaInspeccion(string connection)
        {
            List<SAS_CALIDAD_FRECUENCIA_REGISTRO> listado = new List<SAS_CALIDAD_FRECUENCIA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.OrderBy(x => x.descripcion).ToList();

            }
            return listado;
        }

        public int ToRegisterFrecuenciaInspeccion(string connection, SAS_CALIDAD_FRECUENCIA_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FRECUENCIA_REGISTRO> listado = new List<SAS_CALIDAD_FRECUENCIA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.Where(x => x.idFrecuencia == item.idFrecuencia).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FRECUENCIA_REGISTRO oItem = new SAS_CALIDAD_FRECUENCIA_REGISTRO();
                    //oItem.idTipoRegistro = item.idTipoRegistro;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idFrecuencia;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FRECUENCIA_REGISTRO oItem = new SAS_CALIDAD_FRECUENCIA_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idFrecuencia;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusFrecuenciaInspeccion(string connection, SAS_CALIDAD_FRECUENCIA_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FRECUENCIA_REGISTRO> listado = new List<SAS_CALIDAD_FRECUENCIA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.Where(x => x.idFrecuencia == item.idFrecuencia).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FRECUENCIA_REGISTRO oItem = new SAS_CALIDAD_FRECUENCIA_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    if (oItem.estado == Convert.ToChar("1"))
                    {
                        oItem.estado = Convert.ToChar("0");
                    }
                    else
                    {
                        oItem.estado = Convert.ToChar("1");
                    }
                    Modelo.SubmitChanges();
                    resultado = oItem.idFrecuencia;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToDeleteFrecuenciaInspeccion(string connection, SAS_CALIDAD_FRECUENCIA_REGISTRO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FRECUENCIA_REGISTRO> listado = new List<SAS_CALIDAD_FRECUENCIA_REGISTRO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.Where(x => x.idFrecuencia == item.idFrecuencia).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FRECUENCIA_REGISTRO oItem = new SAS_CALIDAD_FRECUENCIA_REGISTRO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FRECUENCIA_REGISTRO.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idFrecuencia;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Formatos de Liberación de Líneas packing
        public List<SAS_ListadoItemsParaLiberacionDeLineasAll> ListAllLiberacionLineas(string connection)
        {
            List<SAS_ListadoItemsParaLiberacionDeLineasAll> listado = new List<SAS_ListadoItemsParaLiberacionDeLineasAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoItemsParaLiberacionDeLineasAll.OrderBy(x => x.AreaDeInspeccion).ThenBy(x => x.ItemLiberacion).ToList();
            }
            return listado;
        }

        public int ToRegisterLiberacionLineas(string connection, SAS_CALIDAD_ITEMS_LIBERACION_LINEAS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS> listado = new List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_LIBERACION_LINEAS.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_ITEMS_LIBERACION_LINEAS oItem = new SAS_CALIDAD_ITEMS_LIBERACION_LINEAS();
                    //oItem.idItem = item.idItem;
                    oItem.descripcion = item.descripcion;
                    oItem.idArea = item.idArea;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_ITEMS_LIBERACION_LINEAS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ITEMS_LIBERACION_LINEAS oItem = new SAS_CALIDAD_ITEMS_LIBERACION_LINEAS();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.idArea = item.idArea;
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusLiberacionLineas(string connection, SAS_CALIDAD_ITEMS_LIBERACION_LINEAS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS> listado = new List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_LIBERACION_LINEAS.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ITEMS_LIBERACION_LINEAS oItem = new SAS_CALIDAD_ITEMS_LIBERACION_LINEAS();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    if (oItem.estado == Convert.ToChar("1"))
                    {
                        oItem.estado = Convert.ToChar("0");
                    }
                    else
                    {
                        oItem.estado = Convert.ToChar("1");
                    }
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToDeleteLiberacionLineas(string connection, SAS_CALIDAD_ITEMS_LIBERACION_LINEAS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS> listado = new List<SAS_CALIDAD_ITEMS_LIBERACION_LINEAS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_LIBERACION_LINEAS.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_ITEMS_LIBERACION_LINEAS oItem = new SAS_CALIDAD_ITEMS_LIBERACION_LINEAS();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_ITEMS_LIBERACION_LINEAS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Sensores() 
        public List<SAS_CALIDAD_SENSORES> ListAllSensores(string connection)
        {
            List<SAS_CALIDAD_SENSORES> listado = new List<SAS_CALIDAD_SENSORES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_SENSORES.OrderBy(x => x.tipo).ThenBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterSensores(string connection, SAS_CALIDAD_SENSORES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_SENSORES> listado = new List<SAS_CALIDAD_SENSORES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_SENSORES.Where(x => x.idSensor == item.idSensor).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_SENSORES oItem = new SAS_CALIDAD_SENSORES();
                    //oItem.idSensor = item.idSensor;
                    oItem.descripcion = item.descripcion;
                    oItem.tipo = item.tipo;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_SENSORES.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idSensor;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_SENSORES oItem = new SAS_CALIDAD_SENSORES();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.tipo = item.tipo;
                    Modelo.SubmitChanges();
                    resultado = oItem.idSensor;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusSensores(string connection, SAS_CALIDAD_SENSORES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_SENSORES> listado = new List<SAS_CALIDAD_SENSORES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_SENSORES.Where(x => x.idSensor == item.idSensor).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_SENSORES oItem = new SAS_CALIDAD_SENSORES();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idSensor;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteSensores(string connection, SAS_CALIDAD_SENSORES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_SENSORES> listado = new List<SAS_CALIDAD_SENSORES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_SENSORES.Where(x => x.idSensor == item.idSensor).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_SENSORES oItem = new SAS_CALIDAD_SENSORES();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_SENSORES.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idSensor;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Items Descarte Uva() 


        public List<SAS_CALIDAD_ITEMS_DESCARTE_UVA> ListAllItemsDescarteUva(string connection)
        {
            List<SAS_CALIDAD_ITEMS_DESCARTE_UVA> listado = new List<SAS_CALIDAD_ITEMS_DESCARTE_UVA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.OrderBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterItemsDescarteUva(string connection, SAS_CALIDAD_ITEMS_DESCARTE_UVA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_DESCARTE_UVA> listado = new List<SAS_CALIDAD_ITEMS_DESCARTE_UVA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_ITEMS_DESCARTE_UVA oItem = new SAS_CALIDAD_ITEMS_DESCARTE_UVA();
                    //oItem.idItem = item.idItem;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ITEMS_DESCARTE_UVA oItem = new SAS_CALIDAD_ITEMS_DESCARTE_UVA();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusItemsDescarteUva(string connection, SAS_CALIDAD_ITEMS_DESCARTE_UVA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_DESCARTE_UVA> listado = new List<SAS_CALIDAD_ITEMS_DESCARTE_UVA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_ITEMS_DESCARTE_UVA oItem = new SAS_CALIDAD_ITEMS_DESCARTE_UVA();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idItem;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteItemsDescarteUva(string connection, SAS_CALIDAD_ITEMS_DESCARTE_UVA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ITEMS_DESCARTE_UVA> listado = new List<SAS_CALIDAD_ITEMS_DESCARTE_UVA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.Where(x => x.idItem == item.idItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_ITEMS_DESCARTE_UVA oItem = new SAS_CALIDAD_ITEMS_DESCARTE_UVA();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_ITEMS_DESCARTE_UVA.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idItem;
                    #endregion
                }
            }
            return resultado;
        }

        #endregion

        #region Herramientas desinfeccion() 


        public List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION> ListAllHerramientasDesinfeccion(string connection)
        {
            List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION> listado = new List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.OrderBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterHerramientasDesinfeccion(string connection, SAS_CALIDAD_HERRAMIENTAS_DESINFECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION> listado = new List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.Where(x => x.idHerramienta == item.idHerramienta).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_HERRAMIENTAS_DESINFECCION oItem = new SAS_CALIDAD_HERRAMIENTAS_DESINFECCION();
                    //oItem.idItem = item.idItem;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idHerramienta;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_HERRAMIENTAS_DESINFECCION oItem = new SAS_CALIDAD_HERRAMIENTAS_DESINFECCION();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idHerramienta;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusHerramientasDesinfeccion(string connection, SAS_CALIDAD_HERRAMIENTAS_DESINFECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION> listado = new List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.Where(x => x.idHerramienta == item.idHerramienta).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_HERRAMIENTAS_DESINFECCION oItem = new SAS_CALIDAD_HERRAMIENTAS_DESINFECCION();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idHerramienta;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteHerramientasDesinfeccion(string connection, SAS_CALIDAD_HERRAMIENTAS_DESINFECCION item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION> listado = new List<SAS_CALIDAD_HERRAMIENTAS_DESINFECCION>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.Where(x => x.idHerramienta == item.idHerramienta).ToList();
                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_HERRAMIENTAS_DESINFECCION oItem = new SAS_CALIDAD_HERRAMIENTAS_DESINFECCION();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_HERRAMIENTAS_DESINFECCION.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idHerramienta;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Instrumentos Calidad() 


        public List<SAS_CALIDAD_INSTRUMENTOS> ListAllInstrumentosCalidad(string connection)
        {
            List<SAS_CALIDAD_INSTRUMENTOS> listado = new List<SAS_CALIDAD_INSTRUMENTOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_INSTRUMENTOS.OrderBy(x => x.descripcionTipo).ThenBy(x => x.codigo).ToList();
            }
            return listado;
        }

        public int ToRegisterInstrumentosCalidad(string connection, SAS_CALIDAD_INSTRUMENTOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_INSTRUMENTOS> listado = new List<SAS_CALIDAD_INSTRUMENTOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_INSTRUMENTOS.Where(x => x.idInstrumento == item.idInstrumento).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_INSTRUMENTOS oItem = new SAS_CALIDAD_INSTRUMENTOS();
                    //oItem.idInstrumento = item.idInstrumento;
                    oItem.codigo = item.codigo;
                    oItem.descripcionTipo = item.descripcionTipo;
                    oItem.idTipo = item.idTipo;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_INSTRUMENTOS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idInstrumento;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_INSTRUMENTOS oItem = new SAS_CALIDAD_INSTRUMENTOS();
                    //oItem.idInstrumento item.idInstrumento;
                    oItem = listado.ElementAt(0);
                    oItem.codigo = item.codigo;
                    oItem.descripcionTipo = item.descripcionTipo;
                    oItem.idTipo = item.idTipo;
                    Modelo.SubmitChanges();
                    resultado = oItem.idInstrumento;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusInstrumentosCalidad(string connection, SAS_CALIDAD_INSTRUMENTOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_INSTRUMENTOS> listado = new List<SAS_CALIDAD_INSTRUMENTOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_INSTRUMENTOS.Where(x => x.idInstrumento == item.idInstrumento).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_INSTRUMENTOS oItem = new SAS_CALIDAD_INSTRUMENTOS();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idInstrumento;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteInstrumentosCalidad(string connection, SAS_CALIDAD_INSTRUMENTOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_INSTRUMENTOS> listado = new List<SAS_CALIDAD_INSTRUMENTOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_INSTRUMENTOS.Where(x => x.idInstrumento == item.idInstrumento).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_INSTRUMENTOS oItem = new SAS_CALIDAD_INSTRUMENTOS();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_INSTRUMENTOS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idInstrumento;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Actividad Lavado de manos() 


        public List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS> ListAllActividadLavadoDeManos(string connection)
        {
            List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS> listado = new List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.OrderBy(x => x.descripcion).ThenBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterActividadLavadoDeManos(string connection, SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS> listado = new List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS oItem = new SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS();
                    //oItem.idActividad = item.idActividad;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idActividad;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS oItem = new SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS();
                    //oItem.idActividad item.idActividad;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idActividad;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusActividadLavadoDeManos(string connection, SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS> listado = new List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS oItem = new SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idActividad;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteActividadLavadoDeManos(string connection, SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS> listado = new List<SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS oItem = new SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS();
                    oItem = listado.ElementAt(0);
                    Modelo.SAS_CALIDAD_ACTIVIDAD_LAVADO_MANOS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idActividad;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Ambientes Luminaria Vidrios y Plasticos() 


        public List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS> ListAllAmbientesLuminariaVidriosPlasticos(string connection)
        {
            List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS> listado = new List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.OrderBy(x => x.descripcion).ThenBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterAmbientesLuminariaVidriosPlasticos(string connection, SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS> listado = new List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.Where(x => x.idAmbiente == item.idAmbiente).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS oItem = new SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS();
                    //oItem.idAmbiente = item.idAmbiente;
                    oItem.descripcion = item.descripcion;
                    oItem.cantLuminarias = item.cantLuminarias;
                    oItem.cantPlasticos = item.cantPlasticos;
                    oItem.cantVidrios = item.cantVidrios;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idAmbiente;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS oItem = new SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS();
                    //oItem.idAmbiente item.idAmbiente;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.cantLuminarias = item.cantLuminarias;
                    oItem.cantPlasticos = item.cantPlasticos;
                    oItem.cantVidrios = item.cantVidrios;
                    Modelo.SubmitChanges();
                    resultado = oItem.idAmbiente;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusAmbientesLuminariaVidriosPlasticos(string connection, SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS> listado = new List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.Where(x => x.idAmbiente == item.idAmbiente).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS oItem = new SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idAmbiente;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteAmbientesLuminariaVidriosPlasticos(string connection, SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS> listado = new List<SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.Where(x => x.idAmbiente == item.idAmbiente).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS oItem = new SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS();
                    oItem = listado.ElementAt(0);
                    Modelo.SAS_CALIDAD_AMBIENTES_LUMINARIAS_VIDRIOS_PLASTICOS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idAmbiente;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion

        #region Criterios de Manufactura() 


        public List<SAS_CALIDAD_CRITERIOS_MANUFACTURA> ListAllCriteriosManufactura(string connection)
        {
            List<SAS_CALIDAD_CRITERIOS_MANUFACTURA> listado = new List<SAS_CALIDAD_CRITERIOS_MANUFACTURA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.OrderBy(x => x.descripcion).ThenBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterCriteriosManufactura(string connection, SAS_CALIDAD_CRITERIOS_MANUFACTURA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_CRITERIOS_MANUFACTURA> listado = new List<SAS_CALIDAD_CRITERIOS_MANUFACTURA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_CRITERIOS_MANUFACTURA oItem = new SAS_CALIDAD_CRITERIOS_MANUFACTURA();
                    //oItem.idCriterio = item.idCriterio;
                    oItem.descripcion = item.descripcion;

                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_CRITERIOS_MANUFACTURA oItem = new SAS_CALIDAD_CRITERIOS_MANUFACTURA();
                    //oItem.idAmbiente item.idAmbiente;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;

                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusCriteriosManufactura(string connection, SAS_CALIDAD_CRITERIOS_MANUFACTURA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_CRITERIOS_MANUFACTURA> listado = new List<SAS_CALIDAD_CRITERIOS_MANUFACTURA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_CRITERIOS_MANUFACTURA oItem = new SAS_CALIDAD_CRITERIOS_MANUFACTURA();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idCriterio;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteCriteriosManufactura(string connection, SAS_CALIDAD_CRITERIOS_MANUFACTURA item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_CRITERIOS_MANUFACTURA> listado = new List<SAS_CALIDAD_CRITERIOS_MANUFACTURA>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_CRITERIOS_MANUFACTURA oItem = new SAS_CALIDAD_CRITERIOS_MANUFACTURA();
                    oItem = listado.ElementAt(0);
                    Modelo.SAS_CALIDAD_CRITERIOS_MANUFACTURA.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Turnos de evaluacion de calidad() 


        public List<SAS_CalidadTurnosDeEValuacion> ListAllTurnosTrabajoCalidad(string connection)
        {
            List<SAS_CalidadTurnosDeEValuacion> listado = new List<SAS_CalidadTurnosDeEValuacion>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                //listado = Modelo.SAS_CalidadTurnosDeEValuacion.OrderBy(x => x.Descripcion).ThenBy(x => x.Descripcion).ToList();
                listado = Modelo.SAS_CalidadTurnosDeEValuacion.ToList();
            }
            return listado;
        }

        public int ToRegisterTurnosTrabajoCalidad(string connection, SAS_CalidadTurnosDeEValuacion item)
        {
            int resultado = 1;
            List<SAS_CalidadTurnosDeEValuacion> listado = new List<SAS_CalidadTurnosDeEValuacion>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CalidadTurnosDeEValuacion.Where(x => x.IdTurno == item.IdTurno).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CalidadTurnosDeEValuacion oItem = new SAS_CalidadTurnosDeEValuacion();
                    oItem.IdTurno = item.IdTurno;
                    oItem.Descripcion = item.Descripcion;
                    oItem.Estado = Convert.ToByte("1");
                    Modelo.SAS_CalidadTurnosDeEValuacion.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = 1;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CalidadTurnosDeEValuacion oItem = new SAS_CalidadTurnosDeEValuacion();
                    //oItem.idAmbiente item.idAmbiente;
                    oItem = listado.ElementAt(0);
                    oItem.Descripcion = item.Descripcion;
                    Modelo.SubmitChanges();
                    resultado = 2;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusTurnosTrabajoCalidad(string connection, SAS_CalidadTurnosDeEValuacion item)
        {
            int resultado = 1;
            List<SAS_CalidadTurnosDeEValuacion> listado = new List<SAS_CalidadTurnosDeEValuacion>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CalidadTurnosDeEValuacion.Where(x => x.IdTurno == item.IdTurno).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CalidadTurnosDeEValuacion oItem = new SAS_CalidadTurnosDeEValuacion();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.Estado == Convert.ToByte("1"))
                        {
                            oItem.Estado = Convert.ToByte("0");
                        }
                        else
                        {
                            oItem.Estado = Convert.ToByte("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = 3;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteTurnosTrabajoCalidad(string connection, SAS_CalidadTurnosDeEValuacion item)
        {
            int resultado = 1;
            List<SAS_CalidadTurnosDeEValuacion> listado = new List<SAS_CalidadTurnosDeEValuacion>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CalidadTurnosDeEValuacion.Where(x => x.IdTurno == item.IdTurno).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CalidadTurnosDeEValuacion oItem = new SAS_CalidadTurnosDeEValuacion();
                    oItem = listado.ElementAt(0);
                    Modelo.SAS_CalidadTurnosDeEValuacion.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = 4;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        // 11.09.2023

        #region Sub Proceso SO2() 


        public List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO> ListAllSubProcesoSO2(string connection)
        {
            List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.OrderBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterSubProcesoSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.Where(x => x.idSubProceso == item.idSubProceso).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO();
                    //oItem.idSubProceso = item.idSubProceso;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idSubProceso;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idSubProceso;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusSubProcesoSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.Where(x => x.idSubProceso == item.idSubProceso).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idSubProceso;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteSubProcesoSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.Where(x => x.idSubProceso == item.idSubProceso).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_SUBPROCESO.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idSubProceso;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Actividades SO2() 


        public List<SAS_AplicacionSO2ActividadesAll> ListAllActividadesSO2(string connection)
        {
            List<SAS_AplicacionSO2ActividadesAll> listado = new List<SAS_AplicacionSO2ActividadesAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_AplicacionSO2ActividadesAll.OrderBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisteActividadesSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES();
                    //oItem.idActividad = item.idActividad;                    
                    oItem.idSubProceso = item.idSubProceso;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idSubProceso;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.idSubProceso = item.idSubProceso;
                    Modelo.SubmitChanges();
                    resultado = oItem.idSubProceso;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusActividadesSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idActividad;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteActividadesSO2(string connection, SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES> listado = new List<SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES.Where(x => x.idActividad == item.idActividad).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES oItem = new SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_APLICACION_SO2_ACTIVIDADES.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idActividad;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Limpieza desinfeccionItems() 


        public List<SAS_ListadoLimpiezaDesinfeccionItemsByAreaAll> ListAllLimpiezaDesinfeccionItems(string connection)
        {
            List<SAS_ListadoLimpiezaDesinfeccionItemsByAreaAll> listado = new List<SAS_ListadoLimpiezaDesinfeccionItemsByAreaAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoLimpiezaDesinfeccionItemsByAreaAll.OrderBy(x => x.AreaAInspeccion).ThenBy(x=> x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisteLimpiezaDesinfeccionItems(string connection, SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS> listado = new List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS.Where(x => x.item == item.item).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS oItem = new SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS();
                    //oItem.idActividad = item.idActividad;                    
                    oItem.idArea = item.idArea;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.item;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS oItem = new SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    oItem.idArea = item.idArea;
                    Modelo.SubmitChanges();
                    resultado = oItem.item;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusLimpiezaDesinfeccionItems(string connection, SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS> listado = new List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS.Where(x => x.item == item.item).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS oItem = new SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS();
                        //oItem.item = item.item;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.item;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteLimpiezaDesinfeccionItems(string connection, SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS> listado = new List<SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS.Where(x => x.item == item.item).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS oItem = new SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS();
                    //oItem.item = item.item;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_LIMPIEZA_DESINFECCION_ITEMS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.item;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion



        #region Verificacion de Vidrios Item Cabecera() 

        public List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem> ListAllVerificacionVidriosItem(string connection)
        {
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.OrderBy(x => x.Descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterVerificacionVidriosItem(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.Where(x => x.TipoItem == item.TipoItem).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem();
                    //oItem.TipoItem = item.TipoItem;
                    oItem.Descripcion = item.Descripcion;
                    oItem.Estado = Convert.ToByte("1");
                    Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.TipoItem;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem();
                    //oItem.idItem item.idItem;
                    oItem = listado.ElementAt(0);
                    oItem.Descripcion = item.Descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.TipoItem;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusVerificacionVidriosItem(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.Where(x => x.TipoItem == item.TipoItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.Estado == Convert.ToByte("1"))
                        {
                            oItem.Estado = Convert.ToByte("0");
                        }
                        else
                        {
                            oItem.Estado = Convert.ToByte("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.TipoItem;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteVerificacionVidriosItem(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.Where(x => x.TipoItem == item.TipoItem).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItem.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.TipoItem;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion



        #region Verificacion de Vidrios Item Detalle() 

        public List<SAS_ListadoVerificacionVidriosDetalleAll> ListAllVerificacionVidriosItemDetalle(string connection)
        {
            List<SAS_ListadoVerificacionVidriosDetalleAll> listado = new List<SAS_ListadoVerificacionVidriosDetalleAll>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoVerificacionVidriosDetalleAll.OrderBy(x => x.ItemDetalle).ThenBy(x=> x.Descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterVerificacionVidriosItemDetalle(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle.Where(x => x.id == item.id).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle();
                    //oItem.id = item.id;
                    oItem.Descripcion = item.Descripcion;
                    oItem.Abrevitura = item.Abrevitura;
                    oItem.idDetalle = item.idDetalle;
                    oItem.Estado = Convert.ToByte("1");
                    Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.id;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle();
                    //oItem.id item.id;
                    oItem = listado.ElementAt(0);
                    oItem.Descripcion = item.Descripcion;
                    oItem.Abrevitura = item.Abrevitura;
                    oItem.idDetalle = item.idDetalle;
                    Modelo.SubmitChanges();
                    resultado = oItem.id;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusVerificacionVidriosItemDetalle(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle.Where(x => x.id == item.id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle();
                        //oItem.id = item.id;
                        oItem = listado.ElementAt(0);
                        if (oItem.Estado == Convert.ToByte("1"))
                        {
                            oItem.Estado = Convert.ToByte("0");
                        }
                        else
                        {
                            oItem.Estado = Convert.ToByte("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.id;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteVerificacionVidriosItemDetalle(string connection, SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle> listado = new List<SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle.Where(x => x.id == item.id).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle oItem = new SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle();
                    //oItem.idRegistro = item.id;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_VERIFICACION_VIDRIOS_TipoItemDetalle.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.id;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion



        #region Inocuidad Criterios() 


        public List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS> ListAllInocuidadCriterios(string connection)
        {
            List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS> listado = new List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.OrderBy(x => x.descripcion).ToList();
            }
            return listado;
        }

        public int ToRegisterInocuidadCriterios(string connection, SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS> listado = new List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 0)
                {
                    #region Registro Nuevo()
                    SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS oItem = new SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS();
                    //oItem.idCriterio = item.idCriterio;
                    oItem.descripcion = item.descripcion;
                    oItem.estado = Convert.ToChar("1");
                    Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.InsertOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
                else if (listado != null && listado.ToList().Count == 1)
                {
                    #region Editar()
                    SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS oItem = new SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS();
                    //oItem.idCriterio item.idCriterio;
                    oItem = listado.ElementAt(0);
                    oItem.descripcion = item.descripcion;
                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
            }
            return resultado;
        }

        public int ToChangeStatusInocuidadCriterios(string connection, SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS> listado = new List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    {
                        #region Editar()
                        SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS oItem = new SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS();
                        //oItem.idRegistro = item.idRegistro;
                        oItem = listado.ElementAt(0);
                        if (oItem.estado == Convert.ToChar("1"))
                        {
                            oItem.estado = Convert.ToChar("0");
                        }
                        else
                        {
                            oItem.estado = Convert.ToChar("1");
                        }
                        Modelo.SubmitChanges();
                        resultado = oItem.idCriterio;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToDeleteInocuidadCriterios(string connection, SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS item)
        {
            int resultado = 1;
            List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS> listado = new List<SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();
            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                listado = Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.Where(x => x.idCriterio == item.idCriterio).ToList();

                if (listado != null && listado.ToList().Count == 1)
                {
                    #region Eliminar() 
                    SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS oItem = new SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS();
                    //oItem.idRegistro = item.idRegistro;
                    oItem = listado.ElementAt(0);
                    // Aqui tengo que validar que no tenga matricula en algun registro de evaluacion
                    Modelo.SAS_CALIDAD_FORM_INOCUIDAD_CRITERIOS.DeleteOnSubmit(oItem);
                    Modelo.SubmitChanges();
                    resultado = oItem.idCriterio;
                    #endregion
                }
            }
            return resultado;
        }
        #endregion


        #region Reportes   | PDF() 

        #region CumplimientoDiarioDeLavadoDeManosReporte()
        public List<SAS_ListadoLavadoDeManoAllByDatesResult> ListReporteCumplimientoDiarioDeLavadoDeManosReporte(string connection, string Desde, string Hasta, int EsResumido)
        {
            List<SAS_ListadoLavadoDeManoAllByDatesResult> listado = new List<SAS_ListadoLavadoDeManoAllByDatesResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                var list = Modelo.SAS_ListadoLavadoDeManoAllByDates(Desde, Hasta).OrderBy(x => x.FechaRegistro).ThenBy(x => x.periodo).ToList();

                if (EsResumido == 1)
                {
                    #region Agrupar Lista
                    listado = (from item in list
                               group item by new { item.CabeceraId } into j
                               select new SAS_ListadoLavadoDeManoAllByDatesResult
                               {
                                   CabeceraId = j.Key.CabeceraId,
                                   RegistroId = j.FirstOrDefault().RegistroId != (int?)null ? j.FirstOrDefault().RegistroId : 0,
                                   Sede = j.FirstOrDefault().Sede != null ? j.FirstOrDefault().Sede : string.Empty,
                                   SedeId = j.FirstOrDefault().SedeId != null ? j.FirstOrDefault().SedeId : string.Empty,
                                   Turno = j.FirstOrDefault().Turno != null ? j.FirstOrDefault().Turno : string.Empty,
                                   TurnoId = j.FirstOrDefault().TurnoId != null ? j.FirstOrDefault().TurnoId : string.Empty,
                                   Semana = j.FirstOrDefault().Semana != null ? j.FirstOrDefault().Semana : string.Empty,
                                   FechaEvaluacion = j.FirstOrDefault().FechaEvaluacion != (DateTime?)null ? j.FirstOrDefault().FechaEvaluacion.Value : (DateTime?)null,
                                   Responsable = j.FirstOrDefault().Responsable != null ? j.FirstOrDefault().Responsable : string.Empty,
                                   ResponsableId = j.FirstOrDefault().ResponsableId != null ? j.FirstOrDefault().ResponsableId : string.Empty,
                                   Evaluador = j.FirstOrDefault().Evaluador != null ? j.FirstOrDefault().Evaluador : string.Empty,
                                   EvaluadorId = j.FirstOrDefault().EvaluadorId != null ? j.FirstOrDefault().EvaluadorId : string.Empty,
                                   FormatoEvaluacion = j.FirstOrDefault().FormatoEvaluacion != null ? j.FirstOrDefault().FormatoEvaluacion : string.Empty,
                                   FormatoEvaluacionCodigo = j.FirstOrDefault().FormatoEvaluacionCodigo != null ? j.FirstOrDefault().FormatoEvaluacionCodigo : string.Empty,
                                   FormatoEvaluacionVersion = j.FirstOrDefault().FormatoEvaluacionVersion != (int?)null ? j.FirstOrDefault().FormatoEvaluacionVersion : 0,
                                   FormatoEvaluacionPaguinas = j.FirstOrDefault().FormatoEvaluacionPaguinas != (int?)null ? j.FirstOrDefault().FormatoEvaluacionPaguinas : 0,
                                   Observaciones = j.FirstOrDefault().Observaciones != null ? j.FirstOrDefault().Observaciones : string.Empty,
                                   AcccionCorrectiva = j.FirstOrDefault().AcccionCorrectiva != (int?)null ? j.FirstOrDefault().AcccionCorrectiva : 0,
                                   TipoFormatoEvaluacionId = j.FirstOrDefault().TipoFormatoEvaluacionId != (int?)null ? j.FirstOrDefault().TipoFormatoEvaluacionId : 0,
                                   EstadoDeEvaluacion = j.FirstOrDefault().EstadoDeEvaluacion != null ? j.FirstOrDefault().EstadoDeEvaluacion : string.Empty,
                                   firmaEvaluador = j.FirstOrDefault().firmaEvaluador != null ? j.FirstOrDefault().firmaEvaluador : string.Empty,
                                   firmaReponsable = j.FirstOrDefault().firmaReponsable != null ? j.FirstOrDefault().firmaReponsable : string.Empty,
                                   nombreDiaSemana = j.FirstOrDefault().nombreDiaSemana != null ? j.FirstOrDefault().nombreDiaSemana : string.Empty,
                                   periodo = j.FirstOrDefault().periodo != null ? j.FirstOrDefault().periodo : string.Empty,
                                   FechaRegistro = j.FirstOrDefault().FechaRegistro != (DateTime?)null ? j.FirstOrDefault().FechaRegistro : DateTime.Now,
                                   ColaboradorEvaluado = j.FirstOrDefault().ColaboradorEvaluado != null ? j.FirstOrDefault().ColaboradorEvaluado : string.Empty,
                                   ColaboradorEvaluadoId = j.FirstOrDefault().ColaboradorEvaluadoId != null ? j.FirstOrDefault().ColaboradorEvaluadoId : string.Empty,
                                   Actividad = j.FirstOrDefault().Actividad != null ? j.FirstOrDefault().Actividad : string.Empty,
                                   ActividadId = j.FirstOrDefault().ActividadId != (int?)null ? j.FirstOrDefault().ActividadId : 0,
                                   Valor = j.FirstOrDefault().Valor != (char?)null ? j.FirstOrDefault().Valor : Convert.ToChar("0"),

                               }
                                    ).ToList();
                    #endregion
                }
                else
                {
                    listado = list;
                }
            }
            return listado;
        }
        #endregion

        #region ListAmonestacionesIncumplimientosByDates()
        public List<SAS_ListadoAmonestacionesIncumplimientosByDatesResult> ListAmonestacionesIncumplimientosByDates(string connection, string Desde, string Hasta, int EsResumido)
        {
            List<SAS_ListadoAmonestacionesIncumplimientosByDatesResult> listado = new List<SAS_ListadoAmonestacionesIncumplimientosByDatesResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[(connection == "NSFAJAS" ? "SAS" : connection)].ToString();

            using (BoltCalidadPackingDataContext Modelo = new BoltCalidadPackingDataContext(cnx))
            {
                var list = Modelo.SAS_ListadoAmonestacionesIncumplimientosByDates(Desde, Hasta).OrderBy(x => x.FechaRegistro).ThenBy(x => x.periodo).ToList();

                if (EsResumido == 1)
                {
                    #region Agrupar Lista
                    listado = (from item in list
                               group item by new { item.CabeceraId } into j
                               select new SAS_ListadoAmonestacionesIncumplimientosByDatesResult
                               {
                                   CabeceraId = j.Key.CabeceraId,
                                   RegistroId = j.FirstOrDefault().RegistroId != (int?)null ? j.FirstOrDefault().RegistroId : 0,
                                   Sede = j.FirstOrDefault().Sede != null ? j.FirstOrDefault().Sede : string.Empty,
                                   SedeId = j.FirstOrDefault().SedeId != null ? j.FirstOrDefault().SedeId : string.Empty,
                                   Turno = j.FirstOrDefault().Turno != null ? j.FirstOrDefault().Turno : string.Empty,
                                   TurnoId = j.FirstOrDefault().TurnoId != null ? j.FirstOrDefault().TurnoId : string.Empty,
                                   Semana = j.FirstOrDefault().Semana != null ? j.FirstOrDefault().Semana : string.Empty,
                                   FechaEvaluacion = j.FirstOrDefault().FechaEvaluacion != (DateTime?)null ? j.FirstOrDefault().FechaEvaluacion.Value : (DateTime?)null,
                                   Responsable = j.FirstOrDefault().Responsable != null ? j.FirstOrDefault().Responsable : string.Empty,
                                   ResponsableId = j.FirstOrDefault().ResponsableId != null ? j.FirstOrDefault().ResponsableId : string.Empty,
                                   Evaluador = j.FirstOrDefault().Evaluador != null ? j.FirstOrDefault().Evaluador : string.Empty,
                                   EvaluadorId = j.FirstOrDefault().EvaluadorId != null ? j.FirstOrDefault().EvaluadorId : string.Empty,
                                   FormatoEvaluacion = j.FirstOrDefault().FormatoEvaluacion != null ? j.FirstOrDefault().FormatoEvaluacion : string.Empty,
                                   FormatoEvaluacionCodigo = j.FirstOrDefault().FormatoEvaluacionCodigo != null ? j.FirstOrDefault().FormatoEvaluacionCodigo : string.Empty,
                                   FormatoEvaluacionVersion = j.FirstOrDefault().FormatoEvaluacionVersion != (int?)null ? j.FirstOrDefault().FormatoEvaluacionVersion : 0,
                                   FormatoEvaluacionPaguinas = j.FirstOrDefault().FormatoEvaluacionPaguinas != (int?)null ? j.FirstOrDefault().FormatoEvaluacionPaguinas : 0,
                                   Observaciones = j.FirstOrDefault().Observaciones != null ? j.FirstOrDefault().Observaciones : string.Empty,
                                   AcccionCorrectiva = j.FirstOrDefault().AcccionCorrectiva != (int?)null ? j.FirstOrDefault().AcccionCorrectiva : 0,
                                   TipoFormatoEvaluacionId = j.FirstOrDefault().TipoFormatoEvaluacionId != (int?)null ? j.FirstOrDefault().TipoFormatoEvaluacionId : 0,
                                   EstadoDeEvaluacion = j.FirstOrDefault().EstadoDeEvaluacion != null ? j.FirstOrDefault().EstadoDeEvaluacion : string.Empty,
                                   firmaEvaluador = j.FirstOrDefault().firmaEvaluador != null ? j.FirstOrDefault().firmaEvaluador : string.Empty,
                                   firmaReponsable = j.FirstOrDefault().firmaReponsable != null ? j.FirstOrDefault().firmaReponsable : string.Empty,
                                   nombreDiaSemana = j.FirstOrDefault().nombreDiaSemana != null ? j.FirstOrDefault().nombreDiaSemana : string.Empty,
                                   periodo = j.FirstOrDefault().periodo != null ? j.FirstOrDefault().periodo : string.Empty,
                                   FechaRegistro = j.FirstOrDefault().FechaRegistro != (DateTime?)null ? j.FirstOrDefault().FechaRegistro : DateTime.Now,
                                   ColaboradorEvaluado = j.FirstOrDefault().ColaboradorEvaluado != null ? j.FirstOrDefault().ColaboradorEvaluado : string.Empty,
                                   ColaboradorEvaluadoId = j.FirstOrDefault().ColaboradorEvaluadoId != null ? j.FirstOrDefault().ColaboradorEvaluadoId : string.Empty,                                   
                                   DetalleId = j.FirstOrDefault().DetalleId != null ? j.FirstOrDefault().DetalleId : 0,
                                   Justificacion = j.FirstOrDefault().Justificacion != null ? j.FirstOrDefault().Justificacion : string.Empty,
                                   NumeroAmonestacion = j.FirstOrDefault().NumeroAmonestacion != (char?)null ? j.FirstOrDefault().NumeroAmonestacion.Value :0,
                                   Recibido = j.FirstOrDefault().Recibido != null ? j.FirstOrDefault().Recibido : string.Empty,
                                   ObservacionDeEvaluacionAColaborador = j.FirstOrDefault().ObservacionDeEvaluacionAColaborador != null ? j.FirstOrDefault().ObservacionDeEvaluacionAColaborador : string.Empty,
                               }
                                    ).ToList();
                    #endregion
                }
                else
                {
                    listado = list;
                }
            }
            return listado;
        }
        #endregion

        #endregion




    }
}
