using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LVenta_01
    {
        protected IVenta_01 iVenta_01;
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LVenta_01()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iVenta_01 = new RVenta_01(iTi001);
        }

        #region Transacciones

        public bool Guardar(List<VVenta_01> lista, int VentaId, int AlmacenId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = this.iVenta_01.Guardar(lista, VentaId, AlmacenId);
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

        public List<VVenta_01> ListarDetalle(int VentaId)
        {
            try
            {
                return this.iVenta_01.ListarDetalle(VentaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
