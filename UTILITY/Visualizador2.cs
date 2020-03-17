using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTILITY
{
    public partial class Visualizador2 : Form
    {

        public static DateTime Inicio;
        public static DateTime Fin;
        public static int IdAlmacen;

        public Visualizador2(int reporte)
        {
            InitializeComponent();

            switch (reporte)
            {
                case 1:
                    this.MostrarReporteKardexGeneral();
                    break;
                default:
                    break;
            }

        }

        private void Visualizador2_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        #region Metodos

        public void MostrarReporteKardexGeneral()
        {

        }

        #endregion

    }
}
