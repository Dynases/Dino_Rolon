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
    public partial class F1_Deposito : MODEL.ModeloF1
    {
        public F1_Deposito()
        {
            InitializeComponent();
        }


        #region Variables globales        

        #endregion

        #region Metodos Privados

        #endregion

        #region Metodos Heredados

        #endregion

        #region Eventos

        private void F1_Deposito_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "Depositos";
        }

        #endregion
    }
}
