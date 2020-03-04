using ENTITY.inv.Traspaso.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ITraspaso_01
    {
        bool Guardar(List<VTraspaso_01> lista, int TraspasoId);
        List<VTraspaso_01> ListarDetalleTraspaso(int TraspasoId);
    }
}
