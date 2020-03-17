namespace REPOSITORY.Interface
{
    public interface ITI0021
    {
        bool Guardar(int idTI002, int idProducto, int cantidad);
        System.Collections.Generic.List<ENTITY.inv.Almacen.View.VDetalleKardex> ListarDetalleKardex(System.DateTime inicio, System.DateTime fin, int IdAlmacen);
    }
}
