using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Global;

namespace LOGIC.Class
{
    public class LVenta_01
    {
            protected IVenta_01 iVenta_01;          
            protected IProducto iProducto;
            public LVenta_01()
            { 
                iProducto = new RProducto();
                iVenta_01 = new RVenta_01();
            }
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public List<VVenta_01> ListarDetalle(int VentaId)
        {
            try
            {
                return this.iVenta_01.TraerVentas_01(VentaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transacciones

        public bool Nuevo(List<VVenta_01> lVenta_01, int VentaId, int idAlmacen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {                                  
                    foreach (var vVenta_01 in lVenta_01)
                    {
                        var idVentaDetalle = 0;
                        //Registra el detalle de venta
                        if (!this.iVenta_01.Nuevo(vVenta_01, VentaId, ref idVentaDetalle)) { return false; }

                        var producto = iProducto.ListarXId(vVenta_01.IdProducto);

                        //Registra el movimiento de inventario y actualiza el stock
                        var Observacion = "Venta: "+ VentaId +" - IdProducto: "+ vVenta_01.IdProducto + " | " + producto.Descripcion;
                         if (!new LInventario().NuevoMovimientoInventario(idVentaDetalle,
                                                                        vVenta_01.IdProducto,
                                                                        idAlmacen,
                                                                        vVenta_01.Lote, vVenta_01.FechaVencimiento,
                                                                        vVenta_01.Cantidad,
                                                                    (int)ENConcepto.VENTAS,
                                                                    Observacion,
                                                                    EnAccionEnInventario.Descontar,
                                                                    UTGlobal.Usuario))
                        {
                            return false;
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
        public bool Modificar(VVenta_01 vVenta_01, int VentaId, int idAlmacen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //Trae el detalle de venta
                    var venta_01Antiguo = this.iVenta_01.TraerVenta_01(vVenta_01.Id);
                    if (venta_01Antiguo == null) { return false; }

                    var producto = iProducto.ListarXId(vVenta_01.IdProducto);
                    var observacion = "Venta: " + VentaId + " - IdProducto: " + vVenta_01.IdProducto + " | " + producto.Descripcion;
                    //Modifica el movimiento de inventario y actualiza el stock
                    if (!new LInventario().ModificarMovimientoInventario(vVenta_01.Id,
                                                                        vVenta_01.IdProducto,
                                                                        idAlmacen,
                                                                        venta_01Antiguo.Lote, venta_01Antiguo.FechaVencimiento,
                                                                        venta_01Antiguo.Cantidad, vVenta_01.Cantidad,
                                                                        (int)ENConcepto.VENTAS,
                                                                        observacion, UTGlobal.Usuario,
                                                                        vVenta_01.Lote, vVenta_01.FechaVencimiento))
                    { return false; }

                    //Modifica el detalle de venta
                    if (!this.iVenta_01.Modificar(vVenta_01)) { return false; }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdVenta, int IdDetalle, int idAlmacen, ref List<string> lMensaje)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //Trae el detalle de venta
                    var venta_01 = this.iVenta_01.TraerVenta_01(IdDetalle);
                    if (venta_01 == null) { return false; }

                    var StockActual = new LInventario().TraerStockActual(venta_01.IdProducto, idAlmacen, venta_01.Lote, venta_01.FechaVencimiento);
                    if (StockActual < venta_01.Cantidad)
                    {
                        var producto = iProducto.ListarXId(venta_01.IdProducto);
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
                    if (!new LInventario().EliminarMovimientoInventario(venta_01.Id, venta_01.IdProducto, idAlmacen,
                                                                         venta_01.Lote, venta_01.FechaVencimiento, venta_01.Cantidad,
                                                                         (int)ENConcepto.VENTAS, EnAccionEnInventario.Incrementar))
                    {
                        return false;
                    }

                    //Elimina el detalle de venta
                    if (!this.iVenta_01.Eliminar(IdVenta, IdDetalle)) { return false; }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Verificaciones

        #endregion
    }
}
