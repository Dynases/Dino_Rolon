using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTER.reg
{
    public partial class F1_TipoAlmacen : MODEL.ModeloF1
    {
        public F1_TipoAlmacen()
        {
            InitializeComponent();
            this.MP_Inhabilitar();
        }

        #region Variables de instancia
        #endregion

        #region Metodos Privados        

        private void MP_Inhabilitar()
        {
            Tb_Descripcion.ReadOnly = true;
            Tb_TipoAlmacen.ReadOnly = true;
        }

        #endregion

        #region Metodos Heredados
        #endregion

        #region Eventos

        private void F1_TipoAlmacen_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TIPOS DE ALMACEN";
        }

        #endregion                
    }
}
