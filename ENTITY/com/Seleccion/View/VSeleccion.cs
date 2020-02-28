using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.View
{
   public class VSeleccion
    {
        public int Id { get; set; }
        public int IdSucur { get; set; }
        public int IdCompraIng { get; set; }
        public int Estado { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public decimal Merma { get; set; }
    }
}
