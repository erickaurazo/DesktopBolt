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
  public  class LaboresFiltro
    {

        public List<SAS_ListadoLaboresFiltroProductividadResult> ListAll(string conection)
        {
            List<SAS_ListadoLaboresFiltroProductividadResult> listado = new List<SAS_ListadoLaboresFiltroProductividadResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoLaboresFiltroProductividad().ToList();
            }
            return listado;
        }


        public int ToRegister(string conection, SAS_ActividadLaborProductividadEmpleado Item)
        {
            int Operacion = 0;
            SAS_ActividadLaborProductividadEmpleado ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
            List<SAS_ActividadLaborProductividadEmpleado> listado = new List<SAS_ActividadLaborProductividadEmpleado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ActividadLaborProductividadEmpleados.Where(x => x.ActividadLaborID.Trim() == Item.ActividadLaborID.Trim()).ToList();

                if (listado != null)
                {
                    int RegistrosConsulta01 = listado.ToList().Count();
                    if (RegistrosConsulta01 == 0)
                    {
                        #region Nuevo
                        ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegister.ActividadLaborID = Item.ActividadLaborID;
                        ItemRegister.EmpresaID = Item.EmpresaID;
                        ItemRegister.Estado = 1;
                        Modelo.SAS_ActividadLaborProductividadEmpleados.InsertOnSubmit(ItemRegister);
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 1;
                    }
                    else
                    {
                        #region Editar
                        ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegister = listado.ElementAt(0);                                               
                        ItemRegister.Estado = Item.Estado;                        
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 2;
                    }
                }

            }
            return Operacion;
        }


        public int RevokeRegistration(string conection, SAS_ActividadLaborProductividadEmpleado Item)
        {
            int Operacion = 0;
            SAS_ActividadLaborProductividadEmpleado ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
            List<SAS_ActividadLaborProductividadEmpleado> listado = new List<SAS_ActividadLaborProductividadEmpleado>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ActividadLaborProductividadEmpleados.Where(x => x.ActividadLaborID.Trim() == Item.ActividadLaborID.Trim()).ToList();

                if (listado != null)
                {
                    int RegistrosConsulta01 = listado.ToList().Count();
                    if (RegistrosConsulta01 == 0)
                    {
                        #region Nuevo
                        ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegister.ActividadLaborID = Item.ActividadLaborID;
                        ItemRegister.EmpresaID = Item.EmpresaID;
                        ItemRegister.Estado = 1;
                        Modelo.SAS_ActividadLaborProductividadEmpleados.InsertOnSubmit(ItemRegister);
                        Modelo.SubmitChanges();
                        #endregion
                        Operacion = 1;
                    }
                    else
                    {
                        #region Editar
                        ItemRegister = new SAS_ActividadLaborProductividadEmpleado();
                        ItemRegister = listado.ElementAt(0);
                        if (ItemRegister.Estado == 1)
                        {
                            ItemRegister.Estado = 0;
                        }
                        else
                        {
                            ItemRegister.Estado = 1;
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
