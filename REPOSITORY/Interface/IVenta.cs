using ENTITY.ven.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IVenta
    {
        bool Guardar(VVenta VVenta, ref int id);
        bool ModificarEstado(int IdVenta, int estado);
        VVenta TraerVenta(int idVenta);
        List<VVenta> TraerVentas(int usuarioId);
    }
}
