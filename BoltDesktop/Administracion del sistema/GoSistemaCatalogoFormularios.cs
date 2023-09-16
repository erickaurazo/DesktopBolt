using Asistencia.Datos;
using Asistencia.Helper;
using Asistencia.Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;

namespace Asistencia
{
    public partial class GoSistemaCatalogoFormularios : Form
    {
        private FormsController Modelo;
        private List<SAS_FormularioSistema> forms;
        private List<SAS_FormularioSistema> SASforms, SASformsPorModulo = new List<SAS_FormularioSistema>();
        private TreeviewHelper modelHelper;
        private ComboBoxHelper modelComboBox;
        private List<Grupo> resultComboBoxStatus;
        private List<Grupo> resultComboBoxModules;
        private List<Grupo> resultComboBoxHierarchies;
        private List<Grupo> moduloInformatico;
        private List<Grupo> resultComboBoxForms;
        private List<Grupo> resultComboBoxParentForms;
        private SAS_FormularioSistema formByConsult;
        private string _conection;
        private ASJ_USUARIOS _user;
        private string _companyId;
        private string codigoFormularioSelecionado;

        public GoSistemaCatalogoFormularios()
        {
            InitializeComponent();
            _conection = "SAS";
            _user = new ASJ_USUARIOS();
            _user.IdUsuario = "eaurazo";
            _user.NombreCompleto = "ERICK AURAZO CARHUATANTA";
            _companyId = "001";
            LoadInitialForm();
            RefreshList();
            //LoadData();
            RadMenuItem item = new RadMenuItem("Niguno");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            item = new RadMenuItem("Ascending");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            item = new RadMenuItem("Descending");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            ImagePrimitive searchIcon = new ImagePrimitive();
            searchIcon.Image = imageList1.Images[4];
            searchIcon.Alignment = ContentAlignment.MiddleRight;
            this.txtFormulario.TextBoxElement.Children.Add(searchIcon);
            this.txtFormulario.TextBoxElement.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
            this.txtFormulario.TextBoxElement.TextBoxItem.StretchHorizontally = false;
            this.txtFormulario.TextBoxElement.TextBoxItem.HostedControl.MinimumSize = new Size(120, 0);
            this.txtFormulario.TextBoxElement.TextBoxItem.PropertyChanged += new PropertyChangedEventHandler(TextBoxItem_PropertyChanged);
            this.tvFormulario.TreeViewElement.AllowAlternatingRowColor = true;
            this.tvFormulario.AllowEdit = false;
            this.tvFormulario.AllowAdd = true;
            this.tvFormulario.AllowRemove = true;
            this.tvFormulario.ItemHeight = 27;
            this.tvFormulario.AllowDefaultContextMenu = true;
            this.AutoScroll = false;
            //this.radPanel1.Text = "Subscriptions";


        }

        public GoSistemaCatalogoFormularios(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
            LoadInitialForm();
            RefreshList();
            //LoadData();
            RadMenuItem item = new RadMenuItem("None");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            item = new RadMenuItem("Ascending");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            item = new RadMenuItem("Descending");
            item.Click += new EventHandler(item_Click);
            this.cbOrder.Items.Add(item);
            ImagePrimitive searchIcon = new ImagePrimitive();
            searchIcon.Image = imageList1.Images[4];

            searchIcon.Alignment = ContentAlignment.MiddleRight;
            this.txtFormulario.TextBoxElement.Children.Add(searchIcon);
            this.txtFormulario.TextBoxElement.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
            this.txtFormulario.TextBoxElement.TextBoxItem.StretchHorizontally = false;
            this.txtFormulario.TextBoxElement.TextBoxItem.HostedControl.MinimumSize = new Size(120, 0);
            this.txtFormulario.TextBoxElement.TextBoxItem.PropertyChanged += new PropertyChangedEventHandler(TextBoxItem_PropertyChanged);
            this.tvFormulario.TreeViewElement.AllowAlternatingRowColor = true;
            this.tvFormulario.AllowEdit = false;
            this.tvFormulario.AllowAdd = false;
            this.tvFormulario.AllowRemove = false;
            this.tvFormulario.ItemHeight = 27;
            this.tvFormulario.AllowDefaultContextMenu = true;
            this.AutoScroll = false;
            //this.radPanel1.Text = "Subscriptions";


        }

