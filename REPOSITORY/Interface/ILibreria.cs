using ENTITY.Libreria.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace REPOSITORY.Interface
{
    public interface ILibreria
    {
        List<VLibreria> Listar(int idGrupo, int idOrden);
        bool Guardar(VLibreriaLista vLibreria);
        bool Modificar(VLibreriaLista vLibreria);
        bool Eliminar(VLibreriaLista vLibreria);
        List<VLibreria> TraerProgramas();
        List<VLibreria> TraerCategorias(int idPrograma);
        List<VLibreriaLista> TraerLibreriasXCategoria(int idGrupo, int idOrden);
    }
}
