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

namespace PRESENTER.com
{
    public partial class F1_Transformacion : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "TRANSFORMACION";
        bool _Limpiar = false;
        int _idOriginal = 0;
        int _MPos = 0;
        #endregion
        public F1_Transformacion()
        {
            InitializeComponent();
        }

        private void F1_Transformacion_Load(object sender, EventArgs e)
        {
            this.Name = _NombreFormulario;
            MP_Iniciar();
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
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Transformacion_Lista().ToList();    
                if (ListaCompleta.Count() > 0)
                {
                    Dgv_GBuscador.DataSource = ListaCompleta;
                    Dgv_GBuscador.RetrieveStructure();
                    Dgv_GBuscador.AlternatingColors = true;

                    Dgv_GBuscador.RootTable.Columns["id"].Caption = "Nro. Transformacion";
                    Dgv_GBuscador.RootTable.Columns["id"].Width = 250;
                    Dgv_GBuscador.RootTable.Columns["id"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["id"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["id"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["IdSucSalida"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].Caption = "Nota Sucursal2";
                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].Width = 350;
                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Sucursal2"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["IdSucIngreso"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].Caption = "Salida";
                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].Width = 350;
                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["Sucursal1"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["Observ"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Fecha"].Caption = "Fecha";
                    Dgv_GBuscador.RootTable.Columns["Fecha"].Width = 200;
                    Dgv_GBuscador.RootTable.Columns["Fecha"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Fecha"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Fecha"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = true;

                   
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
                var lresult = new ServiceDesktop.ServiceDesktopClient().Transformacion_01_Lista().Where(a => a.IdTransformacion == id).ToList();      
                if (lresult.Count() > 0)
                {
                    Dgv_Detalle.DataSource = lresult;
                    Dgv_Detalle.RetrieveStructure();
                    Dgv_Detalle.AlternatingColors = true;

                    Dgv_Detalle.RootTable.Columns["id"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["IdTransformacion"].Visible = false;
                    Dgv_Detalle.RootTable.Columns["IdProducto"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                    Dgv_Detalle.RootTable.Columns["Producto"].Width = 150;
                    Dgv_Detalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Detalle.RootTable.Columns["Producto"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;

                    Dgv_Detalle.RootTable.Columns["TotalProd"].Caption = "TOTAL PROD";
                    Dgv_Detalle.RootTable.Columns["TotalProd"].FormatString = "0";
                    Dgv_Detalle.RootTable.Columns["TotalProd"].Width = 120;
                    Dgv_Detalle.RootTable.Columns["TotalProd"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["TotalProd"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["TotalProd"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["TotalProd"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Producto2"].Caption = "M. PRIMA";
                    Dgv_Detalle.RootTable.Columns["Producto2"].Width = 150;
                    Dgv_Detalle.RootTable.Columns["Producto2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Producto2"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Producto2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_Detalle.RootTable.Columns["Producto2"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Cantidad"].Caption = "CANT.";
                    Dgv_Detalle.RootTable.Columns["Cantidad"].FormatString = "0.00";
                    Dgv_Detalle.RootTable.Columns["Cantidad"].Width = 90;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

                    Dgv_Detalle.RootTable.Columns["Total"].Caption = "TOTAL";
                    Dgv_Detalle.RootTable.Columns["Total"].FormatString = "0.00";
                    Dgv_Detalle.RootTable.Columns["Total"].Width = 100;
                    Dgv_Detalle.RootTable.Columns["Total"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_Detalle.RootTable.Columns["Total"].CellStyle.FontSize = 9;
                    Dgv_Detalle.RootTable.Columns["Total"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_Detalle.RootTable.Columns["Total"].Visible = true;
               
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
            UTGlobal.MG_ArmarComboSucursal(Cb_Almacen1,
                                              new ServiceDesktop.ServiceDesktopClient().SucursalListarCombo().ToList());
            
            UTGlobal.MG_ArmarComboSucursal(Cb_Almacen2,
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
            Cb_Almacen1.ReadOnly = false;
            Cb_Almacen2.ReadOnly = false;
            Tb_Observacion.ReadOnly = false;
            Tb_Total1.IsInputReadOnly = false;
            Tb_Total2.IsInputReadOnly =false;        
            Dgv_Detalle.Enabled = true;
        }
        private void MP_InHabilitar()
        {
            Tb_Id.ReadOnly = true;
            Cb_Almacen1.ReadOnly = true;
            Cb_Almacen2.ReadOnly = true;
            Tb_Observacion.ReadOnly = true;
            Tb_Total1.IsInputReadOnly = true;
            Tb_Total2.IsInputReadOnly = true;
            Dgv_Detalle.Enabled = true;
            _Limpiar = false;
            Dgv_Detalle.Enabled = false;         

        }
        private void MP_Limpiar()
        {
            try
            {
                Tb_Id.Clear();
                Cb_Almacen1.SelectedIndex = 0;
                Cb_Almacen2.SelectedIndex = 0;
                Tb_Observacion.Clear();
                Tb_Total1.Value = 0;
                Tb_Total2.Value = 0;
                Dgv_Detalle.Enabled = true;
                if (_Limpiar == false)
                {
                    UTGlobal.MG_SeleccionarCombo(Cb_Almacen1);
                    UTGlobal.MG_SeleccionarCombo(Cb_Almacen2);
                }              
                // ((DataTable)Dgv_Detalle.DataSource).Clear();
                //  Dgv_Detalle.DataSource = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace, GLMensaje.Error);
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
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Transformacion_Lista().Where(A => A.Id == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();
                    Tb_Id.Text = Lista.Id.ToString();
                    Cb_Almacen1.Value = Lista.IdSucSalida;
                    Cb_Almacen2.Value = Lista.IdSucIngreso;
                    Tb_Observacion.Text = Lista.Observ;               
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
                Tb_Total1.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["TotalProd"], AggregateFunction.Sum));
                Tb_Total2.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
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
                        e.Column.Index == Dgv_Detalle.RootTable.Columns["TotalProd"].Index)
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
                Double cantidad, totalProd, total;
                cantidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                totalProd = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["TotalProd"].Value);
                total = cantidad * totalProd;
                Dgv_Detalle.CurrentRow.Cells["Total"].Value = total;
                //MP_ObtenerCalculo();
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
               
                if (Cb_Almacen1.SelectedIndex == 0)
                {
                    Cb_Almacen1.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen1.BackColor = Color.White;

                if (Cb_Almacen2.SelectedIndex == 0)
                {
                    Cb_Almacen2.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen2.BackColor = Color.White;
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

        private void Dgv_Detalle_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            
        }

        private void Dgv_Detalle_KeyDown(object sender, KeyEventArgs e)
        {
            Tb_Total1.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["TotalProd"], AggregateFunction.Sum));
            Tb_Total2.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
        }
    }
}
