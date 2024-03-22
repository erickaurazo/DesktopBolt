using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_CalidadFrutaVariedadController
    {
        public List<SAS_ListadoVariedadPorTipoCultivo> ListadoVariedadesPorCultivoViewByCC(string conection)
        {
            List<SAS_ListadoVariedadPorTipoCultivo> result = new List<SAS_ListadoVariedadPorTipoCultivo>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_ListadoVariedadPorTipoCultivo.ToList();
            }
            return result;
        }

        public List<SAS_CalidadFrutaVariedadListadoAll> ListadoVariedadesPorCultivoViewAll(string conection)
        {
            List<SAS_CalidadFrutaVariedadListadoAll> result = new List<SAS_CalidadFrutaVariedadListadoAll>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_CalidadFrutaVariedadListadoAll.ToList();
            }
            return result;
        }



        public List<SAS_CalidadFrutaVariedad> SAS_CalidadFrutaVariedadAll(string conection)
        {
            List<SAS_CalidadFrutaVariedad> result = new List<SAS_CalidadFrutaVariedad>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_CalidadFrutaVariedad.ToList();
            }
            return result;
        }


        public int ToRegister(string conection, SAS_CalidadFrutaVariedad item)
        {
            int RetornoOperacion = 0;
            List<SAS_CalidadFrutaVariedad> result = new List<SAS_CalidadFrutaVariedad>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_CalidadFrutaVariedad.Where(x => x.idEmpresa == item.idEmpresa && x.idCultivo == item.idCultivo && x.idVariedad == item.idVariedad && x.tipoCultivo == item.tipoCultivo && x.desde == item.desde  && x.idCalidad == item.idCalidad).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 0)
                    {
                        #region Registro nuevo()
                        SAS_CalidadFrutaVariedad oCalidad = new SAS_CalidadFrutaVariedad();
                        oCalidad.idEmpresa = item.idEmpresa;
                        oCalidad.idCultivo = item.idCultivo;
                        oCalidad.idVariedad = item.idVariedad;
                        oCalidad.tipoCultivo = item.tipoCultivo;
                        oCalidad.idCalidad = item.idCalidad;
                        oCalidad.desde = item.desde;
                        oCalidad.hasta = item.hasta;
                        oCalidad.descripcion = item.descripcion;
                        oCalidad.descripcion2 = item.descripcion2;
                        oCalidad.descripcion3 = item.descripcion3;
                        oCalidad.estado = 1;
                        Modelo.SAS_CalidadFrutaVariedad.InsertOnSubmit(oCalidad);
                        Modelo.SubmitChanges();
                        RetornoOperacion = 1;
                        #endregion
                    }
                    else
                    {
                        #region Editar();
                        SAS_CalidadFrutaVariedad oCalidad = new SAS_CalidadFrutaVariedad();
                        oCalidad = result.ElementAt(0);
                        oCalidad.hasta = item.hasta;
                        oCalidad.descripcion = item.descripcion;
                        oCalidad.descripcion2 = item.descripcion2;
                        oCalidad.descripcion3 = item.descripcion3;                        
                        Modelo.SubmitChanges();
                        RetornoOperacion = 2;
                        #endregion
                    }
                }

            }
            return RetornoOperacion;
        }


        public int ChangeState(string conection, SAS_CalidadFrutaVariedad item)
        {
            int operacion = 0;
            List<SAS_CalidadFrutaVariedad> result = new List<SAS_CalidadFrutaVariedad>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_CalidadFrutaVariedad.Where(x => x.idEmpresa == item.idEmpresa && x.idCultivo == item.idCultivo && x.idVariedad == item.idVariedad && x.tipoCultivo == item.tipoCultivo && x.hasta == item.hasta && x.idCalidad == item.idCalidad).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        #region Registro ();
                        SAS_CalidadFrutaVariedad oCalidad = new SAS_CalidadFrutaVariedad();
                        oCalidad = result.ElementAt(0);

                        if (oCalidad.estado == 1)
                        {
                            oCalidad.estado = 0;
                        }
                        else
                        {
                            oCalidad.estado = 1;
                        }

                        Modelo.SubmitChanges();
                        operacion = 1;
                        #endregion
                    }
                }

                return operacion;
            }
        }


        public int DeleteRecord(string conection, SAS_CalidadFrutaVariedad item)
        {
            int operacion = 0;
            List<SAS_CalidadFrutaVariedad> result = new List<SAS_CalidadFrutaVariedad>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                result = Modelo.SAS_CalidadFrutaVariedad.Where(x => x.idEmpresa == item.idEmpresa && x.idCultivo == item.idCultivo && x.idVariedad == item.idVariedad && x.tipoCultivo == item.tipoCultivo &&  x.hasta == item.hasta && x.idCalidad == item.idCalidad).ToList();
                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        #region Registro ();
                        SAS_CalidadFrutaVariedad oCalidad = new SAS_CalidadFrutaVariedad();
                        oCalidad = result.ElementAt(0);
                        Modelo.SAS_CalidadFrutaVariedad.DeleteOnSubmit(oCalidad);
                        Modelo.SubmitChanges();
                        operacion = 1;
                        #endregion
                    }
                }
                return operacion;
            }
        }
    }
}
