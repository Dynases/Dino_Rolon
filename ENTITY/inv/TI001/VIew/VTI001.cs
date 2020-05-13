using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.TI001.VIew
{
    public class VTI001
    {
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public Nullable<int> Unidad { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVen { get; set; }
        public int id { get; set; }
    }
}
