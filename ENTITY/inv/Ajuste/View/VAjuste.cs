using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Ajuste.View
{
  public  class VAjuste
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int IdConcepto { get; set; }
        public int IdAlmacen { get; set; }
        public string Obs { get; set; }

    }
}
