using ENTITY.inv.Transformacion.Report;
using ENTITY.inv.Transformacion.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ITransformacion
    {
        bool Guardar(VTransformacion vTransformacion, ref int id);
        List<VTransformacion> Listar(int usuarioId);
        List<VTransformacionReport> ListarSalida(int Id);
        List<VTransformacionReport> ListarIngreso(int Id);
        bool ModificarEstado(int IdTransformacion, int estado, ref List<string> lMensaje);
    }
}
