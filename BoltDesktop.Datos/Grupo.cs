using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asistencia.Datos
{
    public class Grupo
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public string Id { get; set; }

        public int Code { get; set; }

        public string userBydb { get; set; }
        public string passwordBydb { get; set; }

        public string isntancePublic { get; set; }
        public string isntanceLocal { get; set; }

        public DateTime fecha { get; set; }
    }
}
