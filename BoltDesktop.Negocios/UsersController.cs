using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asistencia.Datos;
using System.Configuration;
using System.Transactions;
using System.Net.Mail;

namespace Asistencia.Negocios
{
    public class UsersController
    {
        // clase con los usuarios y privilegios
        const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com"; 
        //const string passwordCorreo = @"YF6TE-XWW79-A77WE-FLLM7-6N5A2";
        const string passwordCorreo = @"iompqiiuhkjngkjr";
        public List<SAS_USUARIOS> GetListAllUser(string conection, string companyId)
        {
            var result = new List<SAS_USUARIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
            {
                result = context.SAS_USUARIOS.ToList();
            }
            return result;
        }

        public List<SAS_ListadoDeUsuariosDelSistema> GetListAllUserSystem(string conection, string companyId)
        {
            var result = new List<SAS_ListadoDeUsuariosDelSistema>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (BoltCoorpADDataContext Modelo = new BoltCoorpADDataContext(cnx))
            {
                result = Modelo.SAS_ListadoDeUsuariosDelSistema.ToList();
            }
            return result;
        }

        public int CopiarCuentasDeERPaBolt(string conection)
        {
            int result = 0;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                Modelo.SAS_CopiarCuentasDeERPaBolt();
                result = 1;
            }

            return result;
        }

        



        public SAS_USUARIOS FindUserByIdUser(string conection, string idUser, string companyId)
        {
            var result = new SAS_USUARIOS();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
            {
                var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == idUser.Trim()).ToList();
                if (resultQuery != null && resultQuery.Count == 1)
                {
                    result = resultQuery.Single();
                }
            }

