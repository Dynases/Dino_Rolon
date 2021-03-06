﻿using DATA.EntityDataModel.DiAvi;
using ENTITY.ven.Report;
using ENTITY.ven.view;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RVenta : BaseConexion, IVenta
    {
        #region Venta
        #region Trasanciones

        public bool Guardar(VVenta VVenta, ref int id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {

                    var idAux = id;
                    Venta venta;
                    if (id > 0)
                    {
                        venta = db.Venta.Where(a => a.Id == idAux).FirstOrDefault();
                        if (venta == null)
                            throw new Exception("No existe la Venta con id " + idAux);
                    }
                    else
                    {
                        venta = new Venta();
                        db.Venta.Add(venta);
                    }
                    venta.IdAlmacen = VVenta.IdAlmacen;
                    venta.IdCliente = VVenta.IdCliente;
                    venta.FechaVenta = VVenta.FechaVenta;
                    venta.Estado = VVenta.Estado;
                    venta.Tipo = VVenta.Tipo;
                    venta.Observaciones = VVenta.Observaciones;
                    venta.EncPrVenta = VVenta.EncPrVenta;
                   
                    venta.EncEntrega = VVenta.EncEntrega;
                    venta.EmpresaFactura = VVenta.FacturaEmpresa;
                    venta.EncVenta = VVenta.EncVenta;
                    venta.Fecha = VVenta.Fecha;
                    venta.Hora = VVenta.Hora;
                    venta.Usuario = VVenta.Usuario;
                    venta.IdPedidoDisoft = 0;
                    venta.IdCompraIngreso = VVenta.IdCompraIngreso;
                    venta.FacturaExterna = VVenta.FacturaExterna;
                    db.SaveChanges();
                    id = venta.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdVenta, int estado)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var venta = db.Venta.Where(c => c.Id.Equals(IdVenta)).FirstOrDefault();
                    venta.Estado = estado;
                    db.Venta.Attach(venta);
                    db.Entry(venta).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GuardarIdPedido(int IdVenta, int idPedido)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var venta = db.Venta.Where(c => c.Id.Equals(IdVenta)).FirstOrDefault();
                    venta.IdPedidoDisoft = idPedido;
                    db.Venta.Attach(venta);
                    db.Entry(venta).State = EntityState.Modified;
                    db.SaveChanges();
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
        public VVenta TraerVenta(int idVenta)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.Venta.Where(c => c.Id == idVenta &&
                                          c.Estado != (int)ENEstado.ELIMINAR)
                             .Select(v => new VVenta
                             {
                                 DescripcionAlmacen = v.Almacen.Descrip,
                                 DescripcionCliente = v.Cliente.Descrip,
                                 EncEntrega = v.EncEntrega,
                                 EncPrVenta = v.EncPrVenta,
                                 FacturaEmpresa = v.EmpresaFactura,
                               
                                 EncVenta = v.EncVenta,
                                 Estado = v.Estado,
                                 Fecha = v.Fecha,
                                 FechaVenta = v.FechaVenta,
                                 Id = v.Id,
                                 IdAlmacen = v.IdAlmacen,
                                 IdCliente = v.IdCliente,
                                 NitCliente = v.Cliente.Nit,
                                 Observaciones = v.Observaciones,
                                 Tipo = v.Tipo,
                                 Usuario = v.Usuario,
                                 Hora = v.Hora,
                                 IdCategoriaCliente = v.Cliente.IdCategoria,
                                 IdCompraIngreso = v.IdCompraIngreso,
                                 IdPedidoDisoft = v.IdPedidoDisoft,
                                 FacturaExterna = v.FacturaExterna
                             }).FirstOrDefault();
                }
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
                using (var db = this.GetEsquema())
                {
                    return db.Venta
                            .OrderBy(x => x.Id)
                            .Where(x => x.Estado != (int)ENEstado.ELIMINAR &&
                               (db.Usuario_01
                               .Where(b => b.IdUsuario == usuarioId &&
                                           b.Acceso == true)
                               .Select(d => d.IdAlmacen)).Contains(x.IdAlmacen))
                            .OrderByDescending(a=>a.Id)
                             .Select(v => new VVenta
                             {
                                 DescripcionAlmacen = v.Almacen.Descrip,
                                 DescripcionCliente = v.Cliente.Descrip,
                                 EncEntrega = v.EncEntrega,
                                 EncPrVenta = v.EncPrVenta,
                                 FacturaEmpresa = v.EmpresaFactura,                              
                                 EncVenta = v.EncVenta,
                                 Estado = v.Estado,
                                 Fecha = v.Fecha,
                                 FechaVenta = v.FechaVenta,
                                 Id = v.Id,
                                 IdAlmacen = v.IdAlmacen,
                                 IdCliente = v.IdCliente,
                                 NitCliente = v.Cliente.Nit,
                                 Observaciones = v.Observaciones,
                                 Tipo = v.Tipo,
                                 Usuario = v.Usuario,
                                 Hora = v.Hora,
                                 IdCategoriaCliente = v.Cliente.IdCategoria,
                                 IdCompraIngreso = v.IdCompraIngreso,
                                 FacturaExterna = v.FacturaExterna,
                                 IdPedidoDisoft = v.IdPedidoDisoft
                             }).ToList();
                }
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
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Report_Venta
                        .Where(x => x.ventaId == ventaId)
                        .Select(ti => new VVentaTicket
                        {
                            ventaId = ti.ventaId,
                            FechaVenta = ti.FechaVenta,
                            alamcen = ti.alamcen,
                            Cliente = ti.Cliente,
                            Nit = ti.Nit,
                            FacturaExterna = ti.FacturaExterna,
                            IdCompraIngreso = ti.IdCompraIngreso,
                            EncEntrega = ti.EncEntrega,
                            encVenta = ti.encVenta,                            
                            detalleId = ti.detalleId,
                            Producto = ti.Producto,
                            Cantidad = ti.Cantidad,
                            Precio = ti.Precio,
                            Total = ti.Total.Value,
                            TotalUnidad = ti.TotalUnidad
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
        #region Verificaciones

        #endregion

        #endregion

        #region Detalle
        public void GuardarDetalle(List<VVenta_01> detalle, int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    Venta_01 data;
                    foreach (var item in detalle)
                    {
                        switch (item.Estado)
                        {
                            case (int)ENEstado.NUEVO:
                                data = new Venta_01();
                                data.IdVenta = id;
                                data.IdProducto = item.IdProducto;
                                data.Estado = (int)ENEstado.GUARDADO;
                                data.Unidad = item.Unidad;
                                data.Cantidad = item.Cantidad;
                                data.Precio = item.PrecioVenta;
                                data.PrecioCosto = item.PrecioCosto;
                                data.Contenido = item.Contenido;
                                data.Lote = item.Lote;
                                data.FechaVencimiento = item.FechaVencimiento;
                                data.TotalUnidad = item.TotalContenido;
                                db.Venta_01.Add(data);
                                db.SaveChanges();
                                break;
                            case (int)ENEstado.MODIFICAR:
                                data = db.Venta_01.Where(a => a.Id == item.Id).FirstOrDefault();
                                data.IdProducto = item.IdProducto;
                                data.Estado = (int)ENEstado.GUARDADO;
                                data.Unidad = item.Unidad;
                                data.Cantidad = item.Cantidad;
                                data.Precio = item.PrecioVenta;
                                data.PrecioCosto = item.PrecioCosto;
                                data.Contenido = item.Contenido;
                                data.Lote = item.Lote;
                                data.FechaVencimiento = item.FechaVencimiento;
                                data.TotalUnidad = item.TotalContenido;
                                break;
                            case (int)ENEstado.ELIMINAR:
                                data = db.Venta_01.Where(a => a.Id == item.Id).FirstOrDefault();
                                db.Venta_01.Remove(data);
                                break;
                        }
                    }
                    db.SaveChanges();
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
