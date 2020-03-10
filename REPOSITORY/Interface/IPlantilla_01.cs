using ENTITY.Plantilla;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IPlantilla_01
    {
        bool Guardar(List<VPlantilla01> lista, int PlantillaId);
        List<VPlantilla01> ListarDetallePlantilla(int PlantillaId);
    }
}
