using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Usuario.View
{
    public class VUsuario_01
    {
        public int IdUsuario_01 { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlmacen { get; set; }
        public string DescripAlm { get; set; }
        public int IdSuc { get; set; }
        public string DescripSuc { get; set; }       
        public bool Acceso { get; set; }
        public int Estado { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }


    }
}
