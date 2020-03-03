using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Deposito
{
    public class VDeposito
    {
        public int Id { get; set; }

        public int Estado { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

        public string Imagen { get; set; }

        public string Hora { get; set; }

        public string Usuario { get; set; }
    }
}
