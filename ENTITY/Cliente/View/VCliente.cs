using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Cliente.View
{
    public class VCliente
    {
        public int Id { get; set; }
        public string IdSpyre { get; set; }
        public int Estado { get; set; }
        public string Descripcion { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public int TipoCliente { get; set; }
        public string Direcccion { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public string Telfono1 { get; set; }
        public string Telfono2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int Ciudad { get; set; }
        public int Facturacion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longittud { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
