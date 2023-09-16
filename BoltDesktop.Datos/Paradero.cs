using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class Paradero
    {


        public string idParadero { get; set; }
        public string descripcionParadero { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public string estadoDescripcion { get; set; }
        public string tipo { get; set; }
        public string tipoDescripcion { get; set; }
        public int numeroPasajeros { get; set; }
        public int EsSelecionado { get; set; }


    }
}
