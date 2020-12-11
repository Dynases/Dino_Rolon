using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Ajuste.Report
{
   public class VAjusteTicket
    {
        public int AjusteId { get; set; }
        public System.DateTime FechaReg { get; set; }
        public string alamcen { get; set; }
        public string concepto { get; set; }
        public string Cliente { get; set; }
        public string almDestino { get; set; }
        public string RecibidoPor { get; set; }
        public string TransportadoPor { get; set; }
        public int detalleId { get; set; }
        public string Producto { get; set; }
        public decimal Diferencia { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }
}
