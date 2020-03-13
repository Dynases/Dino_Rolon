using ENTITY.inv.Traspaso.View;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ITraspaso_01
    {
        bool Guardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId);
        List<VTraspaso_01> ListarDetalleTraspaso(int TraspasoId);
    }
}
