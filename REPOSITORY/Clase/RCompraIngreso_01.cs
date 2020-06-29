using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.CompraIngreso_01;
using DATA.EntityDataModel.DiAvi;
using System.Data.Entity;
using UTILITY.Enum.EnEstado;

namespace REPOSITORY.Clase
{
    public class RCompraIngreso_01 : BaseConexion, ICompraIngreso_01
    {
        #region Transacciones
        public bool Nuevo(VCompraIngreso_01 vCompraIngreso_01, int IdCommpra, ref int IdDetalle,int totalMaple)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compra = db.CompraIng.Where(c => c.Id == IdCommpra).FirstOrDefault();
                    if (compra == null)
                    {
                        throw new Exception("No existe la compra con id:" + IdCommpra);
                    }
                    var compraIng_01 = new CompraIng_01();
                    compraIng_01.IdCompra = IdCommpra;
                    compraIng_01.IdProduc = vCompraIngreso_01.IdProduc;
                    compraIng_01.Estado = (int)ENEstado.GUARDADO; //Estatico                       
                    compraIng_01.Caja = vCompraIngreso_01.Caja;
                    compraIng_01.Cantidad = vCompraIngreso_01.Cantidad;
                    compraIng_01.Grupo = vCompraIngreso_01.Grupo;
                    compraIng_01.Maple = vCompraIngreso_01.Maple;
                    compraIng_01.Cantidad = vCompraIngreso_01.Cantidad;
                    compraIng_01.TotalCant = vCompraIngreso_01.TotalCant;
                    compraIng_01.PrecioCost = vCompraIngreso_01.PrecioCost;
                    compraIng_01.Total = vCompraIngreso_01.Total;
                    compraIng_01.TotalMaple = totalMaple;
                    db.CompraIng_01.Add(compraIng_01);
                    db.SaveChanges();
                    IdDetalle = compraIng_01.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCompraIngreso_01 vCompraIngreso_01, int totalMaple)
        {
            try
            {
                using (var db = GetEsquema())
                {                  
                    var compraIng_01 = db.CompraIng_01
                                  .Where(d => d.Id == vCompraIngreso_01.Id)
                                  .FirstOrDefault();
                    if (compraIng_01 == null)
                    {
                        throw new Exception("No existe el detalle de compra con id:" + vCompraIngreso_01.Id);
                    }
                    compraIng_01.IdProduc = vCompraIngreso_01.IdProduc;
                    compraIng_01.Estado = (int)ENEstado.GUARDADO; //Estatico                       
                    compraIng_01.Caja = vCompraIngreso_01.Caja;
                    compraIng_01.Grupo = vCompraIngreso_01.Grupo;
                    compraIng_01.Maple = vCompraIngreso_01.Maple;
                    compraIng_01.Cantidad = vCompraIngreso_01.Cantidad;
                    compraIng_01.TotalCant = vCompraIngreso_01.TotalCant;
                    compraIng_01.PrecioCost = vCompraIngreso_01.PrecioCost;
                    compraIng_01.Total = vCompraIngreso_01.Total;
                    compraIng_01.TotalMaple = totalMaple;
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
        public VCompraIngreso_01 TraerCompraIngreso_01(int id)
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
                                      where a.Id.Equals(id)
                                      select new VCompraIngreso_01
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
                                      }).FirstOrDefault();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /********** VARIOS REGISTROS ***********/
        public List<VCompraIngreso_01> ListarXId(int id)
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
                                      orderby c.Grupo2 ascending, c.UniVen descending
                                      select new VCompraIngreso_01
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
        public List<VCompraIngreso_01> ListarXId2(int IdGrupo2, int idAlmacen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var tipos = new int[] { IdGrupo2, 34 };
                    var listResult = (from c in db.Producto
                                      join b in db.Precio on c.Id equals b.IdProduc
                                      join s in db.Sucursal on b.IdSucursal equals s.Id
                                      join a in db.Almacen on s.Id equals a.IdSuc
                                      where b.IdPrecioCat.Equals(8) && tipos.Contains(c.Grupo2) && c.Tipo.Equals(2) && a.Id.Equals(idAlmacen)
                                      orderby c.Grupo2 ascending, c.UniVen descending
                                      select new VCompraIngreso_01
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

        /********** REPORTE ***********/
        
        #endregion



    }
}
