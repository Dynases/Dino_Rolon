using ENTITY.reg.Precio.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IPrecio
    {
        List<VPrecioLista> ListarProductoPrecio(int idAlmacen);
    }
}
