using Asistencia.Datos;
using Asistencia.Negocios;
using ComparativoHorasVisualSATNISIRA;
using ComparativoHorasVisualSATNISIRA.Administracion_del_sistema;
using ComparativoHorasVisualSATNISIRA.Almacen;
using ComparativoHorasVisualSATNISIRA.Calidad;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.BPM._010;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.BPM._013;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.BPM._014;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._023;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Frio_y_Despacho._045;
using ComparativoHorasVisualSATNISIRA.Calidad.CalidadPackingPostCosecha.Maestros;
using ComparativoHorasVisualSATNISIRA.Calidad.ReportesCalidadPostCosecha;
using ComparativoHorasVisualSATNISIRA.Cosecha;
using ComparativoHorasVisualSATNISIRA.Costos;
using ComparativoHorasVisualSATNISIRA.Evaluaciones_agricolas;
using ComparativoHorasVisualSATNISIRA.Exportaciones;
using ComparativoHorasVisualSATNISIRA.Maquinaria;
using ComparativoHorasVisualSATNISIRA.MRP;
using ComparativoHorasVisualSATNISIRA.Planeamiento_Agricola;
using ComparativoHorasVisualSATNISIRA.Presupuestos;
using ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga;
using ComparativoHorasVisualSATNISIRA.T.I;
using ComparativoHorasVisualSATNISIRA.T.I.Correos_electronicos;
using ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Asistencia
{
    public partial class Menu : Form
    {
        private int childFormNumber = 0;
        private SAS_USUARIOS _user2;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private string _conection;
        private UsersController modelPrivileges;
        private List<PrivilegesByUser> privilegesByUser;
        private string _descripcionConexion;
        private object privilege;

        public Menu()
        {
            InitializeComponent();
            Login ofrm = new Login();
            if (ofrm.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Bienvenido al Sistema", "Mensaje de Bienvenida");                
                _user2 = ofrm.user;
                _user = new ASJ_USUARIOS();
                _user.IdUsuario = _user2.IdUsuario;
                _user.AREA = _user2.AREA;
                _user.EmpresaID = _user2.EmpresaID;
                _user.Password = _user2.Password;
                _user.NombreCompleto = _user2.NombreCompleto;
                _companyId = ofrm.companyId != null ? ofrm.companyId.Trim() : string.Empty;
                _conection = ofrm.conection != null ? ofrm.conection.Trim() : string.Empty;
                _descripcionConexion = ofrm.descripcionConexion != null ? ofrm.descripcionConexion.Trim() : string.Empty;
                bgwHilo.RunWorkerAsync();
            }
            else
            {
                this.Dispose();
                this.Close();
            }
            //ActivarModulo(string.Empty, this);
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void ActivarModulo(string nombreModulo, Control control)
        {
            foreach (var opcion in control.Controls)
            {
                if (opcion is System.Windows.Forms.MenuStrip)
                {
                    foreach (ToolStripMenuItem mnuitOpcion in menuStrip.Items)
                    {
                        // si esta opción despliega un submenú
                        // llamar a un método para hacer cambios
                        // en las opciones del submenú
                        if (mnuitOpcion.DropDownItems.Count > 0)
                        {
                            CambiarOpcionesMenu(mnuitOpcion.DropDownItems, nombreModulo);
                        }
                    }
                }
            }

            //CajaBancos.Enabled = true;
            //CajaBancos.Visible = true;

            GoUtilitariosMosaicoVertical.Visible = true;
            GoUtilitariosCascada.Visible = true;
            GoUtilitariosCerrarTodo.Visible = true;
            GoUtilitariosMosaicoHorizontal.Visible = true;
            GoUtilitariosOrganizarTodo.Visible = true;
            GoSistemaUtilitariosIniciarSesion.Visible = true;
            GoUtilitarioNuevaVentana.Visible = true;

            GoUtilitariosMosaicoVertical.Enabled = true;
            GoUtilitariosCascada.Enabled = true;
            GoUtilitariosCerrarTodo.Enabled = true;
            GoUtilitariosMosaicoHorizontal.Enabled = true;
            GoUtilitariosOrganizarTodo.Enabled = true;
            GoSistemaUtilitariosIniciarSesion.Enabled = true;
            GoUtilitarioNuevaVentana.Enabled = true;


            GoSeparador02.Visible = true;
            GoSeparador01.Visible = true;
            GoSeparador03.Visible = true;
            GoSeparador04.Visible = true;
            GoSeparador05.Visible = true;
            GoSeparador06.Visible = true;
            GoSeparador07.Visible = true;

            GoTI.Enabled = true;
            GoTI.Visible = true;

            GoHidraulicaYFertilizacion.Enabled = true;
            GoHidraulicaYFertilizacion.Visible = true;

            GoSistema.Enabled = true;
            GoSistema.Visible = true;

            GoTransporte.Visible = true;
            GoTransporte.Enabled = true;

            GoAcopio.Enabled = true;
            GoAcopio.Visible = true;

            GoProduccion.Enabled = true;
            GoProduccion.Visible = true;

            GoPlanilla.Enabled = true;
            GoPlanilla.Visible = true;

            GoExportaciones.Enabled = true;
            GoExportaciones.Visible = true;

            GoMantenimiento.Enabled = true;
            GoMantenimiento.Visible = true;

            GoMaquinaria.Enabled = true;
            GoMaquinaria.Visible = true;

            GoSanidad.Enabled = true;
            GoSanidad.Visible = true;

            GoEvaluacionAgricola.Enabled = true;
            GoEvaluacionAgricola.Visible = true;

            GoSalir.Enabled = true;
            GoSalir.Visible = true;

            GoVentaComercial.Enabled = true;
            GoVentaComercial.Visible = true;


            GoAlmacen.Enabled = true;
            GoAlmacen.Visible = true;

            GoLogistica.Enabled = true;
            GoLogistica.Visible = true;

            GoContabilidad.Enabled = true;
            GoContabilidad.Visible = true;

            GoSST.Enabled = true;
            GoSST.Visible = true;


            GoAsegCalCer.Enabled = true;
            GoAsegCalCer.Visible = true;

            GoPRESUPUESTOS.Enabled = true;
            GoPRESUPUESTOS.Visible = true;

            GoPlaneamientoAgricola.Enabled = true;
            GoPlaneamientoAgricola.Visible = true;


        }

        public void CambiarOpcionesMenu(ToolStripItemCollection colOpcionesMenu, string nombreModulo)
        {
            if (this.bgwHilo.IsBusy != true)
            {
                // recorrer el submenú
                foreach (ToolStripItem itmOpcion in colOpcionesMenu)
                {
                    // restaurar el tipo de letra original
                    // si es una opción de menú normal...
                    if (itmOpcion.GetType() == typeof(ToolStripMenuItem))
                    {
                        // OJO que hay que colocar el texto que contiene el elemento ej. Imprimir
                        if (itmOpcion.Name.ToUpper().Contains("GoSST".ToUpper()) && nombreModulo.ToUpper() == "GoSST".ToUpper())
                        {
                            #region GoSST() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoAsegCalCer".ToUpper()) && nombreModulo.ToUpper() == "GoAsegCalCer".ToUpper())
                        {
                            #region GoContabilidad() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoContabilidad".ToUpper()) && nombreModulo.ToUpper() == "GoContabilidad".ToUpper())
                        {
                            #region GoContabilidad() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoLogistica".ToUpper()) && nombreModulo.ToUpper() == "GoLogistica".ToUpper())
                        {
                            #region GoLogistica() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoAlmacen".ToUpper()) && nombreModulo.ToUpper() == "GoAlmacen".ToUpper())
                        {
                            #region GoLogistica() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoVentaComercial".ToUpper()) && nombreModulo.ToUpper() == "GoVentaComercial".ToUpper())
                        {
                            #region GoVentaComercial() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }


                        if (itmOpcion.Name.ToUpper().Contains("GoPlanilla".ToUpper()) && nombreModulo.ToUpper() == "GoPlanilla".ToUpper())
                        {
                            #region Planillas() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        if (itmOpcion.Name.ToUpper().Contains("GoAcopio".ToUpper()) && nombreModulo.ToUpper() == "GoAcopio".ToUpper())
                        {
                            #region Acopio() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        if (itmOpcion.Name.ToUpper().Contains("GoProduccion".ToUpper()) && nombreModulo.ToUpper() == "GoProduccion".ToUpper())
                        {
                            #region Produccion() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoTransportes".ToUpper()) && nombreModulo.ToUpper() == "GoTransportes".ToUpper())
                        {
                            #region Transportes()                             
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoSistema".ToUpper()) && nombreModulo.ToUpper() == "GoSistema".ToUpper())
                        {
                            #region Administrador del sistemas() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoPRESUPUESTOS".ToUpper()) && nombreModulo.ToUpper() == "GoPRESUPUESTOS".ToUpper())
                        {
                            #region TI()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoTI".ToUpper()) && nombreModulo.ToUpper() == "GoTI".ToUpper())
                        {
                            #region TI()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        else if (itmOpcion.Name.ToUpper().Contains("GoPlaneamientoAgricola".ToUpper()) && nombreModulo.ToUpper() == "GoPlaneamientoAgricola".ToUpper())
                        {
                            #region GoPlaneamientoAgricola()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }



                        else if (itmOpcion.Name.ToUpper().Contains("GoIT".ToUpper()) && nombreModulo.ToUpper() == "GoIT".ToUpper())
                        {
                            #region TI()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                        }
                        // si esta opción a su vez despliega un nuevo submenú
                        // llamar recursivamente a este método para cambiar sus opciones
                        if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                        {
                            this.CambiarOpcionesMenu(((ToolStripMenuItem)itmOpcion).DropDownItems, nombreModulo);
                        }

                    }
                }
                GoUtilitariosMosaicoVertical.Enabled = true;
                GoUtilitariosCascada.Enabled = true;
                GoUtilitariosCerrarTodo.Enabled = true;
                GoUtilitariosMosaicoHorizontal.Enabled = true;
                GoUtilitariosOrganizarTodo.Enabled = true;
                GoSistemaUtilitariosIniciarSesion.Enabled = true;
                GoUtilitarioNuevaVentana.Enabled = true;

                GoUtilitariosMosaicoVertical.Visible = true;
                GoUtilitariosCascada.Visible = true;
                GoUtilitariosCerrarTodo.Visible = true;
                GoUtilitariosMosaicoHorizontal.Visible = true;
                GoUtilitariosOrganizarTodo.Visible = true;
                GoSistemaUtilitariosIniciarSesion.Visible = true;
                GoUtilitarioNuevaVentana.Visible = true;

            }
        }

        public void CambiarOpcionesMenu(ToolStripItemCollection colOpcionesMenu, string nombreModulo, List<PrivilegesByUser> privilegesByUserByModule)
        {
            if (this.bgwHilo.IsBusy != true)
            {
                // recorrer el submenú
                foreach (ToolStripItem itmOpcion in colOpcionesMenu)
                {
                    // restaurar el tipo de letra original
                    // si es una opción de menú normal...
                    itmOpcion.Enabled = false;
                    itmOpcion.Visible = false;

                    if (itmOpcion.GetType() == typeof(ToolStripMenuItem))
                    {
                        #region 
                        // OJO que hay que colocar el texto que contiene el elemento ej. Imprimir
                        if (itmOpcion.Name.ToUpper().Contains("GoSST".ToUpper()) && nombreModulo.ToUpper() == "GoSST".ToUpper())
                        {
                            #region GoSST() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoCCostos".ToUpper()) && nombreModulo.ToUpper() == "GoCCostos".ToUpper())
                        {
                            #region GoCCostos() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoAsegCalCer".ToUpper()) && nombreModulo.ToUpper() == "GoAsegCalCer".ToUpper())
                        {
                            #region GoAsegCalCer() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoContabilidad".ToUpper()) && nombreModulo.ToUpper() == "GoContabilidad".ToUpper())
                        {
                            #region GoContabilidad() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoLogistica".ToUpper()) && nombreModulo.ToUpper() == "GoLogistica".ToUpper())
                        {
                            #region GoLogistica() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoAlmacen".ToUpper()) && nombreModulo.ToUpper() == "GoAlmacen".ToUpper())
                        {
                            #region GoLogistica() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoVentaComercial".ToUpper()) && nombreModulo.ToUpper() == "GoVentaComercial".ToUpper())
                        {
                            #region GoVentaComercial() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoMaquinaria".ToUpper()) && nombreModulo.ToUpper() == "GoMaquinaria".ToUpper())
                        {
                            #region GoMaquinaria() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoMantenimiento".ToUpper()) && nombreModulo.ToUpper() == "GoMantenimiento".ToUpper())
                        {
                            #region GoMantenimiento() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoPlanilla".ToUpper()) && nombreModulo.ToUpper() == "GoPlanilla".ToUpper())
                        {
                            #region Planillas() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        else if (itmOpcion.Name.ToUpper().Contains("GoPlaneamientoAgricola".ToUpper()) && nombreModulo.ToUpper() == "GoPlaneamientoAgricola".ToUpper())
                        {
                            #region GoPlaneamientoAgricola()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoHidraulicaYFertilizacion".ToUpper()) && nombreModulo.ToUpper() == "GoHidraulicaYFertilizacion".ToUpper())
                        {
                            #region GoHidraulicaYFertilizacion() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }


                        if (itmOpcion.Name.ToUpper().Contains("GoEvaluacionAgricola".ToUpper()) && nombreModulo.ToUpper() == "GoEvaluacionAgricola".ToUpper())
                        {
                            #region GoEvaluacionAgricola() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoSanidad".ToUpper()) && nombreModulo.ToUpper() == "GoSanidad".ToUpper())
                        {
                            #region GoSanidad() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoExportaciones".ToUpper()) && nombreModulo.ToUpper() == "GoExportaciones".ToUpper())
                        {
                            #region GoExportaciones() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        if (itmOpcion.Name.ToUpper().Contains("GoAcopio".ToUpper()) && nombreModulo.ToUpper() == "GoAcopio".ToUpper())
                        {
                            #region Acopio() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        if (itmOpcion.Name.ToUpper().Contains("GoProduccion".ToUpper()) && nombreModulo.ToUpper() == "GoProduccion".ToUpper())
                        {
                            #region Produccion() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoTransportes".ToUpper()) && nombreModulo.ToUpper() == "GoTransportes".ToUpper())
                        {
                            #region Transportes()                             
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoSistema".ToUpper()) && nombreModulo.ToUpper() == "GoSistema".ToUpper())
                        {
                            #region Administrador del sistemas() 
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }

                        else if (itmOpcion.Name.ToUpper().Contains("GoTI".ToUpper()) && nombreModulo.ToUpper() == "GoTI".ToUpper())
                        {
                            #region TI()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoIT".ToUpper()) && nombreModulo.ToUpper() == "GoIT".ToUpper())
                        {
                            #region TI()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else if (itmOpcion.Name.ToUpper().Contains("GoPRESUPUESTOS".ToUpper()) && nombreModulo.ToUpper() == "GoPRESUPUESTOS".ToUpper())
                        {
                            #region PRESUPUESTOS()
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region
                            /*
                            string nameForm = (itmOpcion.Name.ToUpper());
                            // Verifico sólo que tenga acceso al formulario para activar o desactivar su vista.
                            if (ValidateAccessByUserAndFormDescription(nameForm) == true)
                            {
                                //Aqui lo deshabilitamos
                                ((ToolStripMenuItem)itmOpcion).Enabled = !true;
                                ((ToolStripMenuItem)itmOpcion).Visible = true;
                            }
                            else
                            {
                                //Puede ser padre --> si es padre lo valido y lo dejo pasar, caso contrario no le doy acceso
                                if (ValidateAccessByUserAndFormDescriptionIsParent(nameForm) == true)
                                {
                                    //Aqui lo deshabilitamos
                                    ((ToolStripMenuItem)itmOpcion).Enabled = !true;
                                    ((ToolStripMenuItem)itmOpcion).Visible = true;
                                }
                            }
                            */
                            #endregion
                        }
                        // si esta opción a su vez despliega un nuevo submenú
                        // llamar recursivamente a este método para cambiar sus opciones
                        if (((ToolStripMenuItem)itmOpcion).DropDownItems.Count > 0)
                        {
                            this.CambiarOpcionesMenu(((ToolStripMenuItem)itmOpcion).DropDownItems, nombreModulo, privilegesByUserByModule);
                        }
                        #endregion  
                    }
                }

                GoUtilitariosMosaicoVertical.Enabled = true;
                GoUtilitariosCascada.Enabled = true;
                GoUtilitariosCerrarTodo.Enabled = true;
                GoUtilitariosMosaicoHorizontal.Enabled = true;
                GoUtilitariosOrganizarTodo.Enabled = true;
                GoSistemaUtilitariosIniciarSesion.Enabled = true;
                GoUtilitarioNuevaVentana.Enabled = true;

                GoUtilitariosMosaicoVertical.Visible = true;
                GoUtilitariosCascada.Visible = true;
                GoUtilitariosCerrarTodo.Visible = true;
                GoUtilitariosMosaicoHorizontal.Visible = true;
                GoUtilitariosOrganizarTodo.Visible = true;
                GoSistemaUtilitariosIniciarSesion.Visible = true;
                GoUtilitarioNuevaVentana.Visible = true;
            }
        }

        private bool ValidateAccessByUserAndFormDescriptionIsParent(string nameForm)
        {
            bool state = false;
            try
            {
                var result = privilegesByUser.Where(x =>
                x.nombreEnElSistema.Trim().ToUpper() == nameForm.Trim().ToUpper()
                ).ToList();


                if (result != null && result.ToList().Count == 1)
                {
                    string jerarquia = result.Single().jerarquia != null ? result.Single().jerarquia.Trim() : string.Empty;
                    var resultByJerarquia = privilegesByUser.Where(x => x.barraPadre.Trim().ToUpper() == jerarquia.Trim().ToUpper()).ToList();
                    if (resultByJerarquia != null && resultByJerarquia.ToList().Count > 1)
                    {
                        state = true;
                    }

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return state;
            }

            return state;
        }

        private bool ValidateAccessByUserAndFormDescription(string nameForm)
        {
            bool state = false;
            try
            {
                var result = privilegesByUser.Where(x =>
                x.usuarioCodigo.Trim().ToUpper() == _user.IdUsuario.Trim().ToUpper() &&
                x.nombreEnElSistema.Trim().ToUpper() == nameForm.Trim().ToUpper()
                ).ToList();


                if (result != null && result.ToList().Count == 1)
                {
                    if (result.Single().consultar == 1)
                    {
                        state = true;
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return state;
            }

            return state;
        }

        private bool ValidateAccessByUserAndFormDescriptionIsParent(string nameForm, List<PrivilegesByUser> privilegesByUserByModule)
        {
            bool state = false;
            try
            {
                var result = privilegesByUserByModule.Where(x =>
                x.nombreEnElSistema.Trim().ToUpper() == nameForm.Trim().ToUpper()
                ).ToList();


                if (result != null && result.ToList().Count == 1)
                {
                    string jerarquia = result.Single().jerarquia != null ? result.Single().jerarquia.Trim() : string.Empty;
                    var resultByJerarquia = privilegesByUser.Where(x => x.barraPadre.Trim() == jerarquia.Trim()).ToList();
                    if (resultByJerarquia != null && resultByJerarquia.ToList().Count > 1)
                    {
                        state = true;
                    }

                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return state;
            }

            return state;
        }

        private bool ValidateAccessByUserAndFormDescription(string nameForm, List<PrivilegesByUser> privilegesByUserByModule)
        {
            bool state = false;
            try
            {
                var result = privilegesByUserByModule.Where(x =>
                x.usuarioCodigo.Trim().ToUpper() == _user.IdUsuario.Trim().ToUpper() &&
                x.nombreEnElSistema.Trim().ToUpper() == nameForm.Trim().ToUpper()
                ).ToList();


                if (result != null && result.ToList().Count == 1)
                {
                    if (result.Single().consultar == 1)
                    {
                        state = true;
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "ADVERTENCIA DEL SISTEMA");
                return state;
            }

            return state;
        }

        //, List<PrivilegesByUser> privilegesByUserByModule
        private void transportistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string form2 = GoTransportesCatalogoTransportista.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            CatalogoEmpresaDeServicioDeTransporteDePersonal frmHijo = new CatalogoEmpresaDeServicioDeTransporteDePersonal(_conection, _user, _companyId, privilege);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoTransportesCatalogoRutas frmHijo = new GoTransportesCatalogoRutas(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHReporteDeAsistenciaMóvilBuses_Click(object sender, EventArgs e)
        {
            GoTransportesReporteAsistenciaBuses frmHijo = new GoTransportesReporteAsistenciaBuses(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHparaderos_Click(object sender, EventArgs e)
        {

        }

        private void RRHHpersonalPorParadero_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonaPorParadero frmHijo = new GoPlanillasCatalogoPersonaPorParadero(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void tipoDeBloqueoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoTiposDeBloqueo frmHijo = new GoPlanillasCatalogoTiposDeBloqueo(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHpersonalBloqueado_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonalBloqueado frmHijo = new GoPlanillasCatalogoPersonalBloqueado(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHRegistroAsistencia_Click(object sender, EventArgs e)
        {
            GoTransportesMovimientoAsistenciaBuses frmHijo = new GoTransportesMovimientoAsistenciaBuses(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHreporteDeAsistenciaObservados_Click(object sender, EventArgs e)
        {
            GoPlanillasReporteAsistenciasObservadas frmHijo = new GoPlanillasReporteAsistenciasObservadas(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHreporteDeAsistenciaEnPuertas_Click(object sender, EventArgs e)
        {
            GoPlanillasReporteAsistenciaPorPuerta frmHijo = new GoPlanillasReporteAsistenciaPorPuerta(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHReporteDeVencimientodEDocumentos_Click(object sender, EventArgs e)
        {
            GoTransportesReporteVencimientoDocumentos frmHijo = new GoTransportesReporteVencimientoDocumentos(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHmenu_Click(object sender, EventArgs e)
        {
            GoSistemaCatalogoModulos frmHijo = new GoSistemaCatalogoModulos(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void RRHHformularioDeSistema_Click(object sender, EventArgs e)
        {
            GoSistemaCatalogoFormularios frmHijo = new GoSistemaCatalogoFormularios(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;

        }

        private void GoPlanilla_Click(object sender, EventArgs e)
        {

            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoPlanilla".ToUpper())).ToList();
            ActivarModulo("GoPlanilla", this, privilegesByUserByModule);
        }

        private void GoTransporte_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoTransportes".ToUpper())).ToList();
            ActivarModulo("GoTransportes", this, privilegesByUserByModule);
        }

        private void GoExportaciones_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoExportaciones".ToUpper())).ToList();
            ActivarModulo("GoExportaciones", this, privilegesByUserByModule);
        }

        private void GoMantenimiento_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoMantenimiento".ToUpper())).ToList();
            ActivarModulo("GoMantenimiento", this, privilegesByUserByModule);
        }

        private void GoMaquinaria_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoMaquinaria".ToUpper())).ToList();
            ActivarModulo("GoMaquinaria", this, privilegesByUserByModule);
        }

        private void GoEvaluacionAgricola_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoEvaluacionAgricola".ToUpper())).ToList();
            ActivarModulo("GoEvaluacionAgricola", this, privilegesByUserByModule);
        }

        private void GoSanidad_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoSanidad".ToUpper())).ToList();
            ActivarModulo("GoSanidad", this, privilegesByUserByModule);
        }

        private void GoSistema_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoSistema".ToUpper())).ToList();
            ActivarModulo("GoSistema", this, privilegesByUserByModule);
        }

        private void ActivarModulo(string moduleName, Menu menu, List<PrivilegesByUser> privilegesByUserByModule)
        {
            foreach (var opcion in menu.Controls)
            {
                if (opcion is System.Windows.Forms.MenuStrip)
                {
                    foreach (ToolStripMenuItem mnuitOpcion in menuStrip.Items)
                    {
                        // si esta opción despliega un submenú
                        // llamar a un método para hacer cambios
                        // en las opciones del submenú
                        if (mnuitOpcion.DropDownItems.Count > 0)
                        {
                            CambiarOpcionesMenu(mnuitOpcion.DropDownItems, moduleName, privilegesByUserByModule);
                        }
                    }
                }
            }


            GoSeparador02.Visible = true;
            GoSeparador01.Visible = true;
            GoSeparador03.Visible = true;
            GoSeparador04.Visible = true;
            GoSeparador05.Visible = true;
            GoSeparador06.Visible = true;
            GoSeparador08.Visible = true;
            GoSeparador07.Visible = true;
            GoSeparador08.Visible = true;
            GoSeparador09.Visible = true;
            GoSeparador10.Visible = true;

            GoAsegCalCer.Enabled = true;
            GoAsegCalCer.Visible = true;


            GoCCostos.Enabled = true;
            GoCCostos.Visible = true;

            GoTI.Enabled = true;
            GoTI.Visible = true;

            GoHidraulicaYFertilizacion.Enabled = true;
            GoHidraulicaYFertilizacion.Visible = true;

            GoSistema.Enabled = true;
            GoSistema.Visible = true;

            GoTransporte.Visible = true;
            GoTransporte.Enabled = true;

            GoAcopio.Enabled = true;
            GoAcopio.Visible = true;

            GoProduccion.Enabled = true;
            GoProduccion.Visible = true;

            GoPlanilla.Enabled = true;
            GoPlanilla.Visible = true;

            GoExportaciones.Enabled = true;
            GoExportaciones.Visible = true;

            GoMantenimiento.Enabled = true;
            GoMantenimiento.Visible = true;

            GoMaquinaria.Enabled = true;
            GoMaquinaria.Visible = true;

            GoSanidad.Enabled = true;
            GoSanidad.Visible = true;

            GoEvaluacionAgricola.Enabled = true;
            GoEvaluacionAgricola.Visible = true;

            GoSalir.Enabled = true;
            GoSalir.Visible = true;

            GoVentaComercial.Enabled = true;
            GoVentaComercial.Visible = true;


            GoAlmacen.Enabled = true;
            GoAlmacen.Visible = true;

            GoLogistica.Enabled = true;
            GoLogistica.Visible = true;

            GoContabilidad.Enabled = true;
            GoContabilidad.Visible = true;

            GoSST.Enabled = true;
            GoSST.Visible = true;

            GoPlaneamientoAgricola.Enabled =  true;
            GoPlaneamientoAgricola.Visible = true;

            GoPRESUPUESTOS.Enabled = true;
            GoPRESUPUESTOS.Visible = true;

        }

        private void GoSalir_Click(object sender, EventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.Close();
            }
        }

        private void GoSistemaCatalogoPrivilegios_Click(object sender, EventArgs e)
        {

            string form2 = GoSistemaCatalogoUsers.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                GoSistemaCatalogoUsers frmHijo = new GoSistemaCatalogoUsers(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            GetPrivilesByUser();
        }

        private void GetPrivilesByUser()
        {
            try
            {
                modelPrivileges = new UsersController();
                privilegesByUser = new List<PrivilegesByUser>();
                privilegesByUser = modelPrivileges.ObtenerListadoPrivilegiosPorUsuario(_conection, _user2.IdUsuario.Trim(), _companyId.Trim());
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                lblUsuarioNombre.Text = _user2.IdUsuario != null ? _user2.IdUsuario : string.Empty;
                lblNombreDescripcion.Text = _user2.NombreCompleto != null ? _user2.NombreCompleto : string.Empty;
                lblConexionDescripcion.Text = _descripcionConexion != null ? _descripcionConexion.Trim() : string.Empty;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;

            }

        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStrip.Visible == true)
            {
                statusStrip.Visible = false;
            }
            else
            {
                statusStrip.Visible = true;
            }


        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void GoTransportesReporteIngresoSalidaBuses_Click(object sender, EventArgs e)
        {
            GoTransportesReporteIngresoBuses frmHijo = new GoTransportesReporteIngresoBuses(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void personalObservadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonalBloqueado frmHijo = new GoPlanillasCatalogoPersonalBloqueado(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoPlanillasCatalogoPersonalPorParadero_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonaPorParadero frmHijo = new GoPlanillasCatalogoPersonaPorParadero(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoSistemaUtilitariosIniciarSesion_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (Form childForm in MdiChildren)
            {
                if (childForm != this)
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                MessageBox.Show("Debe cerrar las ventanas abiertas", "MENSAJE DEL SISTEMA");
                return;
            }
            else
            {
                Login ofrm = new Login();
                if (ofrm.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show("Bienvenido al Sistema", "Mensaje de Bienvenida");                
                    _user2 = ofrm.user;
                    _companyId = ofrm.companyId != null ? ofrm.companyId.Trim() : string.Empty;
                    _conection = ofrm.conection != null ? ofrm.conection.Trim() : string.Empty;
                    _descripcionConexion = ofrm.descripcionConexion != null ? ofrm.descripcionConexion.Trim() : string.Empty;
                    bgwHilo.RunWorkerAsync();
                }
                else
                {
                    //this.Dispose();
                    //this.Close();
                    statusStrip.Visible = true;
                }
            }
        }


        private void cerrarTodasLasVentanasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                if (childForm != this)
                {
                    childForm.Close();
                }
            }


        }

        private void GoTIMaestroDeequipos_Click(object sender, EventArgs e)
        {

            try
            {
                string form2 = GoTIMaestroDeequipos.Name.ToString().Trim().ToUpper();

                var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2.ToUpper()).ToList();
                PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
                if (result != null && result.ToList().Count > 0)
                {
                    privilege = result.FirstOrDefault();
                }
                if (privilege.consultar == 1)
                {
                    DispositivosListado frmHijo = new DispositivosListado(_conection, _user2, _companyId, privilege);
                    frmHijo.MdiParent = this;
                    frmHijo.Show();
                    frmHijo.WindowState = FormWindowState.Maximized;
                    frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                    statusStrip.Visible = false;
                }
                else
                {
                    MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                    return;
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }

        }

        private void GoTI_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoTI".ToUpper())).ToList();
            ActivarModulo("GoTI", this, privilegesByUserByModule);
        }

        private void GoTIMaestroNumeroDeIP_Click(object sender, EventArgs e)
        {

            string form2 = GoTIMaestroNumeroDeIP.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                NumeroIP frmHijo = new NumeroIP(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroSegmentos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroSegmentos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                SegmentoDeRed frmHijo = new SegmentoDeRed(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;

            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIcuentaDeDominio_Click(object sender, EventArgs e)
        {

            string form2 = GoTIcuentaDeDominio.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {

                TipoLicenciaCorreo frmHijo = new TipoLicenciaCorreo(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;

            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIcuentaDeCorreos_Click(object sender, EventArgs e)
        {

            string form2 = GoTIcuentaDeCorreos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {

                CuentaDeCorreos frmHijo = new CuentaDeCorreos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;

            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroMarcaProductos_Click_1(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroMarcaProductos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                Marca frmHijo = new Marca(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroColores_Click_1(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroColores.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                Colores frmHijo = new Colores(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroModeloProductos_Click_1(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroModeloProductos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                Modelos frmHijo = new Modelos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIcuentaVPN_Click(object sender, EventArgs e)
        {
            string form2 = GoTIcuentaVPN.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                CuentaVPN frmHijo = new CuentaVPN(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIcuentaDeCorreos_Click_1(object sender, EventArgs e)
        {
            string form2 = GoTIcuentaDeCorreos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {

                CuentaDeCorreos frmHijo = new CuentaDeCorreos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroColaboradoresEmpresa_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroColaboradoresEmpresa.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                ColaboradoresListado frmHijo = new ColaboradoresListado(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroGerenciaDeTrabajo_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroGerenciaDeTrabajo.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                Gerencias frmHijo = new Gerencias(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroAreaDeTrabajo_Click(object sender, EventArgs e)
        {

            string form2 = GoTIMaestroAreaDeTrabajo.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                AreaDeTrabajo frmHijo = new AreaDeTrabajo(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;

            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroPersonalExterno_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroPersonalExterno.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                PersonalExterno frmHijo = new PersonalExterno(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void GoTIcuentaDeDominio_Click_1(object sender, EventArgs e)
        {
            string form2 = GoTIcuentaDeDominio.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                CuentaDeDominio frmHijo = new CuentaDeDominio(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIlicenciaDeCorreo_Click(object sender, EventArgs e)
        {
            string form2 = GoTIlicenciaDeCorreo.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoLicenciaCorreo frmHijo = new TipoLicenciaCorreo(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIpersonalExterno_Click(object sender, EventArgs e)
        {

            string form2 = GoTIpersonalExterno.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                PersonalExterno frmHijo = new PersonalExterno(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIOperadorDeServiciosMoviles_Click(object sender, EventArgs e)
        {

            string form2 = GoTIOperadorDeServiciosMoviles.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                OperadorDeServiciosMoviles frmHijo = new OperadorDeServiciosMoviles(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTILineaCelularesCorporativas_Click(object sender, EventArgs e)
        {

            string form2 = GoTILineaCelularesCorporativas.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                LineasCelulares frmHijo = new LineasCelulares(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void GoTIPlanesDeLineaCelularesCorporativas_Click(object sender, EventArgs e)
        {

            string form2 = GoTIPlanesDeLineaCelularesCorporativas.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                PlanesDeLineasMoviles frmHijo = new PlanesDeLineasMoviles(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIReportesDeLineasTelefonicas_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesDeLineasTelefonicas.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                ReporteDeLineasCelulares frmHijo = new ReporteDeLineasCelulares(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoSolicitudAccesoYEquipamiento_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoSolicitudAccesoYEquipamiento.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                SolicitudDeEquipamientoTecnologico frmHijo = new SolicitudDeEquipamientoTecnologico(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoProgramaciónDeMantenimiento_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoProgramaciónDeMantenimiento.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                OrdenDeTrabajoIT frmHijo = new OrdenDeTrabajoIT(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void nuevaVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void mosaicoVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void mosaicoHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void cerrarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void organizarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void GoTIMovimientoProgramacionSopoteFuncional_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoProgramacionSopoteFuncional.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AtencionesSoporteFuncional frmHijo = new AtencionesSoporteFuncional(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroLicenciasSoftware_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroLicenciasSoftware.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoSoporteTecnico frmHijo = new TipoSoporteTecnico(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIMaestroClasificacionSoftware_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroClasificacionSoftware.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                ClasificacionSoftware frmHijo = new ClasificacionSoftware(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroClasificacionHardware_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroClasificacionHardware.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                ClasificacionHardware frmHijo = new ClasificacionHardware(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroTipoHardware_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroTipoHardware.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoHardware frmHijo = new TipoHardware(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroTipoSoftware_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroTipoSoftware.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoSoftware frmHijo = new TipoSoftware(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroPlanMantenimientoTipoSoporteFuncional_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroPlanMantenimientoTipoSoporteFuncional.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoSoporteFuncional frmHijo = new TipoSoporteFuncional(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroPlanMantenimientoTipoSoporteTecnico_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroPlanMantenimientoTipoSoporteTecnico.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoSoporteTecnico frmHijo = new TipoSoporteTecnico(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroPlanMantenimientoTipoDePlan_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroPlanMantenimientoTipoDePlan.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }
            if (privilege.consultar == 1)
            {
                TipoDeMantenimientos frmHijo = new TipoDeMantenimientos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTICategoriaDeCuentasDeCorreos_Click(object sender, EventArgs e)
        {

        }

        private void GoAcopio_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoAcopio".ToUpper())).ToList();
            ActivarModulo("GoAcopio", this, privilegesByUserByModule);
        }

        private void GoProduccion_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoProduccion".ToUpper())).ToList();
            ActivarModulo("GoProduccion", this, privilegesByUserByModule);
        }

        private void GoAcopioMovimientoRegistroDeTicketsParaAbastecimiento_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioMovimientoRegistroDeTicketsParaAbastecimiento.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ImpresionTicketsAbastecimientoMateriaPrima frmHijo = new ImpresionTicketsAbastecimientoMateriaPrima(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReporteDeInventantario_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReporteDeInventantario.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteDeInventantario frmHijo = new ReporteDeInventantario(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoDeMACs_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoDeMACs.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportedeListadoDeNumeroIPyMAC frmHijo = new ReportedeListadoDeNumeroIPyMAC(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoActividadesSoporteFuncional_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoActividadesSoporteFuncional.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoActividadesSoporteFuncional frmHijo = new ReportesListadoActividadesSoporteFuncional(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoActividadesSoporteTecnico_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoActividadesSoporteTecnico.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoActividadesSoporteTecnico frmHijo = new ReportesListadoActividadesSoporteTecnico(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoDeSolicitudesDeAsignacion_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoDeSolicitudesDeAsignacion.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoDeSolicitudesDeAsignacion frmHijo = new ReportesListadoDeSolicitudesDeAsignacion(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoSolicitudesRenovacionCelular_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoSolicitudesRenovacionCelular.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoSolicitudesRenovacionCelular frmHijo = new ReportesListadoSolicitudesRenovacionCelular(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoSeguimientoPedidoServicios_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoSeguimientoPedidoServicios.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoSeguimientoPedidoServicios frmHijo = new ReportesListadoSeguimientoPedidoServicios(_conection, _user2, _companyId, privilege,"");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoSeguimientoPedidoCompra_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoSeguimientoPedidoCompra.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoSeguimientoPedidoCompra frmHijo = new ReportesListadoSeguimientoPedidoCompra(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoSoftwareEnDispositivos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoSoftwareEnDispositivos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoSoftwareEnDispositivos frmHijo = new ReportesListadoSoftwareEnDispositivos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoCuentasDeUsuariosEnDispositivos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoCuentasDeUsuariosEnDispositivos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoCuentasDeUsuariosEnDispositivos frmHijo = new ReportesListadoCuentasDeUsuariosEnDispositivos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoCaracteristicasPorDispositivos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoCaracteristicasPorDispositivos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoCaracteristicasPorDispositivos frmHijo = new ReportesListadoCaracteristicasPorDispositivos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesListadoComponentesPorDispositivos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesListadoComponentesPorDispositivos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteListadoComponentesPorDispositivo frmHijo = new ReporteListadoComponentesPorDispositivo(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoSolicitudDeRenovaciónDeEquipoCelular_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoSolicitudDeRenovaciónDeEquipoCelular.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                SolicitudDeRenovaciónDeEquipoCelular frmHijo = new SolicitudDeRenovaciónDeEquipoCelular(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void GoTIMaestroRenovacionDeCelulares_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroRenovacionDeCelulares.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TipoSolicitudRenovacionLineasCelulares frmHijo = new TipoSolicitudRenovacionLineasCelulares(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMaestroEquipamientoTecnologico_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMaestroEquipamientoTecnologico.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TipoSolicitudEquipamientoTecnologico frmHijo = new TipoSolicitudEquipamientoTecnologico(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoConsolidadDeSolitudesParaCompras_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoConsolidadDeSolitudesParaCompras.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                CotizacionDeCompraDeEquiposCelularesConsolidar frmHijo = new CotizacionDeCompraDeEquiposCelularesConsolidar(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoRegistroDeCompras_Click(object sender, EventArgs e)
        {
            string form2 = GoTIMovimientoRegistroDeCompras.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                RegistroDeCompraEquiposCelulares frmHijo = new RegistroDeCompraEquiposCelulares(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIReportesDeListadoDeCorreosParaEquipamientoCelular_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesDeListadoDeCorreosParaEquipamientoCelular.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoCuentasDeUsuariosEnDispositivos frmHijo = new ReportesListadoCuentasDeUsuariosEnDispositivos(_conection, _user2, _companyId, privilege, "Celular".ToUpper());
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTransportesCatalogoParaderos_Click(object sender, EventArgs e)
        {
            GoTransportesCatalogoParaderos frmHijo = new GoTransportesCatalogoParaderos(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoPlanillasMaestrosPersonalPorParadero_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonaPorParadero frmHijo = new GoPlanillasCatalogoPersonaPorParadero(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoTransportesCatalogoRutas_Click(object sender, EventArgs e)
        {
            GoTransportesCatalogoRutas frmHijo = new GoTransportesCatalogoRutas(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoPlanillasMaestrosPersonalBloqueado_Click(object sender, EventArgs e)
        {
            GoPlanillasCatalogoPersonalBloqueado frmHijo = new GoPlanillasCatalogoPersonalBloqueado(_conection, _user, _companyId);
            frmHijo.MdiParent = this;
            frmHijo.Show();
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            statusStrip.Visible = false;
        }

        private void GoAcopioMaestroTicketReservado_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioMaestroTicketReservado.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TicketsReservados frmHijo = new TicketsReservados(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoVentaComercial_Click(object sender, EventArgs e)
        {

            try
            {
                var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoVentaComercial".ToUpper())).ToList();
                ActivarModulo("GoVentaComercial", this, privilegesByUserByModule);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoAlmacen_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoAlmacen".ToUpper())).ToList();
            ActivarModulo("GoAlmacen".ToUpper(), this, privilegesByUserByModule);
        }

        private void GoLogistica_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoLogistica".ToUpper())).ToList();
            ActivarModulo("GoLogistica", this, privilegesByUserByModule);
        }

        private void GoSST_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoSST".ToUpper())).ToList();
            ActivarModulo("GoSST", this, privilegesByUserByModule);
        }

        private void GoContabilidad_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoContabilidad".ToUpper())).ToList();
            ActivarModulo("GoContabilidad", this, privilegesByUserByModule);
        }

        private void GoHidraulicaYFertilizacion_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoHidraulicaYFertilizacion".ToUpper())).ToList();
            ActivarModulo("GoHidraulicaYFertilizacion", this, privilegesByUserByModule);
        }

        private void GoTIReportesFormatosGenericos_Click(object sender, EventArgs e)
        {
            string form2 = GoTIReportesFormatosGenericos.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteDeFormatosDeImpresion frmHijo = new ReporteDeFormatosDeImpresion(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAlmacenCatalogoProducto_Click(object sender, EventArgs e)
        {
            string form2 = GoAlmacenCatalogoProducto.Name.ToString().Trim().ToUpper();

            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                Productos frmHijo = new Productos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoLogisticaReporteSeguimientoLogisticoPedidoCompras_Click(object sender, EventArgs e)
        {
            string form2 = GoLogisticaReporteSeguimientoLogisticoPedidoCompras.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GestionDeSolicitudPedidoParaCompras frmHijo = new GestionDeSolicitudPedidoParaCompras(_conection, _user2, _companyId, privilege, "PEDIDO COMPRAS");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoLogisticaReporteSeguimientoLogisticoPedidoServicios_Click(object sender, EventArgs e)
        {
            string form2 = GoLogisticaReporteSeguimientoLogisticoPedidoServicios.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReportesListadoSeguimientoPedidoServicios frmHijo = new ReportesListadoSeguimientoPedidoServicios(_conection, _user2, _companyId, privilege, "PEDIDO SERVICIOS");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoLogisticaReporteSeguimientoLogisticoOrdenCompras_Click(object sender, EventArgs e)
        {
            string form2 = GoLogisticaReporteSeguimientoLogisticoOrdenCompras.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GestionDeSolicitudPedidoParaCompras frmHijo = new GestionDeSolicitudPedidoParaCompras(_conection, _user2, _companyId, privilege, "ORDEN COMPRAS");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoLogisticaReporteSeguimientoLogisticoOrdenServicios_Click(object sender, EventArgs e)
        {
            string form2 = GoLogisticaReporteSeguimientoLogisticoOrdenServicios.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GestionDeSolicitudPedidoParaCompras frmHijo = new GestionDeSolicitudPedidoParaCompras(_conection, _user2, _companyId, privilege, "ORDEN SERVICIOS");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoLogisticaReporteSeguimientoLogisticoConformidadServicios_Click(object sender, EventArgs e)
        {

            string form2 = GoLogisticaReporteSeguimientoLogisticoConformidadServicios.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GestionDeSolicitudPedidoParaCompras frmHijo = new GestionDeSolicitudPedidoParaCompras(_conection, _user2, _companyId, privilege, "OTRO");
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAlmacenReporteStockDeProducto_Click(object sender, EventArgs e)
        {
            string form2 = GoAlmacenReporteStockDeProducto.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteStockProductosRequerimientosPedidos frmHijo = new ReporteStockProductosRequerimientosPedidos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoMAQUINARIAReporteComparativoVSATvsNISIRA_Click(object sender, EventArgs e)
        {
            string form2 = GoMaquinariaReporteComparativoVSATvsNISIRA.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GoMaquinariaReporteUsoMaquinariaPorSemana frmHijo = new GoMaquinariaReporteUsoMaquinariaPorSemana(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoContabilidadReporteAnalisisDiferencialTipoDeCambio_Click(object sender, EventArgs e)
        {
            string form2 = GoContabilidadReporteAnalisisDiferencialTipoDeCambio.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteDiferencialTipoCambio frmHijo = new ReporteDiferencialTipoCambio(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoContabilidadReporteAnalisisRevisionDatosContables_Click(object sender, EventArgs e)
        {
            string form2 = GoContabilidadReporteAnalisisRevisionDatosContables.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                DetalleRevisionDatosContableSAS frmHijo = new DetalleRevisionDatosContableSAS(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoProduccionMovimientoReAsignarClienteAPallet_Click(object sender, EventArgs e)
        {
            string form2 = GoProduccionMovimientoReAsignarClienteAPallet.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReasignacionDeClientesEnPaletasLibres frmHijo = new ReasignacionDeClientesEnPaletasLibres(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoVentaComercialMovimientoActivarSegundaLiquidacion_Click(object sender, EventArgs e)
        {

            string form2 = GoVentaComercialMovimientoActivarSegundaLiquidacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GoVentasHabilitarSegundaLiquidación frmHijo = new GoVentasHabilitarSegundaLiquidación(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }


        }

        private void GoAcopioMaestroPesoPromedioDeJabaPorVariedad_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioMaestroPesoPromedioDeJabaPorVariedad.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                PesoPromedioJabaDeCosechaExportables frmHijo = new PesoPromedioJabaDeCosechaExportables(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAcopioReportePlanDeCosecha_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioReportePlanDeCosecha.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                PlanDeCosechaReporte frmHijo = new PlanDeCosechaReporte(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAcopioMovimientoPlanesDeCosecha_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioMovimientoPlanesDeCosecha.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                PlanDeCosecha frmHijo = new PlanDeCosecha(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAcopioMovimientoPlanesYProgramaMRP_Click(object sender, EventArgs e)
        {

        }

        private void GoMAQUINARIAMovimientoProgramacionSemanalDeMaquinaria_Click(object sender, EventArgs e)
        {
            string form2 = GoMAQUINARIAMovimientoProgramacionSemanalDeMaquinaria.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ProgramacionDeMaquinaria frmHijo = new ProgramacionDeMaquinaria(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCer_Click(object sender, EventArgs e)
        {
            try
            {
                var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoAsegCalCer".ToUpper())).ToList();
                ActivarModulo("GoAsegCalCer", this, privilegesByUserByModule);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoCalidadesDeFrutaPorVariedad_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoCalidadesDeFrutaPorVariedad.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                Calidades frmHijo = new Calidades(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAcopioReporteRegistroDeTicketsVsLecturasAAbastecimiento_Click(object sender, EventArgs e)
        {
            string form2 = GoAcopioReporteRegistroDeTicketsVsLecturasAAbastecimiento.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteLecturasAAbastecimientoALineas frmHijo = new ReporteLecturasAAbastecimientoALineas("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerMovimientoRegistroDeGasificado_Click(object sender, EventArgs e)
        {

            string form2 = GoAsegCalCerMovimientoRegistroDeGasificado.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                RegistroDeIngresoSalidaGasificado frmHijo = new RegistroDeIngresoSalidaGasificado("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoAsegCalCerReporteRegistroDeGasificadoIngresoSalida_Click(object sender, EventArgs e)
        {
            //ReporteIngresoSalidaGasificado
            string form2 = GoAsegCalCerReporteRegistroDeGasificadoIngresoSalida.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteIngresoSalidaGasificado frmHijo = new ReporteIngresoSalidaGasificado("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteRegistroDeGasificadoTicketLeidos_Click(object sender, EventArgs e)
        {
            //ReporteIngresoSalidaGasificadoLeidos
            string form2 = GoAsegCalCerReporteRegistroDeGasificadoTicketLeidos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteIngresoSalidaGasificadoLeidos frmHijo = new ReporteIngresoSalidaGasificadoLeidos("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMotivosDeExoneracion_Click(object sender, EventArgs e)
        {
            //GoAsegCalCerCatalogoMotivosDeExoneracion | MotivosDeExoneracionACamaraDeGasificados
            string form2 = GoAsegCalCerCatalogoMotivosDeExoneracion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                MotivosDeExoneracionACamaraDeGasificados frmHijo = new MotivosDeExoneracionACamaraDeGasificados("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerMovimientoRegistroDeGasificadoExonerado_Click(object sender, EventArgs e)
        {
            //GoAsegCalCerMovimientoRegistroDeGasificadoExonerado | ExonerarTicketACamaraDeGasificado
            string form2 = GoAsegCalCerMovimientoRegistroDeGasificadoExonerado.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ExonerarTicketACamaraDeGasificado frmHijo = new ExonerarTicketACamaraDeGasificado("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoSistemaCatalogoCuentasBoltWeb_Click(object sender, EventArgs e)
        {
          
        }

        private void GoExportacionesMovimientoDistribuirPaletasEnContenedor_Click(object sender, EventArgs e)
        {
            //GoAsegCalCerMovimientoRegistroDeGasificadoExonerado | ExonerarTicketACamaraDeGasificado
            string form2 = GoExportacionesMovimientoDistribuirPaletasEnContenedor.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                DistribucionDeCargaEnPackingList frmHijo = new DistribucionDeCargaEnPackingList(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoCCostosMovimientoActualizarFechaPodas_Click(object sender, EventArgs e)
        {
            string form2 = GoCCostosMovimientoActualizarFechaPodas.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ActualizarFechaPodaPorCampañaListado frmHijo = new ActualizarFechaPodaPorCampañaListado(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoCCostosMovimientoActualizarFechaAplicacionCianamida_Click(object sender, EventArgs e)
        {
            string form2 = GoCCostosMovimientoActualizarFechaAplicacionCianamida.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ActualizacionFechaAplicacionCianamida frmHijo = new ActualizacionFechaAplicacionCianamida(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoCCostos_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoCCostos".ToUpper())).ToList();
            ActivarModulo("GoCCostos".ToUpper(), this, privilegesByUserByModule);
        }

        private void GoEvaluacionAgricolaCatalogoLotes_Click(object sender, EventArgs e)
        {
            string form2 = GoEvaluacionAgricolaCatalogoLotes.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                Lotes frmHijo = new Lotes(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTIMovimientoParteDiarioDeEquipamientoTecnologico_Click(object sender, EventArgs e)
        {
            // GoTIMovimientoParteDiarioDeEquipamientoTecnologico
            string form2 = GoTIMovimientoParteDiarioDeEquipamientoTecnologico.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                PartesDiariosDeEquipamiento frmHijo = new PartesDiariosDeEquipamiento(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void GoTIProcesoGeneracionMasivaParteDiario_Click(object sender, EventArgs e)
        {
            string form2 = GoTIProcesoGeneracionMasivaParteDiario.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                PartesDiariosDeEquipamientoGeneracionMasiva frmHijo = new PartesDiariosDeEquipamientoGeneracionMasiva(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoPRESUPUESTOSCatalogoPresupuestos_Click(object sender, EventArgs e)
        {
            string form2 = GoPRESUPUESTOSCatalogoPresupuestos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AperturaCierrePresupuestos frmHijo = new AperturaCierrePresupuestos(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoPRESUPUESTOS_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoPRESUPUESTOS".ToUpper())).ToList();
            ActivarModulo("GoPRESUPUESTOS", this, privilegesByUserByModule);
        }



        private void GoSistemaCatalogoCuentasBoltWebRendimiento_Click(object sender, EventArgs e)
        {
            //GoSistemaCatalogoCuentasBoltWeb
            string form2 = GoSistemaCatalogoCuentasBoltWeb.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                UsuariosRendimientosBolt frmHijo = new UsuariosRendimientosBolt("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoSistemaCatalogoCuentasBoltWebWeb_Click(object sender, EventArgs e)
        {
            
            string form2 = GoSistemaCatalogoCuentasBoltWebWeb.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                UsuariosBoltWeb frmHijo = new UsuariosBoltWeb("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoSistemaCatalogoCuentasBoltWebDesktop_Click(object sender, EventArgs e)
        {
            string form2 = GoSistemaCatalogoCuentasBoltWebDesktop.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                GoSistemaCatalogoUsers frmHijo = new GoSistemaCatalogoUsers("NSFAJAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoAccionesCorrectivasEvaluacionesInspecciones_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoAccionesCorrectivasEvaluacionesInspecciones.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AccionesCorrectivas frmHijo = new AccionesCorrectivas("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteBPMincumplimientoYPracticasHigiene_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 008 */
            string form2 = GoAsegCalCerReporteBPMincumplimientoYPracticasHigiene.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                IncumplimientoBuenasPracticasHigieneReporte frmHijo = new IncumplimientoBuenasPracticasHigieneReporte(_conection, _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }


        private void GoAsegCalCerReporteBPMCumplimientoDiarioDeLavadoDeManos_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 010 */
            string form2 = GoAsegCalCerReporteBPMCumplimientoDiarioDeLavadoDeManos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                CumplimientoDiarioDeLavadoDeManosReporte frmHijo = new CumplimientoDiarioDeLavadoDeManosReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteBPMVerificacionDeLimpiezaYDesinfeccionPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 011 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMVerificacionDeLuminariasVidriosYplasticosDurosPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 012 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMAmonestacionesIncumplimientosDeBuenasPracticasPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 013 - AmonestacionesIncumplimientoDeBuenasPracticasPackingReporte*/
            string form2 = GoAsegCalCerReporteBPMAmonestacionesIncumplimientosDeBuenasPracticasPacking.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AmonestacionesIncumplimientoDeBuenasPracticasPackingReporte frmHijo = new AmonestacionesIncumplimientoDeBuenasPracticasPackingReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteBPMCheckListBuenasPracticasDeManufactura_Click(object sender, EventArgs e)
        {

            /* FORMULARIO 014 */
            string form2 = GoAsegCalCerReporteBPMCheckListBuenasPracticasDeManufactura.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                CheckListBuenasPractivasManufacturaReporte frmHijo = new CheckListBuenasPractivasManufacturaReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteBPMMonitoreoYdetencionDePlagasPacking_Click(object sender, EventArgs e)
        {

            /* FORMULARIO 017 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMMonitoreoDeTrampasDeRoedoresPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 018 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMVerificacionDeCalibracionDiariaDeBalanzasPacking_Click(object sender, EventArgs e)
        {

            /* FORMULARIO 032 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMCalibracionDeInstrumentosDeCalidadPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 033 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMVerificacionDeVidriosYPlasticosDurosEnFiltroPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 038 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMCumplimientoDiarioDeDesinfeccionDeHerramientasPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 043 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMVerificacionDeInocuidadRecepcionDeFruta_Click(object sender, EventArgs e)
        {

            /* FORMULARIO 089 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMValidacionDeLimpiezaParaLiberacionDeLineasDeProduccion_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 130 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMChecklistProcesoDeAplicacionDeSo2_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 146 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteBPMVerificacionDeLimpiezaEInocuidadDeMandilesEnFiltroPacking_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 151 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteFrioYDespachoVerificacionYCalibracionDeSensoresDeTunelesDeEnfriamiento_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 015 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteFrioYDespachoTemperaturaEnCamaras_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 034 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteProductoTerminadoCosechaControlDeCalidadRecepcionUva_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 005 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteProductoTerminadoControlDeCalidadEnDescarteUva_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 072 */

            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerReporteProductoTerminadoControlDeCalidadEnDescarteUvaTrozosExportables_Click(object sender, EventArgs e)
        {
            /* FORMULARIO 112 */
            MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
        }

        private void GoAsegCalCerCatalogoGrupoTipoItemEvaluacion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoGrupoTipoItemEvaluacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TipoGrupoMaestrosDespacho frmHijo = new TipoGrupoMaestrosDespacho("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoGrupoItemEvaluacion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoGrupoItemEvaluacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TipoGrupoMaestrosDespachoDetalle frmHijo = new TipoGrupoMaestrosDespachoDetalle("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoAreasInspeccionEvaluacion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoAreasInspeccionEvaluacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AreasInspeccionEvaluacion frmHijo = new AreasInspeccionEvaluacion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoFormatosInspeccionFrecuenciaEvaluacion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoFormatosInspeccionFrecuenciaEvaluacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                FrecuenciaFormatoDeEvaluacionInspeccion frmHijo = new FrecuenciaFormatoDeEvaluacionInspeccion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoFormatosInspeccionGrupoInspeccion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoFormatosInspeccionGrupoInspeccion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                FormatosDeEvaluacionInspeccion frmHijo = new FormatosDeEvaluacionInspeccion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoFormatosInspeccionListado_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoFormatosInspeccionListado.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                FormatosDeEvaluacionInspeccion frmHijo = new FormatosDeEvaluacionInspeccion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormLiberacionLineas_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormLiberacionLineas.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                LiberacionDeLineas frmHijo = new LiberacionDeLineas("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormSensores_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormSensores.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                Sensores frmHijo = new Sensores("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormItemsDescartesUva_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormItemsDescartesUva.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ItemsDescarteUva frmHijo = new ItemsDescarteUva("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormHerramientasDesinfeccion_Click(object sender, EventArgs e)
        {

            string form2 = GoAsegCalCerCatalogoMaestrosByFormHerramientasDesinfeccion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                HerramientasDesinfeccion frmHijo = new HerramientasDesinfeccion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
            
        }

        private void GoAsegCalCerCatalogoMaestrosByFormInstrumentosCalidad_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormInstrumentosCalidad.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                InstrumentosDeCalidad frmHijo = new InstrumentosDeCalidad("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoFormatosInspeccionTurnoDeEvaluacion_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoFormatosInspeccionTurnoDeEvaluacion.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TurnosDeEvaluacion frmHijo = new TurnosDeEvaluacion("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormActividadLavadoManos_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormActividadLavadoManos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ActividadLavadoDeManos frmHijo = new ActividadLavadoDeManos("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormAmbientesLuminariasVidriosPlasticos_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormAmbientesLuminariasVidriosPlasticos.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                AmbientesLuminariasVidriosPlasticos frmHijo = new AmbientesLuminariasVidriosPlasticos("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormCriteriosDeManufactura_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormCriteriosDeManufactura.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                CriteriosManufactura frmHijo = new CriteriosManufactura("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormSubProcesoSO2_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormSubProcesoSO2.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                SubProcesoSo2 frmHijo = new SubProcesoSo2("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormSO2Actividades_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormSO2Actividades.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ActividadesAplicacionSO2 frmHijo = new ActividadesAplicacionSO2("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormLimpiezaDesinfeccionItems_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormLimpiezaDesinfeccionItems.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                frmLimpiezaDesinfeccionItems frmHijo = new frmLimpiezaDesinfeccionItems("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormVerificacionVidriosCabecera_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormVerificacionVidriosCabecera.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                VerificacionVidriosTipoItem frmHijo = new VerificacionVidriosTipoItem("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormVerificacionVidriosDetalle_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormVerificacionVidriosDetalle.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                VerificacionVidriosTipoItemDetalle frmHijo = new VerificacionVidriosTipoItemDetalle("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormCriteriosInocuidad_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormCriteriosInocuidad.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                InocuidadCriterios frmHijo = new InocuidadCriterios("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteFrioYDespachoEvaluacionCarga_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerReporteFrioYDespachoEvaluacionCarga.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                TrazabilidadDeContenedorDespachosReporte frmHijo = new TrazabilidadDeContenedorDespachosReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerReporteFrioYDespachoCheckListRevisionContenedor_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerReporteFrioYDespachoCheckListRevisionContenedor.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ChecklistRevisionDelContenedorReporte frmHijo = new ChecklistRevisionDelContenedorReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormTermoregistroPorCliente_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormTermoregistroPorCliente.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ParametroTemperaturaCultivoCampana frmHijo = new ParametroTemperaturaCultivoCampana("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoAsegCalCerCatalogoMaestrosByFormParametrosDeTemperaturaPorCampaña_Click(object sender, EventArgs e)
        {
            string form2 = GoAsegCalCerCatalogoMaestrosByFormParametrosDeTemperaturaPorCampaña.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ChecklistRevisionDelContenedorReporte frmHijo = new ChecklistRevisionDelContenedorReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
               // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoProduccionReporteConformacionDeCarga_Click(object sender, EventArgs e)
        {
            //ConformacionDeCargaReporte
            string form2 = GoProduccionReporteConformacionDeCarga.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ConformacionDeCargaReporte frmHijo = new ConformacionDeCargaReporte("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoProduccionMovimientoConformidadDeCarga_Click(object sender, EventArgs e)
        {
            //ConformacionDeCarga
            string form2 = GoProduccionMovimientoConformidadDeCarga.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ConformacionDeCarga frmHijo = new ConformacionDeCarga("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoPlaneamientoAgricola_Click(object sender, EventArgs e)
        {
            var privilegesByUserByModule = privilegesByUser.Where(x => x.nombreEnElSistema.ToUpper().Trim().Contains("GoPlaneamientoAgricola".ToUpper())).ToList();
            ActivarModulo("GoPlaneamientoAgricola", this, privilegesByUserByModule);
        }

        private void programaSemanalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //ConformacionDeCarga
            string form2 = GoPlaneamientoAgricolaMovimientoProgramaSemanal.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ProgramaAgricola frmHijo = new ProgramaAgricola("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
            
        }

        private void GoAlmacenReporteSolicitudRequerimientoInternoConProgramaSemanal_Click(object sender, EventArgs e)
        {
            string form2 = GoAlmacenReporteSolicitudRequerimientoInternoConProgramaSemanal.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna frmHijo = new ReporteSeguimientoDeSolicitudesDeRequerimientoProgramaSemanalSalidaInterna("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void GoTITipoYClasificacionesHardwareYSoftwareTipoCuentaLogCorreoElectronico_Click(object sender, EventArgs e)
        {
            string form2 = GoTITipoYClasificacionesHardwareYSoftwareTipoCuentaLogCorreoElectronico.Name.ToString().Trim().ToUpper();
            var result = privilegesByUser.Where(x => x.nombreEnElSistema.Trim().ToUpper() == form2).ToList();
            PrivilegesByUser privilege = new PrivilegesByUser { anular = 0, consultar = 0, eliminar = 0, imprimir = 0, nuevo = 0, ninguno = 1, editar = 0 };
            if (result != null && result.ToList().Count > 0)
            {
                privilege = result.FirstOrDefault();
            }

            if (privilege.consultar == 1)
            {
                CuentaDeCorreosTipoLog frmHijo = new CuentaDeCorreosTipoLog("SAS", _user2, _companyId, privilege);
                frmHijo.MdiParent = this;
                frmHijo.Show();
                frmHijo.WindowState = FormWindowState.Maximized;
                frmHijo.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
                // frmHijo.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                statusStrip.Visible = false;
            }
            else
            {
                MessageBox.Show("No tiene privilegios para realizar esta acción", "MENSAJE DEL SISTEMA");
                return;
            }
        }
    }
}
