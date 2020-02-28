using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Cliente.View
{
    public class VClienteLista
    {
        public int Id { get; set; }      
        public string Descripcion { get; set; }
        public string RazonSocial { get; set; }
        public int Ciudad { get; set; }
        public string NombreCiudad { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
    }
}
