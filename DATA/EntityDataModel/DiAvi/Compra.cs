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
    
    public partial class Compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Compra()
        {
            this.Compra_01 = new HashSet<Compra_01>();
        }
    
        public int Id { get; set; }
        public int IdSuc { get; set; }
        public int IdProvee { get; set; }
        public int Estado { get; set; }
        public System.DateTime FechaDoc { get; set; }
        public int TipoVenta { get; set; }
        public System.DateTime FechaVen { get; set; }
        public string Observ { get; set; }
        public decimal Descu { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra_01> Compra_01 { get; set; }
        public virtual Proveed Proveed { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}
