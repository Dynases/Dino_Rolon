using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Transformacion.Report
{
    public class VTransformacionReport
    {
        public int Id { get; set; }
        public string AlmacenIngreso { get; set; }
        public string AlmacenSalida { get; set; }
        public string Observ { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdProducto { get; set; }
        public int IdProducto_Mat { get; set; }
        public string Descrip { get; set; }
        public decimal TotalProd { get; set; }
        public decimal Total { get; set; }


    }
}
