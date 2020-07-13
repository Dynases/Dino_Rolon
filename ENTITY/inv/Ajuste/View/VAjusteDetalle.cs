using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Ajuste.View
{
    public class VAjusteDetalle
    {
        private string _idView;
        public VAjusteDetalle()
        {
            _idView = Guid.NewGuid().ToString();
        }

        public string IdView
        {
            get
            {
                return _idView;
            }
        }
        public int Id { get; set; }
        public int IdAjuste { get; set; }
        public int IdProducto { get; set; }
        public string CodProducto { get; set; }
        public string NProducto { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Contenido { get; set; }
        public decimal TotalContenido { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public string Lote { get; set; }
        public DateTime FechaVen { get; set; }
        public int Estado { get; set; }
    }
}
