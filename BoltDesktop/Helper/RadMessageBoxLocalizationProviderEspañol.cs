using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls;

namespace Asistencia.ClaseTelerik
{
    public class RadMessageBoxLocalizationProviderEspañol : RadMessageLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadMessageStringID.AbortButton:
                    return "Abortar";
                case RadMessageStringID.CancelButton:
                    return "Cancelar";
                case RadMessageStringID.IgnoreButton:
                    return "Ignorar";
                case RadMessageStringID.NoButton:
                    return "No";
                case RadMessageStringID.OKButton:
                    return "Ok";
                case RadMessageStringID.RetryButton:
                    return "Reintentar";
                case RadMessageStringID.YesButton:
                    return "Sí";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}