using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.inv.Ajuste.View
{
   public class VAjusteFisicoProducto
    {
        private string _idView;
        public VAjusteFisicoProducto()
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
        public int Estado { get; set; }
        public int IdAjuste { get; set; }
        public int IdProducto { get; set; }
        public string CodProducto { get; set; }
        public string NProducto { get; set; }
        public decimal Saldo { get; set; }
        public decimal Fisico { get; set; }
        public decimal Diferencia { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaVen { get; set; }
        public string Lote { get; set; }

    }
}
