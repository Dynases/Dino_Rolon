using ENTITY.inv.Deposito;
using ENTITY.inv.Sucursal.View;
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

        public List<VDepositoLista> ListarDepositos()
        {
            try
            {
                return iDeposito.ListarDepositos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VSucursalLista> ListarSucursalXDepositoId(int Id)
        {
            try
            {
                return iDeposito.ListarSucursalXDepositoId(Id);
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
