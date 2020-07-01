using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTI0021 : BaseConexion, ITI0021
    {
        #region Trasancciones

        public bool Guardar(int idTI002,
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
                        icibid = idTI002,
                        iclot = lote,
                        icid = db.TI0021.Max(t => t.icid) + 1
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
        #endregion

        #region Consultas

        public List<VDetalleKardex> ListarDetalleKardex(DateTime inicio, DateTime fin, int IdAlmacen, int codProducto)
        {
            try
            {
                var result = new List<VDetalleKardex>();

                using (var db = this.GetEsquema())
                {

                    var productos = db.Producto.ToList();
                    var listati002 = db.TI002.ToList();
                    var listati0021 = db.TI0021.ToList();

                    if (codProducto != 0)
                    {
                        productos= productos.Where(h => h.Id==codProducto).ToList();
                    }


                    foreach (var p in productos)
                    {
                        var detalle = new VDetalleKardex();

                        //PARA SALDO ANTERIOR
                        decimal entradasAnteriores = 0;
                        decimal salidasAnteriores = 0;
                        var detalleAnterior = listati002.Where(h => h.ibfdoc < inicio.Date && h.ibalm == IdAlmacen).ToList();
                        foreach (var i in detalleAnterior)
                        {                            
                            var detalleAnteriorProducto = listati0021.Where(ti => ti.iccprod == p.Id
                                                                             && ti.icibid == i.ibid ).FirstOrDefault();

                            if (detalleAnteriorProducto != null)
                            {
                                //INGRESOS
                                if (i.ibconcep == 1 || i.ibconcep == 3 ||
                                    i.ibconcep == 5 || i.ibconcep == 8 ||
                                    i.ibconcep == 9 || i.ibconcep == 7)
                                {
                                    entradasAnteriores += detalleAnteriorProducto.iccant;
                                }
                                //SALIDAS
                                else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                                    i.ibconcep == 6 || i.ibconcep == 10 || i.ibconcep == 11)
                                {
                                    salidasAnteriores += detalleAnteriorProducto.iccant;
                                }
                            }
                        }

                        //CALCULOS DEL PERIODO Y ALMACEN SELECCIONADOS
                        decimal entradasPeriodo = 0;
                        decimal salidasPeriodo = 0;
                        var detallePeriodo = listati002.Where(h => h.ibfdoc >= inicio.Date &&
                                                                   h.ibfdoc <= fin.Date &&
                                                                   h.ibalm == IdAlmacen).ToList();
                        foreach (var i in detallePeriodo)
                        {
                            var detalleProducto = db.TI0021.Where(ti => ti.iccprod == p.Id
                                                                             && ti.icibid == i.ibid).FirstOrDefault();

                            if (detalleProducto != null)
                            {
                                //INGRESOS
                                if (i.ibconcep == 1 || i.ibconcep == 3 ||
                                    i.ibconcep == 5 || i.ibconcep == 8 ||
                                    i.ibconcep == 9 || i.ibconcep == 7)
                                {
                                    entradasPeriodo += detalleProducto.iccant;
                                }
                                //SALIDAS
                                else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                                    i.ibconcep == 6 || i.ibconcep == 10 || i.ibconcep == 11)
                                {
                                    salidasPeriodo += detalleProducto.iccant;
                                }
                            }
                        }

                        detalle.Descripcion = p.Descrip;
                        detalle.Entradas = entradasPeriodo;
                        detalle.Id = p.Id;                        
                        detalle.SaldoAnterior = entradasAnteriores - salidasAnteriores;                        
                        detalle.Salidas = salidasPeriodo;
                        detalle.Saldo = (entradasPeriodo + detalle.SaldoAnterior ) - salidasPeriodo;
                        //detalle.Unidad = p.UniVen.ToString();
                        var Uni=db.Libreria.Where(l => l.IdGrupo == 3 &&
                                                      l.IdOrden == 6 &&
                                                      l.IdLibrer == p.UniVen)
                                               .FirstOrDefault();
                        detalle.Unidad = Uni.Descrip.ToString();
                       

                        result.Add(detalle);
                    }

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
