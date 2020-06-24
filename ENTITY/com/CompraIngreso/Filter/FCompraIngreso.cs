using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.CompraIngreso.Filter
{
    public class FCompraIngreso
    {
        public int Id { get; set; }
        public int? IdProveedor { get; set; }
        public DateTime? fechaDesde { get; set; }
        public DateTime? fechaHasta { get; set; }
        public int estadoCompra { get; set; }
        public int TipoCategoria { get; set; }
        public int Detalle { get; set; }
    }
}
