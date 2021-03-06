﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY.com.Seleccion.View
{
   public class VSeleccion
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdCompraIng { get; set; }
        public int Estado { get; set; }
        public DateTime FechaReg { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Usuario { get; set; }
        public decimal Merma { get; set; }
        public decimal MermaPorcentaje { get; set; }
        public decimal ManchadoPorcentaje { get; set; }
        public decimal PicadoPorcentaje { get; set; }
        public decimal ManchadoAbsoluto{ get; set; }
        public decimal PicadoAbsoluto { get; set; }
    }
}
