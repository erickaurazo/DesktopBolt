using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class TablaComparativaHorasVSNISIRA
    {

        public string periodo { get; set; }
        public Int32? semana { get; set; }
        public string idMaquinaria { get; set; }
        public string maquinaria { get; set; }
        public decimal? horasVisualSAT { get; set; }
        public decimal? horasParteMaquinariaNISIRA { get; set; }
        public decimal? horasDiferencia { get; set; }
        public decimal? cantidadCombustible { get; set; }
        public decimal? galonHoraPorParteMaquinaria { get; set; }
        public decimal? galonHoraPorRegistroVisualSAT { get; set; }

    }
}
