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
    
    public partial class TI001
    {
        public int icalm { get; set; }
        public int iccprod { get; set; }
        public decimal iccven { get; set; }
        public Nullable<int> icuven { get; set; }
        public string iclot { get; set; }
        public System.DateTime icfven { get; set; }
        public int id { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
