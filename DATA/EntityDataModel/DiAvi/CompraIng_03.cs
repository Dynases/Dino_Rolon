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
    
    public partial class CompraIng_03
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProduc { get; set; }
        public int Estado { get; set; }
        public decimal Caja { get; set; }
        public decimal Grupo { get; set; }
        public decimal Maple { get; set; }
        public decimal Cantidad { get; set; }
        public decimal TotalCant { get; set; }
        public decimal PrecioCost { get; set; }
        public decimal Total { get; set; }
        public int TotalMaple { get; set; }
    
        public virtual CompraIng CompraIng { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
