using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.DiSoft.Pedido.View
{
    public  class VPedidoProductoD
    {
        public int PedidoId { get; set; }
        public string ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public int Familia { get; set; }
        public int  Campo1 { get; set; }
        public decimal Campo2 { get; set; }
    }
}

