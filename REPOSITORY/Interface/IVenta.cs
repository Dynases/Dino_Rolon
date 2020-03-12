using ENTITY.ven.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IVenta
    {
        bool Guardar(VVenta VVenta, ref int id);
        List<VVenta> Listar();
    }
}
