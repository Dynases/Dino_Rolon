using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.Report
{
   public class VSeleccionReport
    {
        public int Id { get; set; }
        public string SucursalIngreso { get; set; }
        public string SucursalSalida { get; set; }
        public string Observ { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
    }
}
