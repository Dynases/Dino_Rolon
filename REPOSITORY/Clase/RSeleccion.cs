using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Seleccion.View;
using DATA.EntityDataModel.DiAvi;
using ENTITY.com.Seleccion.Report;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using System.Data.Entity.SqlServer;
using System.Data;
using System.Data.SqlClient;

namespace REPOSITORY.Clase
{

    public class RSeleccion : BaseConexion, ISeleccion
    {
        private readonly ITI001 tI001;
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;

        public RSeleccion(ITI001 tI001, ITI002 tI002, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI002 = tI002;
            this.tI0021 = tI0021;
        }
        #region tRANSACCIONES
        public bool Guardar(VSeleccion vSeleccion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Seleccion seleccion;
                    if (id > 0)
                    {
                        seleccion = db.Seleccion.Where(a => a.Id == idAux).FirstOrDefault();
                        if (seleccion == null)
                            throw new Exception("No existe la seleccion con id " + idAux);
                    }
                    else
                    {
                        seleccion = new Seleccion();
                        db.Seleccion.Add(seleccion);
                    }
                    seleccion.IdAlmacen = vSeleccion.IdAlmacen;
                    seleccion.IdCompraIng = vSeleccion.IdCompraIng;
                    seleccion.Estado = vSeleccion.Estado;
                    seleccion.FechaReg = vSeleccion.FechaReg;
                    seleccion.Cantidad = vSeleccion.Cantidad ;
                    seleccion.Precio = vSeleccion.Precio;
                    seleccion.Total = vSeleccion.Total;                  
                    seleccion.Fecha = vSeleccion.Fecha;
                    seleccion.Hora = vSeleccion.Hora;
                    seleccion.Usuario = vSeleccion.Usuario;
                    seleccion.Merma = vSeleccion.Merma;
                    seleccion.MermaPor = vSeleccion.MermaPorcentaje;
                    seleccion.ManchadoPor = vSeleccion.ManchadoPorcentaje;
                    seleccion.PicadoPor = vSeleccion.PicadoPorcentaje;
                    seleccion.ManchadoAbsoluto = vSeleccion.ManchadoAbsoluto;
                    seleccion.PicadoAbsoluto = vSeleccion.PicadoAbsoluto;
                    db.SaveChanges();
                    id = seleccion.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdSeleccion, int estado,ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    #region Validacion
                    DateTime fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";
                    var seleccion = db.Seleccion.Where(c => c.Id.Equals(IdSeleccion)).FirstOrDefault();
                    var seleccion_01 = db.Seleccion_01.Where(c => c.IdSeleccion.Equals(IdSeleccion)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in seleccion_01)
                    {
                        var StockActual = this.tI001.TraerStockActual(item.IdProducto, seleccion.IdAlmacen, lote, fechaVen);
                        if (StockActual < item.Cantidad)
                        {
                            var producto = db.Producto.Where(p => p.Id == item.IdProducto).Select(p => p.Descrip).FirstOrDefault();
                            lMensaje.Add("No existe stock actual suficiente para el producto: " + producto);
                        }                    
                    }
                    if (lMensaje.Count > 0)
                    {
                        var mensaje = "";
                        foreach (var item in lMensaje)
                        {
                            mensaje = mensaje + "- " + item + "\n";
                        }
                        return false;
                    }
                    #endregion
                    #region Actualiza TI001, TI002 Y TI0021
                    //Actualizar saldo, Eliminar Movimientos
                    foreach (var i in seleccion_01)
                    {
                        if (i.Cantidad > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProducto, seleccion.IdAlmacen, lote, fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProducto,
                                                               seleccion.IdAlmacen,
                                                               EnAccionEnInventario.Descontar,
                                                               Convert.ToDecimal(i.Cantidad),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.SELECCION_INGRESO);
                            }
                        }
                        
                    }

