using System;

namespace ENTITY.inv.Traspaso.View
{
    public class VTraspaso
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int Concepto { get; set; }

        public string Observaciones { get; set; }

        public int Estado { get; set; }

        public int Origen { get; set; }

        public int Destino { get; set; }

        public string Hora { get; set; }

        public string Usuario { get; set; }
    }
}
