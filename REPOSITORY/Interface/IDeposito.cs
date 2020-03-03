using ENTITY.inv.Deposito;
using ENTITY.inv.Sucursal.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IDeposito
    {
        List<VDepositoCombo> Listar();
        List<VDepositoLista> ListarDepositos();
        List<VSucursalLista> ListarSucursalXDepositoId(int Id);
    }
}
