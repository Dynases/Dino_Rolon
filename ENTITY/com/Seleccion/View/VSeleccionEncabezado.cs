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
        public int IdCompraIng { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Granja { get; set; }
        public string Proveedor { get; set; }
        public decimal TotalRecepcion { get; set; }
        public string IdSkype { get; set; }
        public string TipoProveedor { get; set; }
        public string Devolucion { get; set; }
        public DateTime FechaReg { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Merma { get; set; }
        public decimal MermaPorcentaje { get; set; }
        public decimal PicadoPorcentaje { get; set; }
        public decimal ManchadoPorcentaje { get; set; }
        public int? Dias { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int Id { get; set; }
        public string TipoCategoria { get; set; }
        public decimal ManchadoAbsoluto { get; set; }
        public decimal PicadoAbsoluto { get; set; }
    }
}
