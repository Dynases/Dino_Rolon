namespace ENTITY.inv.Almacen.View
{
    public class VAlmacen
    {
        public int Id { get; set; }

        public int IdSucursal { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

        public string Imagen { get; set; }

        public string Hora { get; set; }

        public string Usuario { get; set; }

        public string Encargado { get; set; }

        public int TipoAlmacenId { get; set; }

    }
}
