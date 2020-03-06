using ENTITY.inv.Almacen.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IAlmacen
    {
        bool Guardar(VAlmacen vAlmacen);
        List<VAlmacenCombo> Listar();
        List<VAlmacenLista> ListarAlmacenes();
    }
}
