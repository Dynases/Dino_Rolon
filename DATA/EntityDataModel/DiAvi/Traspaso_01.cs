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
    
    public partial class Traspaso_01
    {
        public int Id { get; set; }
        public Nullable<int> TraspasoId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> Estado { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Traspaso Traspaso { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
