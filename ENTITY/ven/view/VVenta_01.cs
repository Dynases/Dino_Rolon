using System;

namespace ENTITY.ven.view
{
    public class VVenta_01
    {
        public int Id { get; set; } //0

        public int IdProducto { get; set; } //1

        public string DescripcionProducto { get; set; } //2

        public int Cantidad { get; set; } //3

        public decimal Precio { get; set; } //4

        public string Unidad { get; set; } //5

        public int Estado { get; set; } //6

        public decimal SubTotal //7
        {
            get
            { return this.Precio * Convert.ToDecimal(this.Cantidad); }
        }

        public string CodBar { get; set; } //8

        public int IdVenta { get; set; } //9

        public string Delete { get; set; } //10
    }
}
