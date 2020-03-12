using ENTITY.ven.view;
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
    public class LVenta
    {
        protected IVenta iVenta;

        public LVenta()
        {
            iVenta = new RVenta();
        }

        #region Transacciones

        public bool Guardar(VVenta vVenta, ref int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iVenta.Guardar(vVenta, ref id);
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

        public List<VVenta> Listar()
        {
            try
            {
                return this.iVenta.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        

        #endregion
    }
}
