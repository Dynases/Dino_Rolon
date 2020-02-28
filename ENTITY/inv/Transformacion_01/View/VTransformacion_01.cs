using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Transformacion_01.View
{
   public class VTransformacion_01
    {
        public int Id { get; set; }
        public int IdTransformacion { get; set; }
        public int IdProducto { get; set; }
        public int Estado { get; set; }
        public decimal TotalProd { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
