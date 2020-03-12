using ENTITY.reg.PrecioCategoria.View;
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
   public class LPrecioCategoria
    {
        protected IPrecioCategoria iPrecioCat;
        public LPrecioCategoria()
        {
            iPrecioCat = new RPrecioCategoria();
        }
        #region Transacciones
        public bool Guardar(VPrecioCategoria vPrecioCat, ref int Id)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = Id;
                    result = iPrecioCat.Guardar(vPrecioCat, ref Id);
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
        public List<VPrecioCategoria> Listar()
        {
            try
            {
                return iPrecioCat.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
