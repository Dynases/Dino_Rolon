using ENTITY.com.Seleccion_01.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ISeleccion_01
    {
        bool Guardar(List<VSeleccion_01_Lista> Lista, int Id_Seleccion);
        bool GuardarModificar(List<VSeleccion_01_Lista> Lista, int Id);
        bool GuardarModificar_CompraIngreso(List<VSeleccion_01_Lista> Lista, int IdCompraIngreso);

        /******** VALOR/REGISTRO ÚNICO *********/
        List<VSeleccion_01_Lista> TraerSeleccion_01(int idSeleccion);
        /********** VARIOS REGISTROS ***********/
        List<VSeleccion_01_Lista> Listar();
        List<VSeleccion_01_Lista> ListarXId_CompraIng(int id, int tipo);
        List<VSeleccion_01_Lista> ListarXId_CompraIng_XSeleccion(int id);
        }
}
