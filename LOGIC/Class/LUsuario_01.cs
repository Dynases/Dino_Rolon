using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPOSITORY.Interface;
using ENTITY.Usuario.View;
using REPOSITORY.Clase;
using System.Transactions;

namespace LOGIC.Class
{
    public class LUsuario_01
    {
        protected IUsuario_01 iUsuario_01;
        public LUsuario_01()
        {
            iUsuario_01 = new RUsuario_01();
        }
        #region Transacciones
        public bool Nuevo(List<VUsuario_01> lista, int IdUsuario, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iUsuario_01.Nuevo(lista, IdUsuario, usuario);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VUsuario_01 lista, int IdUsuario, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iUsuario_01.Modificar(lista, usuario);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Eliminar(int idDetalle)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iUsuario_01.Eliminar(idDetalle);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarTodoDetalle(int idUsuario, List<VUsuario_01> detalle)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iUsuario_01.EliminarDetalle(idUsuario, detalle);
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
        public List<VUsuario_01> Listar(int IdUsuario)
        {
            try
            {
                return iUsuario_01.Lista(IdUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
