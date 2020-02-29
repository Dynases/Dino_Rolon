using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTER.alm
{
    public partial class F1_Traspaso : MODEL.ModeloF1
    {
        public F1_Traspaso()
        {
            InitializeComponent();
        }

        //===============
        #region Variables de entorno



        #endregion

        //===============
        #region Metodos

        //===============
        #region Metodos Heredados
        #endregion

        #endregion


        //===============
        #region Eventos

        private void F1_Traspaso_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TRASPASOS";
        }

        #endregion       
    }
}
