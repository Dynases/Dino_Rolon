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
    
    public partial class Vr_TransformacionIngreso
    {
        public int Id { get; set; }
        public string AlmacenIngreso { get; set; }
        public string AlmacenSalida { get; set; }
        public string Observ { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdProducto { get; set; }
        public string Descrip { get; set; }
        public decimal TotalProd { get; set; }
        public int IdProducto_Mat { get; set; }
        public string MateriaPrima { get; set; }
        public decimal TotalMateriaPrima { get; set; }
        public decimal Cantidad { get; set; }
    }
}
