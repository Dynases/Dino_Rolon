using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.Report
{
   public  class RSeleccionNota
    {
        public int Id    { get; set; }
        public string NumNota { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaRec { get; set; }

        public string Proveedor { get; set; }
        public string IdSpyre { get; set; }
        public string MarcaTipo { get; set; }
        public string Entregado { get; set; }
        public string DescripcionRecibido { get; set; }
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public string Porcen { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public decimal Merma { get; set; }
        public string MermaPorcentaje { get; set; }

    }
}
