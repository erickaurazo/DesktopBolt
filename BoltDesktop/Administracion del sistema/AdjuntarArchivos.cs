using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using System.Linq;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Diagnostics;

namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    public partial class AdjuntarArchivos : Form
    {
        private string codigoSelecionado;
        private string companyId;
        private string conection;
        private PrivilegesByUser privilege;
        private SAS_USUARIOS user2;
        private SAS_DocumentosAdjuntosAlFormularioController model;
        private List<SAS_DocumentosAdjuntosAlFormulario> listado;
        private List<SAS_DocumentosAdjuntosAlFormulario> listadoVista;
        private string formulario;
        private SAS_DocumentosAdjuntosAlFormulario selectedItem;
        private string direcionLogica = @"C:\SOLUTION\Documents";
        private string direcionLogicaCompleta = @"C:\SOLUTION\Documents\temp\";

        public AdjuntarArchivos()
        {
            InitializeComponent();
        }

        public AdjuntarArchivos(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, string _codigoSelecionado, string _formulario)
        {
            InitializeComponent();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            codigoSelecionado = _codigoSelecionado;
            formulario = _formulario;
            lblUsuario.Text = user2.NombreCompleto;
            lblUsuarioId.Text = user2.IdUsuario;
            this.txtCodigo.Text = "0";
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                gbEdicion.Enabled = false;
                gbListado.Enabled = false;
                progressBar1.Visible = true;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                gbEdicion.Enabled = !false;
                gbListado.Enabled = !false;
                progressBar1.Visible = !true;
                return;
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            direcionLogica = "C:\\SOLUTION\\Documents";
            openFileDialog1.InitialDirectory = direcionLogica;
            openFileDialog1.Filter = "Todos los archivos (*.*) |*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = openFileDialog1.FileName;
            }
        }

        private void bgwHilo_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            model = new SAS_DocumentosAdjuntosAlFormularioController();
            listado = new List<SAS_DocumentosAdjuntosAlFormulario>();
            listado = model.GetListByIdReference(conection, formulario, codigoSelecionado.ToString().Trim());
        }

        private void bgwHilo_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            dgvListado.DataSource = listado.ToDataTable<SAS_DocumentosAdjuntosAlFormulario>();
            dgvListado.Refresh();
            gbEdicion.Enabled = !false;
            gbListado.Enabled = !false;
            progressBar1.Visible = !true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            GrabarAdjunto();


        }

        private void GrabarAdjunto()
        {
            try
            {
                if (this.txtTitulo.Text.Trim().Length < 10)
                {
                    MessageBox.Show("Ingrese titulo del archivo con una descripcion mayor a los 10 caracteres", "Advertencia del sistema");
                }
                else
                {
                    byte[] archivo = null;
                    Stream MyStream = openFileDialog1.OpenFile();
                    MemoryStream obj = new MemoryStream();
                    MyStream.CopyTo(obj);
                    archivo = obj.ToArray();


                    SAS_DocumentosAdjuntosAlFormulario item = new SAS_DocumentosAdjuntosAlFormulario();
                    item.id = Convert.ToInt32((this.txtCodigo.Text != string.Empty ? this.txtCodigo.Text : "0"));
                    item.nombre = this.txtTitulo.Text;
                    item.ruta = archivo;
                    item.extension = openFileDialog1.SafeFileName;
                    item.formulario = formulario;
                    item.idReferencia = codigoSelecionado;
                    item.estado = 1;
                    item.visibleEnReporte = 1;
                    item.idusuario = user2.IdUsuario;
                    item.fecha = DateTime.Now;
                    model = new SAS_DocumentosAdjuntosAlFormularioController();
                    model.ToRegister(conection, item);
                    Limpiar();
                    Consultar();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void AdjuntarArchivos_Load(object sender, EventArgs e)
        {

        }

        private void Limpiar()
        {
            this.txtCodigo.Text = "0";
            this.txtRuta.Text = string.Empty;
            this.txtTitulo.Text = string.Empty;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {


            try
            {
                #region 
                selectedItem = new SAS_DocumentosAdjuntosAlFormulario();
                selectedItem.id = 0;
                if (dgvListado != null && dgvListado.Rows.Count > 0)
                {
                    if (dgvListado.CurrentRow != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvListado.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                int id = (dgvListado.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvListado.CurrentRow.Cells["chid"].Value.ToString()) : 0);

                                if (id != 0)
                                {
                                    selectedItem.id = id;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void dgvListado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                VerArchivo();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }

        }

        private void VerArchivo()
        {
            if (selectedItem != null)
            {
                #region 
                if (selectedItem.id != null)
                {
                    if (selectedItem.id > 0)
                    {
                        model = new SAS_DocumentosAdjuntosAlFormularioController();
                        listadoVista = new List<SAS_DocumentosAdjuntosAlFormulario>();
                        listadoVista = model.GetListById(conection, (selectedItem.id.ToString()));
                        if (listadoVista != null)
                        {
                            if (listadoVista.ToList().Count > 0)
                            {
                                foreach (var item in listadoVista)
                                {
                                    //string direccion = AppDomain.CurrentDomain.BaseDirectory;
                                    string direccion = direcionLogica;
                                    string ubicacioncompleta = direcionLogicaCompleta + item.extension;

                                    if (!Directory.Exists(direcionLogicaCompleta))
                                        Directory.CreateDirectory(direcionLogicaCompleta);


                                    if (File.Exists(ubicacioncompleta))
                                        File.Delete(ubicacioncompleta);


                                    File.WriteAllBytes(ubicacioncompleta, (byte[])item.ruta.ToArray());
                                    Process.Start(ubicacioncompleta);
                                }
                            }
                        }

                    }
                }
                #endregion
            }
        }

        private void btnDetalleQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarAdjunto();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void EliminarAdjunto()
        {
            if (selectedItem != null)
            {
                #region 
                if (selectedItem.id != null)
                {
                    if (selectedItem.id > 0)
                    {
                        model = new SAS_DocumentosAdjuntosAlFormularioController();
                        model.ChangeState(conection, selectedItem.id);
                        Limpiar();
                        Consultar();

                    }
                }
                #endregion
            }
        }

        private void btnDetalleCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                HabilitarEnReportes();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistems");
                return;
            }
        }

        private void HabilitarEnReportes()
        {
            if (selectedItem != null)
            {
                #region 
                if (selectedItem.id != null)
                {
                    if (selectedItem.id > 0)
                    {
                        model = new SAS_DocumentosAdjuntosAlFormularioController();
                        model.EnabledInReports(conection, selectedItem.id);
                        Limpiar();
                        Consultar();

                    }
                }
                #endregion
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
