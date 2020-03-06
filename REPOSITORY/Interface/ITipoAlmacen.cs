using ENTITY.inv.TipoAlmacen.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ITipoAlmacen
    {
        bool Guardar(VTipoAlmacen vtipoAlmacen);
        List<VTipoAlmacenListar> Listar();
        List<VTipoAlmacenCombo> ListarCombo();
    }
}
