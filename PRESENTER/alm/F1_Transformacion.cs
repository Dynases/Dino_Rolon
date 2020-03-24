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
using ENTITY.inv.Transformacion_01.View;
using ENTITY.inv.Transformacion.View;
using UTILITY.Enum.EnEstado;
using PRESENTER.Report;
using UTILITY;

namespace PRESENTER.com
{
    public partial class F1_Transformacion : MODEL.ModeloF1
    {
        #region Variables
        string _NombreFormulario = "TRANSFORMACION";
        bool _Limpiar = false;
        int _idOriginal = 0;
        int _MPos = 0;
        int _IdProducto = 0;
        int _idProducto_Mat = 0;
        List<VTransformacion_01> ListaDetalle = new List<VTransformacion_01>();
        #endregion
        public F1_Transformacion()
        {
            InitializeComponent();
        }

        private void F1_Transformacion_Load(object sender, EventArgs e)
        {
            this.Name = _NombreFormulario;
            superTabControl1.SelectedTabIndex = 0;
            MP_Iniciar();
        }

        #region Metodos privados
        private void MP_Iniciar()
        {
            try
            {
                LblTitulo.Text = _NombreFormulario;
                MP_CargarAlmacenes();
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

                    Dgv_GBuscador.RootTable.Columns["IdAlmacenSalida"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Almacen2"].Caption = "Almacen Salida";
                    Dgv_GBuscador.RootTable.Columns["Almacen2"].Width = 350;
                    Dgv_GBuscador.RootTable.Columns["Almacen2"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Almacen2"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Almacen2"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Almacen2"].Visible = true;

                    Dgv_GBuscador.RootTable.Columns["IdAlmacenIngreso"].Visible = false;

                    Dgv_GBuscador.RootTable.Columns["Almacen1"].Caption = "Almacen Ingreso";
                    Dgv_GBuscador.RootTable.Columns["Almacen1"].Width = 350;
                    Dgv_GBuscador.RootTable.Columns["Almacen1"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_GBuscador.RootTable.Columns["Almacen1"].CellStyle.FontSize = 8;
                    Dgv_GBuscador.RootTable.Columns["Almacen1"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_GBuscador.RootTable.Columns["Almacen1"].Visible = true;

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
        private void MP_CargarDetalle(int idTransformacion)
        {
            try
            {
                ListaDetalle = new ServiceDesktop.ServiceDesktopClient().Transformacion_01_Lista(idTransformacion).ToList();
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
                Dgv_Detalle.RootTable.Columns["IdTransformacion"].Visible = false;
                Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;

                Dgv_Detalle.RootTable.Columns["IdProducto"].Visible = false;

                Dgv_Detalle.RootTable.Columns["Producto"].Caption = "PRODUCTO";
                Dgv_Detalle.RootTable.Columns["Producto"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["Producto"].Visible = true;

                Dgv_Detalle.RootTable.Columns["IdProducto_Mat_Prima"].Visible = false;

                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].Caption = "M. PRIMA";
                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].Width = 150;
                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Detalle.RootTable.Columns["Producto_Mat_Prima"].Visible = true;

                Dgv_Detalle.RootTable.Columns["TotalProd"].Caption = "TOTAL PROD";
                Dgv_Detalle.RootTable.Columns["TotalProd"].FormatString = "0";
                Dgv_Detalle.RootTable.Columns["TotalProd"].Width = 120;
                Dgv_Detalle.RootTable.Columns["TotalProd"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Detalle.RootTable.Columns["TotalProd"].CellStyle.FontSize = 9;
                Dgv_Detalle.RootTable.Columns["TotalProd"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Detalle.RootTable.Columns["TotalProd"].Visible = true;

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
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
        }
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen1, almacenes);
                UTGlobal.MG_ArmarComboAlmacen(Cb_Almacen2, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
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
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Habilitar()
        {
            try
            {
                this.Cb_Almacen1.ReadOnly = false;
                this.Cb_Almacen2.ReadOnly = false;
                this.Tb_Observacion.ReadOnly = false;
                this.Tb_Total1.IsInputReadOnly = false;
                this.Tb_Total2.IsInputReadOnly = false;
                this.Dgv_Detalle.Enabled = true;
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }          
        }
        private void MP_InHabilitar()
        {
            try
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
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }          
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
                    UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Almacen1);
                    UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Almacen2);
                }
                // ((DataTable)Dgv_Detalle.DataSource).Clear();
                Dgv_Detalle.DataSource = null;
                ListaDetalle.Clear();
                MP_AddFila();
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
                    var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().Transformacion_Lista().Where(A => A.Id == _idOriginal).ToList();
                    var Lista = ListaCompleta.First();
                    Tb_Id.Text = Lista.Id.ToString();
                    Cb_Almacen1.Value = Lista.IdAlmacenSalida;
                    Cb_Almacen2.Value = Lista.IdAlmacenIngreso;
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
                Dgv_Detalle.UpdateData();
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
            try
            {
                MP_CargarEncabezado();
                if (Dgv_GBuscador.RowCount > 0)
                {
                    //_MPos = 0;
                    MP_MostrarRegistro(tipo == 1 ? 0 : _MPos);
                }
                else
                {
                    MP_Limpiar();
                    LblPaginacion.Text = "0/0";
                }
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
                if (_IdProducto == 0)
                {
                    //Productos Comerciales
                    GPanel_Producto.Text = "PRODUCTO COMERCIALES";
                    result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().Where(p => p.Tipo.Equals(1)).ToList();
                }
                else
                {
                    //Productos materia prima
                    GPanel_Producto.Text = "PRODUCTOS DE MATERIA PRIMA";
                    result = new ServiceDesktop.ServiceDesktopClient().ProductoListar().Where(p => p.Tipo.Equals(2)).ToList();

                }
                MP_CargarProducto(result);
                MP_HabilitarProducto();
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

        private void MP_HabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = true;
                Dgv_Producto.Focus();
                Dgv_Producto.MoveTo(Dgv_Producto.FilterRow);
                Dgv_Producto.Col = 3;
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
                Dgv_Detalle.Select();
                Dgv_Detalle.Col = 3;
                Dgv_Detalle.Row = Dgv_Detalle.RowCount - 1;
                _IdProducto = 0;
                _idProducto_Mat = 0;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_Calcular()
        {
            try
            {
                Dgv_Detalle.UpdateData();
                Tb_Total1.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["TotalProd"], AggregateFunction.Sum));
                Tb_Total2.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["Total"], AggregateFunction.Sum));
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
                    if (Dgv_Detalle.GetValue("Producto").ToString() != string.Empty && Dgv_Detalle.GetValue("IdProducto_Mat_Prima").ToString() != string.Empty)
                    {
                        MP_AddFila();
                        MP_HabilitarProducto();
                        MP_InsertarProducto();
                    }
                    else
                        throw new Exception("Seleccione un producto y materia prima");
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }
        private void MP_AddFila()
        {
            try
            {
                VTransformacion_01 nuevo = new VTransformacion_01()
                {
                    Id = Dgv_Detalle.RowCount + 1,
                    IdTransformacion = 0,
                    Estado = 0,
                    IdProducto = 0,
                    Producto = "",
                    IdProducto_Mat_Prima = 0,
                    Producto_Mat_Prima = "",
                    TotalProd = 0,
                    Cantidad = 0,
                    Total = 0,
                };
                ListaDetalle.Insert(Dgv_Detalle.RowCount, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ObtenerPosicion(ref int pos, int IdDetalle)
        {
            for (int i = 0; i < ((List<VTransformacion_01>)Dgv_Detalle.DataSource).Count; i++)
            {
                int _IdDetalle = ((List<VTransformacion_01>)Dgv_Detalle.DataSource)[i].Id;
                if (_IdDetalle == IdDetalle)
                {
                    pos = i;
                    return;
                }
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
            try
            {
                if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
                {
                    MP_MostrarRegistro(Dgv_GBuscador.Row);
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
           
        }

        private void Dgv_Detalle_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (Tb_Observacion.ReadOnly == false)
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
                Dgv_Detalle.UpdateData();
                int estado = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.COMPLETADO)
                {
                    throw new Exception("PRODUCTO COMPLETADO NO SE PUEDE  MODIFICAR");
                }
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        CalcularFila();
                        Dgv_Detalle.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void CalcularFila()
        {
            try
            {
                Double cantidad, totalProd, total;
                cantidad = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                totalProd = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["TotalProd"].Value);
                total = cantidad * totalProd;
                Dgv_Detalle.CurrentRow.Cells["Total"].Value = total;
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }           
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
        private void Dgv_Producto_EditingCell(object sender, EditingCellEventArgs e)
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

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            MP_Imprimir(Convert.ToInt32(Tb_Id.Text));
        }

        private void MP_Imprimir(int Id)
        {
            try
            {

                //TRANSAFORMACION INGRESO
                if (UTGlobal.visualizador != null)
                {
                    UTGlobal.visualizador.Close();
                }
                UTGlobal.visualizador = new Visualizador();
                var lista = new ServiceDesktop.ServiceDesktopClient().TransformacionIngreso(Id);
                var lista2 = new ServiceDesktop.ServiceDesktopClient().TransformacionSalida(Id);
                var ObjetoReport = new RTransformacionIngreso();
                ObjetoReport.Subreports["RTransformacionSalida.rpt"].SetDataSource(lista2);
                ObjetoReport.SetDataSource(lista);
                UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport;
                UTGlobal.visualizador.ShowDialog();
                UTGlobal.visualizador.BringToFront();

                //TRANSFORMACION SALIDA
                //if (UTGlobal.visualizador != null)
                //{
                //    UTGlobal.visualizador.Close();
                //}
                //UTGlobal.visualizador = new Visualizador();
                //var lista2 = new ServiceDesktop.ServiceDesktopClient().TransformacionSalida(Convert.ToInt32(Tb_Id.Text));
                //var ObjetoReport2 = new RTransformacionSalida();
                //ObjetoReport2.SetDataSource(lista2);
                //UTGlobal.visualizador.ReporteGeneral.ReportSource = ObjetoReport2;
                //UTGlobal.visualizador.ShowDialog();
                //UTGlobal.visualizador.BringToFront();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }          
        }

        private void Dgv_Detalle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Almacen1.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("Producto");
                        MP_VerificarSeleccion("IdProducto_Mat_Prima");
                        MP_VerificarSeleccion("TotalProd");
                        MP_VerificarSeleccion("Cantidad");
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter && Dgv_Detalle.Row >= 0
                        && Dgv_Detalle.Col == Dgv_Detalle.RootTable.Columns["Producto"].Index)
                    {
                        MP_HabilitarProducto();
                        MP_InsertarProducto();
                    }
                    if (e.KeyCode == Keys.Escape)
                    {
                        //Eliminar FIla
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }

        }

        private void Dgv_Producto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Almacen1.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (_IdProducto == 0)
                        {
                            _IdProducto = Convert.ToInt32(Dgv_Producto.GetValue("id"));
                            MP_InsertarProducto();
                        }
                        else
                        {
                            var idDetalle = Convert.ToInt32(Dgv_Detalle.GetValue("id"));
                            _idProducto_Mat = Convert.ToInt32(Dgv_Producto.GetValue("id"));
                            if (_idProducto_Mat > 0)
                            {
                                var ProductoNombre = new ServiceDesktop.ServiceDesktopClient().Transformacion_01_TraerFilaProducto(_IdProducto, _idProducto_Mat);
                                ListaDetalle = (List<VTransformacion_01>)Dgv_Detalle.DataSource;
                                foreach (var fila in ListaDetalle)
                                {
                                    if (fila.Id == idDetalle)
                                    {
                                        fila.IdProducto = ProductoNombre.IdProducto;
                                        fila.Producto = ProductoNombre.Producto;
                                        fila.IdProducto_Mat_Prima = ProductoNombre.IdProducto_Mat_Prima;
                                        fila.Producto_Mat_Prima = ProductoNombre.Producto_Mat_Prima;
                                        fila.Cantidad = ProductoNombre.Cantidad;
                                    }
                                }
                                MP_ArmarDetalle();
                                MP_InHabilitarProducto();
                            }
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
        #endregion


        #region Metodo heredados

        public override bool MH_NuevoRegistro()
        {
            bool resultado = false;
            try
            {
                string mensaje = "";

                VTransformacion CompraIngreso = new VTransformacion()
                {
                    IdAlmacenIngreso = Convert.ToInt32(Cb_Almacen2.Value),
                    IdAlmacenSalida = Convert.ToInt32(Cb_Almacen1.Value),
                    Observ = Tb_Observacion.Text,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = UTGlobal.Usuario,
                };
                int id = Tb_Id.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Id.Text);
                int idAux = id;
                var detalle = ((List<VTransformacion_01>)Dgv_Detalle.DataSource).ToArray<VTransformacion_01>();

                resultado = new ServiceDesktop.ServiceDesktopClient().TransformacionGuardar(CompraIngreso, detalle, ref id);
                if (resultado)
                {
                    if (idAux == 0)//Registar
                    {
                        Cb_Almacen1.Focus();
                        MP_Imprimir(id);
                        MP_CargarEncabezado();
                        MP_Limpiar();
                        _Limpiar = true;
                        mensaje = GLMensaje.Nuevo_Exito(_NombreFormulario, id.ToString());

                    }
                    else//Modificar
                    {
                        MP_Imprimir(id);
                        MP_Filtrar(1);
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

                if (Cb_Almacen1.SelectedIndex == -1)
                {
                    Cb_Almacen1.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen1.BackColor = Color.White;

                if (Cb_Almacen2.SelectedIndex == -1)
                {
                    Cb_Almacen2.BackColor = Color.Red;
                    _Error = true;
                }
                else
                    Cb_Almacen2.BackColor = Color.White;
                if (Dgv_Detalle.RowCount == 0)
                {
                    throw new Exception("El detalle no tiene registros");
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





    }
}
