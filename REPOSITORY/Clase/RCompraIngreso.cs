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
using System.Data.SqlClient;
using System.Data.Common;

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
                    CompraIngreso.CantidadCaja = vCompraIngreso.CantidadCaja;
                    CompraIngreso.CantidadGrupo = vCompraIngreso.CantidadGrupo;
                    CompraIngreso.Fecha = vCompraIngreso.Fecha;
                    CompraIngreso.Hora = vCompraIngreso.Hora;
                    CompraIngreso.Usuario = vCompraIngreso.Usuario;
                    CompraIngreso.CompraAntiguaFecha = vCompraIngreso.CompraAntiguaFecha;
                    CompraIngreso.TotalMaple = vCompraIngreso.TotalMaple;
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
                    DateTime fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";
                    var compraIng = db.CompraIng.Where(c => c.Id.Equals(IdCompraIngreso)).FirstOrDefault();
                    var compraing_01 = db.CompraIng_01.Where(c => c.IdCompra.Equals(IdCompraIngreso)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in compraing_01)
                    {                        
                        var StockActual = this.tI001.TraerStockActual(item.IdProduc, compraIng.IdAlmacen,lote , fechaVen);                                           
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
                        if (i.TotalCant > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProduc, compraIng.IdAlmacen, lote, fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProduc,
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
        /******** VALOR/REGISTRO ÚNICO *********/
   
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
                                         CantidadCaja = a.CantidadCaja,
                                         CantidadGrupo = a.CantidadGrupo,
                                         CompraAntiguaFecha = a.CompraAntiguaFecha,
                                         TotalMaple = a.TotalMaple
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public DataTable BuscarCompraIngreso(int estado)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append( @"SELECT	
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
                                    a.Estado <> -1 AND   ");
                if (estado == (int)ENEstado.COMPLETADO)
                {
                    sb.AppendFormat("a.Estado <> 3 AND   ");
                }
                sb.Length -= 7;
                return tabla = BD.EjecutarConsulta(sb.ToString()).Tables[0];
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
                                      where a.Estado != (int)ENEstado.ELIMINAR
                                      select new VCompraIngreso
                                      {
                                          Id = a.Id,
                                          NotaProveedor=a.NumNota,
                                          Proveedor = c.Descrip,
                                          FechaEnt = a.FechaEnt,
                                          FechaRec = a.FechaRec,
                                          Entregado = a.Entregado,
                                          TotalMaple = a.TotalMaple,
                                          TotalUnidades = (from y in db.CompraIng_01
                                                           where y.IdCompra == a.Id
                                                           select y.TotalCant).Sum(),
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

        /********** REPORTES ***********/
        public DataTable ReporteCompraIngreso(DateTime? fechaDesde, DateTime? fechaHasta, int estado)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT	
                                a.Id,
                                a.NumNota,
                                a.FechaRec,
                                b.Descrip as Proveedor,
                                a.IdAlmacen,
                                c.Descrip as Almacen,
                                SUM(e.TotalCant) AS TotalCantidad,
                                a.TotalMaple,
                                DATEDIFF(Day, a.FechaRec, GETDATE()) Dias
                            FROM 
                                COM.CompraIng a JOIN
                                COM.Proveed b ON b.Id = a.IdProvee JOIN
                                INV.Almacen c ON c.Id = a.IdAlmacen JOIN
                                COM.CompraIng_01 e ON e.IdCompra = a.Id
                                WHERE
                                a.Estado <> -1  AND   ");
                List<SqlParameter> lPars = new List<SqlParameter>();
                if (fechaDesde.HasValue && fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("a.FechaRec between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime,0, fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime,0, fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59) ));
                }
                else if (fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fechaDesde.Value));
                }
                else if (fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fechaHasta.Value.AddDays(1)));
                }
                if (estado == (int)ENEstado.TODOS)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.Estado IN ({0},{1}) AND   ", (int)ENEstado.GUARDADO, (int)ENEstado.COMPLETADO));
                }
                else
                {
                    sb.Append(string.Format("a.Estado IN ({0}) AND   ", estado));
                }

                sb.Length -= 7;
                sb.Append(@"GROUP BY 
                                a.Id, a.NumNota, a.FechaRec, b.Descrip, a.IdAlmacen, c.Descrip, a.TotalMaple
                            ORDER BY
                                a.FechaRec ASC");
                return tabla = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngresoNota> NotaCompraIngreso(int Id)
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
                                          NumNota = a.NumNota,
                                          FechaRec = a.FechaRec,
                                          FechaEnt = a.FechaEnt,
                                          Proveedor = a.Proveedor,
                                          IdSkype = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          TotalMaple = a.TotalMaple
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoNota> NotaCompraIngresoDevolucion(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_CompraIngresoDevolucion
                                      where a.Id.Equals(Id)
                                      select new VCompraIngresoNota
                                      {
                                          Id = a.Id,
                                          NumNota = a.NumNota,
                                          FechaRec = a.FechaRec,
                                          FechaEnt = a.FechaEnt,
                                          Proveedor = a.Proveedor,
                                          IdSkype = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          TotalMaple = a.TotalMaple
                                      }).ToList();
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
