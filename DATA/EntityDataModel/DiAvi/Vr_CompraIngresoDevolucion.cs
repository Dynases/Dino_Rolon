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
    
    public partial class Vr_CompraIngresoDevolucion
    {
        public int Id { get; set; }
        public string NumNota { get; set; }
        public System.DateTime FechaRec { get; set; }
        public System.DateTime FechaEnt { get; set; }
        public string Proveedor { get; set; }
        public string IdSpyre { get; set; }
        public string MarcaTipo { get; set; }
        public string Entregado { get; set; }
        public string DescripcionRecibido { get; set; }
        public int IdProduc { get; set; }
        public string Producto { get; set; }
        public decimal TotalCant { get; set; }
        public decimal PrecioCost { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> totalMaple { get; set; }
    }
}
