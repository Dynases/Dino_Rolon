using ENTITY.inv.TipoAlmacen.view;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ITipoAlmacen
    {    
        void Guardar(VTipoAlmacen vtipoAlmacen, ref int Id);
        List<VTipoAlmacenListar> Listar();
        List<VTipoAlmacenCombo> ListarCombo();
    }
}