            return result;
        }

        public List<SAS_USUARIOS> FindUserByIdUser(string conection, string idUser)
        {
            List<SAS_USUARIOS> result = new List<SAS_USUARIOS>();
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
            {
                result = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == idUser.Trim()).ToList();
            }

            return result;
        }


        public bool AddUser(string conection, SAS_USUARIOS user, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == user.IdUsuario.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region Edit()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();
                        oUserNew.IdCodigoGeneral = user.IdCodigoGeneral != null ? user.IdCodigoGeneral.ToUpper().Trim() : string.Empty;
                        //oUserNew.Password = user.Password != null ? user.Password.ToUpper().Trim() : string.Empty;
                        oUserNew.NombreCompleto = user.NombreCompleto != null ? user.NombreCompleto.ToUpper().Trim() : string.Empty;
                        oUserNew.AREA = user.AREA != null ? user.AREA.Trim() : string.Empty;
                        oUserNew.email = user.email != null ? user.email.ToLower().Trim() : string.Empty;
                        oUserNew.idestado = user.idestado != null ? user.idestado.Trim() : string.Empty;
                        oUserNew.Local = user.Local != null ? user.Local.Trim() : string.Empty;
                        oUserNew.nivel = user.nivel != null ? user.nivel.ToUpper().Trim() : string.Empty;
                        oUserNew.IDSUCURSAL = user.IDSUCURSAL != null ? user.IDSUCURSAL.ToUpper().Trim() : string.Empty;
                        oUserNew.SUCURSAL = user.SUCURSAL != null ? user.SUCURSAL.ToUpper().Trim() : string.Empty;
                        oUserNew.id_puerta = user.id_puerta;
                        oUserNew.EmpresaID = "001";
                        oUserNew.puerta = user.puerta != null ? user.puerta.ToUpper().Trim() : string.Empty;
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                    else
                    {
                        #region Add()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew.IdUsuario = user.IdUsuario.ToLower().Trim();
                        oUserNew.IdCodigoGeneral = user.IdCodigoGeneral != null ? user.IdCodigoGeneral.ToUpper().Trim() : string.Empty;
                        oUserNew.Password = user.Password != null ? user.Password.Trim() : string.Empty;
                        oUserNew.NombreCompleto = user.NombreCompleto != null ? user.NombreCompleto.ToUpper().Trim() : string.Empty;
                        oUserNew.AREA = user.AREA != null ? user.AREA.ToUpper().Trim() : string.Empty;
                        oUserNew.email = user.email != null ? user.email.ToLower().Trim() : string.Empty;
                        oUserNew.idestado = user.idestado != null ? user.idestado.ToUpper().Trim() : string.Empty;
                        oUserNew.Local = user.Local != null ? user.Local.ToUpper().Trim() : string.Empty;
                        oUserNew.nivel = user.nivel != null ? user.nivel.ToUpper().Trim() : string.Empty;
                        oUserNew.IDSUCURSAL = user.IDSUCURSAL != null ? user.IDSUCURSAL.ToUpper().Trim() : "001";
                        oUserNew.SUCURSAL = user.SUCURSAL != null ? user.SUCURSAL.ToUpper().Trim() : "SOCIEDAD AGRICOLA SATURNO SA";
                        oUserNew.EmpresaID = user.EmpresaID != null ? user.EmpresaID.ToUpper().Trim() : "001";
                        oUserNew.id_puerta = user.id_puerta;
                        oUserNew.puerta = user.puerta != null ? user.puerta.ToUpper().Trim() : string.Empty;
                        context.SAS_USUARIOS.InsertOnSubmit(oUserNew);
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();
            }
            return status;
        }

        public void EnviarCorreo(string Para,  string Asunto,  SAS_USUARIOS userToModify)
        {
            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica actualización en los privilegios de usuarios.\n"));
                Mensaje.Append(string.Format("Usuario actualizado : " + userToModify.IdUsuario + "\n"));
                Mensaje.Append(string.Format("Este correo ha sido enviado el día {0:dd/MM/yyyy} a las {0:HH:mm:ss} hrs: \n\n", DateTime.Now));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2022 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));

                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mail.From = new MailAddress(usuarioCorreo, "Notificaciones - ERP | Sociedad Agrícola Saturno SA");
                mail.To.Add(Para);
                mail.Subject = Asunto;
                mail.Body = Mensaje.ToString();

                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(usuarioCorreo, passwordCorreo);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                //Error = "Éxito";
                //MessageBox.Show(Error, "Mensaje del sistema");


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString().Trim();
                //MessageBox.Show(Error, "ADVERTANCIA DEL SISTEMA");                
            }
            #endregion
        }

        public bool RemoveUser(string conection, SAS_USUARIOS user, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == user.IdUsuario.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region Remove()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();

                        var privilegesByUser = context.SAS_PrivilegioFormulario.Where(x => x.usuarioCodigo.Trim() == user.IdUsuario.Trim()).ToList();

                        if (privilegesByUser != null && privilegesByUser.ToList().Count > 0)
                        {
                            context.SAS_PrivilegioFormulario.DeleteAllOnSubmit(privilegesByUser);
                            context.SubmitChanges();
                        }

                        context.SAS_USUARIOS.DeleteOnSubmit(oUserNew);
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();

            }
            return status;
        }

        public bool ChangeStateUser(string conection, SAS_USUARIOS user, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == user.IdUsuario.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region Edit()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();
                        if (oUserNew.idestado.Trim() == "1")
                        {
                            oUserNew.idestado = "0";
                        }
                        else
                        {
                            oUserNew.idestado = "1";
                        }

                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();
            }
            return status;
        }

        public bool ResetPasswordByUser(string conection, SAS_USUARIOS userToRegister, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == userToRegister.IdUsuario.Trim() && x.EmpresaID.Trim() == companyId.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region ResetPassword()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();
                        oUserNew.Password = string.Empty;
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();

            }
            return status;
        }


        public bool ChangePasswordByUser(string conection, SAS_USUARIOS userToRegister, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == userToRegister.IdUsuario.Trim() && x.EmpresaID.Trim() == companyId.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region ResetPassword()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();
                        oUserNew.Password = userToRegister.Password;
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();

            }
            return status;
        }


        public bool UpdatePassWordByUser(string conection, SAS_USUARIOS userToRegister, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    var resultQuery = context.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == userToRegister.IdUsuario.Trim()).ToList();
                    if (resultQuery != null && resultQuery.Count == 1)
                    {
                        #region Edit()
                        SAS_USUARIOS oUserNew = new SAS_USUARIOS();
                        oUserNew = resultQuery.Single();
                        oUserNew.Password = userToRegister.Password.Trim();
                        context.SubmitChanges();
                        status = true;
                        #endregion
                    }
                }
                scope.Complete();

            }
            return status;
        }

        public List<PrivilegesByUser> GetListPrivilegesByUser(string conection, string userToConsult, string companyId)
        {
            string cnx = string.Empty;
            List<PrivilegesByUser> result = new List<PrivilegesByUser>();
            cnx = ConfigurationManager.AppSettings[conection];
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {

                Modelo.SAS_CompletarPrivilegiosParaUsuario(userToConsult);

                result = (from p in Modelo.SAS_PrivilegioFormulario
                          join m in Modelo.SAS_FormularioSistema on p.formularioCodigo equals m.formularioCodigo
                          join mm in Modelo.SAS_ModuloSistema on m.moduloCodigo.Trim() equals mm.moduloCodigo.Trim()
                          join usu in Modelo.SAS_USUARIOS on p.usuarioCodigo.Trim() equals usu.IdUsuario.Trim()
                          where p.usuarioCodigo.Trim() == userToConsult.Trim() && usu.EmpresaID.Trim() == companyId
                          select new PrivilegesByUser
                          {
                              usuarioCodigo = p.usuarioCodigo.Trim(),
                              nameUser = usu.NombreCompleto != null ? usu.NombreCompleto.Trim() : string.Empty,
                              formularioCodigo = p.formularioCodigo,
                              nuevo = p.nuevo,
                              editar = p.editar,
                              anular = p.anular,
                              eliminar = p.eliminar,
                              imprimir = p.imprimir,
                              exportar = p.exportar,
                              ninguno = p.ninguno,
                              consultar = p.consultar,
                              jerarquia = m.Jerarquia.Trim(),
                              descripcionFormulario = m.formulario.Trim() + "|  " + m.descripcion.Trim(),
                              moduloCodigo = m.moduloCodigo.Trim(),
                              modulo = mm.descripcion.Trim(),
                              tipoFormulario = m.formulario.Trim(),
                              nombreEnElSistema = m.nombreEnSistema.Trim(),
                              barraPadre = m.barraPadre.Trim(),
                          }).ToList();
            }

            return result.OrderBy(x => x.jerarquia).ToList();
        }



        public List<PrivilegesByUser> ObtenerListadoPrivilegiosPorUsuario(string conection, string userToConsult, string companyId)
        {
            string cnx = string.Empty;
            List<PrivilegesByUser> result = new List<PrivilegesByUser>();
            cnx = ConfigurationManager.AppSettings["SAS"];
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var listadoUsuario = Modelo.SAS_USUARIOS.ToList();

                result = (from p in Modelo.SAS_PrivilegioFormulario
                          join m in Modelo.SAS_FormularioSistema on p.formularioCodigo equals m.formularioCodigo
                          join mm in Modelo.SAS_ModuloSistema on m.moduloCodigo.Trim() equals mm.moduloCodigo.Trim()
                          join usu in Modelo.SAS_USUARIOS on p.usuarioCodigo.Trim() equals usu.IdUsuario.Trim()
                          where p.usuarioCodigo.Trim() == userToConsult.Trim() && usu.EmpresaID.Trim() == companyId
                          select new PrivilegesByUser
                          {
                              usuarioCodigo = p.usuarioCodigo.Trim(),
                              nameUser = usu.NombreCompleto != null ? usu.NombreCompleto.Trim() : string.Empty,
                              formularioCodigo = p.formularioCodigo.Trim(),
                              nuevo = p.nuevo,
                              editar = p.editar,
                              anular = p.anular,
                              eliminar = p.eliminar,
                              imprimir = p.imprimir,
                              exportar = p.exportar,
                              ninguno = p.ninguno,
                              consultar = p.consultar,
                              jerarquia = m.Jerarquia.Trim(),
                              descripcionFormulario = m.formulario.Trim() + "|  " + m.descripcion.Trim(),
                              moduloCodigo = m.moduloCodigo.Trim(),
                              modulo = mm.descripcion.Trim(),
                              tipoFormulario = m.formulario.Trim(),
                              nombreEnElSistema = m.nombreEnSistema.Trim(),
                              barraPadre = m.barraPadre.Trim(),
                          }).ToList();
            }

            return result.OrderBy(x => x.jerarquia).ToList();
        }


        public bool AddListPrivilegesByUser(string conection, List<PrivilegioFormulario> privilegesToAdd, string companyId)
        {
            bool status = false;
            string cnx = string.Empty;
            cnx = ConfigurationManager.AppSettings[conection];
            using (TransactionScope scope = new TransactionScope())
            {
                using (AgroSaturnoDataContext context = new AgroSaturnoDataContext(cnx))
                {
                    if (privilegesToAdd != null && privilegesToAdd.ToList().Count > 0)
                    {
                        foreach (var itemPrivilege in privilegesToAdd)
                        {
                            var resultQuery = context.SAS_PrivilegioFormulario.Where(x => x.formularioCodigo.Trim() == itemPrivilege.formularioCodigo.Trim() && x.usuarioCodigo.Trim() == itemPrivilege.usuarioCodigo.Trim()).ToList();
                            if (resultQuery != null && resultQuery.Count == 1)
                            {
                                var oPrivilege = new SAS_PrivilegioFormulario();
                                oPrivilege = resultQuery.Single();
                                oPrivilege.nuevo = itemPrivilege.nuevo != (byte?)null ? Convert.ToByte(itemPrivilege.nuevo) : Convert.ToByte("0");
                                oPrivilege.editar = itemPrivilege.editar != (byte?)null ? Convert.ToByte(itemPrivilege.editar) : Convert.ToByte("0");
                                oPrivilege.anular = itemPrivilege.anular != (byte?)null ? Convert.ToByte(itemPrivilege.anular) : Convert.ToByte("0");
                                oPrivilege.eliminar = itemPrivilege.eliminar != (byte?)null ? Convert.ToByte(itemPrivilege.eliminar) : Convert.ToByte("0");
                                oPrivilege.imprimir = itemPrivilege.imprimir != (byte?)null ? Convert.ToByte(itemPrivilege.imprimir) : Convert.ToByte("0");
                                oPrivilege.exportar = itemPrivilege.exportar != (byte?)null ? Convert.ToByte(itemPrivilege.exportar) : Convert.ToByte("0");
                                oPrivilege.ninguno = itemPrivilege.ninguno != (byte?)null ? Convert.ToByte(itemPrivilege.ninguno) : Convert.ToByte("0");
                                oPrivilege.consultar = itemPrivilege.consultar != (byte?)null ? Convert.ToByte(itemPrivilege.consultar) : Convert.ToByte("0");
                                context.SubmitChanges();
                                status = true;
                            }
                        }
                    }
                }
                scope.Complete();

            }

            return status;
        }

        public int CopyFromUserID(string _conection, string idUsuario, string idUsuarioBase)
        {
            int resultado = 0;
            string cnx = string.Empty;
            List<SAS_USUARIOS> result = new List<SAS_USUARIOS>();
            List<SAS_USUARIOS> resultBase = new List<SAS_USUARIOS>();
            SAS_USUARIOS item = new SAS_USUARIOS();
            SAS_USUARIOS itemBase = new SAS_USUARIOS();

            cnx = ConfigurationManager.AppSettings[_conection];
            using (AgroSaturnoDataContext modelo = new AgroSaturnoDataContext(cnx))
            {
                result = modelo.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == idUsuario).ToList();
                resultBase = modelo.SAS_USUARIOS.Where(x => x.IdUsuario.Trim() == idUsuarioBase).ToList();

                if (result != null)
                {
                    if (result.ToList().Count > 0)
                    {
                        item = new SAS_USUARIOS();
                        item = result.ElementAt(0);
                    }
                }

                if (resultBase != null)
                {
                    if (resultBase.ToList().Count > 0)
                    {
                        itemBase = new SAS_USUARIOS();
                        itemBase = result.ElementAt(0);
                    }
                }

                if (result.ToList().Count > 0 && resultBase.ToList().Count > 0)
                {
                    #region Eliminar detalle del usuario
                    List<SAS_PrivilegioFormulario> listadoPrivilegiosUsuario = new List<SAS_PrivilegioFormulario>();
                    listadoPrivilegiosUsuario = modelo.SAS_PrivilegioFormulario.Where(x => x.usuarioCodigo.Trim().ToUpper() == idUsuario).ToList();
                    modelo.SAS_PrivilegioFormulario.DeleteAllOnSubmit(listadoPrivilegiosUsuario);
                    modelo.SubmitChanges();


                    #endregion


                    #region obtener detalle del usuario base
                    List<SAS_PrivilegioFormulario> listadoPrivilegiosUsuarioBase = new List<SAS_PrivilegioFormulario>();
                    listadoPrivilegiosUsuarioBase = modelo.SAS_PrivilegioFormulario.Where(x => x.usuarioCodigo.Trim().ToUpper() == idUsuarioBase).ToList();
                    if (listadoPrivilegiosUsuarioBase != null)
                    {
                        if (listadoPrivilegiosUsuarioBase.ToList().Count > 0)
                        {
                            listadoPrivilegiosUsuario = new List<SAS_PrivilegioFormulario>();
                            listadoPrivilegiosUsuario = (from itemPrivilegio in listadoPrivilegiosUsuarioBase
                                                         group itemPrivilegio by new { itemPrivilegio.formularioCodigo } into j
                                                         select new SAS_PrivilegioFormulario
                                                         {
                                                             usuarioCodigo = idUsuario,
                                                             formularioCodigo = j.Key.formularioCodigo,
                                                             nuevo = j.FirstOrDefault().nuevo,
                                                             editar = j.FirstOrDefault().editar,
                                                             anular = j.FirstOrDefault().anular,
                                                             eliminar = j.FirstOrDefault().eliminar,
                                                             imprimir = j.FirstOrDefault().imprimir,
                                                             exportar = j.FirstOrDefault().exportar,
                                                             ninguno = j.FirstOrDefault().ninguno,
                                                             consultar = j.FirstOrDefault().consultar,
                                                         }).ToList();
                        }
                    }
                    #endregion


                    #region Insertar detalle de usuario base a usuario
                    if (listadoPrivilegiosUsuario != null)
                    {
                        if (listadoPrivilegiosUsuario.ToList().Count > 0)
                        {
                            modelo.SAS_PrivilegioFormulario.InsertAllOnSubmit(listadoPrivilegiosUsuario);
                            modelo.SubmitChanges();
                        }
                    }
                    #endregion
                }



            }

            return resultado;
        }
    }
}
