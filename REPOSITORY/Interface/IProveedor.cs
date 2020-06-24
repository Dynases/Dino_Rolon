using ENTITY.Proveedor.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface IProveedor
    {
        List<VProveedorLista> Listar();
        List<VProveedor> ListarXId(int id);        
        bool Guardar(VProveedor Cliente, ref int idCliente);
        bool Eliminar(int idProveedor);
        DataTable ListarEncabezado();
        bool ExisteEnCompraIng(int idProveedor);
        bool ExisteEnCompra(int idProveedor);
        List<VProveedorCombo> TraerProveedoresEdadSemana();
    }
}
