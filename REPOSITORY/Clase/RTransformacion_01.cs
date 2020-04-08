using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.inv.Transformacion_01.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using System.Data.Entity;

namespace REPOSITORY.Clase
{
    public class RTransformacion_01 : BaseConexion, ITransformacion_01
    {
        private readonly ITI001 tI001;
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;
        public RTransformacion_01(ITI001 tI001, ITI002 tI002, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI002 = tI002;
            this.tI0021 = tI0021;            
        }
        #region TRANSACCIONES
        public bool Nuevo(List<VTransformacion_01> Lista, int idTransformacion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    //El movimiento de ingreso y salida esta en los TRIGGER de Transformacion_01
                    foreach (var vTransformacion_01 in Lista)
                    {                        
                        Transformacion_01 transformacion_01;
                        if (idAux > 0)
                        {
                            transformacion_01 = db.Transformacion_01.Where(a => a.Id == idAux).FirstOrDefault();
                            if (transformacion_01 == null)
                                throw new Exception("No existe la transformacion con id " + idAux);
                        }
                        else
                        {
                            transformacion_01 = new Transformacion_01();
                            db.Transformacion_01.Add(transformacion_01);
                        }
                        //var transformacion_01 = new Transformacion_01();
                        transformacion_01.IdTransformacion = idTransformacion;
                        transformacion_01.IdProducto = vTransformacion_01.IdProducto;
                        transformacion_01.IdProducto_Mat = vTransformacion_01.IdProducto_Mat_Prima;
                        transformacion_01.Estado = (int)ENEstado.GUARDADO;
                        transformacion_01.TotalProd = vTransformacion_01.TotalProd;
                        transformacion_01.Cantidad = vTransformacion_01.Cantidad;
                        transformacion_01.Total = vTransformacion_01.Total;
                        //db.Transformacion_01.Add(transformacion_01);
                        db.SaveChanges();
                        id = transformacion_01.Id;
                    }                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(List<VTransformacion_01> Lista, int idTransformacion, string usuario, ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    DateTime? fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";
                    int idDetalle = 0;
                    foreach (var i in Lista)
                    {
                        var transformacion_01 = db.Transformacion_01
                                                   .Where(t => t.Id.Equals(i.Id))
                                                   .FirstOrDefault();
                        if (transformacion_01 == null)
                        {
                            transformacion_01 = new Transformacion_01();
                        }
                        var transformacion = db.Transformacion.Where(t => t.Id == idTransformacion).FirstOrDefault();                     
                        var AlmacenSalida = db.Almacen.Where(a => a.Id.Equals(transformacion.IdAlmacenSalida)).Select(a => a.Descrip).FirstOrDefault();
                        var AlmacenIngreso = db.Almacen.Where(a => a.Id.Equals(transformacion.IdAlmacenIngreso)).Select(a => a.Descrip).FirstOrDefault();
                        if (i.Estado == (int)ENEstado.NUEVO)
                        {
                            #region Nuevo                          
                            List<VTransformacion_01> detalle = new List<VTransformacion_01>();
                            detalle.Add(i);
                            if (!Nuevo(detalle, transformacion.Id, ref idDetalle))
                            {
                                return false;
                            }                         
                            //if (i.Total > 0)
                            //{
                            //    if (this.tI001.ExisteProducto(i.IdProducto_Mat_Prima.ToString(), transformacion.IdAlmacenSalida, lote, fechaVen))
                            //    {
                            //        if (StockActual_MatPrima >= i.Total)
                            //        {
                            //            if (!this.tI001.ActualizarInventario(i.IdProducto_Mat_Prima.ToString(),
                            //                                                                                   transformacion.IdAlmacenSalida,
                            //                                                                                   EnAccionEnInventario.Descontar,
                            //                                                                                   Convert.ToDecimal(i.Total),
                            //                                                                                   lote,
                            //                                                                                   fechaVen))
                            //            {
                            //                return false;
                            //            }

                                       
                            //            int idMovimiento = 0;
                            //            //ACTUALIZACION EN TI002: CABECERA
                            //            if (!this.tI002.Guardar(transformacion.IdAlmacenSalida, AlmacenSalida,
                            //                                    0, "",
                            //                                    idDetalle, usuario,
                            //                                    (transformacion.Id + " - " + " I -Transformacion Salida numiprod: " + transformacion_01.IdProducto_Mat + "| " + AlmacenSalida),
                            //                                    (int)ENConcepto.TRANSFORMACION_SALIDA,
                            //                                    ref idMovimiento))
                            //            {
                            //                return false;
                            //            }
                            //            //ACTUALIZACION  EN TI0021: DETALLE                                        
                            //            if (!this.tI0021.Guardar(idMovimiento, i.IdProducto_Mat_Prima, i.Total,lote,fechaVen))
                            //            {
                            //                return false;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            var producto = db.Producto.Where(p => p.Id == i.IdProducto_Mat_Prima).Select(p => p.Descrip).FirstOrDefault();
                            //            lMensaje.Add("No existe stock actual suficiente para el producto: " + producto);
                            //        }
                            //    }

                            //}
                            //if (i.TotalProd > 0)
                            //{
                            //    if (this.tI001.ExisteProducto(i.IdProducto.ToString(), transformacion.IdAlmacenIngreso, lote, fechaVen))
                            //    {
                            //        if (!this.tI001.ActualizarInventario(i.IdProducto.ToString(),
                            //                                       transformacion.IdAlmacenIngreso,
                            //                                       EnAccionEnInventario.Incrementar,
                            //                                       Convert.ToDecimal(i.TotalProd),
                            //                                       lote,
                            //                                       fechaVen))
                            //        {
                            //            return false;
                            //        }
                            //        int idMovimiento = 0;
                            //        //ACTUALIZACION EN TI002: CABECERA
                            //        if (!this.tI002.Guardar(transformacion.IdAlmacenIngreso, AlmacenSalida,
                            //                                0, "",
                            //                                idDetalle, usuario,
                            //                                (transformacion.Id + " - " + " I -Transformacion Salida numiprod: " + transformacion_01.IdProducto + "| " + AlmacenIngreso),
                            //                                (int)ENConcepto.TRANSFORMACION_INGRESO,
                            //                                ref idMovimiento))
                            //        {
                            //            return false;
                            //        }
                            //        //ACTUALIZACION  EN TI0021: DETALLE                                        
                            //        if (!this.tI0021.Guardar(idMovimiento, i.IdProducto, i.TotalProd,lote,fechaVen))
                            //        {
                            //            return false;
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                        else
                        {                         
                            #region Modificar
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                if (i.Total > 0)
                                {
                                    //Modificar Cantidad de Producto materia prima
                                    if (transformacion_01.Total != i.Total)
                                    {
                                        //Se obtiene el saldo a modificar
                                        //var saldoModificar = transformacion_01.Total > i.Total ? transformacion_01.Total - i.Total:  i.Total - transformacion_01.Total;
                                        //if (StockActual_MatPrima >= saldoModificar)
                                        //{
                                        if (this.tI001.ExisteProducto(i.IdProducto_Mat_Prima.ToString(), transformacion.IdAlmacenSalida, lote, fechaVen))
                                        {
                                            if (!this.tI001.ActualizarInventarioModificados(i.IdProducto_Mat_Prima.ToString(),
                                                                           transformacion.IdAlmacenSalida,                                                                           
                                                                           Convert.ToDecimal(i.Total),
                                                                           transformacion_01.Total,
                                                                           lote,
                                                                           fechaVen))
                                            {
                                                return false;
                                            }
                                            if (this.tI002.ExisteEnMovimiento(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                            {
                                                //MODIFICA EL MOVIMIENTO
                                                if (!this.tI002.Modificar(transformacion.IdAlmacenSalida,
                                                                    0,
                                                                    i.Id,
                                                                    usuario,
                                                                    (transformacion.Id + " - " + " I -Transformacion Salida numiprod: " + transformacion_01.IdProducto_Mat + "| " + AlmacenSalida),
                                                                    (int)ENConcepto.TRANSFORMACION_SALIDA))
                                                {
                                                    return false;
                                                }
                                                //MODIFICA EL DETALLE DE MOVIMIENTO
                                                if (!this.tI0021.Modificar(i.Total,
                                                                      i.Id,
                                                                      (int)ENConcepto.TRANSFORMACION_SALIDA,
                                                                       lote,
                                                                        fechaVen))
                                                {
                                                    return false;
                                                }
                                            }                                            
                                        }
                                        //}
                                        //else
                                        //{
                                        //    var producto = db.Producto.Where(p => p.Id == i.IdProducto_Mat_Prima).Select(p => p.Descrip).FirstOrDefault();
                                        //    lMensaje.Add("No existe stock actual suficiente para el producto: " + producto);
                                        //}

                                    }
                                }
                                if (i.TotalProd > 0)
                                {
                                    //Modificar Cantidad de Producto comercial
                                    if (transformacion_01.TotalProd != i.TotalProd)
                                    {
                                        if (this.tI001.ExisteProducto(i.IdProducto.ToString(), transformacion.IdAlmacenIngreso, lote, fechaVen))
                                        {
                                            if (!this.tI001.ActualizarInventarioModificados(i.IdProducto.ToString(),
                                                                           transformacion.IdAlmacenIngreso,
                                                                           transformacion_01.TotalProd,
                                                                           Convert.ToDecimal(i.TotalProd),
                                                                           lote,
                                                                           fechaVen))
                                            {
                                                return false;
                                            }
                                            if (this.tI002.ExisteEnMovimiento(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                            {
                                                //MODIFICA EL MOVIMIENTO
                                                if (!this.tI002.Modificar(transformacion.IdAlmacenIngreso,
                                                                0,
                                                                i.Id,
                                                                usuario,
                                                                (transformacion.Id + " - " + " I -Transformacion ingreso numiprod: " + transformacion_01.IdProducto + "| " + AlmacenIngreso),
                                                                (int)ENConcepto.TRANSFORMACION_INGRESO))
                                                {
                                                    return false;
                                                }
                                                //MODIFICA EL DETALLE DE MOVIMIENTO
                                                if (!this.tI0021.Modificar(i.TotalProd,
                                                                      i.Id,
                                                                      (int)ENConcepto.TRANSFORMACION_INGRESO,
                                                                      lote,
                                                                      fechaVen))
                                                {
                                                    return false;
                                                }
                                            }                                               
                                        }
                                    }
                                }
                                List<VTransformacion_01> detalle = new List<VTransformacion_01>();
                                detalle.Add(i);
                                idDetalle = i.Id;
                                if (!Nuevo(detalle, transformacion.Id, ref idDetalle))
                                {
                                    return false;
                                }                               
                            }
                            #endregion
                            #region Eliminar
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {
                                if (i.TotalProd > 0)
                                {
                                    if (this.tI001.ExisteProducto(i.IdProducto.ToString(), transformacion.IdAlmacenIngreso, lote, fechaVen))
                                    {
                                        if (!this.tI001.ActualizarInventario(i.IdProducto.ToString(),
                                                                       transformacion.IdAlmacenIngreso,
                                                                       EnAccionEnInventario.Descontar,
                                                                       Convert.ToDecimal(i.TotalProd),
                                                                       lote,
                                                                       fechaVen))
                                        {
                                            return false;
                                        }
                                        if (this.tI002.ExisteEnMovimiento(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                        {
                                            //ELIMINA EL DETALLE DE MOVIMIENTO
                                            if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                            {
                                                return false;
                                            }
                                            //ELIMINA EL MOVIMIENTO
                                            if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                            {
                                                return false;
                                            }
                                        }
                                       
                                    }
                                }
                                if (i.Total > 0)
                                {

                                    if (this.tI001.ExisteProducto(i.IdProducto_Mat_Prima.ToString(), transformacion.IdAlmacenSalida, lote, fechaVen))
                                    {
                                        if (!this.tI001.ActualizarInventario(i.IdProducto_Mat_Prima.ToString(),
                                                                       transformacion.IdAlmacenSalida,
                                                                       EnAccionEnInventario.Incrementar,
                                                                       Convert.ToDecimal(i.Total),
                                                                       lote,
                                                                       fechaVen))
                                        {
                                            return false;
                                        }
                                        if (this.tI002.ExisteEnMovimiento(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                        {
                                            //ELIMINA EL DETALLE DE MOVIMIENTO
                                            if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                            {
                                                return false;
                                            }
                                            //ELIMINA EL MOVIMIENTO
                                            if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                            {
                                                return false;
                                            }
                                        }                                     
                                    }
                                    db.Transformacion_01.Remove(transformacion_01);
                                    db.SaveChanges();
                                }
                            }
                            #endregion                            
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
        public List<VTransformacion_01> Listar(int idTransformacion)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Transformacion_01
                                      join b in db.Producto on a.IdProducto equals b.Id
                                      join c in db.Producto on a.IdProducto_Mat equals c.Id
                                      where a.IdTransformacion.Equals(idTransformacion)
                                      select new VTransformacion_01
                                      {
                                          Id = a.Id,
                                          IdTransformacion = a.IdTransformacion,
                                          Estado = a.Estado,
                                          IdProducto = a.IdProducto,
                                          Producto = b.Descrip,
                                          IdProducto_Mat_Prima = a.IdProducto_Mat,
                                          Producto_Mat_Prima = c.Descrip,
                                          TotalProd = a.TotalProd,
                                          Cantidad = a.Cantidad,
                                          Total = a.Total
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public VTransformacion_01 TraerFilaProducto(int idIdProducto, int idProducto_Mat)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = new VTransformacion_01()
                    {
                        Id = 0,
                        IdTransformacion = 0,
                        Estado = (int)ENEstado.NUEVO,
                        IdProducto = idIdProducto,
                        Producto = db.Producto.Where(p => p.Id.Equals(idIdProducto)).Select(a => a.Descrip).FirstOrDefault().ToString(),
                        IdProducto_Mat_Prima = idProducto_Mat,
                        Producto_Mat_Prima = db.Producto.Where(p => p.Id.Equals(idProducto_Mat)).Select(a => a.Descrip).FirstOrDefault().ToString(),
                        TotalProd = 0,
                        Cantidad =Convert.ToDecimal(db.Producto.Where(p => p.Id.Equals(idIdProducto)).Select(a => a.Cantidad).FirstOrDefault()),
                        Total = 0
                    };
                    return producto;
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
