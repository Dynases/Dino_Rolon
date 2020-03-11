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
    
    public partial class Almacen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Almacen()
        {
            this.Compra = new HashSet<Compra>();
            this.CompraIng = new HashSet<CompraIng>();
            this.Seleccion = new HashSet<Seleccion>();
            this.Plantilla = new HashSet<Plantilla>();
            this.Plantilla1 = new HashSet<Plantilla>();
            this.Traspaso = new HashSet<Traspaso>();
            this.Traspaso1 = new HashSet<Traspaso>();
            this.Venta = new HashSet<Venta>();
            this.Precio = new HashSet<Precio>();
        }
    
        public int Id { get; set; }
        public int IdSuc { get; set; }
        public string Descrip { get; set; }
        public string Direcc { get; set; }
        public string Telef { get; set; }
        public Nullable<decimal> Latit { get; set; }
        public Nullable<decimal> Longi { get; set; }
        public string Imagen { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public Nullable<int> TipoAlmacen { get; set; }
        public string Encargado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraIng> CompraIng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seleccion> Seleccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla> Plantilla { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla> Plantilla1 { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual TipoAlmacen TipoAlmacen1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traspaso> Traspaso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traspaso> Traspaso1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Venta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Precio> Precio { get; set; }
    }
}
