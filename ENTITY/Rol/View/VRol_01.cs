using DATA.EntityDataModel.DiAvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTITY.Rol.View
{
    public class VRol_01
    {
        public int IdRol_01 { get; set; }
        public Nullable<int> IdRol { get; set; }
        public Nullable<int> IdPrograma { get; set; }        
        public string NombreProg { get; set; }
        public string Descripcion { get; set; }
        public int? IdModulo { get; set; }
        public int Estado { get; set; }
        public Nullable<bool> Show { get; set; }
        public Nullable<bool> Add { get; set; }
        public Nullable<bool> Mod { get; set; }
        public Nullable<bool> Del { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }

        public virtual VRol Rol { get; set; }
    }
}
