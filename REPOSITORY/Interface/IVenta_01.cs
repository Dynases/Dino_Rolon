using ENTITY.ven.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IVenta_01
    {
        bool Guardar(List<VVenta_01> lista, int ventaId);
        List<VVenta_01> ListarDetalle(int VentaId);
    }
}
