using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Libreria.View
{
    public class VLibreriaLista
    {
        public int IdGrupo { get; set; }
        public int IdOrden { get; set; }
        public int IdLibrer { get; set; }
        public string Descrip { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
