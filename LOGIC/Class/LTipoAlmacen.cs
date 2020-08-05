using ENTITY.inv.TipoAlmacen.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTipoAlmacen
    {
        protected ITipoAlmacen iTipoAlmacen;

        public LTipoAlmacen()
        {
            iTipoAlmacen = new RTipoAlmacen();
        }

        #region Consulta

        public List<VTipoAlmacenListar> Listar()
        {
            try
            {
                return iTipoAlmacen.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VTipoAlmacenCombo> ListarCombo()
        {
            try
            {
                return iTipoAlmacen.ListarCombo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Transacciones

        public void Guardar(VTipoAlmacen vtipoAlmacen, ref int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    iTipoAlmacen.Guardar(vtipoAlmacen, ref Id);
                    scope.Complete();
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
