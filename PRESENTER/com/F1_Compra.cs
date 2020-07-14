using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using UTILITY.Global;
using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using UTILITY.Enum.EnEstado;
using PRESENTER.frm;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Enum;

namespace PRESENTER.com
{
    public partial class F1_Compra : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "COMPRA";
        bool _Limpiar = false;
        int _idOriginal = 0;
        int _MPos = 0;
        int _IdProveedor = 0;
        List<VCompra_01> ListaDetalle = new List<VCompra_01>();
        #endregion

        public F1_Compra()
        {
            //prueba
            InitializeComponent();
            MP_Iniciar();
            superTabControl1.SelectedTabIndex = 0;
        }

        private void F1_Compra_Load(object sender, EventArgs e)
        {
            this.Name = _NombreFormulario;
        }
        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_CargarAlmacenes();
                //btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
                MP_AsignarPermisos();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_AsignarPermisos()
        {
            try
            {
                DataTable dtRolUsu = new ServiceDesktop.ServiceDesktopClient().AsignarPermisos((UTGlobal.UsuarioRol).ToString(), _NombreProg);
                if (dtRolUsu.Rows.Count > 0)
                {
                    bool show = Convert.ToBoolean(dtRolUsu.Rows[0]["Show"]);
                    bool add = Convert.ToBoolean(dtRolUsu.Rows[0]["Add"]);
                    bool modif = Convert.ToBoolean(dtRolUsu.Rows[0]["Mod"]);
                    bool del = Convert.ToBoolean(dtRolUsu.Rows[0]["Del"]);

                    if (add == false)
                        BtnNuevo.Visible = false;
                    if (modif == false)
                        BtnModificar.Visible = false;
                    if (del == false)
                        BtnEliminar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        public void MP_LimpiarColor()
        {
            Tb_Total.BackColor = Color.White;          
            Cb_Almacen.BackColor = Color.White;          
        }
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarEncabezado()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Compra_Lista().ToList();
                Dgv_GBuscador.DataSource = ListaCompleta;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["Id"].Caption = "Id";
                Dgv_GBuscador.RootTable.Columns["Id"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["Id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Id"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["Id"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["IdAlmacen"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["IdProvee"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Caption = "Fecha";
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Width = 220;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["FechaDoc"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["TipoVenta"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Caption = "Tipo";
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["NombreTipo"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Descu"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["Total"].Caption = "Total";
                Dgv_GBuscador.RootTable.Columns["Total"].Width = 250;
                Dgv_GBuscador.RootTable.Columns["Total"].FormatString = "0.00";
                Dgv_GBuscador.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["Total"].Visible = true;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["FechaVen"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Factura"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Recibo"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["TipoFactura"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Observ"].Visible = false;

                //Habilitar filtradores
                Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_GBuscador.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_GBuscador.GroupByBoxVisible = false;
                Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarDetalle(int id)
        {
            try
            {
                ListaDetalle = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista(id).ToList();
                MP_ArmarDetalle();

            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_ArmarDetalle()
        {
            try
            {
                Dgv_Detalle.DataSource = ListaDetalle;
                Dgv_Detalle.RetrieveStructure();
                Dgv_Detalle.AlternatingColors = true;

                Dgv_Detalle.RootTable.Columns["id"].Visible = false;
                Dgv_Detalle.RootTable.Columns["IdCompra"].Visible = false;
                Dgv_Detalle.RootTable.Columns["IdProducto"].Visible = false;
                Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;

                Dgv_Detalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                Dgv_Detalle.RootTable.Columns["Producto"].Width = 200;
                Dgv_Detalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["Producto"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
                Dgv_Detalle.RootTable.Columns["Cantidad"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Cantidad"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Unidad"].Caption = " UN.";
                Dgv_Detalle.RootTable.Columns["Unidad"].Width = 90;
                Dgv_Detalle.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["Unidad"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Costo"].Caption = "PRECIO U.";
                Dgv_Detalle.RootTable.Columns["Costo"].Width = 90;
                Dgv_Detalle.RootTable.Columns["Costo"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Costo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Costo"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Costo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Costo"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Total"].Caption = "SUBTOTAL";
                Dgv_Detalle.RootTable.Columns["Total"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Total"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Total"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Lote"].Caption = "Lote";
                //Dgv_Detalle.RootTable.Columns["Lote"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Lote"].Width = 120;
                Dgv_Detalle.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Lote"].Visible = true;

                Dgv_Detalle.RootTable.Columns["FechaVen"].Caption = "Fecha Ven.";
                //Dgv_Detalle.RootTable.Columns["Lote"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["FechaVen"].Width = 120;
                Dgv_Detalle.RootTable.Columns["FechaVen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["FechaVen"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["FechaVen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["FechaVen"].Visible = true;

                //Dgv_Detalle.RootTable.Columns["Lote"].Visible = false;
                //Dgv_Detalle.RootTable.Columns["FechaVen"].Visible = false;

                Dgv_Detalle.RootTable.Columns["Utilidad"].Caption = "UTILIDAD(%)";
                Dgv_Detalle.RootTable.Columns["Utilidad"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Utilidad"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Utilidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Utilidad"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Utilidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Utilidad"].Visible = true;

                Dgv_Detalle.RootTable.Columns["Porcent"].Caption = "PRECIO VENTA";
                Dgv_Detalle.RootTable.Columns["Porcent"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Porcent"].FormatString = "0.00";
                Dgv_Detalle.RootTable.Columns["Porcent"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Porcent"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Porcent"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["Porcent"].Visible = true;

                Dgv_Detalle.GroupByBoxVisible = false;
                Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
                Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_CargarProducto(List<ENTITY.Producto.View.VProductoLista> result)
        {
            try
            {
                Dgv_Producto.DataSource = result;
                Dgv_Producto.RetrieveStructure();
                Dgv_Producto.AlternatingColors = true;
                Dgv_Producto.RootTable.Columns["id"].Visible = false;

                Dgv_Producto.RootTable.Columns["Codigo"].Caption = "Codigo";
                Dgv_Producto.RootTable.Columns["Codigo"].Width = 100;
                Dgv_Producto.RootTable.Columns["Codigo"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Codigo"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Codigo"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Codigo"].Visible = true;

                Dgv_Producto.RootTable.Columns["Descripcion"].Caption = "Descripcion";
                Dgv_Producto.RootTable.Columns["Descripcion"].Width = 150;
                Dgv_Producto.RootTable.Columns["Descripcion"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Descripcion"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Descripcion"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Descripcion"].Visible = true;

                Dgv_Producto.RootTable.Columns["Grupo1"].Caption = "División";
                Dgv_Producto.RootTable.Columns["Grupo1"].Width = 120;
                Dgv_Producto.RootTable.Columns["Grupo1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Grupo1"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Grupo1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Grupo1"].Visible = true;

                Dgv_Producto.RootTable.Columns["Grupo2"].Caption = "Tipo";
                Dgv_Producto.RootTable.Columns["Grupo2"].Width = 120;
                Dgv_Producto.RootTable.Columns["Grupo2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Grupo2"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Grupo2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Grupo2"].Visible = true;


                Dgv_Producto.RootTable.Columns["Grupo3"].Caption = "CategorIas";
                Dgv_Producto.RootTable.Columns["Grupo3"].Width = 120;
                Dgv_Producto.RootTable.Columns["Grupo3"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Grupo3"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Grupo3"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Grupo3"].Visible = true;

                Dgv_Producto.RootTable.Columns["Tipo"].Visible = false;
                Dgv_Producto.RootTable.Columns["Usuario"].Visible = false;
                Dgv_Producto.RootTable.Columns["Hora"].Visible = false;
                Dgv_Producto.RootTable.Columns["Fecha"].Visible = false;

                //Habilitar filtradores
                Dgv_Producto.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_Producto.FilterMode = FilterMode.Automatic;
                Dgv_Producto.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                Dgv_Producto.GroupByBoxVisible = false;
                Dgv_Producto.VisualStyle = VisualStyle.Office2007;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_InicioArmarCombo()
        {
            UTGlobal.MG_ArmarComboSucursal(Cb_Almacen,
                                              new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());
           
        }
        private void MP_SeleccionarButtonCombo(MultiColumnCombo combo, ButtonX btn)
        {
            try
            {
                if (combo.SelectedIndex < 0 && combo.Text != string.Empty)
                {
                    btn.Visible = true;
                }
                else
                {
                    btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void MP_Habilitar()
        {
            //Tb_Id.ReadOnly = false;
            Tb_Proveedor.ReadOnly = false;
            Tb_Fecha.IsInputReadOnly = false;
            Tb_FechaVenc.IsInputReadOnly = false;
            Tb_Observacion.ReadOnly = false;
            Cb_Almacen.ReadOnly = false;
            Tb_NFactura.ReadOnly = false;
            Sw_Emision.IsReadOnly = false;
            Sw_TipoVenta.IsReadOnly = false;  
            Dgv_Detalle.Enabled = true;
        }
        private void MP_InHabilitar()
        {
            Tb_Id.ReadOnly = true;
            Tb_Proveedor.ReadOnly = true;
            Tb_Fecha.IsInputReadOnly = true;
            Tb_FechaVenc.IsInputReadOnly = true;
            Tb_Observacion.ReadOnly = true;
            Cb_Almacen.ReadOnly = true;
            Tb_NFactura.ReadOnly = true;
            Sw_Emision.IsReadOnly = true;
            Sw_TipoVenta.IsReadOnly = true;
            Dgv_Detalle.Enabled = true;
            _Limpiar = false;
        }
        private void MP_Limpiar()
        {
            try
            {
                Tb_Id.Clear();
                Tb_Proveedor.Clear();
                Tb_Fecha.Value = DateTime.Today;
                Tb_FechaVenc.Value = DateTime.Today;
                Tb_Observacion.Clear();
                Tb_NFactura.Clear();
                Sw_Emision.Value = true;
                Sw_TipoVenta.Value = true;               
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Almacen);
                }
                Dgv_Detalle.DataSource = null;
                ListaDetalle.Clear();
                MP_LimpiarColor();
                MP_AddFila();
                Tb_MDesc.Value = 0;
                Tb_PDesc.Value = 0;
                Tb_Total.Value = 0;
                // ((DataTable)Dgv_Detalle.DataSource).Clear();
                //  Dgv_Detalle.DataSource = null;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_MostrarRegistro(int _Pos)
        {
            try
            {
                Dgv_GBuscador.Row = _Pos;
                _idOriginal = (int)Dgv_GBuscador.GetValue("id");
                if (_idOriginal != 0)
                {
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Compra_Lista().Where(A => A.Id == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();
                    Tb_Id.Text = Lista.Id.ToString();
                    Cb_Almacen.Value = Lista.IdAlmacen;
                    _IdProveedor = Lista.IdProvee;
                    Tb_Proveedor.Text = Lista.Proveedor.ToString();
                    Tb_Fecha.Value = Lista.FechaDoc;
                    Sw_TipoVenta.Value = Lista.TipoVenta == 1 ? true : false;
                    Sw_Emision.Value = Lista.TipoFactura == 1 ? true : false;
                    Tb_NFactura.Text = Lista.Factura;
                    Tb_Recibo.Text = Lista.Recibo;              
                    Tb_Observacion.Text = Lista.Observ;
                    Tb_FechaVenc.Value = Lista.FechaVen;
                    MP_CargarDetalle(Convert.ToInt32(Tb_Id.Text));
                    Tb_MDesc.Value = Convert.ToDouble(Lista.Descu);
                    Tb_Total.Value = Convert.ToDouble(Lista.Total);
                    MP_ObtenerCalculo();

                    LblPaginacion.Text = Convert.ToString(_Pos + 1) + "/" + Dgv_GBuscador.RowCount.ToString();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_Detalle.UpdateData();
                var total= Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
                var totaldesc = total - Tb_MDesc.Value;
                Tb_Total.Value = totaldesc;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_Filtrar(int tipo)
        {
            MP_CargarEncabezado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                _MPos = 0;
                MP_MostrarRegistro(tipo == 1 ? _MPos : Dgv_GBuscador.RowCount - 1);
            }
            else
            {
                MP_Limpiar();
                LblPaginacion.Text = "0/0";
            }
        }
   
        void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        private void MP_AddFila()
        {
            try
            {
                var fechaVencimiento = Convert.ToDateTime("2017-01-01");
                int idDetalle = ((List<VCompra_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VCompra_01>)Dgv_Detalle.DataSource).Max(c => c.Id);
                int posicion = ((List<VCompra_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VCompra_01>)Dgv_Detalle.DataSource).Count;
                VCompra_01 nuevo = new VCompra_01()
                {
                    Id = idDetalle + 1,
                    IdCompra = 0,
                    Estado = 0,
                    IdProducto = 0,
                    Producto = "",
                    Unidad = "",                  
                    Cantidad = 0,
                    Costo = 0,
                    Total = 0,
                    Lote= "20170101",
                    FechaVen = fechaVencimiento,
                    Utilidad = 0,
                    Porcent = 0
                };
                ListaDetalle.Insert(posicion, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InHabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = false;
                GPanel_Producto.Height = 30;
                Dgv_Detalle.Select();
                Dgv_Detalle.Col = 5;
                Dgv_Detalle.Row = Dgv_Detalle.RowCount - 1;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InsertarProducto()
        {
            try
            {
                List<ENTITY.Producto.View.VProductoLista> result;
                //Productos materia prima
                GPanel_Producto.Text = "PRODUCTOS DE COMERCIALES";
                result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().Where(p => p.Tipo.Equals(1)).ToList();
                MP_CargarProducto(result);
                MP_HabilitarProducto();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_HabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = true;
                GPanel_Producto.Height = 350; 
                Dgv_Producto.Focus();
                Dgv_Producto.MoveTo(Dgv_Producto.FilterRow);
                Dgv_Producto.Col = 3;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_VerificarSeleccion(string columna)
        {
            try
            {
                if (Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns[columna].Index)
                {
                    if (Dgv_Detalle.GetValue("Producto").ToString() != string.Empty && Dgv_Detalle.GetValue("IdProducto").ToString() != string.Empty)
                    {
                        MP_AddFila();
                        MP_HabilitarProducto();
                        MP_InsertarProducto();
                    }
                    else
                        throw new Exception("Seleccione un producto");
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }
        private void MP_EliminarFila()
        {
            try
            {
                if (Dgv_Detalle.RowCount > 1)
                {
                    Dgv_Detalle.UpdateData();
                    int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                    int idDetalle = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Id"].Value);
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        //Elimina
                        ListaDetalle = ((List<VCompra_01>)Dgv_Detalle.DataSource).ToList();
                        var lista = ListaDetalle.Where(t => t.Id == idDetalle).FirstOrDefault();
                        ListaDetalle.Remove(lista);
                        MP_ArmarDetalle();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                        {
                            Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                            Dgv_Detalle.UpdateData();
                            Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
                        }

                    }
                }
                else
                    throw new Exception("El detalle no puede estar vacio");
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion
        #region Eventos
        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }
        private void Dgv_GBuscador_EditingCell(object sender, EditingCellEventArgs e)
        {
            e.Cancel = true;
        }
        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (Tb_Observacion.ReadOnly == false)
                {
                    if (e.Column.Index == Dgv_Detalle.RootTable.Columns["Cantidad"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Utilidad"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Costo"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Porcent"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Lote"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["FechaVen"].Index)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Detalle_CellEdited(object sender, ColumnActionEventArgs e)
        {
            try
            {
                Dgv_Detalle.UpdateData();
                int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    MP_CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        MP_CalcularFila();
                        Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CalcularFila()
        {
            try
            {
                Double cantidad, precioCompra, total, utilidad, precioVenta, porcentaje;
                cantidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                precioCompra = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Costo"].Value);
                total = cantidad * precioCompra;
                Dgv_Detalle.CurrentRow.Cells["Total"].Value = total;
                if (Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns["Porcent"].Index)
                {
                    precioVenta = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Porcent"].Value);
                    utilidad = ((precioVenta - precioCompra) * 100) / precioCompra;
                    Dgv_Detalle.CurrentRow.Cells["utilidad"].Value = utilidad;
                }
                if (Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns["Utilidad"].Index)
                {
                    utilidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Utilidad"].Value);
                    porcentaje = (precioCompra + ((precioCompra)) * (utilidad / 100));
                    Dgv_Detalle.CurrentRow.Cells["Porcent"].Value = porcentaje;
                }
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Producto_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (Tb_Observacion.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        var idDetalle = Convert.ToInt32(Dgv_Detalle.GetValue("id"));
                        if (idDetalle > 0)
                        {
                            var idProducto = Convert.ToInt32(Dgv_Producto.GetValue("Id"));
                            var Lista = new ServiceDesktop.ServiceDesktopClient().ProductoListarXId(idProducto);
                            var UnidadVenta = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo((int)ENEstaticosGrupo.PRODUCTO,
                                                                                                             (int)ENEstaticosOrden.PRODUCTO_UN_VENTA)
                                                                                                             .Where(l => l.IdLibreria == Lista.UniVenta)
                                                                                                             .Select(l => l.Descripcion)
                                                                                                             .FirstOrDefault();
                            var IdSucursal = new ServiceDesktop.ServiceDesktopClient().AlmacenListar()
                                                                                    .Where(a => a.Id == Convert.ToInt32(Cb_Almacen.Value))
                                                                                    .Select(a => a.SucursalId).FirstOrDefault();
                            var PrecioCompra = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar(IdSucursal).Where(
                                                                                    p => p.IdProducto == idProducto
                                                                                    && p.IdPrecioCat == (int)ENCategoriaPrecio.COSTO).Select(
                                                                                    p => p.Precio).FirstOrDefault();
                            var PrecioVenta = new ServiceDesktop.ServiceDesktopClient().PrecioProductoListar(IdSucursal).Where(
                                                                          p => p.IdProducto == idProducto
                                                                          && p.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Select(
                                                                          p => p.Precio).FirstOrDefault();


                            ListaDetalle = (List<VCompra_01>)Dgv_Detalle.DataSource;
                            foreach (var fila in ListaDetalle)
                            {
                                if (idProducto == fila.IdProducto)
                                {
                                    throw new Exception("El producto ya fue seleccionado");
                                }
                                if (fila.Id == idDetalle)
                                {
                                    fila.IdProducto = Lista.Id;
                                    fila.Producto = Lista.Descripcion;
                                    fila.Unidad = UnidadVenta;
                                    fila.Cantidad = 1;
                                    fila.Costo = PrecioCompra;
                                    fila.Porcent = PrecioVenta;
                                    fila.Utilidad = (((PrecioVenta - PrecioCompra) * 100) / PrecioCompra);
                                    fila.Total = PrecioCompra;
                                }
                            }
                            MP_ArmarDetalle();
                            MP_InHabilitarProducto();
                        }
                    }
                    if (e.KeyData == Keys.Escape)
                    {
                        MP_InHabilitarProducto();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Producto_EditingCell_1(object sender, EditingCellEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Dgv_Detalle_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (Tb_Observacion.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("Producto");
                        MP_VerificarSeleccion("Cantidad");
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter && Dgv_Detalle.Row >= 0
                        && Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns["Producto"].Index)
                    {
                        int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                        if (estado == (int)ENEstado.NUEVO)
                        {

                            MP_InsertarProducto();
                        }
                        else
                        {
                            string mensaje = "-No se puede cambiar de producto " + "\n" +
                                             "-Modifique la cantidad";
                            throw new Exception(mensaje);
                        }

                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        //Eliminar FIla.
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void Tb_Proveedor_KeyDown(object sender, KeyEventArgs e)
        {
            //yea
            if (Tb_Observacion.ReadOnly == false)
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                {
                    var lista = new ServiceDesktop.ServiceDesktopClient().ProveedorListarEncabezado();
                    List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "ID", tamano = 80 },
                        new GLCelda() { campo = "Descrip", visible = true, titulo = "DESCRIPCION", tamano = 150 },
                        new GLCelda() { campo = "Contacto", visible = true, titulo = "CONTACTO", tamano = 100 },
                        new GLCelda() { campo = "Ciudad", visible = false, titulo = "IDCIUDAD", tamano = 80 },
                        new GLCelda() { campo = "CiudadNombre", visible = true, titulo = "CIUDAD", tamano = 120 },
                        new GLCelda() { campo = "Telfon", visible = true, titulo = "TELEFONO", tamano = 100 },
                        new GLCelda() { campo = "EdadSemana", visible = true, titulo = "EDAD EN SEMANAS", tamano = 100 }
                    };
                    Efecto efecto = new Efecto();
                    efecto.Tipo = 3;
                    efecto.Tabla = lista;
                    efecto.SelectCol = 2;
                    efecto.listaCelda = listEstCeldas;
                    efecto.Alto = 50;
                    efecto.Ancho = 350;
                    efecto.Context = "SELECCIONE UN PROVEEDOR";
                    efecto.ShowDialog();
                    bool bandera = false;
                    bandera = efecto.Band;
                    if (bandera)
                    {
                        Janus.Windows.GridEX.GridEXRow Row = efecto.Row;
                        if (Row != null)
                        {
                            _IdProveedor = Convert.ToInt32(Row.Cells["Id"].Value);
                            Tb_Proveedor.Text = Row.Cells["Descrip"].Value.ToString();
                        }                      
                    }
                }
            }
        }
        private void Sw_TipoVenta_ValueChanged(object sender, EventArgs e)
        {
            if (Sw_TipoVenta.Value == true)
            {
                lbCredito.Visible = false;
                Tb_FechaVenc.Visible = false;
            }
            else
            {
                lbCredito.Visible = true;
                Tb_FechaVenc.Visible = true;
            }
        }
        private void Sw_Emision_ValueChanged(object sender, EventArgs e)
        {
            if (Sw_Emision.Value == true)
            {
                lbNFactura.Visible = true;
                Tb_NFactura.Visible = true;
                LblRecibo.Visible = false;
                Tb_Recibo.Visible = false;
            }
            else
            {
                lbNFactura.Visible = false;
                Tb_NFactura.Visible = false;
                LblRecibo.Visible = true;
                Tb_Recibo.Visible = true;
            }
        }
        private void Tb_MDesc_ValueChanged(object sender, EventArgs e)
        {
            MP_ObtenerCalculo();
        }
        private void btnPrimero_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = 0;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos > 0 && Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = _MPos - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (_MPos < Dgv_GBuscador.RowCount - 1 && _MPos >= 0)
                {
                    _MPos = Dgv_GBuscador.Row + 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            try
            {
                _MPos = Dgv_GBuscador.Row;
                if (Dgv_GBuscador.RowCount > 0)
                {
                    _MPos = Dgv_GBuscador.RowCount - 1;
                    Dgv_GBuscador.Row = _MPos;
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion
        #region Metodo heredados
        private void MP_VerificarExistenciaLote()
        {
            int IdCompra = Convert.ToInt32(Tb_Id.Text);
            var compra_01 = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista(IdCompra).ToList();
            var detalle = ((List<VCompra_01>)Dgv_Detalle.DataSource).ToList();
            foreach (var item in detalle)
            {
                if (new ServiceDesktop.ServiceDesktopClient().CompraIngreso_ExisteEnSeleccion(IdCompra))
                {
                    throw new Exception("La compra esta asociado a una Seleccion.");
                }
                var producto = compra_01.Where(p=> p.IdProducto == item.IdProducto).FirstOrDefault();
                if (item.Lote == producto.Lote && item.FechaVen == producto.FechaVen)
                {

                }
            }
           
        }
        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                string mensaje = "";
                VCompra CompraIngreso = new VCompra()
                {
                    IdAlmacen = Convert.ToInt32(Cb_Almacen.Value),
                    IdProvee = _IdProveedor,
                    Estado = (int)ENEstado.GUARDADO,
                    FechaDoc = Tb_Fecha.Value,
                    TipoVenta = Sw_TipoVenta.Value == true ? 1 : 2,
                    FechaVen = Tb_FechaVenc.Value,
                    TipoFactura = Sw_Emision.Value == true ? 1 : 2,
                    Factura = Sw_Emision.Value? "0" : Tb_NFactura.Text,
                    Recibo = Sw_Emision.Value? Tb_Recibo.Text:"0",
                    Observ = Tb_Observacion.Text,            
                    Descu = Convert.ToDecimal(Tb_MDesc.Value),                  
                    Total = Convert.ToDecimal(Tb_Total.Value),
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                int id = Tb_Id.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Id.Text);
                int idAux = id;
                var detalle = ((List<VCompra_01>)Dgv_Detalle.DataSource).ToArray<VCompra_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                resultado = new ServiceDesktop.ServiceDesktopClient().CompraGuardar(CompraIngreso, detalle, ref id, ref LMensaje, TxtNombreUsu.Text);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        Tb_Proveedor.Focus();                      
                        MP_Filtrar(1);
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());
                    }
                    else//Modificar
                    {
                        MP_Filtrar(2);
                        MP_InHabilitar();//El formulario
                        _Limpiar = true;
                        mensaje = GLMensaje.Modificar_Exito(_NombreFormulario, id.ToString());
                        MH_Habilitar();//El menu                   
                    }
                }
                //Resultado
                if (resultado)
                {

                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                else
                {
                    //Obtiene los productos sin stock
                    var mensajeLista = LMensaje.ToList();
                    if (mensajeLista.Count > 0)
                    {
                        var mensajes = "";
                        foreach (var item in mensajeLista)
                        {
                            mensajes = mensajes + "- " + item + "\n";
                        }
                        MP_MostrarMensajeError(mensajes);
                        return false;
                    }
                    mensaje = GLMensaje.Registro_Error(_NombreFormulario);
                    ToastNotification.Show(this, mensaje, PRESENTER.Properties.Resources.CANCEL, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return resultado;
            }
        }
        public override bool MH_Eliminar()
        {
            try
            {
                int IdCompra= Convert.ToInt32(Tb_Id.Text);             
                Efecto efecto = new Efecto();
                efecto.Tipo = 2;
                efecto.Context = GLMensaje.Pregunta_Eliminar.ToUpper();
                efecto.Header = GLMensaje.Mensaje_Principal.ToUpper();
                efecto.ShowDialog();
                bool resul = false;
                if (efecto.Band)
                {
                    List<string> Mensaje = new List<string>();
                    var LMensaje = Mensaje.ToArray();
                    resul = new ServiceDesktop.ServiceDesktopClient().CompraModificarEstado(IdCompra, (int)ENEstado.ELIMINAR, ref LMensaje);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito(_NombreFormulario, Tb_Id.Text));
                    }
                    else
                    {
                        //Obtiene los codigos de productos sin stock
                        var mensajeLista = LMensaje.ToList();
                        if (mensajeLista.Count > 0)
                        {
                            var mensaje = "";
                            foreach (var item in mensajeLista)
                            {
                                mensaje = mensaje + "- " + item + "\n";
                            }
                            MP_MostrarMensajeError(mensaje);
                            return false;
                        }
                        else
                        {
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error(_NombreFormulario, Tb_Id.Text));
                        }
                    }
                }
                return resul;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        public override void MH_Nuevo()
        {
            MP_Habilitar();
            MP_Limpiar();

        }        
        public override void MH_Modificar()
        {
            
            MP_Habilitar();
        }
        public override void MH_Salir()
        {
            MP_InHabilitar();
            MP_Filtrar(1);
        }
        public override bool MH_Validar()
        {
            bool _Error = false;
            try
            {

                if (Cb_Almacen.SelectedIndex == -1)
                {
                    Cb_Almacen.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen.BackColor = Color.White;

                if (_IdProveedor == 0)
                {
                    Tb_Proveedor.BackColor = Color.Red;
                    _Error = true;
                    throw new Exception("Seleccionar un proveedor");
                }
                else
                    Tb_Proveedor.BackColor = Color.White;
                if (((List<VCompra_01>)Dgv_Detalle.DataSource).Count() == 0)
                {
                    _Error = true;
                    throw new Exception("El detalle se encuentra vacio");
                }           
                if (Tb_Total.Value == 0)
                {
                    Tb_Total.BackColor = Color.Red;
                    _Error = true;
                    throw new Exception("El total se encuentra en 0");
                }
                else
                    Tb_Proveedor.BackColor = Color.White;
                return _Error;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
                return _Error;
            }
        }



        #endregion
    }
}
