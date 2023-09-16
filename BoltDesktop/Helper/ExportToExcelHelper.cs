using Microsoft.Win32;
using MyControlsDataBinding.Controles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Asistencia.Helper
{
    public class ExportToExcelHelper
    {

        public void ExportarToExcel(RadGridView radGridView, System.Windows.Forms.SaveFileDialog saveFileDialog)
        {
            saveFileDialog.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                RadMessageBox.Show("Ingrese nombre al archivo.");
                return;
            }

            string fileName = saveFileDialog.FileName;
            bool openExportFile = false;
            bool exportVisualSettings = true;
            RunExportToExcelML(fileName, ref openExportFile, radGridView, exportVisualSettings);


            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    string message = String.Format("El archivo no pudo ser ejecutado por el sistema.\nError message: {0}", ex.Message);
                    RadMessageBox.Show(message, "Abrir Archivo", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void RunExportToExcelML(string fileName, ref bool openExportFile, RadGridView grilla, bool exportVisualSettings)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(grilla);
            excelExporter.SheetName = "Document";
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;
            excelExporter.SheetMaxRows = ExcelMaxRows._1048576;
            excelExporter.ExportVisualSettings = exportVisualSettings;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;


            try
            {
                excelExporter.RunExport(fileName);
                RadMessageBox.SetThemeName(grilla.ThemeName);
                DialogResult dr = RadMessageBox.Show("La exportación ha sido generada correctamente. Desea abrir el Archivo?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    openExportFile = true;
                }
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(grilla.ThemeName);
                RadMessageBox.Show(ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }



        public void LimpiarControlesEnGrupoBox(Control control, GroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }

        public void LimpiarControlesEnGrupoBox(Control control, RadGroupBox gb)
        {
            // Checar todos los textbox del formulario
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                if (txt is ComboBox)
                {
                    ((ComboBox)txt).SelectedIndex = 0;
                }
            }
            foreach (var combo in gb.Controls)
            {
                if (combo is TextBox)
                {
                    ((TextBox)combo).Clear();
                }
                if (combo is ComboBox)
                {
                    ((ComboBox)combo).SelectedIndex = 0;
                }
                if (combo is RadTextBox)
                {
                    ((RadTextBox)combo).Clear();
                }
                if (combo is MyTextBox)
                {
                    ((MyTextBox)combo).Clear();
                }
                if (combo is MyTextBoxSearchSimple)
                {
                    ((MyTextBoxSearchSimple)combo).Clear();
                }
                if (combo is MyTextSearch)
                {
                    ((MyTextSearch)combo).Clear();
                }
                if (combo is MyMaskedDate)
                {
                    ((MyMaskedDate)combo).Clear();
                }
                if (combo is MyMaskedDateTime)
                {
                    ((MyMaskedDateTime)combo).Clear();
                }
            }
        }

    }
}
