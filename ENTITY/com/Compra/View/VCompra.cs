using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Compra.View
{
    public class VCompra
    {
        public int Id { get; set; }
        public int IdSuc { get; set; }
        public int IdProvee { get; set; }
        public int Estado { get; set; }
        public System.DateTime FechaDoc { get; set; }
        public int TipoVenta { get; set; }
        public System.DateTime FechaVen { get; set; }
        public string Observ { get; set; }
        public decimal Descu { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
