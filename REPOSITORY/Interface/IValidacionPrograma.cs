using ENTITY.adm.ValidacioinPrograma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface IValidacionPrograma
    {
        List<VValidacionPrograma> TrerValidacionProgramas(FValidacionPrograma validacionPrograma);
        bool ExisteEnProgramaDestino(FValidacionPrograma validacionPrograma);
        List<VValidacionPrograma> TrerValidacionProgramasLibreria(FValidacionPrograma validacionPrograma);
    }
}
