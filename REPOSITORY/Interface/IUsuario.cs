using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Usuario.View;

namespace REPOSITORY.Interface
{
    public interface IUsuario
    {
        bool Guardar(VUsuario Usuario, ref int idUsuario);
        bool Eliminar(int idUsuario);
        List<VUsuario> ListaUsuarios();
    }
}
