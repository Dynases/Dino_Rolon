using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Seleccion.View;
using DATA.EntityDataModel.DiAvi;
using ENTITY.com.Seleccion.Report;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{

    public class RSeleccion : BaseConexion, ISeleccion
    {
        private readonly ITI001 tI001;
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;

        public RSeleccion(ITI001 tI001, ITI002 tI002, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI002 = tI002;
            this.tI0021 = tI0021;
        }
        #region tRANSACCIONES
        public bool Guardar(VSeleccion vSeleccion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Seleccion seleccion;
                    if (id > 0)
                    {
                        seleccion = db.Seleccion.Where(a => a.Id == idAux).FirstOrDefault();
                        if (seleccion == null)
                            throw new Exception("No existe la seleccion con id " + idAux);
                    }
                    else
                    {
                        seleccion = new Seleccion();
                        db.Seleccion.Add(seleccion);
                    }
                    seleccion.IdAlmacen = vSeleccion.IdAlmacen;
                    seleccion.IdCompraIng = vSeleccion.IdCompraIng;
                    seleccion.Estado = vSeleccion.Estado;
                    seleccion.FechaReg = vSeleccion.FechaReg;
                    seleccion.Cantidad = vSeleccion.Cantidad ;
                    seleccion.Precio = vSeleccion.Precio;
                    seleccion.Total = vSeleccion.Total;                  
                    seleccion.Fecha = vSeleccion.Fecha;
                    seleccion.Hora = vSeleccion.Hora;
                    seleccion.Usuario = vSeleccion.Usuario;
                    seleccion.Merma = vSeleccion.Merma;
                    db.SaveChanges();
                    id = seleccion.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdSeleccion, int estado,ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    #region Validacion
                    DateTime fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";
                    var seleccion = db.Seleccion.Where(c => c.Id.Equals(IdSeleccion)).FirstOrDefault();
                    var seleccion_01 = db.Seleccion_01.Where(c => c.IdSeleccion.Equals(IdSeleccion)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in seleccion_01)
                    {
                        var StockActual = this.tI001.TraerStockActual(item.IdProducto, seleccion.IdAlmacen, lote, fechaVen);
                        if (StockActual < item.Cantidad)
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
                    #endregion
                    #region Actualiza TI001, TI002 Y TI0021
                    //Actualizar saldo, Eliminar Movimientos
                    foreach (var i in seleccion_01)
                    {
                        if (i.Cantidad > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProducto, seleccion.IdAlmacen, lote, fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProducto,
                                                               seleccion.IdAlmacen,
                                                               EnAccionEnInventario.Descontar,
                                                               Convert.ToDecimal(i.Cantidad),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                            }
                        }
                        
                    }

                    //INGRESA EL STOCK DE LA COMPRA
                    var idCompra = db.CompraIng_01
                                                .Where(c => c.IdCompra == db.Seleccion
                                                                                        .Where(s => s.Id == IdSeleccion)
                                                                                        .Select(d => d.IdCompraIng)
                                                                                        .FirstOrDefault())
                                                                                        .Select(a => a.IdCompra)
                                                                                        .FirstOrDefault();
                    var compraing_01 = db.CompraIng_01
                                            .Where(c => c.IdCompra == idCompra).ToList();
                    var compraIng = db.CompraIng
                                            .Where(c => c.Id.Equals(idCompra))
                                            .FirstOrDefault();
                    foreach (var i in compraing_01)
                    {
                        if (i.TotalCant > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProduc,
                                                      compraIng.IdAlmacen,
                                                      lote,
                                                      fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProduc,
                                                               compraIng.IdAlmacen,
                                                               EnAccionEnInventario.Incrementar,
                                                               Convert.ToDecimal(i.TotalCant),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_SALIDA);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_SALIDA);
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_SALIDA);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_SALIDA);
                            }
                        }                                                                
                    }
                    #endregion
                    compraIng.Estado = (int)ENEstado.GUARDADO;
                    db.CompraIng.Attach(compraIng);
                    db.Entry(compraIng).State = EntityState.Modified;

                    seleccion.Estado = estado;
                    db.Seleccion.Attach(seleccion);                   
                    db.Entry(seleccion).State = EntityState.Modified;
                  
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
        #region CONSULTAS
        public List<VSeleccionLista> Listar()
        {
            try
            {
                //var idGrupo = (int)ENEstaticosGrupo.COMPRA_INGRESO;
                //var idOrden = (int)ENEstaticosOrden.COMPRA_INGRESO_PLACA;
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Seleccion
                                      join b in db.CompraIng on
                                      new { idCompraIng = a.IdCompraIng }
                                        equals new { idCompraIng = b.Id }
                                      join c in db.Proveed on
                                      new { idProve = b.IdProvee }
                                            equals new { idProve = c.Id }  
                                      where a.Estado != (int)ENEstado.ELIMINAR
                                      select new VSeleccionLista
                                      {
                                          Id = a.Id,
                                          IdCompraIng = a.IdCompraIng,
                                          Granja = b.NumNota,
                                          FechaReg = a.FechaReg,
                                          FechaEntrega = b.FechaEnt,
                                          FechaRecepcion = b.FechaRec,
                                          Proveedor = c.Descrip,
                                          Placa = b.Placa,
                                          Tipo = b.Tipo,
                                          Edad = b.EdadSemana,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario,
                                          IdAlmacen = b.IdAlmacen,
                                          Merma = a.Merma,
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
