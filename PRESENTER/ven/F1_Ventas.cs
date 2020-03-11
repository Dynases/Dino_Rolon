using DevComponents.DotNetBar;
using ENTITY.Plantilla;
using ENTITY.ven.view;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UTILITY.Global;

namespace PRESENTER.ven
{
    public partial class F1_Ventas : MODEL.ModeloF1
    {
        public F1_Ventas()
        {
            InitializeComponent();
            this.MP_InHabilitar();
            this.MP_CargarAlmacenes();
            listaDetalleVenta = new List<VVenta_01>();
        }

        #region Variables de Entorno

        //public static List<Producto> detalleProductos;
        private static List<VVenta> listaVentas;
        private static List<VPlantilla> listaPlantillas;
        private static List<VVenta_01> listaDetalleVenta;
        private static int index;
        private static int plantillaIndex;

        #endregion

        #region Metodos Privados

        private void MP_MostrarMensajeError(string mensaje)
        {
            ToastNotification.Show(this, mensaje.ToUpper(), PRESENTER.Properties.Resources.WARNING, (int)GLMensajeTamano.Mediano, eToastGlowColor.Green, eToastPosition.TopCenter);
        }

        private void MP_InHabilitar()
        {
            this.lblId.Visible = false;
            this.lblIdCliente.Visible = false;
            this.Dt_FechaVenta.Enabled = false;
            this.Tb_Cod.Enabled = false;
            this.Cb_Origen.Enabled = false;
            this.Tb_Usuario.ReadOnly = true;
            this.TbCliente.ReadOnly = true;
            this.sw_estado.Enabled = false;
            this.Sw_TipoVenta.Enabled = false;
            this.TbEncVenta.ReadOnly = true;
            this.TbEncEntrega.ReadOnly = true;
            this.TbEncTransporte.ReadOnly = true;
            this.TbEncRecepcion.ReadOnly = true;
            this.Tb_Observaciones.ReadOnly = true;
            this.TbEncPrVenta.ReadOnly = true;
        }

        private void MP_Habilitar()
        {
            this.lblFechaRegistro.Visible = false;
            this.Dt_FechaVenta.Enabled = true;
            this.Cb_Origen.Enabled = true;
            this.TbCliente.ReadOnly = false;
            this.sw_estado.Enabled = true;
            this.Sw_TipoVenta.Enabled = true;
            this.TbEncVenta.ReadOnly = false;
            this.TbEncEntrega.ReadOnly = false;
            this.TbEncTransporte.ReadOnly = false;
            this.TbEncRecepcion.ReadOnly = false;
            this.Tb_Observaciones.ReadOnly = false;
            this.TbEncPrVenta.ReadOnly = false;
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

        private void MP_AgregarFila(VVenta_01 vVenta_01)
        {
            try
            {
                listaDetalleVenta.Add(vVenta_01);

                this.MP_CargarDetalle();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MP_CargarDetalle()
        {
            Dgv_DetalleVenta.DataSource = listaDetalleVenta;
            Dgv_DetalleVenta.RetrieveStructure();
            Dgv_DetalleVenta.AlternatingColors = true;

            Dgv_DetalleVenta.RootTable.Columns[0].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[1].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[2].Caption = "PRODUCTO";
            Dgv_DetalleVenta.RootTable.Columns[2].Width = 150;
            Dgv_DetalleVenta.RootTable.Columns[2].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[2].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[2].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns[2].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[3].Caption = "CANT.";
            Dgv_DetalleVenta.RootTable.Columns[3].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns[3].Width = 90;
            Dgv_DetalleVenta.RootTable.Columns[3].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[3].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[3].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns[3].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[4].Caption = "PRECIO";
            Dgv_DetalleVenta.RootTable.Columns[4].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns[4].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns[4].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[4].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[4].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns[4].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[5].Caption = "UNIDAD";
            Dgv_DetalleVenta.RootTable.Columns[5].Width = 150;
            Dgv_DetalleVenta.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns[5].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[6].Caption = "TOTAL";
            Dgv_DetalleVenta.RootTable.Columns[6].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns[6].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns[6].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[6].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[6].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns[6].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[7].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[8].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[9].Visible = false;

            Dgv_DetalleVenta.GroupByBoxVisible = false;
            Dgv_DetalleVenta.VisualStyle = VisualStyle.Office2007;
        }

        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_AgregarFila(new VVenta_01
            {
                Cantidad = 0,
                CodBar = "",
                DescripcionProducto = "",
                Estado = 0,
                IdProducto = 0,
                IdVenta = 0,
                Precio = 0,
                Unidad = "",
                Id = 0
            });
        }

        public override void MH_Salir()
        {
            base.MH_Salir();
            this.MP_InHabilitar();
        }

        #endregion

        #region Eventos

        private void F1_Ventas_Load(object sender, EventArgs e)
        {

        }

        private void TbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (TbCliente.ReadOnly.Equals(false))
            {
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
                        new GLCelda() { campo = "Factur", visible = true, titulo = "Facturacion", tamano = 100 }
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
                        lblIdCliente.Text = Row.Cells[0].Value.ToString();
                        TbCliente.Text = Row.Cells[2].Value.ToString();
                    }
                }
            }
        }

