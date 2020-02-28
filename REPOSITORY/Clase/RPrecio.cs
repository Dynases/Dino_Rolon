using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.reg.Precio.View;

namespace REPOSITORY.Clase
{
    public class RPrecio : BaseConexion, IPrecio
    {
        public List<VPrecioLista> ListarProductoPrecio(int idAlmacen)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Precio
                                      join pro in db.Producto on
                                        new { idpro = a.IdProduc, idAlm = a.IdAlmac }
                                         equals new { idpro= pro.Id, idAlm = idAlmacen }
                                      join cat in db.PrecioCat on
                                           new { idCat= a.IdPrecioCat }
                                         equals new { idCat = cat.Id }
                                      select new VPrecioLista
                                      {
                                          Id = a.Id,
                                          IdProducto = a.IdProduc,
                                          IdPrecioCat = a.IdPrecioCat,
                                          COd = cat.Cod,
                                          Precio = a.Prrecio,
                                          Estado = 1
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
