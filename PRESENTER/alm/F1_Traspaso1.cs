using DevComponents.DotNetBar;
using ENTITY.inv.TI001.VIew;
using ENTITY.inv.Traspaso.View;
using ENTITY.Producto.View;
using Janus.Windows.GridEX;
using MODEL;
using PRESENTER.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTILITY.Enum.EnEstado;
using UTILITY.Global;

namespace PRESENTER.alm
{
    public partial class F1_Traspaso1 : ModeloF1
    {
        public F1_Traspaso1()
        {
            InitializeComponent();
            this.MP_CargarAlmacenes();
            this.MP_AsignarPermisos();
        }
        #region Variables   
        private static List<VTraspaso> LTraspaso;
        private static List<VTraspaso_01> LTraspasoDetalle;
        private static VProductoListaStock vProductos = new VProductoListaStock();
        private static int index;
        private bool _Limpiar = false;
        #endregion
        #region Metodos Privados
        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Destino, almacenes);
                UTGlobal.MG_ArmarComboAlmacen(Cb_Origen, almacenes);
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }
        void MP_MostrarMensajeExito(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
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
        private void MP_InHabilitar()
        {
            this.Tb_UsuarioEnvio.ReadOnly = true;
            this.Tb_UsuarioRecibe.ReadOnly = true;
            this.Tb_Observacion.ReadOnly = true;
            this.Cb_Destino.ReadOnly = true;
            this.Cb_Origen.ReadOnly = true;
            this.Tb_FechaEnvio.IsInputReadOnly = true;
            this.Tb_FechaDestino.IsInputReadOnly = true;
            this.Sw_Estado.IsReadOnly = true;
            BtnHabilitar.Enabled = true;
            this.Tb_Id.ReadOnly = true;
            _Limpiar = false;
        }

        private void MP_Habilitar()
        {
            this.Tb_UsuarioEnvio.Text = UTGlobal.Usuario;
            this.Tb_Observacion.ReadOnly = false;
            this.Cb_Destino.ReadOnly = false;
            this.Cb_Origen.ReadOnly = false;
            this.Tb_FechaEnvio.IsInputReadOnly = false;
            this.Tb_FechaDestino.IsInputReadOnly = false;
            this.Sw_Estado.IsReadOnly = false;
            BtnHabilitar.Enabled = false;
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
                vProductos = new VProductoListaStock();
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
                LTraspaso = new ServiceDesktop.ServiceDesktopClient().TraspasosListar().ToList();               
                MP_ArmarEncabezado();
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_MostrarRegistro(int index)
        {
            var traspaso = LTraspaso[index];

            Tb_Id.Text = traspaso.Id.ToString();
            Cb_Destino.Value = traspaso.IdDestino;
            Cb_Origen.Value = traspaso.AlmacenOrigen;
            Tb_UsuarioEnvio.Text = traspaso.Usuario;
            Tb_FechaEnvio.Value = traspaso.Fecha;
            Tb_FechaDestino.Value = traspaso.Fecha;
            Sw_Estado.Value = traspaso.Estado == 1? true: false;
            Tb_Observacion.Text = traspaso.Observaciones;        
            this.MP_CargarDetalleRegistro(traspaso.Id,1);
            this.MP_ObtenerCalculo();
            this.LblPaginacion.Text = (index + 1) + "/" + LTraspaso.Count.ToString() + " Traspaso";
        }

        private void MP_CargarDetalleRegistro(int id, int EsPlantilla)
        {
            try
            {
                if (EsPlantilla == 1)
                {
                    LTraspasoDetalle = new ServiceDesktop.ServiceDesktopClient().TraspasoDetalleListar(id).ToList();
                }
                else
                    //CargarPlantilla             
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_ArmarLotes(List<VTI001> listaLotes)
        {
            Dgv_Producto.DataSource = listaLotes;
            Dgv_Producto.RetrieveStructure();
            Dgv_Producto.AlternatingColors = true;

            Dgv_Producto.RootTable.Columns["IdAlmacen"].Visible = false;
            Dgv_Producto.RootTable.Columns["IdProducto"].Visible = false;
            Dgv_Producto.RootTable.Columns["Unidad"].Visible = false;

            Dgv_Producto.RootTable.Columns["Lote"].Caption = "Lote";
            Dgv_Producto.RootTable.Columns["Lote"].Width = 150;
            Dgv_Producto.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Producto.RootTable.Columns["Lote"].Visible = true;

            Dgv_Producto.RootTable.Columns["FechaVen"].Caption = "FechaVen";
            Dgv_Producto.RootTable.Columns["FechaVen"].Width = 150;
            Dgv_Producto.RootTable.Columns["FechaVen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["FechaVen"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["FechaVen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Producto.RootTable.Columns["FechaVen"].Visible = true;

            Dgv_Producto.RootTable.Columns["Cantidad"].Caption = "Stock";
            Dgv_Producto.RootTable.Columns["Cantidad"].Width = 150;
            Dgv_Producto.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_Producto.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Producto.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_Producto.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Producto.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_Producto.RootTable.Columns["id"].Visible = false;

            Dgv_Producto.GroupByBoxVisible = false;
            Dgv_Producto.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007;
            Dgv_Producto.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Producto.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));

        }
        private void MP_ArmarDetalle()
        {
            Dgv_Detalle.DataSource = LTraspasoDetalle;
            Dgv_Detalle.RetrieveStructure();
            Dgv_Detalle.AlternatingColors = true;

            Dgv_Detalle.RootTable.Columns["Id"].Visible = false;
            Dgv_Detalle.RootTable.Columns["Estado"].Visible = false;
            Dgv_Detalle.RootTable.Columns["TraspasoId"].Visible = false;
            Dgv_Detalle.RootTable.Columns["ProductoId"].Visible = false;

            //Dgv_DetalleVenta.RootTable.Columns["Detalle"].Caption = "CODIGO";
            //Dgv_DetalleVenta.RootTable.Columns["Detalle"].Width = 80;
            //Dgv_DetalleVenta.RootTable.Columns["Detalle"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            //Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 9;
            //Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            //Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Visible = true;

            //Dgv_DetalleVenta.RootTable.Columns["CodigoBarra"].Visible = false;

            Dgv_Detalle.RootTable.Columns["Detalle"].Caption = "PRODUCTO";
            Dgv_Detalle.RootTable.Columns["Detalle"].Width = 240;
            Dgv_Detalle.RootTable.Columns["Detalle"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Detalle"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Detalle"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_Detalle.RootTable.Columns["Detalle"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Unidad"].Caption = "UN.";
            Dgv_Detalle.RootTable.Columns["Unidad"].Width = 80;
            Dgv_Detalle.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Unidad"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
            Dgv_Detalle.RootTable.Columns["Cantidad"].Width = 130;
            Dgv_Detalle.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Contenido"].Caption = "CONT.";
            Dgv_Detalle.RootTable.Columns["Contenido"].Width = 80;
            Dgv_Detalle.RootTable.Columns["Contenido"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["Contenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Contenido"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Contenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_Detalle.RootTable.Columns["TotalContenido"].Caption = "TOTAL UN.";
            Dgv_Detalle.RootTable.Columns["TotalContenido"].Width = 100;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].FormatString = "0.00";
            Dgv_Detalle.RootTable.Columns["TotalContenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["TotalContenido"].Visible = true;            

            Dgv_Detalle.RootTable.Columns["Lote"].Caption = "LOTE";
            Dgv_Detalle.RootTable.Columns["Lote"].Width = 100;
            Dgv_Detalle.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["Lote"].Visible = true;

            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Caption = "FechaVencimiento";
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Width = 100;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_Detalle.RootTable.Columns["FechaVencimiento"].Visible = true;

            Dgv_Detalle.RootTable.Columns["Stock"].Visible = false;

            Dgv_Detalle.RootTable.Columns["Delete"].Caption = "ELIMINAR";
            Dgv_Detalle.RootTable.Columns["Delete"].Width = 60;
            Dgv_Detalle.RootTable.Columns["Delete"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Delete"].CellStyle.FontSize = 9;
            Dgv_Detalle.RootTable.Columns["Delete"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_Detalle.RootTable.Columns["Delete"].Visible = true;
            Dgv_Detalle.RootTable.Columns["Delete"].Image = Resources.delete;
            Dgv_Detalle.RootTable.Columns["Delete"].EditType = EditType.NoEdit;

            Dgv_Detalle.GroupByBoxVisible = false;
            Dgv_Detalle.VisualStyle = VisualStyle.Office2007;
            Dgv_Detalle.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Detalle.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
        }
        private void MP_Reiniciar()
        {
            this.Tb_Id.Clear();
            this.Tb_UsuarioEnvio.Text = UTGlobal.Usuario;
            this.Tb_Observacion.Clear();
            //this.Cb_Destino.Value = true;
            //this.Cb_Origen.ReadOnly = false;
            this.Tb_FechaEnvio.Value = DateTime.Today;
            this.Tb_FechaDestino.Value = DateTime.Today;
            this.Sw_Estado.Value = true;
            if (_Limpiar == false)            {
                
                UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Origen);
                UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Destino);
            }
            index = 0;
            LTraspasoDetalle = new List<VTraspaso_01>();
            this.Dgv_Detalle.DataSource = null;           
            this.MP_AddFila();
        }    
        private void MP_ArmarEncabezado()
        {
            try
            {               
                Dgv_GBuscador.DataSource = LTraspaso;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;
                    
                Dgv_GBuscador.RootTable.Columns["Id"].Visible = true;        

                Dgv_GBuscador.RootTable.Columns[1].Visible = false;

                Dgv_GBuscador.RootTable.Columns[2].Caption = "Almacen";
                Dgv_GBuscador.RootTable.Columns[2].Width = 120;
                Dgv_GBuscador.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[2].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[2].Visible = true;
                Dgv_GBuscador.RootTable.Columns[2].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[3].Visible = false;

                Dgv_GBuscador.RootTable.Columns[4].Caption = "Cliente";
                Dgv_GBuscador.RootTable.Columns[4].Width = 180;
                Dgv_GBuscador.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[4].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[4].Visible = true;
                Dgv_GBuscador.RootTable.Columns[4].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[5].Caption = "Fch Registro";
                Dgv_GBuscador.RootTable.Columns[5].Width = 100;
                Dgv_GBuscador.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[5].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[5].Visible = true;
                Dgv_GBuscador.RootTable.Columns[5].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[6].Caption = "Fch. Venta";
                Dgv_GBuscador.RootTable.Columns[6].Width = 100;
                Dgv_GBuscador.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[6].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[6].Visible = true;
                Dgv_GBuscador.RootTable.Columns[6].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[7].Caption = "Usuario";
                Dgv_GBuscador.RootTable.Columns[7].Width = 100;
                Dgv_GBuscador.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[7].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[7].Visible = true;
                Dgv_GBuscador.RootTable.Columns[7].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[8].Visible = false;
                Dgv_GBuscador.RootTable.Columns[9].Visible = false;

                Dgv_GBuscador.RootTable.Columns[10].Caption = "Observaciones";
                Dgv_GBuscador.RootTable.Columns[10].Width = 250;
                Dgv_GBuscador.RootTable.Columns[10].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[10].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[10].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[10].Visible = true;
                Dgv_GBuscador.RootTable.Columns[10].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[11].Visible = false;
                Dgv_GBuscador.RootTable.Columns[12].Visible = false;
                Dgv_GBuscador.RootTable.Columns[13].Visible = false;

                Dgv_GBuscador.RootTable.Columns[14].Caption = "Enc Entrega";
                Dgv_GBuscador.RootTable.Columns[14].Width = 150;
                Dgv_GBuscador.RootTable.Columns[14].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[14].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[14].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[14].Visible = true;
                Dgv_GBuscador.RootTable.Columns[14].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[15].Visible = false;

                Dgv_GBuscador.RootTable.Columns[16].Caption = "NIT";
                Dgv_GBuscador.RootTable.Columns[16].Width = 150;
                Dgv_GBuscador.RootTable.Columns[16].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[16].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[16].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[16].Visible = true;
                Dgv_GBuscador.RootTable.Columns[16].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns[17].Visible = false;


                //Habilitar filtradores
                Dgv_GBuscador.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                Dgv_GBuscador.FilterMode = FilterMode.Automatic;
                Dgv_GBuscador.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
                //Dgv_Buscardor.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;
                Dgv_GBuscador.GroupByBoxVisible = false;
                Dgv_GBuscador.VisualStyle = VisualStyle.Office2007;


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
                Double saldo, precioVenta, precioCosto;
                int idProducto, stock, contenido; 
               // string Producto, codPrducto, unidadVenta;
                saldo = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value);
                stock = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Stock"].Value);
                precioVenta = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["PrecioVenta"].Value);
                precioCosto = Convert.ToDouble(Dgv_Detalle.CurrentRow.Cells["PrecioCosto"].Value);
                contenido = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Contenido"].Value);
                if (saldo > stock)
                {
                    //var idDetalle = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Id"].Value);
                    //idProducto = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["IdProducto"].Value);
                    //var InventarioLotes = new ServiceDesktop.ServiceDesktopClient().TraerInventarioLotes(idProducto, Convert.ToInt32(Cb_Origen.Value)).ToList();
                    //MP_ActualizarLote2(ref InventarioLotes, idProducto, idDetalle);
                    //var sumaStockTotal = InventarioLotes.Sum(a => a.Cantidad);
                    //if ((decimal)saldo <= sumaStockTotal)
                    //{
                    //    Producto = Dgv_DetalleVenta.CurrentRow.Cells["Producto"].Value.ToString();
                    //    codPrducto = Dgv_DetalleVenta.CurrentRow.Cells["CodigoProducto"].Value.ToString();
                    //    unidadVenta = Dgv_DetalleVenta.CurrentRow.Cells["Unidad"].Value.ToString();
                    //    foreach (var fila in InventarioLotes)
                    //    {
                    //        if (saldo > 0)
                    //        {
                    //            if ((Double)fila.Cantidad >= saldo)
                    //            {
                    //                IngresarCantidadLote(idProducto, Producto, codPrducto, unidadVenta, saldo,
                    //                                     precioVenta, precioCosto, fila.Lote, fila.FechaVen);

                    //                saldo = 0;
                    //            }
                    //            else//Si el inventario es menor
                    //            {
                    //                IngresarCantidadLote(idProducto, Producto, codPrducto, unidadVenta, (Double)fila.Cantidad,
                    //                                    precioVenta, precioCosto, fila.Lote, fila.FechaVen);
                    //                saldo -= (Double)fila.Cantidad;
                    //            }
                    //            if (saldo > 0)
                    //            {
                    //                MP_AddFila();
                    //                Dgv_DetalleVenta.Row = Dgv_DetalleVenta.RowCount - 1;
                    //            }
                    //        }                                                   
                    //    }
                    //}
                    //else
                    //{
                    //    Dgv_DetalleVenta.CurrentRow.Cells["Cantidad"].Value = 1;
                    //    Dgv_DetalleVenta.CurrentRow.Cells["SubTotal"].Value = precioVenta;
                    //    Dgv_DetalleVenta.CurrentRow.Cells["subTotalCosto"].Value = precioCosto;
                    //    throw new Exception("No existe stock para algun producto");
                    //}

                    Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value = 1;
                    Dgv_Detalle.CurrentRow.Cells["Contenido"].Value = contenido;
                    Dgv_Detalle.CurrentRow.Cells["TotalContenido"].Value = contenido;
                    Dgv_Detalle.CurrentRow.Cells["SubTotal"].Value = precioVenta;

                    Dgv_Detalle.CurrentRow.Cells["subTotalCosto"].Value = precioCosto;
                    throw new Exception("No existe stock para algun producto");
                }
                else
                {
                    IngresarCantidad(saldo, precioVenta, precioCosto,contenido);
                }
                MP_ObtenerCalculo();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void IngresarCantidad(double cantidad, double precioVenta, double precioCosto, int contenido)
        {
            var totalUnidad = cantidad * contenido;
            var subTotal = cantidad * precioVenta;
            var subTotalCosto = cantidad * precioCosto;
            Dgv_Detalle.CurrentRow.Cells["SubTotal"].Value = subTotal;
            Dgv_Detalle.CurrentRow.Cells["TotalContenido"].Value = totalUnidad;
            Dgv_Detalle.CurrentRow.Cells["subTotalCosto"].Value = subTotalCosto;
        }
        private void IngresarCantidadLote(int idProducto,string Producto, string codigoProducto, string unidadVenta,
                                            double cantidad, double precioVenta, double precioCosto, 
                                            string Lote, DateTime fechaVecnmiento)
        {
            var subTotal = cantidad * precioVenta;
            var subTotalCosto = cantidad * precioCosto;
            Dgv_Detalle.CurrentRow.Cells["IdProducto"].Value = idProducto;
            Dgv_Detalle.CurrentRow.Cells["Producto"].Value = Producto;
            Dgv_Detalle.CurrentRow.Cells["CodigoProducto"].Value = codigoProducto;
            Dgv_Detalle.CurrentRow.Cells["Unidad"].Value = unidadVenta;
            Dgv_Detalle.CurrentRow.Cells["Cantidad"].Value = cantidad;
            Dgv_Detalle.CurrentRow.Cells["PrecioVenta"].Value = precioVenta;
            Dgv_Detalle.CurrentRow.Cells["SubTotal"].Value = subTotal;
            Dgv_Detalle.CurrentRow.Cells["PrecioCosto"].Value = precioCosto;
            Dgv_Detalle.CurrentRow.Cells["subTotalCosto"].Value = subTotalCosto;
            Dgv_Detalle.CurrentRow.Cells["Lote"].Value = Lote;
            Dgv_Detalle.CurrentRow.Cells["FechaVencimiento"].Value = fechaVecnmiento;
            Dgv_Detalle.UpdateData();
        }
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_Detalle.UpdateData();               
                Tb_Total.Value = Convert.ToDouble(Dgv_Detalle.GetTotal(Dgv_Detalle.RootTable.Columns["TotalContenido"], AggregateFunction.Sum));
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_CargarProducto(List<VProductoListaStock> lProductosConStock)
        {
            try
            {
                Dgv_Producto.DataSource = lProductosConStock;
                Dgv_Producto.RetrieveStructure();
                Dgv_Producto.AlternatingColors = true;
                Dgv_Producto.RootTable.Columns["IdProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["IdAlmacen"].Visible = false;
                Dgv_Producto.RootTable.Columns["IdCategoriaPrecio"].Visible = false;

                Dgv_Producto.RootTable.Columns["CodigoProducto"].Caption = "Codigo Producto";
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Width = 80;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["CodigoBarras"].Visible = false;

                Dgv_Producto.RootTable.Columns["Producto"].Caption = "Producto";
                Dgv_Producto.RootTable.Columns["Producto"].Width = 180;
                Dgv_Producto.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Producto"].Visible = true;

                Dgv_Producto.RootTable.Columns["MarcaProducto"].Caption = "Marca";
                Dgv_Producto.RootTable.Columns["MarcaProducto"].Width = 100;
                Dgv_Producto.RootTable.Columns["MarcaProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["MarcaProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["MarcaProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["MarcaProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["TipoProducto"].Caption = "Tipo";
                Dgv_Producto.RootTable.Columns["TipoProducto"].Width = 100;
                Dgv_Producto.RootTable.Columns["TipoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["TipoProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["TipoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["TipoProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["CategoriaProducto"].Caption = "Categoria";
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].Width = 100;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["UnidadVenta"].Caption = "UN.";
                Dgv_Producto.RootTable.Columns["UnidadVenta"].Width = 60;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["UnidadVenta"].Visible = true;

                Dgv_Producto.RootTable.Columns["PrecioVenta"].Caption = "Precio";
                Dgv_Producto.RootTable.Columns["PrecioVenta"].Width = 100;
                Dgv_Producto.RootTable.Columns["PrecioVenta"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["PrecioVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["PrecioVenta"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["PrecioVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["PrecioVenta"].Visible = true;

                Dgv_Producto.RootTable.Columns["PrecioCosto"].Caption = "PrecioCosto";
                Dgv_Producto.RootTable.Columns["PrecioCosto"].Width = 100;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["PrecioCosto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["PrecioCosto"].Visible = false;

                Dgv_Producto.RootTable.Columns["Stock"].Caption = "Stock";
                Dgv_Producto.RootTable.Columns["Stock"].Width = 100;
                Dgv_Producto.RootTable.Columns["Stock"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["Stock"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["Stock"].Visible = true;

                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].Caption = "CategoriaPrecio";
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].Width = 120;
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].Visible = true;

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
        private void MP_InsertarProducto()
        {
            try
            {

                GPanel_Producto.Text = "SELECCIONE PRODUCTOS";
                var almacen = new ServiceDesktop.ServiceDesktopClient()
                                                        .AlmacenListar()
                                                        .ToList()
                                                        .Find(a => a.Id == Convert.ToInt32(Cb_Origen.Value));

                var lProductosConStock = new ServiceDesktop.ServiceDesktopClient().ListarProductosStock(almacen.SucursalId, almacen.Id,Convert.ToInt32( Cb_CatPrecio.Value)).ToList();
                MP_CargarProducto(lProductosConStock);
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
                Dgv_Producto.Col = 5;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        //REVISAR
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
                        LTraspasoDetalle = ((List<VTraspaso_01>)Dgv_Detalle.DataSource).ToList();
                        var lista = LTraspasoDetalle.Where(t => t.Id == idDetalle).FirstOrDefault();
                        LTraspasoDetalle.Remove(lista);
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
        private void MP_Filtrar(int tipo)
        {
            MP_ArmarEncabezado();
            if (Dgv_GBuscador.RowCount > 0)
            {
                index = 0;
                MP_MostrarRegistro(tipo == 1 ? index : Dgv_GBuscador.RowCount - 1);
            }
            else
            {
                MP_Reiniciar();
                LblPaginacion.Text = "0/0";
            }
        }
        private void MP_AddFila()
        {
            try
            {
                var fechaVencimiento = Convert.ToDateTime("2017-01-01");
                int idDetalle = ((List<VTraspaso_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VTraspaso_01>)Dgv_Detalle.DataSource).Max(c => c.Id);
                int posicion = ((List<VTraspaso_01>)Dgv_Detalle.DataSource) == null ? 0 : ((List<VTraspaso_01>)Dgv_Detalle.DataSource).Count;
                VTraspaso_01 nuevo = new VTraspaso_01()
                {
                    Id = idDetalle + 1,
                    TraspasoId = 0,
                    ProductoId = 0,
                    Estado = 0,
                    CodigoProducto = "",            
                    Producto = "",
                    Unidad = "",
                    Cantidad = 1,                   
                    Lote = "20170101",
                    FechaVencimiento = fechaVencimiento,
                    Stock = 0,
                    Delete = null
                };
                LTraspasoDetalle.Insert(posicion, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        private static void MP_IngresarProductoDetalle(int idDetalle, int idLote, List<VTI001> lLotes)
        {
            if (lLotes.Count != 0)
            {
                foreach (var vDetalleVenta in LTraspasoDetalle)
                {
                    if (vProductos.IdProducto == vDetalleVenta.ProductoId &&
                        lLotes.FirstOrDefault(a => a.id == idLote).Lote == vDetalleVenta.Lote &&
                        lLotes.FirstOrDefault(a => a.id == idLote).FechaVen == vDetalleVenta.FechaVencimiento)
                    {
                        throw new Exception("El producto ya existe en el detalle");
                    }
                    if (vDetalleVenta.Id == idDetalle)
                    {
                        vDetalleVenta.ProductoId = vProductos.IdProducto;
                        vDetalleVenta.CodigoProducto = vProductos.CodigoProducto;
                        vDetalleVenta.Producto = vProductos.Producto;
                        vDetalleVenta.Unidad = vProductos.UnidadVenta;
                        vDetalleVenta.Cantidad = 1;
                        vDetalleVenta.Contenido = Convert.ToDecimal(vProductos.Contenido);
                        vDetalleVenta.TotalContenido = Convert.ToDecimal(vProductos.Contenido);            
                        vDetalleVenta.Lote = lLotes.FirstOrDefault(a => a.id == idLote).Lote;
                        vDetalleVenta.FechaVencimiento = lLotes.FirstOrDefault(a => a.id == idLote).FechaVen;
                        //Revisar
                        vDetalleVenta.Stock = lLotes.FirstOrDefault(a => a.id == idLote).Cantidad;
                        break;
                    }
                }
                vProductos = new VProductoListaStock();
            }
        }

        private void MP_ActualizarLote(ref List<VTI001> Lotes, int idProducto)
        {
            foreach (var fila in Lotes)
            {
                var sumaCantidad = LTraspasoDetalle.Where(a => a.Lote.Equals(fila.Lote) &&
                                                              a.FechaVencimiento == fila.FechaVen &&
                                                              a.ProductoId == idProducto &&
                                                              a.Estado == 0).Sum(a => a.Cantidad);
                fila.Cantidad = fila.Cantidad - sumaCantidad;
            }
        }
        private void MP_ActualizarLote2(ref List<VTI001> Lotes, int idProducto, int idDetalle)
        {
            var idDetalleActual = Convert.ToInt32(Dgv_Detalle.CurrentRow.Cells["Id"].Value);

            foreach (var fila in Lotes)
            {

                var sumaCantidad = LTraspasoDetalle.Where(a => a.Lote.Equals(fila.Lote) &&
                                                          a.FechaVencimiento == fila.FechaVen &&
                                                          a.ProductoId == idProducto &&
                                                          a.Estado == 0 &&
                                                          a.Id != idDetalle).Sum(a => a.Cantidad);
                fila.Cantidad = fila.Cantidad - sumaCantidad;

            }
        }
        #endregion
        #region Eventos

        #endregion
        #region Metodos Heredados

        #endregion
    }
}
