using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class oFechaAsistencia
    {
        public DateTime? fecha { get; set; }

        public int anio { get; set; }
        public string numeroMes { get; set; }
        public int numeroSemana { get; set; }
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }

    }
}
