using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Almacen.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTI0021 : BaseConexion, ITI0021
    {
        #region Trasancciones

        public bool Guardar(int idTI002,
                            int idProducto,
                            int cantidad)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti0021 = new TI0021
                    {
                        iccant = cantidad,
                        iccprod = idProducto,
                        icfvenc = null,
                        icibid = idTI002,
                        iclot = "",
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

        #endregion

        #region Consultas

        public List<VDetalleKardex> ListarDetalleKardex(DateTime inicio, DateTime fin, int IdAlmacen)
        {
            try
            {
                var result = new List<VDetalleKardex>();

                using (var db = this.GetEsquema())
                {

                    var productos = db.Producto.ToList();
                    var listati002 = db.TI002;
                    var listati0021 = db.TI0021;

                    foreach (var p in productos)
                    {
                        var detalle = new VDetalleKardex();

                        //PARA SALDO ANTERIOR
                        decimal entradasAnteriores = 0;
                        decimal salidasAnteriores = 0;
                        var detalleAnterior = listati002.Where(h => h.ibfdoc < inicio && h.ibalm == IdAlmacen).ToList();
                        foreach (var i in detalleAnterior)
                        {
                            var detalleAnteriorProducto = listati0021.Where(ti => ti.iccprod == p.Id
                                                                             && ti.icibid == i.ibid).FirstOrDefault();

                            if (detalleAnteriorProducto != null)
                            {
                                //INGRESOS
                                if (i.ibconcep == 1 || i.ibconcep == 3 ||
                                    i.ibconcep == 5 || i.ibconcep == 8 ||
                                    i.ibconcep == 9)
                                {
                                    entradasAnteriores += detalleAnteriorProducto.iccant.Value;
                                }
                                //SALIDAS
                                else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                                    i.ibconcep == 6 || i.ibconcep == 10)
                                {
                                    salidasAnteriores += detalleAnteriorProducto.iccant.Value;
                                }
                            }
                        }

                        //CALCULOS DEL PERIODO Y ALMACEN SELECCIONADOS
                        decimal entradasPeriodo = 0;
                        decimal salidasPeriodo = 0;
                        var detallePeriodo = listati002.Where(h => h.ibfdoc >= inicio &&
                                                                   h.ibfdoc <= fin &&
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
                                    i.ibconcep == 9)
                                {
                                    entradasPeriodo += detalleProducto.iccant.Value;
                                }
                                //SALIDAS
                                else if (i.ibconcep == 2 || i.ibconcep == 4 ||
                                    i.ibconcep == 6 || i.ibconcep == 10)
                                {
                                    salidasPeriodo += detalleProducto.iccant.Value;
                                }
                            }
                        }

                        detalle.Descripcion = p.Descrip;
                        detalle.Entradas = entradasPeriodo;
                        detalle.Id = p.Id;
                        detalle.Saldo = entradasPeriodo - salidasPeriodo;
                        detalle.SaldoAnterior = entradasAnteriores - salidasAnteriores;
                        detalle.Salidas = salidasPeriodo;
                        detalle.Unidad = p.UniVen.ToString();

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
