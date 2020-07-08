using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UTILITY.Enum;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RTraspaso_01 : BaseConexion, ITraspaso_01
    {

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VTraspaso_01 TraerTraspaso_01(int idDetalle)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso_01
                       .Where(d => d.Id == idDetalle && d.Traspaso.Estado != (int)ENEstado.ELIMINAR)
                       .Select(d => new VTraspaso_01
                       {
                           Id= d.Id,
                           IdTraspaso = d.IdTraspaso,
                           IdProducto = d.IdProducto,
                           Estado = d.Estado,
                           CodigoProducto = d.Producto.IdProd,
                           Producto = d.Producto.Descrip,
                           Cantidad = d.Cantidad,
                           Unidad = d.Unidad,
                           Contenido = d.Contenido,
                           TotalContenido = d.TotalContenido,
                           Precio = d.Precio,
                           Total = d.Total,
                           FechaVencimiento = d.FechaVencimiento,
                           Lote = d.Lote,
                           Delete = "",
                           Stock = db.TI001.FirstOrDefault(a => a.icalm == d.Traspaso.IdAlmacenOrigen &&
                                                                    a.iccprod == d.IdProducto &&
                                                                    a.iclot == d.Lote &&
                                                                    a.icfven == d.FechaVencimiento).iccven + d.Cantidad
                       }).FirstOrDefault();

                    return listResult;
                }
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
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso_01
                       .Where(d => d.IdTraspaso == idTraspaso && d.Traspaso.Estado != (int)ENEstado.ELIMINAR)
                       .Select(d => new VTraspaso_01
                       {
                           Id = d.Id,
                           IdTraspaso = d.IdTraspaso,
                           IdProducto = d.IdProducto,
                           Estado = d.Estado,
                           CodigoProducto =d.Producto.IdProd,
                           Producto = d.Producto.Descrip,                           
                           Unidad = d.Unidad,
                           Cantidad = d.Cantidad,
                           Contenido = d.Contenido,
                           TotalContenido = d.TotalContenido,
                           Precio = d.Precio,
                           Total = d.Total,
                           FechaVencimiento = d.FechaVencimiento,
                           Lote = d.Lote,
                           Delete = "",
                           Stock = db.TI001.FirstOrDefault(a => a.icalm == d.Traspaso.IdAlmacenOrigen &&
                                                                    a.iccprod == d.IdProducto &&
                                                                    a.iclot == d.Lote &&
                                                                    a.icfven == d.FechaVencimiento).iccven + d.Cantidad
                       }).ToList();

                    return listResult;
                }
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
                using (var db = this.GetEsquema())                     
                {
                    var listResult = db.Traspaso_01
                       .Where(d => d.IdTraspaso == idTraspaso && d.Traspaso.Estado != (int)ENEstado.ELIMINAR)
                       .Select(d => new VTraspaso_01
                       {
                           Id = d.Id,
                           IdTraspaso = 0,
                           IdProducto = d.IdProducto,
                           Estado = (int)ENEstado.NUEVO,
                           CodigoProducto = d.Producto.IdProd,
                           Producto = d.Producto.Descrip,
                           Unidad = d.Unidad,
                           Cantidad = 0,
                           Contenido = d.Contenido,
                           TotalContenido = 0,
                           Precio = db.Precio.FirstOrDefault(x => x.IdProduc == d.IdProducto &&
                                                                      x.IdSucursal == (db.Almacen.
                                                                                       FirstOrDefault(z => z.Id == d.Traspaso.IdAlmacenOrigen).Sucursal.Id) &&
                                                                                       x.IdPrecioCat == (int)ENCategoriaPrecio.COSTO).Precio1,
                           Total = d.Total,
                           FechaVencimiento = db.TI001.OrderBy(o=> o.icfven).FirstOrDefault(a => a.icalm == d.Traspaso.IdAlmacenOrigen &&
                                                                                a.iccprod == d.IdProducto &&
                                                                                a.iccven > 0).icfven,
                           Lote = db.TI001.OrderBy(o => o.icfven).FirstOrDefault(a => a.icalm == d.Traspaso.IdAlmacenOrigen &&
                                                                                 a.iccprod == d.IdProducto &&
                                                                                 a.iccven > 0).iclot,
                           Delete = "",
                           Stock = db.TI001.OrderBy(o => o.icfven).FirstOrDefault(a => a.icalm == d.Traspaso.IdAlmacenOrigen &&
                                                                                 a.iccprod == d.IdProducto &&
                                                                                 a.iccven > 0).iccven 
                       }).ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        public bool Nuevo(VTraspaso_01 vTraspaso_01, int idTraspaso )
        {
            try
            {
                using(var db = GetEsquema())
                {
                    var venta = db.Traspaso.Where(v => v.Id == idTraspaso).FirstOrDefault();
                    if (venta == null)
                    {
                        throw new Exception("No existe el Traspaso con id:" + idTraspaso);
                    }
                    var detalle = new Traspaso_01
                    {                   
                        IdTraspaso = idTraspaso,
                        IdProducto= vTraspaso_01.IdProducto,
                        Estado = (int)ENEstado.GUARDADO,
                        Cantidad = vTraspaso_01.Cantidad,
                        Unidad = vTraspaso_01.Unidad,
                        Contenido = vTraspaso_01.Contenido,
                        TotalContenido = vTraspaso_01.TotalContenido,
                        Precio = vTraspaso_01.Precio,
                        Total = vTraspaso_01.Total,
                        FechaVencimiento = vTraspaso_01.FechaVencimiento,
                        Lote = vTraspaso_01.Lote                     
                    };
                    db.Traspaso_01.Add(detalle);
                    db.SaveChanges();                    
                    return true;
                }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VTraspaso_01 vTraspaso_01)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var traspaso_01 = db.Traspaso_01.Where(v => v.Id == vTraspaso_01.Id).FirstOrDefault();
                    if (traspaso_01 == null)
                    {
                        throw new Exception("No existe el detalle de Traspaso con id:" + vTraspaso_01.Id);
                    }             
                    traspaso_01.IdProducto = vTraspaso_01.IdProducto;
                    traspaso_01.Estado = vTraspaso_01.Estado;
                    traspaso_01.Cantidad = vTraspaso_01.Cantidad;
                    traspaso_01.Unidad = vTraspaso_01.Unidad;
                    traspaso_01.Contenido = vTraspaso_01.Contenido;
                    traspaso_01.TotalContenido = vTraspaso_01.TotalContenido;
                    traspaso_01.Precio = vTraspaso_01.Precio;
                    traspaso_01.Total = vTraspaso_01.Total;
                    traspaso_01.FechaVencimiento = vTraspaso_01.FechaVencimiento;
                    traspaso_01.Lote = vTraspaso_01.Lote;                    
                    db.SaveChanges();                
                    return true;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Eliminar( int IdDetalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var traspaso_01 = db.Traspaso_01.Where(a => a.Id == IdDetalle).FirstOrDefault();
                    if (traspaso_01 == null)
                    { throw new Exception("No existe el detalle de Traspaso con id " + IdDetalle); }
                    db.Traspaso_01.Remove(traspaso_01);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ConfirmarRecepcionDetalle(List<Traspaso_01> detalle, int idTI2)
        {
            try
            {
                //DateTime fechaVen = Convert.ToDateTime("2017-01-01");
                //string lote = "20170101";
                //using (var db = GetEsquema())
                //{
                //    foreach (var i in detalle)
                //    {
                //        //ACTUALIZAMOS EL INVENTARIO
                //        if (!this.tI001.ActualizarInventario(i.ProductId,
                //                                            i.Traspaso.AlmacenDestino.Value,
                //                                            EnAccionEnInventario.Incrementar,
                //                                            Convert.ToDecimal(i.Cantidad),
                //                                            lote,
                //                                            fechaVen))
                //        {
                //            return false;
                //        }

                //        //REGISTRAMOS LA CONFIRMACION COMO UN REGISTRO DONDE SE ESPEFICICA LA RECEPCION DE ESE TRASPASO
                //        if (!this.tI0021.Guardar(idTI2, i.ProductId, i.Cantidad, lote, fechaVen))
                //        {
                //            return false;
                //        }
                //    }

                    return true;
                //}
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
