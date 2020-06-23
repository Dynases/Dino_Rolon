using ENTITY.com.Seleccion.Report;
using ENTITY.com.Seleccion.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ISeleccion
    {
       
        bool Guardar(VSeleccion vSeleccion, ref int id);

        bool ModificarEstado(int IdSeleccion, int estado,ref List<string> lMensaje);
        List<VSeleccionEncabezado> TraerSelecciones();
        VSeleccionLista TraerSeleccion(int idSeleccion);
        List<RSeleccionNota> NotaSeleccion(int Id);
    }
}
