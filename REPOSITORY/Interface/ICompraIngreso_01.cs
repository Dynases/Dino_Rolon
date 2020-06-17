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
        bool Nuevo(VCompraIngreso_01 vCompraIngreso_01, int IdCommpra, ref int IdDetalle);
        bool Modificar(VCompraIngreso_01 vCompraIngreso_01);
        VCompraIngreso_01 TraerCompraIngreso_01(int id);
        List<VCompraIngreso_01> ListarXId(int id);

        List<VCompraIngreso_01> ListarXId2(int IdGrupo2, int idAlmacen);
 
    }
}
