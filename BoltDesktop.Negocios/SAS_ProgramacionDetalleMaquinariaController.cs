using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class SAS_ProgramacionDetalleMaquinariaController
    {

        public List<SAS_ProgramacionMaquinariaListAllByTurn> GetListAll(string conection, int periodo)
        {

            List<SAS_ProgramacionMaquinariaListAllByTurn> listado = new List<SAS_ProgramacionMaquinariaListAllByTurn>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.SAS_ProgramacionMaquinariaListAllByTurn.Where(x => x.periodo == periodo).ToList();
            }

            return listado;
        }


        public List<SAS_ProgramacionDetalleMaquinariaByIdResult> GetListDetailAllById(string conection, int id)
        {

            List<SAS_ProgramacionDetalleMaquinariaByIdResult> listado = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                listado = Modelo.SAS_ProgramacionDetalleMaquinariaById(id).ToList();
            }

            return listado;
        }


        public int ToRegister(string conection, SAS_ProgramacionMaquinaria oPrograma, List<SAS_ProgramacionDetalleMaquinaria> oDetailListOfProgram)
        {
            int registro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;

                if (oPrograma != null)
                {
                    if (oPrograma.idProgramacionMaquinaria != null)
                    {
                        if (oPrograma.idProgramacionMaquinaria == 0)
                        {
                            #region Nuevo() 
                            SAS_ProgramacionMaquinaria program = new SAS_ProgramacionMaquinaria();
                            program.Idempresa = oPrograma.Idempresa;
                            program.idProgramacionMaquinaria = oPrograma.idProgramacionMaquinaria;
                            program.idSucursal = oPrograma.idSucursal;
                            program.iddocumento = oPrograma.iddocumento;
                            program.periodo = oPrograma.periodo;
                            program.semana = oPrograma.semana;
                            program.idResponsable = oPrograma.idResponsable;
                            program.fechaInicio = oPrograma.fechaInicio;
                            program.fechaFin = oPrograma.fechaFin;
                            program.estado = oPrograma.estado;
                            program.fecharegistro = oPrograma.fecharegistro;
                            program.idusuario = oPrograma.idusuario;
                            Modelo.SAS_ProgramacionMaquinaria.InsertOnSubmit(program);
                            Modelo.SubmitChanges();

                            registro = program.idProgramacionMaquinaria;
                            Modelo.SAS_MaquinariaAProgramacionByWeekToRegister(registro, program.semana, program.periodo);


                            #endregion
                        }
                        else
                        {
                            #region Editar() 
                            SAS_ProgramacionMaquinaria program = new SAS_ProgramacionMaquinaria();
                            program = oPrograma;
                            //Modelo.SAS_ProgramacionMaquinaria.InsertOnSubmit(program);
                            Modelo.SubmitChanges();

                            #endregion
                        }
                    }
                }


            }


            return registro;
        }



        public int ToDelete(string conection, SAS_ProgramacionMaquinaria oPrograma)
        {
            int registro = 0;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;

                if (oPrograma != null)
                {
                    if (oPrograma.idProgramacionMaquinaria != null)
                    {
                        if (oPrograma.idProgramacionMaquinaria > 0)
                        {
                            #region Eliminar() 
                            SAS_ProgramacionMaquinaria program = new SAS_ProgramacionMaquinaria();
                            var result = Modelo.SAS_ProgramacionMaquinaria.Where(x => x.idProgramacionMaquinaria == oPrograma.idProgramacionMaquinaria).ToList();
                            if (result != null && result.ToList().Count == 1)
                            {
                                program = result.ElementAt(0);

                                if (program.estado == 0) // SOLO SE PUEDEN ELIMINAR EN ESTADO PENDIENTE ES DECIR 0, PARA EL CASO 1 ES APROBADO Y 2 ES ANULADO
                                {
                                    List<SAS_ProgramacionDetalleMaquinaria> listadoDetalle = new List<SAS_ProgramacionDetalleMaquinaria>();
                                    listadoDetalle = Modelo.SAS_ProgramacionDetalleMaquinaria.Where(x => x.idProgramacionMaquinaria == program.idProgramacionMaquinaria).ToList();

                                    if (listadoDetalle != null && listadoDetalle.ToList().Count > 0)
                                    {
                                        Modelo.SAS_ProgramacionDetalleMaquinaria.DeleteAllOnSubmit(listadoDetalle);
                                        Modelo.SubmitChanges();
                                    }

                                    Modelo.SAS_ProgramacionMaquinaria.DeleteOnSubmit(program);
                                    Modelo.SubmitChanges();

                                    registro = 1; // CONFIRMAR QUE SE ELIMINO DETALLE Y CABECERA
                                }
                            }

                            #endregion
                        }

                    }
                }


            }


            return registro;
        }

        public List<SAS_ProgramacionDetalleMaquinariaByIdResult> GetListDetailAllByIdBlank(string conection, int idProgramacionMaquinaria, int periodo, short semanaPeriodo)
        {
            List<SAS_ProgramacionDetalleMaquinariaByIdResult> list = new List<SAS_ProgramacionDetalleMaquinariaByIdResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.CommandTimeout = 9999900;
                var result = Modelo.SAS_MaquinariaAProgramacionByWeek(idProgramacionMaquinaria, semanaPeriodo, periodo).ToList();

                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        list = (from item in result
                                group item by item.idProgramacionDetalle into j
                                select new SAS_ProgramacionDetalleMaquinariaByIdResult
                                {
                                    idProgramacionDetalle = Convert.ToInt32(j.Key.Value),
                                    idProgramacionMaquinaria = j.FirstOrDefault().idProgramacionMaquinaria != (int?)null ? j.FirstOrDefault().idProgramacionMaquinaria.Value : 0,
                                    tipoLineaId = j.FirstOrDefault().tipoLineaId != null ? j.FirstOrDefault().tipoLineaId : string.Empty,
                                    tipoLinea = j.FirstOrDefault().tipoLinea != null ? j.FirstOrDefault().tipoLinea : string.Empty,
                                    idSupervisor = j.FirstOrDefault().idSupervisor != null ? j.FirstOrDefault().idSupervisor : string.Empty,
                                    supervisor = j.FirstOrDefault().supervisor != null ? j.FirstOrDefault().supervisor : string.Empty,
                                    idMaquinaria = j.FirstOrDefault().idconsumidor != null ? j.FirstOrDefault().idconsumidor : string.Empty,
                                    maquinaria = j.FirstOrDefault().consumidor != null ? j.FirstOrDefault().consumidor : string.Empty,
                                    idImplemento = j.FirstOrDefault().idImplemento != null ? j.FirstOrDefault().idImplemento : 0,
                                    implemento = j.FirstOrDefault().implemento != null ? j.FirstOrDefault().implemento : string.Empty,
                                    idSector = j.FirstOrDefault().idsector != (int?)null ? j.FirstOrDefault().idsector : 0,
                                    sector = j.FirstOrDefault().sector != null ? j.FirstOrDefault().sector : string.Empty,
                                    fecha = j.FirstOrDefault().fecha.Value,
                                    diurno = Convert.ToByte(j.FirstOrDefault().diurno),
                                    tarde = Convert.ToByte(j.FirstOrDefault().tarde),
                                    noche = Convert.ToByte(j.FirstOrDefault().noches),
                                    estado = Convert.ToByte(j.FirstOrDefault().estado),

                                }
                                ).ToList();
                    }
                }

            }

            return list;

        }

        public List<Grupo> ObtenerListadoDiasSemana(List<SAS_ProgramacionDetalleMaquinariaByIdResult> listing)
        {
            List<Grupo> listado = new List<Grupo>();

            listado = (from item in listing.OrderBy(x=> x.fecha).ToList()
                       group item by item.fecha into j
                       select new Grupo { Codigo = j.Key.ToShortDateString() }
                      ).ToList();

            return listado;
        }
    }


}