        private void LoadInitialForm()
        {

            gbEdition.Enabled = false;
            gbList.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnAtras.Enabled = false;

            ProgressBar.Visible = true;
            bgwHiloInicio.RunWorkerAsync();
        }

        private void LoadData()
        {

            //// SISTEMAS
            //tvFormulario.Nodes.Clear();
            //RadTreeNode root = this.tvFormulario.Nodes.Add("Sistema", 1);
            ////root.Nodes.Add("Catálogo", 2);
            //RadTreeNode folderCatalogoSistema = root.Nodes.Add("Catálogo", 2);
            //folderCatalogoSistema.Nodes.Add("Configuración del sistema", 3);
            //folderCatalogoSistema.Nodes.Add("Módulos", 3);
            //folderCatalogoSistema.Nodes.Add("Administracion de formularios", 3);
            //folderCatalogoSistema.Nodes.Add("Administracion de accesos y privilegios", 3);
            //root.Nodes.Add("Movimiento", 2);
            //root.Nodes.Add("Proceso", 2);
            //root.Nodes.Add("Rerportes", 2);
            //root.Nodes.Add("Utilitarios", 2);


            //// PLANILLA
            //root = this.tvFormulario.Nodes.Add("Planillas", 1);
            ////root.Nodes.Add("Catálogo", 2);
            //RadTreeNode folderCatalogoPlanilla = root.Nodes.Add("Catálogo", 2);
            //RadTreeNode folderCatalogoPersona = folderCatalogoPlanilla.Nodes.Add("Personal", 2);
            //folderCatalogoPersona.Nodes.Add("Personal", 3);
            //folderCatalogoPersona.Nodes.Add("Personal por paradero", 3);
            //folderCatalogoPersona.Nodes.Add("Personal observado", 3);
            //folderCatalogoPlanilla.Nodes.Add("Tipo de observaciones en ingreso", 3);
            //RadTreeNode folderMovimientoPlanilla = root.Nodes.Add("Movimiento", 2);
            //folderMovimientoPlanilla.Nodes.Add("Registro de asistencia", 3);
            //RadTreeNode folderProcesoPlanilla = root.Nodes.Add("Proceso", 2);
            //folderProcesoPlanilla.Nodes.Add("Actualizar lista para sincronización", 3);
            //RadTreeNode folderProcesoReportes = root.Nodes.Add("Reportes", 2);
            //folderProcesoReportes.Nodes.Add("Reporte de asistencia", 3);
            //folderProcesoReportes.Nodes.Add("Reporte de asistencia observadas", 3);
            //root.Nodes.Add("Utilitarios", 2);

            //root = this.tvFormulario.Nodes.Add("Transportes", 1);
            //RadTreeNode folderCatalogoTransportes = root.Nodes.Add("Catálogo", 2);
            //folderCatalogoTransportes.Nodes.Add("Catálogo de empresas de transportes", 3);
            //folderCatalogoTransportes.Nodes.Add("Paraderos", 3);
            //folderCatalogoTransportes.Nodes.Add("Rutas", 3);
            //RadTreeNode folderMovimientoTransportes = root.Nodes.Add("Movimiento", 2);
            //root.Nodes.Add("Proceso", 2);
            //RadTreeNode folderReportesTransportes = root.Nodes.Add("Reportes", 2);
            //folderReportesTransportes.Nodes.Add("Asistencia de personal por tablet", 3);
            //folderReportesTransportes.Nodes.Add("Ingreso y salida de unidades móviles", 3);
            //folderReportesTransportes.Nodes.Add("Vencimiento de documentos", 3);
            //root.Nodes.Add("Utilitarios", 2);
        }


        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.tvFormulario.Filter = this.txtFormulario.Text;
        }


