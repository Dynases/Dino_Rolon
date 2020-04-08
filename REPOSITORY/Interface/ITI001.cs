using ENTITY.inv.TI001.VIew;
using System;
using System.Collections.Generic;
using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface ITI001
    {
        bool Nuevo(int idAlmacen, string idProducto, decimal cantidad, string lote, DateTime? fechaVen);
        bool ActualizarInventario(string idProducto, int idAlmacen, EnAccionEnInventario accionEnInventario, decimal cantidad, string lote, DateTime? fechaVen);
        bool ActualizarInventarioModificados(string idProducto,
                                        int idAlmacen,
                                        decimal cantidadAnterior,
                                        decimal cantidadNueva,
                                        string lote,
                                        DateTime? fechaVen);
        List<VTI001> Listar(int IdProducto);
        decimal? StockActual(string IdProducto, int? idAlmacen, string lote, DateTime? fecha);
        bool ExisteProducto(string IdProducto, int? idAlmacen, string lote, DateTime? fecha);
        bool NuevoMovimientoInventario(int idDetalle,
                                 string idProducto,
                                 int idAlmacen,
                                 string lote,
                                 DateTime? fechaVen,
                                 decimal cantidad,
                                 int concepto,
                                 string Observacion,
                                 EnAccionEnInventario accion,
                                 string usuario);
        bool ModificarMovimientoInventario(int idDetalle,
                               string idProducto,
                               int idAlmacen,
                               string lote,
                               DateTime? fechaVen,
                               decimal cantidad,
                               decimal cantidad2,
                               int concepto,
                               string Observacion,
                               string usuario, string loteNuevo, DateTime? fechaVenNuevo);
        bool EliminarMovimientoInventario(int idDetalle,
                              string idProducto,
                              int idAlmacen,
                              string lote,
                              DateTime? fechaVen,
                              decimal cantidad,
                              int concepto,
                              EnAccionEnInventario accion);
    }
}
