using ENTITY.DiSoft.Libreria;
using ENTITY.Libreria.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface.DiSoft
{
   public interface ILibreriaD
    {
        bool Guardar(VLibreriaLista vLibreria);
    }
}
