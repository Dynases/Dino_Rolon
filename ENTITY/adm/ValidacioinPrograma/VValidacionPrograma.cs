using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.adm.ValidacioinPrograma
{
  public  class VValidacionPrograma
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string TablaOrigen { get; set; }
        public string CampoOrigen { get; set; }
        public string TableDestino { get; set; }
        public string CampoDestino { get; set; }
        public string Programa { get; set; }
    }
}
