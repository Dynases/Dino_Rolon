using ENTITY.com.Seleccion_01.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ISeleccion_01
    {
        bool Guardar(List<VSeleccion_01> Lista, int Id_Seleccion);
        List<VSeleccion_01_Lista> Listar();
        List<VSeleccion_01_Lista> ListarXId_Vacio(int id);
    }
}
