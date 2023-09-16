using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Asistencia.Datos;
using System.Net;

namespace Asistencia.Negocios
{
    public class LoginController
    {
        //private Boolean Validado;
        //private ASJ_USUARIOS oSJUsuario;
        //private List<Grupo> listadoBaseDatos;
        //private List<Grupo> listadoEmpresa;
        //private Grupo Empresa;
        //private Grupo oBaseDatos;

        public ASJ_USUARIOS CheckStatusSession(string userName, string password, string companyId, string conection)
        {            
            string cnx = string.Empty;
            ASJ_USUARIOS user = new ASJ_USUARIOS();
            user = new ASJ_USUARIOS { IdUsuario = string.Empty, Password = string.Empty, nivel = "1", idestado = "0", EmpresaID = companyId.Trim() };
            cnx = ConfigurationManager.AppSettings[conection];
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                var result = Modelo.ASJ_USUARIOS.Where(x => x.IdUsuario == userName && x.EmpresaID.Trim() == companyId.Trim()).ToList();
                if (result != null && result.Count == 1)
                {
                    user = new ASJ_USUARIOS();
                    user = result.Single() ;
                }
            }

            return user;
        }


        public SAS_USUARIOS VerificarStatusDeInicioSesion(string userName, string password, string companyId, string conection)
        {
            string cnx = string.Empty;
            SAS_USUARIOS user = new SAS_USUARIOS();
            user = new SAS_USUARIOS { IdUsuario = string.Empty, Password = string.Empty, nivel = "1", idestado = "0", EmpresaID = companyId.Trim() };
            cnx = ConfigurationManager.AppSettings["SAS"];
           using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_USUARIOS.Where(x => x.IdUsuario == userName && x.EmpresaID.Trim() == companyId.Trim()).ToList();
                if (result != null && result.Count == 1)
                {
                    user = new SAS_USUARIOS();
                    user = result.Single();
                }
            }

            return user;
        }

        public ASJ_USUARIOS GetUserById(string userName, string password, string companyId, string conection)
        {
            string cnx = string.Empty;
            var user = new ASJ_USUARIOS();
            user = new ASJ_USUARIOS { IdUsuario = "", Password = "", nivel = "1", idestado = "0", EmpresaID = companyId.Trim() };

            cnx = ConfigurationManager.AppSettings[conection];
            using (BDAsistenciaDataContext Modelo = new BDAsistenciaDataContext(cnx))
            {
                var result = Modelo.ASJ_USUARIOS.Where(x => x.IdUsuario == userName && x.Password == password && x.idestado == "1" && x.EmpresaID.Trim() == companyId.Trim()).ToList();
                if (result != null && result.Count == 1)
                {
                    user = new ASJ_USUARIOS();
                    user = result.Single();
                }
            }

            return user;
        }

        public List<Grupo> GetDatabases()
        {
            bool resultadoConexionInternet = false;
            Grupo oBaseDatos;
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    resultadoConexionInternet = true;
                }
            }
            catch
            {
                resultadoConexionInternet = false; ;
            }

            List<Grupo> listadoBaseDatos = new List<Grupo>();
            if (resultadoConexionInternet == true)
            {
                oBaseDatos = new Grupo() { Id = "10", Descripcion = "AGRICOLA2017 | Publica".ToUpper() };
                listadoBaseDatos.Add(oBaseDatos);
            }

            //listadoBaseDatos = new List<Grupo>();
            oBaseDatos = new Grupo() { Id = "01", Descripcion = "AGRICOLA2017 | Local".ToUpper() };
            listadoBaseDatos.Add(oBaseDatos);

            return listadoBaseDatos;
        }

        public List<Grupo> ObtenerBasesDatos()
        {
            bool resultadoConexionInternet = false;
            Grupo oBaseDatos;
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://www.google.com"))
                {
                    resultadoConexionInternet = true;
                }
            }
            catch
            {
                resultadoConexionInternet = false; ;
            }

            List<Grupo> listadoBaseDatos = new List<Grupo>();
            if (resultadoConexionInternet == true)
            {
                oBaseDatos = new Grupo() { Id = "10", Descripcion = "SATURNO | Publica".ToUpper() };
                listadoBaseDatos.Add(oBaseDatos);
            }

            //listadoBaseDatos = new List<Grupo>();
            oBaseDatos = new Grupo() { Id = "01", Descripcion = "SATURNO_TEST | Local".ToUpper() };
            listadoBaseDatos.Add(oBaseDatos);

            return listadoBaseDatos;
        }
    }
}
