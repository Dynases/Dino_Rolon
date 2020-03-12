using DATA.EntityDataModel.DiAvi;
using ENTITY.ven.view;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase
{
    public class RVenta_01 : BaseConexion, IVenta_01
    {
        #region Trasancciones

        public bool Guardar(List<VVenta_01> lista, int ventaId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var i in lista)
                    {
                        var detalle = new Venta_01
                        {
                            Cantidad = i.Cantidad,
                            Estado = i.Estado,
                            IdProducto = i.IdProducto,
                            IdVenta = ventaId,
                            Precio = i.Precio,
                            Unidad = i.Unidad
                        };

                        db.Venta_01.Add(detalle);
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

        #region Consultas

        public List<VVenta_01> ListarDetalle(int VentaId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Venta_01
                       .Where(v => v.Venta.Id == VentaId)
                       .Select(v => new VVenta_01
                       {
                           Cantidad = v.Cantidad,
                           CodBar = v.Producto.CodBar,
                           DescripcionProducto = v.Producto.Descrip,
                           Estado = v.Estado,
                           Id = v.Id,
                           IdProducto = v.IdProducto,
                           IdVenta = v.IdVenta.Value,
                           Precio = v.Precio,
                           Unidad = v.Unidad,
                           Delete = ""
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
