using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.ven.view
{
    public class VVenta_01
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Estado { get; set; }
        public string Unidad { get; set; }
        public int IdVenta { get; set; }
    }
}
