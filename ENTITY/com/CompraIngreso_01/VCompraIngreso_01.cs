using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.CompraIngreso_01
{
    public class VCompraIngreso_01
    {
        public int Id { get; set; }       
        public int IdProduc { get; set; }
        public string Producto { get; set; }
        public decimal Caja { get; set; }
        public decimal Grupo { get; set; }
        public decimal Maple { get; set; }
        public decimal Cantidad { get; set; }
        public decimal TotalCant { get; set; }
        public decimal PrecioCost { get; set; }
        public decimal Total { get; set; }
        public int TotalMaple { get; set; }
        public int Estado { get; set; }     


    }
}
