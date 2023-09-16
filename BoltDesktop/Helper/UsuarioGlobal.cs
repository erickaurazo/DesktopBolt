using Asistencia.Datos;
using System.IO;

namespace Asistencia
{
    static class UsuarioGlobal
    {
        public static string gbUsuario;
        public static string gbNombre;
        public static string gbPerfil;
        public static ASJ_USUARIOS sjUsuario;

        public static void Inicializar()
        {
            gbUsuario = string.Empty;
            gbPerfil = string.Empty;
            gbNombre = string.Empty;
            sjUsuario = new ASJ_USUARIOS();
            sjUsuario.IdUsuario = string.Empty;
            sjUsuario.NombreCompleto = string.Empty;
        }

       

    }
}
