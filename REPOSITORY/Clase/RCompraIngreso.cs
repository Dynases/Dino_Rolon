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
using UTILITY.Enum.EnEstaticos;
using ENTITY.com.CompraIngreso.Filter;

namespace REPOSITORY.Clase
{
    public class RCompraIngreso : BaseConexion, ICompraIngreso
    {   
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
        public bool ModificarEstado(int IdCompraIngreso, int estado)
        {
            try
            {
                using (var db = GetEsquema())
                {                  
                    var compraIng = db.CompraIng.Where(c => c.Id.Equals(IdCompraIngreso)).FirstOrDefault();                    
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
   
        public VCompraIngresoLista TraerCompraIngreso(int id)
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

        public List<VCompraIngreso> TraerComprasIngreso()
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
                                          NotaProveedor = a.NumNota,
                                          Proveedor = c.Descrip,
                                          FechaEnt = a.FechaEnt,
                                          FechaRec = a.FechaRec,
                                          Entregado = a.Entregado,
                                          TotalMaple = a.TotalMaple,
                                          TotalUnidades = a.TotalUnidades,
                                          Total = a.Total,
                                          TipoCategoria = db.Libreria.FirstOrDefault(x => x.IdGrupo == (int)ENEstaticosGrupo.PRODUCTO &&
                                                                                x.IdOrden == (int)ENEstaticosOrden.PRODUCTO_GRUPO2 &&
                                                                                x.IdLibrer == a.Tipo).Descrip,
                                          TipoCompra = a.TipoCompra == 1 ? "CON SELECCIÓN" : "SIN SELECCIÓN",
                                          Devolucion = a.CompraIng_03.Count(s => s.IdCompra == a.Id) == 0 ? "NO" : "SI",
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

        public List<VCompraIngresoCombo> TraerCompraIngresoCombo()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = db.CompraIng
                                    .Where(a=> a.Estado != (int)ENEstado.COMPLETADO &&
                                               a.Estado != (int)ENEstado.ELIMINAR)
                      .Select(v => new VCompraIngresoCombo
                      {
                          Id = v.Id,
                          NumGranja = v.NumNota,
                          Proveedor = v.Proveed.Descrip                                        
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
        public DataTable ReporteCompraIngreso(FCompraIngreso fcompraIngreso)
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
                if (fcompraIngreso.Id != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.id IN ({0}) AND   ", fcompraIngreso.Id));
                }
                if (fcompraIngreso.IdProveedor != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("c.id IN ({0}) AND   ", fcompraIngreso.IdProveedor));
                }
                if (fcompraIngreso.TipoCategoria != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("A.Tipo IN ({0}) AND   ", fcompraIngreso.TipoCategoria));
                }
                if (fcompraIngreso.fechaDesde.HasValue && fcompraIngreso.fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("a.FechaRec between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime, 0, fcompraIngreso.fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime, 0, fcompraIngreso.fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59)));
                }
                else if (fcompraIngreso.fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fcompraIngreso.fechaDesde.Value));
                }
                else if (fcompraIngreso.fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fcompraIngreso.fechaHasta.Value.AddDays(1)));
                }
                if (fcompraIngreso.estadoCompra == (int)ENEstado.TODOS)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.Estado IN ({0},{1}) AND   ", (int)ENEstado.GUARDADO, (int)ENEstado.COMPLETADO));
                }
                else
                {
                    sb.Append(string.Format("a.Estado IN ({0}) AND   ", fcompraIngreso.estadoCompra));
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
                                          IdSpyre = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          TotalMaple = (int)a.totalMaple
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
                                          IdSpyre = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = a.Total,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          TotalMaple = (int)a.totalMaple
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoNota> NotaCompraIngresoResultado(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_CompraIngresoResultado
                                      where a.Id.Equals(Id)
                                      select new VCompraIngresoNota
                                      {
                                          Id = a.Id,
                                          NumNota = a.NumNota,
                                          FechaRec = a.FechaRec,
                                          FechaEnt = a.FechaEnt,
                                          Proveedor = a.Proveedor,
                                          IdSpyre = a.IdSpyre,
                                          MarcaTipo = a.MarcaTipo,
                                          IdProducto = a.IdProduc,
                                          Producto = a.Producto,
                                          TotalCant = (decimal)a.TotalCant,
                                          PrecioCost = a.PrecioCost,
                                          Total = (decimal)a.Total,
                                          Entregado = a.Entregado,
                                          DescripcionRecibido = a.DescripcionRecibido,
                                          TotalMaple = (int)a.totalMaple
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReporteCriterioCompraIngreso(FCompraIngreso fcompraIngreso)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT
                        --ENCABEZADO 
	                        A.Id,
	                        A.NumNota,
	                        A.FechaRec,
	                        A.FechaEnt,
	                        H.Descrip as Almacen,
	                        a.TotalMaple TotalMapleEncabezado,
	                        a.Total as TotalEncabezado,
                        --PLACA
	                        A.Entregado,
	                        F.Descrip AS Placa,
                        --Proveedor
	                        c.id as IdProveedor,
	                        c.Descrip  as Proveedor,
                        --Tipo Categoria
	                        E.Descrip  AS TipoCategoria,
	                        i.Descrip as RecibidoPor,
                        --Tipo Compra
	                        a.TipoCompra,
	                        (IIF(A.TipoCompra = 1,'CON SELECCION','SIN SELECCION')) AS TipoCompraDescripcion,
                        --Detalle
	                        b.Id as IdDetalle,
	                        d.Descrip as Producto,
	                        b.Caja,
	                        b.Grupo,
	                        b.Maple,
	                        b.Cantidad,
	                        b.TotalCant,
	                        b.PrecioCost,
	                        b.Total,
	                        b.TotalMaple as TotalMapleDetalle
                        FROM
	                        COM.CompraIng a JOIN
	                        COM.CompraIng_01 b ON A.Id = B.IdCompra JOIN
	                        COM.Proveed c ON A.IdProvee = C.Id JOIN
	                        REG.Producto D ON B.IdProduc = D.Id JOIN
	                        ADM.Libreria e ON e.IdGrupo = 3 AND E.IdOrden = 2 AND E.IdLibrer= A.Tipo JOIN
	                        ADM.Libreria F ON F.IdGrupo = 4 AND F.IdOrden = 1 AND F.IdLibrer= A.Placa JOIN
	                        COM.CompraIng_02 G ON A.Placa = G.IdLibreria JOIN
	                        INV.Almacen H ON A.IdAlmacen = H.Id JOIN
	                        ADM.Libreria I ON I.IdGrupo = 4 AND I.IdOrden = 4 AND I.IdLibrer= A.Recibido
                        WHERE 
	                        A.TipoCompra = 2 AND A.Estado <> -1  AND   ");
                List<SqlParameter> lPars = new List<SqlParameter>();
                if (fcompraIngreso.Id !=  0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.id IN ({0}) AND   ", fcompraIngreso.Id));
                }
                if (fcompraIngreso.IdProveedor != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("c.id IN ({0}) AND   ", fcompraIngreso.IdProveedor));
                }
                if (fcompraIngreso.TipoCategoria != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("A.Tipo IN ({0}) AND   ", fcompraIngreso.TipoCategoria));
                }
                if (fcompraIngreso.fechaDesde.HasValue && fcompraIngreso.fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("a.FechaRec between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime, 0, fcompraIngreso.fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime, 0, fcompraIngreso.fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59)));
                }
                else if (fcompraIngreso.fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fcompraIngreso.fechaDesde.Value));
                }
                else if (fcompraIngreso.fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fcompraIngreso.fechaHasta.Value.AddDays(1)));
                }
                if (fcompraIngreso.estadoCompra == (int)ENEstado.TODOS)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.Estado IN ({0},{1}) AND   ", (int)ENEstado.GUARDADO, (int)ENEstado.COMPLETADO));
                }
                else
                {
                    sb.Append(string.Format("a.Estado IN ({0}) AND   ", fcompraIngreso.estadoCompra));
                }
                sb.Length -= 7;
                sb.Append(@"ORDER BY a.Id ASC");
                return tabla = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoDevolucion(FCompraIngreso fcompraIngreso)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT
                        --ENCABEZADO 
	                        A.Id,
	                        A.NumNota,
	                        A.FechaRec,
	                        A.FechaEnt,
	                        H.Descrip as Almacen,
	                        a.TotalMaple TotalMapleEncabezado,
	                        a.Total as TotalEncabezado,
                        --PLACA
	                        A.Entregado,
	                        F.Descrip AS Placa,
                        --Proveedor
	                        c.id as IdProveedor,
	                        c.Descrip  as Proveedor,
                        --Tipo Categoria
	                        E.Descrip  AS TipoCategoria,
	                        i.Descrip as RecibidoPor,
                        --Tipo Compra
	                        a.TipoCompra,
	                        (IIF(A.TipoCompra = 1,'CON SELECCION','SIN SELECCION')) AS TipoCompraDescripcion,
                        --Detalle
	                        b.Id as IdDetalle,
	                        d.Descrip as Producto,
	                        b.Caja,
	                        b.Grupo,
	                        b.Maple,
	                        b.Cantidad,
	                        b.TotalCant,
	                        b.PrecioCost,
	                        b.Total,
	                        b.TotalMaple as TotalMapleDetalle
                        FROM
	                        COM.CompraIng a JOIN
	                        COM.CompraIng_03 b ON A.Id = B.IdCompra JOIN
	                        COM.Proveed c ON A.IdProvee = C.Id JOIN
	                        REG.Producto D ON B.IdProduc = D.Id JOIN
	                        ADM.Libreria e ON e.IdGrupo = 3 AND E.IdOrden = 2 AND E.IdLibrer= A.Tipo JOIN
	                        ADM.Libreria F ON F.IdGrupo = 4 AND F.IdOrden = 1 AND F.IdLibrer= A.Placa JOIN
	                        COM.CompraIng_02 G ON A.Placa = G.IdLibreria JOIN
	                        INV.Almacen H ON A.IdAlmacen = H.Id JOIN
	                        ADM.Libreria I ON I.IdGrupo = 4 AND I.IdOrden = 4 AND I.IdLibrer= A.Recibido
                        WHERE 
	                        A.TipoCompra = 2 AND A.Estado <> -1  AND   ");
                List<SqlParameter> lPars = new List<SqlParameter>();
                if (fcompraIngreso.Id != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.id IN ({0}) AND   ", fcompraIngreso.Id));
                }
                if (fcompraIngreso.IdProveedor != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("c.id IN ({0}) AND   ", fcompraIngreso.IdProveedor));
                }
                if (fcompraIngreso.TipoCategoria != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("A.Tipo IN ({0}) AND   ", fcompraIngreso.TipoCategoria));
                }
                if (fcompraIngreso.fechaDesde.HasValue && fcompraIngreso.fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("a.FechaRec between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime, 0, fcompraIngreso.fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime, 0, fcompraIngreso.fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59)));
                }
                else if (fcompraIngreso.fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fcompraIngreso.fechaDesde.Value));
                }
                else if (fcompraIngreso.fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fcompraIngreso.fechaHasta.Value.AddDays(1)));
                }
                if (fcompraIngreso.estadoCompra == (int)ENEstado.TODOS)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.Estado IN ({0},{1}) AND   ", (int)ENEstado.GUARDADO, (int)ENEstado.COMPLETADO));
                }
                else
                {
                    sb.Append(string.Format("a.Estado IN ({0}) AND   ", fcompraIngreso.estadoCompra));
                }
                sb.Length -= 7;
                sb.Append(@"ORDER BY a.Id ASC");
                return tabla = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoResultado(FCompraIngreso fcompraIngreso)
        {
            try
            {
                DataTable tabla = new DataTable();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT
                            --ENCABEZADO 
	                            A.Id,
	                            A.NumNota,
	                            A.FechaRec,
	                            A.FechaEnt,
	                            H.Descrip as Almacen,
	                            a.TotalMaple TotalMapleEncabezado,
	                            a.Total as TotalEncabezado,
                            --PLACA
	                            A.Entregado,
	                            F.Descrip AS Placa,
                            --Proveedor
	                            c.id as IdProveedor,
	                            c.Descrip  as Proveedor,
                            --Tipo Categoria
	                            E.Descrip  AS TipoCategoria,
	                            i.Descrip as RecibidoPor,
                            --Tipo Compra
	                            a.TipoCompra,
	                            (IIF(A.TipoCompra = 1,'CON SELECCION','SIN SELECCION')) AS TipoCompraDescripcion,
                            --Detalle
	                            b.Id as IdDetalle,
	                            d.Descrip as Producto,	
	                            b.TotalCant,
	                            b.PrecioCost,
	                            b.Total as totalDetalle,
	                            b.TotalMaple as TotalMapleDetalle,
                            --Devolucion
	                            j.Id as IdDevolucion,
	                            K.Descrip as ProductoDevolucion,	
	                            j.TotalCant AS TotalCantDevolucion,
	                            j.PrecioCost AS PrecioCostDevolucion,
	                            j.Total AS TotalDevolucion,
	                            j.TotalMaple as TotalMapleDevolucion,
                            --Resultado
	                            (b.TotalCant - j.TotalCant) as TotalCantResultado,
	                            (b.Total - j.Total) as TotalResultado

                            FROM
	                            COM.CompraIng a JOIN
	                            COM.CompraIng_01 b ON A.Id = B.IdCompra JOIN 
	                            COM.Proveed c ON A.IdProvee = C.Id JOIN
	                            REG.Producto D ON B.IdProduc = D.Id JOIN
	                            ADM.Libreria e ON e.IdGrupo = 3 AND E.IdOrden = 2 AND E.IdLibrer= A.Tipo JOIN
	                            ADM.Libreria F ON F.IdGrupo = 4 AND F.IdOrden = 1 AND F.IdLibrer= A.Placa JOIN
	                            COM.CompraIng_02 G ON A.Placa = G.IdLibreria JOIN
	                            INV.Almacen H ON A.IdAlmacen = H.Id JOIN
	                            ADM.Libreria I ON I.IdGrupo = 4 AND I.IdOrden = 4 AND I.IdLibrer= A.Recibido JOIN
	                            COM.CompraIng_03 J ON A.Id = J.IdCompra  join
	                            REG.Producto K ON J.IdProduc = K.ID and j.IdProduc = b.IdProduc 
                            WHERE 
	                             A.Estado <> -1   AND   ");
                List<SqlParameter> lPars = new List<SqlParameter>();
                if (fcompraIngreso.Id != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.id IN ({0}) AND   ", fcompraIngreso.Id));
                }
                if (fcompraIngreso.IdProveedor != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("c.id IN ({0}) AND   ", fcompraIngreso.IdProveedor));
                }
                if (fcompraIngreso.TipoCategoria != 0)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("A.Tipo IN ({0}) AND   ", fcompraIngreso.TipoCategoria));
                }
                if (fcompraIngreso.fechaDesde.HasValue && fcompraIngreso.fechaHasta.HasValue) //Consulta por rango de fecha 
                {
                    sb.Append(string.Format("a.FechaRec between {0} and {1} AND   ", "@fechaDesde", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("fechaDesde", SqlDbType.DateTime, 0, fcompraIngreso.fechaDesde.Value));
                    lPars.Add(BD.CrearParametro("fechaHasta", SqlDbType.DateTime, 0, fcompraIngreso.fechaHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59)));
                }
                else if (fcompraIngreso.fechaDesde.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec >= {0} AND   ", "@fechaDesde"));
                    lPars.Add(BD.CrearParametro("@fechaDesde", SqlDbType.Date, 0, fcompraIngreso.fechaDesde.Value));
                }
                else if (fcompraIngreso.fechaHasta.HasValue) //Consulta por fecha especifica
                {
                    sb.Append(string.Format("a.FechaRec <= {0} AND   ", "@fechaHasta"));
                    lPars.Add(BD.CrearParametro("@fechaHasta", SqlDbType.Date, 0, fcompraIngreso.fechaHasta.Value.AddDays(1)));
                }
                if (fcompraIngreso.estadoCompra == (int)ENEstado.TODOS)
                {
                    //Consulta para mostrar Con seleccion y Sin seleccion
                    sb.Append(string.Format("a.Estado IN ({0},{1}) AND   ", (int)ENEstado.GUARDADO, (int)ENEstado.COMPLETADO));
                }
                else
                {
                    sb.Append(string.Format("a.Estado IN ({0}) AND   ", fcompraIngreso.estadoCompra));
                }
                sb.Length -= 7;
                sb.Append(@"ORDER BY a.Id ASC");
                return tabla = BD.EjecutarConsulta(sb.ToString(), lPars.ToArray()).Tables[0];
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
        public bool ExisteEnDevolucion(int idCompraIng)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = (from a in db.CompraIng
                                     join b in db.CompraIng_03 on a.Id equals b.IdCompra
                                     where a.Id.Equals(idCompraIng) && b.Estado != -1
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
