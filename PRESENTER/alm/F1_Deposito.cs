using DevComponents.DotNetBar;
using ENTITY.inv.Deposito;
using Janus.Windows.GridEX;
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
    public partial class F1_Deposito : MODEL.ModeloF1
    {
        public F1_Deposito()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarDepositos();
        }

        #region Variables globales        

        private static int index;
        private static List<VDepositoLista> listaDeposito;

        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);

        }

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
            index = 0;
            try
            {
                listaDeposito = new ServiceDesktop.ServiceDesktopClient().DepositoListar().ToList();
                if (listaDeposito != null && listaDeposito.Count >= 0)
                {
                    this.MP_MostrarRegistro(index);
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            var deposito = listaDeposito[index];
            lblId.Text = deposito.Id.ToString();
            Tb_Descrip.Text = deposito.Descripcion;
            Tb_Direcc.Text = deposito.Direccion;
            Tb_Telef.Text = deposito.Telefono;

            this.MP_CargarDetalleRegistro(deposito.Id);
        }

        private void MP_CargarDetalleRegistro(int id)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ListarSucursalXDepositoId(id).ToList();

                if (result.Count > 0)
                {
                    Dgv_Sucursales.DataSource = result;
                    Dgv_Sucursales.RetrieveStructure();
                    Dgv_Sucursales.AlternatingColors = true;

                    Dgv_Sucursales.RootTable.Columns[0].Key = "Id";
                    Dgv_Sucursales.RootTable.Columns[0].Caption = "Id";
                    Dgv_Sucursales.RootTable.Columns[0].Visible = false;

                    Dgv_Sucursales.RootTable.Columns[1].Key = "Descripcion";
                    Dgv_Sucursales.RootTable.Columns[1].Caption = "Sucursal";
                    Dgv_Sucursales.RootTable.Columns[1].Width = 250;
                    Dgv_Sucursales.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[1].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[1].Visible = true;

                    Dgv_Sucursales.RootTable.Columns[2].Key = "Direccion";
                    Dgv_Sucursales.RootTable.Columns[2].Caption = "Direccion";
                    Dgv_Sucursales.RootTable.Columns[2].Width = 250;
                    Dgv_Sucursales.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[2].Visible = true;

                    Dgv_Sucursales.RootTable.Columns[3].Key = "Telefono";
                    Dgv_Sucursales.RootTable.Columns[3].Caption = "Telefono";
                    Dgv_Sucursales.RootTable.Columns[3].Width = 150;
                    Dgv_Sucursales.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[3].Visible = true;

                    Dgv_Sucursales.RootTable.Columns[4].Key = "Deposito";
                    Dgv_Sucursales.RootTable.Columns[4].Caption = "Deposito";
                    Dgv_Sucursales.RootTable.Columns[4].Width = 250;
                    Dgv_Sucursales.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Sucursales.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_Sucursales.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Sucursales.RootTable.Columns[4].Visible = true;

                    //Habilitar filtradores
                    Dgv_Sucursales.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_Sucursales.FilterMode = FilterMode.Automatic;
                    Dgv_Sucursales.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_Sucursales.GroupByBoxVisible = false;
                    Dgv_Sucursales.VisualStyle = VisualStyle.Office2007;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
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
        }

        #endregion
    }
}
