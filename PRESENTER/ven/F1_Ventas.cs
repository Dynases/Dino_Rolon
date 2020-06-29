using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using ENTITY.inv.TI001.VIew;
using ENTITY.Plantilla;
using ENTITY.Producto.View;
using ENTITY.ven.view;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using PRESENTER.Properties;
using PRESENTER.reg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;

namespace PRESENTER.ven
{
    public partial class F1_Ventas : MODEL.ModeloF1
    {
        public F1_Ventas()
        {
            InitializeComponent();
            this.MP_CargarBuscador();
            this.MP_InHabilitar();
            this.MP_ArmarCombo();
            this.MP_CargarAlmacenes();
            this.MP_CargarVentas();
            this.TxtNombreUsu.Visible = false;        
            superTabControl1.SelectedTabIndex = 0;
            listaDetalleVenta = new List<VVenta_01>();
            MP_AsignarPermisos();
        }

        #region Variables de Entorno

        //public static List<Producto> detalleProductos;
        private static List<VVenta> listaVentas;   
        private static List<VVenta_01> listaDetalleVenta;
        private static VProductoListaStock vProductos;
        private static int index;
        private static int idCategoriaPrecio;
        private bool _Limpiar = false;
        #endregion

        #region Metodos Privados
        void MP_ArmarCombo()
        {
            UTGlobal.MG_ArmarComboClientes(cb_Cliente,
                                  new ServiceDesktop.ServiceDesktopClient().TraerClienteCombo().ToList(), ENEstado.NOCARGARPRIMERFILA);
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
            this.lblId.Visible = false;
        
            this.Dt_FechaVenta.Enabled = false;
            this.Tb_Cod.Enabled = false;
            this.Tb_Usuario.ReadOnly = true;
            this.cb_Cliente.ReadOnly = true;
            this.sw_estado.Enabled = false;
            this.Sw_TipoVenta.Enabled = false;
            this.TbEncVenta.ReadOnly = true;
            this.TbEncEntrega.ReadOnly = true;
            this.TbEncTransporte.ReadOnly = true;
            this.TbEncRecepcion.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;
            this.TbEncPrVenta.ReadOnly = true;
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = false;
            this.btnNuevoCliente.Visible = false;
            this.TbNumFacturaExterna.ReadOnly = true;
            this.TbEmpresaProveedoraCliente.ReadOnly = true;
            this.Cb_Origen.ReadOnly = true;
            _Limpiar = false;
        }

