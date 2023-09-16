using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos.MRP
{
    public class PlanCosechaAgrupado
    {




        public int idplan { get; set; }
        public int? mes { get; set; }
        public string tipocultivo { get; set; }
        public string variedad { get; set; }
        public string cultivo { get; set; }
        public string idconsumidor { get; set; }
        public string consumidor { get; set; }
        public decimal? cantidad { get; set; }
        public string sector { get; set; }
        public string periodoSemana { get; set; }
        public decimal? pesoPromedioJabaPorVariedad { get; set; }
        public decimal? numeroJabas { get; set; }
        public decimal? contenedores { get; set; }

    }
}
