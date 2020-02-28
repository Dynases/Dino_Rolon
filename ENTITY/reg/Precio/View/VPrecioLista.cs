using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.reg.Precio.View
{
   public class VPrecioLista
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdPrecioCat { get; set; }
        public string COd { get; set; }
        public decimal Precio { get; set; }
        public int Estado { get; set; }
        
    }
}
