using ENTITY.DiSoft.Pedido.View;
using ENTITY.ven.Report;
using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Clase.DiSoft;
using REPOSITORY.Interface;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Transactions;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;

namespace LOGIC.Class
{
    public class LVenta
    {
        protected IVenta iVenta;
        protected IVenta_01 iVenta_01;
        protected IProducto iProducto;
        protected IClienteD iClienteD;
        protected IPedidoD iPedidoD;
        public LVenta()
        {
            iVenta = new RVenta();
            iVenta_01 = new RVenta_01();
            iProducto = new RProducto();
            iClienteD = new RClienteD();
            iPedidoD = new RPedidoD();
        }
        #region Transacciones

        public bool Guardar(VVenta vVenta, List<VVenta_01> detalle, ref int IdVenta, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    VPedidoD pedido = null;
                    List<VPedidoProductoD> pedidoDetalle = null;
                    int aux = IdVenta;
                    int pedidoId = vVenta.IdPedidoDisoft;

                    result = iVenta.Guardar(vVenta, ref IdVenta);

                    pedido = LlenarPedido(vVenta, pedidoId);
                    pedidoDetalle = LlenarPedidoDetalle(detalle, pedidoId);

                    iPedidoD.Guardar(pedido,ref pedidoId, vVenta.Usuario);
                    iPedidoD.GuardarDetalle(pedidoDetalle, pedidoId, vVenta.Tipo);
                    iPedidoD.GuardarExtencionPedido(pedidoId, vVenta.EncPrVenta);

                    //Venta directa
                    if (vVenta.EsFActuracion)
                    {
                        //Id estatido de Enc de distribucion
                        iPedidoD.GuardarPedidoDirecto(pedidoId, 4);
                        iPedidoD.ModificarEstadoPedido(pedidoId, (int)ENEstadoPedido.ENTREGADO);
                    }
                    else iPedidoD.ModificarEstadoPedido(pedidoId, (int)ENEstadoPedido.DICTADO);

                    //Actualiza la venta con el IdPedido
                    iVenta.GuardarIdPedido(IdVenta, pedidoId);


                    if (aux == 0)//Nuevo
                    {
                        var resultDetalle = new LVenta_01().Nuevo(detalle, IdVenta,vVenta.IdAlmacen);
                    }
                    else//Modificar
                    {
                        foreach (var i in detalle)
                        {
                            switch (i.Estado)
                            {
                                case (int)ENEstado.NUEVO:
                                    List<VVenta_01> detalleNuevo = new List<VVenta_01>();
                                    detalleNuevo.Add(i);
                                    new LVenta_01().Nuevo(detalleNuevo, IdVenta, vVenta.IdAlmacen);
                                    break;
                                case (int)ENEstado.MODIFICAR:
                                    new LVenta_01().Modificar(i, IdVenta, vVenta.IdAlmacen);
                                    break;
                                case (int)ENEstado.ELIMINAR:
                                    new LVenta_01().Eliminar(IdVenta, i.Id, vVenta.IdAlmacen, ref lMensaje);
                                    break;
                            }                           
                        }
                    }

                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<VPedidoProductoD> LlenarPedidoDetalle(List<VVenta_01> detalle, int idPedido)
        {
            List<VPedidoProductoD> lista = new List<VPedidoProductoD>();
            foreach (var item in detalle)
            {
                VPedidoProductoD itemDetalle = new VPedidoProductoD();
                itemDetalle.PedidoId = idPedido;
                itemDetalle.ProductoId = item.IdProducto.ToString();
                itemDetalle.Cantidad = item.Cantidad;
                itemDetalle.Precio = item.PrecioCosto;
                itemDetalle.SubTotal = item.SubTotal;
                itemDetalle.Descuento = 0;
                itemDetalle.Total = item.SubTotal;                
                lista.Add(itemDetalle);
            }
            return lista;
        }

        private VPedidoD LlenarPedido(VVenta venta, int IdPedido)
        {        
            var pedido = new VPedidoD();
            pedido.Id = IdPedido;
            pedido.FechaReg = venta.FechaVenta;
            pedido.ClienteId = venta.IdCliente;
            pedido.VendedorId = venta.EncPrVenta;
            pedido.Observacion = venta.Observaciones;
            //pedido.EstadoPedido = venta.EsFActuracion ? (int)ENEstadoPedido.DICTADO : (int)ENEstadoPedido.ENTREGADO;
            pedido.Observacion = venta.Observaciones;
            return pedido;
        }
        public bool ModificarEstado(int IdVenta, int estado, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    //Trae el detalle de venta completo
                    var venta = this.TraerVenta(IdVenta);
                    var venta_01 = this.iVenta_01.TraerVentas_01(IdVenta);
                    foreach (var vventa_01 in venta_01)
                    {
                        if (venta_01 == null) { return false; }

                        var StockActual = new LInventario().TraerStockActual(vventa_01.IdProducto, venta.IdAlmacen, vventa_01.Lote, vventa_01.FechaVencimiento);
                        if (StockActual < vventa_01.Cantidad)
                        {
                            var producto = iProducto.ListarXId(vventa_01.IdProducto);
                            lMensaje.Add("No existe stock actual suficiente para el producto: " + producto.Id + " - " + producto.Descripcion);
                        }
                        if (lMensaje.Count > 0)
                        {
                            var mensaje = "";
                            foreach (var item in lMensaje)
                            {
                                mensaje = mensaje + "- " + item + "\n";
                            }
                            return false;
                        }

                        //Elimina el movimiento de inventario y actualiza el stock
                        if (!new LInventario().EliminarMovimientoInventario(vventa_01.Id, vventa_01.IdProducto, venta.IdAlmacen,
                                                                             vventa_01.Lote, vventa_01.FechaVencimiento, vventa_01.Cantidad,
                                                                             (int)ENConcepto.VENTAS, EnAccionEnInventario.Incrementar))
                        {
                            return false;
                        }
                    }                    
                    result = iVenta.ModificarEstado(IdVenta, estado);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VVenta TraerVenta(int idVenta)
        {
            try
            {
                return this.iVenta.TraerVenta(idVenta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal? SaldoPendienteCredito(int clienteId)
        {
            try
            {
                return this.iClienteD.saldoPendienteCredito(clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VVenta> TraerVentas(int usuarioId)
        {
            try
            {
                return this.iVenta.TraerVentas(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VVentaTicket> ReporteVenta(int ventaId)
        {
            try
            {
                return this.iVenta.ReporteVenta(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verfiicacion


        #endregion

    }
}
