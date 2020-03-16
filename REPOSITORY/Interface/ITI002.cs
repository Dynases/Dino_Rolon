using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ITI002
    {
        bool Guardar(int idAlmacenSalida, string almacenSalida, int idAlmacenDestino, string almacenDestino, int idTraspaso, string usuario);
    }
}
