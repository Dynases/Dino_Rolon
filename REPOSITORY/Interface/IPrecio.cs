using ENTITY.reg.Precio.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IPrecio
    {
        List<VPrecioLista> ListarProductoPrecio(int idAlmacen);
        DataTable ListarProductoPrecio2(int idSucursal);
        bool Nuevo(VPrecioLista vPrecio, int idAlmacen, string usuario);
        bool Modificar(VPrecioLista vPrecio, string usuario);
    }
}
