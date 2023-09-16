using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace Asistencia.ClaseTelerik
{
    internal class RadWizardLocalizationProviderEspañol : RadWizardLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadWizardStringId.BackButtonText:
                    return "<   Atras";
                case RadWizardStringId.NextButtonText:
                    return "Adelante   >";
                case RadWizardStringId.CancelButtonText:
                    return "Cancelar";
                case RadWizardStringId.FinishButtonText:
                    return "Finalizar";
                case RadWizardStringId.HelpButtonText:
                    return "<html><u>Ayuda</u></html>";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}