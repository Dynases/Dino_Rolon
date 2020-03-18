namespace ENTITY.inv.Almacen.View
{
    public class VDetalleKardex
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Unidad { get; set; }

        public decimal SaldoAnterior { get; set; }

        public decimal Entradas { get; set; }

        public decimal Salidas { get; set; }

        public decimal Saldo { get; set; }
    }
}
