using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.TI001.VIew;
using ENTITY.inv.TI0021.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;

namespace REPOSITORY.Clase
{
    public class RTI001 : BaseConexion, ITI001
    {
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;
        public RTI001(ITI002 tI002, ITI0021 tI0021)
        {
            this.tI002 =  tI002;
            this.tI0021 = tI0021;
        }
        public RTI001()
        {

        }
        #region Trasancciones

        public bool Nuevo(int idAlmacen, int idProducto, decimal cantidad, string lote, DateTime fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti001 = new TI001
                    {
                        icalm = idAlmacen,
                        icuven = db.Producto.FirstOrDefault(a => a.Id == idProducto).UniVen,
                        iccprod = idProducto,
                        iccven = cantidad,
                        iclot = lote,
                        icfven = fechaVen                        
                    };
                    db.TI001.Add(ti001);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ActualizarInventario(int idProducto,
                                         int idAlmacen,
                                         EnAccionEnInventario accionEnInventario,
                                         decimal cantidad,
                                         string lote,
                                         DateTime fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var inventario = db.TI001.Where(i => i.icalm == idAlmacen
                                                    && i.iccprod == idProducto
                                                    && i.iclot.Equals(lote)
                                                    && i.icfven == fechaVen)
                                             .FirstOrDefault();

                    var stock = inventario.iccven;
                    if (accionEnInventario.Equals(EnAccionEnInventario.Incrementar))
                    { inventario.iccven = stock +  cantidad; }
                    else if (accionEnInventario.Equals(EnAccionEnInventario.Descontar))
                    { inventario.iccven = stock -cantidad; }

                    db.TI001.Attach(inventario);
                    db.Entry(inventario).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ActualizarInventario(int idProducto, int idAlmacen, decimal cantidad, string lote, DateTime fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var saldo = db.TI001.Where(i => i.icalm == idAlmacen
                                               && i.iccprod == idProducto
                                               && i.iclot.Equals(lote)
                                               && i.icfven == fechaVen)
                        .FirstOrDefault();

                    if (saldo == null)
                    {
                        saldo = new TI001
                        {
                            icalm = idAlmacen,
                            icuven = db.Producto.FirstOrDefault(a => a.Id == idProducto).UniVen,
                            iccprod = idProducto,
                            iccven = cantidad,
                            iclot = lote,
                            icfven = fechaVen
                        };

                        db.TI001.Add(saldo);
                    }
                    else
                    {
                        var stock = saldo.iccven;
                        saldo.iccven = stock + cantidad;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ActualizarInventarioModificados(int idProducto,
                                        int idAlmacen,                                      
                                        decimal cantidadAnterior,
                                        decimal cantidadNueva,
                                        string lote,
                                        DateTime fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var inventario = db.TI001.Where(i => i.icalm == idAlmacen
                                                    && i.iccprod == idProducto
                                                    && i.iclot.Equals(lote)
                                                    && i.icfven == fechaVen)
                                             .FirstOrDefault();
                    var stock = inventario.iccven;
                    inventario.iccven = stock- (cantidadAnterior - cantidadNueva);               
                    db.TI001.Attach(inventario);
                    db.Entry(inventario).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }     
        public bool NuevoMovimientoInventario(int idDetalle, int idProducto, int idAlmacen, string lote,
                                DateTime fechaVen, decimal cantidad, int concepto, string Observacion,
                                EnAccionEnInventario accion, string usuario)
        {
            try
            {
                if (cantidad > 0)
                {
                    if (!this.ExisteProducto(idProducto, idAlmacen, lote, fechaVen))
                    {
                        if (!this.Nuevo(idAlmacen, idProducto, cantidad, lote, fechaVen))
                        {
                            return false;
                        }
                    }
                    if (!this.ActualizarInventario(idProducto,
                                                     idAlmacen,
                                                     accion,
                                                     cantidad,
                                                     lote,
                                                     fechaVen))
                    {
                        return false;
                    }
                    int idMovimiento = 0;
                    //NUEVO EL MOVIMIENTO
                    if (!this.tI002.Guardar(idAlmacen,
                                        0, 
                                        idDetalle,
                                        usuario,
                                        Observacion,
                                        concepto,
                                        ref idMovimiento,0))
                    {
                        return false;
                    }
                    //NUEVO DETALLE DE MOVIMIENTO
                    if (!this.tI0021.Guardar(idMovimiento, Convert.ToInt32(idProducto),
                                          cantidad,
                                          lote,
                                          fechaVen))
                    {
                        return false;
                    }                  
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarMovimientoInventario(int idDetalle, int idProducto, int idAlmacen, string lote, 
                                        DateTime fechaVen, decimal cantidad, decimal cantidad2, int concepto,
                                        string Observacion, string usuario, string loteNuevo, DateTime fechaVenNuevo)
        {
            try
            {
                if (cantidad2 > 0)
                {                   
                    if (!this.ExisteProducto(idProducto, idAlmacen, lote, fechaVen))
                    {
                        if (!this.Nuevo(idAlmacen, idProducto, cantidad2, loteNuevo, fechaVenNuevo))
                        {
                            return false;
                        }
                    }
                    if (!this.ActualizarInventarioModificados(idProducto,
                                                    idAlmacen,
                                                    cantidad,
                                                    cantidad2,
                                                    loteNuevo,
                                                    fechaVenNuevo))
                    {
                        return false;
                    }
                    if (this.tI002.ExisteEnMovimiento(idDetalle, concepto))
                    {
                        //MODIFICA EL MOVIMIENTO
                        if (!this.tI002.Modificar(idAlmacen,
                                            0,
                                            idDetalle,
                                            usuario,
                                            Observacion,
                                            concepto,0))
                        {
                            return false;
                        }
                        //MODIFICA EL DETALLE DE MOVIMIENTO
                        if (!this.tI0021.Modificar(cantidad2,
                                              idDetalle,
                                              concepto,
                                              loteNuevo,
                                              fechaVenNuevo))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        int idMovimiento = 0;
                        //NUEVO EL MOVIMIENTO
                        if (!this.tI002.Guardar(idAlmacen, 
                                            0, 
                                            idDetalle,
                                            usuario,
                                            Observacion,
                                            concepto,
                                            ref idMovimiento,0))
                        {
                            return false;
                        }
                        //NUEVO DETALLE DE MOVIMIENTO
                        if (!this.tI0021.Guardar(idMovimiento, Convert.ToInt32(idProducto),
                                              cantidad,
                                              lote,
                                              fechaVen))
                        {
                            return false;
                        }
                    }
                }               
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarMovimientoInventario( int idDetalle,
                               int idProducto,
                               int idAlmacen,
                               string lote,
                               DateTime fechaVen,
                               decimal cantidad,
                               int concepto,                               
                               EnAccionEnInventario accion)
                               
        {
            try
            {
                if (cantidad == 0)
                {
                    return false;
                }
                if (this.ExisteProducto(idProducto, idAlmacen, lote, fechaVen))
                {
                    if (!this.ActualizarInventario(idProducto,
                                                 idAlmacen,
                                                 accion,
                                                 cantidad,
                                                 lote,
                                                 fechaVen))
                    {
                        return false;
                    }                    
                    if (this.tI002.ExisteEnMovimiento(idDetalle, concepto))
                    {
                        //ELIMINA EL DETALLE DE MOVIMIENTO
                        if (!this.tI0021.Eliminar(idDetalle, concepto))
                        {
                            return false;
                        }
                        //ELIMINA EL MOVIMIENTO
                        if (!this.tI002.Eliminar(idDetalle, concepto))
                        {
                            return false;
                        }
                    }                   
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool NuevoTraspasoInventario(VTI0021 detalle,  int idAlmacen,                               
                               EnAccionEnInventario accion, int idEncabezado)
        {
            try
            {
                if (detalle.Cantidad > 0)
                {
                    if (!this.ExisteProducto(detalle.IdProducto, idAlmacen, detalle.Lote, detalle.FechaVencimiento))
                    {
                        if (!this.Nuevo(idAlmacen, detalle.IdProducto, detalle.Cantidad,detalle.Lote, detalle.FechaVencimiento))
                        {
                            return false;
                        }
                    }
                    if (!this.ActualizarInventario(detalle.IdProducto,
                                                     idAlmacen,
                                                     accion,
                                                     detalle.Cantidad,
                                                     detalle.Lote,
                                                     detalle.FechaVencimiento))
                    {
                        return false;
                    }                    
                    //NUEVO DETALLE DE MOVIMIENTO
                    if (!this.tI0021.Guardar(idEncabezado, detalle.IdProducto,
                                          detalle.Cantidad,
                                          detalle.Lote, 
                                          detalle.FechaVencimiento))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarTraspasoInventario(VTI0021 detalle, int idAlmacen,int idEncabezado)
        {
            try
            {
                if (detalle.CantidadNueva > 0)
                {
                    if (!this.ExisteProducto(detalle.IdProducto, idAlmacen, detalle.Lote, detalle.FechaVencimiento))
                    {
                        if (!this.Nuevo(idAlmacen, detalle.IdProducto, detalle.CantidadNueva, detalle.LoteNuevo, detalle.FechaVencimientoNuevo))
                        {
                            return false;
                        }
                    }
                    if (!this.ActualizarInventarioModificados(detalle.IdProducto,
                                                    idAlmacen,
                                                    detalle.Cantidad,
                                                    detalle.CantidadNueva,
                                                    detalle.LoteNuevo,
                                                    detalle.FechaVencimientoNuevo))
                    {
                        return false;
                    }
                    //MODIFICA EL DETALLE DE MOVIMIENTO
                    if (!this.tI0021.ModificarTraspaso(detalle.CantidadNueva,
                                                      idEncabezado,
                                                      detalle.IdProducto,
                                                      detalle.LoteNuevo,
                                                      detalle.FechaVencimientoNuevo))
                    {
                        return false;
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarTraspasoInventario(VTI0021 detalle,int idEncabezado,                             
                               int idAlmacen,                             
                               int concepto,
                               EnAccionEnInventario accion)

        {
            try
            {
                if (detalle.Cantidad == 0)
                {
                    return false;
                }
                if (this.ExisteProducto(detalle.IdProducto, idAlmacen, detalle.Lote, detalle.FechaVencimiento))
                {
                    if (!this.ActualizarInventario(detalle.IdProducto,
                                                      idAlmacen,
                                                      accion,
                                                      detalle.Cantidad,
                                                      detalle.Lote,
                                                      detalle.FechaVencimiento))
                    {
                        return false;
                    }
                    if (this.tI002.ExisteEnMovimiento(idEncabezado, concepto))
                    {
                        //ELIMINA EL DETALLE DE MOVIMIENTO
                        if (!this.tI0021.EliminarTraspaso(idEncabezado,
                                                      detalle.IdProducto,
                                                      detalle.LoteNuevo,
                                                      detalle.FechaVencimiento))
                        {
                            return false;
                        }                       
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region Consulta

        /******** VALOR/REGISTRO ÚNICO *********/
        public decimal TraerStockActual(int IdProducto, int idAlmacen, string lote, DateTime fecha)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cantidad = db.TI001.Where(c => c.icalm == idAlmacen &&
                                                       c.iccprod == IdProducto &&
                                                       c.iclot.Equals(lote) &&
                                                       c.icfven == fecha).Select(c => c.iccven).FirstOrDefault();
                    return cantidad;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public List<VTI001> TraerInventarioLotes(int IdProducto, int idAlmacen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                  
                    var listResult = (from a in db.TI001
                                      where a.iccprod.Equals(IdProducto)  &&
                                            a.icalm == idAlmacen && a.iccven > 0
                                        orderby a.icfven ascending
                                      select new VTI001
                                      {
                                          id = a.id,
                                          IdAlmacen = a.icalm,
                                          IdProducto = a.iccprod,
                                          Cantidad = a.iccven,
                                          Unidad = a.icuven,
                                          Lote = a.iclot,
                                          FechaVen = a.icfven
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }      
        #endregion


        #endregion
        #region Verificacion
        public bool ExisteProducto(int IdProducto, int idAlmacen, string lote, DateTime fecha)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = db.TI001.Where(c => c.icalm == idAlmacen &&
                                                       c.iccprod == IdProducto &&
                                                       c.iclot.Equals(lote) &&
                                                       c.icfven == fecha).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #endregion
    }
}
