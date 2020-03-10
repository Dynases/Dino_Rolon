using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Seleccion_01.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using System.Data.Entity;

namespace REPOSITORY.Clase
{
    public class RSeleccion_01 : BaseConexion, ISeleccion_01
    {
        #region TRANSACCIONES
        public bool Guardar(List<VSeleccion_01_Lista> Lista, int Id_Seleccion)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.Seleccion_01.Where(a => a.IdSeleccion == Id_Seleccion).ToList();
                    if (listResult.Count != 0)
                        db.Seleccion_01.RemoveRange(listResult);

                    foreach (var vSeleccion_01 in Lista)
                    {
                        var seleccion_01 = new Seleccion_01();
                        seleccion_01.IdSeleccion = Id_Seleccion;
                        seleccion_01.IdProducto = vSeleccion_01.IdProducto;
                        seleccion_01.Estado = (int)ENEstado.GUARDADO; //Estatico                       
                        seleccion_01.Cantidad = vSeleccion_01.Cantidad;
                        seleccion_01.Precio = vSeleccion_01.Precio;
                        seleccion_01.Total = vSeleccion_01.Total;
                        seleccion_01.Porcen = vSeleccion_01.Porcen;
                        db.Seleccion_01.Add(seleccion_01);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool GuardarModificar(List<VSeleccion_01_Lista> Lista, int Id )
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var vSeleccion_01 in Lista)
                    {
                        if (vSeleccion_01.Estado == (int)ENEstado.MODIFICAR)
                        {
                            var seleccion_01 = db.Seleccion_01
                                             .Where(d => d.Id.Equals(vSeleccion_01.Id))
                                             .FirstOrDefault();

                            seleccion_01.IdProducto = vSeleccion_01.IdProducto;
                            seleccion_01.Estado = (int)ENEstado.GUARDADO; //Estatico                     
                            seleccion_01.Cantidad = vSeleccion_01.Cantidad;
                            seleccion_01.Precio = vSeleccion_01.Precio;
                            seleccion_01.Total = vSeleccion_01.Total;
                            seleccion_01.Porcen = vSeleccion_01.Porcen;
                            db.Seleccion_01.Attach(seleccion_01);
                            db.Entry(seleccion_01).State = EntityState.Modified;
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

        public bool GuardarModificar_CompraIngreso(List<VSeleccion_01_Lista> Lista, int IdCompraIngreso)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compraIng = db.CompraIng.Where(c => c.Id.Equals(IdCompraIngreso)).FirstOrDefault();
                    compraIng.Estado = (int)ENEstado.COMPLETADO;
                    db.CompraIng.Attach(compraIng);
                    db.Entry(compraIng).State = EntityState.Modified;                 
                    foreach (var vCompraIng_01 in Lista)
                    {
                        var compraIng_01 = db.CompraIng_01
                                             .Where(d =>d.Id.Equals(vCompraIng_01.Id))
                                             .FirstOrDefault();
                        compraIng_01.Estado = (int)ENEstado.COMPLETADO; //Estatico                     
                        db.CompraIng_01.Attach(compraIng_01);
                        db.Entry(compraIng_01).State = EntityState.Modified;
                    }
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
        public List<VSeleccion_01_Lista> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Seleccion_01
                                      join b in db.Producto on
                                      new { idProducto = a.IdProducto }
                                        equals new { idProducto = b.Id }
                                      select new VSeleccion_01_Lista
                                      {
                                          Id = a.Id,
                                          IdSeleccion = a.IdSeleccion,
                                          Producto = b.Descrip,
                                          Estado = a.Estado,
                                          Cantidad = a.Cantidad,
                                          Porcen =a.Porcen,
                                          Precio = a.Precio,
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
        public List<VSeleccion_01_Lista> ListarXId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Seleccion_01
                                      join b in db.Producto on
                                      new { idProducto = a.IdProducto }
                                        equals new { idProducto = b.Id }
                                      select new VSeleccion_01_Lista
                                      {
                                          Id = a.Id,
                                          IdSeleccion = a.IdSeleccion,
                                          Producto = b.Descrip,
                                          Cantidad = a.Cantidad,
                                          Porcen = a.Porcen,
                                          Precio = a.Precio,
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
        public List<VSeleccion_01_Lista> ListarXId_CompraIng(int id,int  tipo)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.CompraIng_01
                                      join c in db.Producto on
                                       new
                                       {
                                           idProve = a.IdProduc
                                       }
                                       equals
                                       new
                                       {
                                           idProve = c.Id
                                       }
                                      where a.IdCompra.Equals(id)
                                      select new VSeleccion_01_Lista
                                      {
                                          Id = a.Id,
                                          IdSeleccion = 0,
                                          IdProducto = a.IdProduc,
                                          Estado = tipo == 1 ? (int)ENEstado.COMPLETADO : a.Estado,
                                          Producto = c.Descrip,
                                          Cantidad = tipo == 1 ? 0 : a.TotalCant,                                         
                                          Precio = a.PrecioCost,
                                          Total = tipo == 1 ? 0 : a.Total                                         
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> ListarXId_CompraIng_XSeleccion(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.CompraIng_01
                                      join c in db.Producto on
                                       new
                                       {
                                           idProve = a.IdProduc
                                       }
                                       equals
                                       new
                                       {
                                           idProve = c.Id
                                       }
                                      where a.IdCompra.Equals(id)
                                      select new VSeleccion_01_Lista
                                      {
                                          Id = a.Id,
                                          IdSeleccion = 0,
                                          IdProducto = a.IdProduc,
                                          Estado =  a.Estado,
                                          Producto = c.Descrip,
                                          Cantidad =  0,
                                          Precio = 0,
                                          Total = 0
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