        private void MP_Habilitar()
        {
            this.lblFechaRegistro.Visible = false;
            this.Dt_FechaVenta.Enabled = true;
            this.cb_Cliente.ReadOnly = false;
            this.sw_estado.Enabled = true;
            this.Sw_TipoVenta.Enabled = true;
            this.TbEncVenta.ReadOnly = false;
            this.TbEncEntrega.ReadOnly = false;
            this.TbEncTransporte.ReadOnly = false;
            this.TbEncRecepcion.ReadOnly = false;
            this.Tb_Observaciones.ReadOnly = false;
            this.TbEncPrVenta.ReadOnly = false;
            this.Tb_Usuario.Text = UTGlobal.Usuario;
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = true;
            this.btnNuevoCliente.Visible = true;
            this.TbNumFacturaExterna.ReadOnly = false;
            this.Cb_Origen.ReadOnly = false;
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo().ToList();
                UTGlobal.MG_ArmarComboAlmacen(Cb_Origen, almacenes);

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }
        private void MP_InHabilitarProducto()
        {
            try
            {
                GPanel_Producto.Visible = false;
                GPanel_Producto.Height = 30;
                Dgv_DetalleVenta.Select();
                Dgv_DetalleVenta.Col = 5;
                Dgv_DetalleVenta.Row = Dgv_DetalleVenta.RowCount - 1;
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }       

        private void MP_CargarVentas()
        {
            index = 0;
            try
            {
                listaVentas = new ServiceDesktop.ServiceDesktopClient().TraerVentas().ToList();
                if (listaVentas != null && listaVentas.Count > 0)
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
            var venta = listaVentas[index];

            Tb_Cod.Text = venta.Id.ToString();
            lblId.Text = venta.Id.ToString();
            Cb_Origen.SelectedItem = venta.DescripcionAlmacen;
            Dt_FechaVenta.Value = venta.FechaVenta;
            Tb_Usuario.Text = venta.Usuario;
            cb_Cliente.Value = venta.IdCliente;          
            TbNitCliente.Text = venta.NitCliente;
            Sw_TipoVenta.Value = true;
            sw_estado.Value = true;
            TbEncVenta.Text = venta.EncVenta;
            TbEncEntrega.Text = venta.EncEntrega;
            TbEncPrVenta.Text = venta.EncPrVenta;
            TbEncRecepcion.Text = venta.EncRecepcion;
            TbEncTransporte.Text = venta.EncTransporte;
            Tb_Observaciones.Text = venta.Observaciones;
            lblFechaRegistro.Text = venta.Fecha.ToString();
            lblHora.Text = venta.Hora;
            lblUsuario.Text = venta.Usuario;
            idCategoriaPrecio = venta.IdCategoriaCliente;
            this.MP_CargarDetalleRegistro(venta.Id);
            this.LblPaginacion.Text = (index + 1) + "/" + Dgv_GBuscador.RowCount.ToString() + " Ventas";
        }

        private void MP_CargarDetalleRegistro(int id)
        {
            try
            {
                listaDetalleVenta = new ServiceDesktop.ServiceDesktopClient().VentaDetalleListar(id).ToList();
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
            Dgv_Producto.VisualStyle = VisualStyle.Office2007;
            Dgv_Producto.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_Producto.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));

        }
        private void MP_ArmarDetalle()
        {
            Dgv_DetalleVenta.DataSource = listaDetalleVenta;
            Dgv_DetalleVenta.RetrieveStructure();
            Dgv_DetalleVenta.AlternatingColors = true;

            Dgv_DetalleVenta.RootTable.Columns["Id"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["IdVenta"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["IdProducto"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["Estado"].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Caption = "CODIGO";
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["CodigoBarra"].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns["Producto"].Caption = "PRODUCTO";
            Dgv_DetalleVenta.RootTable.Columns["Producto"].Width = 260;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Caption = "UN.";
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Width = 130;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Caption = "PRECIO";
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Width = 130;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Caption = "TOTAL";
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Width = 130;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["PrecioCosto"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["SubTotalCosto"].Visible = false;


            Dgv_DetalleVenta.RootTable.Columns["Lote"].Caption = "LOTE";
            Dgv_DetalleVenta.RootTable.Columns["Lote"].Width = 130;
            Dgv_DetalleVenta.RootTable.Columns["Lote"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Lote"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Lote"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Lote"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].Caption = "FechaVencimiento";
            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].Width = 130;
            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["FechaVencimiento"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Stock"].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns["Delete"].Caption = "ELIMINAR";
            Dgv_DetalleVenta.RootTable.Columns["Delete"].Width = 80;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].Visible = true;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].Image = Resources.delete;
            Dgv_DetalleVenta.RootTable.Columns["Delete"].EditType = EditType.NoEdit;

            Dgv_DetalleVenta.GroupByBoxVisible = false;
            Dgv_DetalleVenta.VisualStyle = VisualStyle.Office2007;
            Dgv_DetalleVenta.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_DetalleVenta.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));

        }
        private void MP_Reiniciar()
        {
            this.Tb_Cod.Text = "";
            this.Tb_Usuario.Text = UTGlobal.Usuario;
            this.Dt_FechaVenta.Value = DateTime.Today;         
            this.Sw_TipoVenta.Value = true;
            this.sw_estado.Value = true;
            this.TbEncEntrega.Text = "";
            this.TbEncPrVenta.Text = "";
            this.TbEncRecepcion.Text = "";
            this.TbEncTransporte.Text = "";
            this.TbEncVenta.Text = "";
            this.Tb_Observaciones.Text = "";
            this.lblFechaRegistro.Text = "";
            this.lblId.Text = "";
            this.TbNitCliente.Text = "";
            this.TbTotal.Text = "";
            if (_Limpiar == false)            {
                
                UTGlobal.MG_SeleccionarComboCliente(cb_Cliente);
                // UTGlobal.MG_SeleccionarCombo(Cb_Almacen);
            }
            index = 0;
            listaDetalleVenta.Clear();
            this.Dgv_DetalleVenta.DataSource = null;
            this.MP_CargarEncargado(Convert.ToInt32(Cb_Origen.Value));
            this.MP_AddFila();
        }
     

        private void MP_CargarEncargado(int almacenOrigen)
        {


            var almacen = new ServiceDesktop.ServiceDesktopClient().AlmacenListar()
                                                                       .ToList()
                                                                       .Where(a => a.Id == almacenOrigen)
                                                                       .FirstOrDefault();
            if (almacen != null)
            {
                TbEncEntrega.Text = almacen.Encargado;
            }
        }

        private void MP_CargarBuscador()
        {
            try
            {
                listaVentas = new ServiceDesktop.ServiceDesktopClient().TraerVentas().ToList();
                Dgv_GBuscador.DataSource = listaVentas;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns[0].Caption = "COD";
                Dgv_GBuscador.RootTable.Columns[0].Width = 60;
                Dgv_GBuscador.RootTable.Columns[0].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns[0].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns[0].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns[0].Visible = true;
                Dgv_GBuscador.RootTable.Columns[0].EditType = EditType.NoEdit;

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
                Double cantidad, precioVenta, subTotal, subTotalCosto, precioCosto;
                cantidad = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["Cantidad"].Value);
                precioVenta = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioVenta"].Value);
                precioCosto = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioCosto"].Value);
                subTotal = cantidad * precioVenta;
                subTotalCosto = cantidad * precioCosto;
                Dgv_DetalleVenta.CurrentRow.Cells["SubTotal"].Value = subTotal;
                Dgv_DetalleVenta.CurrentRow.Cells["subTotalCosto"].Value = subTotalCosto;
                MP_ObtenerCalculo();
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
                Dgv_DetalleVenta.UpdateData();
                var total = Convert.ToDouble(Dgv_DetalleVenta.GetTotal(Dgv_DetalleVenta.RootTable.Columns["SubTotal"], AggregateFunction.Sum));
                TbTotal.Text = total.ToString();
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

                var lProductosConStock = new ServiceDesktop.ServiceDesktopClient().ListarProductosStock(almacen.SucursalId, almacen.Id, idCategoriaPrecio).ToList();
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
        private void MP_VerificarSeleccion(string columna)
        {
            try
            {
                if (Dgv_DetalleVenta.Col == Dgv_DetalleVenta.RootTable.Columns[columna].Index)
                {
                    if (Dgv_DetalleVenta.GetValue("Producto").ToString() != string.Empty && Dgv_DetalleVenta.GetValue("IdProducto").ToString() != string.Empty)
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
                if (Dgv_DetalleVenta.RowCount > 1)
                {
                    Dgv_DetalleVenta.UpdateData();
                    int estado = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Estado"].Value);
                    int idDetalle = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Id"].Value);
                    if (estado == (int)ENEstado.NUEVO)
                    {
                        //Elimina
                        listaDetalleVenta = ((List<VVenta_01>)Dgv_DetalleVenta.DataSource).ToList();
                        var lista = listaDetalleVenta.Where(t => t.Id == idDetalle).FirstOrDefault();
                        listaDetalleVenta.Remove(lista);
                        MP_ArmarDetalle();
                    }
                    else
                    {
                        if (estado == (int)ENEstado.GUARDADO || estado == (int)ENEstado.MODIFICAR)
                        {
                            Dgv_DetalleVenta.CurrentRow.Cells["Estado"].Value = (int)ENEstado.ELIMINAR;
                            Dgv_DetalleVenta.UpdateData();
                            Dgv_DetalleVenta.RootTable.ApplyFilter(new Janus.Windows.GridEX.GridEXFilterCondition(Dgv_DetalleVenta.RootTable.Columns["Estado"], Janus.Windows.GridEX.ConditionOperator.NotEqual, -1));
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
            MP_CargarBuscador();
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

        #endregion
        private void MP_AddFila()
        {
            try
            {
                var fechaVencimiento = Convert.ToDateTime("2017-01-01");
                int idDetalle = ((List<VVenta_01>)Dgv_DetalleVenta.DataSource) == null ? 0 : ((List<VVenta_01>)Dgv_DetalleVenta.DataSource).Max(c => c.Id);
                int posicion = ((List<VVenta_01>)Dgv_DetalleVenta.DataSource) == null ? 0 : ((List<VVenta_01>)Dgv_DetalleVenta.DataSource).Count;
                VVenta_01 nuevo = new VVenta_01()
                {
                    Id = idDetalle + 1,
                    IdVenta = 0,
                    IdProducto = 0,
                    Estado = 0,
                    CodigoProducto = "",
                    CodigoBarra = "",
                    Producto = "",
                    Unidad = "",
                    Cantidad = 1,
                    PrecioVenta = 0,
                    PrecioCosto = 0,
                    Lote = "20170101",
                    FechaVencimiento = fechaVencimiento,
                    Stock = 0,
                    Delete = null
                };
                listaDetalleVenta.Insert(posicion, nuevo);
                MP_ArmarDetalle();
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_Reiniciar();
        }
        public override void MH_Modificar()
        {
            MP_Habilitar();
        }
        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_InHabilitar();
            this.MP_Reiniciar();
            this.MP_CargarAlmacenes();
            this.MP_CargarVentas();
        }

        public override bool MH_Validar()
        {
            var flag = true;

            if (cb_Cliente.SelectedIndex == -1)
            {
                cb_Cliente.BackColor = Color.Red;
                flag = true;
            }
            else
                cb_Cliente.BackColor = Color.White;
            if (this.Dgv_DetalleVenta.RowCount == 0 )
            {
                this.MP_MostrarMensajeError("Debe seleccionar productos para realizar una venta");
                return flag;
            }

            flag = false;
            return flag;
        }

        public override bool MH_NuevoRegistro()
        {
            try
            {
                var vVenta = new VVenta
                {
                    EncEntrega = this.TbEncEntrega.Text,
                    EncPrVenta = this.TbEncPrVenta.Text,
                    EncRecepcion = this.TbEncRecepcion.Text,
                    EncTransporte = this.TbEncTransporte.Text,
                    EncVenta = this.TbEncVenta.Text,
                    Estado = (int)ENEstado.GUARDADO,
                    FechaVenta = this.Dt_FechaVenta.Value,
                    IdAlmacen = Convert.ToInt32(this.Cb_Origen.Value),
                    IdCliente = Convert.ToInt32(this.cb_Cliente.Value),
                    Observaciones = this.Tb_Observaciones.Text,
                    Tipo = 1,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = lblUsuario.Text
                };
                int id = Tb_Cod.Text == string.Empty ? 0 : Convert.ToInt32(Tb_Cod.Text);
                int idAux = id;
                var detalle = ((List<VVenta_01>)Dgv_DetalleVenta.DataSource).ToArray<VVenta_01>();
                List<string> Mensaje = new List<string>();
                var LMensaje = Mensaje.ToArray();
                try
                {                  
                    if (new ServiceDesktop.ServiceDesktopClient().VentaGuardar(vVenta, detalle, ref id, ref LMensaje))
                    {
                        if (idAux == 0)//Registar
                        {
                           
                            this.MP_Filtrar(1);
                            this.MP_Reiniciar();
                            _Limpiar = true;
                            ToastNotification.Show(this, GLMensaje.Nuevo_Exito("VENTAS", id.ToString()), Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                            return true;
                        }
                        else
                        {
                            this.MP_Filtrar(2);
                            this.MP_InHabilitar();
                            _Limpiar = false;
                            MH_Habilitar();//El menu  
                            ToastNotification.Show(this, GLMensaje.Modificar_Exito("VENTAS", id.ToString()), Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                            return true;
                        }                      

                    }
                    else
                    {
                        this.MP_MostrarMensajeError("Ocurrio un error al guardar sus datos");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    this.MP_MostrarMensajeError(ex.Message);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
                return false;
            }
        }
        public override bool MH_Eliminar()
        {
            try
            {
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
                    resul = new ServiceDesktop.ServiceDesktopClient().VentaModificarEstado(Convert.ToInt32(Tb_Cod.Text), (int)ENEstado.ELIMINAR, ref LMensaje);
                    if (resul)
                    {
                        MP_Filtrar(1);
                        MP_MostrarMensajeExito(GLMensaje.Eliminar_Exito("Venta", Tb_Cod.Text));
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
                            MP_MostrarMensajeError(GLMensaje.Eliminar_Error("Venta", Tb_Cod.Text));
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
        #endregion

        #region Eventos

        private void F1_Ventas_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "VENTAS";
            btnMax.Visible = false;
            superTabControl1.SelectedPanel = PanelContenidoRegistro;
        }       
        private void Dgv_DetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Origen.Enabled == true)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        MP_VerificarSeleccion("CodigoProducto");
                        MP_VerificarSeleccion("Producto");
                        MP_VerificarSeleccion("Cantidad");
                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter &&
                        Dgv_DetalleVenta.Row >= 0)
                    {
                        if (idCategoriaPrecio == 0)
                        {
                            throw new Exception("Seleccione un cliente.");
                        }

                        int estado = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Estado"].Value);
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
                        MP_EliminarFila();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Dgv_DetalleVenta_CellEdited(object sender, ColumnActionEventArgs e)
        {

            try
            {
                Dgv_DetalleVenta.UpdateData();
                int estado = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Estado"].Value);
                if (estado == (int)ENEstado.NUEVO || estado == (int)ENEstado.MODIFICAR)
                {
                    MP_CalcularFila();
                }
                else
                {
                    if (estado == (int)ENEstado.GUARDADO)
                    {
                        MP_CalcularFila();
                        Dgv_DetalleVenta.CurrentRow.Cells["Estado"].Value = (int)ENEstado.MODIFICAR;
                    }
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {            
            //this.TbNitCliente.Text = "";
            //idCategoriaPrecio = 0;
            //ToastNotification.Show(this, "El cliente fue borrado de la venta con exito", PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void Cb_Origen_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Cb_Origen.Value != null)
                {
                    int AlmacenOrigenId;
                    if (int.TryParse(Cb_Origen.Value.ToString(), out AlmacenOrigenId))
                    {
                        this.MP_CargarEncargado(AlmacenOrigenId);
                    }
                }

            }
            catch (Exception ex)
            {
                this.MP_MostrarMensajeError(ex.Message);
            }
        }     

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            index = 0;
            this.MP_MostrarRegistro(index);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index -= 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (index < listaVentas.Count - 1)
            {
                index += 1;
                this.MP_MostrarRegistro(index);
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            index = listaVentas.Count - 1;
            this.MP_MostrarRegistro(index);
        }

        private void Dgv_GBuscador_Click(object sender, EventArgs e)
        {
            if (this.Dgv_GBuscador.CurrentRow != null)
            {
                if (this.Dgv_GBuscador.CurrentRow.Cells[0].Value != null &&
                    !string.IsNullOrEmpty(this.Dgv_GBuscador.CurrentRow.Cells[0].Value.ToString()))
                {
                    this.MP_MostrarRegistro(listaVentas.FindIndex(v => v.Id == Convert.ToInt32(this.Dgv_GBuscador.CurrentRow.Cells[0].Value)));
                    this.superTabControl1.SelectedPanel = this.PanelContenidoRegistro;
                }
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            F1_Clientes frm = new F1_Clientes();
            frm.ShowDialog();
        }

        private void Dgv_Producto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Origen.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        var idDetalle = Convert.ToInt32(Dgv_DetalleVenta.GetValue("id"));
                        var cantidad = Convert.ToInt32(Dgv_DetalleVenta.GetValue("Cantidad"));
                        if (idDetalle > 0)
                        {
                            if (vProductos == null)
                            {

                                var idProducto = Convert.ToInt32(Dgv_Producto.GetValue("IdProducto"));
                                vProductos = ((List<VProductoListaStock>)Dgv_Producto.DataSource)
                                                                               .Where(p => p.IdProducto == idProducto)
                                                                               .FirstOrDefault();

                                var InventarioLotes = new ServiceDesktop.ServiceDesktopClient().TraerInventarioLotes(vProductos.IdProducto, Convert.ToInt32(Cb_Origen.Value)).ToList();
                                MP_ArmarLotes(InventarioLotes);
                                GPanel_Producto.Text = vProductos.Producto;
                            }
                            else
                            {
                                var idLote = Convert.ToInt32(Dgv_Producto.GetValue("id"));
                                var lLotes = ((List<VTI001>)Dgv_Producto.DataSource);
                                if (lLotes.Count != 0)
                                {
                                    if (cantidad <= lLotes.FirstOrDefault( a=> a.id == idLote).Cantidad)
                                    {

                                    }
                                    foreach (var vLotes in lLotes)
                                    {
                                        
                                    }
                                    listaDetalleVenta = (List<VVenta_01>)Dgv_DetalleVenta.DataSource;
                                    foreach (var vDetalleVenta in listaDetalleVenta)
                                    {
                                        if (vProductos.IdProducto == vDetalleVenta.IdProducto)
                                        {
                                            throw new Exception("El producto ya fue seleccionado");
                                        }
                                        if (vDetalleVenta.Id == idDetalle)
                                        {
                                            vDetalleVenta.IdProducto = vProductos.IdProducto;
                                            vDetalleVenta.CodigoProducto = vProductos.CodigoProducto;
                                            vDetalleVenta.CodigoBarra = vProductos.CodigoBarras;
                                            vDetalleVenta.Producto = vProductos.Producto;
                                            vDetalleVenta.Unidad = vProductos.UnidadVenta;
                                            vDetalleVenta.Cantidad = 1;
                                            vDetalleVenta.PrecioVenta = vProductos.PrecioVenta;
                                            vDetalleVenta.PrecioCosto = vProductos.PrecioCosto;
                                            vDetalleVenta.Lote = "20170101";
                                            vDetalleVenta.FechaVencimiento = Convert.ToDateTime("2017/01/01");
                                            vDetalleVenta.Stock = vDetalleVenta.Stock;
                                            break;
                                        }
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
        private void Dgv_DetalleVenta_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (Cb_Origen.ReadOnly == false)
                {
                    if (e.Column.Index == Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Index)
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

        private void Dgv_GBuscador_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (Dgv_GBuscador.Row >= 0 && Dgv_GBuscador.RowCount >= 0)
            {
                MP_MostrarRegistro(Dgv_GBuscador.Row);
            }
        }

        #endregion

        private void cb_Cliente_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cb_Cliente.ReadOnly == false)
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        if (cb_Cliente.SelectedIndex != -1)
                        {
                            var lista = new ServiceDesktop.ServiceDesktopClient().TraerClienteCombo().Where(a => a.Id.Equals(Convert.ToInt32(cb_Cliente.Value))).FirstOrDefault();
                            if (lista == null)
                            {
                                throw new Exception("No se encontro un cliente");
                            }                            
                            TbNitCliente.Text = lista.Nit;
                            idCategoriaPrecio = lista.IdCategoriaPrecio;
                            TbEmpresaProveedoraCliente.Text = lista.EmpresaProveedora;
                        }
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        var lista = new ServiceDesktop.ServiceDesktopClient().ClienteListarEncabezado();
                        List<GLCelda> listEstCeldas = new List<GLCelda>
                    {
                        new GLCelda() { campo = "Id", visible = true, titulo = "COD", tamano = 50 },
                        new GLCelda() { campo = "IdSpyre", visible = true, titulo = "CÓDIGO SPYRE", tamano = 80 },
                        new GLCelda() { campo = "Descrip", visible = true, titulo = "Nombre y Apellido", tamano = 150 },
                        new GLCelda() { campo = "RazonSo", visible = false, titulo = "Razon Social", tamano = 100 },
                        new GLCelda() { campo = "Nit", visible = true, titulo = "NIT", tamano = 100 },
                        new GLCelda() { campo = "Direcc", visible = true, titulo = "Direccion", tamano = 150 },
                        new GLCelda() { campo = "b.Descrip", visible = true, titulo = "Ciudad", tamano = 120 },
                        new GLCelda() { campo = "Factur", visible = true, titulo = "Facturacion", tamano = 100 },
                        new GLCelda() { campo = "IdCategoria", visible = false, titulo = "IdCategoria", tamano = 100 }
                    };

                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 350;
                        efecto.Context = "SELECCIONE UN CLIENTE";
                        efecto.ShowDialog();

                        var bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            GridEXRow Row = efecto.Row;
                            cb_Cliente.Value = Row.Cells[0].Value.ToString();                           
                            TbNitCliente.Text = Row.Cells[4].Value.ToString();
                            var libreria = new ServiceDesktop.ServiceDesktopClient().LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                                    Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE)).ToList();
                            TbEmpresaProveedoraCliente.Text = libreria.Where(l => l.IdLibreria == Convert.ToInt32(Row.Cells[7].Value)).FirstOrDefault().Descripcion;
                            idCategoriaPrecio = Convert.ToInt32(Row.Cells["IdCategoria"].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MP_MostrarMensajeError(ex.Message);
            }
        }
    }
}
