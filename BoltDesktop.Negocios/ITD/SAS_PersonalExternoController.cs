using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_PersonalExternoController
    {

        // Obtener listado del personal externo()
        public List<SAS_PersonalExternolistado> GetListOfExternalStaff(string conection)
        {
            List<SAS_PersonalExternolistado> resultado = new List<SAS_PersonalExternolistado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                resultado = Modelo.SAS_PersonalExternolistado.ToList();
            }

            return resultado;
        }

        // Registrar()
        public int ToRegister(string conection, SAS_PersonalExterno item)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_PersonalExterno.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 0)
                        {
                            #region Registrar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            oRegistro.id = item.id;
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.dni = item.dni;
                            oRegistro.nombres = item.nombres;
                            oRegistro.idAreaSolicita = item.idAreaSolicita;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.empresa = item.empresa;
                            oRegistro.fechaRegistro = item.fechaRegistro;
                            oRegistro.registradoPor = item.registradoPor;
                            oRegistro.cargo = item.cargo;
                            oRegistro.vigenciaDesde = item.vigenciaDesde;
                            oRegistro.vigenciaHasta = item.vigenciaHasta;
                            oRegistro.codigoFuncionarioAprueba = item.codigoFuncionarioAprueba;
                            oRegistro.estado = item.estado;
                            oRegistro.glosa = item.glosa;
                            Modelo.SAS_PersonalExterno.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoRegistro = 1;
                            #endregion
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            //oRegistro.id = item.id;
                            oRegistro = resultado.Single();
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.dni = item.dni;
                            oRegistro.nombres = item.nombres;
                            oRegistro.idAreaSolicita = item.idAreaSolicita;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.empresa = item.empresa;
                            oRegistro.fechaRegistro = item.fechaRegistro;
                            oRegistro.registradoPor = item.registradoPor;
                            oRegistro.cargo = item.cargo;
                            oRegistro.vigenciaDesde = item.vigenciaDesde;
                            oRegistro.vigenciaHasta = item.vigenciaHasta;
                            oRegistro.codigoFuncionarioAprueba = item.codigoFuncionarioAprueba;
                            oRegistro.estado = item.estado;
                            oRegistro.glosa = item.glosa;
                            Modelo.SubmitChanges();
                            tipoRegistro = 2;
                            #endregion  
                        }
                    }
                    Scope.Complete();
                }
            }
            return tipoRegistro;
        }

        // Registrar item con detalle() 
        public int ToRegister(string conection, SAS_PersonalExterno item, List<SAS_PersonalExternoDetalle> deletedDetailList, List<SAS_PersonalExternoDetalle> detailList)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_PersonalExterno.Where(x => x.id == item.id).ToList();
                    var max = Modelo.SAS_PersonalExterno.Max(x => x.id) + 1;
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 0)
                        {
                            #region Registrar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            //oRegistro.id = item.id;
                            oRegistro.idCodigoGeneral = "EXT" + max.ToString().PadLeft(9, '0');
                            oRegistro.dni = item.dni;
                            oRegistro.nombres = item.nombres;
                            oRegistro.idAreaSolicita = item.idAreaSolicita;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.empresa = item.empresa;
                            oRegistro.fechaRegistro = item.fechaRegistro;
                            oRegistro.registradoPor = item.registradoPor;
                            oRegistro.cargo = item.cargo;
                            oRegistro.vigenciaDesde = item.vigenciaDesde;
                            oRegistro.vigenciaHasta = item.vigenciaHasta;
                            oRegistro.codigoFuncionarioAprueba = item.codigoFuncionarioAprueba;
                            oRegistro.estado = item.estado;
                            oRegistro.glosa = item.glosa;
                            Modelo.SAS_PersonalExterno.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            tipoRegistro = 1;
                            #endregion

                            #region Registrar la lista detalle()
                            if (detailList != null)
                            {
                                #region Registro o edición de la lista detalle() 
                                if (detailList.ToList().Count > 0)
                                {
                                    foreach (var itemDetail in detailList)
                                    {
                                        #region Registro o edición del detalle()
                                        var result01 = Modelo.SAS_PersonalExternoDetalle.Where(x => x.id == itemDetail.id && x.item == itemDetail.item).ToList();

                                        if (result01 != null)
                                        {
                                            if (result01.ToList().Count == 0)
                                            {
                                                #region Registrar
                                                SAS_PersonalExternoDetalle detail = new SAS_PersonalExternoDetalle();
                                                detail.id = itemDetail.id;
                                                detail.item = itemDetail.item;
                                                detail.idTipo = itemDetail.idTipo;
                                                detail.link = itemDetail.link;
                                                detail.descripcion = itemDetail.descripcion;
                                                detail.estado = itemDetail.estado;
                                                detail.creadoPor = itemDetail.creadoPor;
                                                Modelo.SAS_PersonalExternoDetalle.InsertOnSubmit(detail);
                                                Modelo.SubmitChanges();
                                                #endregion
                                            }
                                            else if (result01.ToList().Count == 1)
                                            {
                                                #region Editar()
                                                SAS_PersonalExternoDetalle detail = new SAS_PersonalExternoDetalle();
                                                detail = result01.Single();
                                                //detail.id = itemDetail.id;                                                
                                                //detail.item = itemDetail.item;
                                                detail.idTipo = itemDetail.idTipo;
                                                detail.link = itemDetail.link;
                                                detail.descripcion = itemDetail.descripcion;
                                                detail.estado = itemDetail.estado;
                                                detail.creadoPor = itemDetail.creadoPor;
                                                //Modelo.SAS_PersonalExternoDetalle.InsertOnSubmit(detail);
                                                Modelo.SubmitChanges();
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            tipoRegistro = 0;
                        }
                        else if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            //oRegistro.id = item.id;
                            oRegistro = resultado.Single();
                            oRegistro.idCodigoGeneral = item.idCodigoGeneral;
                            oRegistro.dni = item.dni;
                            oRegistro.nombres = item.nombres;
                            oRegistro.idAreaSolicita = item.idAreaSolicita;
                            oRegistro.idclieprov = item.idclieprov;
                            oRegistro.empresa = item.empresa;
                            oRegistro.fechaRegistro = item.fechaRegistro;
                            oRegistro.registradoPor = item.registradoPor;
                            oRegistro.cargo = item.cargo;
                            oRegistro.vigenciaDesde = item.vigenciaDesde;
                            oRegistro.vigenciaHasta = item.vigenciaHasta;
                            oRegistro.codigoFuncionarioAprueba = item.codigoFuncionarioAprueba;
                            oRegistro.estado = item.estado;
                            oRegistro.glosa = item.glosa;
                            Modelo.SubmitChanges();
                            #endregion

                            #region Eliminar lista de eliminados()s
                            if (deletedDetailList != null)
                            {
                                #region Registro o edición de la lista detalle() 
                                if (deletedDetailList.ToList().Count > 0)
                                {
                                    foreach (var itemDetail in deletedDetailList)
                                    {
                                        #region Registro o edición del detalle()
                                        var result01 = Modelo.SAS_PersonalExternoDetalle.Where(x => x.id == itemDetail.id && x.item == itemDetail.item).ToList();

                                        if (result01 != null)
                                        {
                                            if (result01.ToList().Count == 1)
                                            {
                                                #region Editar()
                                                SAS_PersonalExternoDetalle detail = new SAS_PersonalExternoDetalle();
                                                detail = result01.Single();
                                                Modelo.SAS_PersonalExternoDetalle.DeleteOnSubmit(detail);
                                                Modelo.SubmitChanges();
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Registrar la lista detalle()
                            if (detailList != null)
                            {
                                #region Registro o edición de la lista detalle() 
                                if (detailList.ToList().Count > 0)
                                {
                                    foreach (var itemDetail in detailList)
                                    {
                                        #region Registro o edición del detalle()
                                        var result01 = Modelo.SAS_PersonalExternoDetalle.Where(x => x.id == itemDetail.id && x.item == itemDetail.item).ToList();

                                        if (result01 != null)
                                        {
                                            if (result01.ToList().Count == 0)
                                            {
                                                #region Registrar
                                                SAS_PersonalExternoDetalle detail = new SAS_PersonalExternoDetalle();
                                                detail.id = itemDetail.id;
                                                detail.item = itemDetail.item;
                                                detail.idTipo = itemDetail.idTipo;
                                                detail.link = itemDetail.link;
                                                detail.descripcion = itemDetail.descripcion;
                                                detail.estado = itemDetail.estado;
                                                detail.creadoPor = itemDetail.creadoPor;
                                                Modelo.SAS_PersonalExternoDetalle.InsertOnSubmit(detail);
                                                Modelo.SubmitChanges();
                                                #endregion
                                            }
                                            else if (result01.ToList().Count == 1)
                                            {
                                                #region Editar()
                                                SAS_PersonalExternoDetalle detail = new SAS_PersonalExternoDetalle();
                                                detail = result01.Single();
                                                //detail.id = itemDetail.id;                                                
                                                //detail.item = itemDetail.item;
                                                detail.idTipo = itemDetail.idTipo;
                                                detail.link = itemDetail.link;
                                                detail.descripcion = itemDetail.descripcion;
                                                detail.estado = itemDetail.estado;
                                                detail.creadoPor = itemDetail.creadoPor;
                                                //Modelo.SAS_PersonalExternoDetalle.InsertOnSubmit(detail);
                                                Modelo.SubmitChanges();
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            tipoRegistro = 1;
                        }
                    }
                    Scope.Complete();
                }
            }
            return tipoRegistro;
        }

        // Cambiar estado del documento()
        public int ChangeStatus(string conection, SAS_PersonalExterno item)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_PersonalExterno.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 1)
                        {
                            #region Actualizar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            //oRegistro.dispositivoCodigo = colaborador.dispositivoCodigo;
                            //oRegistro.item = colaborador.item;
                            oRegistro = resultado.Single();
                            if (oRegistro.estado == 0)
                            {
                                oRegistro.estado = 1;
                                tipoRegistro = 3; //Activar
                            }
                            else
                            {
                                oRegistro.estado = 0;
                                tipoRegistro = 4; //Desactivar
                            }
                            //Modelo.SAS_DispositivoUsuarios.InsertOnSubmit(oRegistro);
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }

                    Scope.Complete();
                }

            }



            return tipoRegistro;
        }

        // Eliminar Documento, esta opción sólo es para el administrador del sistema()
        public int Eliminate(string conection, SAS_PersonalExterno item)
        {
            int tipoRegistro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    var resultado = Modelo.SAS_PersonalExterno.Where(x => x.id == item.id).ToList();
                    if (resultado != null)
                    {
                        if (resultado.ToList().Count == 1)
                        {
                            #region Eliminar() 
                            SAS_PersonalExterno oRegistro = new SAS_PersonalExterno();
                            oRegistro = resultado.Single();
                            Modelo.SAS_PersonalExterno.DeleteOnSubmit(oRegistro);
                            tipoRegistro = 5; //Eliminar
                            Modelo.SubmitChanges();
                            #endregion
                        }
                    }
                    Scope.Complete();
                }
            }
            return tipoRegistro;
        }

        // Obtener la lista de documento detalles del personal externo, por lo general son el link de la autorización o foto del DNI o documento, también puede ser el archivo de correo que aprueba la solicitud()
        public List<SAS_PersonalExternoDetalleByIdResult> ListadoColaboradoresByDispositivoByCodigo(string conection, int codigo)
        {
            List<SAS_PersonalExternoDetalleByIdResult> resultado = new List<SAS_PersonalExternoDetalleByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                resultado = Modelo.SAS_PersonalExternoDetalleById(codigo).ToList();
            }
            return resultado;
        }


        public List<DFormatoSimple> FeatureType(string conection)
        {
            List<DFormatoSimple> listado = new List<DFormatoSimple>();
            string cnx;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado.Add(new DFormatoSimple { Codigo = "1", Descripcion = "Solicitud de autorización" });
                listado.Add(new DFormatoSimple { Codigo = "2", Descripcion = "Imagen" });
                listado.Add(new DFormatoSimple { Codigo = "3", Descripcion = "Correo que autoriza" });
                listado.Add(new DFormatoSimple { Codigo = "4", Descripcion = "Otro" });
            }
            return listado;
        }

    }
}
