using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MyDataGridViewColumns;
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
using System.Globalization;
using System.Collections;
using MyControlsDataBinding.Controles;
using MyControlsDataBinding;
using ComparativoHorasVisualSATNISIRA.Produccion;
using MyControlsDataBinding.Busquedas;
using ComparativoHorasVisualSATNISIRA.Cosecha;
using ComparativoHorasVisualSATNISIRA.Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters;
using CrystalDecisions.CrystalReports.Engine;

namespace ComparativoHorasVisualSATNISIRA
{
    public partial class RegistroTicketsAbastecimientoMateriaPrimaDetalle : Form
    {
        private ListadoAcopioByTiktesResult oRecepcion;
        private RegistroAbastecimiento oRegistroNew;
        private RegistroAbastecimiento oRegistro;
        private RegistroAbastecimientoDetalle oDetalle;
        private RegistroAbastecimientoController modelo;
        private List<RegistroAbastecimientoDetalle> listadoDetalleTicketRegistrados;
        private PrivilegesByUser privilege;
        private string _companyId;
        private string _conection;
        private SAS_USUARIOS _user2;
        private GlobalesHelper globalHelper;
        private string result;
        private string fileName = "DEFAULT";
        private bool exportVisualSettings = true;
        private MesController MesesNeg;
        private SAS_TicketReservadoController modeloTickerReservado;
        private ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS dsReporte;
        private ListadoAcopioByTiktesByCorrelativoTableAdapter adaptador;
        private DataTable dta;
        private ReportDocument oRpt;

        public RegistroTicketsAbastecimientoMateriaPrimaDetalle()
        {
            InitializeComponent();
        }

        public RegistroTicketsAbastecimientoMateriaPrimaDetalle(ListadoAcopioByTiktesResult recepcion)
        {
            InitializeComponent();
            this.oRecepcion = recepcion;
            this.txtSucursal.Text = this.oRecepcion.sucursal.Trim().ToUpper();
            this.txtAlmacen.Text = this.oRecepcion.almacen.Trim().ToUpper();
            this.txtPesoBruto.Text = this.oRecepcion.PESOBRUTO.Value.ToDecimalPresentation();
            this.txtPesoNeto.Text = this.oRecepcion.PESONETO.Value.ToDecimalPresentation();
            this.txtPesoPromedio.Text = this.oRecepcion.PESOPROMEDIO.Value.ToDecimalPresentation();
            this.txtCodigoProducto.Text = this.oRecepcion.IDPRODUCTO.Trim().ToUpper();
            this.txtDescripcionProducto.Text = this.oRecepcion.DESCRIPCION.Trim().ToUpper();
            this.txtDocumento.Text = this.oRecepcion.DOCUMENTO.Trim().ToUpper();
            this.txtFechaRegistro.Text = this.oRecepcion.FechaCreacionitem.Value.ToPresentationDateTime();
            this.txtBalanza.Text = this.oRecepcion.BALANZA;
            this.txtEstado.Text = this.oRecepcion.estado;
            this.txtUm.Text = this.oRecepcion.IDMEDIDA;
            this.txtCodigoRegistro.Text = this.oRecepcion.IDINGRESOSALIDAACOPIOCAMPO.Trim();
            this.txtItem.Text = this.oRecepcion.item;
            this.txtCantidadJabas.Text = this.oRecepcion.NROJABAS.Value.ToDecimalPresentation();
            this.txtCultivo.Text = this.oRecepcion.cultivo.Trim();
            this.txtVariedad.Text = this.oRecepcion.variedad.Trim();
            this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString().Trim();
            this.txtSector.Text = this.oRecepcion.sector.ToString().Trim();
            this.txtTipoCultivo.Text = this.oRecepcion.tipoProducto.ToString().Trim();
            this.txtCampaña.Text = this.oRecepcion.Campaña != null ? this.oRecepcion.Campaña.ToString().Trim() : string.Empty;
            this.txtMercado.Text = this.oRecepcion.mercado != null ? this.oRecepcion.mercado.ToString().Trim() : string.Empty;


            if (this.oRecepcion.correlativo == 0)
            {
                // Verificamos si tiene algun registro con anterioridad.

                modelo = new RegistroAbastecimientoController();
                this.oRecepcion.correlativo = modelo.ObtenerCorrelativoDesdeIngresoAcopioPorItem("NSFAJAS", oRecepcion);
                if (this.oRecepcion.correlativo == 0)
                {
                    this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString();
                    this.txtPuntoQuiebraTicket.Enabled = true;
                    this.btnRegistrarTicket.Enabled = true;
                    this.btnDistribuir.Enabled = true;
                    this.dgvTickets.Enabled = true;

                }
                else
                {
                    this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString().Trim();
                }
            }
            bgwHilo.RunWorkerAsync();
            gbDatosGenerales.Enabled = false;
            gbDetalle.Enabled = false;
            gbOpcionesImpresion.Enabled = false;

        }

