using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Compra.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using System.Data.Entity;

namespace REPOSITORY.Clase
{
    public class RCompra : BaseConexion, ICompra
    {
        private readonly ITI001 tI001;
       
        public RCompra(ITI001 tI001)       
        {
            this.tI001 = tI001;
         
        }
        public bool Guardar(VCompra vcompra, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Compra Compra;
                    if (id > 0)
                    {
                        Compra = db.Compra.Where(a => a.Id == idAux).FirstOrDefault();
                        if (Compra == null)
                            throw new Exception("No existe la compra con id " + idAux);
                    }
                    else
                    {
                        Compra = new Compra();
                        db.Compra.Add(Compra);
                    }
                    Compra.IdAlmacen = vcompra.IdAlmacen;
                    Compra.IdProvee = vcompra.IdProvee;
                    Compra.Estado = vcompra.Estado;
                    Compra.FechaDoc = vcompra.FechaDoc;
                    Compra.TipoVenta = vcompra.TipoVenta;
                    Compra.FechaVen = vcompra.FechaVen;
                    Compra.TipoFactura = vcompra.TipoFactura;
                    Compra.Factura = vcompra.Factura;
                    Compra.Recibo = vcompra.Recibo;
                    Compra.Observ = vcompra.Observ;
                    Compra.Descu = vcompra.Descu;
                    Compra.Total = vcompra.Total;
                    Compra.Fecha = vcompra.Fecha;
                    Compra.Hora = vcompra.Hora;
                    Compra.Usuario = vcompra.Usuario;
                    db.SaveChanges();
                    id = Compra.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdCompra, int estado, ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {                
                    var compra = db.Compra.Where(c => c.Id.Equals(IdCompra)).FirstOrDefault();
                    var compra_01 = db.Compra_01.Where(c => c.IdCompra.Equals(IdCompra)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in compra_01)
                    {
                        var StockActual = this.tI001.StockActual(item.IdProducto.ToString(), compra.IdAlmacen, item.Lote, item.FechaVen);
                        if (StockActual < item.Canti)
                        {
                            var producto = db.Producto.Where(p => p.Id == item.IdProducto).Select(p => p.Descrip).FirstOrDefault();
                            lMensaje.Add("No existe stock actual suficiente para el producto: " + producto);
                        }
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
                    //Actualizar saldo, Eliminar Movimientos
                    foreach (var i in compra_01)
                    {
                        if (!this.tI001.EliminarMovimientoInventario(i.Id, i.IdProducto.ToString(), compra.IdAlmacen,
                                                                         i.Lote, i.FechaVen, i.Canti,
                                                                         (int)ENConcepto.COMPRAS, EnAccionEnInventario.Descontar))
                        {
                            return false;
                        }

                        //if (this.tI001.ExisteProducto(i.IdProducto.ToString(), compra.IdAlmacen, i.Lote, i.FechaVen))
                        //{
                        //    if (!this.tI001.ActualizarInventario(i.IdProducto.ToString(),
                        //                                   compra.IdAlmacen,
                        //                                   EnAccionEnInventario.Descontar,
                        //                                   Convert.ToDecimal(i.Canti),
                        //                                   i.Lote,
                        //                                   i.FechaVen))
                        //    {
                        //        return false;
                        //    }
                        //    //ELIMINA EL DETALLE DE MOVIMIENTO
                        //    this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRAS);
                        //    //ELIMINA EL MOVIMIENTO
                        //    this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRAS);
                        //}
                        //else
                        //{
                        //    //ELIMINA EL DETALLE DE MOVIMIENTO
                        //    this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRAS);
                        //    //ELIMINA EL MOVIMIENTO
                        //    this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRAS);
                        //}
                    }
                    compra.Estado = estado;
                    db.Compra.Attach(compra);
                    db.Entry(compra).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region CONSULTA
        public List<VCompraLista> Lista()
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Compra
                                      join b in db.Proveed on a.IdProvee equals b.Id
                                      where a.Estado != -1
                                      select new VCompraLista
                                      {
                                          Id = a.Id,
                                          IdAlmacen = a.IdAlmacen,
                                          IdProvee = a.IdProvee,
                                          Proveedor = b.Descrip,
                                          Estado = a.Estado,
                                          FechaDoc = a.FechaDoc,
                                          TipoVenta = a.TipoVenta,
                                          NombreTipo = a.TipoVenta == 1 ? "CONTADO" : "CREDITO",
                                          Descu = a.Descu,
                                          Total = a.Total,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario
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
