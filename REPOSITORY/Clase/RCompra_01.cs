using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Compra_01.View;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RCompra_01 : BaseConexion, ICompra_01
    {
        public List<VCompra_01_Lista> Lista()
        {
            try
            {
                
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Compra_01
                                      join b in db.Producto on a.IdProducto equals b.Id
                                      join c in db.Libreria on b.UniVen equals c.IdLibrer
                                      where c.IdGrupo.Equals((int)ENEstaticosGrupo.PRODUCTO) && c.IdOrden.Equals((int)ENEstaticosOrden.PRODUCTO_UN_VENTA)
                                      select new VCompra_01_Lista
                                      {
                                          Id = a.Id,
                                          IdCompra = a.IdCompra,
                                          IdProducto = a.IdProducto,
                                          Estado = a.Estado,
                                          Producto =b.Descrip,
                                          Cantidad = a.Canti,
                                          Unidad = c.Descrip,
                                          Costo = a.Costo,
                                          Total = a.Total,
                                          Lote = a.Lote,
                                          FechaVen = a.FechaVen,
                                          Utilidad = a.Utilidad,
                                          Porcent = a.Porcent                                          
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
