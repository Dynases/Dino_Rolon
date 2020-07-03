using ENTITY.inv.TI002.View;

namespace REPOSITORY.Interface
{
    public interface ITI002
    {
        bool Guardar(int idAlmacenSalida, int idAlmacenDestino, int idTraspaso, string usuario, string observaciones, int concepto, ref int idTI2,int idDestino);

        bool Modificar(int idAlmacenSalida,                          
                           int idAlmacenDestino,                           
                           int idDetalle,
                           string usuario,
                           string observaciones,
                           int concepto,int idDestino);

        bool Eliminar(int IdDetalle, int concepto);
        bool ExisteEnMovimiento(int idDetalle, int concepto);

        bool ModificarCampoDestinoTraspaso(int idTraspaso);
        VTI002 TraerMovimiento(int idDetalle, int idConcepto);
    }
}
