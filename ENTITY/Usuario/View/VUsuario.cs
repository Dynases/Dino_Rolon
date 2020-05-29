using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Usuario.View
{
    public class VUsuario
    {
        public int IdUsuario { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public int Estado { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario1 { get; set; }
    }
}
