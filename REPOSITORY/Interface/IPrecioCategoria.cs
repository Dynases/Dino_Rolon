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
        bool Guardar(VPrecioCategoria vPrecioCat, ref int id);
        List<VPrecioCategoria> Listar();
    }
}
