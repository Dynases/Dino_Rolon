using ENTITY.ven.view;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;
using UTILITY.Enum.EnEstado;

namespace LOGIC.Class
{
    public class LVenta
    {
        protected IVenta iVenta;

        public LVenta()
        {
            iVenta = new RVenta();
        }

        #region Transacciones

        public bool Guardar(VVenta vVenta, List<VVenta_01> detalle, ref int IdVenta, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = IdVenta;
                    result = iVenta.Guardar(vVenta, ref IdVenta);
                    if (aux == 0)//Nuevo
                    {
                        var resultDetalle = new LVenta_01().Nuevo(detalle, IdVenta,vVenta.IdAlmacen, vVenta.Usuario);
                    }
                    else//Modificar
                    {
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VVenta_01> detalleNuevo = new List<VVenta_01>();
                                detalleNuevo.Add(i);                            
                                if (!new LVenta_01().Nuevo(detalleNuevo, IdVenta,vVenta.IdAlmacen, vVenta.Usuario))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {                            
                                if (!new LVenta_01().Modificar(i, IdVenta,vVenta.IdAlmacen,vVenta.Usuario))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {                                
                                if (!new LVenta_01().Eliminar(IdVenta, i.Id,vVenta.IdAlmacen, ref lMensaje))
                                {
                                    return false;
                                }
                            }
                        }
                    }
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

        #region Consulta

        public List<VVenta> Listar()
        {
            try
            {
                return this.iVenta.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
