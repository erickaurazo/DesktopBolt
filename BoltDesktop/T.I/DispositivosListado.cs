using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Configuration;
using Telerik.WinControls.UI.Localization;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using ComparativoHorasVisualSATNISIRA.T.I;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class DispositivosListado : Form
    {
        private List<SAS_ListadoDeDispositivos> listado;
        private SAS_DispositivoIPController modelo;
        private SAS_DispostivoController deviceModelo ;
        private SAS_ListadoDeDispositivos oDispositivo;
        private SAS_Dispostivo dispositivo;
        private string _conection;
        private SAS_USUARIOS _user2;
        private string _companyId;
        private PrivilegesByUser privilege;

        public DispositivosListado()
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            Consultar();
            Inicio();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = -1;
        }

        public DispositivosListado(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            Consultar();
            Inicio();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = -1;
        }

        public void Inicio()
        {
            try
            {

                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dgvDispositivo.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvDispositivo.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvDispositivo.MasterTemplate.AutoExpandGroups = true;
            this.dgvDispositivo.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDispositivo.GroupDescriptors.Clear();
            this.dgvDispositivo.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chnombres", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvDispositivo.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            modelo = new SAS_DispositivoIPController();
            listado = new List<SAS_ListadoDeDispositivos>();

            listado = modelo.ListadoDeDispositivos("SAS").ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvDispositivo.DataSource = listado.ToDataTable<SAS_ListadoDeDispositivos>();
            dgvDispositivo.Refresh();
            dgvDispositivo.Enabled = true;

        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            try
            {
                dgvDispositivo.Enabled = false;
                bgwHilo.RunWorkerAsync();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void DispositivosListado_Load(object sender, EventArgs e)
        {

        }

        private void dgvDispositivo_SelectionChanged(object sender, EventArgs e)
        {
            string imgQr = string.Empty;
            oDispositivo = new SAS_ListadoDeDispositivos();
            dispositivo = new SAS_Dispostivo();
            dispositivo.id = -1;

            oDispositivo.id = -1;
            oDispositivo.nombres = string.Empty;
            oDispositivo.dispositivo = string.Empty;

            if (dgvDispositivo != null && dgvDispositivo.Rows.Count > 0)
            {
                if (dgvDispositivo.CurrentRow != null)
                {
                    if (dgvDispositivo.CurrentRow.Cells["chid"].Value != null)
                    {
                        if (dgvDispositivo.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                        {
                            int codigo = (dgvDispositivo.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvDispositivo.CurrentRow.Cells["chid"].Value) : 0);
                            
                            var resultado = listado.Where(x => x.id == codigo).ToList();
                            if (resultado.ToList().Count == 1)
                            {
                                oDispositivo = resultado.Single();
                                dispositivo.id = codigo;
                                if (oDispositivo.imagen != null)
                                {
                                    if (oDispositivo.imagen.ToString().Length > 10)
                                    {
                                        btnImprimirTicketQR.Enabled = true;
                                    }
                                    else
                                    {
                                        btnImprimirTicketQR.Enabled = false;
                                    }
                                    
                                }
                                else
                                {
                                    btnImprimirTicketQR.Enabled = false;
                                }
                            }
                            if (resultado.ToList().Count > 1)
                            {
                                oDispositivo = resultado.ElementAt(0);
                                dispositivo.id = codigo;
                                if (oDispositivo.imagen != null)
                                {
                                    if (oDispositivo.imagen.ToString().Length > 10)
                                    {
                                        btnImprimirTicketQR.Enabled = true;
                                    }
                                    else
                                    {
                                        btnImprimirTicketQR.Enabled = false;
                                    }

                                }
                                else
                                {
                                    btnImprimirTicketQR.Enabled = false;
                                }

                            }
                        }
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Editar();
        }

        private void Editar()
        {
            if (oDispositivo != null)
            {
                if (oDispositivo.id >= 0)
                {
                    DispositivosEdicion oFron = new DispositivosEdicion("SAS", oDispositivo);
                    oFron.Show();
                }
            }
        }

        private void dgvDispositivo_DoubleClick(object sender, EventArgs e)
        {
            Editar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void Anular()
        {
            if (oDispositivo.id >= 0)
            {

                #region Anular ()                 
                deviceModelo = new SAS_DispostivoController();
                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                int resultadoAccion = deviceModelo.Unregister("SAS", dispositivo);
                MessageBox.Show("Se ejecutado exitosamente la acción para el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                Consultar();
                #endregion

            }
        }


        private void Eliminar()
        {
            if (oDispositivo.id >= 0)
            {

                #region Eliminar ()                 
                deviceModelo = new SAS_DispostivoController();
                SAS_Dispostivo oDevice = new SAS_Dispostivo();
                int resultadoAccion = deviceModelo.Delete("SAS", dispositivo);
                MessageBox.Show("Se ejecutado exitosamente la acción para el registro " + resultadoAccion.ToString().PadLeft(7, '0'), "Mensaje del sistema");
                Consultar();
                #endregion

            }
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void anularRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void verDocumentosYMovimientoAsociadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void imprimirTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oDispositivo != null)
            {
                if (oDispositivo.id != null)
                {
                    if (oDispositivo.id >0)
                    {
                        DispositivosEdicionImprimirEtiquetas ofrm = new DispositivosEdicionImprimirEtiquetas(Convert.ToInt32(oDispositivo.id));
                        ofrm.ShowDialog();
                    }
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
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

        private void DispositivosListado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
