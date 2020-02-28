using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.Producto.View
{
    public class VProductoLista
    {
        public int Id { get; set; }

        public string Cod_Producto { get; set; }
        public string Descripcion { get; set; }
        //public string UniVenta { get; set; }       
        public string Grupo1 { get; set; }
        public string Grupo2 { get; set; }
        public string Grupo3 { get; set; }
        public string Usuario { get; set; }
        public string Hora { get; set; }
        public DateTime Fecha { get; set; }
    }
}
