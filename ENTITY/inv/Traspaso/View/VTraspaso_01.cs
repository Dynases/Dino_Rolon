using System;

namespace ENTITY.inv.Traspaso.View
{
    public class VTraspaso_01
    {
        public int Id { get; set; }

        public int Estado { get; set; }

        public int TraspasoId { get; set; }

        public int ProductoId { get; set; }
        public string CodigoProducto { get; set; }

        public string Producto { get; set; }

        public decimal Cantidad { get; set; }
        public decimal Contenido { get; set; }
        public decimal TotalContenido { get; set; }

        public string Lote { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public string Unidad { get; set; }

        public string Marca { get; set; }
        public decimal Stock { get; set; }
        public  string Delete { get; set; }
    }
}
