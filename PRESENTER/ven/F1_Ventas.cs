using DevComponents.DotNetBar;
using ENTITY.Plantilla;
using ENTITY.ven.view;
using Janus.Windows.GridEX;
using PRESENTER.frm;
using PRESENTER.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.MP_CargarVentas();
            this.TxtNombreUsu.Visible = false;
            this.lblIdCliente.Text = "";
            this.lblId.Text = "";
            listaDetalleVenta = new List<VVenta_01>();
            this.MP_CargarBuscador();
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
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = false;
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
            this.Tb_Usuario.Text = UTGlobal.Usuario;
            this.TbNitCliente.ReadOnly = true;
            this.btnLimpiarCliente.Visible = true;
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

            Dgv_DetalleVenta.RootTable.Columns[1].Caption = "COD";
            Dgv_DetalleVenta.RootTable.Columns[1].Visible = true;

            Dgv_DetalleVenta.RootTable.Columns[2].Caption = "PRODUCTO";
            Dgv_DetalleVenta.RootTable.Columns[2].Width = 250;
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
            Dgv_DetalleVenta.RootTable.Columns[4].EditType = EditType.NoEdit;

            Dgv_DetalleVenta.RootTable.Columns[5].Caption = "UNIDAD";
            Dgv_DetalleVenta.RootTable.Columns[5].Width = 120;
            Dgv_DetalleVenta.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
            Dgv_DetalleVenta.RootTable.Columns[5].Visible = true;
            Dgv_DetalleVenta.RootTable.Columns[5].EditType = EditType.NoEdit;

            Dgv_DetalleVenta.RootTable.Columns[6].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[7].Caption = "TOTAL";
            Dgv_DetalleVenta.RootTable.Columns[7].FormatString = "0.00";
            Dgv_DetalleVenta.RootTable.Columns[7].Width = 100;
            Dgv_DetalleVenta.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[7].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
            Dgv_DetalleVenta.RootTable.Columns[7].Visible = true;
            Dgv_DetalleVenta.RootTable.Columns[7].EditType = EditType.NoEdit;

            Dgv_DetalleVenta.RootTable.Columns[8].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[9].Visible = false;

            Dgv_DetalleVenta.RootTable.Columns[10].Caption = "";
            Dgv_DetalleVenta.RootTable.Columns[10].Width = 40;
            Dgv_DetalleVenta.RootTable.Columns[10].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[10].CellStyle.FontSize = 9;
            Dgv_DetalleVenta.RootTable.Columns[10].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
            Dgv_DetalleVenta.RootTable.Columns[10].Visible = true;
            Dgv_DetalleVenta.RootTable.Columns[10].Image = Resources.delete;
            Dgv_DetalleVenta.RootTable.Columns[10].EditType = EditType.NoEdit;

            Dgv_DetalleVenta.GroupByBoxVisible = false;
            Dgv_DetalleVenta.VisualStyle = VisualStyle.Office2007;
        }

        private bool MP_ExisteEnGrilla(string Id)
        {
            var response = false;

            foreach (var i in Dgv_DetalleVenta.GetRows())
            {
                if (i.Cells[1].Value.ToString().Equals(Id))
                {
                    return true;
                }
            }

            return response;
        }

        private void MP_CargarVentas()
        {
            index = 0;
            try
            {
                listaVentas = new ServiceDesktop.ServiceDesktopClient().VentasListar().ToList();
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
            TbCliente.Text = venta.DescripcionCliente;
            lblIdCliente.Text = venta.IdCliente.ToString();
            TbNitCliente.Text = venta.NitCliente;
            Sw_TipoVenta.Value = true;
            sw_estado.Value = true;
            TbEncVenta.Text = venta.EncVenta;
            TbEncEntrega.Text = venta.EncEntrega;
            TbEncPrVenta.Text = venta.EncPrVenta;
            TbEncRecepcion.Text = venta.EncRecepcion;
            TbEncTransporte.Text = venta.EncTransporte;
            Tb_Observaciones.Text = venta.Observaciones;
            lblFechaRegistro.Text = venta.FechaRegistro.ToString();

            this.MP_CargarDetalleRegistro(venta.Id);

            this.LblPaginacion.Text = (index + 1) + "/" + listaVentas.Count + " Ventas";
        }

        private void MP_CargarDetalleRegistro(int id)
        {
            try
            {
                var result = new ServiceDesktop.ServiceDesktopClient().VentaDetalleListar(id).ToList();

                if (result.Count > 0)
                {
                    Dgv_DetalleVenta.DataSource = result;
                    Dgv_DetalleVenta.RetrieveStructure();
                    Dgv_DetalleVenta.AlternatingColors = true;

                    Dgv_DetalleVenta.RootTable.Columns[0].Visible = false;

                    Dgv_DetalleVenta.RootTable.Columns[1].Caption = "COD";
                    Dgv_DetalleVenta.RootTable.Columns[1].Visible = true;

                    Dgv_DetalleVenta.RootTable.Columns[2].Caption = "PRODUCTO";
                    Dgv_DetalleVenta.RootTable.Columns[2].Width = 250;
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
                    Dgv_DetalleVenta.RootTable.Columns[4].EditType = EditType.NoEdit;

                    Dgv_DetalleVenta.RootTable.Columns[5].Caption = "UNIDAD";
                    Dgv_DetalleVenta.RootTable.Columns[5].Width = 120;
                    Dgv_DetalleVenta.RootTable.Columns[5].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.FontSize = 9;
                    Dgv_DetalleVenta.RootTable.Columns[5].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near;
                    Dgv_DetalleVenta.RootTable.Columns[5].Visible = true;
                    Dgv_DetalleVenta.RootTable.Columns[5].EditType = EditType.NoEdit;

                    Dgv_DetalleVenta.RootTable.Columns[6].Visible = false;

                    Dgv_DetalleVenta.RootTable.Columns[7].Caption = "TOTAL";
                    Dgv_DetalleVenta.RootTable.Columns[7].FormatString = "0.00";
                    Dgv_DetalleVenta.RootTable.Columns[7].Width = 100;
                    Dgv_DetalleVenta.RootTable.Columns[7].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleVenta.RootTable.Columns[7].CellStyle.FontSize = 9;
                    Dgv_DetalleVenta.RootTable.Columns[7].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far;
                    Dgv_DetalleVenta.RootTable.Columns[7].Visible = true;
                    Dgv_DetalleVenta.RootTable.Columns[7].EditType = EditType.NoEdit;

                    Dgv_DetalleVenta.RootTable.Columns[8].Visible = false;

                    Dgv_DetalleVenta.RootTable.Columns[9].Visible = false;

                    Dgv_DetalleVenta.RootTable.Columns[10].Caption = "";
                    Dgv_DetalleVenta.RootTable.Columns[10].Width = 40;
                    Dgv_DetalleVenta.RootTable.Columns[10].HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleVenta.RootTable.Columns[10].CellStyle.FontSize = 9;
                    Dgv_DetalleVenta.RootTable.Columns[10].CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center;
                    Dgv_DetalleVenta.RootTable.Columns[10].Visible = true;
                    Dgv_DetalleVenta.RootTable.Columns[10].Image = Resources.delete;
                    Dgv_DetalleVenta.RootTable.Columns[10].EditType = EditType.NoEdit;

                    Dgv_DetalleVenta.GroupByBoxVisible = false;
                    Dgv_DetalleVenta.VisualStyle = VisualStyle.Office2007;

                    this.MP_CalcularTotal();
                }
            }
            catch (Exception ex)
            {
                MP_MostrarMensajeError(ex.Message);
            }
        }

        private void MP_Reiniciar()
        {
            this.Tb_Cod.Text = "";
            this.Tb_Usuario.Text = UTGlobal.Usuario;
            this.Dt_FechaVenta.Value = DateTime.Today;
            this.TbCliente.Text = "";
            this.lblIdCliente.Text = "";
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
            index = 0;
            listaDetalleVenta.Clear();
            this.Dgv_DetalleVenta.DataSource = null;
            this.MP_CargarEncargado(Convert.ToInt32(Cb_Origen.Value));
        }

        private void MP_CalcularTotal()
        {
            decimal total = 0;
            foreach (var i in Dgv_DetalleVenta.GetRows())
            {
                total += Convert.ToDecimal(i.Cells[7].Value);
            }

            TbTotal.Text = total.ToString();
        }

        private void MP_GuardarDetalleVenta(VVenta vVenta)
        {
            var lista = new List<VVenta_01>();

            foreach (var i in this.Dgv_DetalleVenta.GetRows())
            {
                if (!i.Cells[1].Value.ToString().Equals("0"))
                {
                    var detalleVenta = new VVenta_01
                    {
                        Cantidad = Convert.ToInt32(i.Cells[3].Value),
                        CodBar = i.Cells[8].Value.ToString(),
                        Delete = "",
                        DescripcionProducto = i.Cells[2].Value.ToString(),
                        Estado = 1,
                        IdProducto = Convert.ToInt32(i.Cells[1].Value),
                        IdVenta = vVenta.Id,
                        Precio = Convert.ToDecimal(i.Cells[4].Value),
                        Unidad = i.Cells[5].Value.ToString()
                    };

                    lista.Add(detalleVenta);
                }
            }

            if (!new ServiceDesktop.ServiceDesktopClient().VentaDetalleGuardar(lista.ToArray(), vVenta.Id, vVenta.IdAlmacen))
            {
                var mensaje = GLMensaje.Registro_Error("VENTAS");
                this.MP_MostrarMensajeError(mensaje);
            }
        }

        private void MP_CargarEncargado(int almacenOrigen)
        {
            var almacen = new ServiceDesktop.ServiceDesktopClient().AlmacenListar()
                                                                       .ToList()
                                                                       .Where(a => a.Id == almacenOrigen)
                                                                       .FirstOrDefault();
            TbEncEntrega.Text = almacen.Encargado;
        }

        private void MP_CargarBuscador()
        {
            try
            {
                var ListaCompleta = new ServiceDesktop.ServiceDesktopClient().VentasListar().ToList();
                if (ListaCompleta.Count() > 0)
                {
                    Dgv_GBuscador.DataSource = ListaCompleta;
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

        #endregion

        #region Metodos Heredados

        public override void MH_Nuevo()
        {
            base.MH_Nuevo();
            this.MP_Habilitar();
            this.MP_Reiniciar();
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

            Dgv_DetalleVenta.GetRow(0).Cells[0].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[1].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[2].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[3].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[4].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[5].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[6].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[7].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[8].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[9].Value = "";
            Dgv_DetalleVenta.GetRow(0).Cells[10].Image = null;
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

            if (string.IsNullOrEmpty(this.TbCliente.Text))
            {
                this.TbCliente.BackColor = Color.Red;
                return flag;
            }
            if (string.IsNullOrEmpty(lblIdCliente.Text))
            {
                this.MP_MostrarMensajeError("Debe seleccionar un cliente. ingrese al buscador con las teclas: Ctrl + Enter");
                return flag;
            }
            if (this.Dgv_DetalleVenta.RowCount <= 1)
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
                    Estado = 1,
                    FechaRegistro = DateTime.Now,
                    FechaVenta = this.Dt_FechaVenta.Value,
                    IdAlmacen = Convert.ToInt32(this.Cb_Origen.Value),
                    IdCliente = Convert.ToInt32(this.lblIdCliente.Text),
                    Observaciones = this.Tb_Observaciones.Text,
                    Tipo = 1,
                    Usuario = UTGlobal.Usuario
                };

                try
                {
                    int id = 0;
                    if (new ServiceDesktop.ServiceDesktopClient().VentaGuardar(vVenta, ref id))
                    {
                        vVenta.Id = id;
                        this.MP_GuardarDetalleVenta(vVenta);
                        this.MP_InHabilitar();
                        this.MP_Reiniciar();
                        this.MP_CargarAlmacenes();
                        this.MP_CargarVentas();
                        this.MP_CargarBuscador();
                        ToastNotification.Show(this, GLMensaje.Modificar_Exito("TRASPASOS", vVenta.Id.ToString()), Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
                        return true;

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

        #endregion

        #region Eventos

        private void F1_Ventas_Load(object sender, EventArgs e)
        {
            this.LblTitulo.Text = "VENTAS";
            btnMax.Visible = false;
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
                        TbNitCliente.Text = Row.Cells[4].Value.ToString();
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

                            if (!this.MP_ExisteEnGrilla(Row.Cells[0].Value.ToString()))
                            {
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
                            else
                            {
                                this.MP_MostrarMensajeError("El producto ya fue seleccionado");
                            }
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
                this.MP_MostrarMensajeError(ex.Message);
            }
        }

        private void Dgv_DetalleVenta_CellEdited(object sender, ColumnActionEventArgs e)
        {
            if (!string.IsNullOrEmpty(Dgv_DetalleVenta.CurrentRow.Cells[4].Value.ToString()) &&
                !string.IsNullOrEmpty(Dgv_DetalleVenta.CurrentRow.Cells[3].Value.ToString()) &&
                !string.IsNullOrEmpty(Dgv_DetalleVenta.CurrentRow.Cells[6].Value.ToString()))
            {
                var precio = Convert.ToDecimal(Dgv_DetalleVenta.CurrentRow.Cells[4].Value);
                var cantidad = Convert.ToUInt32(Dgv_DetalleVenta.CurrentRow.Cells[3].Value);

                Dgv_DetalleVenta.CurrentRow.Cells[6].Value = precio * cantidad;
                Dgv_DetalleVenta.UpdateData();

                this.MP_CalcularTotal();
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            this.lblIdCliente.Text = "";
            this.TbCliente.Text = "";
            this.TbNitCliente.Text = "";
            ToastNotification.Show(this, "El cliente fue borrado de la venta con exito", PRESENTER.Properties.Resources.GRABACION_EXITOSA, (int)GLMensajeTamano.Chico, eToastGlowColor.Green, eToastPosition.TopCenter);
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

        private void lblIdCliente_TextChanged(object sender, EventArgs e)
        {
            if (this.TbCliente.BackColor.Equals(Color.Red))
            {
                this.TbCliente.BackColor = Color.White;
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

        #endregion

    }
}
