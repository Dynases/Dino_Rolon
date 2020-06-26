using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILITY.Enum.EnEstado
{
    public enum ENEstado
    {
        ELIMINAR=-1,
        NUEVO = 0,
        GUARDADO= 1,
        MODIFICAR = 2,
        COMPLETADO= 3,
        TODOS = 4,

        CARGARPRIMERFILA =5,
        NOCARGARPRIMERFILA = 6
    }
}
