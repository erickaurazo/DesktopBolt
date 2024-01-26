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
using ComparativoHorasVisualSATNISIRA.Almacen.ProductoImpresinCodigoBarrasDSTableAdapters;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    public partial class Productos : Form
    {
        private ProductoController modelo;
        private List<SAS_ListadoProducto> listado;
        private int incluirAnulados;

        string codigoProductoSelecionado = string.Empty;
        private ProductoImpresinCodigoBarrasDS dsReporte;
        private SAS_ProductosByIdTableAdapter adaptador;
        private DataTable dta;
        private ReportDocument oRpt;

        private PrivilegesByUser _privilege;
        private SAS_USUARIOS _user;
        private string _companyId;
        private string _conection;

        public Productos()
        {
            InitializeComponent();
            Consultar();           
        }

        public Productos(string conection, SAS_USUARIOS user, string companyId, PrivilegesByUser privilege)
        {
            _conection = conection;
            _user = user;
            _companyId = companyId;
            _privilege = privilege;
            InitializeComponent();
            Consultar();
        }


        protected override void OnLoad(EventArgs e)
        {
            this.dgvListado.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvListado.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvListado.GroupDescriptors.Clear();
            this.dgvListado.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chdescripcion", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvListado.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {

            incluirAnulados = 0;
            if (chkIncluirAnulados.Checked == true)
            {
                incluirAnulados = 1;
            }

            gbCabecera.Enabled = false;
            gbListado.Enabled = false;
            progressBar.Visible = true;
            bgwHilo.RunWorkerAsync();

        }

        private void VistaPrevia()
        {

        }

        private void Imprimir()
        {

        }

        private void Exportar()
        {

        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                modelo = new ProductoController();
                listado = new List<SAS_ListadoProducto>();
                listado = modelo.GetListAll("SAS", incluirAnulados).ToList();
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
                dgvListado.DataSource = listado.ToDataTable<SAS_ListadoProducto>();
                dgvListado.Refresh();
                dgvListado.Enabled = true;
                gbCabecera.Enabled = !false;
                gbListado.Enabled = !false;
                progressBar.Visible = !true;

            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void PrintLabels()
        {            
            if (codigoProductoSelecionado != string.Empty)
            {                
                PrintDialog impresion = new PrintDialog();
                DialogResult Result = impresion.ShowDialog();
                //if (impresion.ShowDialog() == DialogResult.OK)
                if (Result == DialogResult.OK)
                {                    
                    dsReporte = new ProductoImpresinCodigoBarrasDS();                    
                    //adaptador = new DispositivosEdicionImprimirEtiquetasQRDSTableAdapters._SAS_DispositivoImpresionDeTicketsQRTableAdapter();
                     adaptador = new ProductoImpresinCodigoBarrasDSTableAdapters.SAS_ProductosByIdTableAdapter();
                     dsReporte.EnforceConstraints = false;
                     //adaptador.Fill(dsReporte._SAS_DispositivoImpresionDeTicketsQR, this.correlativo);
                     adaptador.Fill(dsReporte.SAS_ProductosById, codigoProductoSelecionado.ToString().Trim());
                     dta = new DataTable();
                     //if (dsReporte._SAS_DispositivoImpresionDeTicketsQR.Rows.Count <= 0)
                     if (dsReporte.SAS_ProductosById.Rows.Count <= 0)
                     {
                         MessageBox.Show("No se encontró información para imprimir !");
                         return;
                     }
                     oRpt = new ReportDocument();
                    CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    oRpt.Load(@"C:\SOLUTION\ProductoImpresionCodigoBarraRPT.rpt");
                    //oRpt.Load(@"D:\Dev\SAS\PensionistasRefrigerios\Almacen\ProductoImpresionCodigoBarraRPT.rpt");
                   
                    //oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                    //oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies,printDialog.PrinterSettings.Collate,printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
                    dta = dsReporte.SAS_ProductosById;
                    oRpt.SetDataSource(dta);

                    reportDocumento = oRpt;
                    oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                    oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);

                }
            }
            else
            {
                MessageBox.Show("No se puede editar el siguiente registro", "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void Preview()
        {
            if (codigoProductoSelecionado != string.Empty)
            {

                ProductosImpresionCodigoBarCode ofrm = new ProductosImpresionCodigoBarCode(codigoProductoSelecionado);
                ofrm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se puede editar el siguiente registro", "MENSAJE DEL SISTEMA");
                return;
            }

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            codigoProductoSelecionado = string.Empty;

            if (dgvListado != null && dgvListado.Rows.Count > 0)
            {
                if (dgvListado.CurrentRow != null)
                {
                    if (dgvListado.CurrentRow.Cells["chidproducto"].Value != null)
                    {
                        if (dgvListado.CurrentRow.Cells["chidproducto"].Value.ToString() != string.Empty)
                        {
                            codigoProductoSelecionado = dgvListado.CurrentRow.Cells["chidproducto"].Value.ToString();
                        }
                    }
                }
            }

        }

        private void btnImprimirTicketQR_Click(object sender, EventArgs e)
        {
            Preview();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintLabels();
        }
    }
}
