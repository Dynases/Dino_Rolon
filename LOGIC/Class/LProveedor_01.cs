using ENTITY.Proveedor.View;
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
    public class LProveedor_01
    {
        protected IProveedor_01 iProveedor_01;
        public LProveedor_01()
        {
            iProveedor_01 = new RProveedor_01();
        }
        #region Transaciones
        public bool Guardar(List<VProveedor_01Lista> listVProveedor, int idProveedor, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iProveedor_01.Guardar(listVProveedor, idProveedor, usuario);
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
        #region Consultas       
        public List<VProveedor_01Lista> ListarXId(int id)
        {
            try
            {
                return iProveedor_01.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
