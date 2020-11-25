using ENTITY.Cliente.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface ICliente
    {
        List<VCliente> Listar();
        List<VCliente> ListarCliente(int id);
        List<VClienteLista> ListarClientes();
        bool Guardar(VCliente Cliente, ref int idCliente);
        bool Modificar(VCliente Cliente, int idCliente);
        bool Eliminar(int IdCliente);
        DataTable ListarEncabezado();
        bool ExisteEnVenta(int idCliente);
        List<VClienteCombo> TraerClienteCombo();
        bool EsClienteCredito(int clienteId);
        decimal TraerLimiteCredito(int clienteId);

    }
}
