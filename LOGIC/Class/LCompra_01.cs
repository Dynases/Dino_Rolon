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
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LCompra_01()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iCompra_01 = new RCompra_01(iTi001);
        }
        #region Transacciones
        public bool Nuevo(List<VCompra_01> lista, int IdCompra, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCompra_01.Nuevo(lista, IdCompra, usuario);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VCompra_01 lista, int Id, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCompra_01.Modificar(lista, Id, usuario);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int idCompra, int idDetalle, ref List<string> lMensaje)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iCompra_01.Eliminar(idCompra, idDetalle, ref lMensaje);
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
