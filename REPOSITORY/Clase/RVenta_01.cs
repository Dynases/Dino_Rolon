﻿using DATA.EntityDataModel.DiAvi;
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
                        FechaVencimiento = vventa_01.FechaVencimiento
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
        public bool Modificar(VVenta_01 vventa_01, int ventaId)
        {
            try
            {               
                using (var db = GetEsquema())
                {
                    Venta_01 venta_01 = db.Venta_01.Where(a => a.Id == vventa_01.Id).FirstOrDefault();
                    if (venta_01 == null)
                    { throw new Exception("No existe la venta con id " + vventa_01.Id); }

                    venta_01.IdVenta = ventaId;
                    venta_01.IdProducto = vventa_01.IdProducto;
                    venta_01.Estado = (int)ENEstado.GUARDADO;
                    venta_01.Unidad = vventa_01.Unidad;
                    venta_01.Cantidad = vventa_01.Cantidad;
                    venta_01.Precio = vventa_01.PrecioVenta;
                    venta_01.PrecioCosto = vventa_01.PrecioCosto;
                    venta_01.Lote = vventa_01.Lote;
                    venta_01.FechaVencimiento = vventa_01.FechaVencimiento;                  
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
                    { throw new Exception("No existe la venta con id " + IdDetalle); }
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
                           PrecioVenta = v.Precio,
                           PrecioCosto = v.PrecioCosto,
                           Lote = v.Lote,
                           FechaVencimiento = v.FechaVencimiento,
                           Delete = ""
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
                           PrecioVenta = v.Precio,
                           PrecioCosto= v.PrecioCosto,
                           Lote=v.Lote,
                           FechaVencimiento=v.FechaVencimiento,                      
                           Delete = ""
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
