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
    public class ProgramaSemanaController
    {
        //const string usuarioCorreo = "notify.bolt.agrosaturno@outlook.com";
        //const string passwordCorreo = @"iompqiiuhkjngkjr";
        const string usuarioCorreo = "notify.bolt@gmail.com";
        const string passwordCorreo = "wppi kiav vegf sesx";

        public List<SAS_ProgramaSemanalListadoByPeriodoResult> ListadoProgramasPorPeriodos(string conection, string desde, string hasta)
        {
            List<SAS_ProgramaSemanalListadoByPeriodoResult> listado = new List<SAS_ProgramaSemanalListadoByPeriodoResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ProgramaSemanalListadoByPeriodo(desde, hasta).ToList();
            }
            return listado;
        }

        public List<SAS_ProgramaSemanalListadoByIDResult> ObtenerDetallePorID(string conection, string ID)
        {
            List<SAS_ProgramaSemanalListadoByIDResult> listado = new List<SAS_ProgramaSemanalListadoByIDResult>();
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                listado = Modelo.SAS_ProgramaSemanalListadoByID(ID).ToList();
            }
            return listado;
        }

        public string ActualizarDosis(string connection, PROGRAMASEMANA programa, List<DPROGRAMASEMANA> detallePrograma, List<DPROGRAMASEMANA> ListadoDetalleAnterior)
        {
            string ID = string.Empty;

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                var result01 = Modelo.PROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == programa.IDPROGRAMASEMANA.Trim()).ToList();

                if (result01 != null && result01.ToList().Count > 0)
                {
                    #region Edición() 
                    PROGRAMASEMANA oPrograma = new PROGRAMASEMANA();
                    oPrograma = result01.ElementAt(0);

                    if (detallePrograma != null && detallePrograma.ToList().Count > 0)
                    {
                        foreach (var item in detallePrograma)
                        {
                            var result02 = Modelo.DPROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == item.IDPROGRAMASEMANA.Trim() && x.ITEM.Trim() == item.ITEM.Trim()).ToList();
                            if (result02 != null && result01.ToList().Count == 1)
                            {
                                DPROGRAMASEMANA oDetallePrograma = new DPROGRAMASEMANA();
                                oDetallePrograma = result02.ElementAt(0);
                                ID = item.IDPROGRAMASEMANA.Trim();
                                oDetallePrograma.cantidad_hectarea = item.cantidad_hectarea.Value;
                                oDetallePrograma.AREA = item.AREA.Value;
                                oDetallePrograma.total = item.total.Value;
                                Modelo.SubmitChanges();

                                DetalleRequerimientosInternos oDetalleRequerimiento = new DetalleRequerimientosInternos();
                                var result03 = Modelo.DetalleRequerimientosInternos.Where(x =>
                                x.IDREQINTERNO.Trim() == oPrograma.IDREQINTERNO.Trim() &&
                                x.IDCONSUMIDOR.Trim() == oDetallePrograma.IDCONSUMIDOR.Trim() &&
                                 x.IDPRODUCTO.Trim() == oDetallePrograma.IDPRODUCTO.Trim()
                                ).ToList();

                                if (result03 != null && result03.ToList().Count == 1)
                                {
                                    oDetalleRequerimiento = result03.ElementAt(0);
                                    oDetalleRequerimiento.CANTIDAD = oDetallePrograma.total.Value;
                                    Modelo.SubmitChanges();
                                }

                            }

                        }
                    }


                    #endregion
                }
            }
            return ID;
        }

        public void ActualizarDosis(string conection, PROGRAMASEMANA programa, List<DPROGRAMASEMANA> detallePrograma, string EmailPara, string Asunto, List<DPROGRAMASEMANA> _listadoDetalleAnterior)
        {
            List<DPROGRAMASEMANA> ListadoDetalleByItem = new List<DPROGRAMASEMANA>();
            List<DPROGRAMASEMANA> ListadoDetalleAnterior = new List<DPROGRAMASEMANA>();
            ListadoDetalleAnterior = _listadoDetalleAnterior;
            List<DPROGRAMASEMANA> ListadoDetalleNuevo = new List<DPROGRAMASEMANA>();
            //ListadoDetalleAnterior = detallePrograma;
            string ProgramaSemanalNumero = string.Empty;
            string ProgramaSemanalFecha = DateTime.Now.ToPresentationDate();
            string ProgramaSemanalResponsable = string.Empty;
            string ProgramaSemanalTipoRecurso = string.Empty;
            string ID = string.Empty;

            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                var result01 = Modelo.PROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == programa.IDPROGRAMASEMANA.Trim()).ToList();
                

                if (result01 != null && result01.ToList().Count > 0)
                {
                    ProgramaSemanalNumero = result01.ElementAt(0).NUMERO.ToString();
                    ProgramaSemanalFecha = result01.ElementAt(0).FECHA.ToPresentationDate();
                    ProgramaSemanalResponsable = result01.ElementAt(0).IDRESPONSABLE != null ? Modelo.ResponsableProgramaSemanals.Where(x => x.IDRESPONSABLE.Trim() == result01.ElementAt(0).IDRESPONSABLE.ToString()).FirstOrDefault().NOMBRE.Trim() : string.Empty;
                    ProgramaSemanalTipoRecurso = result01.ElementAt(0).IDTIPORECURSO != null ? Modelo.TipoRecursoProgramaSemanals.Where(x => x.idtiporecurso.Trim() == result01.ElementAt(0).IDTIPORECURSO.ToString()).FirstOrDefault().descripcion.Trim() : string.Empty;

                    #region Edición() 
                    PROGRAMASEMANA oPrograma = new PROGRAMASEMANA();
                    oPrograma = result01.ElementAt(0);

                    if (detallePrograma != null && detallePrograma.ToList().Count > 0)
                    {
                        foreach (var item in detallePrograma)
                        {
                            ListadoDetalleByItem = new List<DPROGRAMASEMANA>();
                            ListadoDetalleByItem = Modelo.DPROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == item.IDPROGRAMASEMANA.Trim() && x.ITEM.Trim() == item.ITEM.Trim()).ToList();

                            if (ListadoDetalleByItem != null && ListadoDetalleByItem.ToList().Count == 1)
                            {
                                DPROGRAMASEMANA oDetallePrograma = new DPROGRAMASEMANA();
                                oDetallePrograma = ListadoDetalleByItem.ElementAt(0);
                                ID = item.IDPROGRAMASEMANA.Trim();
                                oDetallePrograma.cantidad_hectarea = item.cantidad_hectarea.Value;
                                oDetallePrograma.AREA = item.AREA.Value;
                                oDetallePrograma.total = item.total.Value;
                                Modelo.SubmitChanges();
                                ListadoDetalleNuevo.Add(oDetallePrograma);

                                DetalleRequerimientosInternos oDetalleRequerimiento = new DetalleRequerimientosInternos();
                                var result03 = Modelo.DetalleRequerimientosInternos.Where(x =>
                                x.IDREQINTERNO.Trim() == oPrograma.IDREQINTERNO.Trim() &&
                                x.IDCONSUMIDOR.Trim() == oDetallePrograma.IDCONSUMIDOR.Trim() &&
                                 x.IDPRODUCTO.Trim() == oDetallePrograma.IDPRODUCTO.Trim()
                                ).ToList();

                                if (result03 != null && result03.ToList().Count == 1)
                                {
                                    oDetalleRequerimiento = result03.ElementAt(0);
                                    oDetalleRequerimiento.CANTIDAD = oDetallePrograma.total.Value;
                                    Modelo.SubmitChanges();

                                }

                            }

                        }
                    }


                    #endregion
                }
            }

            #region  Notify()
            StringBuilder Mensaje = new StringBuilder();
            try
            {
                Mensaje.Append(string.Format("Envio Automático, no responder a este correo \n"));
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Por el presente se notifica la modificación al Programa de semanal agrícola: " + ProgramaSemanalNumero + "\n"));
                Mensaje.Append(string.Format("Fecha de solicitud : " + ProgramaSemanalFecha + "\n"));
                Mensaje.Append(string.Format("Responsable del programa : " + "" + ProgramaSemanalResponsable + "" + "\n"));
                Mensaje.Append(string.Format("Tipo de Programa : " + ProgramaSemanalTipoRecurso + "\n"));
                Mensaje.Append(string.Format("Detalle del plan original" + "\n"));

                if (ListadoDetalleAnterior != null && ListadoDetalleAnterior.ToList().Count > 0)
                {
                    foreach (var itemAnterior in ListadoDetalleAnterior)
                    {
                        Mensaje.Append(string.Format("Item : " + itemAnterior.ITEM.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("CC : " + itemAnterior.IDCONSUMIDOR.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("Producto : " + itemAnterior.IDPRODUCTO.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("Area : " + itemAnterior.AREA.Value.ToDecimalPresentation() + "\n"));
                        Mensaje.Append(string.Format("Dosis : " + String.Format("{0:#,##0.0000}", itemAnterior.cantidad_hectarea.Value.ToString()) + "\n"));
                        Mensaje.Append(string.Format("Total : " + String.Format("{0:#,##0.0000}", itemAnterior.total.Value.ToString()) + "\n"));
                        Mensaje.Append(Environment.NewLine);
                        Mensaje.Append(Environment.NewLine);
                    }
                }

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Detalle del plan modificado" + "\n"));
                if (ListadoDetalleNuevo != null && ListadoDetalleNuevo.ToList().Count > 0)
                {
                    foreach (var itemActual in ListadoDetalleNuevo)
                    {
                        Mensaje.Append(string.Format("Item : " + itemActual.ITEM.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("CC : " + itemActual.IDCONSUMIDOR.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("Producto : " + itemActual.IDPRODUCTO.ToString().Trim() + "\n"));
                        Mensaje.Append(string.Format("Area : " + itemActual.AREA.Value.ToDecimalPresentation() + "\n"));
                        Mensaje.Append(string.Format("Dosis : " + String.Format("{0:#,##0.0000}", itemActual.cantidad_hectarea.Value.ToString()) + "\n"));
                        Mensaje.Append(string.Format("Total : " + String.Format("{0:#,##0.0000}", itemActual.total.Value.ToString()) + "\n"));
                        Mensaje.Append(Environment.NewLine);
                        Mensaje.Append(Environment.NewLine);
                    }
                }
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Equipo del área de Innovación y Transformación Digital - 2024 \n"));
                Mensaje.Append(string.Format("Área de Innovación y transformación digital | Sociedad Agrícola Saturno S.A \n"));
                Mensaje.Append(string.Format("Correo electrónico: soporte@saturno.net.pe  \n"));
                Mensaje.Append(string.Format("Carr.Chulucanas TamboGrande Nro. 13 Piura - Morropón - Chulucanas \n"));
                Mensaje.Append(string.Format("Celular: 947 411 097 \n"));
                MailMessage mail = new MailMessage();
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.From = new MailAddress(usuarioCorreo, "Notificaciones - ERP | Sociedad Agrícola Saturno SA");
                //EmailPara = "sistemas.agrosaturno@gmail.com";
                mail.To.Add(EmailPara);
                mail.Subject = Asunto + " Mofificación al programa semanal N° " + ProgramaSemanalNumero + "";
                mail.Body = Mensaje.ToString();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(usuarioCorreo, passwordCorreo);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Mensaje = null;
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

        public List<DPROGRAMASEMANA> ObtenerListadoDetalle(string connection, string programaSemanalID)
        {
            List<DPROGRAMASEMANA> listado = new List<DPROGRAMASEMANA>();
            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {

                listado = Modelo.DPROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == programaSemanalID.Trim()).ToList();

            }

            return listado;
        }

        public string CambiarEstadoProgramaSemanal(string connection, PROGRAMASEMANA programa, String _EstadoAActualizar)
        {
            string ID = string.Empty;

            string cnx = ConfigurationManager.AppSettings[connection].ToString();
            using (PlaneamientoAgricolaDBDataContext Modelo = new PlaneamientoAgricolaDBDataContext(cnx))
            {
                var result01 = Modelo.PROGRAMASEMANAs.Where(x => x.IDPROGRAMASEMANA.Trim() == programa.IDPROGRAMASEMANA.Trim()).ToList();

                if (result01 != null && result01.ToList().Count > 0)
                {
                    #region Edición() 
                    PROGRAMASEMANA oPrograma = new PROGRAMASEMANA();
                    oPrograma = result01.ElementAt(0);
                    oPrograma.IDESTADO = _EstadoAActualizar;
                    Modelo.SubmitChanges();
                    #endregion
                }
            }

            return ID;
        }


    }
}
