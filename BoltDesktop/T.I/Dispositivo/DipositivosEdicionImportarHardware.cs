using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MyControlsDataBinding.Extensions;
using System.IO;
using System.Configuration;
using Asistencia.Negocios;
using Asistencia.Datos;
using Asistencia.Helper;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding.Busquedas;
using System.Collections;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using ComparativoHorasVisualSATNISIRA.T.I;
using Telerik.WinControls.UI;

namespace ComparativoHorasVisualSATNISIRA.T.I
{
    public partial class DipositivosEdicionImportarHardware : Form
    {
        private List<SAS_DispositivoHardwareByDeviceResult> hardwarePorDevice;
        private SAS_Dispostivo itemSelecionado;
        private List<SAS_Dispostivo> listadoProductosParecidos;
        private SAS_DispositivoHardwareController modelHardware;
        private SAS_DispostivoController modelo;
        private ProductoController productoController;
        private SAS_Dispostivo _oDispositivo;

        public DipositivosEdicionImportarHardware()
        {
            InitializeComponent();
        }

        public DipositivosEdicionImportarHardware(SAS_Dispostivo oDispositivo)
        {
            InitializeComponent();
            _oDispositivo = oDispositivo;

            txtCodigo.Text = oDispositivo.id.ToString().PadLeft(7, '0');
            modelo = new SAS_DispostivoController();
            _oDispositivo = modelo.GetDeviceById("SAS", oDispositivo.id);

            txtDescripcion.Text = _oDispositivo.descripcion.Trim();
            txtProductoCodigo.Text = _oDispositivo.idProducto != null ? _oDispositivo.idProducto.Trim() : string.Empty;

            if ((_oDispositivo.idProducto != null ? _oDispositivo.idProducto.Trim() : string.Empty) != string.Empty)
            {
                productoController = new ProductoController();
                txtProductoDescripcion.Text = productoController.GetDescriptionProductById("SAS", _oDispositivo.idProducto.Trim()).idproducto.Trim();
                bgwHilo.RunWorkerAsync();
            }

        }


        protected override void OnLoad(EventArgs e)
        {
            this.dgvDispositivoEnComun.TableElement.BeginUpdate();
            this.dgvDetalleHardware.TableElement.BeginUpdate();


            this.LoadFreightSummary();
            this.dgvDispositivoEnComun.TableElement.EndUpdate();
            this.dgvDetalleHardware.TableElement.EndUpdate();

            base.OnLoad(e);
        }

        private void LoadFreightSummary()
        {
            this.dgvDispositivoEnComun.MasterTemplate.AutoExpandGroups = true;
            this.dgvDispositivoEnComun.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDispositivoEnComun.GroupDescriptors.Clear();
            this.dgvDispositivoEnComun.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chNombres", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvDispositivoEnComun.MasterTemplate.SummaryRowsTop.Add(items1);


            this.dgvDetalleHardware.MasterTemplate.AutoExpandGroups = true;
            this.dgvDetalleHardware.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleHardware.GroupDescriptors.Clear();
            this.dgvDetalleHardware.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items2 = new GridViewSummaryRowItem();
            items2.Add(new GridViewSummaryItem("chhardware", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvDetalleHardware.MasterTemplate.SummaryRowsTop.Add(items2);


        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            listadoProductosParecidos = new List<SAS_Dispostivo>();
            modelo = new SAS_DispostivoController();
            listadoProductosParecidos = modelo.GetDeviceByIdProduct("SAS", _oDispositivo.idProducto.Trim()).ToList();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            dgvDispositivoEnComun.DataSource = listadoProductosParecidos.ToDataTable<SAS_Dispostivo>();
            dgvDispositivoEnComun.Refresh();
        }

        private void dgvDispositivoEnComun_SelectionChanged(object sender, EventArgs e)
        {
            hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>();
            dgvDetalleHardware.DataSource = hardwarePorDevice.ToDataTable<SAS_DispositivoHardwareByDeviceResult>();
            dgvDetalleHardware.Refresh();
            txtCodigoBase.Clear();
            txtCodigoBaseDescripcion.Clear();

            try
            {
                #region 
                itemSelecionado = new SAS_Dispostivo();
                if (dgvDispositivoEnComun != null && dgvDispositivoEnComun.Rows.Count > 0)
                {
                    if (dgvDispositivoEnComun.CurrentRow != null)
                    {
                        if (dgvDispositivoEnComun.CurrentRow.Cells["chid"].Value != null)
                        {
                            if (dgvDispositivoEnComun.CurrentRow.Cells["chid"].Value.ToString() != string.Empty)
                            {
                                int codigo = (dgvDispositivoEnComun.CurrentRow.Cells["chid"].Value != null ? Convert.ToInt32(dgvDispositivoEnComun.CurrentRow.Cells["chid"].Value.ToString()) : 0);
                                string nombreDelDispositivo = (dgvDispositivoEnComun.CurrentRow.Cells["chNombres"].Value != null ? Convert.ToString(dgvDispositivoEnComun.CurrentRow.Cells["chNombres"].Value.ToString()) : string.Empty);
                                if (codigo != 0)
                                {
                                    SAS_Dispostivo oDispositivo = new SAS_Dispostivo();
                                    oDispositivo.id = codigo;
                                    oDispositivo.descripcion = nombreDelDispositivo;
                                    hardwarePorDevice = new List<SAS_DispositivoHardwareByDeviceResult>();
                                    modelHardware = new SAS_DispositivoHardwareController();
                                    hardwarePorDevice = modelHardware.GetDispositivoHardwareByDevice("SAS", oDispositivo);

                                    dgvDetalleHardware.DataSource = hardwarePorDevice.ToDataTable<SAS_DispositivoHardwareByDeviceResult>();
                                    dgvDetalleHardware.Refresh();


                                    txtCodigoBase.Text = oDispositivo.id.ToString().PadLeft(7, '0');
                                    txtCodigoBaseDescripcion.Text = oDispositivo.descripcion.ToString().PadLeft(7, '0');


                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void btnCopiarADispositivo_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtCodigoBase.Text.Trim() != string.Empty )
            {
                modelo = new SAS_DispostivoController();
                modelo.CopiarDetalleDeHardwareDeUnDispositivoAOtro("SAS", this.txtCodigo.Text, this.txtCodigoBase.Text);
                MessageBox.Show("Registro realizado con éxito","Confirmación del sistema");
            }

         

        }

        private void btnCopiarATodosLosDispositivo_Click(object sender, EventArgs e)
        {
            if (this.txtCodigo.Text.Trim() != string.Empty && this.txtCodigoBase.Text.Trim() != string.Empty)
            {
                modelo = new SAS_DispostivoController();
                modelo.CopiarDetalleDeHardwareDeUnDispositivoATodos("SAS", this.txtCodigo.Text, this.txtCodigoBase.Text, this.txtProductoCodigo.Text);
                MessageBox.Show("Registro realizado con éxito", "Confirmación del sistema");
            }

        }
    }
}
