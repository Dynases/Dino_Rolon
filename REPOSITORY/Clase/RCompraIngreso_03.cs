using DATA.EntityDataModel.DiAvi;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RCompraIngreso_03:BaseConexion, ICompraIngreso_03
    {
        #region Transacciones
        public bool Guardar(VCompraIngreso_03 vCompraIngreso_03, int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compraIng_03 = new CompraIng_03();
                    compraIng_03.IdCompra = Id;
                    compraIng_03.IdProduc = vCompraIngreso_03.IdProduc;
                    compraIng_03.Estado = (int)ENEstado.GUARDADO; //Estatico                       
                    compraIng_03.Caja = vCompraIngreso_03.Caja;
                    compraIng_03.Cantidad = vCompraIngreso_03.Cantidad;
                    compraIng_03.Grupo = vCompraIngreso_03.Grupo;
                    compraIng_03.Maple = vCompraIngreso_03.Maple;
                    compraIng_03.Cantidad = vCompraIngreso_03.Cantidad;
                    compraIng_03.TotalCant = vCompraIngreso_03.TotalCant;
                    compraIng_03.PrecioCost = vCompraIngreso_03.PrecioCost;
                    compraIng_03.Total = vCompraIngreso_03.Total;
                    db.CompraIng_03.Add(compraIng_03);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCompraIngreso_03 vCompraIngreso_03, int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {

                    var compraIng_03 = db.CompraIng_03
                                     .Where(d => d.Id.Equals(vCompraIngreso_03.Id))
                                     .FirstOrDefault();
                    compraIng_03.IdProduc = vCompraIngreso_03.IdProduc;
                    compraIng_03.Estado = (int)ENEstado.GUARDADO; //Estatico                       
                    compraIng_03.Caja = vCompraIngreso_03.Caja;
                    compraIng_03.Grupo = vCompraIngreso_03.Grupo;
                    compraIng_03.Maple = vCompraIngreso_03.Maple;
                    compraIng_03.Cantidad = vCompraIngreso_03.Cantidad;
                    compraIng_03.TotalCant = vCompraIngreso_03.TotalCant;
                    compraIng_03.PrecioCost = vCompraIngreso_03.PrecioCost;
                    compraIng_03.Total = vCompraIngreso_03.Total;
                    db.CompraIng_03.Attach(compraIng_03);
                    db.Entry(compraIng_03).State = EntityState.Modified;
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

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
      

       
        /********** VARIOS REGISTROS ***********/
        public List<VCompraIngreso_03> TraerDevoluciones(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.CompraIng_03
                                      from b in db.CompraIng
                                      join c in db.Producto on a.IdProduc equals c.Id 
                                      where a.IdCompra.Equals(id) && a.IdCompra == b.Id && b.Estado != (int)ENEstado.ELIMINAR
                                      select new VCompraIngreso_03
                                      {
                                          Id = a.Id,
                                          IdProduc = a.IdProduc,
                                          Producto = c.Descrip,
                                          Caja = a.Caja,
                                          Grupo = a.Grupo,
                                          Maple = a.Maple,
                                          Cantidad = a.Cantidad,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                          Estado = a.Estado
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso_03> TraerDevolucionesTipoProducto(int IdGrupo2, int idAlmacen)
        {

            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from c in db.Producto
                                      join b in db.Precio on c.Id equals b.IdProduc
                                      join s in db.Sucursal on b.IdSucursal equals s.Id
                                      join a in db.Almacen on s.Id equals a.IdSuc
                                      where b.IdPrecioCat.Equals(8) && c.Grupo2.Equals(IdGrupo2) && c.Tipo.Equals(2) && a.Id.Equals(idAlmacen)
                                      select new VCompraIngreso_03
                                      {
                                          Id = 0,
                                          IdProduc = c.Id,
                                          Producto = c.Descrip,
                                          Caja = 0,
                                          Grupo = 0,
                                          Maple = 0,
                                          Cantidad = 0,
                                          TotalCant = 0,
                                          PrecioCost = b.Precio1,
                                          Total = 0,
                                          Estado = 0
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
