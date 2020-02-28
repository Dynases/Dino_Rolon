using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.inv.Transformacion_01.View;

namespace REPOSITORY.Clase
{
    public class RTransformacion_01 : BaseConexion, ITransformacion_01
    {
        public List<VTransformacion_01_Lista> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Transformacion_01
                                      join b in db.Producto on a.IdProducto equals b.Id
                                      select new VTransformacion_01_Lista
                                      {
                                          Id = a.Id,
                                          IdTransformacion =a.IdTransformacion,
                                          IdProducto = a.IdProducto,
                                          Producto = b.Descrip,
                                          Estado = a.Estado,                                
                                          TotalProd =a.TotalProd,
                                          Producto2 =b.DescripProduc,
                                          Cantidad = a.Cantidad,
                                          Total =a.Total
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
