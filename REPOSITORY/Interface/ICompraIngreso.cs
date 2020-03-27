using ENTITY.com.CompraIngreso.View;
using ENTITY.inv.TI001.VIew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface ICompraIngreso
    {
        bool Guardar(VCompraIngresoLista vCompraIngreso, ref int id);
        List<VCompraIngreso> Listar();
        List<VCompraIngresoLista> ListarXId(int id);
        List<VCompraIngresoNota> ListarNotaXId(int Id);
        DataTable ListarEncabezado();
        bool ModificarEstado(int IdCompraIngreso, int estado, ref List<string> lMensaje);
        bool ExisteEnSeleccion(int IdCompraIngreso);
        List<VTI001> ListarStock(int IdProducto);
    }
}
