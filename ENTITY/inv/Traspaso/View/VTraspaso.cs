using System;

namespace ENTITY.inv.Traspaso.View
{
    public class VTraspaso
    {
        public int Id { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public string AlamacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public string AlamacenDestino { get; set; }
        public int Estado { get; set; }
        public string UsuarioEnvio { get; set; }
        public string UsuarioRecepcion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Observaciones { get; set; }
        public int EstadoEnvio { get; set; }
        public decimal TotalUnidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
    }
}
