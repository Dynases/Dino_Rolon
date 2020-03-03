using DevComponents.DotNetBar;
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
    public partial class F1_Traspaso : MODEL.ModeloF1
    {
        public F1_Traspaso()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarSucursales();
        }

        //===============
        #region Variables de entorno

        public static List<Producto> detalleProductos;

        #endregion

        //===============
        #region Metodos Privados      

        private void MP_InHabilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = true;
            this.Tb_UsuarioRecibe.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;

            Cb_Destino.Enabled = false;
            Cb_Origen.Enabled = false;

            Dgv_DetalleTraspaso.Visible = true;
            this.panelDerecha.Visible = false;
            this.panelIzquierda.Visible = false;

            GPanel_Detalles.Text = "DETALLE DE TRASPASO";

            this.lblId.Visible = false;

        }

        private void MP_Habilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = false;
            this.Tb_UsuarioRecibe.ReadOnly = false;
            this.Tb_Observaciones.ReadOnly = false;

            Cb_Destino.Enabled = true;
            Cb_Origen.Enabled = true;

            Dgv_DetalleTraspaso.Visible = false;
            this.panelDerecha.Visible = true;
            this.panelIzquierda.Visible = true;

            GPanel_Detalles.Text = "SELECCIONE LOS PRODUCTOS PARA EL TRASPASO";

            this.lblId.Visible = false;
        }

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_CargarSucursales()
        {
            try
            {
                var sucursales = new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList();
                UTGlobal.MG_ArmarComboSucursal(Cb_Destino, sucursales);
                UTGlobal.MG_ArmarComboSucursal(Cb_Origen, sucursales);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Reiniciar()
        {
            Dgv_ProductosInventario.DataSource = null;
            Tb_Observaciones.Text = "";
            Tb_UsuarioEnvio.Text = "";
            Tb_UsuarioRecibe.Text = "";

            this.MP_InHabilitar();
        }

        private void MP_CargarProductos()
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().ToList();
                Dgv_ProductosInventario.DataSource = result;

                if (result.Count > 0)
                {
                    Dgv_ProductosInventario.RetrieveStructure();
                    Dgv_ProductosInventario.AlternatingColors = true;

                    Dgv_ProductosInventario.RootTable.Columns[0].Key = "id";
                    Dgv_ProductosInventario.RootTable.Columns[0].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[1].Key = "CodProducto";
                    Dgv_ProductosInventario.RootTable.Columns[1].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[2].Key = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[2].Caption = "Descripcion";
                    Dgv_ProductosInventario.RootTable.Columns[2].Width = 150;
                    Dgv_ProductosInventario.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[2].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[2].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[3].Key = "División";
                    Dgv_ProductosInventario.RootTable.Columns[3].Caption = "División";
                    Dgv_ProductosInventario.RootTable.Columns[3].Width = 150;
                    Dgv_ProductosInventario.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[3].Visible = false;


                    Dgv_ProductosInventario.RootTable.Columns[4].Key = "Marca/Tipo";
                    Dgv_ProductosInventario.RootTable.Columns[4].Caption = "Marca/Tipo";
                    Dgv_ProductosInventario.RootTable.Columns[4].Width = 150;
                    Dgv_ProductosInventario.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[4].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[5].Key = "Categorías/Tipo";
                    Dgv_ProductosInventario.RootTable.Columns[5].Caption = "Categoría";
                    Dgv_ProductosInventario.RootTable.Columns[5].Width = 150;
                    Dgv_ProductosInventario.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.FontSize = 8;
                    Dgv_ProductosInventario.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_ProductosInventario.RootTable.Columns[5].Visible = true;

                    Dgv_ProductosInventario.RootTable.Columns[6].Key = "Usuario";
                    Dgv_ProductosInventario.RootTable.Columns[6].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[7].Key = "Hora";
                    Dgv_ProductosInventario.RootTable.Columns[7].Visible = false;

                    Dgv_ProductosInventario.RootTable.Columns[8].Key = "Fecha";
                    Dgv_ProductosInventario.RootTable.Columns[8].Visible = false;

                    //Habilitar filtradores
                    Dgv_ProductosInventario.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_ProductosInventario.FilterMode = FilterMode.Automatic;
                    Dgv_ProductosInventario.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    Dgv_ProductosInventario.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_ProductosInventario.GroupByBoxVisible = false;
                    Dgv_ProductosInventario.VisualStyle = VisualStyle.Office2007;
                }

                detalleProductos = new List<Producto>();                                                
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        #endregion

        //===============
        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_CargarProductos();
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_Reiniciar();
        }

        #endregion

        //===============
        #region Eventos

        private void F1_Traspaso_Load(object sender, EventArgs e)
        {
            LblTitulo.Text = "TRASPASOS";
        }

        private void Dgv_ProductosInventario_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }

        private void Dgv_ProductosInventario_Click(object sender, EventArgs e)
        {
            var row = Dgv_ProductosInventario.CurrentRow;

            detalleProductos.Add(new Producto
            {
                Id = row.Cells[0].Value.ToString(),
                Descripcion = row.Cells[2].Value.ToString(),
                Campo1 = ""
            });

            Dgv_DetalleNuevo.DataSource = detalleProductos;
            Dgv_DetalleNuevo.RetrieveStructure();
            Dgv_DetalleNuevo.AlternatingColors = true;

            Dgv_DetalleNuevo.RootTable.Columns[0].Key = "id";
            Dgv_DetalleNuevo.RootTable.Columns[0].Visible = false;

            Dgv_DetalleNuevo.RootTable.Columns[1].Key = "Descripcion";
            Dgv_DetalleNuevo.RootTable.Columns[1].Caption = "Descripcion";
            Dgv_DetalleNuevo.RootTable.Columns[1].Width = 150;
            Dgv_DetalleNuevo.RootTable.Columns[1].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.FontSize = 8;
            Dgv_DetalleNuevo.RootTable.Columns[1].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleNuevo.RootTable.Columns[1].Visible = true;

            Dgv_DetalleNuevo.RootTable.Columns[2].Key = "Campo1";
            Dgv_DetalleNuevo.RootTable.Columns[2].Caption = "Cantidad";
            Dgv_DetalleNuevo.RootTable.Columns[2].Width = 150;
            Dgv_DetalleNuevo.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.FontSize = 8;
            Dgv_DetalleNuevo.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleNuevo.RootTable.Columns[2].Visible = true;

            //Habilitar filtradores
            Dgv_DetalleNuevo.DefaultFilterRowComparison = FilterConditionOperator.Contains;
            //Dgv_DetalleNuevo.FilterMode = FilterMode.Automatic;
            //Dgv_DetalleNuevo.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            //Dgv_DetalleNuevo.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
            Dgv_DetalleNuevo.GroupByBoxVisible = false;
            Dgv_DetalleNuevo.VisualStyle = VisualStyle.Office2007;
        }

        #endregion        

        public class Producto
        {
            public string Id { get; set; }
            public string Descripcion { get; set; }
            public string Campo1 { get; set; }
        }
    }
}

