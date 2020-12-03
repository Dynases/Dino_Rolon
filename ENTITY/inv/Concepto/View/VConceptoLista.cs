using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Concepto.View
{
  public  class VConceptoLista
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string TipoMovimiento { get; set; }      
        public string AjusteCliente { get; set; }    
        public string Estado { get; set; }

    }
}
