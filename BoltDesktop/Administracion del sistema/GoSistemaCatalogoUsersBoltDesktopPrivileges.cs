﻿using Asistencia.Datos;
using Asistencia.Negocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Asistencia
{
    public partial class GoSistemaCatalogoUsersPrivileges : Form
    {
        private string userId;
        private string fullName;
        private UsersController Modelo;
        private List<PrivilegesByUser> privileges;
        private string conection;
        private SAS_USUARIOS user;
        private SAS_USUARIOS userLogin;
        private string companyId;

        public GoSistemaCatalogoUsersPrivileges()
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            this.dgvList.TableElement.BeginUpdate();
            this.LoadFreightSummary();
            this.dgvList.TableElement.EndUpdate();

            base.OnLoad(e);
        }


        private void LoadFreightSummary()
        {
            this.dgvList.MasterTemplate.AutoExpandGroups = true;
            this.dgvList.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvList.GroupDescriptors.Clear();
            this.dgvList.GroupDescriptors.Add(new GridGroupByExpression("CustomerID Group By CustomerID"));
            GridViewSummaryRowItem items1 = new GridViewSummaryRowItem();
            items1.Add(new GridViewSummaryItem("chDescripcionFormulario", "Count : {0:N2}; ", GridAggregateFunction.Count));
            this.dgvList.MasterTemplate.SummaryRowsTop.Add(items1);
        }


        public GoSistemaCatalogoUsersPrivileges(string _userId, string _fullName, string _conection, SAS_USUARIOS _userLogin, string _companyId)
        {
            InitializeComponent();
            userId = _userId;
            fullName = _fullName;
            conection = _conection;
            userLogin = _userLogin;
            companyId = _companyId;
            this.txtFullName.Text = _fullName.Trim();
            this.txtUserCode.Text = _userId.Trim();
            RefreshList();
        }

        private void RefreshList()
        {
            gbEdition.Enabled = false;
            gbList.Enabled = false;
            ProgressBar.Visible = true;
            bgwHilo.RunWorkerAsync();
        }

        private void Privileges_Load(object sender, EventArgs e)
        {

        }



        private void bgwHilo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Modelo = new UsersController();
                privileges = new List<PrivilegesByUser>();
                privileges = Modelo.GetListPrivilegesByUser(conection, userId, companyId);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
            }
        }

        private void bgwHilo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvList.DataSource = privileges;
                dgvList.Refresh();
                gbEdition.Enabled = !false;
                gbList.Enabled = !false;
                ProgressBar.Visible = !true;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                gbEdition.Enabled = !false;
                gbList.Enabled = !false;
                ProgressBar.Visible = !true;
                return;
            }

        }

        private void dgvList_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //nada

        }

        private void dgvList_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //nada
        }

        private void dgvList_CellBeginEdit(object sender, Telerik.WinControls.UI.GridViewCellCancelEventArgs e)
        {
            if (this.dgvList.CurrentColumn.Name == "chNinguno")
            {
                this.dgvList.CurrentRow.Cells["chNuevo"].Value = false;
                this.dgvList.CurrentRow.Cells["chEditar"].Value = false;
                this.dgvList.CurrentRow.Cells["chAnular"].Value = false;
                this.dgvList.CurrentRow.Cells["chEliminar"].Value = false;
                this.dgvList.CurrentRow.Cells["chImprimir"].Value = false;
                this.dgvList.CurrentRow.Cells["chExportar"].Value = false;
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = false;
            }

            if (this.dgvList.CurrentColumn.Name == "chConsultar")
            {
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }

            if (this.dgvList.CurrentColumn.Name == "chNuevo")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }

            if (this.dgvList.CurrentColumn.Name == "chEditar")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }


            if (this.dgvList.CurrentColumn.Name == "chAnular")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }

            if (this.dgvList.CurrentColumn.Name == "chEliminar")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }


            if (this.dgvList.CurrentColumn.Name == "chImprimir")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }


            if (this.dgvList.CurrentColumn.Name == "chExportar")
            {
                this.dgvList.CurrentRow.Cells["chConsultar"].Value = true;
                this.dgvList.CurrentRow.Cells["chNinguno"].Value = false;
            }


        }

        private void dgvList_CellValueChanged(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //nada   
        }

        private void dgvList_ValueChanged(object sender, EventArgs e)
        {
            // nadad
        }

        private void dgvList_CurrentCellChanged(object sender, Telerik.WinControls.UI.CurrentCellChangedEventArgs e)
        {

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (fullName != string.Empty && userId != string.Empty)
                {
                    #region Obtener listado()
                    if (dgvList.RowCount > 0)
                    {
                        List<PrivilegioFormulario> privileges = new List<PrivilegioFormulario>();
                        foreach (GridViewRowInfo rowInfo in dgvList.Rows)
                        {
                            privileges.Add(new PrivilegioFormulario
                            {
                                usuarioCodigo = userId,
                                formularioCodigo = rowInfo.Cells["chFormularioCodigo"].Value != null ? rowInfo.Cells["chFormularioCodigo"].Value.ToString().Trim() : string.Empty,
                                nuevo = rowInfo.Cells["chNuevo"].Value != null ? Convert.ToByte(rowInfo.Cells["chnuevo"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                editar = rowInfo.Cells["chEditar"].Value != null ? Convert.ToByte(rowInfo.Cells["chEditar"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                anular = rowInfo.Cells["chAnular"].Value != null ? Convert.ToByte(rowInfo.Cells["chAnular"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                eliminar = rowInfo.Cells["chEliminar"].Value != null ? Convert.ToByte(rowInfo.Cells["chEliminar"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                imprimir = rowInfo.Cells["chImprimir"].Value != null ? Convert.ToByte(rowInfo.Cells["chImprimir"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                exportar = rowInfo.Cells["chExportar"].Value != null ? Convert.ToByte(rowInfo.Cells["chExportar"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                ninguno = rowInfo.Cells["chNinguno"].Value != null ? Convert.ToByte(rowInfo.Cells["chNinguno"].Value.ToString().Trim()) : Convert.ToByte("0"),
                                consultar = rowInfo.Cells["chConsultar"].Value != null ? Convert.ToByte(rowInfo.Cells["chConsultar"].Value.ToString().Trim()) : Convert.ToByte("0"),
                            });
                        }

                        if (Modelo.AddListPrivilegesByUser(conection, privileges, companyId) == true)
                        {
                            MessageBox.Show("Actualización correcta", "Confirmación del sistem");
                            RefreshList();
                        }

                    }

                    #endregion
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message.ToString(), "MENSAJE DEL SISTEMA");
                return;
            }

        }
    }
}
