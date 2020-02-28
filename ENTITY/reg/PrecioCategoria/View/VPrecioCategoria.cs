using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.reg.PrecioCategoria.View
{
     public  class VPrecioCategoria
    {
        public int Id { get; set; }
        public string Cod { get; set; }
        public string Descrip { get; set; }
        public int Tipo { get; set; }
        public string NombreTipo { get; set; }
        public decimal Margen { get; set; }
    }
}
