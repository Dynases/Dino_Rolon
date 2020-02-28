﻿using Janus.Windows.GridEX;
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
        #endregion
        public F1_Compra()
        {
            InitializeComponent();
            MP_Iniciar();
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
                MP_InicioArmarCombo();
                btnMax.Visible = false;
                MP_CargarEncabezado();
                MP_InHabilitar();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarEncabezado()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Compra_Lista().ToList();
                if (ListaCompleta.Count() > 0)
                {
                    Dgv_GBuscador.DataSource = ListaCompleta;
                    Dgv_GBuscador.RetrieveStructure();
                    Dgv_GBuscador.AlternatingColors = true;

                    Dgv_GBuscador.RootTable.Columns["id"].Caption = "Id";
                    Dgv_GBuscador.RootTable.Columns["id"].Width = 250;
                    Dgv_GBuscador.RootTable.Columns["id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["id"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["IdSuc"].Visible = false;
                    Dgv_GBuscador.RootTable.Columns["IdProvee"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Caption = "Proveedor";
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Width = 350;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Proveedor"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["FechaDoc"].Caption = "Fecha";
                    Dgv_GBuscador.RootTable.Columns["FechaDoc"].Width = 350;
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
                    Dgv_GBuscador.RootTable.Columns["Total"].Width = 200;
                    Dgv_GBuscador.RootTable.Columns["Total"].FormatString = "0.00";
                    Dgv_GBuscador.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["Total"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;
                    Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;
                    Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                    //Habilitar filtradores
                    Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                    Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                    //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                    Dgv_GBuscador.GroupByBoxVisible = false;
                    Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;
                }
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
                var lresult = new ServiceDesktop.ServiceDesktopClient().Compra_01_Lista().Where(a => a.IdCompra == id).ToList();
                if (lresult.Count() > 0)
                {
                    Dgv_Detalle.DataSource = lresult;
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


                    Dgv_Detalle.RootTable.Columns["Lote"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["FechaVen"].Visible = false;

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
            }
        }
        private void MP_InicioArmarCombo()
        {
            UTGlobal.MG_ArmarComboSucursal(Cb_Sucursal,
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
            Tb_Id.ReadOnly = false;
            Tb_Proveedor.ReadOnly = false;
            Tb_FechaVenta.IsInputReadOnly = false;
            Tb_FechaVenc.IsInputReadOnly = false;
            Tb_Observacion.ReadOnly = false;
            Cb_Sucursal.ReadOnly = false;
            Tb_NFactura.ReadOnly = false;
            Sw_Emision.IsReadOnly = false;
            Sw_TipoVenta.IsReadOnly = false;  
            Dgv_Detalle.Enabled = true;
        }
        private void MP_InHabilitar()
        {
            Tb_Id.ReadOnly = true;
            Tb_Proveedor.ReadOnly = true;
            Tb_FechaVenta.IsInputReadOnly = true;
            Tb_FechaVenc.IsInputReadOnly = true;
            Tb_Observacion.ReadOnly = true;
            Cb_Sucursal.ReadOnly = true;
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
                Tb_FechaVenta.Value = DateTime.Today;
                Tb_FechaVenc.Value = DateTime.Today;
                Tb_Observacion.Clear();
                Tb_NFactura.Clear();
                Sw_Emision.Value = true;
                Sw_TipoVenta.Value = true;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo(Cb_Sucursal);
                }
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
                    Cb_Sucursal.Value = Lista.IdSuc;
                    _IdProveedor = Lista.IdProvee;
                    Tb_Proveedor.Text = Lista.Proveedor.ToString();
                    Tb_FechaVenta.Value = Lista.FechaDoc;
                    Sw_TipoVenta.Value = Lista.TipoVenta == 1 ? true : false;
                    Tb_MDesc.Value = Convert.ToDouble( Lista.Descu);
                    Tb_Total.Value = Convert.ToDouble( Lista.Total);
                    Tb_Observacion.Text = Lista.Observ;
                    Tb_FechaVenc.Value = Lista.FechaVen;
                    MP_CargarDetalle(Convert.ToInt32(Tb_Id.Text));
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
                Tb_Total.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
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

        #endregion
        #region Eventos
        private void Dgv_GBuscador_SelectionChanged(object sender, EventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (Tb_Id.ReadOnly == false)
                {
                    if (e.Column.Index == Dgv_Detalle.RootTable.Columns["Cantidad"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Utilidad"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Costo"].Index ||
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["Porcent"].Index)
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
                Double cantidad, precio, total, utilidad,precioVenta;
                cantidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                precio = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Costo"].Value);
                total = cantidad * precio;
                Dgv_Detalle.CurrentRow.Cells["Total"].Value = total;
                utilidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Utilidad"].Value);
                precioVenta = total * (utilidad / 100);
                Dgv_Detalle.CurrentRow.Cells["Porcent"].Value = precioVenta;
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        #endregion


        #region Metodo heredados

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

                if (Cb_Sucursal.SelectedIndex == 0)
                {
                    Cb_Sucursal.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Sucursal.BackColor = Color.White;

                if (_IdProveedor == 0)
                {
                    Tb_Proveedor.BackColor = Color.Red;
                    _Error = true;
                    throw new Exception("Debe seleccionar un proveedor, no se puede guardar");
                }
                else
                    Tb_Proveedor.BackColor = Color.White;
                if (Dgv_Detalle.RowCount == 0)
                {
                    throw new Exception("Detalle vacio, no se puede guardar");
                }
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
