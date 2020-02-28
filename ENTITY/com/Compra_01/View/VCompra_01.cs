using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Compra_01.View
{
   public class VCompra_01
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Estado { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }

        public decimal Total { get; set; }
        public string Lote { get; set; }
        public System.DateTime FechaVen { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Porcent { get; set; }


    }
}
