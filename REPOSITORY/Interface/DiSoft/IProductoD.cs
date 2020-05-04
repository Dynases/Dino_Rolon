using ENTITY.Producto.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface.DiSoft
{
    public interface IProductoD
    {
        bool Guardar(VProducto Producto, ref int id);        
        bool Eliminar(int idProducto);
    }
}
