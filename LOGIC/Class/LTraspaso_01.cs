using ENTITY.inv.TI0021.View;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Global;

namespace LOGIC.Class
{
    public class LTraspaso_01
    {
        protected ITraspaso_01 iTraspaso_01;
        protected IProducto iProducto;

        public LTraspaso_01()
        {
            iProducto = new RProducto();
            iTraspaso_01 = new RTraspaso_01();
        }
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VTraspaso_01 TraerTraspass_01(int idDetalle)
        {
            try
            {
                return this.iTraspaso_01.TraerTraspaso_01(idDetalle);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VTraspaso_01> TraerTraspasos_01(int idTraspaso)
        {
            try
            {
                return this.iTraspaso_01.TraerTraspasos_01(idTraspaso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTraspaso_01> TraerTraspasos_01Vacio(int idTraspaso)
        {
            try
            {
                return this.iTraspaso_01.TraerTraspasos_01Vacio(idTraspaso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTE ***********/
        #endregion

        #region Transacciones
        public bool NuevoMovimiento(List<VTraspaso_01> lTraspaso_01, int idAlmacen, int idMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var detalleMovimiento = new VTI0021();
                    foreach (var vTraspaso_01 in lTraspaso_01)
                    {
                        if (vTraspaso_01.Estado != (int)ENEstado.ELIMINAR)
                        {
                            detalleMovimiento.Cantidad = vTraspaso_01.Cantidad;
                            detalleMovimiento.IdProducto = vTraspaso_01.IdProducto;
                            detalleMovimiento.Lote = vTraspaso_01.Lote;
                            detalleMovimiento.FechaVencimiento = vTraspaso_01.FechaVencimiento;

                            //Registra el movimiento de inventario y actualiza el stock                        
                            if (!new LInventario().NuevoTraspasoInventario(detalleMovimiento,
                                                                           idAlmacen,
                                                                           EnAccionEnInventario.Incrementar,
                                                                           idMovimiento))
                            {
                                return false;
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
        public bool Nuevo(List<VTraspaso_01> lTraspaso_01, int idTraspaso, int idAlmacen, int idMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var detalleMovimiento = new VTI0021();
                    foreach (var vTraspaso_01 in lTraspaso_01)
                    {
                        if (!this.iTraspaso_01.Nuevo(vTraspaso_01, idTraspaso)) { return false; }

                        detalleMovimiento.Cantidad = vTraspaso_01.Cantidad;
                        detalleMovimiento.IdProducto = vTraspaso_01.IdProducto;
                        detalleMovimiento.Lote = vTraspaso_01.Lote;
                        detalleMovimiento.FechaVencimiento = vTraspaso_01.FechaVencimiento;

                        //Registra el movimiento de inventario y actualiza el stock                        
                        if (!new LInventario().NuevoTraspasoInventario(detalleMovimiento,
                                                                       idAlmacen,                                                                       
                                                                       EnAccionEnInventario.Descontar,
                                                                       idMovimiento))
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
        public bool Modificar(VTraspaso_01 vTraspaso_01, int idAlmacen, int idMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {              
                    var traspaso_01Antiguo = this.iTraspaso_01.TraerTraspaso_01(vTraspaso_01.Id);

                    var detalleMovimiento = new VTI0021()
                    {
                        Cantidad = traspaso_01Antiguo.Cantidad,
                        CantidadNueva = traspaso_01Antiguo.Cantidad,
                        IdProducto = traspaso_01Antiguo.IdProducto,
                        Lote = traspaso_01Antiguo.Lote,
                        FechaVencimiento = traspaso_01Antiguo.FechaVencimiento,
                        LoteNuevo = traspaso_01Antiguo.Lote,
                        FechaVencimientoNuevo = traspaso_01Antiguo.FechaVencimiento,
                    };

                    //Registra el movimiento de inventario y actualiza el stock                        
                    if (!new LInventario().ModificarTraspasoInventario(detalleMovimiento,
                                                                           idAlmacen,                                                                          
                                                                           idMovimiento))
                    {
                        return false;
                    }
                    if (!this.iTraspaso_01.Modificar(vTraspaso_01)) { return false; }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Eliminar(int idDetalle, int idAlmacen, int idMovimiento, ref List<string> lMensaje)
        {
            try
            {
                using (var scope = new TransactionScope())
                {                  
                    var traspaso_01 = this.iTraspaso_01.TraerTraspaso_01(idDetalle);
                    var detalleMovimiento = new VTI0021()
                    {
                        Cantidad = traspaso_01.Cantidad,                        
                        IdProducto = traspaso_01.IdProducto,
                        Lote = traspaso_01.Lote,
                        FechaVencimiento = traspaso_01.FechaVencimiento,                        
                    };                  

                    var StockActual = new LInventario().TraerStockActual(traspaso_01.IdProducto, idAlmacen, traspaso_01.Lote, traspaso_01.FechaVencimiento);
                    if (StockActual < traspaso_01.Cantidad)
                    {
                        var producto = iProducto.ListarXId(traspaso_01.IdProducto);
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
                    //Registra el movimiento de inventario y actualiza el stock                        
                    if (!new LInventario().EliminarTraspasoInventario(detalleMovimiento,
                                                                           idMovimiento,
                                                                           idAlmacen,
                                                                           (int)ENConcepto.TRANSFORMACION_SALIDA,
                                                                           EnAccionEnInventario.Incrementar))
                    {
                        return false;
                    }
                    if (!this.iTraspaso_01.Eliminar(idDetalle)) { return false; }
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
