﻿using ENTITY.inv.TI001.VIew;
using ENTITY.inv.TI002.View;
using ENTITY.inv.TI0021.View;
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
                return this.iTi001.TraerStockActual(IdProducto, idAlmacen, lote, fecha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public List<VTI001> TraerInventarioLotes(int IdProducto, int idAlmacen)
        {
            try
            {
                return this.iTi001.TraerInventarioLotes(IdProducto, idAlmacen);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region Transacciones
        public bool NuevoMovimientoInventario(int idVentaDetalle,
                                              int idProducto,
                                              int idAlmacen,
                                              string lote, DateTime fechaVencimiento,
                                              decimal cantidad,
                                              int concepto,
                                              string Observacion,
                                              EnAccionEnInventario accion,
                                              string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi001.NuevoMovimientoInventario(idVentaDetalle,
                                                               idProducto,
                                                               idAlmacen,
                                                               lote, fechaVencimiento,
                                                               cantidad,
                                                               concepto,
                                                               Observacion,
                                                               accion,
                                                               usuario);
                    scope.Complete();
                    return resultado;
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
                    var resultado = this.iTi001.ModificarMovimientoInventario(idVentaDetalle,
                                                                   idProducto,
                                                                   idAlmacen,
                                                                   lote, fechaVencimiento,
                                                                   cantidadAnterior, cantidadNueva,
                                                                   concepto,
                                                                   Observacion,
                                                                   usuario, loteNuevo, fechaVencimientoNuevo);
                    scope.Complete();
                    return resultado;
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
                    var resultado = this.iTi001.EliminarMovimientoInventario(idVentaDetalle,
                                                                   idProducto,
                                                                   idAlmacen,
                                                                   lote, fechaVencimiento,
                                                                   cantidad,
                                                                   concepto,
                                                                   accion
                                                                   );
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool NuevoTraspasoInventario(VTI0021 detalle, int idAlmacen,
                               EnAccionEnInventario accion, int idTraspaso)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi001.NuevoTraspasoInventario(detalle,idAlmacen, accion, idTraspaso);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Funcionalidad de traspasos, valida existenia, ingresa o modifica el inventario y las tablas de movimientos de detalle.
        public bool ModificarTraspasoInventario(VTI0021 detalle, int idAlmacen,int idMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi001.ModificarTraspasoInventario(detalle, idAlmacen, idMovimiento);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EliminarTraspasoInventario(VTI0021 detalle, int idEncabezadp,
                               int idAlmacen,
                               int concepto,
                               EnAccionEnInventario accion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi001.EliminarTraspasoInventario(detalle, idEncabezadp, idAlmacen, concepto, accion);
                    scope.Complete();
                    return resultado;
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
