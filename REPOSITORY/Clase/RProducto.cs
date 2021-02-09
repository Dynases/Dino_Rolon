
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
using System.Data;
using ENTITY.inv.Traspaso.View;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RProducto : BaseConexion, IProducto
    {
        #region Transacciones
        public bool Guardar(VProducto vProducto, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var producto = new Producto();
                    producto.IdProd = vProducto.IdProd;
                    producto.Estado = vProducto.Estado;
                    producto.Tipo = vProducto.Tipo;
                    producto.CodBar = vProducto.CodBar;
                    producto.Descrip = vProducto.Descripcion;
                    producto.UniVen = vProducto.UniVenta;
                    producto.UniPeso = vProducto.UniPeso;
                    producto.Peso = vProducto.Peso;
                    producto.Grupo1 = vProducto.Grupo1;
                    producto.Grupo2 = vProducto.Grupo2;
                    producto.Grupo3 = vProducto.Grupo3;
                    producto.Grupo4 = vProducto.Grupo4;
                    producto.Grupo5 = vProducto.Grupo5;
                    producto.Imagen = vProducto.Imagen;
                    producto.IdProducto = vProducto.IdProducto;
                    producto.DescripProduc = vProducto.Producto2;
                    producto.Cantidad = vProducto.Cantidad;
                    producto.Fecha = vProducto.Fecha;
                    producto.Hora = vProducto.Hora;
                    producto.Usuario = vProducto.Usuario;
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
                    this.EliminarTI001(idProducto);
                    this.EliminarTI001(idProducto);
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
        public bool EliminarTI001(int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var alamacen = db.Almacen.ToList();
                    foreach (var fila in alamacen)
                    {
                        var productoTI001 = db.TI001.FirstOrDefault(b => b.iccprod == idProducto &&
                                                                         b.icalm == fila.Id);
                        if (productoTI001 != null)
                        {
                            db.TI001.Remove(productoTI001);
                        }
                        
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
        public bool EliminarPrecio(int idProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var sucursal = db.Sucursal.ToList();
                    foreach (var fila in sucursal)
                    {
                        var productoPrecio = db.Precio.FirstOrDefault(b => b.IdProduc == idProducto &&
                                                                           b.IdSucursal == fila.Id);
                        if (productoPrecio != null)
                        {
                            db.Precio.Remove(productoPrecio);
                        }                     
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
        #endregion
        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/
        public VProducto ListarXId(int id)
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
                                          IdProd = a.IdProd,
                                          CodBar = a.CodBar,
                                          Tipo = a.Tipo,
                                          Descripcion = a.Descrip,
                                          Peso = a.Peso,
                                          UniVenta = a.UniVen,
                                          UniPeso = a.UniPeso,
                                          Grupo1 = a.Grupo1,
                                          Grupo2 = a.Grupo2,
                                          Grupo3 = a.Grupo3,
                                          Grupo4 = a.Grupo4,
                                          Grupo5 = a.Grupo5,
                                          Imagen = a.Imagen,
                                          IdProducto = a.IdProducto,
                                          Producto2 = a.DescripProduc,
                                          Cantidad = a.Cantidad,
                                          EsLote = a.EsLote,
                                          Usuario = a.Usuario,
                                          Hora = a.Hora,
                                          Fecha = a.Fecha
                                      }).FirstOrDefault();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public List<VProductoLista> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var grupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO);
                    var Orden1 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1);
                    var Orden2 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2);
                    var Orden3 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3);
                    var listResult = (from a in db.Producto
                                      join grupo1 in db.Libreria on
                                      new { Grupo = grupo, Orden = Orden1, Libreria = a.Grupo1 }
                                         equals new { Grupo = grupo1.IdGrupo, Orden = grupo1.IdOrden, Libreria = grupo1.IdLibrer }
                                      join grupo2 in db.Libreria on
                                      new { Grupo = grupo, Orden = Orden2, Libreria = a.Grupo2 }
                                         equals new { Grupo = grupo2.IdGrupo, Orden = grupo2.IdOrden, Libreria = grupo2.IdLibrer }
                                      join grupo3 in db.Libreria on
                                      new { Grupo = grupo, Orden = Orden3, Libreria = a.Grupo3 }
                                         equals new { Grupo = grupo3.IdGrupo, Orden = grupo3.IdOrden, Libreria = grupo3.IdLibrer }
                                      select new VProductoLista
                                      {
                                          Id = a.Id,
                                          Codigo = a.IdProd,
                                          Descripcion = a.Descrip,
                                          Grupo1 = grupo1.Descrip,
                                          Grupo2 = grupo2.Descrip,
                                          Grupo3 = grupo3.Descrip,
                                          Tipo = a.Tipo,
                                          Usuario = a.Usuario,
                                          Hora = a.Hora,
                                          Fecha = a.Fecha
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado(int IdSucursal, int IdAlmacen,
                                          int IdCategoriaPrecio)
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = "SELECT " +
                                    "p.Id as ProductoId, p.Descrip as Producto, i.iccven as 'Stock Disponible', " +
                                    " a.Descrip as Almacen, pr.Precio as Precio, l.Descrip as 'Unidad de Venta', " +
                                    " prc.Descrip as 'Cat. Precio' " +
                                    " FROM " +
                                        " dbo.TI001 i " +
                                        "JOIN REG.Producto p ON i.iccprod = p.Id " +
                                        "JOIN INV.Almacen a ON i.icalm = a.Id " +                                        
                                        "JOIN INV.Sucursal s ON s.Id = a.IdSuc " +
                                        "JOIN REG.Precio pr ON pr.IdProduc = p.Id AND s.Id = pr.IdSucursal " +
                                        "JOIN REG.PrecioCat prc ON prc.Id = pr.IdPrecioCat " +
                                        "JOIN ADM.Libreria l ON l.IdLibrer = p.UniVen " +
                                    "WHERE " +
                                        "s.Id = " + IdSucursal + " and a.Id = " + IdAlmacen + " and prc.Id = " + IdCategoriaPrecio + " and l.IdGrupo = 3 and l.IdOrden = 6  " +
                                    "GROUP BY " +
                                        "s.Id, a.Id, p.Id, p.Descrip, a.Descrip, i.iccven, pr.Precio, l.Descrip, prc.Descrip";
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VProductoListaStock> ListarProductoStock(int IdSucursal, int IdAlmacen,
                                          int IdCategoriaPrecio)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from p in db.Producto
                                      join i in db.TI001 on p.Id equals i.iccprod
                                      join a in db.Almacen on i.icalm equals a.Id
                                      join s in db.Sucursal on a.IdSuc equals s.Id
                                      //PrecioVenta
                                      join pr in db.Precio on
                                      new { idProducto = p.Id, IdSucur = s.Id }
                                         equals new { idProducto = pr.IdProduc, IdSucur = pr.IdSucursal }
                                         //join pr in db.Precio on p.Id equals pr.IdProduc
                                      join prc in db.PrecioCat on pr.IdPrecioCat equals prc.Id
                                      join l in db.Libreria on p.UniVen equals l.IdLibrer
                                      join m in db.Libreria on p.Grupo2 equals m.IdLibrer
                                      join t in db.Libreria on p.Grupo3 equals t.IdLibrer
                                      join c in db.Libreria on p.Grupo4 equals c.IdLibrer
                                      //PrecioCosto
                                      join prcosto in db.Precio on
                                      new { idProducto = p.Id, IdSucur = s.Id }
                                         equals new { idProducto = prcosto.IdProduc, IdSucur = prcosto.IdSucursal }
                                      join prcc in db.PrecioCat on prcosto.IdPrecioCat equals prcc.Id
                                      where s.Id == IdSucursal &&
                                            a.Id == IdAlmacen &&
                                            prc.Id == IdCategoriaPrecio &&
                                            prcc.Id == (int)ENCategoriaPrecio.COSTO && //Precio de costo Id Estatico
                                            l.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO && l.IdOrden == (int)ENEstaticosOrden.PRODUCTO_UN_VENTA &&
                                            m.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO && m.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO2 &&
                                            t.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO && t.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO3 &&
                                            c.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO && c.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO4 
                                      select new VProductoListaStock
                                      {
                                          IdProducto = p.Id,
                                          IdAlmacen = a.Id,
                                          IdCategoriaPrecio = prc.Id,
                                          CodigoProducto = p.IdProd,                         
                                          Producto = p.Descrip,
                                          Division = m.Descrip,
                                          TipoProducto = t.Descrip,
                                          CategoriaProducto = t.Descrip,
                                          PrecioVenta = pr.Precio1,
                                          PrecioCosto = prcosto.Precio1,
                                          PrecioMinVenta = db.Precio.FirstOrDefault(pr=>pr.IdProduc == p.Id &&
                                                                                        pr.IdSucursal == s.Id &&
                                                                                        pr.IdPrecioCat == (int)ENCategoriaPrecio.B005).Precio1,
                                          PrecioMaxVenta = db.Precio.FirstOrDefault(pr => pr.IdProduc == p.Id &&
                                                                                 pr.IdSucursal == s.Id &&
                                                                                 pr.IdPrecioCat == (int)ENCategoriaPrecio.VENTA).Precio1,
                                          UnidadVenta = l.Descrip,
                                          Stock = (from y in db.TI001
                                                   where y.icalm == IdAlmacen && y.iccprod == p.Id
                                                   select y.iccven).Sum(),
                                          CategoriaPrecio = prc.Descrip,
                                          EsLote = p.EsLote,
                                          Contenido =db.Libreria.FirstOrDefault(x => x.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO &&
                                                                                     x.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO5 &&
                                                                                     x.IdLibrer == p.Grupo5).Descrip,
                                          EsMateriaPrima  = p.Tipo
                                      }).Distinct().ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool EsCategoriaSuper(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     from b in db.Libreria
                                     where a.Id.Equals(IdProducto) &&
                                     b.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO && 
                                     b.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO3 &&
                                     a.Grupo3 == (int)ENEstaticosLibreria.PRODUCTO_SUPER
                                     select a).Count();
                    return resultado != 0 ? true : false;
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
                                     join b in db.CompraIng_01 on a.Id equals b.IdProduc                                     
                                     where a.Id.Equals(IdProducto) && b.Estado != -1
                                     select a).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnCompraNormal(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.Compra_01 on a.Id equals b.IdProducto
                                     where b.IdProducto.Equals(IdProducto) && b.Estado != -1
                                     select a).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnMovimiento(int IdProducto)
        {
            try
            {
                //Verificar si Movimiento tiene estado de eliminado y poner
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.TI0021 on a.Id equals b.iccprod
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
        public bool ExisteEnVenta(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.Venta_01 on a.Id equals b.IdProducto
                                     where a.Id.Equals(IdProducto) && b.Estado != -1
                                     select a).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnSeleccion (int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.Seleccion_01 on a.Id equals b.IdProducto
                                     where a.Id.Equals(IdProducto) && b.Estado != -1
                                     select a).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnTransformacion(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.Producto
                                     join b in db.Transformacion_01 on a.Id equals b.IdProducto
                                     where a.Id.Equals(IdProducto) && b.Estado != -1
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
