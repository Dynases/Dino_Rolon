using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.CompraIngreso.View;
using DATA.EntityDataModel.DiAvi;
using System.Data;
using UTILITY.Enum.EnEstado;
using System.Data.Entity;
using ENTITY.inv.TI001.VIew;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;

namespace REPOSITORY.Clase
{
    public class RCompraIngreso : BaseConexion, ICompraIngreso
    {
        private readonly ITI001 tI001;
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;

        public RCompraIngreso(ITI001 tI001, ITI002 tI002, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI002 = tI002;
            this.tI0021 = tI0021;
        }
        #region Transacciones
        public bool Guardar(VCompraIngresoLista vCompraIngreso, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    CompraIng CompraIngreso;
                    if (id > 0)
                    {
                        CompraIngreso = db.CompraIng.Where(a => a.Id == idAux).FirstOrDefault();
                        if (CompraIngreso == null)
                            throw new Exception("No existe la compra con id " + idAux);
                    }
                    else
                    {
                        CompraIngreso = new CompraIng();
                        db.CompraIng.Add(CompraIngreso);
                    }

                    CompraIngreso.IdAlmacen = vCompraIngreso.IdAlmacen;
                    CompraIngreso.IdProvee = vCompraIngreso.IdProvee;
                    CompraIngreso.Estado = vCompraIngreso.estado;
                    CompraIngreso.NumNota = vCompraIngreso.NumNota;
                    CompraIngreso.FechaEnt = vCompraIngreso.FechaEnt;
                    CompraIngreso.FechaRec = vCompraIngreso.FechaRec;          
                    CompraIngreso.Placa = vCompraIngreso.Placa;
                    CompraIngreso.EdadSemana = vCompraIngreso.CantidadSemanas;
                    CompraIngreso.Tipo = vCompraIngreso.Tipo;
                    CompraIngreso.Obser = vCompraIngreso.Observacion;
                    CompraIngreso.Entregado = vCompraIngreso.Entregado;
                    CompraIngreso.Recibido = vCompraIngreso.Recibido;
                    CompraIngreso.TotalVendido = vCompraIngreso.TotalVendido;
                    CompraIngreso.TotalRecibido = vCompraIngreso.TotalRecibido;
                    CompraIngreso.Total = vCompraIngreso.Total;
                    CompraIngreso.TipoCompra = vCompraIngreso.TipoCompra;
                    CompraIngreso.Fecha = vCompraIngreso.Fecha;
                    CompraIngreso.Hora = vCompraIngreso.Hora;
                    CompraIngreso.Usuario = vCompraIngreso.Usuario;
                    db.SaveChanges();
                    id = CompraIngreso.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdCompraIngreso, int estado,ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    DateTime? fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";
                    var compraIng = db.CompraIng.Where(c => c.Id.Equals(IdCompraIngreso)).FirstOrDefault();
                    var compraing_01 = db.CompraIng_01.Where(c => c.IdCompra.Equals(IdCompraIngreso)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in compraing_01)
                    {                        
                        var StockActual = this.tI001.StockActual(item.IdProduc.ToString(), compraIng.IdAlmacen,lote , fechaVen);                                           
                        if (StockActual < item.TotalCant)
                        {
                            var producto = db.Producto.Where(p => p.Id == item.IdProduc).Select(p => p.Descrip).FirstOrDefault();
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
                    //Actualizar saldo, Eliminar Movimientos
                    foreach (var i in compraing_01)
                    {
                        if (this.tI001.ExisteProducto(i.IdProduc.ToString(), compraIng.IdAlmacen, lote, fechaVen))
                        {
                            if (!this.tI001.ActualizarInventario(i.IdProduc.ToString(),
                                                           compraIng.IdAlmacen,
                                                           EnAccionEnInventario.Descontar,
                                                           Convert.ToDecimal(i.TotalCant),
                                                           lote,
                                                           fechaVen))
                            {
                                return false;
                            }
                            //ELIMINA EL DETALLE DE MOVIMIENTO
                            this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRA_INGRES0);
                            //ELIMINA EL MOVIMIENTO
                            this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRA_INGRES0);
                        }
                        else
                        {
                            //ELIMINA EL DETALLE DE MOVIMIENTO
                            this.tI0021.Eliminar(i.Id, (int)ENConcepto.COMPRA_INGRES0);
                            //ELIMINA EL MOVIMIENTO
                            this.tI002.Eliminar(i.Id, (int)ENConcepto.COMPRA_INGRES0);
                        }
                    }
                    compraIng.Estado = estado;
                    db.CompraIng.Attach(compraIng);
                    db.Entry(compraIng).State = EntityState.Modified;                   
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
        #region Consulta
        public List<VTI001> ListarStock(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compraIng = db.TI001.Where(c => c.iccprod.Equals(IdProducto)).ToList();
                    var listResult = (from a in db.TI001
                                      where a.iccprod.Equals(IdProducto) && a.iccven > 0
                                      select new VTI001
                                      {
                                          id = a.id,
                                          IdAlmacen = a.icalm,
                                          IdProducto = a.iccprod,
                                          Cantidad = a.iccven,
                                          Unidad = a.icuven,
                                          Lote = a.iclot,
                                          FechaVen = a.icfven
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.CompraIng
                                      join c in db.Proveed on 
                                       new
                                       {
                                           idProve = a.IdProvee                                          
                                       }
                                       equals
                                       new
                                       {
                                           idProve = c.Id                                          
                                       }
                                       where  a.Estado != (int)ENEstado.ELIMINAR
                                      select new VCompraIngreso
                                      {
                                          Id = a.Id,
                                          Proveedor = c.Descrip,
                                          FechaEnt = a.FechaEnt,
                                          FechaRec = a.FechaRec,
                                          Entregado = a.Entregado,
                                          Total = a.Total,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngresoNota> ListarNotaXId(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.V_NotaCompraIngreso
                                      where a.Id.Equals(Id)
                                      select new VCompraIngresoNota
                                      {
                                          Id = a.Id,
                                          NumNota=a.NumNota,
                                          FechaRec = a.FechaRec,
                                          FechaEnt = a.FechaEnt,
                                          Proveedor= a.Proveedor,
                                          IdSkype= a.IdSpyre,
                                          MarcaTipo =a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngresoLista> ListarXId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.CompraIng
                                      join c in db.Proveed on
                                      new
                                      {
                                          idProve = a.IdProvee
                                      }
                                      equals
                                      new
                                      {
                                          idProve = c.Id
                                      }
                                      join b in db.Proveed_01 on
                                         new
                                         {
                                             idProve = c.Id
                                         }
                                         equals
                                         new
                                         {
                                             idProve = b.IdProveed
                                         }
                                      where a.Id.Equals(id)
                                      select new VCompraIngresoLista
                                      {
                                          Id = a.Id,
                                          IdAlmacen = a.IdAlmacen,
                                          IdProvee = a.IdProvee,                                         
                                          CantidadSemanas = a.EdadSemana,
                                          Proveedor = c.Descrip,
                                          NumNota = a.NumNota,
                                          FechaEnt = a.FechaEnt,
                                          FechaRec = a.FechaRec,
                                          Placa = a.Placa,
                                          Tipo = a.Tipo,
                                          Observacion = a.Obser,
                                          Entregado = a.Entregado,
                                          Recibido = a.Recibido,
                                          Total = a.Total,
                                          TotalRecibido= a.TotalRecibido,
                                          TotalVendido =a.TotalVendido,
                                          TipoCompra = a.TipoCompra,
                                         estado= a.Estado,
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                DataTable tabla = new DataTable();
                string consulta = @"SELECT	
	                                a.Id,
	                                a.NumNota,
	                                a.FechaEnt,
	                                a.FechaRec,
	                                a.Placa,
	                                a.IdProvee,
	                                b.Descrip as Proveedor,
	                                a.Tipo,
	                                a.EdadSemana,
	                                a.IdAlmacen,
                                    a.TipoCompra
                                FROM 
	                                COM.CompraIng a JOIN
	                                COM.Proveed b ON b.Id = a.IdProvee 
                                WHERE
                                    a.Estado <> 3 AND a.Estado <> -1";
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnSeleccion(int IdCompraIngreso)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.CompraIng
                                     join b in db.Seleccion on a.Id equals b.IdCompraIng
                                     where a.Id.Equals(IdCompraIngreso) && b.Estado != (int)ENEstado.ELIMINAR
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
