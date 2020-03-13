using ENTITY.inv.Traspaso.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTraspaso_01
    {
        protected ITraspaso_01 iTraspaso_01;
        protected ITI001 iTi001;

        public LTraspaso_01()
        {
            iTi001 = new RTI001();
            iTraspaso_01 = new RTraspaso_01(iTi001);
        }

        #region Transacciones

        public bool Guardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = this.iTraspaso_01.Guardar(lista, TraspasoId, almacenId);
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

        public List<VTraspaso_01> ListarDetalleTraspaso(int TraspasoId)
        {
            try
            {
                return this.iTraspaso_01.ListarDetalleTraspaso(TraspasoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}
