using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Transformacion.View
{
   public class VTransformacion
    {
        public int Id { get; set; }
        public int IdAlmacenSalida { get; set; }
        public string Almacen1 { get; set; }
        public int IdAlmacenIngreso { get; set; }
        public string Almacen2 { get; set; }
        public int Estado { get; set; }
        public string Observ { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }

    }
}
