using ENTITY.com.CompraIngreso.Filter;
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
        bool ModificarEstado(int IdCompraIngreso, int estado);

        //Consultas
        VCompraIngresoLista TraerCompraIngreso(int id);
        List<VCompraIngreso> TraerComprasIngreso(int usuarioId);           
        DataTable BuscarCompraIngreso(int estado, int usuarioId);
        List<VCompraIngresoCombo> TraerCompraIngresoCombo(int usuarioId);
        List<VCompraIngresoCombo> TraerCompraIngresoComboCompleto();

            //Reportes

        List<VCompraIngresoNota> NotaCompraIngreso(int Id);
        List<VCompraIngresoNota> NotaCompraIngresoDevolucion(int Id);
        List<VCompraIngresoNota> NotaCompraIngresoResultado(int Id);
        DataTable ReporteCompraIngreso(FCompraIngreso fcompraIngreso);
        DataTable ReporteCriterioCompraIngreso(FCompraIngreso fcompraIngreso);
        DataTable ReporteCriterioCompraIngresoDevolucion(FCompraIngreso fcompraIngreso);
        DataTable ReporteCriterioCompraIngresoResultado(FCompraIngreso fcompraIngreso);
        //Verificaciones
        bool ExisteEnSeleccion(int IdCompraIngreso);
        bool ExisteEnDevolucion(int idCompraIng);
    }
}
