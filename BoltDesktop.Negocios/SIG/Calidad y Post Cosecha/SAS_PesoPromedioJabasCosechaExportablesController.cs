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
    public class SAS_PesoPromedioJabasCosechaExportablesController
    {
        public List<SAS_PesoPromedioJabasCosechaExportablesListado> GetListAll(string conection)
        {
            List<SAS_PesoPromedioJabasCosechaExportablesListado> list = new List<SAS_PesoPromedioJabasCosechaExportablesListado>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                list = Modelo.SAS_PesoPromedioJabasCosechaExportablesListado.ToList();
            }
            return list;
        }


        public List<SAS_PesoPromedioJabasCosechaExportablesListado> GetListAll(string conection, string codigoCampana, string codigoCultivo)
        {
            List<SAS_PesoPromedioJabasCosechaExportablesListado> list = new List<SAS_PesoPromedioJabasCosechaExportablesListado>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_PesoPromedioJabasCosechaExportablesListado.ToList();

                if (result != null && result.ToList().Count > 0)
                {
                    var result02 = result.Where(x => x.IdCampana.Trim() == codigoCampana && x.idCultivo.Trim() == codigoCultivo).ToList();

                    if (result02 != null && result02.ToList().Count > 0)
                    {
                        list = result02.OrderBy(x => x.tipoDeCultivo).OrderBy(x => x.variedad).ToList();
                    }

                }
            }
            return list;
        }

        public int ToRegister(string conection, SAS_PesoPromedioJabasCosechaExportables item)
        {
            int result = 0;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                
                var list = Modelo.SAS_PesoPromedioJabasCosechaExportables.Where(x=> x.id == item.id).ToList();
                if (list != null)
                {
                    if (list.ToList().Count == 0)
                    {
                        SAS_PesoPromedioJabasCosechaExportables oItem = new SAS_PesoPromedioJabasCosechaExportables();
                        //oItem.id = item.id;
                        oItem.idempresa = item.idempresa;
                        oItem.tipoCultivo = item.tipoCultivo;
                        oItem.idCultivo = item.idCultivo;
                        oItem.idVariedad = item.idVariedad;
                        oItem.desde = item.desde;
                        oItem.hasta = item.hasta;
                        oItem.valor = item.valor;
                        oItem.estado = 1;
                        oItem.glosa = item.glosa;
                        oItem.registradoPor = Environment.UserName;
                        oItem.fechaCreacion = DateTime.Now;
                        Modelo.SAS_PesoPromedioJabasCosechaExportables.InsertOnSubmit(oItem);
                        Modelo.SubmitChanges();

                    }
                    else if (list.ToList().Count > 1)
                    {
                        SAS_PesoPromedioJabasCosechaExportables oItem = new SAS_PesoPromedioJabasCosechaExportables();
                        oItem = list.ElementAt(0);
                        oItem.idempresa = item.idempresa;
                        oItem.tipoCultivo = item.tipoCultivo;
                        oItem.idCultivo = item.idCultivo;
                        oItem.idVariedad = item.idVariedad;
                        oItem.desde = item.desde;
                        oItem.hasta = item.hasta;
                        oItem.valor = item.valor;
                        oItem.estado = 1;
                        oItem.glosa = item.glosa;
                        Modelo.SubmitChanges();
                    }
                }
            }

            return result;
        }




    }
}
