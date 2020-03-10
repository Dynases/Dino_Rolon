using ENTITY.Plantilla;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IPlantilla
    {
        bool Guardar(VPlantilla VPlantilla);
        List<VPlantilla> Listar();
    }
}
