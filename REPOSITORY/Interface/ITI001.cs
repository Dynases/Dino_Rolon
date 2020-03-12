using UTILITY.Enum;

namespace REPOSITORY.Interface
{
    public interface ITI001
    {
        bool ActualizarInventario(string idProducto, int idAlmacen, EnAccionEnInventario accionEnInventario, decimal cantidad);
    }
}
