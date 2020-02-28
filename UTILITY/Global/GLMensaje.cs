using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILITY.Global
{
   public static class GLMensaje
    {
        public static string Error = "Comuniquece con el administrador del sistema";
        public static string Nuevo_Exito(string programa, string codigo)
        {
            return "Código de " + programa+" " + codigo + " grabado con éxito";
        }
        public static string Modificar_Exito(string programa, string codigo)
        {
            return "Código de " + programa +" " + codigo + " modificado con éxito";
        }
        public static string Registro_Error(string programa)
        {
            return programa + " No pudo ser grabada"; 
        }
        public static string Eliminar_Error_Transaciones_Relacionadas(string programa)
        {
            return programa + "  no puede ser eliminado por que tiene transacciones relacionadas";
        }
        public static string Eliminar_Error(string programa,string id)
        {
            return programa + " con código "+ id +" no puede ser eliminado ";
        }
        public static string Eliminar_Exito(string programa, string codigo)
        {
            return "Código de " + programa +" "+ codigo + " eliminado con éxito";
        }
        public static string Pregunta_Eliminar = "¿Esta seguro de eliminar el registro?";
        public static string Mensaje_Principal = "Mensaje Principal";     

    }
}
