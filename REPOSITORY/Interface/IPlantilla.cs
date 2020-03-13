using ENTITY.Plantilla;
using System.Collections.Generic;
using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface IPlantilla
    {
        bool Guardar(VPlantilla vPlantilla, ref int id);
        List<VPlantilla> Listar(ENConceptoPlantilla concepto);
    }
}
