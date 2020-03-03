using ENTITY.inv.Transformacion_01.View;
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
   public  class LTransformacion_01
    {

        protected ITransformacion_01 ITransformacion_01;
        public LTransformacion_01()
        {
            ITransformacion_01 = new RTransformacion_01();
        }
        #region TRANSACCIONES
        public bool Nuevo(List<VTransformacion_01> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = ITransformacion_01.Nuevo(lista, Id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(List<VTransformacion_01> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = ITransformacion_01.Modificar(lista, Id);
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
        #region CONSULTAR
        public List<VTransformacion_01> Listar(int idTransformacion)
        {
            try
            {
                return ITransformacion_01.Listar(idTransformacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public VTransformacion_01 TraerFilaProducto(int idIdProducto, int idProducto_Mat)
        {
            try
            {
                return ITransformacion_01.TraerFilaProducto(idIdProducto, idProducto_Mat);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
