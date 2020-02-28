using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Producto.View
{
    public class VProducto
    {
        public int Id { get; set; }
        public string IdProd { get; set; }
        public int Estado { get; set; }
        public int Tipo { get; set; }
        public string CodBar { get; set; }
        public string Descripcion { get; set; }
        public int UniVenta { get; set; }
        public int UniPeso { get; set; }
        public decimal Peso { get; set; }
        public int Grupo1 { get; set; }
        public int Grupo2 { get; set; }
        public int Grupo3 { get; set; }
        public int Grupo4 { get; set; }
        public int Grupo5 { get; set; }
        public string Imagen { get; set; }

        public int IdProducto { get; set; }
        public string Producto2 { get; set; }
        public decimal Cantidad { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public int Count { get; set; }
    }
}
