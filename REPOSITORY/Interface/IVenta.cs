using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IVenta
    {
        bool Guardar(ENTITY.ven.view.VVenta VVenta);
        List<ENTITY.ven.view.VVenta> Listar();
    }
}
