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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Compra_01 = new HashSet<Compra_01>();
            this.CompraIng_01 = new HashSet<CompraIng_01>();
            this.Seleccion_01 = new HashSet<Seleccion_01>();
            this.Plantilla_01 = new HashSet<Plantilla_01>();
            this.Traspaso_01 = new HashSet<Traspaso_01>();
            this.Venta_01 = new HashSet<Venta_01>();
        }
    
        public int Id { get; set; }
        public string IdProd { get; set; }
        public int Estado { get; set; }
        public Nullable<int> Granja { get; set; }
        public int Tipo { get; set; }
        public string CodBar { get; set; }
        public string Descrip { get; set; }
        public int UniVen { get; set; }
        public int UniPeso { get; set; }
        public decimal Peso { get; set; }
        public int Grupo1 { get; set; }
        public int Grupo2 { get; set; }
        public int Grupo3 { get; set; }
        public int Grupo4 { get; set; }
        public int Grupo5 { get; set; }
        public string Imagen { get; set; }
        public int IdProducto { get; set; }
        public string DescripProduc { get; set; }
        public decimal Cantidad { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra_01> Compra_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompraIng_01> CompraIng_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seleccion_01> Seleccion_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantilla_01> Plantilla_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traspaso_01> Traspaso_01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta_01> Venta_01 { get; set; }
    }
}
