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
    
    public partial class Plantilla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plantilla()
        {
            this.Plantilla_01 = new HashSet<Plantilla_01>();
        }
    
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int Concepto { get; set; }
        public string Nombre { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Almacen Almacen1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla_01> Plantilla_01 { get; set; }
    }
}