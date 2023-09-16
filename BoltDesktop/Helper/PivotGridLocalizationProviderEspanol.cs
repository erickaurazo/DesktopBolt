using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Asistencia.ClaseTelerik
{
    public class PivotGridLocalizationProviderEspanol : PivotGridLocalizationProvider
    {

        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case PivotStringId.GrandTotal: return "Gran Total";
                case PivotStringId.Values: return "Valores";
                case PivotStringId.TotalP0: return "Total {0}";
                case PivotStringId.Product: return "Producto";
                case PivotStringId.StdDevP: return "StdDevP";
                case PivotStringId.Min: return "Minimo";
                case PivotStringId.Count: return "Contar";
                case PivotStringId.StdDev: return "StdDev";
                case PivotStringId.Sum: return "Suma";
                case PivotStringId.Average: return "Promedio";
                case PivotStringId.Var: return "Var";
                case PivotStringId.VarP: return "VarP";
                case PivotStringId.GroupP0AggregateP1: return "{0} {1}";
                case PivotStringId.YearGroupField: return "Año";
                case PivotStringId.MonthGroupField: return "Mes";
                case PivotStringId.QuarterGroupField: return "Trimestre";
                case PivotStringId.WeekGroupField: return "Semana";
                case PivotStringId.DayGroupField: return "Día";
                case PivotStringId.HourGroupField: return "Hora";
                case PivotStringId.MinuteGroupField: return "Minute";
                case PivotStringId.SecondsGroupField: return "Segundos";
                case PivotStringId.P0Total: return "{0} Total";
                case PivotStringId.PivotAggregateP0ofP1: return "{0} de {1}";
                case PivotStringId.ExpandCollapseMenuItem: return "Expandir";
                case PivotStringId.CollapseAllMenuItems: return "Contraer Todos";
                case PivotStringId.ExpandAllMenuItems: return "Expandir Todos";
                case PivotStringId.CopyMenuItem: return "Copiar";
                case PivotStringId.HideMenuItem: return "Ocultar";
                case PivotStringId.SortMenuItem: return "Ordenar";
                case PivotStringId.BestFitMenuItem: return "Mejor Forma";
                case PivotStringId.ReloadDataMenuItem: return "Actualizar Datos";
                case PivotStringId.FieldListMenuItem: return "Mostrar Lista de campos";
                case PivotStringId.SortAZMenuItem: return "Ordenar A-Z";
                case PivotStringId.SortZAMenuItem: return "Ordenar Z-A";
                case PivotStringId.SortOptionsMenuItem: return "Más opciones Ordenar ...";
                case PivotStringId.ClearFilterMenuItem: return "Borrar filtro";
                case PivotStringId.LabelFilterMenuItem: return "Etiqueta Filtro";

                case PivotStringId.ValueFilterMenuItem: return "Valor de Filtro";
                case PivotStringId.AllNode: return "(Seleccionar Todo)";
                case PivotStringId.FilterMenuItemEqual: return "Equivale...";
                case PivotStringId.FilterMenuItemDoesNotEquals: return "No Es Igual...";
                case PivotStringId.FilterMenuItemBeginsWith: return "Empieza Con...";
                case PivotStringId.FilterMenuItemDoesNotBeginWith: return "No comienza con...";
                case PivotStringId.FIlterMenuItemEndsWith: return "termina Con...";
                case PivotStringId.FilterMenuItemDoesNotEndsWith: return "No termina c on...";
                case PivotStringId.FilterMenuItemContains: return "Contiene...";
                case PivotStringId.FilterMenuItemDoesNotContain: return "No Contiene...";
                case PivotStringId.FilterMenuItemGreaterThan: return "Mas Grande Que...";
                case PivotStringId.FilterMenuItemGreaterThanOrEqualTo: return "Mayor Qué O Igual A...";
                case PivotStringId.FilterMenuItemLessThan: return "Menos Que...";
                case PivotStringId.FilterMenuItemLessThanOrEqualTo: return "Menos Que O Igual A...";
                case PivotStringId.FilterMenuItemBetween: return "entre...";
                case PivotStringId.FilterMenuItemNotBetween: return "No está entre ...";
                case PivotStringId.TopTenItem: return " últimos 10...";
                case PivotStringId.AllNodeSelectAllSearchResult: return "(Seleccionar todo Resultados de la búsqueda)";
                case PivotStringId.FilterMenuAvailableFilters: return " Filtros Disponibles";
                case PivotStringId.CheckBoxMenuItem: return "Seleccionar varios elementos";

                case PivotStringId.CalculationOptionsDialogNoCalculation: return "No hay cálculos";
                case PivotStringId.CalculationOptionsDialogPrevious: return "(anterior)";
                case PivotStringId.CalculationOptionsDialogNext: return "(siguiente)";
                case PivotStringId.CalculationOptionsDialogGrandTotal: return "% del Gran Total";
                case PivotStringId.CalculationOptionsDialogColumnTotal: return "% del total de la columna";
                case PivotStringId.CalculationOptionsDialogRowTotal: return "% de la fila total";
                case PivotStringId.CalculationOptionsDialogOf: return "% de";
                case PivotStringId.CalculationOptionsDialogDifferenceFrom: return "Diferencia De";
                case PivotStringId.CalculationOptionsDialogPercentDifferenceFrom: return "% Diferencia De";
                case PivotStringId.CalculationOptionsDialogRunningTotalIn: return "Total intermedio En";
                case PivotStringId.CalculationOptionsDialogPercentRunningTotalIn: return "% Total intermedio En";
                case PivotStringId.CalculationOptionsDialogRankSmallestToLargest: return "Posición menor a mayor";
                case PivotStringId.CalculationOptionsDialogRankLargestToSmallest: return "Posición de mayor a menor";
                case PivotStringId.CalculationOptionsDialogIndex: return "índice";
                case PivotStringId.CalculationOptionsDialogShowValueAs: return "Mostrar valor como ({0})";

                case PivotStringId.LabelFilterOptionsDialogEquals: return "es igual a";
                case PivotStringId.LabelFilterOptionsDialogDoesNotEqual: return "no es igual";
                case PivotStringId.LabelFilterOptionsDialogIsGreaterThen: return "es mayor que";
                case PivotStringId.LabelFilterOptionsDialogIsGreaterThanOrEqualTo: return "es mayor que o igual a";
                case PivotStringId.LabelFilterOptionsDialogIsLessThan: return "es menos que";
                case PivotStringId.LabelFilterOptionsDialogIsLessThanOrEqualTo: return "es menor que o igual a";
                case PivotStringId.LabelFilterOptionsDialogBeginsWith: return "Empezando con";
                case PivotStringId.LabelFilterOptionsDialogDoesNotBeginWith: return "no comienza con";
                case PivotStringId.LabelFilterOptionsDialogEndsWith: return "termina con";
                case PivotStringId.LabelFilterOptionsDialogDoesNotEndsWith: return "no termina con";
                case PivotStringId.LabelFilterOptionsDialogContains: return "contiene";
                case PivotStringId.LabelFilterOptionsDialogDoesNotContain: return "no contiene";
                case PivotStringId.LabelFilterOptionsDialogIsBetween: return "está entre";
                case PivotStringId.LabelFilterOptionsDialogIsNotBetween: return "No está entre";
                case PivotStringId.LabelFilterOptionsDialogLabelFilter: return "Etiqueta Filtro ({0})";

                case PivotStringId.NumberFormatOptionsDialogCustomFormat: return "formato personalizado";
                case PivotStringId.NumberFormatOptionsDialogFixedPoint: return "De punto fijo con 2 dígitos decimales";
                case PivotStringId.NumberFormatOptionsDialogPrefixedCurrency: return "$ moneda fija post con 2 dígitos decimales";
                case PivotStringId.NumberFormatOptionsDialogPostfixedCurrency: return "€ moneda fija post con 2 dígitos decimales";
                case PivotStringId.NumberFormatOptionsDialogPostfixedTemperatureC: return "°C temperatura fija post con 2 dígitos decimales";
                case PivotStringId.NumberFormatOptionsDialogPostfixedTemperatureF: return "°F temperatura fija post con 2 dígitos decimales";
                case PivotStringId.NumberFormatOptionsDialogExponential: return "Exponential (scientific)";
                case PivotStringId.NumberFormatOptionsDialogFormatOptions: return "Opciones de formato";
                case PivotStringId.NumberFormatOptionsDialogFormatOptionsDescription: return "Opciones de formato ({0})";

                case PivotStringId.SortOptionsDialogSortOptions: return "Opciones de ordenación ({0})";
                case PivotStringId.Top10FilterOptionsDialogTop: return "Top";
                case PivotStringId.Top10FilterOptionsDialogBottom: return "último";
                case PivotStringId.Top10FilterOptionsDialogItems: return "Items";
                case PivotStringId.Top10FilterOptionsDialogPercent: return "por ciento";
                case PivotStringId.Top10FilterOptionsDialogTop10: return "Top 10 Filtro({0})";
                case PivotStringId.ValueFilter: return "Valor de Filtro ({0})";

                case PivotStringId.AggregateOptionsDialogGroupBoxText: return "Resumir Valores Por";
                case PivotStringId.AggregateOptionsDialogLabelCustomName: return "Nombre personalizado:";
                case PivotStringId.AggregateOptionsDialogLabelDescription: return "Elija el tipo de calculation that you want to use to summarize data from the selected field.";
                case PivotStringId.AggregateOptionsDialogLabelField: return "Codigo de la Etiqueta";
                case PivotStringId.AggregateOptionsDialogLabelSourceName: return "Nombre de la fuente:";
                case PivotStringId.AggregateOptionsDialogText: return "AggregateOptionsDialog";

                case PivotStringId.DialogButtonCancel: return "cancelar";
                case PivotStringId.DialogButtonOK: return "OK";

                case PivotStringId.CalculationOptionsDialogText: return "Cálculo de diálogo Opciones";
                case PivotStringId.CalculationOptionsDialogLabelBaseItem: return "Punto Base:";
                case PivotStringId.CalculationOptionsDialogLabelBaseField: return "campo Base:";
                case PivotStringId.CalculationOptionsDialogGroupBoxText: return "Mostrar valor Como";

                case PivotStringId.LabelFilterOptionsDialogGroupBoxText: return "Mostrar elementos para los que la etiqueta";
                case PivotStringId.LabelFilterOptionsDialogText: return "Etiqueta Filtro de diálogo Opciones";
                case PivotStringId.LabelFilterOptionsDialogLabelAnd: return "y";

                case PivotStringId.NumberFormatOptionsDialogFormat: return "Formato";
                case PivotStringId.NumberFormatOptionsDialogLabelDescription: return "El formato debe identificar el tipo de medición del valor. ($, ¥, €, kg., lb.," +
    " m.) El formato sería utilizado para los cálculos generales, tales como Suma , Promedio, Mínimo" +
    ", Máximo entre otros.";
                case PivotStringId.NumberFormatOptionsDialogText: return "Formato de número cuadro de diálogo Opciones";
                case PivotStringId.NumberFormatOptionsDialogGroupBoxText: return "General Format";

                case PivotStringId.SortOptionsDialogAscending: return "Orden ascendente (A-Z) por:";
                case PivotStringId.SortOptionsDialogDescending: return "Orden Descendiente (Z-A) por:";
                case PivotStringId.SortOptionsDialogGroupBoxText: return "opciones Ordenar";
                case PivotStringId.SortOptionsDialogText: return "Ordenar cuadro de diálogo Opciones";

                case PivotStringId.Top10FilterOptionsDialogGroupBoxText: return "mostrar";
                case PivotStringId.Top10FilterOptionsDialogLabelBy: return "por";
                case PivotStringId.Top10FilterOptionsDialogText: return "Top 10 Filtro de diálogo Opciones";

                case PivotStringId.ValueFilterOptionsDialogGroupBox: return "Mostrar partidas para las que";

                case PivotStringId.ValueFilterOptionsDialogText: return "ValueFilterOptionsDialog";
                case PivotStringId.DragDataItemsHere: return "Elementos de datos Arrastre aquí";
                case PivotStringId.DragColumnItemsHere: return "Elementos columna Arrastre aquí";
                case PivotStringId.DragItemsHere: return "Arrastre los elementos aquí";
                case PivotStringId.DragFilterItemsHere: return "Elementos de filtro Arrastre aquí";
                case PivotStringId.DragRowItemsHere: return "Artículos fila Arrastrar aquí";
                case PivotStringId.ResultItemFormat: return "Key: {0}; Agregados: {1}";
                case PivotStringId.Error: return "Error";
                case PivotStringId.KpiSchemaElementValidatorError: return "Debe tener al menos un miembro de KPI define ( Gol, Estado , Treand , Valor )";
                case PivotStringId.SchemaElementValidatorMissingPropertyFormat: return "Propiedad requerida no se encuentra: {0}";
                case PivotStringId.AdomdCellInfoToStringFormat: return "Ordinal: {0} | Valor: {1}";
                case PivotStringId.Aggregates: return "Agregados";
                case PivotStringId.FilterMenuTextBoxItemNullText: return "búsqueda...";
                case PivotStringId.FieldChooserFormButtonAdd: return "añadir";
                case PivotStringId.FieldChooserFormFields: return "campos:";
                case PivotStringId.FieldChooserFormText: return "Selector de campos";

                case PivotStringId.FieldChooserFormColumnArea: return "Área columna";
                case PivotStringId.FieldChooserFormDataArea: return "Área de datos";
                case PivotStringId.FieldChooserFormFilterArea: return "Área Filtro";
                case PivotStringId.FieldChooserFormRowArea: return "Área Fila";

                case PivotStringId.FieldListlabelChooseFields: return "Elija los campos:";
                case PivotStringId.FieldListButtonUpdate: return "actualización";
                case PivotStringId.FieldListCheckBoxDeferUpdate: return "Aplazar Disposición Actualización";
                case PivotStringId.FieldListLabelDrag: return "Arrastre los campos entre las áreas siguientes:";

                case PivotStringId.FieldListLabelRowLabels: return "Etiquetas De Fila";
                case PivotStringId.FieldListLabelColumnLabels: return "Etiquetas de columna";
                case PivotStringId.FieldListLabelReportFilter: return "Filtro de informe";

                case PivotStringId.None: return "ninguno";
                case PivotStringId.PrintSettingsFitWidth: return "ajuste ancho";
                case PivotStringId.PrintSettingsFitHeight: return "Ajustar la altura";
                case PivotStringId.PrintSettingsCompact: return "compacto";
                case PivotStringId.PrintSettingsTabular: return "Tabular";
                case PivotStringId.PrintSettingsFitAll: return "Montar todo";

                case PivotStringId.PrintSettingsPrintOrder: return "Print order";
                case PivotStringId.PrintSettingsThenOver: return "Abajo , a continuación, sobre";
                case PivotStringId.PrintSettingsThenDown: return "Abajo, luego hacia abajo";
                case PivotStringId.PrintSettingsFontsAndColors: return "Fuentes y colores";
                case PivotStringId.PrintSettingsBackground: return "fondo";
                case PivotStringId.PrintSettingsNone: return "(ninguno)";
                case PivotStringId.PrintSettingsFont: return "fuente";
                case PivotStringId.PrintSettingsGrantTotal: return "Total de Celdas:";
                case PivotStringId.PrintSettingsDescriptors: return "Descriptores de agregado / grupo:";
                case PivotStringId.PrintSettingsSubTotal: return "Sub Total de Celdas:";
                case PivotStringId.PrintSettingsHeaderCells: return "Column/row header cells:";
                case PivotStringId.PrintSettingsDataCells: return "datos de celda:";
                case PivotStringId.PrintSettingsGridLinesColor: return "Grid lines color:";
                case PivotStringId.PrintSettingsSettings: return "Settings";
                case PivotStringId.PrintSettingsLayuotType: return "Tipo diseño:";
                case PivotStringId.PrintSettingsScaleMode: return "modo Escala:";
                case PivotStringId.PrintSettingsPrintSelectionOnly: return "Sólo selección Imprimir";
                case PivotStringId.PrintSettingsShowGridLines: return "Mostrar líneas de cuadrícula";
                case PivotStringId.CollapseMenuItem: return "colapsar";
                case PivotStringId.CalcualtedFields: return "Los campos calculados";
                case PivotStringId.Max: return "Maximo";
                case PivotStringId.NullValue: return "(blanco)";
                default: return base.GetLocalizedString(id);
            }
        }
    }
}
