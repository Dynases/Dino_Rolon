using ENTITY.inv.TI001.VIew;
using System;
using System.Collections.Generic;
using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface ITI001
    {
        bool Nuevo(int idAlmacen, int idProducto, decimal cantidad, string lote, DateTime fechaVen);
        bool ActualizarInventario(int idProducto, int idAlmacen, EnAccionEnInventario accionEnInventario, decimal cantidad, string lote, DateTime fechaVen);
        void ActualizarInventario(int idProducto, int idAlmacen, decimal cantidad, string lote, DateTime fechaVen);
        bool ActualizarInventarioModificados(int idProducto,
                                        int idAlmacen,
                                        decimal cantidadAnterior,
                                        decimal cantidadNueva,
                                        string lote,
                                        DateTime fechaVen);
        List<VTI001> Listar(int IdProducto);
        decimal TraerStockActual(int IdProducto, int idAlmacen, string lote, DateTime fecha);
        bool ExisteProducto(int IdProducto, int idAlmacen, string lote, DateTime fecha);
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
    }
}
