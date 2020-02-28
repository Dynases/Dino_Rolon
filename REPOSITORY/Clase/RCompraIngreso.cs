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

                    CompraIngreso.IdSucur = vCompraIngreso.IdSucur;
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

        #endregion
        #region Consulta
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
                                          IdSucur = a.IdSucur,
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
                                          TotalVendido =a.TotalVendido
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
	                                a.IdSucur
                                FROM 
	                                COM.CompraIng a JOIN
	                                COM.Proveed b ON b.Id = a.IdProvee ";
                return tabla = BD.EjecutarConsulta(consulta).Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
