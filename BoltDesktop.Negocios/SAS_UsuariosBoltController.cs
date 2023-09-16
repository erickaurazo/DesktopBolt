using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Asistencia.Negocios
{
    public class SAS_UsuariosBoltController
    {

        // Cuenta usuarios Sistema Rendimiento
        public List<SAS_ListadoUsuarioBolt> GetListUsers(string conection)
        {
            List<SAS_ListadoUsuarioBolt> list = new List<SAS_ListadoUsuarioBolt>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext Modelo = new NSFAJASDataContext(cnx))
            {
                list = Modelo.SAS_ListadoUsuarioBolt.ToList();
            }

            return list;
        }

        public int UpdateUser(string conection, USUARIOSFAJA item)
        {
            int resultado = 0;
            USUARIOSFAJA registroUpdate = new USUARIOSFAJA();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (NSFAJASDataContext model = new NSFAJASDataContext(cnx))
            {
                var result = model.USUARIOSFAJA.Where(x => x.idusuariofaja == item.idusuariofaja).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        #region Editar();
                        registroUpdate = result.ElementAt(0);
                        registroUpdate.perfil = item.perfil;
                        registroUpdate.firma = item.firma != null ? item.firma.Trim() : string.Empty;
                        model.SubmitChanges();
                        resultado = registroUpdate.idusuariofaja;
                        #endregion
                    }
                }

            }

            return resultado;
        }
        
        public List<Grupo> GetTypeUser(string conection)
        {
            List<Grupo> list = new List<Grupo>();
            list.Add(new Grupo { Codigo = "O", Descripcion="Operario", Id = "O" });
            list.Add(new Grupo { Codigo = "S", Descripcion = "Supervisor", Id = "S" });
            list.Add(new Grupo { Codigo = "P", Descripcion = "Supervisor de producción", Id = "P" });
            return list;
        }

        // Cuentas Bolt Web
        public List<SAS_ListadoCuentasBoltWeb> GetListBoltWebUsers(string conection)
        {
            List<SAS_ListadoCuentasBoltWeb> list = new List<SAS_ListadoCuentasBoltWeb>();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext Modelo = new BoltCoorpADDataContext(cnx))
            {
                list = Modelo.SAS_ListadoCuentasBoltWeb.OrderBy(x=> x.ColaboradorNombresCompletos).ToList();
            }
            return list;
        }

        public int ToRegisterUserBoltWeb(string conection, usuarioMaquinaria item)
        {
            int resultado = 0;
            usuarioMaquinaria userBoltWeb = new usuarioMaquinaria();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext model = new BoltCoorpADDataContext(cnx))
            {
                var result = model.usuarioMaquinaria.Where(x => x.idUsuarioMaquinaria == item.idUsuarioMaquinaria).ToList();
                if (result != null)
                {
                    if (result.ToList().Count == 0)
                    {
                        #region Nuevo();
                        userBoltWeb = new usuarioMaquinaria();
                        userBoltWeb.nroDocumento = item.nroDocumento;
                        userBoltWeb.nombres = item.nombres != null ? item.nombres.Trim() : string.Empty;
                        userBoltWeb.codigoTrabajador = item.codigoTrabajador != null ? item.codigoTrabajador.Trim() : string.Empty;
                        userBoltWeb.clave = item.clave != null ? item.clave.Trim() : string.Empty;
                        userBoltWeb.perfil = item.perfil != null ? item.perfil.Trim() : string.Empty;
                        userBoltWeb.estado = item.estado != (char?)null ? Convert.ToChar(item.estado) :Convert.ToChar(1);
                        userBoltWeb.email = item.email != null ? item.email.Trim() : string.Empty;
                        userBoltWeb.usuario = item.usuario != null ? item.usuario.Trim() : string.Empty;
                        userBoltWeb.area = item.area != null ? item.area.Trim() : string.Empty;
                        userBoltWeb.RutaDeFirma = item.RutaDeFirma != null ? item.RutaDeFirma.Trim() : string.Empty;
                        userBoltWeb.FIRMA = item.FIRMA;
                        model.usuarioMaquinaria.InsertOnSubmit(userBoltWeb);
                        model.SubmitChanges();
                        resultado = userBoltWeb.idUsuarioMaquinaria;
                        #endregion
                    }
                    else
                    {
                        #region Editar()
                        userBoltWeb = result.ElementAt(0);
                        userBoltWeb.nroDocumento = item.nroDocumento;
                        userBoltWeb.nombres = item.nombres != null ? item.nombres.Trim() : string.Empty;
                        userBoltWeb.codigoTrabajador = item.codigoTrabajador != null ? item.codigoTrabajador.Trim() : string.Empty;
                        userBoltWeb.clave = item.clave != null ? item.clave.Trim() : string.Empty;
                        userBoltWeb.usuario = item.usuario != null ? item.usuario.Trim() : string.Empty;
                        userBoltWeb.perfil = item.perfil != null ? item.perfil.Trim() : string.Empty;
                        userBoltWeb.estado = item.estado != (char?)null ? Convert.ToChar(item.estado) : Convert.ToChar(1);
                        userBoltWeb.email = item.email != null ? item.email.Trim() : string.Empty;
                        userBoltWeb.area = item.area != null ? item.area.Trim() : string.Empty;
                        userBoltWeb.RutaDeFirma = item.RutaDeFirma != null ? item.RutaDeFirma.Trim() : string.Empty;
                        userBoltWeb.FIRMA = item.FIRMA;
                        model.SubmitChanges();
                        resultado = userBoltWeb.idUsuarioMaquinaria;
                        #endregion
                    }
                }
            }
            return resultado;
        }

        public int ToChangeStatusUserBoltWeb(string conection, usuarioMaquinaria item)
        {
            int resultado = 0;
            usuarioMaquinaria userBoltWeb = new usuarioMaquinaria();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext model = new BoltCoorpADDataContext(cnx))
            {
                var result = model.usuarioMaquinaria.Where(x => x.idUsuarioMaquinaria == item.idUsuarioMaquinaria).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        userBoltWeb = result.ElementAt(0);
                        #region Nuevo();

                        if (userBoltWeb.estado == '1')
                        {
                            userBoltWeb.estado = '0';
                            resultado = 4;
                        }
                        else
                        {
                            userBoltWeb.estado = '1';
                            resultado = 5;
                        }                                                
                        model.SubmitChanges();
                        #endregion
                    }
                }

            }

            return resultado;
        }

        public int Eliminar(string conection, usuarioMaquinaria item)
        {
            int resultado = 0;
            usuarioMaquinaria userBoltWeb = new usuarioMaquinaria();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext model = new BoltCoorpADDataContext(cnx))
            {
                var result = model.usuarioMaquinaria.Where(x => x.idUsuarioMaquinaria == item.idUsuarioMaquinaria).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        #region Nuevo();
                        userBoltWeb = result.ElementAt(0);
                        model.usuarioMaquinaria.DeleteOnSubmit(userBoltWeb);
                        model.SubmitChanges();
                        resultado = 3;
                        #endregion
                    }
                }

            }

            return resultado;
        }


        public void ResetearClave(string conection, usuarioMaquinaria item)
        {
            usuarioMaquinaria userBoltWeb = new usuarioMaquinaria();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext model = new BoltCoorpADDataContext(cnx))
            {
                var result = model.usuarioMaquinaria.Where(x => x.idUsuarioMaquinaria == item.idUsuarioMaquinaria).ToList();

                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        #region AsignarClave();
                        userBoltWeb = result.ElementAt(0);
                        userBoltWeb.clave = userBoltWeb.nroDocumento.Trim();
                        model.SubmitChanges();
                        #endregion
                    }
                }

            }
        }

        public SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos ListarDatosParaAutoCompletarDatosDelUsuario(string conection, string codigoEmpleado)
        {
            List<SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos> list = new List<SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos>();
            SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos user = new SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos();

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (BoltCoorpADDataContext Modelo = new BoltCoorpADDataContext(cnx))
            {
                list = Modelo.SAS_ListadoUsuariosNISIRAvsUsuarioBoltActivos.Where(x => x.PersonalCodigo.Trim() == codigoEmpleado).ToList();

                if (list != null && list.ToList().Count == 1)
                {
                    user = list.ElementAt(0);
                }
            }
            return user;
        }
        
        // Combos para usuarios bolt Web

        public List<Grupo> GetComboBoxArea()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "", Descripcion = "-- Seleccionar Área --" });
            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (BoltCoorpADDataContext Modelo = new BoltCoorpADDataContext(cnx))
            {
                var areasDeTrabajo = Modelo.SAS_ListadoAreaCuentaBolt.ToList();
                if (areasDeTrabajo != null)
                {
                    if (areasDeTrabajo.ToList().Count > 0)
                    {
                        foreach (var item in areasDeTrabajo)
                        {
                            result.Add(new Grupo { Codigo = item.AreaId.Trim(), Descripcion = item.perfil.Trim() });
                        }
                    }
                }
            }
            return result;
        }

        public List<Grupo> GetComboBoxPerfil()
        {
            List<Grupo> result = new List<Grupo>();
            result.Add(new Grupo { Codigo = "", Descripcion = "-- Seleccionar Perfil --" });
            string cnx = ConfigurationManager.AppSettings["SAS"].ToString();
            using (BoltCoorpADDataContext Modelo = new BoltCoorpADDataContext(cnx))
            {
                var areasDeTrabajo = Modelo.SAS_ListadoPerfilCuentaBolt.ToList();
                if (areasDeTrabajo != null)
                {
                    if (areasDeTrabajo.ToList().Count > 0)
                    {
                        foreach (var item in areasDeTrabajo)
                        {
                            result.Add(new Grupo { Codigo = item.PerfilId.Trim(), Descripcion = item.perfil.Trim() });
                        }
                    }
                }
            }
            return result;
        }

    }
}
