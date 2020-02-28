using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.Producto.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;
namespace REPOSITORY.Clase
{
    public class RProducto : BaseConexion, IProducto
    {
        #region Transacciones
        public bool Guardar(VProducto Producto,ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = new Producto();
                    producto.IdProd = Producto.IdProd;
                    producto.Estado = producto.Estado;
                    producto.Tipo = Producto.Tipo;
                    producto.CodBar = Producto.CodBar;
                    producto.Descrip = Producto.Descripcion;
                    producto.UniVen = Producto.UniVenta;
                    producto.UniPeso = Producto.UniPeso;
                    producto.Peso = Producto.Peso;
                    producto.Grupo1 = Producto.Grupo1;
                    producto.Grupo2 = Producto.Grupo2;
                    producto.Grupo3 = Producto.Grupo3;
                    producto.Grupo4 = Producto.Grupo4;
                    producto.Grupo5 = Producto.Grupo5;
                    producto.Imagen = Producto.Imagen;
                    producto.IdProducto = Producto.IdProducto;
                    producto.DescripProduc = Producto.Producto2;
                    producto.Cantidad = Producto.Cantidad;
                    producto.Fecha = Producto.Fecha;
                    producto.Hora = Producto.Hora;
                    producto.Usuario = Producto.Usuario;
                    db.Producto.Add(producto);
                    db.SaveChanges();
                    id = producto.Id;
                    return true;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VProducto Producto, int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = db.Producto.SingleOrDefault(b => b.Id == idProducto);
                    producto.IdProd = Producto.IdProd;
                    producto.Estado = producto.Estado;
                    producto.CodBar = Producto.CodBar;
                    producto.Descrip = Producto.Descripcion;
                    producto.UniVen = Producto.UniVenta;
                    producto.UniPeso = Producto.UniPeso;
                    producto.Peso = Producto.Peso;
                    producto.Grupo1 = Producto.Grupo1;
                    producto.Grupo2 = Producto.Grupo2;
                    producto.Grupo3 = Producto.Grupo3;
                    producto.Grupo4 = Producto.Grupo4;
                    producto.Grupo5 = Producto.Grupo5;
                    producto.Imagen = Producto.Imagen;
                    producto.IdProducto = Producto.IdProducto;
                    producto.DescripProduc = Producto.Producto2;
                    producto.Cantidad = Producto.Cantidad;
                    producto.Fecha = Producto.Fecha;
                    producto.Hora = Producto.Hora;
                    producto.Usuario = Producto.Usuario;
                    db.Producto.Attach(producto);
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    idProducto = producto.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = db.Producto.FirstOrDefault(b => b.Id == idProducto);
                    db.Producto.Remove(producto);
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
        #region Consultas
        public List<VProductoLista> Listar()
        {
            try
            {
                using ( var db= GetEsquema())
                {
                    var grupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO);
                    var Orden1 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1);
                    var Orden2 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2);
                    var Orden3 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3);
                    var listResult = (from a in db.Producto 
                                      join grupo1 in db.Libreria on
                                      new { Grupo =grupo, Orden = Orden1, Libreria = a.Grupo1}
                                         equals new { Grupo = grupo1.IdGrupo, Orden = grupo1.IdOrden, Libreria = grupo1.IdLibrer}
                                      join grupo2 in db.Libreria on
                                      new { Grupo = grupo, Orden = Orden2, Libreria = a.Grupo2 }
                                         equals new { Grupo = grupo2.IdGrupo, Orden = grupo2.IdOrden, Libreria = grupo2.IdLibrer }
                                      join grupo3 in db.Libreria on
                                      new { Grupo = grupo, Orden = Orden3, Libreria = a.Grupo3 }
                                         equals new { Grupo = grupo3.IdGrupo, Orden = grupo3.IdOrden, Libreria = grupo3.IdLibrer }
                                      select new VProductoLista
                                      {
                                          Id = a.Id,
                                          Cod_Producto = a.IdProd,
                                          Descripcion = a.Descrip,
                                          Grupo1 = grupo1.Descrip,
                                          Grupo2 = grupo2.Descrip,
                                          Grupo3 = grupo3.Descrip,
                                          Usuario = a.Usuario,
                                          Hora = a.Hora,
                                          Fecha =a.Fecha                                          
                                      }).ToList();
                    return listResult;
                }
            }
            catch ( Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        
        
        //join grupo4 in db.Libreria on
        //new { Grupo = ENEstaticosGrupo.PRODUCTO, Orden = ENEstaticosOrden.PRODUCTO_GRUPO4, Libreria = a.Grupo4 }
        //   equals new { Grupo = grupo4.IdGrupo, Orden = grupo4.IdOrden, Libreria = grupo4.IdLibrer }
        //join grupo5 in db.Libreria on
        //new { Grupo = ENEstaticosGrupo.PRODUCTO, Orden = ENEstaticosOrden.PRODUCTO_GRUPO4, Libreria = a.Grupo5 }
        //   equals new { Grupo = grupo5.IdGrupo, Orden = grupo5.IdOrden, Libreria = grupo5.IdLibrer }
        //join uniVenta in db.Libreria on
        //new { Grupo = ENEstaticosGrupo.PRODUCTO, Orden = ENEstaticosOrden.PRODUCTO_UN_VENTA, Libreria = a.UniVenta }
        //   equals new { Grupo = uniVenta.IdGrupo, Orden = uniVenta.IdOrden, Libreria = uniVenta.IdLibrer }
        public List<VProducto> ListarXId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Producto
                                      where a.Id.Equals(id)
                                      select new VProducto
                                      {
                                          Id = a.Id,
                                          IdProd =a.IdProd,
                                          CodBar = a.CodBar,
                                          Tipo = a.Tipo,
                                          Descripcion = a.Descrip,
                                          Peso =a.Peso,
                                          UniVenta =a.UniVen,
                                          UniPeso = a.UniPeso,
                                          Grupo1 = a.Grupo1,
                                          Grupo2 =a.Grupo2,
                                          Grupo3 =a.Grupo3,
                                          Grupo4 =a.Grupo4,
                                          Grupo5 =a.Grupo5,
                                          Imagen =a.Imagen,
                                          IdProducto =  a.IdProducto,
                                          Producto2 = a.DescripProduc,
                                          Cantidad=a.Cantidad,
                                          Usuario =a.Usuario,
                                          Hora =a.Hora,
                                          Fecha =a.Fecha                                        
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnCompra(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.CompraIng_01 on
                                     new { Id = a.Id }
                                        equals new { Id = b.IdProduc }
                                     join c in db.Compra_01 on
                                     new { Id = a.Id }
                                        equals new { Id = c.IdProducto }
                                     where a.Id.Equals(IdProducto)
                                     select a).Count();
                    return resultado != 0 ? true : false;
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
