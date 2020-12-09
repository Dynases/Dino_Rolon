using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using ENTITY.Cliente.View;
using ENTITY.com.CompraIngreso.View;
using ENTITY.inv.TI001.VIew;
using ENTITY.Libreria.View;
using ENTITY.Plantilla;
using ENTITY.Producto.View;
using ENTITY.ven.view;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using PRESENTER.frm;
using PRESENTER.Properties;
using PRESENTER.reg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using UTILITY.Enum;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;
using UTILITY.Global;

namespace PRESENTER.ven
{
    public partial class F1_Ventas : MODEL.ModeloF1
    {
        string _NombreFormulario = "VENTAS";
        public F1_Ventas()
        {
            InitializeComponent();
            
        }

        #region Variables de Entorno        
        private static List<VVenta> listaVentas;   
        private static List<VVenta_01> listaDetalleVenta;
        private static VProductoListaStock vProductos = new VProductoListaStock();
        private static int index;
        private static int idCategoriaPrecio;
        private bool _Limpiar = false;
        public  int idCompraIngreso;
        #endregion

        #region Metodos Privados
 
        private void inicioDesdeCompraIngreso()
        {
            if (idCompraIngreso != 0)
            {
                MH_Inhanbilitar();
                using (var servicio = new ServiceDesktop.ServiceDesktopClient())
                {
                    var lCompaIngreso = servicio.TraerCompraIngreso(idCompraIngreso);
                    PasarDatosDeObjeto(lCompaIngreso);
                    MP_MostrarMensajeError("DEBE SELECCIONAR UN CLIENTE");
                    Dgv_DetalleVenta.Enabled = false;
                }
            }
        }
        private void PasarDatosDeObjeto(VCompraIngresoLista vCompraIngreso)
        {           
            MP_Reiniciar();
            MP_Habilitar();
            idCategoriaPrecio = (int)ENCategoriaPrecio.COSTO;
            var productos = MP_ObtenerProductos(vCompraIngreso.IdAlmacen, idCategoriaPrecio);
            Cb_Origen.Value = vCompraIngreso.IdAlmacen;
            Cb_Origen.ReadOnly = true;
            var productoId = productos.Where(x => x.Producto.Contains("SIN SELECCION")).ToList().First().IdProducto;
            var idDetalle = listaDetalleVenta.First().Id;
            Dgv_Producto.DataSource = productos;
            MP_ValidarLotesInsertarProductosDetalle(productoId, 0, idDetalle, Convert.ToInt32(vCompraIngreso.TotalVendido));
        }
        private void IngresarClienteConControlEnter()
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
                        new GLCelda() { campo = "Factur", visible = false, titulo = "Facturacion", tamano = 100 },
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
                cb_Cliente.Value = Convert.ToInt32(Row.Cells[0].Value);
                TbNitCliente.Text = Row.Cells["Nit"].Value.ToString();
                cbFacturaEmpresa.Value = Convert.ToInt32(Row.Cells["Facturacion"].Value);
                idCategoriaPrecio = Convert.ToInt32(Row.Cells["IdCategoria"].Value);
                ingresarMontoCreditoCliente(Convert.ToInt32(Row.Cells[0].Value));
            }
        }

        private void IngresarClienteConEnter()
        {
            var lista = new ServiceDesktop.ServiceDesktopClient().TraerClienteCombo().Where(a => a.Id.Equals(Convert.ToInt32(cb_Cliente.Value))).FirstOrDefault();
            if (lista == null)
            {
                throw new Exception("No se encontro un cliente");
            }
            TbNitCliente.Text = lista.Nit;
            idCategoriaPrecio = lista.IdCategoriaPrecio;
            cbFacturaEmpresa.Value = lista.FacturaEmpresa;
            ingresarMontoCreditoCliente(lista.Id);
        }

        private void ingresarMontoCreditoCliente(int clienteId)
        {
            using (var servicio = new ServiceDesktop.ServiceDesktopClient())
            {
                if (servicio.EsClienteCredito(clienteId))
                {
                    tbLimiteCred.Visible = true;
                    tbMontoDeuda.Visible = true;
                    tbMontoDisponible.Visible = true;
                    lblLimiteCred.Visible = true;
                    lblMontoDisponible.Visible = true;
                    lblMontoPagado.Visible = true;
                    Sw_TipoVenta.Value = false;
                    tbLimiteCred.Text = servicio.TraerLimiteCredito(clienteId).ToString();
                    tbMontoDeuda.Text = servicio.SaldoPendienteCredito(clienteId).ToString();
                    tbMontoDisponible.Text = Convert.ToString(Convert.ToDecimal(tbLimiteCred.Text) - Convert.ToDecimal(tbMontoDeuda.Text));
                    tbMontoCredito.Text = TbTotal.Text;
                }
                else
                {
                    tbLimiteCred.Visible = false;
                    tbMontoDeuda.Visible = false;
                    tbMontoDisponible.Visible = false;
                    lblLimiteCred.Visible = false;
                    lblMontoDisponible.Visible = false;
                    lblMontoPagado.Visible = false;
                    Sw_TipoVenta.Value = true;
                }
            }
        }

        void MP_ArmarCombo()
        {
            using (var servicio = new ServiceDesktop.ServiceDesktopClient())
            {
                UTGlobal.MG_ArmarComboClientes(cb_Cliente,servicio.TraerClienteCombo().ToList(), ENEstado.NOCARGARPRIMERFILA);  
                
                UTGlobal.MG_ArmarCombos(Cb_EncPreVenta, servicio.PersonalCombo().ToList());

                UTGlobal.MG_ArmarCombos(Cb_EncVenta, servicio.PersonalCombo().ToList());

                UTGlobal.MG_ArmarCombos(Cb_EncTransporte, servicio.PersonalCombo().ToList());

                UTGlobal.MG_ArmarCombo(cbFacturaEmpresa, servicio.LibreriaListarCombo(Convert.ToInt32(ENEstaticosGrupo.CLIENTE),
                                                                                      Convert.ToInt32(ENEstaticosOrden.FACTURACION_CLIENTE)).ToList());
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
                MP_MostrarMensajeError(ex.Message);
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
            this.lblId.Visible = false;
        
            this.Dt_FechaVenta.Enabled = false;
            this.Tb_Cod.Enabled = false;
            this.Tb_Usuario.ReadOnly = true;
            this.cb_Cliente.ReadOnly = true;
            this.sw_estado.Enabled = false;
            this.Sw_TipoVenta.IsReadOnly = true;
            cbFacturaEmpresa.ReadOnly = true;
            Cb_EncVenta.ReadOnly = true;
            Cb_EncPreVenta.ReadOnly = true;
            Cb_EncTransporte.ReadOnly = true;           
            this.TbEncEntrega.ReadOnly = true;           
            this.Tb_Observaciones.ReadOnly = true;           
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = false;
            this.btnNuevoCliente.Visible = false;
            this.TbNumFacturaExterna.ReadOnly = true;
            this.Cb_Origen.ReadOnly = true;
            this.tbTotalCantidad.ReadOnly = true;
            this.TbTotal.ReadOnly = true;
            BtnHabilitar.Enabled = true;
            tbLimiteCred.ReadOnly = true;
            tbMontoCredito.ReadOnly = true;
            tbMontoDisponible.ReadOnly = true;
            _Limpiar = false;
        }

        private void MP_Habilitar()
        {
            cbFacturaEmpresa.ReadOnly = false;
            Cb_EncVenta.ReadOnly = false;
            Cb_EncPreVenta.ReadOnly = false;
            Cb_EncTransporte.ReadOnly = false;
            this.lblFechaRegistro.Visible = false;
            this.Dt_FechaVenta.Enabled = true;
            this.cb_Cliente.ReadOnly = false;
            this.sw_estado.Enabled = true;                   
            this.TbEncEntrega.ReadOnly = false;           
            this.Tb_Observaciones.ReadOnly = false;           
            this.Tb_Usuario.Text = UTGlobal.Usuario;
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = true;
            this.btnNuevoCliente.Visible = true;
            this.TbNumFacturaExterna.ReadOnly = false;
            this.Cb_Origen.ReadOnly = false;
            tbLimiteCred.ReadOnly = false;
            tbMontoCredito.ReadOnly = false;
            tbMontoDisponible.ReadOnly = false;
            BtnHabilitar.Enabled = false;
        }

        private void MP_CargarAlmacenes()
        {
            try
            {
                var almacenes = new ServiceDesktop.ServiceDesktopClient().AlmacenListarCombo(UTGlobal.UsuarioId).ToList();
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
                vProductos = new VProductoListaStock();
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
                listaVentas = new ServiceDesktop.ServiceDesktopClient().TraerVentas(UTGlobal.UsuarioId).ToList();
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
            try
            {
                var venta = listaVentas[index];

                Tb_Cod.Text = venta.Id.ToString();
                lblId.Text = venta.Id.ToString();
                Cb_Origen.Value = venta.IdAlmacen;
                //Cb_Origen.SelectedItem = venta.DescripcionAlmacen;
                Dt_FechaVenta.Value = venta.FechaVenta;
                Tb_Usuario.Text = venta.Usuario;
                cb_Cliente.Value = venta.IdCliente;
                TbNitCliente.Text = venta.NitCliente;
                // Sw_TipoVenta.Value = true;
                sw_estado.Value = true;
                cbFacturaEmpresa.Value = venta.FacturaEmpresa;
                Cb_EncVenta.Value = venta.EncVenta;
                Cb_EncPreVenta.Value = venta.EncPrVenta;
                Cb_EncTransporte.Value = venta.EncTransporte;
                TbEncEntrega.Text = venta.EncEntrega;
                Tb_Observaciones.Text = venta.Observaciones;
                lblFechaRegistro.Text = venta.Fecha.ToString();
                lblHora.Text = venta.Hora;
                lblUsuario.Text = venta.Usuario;
                idCategoriaPrecio = venta.IdCategoriaCliente;
                if (venta.IdCompraIngreso != 0)
                {
                    LblCompraIngreso.Visible = true;
                    LblIdCompaIngreso.Visible = true;
                    LblIdCompaIngreso.Text = venta.IdCompraIngreso.ToString();
                }
                else
                {
                    LblCompraIngreso.Visible = false;
                    LblIdCompaIngreso.Visible = false;
                }
                this.MP_CargarDetalleRegistro(venta.Id, 1);
                this.MP_ObtenerCalculo();
                this.LblPaginacion.Text = (index + 1) + "/" + Dgv_GBuscador.RowCount.ToString() + " Ventas";
                ingresarMontoCreditoCliente(venta.IdCliente);
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
          
        }

        private void MP_CargarDetalleRegistro(int id, int EsPlantilla)
        {
            try
            {
                if (EsPlantilla == 1)
                {
                    listaDetalleVenta = new ServiceDesktop.ServiceDesktopClient().VentaDetalleListar(id).ToList();
                }
                else
                    listaDetalleVenta = new ServiceDesktop.ServiceDesktopClient().TraerDetalleVentaVacio(id).ToList();

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
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Width = 80;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns["CodigoProducto"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["CodigoBarra"].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns["Producto"].Caption = "PRODUCTO";
            Dgv_DetalleVenta.RootTable.Columns["Producto"].Width = 200;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns["Producto"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Caption = "UN.";
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Width = 70;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Unidad"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Caption = "CANTIDAD";
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Width = 110;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["Contenido"].Caption = "CONT.";
            Dgv_DetalleVenta.RootTable.Columns["Contenido"].Width = 110;
            Dgv_DetalleVenta.RootTable.Columns["Contenido"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["Contenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["Contenido"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["Contenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].Caption = "TOTAL UN.";
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].Width = 110;
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["TotalContenido"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Caption = "PRECIO";
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Visible = true;


            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Caption = "TOTAL";
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns["SubTotal"].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns["PrecioCosto"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["PrecioMinVenta"].Visible = false;
            Dgv_DetalleVenta.RootTable.Columns["PrecioMaxVenta"].Visible = false;
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
            Dgv_DetalleVenta.RootTable.Columns["Delete"].Width = 60;
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
            this.TbEncEntrega.Clear();

            this.Tb_Observaciones.Clear();
            this.lblFechaRegistro.Text = "";
            this.lblId.Text = "";
            this.TbNitCliente.Clear();
            this.TbTotal.Text = "0";
            this.TbNumFacturaExterna.Clear();
            LblCompraIngreso.Visible = false;
            LblIdCompaIngreso.Visible = false;
            LblIdCompaIngreso.Text = "0";
            tbLimiteCred.Text = "0";
            tbMontoDeuda.Text = "0";
            tbMontoDisponible.Text = "0";
            tbTotalCantidad.Text = "0";
            Dgv_DetalleVenta.Enabled = true;
            if (_Limpiar == false)    
            {                
                UTGlobal.MG_SeleccionarComboCliente(cb_Cliente);
                UTGlobal.MG_SeleccionarCombo(cbFacturaEmpresa);
                UTGlobal.MG_SeleccionarCombos(Cb_EncVenta);
                UTGlobal.MG_SeleccionarCombos(Cb_EncPreVenta);
                UTGlobal.MG_SeleccionarCombos(Cb_EncTransporte);
                UTGlobal.MG_SeleccionarCombo_Almacen(Cb_Origen);
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
                listaVentas = new ServiceDesktop.ServiceDesktopClient().TraerVentas(UTGlobal.UsuarioId).ToList();
                Dgv_GBuscador.DataSource = listaVentas;
                Dgv_GBuscador.RetrieveStructure();
                Dgv_GBuscador.AlternatingColors = true;

                Dgv_GBuscador.RootTable.Columns["Id"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["IdAlmacen"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].Caption = "ALMACEN";
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].Width = 350;
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].Visible = true;
                Dgv_GBuscador.RootTable.Columns["DescripcionAlmacen"].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns["IdCliente"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].Caption = "CLIENTE";
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].Width = 300;
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].Visible = true;
                Dgv_GBuscador.RootTable.Columns["DescripcionCliente"].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns["FechaVenta"].Caption = "FECHA";
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].Width = 150;
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].Visible = true;
                Dgv_GBuscador.RootTable.Columns["FechaVenta"].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Tipo"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Observaciones"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["EncEntrega"].Caption = "Enc Entrega";
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].Width = 240;
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].Visible = true;
                Dgv_GBuscador.RootTable.Columns["EncEntrega"].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns["Estado"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Tipo"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Observaciones"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["EncPrVenta"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["EncVenta"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["EncTransporte"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["FacturaEmpresa"].Visible = false;


                Dgv_GBuscador.RootTable.Columns["NitCliente"].Caption = "NIT";
                Dgv_GBuscador.RootTable.Columns["NitCliente"].Width = 180;
                Dgv_GBuscador.RootTable.Columns["NitCliente"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_GBuscador.RootTable.Columns["NitCliente"].CellStyle.FontSize = 8;
                Dgv_GBuscador.RootTable.Columns["NitCliente"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_GBuscador.RootTable.Columns["NitCliente"].Visible = true;
                Dgv_GBuscador.RootTable.Columns["NitCliente"].EditType = EditType.NoEdit;

                Dgv_GBuscador.RootTable.Columns["Fecha"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Hora"].Visible = false;
                Dgv_GBuscador.RootTable.Columns["Usuario"].Visible = false;

                Dgv_GBuscador.RootTable.Columns["IdCategoriaCliente"].Visible = false;


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
                Double saldo, precioVenta, precioCosto, precioMinVenta, precioMaxVenta;
                int stock, contenido; 
                saldo = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["Cantidad"].Value);
                stock = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Stock"].Value);
                precioVenta = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioVenta"].Value);
                precioCosto = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioCosto"].Value);
                contenido = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Contenido"].Value);
                precioMinVenta = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioMinVenta"].Value);
                precioMaxVenta = Convert.ToDouble(Dgv_DetalleVenta.CurrentRow.Cells["PrecioMaxVenta"].Value);
                if (precioMinVenta > precioVenta)
                {
                    throw new Exception("Debe intrducir un precio mayor al precio minimo de: " + precioMinVenta.ToString());
                }
                else
                {
                    if (saldo > stock)
                    {                       
                        Dgv_DetalleVenta.CurrentRow.Cells["Cantidad"].Value = 1;
                        Dgv_DetalleVenta.CurrentRow.Cells["Contenido"].Value = contenido;
                        Dgv_DetalleVenta.CurrentRow.Cells["TotalContenido"].Value = contenido;
                        Dgv_DetalleVenta.CurrentRow.Cells["SubTotal"].Value = precioVenta;

                        Dgv_DetalleVenta.CurrentRow.Cells["subTotalCosto"].Value = precioCosto;
                        throw new Exception("No existe stock para algun producto");
                    }
                    else
                    {
                        IngresarCantidad(saldo, precioVenta, precioCosto, contenido);
                    }
                    MP_ObtenerCalculo();
                }                
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
            Dgv_DetalleVenta.CurrentRow.Cells["SubTotal"].Value = subTotal;
            Dgv_DetalleVenta.CurrentRow.Cells["TotalContenido"].Value = totalUnidad;
            Dgv_DetalleVenta.CurrentRow.Cells["subTotalCosto"].Value = subTotalCosto;
        }
        private void IngresarCantidadLote(int idProducto,string Producto, string codigoProducto, string unidadVenta,
                                            double cantidad, double precioVenta, double precioCosto, 
                                            string Lote, DateTime fechaVecnmiento)
        {
            var subTotal = cantidad * precioVenta;
            var subTotalCosto = cantidad * precioCosto;
            Dgv_DetalleVenta.CurrentRow.Cells["IdProducto"].Value = idProducto;
            Dgv_DetalleVenta.CurrentRow.Cells["Producto"].Value = Producto;
            Dgv_DetalleVenta.CurrentRow.Cells["CodigoProducto"].Value = codigoProducto;
            Dgv_DetalleVenta.CurrentRow.Cells["Unidad"].Value = unidadVenta;
            Dgv_DetalleVenta.CurrentRow.Cells["Cantidad"].Value = cantidad;
            Dgv_DetalleVenta.CurrentRow.Cells["PrecioVenta"].Value = precioVenta;
            Dgv_DetalleVenta.CurrentRow.Cells["SubTotal"].Value = subTotal;
            Dgv_DetalleVenta.CurrentRow.Cells["PrecioCosto"].Value = precioCosto;
            Dgv_DetalleVenta.CurrentRow.Cells["subTotalCosto"].Value = subTotalCosto;
            Dgv_DetalleVenta.CurrentRow.Cells["Lote"].Value = Lote;
            Dgv_DetalleVenta.CurrentRow.Cells["FechaVencimiento"].Value = fechaVecnmiento;
            Dgv_DetalleVenta.UpdateData();
        }
        private void MP_ObtenerCalculo()
        {
            try
            {
                Dgv_DetalleVenta.UpdateData();
                tbTotalCantidad.Text = Convert.ToDouble(Dgv_DetalleVenta.GetTotal(Dgv_DetalleVenta.RootTable.Columns["TotalContenido"], AggregateFunction.Sum)).ToString();
                var total = Convert.ToDouble(Dgv_DetalleVenta.GetTotal(Dgv_DetalleVenta.RootTable.Columns["SubTotal"], AggregateFunction.Sum));
                TbTotal.Text = total.ToString();
                tbMontoCredito.Text = Sw_TipoVenta.Value ? "0" : TbTotal.Text;
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
                Dgv_Producto.RootTable.Columns["CategoriaPrecio"].Visible = false;
                Dgv_Producto.RootTable.Columns["TipoProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["CategoriaProducto"].Visible = false;
                Dgv_Producto.RootTable.Columns["EsLote"].Visible = false;
                Dgv_Producto.RootTable.Columns["Contenido"].Visible = false;

                Dgv_Producto.RootTable.Columns["CodigoProducto"].Caption = "Codigo";
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Width = 80;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["CodigoProducto"].Visible = true;

                Dgv_Producto.RootTable.Columns["Producto"].Caption = "Producto";
                Dgv_Producto.RootTable.Columns["Producto"].Width = 180;
                Dgv_Producto.RootTable.Columns["Producto"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Producto"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Producto"].Visible = true;

                Dgv_Producto.RootTable.Columns["Division"].Caption = "División";
                Dgv_Producto.RootTable.Columns["Division"].Width = 100;
                Dgv_Producto.RootTable.Columns["Division"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Division"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Division"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                Dgv_Producto.RootTable.Columns["Division"].Visible = true;

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
 
                Dgv_Producto.RootTable.Columns["PrecioCosto"].Visible = false;
                Dgv_Producto.RootTable.Columns["PrecioMinVenta"].Visible = false;
                Dgv_Producto.RootTable.Columns["PrecioMaxVenta"].Visible = false;

                Dgv_Producto.RootTable.Columns["Stock"].Caption = "Stock";
                Dgv_Producto.RootTable.Columns["Stock"].Width = 100;
                Dgv_Producto.RootTable.Columns["Stock"].FormatString = "0.00";
                Dgv_Producto.RootTable.Columns["Stock"].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.FontSize = 8;
                Dgv_Producto.RootTable.Columns["Stock"].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                Dgv_Producto.RootTable.Columns["Stock"].Visible = true;

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
                var productos = MP_ObtenerProductos(Convert.ToInt32(Cb_Origen.Value), idCategoriaPrecio);
                MP_CargarProducto(productos);
                MP_HabilitarProducto();
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
        private List<VProductoListaStock> MP_ObtenerProductos(int almacenId,int categoriaPrecioId)
        {

            var almacen = new ServiceDesktop.ServiceDesktopClient()
                                                       .AlmacenListar()
                                                       .ToList()
                                                       .Find(a => a.Id == Convert.ToInt32(almacenId));

            return new ServiceDesktop.ServiceDesktopClient()
                                      .ListarProductosStock(almacen.SucursalId, 
                                                            almacen.Id, 
                                                            categoriaPrecioId)
                                      .ToList();
            
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
                    PrecioMinVenta = 0,
                    PrecioMaxVenta = 0,
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
        private static void MP_IngresarProductoDetalle(int idDetalle, int idLote, List<VTI001> lLotes, int cantidad)
        {
            if (lLotes.Count != 0)
            {
                foreach (var vDetalleVenta in listaDetalleVenta)
                {
                    if (vProductos.IdProducto == vDetalleVenta.IdProducto &&
                        lLotes.FirstOrDefault(a => a.id == idLote).Lote == vDetalleVenta.Lote &&
                        lLotes.FirstOrDefault(a => a.id == idLote).FechaVen == vDetalleVenta.FechaVencimiento)
                    {
                        throw new Exception("El producto ya existe en el detalle");
                    }
                    if (vDetalleVenta.Id == idDetalle)
                    {
                        vDetalleVenta.IdProducto = vProductos.IdProducto;
                        vDetalleVenta.CodigoProducto = vProductos.CodigoProducto;
                        vDetalleVenta.Producto = vProductos.Producto;
                        vDetalleVenta.Unidad = vProductos.UnidadVenta;
                        vDetalleVenta.Cantidad = cantidad;
                        vDetalleVenta.Contenido =  Convert.ToDecimal(vProductos.Contenido);
                        vDetalleVenta.TotalContenido = cantidad * Convert.ToDecimal(vProductos.Contenido);
                        vDetalleVenta.PrecioVenta = vProductos.PrecioVenta;
                        vDetalleVenta.PrecioCosto = vProductos.PrecioCosto;
                        vDetalleVenta.Lote = lLotes.FirstOrDefault(a => a.id == idLote).Lote;
                        vDetalleVenta.FechaVencimiento = lLotes.FirstOrDefault(a => a.id == idLote).FechaVen;
                        vDetalleVenta.PrecioMinVenta = vProductos.PrecioMinVenta;
                        vDetalleVenta.PrecioMaxVenta = vProductos.PrecioMaxVenta;
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
                var sumaCantidad = listaDetalleVenta.Where(a => a.Lote.Equals(fila.Lote) &&
                                                              a.FechaVencimiento == fila.FechaVen &&
                                                              a.IdProducto == idProducto &&
                                                              a.Estado == 0).Sum(a => a.Cantidad);
                fila.Cantidad = fila.Cantidad - sumaCantidad;
            }
        }
        private void MP_ActualizarLote2(ref List<VTI001> Lotes, int idProducto, int idDetalle)
        {
            var idDetalleActual = Convert.ToInt32(Dgv_DetalleVenta.CurrentRow.Cells["Id"].Value);

            foreach (var fila in Lotes)
            {

                var sumaCantidad = listaDetalleVenta.Where(a => a.Lote.Equals(fila.Lote) &&
                                                          a.FechaVencimiento == fila.FechaVen &&
                                                          a.IdProducto == idProducto &&
                                                          a.Estado == 0 &&
                                                          a.Id != idDetalle).Sum(a => a.Cantidad);
                fila.Cantidad = fila.Cantidad - sumaCantidad;

            }
        }
        #endregion

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
            if (!Sw_TipoVenta.Value)
            {
                if (tbLimiteCred.Text == string.Empty || tbLimiteCred.Text == "0")
                {
                    this.MP_MostrarMensajeError("Debe introducir un limite de crédito para el cliente");
                    return flag;
                }
                if (Convert.ToDecimal(TbTotal.Text) > Convert.ToDecimal(tbMontoDisponible.Text))
                {
                    this.MP_MostrarMensajeError("El monto total es mayor al limite de crédito");
                    return flag;
                }
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
                    EncPrVenta = Convert.ToInt32(this.Cb_EncPreVenta.Value),
                    FacturaEmpresa = Convert.ToInt32(this.cbFacturaEmpresa.Value),
                    EncTransporte = Convert.ToInt32(this.Cb_EncTransporte.Value),
                    EncVenta = Convert.ToInt32(this.Cb_EncVenta.Value),
                    Estado = (int)ENEstado.GUARDADO,
                    FechaVenta = this.Dt_FechaVenta.Value,
                    IdAlmacen = Convert.ToInt32(this.Cb_Origen.Value),
                    IdCliente = Convert.ToInt32(this.cb_Cliente.Value),
                    Observaciones = this.Tb_Observaciones.Text,
                    Tipo = 1,
                    Fecha = DateTime.Now.Date,
                    Hora = DateTime.Now.ToString("hh:mm"),
                    Usuario = lblUsuario.Text,
                    IdCompraIngreso = idCompraIngreso
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
        private void Sw_TipoVenta_ValueChanged(object sender, EventArgs e)
        {
            if (Sw_TipoVenta.Value)
                tbMontoCredito.Visible = false;
            else
                tbMontoCredito.Visible = true;
        }
        private void F1_Ventas_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "VENTAS";
            btnMax.Visible = false;
            superTabControl1.SelectedPanel = PanelContenidoRegistro;
            this.MP_CargarBuscador();
            this.MP_InHabilitar();
            this.MP_ArmarCombo();
            this.MP_CargarAlmacenes();
            this.MP_CargarVentas();
            this.TxtNombreUsu.Visible = false;
            superTabControl1.SelectedTabIndex = 0;
            listaDetalleVenta = new List<VVenta_01>();
            MP_AsignarPermisos();
            BtnHabilitar.Visible = true;
            this.Text = _NombreFormulario;
            LblTitulo.Text = _NombreFormulario;
            inicioDesdeCompraIngreso();
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
                        int idLote = 0; 
                        var idDetalle = Convert.ToInt32(Dgv_DetalleVenta.GetValue("id"));
                        var cantidad = Convert.ToInt32(Dgv_DetalleVenta.GetValue("Cantidad"));
                        listaDetalleVenta = (List<VVenta_01>)Dgv_DetalleVenta.DataSource;
                        if (idDetalle > 0)
                        {
                            if (vProductos.IdProducto == 0)
                            {

                                var idProducto = Convert.ToInt32(Dgv_Producto.GetValue("IdProducto"));
                                //Obtiene el producto
                                MP_ValidarLotesInsertarProductosDetalle(idProducto, idLote, idDetalle,1);
                            }
                            else
                            {
                                idLote = Convert.ToInt32(Dgv_Producto.GetValue("id"));
                                var lLotes = ((List<VTI001>)Dgv_Producto.DataSource);
                                MP_IngresarProductoDetalle(idDetalle, idLote, lLotes,1);
                                MP_ArmarDetalle();
                                MP_InHabilitarProducto();
                            }
                        }
                        MP_ObtenerCalculo();

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
        private void MP_ValidarLotesInsertarProductosDetalle(int productoId,int idLote, int idDetalle, int cantidad)
        {
            vProductos = ((List<VProductoListaStock>)Dgv_Producto.DataSource)
                                                                               .Where(p => p.IdProducto == productoId)
                                                                               .FirstOrDefault();
            if (vProductos.Stock <= 0)
            {
                var producto = vProductos.Producto;
                vProductos = new VProductoListaStock();
                throw new Exception("El producto " + producto + "no cuenta con STOCK disponible.");
            }

            var InventarioLotes = new ServiceDesktop.ServiceDesktopClient()
                                                    .TraerInventarioLotes(vProductos.IdProducto,
                                                                          Convert.ToInt32(Cb_Origen.Value))
                                                    .ToList();
            //Veridica si se mostrara la seleccion de lotes
            if (vProductos.EsLote == 2)
            {
                idLote = InventarioLotes.FirstOrDefault().id;
                if (idLote == 0)
                {
                    throw new Exception("El producto no tiene un lote relacionado");
                }
                MP_IngresarProductoDetalle(idDetalle, idLote, InventarioLotes, cantidad);
                MP_ArmarDetalle();
                MP_InHabilitarProducto();
                MP_ObtenerCalculo();
                return;
            }
            //Actualiza el stock de lotes
            MP_ActualizarLote(ref InventarioLotes, productoId);
            MP_ArmarLotes(InventarioLotes);
            GPanel_Producto.Text = vProductos.Producto;
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
                    if (e.Column.Index == Dgv_DetalleVenta.RootTable.Columns["Cantidad"].Index ||
                        e.Column.Index == Dgv_DetalleVenta.RootTable.Columns["PrecioVenta"].Index)
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
        private void BtnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {            
                base.MH_Inhanbilitar();
                this.MP_Habilitar();
                this.MP_CargarDetalleRegistro(Convert.ToInt32(Tb_Cod.Text),2);
                this.MP_ObtenerCalculo();
                this.LblPaginacion.Text = (index + 1) + "/" + Dgv_GBuscador.RowCount.ToString() + " Ventas";
                Tb_Cod.Text = "";
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }
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
                            IngresarClienteConEnter();
                        }
                    }
                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
                    {
                        IngresarClienteConControlEnter();
                    }
                }                
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }


        #endregion

   
    }
}
