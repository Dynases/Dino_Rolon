using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.View
{
    public class VSeleccionLista:VSeleccion
    {
        public string Granja { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Placa { get; set; }
        public string Proveedor { get; set; }
        public int Tipo { get; set; }
        public string Edad { get; set; }
    }
}
