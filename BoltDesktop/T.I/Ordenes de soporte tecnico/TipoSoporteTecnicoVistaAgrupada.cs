using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asistencia.Datos;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using Asistencia.Negocios;
using MyControlsDataBinding.Extensions;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using Asistencia.Helper;
using Telerik.WinControls.UI.Localization;
using System.Reflection;
using Telerik.WinControls.Data;
using System.Collections;
using System.Configuration;

namespace ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico
{
    public partial class TipoSoporteTecnicoVistaAgrupada : Form
    {

        private PrivilegesByUser privilege;
        private string companyId;
        private string conection;
        private SAS_USUARIOS user2;
        private bool exportVisualSettings;
        private string fileName;
        private SAS_DispositivoTipoMantenimientoController modelo;
        private string result;
        private int lastItem = 1;
        private string msgError = string.Empty;
        string idTipomantenimiento = string.Empty;
        private List<SAS_DispositivosTipoMantenimientoDetalleByMinutosGroupByAplicacion> listadoAgrupado;

        public TipoSoporteTecnicoVistaAgrupada()
        {
            InitializeComponent();
        }


        public TipoSoporteTecnicoVistaAgrupada(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            idTipomantenimiento = string.Empty;
            Consultar(idTipomantenimiento);
        }

        public TipoSoporteTecnicoVistaAgrupada(string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser _privilege, string _idTipomantenimiento)
        {
            InitializeComponent();
            RadGridLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.GridLocalizationProviderEspanol();
            RadPageViewLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadPageViewLocalizationProviderEspañol();
            RadWizardLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadWizardLocalizationProviderEspañol();
            RadMessageLocalizationProvider.CurrentProvider = new Asistencia.ClaseTelerik.RadMessageBoxLocalizationProviderEspañol();
            conection = _conection;
            user2 = _user2;
            companyId = _companyId;
            privilege = _privilege;
            idTipomantenimiento = _idTipomantenimiento;
            Consultar(idTipomantenimiento);
        }


        private void Consultar(string codigoTipoMantenimiento)
        {
            dgvRegistro.Enabled = false;
            progressBar1.Visible = true;
            bgwHilo.RunWorkerAsync();

        }

        private void TipoSoporteTecnicoVistaAgrupada_Load(object sender, EventArgs e)
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new SAS_DispositivoTipoMantenimientoController();
                listadoAgrupado = new List<SAS_DispositivosTipoMantenimientoDetalleByMinutosGroupByAplicacion>();
                if (idTipomantenimiento != string.Empty)
                {
                    listadoAgrupado = modelo.DispositivosTipoMantenimientoDetalleByMinutosGroupByAplicacion(conection).Where(x => x.codigo == idTipomantenimiento).ToList();
                }
                else
                {
                    listadoAgrupado = modelo.DispositivosTipoMantenimientoDetalleByMinutosGroupByAplicacion(conection).ToList();
                }

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvRegistro.DataSource = listadoAgrupado.ToDataTable<SAS_DispositivosTipoMantenimientoDetalleByMinutosGroupByAplicacion>();
                dgvRegistro.Refresh();
                dgvRegistro.Enabled = true;
                progressBar1.Visible = !true;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }
    }
}
