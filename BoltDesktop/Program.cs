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
using ComparativoHorasVisualSATNISIRA.Exportaciones;
using ComparativoHorasVisualSATNISIRA.Maquinaria;
using ComparativoHorasVisualSATNISIRA.MRP;
using ComparativoHorasVisualSATNISIRA.Planeamiento_Agricola;
using ComparativoHorasVisualSATNISIRA.Presupuestos;
using ComparativoHorasVisualSATNISIRA.Produccion.Conformacion_de_carga;
using ComparativoHorasVisualSATNISIRA.RRHH;
using ComparativoHorasVisualSATNISIRA.SIG.SST;
using ComparativoHorasVisualSATNISIRA.T.I;
using ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Asistencia
{
    static class Program
    {
        //////////////[STAThread]
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new ConformacionDeCarga());
            Application.Run(new LineasCelulares());
            // Application.Run(new Menu());
        }
    }
}
