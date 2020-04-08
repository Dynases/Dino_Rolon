using ENTITY.com.Compra_01.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ICompra_01
    {
        List<VCompra_01> Lista(int IdCompra);
        bool Nuevo(List<VCompra_01> Lista, int IdCompra, string usuario);
        bool Modificar(VCompra_01 Lista, int IdCompra, string usuario);
        bool Eliminar(int IdCompra, int IdDetalle, ref List<string> lMensaje);
        bool ExisteEnLoteEnUsoVenta_01(int IdProducto, string lote, DateTime? fechaVen);
    }
}
