using DATA.EntityDataModel.DiAvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Proveedor.View
{
    public class VProveedor_01Lista
    {
        public int Id { get; set; }
        public int IdLinea { get; set; }
        public string Linea { get; set; }
        public System.DateTime FechaNac { get; set; }
        public string EdadSeman { get; set; }
        public decimal Cantidad { get; set; }       
        public int IdTipoAloja { get; set; }
        public string TipoAlojamiento { get; set; }        
    }
}
