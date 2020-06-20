using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
     public interface ICompraIngreso_03
    {
        bool Guardar(VCompraIngreso_03 Lista, int Id);
        bool Modificar(VCompraIngreso_03 Lista, int Id);
         List<VCompraIngreso_03> TraerDevoluciones(int id);
         List<VCompraIngreso_03> TraerDevolucionesTipoProducto(int IdGrupo2, int idAlmacen);
    }
}
