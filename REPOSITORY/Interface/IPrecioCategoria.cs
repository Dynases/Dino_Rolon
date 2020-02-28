using ENTITY.reg.PrecioCategoria.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface IPrecioCategoria
    {
        List<VPrecioCategoria> Listar();
    }
}
