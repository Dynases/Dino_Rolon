using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Rol.View;

namespace REPOSITORY.Interface
{
    public interface IRol_01
    {     

        List<VRol_01> Lista(int IdRol);
        bool Nuevo(List<VRol_01> Lista, int IdRol, string usuario);
        bool Modificar(VRol_01 Lista, string usuario);
        bool EliminarDetalle(int IdRol, List<VRol_01> detalle);
        bool Eliminar(int IdDeltalle);

    }
}
