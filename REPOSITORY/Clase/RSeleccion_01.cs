using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Seleccion_01.View;
using DATA.EntityDataModel.DiAvi;

namespace REPOSITORY.Clase
{
    public class RSeleccion_01 : BaseConexion, ISeleccion_01
    {
        #region TRANSACCIONES
        public bool Guardar(List<VSeleccion_01> Lista, int Id_Seleccion)
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
                        seleccion_01.Estado = 1; //Estatico                       
                        seleccion_01.Cantidad = vSeleccion_01.Cantidad;
                        seleccion_01.Precio = vSeleccion_01.Precio;
                        seleccion_01.Total = vSeleccion_01.Total;
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
                                          Cantidad = a.Cantidad,
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
        public List<VSeleccion_01_Lista> ListarXId_Vacio(int id)
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
                                          Producto = c.Descrip,
                                          Cantidad = 0,
                                          Precio = a.PrecioCost,
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
