using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.ven.view
{
    public class VVenta
    {
        public int Id { get; set; }

        public int IdAlmacen { get; set; }

        public string DescripcionAlmacen { get; set; }

        public int IdCliente { get; set; }

        public string DescripcionCliente { get; set; }       

        public DateTime FechaVenta { get; set; }

        public int Estado { get; set; }

        public int Tipo { get; set; }

        public string Observaciones { get; set; }

        public int EncPrVenta { get; set; }

        public int EncVenta { get; set; }

        public int EncTransporte { get; set; }

        public string EncEntrega { get; set; }

        public int EncRecepcion { get; set; }
        public string NitCliente { get; set; }

        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int IdCategoriaCliente { get; set; }
    }
}
