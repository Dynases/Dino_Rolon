namespace ENTITY.ven.view
{
    public class VVenta_01
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public string DescripcionProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string Unidad { get; set; }

        public int Estado { get; set; }

        public decimal SubTotal
        {
            get
            { return this.Precio * this.Cantidad; }
        }

        public string CodBar { get; set; }

        public int IdVenta { get; set; }
    }
}
