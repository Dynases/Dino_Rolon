using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Producto.View
{
   public class VProductoListaStock
    {
        public int InventarioId { get; set; }

        public int ProductoId { get; set; }

        public int AlmacenId { get; set; }

        public string Descripcion { get; set; }

        public int Existencia { get; set; }

        public string Division { get; set; }

        public string Marca { get; set; }

        public string Categoria { get; set; }

        public int UnidadVenta { get; set; }

        public string UnidadVentaDisplay { get; set; }
    }
}
