using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Proveedor.View
{
   public class VProveedor
    {
        public int Id { get; set; }
        public string IdSpyre { get; set; }
        public int Estado { get; set; }
        public string Descripcion { get; set; }
        public int Ciudad { get; set; }
        public int Tipo { get; set; }
        public int TipoProveeedor { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contacto2 { get; set; }
        public string Telefono2 { get; set; }
        public string Email2 { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longittud { get; set; }
        public string Imagen { get; set; }

        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
