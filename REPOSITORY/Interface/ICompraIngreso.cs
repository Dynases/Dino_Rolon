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
        //Transaccioness
        bool Guardar(VCompraIngresoLista vCompraIngreso, ref int id);
        bool ModificarEstado(int IdCompraIngreso, int estado, ref List<string> lMensaje);
     
        //Consultas
        List<VCompraIngreso> Listar();
        List<VCompraIngresoLista> ListarXId(int id);       
        DataTable BuscarCompraIngreso(int estado);
        
        //Reportes
        DataTable ReporteCompraIngreso(DateTime? fechaDesde, DateTime? fechaHasta, int estado);
        List<VCompraIngresoNota> NotaCompraIngreso(int Id);
        List<VCompraIngresoNota> NotaCompraIngresoDevolucion(int Id);

        //Verificaciones
        bool ExisteEnSeleccion(int IdCompraIngreso);
    }
}