        public RegistroTicketsAbastecimientoMateriaPrimaDetalle(ListadoAcopioByTiktesResult recepcion, string _conection, SAS_USUARIOS _user2, string _companyId, PrivilegesByUser privilege)
        {
            InitializeComponent();

            this._conection = _conection;
            this._user2 = _user2;
            this._companyId = _companyId;
            this.privilege = privilege;
            //this.codigoSelecionado = _codigoSelecionado;

            this.oRecepcion = recepcion;
            this.txtSucursal.Text = this.oRecepcion.sucursal.Trim().ToUpper();
            this.txtAlmacen.Text = this.oRecepcion.almacen.Trim().ToUpper();
            this.txtPesoBruto.Text = this.oRecepcion.PESOBRUTO.Value.ToDecimalPresentation();
            this.txtPesoNeto.Text = this.oRecepcion.PESONETO.Value.ToDecimalPresentation();
            this.txtPesoPromedio.Text = this.oRecepcion.PESOPROMEDIO.Value.ToDecimalPresentation();
            this.txtCodigoProducto.Text = this.oRecepcion.IDPRODUCTO.Trim().ToUpper();
            this.txtDescripcionProducto.Text = this.oRecepcion.DESCRIPCION.Trim().ToUpper();
            this.txtDocumento.Text = this.oRecepcion.DOCUMENTO.Trim().ToUpper();
            this.txtFechaRegistro.Text = this.oRecepcion.FechaCreacionitem.Value.ToPresentationDateTime();
            this.txtBalanza.Text = this.oRecepcion.BALANZA;
            this.txtEstado.Text = this.oRecepcion.estado;
            this.txtUm.Text = this.oRecepcion.IDMEDIDA;
            this.txtCodigoRegistro.Text = this.oRecepcion.IDINGRESOSALIDAACOPIOCAMPO.Trim();
            this.txtItem.Text = this.oRecepcion.item;
            this.txtCantidadJabas.Text = this.oRecepcion.NROJABAS.Value.ToDecimalPresentation();
            this.txtCultivo.Text = this.oRecepcion.cultivo.Trim();
            this.txtVariedad.Text = this.oRecepcion.variedad.Trim();
            this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString().Trim();
            this.txtSector.Text = this.oRecepcion.sector.ToString().Trim();
            this.txtTipoCultivo.Text = this.oRecepcion.tipoProducto.ToString().Trim();
            this.txtCampaña.Text = this.oRecepcion.Campaña != null ? this.oRecepcion.Campaña.ToString().Trim() : string.Empty;
            this.txtMercado.Text = this.oRecepcion.mercado != null ? this.oRecepcion.mercado.ToString().Trim() : string.Empty;


            if (this.oRecepcion.correlativo == 0)
            {
                // Verificamos si tiene algun registro con anterioridad.

                modelo = new RegistroAbastecimientoController();
                this.oRecepcion.correlativo = modelo.ObtenerCorrelativoDesdeIngresoAcopioPorItem("NSFAJAS", oRecepcion);
                if (this.oRecepcion.correlativo == 0)
                {
                    this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString();
                    this.txtPuntoQuiebraTicket.Enabled = true;
                    this.btnRegistrarTicket.Enabled = true;
                    this.btnDistribuir.Enabled = true;
                    btnGenerarEImprimir.Enabled = true;
                    this.dgvTickets.Enabled = true;

                }
                else
                {
                    this.txtCorrelativo.Text = this.oRecepcion.correlativo.ToString().Trim();
                }
            }
            bgwHilo.RunWorkerAsync();
            gbDatosGenerales.Enabled = false;
            gbDetalle.Enabled = false;
            gbOpcionesImpresion.Enabled = false;

        }

