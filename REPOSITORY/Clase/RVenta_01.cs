using DATA.EntityDataModel.DiAvi;
using ENTITY.ven.view;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RVenta_01 : BaseConexion, IVenta_01
    {         
        #region Trasancciones
        public bool Nuevo(VVenta_01 vventa_01, int ventaId, ref int idVentaDetalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var venta = db.Venta.Where(v => v.Id == ventaId).FirstOrDefault();
                    if (venta== null)
                    {
                        throw new Exception("No existe la venta con id:" + ventaId);
                    }
                    var detalle = new Venta_01
                    {
                        IdVenta = ventaId,
                        IdProducto = vventa_01.IdProducto,
                        Estado = (int)ENEstado.GUARDADO,
                        Unidad = vventa_01.Unidad,
                        Cantidad = vventa_01.Cantidad,
                        Precio = vventa_01.PrecioVenta,
                        PrecioCosto = vventa_01.PrecioCosto,
                        Lote = vventa_01.Lote,
                        FechaVencimiento = vventa_01.FechaVencimiento,
                        Contenido = vventa_01.Contenido,
                        TotalUnidad =vventa_01.TotalContenido,
                        
                    };
                    db.Venta_01.Add(detalle);
                    db.SaveChanges();
                    idVentaDetalle = detalle.Id;                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VVenta_01 vventa_01)
        {
            try
            {               
                using (var db = GetEsquema())
                {
                    Venta_01 venta_01 = db.Venta_01.Where(a => a.Id == vventa_01.Id).FirstOrDefault();
                    if (venta_01 == null)
                    { throw new Exception("No existe el detalle de venta con id " + vventa_01.Id); }                    
                    venta_01.IdProducto = vventa_01.IdProducto;
                    venta_01.Estado = (int)ENEstado.GUARDADO;
                    venta_01.Unidad = vventa_01.Unidad;
                    venta_01.Cantidad = vventa_01.Cantidad;
                    venta_01.Precio = vventa_01.PrecioVenta;
                    venta_01.PrecioCosto = vventa_01.PrecioCosto;
                    venta_01.Lote = vventa_01.Lote;
                    venta_01.FechaVencimiento = vventa_01.FechaVencimiento;
                    venta_01.Contenido = vventa_01.Contenido;
                    venta_01.TotalUnidad = vventa_01.TotalContenido;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdVenta, int IdDetalle)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    Venta_01 venta_01 = db.Venta_01.Where(a => a.Id == IdDetalle).FirstOrDefault();
                    if (venta_01 == null)
                    { throw new Exception("No existe el detalle de venta con id " + IdDetalle); }
                    db.Venta_01.Remove(venta_01);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public VVenta_01 TraerVenta_01(int idVentaDetalle)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Venta_01
                       .Where(v => v.Id == idVentaDetalle && v.Venta.Estado != (int)ENEstado.ELIMINAR)
                       .Select(v => new VVenta_01
                       {
                           Id = v.Id,
                           IdVenta = v.IdVenta,
                           IdProducto = v.IdProducto,
                           Estado = v.Estado,
                           CodigoProducto = v.Producto.IdProd,
                           CodigoBarra = v.Producto.CodBar,
                           Producto = v.Producto.Descrip,
                           Unidad = v.Unidad,
                           Cantidad = v.Cantidad,
                           Contenido= v.Contenido,
                           TotalContenido = v.TotalUnidad,
                           PrecioVenta = v.Precio,
                           PrecioCosto = v.PrecioCosto,
                           PrecioMinVenta = db.Precio.FirstOrDefault(a=>a.IdProduc == v.IdProducto &&
                                                                        a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                        a.IdPrecioCat == (int)ENCategoriaPrecio.B005).Precio1,
                           PrecioMaxVenta = db.Precio.FirstOrDefault(a => a.IdProduc == v.IdProducto &&
                                                                       a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                       a.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Precio1,                           
                           Lote = v.Lote,
                           FechaVencimiento = v.FechaVencimiento,
                           Delete = "",
                           Stock = db.TI001.FirstOrDefault(a => a.icalm == v.Venta.IdAlmacen &&
                                                                    a.iccprod == v.IdProducto &&
                                                                    a.iclot == v.Lote &&
                                                                    a.icfven == v.FechaVencimiento).iccven + v.Cantidad

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
      
        public List<VVenta_01> TraerVentas_01(int VentaId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Venta_01
                       .Where(v => v.Venta.Id == VentaId && v.Venta.Estado != (int)ENEstado.ELIMINAR)
                       .Select(v => new VVenta_01
                       {
                           Id = v.Id,
                           IdVenta = v.IdVenta,
                           IdProducto = v.IdProducto,
                           Estado = v.Estado,
                           CodigoProducto = v.Producto.IdProd,
                           CodigoBarra = v.Producto.CodBar,
                           Producto = v.Producto.Descrip,
                           Unidad = v.Unidad,
                           Cantidad = v.Cantidad,
                           Contenido = v.Contenido,
                           TotalContenido = v.TotalUnidad,
                           PrecioVenta = v.Precio,
                           PrecioCosto= v.PrecioCosto,
                           PrecioMinVenta = db.Precio.FirstOrDefault(a => a.IdProduc == v.IdProducto &&
                                                                      a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                      a.IdPrecioCat == (int)ENCategoriaPrecio.B005).Precio1,
                           PrecioMaxVenta = db.Precio.FirstOrDefault(a => a.IdProduc == v.IdProducto &&
                                                                       a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                       a.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Precio1,
                           Lote =v.Lote,
                           FechaVencimiento=v.FechaVencimiento,                      
                           Delete = "",
                           Stock = db.TI001.FirstOrDefault(a => a.icalm == v.Venta.IdAlmacen &&
                                                                    a.iccprod == v.IdProducto &&
                                                                    a.iclot == v.Lote &&
                                                                    a.icfven == v.FechaVencimiento).iccven + v.Cantidad
                       }).ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VVenta_01> TraerVentas_01Vacio(int VentaId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Venta_01
                       .Where(v => v.Venta.Id == VentaId && v.Venta.Estado != (int)ENEstado.ELIMINAR)
                       .Select(v => new VVenta_01
                       {
                           Id = v.Id,
                           IdVenta = 0,
                           IdProducto = v.IdProducto,
                           Estado = (int)ENEstado.NUEVO,
                           CodigoProducto = v.Producto.IdProd,
                           CodigoBarra = v.Producto.CodBar,
                           Producto = v.Producto.Descrip,
                           Unidad = v.Unidad,
                           Cantidad = 0,
                           Contenido = v.Contenido,
                           TotalContenido = 0,
                           PrecioVenta = v.Venta.Cliente.PrecioCat.Precio.FirstOrDefault().Precio1,                       
                           PrecioCosto = db.Precio.FirstOrDefault(x=> x.IdProduc == v.IdProducto &&
                                                                      x.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                      x.IdPrecioCat == (int)ENCategoriaPrecio.COSTO).Precio1,
                           PrecioMinVenta = db.Precio.FirstOrDefault(a => a.IdProduc == v.IdProducto &&
                                                                      a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                      a.IdPrecioCat == (int)ENCategoriaPrecio.B005).Precio1,
                           PrecioMaxVenta = db.Precio.FirstOrDefault(a => a.IdProduc == v.IdProducto &&
                                                                       a.IdSucursal == v.Venta.Almacen.Sucursal.Id &&
                                                                       a.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Precio1,
                           Lote = v.Lote,
                           FechaVencimiento = v.FechaVencimiento,
                           Delete = "",
                           Stock = db.TI001.FirstOrDefault(a => a.icalm == v.Venta.IdAlmacen &&
                                                                    a.iccprod == v.IdProducto &&
                                                                    a.iclot == v.Lote &&
                                                                    a.icfven == v.FechaVencimiento).iccven
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
    }
}
