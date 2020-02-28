using ENTITY.Cliente.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
    public class LCliente
    {
        protected ICliente iCliente;
        public LCliente()
        {
            iCliente = new RCliente();
        }
        #region Consultas
        public List<VCliente> Listar()
        {
            try
            {
                return iCliente.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCliente> ListarCliente(int id)
        {
            try
            {
                return iCliente.ListarCliente(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VClienteLista> ListarClientes()
        {
            try
            {
                return iCliente.ListarClientes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Transacciones
        public bool Guardar(VCliente vcliente, ref int idCliente)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCliente.Guardar(vcliente, ref idCliente);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCliente vcliente, int idCliente)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCliente.Modificar(vcliente, idCliente);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdCliente)
        {
            try
            {
                using (var scope = new TransactionScope())                {
                   
                    var result = iCliente.Eliminar(IdCliente);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion



    }
}
