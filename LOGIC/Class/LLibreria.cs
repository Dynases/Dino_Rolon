using ENTITY.Libreria.View;
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
   public class LLibreria
    {
        protected ILibreria iLibreria;
        public LLibreria()
        {
            iLibreria = new RLibreria();
        }
        #region Transacciones
        public bool Guardar(VLibreriaLista vlibreria)
        {
            try
            {

                using (var scope = new TransactionScope())
                {
                    var result = iLibreria.Guardar(vlibreria);
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
        public List<VLibreria> Listar(int idGrupo,int idOrden)
        {
            try
            {
                return iLibreria.Listar(idGrupo,idOrden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
