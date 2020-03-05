using ENTITY.inv.Almacen.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LAlmacen
    {
        protected IAlmacen iAlmacen;

        public LAlmacen()
        {
            iAlmacen = new RAlmacen();
        }

        #region Consulta

        public List<VAlmacenCombo> Listar()
        {
            try
            {
                return iAlmacen.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAlmacenLista> ListarAlmacenes()
        {
            try
            {
                return iAlmacen.ListarAlmacenes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transacciones

        public bool Guardar(VAlmacen vAlmacen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iAlmacen.Guardar(vAlmacen);
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
