using ENTITY.Cliente.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface.DiSoft
{
   public interface IClienteD
    {
        bool Guardar(VCliente vcliente, ref int idCliente);    
        bool Eliminar(int IdCliente);
        Decimal? saldoPendienteCredito(int clienteId);
    }
}
