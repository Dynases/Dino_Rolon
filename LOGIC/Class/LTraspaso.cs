using ENTITY.inv.Traspaso.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTraspaso
    {
        protected ITraspaso iTraspaso;
        protected ITI002 iTI002;

        public LTraspaso()
        {
            iTI002 = new RTI002();
            iTraspaso = new RTraspaso(iTI002);
        }

        #region Transacciones

        public bool Guardar(VTraspaso vTraspaso, ref int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iTraspaso.Guardar(vTraspaso, ref id);
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

        public List<VTraspaso> Listar()
        {
            try
            {
                return this.iTraspaso.ListarTraspasos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId)
        {
            try
            {
                return this.iTraspaso.ListarInventarioXAlmacenId(AlmacenId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
