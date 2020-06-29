using ENTITY.inv.TI001.VIew;
using System;
using System.Collections.Generic;
using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface ITI001
    {
        /******** CONSULTA *********/
        List<VTI001> TraerInventarioLotes(int IdProducto, int idAlmacen);
        decimal TraerStockActual(int IdProducto, int idAlmacen, string lote, DateTime fecha);
        /******** TRANSACCIONES *********/
        bool Nuevo(int idAlmacen, int idProducto, decimal cantidad, string lote, DateTime fechaVen);
        bool NuevoMovimientoInventario(int idDetalle,
                                int idProducto,
                                int idAlmacen,
                                string lote,
                                DateTime fechaVen,
                                decimal cantidad,
                                int concepto,
                                string Observacion,
                                EnAccionEnInventario accion,
                                string usuario);
        bool ModificarMovimientoInventario(int idDetalle,
                               int idProducto,
                               int idAlmacen,
                               string lote,
                               DateTime fechaVen,
                               decimal cantidad,
                               decimal cantidad2,
                               int concepto,
                               string Observacion,
                               string usuario, string loteNuevo, DateTime fechaVenNuevo);
        bool EliminarMovimientoInventario(int idDetalle,
                              int idProducto,
                              int idAlmacen,
                              string lote,
                              DateTime fechaVen,
                              decimal cantidad,
                              int concepto,
                              EnAccionEnInventario accion);
        bool ActualizarInventario(int idProducto, int idAlmacen, EnAccionEnInventario accionEnInventario, decimal cantidad, string lote, DateTime fechaVen);
        bool ActualizarInventarioModificados(int idProducto,
                                        int idAlmacen,
                                        decimal cantidadAnterior,
                                        decimal cantidadNueva,
                                        string lote,
                                        DateTime fechaVen);
        /******** VERIFICACIONES *********/
  
        bool ExisteProducto(int IdProducto, int idAlmacen, string lote, DateTime fecha);
       
    }
}
