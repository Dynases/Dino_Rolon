using DATA.EntityDataModel.DiAvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Proveedor.View
{
   public class VProveedor_01
    {
        public int Id { get; set; }
        public int IdProveed { get; set; }
        public int Estado { get; set; }
        public System.DateTime FechaNac { get; set; }
        public string EdadSeman { get; set; }
        public decimal Cantidad { get; set; }
        public string Linea { get; set; }
        public int TipoAloja { get; set; }
        public string Usuario { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public virtual Proveed Proveed { get; set; }
    }
}
