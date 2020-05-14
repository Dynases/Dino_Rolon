using ENTITY.ven.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IVenta_01
    {
        bool Nuevo(List<VVenta_01> lventa_01, int ventaId );
        bool Modificar(VVenta_01 vventa_01, int ventaId);
        bool Eliminar(int IdVenta, int IdDetalle, ref List<string> lMensaje);
        List<VVenta_01> ListarDetalle(int VentaId);
    }
}
