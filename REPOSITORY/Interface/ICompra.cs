using ENTITY.com.Compra.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ICompra
    {
        List<VCompraLista> Lista();
        bool Guardar(VCompra vCompraIngreso, ref int id);
        bool ModificarEstado(int IdCompra, int estado, ref List<string> lMensaje);
    }
}
