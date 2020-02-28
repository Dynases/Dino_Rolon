using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion_01.View
{
   public class VSeleccion_01
    {

        public int Id { get; set; }
        public int IdSeleccion { get; set; }
        public int IdProducto { get; set; }
        public int Estado { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
