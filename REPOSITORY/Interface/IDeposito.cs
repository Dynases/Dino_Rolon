using ENTITY.inv.Deposito;
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
    }
}
