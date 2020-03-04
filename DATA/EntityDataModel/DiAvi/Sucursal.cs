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
    
    public partial class Sucursal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sucursal()
        {
            this.Compra = new HashSet<Compra>();
            this.CompraIng = new HashSet<CompraIng>();
            this.Precio = new HashSet<Precio>();
        }
    
        public int Id { get; set; }
        public int IdDepos { get; set; }
        public string Descrip { get; set; }
        public string Direcc { get; set; }
        public string Telef { get; set; }
        public Nullable<decimal> Latit { get; set; }
        public Nullable<decimal> Longi { get; set; }
        public string Imagen { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraIng> CompraIng { get; set; }
        public virtual Deposito Deposito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Precio> Precio { get; set; }
    }
}
