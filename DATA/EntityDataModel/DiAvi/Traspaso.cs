//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EntityDataModel.DiAvi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Traspaso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Traspaso()
        {
            this.Traspaso_01 = new HashSet<Traspaso_01>();
        }
    
        public int Id { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int Estado { get; set; }
        public string UsuarioEnvio { get; set; }
        public string UsuarioRecepcion { get; set; }
        public System.DateTime FechaEnvio { get; set; }
        public System.DateTime FechaRecepcion { get; set; }
        public string Observaciones { get; set; }
        public int EstadoEnvio { get; set; }
        public decimal TotalUnidad { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Almacen Almacen1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traspaso_01> Traspaso_01 { get; set; }
    }
}
