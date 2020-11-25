using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Cliente.View
{
  public  class VClienteCombo
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Nit { get; set; }
        public int FacturaEmpresa { get; set; }
        public string  EmpresaProveedora { get; set; }
        public int IdCategoriaPrecio { get; set; }
        public int tipoCliente { get; set; }

    }
}
