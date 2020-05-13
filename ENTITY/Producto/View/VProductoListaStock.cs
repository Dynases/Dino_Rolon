using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Producto.View
{
    public class VProductoListaStock
    {
        public int IdProducto { get; set; }

        public int IdAlmacen{ get; set ;}

        public int IdCategoriaPrecio { get; set; }

        public string Producto { get; set; }

        public string Almacen { get; set; }

        public decimal Precio  { get; set; }

        public string UnidadVenta { get; set; }

        public string CategoriaPrecio { get; set; }
        public decimal? Stock { get; set; }
    }
}
