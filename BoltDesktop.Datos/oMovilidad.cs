using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class oMovilidad
    {
        public string codigoMovimiento { get; set; }
        public string placa { get; set; }
        public string pseudonombre { get; set; }
        public string nroRUC { get; set; }
        public string razonSocial { get; set; }
        public string tipoMovilidad { get; set; }
        public string choferDNI { get; set; }
        public string chofer { get; set; }
        public int numeroAsientos { get; set; }
        public int esIdaVuelta { get; set; }
        public int esIda { get; set; }
        public int esVuelta { get; set; }
        public int numeroPasajeros { get; set; }
        public string rutaOrigen { get; set; }
        public string rutaDestino { get; set; }

    }
}
