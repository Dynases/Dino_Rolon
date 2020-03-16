using ENTITY.inv.Traspaso.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ITraspaso
    {
        bool ConfirmarRecepcion(int TraspasoId, string usuarioRecepcion);
        bool Guardar(VTraspaso vTraspaso, ref int idTI2, ref int id);
        List<VTListaProducto> ListarInventarioXAlmacenId(int Id);
        List<VTraspaso> ListarTraspasos();
    }
}
