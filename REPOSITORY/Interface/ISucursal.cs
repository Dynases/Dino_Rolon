using ENTITY.inv.Almacen.View;
using ENTITY.inv.Sucursal.View;
using System.Collections.Generic;

namespace REPOSITORY.Interface
{
    public interface ISucursal
    {
        bool Guardar(VSucursal vDeposito);
        List<VSucursalCombo> Listar();
        List<VAlmacenLista> ListarAlmacenXSucursalId(int Id);
        List<VSucursalLista> ListarSucursales();
    }
}
