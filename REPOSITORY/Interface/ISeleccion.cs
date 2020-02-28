using ENTITY.com.Seleccion.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ISeleccion
    {
        bool Guardar(VSeleccion vSeleccion, ref int id);
        List<VSeleccionLista> Listar();
    }
}
