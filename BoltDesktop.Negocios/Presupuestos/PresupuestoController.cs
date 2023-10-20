using Asistencia.Datos;
using MyControlsDataBinding.Busquedas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class PresupuestoController
    {
        //const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        //const string passwordCorreo = @"iompqiiuhkjngkjr";

        const string usuarioCorreo = "sistemas.agrosaturno4@gmail.com";
        const string passwordCorreo = @"vyzwygvvycspltii";
        

        //const string correoDestino = "aperturapresupuestos@saturno.net.pe";
        const string correoDestino = "servicios.ti@saturno.net.pe";

        public List<SAS_ListadoPresupuestosResult> GetListView(string conection, SAS_USUARIOS user)
        {
            List<SAS_ListadoPresupuestosResult> listado = new List<SAS_ListadoPresupuestosResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_ListadoPresupuestos().ToList();
            }
            return listado.OrderBy(x => x.IDPRESUPUESTO).ToList();
        }


        public List<PRESUPUESTO> GetList(string conection, SAS_USUARIOS user)
        {
            List<PRESUPUESTO> listado = new List<PRESUPUESTO>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.PRESUPUESTO.ToList();
            }
            return listado.OrderBy(x => x.IDPRESUPUESTO).ToList();
        }



        public List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult> GetListDetail(string conection, string codigoPresupuesto, SAS_USUARIOS user)
        {
            List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult> listado = new List<SAS_PERIODOPRESUPUESTALByIdPresupuestoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                listado = Modelo.SAS_PERIODOPRESUPUESTALByIdPresupuesto(codigoPresupuesto.Trim()).ToList();
            }
            return listado.OrderBy(x => x.IDPERIODOPSTAL).ToList();
        }


        public int ChangeState(string conection, PRESUPUESTO item, SAS_USUARIOS user)
        {

            int tipoResultadoOperacion = 1; // 1 es no hacer nada , 2 es volverlo a pendiente
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.PRESUPUESTO.Where(x => x.IDPRESUPUESTO == item.IDPRESUPUESTO && x.IDESTADO == "AP").ToList();
                if (resultado != null)
                {
                    if (resultado.ToList().Count == 1)
                    {
                        #region Cambiar de estado()
                        PRESUPUESTO oregistro = new PRESUPUESTO();
                        oregistro = resultado.Single();

                        if (oregistro.IDESTADO == "AP")
                        {
                            oregistro.IDESTADO = "PE";
                            tipoResultadoOperacion = 2; // desactivar
                            PresupuestoController model = new PresupuestoController();
                            model.Notify(conection, "Re-apertura presupuesto | ERP NISIRA", item.IDPRESUPUESTO.Trim(), user);
                        }

                        Modelo.SubmitChanges();
                        #endregion                       
                    }
                }
            }
            return tipoResultadoOperacion;
        }


        public void Notify(string conection, string Asunto, string codigoDocumento, SAS_USUARIOS user)
        {
            PRESUPUESTO presupuesto = new PRESUPUESTO();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result01 = Modelo.PRESUPUESTO.Where(x => x.IDPRESUPUESTO.Trim() == codigoDocumento.Trim()).ToList();
                if (result01 != null && result01.ToList().Count > 0)
                {
                    presupuesto = result01.ElementAt(0);
                }

            }

            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica cambio al estado del presupuesto ") + presupuesto.IDPRESUPUESTO.Trim() + ".\n");
                Mensaje.Append(string.Format("Re aperturado por : " + user.NombreCompleto.Trim() + "\n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2023 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));

                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                mail.From = new MailAddress(usuarioCorreo, "Notificaciones - Bolt | Sociedad Agrícola Saturno SA");
                mail.To.Add(correoDestino);
                mail.Subject = Asunto;
                mail.Body = Mensaje.ToString();

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
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

        public int AprobarPresupuesto(string conection, PRESUPUESTO oPresupuesto, SAS_USUARIOS user)
        {
            int tipoResultadoOperacion = 1; // 1 es no hacer nada , 2 es volverlo a pendiente
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var resultado = Modelo.SAS_AprobarPresupuestoByIdPresupuesto(oPresupuesto.IDPRESUPUESTO.Trim());
            }
            return tipoResultadoOperacion;
        }
    }
}
