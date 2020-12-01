using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ITraspaso_01
    {
        bool ConfirmarRecepcionDetalle(List<Traspaso_01> detalle, int idTI2);
        bool Nuevo(VTraspaso_01 vTraspaso_01, int idTraspaso);
        bool Modificar(VTraspaso_01 vTraspaso_01);
        bool Eliminar( int IdDetalle);
        VTraspaso_01 ObtenerDetalleXId(int idDetalle);
        List<VTraspaso_01> ListaDetalle(int idTraspaso);
        List<VTraspaso_01> TraerTraspasos_01Vacio(int idTraspaso);
    }
}
