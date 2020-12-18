using ENTITY.DiSoft.Pedido.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface.DiSoft
{
    public interface IPedidoD
    {
        void Guardar(VPedidoD pedido, ref int id, string usuario);
        void Eliminar(int PedidoId);
        VPedidoD ObtenerPorId(int id);
        void GuardarDetalle(List<VPedidoProductoD> detalle, int id, int tipoPedido);
        void GuardarPedidoDirecto(int id, int RepartidorId);
        void GuardarExtencionPedido(int id, int VendedorId);
        void ModificarEstadoPedido(int id, int EstadoPedido);
    }
}
