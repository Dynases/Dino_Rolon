using ENTITY.inv.Transformacion_01.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ITransformacion_01
    {
        bool Nuevo(List<VTransformacion_01> Lista, int idTransformacion);
        bool Modificar(List<VTransformacion_01> Lista, int idTransformacion);
        List<VTransformacion_01> Listar(int idTransformacion);

        VTransformacion_01 TraerTransaformacion_01(int idIdProducto, int idProducto_Mat);
    }
}
