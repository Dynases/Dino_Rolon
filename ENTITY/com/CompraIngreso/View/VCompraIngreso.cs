using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ENTITY.com.CompraIngreso.View
{
   public class VCompraIngreso
    {
        public int Id { get; set; }
        public string NotaProveedor { get; set; }
        public string Proveedor { get; set; }    
        public System.DateTime FechaEnt { get; set; }
        public System.DateTime FechaRec { get; set; }
        public string Entregado { get; set; }
        public string PlacaDescripcion { get; set; }
        public string TipoCategoria { get; set; }
        public string TipoCompra { get; set; }
        public string Devolucion { get; set; }
        public decimal TotalMaple { get; set; }
        public decimal TotalUnidades { get; set; }
       
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