        private void radTreeView1_NodeFormatting(object sender, TreeNodeFormattingEventArgs args)
        {
            if (args.Node.Text.Contains("("))
            {
                args.NodeElement.ContentElement.Text = "" + args.Node.Text;
            }
        }


        private void TextBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bounds")
            {
                txtFormulario.TextBoxElement.TextBoxItem.HostedControl.MaximumSize = new Size(txtFormulario.Size.Width - 28, 0);
            }
        }

        protected void WireEvents()
        {
            this.tvFormulario.NodeFormatting += new Telerik.WinControls.UI.TreeNodeFormattingEventHandler(this.radTreeView1_NodeFormatting);
            this.txtFormulario.TextChanged += new System.EventHandler(this.radTextBox1_TextChanged);
        }

        void item_Click(object sender, EventArgs e)
        {
            RadMenuItem item = (RadMenuItem)sender;
            this.cbOrder.Text = item.Text;
            if (item.Text == "None")
            {
                tvFormulario.SortOrder = SortOrder.None;
            }
            else if (item.Text == "Ascending")
            {
                tvFormulario.SortOrder = SortOrder.Ascending;
            }
            else
            {
                tvFormulario.SortOrder = SortOrder.Descending;
            }
        }


        private void Formulario_Load(object sender, EventArgs e)
        {



        }

        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {

            Modelo = new FormsController();
            forms = new List<SAS_FormularioSistema>();
            SASforms = new List<SAS_FormularioSistema>();
            moduloInformatico = new List<Grupo>();

            SASforms = Modelo.ObtenerListadoFormularios(_conection);

            if (SASforms != null && SASforms.ToList().Count > 0)
            {
                var listado = SASforms.Where(x => x.formulario.Trim().ToUpper() == "Menu".ToUpper()).ToList();
                if (listado != null)
                {
                    if (listado.ToList().Count > 0)
                    {

                        moduloInformatico = (from items in listado
                                             select new Grupo {  Codigo = items.moduloCodigo.Trim(), Descripcion = items.descripcion.Trim().ToUpper(), Id = items.moduloCodigo.Trim() }
                                             ).ToList();
                        moduloInformatico.Add(new Grupo { Codigo = "000", Descripcion = "TODOS"});
                    }
                }
            }
            forms = SASforms;
            modelComboBox = new ComboBoxHelper();
            resultComboBoxStatus = new List<Grupo>();
            resultComboBoxModules = new List<Grupo>();
            resultComboBoxHierarchies = new List<Grupo>();
            resultComboBoxForms = new List<Grupo>();
            resultComboBoxParentForms = new List<Grupo>();

            resultComboBoxStatus = modelComboBox.GetComboBoxStatus();
            resultComboBoxModules = modelComboBox.GetComboBoxModule(_conection);
            resultComboBoxHierarchies = modelComboBox.GetComboBoxHierarchy(forms);
            resultComboBoxForms = modelComboBox.GetComboBoxTypeForm(forms);
            resultComboBoxParentForms = modelComboBox.GetComboBoxParentForm(forms);
            modelHelper = new TreeviewHelper();

        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            cboEstatus.DisplayMember = "Descripcion";
            cboEstatus.ValueMember = "Code";
            cboEstatus.DataSource = resultComboBoxStatus;


            cboModulo.DisplayMember = "Descripcion";
            cboModulo.ValueMember = "Codigo";
            cboModulo.DataSource = resultComboBoxModules;


            cboHierarchy.DisplayMember = "Descripcion";
            cboHierarchy.ValueMember = "Codigo";
            cboHierarchy.DataSource = resultComboBoxHierarchies;

            cboForm.DisplayMember = "Descripcion";
            cboForm.ValueMember = "Codigo";
            cboForm.DataSource = resultComboBoxForms;

            cboParentForm.DisplayMember = "Descripcion";
            cboParentForm.ValueMember = "Codigo";
            cboParentForm.DataSource = resultComboBoxParentForms;

            modelHelper.BuildTreeViewForms(tvFormulario, forms);
            this.tvFormulario.ExpandAll();

            if (moduloInformatico != null)
            {
                if (moduloInformatico.ToList().Count > 0)
                {
                    cboModulos.DisplayMember = "Descripcion";
                    cboModulos.ValueMember = "Codigo";
                    cboModulos.DataSource = moduloInformatico.OrderBy(x => x.Id).ToList();
                }
            }

            gbEdition.Enabled = false;
            gbList.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
            ProgressBar.Visible = !true;
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            tvFormulario.Nodes.Clear();
            gbEdition.Enabled = false;
            gbList.Enabled = false;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnAtras.Enabled = false;
            ProgressBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (formByConsult != null && formByConsult.formularioCodigo.Trim() != null && formByConsult.Jerarquia.Trim() != null)
            {
                if (formByConsult.formularioCodigo.Trim() != string.Empty && formByConsult.Jerarquia.Trim().Length > 3)
                {
                    ClearForm();
                    this.txtDescription.Focus();
                    gbEdition.Enabled = true;
                    gbList.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnActualizarLista.Enabled = false;
                    btnAnular.Enabled = false;
                    btnEliminarRegistro.Enabled = false;
                    btnSave.Enabled = true;
                    btnAtras.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se puede crear formulario en esta jerarquía", "Advertencia del sistema");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Debe ubicarse en el registro anterior para generar gerarquía", "Advertencia del sistema");
                return;
            }

        }

        private void ClearForm()
        {
            txtFormCode.Clear();
            txtDescription.Clear();
            txtNameInTheSystem.Clear();
            cboEstatus.SelectedValue = 1;
            cboHierarchy.SelectedValue = (formByConsult.Jerarquia != null ? formByConsult.Jerarquia.Trim() : string.Empty);
            cboForm.SelectedValue = (formByConsult.descripcion != null ? formByConsult.descripcion.Trim() : string.Empty);
            cboParentForm.SelectedValue = (formByConsult.Jerarquia != null ? formByConsult.Jerarquia.Trim() : string.Empty);
            cboModulo.SelectedValue = (formByConsult.moduloCodigo != null ? formByConsult.moduloCodigo.Trim() : string.Empty);
            chkIdModul.Checked = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            gbEdition.Enabled = true;
            gbList.Enabled = false;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnActualizarLista.Enabled = false;
            btnAnular.Enabled = false;
            btnEliminarRegistro.Enabled = false;
            btnSave.Enabled = true;
            btnAtras.Enabled = true;
            txtDescription.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void Save()
        {
            try
            {
                Modelo = new FormsController();
                FormularioSistema oForm = new FormularioSistema();
                oForm.formularioCodigo = txtFormCode.Text.Trim();
                oForm.descripcion = this.txtDescription.Text.Trim();
                oForm.nombreEnSistema = this.txtNameInTheSystem.Text.Trim();
                oForm.Jerarquia = (formByConsult.Jerarquia != null ? formByConsult.Jerarquia.Trim() : string.Empty);
                oForm.formulario = (formByConsult.descripcion != null ? formByConsult.descripcion.Trim() : string.Empty);
                oForm.barraPadre = (formByConsult.Jerarquia != null ? formByConsult.Jerarquia.Trim() : string.Empty);

                if (oForm.descripcion != string.Empty && oForm.nombreEnSistema != string.Empty)
                {
                    #region MyRegion


                    if (Modelo.ToRegister(_conection, oForm) == true)
                    {
                        MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                        gbEdition.Enabled = false;
                        gbList.Enabled = true;
                        btnNuevo.Enabled = true;
                        btnActualizarLista.Enabled = true;
                        btnAnular.Enabled = true;
                        btnEliminarRegistro.Enabled = true;
                        btnSave.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAtras.Enabled = false;
                        RefreshList();

                    }
                    else
                    {
                        MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                        return;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                    return;
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void ChangeStatus()
        {
            try
            {
                Modelo = new FormsController();
                FormularioSistema oForm = new FormularioSistema();
                oForm.formularioCodigo = txtFormCode.Text.Trim();
                oForm.formulario = cboForm.SelectedValue.ToString().Trim();

                if (oForm.descripcion != string.Empty && oForm.nombreEnSistema != string.Empty)
                {
                    #region MyRegion
                    if (oForm.formulario != null)
                    {
                        #region MyRegion                        
                        if (oForm.formulario.Trim().ToUpper() != "SubMenu".ToUpper())
                        {
                            #region MyRegion                            
                            if (oForm.formulario.Trim().ToUpper() != "Menu".ToUpper())
                            {
                                #region MyRegion                                
                                if (Modelo.ChangeStatus(_conection, oForm) == true)
                                {
                                    #region MyRegion                                    
                                    MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                                    gbEdition.Enabled = false;
                                    gbList.Enabled = true;
                                    btnNuevo.Enabled = true;
                                    btnActualizarLista.Enabled = true;
                                    btnAnular.Enabled = true;
                                    btnEliminarRegistro.Enabled = true;
                                    btnSave.Enabled = false;
                                    btnEditar.Enabled = true;
                                    btnAtras.Enabled = false;
                                    RefreshList();
                                    #endregion
                                }
                                else
                                {
                                    MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("No esta permitido la eliminación de opción menú", "Advertencia del sistema");
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("No esta permitido la eliminación de opción submenus", "Advertencia del sistema");
                            return;
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                    return;
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void Remove()
        {
            try
            {
                Modelo = new FormsController();
                FormularioSistema oForm = new FormularioSistema();
                oForm.formularioCodigo = txtFormCode.Text.Trim();
                oForm.formulario = cboForm.SelectedValue.ToString().Trim();
                if (oForm.descripcion != string.Empty && oForm.nombreEnSistema != string.Empty)
                {
                    #region MyRegion
                    if (oForm.formulario != null)
                    {
                        #region MyRegion
                        if (oForm.formulario.Trim().ToUpper() != "SubMenu".ToUpper())
                        {
                            #region MyRegion                            
                            if (oForm.formulario.Trim().ToUpper() != "Menu".ToUpper())
                            {
                                #region MyRegion                                
                                if (Modelo.Remove(_conection, oForm) == true)
                                {
                                    MessageBox.Show("Operacion realizada satisfactoriamente", "Confirmación del sistema");
                                    gbEdition.Enabled = false;
                                    gbList.Enabled = true;
                                    btnNuevo.Enabled = true;
                                    btnActualizarLista.Enabled = true;
                                    btnAnular.Enabled = true;
                                    btnEliminarRegistro.Enabled = true;
                                    btnSave.Enabled = false;
                                    btnEditar.Enabled = true;
                                    btnAtras.Enabled = false;
                                    RefreshList();

                                }
                                else
                                {
                                    MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("No esta permitido la eliminación de opción menú", "Advertencia del sistema");
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("No esta permitido la eliminación de opción submenus", "Advertencia del sistema");
                            return;
                        }
                        #endregion                       
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("El formulario contiene datos no válidos para su registro", "Advertencia del sistema");
                    return;
                }


            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "Advertencia del sistema");
                return;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            ClearForm();
            gbEdition.Enabled = false;
            gbList.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizarLista.Enabled = true;
            btnAnular.Enabled = true;
            btnEliminarRegistro.Enabled = true;
            btnSave.Enabled = false;
            btnEditar.Enabled = true;
            btnAtras.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            ChangeStatus();
        }

        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            Remove();

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

        private void Formularios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bgwHilo.IsBusy == true)
            {
                e.Cancel = true;
                MessageBox.Show("No puede cerrar la ventana, Existe un proceso ejecutandose",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgwHiloInicio_DoWork(object sender, DoWorkEventArgs e)
        {
            //Modelo =new FormsController();
            //forms = new List<FormularioSistema>();
            //forms = Modelo.GetListForms(period);
            //modelComboBox = new ComboBoxHelper();
            //resultComboBoxStatus = new List<Grupo>();
            //resultComboBoxModules = new List<Grupo>();
            //resultComboBoxHierarchies = new List<Grupo>();
            //resultComboBoxForms = new List<Grupo>();
            //resultComboBoxParentForms = new List<Grupo>();

            //resultComboBoxStatus = modelComboBox.GetComboBoxStatus();
            //resultComboBoxModules = modelComboBox.GetComboBoxModule(period);
            //resultComboBoxHierarchies = modelComboBox.GetComboBoxHierarchy(forms);
            //resultComboBoxForms = modelComboBox.GetComboBoxTypeForm(forms);
            //resultComboBoxParentForms = modelComboBox.GetComboBoxParentForm(forms);

        }

        private void bgwHiloInicio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //cboEstatus.DisplayMember = "Descripcion";
            //cboEstatus.ValueMember = "Code";
            //cboEstatus.DataSource = resultComboBoxStatus;


            //cboModulo.DisplayMember = "Descripcion";
            //cboModulo.ValueMember = "Codigo";
            //cboModulo.DataSource = resultComboBoxModules;


            //cboHierarchy.DisplayMember = "Descripcion";
            //cboHierarchy.ValueMember = "Codigo";
            //cboHierarchy.DataSource = resultComboBoxHierarchies;

            //cboForm.DisplayMember = "Descripcion";
            //cboForm.ValueMember = "Codigo";
            //cboForm.DataSource = resultComboBoxForms;

            //cboParentForm.DisplayMember = "Descripcion";
            //cboParentForm.ValueMember = "Codigo";
            //cboParentForm.DataSource = resultComboBoxParentForms;

        }

        private void tvFormulario_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {

            //txtDescription.Text = e.Node.Name.Trim();
            //txtNameInTheSystem.Text = tvFormulario.SelectedNode.Value.ToString();
            codigoFormularioSelecionado = string.Empty;
            formByConsult = new SAS_FormularioSistema();
            if (e.Node != null)
            {
                var result = forms.Where(x => x.formularioCodigo.Trim() == e.Node.Name.ToString().Trim());
                codigoFormularioSelecionado = e.Node.Name.ToString().Trim();
                if (result != null && result.ToList().Count == 1)
                {
                    formByConsult = result.Single();
                    txtFormCode.Text = e.Node.Name.ToString();
                    txtDescription.Text = e.Node.Text.Trim();
                    txtNameInTheSystem.Text = formByConsult.nombreEnSistema.Trim();
                    cboEstatus.SelectedValue = Convert.ToInt32(formByConsult.estado);
                    cboHierarchy.SelectedValue = formByConsult.Jerarquia.Trim();
                    cboForm.SelectedValue = formByConsult.formulario.Trim();
                    cboParentForm.SelectedValue = formByConsult.barraPadre.Trim();
                    cboModulo.SelectedValue = formByConsult.moduloCodigo.Trim();
                    chkIdModul.Checked = Convert.ToInt32(formByConsult.EsModuloPrincipal) == 1 ? true : false;
                }
            }




        }

        private void subMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulos.SelectedIndex > -1)
            {
                if (cboModulos.SelectedValue.ToString().Trim() == "000")
                {
                    #region Listar todos
                    tvFormulario.Nodes.Clear();
                    modelHelper = new TreeviewHelper();
                    modelHelper.BuildTreeViewForms(tvFormulario, SASforms);
                    this.tvFormulario.ExpandAll();
                    #endregion
                }
                else if (cboModulos.SelectedValue.ToString().Trim() == "0")
                {
                }
                else
                {
                    #region Listar modulo selecionados
                    tvFormulario.Nodes.Clear();
                    SASformsPorModulo = new List<SAS_FormularioSistema>();
                    SASformsPorModulo = SASforms.Where(x => x.moduloCodigo.Trim() == cboModulos.SelectedValue.ToString().Trim()).ToList();
                    modelHelper = new TreeviewHelper();
                    modelHelper.BuildTreeViewForms(tvFormulario, SASformsPorModulo);
                    this.tvFormulario.ExpandAll();
                    #endregion
                }
            }
        }
    }
}
