namespace REPOSITORY.Interface
{
    public interface ITI002
    {
        bool Guardar(int idAlmacenSalida, string almacenSalida, int idAlmacenDestino, string almacenDestino, int idTraspaso, string usuario, string observaciones, int concepto, ref int idTI2);

        bool Modificar(int idAlmacenSalida,                          
                           int idAlmacenDestino,                           
                           int? idDetalle,
                           string usuario,
                           string observaciones,
                           int concepto);

        bool Eliminar(int IdDetalle, int concepto);
        bool ExisteEnMovimiento(int idDetalle, int concepto);
    }
}
