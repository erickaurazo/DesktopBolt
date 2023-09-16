using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Asistencia.ClaseTelerik
{
    public class RadPageViewLocalizationProviderEspañol : RadPageViewLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadPageViewStringId.CloseButtonTooltip:
                    return "Cerrar la página seleccionada";
                case RadPageViewStringId.ItemListButtonTooltip:
                    return "Páginas disponibles";
                case RadPageViewStringId.LeftScrollButtonTooltip:
                    return "Desplazar hacia la izquierda";
                case RadPageViewStringId.RightScrollButtonTooltip:
                    return "Desplazar hacia la derecha";
                case RadPageViewStringId.ShowMoreButtonsItemCaption:
                    return "Mostrar más botones";
                case RadPageViewStringId.ShowFewerButtonsItemCaption:
                    return "Mostrar menos botones";
                case RadPageViewStringId.AddRemoveButtonsItemCaption:
                    return "Añadir o eliminar botones";
                case RadPageViewStringId.ItemCloseButtonTooltip:
                    return "Cerrar página";
                case RadPageViewStringId.NewItemTooltipText:
                    return "Añadir nueva página";
                case RadPageViewStringId.CloseSelectedPageCaption:
                    return "Cerrar página seleccionada";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}