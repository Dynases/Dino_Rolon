using ENTITY.adm.ValidacioinPrograma;
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
        public bool Eliminar(int Id, ref List<string> mensaje)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    FValidacionPrograma validacionPrograma = new FValidacionPrograma();
                    validacionPrograma.tablaOrigen = "INV.TipoAlmacen";
                    if (new LValidacionPrograma().ValidadrEliminacion(Id, validacionPrograma, ref mensaje, false))
                    {
                        iTipoAlmacen.Eliminar(Id);
                    }
                    if (mensaje.Count > 0)
                    {
                        return false;
                    }
                    scope.Complete();
                    return true;
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
