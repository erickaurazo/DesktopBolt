using System.Collections.Generic;
using Asistencia.Datos;


namespace Asistencia.Negocios
{
    public class TipoMovilidadController
    {
        private TipoMovilidad tipo;

        public List<TipoMovilidad> ListarTiposdeMovilidad()
        {
            #region 
            List<TipoMovilidad> ListarTiposdeMovilidad = new List<TipoMovilidad>();
            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "000";
            tipo.TipoMovil = "SELECIONAR VALOR";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "001";
            tipo.TipoMovil = "COASTER";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "002";
            tipo.TipoMovil = "BUS";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "003";
            tipo.TipoMovil = "COMBI";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "004";
            tipo.TipoMovil = "MINIBUS";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "005";
            tipo.TipoMovil = "MINIVAN";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "006";
            tipo.TipoMovil = "AUTO";
            ListarTiposdeMovilidad.Add(tipo);

            tipo = new TipoMovilidad();
            tipo.IdTipoMovilidad = "007";
            tipo.TipoMovil = "CAMINIONETA";
            ListarTiposdeMovilidad.Add(tipo);

            #endregion
            return ListarTiposdeMovilidad;
        }

    }
}
