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
    
    public partial class Traspaso_02
    {
        public int Id { get; set; }
        public int TraspasoId { get; set; }
        public int AlmacenId { get; set; }
        public int Estado { get; set; }
    
        public virtual Almacen Almacen { get; set; }
        public virtual Traspaso Traspaso { get; set; }
    }
}