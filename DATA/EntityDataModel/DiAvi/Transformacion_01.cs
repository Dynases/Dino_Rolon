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
    
    public partial class Transformacion_01
    {
        public int Id { get; set; }
        public int IdTransformacion { get; set; }
        public int IdProducto { get; set; }
        public int IdProducto_Mat { get; set; }
        public int Estado { get; set; }
        public decimal TotalProd { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
    
        public virtual Transformacion Transformacion { get; set; }
    }
}