        private void Dgv_DetalleVenta_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cb_Origen.Enabled == true)
                {
                    if (e.KeyData == Keys.Enter)
                    {

                    }

                    if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter &&
                        Dgv_DetalleVenta.Row >= 0)
                    {

                        var almacen = new ServiceDesktop.ServiceDesktopClient()
                                                        .AlmacenListar()
                                                        .ToList()
                                                        .Find(a => a.Id == Convert.ToInt32(Cb_Origen.Value));

                        var lista = new ServiceDesktop.ServiceDesktopClient().PrductoListarEncabezado(almacen.SucursalId, almacen.Id, 1);

                        List<GLCelda> listEstCeldas = new List<GLCelda>
                        {
                            new GLCelda() { campo = "ProductoId", visible = true, titulo = "Cod", tamano = 50 },
                            new GLCelda() { campo = "Producto", visible = true, titulo = "Producto", tamano = 150 },
                            new GLCelda() { campo = "i.iccven", visible = true, titulo = "Stock", tamano = 100 },
                            new GLCelda() { campo = "a.Descrip", visible = false, titulo = "Almacen", tamano = 100 },
                            new GLCelda() { campo = "pr.Precio", visible = true, titulo = "Precio", tamano = 100 },
                            new GLCelda() { campo = "l.Descrip", visible = true, titulo = "Unidad de Venta", tamano = 120 },
                            new GLCelda() { campo = "prc.Descrip", visible = true, titulo = "Cat. Precio", tamano = 120 },
                        };

                        Efecto efecto = new Efecto();
                        efecto.Tipo = 3;
                        efecto.Tabla = lista;
                        efecto.SelectCol = 2;
                        efecto.listaCelda = listEstCeldas;
                        efecto.Alto = 50;
                        efecto.Ancho = 350;
                        efecto.Context = "SELECCIONE UN PRODUCTO";
                        efecto.ShowDialog();

                        var bandera = false;
                        bandera = efecto.Band;
                        if (bandera)
                        {
                            GridEXRow Row = efecto.Row;
                            this.MP_AgregarFila(new VVenta_01
                            {
                                Cantidad = 0,
                                CodBar = "",
                                DescripcionProducto = Row.Cells[1].Value.ToString(),
                                Estado = 1,
                                IdProducto = Convert.ToInt32(Row.Cells[0].Value),
                                IdVenta = 0,
                                Precio = Convert.ToDecimal(Row.Cells[4].Value),
                                Unidad = Row.Cells[5].Value.ToString()
                            });
                        }
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

        private void Dgv_DetalleVenta_CellEdited(object sender, ColumnActionEventArgs e)
        {
            //foreach (var i in this.Dgv_DetalleVenta.GetRows())
            //{
            //    i.Cells[6].Value = 
            //        Convert.ToDecimal(Convert.ToUInt32(i.Cells[3].Value) * Convert.ToDecimal(i.Cells[4].Value)).ToString();
            //}

            var precio = Convert.ToDecimal(Dgv_DetalleVenta.CurrentRow.Cells[4].Value);
            var cantidad = Convert.ToUInt32(Dgv_DetalleVenta.CurrentRow.Cells[3].Value);

            Dgv_DetalleVenta.CurrentRow.Cells[6].Value = precio * cantidad;
        }

        #endregion        
    }
}
