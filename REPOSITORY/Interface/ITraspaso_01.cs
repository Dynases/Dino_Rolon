using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ITraspaso_01
    {
        bool ConfirmarRecepcionDetalle(List<Traspaso_01> detalle, int idTI2);
        bool Guardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2);
        List<VTraspaso_01> ListarDetalleTraspaso(int TraspasoId);
    }
}
