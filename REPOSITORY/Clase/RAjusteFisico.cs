using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Ajuste.Report;
using ENTITY.inv.Ajuste.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RAjusteFisico : BaseConexion, IAjusteFisico
    {
        #region Ajuste
        #region Transacciones
        public void Guardar(VAjusteFisico ajuste, ref int id, string usuario)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Ajuste data;
                    if (id > 0)
                    {
                        data = db.Ajuste.Where(a => a.Id == idAux).FirstOrDefault();
                        if (data == null)
                            throw new Exception("No existe el ajuste con id " + idAux);
                    }
                    else
                    {
                        data = new Ajuste();
                        db.Ajuste.Add(data);                       
                    }                  
                    data.IdConcepto = ajuste.IdConcepto;
                    data.IdAlmacen = ajuste.IdAlmacen;
                    data.Estado = ajuste.Estado;
                    data.IdCliente = ajuste.IdCliente;
                    data.FechaReg = ajuste.FechaReg;
                    data.TransportadorPor = ajuste.TransportadorPor;
                    data.Observacion = ajuste.Observacion;
                    data.Fecha = DateTime.Today;
                    data.Hora = DateTime.Now.ToString("HH:mm");
                    data.Usuario = usuario;
                    db.SaveChanges();
                    id = data.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar(int IdAjuste)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var ajuste = db.Ajuste.Where(c => c.Id == IdAjuste).FirstOrDefault();
                    if (ajuste == null)
                    {
                        throw new Exception("No se encontro el registro");
                    }
                    ajuste.Estado = (int)ENEstado.ELIMINAR;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        private static readonly Expression<Func<Ajuste, VAjusteLista>> CONVERT_LIST_VALUE = (item) => new VAjusteLista()
        {
            Id = item.Id,
            Fecha = item.Fecha,
            NConcepto = item.TCI001.cpdesc,
            Obs = item.Observacion,
            NAlmacen = item.Almacen.Descrip + " | Sucursal : " + item.Almacen.Sucursal.Descrip,
            IdTransportadoPor = item.TransportadorPor,
            IdCliente = item.IdCliente
        };

        public List<VAjusteLista> Lista()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.Ajuste
                        .Where(a => db.TCI001.Where(z => z.cptipo == (int)ENConcepto.CONCEPTO_TIPO_AJUSTE && a.Estado != (int)ENEstado.ELIMINAR)
                        .Select(z => z.cpnumi)
                        .Contains(a.IdConcepto))
                        .OrderByDescending(a => a.Id);
                    return query.Select(CONVERT_LIST_VALUE).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public VAjusteFisico ObtenerPorId(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var query = db.Ajuste
                        .Where(a => a.Id == id)
                        .Select(a => new VAjusteFisico
                        {
                            Id = a.Id,
                            Fecha = a.Fecha,
                            IdConcepto = a.IdConcepto,
                            IdAlmacen = a.IdAlmacen,
                            IdCliente= a.IdCliente,
                            Estado = a.Estado,
                            TransportadorPor = a.TransportadorPor,
                            Observacion = a.Observacion,
                        }).FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VAjusteTicket> ReporteAjuste(int ajusteId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Report_AjusteFisico
                        .Where(x => x.AjusteId == ajusteId)
                        .Select(ti => new VAjusteTicket
                        {
                            AjusteId = ti.AjusteId,
                            FechaReg = ti.FechaReg,
                            concepto = ti.concepto,
                            almDestino = ti.almDestino,
                            alamcen = ti.alamcen,
                            RecibidoPor = ti.RecibidoPor, 
                            Cliente = ti.Cliente,
                            TransportadoPor = ti.TransportadoPor,
                            detalleId = ti.detalleId,
                            Producto = ti.Producto,
                            Diferencia = ti.Diferencia,
                            Precio = ti.Precio,
                            Total = ti.Total
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
        #endregion

        #region AjusteDetalle
        #region Transacciones
        public void GuardarDetalle(List<VAjusteFisicoProducto> detalle, int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    AjusteProducto data;             
                    foreach (var item in detalle)
                    {
                        switch (item.Estado)
                        {
                            case (int)ENEstado.NUEVO:
                                data = new AjusteProducto();                            
                                data.IdAjuste = id;
                                data.IdProducto = item.IdProducto;
                                data.Estado = (int)ENEstado.GUARDADO;
                                data.Saldo = item.Saldo;
                                data.Fisico = item.Fisico;
                                data.Diferencia = item.Diferencia;
                                data.Precio = item.Precio;
                                data.Total = item.Total;
                                data.Lote = item.Lote;
                                data.FechaVen = item.FechaVen;
                                db.AjusteProducto.Add(data);
                                db.SaveChanges();
                                break;
                            case (int)ENEstado.MODIFICAR:
                                data = db.AjusteProducto.Where(a => a.Id == item.Id).FirstOrDefault();
                                data.Saldo = item.Saldo;
                                data.Fisico = item.Fisico;
                                data.Diferencia = item.Diferencia;
                                data.Total = item.Total;
                                data.Lote = item.Lote;
                                data.FechaVen = item.FechaVen;
                                break;
                            case (int)ENEstado.ELIMINAR:
                                data = db.AjusteProducto.Where(a => a.Id == item.Id).FirstOrDefault();                             
                                db.AjusteProducto.Remove(data);
                                break;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        public List<VAjusteFisicoProducto> ListaDetalle(int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.AjusteProducto
                                      join b in db.Producto on a.IdProducto equals b.Id
                                      where a.IdAjuste == id
                                      select (new VAjusteFisicoProducto()
                                      {
                                          Id = a.Id,                                          
                                          IdAjuste = a.IdAjuste,
                                          IdProducto = b.Id,   
                                          CodProducto = b.IdProd,
                                          NProducto = b.Descrip,                                         
                                          Saldo = a.Saldo,
                                          Fisico = a.Fisico,
                                          Diferencia =a.Diferencia,
                                          Precio = a.Precio,
                                          Total = a.Total,                                      
                                          Lote = a.Lote,
                                          FechaVen = a.FechaVen,
                                          Estado = a.Estado
                                      })).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion
    }
}
