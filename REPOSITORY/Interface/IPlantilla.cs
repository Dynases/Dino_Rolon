using ENTITY.Plantilla;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface IPlantilla
    {
        bool Guardar(VPlantilla vPlantilla, ref int id);
        List<VPlantilla> Listar();
    }
}
