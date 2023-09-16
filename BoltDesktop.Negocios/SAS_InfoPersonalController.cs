using Asistencia.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Asistencia.Negocios
{
    public class SAS_InfoPersonalController
    {

        public SAS_InfoPersonal GetInfoById(string conection, string id)
        {
            SAS_InfoPersonal item = new SAS_InfoPersonal();
            item.cargo = string.Empty;
            item.nrodocumento = string.Empty;
            string cnx = ConfigurationManager.AppSettings[conection].ToString();
            using (AgroSaturnoDataContext Modelo = new AgroSaturnoDataContext(cnx))
            {
                var result = Modelo.SAS_InfoPersonal.Where(x => x.idcodigogeneral.Trim().ToUpper() == id).ToList();
                if (result != null)
                {
                    if (result.ToList().Count == 1)
                    {
                        item = result.Single();
                    }
                }

            }
                return item;
        }

    }
}
