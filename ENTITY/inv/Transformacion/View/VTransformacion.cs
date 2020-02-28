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
        public int IdSucSalida { get; set; }
        public string Sucursal1 { get; set; }
        public int IdSucIngreso { get; set; }
        public string Sucursal2 { get; set; }
        public int Estado { get; set; }
        public string Observ { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }

    }
}
