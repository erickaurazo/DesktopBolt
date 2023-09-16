using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Asistencia.Helper
{
    public class GlobalesHelper
    {
        public Grupo GetConfigInitialByLogin()
        {
            string cnx = string.Empty;
            var config = new Grupo();
            string path = Path.Combine(@"C:\SOLUTION\AsistenciaConfig.txt");
            path = Path.GetFullPath(path);
            string[] lines = System.IO.File.ReadAllLines(path);
            int count = 0;
            foreach (string line in lines)
            {
                count += 1;
                switch (count)
                {
                    case 2:
                        // usuario que se conecta a la base de datos
                        var db01 = line.Split(':');
                        config.userBydb = db01[1].Trim();
                        break;

                    case 3:
                        // clave del usuario que se conecta a la base de datos
                        var db02 = line.Split(':');
                        config.passwordBydb = db02[1].Trim();
                        break;

                    case 4:
                        //Instancia local
                        var db03 = line.Split(':');
                        config.isntanceLocal = db03[1].Trim();
                        break;

                    case 5:
                        //Instancia publica
                        var db04 = line.Split(':');
                        config.isntancePublic = db04[1].Trim();
                        break;


                    default:
                        break;
                }
                // Use a tab to indent each line of the file.               
            }

            return config;
        }



        public void ObtenerFechasMes(
            RadDropDownList cboMes,
            MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtdateFrom,
            MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtdateUp,
            RadSpinEditor txtperiod)
        {
            DateTime fecha1;
            DateTime fecha2;

            if (cboMes.SelectedValue.ToString() != "00")
            {
                #region
                txtdateFrom.Enabled = false;
                txtdateUp.Enabled = false;
                if (cboMes.SelectedValue.ToString() == "12")
                {
                    #region Si es mes diciembre
                    fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString() + "/" + txtperiod.Value.ToString()));// 
                    fecha2 = Convert.ToDateTime("31/" + (cboMes.SelectedValue.ToString()) + "/" + txtperiod.Value.ToString());// 
                    txtdateFrom.Text = fecha1.ToShortDateString();
                    txtdateUp.Text = fecha2.ToShortDateString();
                    #endregion

                }
                else
                {
                    #region Si es mes 13 habilitar controles de fecha, caso contrario es un mes de enero a noviembre.
                    if (cboMes.SelectedValue.ToString() == "13")
                    {
                        txtdateFrom.Enabled = true;
                        txtdateUp.Enabled = true;


                    }
                    else
                    {
                        fecha2 = Convert.ToDateTime("01/" + (Convert.ToInt32(cboMes.SelectedValue) + 1) + "/" + txtperiod.Value.ToString()).AddDays(-1);// 
                        fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString()) + "/" + txtperiod.Value.ToString());// 
                        txtdateFrom.Text = fecha1.ToShortDateString();
                        txtdateUp.Text = fecha2.ToShortDateString();


                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (cboMes.SelectedValue.ToString() == "00")
                {
                    fecha2 = Convert.ToDateTime("31/12/" + txtperiod.Value.ToString());// 
                    fecha1 = Convert.ToDateTime("01/01/" + txtperiod.Value.ToString());//
                    txtdateFrom.Text = fecha1.ToShortDateString();
                    txtdateUp.Text = fecha2.ToShortDateString();

                }

            }
        }

        public void ObtenerFechasMesTelerik(
           RadDropDownList cboMes,
           RadMaskedEditBox txtdateFrom,
           RadMaskedEditBox txtdateUp,
           RadSpinEditor txtperiod)
        {
            DateTime fecha1;
            DateTime fecha2;

            if (cboMes.SelectedValue.ToString() != "00")
            {
                #region
                txtdateFrom.Enabled = false;
                txtdateUp.Enabled = false;
                if (cboMes.SelectedValue.ToString() == "12")
                {
                    #region Si es mes diciembre
                    fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString() + "/" + txtperiod.Value.ToString()));// 
                    fecha2 = Convert.ToDateTime("31/" + (cboMes.SelectedValue.ToString()) + "/" + txtperiod.Value.ToString());// 
                    txtdateFrom.Text = fecha1.ToShortDateString();
                    txtdateUp.Text = fecha2.ToShortDateString();
                    #endregion

                }
                else
                {
                    #region Si es mes 13 habilitar controles de fecha, caso contrario es un mes de enero a noviembre.
                    if (cboMes.SelectedValue.ToString() == "13")
                    {
                        txtdateFrom.Enabled = true;
                        txtdateUp.Enabled = true;


                    }
                    else
                    {
                        fecha2 = Convert.ToDateTime("01/" + (Convert.ToInt32(cboMes.SelectedValue) + 1) + "/" + txtperiod.Value.ToString()).AddDays(-1);// 
                        fecha1 = Convert.ToDateTime("01/" + (cboMes.SelectedValue.ToString()) + "/" + txtperiod.Value.ToString());// 
                        txtdateFrom.Text = fecha1.ToShortDateString();
                        txtdateUp.Text = fecha2.ToShortDateString();


                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (cboMes.SelectedValue.ToString() == "00")
                {
                    fecha2 = Convert.ToDateTime("31/12/" + txtperiod.Value.ToString());// 
                    fecha1 = Convert.ToDateTime("01/01/" + txtperiod.Value.ToString());//
                    txtdateFrom.Text = fecha1.ToShortDateString();
                    txtdateUp.Text = fecha2.ToShortDateString();

                }

            }
        }


    }
}
