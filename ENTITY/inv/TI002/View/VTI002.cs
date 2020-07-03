using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.TI002.View
{
   public class VTI002
    {
        public int IdManual { get; set; }
        public DateTime fechaDocumento { get; set; }
        public int IdConecpto { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int idAlmacenDestino { get; set; }
        public int IdDestino { get; set; }
        public int IdDetalle { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int Id { get; set; }

    }
}
