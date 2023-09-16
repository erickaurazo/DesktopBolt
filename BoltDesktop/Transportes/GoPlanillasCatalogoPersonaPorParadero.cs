using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asistencia.Datos;

namespace Asistencia
{
    public partial class GoPlanillasCatalogoPersonaPorParadero : Form
    {
        private string _companyId;
        private string _conection;
        private ASJ_USUARIOS _user;

        public GoPlanillasCatalogoPersonaPorParadero()
        {
            InitializeComponent();
        }

        public GoPlanillasCatalogoPersonaPorParadero(string conection, ASJ_USUARIOS user, string companyId)
        {
            InitializeComponent();
            _conection = conection;
            _user = user;
            _companyId = companyId;
        }

        private void CatalogoPersonaByParadero_Load(object sender, EventArgs e)
        {

        }
    }
}
