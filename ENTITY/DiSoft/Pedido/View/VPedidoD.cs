using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.DiSoft.Pedido.View
{
 public   class VPedidoD
    {
        public int Id { get; set; }
        public DateTime FechaReg { get; set; }
        public string HoraReg { get; set; }
        public int ClienteId { get; set; }
        public int ZonaId { get; set; }
        public int VendedorId { get; set; }
        public string Observacion { get; set; }
        public string Observacion2 { get; set; }
        public int EstadoPedido { get; set; }
        public int Estado { get; set; }
        public int Dato { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int TipoPedido { get; set; }
    }
}
