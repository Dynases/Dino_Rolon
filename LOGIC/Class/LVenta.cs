using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
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
        public LVenta()
        {
            iVenta = new RVenta();
            iVenta_01 = new RVenta_01();
            iProducto = new RProducto();
        }
        #region Transacciones

        public bool Guardar(VVenta vVenta, List<VVenta_01> detalle, ref int IdVenta, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = IdVenta;
                    result = iVenta.Guardar(vVenta, ref IdVenta);
                    if (aux == 0)//Nuevo
                    {
                        var resultDetalle = new LVenta_01().Nuevo(detalle, IdVenta,vVenta.IdAlmacen);
                    }
                    else//Modificar
                    {
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VVenta_01> detalleNuevo = new List<VVenta_01>();
                                detalleNuevo.Add(i);                            
                                if (!new LVenta_01().Nuevo(detalleNuevo, IdVenta,vVenta.IdAlmacen))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {                            
                                if (!new LVenta_01().Modificar(i, IdVenta,vVenta.IdAlmacen))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {                                
                                if (!new LVenta_01().Eliminar(IdVenta, i.Id,vVenta.IdAlmacen, ref lMensaje))
                                {
                                    return false;
                                }
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
        #endregion
    }
}
