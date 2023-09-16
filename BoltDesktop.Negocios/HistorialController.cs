using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;

namespace Asistencia.Negocios
{
    public class HistorialController
    {

        List<HistorialObj> historiales;

        public List<HistorialObj> ListarHistorialSJ(string idHistorial, string conection)
        {
            historiales = new List<HistorialObj>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 98000;
                if (Modelo.SJ_LogTablas.Where(x => x.IDLOG.ToString().Trim() == idHistorial.ToString().Trim()).ToList().Count > 0)
                {
                    historiales = (from items in Modelo.SJ_LogTablas.Where(x => x.IDLOG.ToString().Trim() == idHistorial.ToString().Trim()).OrderBy(x => x.ITEM).ToList()
                                   group items by new { items.ITEM } into j
                                   select new HistorialObj
                                   {
                                       evento = j.FirstOrDefault().EVENTO.ToString().Trim(),
                                       usuario = j.FirstOrDefault().IDUSUARIO.ToString().Trim(),
                                       fecha = j.FirstOrDefault().FECHACREACION.Value,
                                       maquina = j.FirstOrDefault().MAQUINA.ToString().Trim(),
                                   }
                                       ).ToList();
                }
                else
                {
                    historiales = new List<HistorialObj>();
                }
            }

            return historiales;
        }

        public List<HistorialObj> ListarHistorialSJ(string loggerId, string table, string conection)
        {
            historiales = new List<HistorialObj>();

            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 98000;


                var resultadoConsultaHistorial = Modelo.SJ_ListarLogDeTablasxCodigoLogxNombreTabla(loggerId, table).ToList();


                if (resultadoConsultaHistorial != null && resultadoConsultaHistorial.ToList().Count > 0)
                {
                    historiales = (from items in resultadoConsultaHistorial
                                   group items by new { items.ITEM } into j
                                   select new HistorialObj
                                   {
                                       evento = j.FirstOrDefault().EVENTO.ToString().Trim(),
                                       usuario = j.FirstOrDefault().IDUSUARIO.ToString().Trim(),
                                       fecha = j.FirstOrDefault().FECHACREACION.Value,
                                       maquina = j.FirstOrDefault().MAQUINA.ToString().Trim(),
                                   }
                                       ).ToList();
                }
                else
                {
                    historiales = new List<HistorialObj>();
                }
            }

            return historiales;
        }

        public string AsignarNumeroItemHistorial(string conection, string idLog)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            string correlativo = "001";
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 980000;

                int? numeroItem = Modelo.SJ_LogTableObtenerNumeroItem(idLog).FirstOrDefault().Column1;
                Modelo.Connection.Close();
                correlativo = numeroItem.Value.ToString().PadLeft(3, '0');

            }

            return correlativo;
        }

        public void RegistrarHistorial(SJ_LogTablas historial, string conection)
        {
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                Modelo.CommandTimeout = 98000;
                if (historial.TABLA != null && historial.IDUSUARIO != null && historial.IDLOG != null)
                {
                    #region MyRegion
                    SJ_LogTablas oHistorial = new SJ_LogTablas();
                    oHistorial.IDEMPRESA = "001";
                    oHistorial.IDLOG = historial.IDLOG.ToString().Trim();
                    oHistorial.ITEM = AsignarNumeroItemHistorial(conection, historial.IDLOG);
                    oHistorial.TABLA = historial.TABLA.ToString().Trim();
                    oHistorial.IDCAMPO = historial.IDCAMPO.ToString().Trim();
                    oHistorial.CAMPOCLAVE = historial.CAMPOCLAVE.ToString().Trim();
                    oHistorial.IDTABLA = historial.IDTABLA.ToString().Trim();
                    oHistorial.EVENTO = historial.EVENTO.ToString().Trim();
                    oHistorial.VALORANTERIOR = historial.VALORANTERIOR;
                    oHistorial.VALORACTUAL = historial.VALORACTUAL;
                    oHistorial.IDUSUARIO = historial.IDUSUARIO;
                    oHistorial.MAQUINA = historial.MAQUINA.ToString().Trim();
                    oHistorial.FECHACREACION = DateTime.Now;
                    oHistorial.VENTANA = historial.VENTANA.ToString().Trim();
                    Modelo.SJ_LogTablas.InsertOnSubmit(oHistorial);
                    Modelo.SubmitChanges();
                    #endregion
                }
                Modelo.Connection.Close();
            }
        }


    }
}
