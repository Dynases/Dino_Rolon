using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LVenta_01
    {
        protected IVenta_01 iVenta_01;
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LVenta_01()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iVenta_01 = new RVenta_01(iTi001);
        }
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/



        /********** VARIOS REGISTROS ***********/
        public List<VVenta_01> ListarDetalle(int VentaId)
        {
            try
            {
                return this.iVenta_01.ListarDetalle(VentaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transacciones

        public bool Nuevo(List<VVenta_01> lista, int VentaId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = this.iVenta_01.Nuevo(lista, VentaId);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VVenta_01 lista, int VentaId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = this.iVenta_01.Modificar(lista, VentaId);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdVenta, int IdDetalle, ref List<string> lMensaje)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = this.iVenta_01.Eliminar(IdVenta, IdDetalle,ref lMensaje);
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

        #region Verificaciones

        #endregion
    }
}
