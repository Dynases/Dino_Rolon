using System;

namespace ENTITY.ven.view
{
    public class VVenta_01
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }

        public int IdProducto { get; set; } 

        public int Estado { get; set; }

        public string CodigoProducto { get; set; }

        public string CodigoBarra { get; set; }

        public string Producto { get; set; } 

        public string Unidad { get; set; } 
        public int Cantidad { get; set; } 

        public decimal Contenido { get; set; }
        public decimal TotalContenido { get; set; }

        public decimal PrecioVenta { get; set; } 

        public decimal SubTotal 
        {
            get
            { return this.PrecioVenta * Convert.ToDecimal(this.Cantidad); }
        }
        public decimal PrecioCosto { get; set; }
        public decimal SubTotalCosto
        {
            get
            { return this.PrecioCosto * Convert.ToDecimal(this.Cantidad); }
        }
        public decimal PrecioMinVenta{ get; set; }
        public decimal PrecioMaxVenta { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Stock { get; set; }
        public string Delete { get; set; }



    }
}
