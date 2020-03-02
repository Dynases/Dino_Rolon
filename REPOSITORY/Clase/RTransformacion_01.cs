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

namespace REPOSITORY.Clase
{
    public class RTransformacion_01 : BaseConexion, ITransformacion_01
    {
        #region TRANSACCIONES
        public bool Nuevo(List<VTransformacion_01> Lista, int idTransformacion)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var vTransformacion_01 in Lista)
                    {
                        var transformacion_01 = new Transformacion_01();
                        transformacion_01.IdTransformacion = idTransformacion;
                        transformacion_01.IdProducto = vTransformacion_01.IdProducto;
                        transformacion_01.IdProducto_Mat = vTransformacion_01.IdProducto_Mat_Prima;
                        transformacion_01.Estado = (int)ENEstado.GUARDADO;
                        transformacion_01.TotalProd = vTransformacion_01.TotalProd;
                        transformacion_01.Cantidad = vTransformacion_01.Cantidad;
                        transformacion_01.Total = vTransformacion_01.Total;
                        db.Transformacion_01.Add(transformacion_01);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(List<VTransformacion_01> Lista, int idTransformacion)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var vTransformacion_01 in Lista)
                    {
                        if (vTransformacion_01.Estado == (int)ENEstado.MODIFICAR)
                        {
                            var transformacion_01 = db.Transformacion_01
                                                   .Where(t => t.Id.Equals(vTransformacion_01.Id))
                                                   .FirstOrDefault();
                            transformacion_01.IdTransformacion = idTransformacion;
                            transformacion_01.IdProducto = vTransformacion_01.IdProducto;
                            transformacion_01.IdProducto_Mat = vTransformacion_01.IdProducto_Mat_Prima;
                            transformacion_01.Estado = (int)ENEstado.GUARDADO;
                            transformacion_01.TotalProd = vTransformacion_01.TotalProd;
                            transformacion_01.Cantidad = vTransformacion_01.Cantidad;
                            transformacion_01.Total = vTransformacion_01.Total;
                            db.Transformacion_01.Add(transformacion_01);
                            db.SaveChanges();
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
        public VTransformacion_01 TraerTransaformacion_01(int idIdProducto, int idProducto_Mat)
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
