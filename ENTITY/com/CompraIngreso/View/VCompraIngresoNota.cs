using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.CompraIngreso.View
{
  public  class VCompraIngresoNota: VCompraIngresoLista
    {
        public string IdSkype { get; set; }
        public string Proveedor { get; set; }

        public string MarcaTipo { get; set; }

        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public decimal  TotalCant { get; set; }
        public decimal PrecioCost { get; set; }
        public decimal Total { get; set; }
    }
}
