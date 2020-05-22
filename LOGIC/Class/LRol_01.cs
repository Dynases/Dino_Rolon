using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPOSITORY.Interface;
using ENTITY.Rol.View;
using REPOSITORY.Clase;
using System.Transactions;



namespace LOGIC.Class
{
    public class LRol_01
    {
        protected IRol_01 iRol_01;
        public LRol_01()
        {
           iRol_01 = new RRol_01();
        }
        #region Transacciones
        public bool Nuevo(List<VRol_01> lista, int IdRol, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iRol_01.Nuevo(lista, IdRol, usuario);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VRol_01 lista, int IdRol, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iRol_01.Modificar(lista, usuario);
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
                    var result = iRol_01.Eliminar(idDetalle);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarTodoDetalle(int idRol, List<VRol_01> detalle)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iRol_01.EliminarDetalle(idRol, detalle);
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
        public List<VRol_01> Listar(int IdRol)
        {
            try
            {
                return iRol_01.Lista(IdRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
