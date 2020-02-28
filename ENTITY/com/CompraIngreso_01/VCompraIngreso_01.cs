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
        //public decimal UnidadCajas()
        //{
        //    return Caja * 360;
        //}
        //public decimal UnidadGrupo()
        //{
        //    return Grupo * 300;
        //}
        //public decimal UnidadMaple()
        //{
        //    return Maple * 30;
        //}
        //public decimal MapleCajas()
        //{
        //    return Caja * 12;
        //}
        //public decimal MapleGrupo()
        //{
        //    return Grupo * 10;
        //}
        //public decimal MapleMaple()
        //{
        //    return Maple * 1;
        //}
        //public int MapleUnidad()
        //{
        //    return Convert.ToInt32((Maple /300) * 11);
        //}


    }
}
