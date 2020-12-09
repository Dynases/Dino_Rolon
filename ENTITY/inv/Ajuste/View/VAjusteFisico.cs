using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Ajuste.View
{
    public class VAjusteFisico
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int IdAlmacen { get; set; }
        public int IdConcepto { get; set; }
        public int IdCliente { get; set; }
        public System.DateTime FechaReg { get; set; }
        public string Observacion { get; set; }
        public int TransportadorPor { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
