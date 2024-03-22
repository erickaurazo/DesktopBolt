using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios.ITD.Cuentas
{
    public class NISIRAERPCuentasController
    {


        public List<SAS_ListadoCuentasERPALLResult> ListarTodos(string conection)
        {
            List<SAS_ListadoCuentasERPALLResult> ListadoRutas = new List<SAS_ListadoCuentasERPALLResult>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();

            using (ITDContextDataContext Modelo = new ITDContextDataContext(cnx))
            {
                ListadoRutas = Modelo.SAS_ListadoCuentasERPALL().ToList();
                Modelo.Connection.Close();
            }
            return ListadoRutas;
        }


        public void EnviarAReseteo(string CuentaDeUsuarioID, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings[conection].ToString();

                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    var result = Modelo.USUARIOs.Where(x => x.IDUSUARIO.Trim() == CuentaDeUsuarioID).ToList();

                    if (result.Count >= 1)
                    {
                        //Anular
                        try
                        {
                            USUARIO CuentaUsuario = new USUARIO();
                            CuentaUsuario = result.ElementAt(0);
                            CuentaUsuario.PASSWORD = string.Empty;
                            CuentaUsuario.ESTADO = 1;
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }

        public void SuspenderTemporalmenteCuenta(string CuentaDeUsuarioID, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings[conection].ToString();

                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    var result = Modelo.USUARIOs.Where(x => x.IDUSUARIO.Trim() == CuentaDeUsuarioID).ToList();

                    if (result.Count >= 1)
                    {
                        //Anular
                        try
                        {
                            USUARIO CuentaUsuario = new USUARIO();
                            CuentaUsuario = result.ElementAt(0);
                            CuentaUsuario.PASSWORD = "VACACIONES";
                            CuentaUsuario.ESTADO = 0;
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }


        public void SuspenderDefiniticamenteLaCuenta(string CuentaDeUsuarioID, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings[conection].ToString();

                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    var result = Modelo.USUARIOs.Where(x => x.IDUSUARIO.Trim() == CuentaDeUsuarioID).ToList();

                    if (result.Count >= 1)
                    {
                        //Anular
                        try
                        {
                            USUARIO CuentaUsuario = new USUARIO();
                            CuentaUsuario = result.ElementAt(0);
                            CuentaUsuario.PASSWORD = "RETIRADO";
                            CuentaUsuario.ESTADO = 0;
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }
        public void ActivarCuenta(string CuentaDeUsuarioID, string conection)
        {
            using (TransactionScope Scope = new TransactionScope())
            {
                #region Transacción()

                string cnx = string.Empty;

                cnx = ConfigurationManager.AppSettings[conection].ToString();

                using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
                {
                    var result = Modelo.USUARIOs.Where(x => x.IDUSUARIO.Trim() == CuentaDeUsuarioID).ToList();

                    if (result.Count >= 1)
                    {
                        //Anular
                        try
                        {
                            USUARIO CuentaUsuario = new USUARIO();
                            CuentaUsuario = result.ElementAt(0);
                            //CuentaUsuario.PASSWORD = "RETIRADO";
                            CuentaUsuario.ESTADO = 1;
                            Modelo.SubmitChanges();
                        }
                        catch (Exception Ex)
                        {

                            throw Ex;
                        }

                    }

                }
                #endregion

                Scope.Complete();
            }
        }


        

    }
}
