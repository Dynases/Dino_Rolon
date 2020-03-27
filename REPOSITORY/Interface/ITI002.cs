namespace REPOSITORY.Interface
{
    public interface ITI002
    {
        bool Guardar(int idAlmacenSalida, string almacenSalida, int idAlmacenDestino, string almacenDestino, int idTraspaso, string usuario, string observaciones, ref int idTI2);
        bool Eliminar(int IdDetalle, int concepto);
    }
}
