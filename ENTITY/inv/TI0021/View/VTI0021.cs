using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.TI0021.View
{
   public class VTI0021
    {
        public int IdManual { get; set; }
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadNueva { get; set; }
        public string Lote { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public string LoteNuevo { get; set; }
        public System.DateTime FechaVencimientoNuevo { get; set; }
        public int id { get; set; }
    }
}