        public void Inicio()
        {
            try
            {
                Globales.Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
                Globales.UsuarioBaseDatos = ConfigurationManager.AppSettings["Usuario"].ToString();
                Globales.BaseDatos = ConfigurationManager.AppSettings["bdsaturno"].ToString();
                Globales.ClaveBaseDatos = ConfigurationManager.AppSettings["Clave"].ToString();
                Globales.IdEmpresa = "001";
                Globales.Empresa = "SOCIEDAD AGRICOLA SATURNO SA";
                Globales.UsuarioSistema = "EAURAZO";
                Globales.NombreUsuarioSistema = "ERICK AURAZO";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "MENSAJE DEL SISTEMA");
                return;
            }
        }

        private void ImpresionTicketsAbastecimientoMateriaPrimaDetalleImpresion_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarTicket_Click(object sender, EventArgs e)
        {

            #region Registrar()
            RegistrarTickets();
            #endregion
            MessageBox.Show("Registrado correctamente", "Confirmación del sistema");
            btnRegistrarTicket.Enabled = false;
        }

        private void RegistrarTickets()
        {
            oRegistro = new RegistroAbastecimiento();
            List<RegistroAbastecimientoDetalle> listadoDetalle = new List<RegistroAbastecimientoDetalle>();
            oRegistro.correlativo = Convert.ToInt32(this.txtCorrelativo.Text);
            oRegistro.Idingresosalidaacopio = this.oRecepcion.IDINGRESOSALIDAACOPIOCAMPO.Trim();
            oRegistro.item = this.oRecepcion.item != null ? this.oRecepcion.item.Trim() : string.Empty;
            oRegistro.fechaRegistro = DateTime.Now;
            oRegistro.cantidad = this.oRecepcion.NROJABAS != null ? Convert.ToInt32(this.oRecepcion.NROJABAS) : 0;
            oRegistro.hora = DateTime.Now;

            oRegistro.tipoRegistro = 'A';

            int registrosGrilla = this.dgvTickets.Rows.Count;
            if (registrosGrilla > 0)
            {
                foreach (DataGridViewRow item in dgvTickets.Rows)
                {
                    oDetalle = new RegistroAbastecimientoDetalle();
                    oDetalle.itemDetalle = item.Cells["chitemDetalle"].Value != null ? Convert.ToInt32(item.Cells["chitemDetalle"].Value) : 0;
                    oDetalle.correlativo = Convert.ToInt32(this.txtCorrelativo.Text);
                    oDetalle.Idingresosalidaacopio = this.oRecepcion.IDINGRESOSALIDAACOPIOCAMPO.Trim();
                    oDetalle.item = this.oRecepcion.item != null ? this.oRecepcion.item.Trim() : string.Empty;
                    oDetalle.fechaRegistro = DateTime.Now; ;
                    oDetalle.cantidad = item.Cells["chCantidad"].Value != null ? Convert.ToInt32(item.Cells["chCantidad"].Value) : 0;
                    oDetalle.impreso = 0;
                    oDetalle.tipoCultivo = txtTipoCultivo.Text.ToUpper() == "CONVENCIONAL" ? 'C' : 'O';
                    oDetalle.idTicketReservado = item.Cells["chidTicketReservado"].Value != null ? Convert.ToInt32(item.Cells["chidTicketReservado"].Value) : (int?)null;
                    listadoDetalle.Add(oDetalle);
                }
            }
            modelo = new RegistroAbastecimientoController();
            oRegistroNew = new RegistroAbastecimiento();
            oRegistroNew = modelo.ToRegister("NSFAJAS", oRegistro, listadoDetalle);
            this.txtCorrelativo.Text = oRegistroNew.correlativo.ToString();
        }

        private void btnDistribuir_Click(object sender, EventArgs e)
        {
            #region Distribuir()

            Distribuir();
            btnDistribuir.Enabled = false;
            #endregion
        }

