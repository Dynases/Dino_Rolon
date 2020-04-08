using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Compra_01.View;
using UTILITY.Enum.EnEstaticos;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RCompra_01 : BaseConexion, ICompra_01
    {
        private readonly ITI001 tI001;
        public RCompra_01(ITI001 ti001)
        {
            this.tI001 = ti001;
        }

        #region Transacciones
        public bool Nuevo(List<VCompra_01> Lista, int IdCompra, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compra = db.Compra.Where(t => t.Id == IdCompra).FirstOrDefault();
                    var AlmacenSalida = db.Almacen.Where(a => a.Id.Equals(compra.IdAlmacen)).Select(a => a.Descrip).FirstOrDefault();
                    var Proveedor = db.Proveed.Where(c => c.Id == (compra.IdProvee)
                                                         && compra.Id == IdCompra).
                                                         Select(c => c.Descrip).FirstOrDefault();
                    var idAux = IdCompra;
                    foreach (var i in Lista)
                    {                        
                        Compra_01 compra_01 = new Compra_01();
                        compra_01.IdCompra = IdCompra;
                        compra_01.IdProducto = i.IdProducto;
                        compra_01.Estado = (int)ENEstado.GUARDADO;
                        compra_01.Canti = i.Cantidad;
                        compra_01.Costo = i.Costo;
                        compra_01.Lote = i.Lote;
                        compra_01.FechaVen = i.FechaVen;
                        compra_01.Utilidad = i.Utilidad;
                        compra_01.Porcent = i.Porcent;
                        compra_01.Utilidad = i.Utilidad;
                        compra_01.Total = i.Total;
                        db.Compra_01.Add(compra_01);
                        db.SaveChanges();
                        //Guarda el movimiento de inventario y actualiza el stock
                        var Observacion = (compra.Id + " - " + " I -Compra numiprod: " + i.IdProducto + "| " + Proveedor);
                        if (!tI001.NuevoMovimientoInventario(compra_01.Id,
                                                            i.IdProducto.ToString(),
                                                            compra.IdAlmacen,
                                                            i.Lote, i.FechaVen,
                                                            compra_01.Canti,
                                                            (int)ENConcepto.COMPRAS,
                                                            Observacion,
                                                            EnAccionEnInventario.Incrementar, 
                                                            usuario))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCompra_01 Lista, int IdCompra, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    Compra_01 compra_01 = db.Compra_01.Where(a => a.Id == Lista.Id).FirstOrDefault();
                    if (compra_01 == null)
                    { throw new Exception("No existe la compra con id " + Lista.Id); }

                    var compra = db.Compra.Where(t => t.Id == IdCompra).FirstOrDefault();
                    var AlmacenSalida = db.Almacen.Where(a => a.Id.Equals(compra.IdAlmacen)).Select(a => a.Descrip).FirstOrDefault();
                    var Proveedor = db.Proveed.Where(c => c.Id == (compra.IdProvee)
                                                         && compra.Id == IdCompra).
                                                         Select(c => c.Descrip).FirstOrDefault();                    

                    var Observacion = (compra.Id + " - " + " I -Compra numiprod: " + Lista.IdProducto + "| " + Proveedor);
                    //Modifica el movimiento de inventario, actualiza el stock, lote y fechavencimiento.
                    if (!tI001.ModificarMovimientoInventario(Lista.Id,
                                                       Lista.IdProducto.ToString(),
                                                        compra.IdAlmacen,
                                                        compra_01.Lote, compra_01.FechaVen,
                                                        compra_01.Canti, Lista.Cantidad,
                                                        (int)ENConcepto.COMPRAS,
                                                        Observacion, usuario,
                                                        Lista.Lote, Lista.FechaVen))
                    {
                        return false;
                    }
                    compra_01.IdProducto = Lista.IdProducto;
                    compra_01.Estado = (int)ENEstado.GUARDADO;
                    compra_01.Canti = Lista.Cantidad;
                    compra_01.Costo = Lista.Costo;
                    compra_01.Lote = Lista.Lote;
                    compra_01.FechaVen = Lista.FechaVen;
                    compra_01.Utilidad = Lista.Utilidad;
                    compra_01.Porcent = Lista.Porcent;
                    compra_01.Utilidad = Lista.Utilidad;
                    compra_01.Total = Lista.Total;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdCompra,int IdDetalle, ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compra = db.Compra.Where(t => t.Id == IdCompra).FirstOrDefault();
                    Compra_01 compra_01 = db.Compra_01.Where(a => a.Id == IdDetalle).FirstOrDefault();
                    if (compra_01 == null)
                    { throw new Exception("No existe la compra con id " + compra_01.Id); }
                    //vERIFICAR ELIMINADOS
                    var StockActual = this.tI001.StockActual(compra_01.IdProducto.ToString(), compra.IdAlmacen, compra_01.Lote, compra_01.FechaVen);
                    if (StockActual < compra_01.Canti)
                    {
                        var producto = db.Producto.Where(p => p.Id == compra_01.IdProducto).Select(p => p.Descrip).FirstOrDefault();
                        lMensaje.Add("No existe stock actual suficiente para el producto: " + producto);
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
                    if (!this.tI001.EliminarMovimientoInventario(compra_01.Id, compra_01.IdProducto.ToString(), compra.IdAlmacen,
                                                                     compra_01.Lote, compra_01.FechaVen, compra_01.Canti,
                                                                     (int)ENConcepto.COMPRAS, EnAccionEnInventario.Descontar))
                    {
                        return false;
                    }
                    db.Compra_01.Remove(compra_01);
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
        public List<VCompra_01> Lista(int IdCompra)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Compra_01
                                      join b in db.Producto on a.IdProducto equals b.Id
                                      join c in db.Libreria on b.UniVen equals c.IdLibrer
                                      where c.IdGrupo.Equals((int)ENEstaticosGrupo.PRODUCTO) && c.IdOrden.Equals((int)ENEstaticosOrden.PRODUCTO_UN_VENTA) &&
                                      a.IdCompra == IdCompra
                                      select new VCompra_01
                                      {
                                          Id = a.Id,
                                          IdCompra = a.IdCompra,
                                          IdProducto = a.IdProducto,
                                          Estado = a.Estado,
                                          Producto = b.Descrip,
                                          Cantidad = a.Canti,
                                          Unidad = c.Descrip,
                                          Costo = a.Costo,
                                          Total = a.Total,
                                          Lote = a.Lote,
                                          FechaVen = a.FechaVen,
                                          Utilidad = a.Utilidad,
                                          Porcent = a.Porcent
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
        //Hacer uno para Verificar cambios de lote en VENTA.
        public bool ExisteEnLoteEnUsoVenta_01(int IdProducto, string lote, DateTime? fechaVen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Compra
                                     join b in db.Compra_01 on a.Id equals b.IdCompra
                                     where b.IdProducto== IdProducto &&
                                           b.Lote.Equals(lote) &&
                                           b.FechaVen.Equals(fechaVen)&&
                                           a.Estado != (int)ENEstado.ELIMINAR
                                     select a).Count();
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
