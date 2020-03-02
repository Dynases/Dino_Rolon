using ENTITY.inv.Sucursal.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LSucursal
    {
        protected ISucursal iSucursal;

        public LSucursal()
        {
            iSucursal = new RSucursal();
        }

        #region Consulta

        public List<VSucursalCombo> Listar()
        {
            try
            {
                return iSucursal.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VSucursalLista> ListarSucursales()
        {
            try
            {
                return iSucursal.ListarSucursales();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transacciones

        public bool Guardar(VSucursal vSucursal)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSucursal.Guardar(vSucursal);
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


    }
}
