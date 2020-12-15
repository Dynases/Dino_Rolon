using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.ven.Report
{
   public class VVentaTicket
    {
        public int ventaId { get; set; }
        public System.DateTime FechaVenta { get; set; }
        public string Cliente { get; set; }
        public string Nit { get; set; }
        public string FacturaExterna { get; set; }
        public int IdCompraIngreso { get; set; }
        public string alamcen { get; set; }
        public string EncEntrega { get; set; }
        public string encVenta { get; set; }
        public string encTransporte { get; set; }
        public int detalleId { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public decimal TotalUnidad { get; set; }
    }
}
