using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.TI001.VIew
{
    public class VTI001
    {
        public Nullable<int> IdAlmacen { get; set; }
        public string IdProducto { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Lote { get; set; }
        public Nullable<System.DateTime> FechaVen { get; set; }
        public int id { get; set; }
    }
}
