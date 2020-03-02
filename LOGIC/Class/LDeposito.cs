using ENTITY.inv.Deposito;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;

namespace LOGIC.Class
{
    public class LDeposito
    {
        protected IDeposito iDeposito;

        public LDeposito()
        {
            iDeposito = new RDeposito();
        }

        #region Consulta

        public List<VDepositoCombo> Listar()
        {
            try
            {
                return iDeposito.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transaccion


        #endregion

    }
}
