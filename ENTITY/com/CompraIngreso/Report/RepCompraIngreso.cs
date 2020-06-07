using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.CompraIngreso.Report
{
  public class RepCompraIngreso
    {
        public int Id { get; set; }
        public string NumNota { get; set; }
        public DateTime FechaRec { get; set; }
        public int IdAlmacen { get; set; }
        public int Almacen { get; set; }
        public decimal TotalCantidad { get; set; }
        public int TotalMaple { get; set; }
        public int Dias { get; set; }
       
    }
}
