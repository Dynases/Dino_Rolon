using ENTITY.com.Compra_01.View;
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
   public class LCompra_01
    {
        protected ICompra_01 iCompra_01;
        public LCompra_01()
        {
            iCompra_01 = new RCompra_01();
        }
        #region Transacciones
       
        #endregion
        #region Consulta
        public List<VCompra_01_Lista> Listar()
        {
            try
            {
                return iCompra_01.Lista();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
