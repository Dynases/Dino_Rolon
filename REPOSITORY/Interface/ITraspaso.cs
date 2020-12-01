using ENTITY.inv.Traspaso.Report;
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
        bool Guardar(VTraspaso vTraspaso, ref int id);
        List<VTListaProducto> ListarInventarioXAlmacenId(int Id);
        List<VTraspaso> TraerTraspasos(int usuarioId);
        void GuardarDetalleDisoft(int idTraspaso);
        bool EsTraspasoDirecto(int AlmacenId);
        List<VTraspasoTicket> ReporteTraspaso(int traspasoId);
        bool ModificarEstado(int traspasoId, int estado);
        VTraspaso ObtenerPorId(int traspasoId);
    }
}
