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
    
    public partial class Usuario_01
    {
        public int IdUsuario_01 { get; set; }
        public int IdUsuario { get; set; }
        public int IdAlmacen { get; set; }
        public Nullable<bool> Acceso { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    
        public virtual Usuario Usuario1 { get; set; }
        public virtual Almacen Almacen { get; set; }
    }
}
