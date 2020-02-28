using ENTITY.Proveedor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IProveedor_01
    {
        List<VProveedor_01Lista> ListarXId(int id);
        
        bool Guardar(List<VProveedor_01Lista> listProveedor_01, int idProveedor, string usuario);
    }
}
