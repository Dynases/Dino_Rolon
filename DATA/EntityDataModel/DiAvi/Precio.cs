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
    
    public partial class Precio
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProduc { get; set; }
        public int IdPrecioCat { get; set; }
        public decimal Precio1 { get; set; }
        public Nullable<int> Oferta { get; set; }
        public string Observacion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual PrecioCat PrecioCat { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
