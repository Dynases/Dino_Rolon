using ENTITY.ven.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IVenta_01
    {
        bool Nuevo(VVenta_01 vventa_01, int ventaId , ref int idVentaDetalle);
        bool Modificar(VVenta_01 vventa_01, int ventaId);
        bool Eliminar(int IdVenta, int IdDetalle);
        VVenta_01 TraerVenta_01(int idVentaDetalle);
        List<VVenta_01> TraerVentas_01(int VentaId);
    }
}
