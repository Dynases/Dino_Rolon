//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EntityDataModel.DiSoft
{
    using System;
    using System.Collections.Generic;
    
    public partial class App_Listado_CobrosPendientes
    {
        public int PedidoId { get; set; }
        public Nullable<int> ClienteId { get; set; }
        public string cliente { get; set; }
        public Nullable<int> PersonalId { get; set; }
        public string vendedor { get; set; }
        public Nullable<System.DateTime> FechaPedido { get; set; }
        public Nullable<decimal> totalfactura { get; set; }
        public Nullable<decimal> pendiente { get; set; }
    }
}