                    //INGRESA EL STOCK DE LA COMPRA
                    var idCompra = db.CompraIng_01
                                                .Where(c => c.IdCompra == db.Seleccion
                                                                                        .Where(s => s.Id == IdSeleccion)
                                                                                        .Select(d => d.IdCompraIng)
                                                                                        .FirstOrDefault())
                                                                                        .Select(a => a.IdCompra)
                                                                                        .FirstOrDefault();
                    var compraing_01 = db.CompraIng_01
                                            .Where(c => c.IdCompra == idCompra).ToList();
                    var compraIng = db.CompraIng
                                            .Where(c => c.Id.Equals(idCompra))
                                            .FirstOrDefault();
                    foreach (var i in compraing_01)
                    {
                        if (i.TotalCant > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProduc,
                                                      compraIng.IdAlmacen,
                                                      lote,
                                                      fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProduc,
                                                               compraIng.IdAlmacen,
                                                               EnAccionEnInventario.Incrementar,
                                                               Convert.ToDecimal(i.TotalCant),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRA_SALIDA);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRA_SALIDA);
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRA_SALIDA);
                                //ELIMINA EL MOVIMIENTO
                                this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRA_SALIDA);
                            }
                        }                                                                
                    }
                    #endregion
                    compraIng.Estado = (int)ENEstado.GUARDADO;
                    db.CompraIng.Attach(compraIng);
                    db.Entry(compraIng).State = EntityState.Modified;

                    seleccion.Estado = estado;
                    db.Seleccion.Attach(seleccion);                   
                    db.Entry(seleccion).State = EntityState.Modified;
                  
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
        #region CONSULTAS
        /******** VALOR/REGISTRO ÚNICO *********/
        public VSeleccionLista TraerSeleccion(int idSeleccion)
        {
            try
            {

                using (var db = GetEsquema())
                {
                    var lista = db.Seleccion
                        .Where(b => b.Id == idSeleccion)
                     .Select(a => new VSeleccionLista
                     {
                         Id = a.Id,
                         IdCompraIng = a.IdCompraIng,
                         IdAlmacen = a.IdAlmacen,
                         Granja = a.CompraIng.NumNota,
                         FechaReg = a.FechaReg,
                         FechaEntrega = a.CompraIng.FechaEnt,
                         FechaRecepcion = a.CompraIng.FechaRec,
                         Proveedor = a.CompraIng.Proveed.Descrip,                        
                         Placa = a.CompraIng.Placa,
                         Tipo = a.CompraIng.Tipo,                        
                         Edad = a.CompraIng.EdadSemana,
                         Cantidad = a.Cantidad,
                         Total = a.Total,
                         Merma = a.Merma,
                         Fecha = a.Fecha,
                         Hora = a.Hora,
                         Usuario = a.Usuario,
                         AlmacenCompraId = a.CompraIng.IdAlmacen,
                    
                }).FirstOrDefault();
                    return lista;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        
        public List<VSeleccionEncabezado> TraerSelecciones(int usuarioId)
        {
            try
            {

                using (var db = GetEsquema())
                {
                    var lista = db.Seleccion
                        .OrderBy(x=> x.Id)
                        .Where(x=> x.Estado != (int)ENEstado.ELIMINAR &&
                               (db.Usuario_01
                               .Where(b => b.IdUsuario == usuarioId &&
                                           b.Acceso == true)
                               .Select(d => d.IdAlmacen)).Contains(x.IdAlmacen))
                     .Select(a => new VSeleccionEncabezado
                     {
                         Id = a.Id,
                         IdCompraIng = a.IdCompraIng,                      
                         Granja = a.CompraIng.NumNota,
                         FechaReg = a.FechaReg,                     
                         FechaRecepcion = a.CompraIng.FechaRec,
                         Proveedor = a.CompraIng.Proveed.Descrip,                         
                         TipoCategoria = db.Libreria.FirstOrDefault(x => x.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO &&
                                                                                x.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO2 &&
                                                                                x.IdLibrer == a.CompraIng.Tipo).Descrip,
                         Merma = a.Merma,
                         TotalRecepcion = a.CompraIng.TotalUnidades,
                         Cantidad = a.Cantidad,                                    
                         MermaPorcentaje =a.MermaPor,
                         ManchadoPorcentaje = a.ManchadoPor,
                         PicadoPorcentaje = a.PicadoPor,  
                         Dias = SqlFunctions.DateDiff("DAY",a.CompraIng.FechaRec, a.FechaReg),
                         Fecha = a.Fecha,
                         Hora = a.Hora,
                         Usuario = a.Usuario,
                         IdSkype = a.CompraIng.Proveed.IdSpyre,
                         Devolucion = a.CompraIng.Devolucion == 1 ? "NO" : "SI",
                         TipoProveedor = db.Libreria.FirstOrDefault(x => x.IdGrupo == (int)ENEstaticosGrupo.PROVEEDOR &&
                                                                                x.IdOrden == (int)ENEstaticosOrden.PROVEEDOR_TIPO &&
                                                                                x.IdLibrer == a.CompraIng.Proveed.TipoProve).Descrip,
                         ManchadoAbsoluto = a.ManchadoAbsoluto,
                         PicadoAbsoluto = a.PicadoAbsoluto,
                     }).ToList();
                    return lista;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ObtenerUltimoAlmacen(int IdAlmacenCompra ,int usuarioId)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var lCompraIng = db.CompraIng.Where(x => x.IdAlmacen == IdAlmacenCompra &&
                                                            db.Seleccion.Select(a => a.IdCompraIng).Contains(x.Id) &&
                                                                 x.Estado != (int)ENEstado.ELIMINAR).ToList();
                    //var lista = db.Seleccion.Where(x => lCompraIng.Select(a => a.Id).Contains(x.IdCompraIng)).ToList();

                    int ultimoIdCompra = lCompraIng.Count() == 0 ? 0 : lCompraIng.Max(x => x.Id);

                    int idAlmacenSeleccion = 0;
                    if (ultimoIdCompra != 0)
                        idAlmacenSeleccion = db.Seleccion.FirstOrDefault(x => x.IdCompraIng == ultimoIdCompra).IdAlmacen;
               
                    return idAlmacenSeleccion;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTES ***********/
        public List<RSeleccionNota> NotaSeleccion(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_SeleccionNota
                                      where a.Id.Equals(Id)
                                      select new RSeleccionNota
                                      {
                                          Id = a.Id,
                                          NumNota = a.NumNota,
                                          FechaReg = a.FechaReg,
                                          FechaRec = a.FechaRec,
                                          Proveedor = a.Proveedor,
                                          IdSpyre = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          DescripcionPlaca =a.DescripcionPlaca,
                                          IdDetalle = a.IdDetalle,
                                          IdProducto = a.IdProducto,
                                          Producto = a.Producto,
                                          Cantidad = a.Cantidad,
                                          Porcen =a.Porcen,
                                          Precio =a.Precio,
                                          Total = a.Total,
                                          Merma = a.Merma,
                                          MermaPorcentaje = a.MermaPorcentaje                                    
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       

        public DataTable ReporteHistoricoSeleccion(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
               sb.Append(@"SELECT        Prov.IdSpyre, Prov.Descrip as Proveedor,CONVERT(varchar(10), Sel.FechaReg,23) AS fecha, Sel.Id AS ingreso,  Cing.NumNota as NotaPedido, Cing.Id as Trans, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 380)), 0) AS Superc, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 381)), 0) AS Especialc, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 382)), 0) AS Primerac, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 383)), 0) AS Segundac, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 384)), 0) AS Tercerac, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 385)), 0) AS Cuartac, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 386)), 0) AS Quintac, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 387)), 0) AS Picadoc, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 388)), 0) AS manchadoc, CAST(Sel.Merma AS int) AS Merma, 0 AS Media_doc2, 0 AS Docena2, 0 AS Band2_12, 0 AS Map2_6, 0 AS Map2_10, 0 AS Map2_12, 0 AS Map2_15, 
                                     0 AS Map2_18, 0 AS Map2_20, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 399)), 0) AS Vit_Super, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 400)), 0) AS Vit_Especial, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 401)), 0) AS Vit_Primera, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 402)), 0) AS Vit_Segunda, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 403)), 0) AS Vit_Tercera, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 404)), 0) AS Vit_Cuarta, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 405)), 0) AS Vit_Quinta, 0 AS Vit_Map2_6, 0 AS Vit_Map2_10, 0 AS Vit_Map2_12, 0 AS Vit_Map2_15, 0 AS Vit_Map2_18, 0 AS Vit_Map2_20, 0 AS Vit_Media_Docena, 0 AS Vit_Docena2, 0 AS Vit_Band2_12, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 415)), 0) AS Omega_3, 0 AS Omg_Med_Doc2, 0 AS Omg_Docena2, 0 AS Omg_Band2_12, 0 AS Omg_Map2_10, 0 AS Omg_Map2_20, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 421)), 0) AS Blanco_Super, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 422)), 0) AS Blanco_Esp, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 423)), 0) AS Blanco_Primera, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 424)), 0) AS Blanco_Segunda, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 425)), 0) AS Blanco_Tercera, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 426)), 0) AS Blanco_Cuarta, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 427)), 0) AS Blanco_Quinta, 0 AS Blanco_Band2_12, 0 AS Blanco_Map2_20, ISNULL
                                         ((SELECT        CAST(Cantidad AS int) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 430)), 0) AS Codorniz, 0 AS Color_Red50, 0 AS Sedem_Maple1_30, 0 AS Sedem_Map3_18X2, 0 AS Sedem_Vit_Band2_12X2, 0 AS Sedem_Omg_Nut_Map2_10X2, 
                                     0 AS Sedem_Maple3_30, 0 AS Sedem_Map2_12X2, 0 AS Vacio1, 0 AS Vacio2, CAST(Sel.Cantidad AS int) AS Total, Lib.Descrip AS TipoRecepción,CONVERT(varchar(10), Cing.FechaRec,23)  AS FechaNota, Cing.Obser AS Alcaracion, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 380)), 0) AS Superp, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 381)), 0) AS Especialp, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 382)), 0) AS Primerap, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 383)), 0) AS Segundap, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 384)), 0) AS Tercerap, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 385)), 0) AS Cuartap, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 386)), 0) AS Quintap, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 387)), 0) AS Picadop, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 388)), 0) AS manchadop, '0.00' AS Mermap, '0.00' AS Media_doc2_P, '0.00' AS Docena2_P, '0.00' AS Band2_12_P, '0.00' AS Map2_6_P, '0.00' AS Map2_10_P, 
                                     '0.00' AS Map2_12_P, '0.00' AS Map2_15_P, '0.00' AS Map2_18_P, '0.00' AS Map2_20_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 399)), 0) AS Vit_SuperP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 400)), 0) AS Vit_EspecialP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 401)), 0) AS Vit_PrimeraP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 402)), 0) AS Vit_SegundaP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 403)), 0) AS Vit_TerceraP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 404)), 0) AS Vit_CuartaP, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 405)), 0) AS Vit_QuintaP, 0 AS Vit_Map2_6_P, 0 AS Vit_Map2_10_P, 0 AS Vit_Map2_12_P, 0 AS Vit_Map2_15_P, 0 AS Vit_Map2_18_P, 0 AS Vit_Map2_20_P, 0 AS Vit_Media_Docena_P, 0 AS Vit_Docena2_P, 0 AS Vit_Band2_12_P,
                                     0 AS Vit_Media_Doc2_P, 0 AS vit_Docena2_P, 0 AS vit_Band2_12_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 415)), 0) AS Omega_3_P, 0 AS Omg_Med_Doc2_P, 0 AS Omg_Docena2_P, 0 AS Omg_Band2_12_P, 0 AS Omg_Map2_10_P, 0 AS Omg_Map2_20_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 421)), 0) AS Blanco_Super_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 422)), 0) AS Blanco_Esp_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 423)), 0) AS Blanco_Primera_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 424)), 0) AS Blanco_Segunda_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 425)), 0) AS Blanco_Tercera_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 426)), 0) AS Blanco_Cuarta_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 427)), 0) AS Blanco_Quinta_P, 0 AS Blanco_Band2_12_P, 0 AS Blanco_Map2_20_P, ISNULL
                                         ((SELECT        CAST(Precio AS decimal(18, 2)) AS Expr1
                                             FROM            COM.Seleccion_01 AS sel1
                                             WHERE        (IdSeleccion = Sel.Id) AND (IdProducto = 430)), 0) AS Codorniz_P, 0 AS Color_Red50_P, 0 AS Sedem_Maple1_30_P, 0 AS Sedem_Map3_18X2_P, 0 AS Sedem_Vit_Band2_12X2_P, 
                                     0 AS Sedem_Omg_Nut_Map2_10X2_P, 0 AS Sedem_Maple3_30_P, 0 AS Sedem_Map2_12X2_P, 0 AS Vacio3, 0 AS Vacio4, Sel.Precio AS Precio_Promedio, Sel.Total AS Total_Bs,
                                         (SELECT        CAST(SUM(sel1.Cantidad * Prod.Peso) / SUM(sel1.Cantidad) AS decimal(18, 2)) AS Peso
                                           FROM            COM.Seleccion_01 AS sel1 INNER JOIN
                                                                     REG.Producto AS Prod ON sel1.IdProducto = Prod.Id
                                           WHERE        (sel1.IdSeleccion = Sel.Id)) AS Peso_Promedio
            FROM            COM.Seleccion AS Sel INNER JOIN
                                     COM.CompraIng AS Cing ON Sel.IdCompraIng = Cing.Id INNER JOIN
                                     COM.Proveed AS Prov ON Cing.IdProvee = Prov.Id INNER JOIN
                                     ADM.Libreria AS Lib ON Prov.TipoProve = Lib.IdLibrer
            WHERE        (Lib.IdGrupo = 2) AND (Lib.IdOrden = 2)  AND Sel.Estado <> -1 AND   ");
                List<SqlParameter> lPars = new List<SqlParameter>();              
                
                if (fechaDesde.HasValue && fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("Sel.FechaReg between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime, 0, fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime, 0, fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59)));
                }
                else if (fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("Sel.FechaReg >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fechaDesde.Value));
                }
                else if (fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("Sel.FechaReg <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fechaHasta.Value.AddDays(1)));
                }
                
                sb.Length -= 7;
                sb.Append(@" ORDER BY sel.FechaReg Asc");
                return tabla = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
