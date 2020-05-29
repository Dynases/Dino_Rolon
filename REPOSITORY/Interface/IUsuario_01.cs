using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Usuario.View;

namespace REPOSITORY.Interface
{
    public interface IUsuario_01
    {
       
        bool Nuevo(List<VUsuario_01> Lista, int IdUsuario, string usuario);
        bool Modificar(VUsuario_01 Lista, string usuario);
        bool Eliminar(int IdDeltalle);
        bool EliminarDetalle(int IdUsuario, List<VUsuario_01> detalle);        
        List<VUsuario_01> Lista(int IdRol);
    }
}
