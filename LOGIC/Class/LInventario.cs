using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum;

namespace LOGIC.Class
{
   public class LInventario
    {       
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LInventario()
        {
            iTi002 = new RTI002();
            iTi0021 = new RTI0021();
            iTi001 = new RTI001(iTi002, iTi0021); 
        }
        #region Consultas

        /******** VALOR/REGISTRO ÚNICO *********/
        public decimal TraerStockActual(int IdProducto, int idAlmacen, string lote, DateTime fecha)
        {
            try
            {
                return this.iTi001.TraerStockActual(IdProducto,idAlmacen,lote,fecha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/


        #endregion
        #region Transacciones
        public bool NuevoMovimientoInventario(int idVentaDetalle,
                                              int idProducto,
                                              int idAlmacen,
                                              string lote, DateTime fechaVencimiento,
                                              decimal  cantidad,
                                              int concepto,
                                              string Observacion,
                                              EnAccionEnInventario accion,
                                              string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (!this.iTi001.NuevoMovimientoInventario(idVentaDetalle,
                                                               idProducto,
                                                               idAlmacen,
                                                               lote,fechaVencimiento,
                                                               cantidad,
                                                               concepto,
                                                               Observacion,
                                                               accion,
                                                               usuario))
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

        public bool ModificarMovimientoInventario(int idVentaDetalle,
                                                     int idProducto,
                                                     int idAlmacen,
                                                     string lote, DateTime fechaVencimiento,
                                                     decimal cantidadAnterior, decimal cantidadNueva,
                                                     int concepto,
                                                     string Observacion, string usuario,
                                                     string loteNuevo, DateTime fechaVencimientoNuevo)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (!this.iTi001.ModificarMovimientoInventario(idVentaDetalle,
                                                                   idProducto,
                                                                   idAlmacen,
                                                                   lote, fechaVencimiento,
                                                                   cantidadAnterior,cantidadNueva,
                                                                   concepto,
                                                                   Observacion,
                                                                   usuario, loteNuevo, fechaVencimientoNuevo))
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

        public bool EliminarMovimientoInventario(int idVentaDetalle,
                                              int idProducto,
                                              int idAlmacen,
                                              string lote, DateTime fechaVencimiento,
                                              decimal cantidad,
                                              int concepto,                                           
                                              EnAccionEnInventario accion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    if (!this.iTi001.EliminarMovimientoInventario(idVentaDetalle,
                                                                   idProducto,
                                                                   idAlmacen,
                                                                   lote, fechaVencimiento,   
                                                                   cantidad,
                                                                   concepto,
                                                                   accion
                                                                   ))
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
        #region Verificacioines

        #endregion
    }
}
