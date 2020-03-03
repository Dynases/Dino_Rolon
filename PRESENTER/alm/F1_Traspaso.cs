using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Traspaso : MODEL.ModeloF1
    {
        public F1_Traspaso()
        {
            InitializeComponent();
            this.MP_InHabilitar();
        }

        //===============
        #region Variables de entorno



        #endregion

        //===============
        #region Metodos Privados      

        private void MP_InHabilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = true;
            this.Tb_UsuarioRecibe.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;

            //Cb_Destino.Enabled = false;
            //Cb_Origen.Enabled = false;

            this.lblId.Visible = false;
        }

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarDepositos()
        {
            try
            {
                UTGlobal.MG_ArmarComboSucursal(Cb_Destino,
                                                new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());

                UTGlobal.MG_ArmarComboSucursal(Cb_Origen,
                                                new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion

        //===============
        #region Metodos Heredados

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
