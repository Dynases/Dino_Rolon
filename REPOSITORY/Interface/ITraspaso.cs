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
        bool Guardar(VTraspaso vTraspaso);
        List<VTListaProducto> ListarInventarioXAlmacenId(int Id);
        List<VTraspaso> ListarTraspasos();
    }
}
