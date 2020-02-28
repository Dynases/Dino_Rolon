using ENTITY.com.CompraIngreso_01;
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
  public  class LCompraIngreso_01
    {
        protected ICompraIngreso_01 iCompraIngreso_01;
        public LCompraIngreso_01()
        {
            iCompraIngreso_01 = new RCompraIngreso_01();
        }
        #region Transacciones
        public bool Guardar(List<VCompraIngreso_01> lista, int Id,string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCompraIngreso_01.Guardar(lista, Id, usuario);
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

        public List<VCompraIngreso_01> ListarXId(int id)
        {
            try
            {
                return iCompraIngreso_01.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso_01> ListarXId2(int IdGrupo2)
        {
            try
            {
                return iCompraIngreso_01.ListarXId2(IdGrupo2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
