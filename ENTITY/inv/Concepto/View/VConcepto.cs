using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Concepto.View
{
    public class VConcepto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoMovimiento { get; set; }
        public bool TipoMovimientoValor { get; set; }
        public bool AjusteCliente { get; set; }
        public int TipoConcepto { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
