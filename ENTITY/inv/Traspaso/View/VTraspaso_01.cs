using System;

namespace ENTITY.inv.Traspaso.View
{
    public class VTraspaso_01
    {
        public int Id { get; set; }

        public int TraspasoId { get; set; }

        public int ProductoId { get; set; }

        public decimal Cantidad { get; set; }

        public string Lote { get; set; }

        public DateTime Fecha { get; set; }

    }
}
