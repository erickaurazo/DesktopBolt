using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_RegistroTicketCamaraGasificadoExoneradosController
    {

        public List<SAS_ListadoDeRegistrosExoneradosByDatesResult> GetListByDates(string conection, string desde, string hasta)
        {
            List<SAS_ListadoDeRegistrosExoneradosByDatesResult> resultado = new List<SAS_ListadoDeRegistrosExoneradosByDatesResult>();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                resultado = Modelo.SAS_ListadoDeRegistrosExoneradosByDates(desde, hasta).ToList();
            }

            return resultado;
        }


        public List<Grupo> GetSeries(string conection, string formName)
        {
            List<Grupo> result = new List<Grupo>();

            result.Add(new Grupo
            {
                Codigo = "2022",
                Descripcion = "2022",
                Valor = "2022"
            });

            return result;
        }

        public List<Grupo> GetDocument(string conection, string formName)
        {
            List<Grupo> result = new List<Grupo>();

            result.Add(new Grupo
            {
                Codigo = "EXG",
                Descripcion = "EXG",
                Valor = "EXG"
            });

            return result;
        }

        public List<Grupo> GetListOfDocuments(string conection, string formName)
        {
            List<Grupo> result = new List<Grupo>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                var resultado = Modelo.SAS_MotivoExoneracionGasificado.Where(x => x.estado == 1).ToList();

                result = (from item in resultado
                          group item by new { item.idMotivo } into j
                          select new Grupo
                          {
                              Codigo = j.Key.idMotivo.Trim(),
                              Descripcion = j.FirstOrDefault().descripcion != null ? j.FirstOrDefault().descripcion.Trim() : string.Empty,
                              Valor = j.Key.idMotivo.Trim()
                          }).ToList();
            }
            return result;
        }



        public SAS_ListadoDeRegistrosExoneradosByIdResult GetListById(string conection, int codigo)
        {
            List<SAS_ListadoDeRegistrosExoneradosByIdResult> list = new List<SAS_ListadoDeRegistrosExoneradosByIdResult>();
            SAS_ListadoDeRegistrosExoneradosByIdResult result = new SAS_ListadoDeRegistrosExoneradosByIdResult();
            result.codigoExoneracion = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                list = Modelo.SAS_ListadoDeRegistrosExoneradosById(codigo).ToList();

                if (list != null)
                {
                    if (list.Count() == 1)
                    {
                        result = list.ElementAt(0);

                    }
                }
            }

            return result;
        }


        public int ToRegister(string conection, SAS_RegistroTicketCamaraGasificadoExonerados oRegistroGasificado)
        {
            int numeroOperacion = 0;
            List<SAS_RegistroTicketCamaraGasificadoExonerados> listResult = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
            SAS_RegistroTicketCamaraGasificadoExonerados result = new SAS_RegistroTicketCamaraGasificadoExonerados();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listResult = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == oRegistroGasificado.itemDetalle && x.idestado == 1).ToList();
                if (listResult != null)
                {
                    if (listResult.ToList().Count == 0)
                    {
                        #region Nuevo()
                        result = new SAS_RegistroTicketCamaraGasificadoExonerados();
                        //result.itemDDetalle = 0;
                        result.itemDetalle = oRegistroGasificado.itemDetalle != null ? oRegistroGasificado.itemDetalle : 0;
                        result.fechaRegistro = oRegistroGasificado.fechaRegistro != null ? oRegistroGasificado.fechaRegistro : (DateTime?)null;
                        result.cantidad = oRegistroGasificado.cantidad != null ? oRegistroGasificado.cantidad : 0;
                        result.hora = oRegistroGasificado.hora != null ? oRegistroGasificado.hora : (DateTime?)null;
                        result.idMovil = oRegistroGasificado.idMovil != null ? oRegistroGasificado.idMovil : "01";
                        result.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "999";
                        result.idusuario = oRegistroGasificado.idusuario != null ? oRegistroGasificado.idusuario : "soporte";
                        result.idmotivo = oRegistroGasificado.idmotivo != null ? oRegistroGasificado.idmotivo : "000";
                        result.glosa = oRegistroGasificado.glosa != null ? oRegistroGasificado.glosa.Trim() : string.Empty;
                        result.idestado = oRegistroGasificado.idestado != null ? oRegistroGasificado.idestado : 1;
                        Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.InsertOnSubmit(result);
                        Modelo.SubmitChanges();
                        numeroOperacion = result.itemDDetalle;
                        #endregion

                    }
                    else if (listResult.ToList().Count == 1)
                    {
                        #region Editar()
                        result = new SAS_RegistroTicketCamaraGasificadoExonerados();
                        result = listResult.ElementAt(0);
                        result.fechaRegistro = oRegistroGasificado.fechaRegistro != null ? oRegistroGasificado.fechaRegistro : (DateTime?)null;
                        result.cantidad = oRegistroGasificado.cantidad != null ? oRegistroGasificado.cantidad : 0;
                        result.hora = oRegistroGasificado.hora != null ? oRegistroGasificado.hora : (DateTime?)null;
                        result.idMovil = oRegistroGasificado.idMovil != null ? oRegistroGasificado.idMovil : "01";
                        result.idCamara = oRegistroGasificado.idCamara != null ? oRegistroGasificado.idCamara : "999";
                        //result.idusuario = oRegistroGasificado.idusuario != null ? oRegistroGasificado.idusuario : "soporte";
                        result.idmotivo = oRegistroGasificado.idmotivo != null ? oRegistroGasificado.idmotivo : "000";
                        result.glosa = oRegistroGasificado.glosa != null ? oRegistroGasificado.glosa.Trim() : string.Empty;
                        // result.idestado = oRegistroGasificado.idestado != null ? oRegistroGasificado.idestado : 0;
                        Modelo.SubmitChanges();
                        numeroOperacion = result.itemDDetalle;
                        #endregion
                    }
                }
            }


            return numeroOperacion;
        }


        public int ToAnulRegister(string conection, SAS_RegistroTicketCamaraGasificadoExonerados oRegistroGasificado)
        {
            int numeroOperacion = 0;
            List<SAS_RegistroTicketCamaraGasificadoExonerados> listResult = new List<SAS_RegistroTicketCamaraGasificadoExonerados>();
            SAS_RegistroTicketCamaraGasificadoExonerados result = new SAS_RegistroTicketCamaraGasificadoExonerados();


            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                listResult = Modelo.SAS_RegistroTicketCamaraGasificadoExonerados.Where(x => x.itemDetalle == oRegistroGasificado.itemDetalle && x.idestado == 1).ToList();
                if (listResult != null)
                {
                    if (listResult.ToList().Count == 1)
                    {
                        #region Editar()
                        result = new SAS_RegistroTicketCamaraGasificadoExonerados();
                        result = listResult.ElementAt(0);
                        result.idestado = 0;
                        Modelo.SubmitChanges();
                        numeroOperacion = result.itemDDetalle;
                        #endregion
                    }
                }
            }


            return numeroOperacion;
        }

    }
}
