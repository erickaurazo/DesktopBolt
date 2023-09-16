using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class AsistenciaBusByDia
    {
        public DateTime fecha { get; set; }
        public string ruta { get; set; }
        public string empresaTransporte { get; set; }
        public string placa { get; set; }
        public decimal capacidad { get; set; }
        public int Bota { get; set; }
        public int Tablazo { get; set; }
        public int Balsa { get; set; }
        public int SantaMaria  { get; set; }
        public int IMP { get; set; }
        public int totalAsistencia { get; set; }

    }
}
