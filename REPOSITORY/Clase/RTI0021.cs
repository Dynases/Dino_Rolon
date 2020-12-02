using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RTI0021 : BaseConexion, ITI0021
    {
        #region Trasancciones

        public bool Guardar(int idEncabezado,
                            int idProducto,
                            decimal cantidad,
                            string lote,
                            DateTime fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti0021 = new TI0021
                    {
                        iccant = cantidad,
                        iccprod = idProducto,
                        icfvenc = fechaVen,
                        icibid = idEncabezado,
                        iclot = lote,
                        icid = db.TI0021.Select(a => a.icid).DefaultIfEmpty(0).Max() + 1
                    };

                    db.TI0021.Add(ti0021);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(decimal cantidad,
                            int IdDetalle,
                            int concepto,
                            string lote,
                            DateTime fechaVen)

        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti0021 = db.TI0021.FirstOrDefault(b => b.icibid == db.TI002.FirstOrDefault(c => c.ibiddc == IdDetalle &&
                                                                                                         c.ibconcep == concepto)
                                                                                                        .ibid);
                    ti0021.iclot = lote;
                    ti0021.icfvenc = fechaVen;
                    ti0021.iccant = cantidad;            
                    db.TI0021.Attach(ti0021);
                    db.Entry(ti0021).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdDetalle, int concepto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var tI0021 = db.TI0021.FirstOrDefault(b => b.icibid == db.TI002.FirstOrDefault(c => c.ibiddc == IdDetalle &&
                                                                                                       c.ibconcep == concepto)
                                                                                                    .ibid);
                    db.TI0021.Remove(tI0021);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModificarTraspaso(decimal cantidad,
                           int idEncabezado,
                           int idProducto,
                           string lote,
                           DateTime fechaVen)

        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti0021 = db.TI0021.FirstOrDefault(b => b.icibid == idEncabezado &&
                                                               b.iccprod == idProducto &&
                                                               b.iclot == lote && 
                                                               b.icfvenc == fechaVen);                   
                    ti0021.iccant = cantidad;
                    db.TI0021.Attach(ti0021);
                    db.Entry(ti0021).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarTraspaso(int idEncabezado,
                                       int idProducto,
                                       string lote,
                                       DateTime fechaVen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var ti0021 = db.TI0021.FirstOrDefault(b => b.icibid == idEncabezado &&
                                                                b.iccprod == idProducto &&
                                                                b.iclot == lote &&
                                                                b.icfvenc == fechaVen);
                    db.TI0021.Remove(ti0021);
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

        public List<VDetalleKardex> ListarDetalleKardex(DateTime inicio, DateTime fin, int IdAlmacen, int codProducto)
        {
            try
            {
                var result = new List<VDetalleKardex>();

                using (var db = this.GetEsquema())
                {
                    //IQueryable<Producto> pro = null;
                    var productos = db.Producto.Where(x=> db.TI0021.Select(a => a.iccprod).Contains(x.Id)).OrderBy(x=> x.Id).AsQueryable();
                    //var productos = db.Producto.ToList();
                    var listati002 = db.TI002.AsQueryable();
                    var listati0021 = db.TI0021.AsQueryable();                   
                    if (codProducto != 0)
                    {
                        productos = productos.Where(h => h.Id == codProducto);
                    }
                    foreach (var p in productos)
                    {
                        var detalle = new VDetalleKardex();

                        //PARA SALDO ANTERIOR
                        decimal entradasAnteriores = 0;
                        decimal salidasAnteriores = 0;          
                        var detalleAnterior = listati002.Where(h => h.ibfdoc < inicio.Date && h.ibalm == IdAlmacen);
                        if (detalleAnterior != null)
                        {
                            var entradaDetalleAnterior = detalleAnterior.Where(z => db.TCI001.Where(c => c.cpmov == 1)
                                                                                                               .Select(c => c.cpnumi)
                                                                                                               .Contains(z.ibconcep));
                            var salidaDetalleAnterior = detalleAnterior.Where(z => db.TCI001.Where(c => c.cpmov != 1)
                                                                                                         .Select(c => c.cpnumi)
                                                                                                         .Contains(z.ibconcep));

                            if (entradaDetalleAnterior.Any())
                            {
                                entradasAnteriores = listati0021.Where(x => entradaDetalleAnterior.Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id).Any() ?
                                                                                       listati0021.Where(x => entradaDetalleAnterior
                                                                                                   .Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id)
                                                                                                   .Sum(a => a.iccant) : 0;
                            }
                            if (salidaDetalleAnterior.Any())
                            {
                                salidasAnteriores = listati0021.Where(x => salidaDetalleAnterior.Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id).Any() ?
                                                                                       listati0021.Where(x => salidaDetalleAnterior
                                                                                                   .Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id)
                                                                                                   .Sum(a => a.iccant) : 0;
                            }
                        }
                        //CALCULOS DEL PERIODO Y ALMACEN SELECCIONADOS
                        decimal entradasPeriodo = 0;
                        decimal salidasPeriodo = 0;                      
                        var detallePeriodo = listati002.Where(h => h.ibfdoc >= inicio.Date &&
                                                                   h.ibfdoc <= fin.Date &&
                                                                   h.ibalm == IdAlmacen);

                        if (detallePeriodo !=null)
                        {
                            var entradaDetallePeriodo = detallePeriodo.Where(z => db.TCI001.Where(c => c.cpmov == 1)
                                                                                                                 .Select(c => c.cpnumi)
                                                                                                                 .Contains(z.ibconcep));
                            var salidaDetallePeriodo = detallePeriodo.Where(z => db.TCI001.Where(c => c.cpmov != 1)
                                                                                                         .Select(c => c.cpnumi)
                                                                                                         .Contains(z.ibconcep));

                            if (entradaDetallePeriodo.Any())
                            {
                                entradasPeriodo = listati0021.Where(x => entradaDetallePeriodo.Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id).Any() ?
                                                                                       listati0021.Where(x => entradaDetallePeriodo
                                                                                                   .Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id)
                                                                                                   .Sum(a => a.iccant) : 0;
                            }
                            if (salidaDetallePeriodo.Any())
                            {
                                salidasPeriodo = listati0021.Where(x => salidaDetallePeriodo.Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id).Any() ?
                                                                                       listati0021.Where(x => salidaDetallePeriodo
                                                                                                   .Select(z => z.ibid).Contains(x.icibid) && x.iccprod == p.Id)
                                                                                                   .Sum(a => a.iccant) : 0;
                            }
                        }
                        detalle.Descripcion = p.Descrip;
                        detalle.Entradas = entradasPeriodo;
                        detalle.Id = p.Id;
                        detalle.SaldoAnterior = entradasAnteriores - salidasAnteriores;
                        detalle.Salidas = salidasPeriodo;
                        detalle.Saldo = (entradasPeriodo + detalle.SaldoAnterior) - salidasPeriodo;
                        //detalle.Unidad = p.UniVen.ToString();
                        var Uni = db.Libreria.Where(l => l.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO &&
                                                        l.IdOrden == (int)ENEstaticosOrden.PRODUCTO_UN_VENTA &&
                                                        l.IdLibrer == p.UniVen)
                                               .FirstOrDefault();
                        detalle.Unidad = Uni.Descrip.ToString();
                        result.Add(detalle);
                    }

                    //foreach (var p in productos)
                    //{
                    //    var detalle = new VDetalleKardex();

                    //    //PARA SALDO ANTERIOR
                    //    decimal entradasAnteriores = 0;
                    //    decimal salidasAnteriores = 0;
                    //    var detalleAnterior = listati002.Where(h => h.ibfdoc < inicio.Date && h.ibalm == IdAlmacen).ToList();
                    //    foreach (var i in detalleAnterior)
                    //    {                            
                    //        var detalleAnteriorProducto = listati0021.Where(ti => ti.iccprod == p.Id
                    //                                                         && ti.icibid == i.ibid ).FirstOrDefault();

                    //        if (detalleAnteriorProducto != null)
                    //        {
                    //            //INGRESOS
                    //            if (i.ibconcep == 1 || i.ibconcep == 3 ||
                    //                i.ibconcep == 5 || i.ibconcep == 8 ||
                    //                i.ibconcep == 9 || i.ibconcep == 7)
                    //            {
                    //                entradasAnteriores += detalleAnteriorProducto.iccant;
                    //            }
                    //            //SALIDAS
                    //            else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                    //                i.ibconcep == 6 || i.ibconcep == 10 || i.ibconcep == 11)
                    //            {
                    //                salidasAnteriores += detalleAnteriorProducto.iccant;
                    //            }
                    //        }
                    //    }

                    //    //CALCULOS DEL PERIODO Y ALMACEN SELECCIONADOS
                    //    decimal entradasPeriodo = 0;
                    //    decimal salidasPeriodo = 0;
                    //    var detallePeriodo = listati002.Where(h => h.ibfdoc >= inicio.Date &&
                    //                                               h.ibfdoc <= fin.Date &&
                    //                                               h.ibalm == IdAlmacen).ToList();
                    //    foreach (var i in detallePeriodo)
                    //    {
                    //        var detalleProducto = db.TI0021.Where(ti => ti.iccprod == p.Id
                    //                                                         && ti.icibid == i.ibid).FirstOrDefault();

                    //        if (detalleProducto != null)
                    //        {
                    //            //INGRESOS
                    //            if (i.ibconcep == 1 || i.ibconcep == 3 ||
                    //                i.ibconcep == 5 || i.ibconcep == 8 ||
                    //                i.ibconcep == 9 || i.ibconcep == 7)
                    //            {
                    //                entradasPeriodo += detalleProducto.iccant;
                    //            }
                    //            //SALIDAS
                    //            else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                    //                i.ibconcep == 6 || i.ibconcep == 10 || i.ibconcep == 11)
                    //            {
                    //                salidasPeriodo += detalleProducto.iccant;
                    //            }
                    //        }
                    //    }

                    //    detalle.Descripcion = p.Descrip;
                    //    detalle.Entradas = entradasPeriodo;
                    //    detalle.Id = p.Id;                        
                    //    detalle.SaldoAnterior = entradasAnteriores - salidasAnteriores;                        
                    //    detalle.Salidas = salidasPeriodo;
                    //    detalle.Saldo = (entradasPeriodo + detalle.SaldoAnterior ) - salidasPeriodo;
                    //    //detalle.Unidad = p.UniVen.ToString();
                    //    var Uni=db.Libreria.Where(l => l.IdGrupo == 3 &&
                    //                                  l.IdOrden == 6 &&
                    //                                  l.IdLibrer == p.UniVen)
                    //                           .FirstOrDefault();
                    //    detalle.Unidad = Uni.Descrip.ToString();


                    //    result.Add(detalle);
                    //}

                    return result;
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
