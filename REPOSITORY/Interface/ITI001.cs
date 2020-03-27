using ENTITY.inv.TI001.VIew;
using System;
using System.Collections.Generic;
using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface ITI001
    {
        bool ActualizarInventario(string idProducto, int idAlmacen, EnAccionEnInventario accionEnInventario, decimal cantidad, string lote, DateTime? fechaVen);
        List<VTI001> Listar(int IdProducto);
        decimal? StockActual(string IdProducto, int? idAlmacen, string lote, DateTime? fecha);
        bool ExisteProducto(string IdProducto, int? idAlmacen, string lote, DateTime? fecha);
    }
}
