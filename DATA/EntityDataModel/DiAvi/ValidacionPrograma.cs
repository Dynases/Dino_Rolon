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
    
    public partial class ValidacionPrograma
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string TablaOrigen { get; set; }
        public string CampoOrigen { get; set; }
        public string TableDestino { get; set; }
        public string CampoDestino { get; set; }
        public string Programa { get; set; }
        public int IdGrupo { get; set; }
        public int IdOrden { get; set; }
    }
}