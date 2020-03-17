using ENTITY.com.CompraIngreso_02;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
   public class LCompraIngreso_02

    {
        protected ICompraIngreso_02 iCompraIngreso_02;
        public LCompraIngreso_02()
        {
            iCompraIngreso_02 = new RCompraIngreso_02();
        }
        #region Transacciones
        public bool Guardar(VCompraIngreso_02 lista)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCompraIngreso_02.Guardar(lista);
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
        #region Consulta
        public List<VCompraIngreso_02> Listar()
        {
            try
            {
                return iCompraIngreso_02.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarTabla()
        {
            try
            {
                return iCompraIngreso_02.ListarTabla();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