        private void Distribuir()
        {
            try
            {
                //1.- obtener cantidades del detalle de distribución                
                decimal CantidadGrillaDetalle = 0;
                int registrosGrilla = this.dgvTickets.Rows.Count;

                if (registrosGrilla > 0)
                {
                    foreach (DataGridViewRow item in dgvTickets.Rows)
                    {
                        CantidadGrillaDetalle += item.Cells["chCantidad"].Value != null ? Convert.ToDecimal(item.Cells["chCantidad"].Value) : 0;
                    }
                }
                //2.- Si tiene aun pendiente de distribución distribuir por la diferencia
                #region Distribuir()  

                if (CantidadGrillaDetalle < this.oRecepcion.NROJABAS.Value)
                {
                    decimal? segmentos = Math.Round(this.oRecepcion.NROJABAS.Value / Convert.ToInt32(this.txtPuntoQuiebraTicket.Value));

                    switch (segmentos.ToString())
                    {

                        case "0":
                            if (dgvTickets != null)
                            {
                                ArrayList array = new ArrayList();
                                array.Add(0); //itemDetalle                                      
                                array.Add(this.oRecepcion.NROJABAS.Value.ToString()); // Cantidad
                                array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                                array.Add(0); // impreso           
                                array.Add(0); // reservado           
                                dgvTickets.AgregarFila(array);
                            }
                            else
                            {
                                Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                            }
                            break;

                        //case "1":
                        //    //if (dgvTickets != null)
                        //    //{
                        //    //    ArrayList array = new ArrayList();
                        //    //    array.Add(0); //itemDetalle                                      
                        //    //    array.Add(this.oRecepcion.NROJABAS.Value.ToString()); // Cantidad
                        //    //    array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                        //    //    array.Add(0); // impreso                                      
                        //    //    dgvTickets.AgregarFila(array);
                        //    //}
                        //    //else
                        //    //{
                        //    //    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                        //    //}
                        //    break;

                        //case "2":

                        //    //int PuntoQuiebre = Convert.ToInt32(this.txtPuntoQuiebraTicket.Value);
                        //    //decimal? diferenciaPorDistribuir = this.oRecepcion.NROJABAS.Value - PuntoQuiebre;

                        //    //if (dgvTickets != null)
                        //    //{
                        //    //    ArrayList array = new ArrayList();
                        //    //    array.Add(0); //itemDetalle                                      
                        //    //    array.Add(PuntoQuiebre.ToString()); // Cantidad
                        //    //    array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                        //    //    array.Add(0); // impreso                                      
                        //    //    dgvTickets.AgregarFila(array);
                        //    //}
                        //    //else
                        //    //{
                        //    //    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                        //    //}

                        //    //if (dgvTickets != null)
                        //    //{
                        //    //    ArrayList array = new ArrayList();
                        //    //    array.Add(0); //itemDetalle                                      
                        //    //    array.Add(diferenciaPorDistribuir.ToString()); // Cantidad
                        //    //    array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                        //    //    array.Add(0); // impreso                                      
                        //    //    dgvTickets.AgregarFila(array);
                        //    //}
                        //    //else
                        //    //{
                        //    //    Formateador.MostrarMensajeAdvertencia(this, "Haga click en la Grilla a Modificar", "Validacion Ingreso de Datos");
                        //    //}

                        //    int PuntoQuiebre3 = Convert.ToInt32(this.txtPuntoQuiebraTicket.Value);
                        //    decimal? nroJaba3 = this.oRecepcion.NROJABAS.Value;
                        //    decimal sumatoria2 = 0;

                        //    for (int i = 1; i <= Convert.ToInt32(segmentos.ToString()); i++)
                        //    {

                        //        ArrayList array = new ArrayList();
                        //        array.Add(0); //itemDetalle           
                        //        if (i == Convert.ToInt32(segmentos.ToString()))
                        //        {
                        //            array.Add(nroJaba3 - sumatoria2); // Cantidad
                        //        }
                        //        else
                        //        {
                        //            array.Add(PuntoQuiebre3.ToString()); // Cantidad
                        //        }
                        //        array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                        //        array.Add(0); // impreso      

                        //        sumatoria2 += PuntoQuiebre3;

                        //        dgvTickets.AgregarFila(array);
                        //    }
                        //    break;


                        default:
                            int PuntoQuiebre2 = Convert.ToInt32(this.txtPuntoQuiebraTicket.Value);
                            decimal? nroJabas = this.oRecepcion.NROJABAS.Value;
                            decimal sumatoria = 0;

                            for (int i = 1; i <= Convert.ToInt32(segmentos.ToString()); i++)
                            {

                                ArrayList array = new ArrayList();
                                array.Add(0); //itemDetalle 

                                if (i == Convert.ToInt32(segmentos.ToString()))
                                {
                                    #region
                                    // si 42 esmayor que 30
                                    if ((nroJabas - sumatoria) > PuntoQuiebre2)
                                    {
                                        array.Add(PuntoQuiebre2.ToString());
                                        sumatoria += PuntoQuiebre2;
                                    }
                                    else
                                    {
                                        array.Add(nroJabas - sumatoria); // Cantidad
                                        sumatoria += Convert.ToDecimal(nroJabas - sumatoria);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    array.Add(PuntoQuiebre2.ToString()); // Cantidad
                                    sumatoria += PuntoQuiebre2;
                                }
                                array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                                array.Add(0); // impreso      
                                array.Add(0); // ticket reservado   
                                //sumatoria += PuntoQuiebre2;

                                dgvTickets.AgregarFila(array);
                            }


                            if (nroJabas != sumatoria)
                            {
                                ArrayList array = new ArrayList();
                                array.Add(0); //itemDetalle                                      
                                array.Add((nroJabas - sumatoria).ToString()); // Cantidad
                                array.Add(DateTime.Now.ToPresentationDateTime()); // fecha                                                                                                                         ); // Hora
                                array.Add(0); // impreso                                      
                                array.Add(0); // ticker reservado                                      
                                dgvTickets.AgregarFila(array);
                            }

                            break;
                    }
                }



                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "MENSAJE");
                return;
            }
        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                listadoDetalleTicketRegistrados = new List<RegistroAbastecimientoDetalle>();
                modelo = new RegistroAbastecimientoController();
                listadoDetalleTicketRegistrados = modelo.ObtenerDetalleTicketByCorrelativo("NSFAJAS", this.oRecepcion.correlativo).ToList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }


        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (listadoDetalleTicketRegistrados != null)
                {
                    if (listadoDetalleTicketRegistrados.Count > 0)
                    {
                        dgvTickets.DataSource = listadoDetalleTicketRegistrados;
                        dgvTickets.Refresh();
                    }
                }

