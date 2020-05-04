using DATA.EntityDataModel.DiSoft;
using ENTITY.Producto.View;
using REPOSITORY.Base;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase.DiSoft
{
    public class RProductoD : BaseConexion2, IProductoD
    {
        #region Transacciones
        public bool Eliminar(int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = db.TC001.Where(a => a.canumi == idProducto).FirstOrDefault();                  
                    if (producto != null)
                    {
                        db.TC001.Remove(producto);
                    }                   
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Guardar(VProducto vproducto, ref int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    TC001 producto;
                    var aux = idProducto;
                    producto = db.TC001.Where(a => a.canumi == aux).FirstOrDefault();
                  
                    if (producto == null)
                        aux = 0;
                    if (aux == 0)
                    {
                        producto = new TC001();
                        db.TC001.Add(producto);
                        producto.canumi = idProducto;
                    }
                    producto.cacod = vproducto.IdProd;
                    producto.cadesc = vproducto.Descripcion;
                    producto.cadesc2 = "";
                    producto.cacat = 1;//Categoria Alimentos
                    producto.caimg = vproducto.Imagen;
                    producto.castc = true; // PARA QUE SIRVE
                    producto.caest = vproducto.Estado == 1?true:false;
                    producto.caserie = false; //Serie en 0  aparece en productos en 1 no aparece
                    producto.capcom = 0;
                    producto.cafing = DateTime.Now;
                    producto.cacemp = 1; //Empresa
                    producto.cahact = vproducto.Hora;
                    producto.cafact = vproducto.Fecha;
                    producto.cauact = vproducto.Usuario;
                    producto.cacbarra = vproducto.CodBar;
                    producto.casmin =0; //Stock minimo
                    producto.cagr1 = vproducto.Grupo1;
                    producto.cagr2 = vproducto.Grupo2;
                    producto.cagr3 = vproducto.Grupo3;
                    producto.cagr4 = vproducto.Grupo4;
                    producto.caumed =vproducto.UniPeso;
                    producto.cauventa = vproducto.UniVenta;
                    producto.caumax = 2;
                    producto.caconv = vproducto.Peso;
                    producto.capack = 0;          
                    idProducto = producto.canumi;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
