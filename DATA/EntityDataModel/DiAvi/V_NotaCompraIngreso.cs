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
    
    public partial class V_NotaCompraIngreso
    {
        public int Id { get; set; }
        public string NumNota { get; set; }
        public System.DateTime FechaRec { get; set; }
        public System.DateTime FechaEnt { get; set; }
        public string Proveedor { get; set; }
        public string IdSpyre { get; set; }
        public string MarcaTipo { get; set; }
        public string Entregado { get; set; }
        public string DescripcionPlaca { get; set; }
        public string DescripcionRecibido { get; set; }
        public int IdProduc { get; set; }
        public string Producto { get; set; }
        public decimal TotalCant { get; set; }
        public decimal PrecioCost { get; set; }
        public decimal Total { get; set; }
        public int TotalMaple { get; set; }
    }
}
