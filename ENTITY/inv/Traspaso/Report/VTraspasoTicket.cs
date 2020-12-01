using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Traspaso.Report
{
   public class VTraspasoTicket
    {
        public int traspasoId { get; set; }
        public string Estado { get; set; }
        public System.DateTime FechaEnvio { get; set; }
        public System.DateTime FechaRecepcion { get; set; }
        public string almOrigen { get; set; }
        public string EntregadoPor { get; set; }
        public string almDestino { get; set; }
        public string RecibidoPor { get; set; }
        public string TransportadoPor { get; set; }
        public int detalleId { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
