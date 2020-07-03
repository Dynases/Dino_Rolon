using System;

namespace REPOSITORY.Interface
{
    public interface ITI0021
    {
        bool Guardar(int idTI002, int idProducto, decimal cantidad, string lote, DateTime fechaVen);
        System.Collections.Generic.List<ENTITY.inv.Almacen.View.VDetalleKardex> ListarDetalleKardex(System.DateTime inicio, System.DateTime fin, int IdAlmacen);
        bool Modificar(decimal cantidad, int IdDetalle, int concepto, string lote, DateTime fechaVen);
        bool Eliminar(int IdDetalle, int concepto);

        bool ModificarTraspaso(decimal cantidad,
                           int idTraspaso,
                           int idProducto,
                           string lote,
                           DateTime fechaVen);
        bool EliminarTraspaso(int idTraspaso,
                           int idProducto,
                           string lote,
                           DateTime fechaVen);
    }
}
