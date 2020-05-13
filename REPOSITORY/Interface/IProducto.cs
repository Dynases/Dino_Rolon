using ENTITY.Producto.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public  interface IProducto
    {
        List<VProductoLista> Listar();
        List<VProducto> ListarXId( int id);
        bool Guardar (VProducto Producto, ref int id);
        bool Modificar(VProducto Producto, int idProducto);
        bool Eliminar(int idProducto);
        bool ExisteEnCompra(int IdProducto);
        System.Data.DataTable ListarEncabezado(int IdSucursal, int IdAlmacen, int IdCategoriaPrecio);

        bool ExisteEnCompraNormal(int IdProducto);
        bool ExisteEnMovimiento(int IdProducto);
        bool ExisteEnVenta(int IdProducto);
        bool ExisteEnSeleccion(int IdProducto);
        bool ExisteEnTransformacion(int IdProducto);
        List<VProductoListaStock> ListarProductoStock(int IdSucursal, int IdAlmacen,int IdCategoriaPrecio);
    }
}
