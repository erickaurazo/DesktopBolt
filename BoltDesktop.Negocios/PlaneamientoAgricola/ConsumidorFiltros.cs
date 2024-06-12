using Asistencia.Datos;
using MyControlsDataBinding.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;


namespace Asistencia.Negocios.PlaneamientoAgricola
{
    public class ConsumidorFiltros
    {
        public List<SAS_ListadoConsumidoresFiltroProductividadResult> ListAll(string conection)
        {
            List<SAS_ListadoConsumidoresFiltroProductividadResult> listado = new List<SAS_ListadoConsumidoresFiltroProductividadResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoConsumidoresFiltroProductividad().ToList();
            }
            return listado;
        }


        public int ToRegister(string conection, SAS_ConsumidorProductividadEmpleado Consumidor)
        {
            int Operacion = 0;
            SAS_ConsumidorProductividadEmpleado ConsumidorRegister = new SAS_ConsumidorProductividadEmpleado();
            List<SAS_ConsumidorProductividadEmpleado> listado = new List<SAS_ConsumidorProductividadEmpleado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ConsumidorProductividadEmpleados.Where(x => x.ConsumidorID.Trim() == Consumidor.ConsumidorID.Trim()).ToList();

                if (listado != null)
                {
                    int RegistrosConsulta01 = listado.ToList().Count();
                    if (RegistrosConsulta01 == 0)
                    {
                        #region Nuevo
                        SAS_ConsumidorProductividadEmpleado oConsumidor = new SAS_ConsumidorProductividadEmpleado();
                        oConsumidor.ConsumidorID = Consumidor.ConsumidorID;
                        oConsumidor.EmpresaID = Consumidor.EmpresaID;
                        oConsumidor.Estado = 1;
                        Modelo.SAS_ConsumidorProductividadEmpleados.InsertOnSubmit(oConsumidor);
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 1;
                    }
                    else
                    {
                        #region Editar
                        SAS_ConsumidorProductividadEmpleado oConsumidor = new SAS_ConsumidorProductividadEmpleado();
                        oConsumidor = listado.ElementAt(0);
                        // oConsumidor.ConsumidorID = Consumidor.ConsumidorID;
                        // oConsumidor.EmpresaID = Consumidor.EmpresaID;
                        oConsumidor.Estado = Consumidor.Estado;
                        // Modelo.SAS_ConsumidorProductividadEmpleados.InsertOnSubmit(oConsumidor);
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 2;
                    }
                }

            }
            return Operacion;
        }


        public int RevokeRegistration(string conection, SAS_ConsumidorProductividadEmpleado Consumidor)
        {
            int Operacion = 0;
            SAS_ConsumidorProductividadEmpleado ConsumidorRegister = new SAS_ConsumidorProductividadEmpleado();
            List<SAS_ConsumidorProductividadEmpleado> listado = new List<SAS_ConsumidorProductividadEmpleado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ConsumidorProductividadEmpleados.Where(x => x.ConsumidorID.Trim() == Consumidor.ConsumidorID.Trim()).ToList();

                if (listado != null)
                {
                    int RegistrosConsulta01 = listado.ToList().Count();
                    if (RegistrosConsulta01 == 0)
                    {
                        #region Registrar()
                        SAS_ConsumidorProductividadEmpleado oConsumidor = new SAS_ConsumidorProductividadEmpleado();
                        oConsumidor.ConsumidorID = Consumidor.ConsumidorID;
                        oConsumidor.EmpresaID = Consumidor.EmpresaID;
                        oConsumidor.Estado = Consumidor.Estado;
                        Modelo.SAS_ConsumidorProductividadEmpleados.InsertOnSubmit(oConsumidor);
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 1;
                    }
                    else
                    {
                        #region Editar
                        SAS_ConsumidorProductividadEmpleado oConsumidor = new SAS_ConsumidorProductividadEmpleado();
                        oConsumidor = listado.ElementAt(0);
                        if (oConsumidor.Estado == 1)
                        {
                            oConsumidor.Estado = 0;
                        }
                        else
                        {
                            oConsumidor.Estado = 1;
                        }
                       

                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 2;
                    }
                }

            }
            return Operacion;
        }

    }
}
