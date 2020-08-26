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

        public string CodigoProducto { get; set; }

        public string Producto { get; set; }
        public string Division { get; set; }

        public string TipoProducto { get; set; }
        public string CategoriaProducto { get; set; }
        public string UnidadVenta { get; set; }

        public decimal PrecioVenta  { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioMinVenta { get; set; }
        public decimal PrecioMaxVenta { get; set; }

        public decimal? Stock { get; set; }

        public string CategoriaPrecio { get; set; }
        public int EsLote { get; set; }
        public string Contenido { get; set; }


    }
}
