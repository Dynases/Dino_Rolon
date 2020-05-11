using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.CompraIngreso.View
{
    public class VCompraIngresoLista
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProvee { get; set; }
        public int estado { get; set; }
        public int IdProvee_01 { get; set; }
        public string Proveedor { get; set; }
        public string NumNota { get; set; }
        public System.DateTime FechaEnt { get; set; }
        public System.DateTime FechaRec { get; set; }
        public int Placa { get; set; }
        public int Tipo { get; set; }
        public string CantidadSemanas { get; set; }
        public string Observacion { get; set; }
        public string Entregado { get; set; }
        public string Recibido { get; set; }
        public decimal Total { get; set; }
        public decimal TotalRecibido { get; set; }
        public decimal TotalVendido { get; set; }
        public  int TipoCompra { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int CantidadCaja { get; set; }
        public int CantidadGrupo { get; set; }
        public string CompraAntiguaFecha { get; set; }
    }
}
