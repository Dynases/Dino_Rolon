using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Rol.View;
using System.Data;

namespace REPOSITORY.Interface
{
   public interface IRol
    {
        bool Guardar(VRol Rol, ref int idRol);
        bool Eliminar(int idRol);
        List<VRol> ListaRol();
        System.Data.DataTable AsignarPermisos(string idRol, string NombreProg);
        bool ExisteEnUsuario(int IdRol);
    }
}