                gbDatosGenerales.Enabled = !false;
                gbDetalle.Enabled = !false;
                gbOpcionesImpresion.Enabled = !false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                gbDatosGenerales.Enabled = !false;
                gbDetalle.Enabled = !false;
                gbOpcionesImpresion.Enabled = !false;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCorrelativo.Text != string.Empty)
                {
                    if (this.txtCorrelativo.Text != "0")
                    {
                        ImpresionTicketsAbastecimientoMateriaPrimaImprimir ofrm = new ImpresionTicketsAbastecimientoMateriaPrimaImprimir(Convert.ToInt32(this.txtCorrelativo.Text));
                        ofrm.ShowDialog();
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }



        }

        private void bgwObtenerCorrelativo_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwObtenerCorrelativo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnVistaPreviaFormato02_Click(object sender, EventArgs e)
        {
            if (this.txtCorrelativo.Text != string.Empty)
            {
                if (this.txtCorrelativo.Text != "0")
                {
                    RegistroTicketsAbastecimientoMateriaPrimaFormato02 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato02(Convert.ToInt32(this.txtCorrelativo.Text));
                    ofrm.ShowDialog();
                }
            }
        }

        private void btnVistaPreviaFormato03_Click(object sender, EventArgs e)
        {
            if (this.txtCorrelativo.Text != string.Empty)
            {
                if (this.txtCorrelativo.Text != "0")
                {
                    RegistroTicketsAbastecimientoMateriaPrimaFormato03 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato03(Convert.ToInt32(this.txtCorrelativo.Text));
                    ofrm.ShowDialog();
                }
            }
        }

        private void btnVistaPreviaFormato04_Click(object sender, EventArgs e)
        {
            if (this.txtCorrelativo.Text != string.Empty)
            {
                if (this.txtCorrelativo.Text != "0")
                {
                    RegistroTicketsAbastecimientoMateriaPrimaFormato04 ofrm = new RegistroTicketsAbastecimientoMateriaPrimaFormato04(Convert.ToInt32(this.txtCorrelativo.Text));
                    ofrm.ShowDialog();
                }
            }
        }

        private void btnImprimirFormato01_Click(object sender, EventArgs e)
        {
            PrintCodeBarRange(Convert.ToInt32(this.txtCorrelativo.Text));
        }

        private void dgvTickets_KeyUp(object sender, KeyEventArgs e)
        {
            modeloTickerReservado = new SAS_TicketReservadoController();
            if (((DataGridView)sender).RowCount > 0)
            {
                #region Tipo de interface() 
                if (((DataGridView)sender).CurrentCell.OwningColumn.Name == "chidTicketReservado")
                {
                    if (e.KeyCode == Keys.F3)
                    {
                        frmBusquedaFormatoSimple search = new frmBusquedaFormatoSimple();
                        if (this.txtTipoCultivo.Text.Trim().ToUpper() == "CONVENCIONAL")
                        {
                            search.ListaGeneralResultado = modeloTickerReservado.GetListTickerReservados("NSFAJAS", "C");
                            search.Text = "Buscar ticket reservados";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvTickets.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTicketReservado"].Value = search.ObjetoRetorno.Codigo;
                                //this.dgvTickets.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTicketReservado"].Value = search.ObjetoRetorno.Descripcion;
                            }
                        }
                        else if (this.txtTipoCultivo.Text.Trim().ToUpper() == "ORGANICO")
                        {
                            search.ListaGeneralResultado = modeloTickerReservado.GetListTickerReservados("NSFAJAS", "O");
                            search.Text = "Buscar ticket reservados";
                            search.txtTextoFiltro.Text = "";
                            if (search.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                //idRetorno = busquedas.ObjetoRetorno.Codigo;
                                this.dgvTickets.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTicketReservado"].Value = search.ObjetoRetorno.Codigo;
                                //this.dgvTickets.Rows[((DataGridView)sender).CurrentRow.Index].Cells["chidTicketReservado"].Value = search.ObjetoRetorno.Descripcion;
                            }
                        }


                    }
                }
                #endregion

            }
        }

        private void btnImprimirFormatoGrande_Click(object sender, EventArgs e)
        {

        }

        private void PrintCodeBarRange(int correlativo)
        {

            try
            {
                PrintDialog impresion = new PrintDialog();
                DialogResult Result = impresion.ShowDialog();
                //if (impresion.ShowDialog() == DialogResult.OK)
                if (Result == DialogResult.OK)
                {
                    dsReporte = new ImpresionTicketsAbastecimientoMateriaPrimaImprimirDS();
                    adaptador = new Cosecha.ImpresionTicketsAbastecimientoMateriaPrimaImprimirDSTableAdapters.ListadoAcopioByTiktesByCorrelativoTableAdapter();
                    dsReporte.EnforceConstraints = false;
                    adaptador.Fill(dsReporte.ListadoAcopioByTiktesByCorrelativo, correlativo);
                    dta = new DataTable();
                    if (dsReporte.ListadoAcopioByTiktesByCorrelativo.Rows.Count <= 0)
                    {
                        MessageBox.Show("No se encontró información para imprimir !");
                        return;
                    }
                    oRpt = new ReportDocument();
                    CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocumento = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    oRpt.Load(@"C:\SOLUTION\TicketAbastecimientoMateriaPrimaFormato01.rpt");
                    dta = dsReporte.ListadoAcopioByTiktesByCorrelativo;
                    oRpt.SetDataSource(dta);
                    reportDocumento = oRpt;
                    oRpt.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                    oRpt.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

        private void btnEditarTickets_Click(object sender, EventArgs e)
        {
            if (_user2 != null)
            {
                if (_user2.IdUsuario != null)
                {
                    if (_user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || _user2.IdUsuario.Trim().ToUpper() == "EAURAZO" || _user2.IdUsuario.Trim().ToUpper() == "HVALENCIA" || _user2.IdUsuario.Trim().ToUpper() == "FCERNA")
                    {
                        gbDatosGenerales.Enabled = true;
                        gbDetalle.Enabled = true;
                        gbOpcionesImpresion.Enabled = true;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Distribuir();
                btnDistribuir.Enabled = false;
                RegistrarTickets();
                btnRegistrarTicket.Enabled = false;
                PrintCodeBarRange(Convert.ToInt32(this.txtCorrelativo.Text));
                btnGenerarEImprimir.Enabled = false;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Mensaje del sistema");
                return;
            }

        }

    }
}
