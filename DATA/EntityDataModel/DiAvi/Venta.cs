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
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.Venta_01 = new HashSet<Venta_01>();
        }
    
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdCliente { get; set; }
        public System.DateTime FechaVenta { get; set; }
        public int Estado { get; set; }
        public int Tipo { get; set; }
        public string Observaciones { get; set; }
        public int EncPrVenta { get; set; }
        public int EncVenta { get; set; }
        public string EncEntrega { get; set; }
        public int EmpresaFactura { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int IdPedidoDisoft { get; set; }
        public int IdCompraIngreso { get; set; }
        public string FacturaExterna { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta_01> Venta_01 { get; set; }
    }
}
