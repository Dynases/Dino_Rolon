using ENTITY.com.CompraIngreso_01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ICompraIngreso_01
    {
        List<VCompraIngreso_01> ListarXId(int id);

        List<VCompraIngreso_01> ListarXId2(int IdGrupo2);
        bool Guardar(List<VCompraIngreso_01> Lista, int Id,string usuario);
    }
}
