using ENTITY.adm.ValidacioinPrograma;
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

        public List<VAlmacenCombo> Listar(int usuarioId)
        {
            try
            {
                return iAlmacen.Listar(usuarioId);
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

        public bool Guardar(VAlmacen vAlmacen, ref int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iAlmacen.Guardar(vAlmacen,ref Id);
                    scope.Complete();
                    return result;
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
                    validacionPrograma.tablaOrigen = "INV.Almacen";
                    if (new LValidacionPrograma().ValidadrEliminacion(Id, validacionPrograma, ref mensaje, false))
                    {
                        iAlmacen.Eliminar(Id);
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
