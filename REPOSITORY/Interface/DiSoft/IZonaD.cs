using ENTITY.Cliente.View;
using ENTITY.DiSoft.Zona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface.DiSoft
{
   public interface IZonaD
    {
        List<VZona> Listar();
        List<VClienteLista> ListarClienteAdicionarZona(List<VClienteLista> cliente);
    }
}
