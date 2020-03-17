using ENTITY.com.CompraIngreso_02;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ICompraIngreso_02
    {
        bool Guardar(VCompraIngreso_02 Lista);
        List<VCompraIngreso_02> Listar();
        DataTable ListarTabla();
    }
}
