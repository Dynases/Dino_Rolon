using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.View
{
    public class VSeleccionEncabezado
    {
        public int Id { get; set; }
        public int IdCompraIng { get; set; }
        public string Granja { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string tipoCompra { get; set; }
        public string TipoCategoria { get; set; }
       
        public decimal Merma { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
    }
}
