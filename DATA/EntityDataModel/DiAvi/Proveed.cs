//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EntityDataModel.DiAvi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proveed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveed()
        {
            this.Proveed_01 = new HashSet<Proveed_01>();
            this.Compra = new HashSet<Compra>();
            this.CompraIng = new HashSet<CompraIng>();
        }
    
        public int Id { get; set; }
        public string IdSpyre { get; set; }
        public int Estado { get; set; }
        public string Descrip { get; set; }
        public int Ciudad { get; set; }
        public int Tipo { get; set; }
        public int TipoProve { get; set; }
        public string Direcc { get; set; }
        public string Contacto { get; set; }
        public string Telfon { get; set; }
        public string Email { get; set; }
        public string Contacto2 { get; set; }
        public string Telfon2 { get; set; }
        public string Email2 { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longit { get; set; }
        public string Imagen { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveed_01> Proveed_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraIng> CompraIng { get; set; }
    }
}
