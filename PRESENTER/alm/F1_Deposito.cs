using ENTITY.inv.Deposito;
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
            this.MP_InHabilitar();
        }

        #region Variables globales        

        private static int index;
        private static List<VDepositoLista> listaDeposito;

        #endregion

        #region Metodos Privados

        private void MP_InHabilitar()
        {
            this.Tb_Descrip.ReadOnly = true;
            this.Tb_Direcc.ReadOnly = true;
            this.Tb_Telef.ReadOnly = true;
            this.lblId.Visible = false;
        }

        private void MP_Habilitar()
        {
            this.Tb_Descrip.ReadOnly = false;
            this.Tb_Direcc.ReadOnly = false;
            this.Tb_Telef.ReadOnly = false;
        }

        private void MP_CargarDepositos()
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
        }

        #endregion

        #region Eventos

        private void F1_Deposito_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "Depositos";
            index = 0;
            listaDeposito = new List<VDepositoLista>();
        }

        #endregion
    }
}
